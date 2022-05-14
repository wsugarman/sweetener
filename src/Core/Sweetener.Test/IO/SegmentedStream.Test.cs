// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public void Flush()
        => _stream.Flush(); // No-op

    [TestMethod]
    public void Read_ByteArray()
    {
        Assert.ThrowsException<ArgumentNullException>(() => _stream.Read(null!, 0, 10));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _stream.Read(new byte[4], -1,  3));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _stream.Read(new byte[4],  1, -2));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _stream.Read(new byte[4], 1, 5));

        byte[] buffer = new byte[10];

        // Ask to read less than max
        Assert.AreEqual(3, _stream.Read(buffer, 0, 3));
        AssertFilled(buffer, 3);

        // Ask to read until the end
        Assert.AreEqual(7, _stream.Read(buffer, 3, 7));
        AssertFilled(buffer);

        Reset();
        buffer = new byte[20];

        // Ask to read beyond the end
        Assert.AreEqual(10, _stream.Read(buffer, 0, 20));
        AssertFilled(buffer, 10);

        // Ask to read after reaching the end
        Assert.AreEqual(0, _stream.Read(buffer, 0, 1));
        AssertFilled(buffer, 10);
    }

    public void Dispose()
        => _stream.Dispose();

    private void Reset(bool init = false)
    {
        if (!init)
            _stream.Dispose();

        _stream = new SegmentedStream(
            new byte[]?[]
            {
                new byte[] { 0, 1, 2 },
                new byte[] { 3 },
                new byte[] { 4, 5 },
                null,
                new byte[] { 6, 7, 8, 9 },
            });
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
