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
    public void Append()
    {
        // Null source
        Assert.ThrowsException<ArgumentNullException>(() => Collection.Append(null!, "foo"));

        // Lazily evaluate the append and its resulting count
        List<string> source = new List<string>();
        IReadOnlyCollection<string> actual = source.Append("world");
        Assert.AreEqual(1, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, "world");

        source.Add("hello");
        Assert.AreEqual(2, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, "hello", "world");

        // Append additional element
        actual = actual.Append("!");
        Assert.AreEqual(3, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, "hello", "world", "!");

        // Prepend element
        actual = actual.Prepend("why");
        Assert.AreEqual(4, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, "why", "hello", "world", "!");

        // Append to decorator
        IReadOnlyCollection<int> numbers = Collection.Range(1, 4).Append(5);
        Assert.AreEqual(5, numbers.Count);
        CodeCoverageAssert.AreSequencesEqual(numbers, 1, 2, 3, 4, 5);
    }

    [TestMethod]
    public void Prepend()
    {
        // Null source
        Assert.ThrowsException<ArgumentNullException>(() => Collection.Prepend(null!, "bar"));

        // Lazily evaluate the prepend and its resulting count
        List<string> source = new List<string>();
        IReadOnlyCollection<string> actual = source.Prepend("hello");
        Assert.AreEqual(1, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, "hello");

        source.Add("world");
        Assert.AreEqual(2, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, "hello", "world");

        // Prepend additional element
        actual = actual.Prepend("why");
        Assert.AreEqual(3, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, "why", "hello", "world");

        // Append element
        actual = actual.Append("!");
        Assert.AreEqual(4, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, "why", "hello", "world", "!");

        // Prepend to decorator
        IReadOnlyCollection<int> numbers = Collection.Range(2, 4).Prepend(1);
        Assert.AreEqual(5, numbers.Count);
        CodeCoverageAssert.AreSequencesEqual(numbers, 1, 2, 3, 4, 5);
    }
}
