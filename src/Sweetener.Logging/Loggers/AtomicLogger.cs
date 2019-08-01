using System;

namespace Sweetener.Logging.Loggers
{
    /// <summary>
    /// Represents a client that can atomically log messages for a given <see cref="LogLevel"/>
    /// based on their purpose or severity.
    /// </summary>
    public abstract class AtomicLogger : Logger
    {
        /// <summary>
        /// When overridden in a derived class, attempts to atomically add the entry to the log.
        /// </summary>
        /// <param name="logEntry">A <see cref="LogEntry"/> which consists of the message and its context.</param>
        /// <returns>
        /// <see langword="true"/> if the <see cref="LogEntry"/> could be atomically added;
        /// otherwise <see langword="false"/>.
        /// </returns>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        protected abstract bool TryAdd(LogEntry logEntry);

        /// <summary>
        /// Attempts to atomically log the specified message with the given <see cref="LogLevel"/>.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/>
        /// is less than or equal to <paramref name="level"/>.
        /// </remarks>
        /// <param name="level">The <see cref="LogLevel"/> associated with the message.</param>
        /// <param name="message">The message to be logged.</param>
        /// <returns>
        /// <see langword="true"/> if the message could be atomically logged or the
        /// <paramref name="level"/> was below the <see cref="Logger.MinLevel"/>; otherwise
        /// <see langword="false"/>.
        /// </returns>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public virtual bool TryLog(LogLevel level, string message)
            => level >= MinLevel
                ? TryAdd(new LogEntry(level, message))
                : true;

        /// <summary>
        /// Attempts to atomically log the formatted message with the given <see cref="LogLevel"/>
        /// using the same semantics as the <see cref="string.Format(string, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/>
        /// is less than or equal to <paramref name="level"/>.
        /// </remarks>
        /// <param name="level">The <see cref="LogLevel"/> associated with the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">An object to format and log.</param>
        /// <returns>
        /// <see langword="true"/> if the message could be atomically logged or the
        /// <paramref name="level"/> was below the <see cref="Logger.MinLevel"/>; otherwise
        /// <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">
        /// <para><paramref name="format"/> is not a valid composite format string.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than 0 (zero), or greater than or equal to
        /// the number of objects to be formatted (which, for this method overload, is one).
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public virtual bool TryLog(LogLevel level, string format, object arg0)
            => level >= MinLevel
                ? TryAdd(new LogEntry(level, string.Format(FormatProvider, format, arg0)))
                : true;

        /// <summary>
        /// Attempts to atomically log the formatted message with the given <see cref="LogLevel"/>
        /// using the same semantics as the <see cref="string.Format(string, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/>
        /// is less than or equal to <paramref name="level"/>.
        /// </remarks>
        /// <param name="level">The <see cref="LogLevel"/> associated with the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format and log.</param>
        /// <param name="arg1">The second object to format and log.</param>
        /// <returns>
        /// <see langword="true"/> if the message could be atomically logged or the
        /// <paramref name="level"/> was below the <see cref="Logger.MinLevel"/>; otherwise
        /// <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">
        /// <para><paramref name="format"/> is not a valid composite format string.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than 0 (zero), or greater than or equal to
        /// the number of objects to be formatted (which, for this method overload, is two).
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public virtual bool TryLog(LogLevel level, string format, object arg0, object arg1)
            => level >= MinLevel
                ? TryAdd(new LogEntry(level, string.Format(FormatProvider, format, arg0, arg1)))
                : true;

        /// <summary>
        /// Attempts to atomically log the formatted message with the given <see cref="LogLevel"/>
        /// using the same semantics as the <see cref="string.Format(string, object, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/>
        /// is less than or equal to <paramref name="level"/>.
        /// </remarks>
        /// <param name="level">The <see cref="LogLevel"/> associated with the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format and log.</param>
        /// <param name="arg1">The second object to format and log.</param>
        /// <param name="arg2">The third object to format and log.</param>
        /// <returns>
        /// <see langword="true"/> if the message could be atomically logged or the
        /// <paramref name="level"/> was below the <see cref="Logger.MinLevel"/>; otherwise
        /// <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">
        /// <para><paramref name="format"/> is not a valid composite format string.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than 0 (zero), or greater than or equal to
        /// the number of objects to be formatted (which, for this method overload, is three).
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public virtual bool TryLog(LogLevel level, string format, object arg0, object arg1, object arg2)
            => level >= MinLevel
                ? TryAdd(new LogEntry(level, string.Format(FormatProvider, format, arg0, arg1, arg2)))
                : true;

        /// <summary>
        /// Attempts to atomically log the formatted message with the given <see cref="LogLevel"/>
        /// using the same semantics as the <see cref="string.Format(string, object[])"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/>
        /// is less than or equal to <paramref name="level"/>.
        /// </remarks>
        /// <param name="level">The <see cref="LogLevel"/> associated with the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format and log.</param>
        /// <returns>
        /// <see langword="true"/> if the message could be atomically logged or the
        /// <paramref name="level"/> was below the <see cref="Logger.MinLevel"/>; otherwise
        /// <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="format"/> or <paramref name="args"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <para><paramref name="format"/> is not a valid composite format string.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than 0 (zero), or greater than or equal
        /// to the length of the <paramref name="args"/> array.
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public virtual bool TryLog(LogLevel level, string format, params object[] args)
            => level >= MinLevel
                ? TryAdd(new LogEntry(level, string.Format(FormatProvider, format, args)))
                : true;
    }
}
