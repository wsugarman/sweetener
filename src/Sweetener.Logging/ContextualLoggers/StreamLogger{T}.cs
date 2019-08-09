using System;
using System.IO;
using System.Text;

namespace Sweetener.Diagnostics.Logging
{
    /// <summary>
    /// A <see cref="Logger{T}"/> that writes its entries to a <see cref="Stream"/> using
    /// a particular <see cref="System.Text.Encoding"/>.
    /// </summary>
    /// <typeparam name="T">The type of the domain-specific context.</typeparam>
    public class StreamLogger<T> : TemplateLogger<T>
    {
        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="StreamLogger{T}"/> will
        /// flush its buffer to the underlying stream after fulfilling every logging request.
        /// </summary>
        /// <value>
        /// <see langword="true"/> to force <see cref="StreamLogger{T}"/> to flush its buffer;
        /// otherwise, <see langword="false"/>.
        /// </value>
        public bool AutoFlush
        {
            get => _writer.AutoFlush;
            set => _writer.AutoFlush = value;
        }

        /// <summary>
        /// Gets the underlying stream that interfaces with a backing store.
        /// </summary>
        /// <value>The stream this <see cref="StreamLogger{T}"/> is writing to.</value>
        public Stream BaseStream => _writer.BaseStream;

        /// <summary>
        /// Gets the <see cref="System.Text.Encoding"/> in which the logs are written.
        /// </summary>
        /// <value>
        /// The <see cref="System.Text.Encoding"/> specified in the constructor for the
        /// current instance, or <see cref="UTF8Encoding"/> if an encoding was not specified.
        /// </value>
        public Encoding Encoding => _writer.Encoding;

        /// <summary>
        /// Gets or sets the line terminator string used by the current <see cref="StreamLogger{T}"/>.
        /// </summary>
        /// <remarks>
        /// The default value is <see cref="Environment.NewLine"/>, and if the <see cref="NewLine"/>
        /// is set to <see langword="null"/> then the default value will be used.
        /// </remarks>
        /// <value>The line terminator string for the current <see cref="StreamLogger{T}"/>.</value>
        public string NewLine
        {
            get => _writer.NewLine;
            set => _writer.NewLine = value;
        }

        private readonly StreamWriter _writer;

