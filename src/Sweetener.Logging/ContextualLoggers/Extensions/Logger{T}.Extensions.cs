// Generated from Logger{T}.Extensions.tt
using System;

namespace Sweetener.Diagnostics.Logging.Extensions
{
    /// <content>
    /// The portion of <see cref="LoggerExtensions"/> for <see cref="Logger{T}"/> methods.
    /// </content>
    public static partial class LoggerExtensions
    {
        #region Trace
        /// <summary>
        /// Requests that the specified message and its context be logged with the
        /// level <see cref="LogLevel.Trace"/>.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Trace"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context surrounding the message.
        /// </param>
        /// <param name="message">The message to be logged.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Trace<T>(this Logger<T> logger, T context, string message)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Trace, context, message);
        }

        /// <summary>
        /// Requests that the formatted message and its context be logged with the level
        /// <see cref="LogLevel.Trace"/> using the same semantics as the
        /// <see cref="string.Format(string, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Trace"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context about the message.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">An object to format and log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> or <paramref name="format"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <para><paramref name="format"/> is not a valid composite format string.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than 0 (zero), or greater than or equal to
        /// the number of objects to be formatted (which, for this method overload, is one).
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Trace<T>(this Logger<T> logger, T context, string format, object arg0)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Trace, context, format, arg0);
        }

        /// <summary>
        /// Requests that the formatted message and its context be logged with the level
        /// <see cref="LogLevel.Trace"/> using the same semantics as the
        /// <see cref="string.Format(string, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Trace"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context about the message.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format and log.</param>
        /// <param name="arg1">The second object to format and log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> or <paramref name="format"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <para><paramref name="format"/> is not a valid composite format string.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than 0 (zero), or greater than or equal to
        /// the number of objects to be formatted (which, for this method overload, is two).
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Trace<T>(this Logger<T> logger, T context, string format, object arg0, object arg1)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Trace, context, format, arg0, arg1);
        }

        /// <summary>
        /// Requests that the formatted message and its context be logged with the level
        /// <see cref="LogLevel.Trace"/> using the same semantics as the
        /// <see cref="string.Format(string, object, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Trace"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context about the message.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format and log.</param>
        /// <param name="arg1">The second object to format and log.</param>
        /// <param name="arg2">The third object to format and log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> or <paramref name="format"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <para><paramref name="format"/> is not a valid composite format string.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than 0 (zero), or greater than or equal to
        /// the number of objects to be formatted (which, for this method overload, is three).
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Trace<T>(this Logger<T> logger, T context, string format, object arg0, object arg1, object arg2)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Trace, context, format, arg0, arg1, arg2);
        }

        /// <summary>
        /// Requests that the formatted message and its context be logged with the level
        /// <see cref="LogLevel.Trace"/> using the same semantics as the
        /// <see cref="string.Format(string, object[])"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Trace"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context about the message.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format and log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/>, <paramref name="format"/>, or <paramref name="args"/>
        /// is <see langword="null"/>.
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
        public static void Trace<T>(this Logger<T> logger, T context, string format, params object[] args)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Trace, context, format, args);
        }
        #endregion

        #region Debug
        /// <summary>
        /// Requests that the specified message and its context be logged with the
        /// level <see cref="LogLevel.Debug"/>.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Debug"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context surrounding the message.
        /// </param>
        /// <param name="message">The message to be logged.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Debug<T>(this Logger<T> logger, T context, string message)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Debug, context, message);
        }

        /// <summary>
        /// Requests that the formatted message and its context be logged with the level
        /// <see cref="LogLevel.Debug"/> using the same semantics as the
        /// <see cref="string.Format(string, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Debug"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context about the message.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">An object to format and log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> or <paramref name="format"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <para><paramref name="format"/> is not a valid composite format string.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than 0 (zero), or greater than or equal to
        /// the number of objects to be formatted (which, for this method overload, is one).
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Debug<T>(this Logger<T> logger, T context, string format, object arg0)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Debug, context, format, arg0);
        }

        /// <summary>
        /// Requests that the formatted message and its context be logged with the level
        /// <see cref="LogLevel.Debug"/> using the same semantics as the
        /// <see cref="string.Format(string, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Debug"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context about the message.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format and log.</param>
        /// <param name="arg1">The second object to format and log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> or <paramref name="format"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <para><paramref name="format"/> is not a valid composite format string.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than 0 (zero), or greater than or equal to
        /// the number of objects to be formatted (which, for this method overload, is two).
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Debug<T>(this Logger<T> logger, T context, string format, object arg0, object arg1)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Debug, context, format, arg0, arg1);
        }

        /// <summary>
        /// Requests that the formatted message and its context be logged with the level
        /// <see cref="LogLevel.Debug"/> using the same semantics as the
        /// <see cref="string.Format(string, object, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Debug"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context about the message.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format and log.</param>
        /// <param name="arg1">The second object to format and log.</param>
        /// <param name="arg2">The third object to format and log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> or <paramref name="format"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <para><paramref name="format"/> is not a valid composite format string.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than 0 (zero), or greater than or equal to
        /// the number of objects to be formatted (which, for this method overload, is three).
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Debug<T>(this Logger<T> logger, T context, string format, object arg0, object arg1, object arg2)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Debug, context, format, arg0, arg1, arg2);
        }

        /// <summary>
        /// Requests that the formatted message and its context be logged with the level
        /// <see cref="LogLevel.Debug"/> using the same semantics as the
        /// <see cref="string.Format(string, object[])"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Debug"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context about the message.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format and log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/>, <paramref name="format"/>, or <paramref name="args"/>
        /// is <see langword="null"/>.
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
        public static void Debug<T>(this Logger<T> logger, T context, string format, params object[] args)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Debug, context, format, args);
        }
        #endregion

        #region Info
        /// <summary>
        /// Requests that the specified message and its context be logged with the
        /// level <see cref="LogLevel.Info"/>.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Info"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context surrounding the message.
        /// </param>
        /// <param name="message">The message to be logged.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Info<T>(this Logger<T> logger, T context, string message)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Info, context, message);
        }

        /// <summary>
        /// Requests that the formatted message and its context be logged with the level
        /// <see cref="LogLevel.Info"/> using the same semantics as the
        /// <see cref="string.Format(string, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Info"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context about the message.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">An object to format and log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> or <paramref name="format"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <para><paramref name="format"/> is not a valid composite format string.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than 0 (zero), or greater than or equal to
        /// the number of objects to be formatted (which, for this method overload, is one).
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Info<T>(this Logger<T> logger, T context, string format, object arg0)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Info, context, format, arg0);
        }

        /// <summary>
        /// Requests that the formatted message and its context be logged with the level
        /// <see cref="LogLevel.Info"/> using the same semantics as the
        /// <see cref="string.Format(string, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Info"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context about the message.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format and log.</param>
        /// <param name="arg1">The second object to format and log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> or <paramref name="format"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <para><paramref name="format"/> is not a valid composite format string.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than 0 (zero), or greater than or equal to
        /// the number of objects to be formatted (which, for this method overload, is two).
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Info<T>(this Logger<T> logger, T context, string format, object arg0, object arg1)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Info, context, format, arg0, arg1);
        }

        /// <summary>
        /// Requests that the formatted message and its context be logged with the level
        /// <see cref="LogLevel.Info"/> using the same semantics as the
        /// <see cref="string.Format(string, object, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Info"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context about the message.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format and log.</param>
        /// <param name="arg1">The second object to format and log.</param>
        /// <param name="arg2">The third object to format and log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> or <paramref name="format"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <para><paramref name="format"/> is not a valid composite format string.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than 0 (zero), or greater than or equal to
        /// the number of objects to be formatted (which, for this method overload, is three).
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Info<T>(this Logger<T> logger, T context, string format, object arg0, object arg1, object arg2)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Info, context, format, arg0, arg1, arg2);
        }

        /// <summary>
        /// Requests that the formatted message and its context be logged with the level
        /// <see cref="LogLevel.Info"/> using the same semantics as the
        /// <see cref="string.Format(string, object[])"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Info"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context about the message.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format and log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/>, <paramref name="format"/>, or <paramref name="args"/>
        /// is <see langword="null"/>.
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
        public static void Info<T>(this Logger<T> logger, T context, string format, params object[] args)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Info, context, format, args);
        }
        #endregion

        #region Warn
        /// <summary>
        /// Requests that the specified message and its context be logged with the
        /// level <see cref="LogLevel.Warn"/>.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Warn"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context surrounding the message.
        /// </param>
        /// <param name="message">The message to be logged.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Warn<T>(this Logger<T> logger, T context, string message)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Warn, context, message);
        }

        /// <summary>
        /// Requests that the formatted message and its context be logged with the level
        /// <see cref="LogLevel.Warn"/> using the same semantics as the
        /// <see cref="string.Format(string, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Warn"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context about the message.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">An object to format and log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> or <paramref name="format"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <para><paramref name="format"/> is not a valid composite format string.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than 0 (zero), or greater than or equal to
        /// the number of objects to be formatted (which, for this method overload, is one).
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Warn<T>(this Logger<T> logger, T context, string format, object arg0)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Warn, context, format, arg0);
        }

        /// <summary>
        /// Requests that the formatted message and its context be logged with the level
        /// <see cref="LogLevel.Warn"/> using the same semantics as the
        /// <see cref="string.Format(string, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Warn"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context about the message.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format and log.</param>
        /// <param name="arg1">The second object to format and log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> or <paramref name="format"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <para><paramref name="format"/> is not a valid composite format string.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than 0 (zero), or greater than or equal to
        /// the number of objects to be formatted (which, for this method overload, is two).
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Warn<T>(this Logger<T> logger, T context, string format, object arg0, object arg1)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Warn, context, format, arg0, arg1);
        }

        /// <summary>
        /// Requests that the formatted message and its context be logged with the level
        /// <see cref="LogLevel.Warn"/> using the same semantics as the
        /// <see cref="string.Format(string, object, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Warn"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context about the message.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format and log.</param>
        /// <param name="arg1">The second object to format and log.</param>
        /// <param name="arg2">The third object to format and log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> or <paramref name="format"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <para><paramref name="format"/> is not a valid composite format string.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than 0 (zero), or greater than or equal to
        /// the number of objects to be formatted (which, for this method overload, is three).
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Warn<T>(this Logger<T> logger, T context, string format, object arg0, object arg1, object arg2)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Warn, context, format, arg0, arg1, arg2);
        }

        /// <summary>
        /// Requests that the formatted message and its context be logged with the level
        /// <see cref="LogLevel.Warn"/> using the same semantics as the
        /// <see cref="string.Format(string, object[])"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Warn"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context about the message.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format and log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/>, <paramref name="format"/>, or <paramref name="args"/>
        /// is <see langword="null"/>.
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
        public static void Warn<T>(this Logger<T> logger, T context, string format, params object[] args)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Warn, context, format, args);
        }
        #endregion

        #region Error
        /// <summary>
        /// Requests that the specified message and its context be logged with the
        /// level <see cref="LogLevel.Error"/>.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Error"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context surrounding the message.
        /// </param>
        /// <param name="message">The message to be logged.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Error<T>(this Logger<T> logger, T context, string message)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Error, context, message);
        }

        /// <summary>
        /// Requests that the formatted message and its context be logged with the level
        /// <see cref="LogLevel.Error"/> using the same semantics as the
        /// <see cref="string.Format(string, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Error"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context about the message.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">An object to format and log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> or <paramref name="format"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <para><paramref name="format"/> is not a valid composite format string.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than 0 (zero), or greater than or equal to
        /// the number of objects to be formatted (which, for this method overload, is one).
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Error<T>(this Logger<T> logger, T context, string format, object arg0)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Error, context, format, arg0);
        }

        /// <summary>
        /// Requests that the formatted message and its context be logged with the level
        /// <see cref="LogLevel.Error"/> using the same semantics as the
        /// <see cref="string.Format(string, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Error"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context about the message.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format and log.</param>
        /// <param name="arg1">The second object to format and log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> or <paramref name="format"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <para><paramref name="format"/> is not a valid composite format string.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than 0 (zero), or greater than or equal to
        /// the number of objects to be formatted (which, for this method overload, is two).
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Error<T>(this Logger<T> logger, T context, string format, object arg0, object arg1)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Error, context, format, arg0, arg1);
        }

        /// <summary>
        /// Requests that the formatted message and its context be logged with the level
        /// <see cref="LogLevel.Error"/> using the same semantics as the
        /// <see cref="string.Format(string, object, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Error"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context about the message.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format and log.</param>
        /// <param name="arg1">The second object to format and log.</param>
        /// <param name="arg2">The third object to format and log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> or <paramref name="format"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <para><paramref name="format"/> is not a valid composite format string.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than 0 (zero), or greater than or equal to
        /// the number of objects to be formatted (which, for this method overload, is three).
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Error<T>(this Logger<T> logger, T context, string format, object arg0, object arg1, object arg2)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Error, context, format, arg0, arg1, arg2);
        }

        /// <summary>
        /// Requests that the formatted message and its context be logged with the level
        /// <see cref="LogLevel.Error"/> using the same semantics as the
        /// <see cref="string.Format(string, object[])"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Error"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context about the message.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format and log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/>, <paramref name="format"/>, or <paramref name="args"/>
        /// is <see langword="null"/>.
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
        public static void Error<T>(this Logger<T> logger, T context, string format, params object[] args)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Error, context, format, args);
        }
        #endregion

        #region Fatal
        /// <summary>
        /// Requests that the specified message and its context be logged with the
        /// level <see cref="LogLevel.Fatal"/>.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Fatal"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context surrounding the message.
        /// </param>
        /// <param name="message">The message to be logged.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Fatal<T>(this Logger<T> logger, T context, string message)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Fatal, context, message);
        }

        /// <summary>
        /// Requests that the formatted message and its context be logged with the level
        /// <see cref="LogLevel.Fatal"/> using the same semantics as the
        /// <see cref="string.Format(string, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Fatal"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context about the message.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">An object to format and log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> or <paramref name="format"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <para><paramref name="format"/> is not a valid composite format string.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than 0 (zero), or greater than or equal to
        /// the number of objects to be formatted (which, for this method overload, is one).
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Fatal<T>(this Logger<T> logger, T context, string format, object arg0)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Fatal, context, format, arg0);
        }

        /// <summary>
        /// Requests that the formatted message and its context be logged with the level
        /// <see cref="LogLevel.Fatal"/> using the same semantics as the
        /// <see cref="string.Format(string, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Fatal"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context about the message.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format and log.</param>
        /// <param name="arg1">The second object to format and log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> or <paramref name="format"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <para><paramref name="format"/> is not a valid composite format string.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than 0 (zero), or greater than or equal to
        /// the number of objects to be formatted (which, for this method overload, is two).
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Fatal<T>(this Logger<T> logger, T context, string format, object arg0, object arg1)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Fatal, context, format, arg0, arg1);
        }

        /// <summary>
        /// Requests that the formatted message and its context be logged with the level
        /// <see cref="LogLevel.Fatal"/> using the same semantics as the
        /// <see cref="string.Format(string, object, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Fatal"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context about the message.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format and log.</param>
        /// <param name="arg1">The second object to format and log.</param>
        /// <param name="arg2">The third object to format and log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> or <paramref name="format"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <para><paramref name="format"/> is not a valid composite format string.</para>
        /// <para>-or-</para>
        /// <para>
        /// The index of a format item is less than 0 (zero), or greater than or equal to
        /// the number of objects to be formatted (which, for this method overload, is three).
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Fatal<T>(this Logger<T> logger, T context, string format, object arg0, object arg1, object arg2)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Fatal, context, format, arg0, arg1, arg2);
        }

        /// <summary>
        /// Requests that the formatted message and its context be logged with the level
        /// <see cref="LogLevel.Fatal"/> using the same semantics as the
        /// <see cref="string.Format(string, object[])"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Fatal"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the domain-specific context.</typeparam>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="context">
        /// The domain-specific information that provides additional context about the message.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format and log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/>, <paramref name="format"/>, or <paramref name="args"/>
        /// is <see langword="null"/>.
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
        public static void Fatal<T>(this Logger<T> logger, T context, string format, params object[] args)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Fatal, context, format, args);
        }
        #endregion

    }
}
