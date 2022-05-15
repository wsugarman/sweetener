// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sweetener.IO;

/// <summary>
/// Provides a unified stream over a sequence of byte segments.
/// </summary>
public sealed class SegmentedStream : Stream
{
    /// <summary>
    /// Gets a value indicating whether the current stream supports reading.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the stream supports reading and has not been closed;
    /// otherwise, <see langword="false"/>.
    /// </value>
    public override bool CanRead => !_isClosed;

    /// <summary>
    /// Gets a value indicating whether the current stream supports seeking.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the stream supports seeking and has not been closed;
    /// otherwise, <see langword="false"/>.
    /// </value>
    public override bool CanSeek => false;

    /// <summary>
    /// Gets a value indicating whether the current stream supports writing.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the stream supports writing and has not been closed;
    /// otherwise, <see langword="false"/>.
    /// </value>
    public override bool CanWrite => false;

    /// <summary>
    /// Gets the length of the data available on the stream.
    /// This property is not currently supported and always throws a <see cref="NotSupportedException"/>.
    /// </summary>
    /// <value>The length of the data available on the stream.</value>
    /// <exception cref="NotSupportedException">Any use of this property.</exception>
    public override long Length => throw new NotSupportedException(SR.StreamSeekNotSupportedMessage);

    /// <summary>
    /// Gets or sets the current position in the stream.
    /// This property is not currently supported and always throws a <see cref="NotSupportedException"/>.
    /// </summary>
    /// <value>The current position in the stream.</value>
    /// <exception cref="NotSupportedException">Any use of this property.</exception>
    public override long Position
    {
        get => throw new NotSupportedException(SR.StreamSeekNotSupportedMessage);
        set => throw new NotSupportedException(SR.StreamSeekNotSupportedMessage);
    }

    private bool _isClosed;
    private int _currentIndex;
    private readonly IEnumerator<ReadOnlyMemory<byte>> _segments;

    /// <summary>
    /// Initializes a new instance of the <see cref="SegmentedStream"/> class
    /// based on the specified sequence of byte arrays.
    /// </summary>
    /// <param name="buffers">The sequence of byte arrays to be read as a single logical stream.</param>
    /// <exception cref="ArgumentNullException"><paramref name="buffers"/> is <see langword="null"/>.</exception>
    public SegmentedStream(IEnumerable<byte[]?> buffers)
        : this(buffers?.Select(x => new ReadOnlyMemory<byte>(x))!)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="SegmentedStream"/> class
    /// based on the specified sequence of contiguous regions of memory.
    /// </summary>
    /// <param name="segments">
    /// The sequence of contiguous regions of memory to be read as a single logical stream.
    /// </param>
    /// <exception cref="ArgumentNullException"><paramref name="segments"/> is <see langword="null"/>.</exception>
    public SegmentedStream(IEnumerable<ReadOnlyMemory<byte>> segments)
        => _segments = segments?.GetEnumerator() ?? throw new ArgumentNullException(nameof(segments));

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="SegmentedStream"/> and
    /// optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">
    /// <see langword="true"/> to release both managed and unmanaged resources;
    /// <see langword="false"/> to release only unmanaged resources.
    /// </param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
            _isClosed = true;

        base.Dispose(disposing);
    }

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
    /// <exception cref="ArgumentNullException"><paramref name="buffer"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="offset"/> or <paramref name="count"/> is negative.</para>
    /// <para>-or-</para>
    /// <para><paramref name="offset"/> subtracted from the buffer length is less than <paramref name="count"/>.</para>
    /// </exception>
    /// <exception cref="InvalidOperationException">One of the underlying segments is <see langword="null"/>.</exception>
    /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
    public override int Read(byte[] buffer, int offset, int count)
        => Read(new Span<byte>(buffer ?? throw new ArgumentNullException(nameof(buffer)), offset, count));

    /// <summary>
    /// Reads data from the stream and stores it to a span of bytes in memory.
    /// </summary>
    /// <param name="buffer">A region of memory to store data read from the stream.</param>
    /// <returns>
    /// The total number of bytes written into the buffer. This can be less than the number of bytes requested
    /// if that number of bytes are not currently available, or zero if the end of the stream is reached
    /// before any bytes are read.
    /// </returns>
    /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
    public
#if NETCOREAPP2_1_OR_GREATER
    override
