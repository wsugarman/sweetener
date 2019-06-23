using System;
using System.Globalization;

namespace Sweetener.Logging 
{
    /// <summary>
    /// A <see cref="Logger{T}"/> for recording formattable <see cref="string"/> values.
    /// </summary>
    public abstract partial class Logger : Logger<string>
    {
        /// <summary>
        /// Gets an object that controls formatting. 
        /// </summary>
        public IFormatProvider FormatProvider { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class for the current
        /// culture that fulfills all logging requests.
        /// </summary>
        protected Logger()
            : this(LogLevel.Trace)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class for the current
        /// culture that fulfills all logging requests above a specified minimum
        /// <see cref="LogLevel"/>
        /// </summary>
        /// <param name="minLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minLevel"/> is unrecognized.</exception>
        protected Logger(LogLevel minLevel)
            : this(minLevel, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class for a particular
        /// culture that fulfills all logging requests above a specified minimum
        /// <see cref="LogLevel"/>
        /// </summary>
        /// <remarks>
        /// If <paramref name="formatProvider"/> is <see langword="null"/>, the formatting of the current culture is used.
        /// </remarks>
        /// <param name="minLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <param name="formatProvider">An <see cref="IFormatProvider"/> object for a specific culture.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minLevel"/> is unrecognized.</exception>
        protected Logger(LogLevel minLevel, IFormatProvider formatProvider)
            : base(minLevel)
        {
            if (formatProvider == null)
                formatProvider = CultureInfo.CurrentCulture;

            FormatProvider = formatProvider;
        }
    }
}
