// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweetener.Collections.Generic;

namespace Sweetener.Test.Collections.Generic;

[TestClass]
public class BinaryComparerTest
{
    [TestMethod]
    public void Compare_Array()
        => Compare(BinaryComparer.Instance.Compare);

    [TestMethod]
    public void Compare_ReadOnlySpan()
        => Compare(
            (x, y) => BinaryComparer.Instance.Compare(new ReadOnlySpan<byte>(x), new ReadOnlySpan<byte>(y)),
            skipNullCase: true);

    [TestMethod]
    public void Compare_ReadOnlySpan_x86()
        => Compare(
            (x, y) =>
            {
                unsafe
                {
                    fixed (byte* xPtr = x)
                    fixed (byte* yPtr = y)
                    {
                        return BinaryComparer.Compare((uint*)xPtr, (uint*)yPtr, x!.Length, y!.Length);
                    }
                }
            },
            skipNullCase: true);

    [TestMethod]
    public void Compare_ReadOnlySpan_x64()
        => Compare(
            (x, y) =>
            {
                unsafe
                {
                    fixed (byte* xPtr = x)
                    fixed (byte* yPtr = y)
                    {
                        return BinaryComparer.Compare((ulong*)xPtr, (ulong*)yPtr, x!.Length, y!.Length);
                    }
                }
            },
            skipNullCase: true);

    [TestMethod]
    public void Compare_Stream()
        => Compare(
            (x, y) =>
            {
                using Stream? xStream = x != null ? new MemoryStream(x) : null;
                using Stream? yStream = y != null ? new MemoryStream(y) : null;

                if (ReferenceEquals(x, y))
                    return BinaryComparer.Instance.Compare(xStream, xStream);
                else
                    return BinaryComparer.Instance.Compare(xStream, yStream);
            });

    [TestMethod]
    public void Compare_Stream_x32()
        => Compare(
            (x, y) =>
            {
                using Stream xStream = new MemoryStream(x!);
                using Stream yStream = new MemoryStream(y!);

                return BinaryComparer.Compare32(xStream, yStream);
            },
            skipNullCase: true);

    [TestMethod]
    public void Compare_Stream_x64()
        => Compare(
            (x, y) =>
            {
                using Stream xStream = new MemoryStream(x!);
                using Stream yStream = new MemoryStream(y!);

                return BinaryComparer.Compare64(xStream, yStream);
            },
            skipNullCase: true);

    [TestMethod]
    public void Equals_Array()
    {
        // Same reference
        byte[] a = new byte[] { 0, 0, 0, 0, 0 };
        Assert.IsTrue(BinaryComparer.Instance.Equals(a, a));

        // Other test cases
        Equals(BinaryComparer.Instance.Equals);
    }

    [TestMethod]
    public void Equals_ReadOnlySpan()
        => Equals(
            (x, y) => BinaryComparer.Instance.Equals(new ReadOnlySpan<byte>(x), new ReadOnlySpan<byte>(y)),
            skipNullCase: true);

    [TestMethod]
    public void Equals_ReadOnlySpan_x86()
        => Equals(
            (x, y) =>
            {
                unsafe
                {
                    fixed (byte* xPtr = x)
                    fixed (byte* yPtr = y)
                    {
                        return BinaryComparer.Equals((uint*)xPtr, (uint*)yPtr, x!.Length);
                    }
                }
            },
            skipNullCase: true,
            skipUnequalLength: true);

    [TestMethod]
    public void Equals_ReadOnlySpan_x64()
        => Equals(
            (x, y) =>
            {
                unsafe
                {
                    fixed (byte* xPtr = x)
                    fixed (byte* yPtr = y)
                    {
                        return BinaryComparer.Equals((ulong*)xPtr, (ulong*)yPtr, x!.Length);
                    }
                }
            },
            skipNullCase: true,
            skipUnequalLength: true);

    [TestMethod]
    public void Equals_Stream()
        => Equals(
            (x, y) =>
            {
                using Stream? xStream = x != null ? new MemoryStream(x) : null;
                using Stream? yStream = y != null ? new MemoryStream(y) : null;

                if (ReferenceEquals(x, y))
                    return BinaryComparer.Instance.Equals(xStream, xStream);
                else
                    return BinaryComparer.Instance.Equals(xStream, yStream);
            });

    [TestMethod]
    public void Equals_Stream_x32()
        => Equals(
            (x, y) =>
            {
                using Stream xStream = new MemoryStream(x!);
                using Stream yStream = new MemoryStream(y!);

                return BinaryComparer.Equals32(xStream, yStream);
            },
            skipNullCase: true);

    [TestMethod]
    public void Equals_Stream_x64()
        => Equals(
            (x, y) =>
            {
                using Stream xStream = new MemoryStream(x!);
                using Stream yStream = new MemoryStream(y!);

                return BinaryComparer.Equals64(xStream, yStream);
            },
            skipNullCase: true);

    [TestMethod]
    public void GetHashCode_Array()
        => Equals((x, y) => BinaryComparer.Instance.GetHashCode(x!) == BinaryComparer.Instance.GetHashCode(y!));

    [TestMethod]
    public void GetHashCode_ReadOnlySpan()
        => Equals(
            (x, y) =>
                BinaryComparer.Instance.GetHashCode(new ReadOnlySpan<byte>(x)) == BinaryComparer.Instance.GetHashCode(new ReadOnlySpan<byte>(y)),
            skipNullCase: true);

    [TestMethod]
    public void GetHashCode_Stream()
        => Equals((x, y) =>
        {
            using Stream? xStream = x != null ? new MemoryStream(x) : null;
            using Stream? yStream = y != null ? new MemoryStream(y) : null;

            return BinaryComparer.Instance.GetHashCode(xStream!) == BinaryComparer.Instance.GetHashCode(yStream!);
        });

