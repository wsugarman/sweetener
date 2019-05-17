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
        /// A <see cref="Queue{T}"/> of written log messages.
        /// </summary>
        public Queue<string> Log { get; } = new Queue<string>();

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
        /// Initializes a new instance of the <see cref="MemoryLogger"/> class with a minimum
        /// logging level and an <see cref="IFormatProvider"/> object for a specific culture.
        /// </summary>
        /// <remarks>
        /// If <paramref name="formatProvider"/> is <c>null</c>, the formatting of the current culture is used.
        /// </remarks>
        /// <param name="minimumLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <param name="formatProvider">An <see cref="IFormatProvider"/> object for a specific culture.</param>
        /// <param name="pattern">A format string that describes the layout of each log entry.</param>
        /// <exception cref="ArgumentNullException"><paramref name="pattern"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minimumLevel"/> is unrecognized.</exception>
        /// <exception cref="FormatException">The <paramref name="pattern"/> is not formatted correctly.</exception>
        public MemoryLogger(LogLevel minimumLevel, IFormatProvider formatProvider, string pattern)
            : base(minimumLevel, formatProvider, pattern)
        {  }

        protected override void WriteLine(string message)
            => Log.Enqueue(message);
    }
}
