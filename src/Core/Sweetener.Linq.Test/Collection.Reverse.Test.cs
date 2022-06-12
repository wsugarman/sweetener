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
    public void Reverse()
    {
        // Invalid source
        Assert.ThrowsException<ArgumentNullException>(() => Collection.Reverse<int>(null!));

        // Lazily evaluate the reversal and its resulting count
        List<int> source = new List<int> { -1, 0, 1 };
        IReadOnlyCollection<int> actual = ((IReadOnlyCollection<int>)source).Reverse();
        Assert.AreEqual(3, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, 1, 0, -1);

        source.Insert(0, -2);
        source.Add(2);
        Assert.AreEqual(5, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, 2, 1, 0, -1, -2);

        // Reverse a decorator
        IReadOnlyCollection<int> numbers = Collection.Range(1, 5).Reverse();
        Assert.AreEqual(5, numbers.Count);
        CodeCoverageAssert.AreSequencesEqual(numbers, 5, 4, 3, 2, 1);
    }
}