    [SuppressMessage("Performance", "CA1825:Avoid zero-length array allocations", Justification = "Avoid reference equality.")]
    private static void Compare(Func<byte[]?, byte[]?, int> compare, bool skipNullCase = false)
    {
        byte[] aligned1 = new byte[]
        {
             0,  1,  2,  3,  4,  5,  6,  7,
             8,  9, 10, 11, 12, 13, 14, 15,
            16, 17, 18, 19, 20, 21, 22, 23,
        };
        byte[] extra1 = new byte[]
        {
             0,  1,  2,  3,  4,  5,  6,  7,
             8,  9, 10, 11, 12, 13, 14, 15,
            16, 17, 18, 19, 20, 21, 22, 23,
            24, 25, 26
        };
        byte[] aligned2 = new byte[]
        {
             0,  1,  2,  3,  4,  5,  6,  7,
             8,  9, 10, 11, 12, 13, 14, 15,
            24, 25, 26, 27, 28, 29, 30, 31
        };
        byte[] extra2 = new byte[]
        {
             0,  1,  2,  3,  4,  5,  6,  7,
             8,  9, 10, 11, 12, 13, 14, 15,
            16, 17, 18, 19, 20, 21, 22, 23,
            24, 35, 26
        };

        // Null + Empty
        if (!skipNullCase)
        {
            Assert.AreEqual(0, compare(null, null));

            Assert.IsTrue(compare(null, Array.Empty<byte>()) < 0);
            Assert.IsTrue(compare(Array.Empty<byte>(), null) > 0);
        }

        // Less Than
        Assert.IsTrue(compare(aligned1           , aligned2) < 0); // Same length
        Assert.IsTrue(compare(extra1             , extra2  ) < 0); // Same length (unaligned)
        Assert.IsTrue(compare(aligned1           , extra1  ) < 0); // Different length
        Assert.IsTrue(compare(Array.Empty<byte>(), aligned1) < 0); // Different length (empty)

        // Equal
        Assert.AreEqual(0, compare(aligned1   , aligned1                )); // Same reference
        Assert.AreEqual(0, compare(aligned1   , (byte[])aligned1.Clone())); // Aligned
        Assert.AreEqual(0, compare(extra1     , (byte[])extra1  .Clone())); // Unaligned
        Assert.AreEqual(0, compare(new byte[0], new byte[0]             )); // Empty

        // Greater Than
        Assert.IsTrue(compare(aligned2, aligned1           ) > 0); // Same length
        Assert.IsTrue(compare(extra2  , extra1             ) > 0); // Same length (difference not in word)
        Assert.IsTrue(compare(extra1  , aligned1           ) > 0); // Different length
        Assert.IsTrue(compare(aligned1, Array.Empty<byte>()) > 0); // Different length (empty)
    }

    [SuppressMessage("Performance", "CA1825:Avoid zero-length array allocations", Justification = "Avoid reference equality.")]
    private static void Equals(Func<byte[]?, byte[]?, bool> equals, bool skipNullCase = false, bool skipUnequalLength = false)
    {
        byte[] aligned1 = new byte[]
        {
             0,  1,  2,  3,  4,  5,  6,  7,
             8,  9, 10, 11, 12, 13, 14, 15,
            16, 17, 18, 19, 20, 21, 22, 23,
        };
        byte[] extra1 = new byte[]
        {
             0,  1,  2,  3,  4,  5,  6,  7,
             8,  9, 10, 11, 12, 13, 14, 15,
            16, 17, 18, 19, 20, 21, 22, 23,
            24, 25, 26
        };
        byte[] aligned2 = new byte[]
        {
             0,  1,  2,  3,  4,  5,  6,  7,
             8,  9, 10, 11, 12, 13, 14, 15,
            24, 25, 26, 27, 28, 29, 30, 31
        };
        byte[] extra2 = new byte[]
        {
             0,  1,  2,  3,  4,  5,  6,  7,
             8,  9, 10, 11, 12, 13, 14, 15,
            16, 17, 18, 19, 20, 21, 22, 23,
            24, 35, 26
        };

        // Null + Empty
        if (!skipNullCase)
        {
            Assert.IsTrue(equals(null, null));

            Assert.IsFalse(equals(null, Array.Empty<byte>()));
            Assert.IsFalse(equals(Array.Empty<byte>(), null));
        }

        // Equal
        Assert.IsTrue(equals(aligned1                , aligned1                ));
        Assert.IsTrue(equals(aligned1                , (byte[])aligned1.Clone()));
        Assert.IsTrue(equals((byte[])aligned1.Clone(), aligned1                ));

        Assert.IsTrue(equals(extra1                , (byte[])extra1.Clone()));
        Assert.IsTrue(equals((byte[])extra1.Clone(), extra1                ));

        Assert.IsTrue(equals(new byte[0], new byte[0]));

        // Not Equal
        if (!skipUnequalLength)
        {
            Assert.IsFalse(equals(aligned1           , Array.Empty<byte>()));
            Assert.IsFalse(equals(Array.Empty<byte>(), aligned1           ));

            Assert.IsFalse(equals(aligned1, extra1));
            Assert.IsFalse(equals(aligned2, extra1));
        }

        Assert.IsFalse(equals(aligned1, aligned2));
        Assert.IsFalse(equals(aligned2, aligned1));

        Assert.IsFalse(equals(extra1, extra2));
        Assert.IsFalse(equals(extra2, extra1));
    }

    private enum NullTesting
    {
        Skip,
        Include,
        SameAsEmpty,
    }
}
