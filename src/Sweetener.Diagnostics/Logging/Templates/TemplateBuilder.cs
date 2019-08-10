using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Sweetener.Diagnostics.Logging
{
    internal partial class TemplateBuilder
    {
        protected internal readonly string _format;
        protected internal readonly string _template;
        protected readonly Dictionary<TemplateParameter, int> _indices = new Dictionary<TemplateParameter, int>();

        public TemplateBuilder(string template)
        {
            _format   = ParseTemplate(template);
            _template = template;
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
    }
}
