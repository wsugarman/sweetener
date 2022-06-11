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
    public void Skip()
    {
        // Invalid source
        Assert.ThrowsException<ArgumentNullException>(() => Collection.Skip<long>(null!, 1));

        IReadOnlyCollection<long> actual;
        List<long> source = new List<long> { 1 };

        // Empty
        actual = source.Skip(-5);
        Assert.AreEqual(1, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, 1);

        // Lazily evaluate the skip and its resulting count
        actual = source.Skip(2);
        Assert.AreEqual(0, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(Array.Empty<long>(), actual);

        source.AddRange(new long[] { 2, 3, 4, 5 });
        Assert.AreEqual(3, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, 3, 4, 5);
    }

#if NET6_0_OR_GREATER
    [TestMethod]
    public void SkipLast()
    {
        // Invalid source
        Assert.ThrowsException<ArgumentNullException>(() => Collection.SkipLast<long>(null!, 1));

        IReadOnlyCollection<long> actual;
        List<long> source = new List<long> { 1 };

        // Empty
        actual = source.SkipLast(-5);
        Assert.AreEqual(1, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, 1);

        // Lazily evaluate the skip and its resulting count
        actual = source.SkipLast(2);
        Assert.AreEqual(0, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(Array.Empty<long>(), actual);

        source.AddRange(new long[] { 2, 3, 4, 5 });
        Assert.AreEqual(3, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, 1, 2, 3);
    }
#endif
}
