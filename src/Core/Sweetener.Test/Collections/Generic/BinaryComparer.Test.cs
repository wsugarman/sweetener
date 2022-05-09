// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweetener.Collections.Generic;

namespace Sweetener.Test.Collections.Generic;

[TestClass]
public class BinaryComparerTest
{
    [TestMethod]
    public void Compare_Array()
    {
        // Same reference
        byte[] a = new byte[] { 0, 0, 0, 0, 0 };
        Assert.AreEqual(0, BinaryComparer.Instance.Compare(a, a));

        // Other test cases
        Compare(BinaryComparer.Instance.Compare, treatNullEmpty: false);
    }

    [TestMethod]
    public void Compare_ReadOnlySpan()
        => Compare((x, y) => BinaryComparer.Compare(x, y), treatNullEmpty: true);

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
                        return BinaryComparer.Compare((uint*)xPtr, (uint*)yPtr, x.Length, y.Length);
                    }
                }
            });

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
                        return BinaryComparer.Compare((ulong*)xPtr, (ulong*)yPtr, x.Length, y.Length);
                    }
                }
            });

    [TestMethod]
    public void Equals_Array()
    {
        // Same reference
        byte[] a = new byte[] { 0, 0, 0, 0, 0 };
        Assert.IsTrue(BinaryComparer.Instance.Equals(a, a));

        // Other test cases
        Equals(BinaryComparer.Instance.Equals, treatNullEmpty: false);
    }

    [TestMethod]
    public void Equals_ReadOnlySpan()
        => Equals((x, y) => BinaryComparer.Equals(x, y), treatNullEmpty: true);

    [TestMethod]
    public void Equals_ReadOnlySpan_x86()
        => EqualsWordWise(
            (x, y) =>
            {
                unsafe
                {
                    fixed (byte* xPtr = x)
                    fixed (byte* yPtr = y)
                    {
                        return BinaryComparer.Equals((uint*)xPtr, (uint*)yPtr, x.Length);
                    }
                }
            });

    [TestMethod]
    public void Equals_ReadOnlySpan_x64()
        => EqualsWordWise(
            (x, y) =>
            {
                unsafe
                {
                    fixed (byte* xPtr = x)
                    fixed (byte* yPtr = y)
                    {
                        return BinaryComparer.Equals((ulong*)xPtr, (ulong*)yPtr, x.Length);
                    }
                }
            });

    [TestMethod]
    public void GetHashCode_Array()
        => Equals((x, y) => BinaryComparer.Instance.GetHashCode(x) == BinaryComparer.Instance.GetHashCode(y), treatNullEmpty: false);

    [TestMethod]
    public void GetHashCode_ReadOnlySpan()
        => Equals((x, y) => BinaryComparer.GetHashCode(x) == BinaryComparer.GetHashCode(y), treatNullEmpty: true);

    private static void Compare(Func<byte[]?, byte[]?, int> compare, bool treatNullEmpty)
    {
        // Null + Empty
        Assert.AreEqual(0, compare(null, null));

        if (treatNullEmpty)
        {
            Assert.AreEqual(0, compare(null, Array.Empty<byte>()));
            Assert.AreEqual(0, compare(Array.Empty<byte>(), null));
        }
        else
        {
            Assert.IsTrue(compare(null, Array.Empty<byte>()) < 0);
            Assert.IsTrue(compare(Array.Empty<byte>(), null) > 0);
        }

        Compare(compare);
    }

    private static void Compare(Func<byte[], byte[], int> compare)
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

        // Less Than
        Assert.IsTrue(compare(aligned1, aligned2) < 0); // Same length
        Assert.IsTrue(compare(extra1  , extra2  ) < 0); // Same length (unaligned)
        Assert.IsTrue(compare(aligned1, extra1  ) < 0); // Different length

        // Equal
        Assert.AreEqual(0, compare(aligned1, (byte[])aligned1.Clone())); // Aligned
        Assert.AreEqual(0, compare(extra1  , (byte[])extra1  .Clone())); // Unaligned

        // Greater Than
        Assert.IsTrue(compare(aligned2, aligned1) > 0); // Same length
        Assert.IsTrue(compare(extra2  , extra1  ) > 0); // Same length (difference not in word)
        Assert.IsTrue(compare(extra1  , aligned1) > 0); // Different length
    }

    private static void Equals(Func<byte[]?, byte[]?, bool> equals, bool treatNullEmpty)
    {
        // Null + Empty
        Assert.IsTrue(equals(null, null));

        Assert.AreEqual(treatNullEmpty, equals(null, Array.Empty<byte>()));
        Assert.AreEqual(treatNullEmpty, equals(Array.Empty<byte>(), null));

        // Different lengths
        Assert.IsFalse(equals(new byte[] { 1, 2, 3 }, new byte[] { 4, 5 }   ));
        Assert.IsFalse(equals(new byte[] { 4, 5 }   , new byte[] { 1, 2, 3 }));

        EqualsWordWise(equals);
    }

    private static void EqualsWordWise(Func<byte[], byte[], bool> equals)
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

        // Equal
        Assert.IsTrue(equals(aligned1                , (byte[])aligned1.Clone()));
        Assert.IsTrue(equals((byte[])aligned1.Clone(), aligned1                ));

        Assert.IsTrue(equals(extra1                , (byte[])extra1.Clone()));
        Assert.IsTrue(equals((byte[])extra1.Clone(), extra1                ));

        // Not Equal
        Assert.IsFalse(equals(aligned1, aligned2));
        Assert.IsFalse(equals(aligned2, aligned1));

        Assert.IsFalse(equals(extra1, extra2));
        Assert.IsFalse(equals(extra2, extra1));
    }
}
