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
    public void Concat_Collection()
    {
        // Null arguments
        Assert.ThrowsException<ArgumentNullException>(() => Collection.Concat(null!, Array.Empty<int>()));
        Assert.ThrowsException<ArgumentNullException>(() => Collection.Concat(Array.Empty<int>(), null!));

        // Lazily evaluate the concatenation and its resulting count
        List<int> first = new List<int> { 1, 2 };
        List<int> second = new List<int> { 3 };
        IReadOnlyCollection<int> actual = first.Concat(second);
        Assert.AreEqual(3, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, 1, 2, 3);

        first.Insert(0, 0);
        second.Add(4);
        second.Add(5);
        Assert.AreEqual(6, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, 0, 1, 2, 3, 4, 5);
    }
}
