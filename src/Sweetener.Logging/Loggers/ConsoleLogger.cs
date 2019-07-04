using System;
using System.IO;

namespace Sweetener.Logging
{
    /// <summary>
    /// A <see cref="Logger"/> that writes its entries to the console.
    /// </summary>
    public class ConsoleLogger : TemplateLogger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleLogger"/> class for the
        /// current culture that fulfills all logging requests using a default template.
        /// </summary>
        public ConsoleLogger()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleLogger"/> class for the
        /// current culture that fulfills all logging requests above a specified minimum
        /// <see cref="LogLevel"/> using a default template.
        /// </summary>
        /// <param name="minLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minLevel"/> is an unknown value.</exception>
        public ConsoleLogger(LogLevel minLevel)
            : base(minLevel)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleLogger"/> class for the
        /// current culture that fulfills all logging requests above a specified minimum
        /// <see cref="LogLevel"/> using a custom template.
        /// </summary>
        /// <param name="minLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <param name="template">A format string that describes the layout of each log entry.</param>
        /// <exception cref="ArgumentNullException"><paramref name="template"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minLevel"/> is an unknown value.</exception>
        /// <exception cref="FormatException">The <paramref name="template"/> is not formatted correctly.</exception>
        public ConsoleLogger(LogLevel minLevel, string template)
            : base(minLevel, template)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleLogger"/> class for a
        /// particular culture that fulfills all logging requests above a specified minimum
        /// <see cref="LogLevel"/> using a custom template.
        /// </summary>
        /// <remarks>
        /// If <paramref name="formatProvider"/> is <see langword="null"/>, the formatting of the current culture is used.
        /// </remarks>
        /// <param name="minLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <param name="formatProvider">An <see cref="IFormatProvider"/> object for a specific culture.</param>
        /// <param name="template">A format string that describes the layout of each log entry.</param>
        /// <exception cref="ArgumentNullException"><paramref name="template"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minLevel"/> is an unknown value.</exception>
        /// <exception cref="FormatException">The <paramref name="template"/> is not formatted correctly.</exception>
        public ConsoleLogger(LogLevel minLevel, IFormatProvider formatProvider, string template)
            : base(minLevel, formatProvider, template)
        { }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="ConsoleLogger"/> and
        /// optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/>
        /// to release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                try
                {
                    if (disposing)
                        Console.Out.Flush();
                }
                finally
                {
                    base.Dispose(disposing);
                }
            }
        }

        // TODO: Should ConsoleLogger override the logging methods to update the possible exceptions?

        /// <summary>
        /// Writes the <paramref name="message"/> to the console.
        /// </summary>
        /// <param name="message">The value to be written.</param>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        protected override void WriteLine(string message)
            => Console.WriteLine(message);
    }
}
