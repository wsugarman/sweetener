// Generated from Logger{T}.Extensions.tt
using System;

namespace Sweetener.Logging.Extensions
{
    /// <content>
    /// The portion of <see cref="LoggerExtensions"/> for <see cref="Logger{T}"/> methods.
    /// </content>
    public static partial class LoggerExtensions
    {
        #region Trace
        /// <summary>
        /// Requests that the specified message be logged with level <see cref="LogLevel.Trace"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Trace"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
        /// <param name="message">The message requested for logging.</param>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Trace<T>(this Logger<T> logger, T context, string message)
            => logger.Log(LogLevel.Trace, context, message);

        /// <summary>
        /// Requests that the text representation of the specified object, using the
        /// specified format information, be logged with the level <see cref="LogLevel.Trace"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Trace"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">An object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Trace<T>(this Logger<T> logger, T context, string format, object arg0)
            => logger.Log(LogLevel.Trace, context, format, arg0);

        /// <summary>
        /// Requests that the text representation of the specified objects, using the
        /// specified format information, be logged with the level <see cref="LogLevel.Trace"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Trace"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Trace<T>(this Logger<T> logger, T context, string format, object arg0, object arg1)
            => logger.Log(LogLevel.Trace, context, format, arg0, arg1);

        /// <summary>
        /// Requests that the text representation of the specified objects, using the
        /// specified format information, be logged with the level <see cref="LogLevel.Trace"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Trace"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <param name="arg2">The third object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Trace<T>(this Logger<T> logger, T context, string format, object arg0, object arg1, object arg2)
            => logger.Log(LogLevel.Trace, context, format, arg0, arg1, arg2);

        /// <summary>
        /// Requests that the text representation of the specified array of objects, using the
        /// specified format information, be logged with the level <see cref="LogLevel.Trace"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Trace"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
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
        public static void Trace<T>(this Logger<T> logger, T context, string format, params object[] args)
            => logger.Log(LogLevel.Trace, context, format, args);
        #endregion

        #region Debug
        /// <summary>
        /// Requests that the specified message be logged with level <see cref="LogLevel.Debug"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Debug"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
        /// <param name="message">The message requested for logging.</param>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Debug<T>(this Logger<T> logger, T context, string message)
            => logger.Log(LogLevel.Debug, context, message);

        /// <summary>
        /// Requests that the text representation of the specified object, using the
        /// specified format information, be logged with the level <see cref="LogLevel.Debug"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Debug"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">An object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Debug<T>(this Logger<T> logger, T context, string format, object arg0)
            => logger.Log(LogLevel.Debug, context, format, arg0);

        /// <summary>
        /// Requests that the text representation of the specified objects, using the
        /// specified format information, be logged with the level <see cref="LogLevel.Debug"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Debug"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Debug<T>(this Logger<T> logger, T context, string format, object arg0, object arg1)
            => logger.Log(LogLevel.Debug, context, format, arg0, arg1);

        /// <summary>
        /// Requests that the text representation of the specified objects, using the
        /// specified format information, be logged with the level <see cref="LogLevel.Debug"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Debug"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <param name="arg2">The third object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Debug<T>(this Logger<T> logger, T context, string format, object arg0, object arg1, object arg2)
            => logger.Log(LogLevel.Debug, context, format, arg0, arg1, arg2);

        /// <summary>
        /// Requests that the text representation of the specified array of objects, using the
        /// specified format information, be logged with the level <see cref="LogLevel.Debug"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Debug"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
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
        public static void Debug<T>(this Logger<T> logger, T context, string format, params object[] args)
            => logger.Log(LogLevel.Debug, context, format, args);
        #endregion

        #region Info
        /// <summary>
        /// Requests that the specified message be logged with level <see cref="LogLevel.Info"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Info"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
        /// <param name="message">The message requested for logging.</param>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Info<T>(this Logger<T> logger, T context, string message)
            => logger.Log(LogLevel.Info, context, message);

        /// <summary>
        /// Requests that the text representation of the specified object, using the
        /// specified format information, be logged with the level <see cref="LogLevel.Info"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Info"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">An object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Info<T>(this Logger<T> logger, T context, string format, object arg0)
            => logger.Log(LogLevel.Info, context, format, arg0);

        /// <summary>
        /// Requests that the text representation of the specified objects, using the
        /// specified format information, be logged with the level <see cref="LogLevel.Info"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Info"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Info<T>(this Logger<T> logger, T context, string format, object arg0, object arg1)
            => logger.Log(LogLevel.Info, context, format, arg0, arg1);

        /// <summary>
        /// Requests that the text representation of the specified objects, using the
        /// specified format information, be logged with the level <see cref="LogLevel.Info"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Info"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <param name="arg2">The third object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Info<T>(this Logger<T> logger, T context, string format, object arg0, object arg1, object arg2)
            => logger.Log(LogLevel.Info, context, format, arg0, arg1, arg2);

        /// <summary>
        /// Requests that the text representation of the specified array of objects, using the
        /// specified format information, be logged with the level <see cref="LogLevel.Info"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Info"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
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
        public static void Info<T>(this Logger<T> logger, T context, string format, params object[] args)
            => logger.Log(LogLevel.Info, context, format, args);
        #endregion

