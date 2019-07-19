using System;

namespace Sweetener.Logging 
{
    /// <summary>
    /// Represents a client that can log messages for a given <see cref="LogLevel"/>
    /// based on their purpose or severity. This class is abstract.
    /// </summary>
    public abstract partial class Logger : IDisposable
    {
        /// <summary>
        /// Gets an object that controls formatting.
        /// </summary>
        /// <value>
        /// An <see cref="IFormatProvider"/> object for a specific culture, or the
        /// formatting of the current culture if no other culture is specified.
        /// </value>
        public abstract IFormatProvider FormatProvider { get; }

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
        public abstract LogLevel MinLevel { get; }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="Logger"/>.
        /// </summary>
        /// <value>
        /// An object that can be used to synchronize access to the <see cref="Logger"/>.
        /// In the default implementation of <see cref="Logger"/>, this property always
        /// returns the current instance.
        /// </value>
        public virtual object SyncRoot => this;

        /// <summary>
        /// A <see cref="Logger"/> with no backing store for its entries.
        /// </summary>
        public static readonly Logger Null = new NullLogger();

        /// <summary>
        /// When overridden in a derived class, adds the entry to the log.
        /// </summary>
        /// <param name="logEntry">A <see cref="LogEntry"/> which consists of the message and its context.</param>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        protected abstract void Add(LogEntry logEntry);

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
        /// <see langword="true"/> to release both managed and unmanaged resources;
        /// <see langword="false"/> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        { }

        /// <summary>
        /// Requests that the specified message be logged with the given <see cref="LogLevel"/>.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinLevel"/> is less
        /// than or equal to <paramref name="level"/>.
        /// </remarks>
        /// <param name="level">The <see cref="LogLevel"/> associated with the <paramref name="message"/>.</param>
        /// <param name="message">The message requested for logging.</param>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public virtual void Log(LogLevel level, string message)
        {
            if (MinLevel <= level)
                Add(new LogEntry(level, message));
        }

        /// <summary>
        /// Requests that the text representation of the specified object, using the
        /// specified format information, be logged with the given <see cref="LogLevel"/>.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinLevel"/> is less
        /// than or equal to <paramref name="level"/>.
        /// </remarks>
        /// <param name="level">The <see cref="LogLevel"/> associated with the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">An object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public virtual void Log(LogLevel level, string format, object arg0)
        {
            if (MinLevel <= level)
                Add(new LogEntry(level, string.Format(FormatProvider, format, arg0)));
        }

        /// <summary>
        /// Requests that the text representation of the specified objects, using the
        /// specified format information, be logged with the given <paramref name="level"/>.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinLevel"/> is less
        /// than or equal to <paramref name="level"/>.
        /// </remarks>
        /// <param name="level">The <see cref="LogLevel"/> associated with the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public virtual void Log(LogLevel level, string format, object arg0, object arg1)
        {
            if (MinLevel <= level)
                Add(new LogEntry(level, string.Format(FormatProvider, format, arg0, arg1)));
        }

        /// <summary>
        /// Requests that the text representation of the specified objects, using the
        /// specified format information, be logged with the given <paramref name="level"/>.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinLevel"/> is less
        /// than or equal to <paramref name="level"/>.
        /// </remarks>
        /// <param name="level">The <see cref="LogLevel"/> associated with the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <param name="arg2">The third object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public virtual void Log(LogLevel level, string format, object arg0, object arg1, object arg2)
        {
            if (MinLevel <= level)
                Add(new LogEntry(level, string.Format(FormatProvider, format, arg0, arg1, arg2)));
        }

        /// <summary>
        /// Requests that the text representation of the specified array of objects, using the
        /// specified format information, be logged with the given <paramref name="level"/>.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinLevel"/> is less
        /// than or equal to <paramref name="level"/>.
        /// </remarks>
        /// <param name="level">The <see cref="LogLevel"/> associated with the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> or <paramref name="args"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">
        /// <para>The format specification in <paramref name="format"/> is invalid.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than zero, or greater than or equal
        /// to the length of the <paramref name="args"/> array.
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public virtual void Log(LogLevel level, string format, params object[] args)
        {
            if (MinLevel <= level)
                Add(new LogEntry(level, string.Format(FormatProvider, format, args)));
        }
    }
}
