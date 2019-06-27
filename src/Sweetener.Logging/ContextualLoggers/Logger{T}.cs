using System;
using System.Globalization;

namespace Sweetener.Logging 
{
    /// <summary>
    /// Represents a client that can log messages with some domain-specific context
    /// for given <see cref="LogLevel"/> based on their purpose or severity.
    /// </summary>
    /// <typeparam name="T">The type of context for each message.</typeparam>
    public abstract partial class Logger<T> : IDisposable
    {
        /// <summary>
        /// Gets an object that controls formatting. 
        /// </summary>
        public IFormatProvider FormatProvider { get; }

        /// <summary>
        /// Gets a value indicating whether logging is synchronized (thread safe).
        /// </summary>
        /// <returns><see langword="true"/> if logging is synchronized (thread safe); otherwise, <see langword="false"/>.</returns>
        public virtual bool IsSynchronized => false;

        /// <summary>
        /// Gets the minimum level of log requests that will be fulfilled.
        /// </summary>
        /// <returns>The minimum <see cref="LogLevel"/> that will be fulfilled.</returns>
        public LogLevel MinLevel { get; }

        /// <summary>
        /// Gets an object that can be used to synchronize logging.
        /// </summary>
        /// <returns>An object that can be used to synchronize logging.</returns>
        public object SyncRoot { get; } = new object();

        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger{T}"/> class for the current
        /// culture that fulfills all logging requests.
        /// </summary>
        protected Logger()
            : this(LogLevel.Trace)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger{T}"/> class for the current
        /// culture that fulfills all logging requests above a specified minimum <see cref="LogLevel"/>.
        /// </summary>
        /// <param name="minLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minLevel"/> is an unknown value.</exception>
        protected Logger(LogLevel minLevel)
            : this(minLevel, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger{T}"/> class for a particular
        /// culture that fulfills all logging requests above a specified minimum <see cref="LogLevel"/>.
        /// </summary>
        /// <remarks>
        /// If <paramref name="formatProvider"/> is <see langword="null"/>, the formatting of the current culture is used.
        /// </remarks>
        /// <param name="minLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <param name="formatProvider">An <see cref="IFormatProvider"/> object for a specific culture.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minLevel"/> is an unknown value.</exception>
        protected Logger(LogLevel minLevel, IFormatProvider formatProvider)
        {
            if (minLevel < LogLevel.Trace || minLevel > LogLevel.Fatal)
                throw new ArgumentOutOfRangeException(nameof(minLevel), $"Unknown {nameof(LogLevel)} value '{minLevel}'");

            if (formatProvider == null)
                formatProvider = CultureInfo.CurrentCulture;

            FormatProvider = formatProvider;
            MinLevel       = minLevel;
        }

        /// <summary>
        /// Releases all resources used by the <see cref="Logger{T}"/> object.
        /// </summary>
        /// <remarks>
        /// Call <see cref="Dispose()"/> when you are finished using the <see cref="Logger{T}"/>.
        /// The <see cref="Dispose()"/> method leaves the <see cref="Logger{T}"/> in an unusable state.
        /// After calling <see cref="Dispose()"/>, you must release all references to the
        /// <see cref="Logger{T}"/> so the garbage collector can reclaim the memory that the
        /// <see cref="Logger{T}"/> was occupying.
        /// </remarks>
        public void Dispose()
        {
            Dispose(true);

            // Still suppress, in case any derived classes use a finalizer
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="Logger{T}"/> and
        /// optionally releases the managed resources.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is called by <see cref="Dispose()"/> and possible Finalize in a
        /// derived class. By default, this method specifies the <paramref name="disposing"/>
        /// parameter as <see langword="true"/>. Any Finalize implementation specifies the
        /// <paramref name="disposing"/> parameter as <see langword="false"/>.
        /// </para>
        /// <para>
        /// When the <paramref name="disposing"/> parameter is <see langword="true"/>, this method
        /// releases all resources held by any managed objects that this <see cref="Logger{T}"/>
        /// references. This method invokes the <see cref="IDisposable.Dispose"/> method
        /// of each referenced object.
        /// </para>
        /// </remarks>
        /// <param name="disposing">
        /// <see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/>
        /// to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            // In the base class, this flag is used to short circuit calls before Log(...)
            // We don't want method calls on disabled loggers to not throw exceptions
            // because the log request was below the minimum log level!
            if (!_disposed)
                _disposed = true;
        }

        /// <summary>
        /// Logs the specified entry.
        /// </summary>
        /// <param name="logEntry">A log entry which consists of the message and its context.</param>
        protected internal abstract void Log(LogEntry<T> logEntry);

        private void ThrowIfDisposed()
        {
            if (_disposed)
                ThrowObjectDisposedException();

            void ThrowObjectDisposedException() => throw new ObjectDisposedException(GetType().Name, "Cannot log with a disposed logger.");
        }
    }
}
