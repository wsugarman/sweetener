using System;
using System.Collections.Generic;

namespace Sweetener.Logging.Test
{
    /// <summary>
    /// A <see cref="Logger"/> implementation used for testing.
    /// </summary>
    public class MemoryLogger : Logger
    {
        /// <summary>
        /// A <see cref="Queue{T}"/> of written log entries.
        /// </summary>
        public Queue<LogEntry<string>> LogQueue { get; } = new Queue<LogEntry<string>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryLogger"/> class for the current
        /// culture that logs all levels.
        /// </summary>
        public MemoryLogger()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryLogger"/> class with a minimum
        /// logging level for the current culture.
        /// </summary>
        /// <param name="minimumLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minimumLevel"/> is unrecognized.</exception>
        public MemoryLogger(LogLevel minimumLevel)
            : base(minimumLevel)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryLogger"/> class with a minimum
        /// logging level and an <see cref="IFormatProvider"/> object for a specific culture.
        /// </summary>
        /// <remarks>
        /// If <paramref name="formatProvider"/> is <c>null</c>, the formatting of the current culture is used.
        /// </remarks>
        /// <param name="minimumLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <param name="formatProvider">An <see cref="IFormatProvider"/> object for a specific culture.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minimumLevel"/> is unrecognized.</exception>
        public MemoryLogger(LogLevel minimumLevel, IFormatProvider formatProvider)
            : base(minimumLevel, formatProvider)
        { }

        /// <summary>
        /// Logs the specified entry to the <see cref="LogQueue"/>.
        /// </summary>
        /// <param name="logEntry">A log entry which consists of the message and its context.</param>
        protected internal override void Log(LogEntry<string> logEntry)
            => LogQueue.Enqueue(logEntry);
    }
}
