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
        public Queue<string> LogQueue { get; } = new Queue<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryTemplateLogger"/> class for the current
        /// culture that logs all levels.
        /// </summary>
        public MemoryTemplateLogger()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryTemplateLogger"/> class with a minimum
        /// logging level for the current culture.
        /// </summary>
        /// <param name="minimumLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minimumLevel"/> is unrecognized.</exception>
        public MemoryTemplateLogger(LogLevel minimumLevel)
            : base(minimumLevel)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryTemplateLogger"/> class with a minimum
        /// logging level and an <see cref="IFormatProvider"/> object for a specific culture.
        /// </summary>
        /// <remarks>
        /// If <paramref name="formatProvider"/> is <c>null</c>, the formatting of the current culture is used.
        /// </remarks>
        /// <param name="minimumLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <param name="formatProvider">An <see cref="IFormatProvider"/> object for a specific culture.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minimumLevel"/> is unrecognized.</exception>
        public MemoryTemplateLogger(LogLevel minimumLevel, IFormatProvider formatProvider)
            : base(minimumLevel, formatProvider)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryTemplateLogger"/> class with a minimum
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
        public MemoryTemplateLogger(LogLevel minimumLevel, IFormatProvider formatProvider, string template)
            : base(minimumLevel, formatProvider, template)
        {  }

        /// <summary>
        /// Writes the message to the <see cref="LogQueue"/>.
        /// </summary>
        /// <param name="message">The value to be logged.</param>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is <c>null</c>.</exception>
        protected override void WriteLine(string message)
            => LogQueue.Enqueue(message);
    }
}
