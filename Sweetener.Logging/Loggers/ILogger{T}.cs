 // Generated from ILogger{T}.tt
using System;

namespace Sweetener.Logging
{
    /// <summary>
    /// An interface for loggers that write log entries at a given <see cref="LogLevel"/>.
    /// </summary>
    /// <typeparam name="T">The type of values to be logged.</typeparam>
    public interface ILogger<T> : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether logging is synchronized (thread safe).
        /// </summary>
        /// <returns><c>true</c> if logging is synchronized (thread safe); otherwise, <c>false</c>.</returns>
        bool IsSynchronized { get; }

        /// <summary>
        /// Gets the minimum level of log requests that will be fulfilled.
        /// </summary>
        /// <returns>The minimum <see cref="LogLevel"/> that will be fulfilled.</returns>
        LogLevel MinimumLevel { get; }

        /// <summary>
        /// Gets an object that can be used to synchronize logging.
        /// </summary>
        /// <returns>An object that can be used to synchronize logging.</returns>
        object SyncRoot { get; }

        /// <summary>
        /// Request to log the specified value at the <see cref="LogLevel.Trace"/> level.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Trace"/>.
        /// </remarks>
        /// <param name="obj">The value requested for logging.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="T"/> is a reference type and <paramref name="obj"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        void Trace(T obj);

        /// <summary>
        /// Request to log the specified value at the <see cref="LogLevel.Debug"/> level.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Debug"/>.
        /// </remarks>
        /// <param name="obj">The value requested for logging.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="T"/> is a reference type and <paramref name="obj"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        void Debug(T obj);

        /// <summary>
        /// Request to log the specified value at the <see cref="LogLevel.Info"/> level.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Info"/>.
        /// </remarks>
        /// <param name="obj">The value requested for logging.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="T"/> is a reference type and <paramref name="obj"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        void Info(T obj);

        /// <summary>
        /// Request to log the specified value at the <see cref="LogLevel.Warn"/> level.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Warn"/>.
        /// </remarks>
        /// <param name="obj">The value requested for logging.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="T"/> is a reference type and <paramref name="obj"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        void Warn(T obj);

        /// <summary>
        /// Request to log the specified value at the <see cref="LogLevel.Error"/> level.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Error"/>.
        /// </remarks>
        /// <param name="obj">The value requested for logging.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="T"/> is a reference type and <paramref name="obj"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        void Error(T obj);

        /// <summary>
        /// Request to log the specified value at the <see cref="LogLevel.Fatal"/> level.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Fatal"/>.
        /// </remarks>
        /// <param name="obj">The value requested for logging.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="T"/> is a reference type and <paramref name="obj"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        void Fatal(T obj);
    }
}
