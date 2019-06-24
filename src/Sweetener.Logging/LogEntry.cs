using System;

namespace Sweetener.Logging
{
    /// <summary>
    /// A class for creating new instances of <see cref="LogEntry{T}"/>. 
    /// </summary>
    public static class LogEntry
    {
        /// <summary>
        /// Creates a new instance of the <see cref="LogEntry{T}"/> structure with
        /// the specified level and message.
        /// </summary>
        /// <param name="level">The <see cref="LogLevel"/> associated with the <paramref name="value"/>.</param>
        /// <param name="value">The value to be logged.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="level"/> is unrecognized.</exception>
        public static LogEntry<T> Create<T>(LogLevel level, T value)
            => Create(DateTime.UtcNow, level, value);

        /// <summary>
        /// Creates a new instance of the <see cref="LogEntry{T}"/> structure with
        /// the specified level and message.
        /// </summary>
        /// <param name="timestamp">The timestamp when the log request was made.</param>
        /// <param name="level">The <see cref="LogLevel"/> associated with the <paramref name="value"/>.</param>
        /// <param name="value">The value to be logged.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="level"/> is unrecognized.</exception>
        public static LogEntry<T> Create<T>(DateTime timestamp, LogLevel level, T value)
        {
            if (level < LogLevel.Trace || level > LogLevel.Fatal)
                throw new ArgumentOutOfRangeException(nameof(level), $"Unknown {nameof(LogLevel)} value '{level}'");

            return new LogEntry<T>(timestamp, level, value);
        }
    }
}
