using System;
using System.Globalization;

namespace Sweetener.Logging 
{
    /// <summary>
    /// Represents a client that can log messages for a given <see cref="LogLevel"/>
    /// based on their purpose or severity.
    /// </summary>
    public abstract partial class Logger : IDisposable
    {
        /// <summary>
        /// A <see cref="Logger"/> with no backing store for its entries.
        /// </summary>
        public static readonly Logger Null = new NullLogger();

        /// <summary>
        /// Gets an object that controls formatting. 
        /// </summary>
        /// <value>
        /// An <see cref="IFormatProvider"/> object for a specific culture, or the
        /// formatting of the current culture if no other culture is specified.
        /// </value>
        public IFormatProvider FormatProvider { get; }

        /// <summary>
        /// Gets a value indicating whether logging is synchronized (thread safe).
        /// </summary>
        /// <value>
        /// <see langword="true"/> if logging is synchronized (thread safe);
        /// otherwise, <see langword="false"/>.
        /// </value>
        public virtual bool IsSynchronized => false;

        /// <summary>
        /// Gets the minimum level of log requests that will be fulfilled.
        /// </summary>
        /// <value>
        /// The minimum <see cref="LogLevel"/> that will be fulfilled by the <see cref="Logger"/>;
        /// any log request with a <see cref="LogLevel"/> below the minimum will be ignored.
        /// </value>
        public LogLevel MinLevel { get; }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="Logger"/>.
        /// </summary>
        /// <value>
        /// An object that can be used to synchronize access to the <see cref="Logger"/>.
        /// In the default implementation of <see cref="Logger"/>, this property always
        /// returns the current instance.
        /// </value>
        public virtual object SyncRoot => this;

        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class for the current
        /// culture that fulfills all logging requests.
        /// </summary>
        protected Logger()
            : this(LogLevel.Trace)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class for the current
        /// culture that fulfills all logging requests above a specified minimum <see cref="LogLevel"/>.
        /// </summary>
        /// <param name="minLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minLevel"/> is an unknown value.</exception>
        protected Logger(LogLevel minLevel)
            : this(minLevel, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class for a particular
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
        /// Releases all resources used by the <see cref="Logger"/> object.
        /// </summary>
        /// <remarks>
        /// Call <see cref="Dispose()"/> when you are finished using the <see cref="Logger"/>.
        /// The <see cref="Dispose()"/> method leaves the <see cref="Logger"/> in an unusable state.
        /// After calling <see cref="Dispose()"/>, you must release all references to the
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
        /// This method is called by <see cref="Dispose()"/> and possible Finalize in a
        /// derived class. By default, this method specifies the <paramref name="disposing"/>
        /// parameter as <see langword="true"/>. Any Finalize implementation specifies the
        /// <paramref name="disposing"/> parameter as <see langword="false"/>.
        /// </para>
        /// <para>
        /// When the <paramref name="disposing"/> parameter is <see langword="true"/>, this method
        /// releases all resources held by any managed objects that this <see cref="Logger"/>
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
            // In the base class, this flag is used to short-circuit calls before Log(...)
            // We don't want method calls on disabled loggers to skip throwing exceptions
            // because the log request was below the minimum log level!
            if (!_disposed)
                _disposed = true;
        }

        /// <summary>
        /// Logs the specified entry.
        /// </summary>
        /// <param name="logEntry">A log entry which consists of the message and its context.</param>
        protected internal abstract void Log(LogEntry logEntry);

        private void ThrowIfDisposed()
        {
            if (_disposed)
                ThrowObjectDisposedException();

            void ThrowObjectDisposedException() => throw new ObjectDisposedException(GetType().Name, "Cannot log with a disposed logger.");
        }
    }
}
