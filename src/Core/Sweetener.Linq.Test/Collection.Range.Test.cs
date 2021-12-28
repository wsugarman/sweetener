// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweetener.Linq;

namespace Sweetener.Test.Linq;

partial class CollectionTest
{
    [TestMethod]
    public void Range()
    {
        // Invalid count
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => Collection.Range(1, -1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => Collection.Range(int.MaxValue, 5));

        IReadOnlyCollection<int> actual;

        // Empty
        actual = Collection.Range(1, 0);
        Assert.AreEqual(0, actual.Count);
        Assert.AreSame(Array.Empty<int>(), actual);

        // Count > 0
        actual = Collection.Range(1, 5);
        Assert.AreEqual(5, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, 1, 2, 3, 4, 5);
    }
}