        #region Warn
        /// <summary>
        /// Requests that the specified message be logged with level <see cref="LogLevel.Warn"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Warn"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
        /// <param name="message">The message requested for logging.</param>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Warn<T>(this Logger<T> logger, T context, string message)
            => logger.Log(LogLevel.Warn, context, message);

        /// <summary>
        /// Requests that the text representation of the specified object, using the
        /// specified format information, be logged with the level <see cref="LogLevel.Warn"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Warn"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">An object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Warn<T>(this Logger<T> logger, T context, string format, object arg0)
            => logger.Log(LogLevel.Warn, context, format, arg0);

        /// <summary>
        /// Requests that the text representation of the specified objects, using the
        /// specified format information, be logged with the level <see cref="LogLevel.Warn"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Warn"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Warn<T>(this Logger<T> logger, T context, string format, object arg0, object arg1)
            => logger.Log(LogLevel.Warn, context, format, arg0, arg1);

        /// <summary>
        /// Requests that the text representation of the specified objects, using the
        /// specified format information, be logged with the level <see cref="LogLevel.Warn"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Warn"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <param name="arg2">The third object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Warn<T>(this Logger<T> logger, T context, string format, object arg0, object arg1, object arg2)
            => logger.Log(LogLevel.Warn, context, format, arg0, arg1, arg2);

        /// <summary>
        /// Requests that the text representation of the specified array of objects, using the
        /// specified format information, be logged with the level <see cref="LogLevel.Warn"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Warn"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
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
        public static void Warn<T>(this Logger<T> logger, T context, string format, params object[] args)
            => logger.Log(LogLevel.Warn, context, format, args);
        #endregion

        #region Error
        /// <summary>
        /// Requests that the specified message be logged with level <see cref="LogLevel.Error"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Error"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
        /// <param name="message">The message requested for logging.</param>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Error<T>(this Logger<T> logger, T context, string message)
            => logger.Log(LogLevel.Error, context, message);

        /// <summary>
        /// Requests that the text representation of the specified object, using the
        /// specified format information, be logged with the level <see cref="LogLevel.Error"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Error"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">An object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Error<T>(this Logger<T> logger, T context, string format, object arg0)
            => logger.Log(LogLevel.Error, context, format, arg0);

        /// <summary>
        /// Requests that the text representation of the specified objects, using the
        /// specified format information, be logged with the level <see cref="LogLevel.Error"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Error"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Error<T>(this Logger<T> logger, T context, string format, object arg0, object arg1)
            => logger.Log(LogLevel.Error, context, format, arg0, arg1);

        /// <summary>
        /// Requests that the text representation of the specified objects, using the
        /// specified format information, be logged with the level <see cref="LogLevel.Error"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Error"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <param name="arg2">The third object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Error<T>(this Logger<T> logger, T context, string format, object arg0, object arg1, object arg2)
            => logger.Log(LogLevel.Error, context, format, arg0, arg1, arg2);

        /// <summary>
        /// Requests that the text representation of the specified array of objects, using the
        /// specified format information, be logged with the level <see cref="LogLevel.Error"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Error"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
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
        public static void Error<T>(this Logger<T> logger, T context, string format, params object[] args)
            => logger.Log(LogLevel.Error, context, format, args);
        #endregion

        #region Fatal
        /// <summary>
        /// Requests that the specified message be logged with level <see cref="LogLevel.Fatal"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Fatal"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
        /// <param name="message">The message requested for logging.</param>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Fatal<T>(this Logger<T> logger, T context, string message)
            => logger.Log(LogLevel.Fatal, context, message);

        /// <summary>
        /// Requests that the text representation of the specified object, using the
        /// specified format information, be logged with the level <see cref="LogLevel.Fatal"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Fatal"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">An object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Fatal<T>(this Logger<T> logger, T context, string format, object arg0)
            => logger.Log(LogLevel.Fatal, context, format, arg0);

        /// <summary>
        /// Requests that the text representation of the specified objects, using the
        /// specified format information, be logged with the level <see cref="LogLevel.Fatal"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Fatal"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Fatal<T>(this Logger<T> logger, T context, string format, object arg0, object arg1)
            => logger.Log(LogLevel.Fatal, context, format, arg0, arg1);

        /// <summary>
        /// Requests that the text representation of the specified objects, using the
        /// specified format information, be logged with the level <see cref="LogLevel.Fatal"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Fatal"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to write using <paramref name="format"/>.</param>
        /// <param name="arg1">The second object to write using <paramref name="format"/>.</param>
        /// <param name="arg2">The third object to write using <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">The format specification in <paramref name="format"/> is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Fatal<T>(this Logger<T> logger, T context, string format, object arg0, object arg1, object arg2)
            => logger.Log(LogLevel.Fatal, context, format, arg0, arg1, arg2);

        /// <summary>
        /// Requests that the text representation of the specified array of objects, using the
        /// specified format information, be logged with the level <see cref="LogLevel.Fatal"/>.
        /// </summary>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Fatal"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">The domain-specific information that provides additional context about the message.</param>
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
        public static void Fatal<T>(this Logger<T> logger, T context, string format, params object[] args)
            => logger.Log(LogLevel.Fatal, context, format, args);
        #endregion

    }
}
