 // Generated from Logger.Format.tt
using System;

namespace Sweetener.Logging 
{
    /// <content>
    /// The portion of the <see cref="Logger"/> class that defines the various formatting
    /// overloads for its <see cref="ILogger{T}"/> methods.
    /// </content>
    abstract partial class Logger
    {
        #region Trace
        /// <summary>
        /// Request to log the specified string value at the <see cref="LogLevel.Trace"/> level.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Trace"/>.
        /// </remarks>
        /// <param name="message">The value requested for logging.</param>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is <c>null</c>.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Trace(string message)
        {
            ThrowIfDisposed();

            if (message == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Trace)
                Log(new LogEntry<string>(LogLevel.Trace, message));
        }

        /// <summary>
        /// Request to log the text representation of the specified object at the
        /// <see cref="LogLevel.Trace"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Trace"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">An object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Trace(string format, object arg0)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Trace)
                Log(new LogEntry<string>(LogLevel.Trace, string.Format(FormatProvider, format, arg0)));
        }

        /// <summary>
        /// Request to log the text representation of the specified objects at the
        /// <see cref="LogLevel.Trace"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Trace"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Trace(string format, object arg0, object arg1)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Trace)
                Log(new LogEntry<string>(LogLevel.Trace, string.Format(FormatProvider, format, arg0, arg1)));
        }

        /// <summary>
        /// Request to log the text representation of the specified objects at the
        /// <see cref="LogLevel.Trace"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Trace"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <param name="arg2">The third object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Trace(string format, object arg0, object arg1, object arg2)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Trace)
                Log(new LogEntry<string>(LogLevel.Trace, string.Format(FormatProvider, format, arg0, arg1, arg2)));
        }

        /// <summary>
        /// Request to log the text representation of the specified objects at the
        /// <see cref="LogLevel.Trace"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Trace"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <param name="arg2">The third object to write using <paramref name="format"/>.</param>
        /// <param name="arg3">The fourth object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Trace(string format, object arg0, object arg1, object arg2, object arg3)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Trace)
                Log(new LogEntry<string>(LogLevel.Trace, string.Format(FormatProvider, format, arg0, arg1, arg2, arg3)));
        }

        /// <summary>
        /// Request to log the text representation of the specified array of objects at the
        /// <see cref="LogLevel.Trace"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Trace"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> or <paramref name="args"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">
        /// <para>The format specification in <paramref name="format"/> is invalid.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than zero, or greater than or equal
        /// to the length of the <paramref name="args"/> array.
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Trace(string format, params object[] args)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (args == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.args);

            if (MinimumLevel <= LogLevel.Trace)
                Log(new LogEntry<string>(LogLevel.Trace, string.Format(FormatProvider, format, args)));
        }
        #endregion

        #region Debug
        /// <summary>
        /// Request to log the specified string value at the <see cref="LogLevel.Debug"/> level.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Debug"/>.
        /// </remarks>
        /// <param name="message">The value requested for logging.</param>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is <c>null</c>.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Debug(string message)
        {
            ThrowIfDisposed();

            if (message == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Debug)
                Log(new LogEntry<string>(LogLevel.Debug, message));
        }

        /// <summary>
        /// Request to log the text representation of the specified object at the
        /// <see cref="LogLevel.Debug"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Debug"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">An object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Debug(string format, object arg0)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Debug)
                Log(new LogEntry<string>(LogLevel.Debug, string.Format(FormatProvider, format, arg0)));
        }

        /// <summary>
        /// Request to log the text representation of the specified objects at the
        /// <see cref="LogLevel.Debug"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Debug"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Debug(string format, object arg0, object arg1)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Debug)
                Log(new LogEntry<string>(LogLevel.Debug, string.Format(FormatProvider, format, arg0, arg1)));
        }

        /// <summary>
        /// Request to log the text representation of the specified objects at the
        /// <see cref="LogLevel.Debug"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Debug"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <param name="arg2">The third object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Debug(string format, object arg0, object arg1, object arg2)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Debug)
                Log(new LogEntry<string>(LogLevel.Debug, string.Format(FormatProvider, format, arg0, arg1, arg2)));
        }

        /// <summary>
        /// Request to log the text representation of the specified objects at the
        /// <see cref="LogLevel.Debug"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Debug"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <param name="arg2">The third object to write using <paramref name="format"/>.</param>
        /// <param name="arg3">The fourth object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Debug(string format, object arg0, object arg1, object arg2, object arg3)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Debug)
                Log(new LogEntry<string>(LogLevel.Debug, string.Format(FormatProvider, format, arg0, arg1, arg2, arg3)));
        }

        /// <summary>
        /// Request to log the text representation of the specified array of objects at the
        /// <see cref="LogLevel.Debug"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Debug"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> or <paramref name="args"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">
        /// <para>The format specification in <paramref name="format"/> is invalid.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than zero, or greater than or equal
        /// to the length of the <paramref name="args"/> array.
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Debug(string format, params object[] args)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (args == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.args);

            if (MinimumLevel <= LogLevel.Debug)
                Log(new LogEntry<string>(LogLevel.Debug, string.Format(FormatProvider, format, args)));
        }
        #endregion

        #region Info
        /// <summary>
        /// Request to log the specified string value at the <see cref="LogLevel.Info"/> level.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Info"/>.
        /// </remarks>
        /// <param name="message">The value requested for logging.</param>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is <c>null</c>.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Info(string message)
        {
            ThrowIfDisposed();

            if (message == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Info)
                Log(new LogEntry<string>(LogLevel.Info, message));
        }

        /// <summary>
        /// Request to log the text representation of the specified object at the
        /// <see cref="LogLevel.Info"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Info"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">An object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Info(string format, object arg0)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Info)
                Log(new LogEntry<string>(LogLevel.Info, string.Format(FormatProvider, format, arg0)));
        }

        /// <summary>
        /// Request to log the text representation of the specified objects at the
        /// <see cref="LogLevel.Info"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Info"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Info(string format, object arg0, object arg1)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Info)
                Log(new LogEntry<string>(LogLevel.Info, string.Format(FormatProvider, format, arg0, arg1)));
        }

        /// <summary>
        /// Request to log the text representation of the specified objects at the
        /// <see cref="LogLevel.Info"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Info"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <param name="arg2">The third object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Info(string format, object arg0, object arg1, object arg2)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Info)
                Log(new LogEntry<string>(LogLevel.Info, string.Format(FormatProvider, format, arg0, arg1, arg2)));
        }

        /// <summary>
        /// Request to log the text representation of the specified objects at the
        /// <see cref="LogLevel.Info"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Info"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <param name="arg2">The third object to write using <paramref name="format"/>.</param>
        /// <param name="arg3">The fourth object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Info(string format, object arg0, object arg1, object arg2, object arg3)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Info)
                Log(new LogEntry<string>(LogLevel.Info, string.Format(FormatProvider, format, arg0, arg1, arg2, arg3)));
        }

        /// <summary>
        /// Request to log the text representation of the specified array of objects at the
        /// <see cref="LogLevel.Info"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Info"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> or <paramref name="args"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">
        /// <para>The format specification in <paramref name="format"/> is invalid.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than zero, or greater than or equal
        /// to the length of the <paramref name="args"/> array.
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Info(string format, params object[] args)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (args == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.args);

            if (MinimumLevel <= LogLevel.Info)
                Log(new LogEntry<string>(LogLevel.Info, string.Format(FormatProvider, format, args)));
        }
        #endregion

        #region Warn
        /// <summary>
        /// Request to log the specified string value at the <see cref="LogLevel.Warn"/> level.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Warn"/>.
        /// </remarks>
        /// <param name="message">The value requested for logging.</param>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is <c>null</c>.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Warn(string message)
        {
            ThrowIfDisposed();

            if (message == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Warn)
                Log(new LogEntry<string>(LogLevel.Warn, message));
        }

        /// <summary>
        /// Request to log the text representation of the specified object at the
        /// <see cref="LogLevel.Warn"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Warn"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">An object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Warn(string format, object arg0)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Warn)
                Log(new LogEntry<string>(LogLevel.Warn, string.Format(FormatProvider, format, arg0)));
        }

        /// <summary>
        /// Request to log the text representation of the specified objects at the
        /// <see cref="LogLevel.Warn"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Warn"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Warn(string format, object arg0, object arg1)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Warn)
                Log(new LogEntry<string>(LogLevel.Warn, string.Format(FormatProvider, format, arg0, arg1)));
        }

        /// <summary>
        /// Request to log the text representation of the specified objects at the
        /// <see cref="LogLevel.Warn"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Warn"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <param name="arg2">The third object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Warn(string format, object arg0, object arg1, object arg2)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Warn)
                Log(new LogEntry<string>(LogLevel.Warn, string.Format(FormatProvider, format, arg0, arg1, arg2)));
        }

        /// <summary>
        /// Request to log the text representation of the specified objects at the
        /// <see cref="LogLevel.Warn"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Warn"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <param name="arg2">The third object to write using <paramref name="format"/>.</param>
        /// <param name="arg3">The fourth object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Warn(string format, object arg0, object arg1, object arg2, object arg3)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Warn)
                Log(new LogEntry<string>(LogLevel.Warn, string.Format(FormatProvider, format, arg0, arg1, arg2, arg3)));
        }

        /// <summary>
        /// Request to log the text representation of the specified array of objects at the
        /// <see cref="LogLevel.Warn"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Warn"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> or <paramref name="args"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">
        /// <para>The format specification in <paramref name="format"/> is invalid.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than zero, or greater than or equal
        /// to the length of the <paramref name="args"/> array.
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Warn(string format, params object[] args)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (args == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.args);

            if (MinimumLevel <= LogLevel.Warn)
                Log(new LogEntry<string>(LogLevel.Warn, string.Format(FormatProvider, format, args)));
        }
        #endregion

        #region Error
        /// <summary>
        /// Request to log the specified string value at the <see cref="LogLevel.Error"/> level.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Error"/>.
        /// </remarks>
        /// <param name="message">The value requested for logging.</param>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is <c>null</c>.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Error(string message)
        {
            ThrowIfDisposed();

            if (message == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Error)
                Log(new LogEntry<string>(LogLevel.Error, message));
        }

        /// <summary>
        /// Request to log the text representation of the specified object at the
        /// <see cref="LogLevel.Error"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Error"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">An object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Error(string format, object arg0)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Error)
                Log(new LogEntry<string>(LogLevel.Error, string.Format(FormatProvider, format, arg0)));
        }

        /// <summary>
        /// Request to log the text representation of the specified objects at the
        /// <see cref="LogLevel.Error"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Error"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Error(string format, object arg0, object arg1)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Error)
                Log(new LogEntry<string>(LogLevel.Error, string.Format(FormatProvider, format, arg0, arg1)));
        }

        /// <summary>
        /// Request to log the text representation of the specified objects at the
        /// <see cref="LogLevel.Error"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Error"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <param name="arg2">The third object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Error(string format, object arg0, object arg1, object arg2)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Error)
                Log(new LogEntry<string>(LogLevel.Error, string.Format(FormatProvider, format, arg0, arg1, arg2)));
        }

        /// <summary>
        /// Request to log the text representation of the specified objects at the
        /// <see cref="LogLevel.Error"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Error"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <param name="arg2">The third object to write using <paramref name="format"/>.</param>
        /// <param name="arg3">The fourth object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Error(string format, object arg0, object arg1, object arg2, object arg3)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Error)
                Log(new LogEntry<string>(LogLevel.Error, string.Format(FormatProvider, format, arg0, arg1, arg2, arg3)));
        }

        /// <summary>
        /// Request to log the text representation of the specified array of objects at the
        /// <see cref="LogLevel.Error"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Error"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> or <paramref name="args"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">
        /// <para>The format specification in <paramref name="format"/> is invalid.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than zero, or greater than or equal
        /// to the length of the <paramref name="args"/> array.
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Error(string format, params object[] args)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (args == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.args);

            if (MinimumLevel <= LogLevel.Error)
                Log(new LogEntry<string>(LogLevel.Error, string.Format(FormatProvider, format, args)));
        }
        #endregion

        #region Fatal
        /// <summary>
        /// Request to log the specified string value at the <see cref="LogLevel.Fatal"/> level.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Fatal"/>.
        /// </remarks>
        /// <param name="message">The value requested for logging.</param>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is <c>null</c>.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Fatal(string message)
        {
            ThrowIfDisposed();

            if (message == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Fatal)
                Log(new LogEntry<string>(LogLevel.Fatal, message));
        }

        /// <summary>
        /// Request to log the text representation of the specified object at the
        /// <see cref="LogLevel.Fatal"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Fatal"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">An object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Fatal(string format, object arg0)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Fatal)
                Log(new LogEntry<string>(LogLevel.Fatal, string.Format(FormatProvider, format, arg0)));
        }

        /// <summary>
        /// Request to log the text representation of the specified objects at the
        /// <see cref="LogLevel.Fatal"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Fatal"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Fatal(string format, object arg0, object arg1)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Fatal)
                Log(new LogEntry<string>(LogLevel.Fatal, string.Format(FormatProvider, format, arg0, arg1)));
        }

        /// <summary>
        /// Request to log the text representation of the specified objects at the
        /// <see cref="LogLevel.Fatal"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Fatal"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <param name="arg2">The third object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Fatal(string format, object arg0, object arg1, object arg2)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Fatal)
                Log(new LogEntry<string>(LogLevel.Fatal, string.Format(FormatProvider, format, arg0, arg1, arg2)));
        }

        /// <summary>
        /// Request to log the text representation of the specified objects at the
        /// <see cref="LogLevel.Fatal"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Fatal"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <param name="arg2">The third object to write using <paramref name="format"/>.</param>
        /// <param name="arg3">The fourth object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Fatal(string format, object arg0, object arg1, object arg2, object arg3)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (MinimumLevel <= LogLevel.Fatal)
                Log(new LogEntry<string>(LogLevel.Fatal, string.Format(FormatProvider, format, arg0, arg1, arg2, arg3)));
        }

        /// <summary>
        /// Request to log the text representation of the specified array of objects at the
        /// <see cref="LogLevel.Fatal"/> level using the specified format information.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="MinimumLevel"/> is less
        /// than or equal to <see cref="LogLevel.Fatal"/>.
        /// </remarks>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> or <paramref name="args"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException">
        /// <para>The format specification in <paramref name="format"/> is invalid.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than zero, or greater than or equal
        /// to the length of the <paramref name="args"/> array.
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public void Fatal(string format, params object[] args)
        {
            ThrowIfDisposed();

            if (format == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);

            if (args == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.args);

            if (MinimumLevel <= LogLevel.Fatal)
                Log(new LogEntry<string>(LogLevel.Fatal, string.Format(FormatProvider, format, args)));
        }
        #endregion

    }
}