        private const int DefaultBufferSize = 1024;

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamLogger{T}"/> class for the
        /// current culture that fulfills all logging requests and writes to the given
        /// <see cref="Stream"/> using the default UTF-8 encoded template.
        /// </summary>
        /// <param name="stream">The stream to log to.</param>
        /// <exception cref="ArgumentException"><paramref name="stream"/> is not writable.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <see langword="null"/>.</exception>
        public StreamLogger(Stream stream)
            : this(stream, LogLevel.Trace)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamLogger{T}"/> class for the
        /// current culture that fulfills all logging requests above a specified minimum
        /// <see cref="LogLevel"/> and writes them to the given <see cref="Stream"/> the
        /// default UTF-8 encoded template.
        /// </summary>
        /// <param name="stream">The stream to log to.</param>
        /// <param name="minLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <exception cref="ArgumentException"><paramref name="stream"/> is not writable.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minLevel"/> is an unknown value.</exception>
        public StreamLogger(Stream stream, LogLevel minLevel)
            : this(stream, minLevel, DefaultTemplate)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamLogger{T}"/> class for the
        /// current culture that fulfills all logging requests above a specified minimum
        /// <see cref="LogLevel"/> and writes them to the given <see cref="Stream"/> using
        /// a custom UTF-8 encoded template.
        /// </summary>
        /// <param name="stream">The stream to log to.</param>
        /// <param name="minLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <param name="template">A format string that describes the layout of each log entry.</param>
        /// <exception cref="ArgumentException"><paramref name="stream"/> is not writable.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> or <paramref name="template"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minLevel"/> is an unknown value.</exception>
        /// <exception cref="FormatException">The <paramref name="template"/> is not formatted correctly.</exception>
        public StreamLogger(Stream stream, LogLevel minLevel, string template)
            : this(stream, minLevel, null, template)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamLogger{T}"/> class for a
        /// particular culture that fulfills all logging requests above a specified minimum
        /// <see cref="LogLevel"/> and writes them to the given <see cref="Stream"/> using
        /// a custom UTF-8 encoded template.
        /// </summary>
        /// <remarks>
        /// If <paramref name="formatProvider"/> is <see langword="null"/>, the formatting
        /// of the current culture is used.
        /// </remarks>
        /// <param name="stream">The stream to log to.</param>
        /// <param name="minLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <param name="formatProvider">An <see cref="IFormatProvider"/> object for a specific culture.</param>
        /// <param name="template">A format string that describes the layout of each log entry.</param>
        /// <exception cref="ArgumentException"><paramref name="stream"/> is not writable.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> or <paramref name="template"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minLevel"/> is an unknown value.</exception>
        /// <exception cref="FormatException">The <paramref name="template"/> is not formatted correctly.</exception>
        public StreamLogger(Stream stream, LogLevel minLevel, IFormatProvider formatProvider, string template)
            : this(stream, minLevel, formatProvider, template, EncodingCache.UTF8NoBOM)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamLogger{T}"/> class for a
        /// particular culture that fulfills all logging requests above a specified minimum
        /// <see cref="LogLevel"/> and writes them to the given <see cref="Stream"/> using
        /// a specified <see cref="System.Text.Encoding"/> and custom template.
        /// </summary>
        /// <remarks>
        /// If <paramref name="formatProvider"/> is <see langword="null"/>, the formatting
        /// of the current culture is used.
        /// </remarks>
        /// <param name="stream">The stream to log to.</param>
        /// <param name="minLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <param name="formatProvider">An <see cref="IFormatProvider"/> object for a specific culture.</param>
        /// <param name="template">A format string that describes the layout of each log entry.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <exception cref="ArgumentException"><paramref name="stream"/> is not writable.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="stream"/>, <paramref name="template"/>, or
        /// <paramref name="encoding"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minLevel"/> is an unknown value.</exception>
        /// <exception cref="FormatException">The <paramref name="template"/> is not formatted correctly.</exception>
        public StreamLogger(Stream stream, LogLevel minLevel, IFormatProvider formatProvider, string template, Encoding encoding)
            : this(stream, minLevel, formatProvider, template, encoding, DefaultBufferSize)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamLogger{T}"/> class for a
        /// particular culture that fulfills all logging requests above a specified minimum
        /// <see cref="LogLevel"/> and writes them to the given <see cref="Stream"/> using
        /// a specified <see cref="System.Text.Encoding"/> and custom template.
        /// </summary>
        /// <remarks>
        /// If <paramref name="formatProvider"/> is <see langword="null"/>, the formatting
        /// of the current culture is used.
        /// </remarks>
        /// <param name="stream">The stream to log to.</param>
        /// <param name="minLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <param name="formatProvider">An <see cref="IFormatProvider"/> object for a specific culture.</param>
        /// <param name="template">A format string that describes the layout of each log entry.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="bufferSize">The buffer size, in 16-bit characters.</param>
        /// <exception cref="ArgumentException"><paramref name="stream"/> is not writable.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="stream"/>, <paramref name="template"/>, or
        /// <paramref name="encoding"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="minLevel"/> is an unknown value.</para>
        /// <para>-or-</para>
        /// <para><paramref name="bufferSize"/> is negative.</para>
        /// </exception>
        /// <exception cref="FormatException">The <paramref name="template"/> is not formatted correctly.</exception>
        public StreamLogger(
            Stream          stream,
            LogLevel        minLevel,
            IFormatProvider formatProvider,
            string          template,
            Encoding        encoding,
            int             bufferSize)
            : this(stream, minLevel, formatProvider, template, encoding, bufferSize, false)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamLogger{T}"/> class for a
        /// particular culture that fulfills all logging requests above a specified minimum
        /// <see cref="LogLevel"/> and writes them to the given <see cref="Stream"/> using
        /// a specified <see cref="System.Text.Encoding"/> and custom template.
        /// </summary>
        /// <remarks>
        /// If <paramref name="formatProvider"/> is <see langword="null"/>, the formatting
        /// of the current culture is used.
        /// </remarks>
        /// <param name="stream">The stream to log to.</param>
        /// <param name="minLevel">The minimum level of log requests that will be fulfilled.</param>
        /// <param name="formatProvider">An <see cref="IFormatProvider"/> object for a specific culture.</param>
        /// <param name="template">A format string that describes the layout of each log entry.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="bufferSize">The buffer size, in 16-bit characters.</param>
        /// <param name="leaveOpen">
        /// <see langword="true"/> to leave the stream open after the <see cref="StreamLogger{T}"/>
        /// object is disposed; otherwise, <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentException"><paramref name="stream"/> is not writable.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="stream"/>, <paramref name="template"/>, or
        /// <paramref name="encoding"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="minLevel"/> is an unknown value.</para>
        /// <para>-or-</para>
        /// <para><paramref name="bufferSize"/> is negative.</para>
        /// </exception>
        /// <exception cref="FormatException">The <paramref name="template"/> is not formatted correctly.</exception>
        public StreamLogger(
            Stream          stream,
            LogLevel        minLevel,
            IFormatProvider formatProvider,
            string          template,
            Encoding        encoding,
            int             bufferSize,
            bool            leaveOpen)
            : base(minLevel, formatProvider, template)
        {
            _writer = new StreamWriter(stream, encoding, bufferSize, leaveOpen);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="StreamLogger{T}"/> and
        /// optionally releases the managed resources.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is called by <see cref="Logger{T}.Dispose()"/> and possible Finalize in a
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
        /// <see langword="true"/> to release both managed and unmanaged resources;
        /// <see langword="false"/> to release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                    _writer.Dispose();
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        /// <summary>
        /// Clears all buffers for the current logger and causes any buffered data to
        /// be written to the underlying stream.
        /// </summary>
        /// <exception cref="EncoderFallbackException">
        /// The <see cref="Encoding"/> does not support displaying half of a Unicode surrogate pair.
        /// </exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        /// <exception cref="ObjectDisposedException">The current logger is disposed.</exception>
        public void Flush()
            => _writer.Flush();

        /// <summary>
        /// Writes the <paramref name="message"/> to the stream.
        /// </summary>
        /// <param name="message">The value to be written.</param>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        /// <exception cref="ObjectDisposedException">The current logger is disposed.</exception>
        protected override void WriteLine(string message)
            => _writer.WriteLine(message);
    }
}
