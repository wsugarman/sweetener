// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sweetener.IO
{
    /// <summary>
    /// Provides a unified stream over a sequence of byte segments.
    /// </summary>
    public class SegmentedStream : Stream
    {
        /// <summary>
        /// Gets a value indicating whether the current stream supports reading.
        /// </summary>
        /// <value><see langword="true"/> if the stream supports reading; otherwise, <see langword="false"/>.</value>
        public override bool CanRead => true;

        /// <summary>
        /// Gets a value indicating whether the current stream supports seeking.
        /// </summary>
        /// <value><see langword="true"/> if the stream supports seeking; otherwise, <see langword="false"/>.</value>
        public override bool CanSeek => false;

        /// <summary>
        /// Gets a value indicating whether the current stream supports writing.
        /// </summary>
        /// <value><see langword="true"/> if the stream supports writing; otherwise, <see langword="false"/>.</value>
        public override bool CanWrite => false;

        /// <summary>
        /// Gets the length of the data available on the stream.
        /// This property is not currently supported and always throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <value>The length of the data available on the stream.</value>
        /// <exception cref="NotSupportedException">Any use of this property.</exception>
        public override long Length => throw new NotSupportedException();

        /// <summary>
        /// Gets or sets the current position in the stream.
        /// This property is not currently supported and always throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <value>The current position in the stream.</value>
        /// <exception cref="NotSupportedException">Any use of this property.</exception>
        public override long Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        private int _currentIndex;
        private readonly IEnumerator<ReadOnlyMemory<byte>> _segments;

        /// <summary>
        /// Initializes a new instance of the <see cref="SegmentedStream"/> class
        /// based on the specified sequence of byte arrays.
        /// </summary>
        /// <param name="buffers">The sequence of byte arrays to be read as a single logical stream.</param>
        /// <exception cref="ArgumentNullException"><paramref name="buffers"/> is <see langword="null"/>.</exception>
        public SegmentedStream(IEnumerable<byte[]> buffers)
            : this(buffers?.Select(x => new ReadOnlyMemory<byte>(x ?? throw new InvalidOperationException(SR.ReadNullArray)))!)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SegmentedStream"/> class
        /// based on the specified sequence of contiguous regions of memory.
        /// </summary>
        /// <param name="regions">
        /// The sequence of contiguous regions of memory to be read as a single logical stream.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="regions"/> is <see langword="null"/>.</exception>
        public SegmentedStream(IEnumerable<ReadOnlyMemory<byte>> regions)
            => _segments = regions?.GetEnumerator() ?? throw new ArgumentNullException(nameof(regions));

        /// <summary>
        /// Overrides the <see cref="Stream.Flush"/> method so that no action is performed.
        /// </summary>
        /// <remarks>
        /// The <see cref="SegmentedStream"/> does not support write operations, and therefore
        /// this method is redundant.
        /// </remarks>
        public override void Flush()
        { }

        /// <summary>
        /// Reads a block of bytes from the stream and writes the data into a given buffer.
        /// </summary>
        /// <param name="buffer">
        /// When this method returns, contains the specified byte array with the values between
        /// <paramref name="offset"/> and (<paramref name="offset"/> + <paramref name="count"/> - 1) replaced
        /// by the bytes read from the stream.
        /// </param>
        /// <param name="offset">The byte offset in array at which the read bytes will be placed.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <returns>
        /// The total number of bytes written into the buffer. This can be less than the number of bytes requested
        /// if that number of bytes are not currently available, or zero if the end of the stream is reached
        /// before any bytes are read.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="offset"/> subtracted from the buffer length is less than <paramref name="count"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException"><paramref name="buffer"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="offset"/> or <paramref name="offset"/> is negative.
        /// </exception>
        public override int Read(byte[] buffer, int offset, int count)
            => Read(new Span<byte>(buffer ?? throw new ArgumentNullException(nameof(buffer)), offset, count));

        private int Read(Span<byte> buffer)
        {
            int requested = buffer.Length, remaining = buffer.Length;
            while (remaining > 0)
            {
                // If we're at the end of the current segment, check if we can read the next one
                if (_segments.Current.Length - _currentIndex == 0)
                {
                    if (!_segments.MoveNext())
                        break;

                    _currentIndex = 0;
                }

                // Slice the current span based on the current index
                ReadOnlySpan<byte> current = _segments.Current.Span.Slice(_currentIndex);
                if (remaining < current.Length)
                    current = current.Slice(0, remaining);

                current.CopyTo(buffer);
                buffer = buffer.Slice(current.Length);
                _currentIndex += current.Length;
                remaining -= current.Length;
            }

            return requested - remaining;
        }

        /// <summary>
        /// Sets the current position of the stream to the given value.
        /// This method is not currently supported and always throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="offset">This parameter is not used.</param>
        /// <param name="origin">This parameter is not used.</param>
        /// <returns>The position in the stream.</returns>
        /// <exception cref="NotSupportedException">Any use of this method.</exception>
        public override long Seek(long offset, SeekOrigin origin)
            => throw new NotSupportedException();

        /// <summary>
        /// Sets the length of the stream.
        /// This method is not currently supported and always throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="value">This parameter is not used.</param>
        /// <exception cref="NotSupportedException">Any use of this method.</exception>
        public override void SetLength(long value)
            => throw new NotSupportedException();

        /// <summary>
        /// Writes data to the stream from a specified range of a byte array.
        /// This method is not currently supported and always throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="buffer">This parameter is not used.</param>
        /// <param name="offset">This parameter is not used.</param>
        /// <param name="count">This parameter is not used.</param>
        /// <exception cref="NotSupportedException">Any use of this method.</exception>
        public override void Write(byte[] buffer, int offset, int count)
            => throw new NotSupportedException();
    }
}
