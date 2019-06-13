using System;
using System.Collections.Generic;

namespace Sweetener.Logging.Test
{
    /// <summary>
    /// A <see cref="TemplateLogger"/> implementation used for testing.
    /// </summary>
    public class MemoryTemplateLogger : TemplateLogger
    {
        /// <summary>
        /// A <see cref="Queue{T}"/> of written log messages.
        /// </summary>
        public Queue<string> Entries { get; } = new Queue<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryTemplateLogger"/> class for the
        /// current culture that fulfills all logging requests using a default template.
        /// </summary>
        public MemoryTemplateLogger()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryTemplateLogger"/> class for the
        /// current culture that fulfills all logging requests above a specified minimum
        /// <see cref="LogLevel"/> using a default template.
        /// </summary>
        /// <param name="minLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minLevel"/> is unrecognized.</exception>
        public MemoryTemplateLogger(LogLevel minLevel)
            : base(minLevel)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryTemplateLogger"/> class for the
        /// current culture that fulfills all logging requests above a specified minimum
        /// <see cref="LogLevel"/> using a custom template.
        /// </summary>
        /// <param name="minLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <param name="template">A format string that describes the layout of each log entry.</param>
        /// <exception cref="ArgumentNullException"><paramref name="template"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minLevel"/> is unrecognized.</exception>
        /// <exception cref="FormatException">The <paramref name="template"/> is not formatted correctly.</exception>
        public MemoryTemplateLogger(LogLevel minLevel, string template)
            : base(minLevel, template)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryTemplateLogger"/> class for a
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
        public MemoryTemplateLogger(LogLevel minLevel, IFormatProvider formatProvider, string template)
            : base(minLevel, formatProvider, template)
        { }

        /// <summary>
        /// Writes the message to the <see cref="Entries"/>.
        /// </summary>
        /// <param name="message">The value to be logged.</param>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is <see langword="null"/>.</exception>
        protected override void WriteLine(string message)
            => Entries.Enqueue(message);
    }
}