#endif
    int Read(Span<byte> buffer)
    {
        EnsureOpen();

        int requested = buffer.Length, remaining = buffer.Length;
        while (remaining > 0)
        {
            // If we're at the end of the current segment, check if we can read the next one
            if (_segments.Current.Length - _currentIndex <= 0)
            {
                if (!_segments.MoveNext())
                    break;

                _currentIndex = 0;
            }

            // Slice the current span based on the current index
            ReadOnlySpan<byte> current = _segments.Current.Span;
            int length = Math.Min(remaining, current.Length - _currentIndex);
            current = current.Slice(_currentIndex, length);

            current.CopyTo(buffer);
            buffer = buffer[current.Length..];
            _currentIndex += current.Length;
            remaining -= current.Length;
        }

        return requested - remaining;
    }

    /// <summary>
    /// Asynchronously reads a block of bytes from the stream and writes the data into a given buffer after
    /// checking for cancellation requests.
    /// </summary>
    /// <param name="buffer">The buffer to write the data into.</param>
    /// <param name="offset">
    /// The byte offset in <paramref name="buffer"/> at which to begin writing data from the stream.
    /// </param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous read operation.
    /// The value of the <see cref="Task{TResult}.Result"/> parameter contains the total number of bytes read
    /// into <paramref name="buffer"/>. The result value can be less than the number of bytes requested if the number
    /// of bytes currently available is less than the requested number, or it can be <c>0</c> (zero)
    /// if the end of the stream has been reached.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// The sum of <paramref name="count"/> and <paramref name="count"/> is larger than the buffer length.
    /// </exception>
    /// <exception cref="ArgumentNullException"><paramref name="buffer"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="offset"/> or <paramref name="count"/> is negative.
    /// </exception>
    /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
    /// <exception cref="OperationCanceledException">
    /// The <paramref name="cancellationToken"/> requested cancellation.
    /// </exception>
    public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        => cancellationToken.IsCancellationRequested
            ? Task.FromCanceled<int>(cancellationToken)
            : Task.FromResult(Read(buffer, offset, count));

#if NETCOREAPP2_1_OR_GREATER
    /// <summary>
    /// Asynchronously reads a sequence of bytes from the current stream,
    /// advances the position within the stream by the number of bytes read,
    /// and monitors cancellation requests.
    /// </summary>
    /// <param name="buffer">The region of memory to write the data into.</param>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous read operation.
    /// The value of its <see cref="Task{TResult}.Result"/> property contains the total number of bytes read
    /// into the buffer. The result value can be less than the number of bytes allocated in the buffer if that many
    /// bytes are not currently available, or it can be <c>0</c> (zero) if the end of the stream has been reached.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// The <paramref name="cancellationToken"/> requested cancellation.
    /// </exception>
    public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
        => cancellationToken.IsCancellationRequested
            ? ValueTask.FromCanceled<int>(cancellationToken)
            : base.ReadAsync(buffer, cancellationToken);
#endif

    /// <summary>
    /// Reads a byte from the stream and advances the position within the stream by one byte,
    /// or returns <c>-1</c> if at the end of the stream.
    /// </summary>
    /// <returns>
    /// The unsigned byte cast to an <see cref="int"/>, or <c>-1</c> if at the end of the stream.
    /// </returns>
    /// <exception cref="InvalidOperationException">One of the underlying segments is <see langword="null"/>.</exception>
    /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
    public override int ReadByte()
    {
        unsafe
        {
            byte b;
            return Read(new Span<byte>(&b, 1)) == 0 ? -1 : b;
        }
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
        => throw new NotSupportedException(SR.StreamSeekNotSupportedMessage);

    /// <summary>
    /// Sets the length of the stream.
    /// This method is not currently supported and always throws a <see cref="NotSupportedException"/>.
    /// </summary>
    /// <param name="value">This parameter is not used.</param>
    /// <exception cref="NotSupportedException">Any use of this method.</exception>
    public override void SetLength(long value)
        => throw new NotSupportedException(SR.StreamSeekNotSupportedMessage);

    /// <summary>
    /// Writes data to the stream from a specified range of a byte array.
    /// This method is not currently supported and always throws a <see cref="NotSupportedException"/>.
    /// </summary>
    /// <param name="buffer">This parameter is not used.</param>
    /// <param name="offset">This parameter is not used.</param>
    /// <param name="count">This parameter is not used.</param>
    /// <exception cref="NotSupportedException">Any use of this method.</exception>
    public override void Write(byte[] buffer, int offset, int count)
        => throw new NotSupportedException(SR.StreamWriteNotSupportedMessage);

    private void EnsureOpen()
    {
        if (_isClosed)
            throw new ObjectDisposedException(null, SR.ClosedStreamMessage);
    }
}
