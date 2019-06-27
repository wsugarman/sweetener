using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Sweetener.Logging
{
    internal class TemplateBuilder
    {
        protected internal readonly string _template;
        protected internal readonly string _format;
        protected readonly Dictionary<TemplateParameter, int> _indices = new Dictionary<TemplateParameter, int>();

        public TemplateBuilder(string template)
        {
            _format   = ParseTemplate(template);
            _template = template;
        }

        public virtual ILogEntryTemplate Build()
        {
            if (_indices.ContainsKey(TemplateParameter.Context))
                throw new InvalidOperationException("Template cannot contain 'cxt' or 'context' parameter");

            if (!_indices.ContainsKey(TemplateParameter.Message))
                throw new InvalidOperationException("Template is missing required 'msg' or 'message' parameter");

            // The use of an interface will force the template (a struct) to box, but
            // it will provide a hook for polymorphism and further cement the relationship
            // between the builder and the template itself
            return new LogEntryTemplate(_template, _format);
        }

        public virtual ILogEntryTemplate<T> Build<T>()
        {
            if (!_indices.ContainsKey(TemplateParameter.Context))
                throw new InvalidOperationException("Template is missing required 'cxt' or 'context' parameter");

            if (!_indices.ContainsKey(TemplateParameter.Message))
                throw new InvalidOperationException("Template is missing required 'msg' or 'message' parameter");

            // The use of an interface will force the template (a struct) to box, but
            // it will provide a hook for polymorphism and further cement the relationship
            // between the builder and the template itself
            return new LogEntryTemplate<T>(_template, _format);
        }

        protected virtual int GetIndex(TemplateParameter parameter)
            => (int)parameter;

        private string ParseTemplate(string template)
        {
            if (template == null)
                throw new ArgumentNullException(nameof(template));

            StringBuilder builder = new StringBuilder();
            using (StringReader reader = new StringReader(template))
            {
                while (reader.Peek() != -1)
                {
                    char c = (char)reader.Read();
                    if (c == '{')
                    {
                        if (reader.Peek() == '{')
                        {
                            // Escaped opened curly brace
                            builder.Append(c);
                            builder.Append((char)reader.Read());
                        }
                        else
                        {
                            builder.Append(ReadFormatItem(reader).ToString());

                            // Check but don't append -- we already appended it!
                            if (reader.Read() != '}')
                                throw new FormatException("Input string was not in a correct format");
                        }
                    }
                    else if (c == '}')
                    {
                        if (reader.Peek() != '}')
                            throw new FormatException("Input string was not in a correct format");

                        // Escaped closed curly brace
                        builder.Append(c);
                        builder.Append((char)reader.Read());
                    }
                    else
                    {
                        builder.Append(c);
                    }
                }
            }

            return builder.ToString();
        }

        private FormatItem ReadFormatItem(TextReader reader)
        {
            TemplateParameter parameter = ReadTemplateParameter(reader);
            if (!_indices.TryGetValue(parameter, out int index))
            {
                index = GetIndex(parameter);
                _indices[parameter] = index;
            }

            // Create the FormatItem for the parameter
            FormatItem formatItem = new FormatItem(
                index,
                ReadAlignment(reader),
                ReadFormat   (reader));

            // Perform some validation on the template parameter's format if possible
            string compositeFormat = formatItem.ToString(0);
            switch (parameter)
            {
                // DateTime
                case TemplateParameter.Timestamp:
                    string.Format(compositeFormat, DateTime.UtcNow);
                    break;
                // Enum
                case TemplateParameter.Level:
                    string.Format(compositeFormat, LogLevel.Error);
                    break;
                // Integer
                case TemplateParameter.ProcessId:
                case TemplateParameter.ThreadId:
                    string.Format(compositeFormat, 42);
                    break;
                default:
                    break;
            }

            return formatItem;
        }

        private TemplateParameter ReadTemplateParameter(TextReader reader)
        {
            string name = null;
            StringBuilder nameBuilder = new StringBuilder();

            int next;
            while ((next = reader.Peek()) != -1)
            {
                char c = (char)next;

                // Format Item indices allow leading and trailing white space, so the
                // template names also allow white space inside the format item
                if (char.IsLetter(c) && name == null)
                {
                    // Currently all parameter names consist of letters
                    nameBuilder.Append(c);
                }
                else if (c == ' ')
                {
                    if (name == null)
                        name = nameBuilder.ToString();
                }
                else if (c == ',' || c == ':' || c == '}')
                {
                    name = nameBuilder.ToString();
                    break;
                }
                else
                {
                    // Unexpected character!
                    name = null;
                    break;
                }

                // Move onto the next character
                reader.Read();
            }

            // Did we reach the end without breaking or was there an unexpected character?
            if (name == null)
                throw new FormatException("Input string was not in a correct format");

            return TemplateParameterName.Parse(name);
        }

        private int? ReadAlignment(TextReader reader)
        {
            // Read the alignment between the comma ',' and either the colon ':' or closed curly brace '}'
            if (reader.Peek() == ',')
            {
                StringBuilder alignmentBuilder = new StringBuilder();

                int next;
                reader.Read(); // Move past the comma
                while ((next = reader.Peek()) != -1)
                {
                    // There are no recognized escape sequences in the alignment
                    if (next == ':' || next == '}')
                        break;

                    alignmentBuilder.Append((char)reader.Read());
                }

                return int.Parse(alignmentBuilder.ToString());
            }

            return null;
        }

        private string ReadFormat(TextReader reader)
        {
            // Read the format between the comma ':' and the closed curly brace '}'
            if (reader.Peek() == ':')
            {
                StringBuilder formatBuilder = new StringBuilder();

                int next;
                reader.Read(); // Move past the colon
                while ((next = reader.Peek()) != -1)
                {
                    // '}' cannot be escaped, so there is no need to special-case escape sequences
                    if (next == '}')
                        break;

                    formatBuilder.Append((char)reader.Read());
                }

                return formatBuilder.ToString();
            }

            return null;
        }

        #region LogEntryTemplates

        private readonly struct LogEntryTemplate : ILogEntryTemplate
        {
            private readonly string _format;
            private readonly string _template;

            private static readonly Process s_currentProcess = Process.GetCurrentProcess();

            public LogEntryTemplate(string template, string format)
            {
                // TODO: While the originalTemplate is really bloat in production, it is
                //       very helpful to have when debugging and for unit testing.
                //       That said, is there a better mechanism that doesn't involve
                //       different code between debug/release configuration?
                _format   = format;
                _template = template;
            }

            public string Format(IFormatProvider provider, LogEntry logEntry)
            {
                // TODO: It seems silly to use the object[] override here when we avoid the
                //       object[] allocation in the ILogger interface. We should figure out
                //       how to avoid this allocation too if possible.
                // TODO: Can probably replace the process parameters with real values now
                return string.Format(provider, _format,
                    logEntry.Message,              // {0} - Message
                    null,                          // {1} - Context (Unused)
                    logEntry.Timestamp,            // {2} - Timestamp
                    logEntry.Level,                // {3} - LogLevel
                    s_currentProcess.Id,           // {4} - Process Id
                    s_currentProcess.ProcessName,  // {5} - Process Name
                    logEntry.ThreadId,             // {6} - Thread Id
                    logEntry.ThreadName);          // {7} - Thread Name
            }

            public override string ToString()
                => _template;
        }

        #endregion

        #region LogEntryTemplate<T>

        private readonly struct LogEntryTemplate<T> : ILogEntryTemplate<T>
        {
            private readonly string _format;
            private readonly string _template;

            private static readonly Process s_currentProcess = Process.GetCurrentProcess();

            public LogEntryTemplate(string template, string format)
            {
                // TODO: While the originalTemplate is really bloat in production, it is
                //       very helpful to have when debugging and for unit testing.
                //       That said, is there a better mechanism that doesn't involve
                //       different code between debug/release configuration?
                _format   = format;
                _template = template;
            }

            public string Format(IFormatProvider provider, LogEntry<T> logEntry)
            {
                // TODO: It seems silly to use the object[] override here when we avoid the
                //       object[] allocation in the ILogger interface. We should figure out
                //       how to avoid this allocation too if possible.
                // TODO: Can probably replace the process parameters with real values now
                return string.Format(provider, _format,
                    logEntry.Message,              // {0} - Message
                    logEntry.Context,              // {1} - Context
                    logEntry.Timestamp,            // {2} - Timestamp
                    logEntry.Level,                // {3} - LogLevel
                    s_currentProcess.Id,           // {4} - Process Id
                    s_currentProcess.ProcessName,  // {5} - Process Name
                    logEntry.ThreadId,             // {6} - Thread Id
                    logEntry.ThreadName);          // {7} - Thread Name
            }

            public override string ToString()
                => _template;
        }

        #endregion
    }
}
