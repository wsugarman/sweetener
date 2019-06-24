// Generated from Logger{T}.Levels.tt
using System;

namespace Sweetener.Logging
{
    /// <content>
    /// The portion of the <see cref="Logger{T}"/> class that defines the logging
    /// methods for each <see cref="LogLevel"/>.
    /// </content>
    abstract partial class Logger<T>
    {
        /// <summary>
        /// Requests that the specified value be logged with level <see cref="LogLevel.Trace"/>.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Trace"/>.
        /// </remarks>
        /// <param name="value">The value requested for logging.</param>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Trace(T value)
        {
            ThrowIfDisposed();

            if (MinLevel <= LogLevel.Trace)
                Log(new LogEntry<T>(LogLevel.Trace, value));
        }

        /// <summary>
        /// Requests that the specified value be logged with level <see cref="LogLevel.Debug"/>.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Debug"/>.
        /// </remarks>
        /// <param name="value">The value requested for logging.</param>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Debug(T value)
        {
            ThrowIfDisposed();

            if (MinLevel <= LogLevel.Debug)
                Log(new LogEntry<T>(LogLevel.Debug, value));
        }

        /// <summary>
        /// Requests that the specified value be logged with level <see cref="LogLevel.Info"/>.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Info"/>.
        /// </remarks>
        /// <param name="value">The value requested for logging.</param>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Info(T value)
        {
            ThrowIfDisposed();

            if (MinLevel <= LogLevel.Info)
                Log(new LogEntry<T>(LogLevel.Info, value));
        }

        /// <summary>
        /// Requests that the specified value be logged with level <see cref="LogLevel.Warn"/>.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Warn"/>.
        /// </remarks>
        /// <param name="value">The value requested for logging.</param>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Warn(T value)
        {
            ThrowIfDisposed();

            if (MinLevel <= LogLevel.Warn)
                Log(new LogEntry<T>(LogLevel.Warn, value));
        }

        /// <summary>
        /// Requests that the specified value be logged with level <see cref="LogLevel.Error"/>.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Error"/>.
        /// </remarks>
        /// <param name="value">The value requested for logging.</param>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Error(T value)
        {
            ThrowIfDisposed();

            if (MinLevel <= LogLevel.Error)
                Log(new LogEntry<T>(LogLevel.Error, value));
        }

        /// <summary>
        /// Requests that the specified value be logged with level <see cref="LogLevel.Fatal"/>.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Fatal"/>.
        /// </remarks>
        /// <param name="value">The value requested for logging.</param>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Fatal(T value)
        {
            ThrowIfDisposed();

            if (MinLevel <= LogLevel.Fatal)
                Log(new LogEntry<T>(LogLevel.Fatal, value));
        }

    }
}
