using System;
using System.Globalization;

namespace Sweetener.Logging 
{
    /// <summary>
    /// A common base implementation of the <see cref="ILogger{T}"/> interface that
    /// logs optionally formattable string messages.
    /// </summary>
    public abstract partial class Logger : ILogger<string>
    {
        /// <summary>
        /// Gets an object that controls formatting. 
        /// </summary>
        public IFormatProvider FormatProvider { get; }

        /// <summary>
        /// Gets a value indicating whether logging is synchronized (thread safe).
        /// </summary>
        /// <returns><c>true</c> if logging is synchronized (thread safe); otherwise, <c>false</c>.</returns>
        public virtual bool IsSynchronized => false;

        /// <summary>
        /// Gets the minimum level of log requests that will be fulfilled.
        /// </summary>
        /// <returns>The minimum <see cref="LogLevel"/> that will be fulfilled.</returns>
        public LogLevel MinimumLevel { get; }

        /// <summary>
        /// Gets an object that can be used to synchronize logging.
        /// </summary>
        /// <returns>An object that can be used to synchronize logging.</returns>
        public object SyncRoot { get; } = new object();

        private bool _disposed = false;
        private readonly LogPattern _logPattern;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class for the current
        /// culture that logs all levels.
        /// </summary>
        protected Logger()
            : this(LogLevel.Debug, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class with a minimum
        /// logging level for the current culture.
        /// </summary>
        /// <param name="minimumLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minimumLevel"/> is unrecognized.</exception>
        protected Logger(LogLevel minimumLevel)
            : this(minimumLevel, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class with a minimum
        /// logging level and an <see cref="IFormatProvider"/> object for a specific culture.
        /// </summary>
        /// <remarks>
        /// If <paramref name="formatProvider"/> is <c>null</c>, the formatting of the current culture is used.
        /// </remarks>
        /// <param name="minimumLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <param name="formatProvider">An <see cref="IFormatProvider"/> object for a specific culture.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minimumLevel"/> is unrecognized.</exception>
        protected Logger(LogLevel minimumLevel, IFormatProvider formatProvider)
            : this(minimumLevel, formatProvider, LogPattern.Default)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class with a minimum
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
        protected Logger(LogLevel minimumLevel, IFormatProvider formatProvider, string pattern)
        {
            if (minimumLevel < LogLevel.Debug || minimumLevel > LogLevel.Fatal)
                throw new ArgumentOutOfRangeException(nameof(minimumLevel), $"Unknown {nameof(LogLevel)} value '{minimumLevel}'");

            if (formatProvider == null)
                formatProvider = CultureInfo.CurrentCulture;

            FormatProvider = formatProvider;
            MinimumLevel   = minimumLevel;
            _logPattern    = new LogPattern(pattern);
        }

        /// <summary>
        /// Releases all resources used by the <see cref="Logger"/> object.
        /// </summary>
        /// <remarks>
        /// Call <see cref="Dispose"/> when you are finished using the <see cref="Logger"/>.
        /// The <see cref="Dispose"/> method leaves the <see cref="Logger"/> in an unusable state.
        /// After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="Logger"/> so the garbage collector can reclaim the memory that the
        /// <see cref="Logger"/> was occupying.
        /// </remarks>
        public void Dispose()
        {
            Dispose(true);

            // Still suppress, in case any derived classes use a finalizer
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="Logger"/> and
        /// optionally releases the managed resources.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is called by <see cref="Dispose"/> and possible Finalize in a
        /// derived class. By default, this method specifies the <paramref name="disposing"/>
        /// parameter as <c>true</c>. Any Finalize implementation specifies the
        /// <paramref name="disposing"/> parameter as <c>false</c>.
        /// </para>
        /// <para>
        /// When the <paramref name="disposing"/> parameter is <c>true</c>, this method
        /// releases all resources held by any managed objects that this <see cref="Logger"/>
        /// references. This method invokes the <see cref="Dispose"/> method of each referenced object.
        /// </para>
        /// </remarks>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c>
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
        /// Writes the message to the log.
        /// </summary>
        /// <param name="message">The value to be logged.</param>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is <c>null</c>.</exception>
        protected abstract void WriteLine(string message);

        private void Log(LogLevel level, string message)
            => WriteLine(_logPattern.Format(FormatProvider, level, message));

        private void ThrowIfDisposed()
        {
            if (_disposed)
                ThrowObjectDisposedException();

            void ThrowObjectDisposedException() => throw new ObjectDisposedException(GetType().Name, "Cannot log to a disposed logger.");
        }
    }
}