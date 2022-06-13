// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweetener.Linq;

namespace Sweetener.Test.Linq;

partial class CollectionTest
{
    [TestMethod]
    public void Select()
    {
        // Null arguments
        Assert.ThrowsException<ArgumentNullException>(() => Collection.Select<int, int>(null!, x => x));
        Assert.ThrowsException<ArgumentNullException>(() => Collection.Select(Array.Empty<int>(), (Func<int, int>)null!));

        // Lazily evaluate the projection and its resulting count
        List<int> source = new List<int> { 1, 2, 3 };
        IReadOnlyCollection<int> actual = source.Select(x => x * 2);
        Assert.AreEqual(3, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, 2, 4, 6);

        source.Add(4);
        source.Add(5);
        Assert.AreEqual(5, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, 2, 4, 6, 8, 10);

        // Multiple projections
        IReadOnlyCollection<double> composite = actual.Select(x => TimeSpan.FromSeconds(x).TotalMilliseconds);
        Assert.AreEqual(5, composite.Count);
        CodeCoverageAssert.AreSequencesEqual(composite, 2000d, 4000d, 6000d, 8000d, 10000d);

        // Select on a decorator
        IReadOnlyCollection<string> numbers = Collection.Range(1, 5).Select(x => x.ToString(CultureInfo.InvariantCulture));
        Assert.AreEqual(5, numbers.Count);
        CodeCoverageAssert.AreSequencesEqual(numbers, "1", "2", "3", "4", "5");
    }

    [TestMethod]
    public void Select_Index()
    {
        // Null arguments
        Assert.ThrowsException<ArgumentNullException>(() => Collection.Select<int, int>(null!, x => x));
        Assert.ThrowsException<ArgumentNullException>(() => Collection.Select(Array.Empty<int>(), (Func<int, int, int>)null!));

        // Lazily evaluate the projection and its resulting count
        List<int> source = new List<int> { 1, 2, 3 };
        IReadOnlyCollection<int> actual = source.Select((x, i) => (x * 2) + i);
        Assert.AreEqual(3, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, 2, 5, 8);

        source.Add(4);
        source.Add(5);
        Assert.AreEqual(5, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, 2, 5, 8, 11, 14);

        // Multiple projections
        IReadOnlyCollection<string> composite = actual.Select((x, i) => string.Join("", Collection.Repeat(x.ToString(CultureInfo.InvariantCulture), i)));
        Assert.AreEqual(5, composite.Count);
        CodeCoverageAssert.AreSequencesEqual(composite, "", "5", "88", "111111", "14141414");

        // Select on a decorator
        IReadOnlyCollection<string> numbers = Collection.Range(1, 5).Select((x, i) => (x + i).ToString(CultureInfo.InvariantCulture));
        Assert.AreEqual(5, numbers.Count);
        CodeCoverageAssert.AreSequencesEqual(numbers, "1", "3", "5", "7", "9");
    }
}
