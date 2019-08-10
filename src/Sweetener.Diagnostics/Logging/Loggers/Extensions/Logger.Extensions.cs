// Generated from Logger.Extensions.tt
using System;

namespace Sweetener.Diagnostics.Logging.Extensions
{
    /// <summary>
    /// Provides a set of supplemental methods for <see cref="Logger"/> and <see cref="Logger{T}"/>.
    /// </summary>
    public static partial class LoggerExtensions
    {
        #region Trace
        /// <summary>
        /// Requests that the specified message be logged with the level <see cref="LogLevel.Trace"/>.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Trace"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="message">The message to be logged.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Trace(this Logger logger, string message)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Trace, message);
        }

        /// <summary>
        /// Requests that the formatted message be logged with the level <see cref="LogLevel.Trace"/>
        /// using the same semantics as the <see cref="string.Format(string, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Trace"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
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
        public static void Trace(this Logger logger, string format, object arg0)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Trace, format, arg0);
        }

        /// <summary>
        /// Requests that the formatted message be logged with the level <see cref="LogLevel.Trace"/>
        /// using the same semantics as the <see cref="string.Format(string, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Trace"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
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
        public static void Trace(this Logger logger, string format, object arg0, object arg1)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Trace, format, arg0, arg1);
        }

        /// <summary>
        /// Requests that the formatted message be logged with the level <see cref="LogLevel.Trace"/>
        /// using the same semantics as the <see cref="string.Format(string, object, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Trace"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
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
        public static void Trace(this Logger logger, string format, object arg0, object arg1, object arg2)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Trace, format, arg0, arg1, arg2);
        }

        /// <summary>
        /// Requests that the formatted message be logged with the level <see cref="LogLevel.Trace"/>
        /// using the same semantics as the <see cref="string.Format(string, object[])"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Trace"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
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
        public static void Trace(this Logger logger, string format, params object[] args)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Trace, format, args);
        }
        #endregion

        #region Debug
        /// <summary>
        /// Requests that the specified message be logged with the level <see cref="LogLevel.Debug"/>.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Debug"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="message">The message to be logged.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Debug(this Logger logger, string message)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Debug, message);
        }

        /// <summary>
        /// Requests that the formatted message be logged with the level <see cref="LogLevel.Debug"/>
        /// using the same semantics as the <see cref="string.Format(string, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Debug"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
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
        public static void Debug(this Logger logger, string format, object arg0)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Debug, format, arg0);
        }

        /// <summary>
        /// Requests that the formatted message be logged with the level <see cref="LogLevel.Debug"/>
        /// using the same semantics as the <see cref="string.Format(string, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Debug"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
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
        public static void Debug(this Logger logger, string format, object arg0, object arg1)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Debug, format, arg0, arg1);
        }

        /// <summary>
        /// Requests that the formatted message be logged with the level <see cref="LogLevel.Debug"/>
        /// using the same semantics as the <see cref="string.Format(string, object, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Debug"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
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
        public static void Debug(this Logger logger, string format, object arg0, object arg1, object arg2)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Debug, format, arg0, arg1, arg2);
        }

        /// <summary>
        /// Requests that the formatted message be logged with the level <see cref="LogLevel.Debug"/>
        /// using the same semantics as the <see cref="string.Format(string, object[])"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Debug"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
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
        public static void Debug(this Logger logger, string format, params object[] args)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Debug, format, args);
        }
        #endregion

        #region Info
        /// <summary>
        /// Requests that the specified message be logged with the level <see cref="LogLevel.Info"/>.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Info"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="message">The message to be logged.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Info(this Logger logger, string message)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Info, message);
        }

        /// <summary>
        /// Requests that the formatted message be logged with the level <see cref="LogLevel.Info"/>
        /// using the same semantics as the <see cref="string.Format(string, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Info"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
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
        public static void Info(this Logger logger, string format, object arg0)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Info, format, arg0);
        }

        /// <summary>
        /// Requests that the formatted message be logged with the level <see cref="LogLevel.Info"/>
        /// using the same semantics as the <see cref="string.Format(string, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Info"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
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
        public static void Info(this Logger logger, string format, object arg0, object arg1)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Info, format, arg0, arg1);
        }

        /// <summary>
        /// Requests that the formatted message be logged with the level <see cref="LogLevel.Info"/>
        /// using the same semantics as the <see cref="string.Format(string, object, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Info"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
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
        public static void Info(this Logger logger, string format, object arg0, object arg1, object arg2)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Info, format, arg0, arg1, arg2);
        }

        /// <summary>
        /// Requests that the formatted message be logged with the level <see cref="LogLevel.Info"/>
        /// using the same semantics as the <see cref="string.Format(string, object[])"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Info"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
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
        public static void Info(this Logger logger, string format, params object[] args)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Info, format, args);
        }
        #endregion

        #region Warn
        /// <summary>
        /// Requests that the specified message be logged with the level <see cref="LogLevel.Warn"/>.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Warn"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="message">The message to be logged.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Warn(this Logger logger, string message)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Warn, message);
        }

        /// <summary>
        /// Requests that the formatted message be logged with the level <see cref="LogLevel.Warn"/>
        /// using the same semantics as the <see cref="string.Format(string, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Warn"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
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
        public static void Warn(this Logger logger, string format, object arg0)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Warn, format, arg0);
        }

        /// <summary>
        /// Requests that the formatted message be logged with the level <see cref="LogLevel.Warn"/>
        /// using the same semantics as the <see cref="string.Format(string, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Warn"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
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
        public static void Warn(this Logger logger, string format, object arg0, object arg1)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Warn, format, arg0, arg1);
        }

        /// <summary>
        /// Requests that the formatted message be logged with the level <see cref="LogLevel.Warn"/>
        /// using the same semantics as the <see cref="string.Format(string, object, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Warn"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
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
        public static void Warn(this Logger logger, string format, object arg0, object arg1, object arg2)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Warn, format, arg0, arg1, arg2);
        }

        /// <summary>
        /// Requests that the formatted message be logged with the level <see cref="LogLevel.Warn"/>
        /// using the same semantics as the <see cref="string.Format(string, object[])"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Warn"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
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
        public static void Warn(this Logger logger, string format, params object[] args)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Warn, format, args);
        }
        #endregion

        #region Error
        /// <summary>
        /// Requests that the specified message be logged with the level <see cref="LogLevel.Error"/>.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Error"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="message">The message to be logged.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Error(this Logger logger, string message)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Error, message);
        }

        /// <summary>
        /// Requests that the formatted message be logged with the level <see cref="LogLevel.Error"/>
        /// using the same semantics as the <see cref="string.Format(string, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Error"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
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
        public static void Error(this Logger logger, string format, object arg0)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Error, format, arg0);
        }

        /// <summary>
        /// Requests that the formatted message be logged with the level <see cref="LogLevel.Error"/>
        /// using the same semantics as the <see cref="string.Format(string, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Error"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
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
        public static void Error(this Logger logger, string format, object arg0, object arg1)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Error, format, arg0, arg1);
        }

        /// <summary>
        /// Requests that the formatted message be logged with the level <see cref="LogLevel.Error"/>
        /// using the same semantics as the <see cref="string.Format(string, object, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Error"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
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
        public static void Error(this Logger logger, string format, object arg0, object arg1, object arg2)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Error, format, arg0, arg1, arg2);
        }

        /// <summary>
        /// Requests that the formatted message be logged with the level <see cref="LogLevel.Error"/>
        /// using the same semantics as the <see cref="string.Format(string, object[])"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Error"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
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
        public static void Error(this Logger logger, string format, params object[] args)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Error, format, args);
        }
        #endregion

        #region Fatal
        /// <summary>
        /// Requests that the specified message be logged with the level <see cref="LogLevel.Fatal"/>.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Fatal"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
        /// <param name="message">The message to be logged.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">The logger is disposed.</exception>
        public static void Fatal(this Logger logger, string message)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Fatal, message);
        }

        /// <summary>
        /// Requests that the formatted message be logged with the level <see cref="LogLevel.Fatal"/>
        /// using the same semantics as the <see cref="string.Format(string, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Fatal"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
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
        public static void Fatal(this Logger logger, string format, object arg0)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Fatal, format, arg0);
        }

        /// <summary>
        /// Requests that the formatted message be logged with the level <see cref="LogLevel.Fatal"/>
        /// using the same semantics as the <see cref="string.Format(string, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Fatal"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
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
        public static void Fatal(this Logger logger, string format, object arg0, object arg1)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Fatal, format, arg0, arg1);
        }

        /// <summary>
        /// Requests that the formatted message be logged with the level <see cref="LogLevel.Fatal"/>
        /// using the same semantics as the <see cref="string.Format(string, object, object, object)"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Fatal"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
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
        public static void Fatal(this Logger logger, string format, object arg0, object arg1, object arg2)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Fatal, format, arg0, arg1, arg2);
        }

        /// <summary>
        /// Requests that the formatted message be logged with the level <see cref="LogLevel.Fatal"/>
        /// using the same semantics as the <see cref="string.Format(string, object[])"/> method.
        /// </summary>
        /// <remarks>
        /// The log request will only be fulfilled if the <see cref="Logger.MinLevel"/> is less
        /// than or equal to <see cref="LogLevel.Fatal"/>.
        /// </remarks>
        /// <param name="logger">The <see cref="Logger"/> that will process the request.</param>
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
        public static void Fatal(this Logger logger, string format, params object[] args)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(LogLevel.Fatal, format, args);
        }
        #endregion

    }
}
