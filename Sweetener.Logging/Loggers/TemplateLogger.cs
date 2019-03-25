using System;

namespace Sweetener.Logging
{
    /// <summary>
    /// A <see cref="Logger"/> implementation that writes log entries as templated strings.
    /// </summary>
    public abstract class TemplateLogger : Logger
    {
        internal const string DefaultTemplate = "[{ts:O}] [{level:F}] {msg}";

        internal readonly ILogEntryTemplate<string> _template;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateLogger"/> class for the
        /// current culture that logs all levels.
        /// </summary>
        protected TemplateLogger()
            : this(LogLevel.Trace, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateLogger"/> class with a
        /// minimum logging level for the current culture.
        /// </summary>
        /// <param name="minimumLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minimumLevel"/> is unrecognized.</exception>
        protected TemplateLogger(LogLevel minimumLevel)
            : this(minimumLevel, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateLogger"/> class with a minimum
        /// logging level and an <see cref="IFormatProvider"/> object for a specific culture.
        /// </summary>
        /// <remarks>
        /// If <paramref name="formatProvider"/> is <c>null</c>, the formatting of the current culture is used.
        /// </remarks>
        /// <param name="minimumLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <param name="formatProvider">An <see cref="IFormatProvider"/> object for a specific culture.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minimumLevel"/> is unrecognized.</exception>
        protected TemplateLogger(LogLevel minimumLevel, IFormatProvider formatProvider)
            : this(minimumLevel, formatProvider, DefaultTemplate)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateLogger"/> class with a minimum
        /// logging level and an <see cref="IFormatProvider"/> object for a specific culture.
        /// </summary>
        /// <remarks>
        /// If <paramref name="formatProvider"/> is <c>null</c>, the formatting of the current culture is used.
        /// </remarks>
        /// <param name="minimumLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <param name="formatProvider">An <see cref="IFormatProvider"/> object for a specific culture.</param>
        /// <param name="template">A format string that describes the layout of each log entry.</param>
        /// <exception cref="ArgumentNullException"><paramref name="template"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minimumLevel"/> is unrecognized.</exception>
        /// <exception cref="FormatException">The <paramref name="template"/> is not formatted correctly.</exception>
        protected TemplateLogger(LogLevel minimumLevel, IFormatProvider formatProvider, string template)
            : base(minimumLevel, formatProvider)
        {
            TemplateBuilder templateBuilder = new TemplateBuilder(template);
            _template = templateBuilder.Build<string>();
        }

        /// <summary>
        /// Logs the specified entry.
        /// </summary>
        /// <param name="logEntry">A log entry which consists of the message and its context.</param>
        protected internal override void Log(LogEntry<string> logEntry)
            => WriteLine(_template.Format(FormatProvider, logEntry));

        /// <summary>
        /// Writes the message to the log.
        /// </summary>
        /// <param name="message">The value to be logged.</param>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is <c>null</c>.</exception>
        protected abstract void WriteLine(string message);
    }
}
