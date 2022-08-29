// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweetener.IO;

namespace Sweetener.Test.IO;

[TestClass]
public sealed class SegmentedStreamTest : IDisposable
{
    private SegmentedStream _stream;

    public SegmentedStreamTest()
        => _stream = Init();

    [TestMethod]
    public void CanRead()
        => Assert.IsTrue(_stream.CanRead);

    [TestMethod]
    public void CanSeek()
        => Assert.IsFalse(_stream.CanSeek);

    [TestMethod]
    public void CanWrite()
        => Assert.IsFalse(_stream.CanWrite);

    [TestMethod]
    public void Length()
        => Assert.ThrowsException<NotSupportedException>(() => _stream.Length);

    [TestMethod]
    public void Position()
    {
        Assert.ThrowsException<NotSupportedException>(() => _stream.Position);
        Assert.ThrowsException<NotSupportedException>(() => _stream.Position = 0);
    }

    [TestMethod]
    public void Ctor()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new SegmentedStream((IEnumerable<byte[]?>)null!));
        Assert.ThrowsException<ArgumentNullException>(() => new SegmentedStream((IEnumerable<ReadOnlyMemory<byte>>)null!));
    }

    [TestMethod]
    public async Task DisposeTest()
    {
        await _stream.DisposeAsync().ConfigureAwait(false);

        Assert.IsFalse(_stream.CanRead);
        Assert.ThrowsException<ObjectDisposedException>(() => _stream.ReadByte());
        Assert.ThrowsException<ObjectDisposedException>(() => _stream.Read(Array.Empty<byte>(), 0, 0));
        Assert.ThrowsException<ObjectDisposedException>(() => _stream.Read(new Span<byte>()));
        await Assert.ThrowsExceptionAsync<ObjectDisposedException>(() => _stream.ReadAsync(Array.Empty<byte>(), 0, 0)).ConfigureAwait(false);
        await Assert.ThrowsExceptionAsync<ObjectDisposedException>(async () => await _stream.ReadAsync(new Memory<byte>()).ConfigureAwait(false)).ConfigureAwait(false);
    }

    [TestMethod]
    public void Flush()
        => _stream.Flush(); // No-op

    [TestMethod]
    public void Read_ByteArray()
    {
        Assert.ThrowsException<ArgumentNullException>(() => _stream.Read(null!, 0, 10));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _stream.Read(new byte[4], -1, 3));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _stream.Read(new byte[4], 1, -2));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _stream.Read(new byte[4], 1, 5));

        Read((s, b, o, c) => s.Read(b, o, c));
    }

    [TestMethod]
    public void Read_Span()
        => Read((s, b, o, c) => s.Read(new Span<byte>(b, o, c)));

    [TestMethod]
    public async Task ReadAsync_ByteArray()
    {
        using CancellationTokenSource source = new CancellationTokenSource();
        source.Cancel();

        await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _stream.ReadAsync(null!, 0, 10)).ConfigureAwait(false);
        await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => _stream.ReadAsync(new byte[4], -1, 3)).ConfigureAwait(false);
        await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => _stream.ReadAsync(new byte[4], 1, -2)).ConfigureAwait(false);
        await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => _stream.ReadAsync(new byte[4], 1, 5)).ConfigureAwait(false);
        await Assert.ThrowsExceptionAsync<TaskCanceledException>(() => _stream.ReadAsync(Array.Empty<byte>(), 0, 0, source.Token)).ConfigureAwait(false);

        await ReadAsync((s, b, o, c) => s.ReadAsync(b, o, c)).ConfigureAwait(false);
    }

    [TestMethod]
    public async Task ReadAsync_Memory()
    {
        using CancellationTokenSource source = new CancellationTokenSource();
        source.Cancel();

        // Test that the ValueTask itself throws the exception correctly instead of wrapping in Task<T> 
        await Assert.ThrowsExceptionAsync<TaskCanceledException>(async () => await _stream.ReadAsync(new Memory<byte>(), source.Token).ConfigureAwait(false)).ConfigureAwait(false);

        await ReadAsync(async (s, b, o, c) => await s.ReadAsync(new Memory<byte>(b, o, c)).ConfigureAwait(false)).ConfigureAwait(false);
    }

    [TestMethod]
    public void ReadByte()
    {
        for (int i = 0; i < 10; i++)
            Assert.AreEqual(i, _stream.ReadByte());

        Assert.AreEqual(-1, _stream.ReadByte());
    }

    [TestMethod]
    public void Seek()
        => Assert.ThrowsException<NotSupportedException>(() => _stream.Seek(0, SeekOrigin.Begin));

    [TestMethod]
    public void SetLength()
        => Assert.ThrowsException<NotSupportedException>(() => _stream.SetLength(1));

    [TestMethod]
    public void Write()
        => Assert.ThrowsException<NotSupportedException>(() => _stream.Write(new byte[1] { 1 }));

    public void Dispose()
        => _stream.Dispose();

    private void Read(Func<SegmentedStream, byte[], int, int, int> read)
    {
        byte[] buffer = new byte[10];

        // Read nothing
        Assert.AreEqual(0, read(_stream, Array.Empty<byte>(), 0, 0));
        AssertFilled(buffer, 0);

        // Ask to read less than max [0..j]
        Assert.AreEqual(2, read(_stream, buffer, 0, 2));
        AssertFilled(buffer, 2);

        // Ask to read less than max [i..j]
        Assert.AreEqual(1, read(_stream, buffer, 2, 1));
        AssertFilled(buffer, 3);

        // Ask to read less than max [0..]
        Assert.AreEqual(3, read(_stream, buffer, 3, 3));
        AssertFilled(buffer, 6);

        // Ask to read until the end (exactly)
        Assert.AreEqual(4, read(_stream, buffer, 6, 4));
        AssertFilled(buffer);

        Reset();
        buffer = new byte[20];

        // Ask to read beyond the end
        Assert.AreEqual(10, read(_stream, buffer, 0, 20));
        AssertFilled(buffer, 10);

        // Ask to read after reaching the end
        Assert.AreEqual(0, read(_stream, buffer, 0, 1));
        AssertFilled(buffer, 10);
    }

    private async Task ReadAsync(Func<SegmentedStream, byte[], int, int, Task<int>> readAsync)
    {
        byte[] buffer = new byte[10];

        // Read nothing
        Assert.AreEqual(0, await readAsync(_stream, Array.Empty<byte>(), 0, 0).ConfigureAwait(false));
        AssertFilled(buffer, 0);

        // Ask to read less than max [0..j]
        Assert.AreEqual(2, await readAsync(_stream, buffer, 0, 2).ConfigureAwait(false));
        AssertFilled(buffer, 2);

        // Ask to read less than max [i..j]
        Assert.AreEqual(1, await readAsync(_stream, buffer, 2, 1).ConfigureAwait(false));
        AssertFilled(buffer, 3);

        // Ask to read less than max [0..]
        Assert.AreEqual(3, await readAsync(_stream, buffer, 3, 3).ConfigureAwait(false));
        AssertFilled(buffer, 6);

        // Ask to read until the end (exactly)
        Assert.AreEqual(4, await readAsync(_stream, buffer, 6, 4).ConfigureAwait(false));
        AssertFilled(buffer);

        Reset();
        buffer = new byte[20];

        // Ask to read beyond the end
        Assert.AreEqual(10, await readAsync(_stream, buffer, 0, 20).ConfigureAwait(false));
        AssertFilled(buffer, 10);

        // Ask to read after reaching the end
        Assert.AreEqual(0, await readAsync(_stream, buffer, 0, 1).ConfigureAwait(false));
        AssertFilled(buffer, 10);
    }

    private void Reset()
    {
        _stream.Dispose();
        _stream = Init();
    }

    private static void AssertFilled(byte[] buffer, int? length = null)
    {
        int l = length ?? buffer.Length;
        for (int i = 0; i < l; i++)
            Assert.AreEqual(i, buffer[i]);

        for (int i = l; i < buffer.Length; i++)
            Assert.AreEqual(0, buffer[i]);
    }

    private static SegmentedStream Init()
        => new SegmentedStream(
            new byte[]?[]
            {
                new byte[] { 0, 1, 2 },
                new byte[] { 3 },
                new byte[] { 4, 5 },
                null,
                new byte[] { 6, 7, 8, 9 },
            });
}
