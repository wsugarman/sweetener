// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweetener.Collections.Generic;

namespace Sweetener.Test.Collections.Generic;

[TestClass]
public class BinaryEqualityComparerTest
{
    [TestMethod]
    [TestCategory("PlatformSpecific")]
    public void Equals_Array()
    {
        // Same reference
        byte[] a = new byte[] { 0, 0, 0, 0, 0 };
        Assert.IsTrue(BinaryEqualityComparer.Instance.Equals(a, a));

        // Other test cases
        Equals(BinaryEqualityComparer.Instance.Equals, treatNullEmpty: false);
    }

    [TestMethod]
    [TestCategory("PlatformSpecific")]
    public void Equals_ReadOnlySpan()
        => Equals((x, y) => BinaryEqualityComparer.Equals(x, y), treatNullEmpty: true);

    [TestMethod]
    public void GetHashCode_Array()
        => Equals((x, y) => BinaryEqualityComparer.Instance.GetHashCode(x) == BinaryEqualityComparer.Instance.GetHashCode(y), treatNullEmpty: false);

    [TestMethod]
    public void GetHashCode_ReadOnlySpan()
        => Equals((x, y) => BinaryEqualityComparer.GetHashCode(x) == BinaryEqualityComparer.GetHashCode(y), treatNullEmpty: true);

    private static void Equals(Func<byte[], byte[], bool> equals, bool treatNullEmpty)
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
        Assert.IsTrue(equals(null!, null!));

        Assert.AreEqual(treatNullEmpty, equals(null!, Array.Empty<byte>()));
        Assert.AreEqual(treatNullEmpty, equals(Array.Empty<byte>(), null!));

        // Different lengths
        Assert.IsFalse(equals(aligned1, extra1  ));
        Assert.IsFalse(equals(extra1  , aligned1));

        // Same length (aligned to word length)
        Assert.IsTrue(equals(aligned1                , (byte[])aligned1.Clone()));
        Assert.IsTrue(equals((byte[])aligned1.Clone(), aligned1                ));

        Assert.IsFalse(equals(aligned1, aligned2));
        Assert.IsFalse(equals(aligned2, aligned1));

        // Same length (are not aligned)
        Assert.IsTrue(equals(extra1                , (byte[])extra1.Clone()));
        Assert.IsTrue(equals((byte[])extra1.Clone(), extra1                ));

        Assert.IsFalse(equals(extra1, extra2));
        Assert.IsFalse(equals(extra2, extra1));
    }
}
