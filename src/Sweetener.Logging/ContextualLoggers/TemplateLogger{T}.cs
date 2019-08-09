﻿using System;
using System.Globalization;

namespace Sweetener.Diagnostics.Logging
{
    /// <summary>
    /// A <see cref="Logger{T}"/> whose messages are enriched with both standard and
    /// domain-specific contextual information through the use of user-defined message templates.
    /// This class is abstract.
    /// </summary>
    /// <typeparam name="T">The type of the domain-specific context.</typeparam>
    public abstract class TemplateLogger<T> : Logger<T>
    {
        /// <summary>
        /// Gets an object that controls formatting. 
        /// </summary>
        /// <value>
        /// An <see cref="IFormatProvider"/> object for a specific culture, or the
        /// formatting of the current culture if no other culture is specified.
        /// </value>
        public override IFormatProvider FormatProvider { get; }

        /// <summary>
        /// Gets the minimum level of log requests that will be fulfilled.
        /// </summary>
        /// <value>
        /// The minimum <see cref="LogLevel"/> that will be fulfilled by the <see cref="TemplateLogger{T}"/>;
        /// any log request with a <see cref="LogLevel"/> below the minimum will be ignored.
        /// </value>
        public override LogLevel MinLevel { get; }

        internal ILogEntryTemplate<T> Template { get; }

        internal const string DefaultTemplate = "[{ts:O}] [{level:F}] {cxt} {msg}";

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateLogger{T}"/> class for the
        /// current culture that fulfills all logging requests using a default template.
        /// </summary>
        protected TemplateLogger()
            : this(LogLevel.Trace)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateLogger{T}"/> class for the
        /// current culture that fulfills all logging requests above a specified minimum
        /// <see cref="LogLevel"/> using a default template.
        /// </summary>
        /// <param name="minLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minLevel"/> is an unknown value.</exception>
        protected TemplateLogger(LogLevel minLevel)
            : this(minLevel, DefaultTemplate)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateLogger{T}"/> class for the
        /// current culture that fulfills all logging requests above a specified minimum
        /// <see cref="LogLevel"/> using a custom template.
        /// </summary>
        /// <param name="minLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <param name="template">A format string that describes the layout of each log entry.</param>
        /// <exception cref="ArgumentNullException"><paramref name="template"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minLevel"/> is an unknown value.</exception>
        /// <exception cref="FormatException">The <paramref name="template"/> is not formatted correctly.</exception>
        protected TemplateLogger(LogLevel minLevel, string template)
            : this(minLevel, null, template)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateLogger{T}"/> class for a
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
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minLevel"/> is an unknown value.</exception>
        /// <exception cref="FormatException">The <paramref name="template"/> is not formatted correctly.</exception>
        protected TemplateLogger(LogLevel minLevel, IFormatProvider formatProvider, string template)
        {
            if (minLevel < LogLevel.Trace || minLevel > LogLevel.Fatal)
                throw new ArgumentOutOfRangeException(nameof(minLevel), $"Unknown {nameof(LogLevel)} value '{minLevel}'");

            if (formatProvider == null)
                formatProvider = CultureInfo.CurrentCulture;

            FormatProvider = formatProvider;
            MinLevel       = minLevel;
            Template       = new TemplateBuilder(template).Build<T>();
        }

        /// <summary>
        /// Adds the entry to the log.
        /// </summary>
        /// <param name="logEntry">A <see cref="LogEntry"/> which consists of the message and its context.</param>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        protected override void Add(LogEntry<T> logEntry)
            => WriteLine(Template.Format(FormatProvider, logEntry));

        /// <summary>
        /// When overridden in a derived class, writes the <paramref name="message"/> to the log.
        /// </summary>
        /// <param name="message">The value to be written.</param>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        protected abstract void WriteLine(string message);
    }
}
