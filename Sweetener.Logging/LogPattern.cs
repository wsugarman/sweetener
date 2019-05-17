using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Sweetener.Logging
{
    /// <summary>
    /// Encapsulates the logic for parsing and formatting log entry patterns.
    /// </summary>
    internal class LogPattern
    {
        /// <summary>
        /// The internal composite format string used by the <see cref="LogPattern"/>.
        /// </summary>
        internal readonly string CompositeFormatString;

        /// <summary>
        /// The default log layout pattern.
        /// </summary>
        public const string Default = "[{ts:O}] [{level:F}] {msg}";

        /// <summary>
        /// Initializes a new instance of the <see cref="LogPattern"/> class based on
        /// a string layout of each log entry.
        /// </summary>
        /// <param name="pattern">A format string that describes the layout of each log entry.</param>
        /// <exception cref="ArgumentNullException"><paramref name="pattern"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The <paramref name="pattern"/> is not formatted correctly.</exception>
        public LogPattern(string pattern)
        {
            CompositeFormatString = Resolve(pattern);
        }

        /// <summary>
        /// A delegate that yields a log entry using a pre-defined composite string.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> object for a specific culture.</param>
        /// <param name="level">The level associated with the log entry.</param>
        /// <param name="message">The value requested for logging.</param>
        /// <returns>The resolved log entry based on the pattern.</returns>
        public string Format(IFormatProvider provider, LogLevel level, string message)
            => Format(provider, DateTime.UtcNow, level, message);

        /// <summary>
        /// A delegate that yields a log entry using a pre-defined composite string.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> object for a specific culture.</param>
        /// <param name="timestamp">The time associated with the log entry.</param>
        /// <param name="level">The level associated with the log entry.</param>
        /// <param name="message">The value requested for logging.</param>
        /// <returns>The resolved log entry based on the pattern.</returns>
        internal string Format(IFormatProvider provider, DateTime timestamp, LogLevel level, string message)
        {
            // TODO: We should only retrieve process and thread information if it is going
            //       to be logged as part of the format string.
            Process currentProcess = Process.GetCurrentProcess();
            Thread  currentThread  = Thread.CurrentThread;

            // TODO: It seems silly to use the object[] override here when we avoid the
            //       object[] allocation in the ILogger interface. We should figure out
            //       how to avoid this allocation too if possible.
            return string.Format(provider, CompositeFormatString,
                message,                       // {0} - Message
                timestamp,                     // {1} - Timestamp
                level,                         // {2} - LogLevel
                currentProcess.Id,             // {3} - Process Id
                currentProcess.ProcessName,    // {4} - Process Name
                currentThread.ManagedThreadId, // {5} - Thread Id
                currentThread.Name);           // {6} - Thread Name
        }

        private static string Resolve(string pattern)
        {
            if (pattern == null)
                throw new ArgumentNullException(nameof(pattern));

            bool hasMessage = false;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < pattern.Length; i++)
            {
                char c = pattern[i];
                switch (c)
                {
                    case '{':
                        if (i + 1 < pattern.Length && pattern[i + 1] == '{')
                        {
                            // Escaped opened curly brace
                            builder.Append(pattern, i, 2);
                            i++;
                        }
                        else
                        {
                            PatternItem item = PatternItem.Parse(ref i, pattern);
                            if (item.Parameter == Parameter.Message)
                                hasMessage = true;

                            builder.Append(item);
                        }

                        break;
                    case '}':
                        if (i + 1 < pattern.Length && pattern[i + 1] == '}')
                        {
                            // Escaped closed curly brace
                            builder.Append(pattern, i, 2);
                            i++;
                        }
                        else
                        {
                            throw new FormatException("Input string was not in a correct format");
                        }

                        break;
                    default:
                        builder.Append(c);
                        break;
                }
            }

            if (!hasMessage)
                throw new FormatException("Pattern is missing the required message.");

            return builder.ToString();
        }

        /// <summary>
        /// Represents a format item used in a logger's layout pattern.
        /// </summary>
        private readonly struct PatternItem
        {
            /// <summary>
            /// The parameter for the item.
            /// </summary>
            /// <remarks>
            /// The numeric value of the <see cref="LogPattern.Parameter"/> also serves as the item index.
            /// </remarks>
            public readonly Parameter Parameter;

            /// <summary>
            /// The optional alignment for the item.
            /// </summary>
            public readonly string Alignment;

            /// <summary>
            /// The optional format for the item.
            /// </summary>
            public readonly string Format;

            /// <summary>
            /// Creates a new instance of the <see cref="PatternItem"/> structure.
            /// </summary>
            /// <param name="parameter">The type of parameter.</param>
            /// <param name="alignment">The optional alignment of the item.</param>
            /// <param name="format">The optional format of the item.</param>
            /// <exception cref="ArgumentOutOfRangeException">Unrecognized <paramref name="parameter"/> value.</exception>
            /// <exception cref="FormatException"><paramref name="format"/> is not in a correct format</exception>
            public PatternItem(Parameter parameter, string alignment, string format)
            {
                Parameter = parameter;
                Alignment = alignment;
                Format    = format;

                // Validate
                // TODO: We do want to check the validity of the formats aggressively and
                //       and catch problems upon construction rather than on the first
                //       logging call, but is there a better way to perform the validation?
                if (Format != null)
                {
                    // Use Index = 0 for the item when validating with an arbitrary value of the correct type
                    string compositeFormat = ToString(0);
                    switch (Parameter)
                    {
                        // Strings
                        case Parameter.Message:
                        case Parameter.ProcessName:
                        case Parameter.ThreadName:
                            string.Format(compositeFormat, "abc");
                            break;
                        // DateTime
                        case Parameter.Timestamp:
                            string.Format(compositeFormat, DateTime.UtcNow);
                            break;
                        // Enum
                        case Parameter.Level:
                            string.Format(compositeFormat, Parameter.Level);
                            break;
                        // Integer
                        case Parameter.ProcessId:
                        case Parameter.ThreadId:
                            string.Format(compositeFormat, 42);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(parameter));
                    }
                }
            }

            /// <summary>
            /// Gets the format string for the <see cref="LogPattern.Parameter"/>.
            /// </summary>
            /// <returns>The format string.</returns>
            public override string ToString()
                => ToString((int)Parameter);

            /// <summary>
            /// Parses a <see cref="PatternItem"/> from the given string starting
            /// at the index.
            /// </summary>
            /// <param name="i">The starting index that will be updated to the end of the item as it is parsed.</param>
            /// <param name="pattern">The string containing the <see cref="PatternItem"/>.</param>
            /// <returns>The parsed <see cref="PatternItem"/>.</returns>
            public static PatternItem Parse(ref int i, string pattern)
            {
                // Note: This struct is private, so no need to check the args' validity
                //       like ensuring i < pattern.Length or pattern[i] is initially '{'
                i++;

                Parameter parameter = ParseParameter(ref i, pattern);
                string    alignment = ParseAlignment(ref i, pattern);
                string    format    = ParseFormat   (ref i, pattern);

                return new PatternItem(parameter, alignment, format);
            }

            private string ToString(int index)
            {
                string suffix = string.Empty;
                if (Alignment != null)
                    suffix += $",{Alignment}";
                if (Format != null)
                    suffix += $":{Format}";

                return $"{{{index}{suffix}}}";
            }

            private static string ParseAlignment(ref int i, string pattern)
            {
                // Parse the format between the comma ',' and either the colon ':' or closed curly brace '}'
                if (pattern[i] == ',')
                {
                    int alignmentStart = ++i;
                    for (; i < pattern.Length && pattern[i] != ':' && pattern[i] != '}'; i++)
                    { }

                    if (i >= pattern.Length)
                        throw new FormatException("Input string was not in a correct format");

                    return pattern.Substring(alignmentStart, i - alignmentStart);
                }

                return null;
            }

            private static string ParseFormat(ref int i, string pattern)
            {
                // Parse the format between the colon ':' and the closed curly brace '}'
                if (pattern[i] == ':')
                {
                    int formatStart = ++i;
                    for (; i < pattern.Length && pattern[i] != '}'; i++)
                    { }

                    if (i >= pattern.Length)
                        throw new FormatException("Input string was not in a correct format");

                    return pattern.Substring(formatStart, i - formatStart);
                }

                return null;
            }

            private static Parameter ParseParameter(ref int i, string pattern)
            {
                // Need to keep track of the number of characters read because it is not
                // necessarily the same as the size of the name. Spaces are allowed before or after.
                int nameStart = -1, nameEnd = -1;
                for (; i < pattern.Length; i++)
                {
                    char c = pattern[i];
                    // There are currently no patterns with numbers enabled
                    if (char.IsLetter(c) && nameEnd == -1)
                    {
                        if (nameStart == -1)
                            nameStart = i;
                    }
                    else if (c == ' ')
                    {
                        if (nameStart != -1 && nameEnd == -1)
                            nameEnd = i;
                    }
                    else if (c == ',' || c == ':' || c == '}')
                    {
                        if (nameEnd == -1)
                            nameEnd = i;

                        break;
                    }
                    else
                    {
                        throw new FormatException("Input string was not in a correct format");
                    }
                }

                // Did we reach the end without breaking?
                if (i >= pattern.Length)
                    throw new FormatException("Input string was not in a correct format");

                string name = pattern.Substring(nameStart, nameEnd - nameStart);

                // TODO: Case insensitive?
                switch (name)
                {
                    case "msg":
                    case "message":
                        return Parameter.Message; 
                    case "ts":
                    case "timestamp":
                        return Parameter.Timestamp; 
                    case "l":
                    case "level":
                        return Parameter.Level;
                    case "pid":
                    case "processId":
                        return Parameter.ProcessId;
                    case "pn":
                    case "processName":
                        return Parameter.ProcessName;
                    case "tid":
                    case "threadId":
                        return Parameter.ThreadId;
                    case "tn":
                    case "threadName":
                        return Parameter.ThreadName;
                    default:
                        throw new FormatException($"Unrecognized pattern variable '{name}'.");
                }
            }
        }

        /// <summary>
        /// A known variable in the log layout pattern.
        /// </summary>
        public enum Parameter
        {
            /// <summary>
            /// The log message.
            /// </summary>
            Message = 0,

            /// <summary>
            /// The timestamp when the log request was made.
            /// </summary>
            /// <remarks>
            /// By default, the timestamp is UTC.
            /// </remarks>
            Timestamp = 1,

            /// <summary>
            /// The <see cref="LogLevel"/> associated with the log entry.
            /// </summary>
            Level = 2,

            /// <summary>
            /// The <see cref="Process.Id"/> of the process that made the log request.
            /// </summary>
            ProcessId = 3,

            /// <summary>
            /// The <see cref="Process.ProcessName"/> of the process that made the log request.
            /// </summary>
            ProcessName = 4,

            /// <summary>
            /// The <see cref="Thread.ManagedThreadId"/> of the thread that made the log request.
            /// </summary>
            ThreadId = 5,

            /// <summary>
            /// The <see cref="Thread.Name"/> of the thread that made the log request.
            /// </summary>
            ThreadName = 6,
        }
    }
}
