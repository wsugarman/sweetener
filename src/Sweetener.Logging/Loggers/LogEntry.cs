using System;
using System.Threading;

namespace Sweetener.Logging
{
    /// <summary>
    /// An entry to be written to the log.
    /// </summary>
    public readonly struct LogEntry
    {
        /// <summary>
        /// The <see cref="LogLevel"/> associated with the <see cref="Message"/>.
        /// </summary>
        public readonly LogLevel Level;

        /// <summary>
        /// The value to be logged.
        /// </summary>
        public readonly string Message;

        /// <summary>
        /// The timestamp when the log request was made.
        /// </summary>
        /// <remarks>
        /// The <see cref="DateTime.Kind"/> is <see cref="DateTimeKind.Utc"/>.
        /// </remarks>
        public readonly DateTime Timestamp;

        /// <summary>
        /// The <see cref="Thread.ManagedThreadId"/> of the thread that created the log entry.
        /// </summary>
        public readonly int ThreadId;

        /// <summary>
        /// The <see cref="Thread.Name"/> of the thread that created the log entry.
        /// </summary>
        public readonly string ThreadName;

        // TODO: Should process information be here? Thread information is here because
        //       the thread that requests a log be written and the the thread that performs
        //       the write operation may be different. Though, users could perhaps marshal
        //       entries across the app domain into another process as well.

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntry"/> structure that
        /// associates the specified message with additional contextual information.
        /// </summary>
        /// <param name="timestamp">The timestamp when the log request was made.</param>
        /// <param name="level">The <see cref="LogLevel"/> associated with the <see cref="Message"/>.</param>
        /// <param name="message">The value to be logged.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="level"/> is an unknown value.</exception>
        public LogEntry(DateTime timestamp, LogLevel level, string message)
        {
            if (level < LogLevel.Trace || level > LogLevel.Fatal)
                throw new ArgumentOutOfRangeException(nameof(level), $"Unknown {nameof(LogLevel)} value '{level}'");

            // Retrieving the current thread per entry is actually faster than attempting to
            // cache the value in a [ThreadStatic] variable and checking for initialization
            Thread currentThread = Thread.CurrentThread;

            Level      = level;
            Message    = message;
            ThreadId   = currentThread.ManagedThreadId;
            ThreadName = currentThread.Name;
            Timestamp  = timestamp;
        }

        // This ctor is called internally where we don't need additional checks
        internal LogEntry(LogLevel level, string message)
        {
            Thread currentThread = Thread.CurrentThread;

            Level      = level;
            Message    = message;
            ThreadId   = currentThread.ManagedThreadId;
            ThreadName = currentThread.Name;
            Timestamp  = DateTime.UtcNow;
        }
    }
}
