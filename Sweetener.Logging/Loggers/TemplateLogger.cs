using System;

namespace Sweetener.Logging
{
    /// <summary>
    /// A <see cref="Logger"/> whose messages are enriched with contextual information
    /// through the use of user-defined templates.
    /// </summary>
    public abstract class TemplateLogger : Logger
    {
        internal const string DefaultTemplate = "[{ts:O}] [{level:F}] {msg}";

        internal readonly ILogEntryTemplate<string> _template;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateLogger"/> class for the
        /// current culture that fulfills all logging requests using a default template.
        /// </summary>
        protected TemplateLogger()
            : this(LogLevel.Trace)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateLogger"/> class for the
        /// current culture that fulfills all logging requests above a specified minimum
        /// <see cref="LogLevel"/> using a default template.
        /// </summary>
        /// <param name="minLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minLevel"/> is unrecognized.</exception>
        protected TemplateLogger(LogLevel minLevel)
            : this(minLevel, DefaultTemplate)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateLogger"/> class for the
        /// current culture that fulfills all logging requests above a specified minimum
        /// <see cref="LogLevel"/> using a custom template.
        /// </summary>
        /// <param name="minLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <param name="template">A format string that describes the layout of each log entry.</param>
        /// <exception cref="ArgumentNullException"><paramref name="template"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minLevel"/> is unrecognized.</exception>
        /// <exception cref="FormatException">The <paramref name="template"/> is not formatted correctly.</exception>
        protected TemplateLogger(LogLevel minLevel, string template)
            : this(minLevel, null, template)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateLogger"/> class for a
        /// particular culture that fulfills all logging requests above a specified minimum
        /// <see cref="LogLevel"/> using a custom template.
        /// </summary>
        /// <remarks>
        /// If <paramref name="formatProvider"/> is <see langword="null"/>, the formatting of the current culture is used.
        /// </remarks>
        /// <param name="minLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <param name="formatProvider">An <see cref="IFormatProvider"/> object for a specific culture.</param>
        /// <param name="template">A format string that describes the layout of each log entry.</param>
        /// <exception cref="ArgumentNullException"><paramref name="template"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minLevel"/> is unrecognized.</exception>
        /// <exception cref="FormatException">The <paramref name="template"/> is not formatted correctly.</exception>
        protected TemplateLogger(LogLevel minLevel, IFormatProvider formatProvider, string template)
            : base(minLevel, formatProvider)
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
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is <see langword="null"/>.</exception>
        protected abstract void WriteLine(string message);
    }
}
