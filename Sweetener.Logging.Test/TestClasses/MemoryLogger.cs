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
        public Queue<LogEntry<string>> Entries { get; } = new Queue<LogEntry<string>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryLogger"/> class for the current
        /// culture that fulfills all logging requests.
        /// </summary>
        public MemoryLogger()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryLogger"/> class for the current
        /// culture that fulfills all logging requests above a specified minimum
        /// <see cref="LogLevel"/>
        /// </summary>
        /// <param name="minLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minLevel"/> is unrecognized.</exception>
        public MemoryLogger(LogLevel minLevel)
            : base(minLevel)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryLogger"/> class for a particular
        /// culture that fulfills all logging requests above a specified minimum
        /// <see cref="LogLevel"/>
        /// </summary>
        /// <remarks>
        /// If <paramref name="formatProvider"/> is <see langword="null"/>, the formatting of the current culture is used.
        /// </remarks>
        /// <param name="minLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <param name="formatProvider">An <see cref="IFormatProvider"/> object for a specific culture.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minLevel"/> is unrecognized.</exception>
        public MemoryLogger(LogLevel minLevel, IFormatProvider formatProvider)
            : base(minLevel, formatProvider)
        { }

        /// <summary>
        /// Logs the specified entry to the <see cref="Entries"/>.
        /// </summary>
        /// <param name="logEntry">A log entry which consists of the message and its context.</param>
        protected internal override void Log(LogEntry<string> logEntry)
            => Entries.Enqueue(logEntry);
    }
}
