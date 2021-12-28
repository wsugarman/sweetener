// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweetener.Linq;

namespace Sweetener.Test.Linq;

partial class CollectionTest
{
    [TestMethod]
    public void OrderBy()
    {
        // Null arguments
        Assert.ThrowsException<ArgumentNullException>(() => Collection.OrderBy<string, string>(null!, x => x));
        Assert.ThrowsException<ArgumentNullException>(() => Collection.OrderBy<string, string>(Array.Empty<string>(), null!));

        // Lazily evaluate the ordering and its resulting count
        List<string> source = new List<string> { "xof", "nworb", "kciuq" };
        IOrderedReadOnlyCollection<string> actual = source.OrderBy(x => x[^1]);
        Assert.AreEqual(3, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, "nworb", "xof", "kciuq");

        source.Add("depmuj");
        Assert.AreEqual(4, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, "nworb", "xof", "depmuj", "kciuq");
    }

    [TestMethod]
    public void OrderBy_Comparer()
    {
        // Null arguments
        Assert.ThrowsException<ArgumentNullException>(() => Collection.OrderBy<string, string>(null!, x => x, Comparer<string>.Default));
        Assert.ThrowsException<ArgumentNullException>(() => Collection.OrderBy(Array.Empty<string>(), null!, Comparer<string>.Default));

        // Lazily evaluate the ordering and its resulting count
        IOrderedReadOnlyCollection<string> actual;

        List<string> source = new List<string> { "apple", "tuna", "spinach" };
        actual = source.OrderBy(x => x.Length, Comparer<int>.Create((x, y) => Comparer<int>.Default.Compare(y, x))); // Reverse
        Assert.AreEqual(3, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, "spinach", "apple", "tuna");

        source.Add("pistachio");
        Assert.AreEqual(4, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, "pistachio", "spinach", "apple", "tuna");

        // Null comparer uses default
        actual = source.OrderBy(x => x.Length, null);
        CodeCoverageAssert.AreSequencesEqual(actual, "tuna", "apple", "spinach", "pistachio");
    }

    [TestMethod]
    public void ThenBy()
    {
        // Null arguments
        Assert.ThrowsException<ArgumentNullException>(() => Collection.ThenBy<string, string>(null!, x => x));
        Assert.ThrowsException<ArgumentNullException>(() => Collection.ThenBy<string, string>(Array.Empty<string>().OrderBy(x => x), null!));

        // Lazily evaluate the ordering and its resulting count
        List<string> source = new List<string> { "banana", "apple", "alpaca" };
        IOrderedReadOnlyCollection<string> orderedSource = source.OrderBy(x => x[0]);
        IOrderedReadOnlyCollection<string> actual = orderedSource.ThenBy(x => x.Length);
        Assert.AreEqual(3, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, "apple", "alpaca", "banana");

        source.Add("apricot");
        Assert.AreEqual(4, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, "apple", "alpaca", "apricot", "banana");
    }

    [TestMethod]
    public void ThenBy_Comparer()
    {
        // Null arguments
        Assert.ThrowsException<ArgumentNullException>(() => Collection.ThenBy<string, string>(null!, x => x, Comparer<string>.Default));
        Assert.ThrowsException<ArgumentNullException>(() => Collection.ThenBy(Array.Empty<string>().OrderBy(x => x), null!, Comparer<string>.Default));

        // Lazily evaluate the ordering and its resulting count
        List<string> source = new List<string> { "banana", "apple", "alpaca" };
        IOrderedReadOnlyCollection<string> orderedSource = source.OrderBy(x => x[0]);
        IOrderedReadOnlyCollection<string> actual = orderedSource.ThenBy(x => x.Length, Comparer<int>.Create((x, y) => Comparer<int>.Default.Compare(y, x)));
        Assert.AreEqual(3, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, "alpaca", "apple", "banana");

        source.Add("apricot");
        Assert.AreEqual(4, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, "apricot", "alpaca", "apple", "banana");

        // Null comparer uses default
        actual = orderedSource.ThenBy(x => x.Length, null);
        CodeCoverageAssert.AreSequencesEqual(actual, "apple", "alpaca", "apricot", "banana");
    }

    [TestMethod]
    public void OrderByDescending()
    {
        // Null arguments
        Assert.ThrowsException<ArgumentNullException>(() => Collection.OrderByDescending<string, string>(null!, x => x));
        Assert.ThrowsException<ArgumentNullException>(() => Collection.OrderByDescending<string, string>(Array.Empty<string>(), null!));

        // Lazily evaluate the ordering and its resulting count
        List<string> source = new List<string> { "xof", "nworb", "kciuq" };
        IOrderedReadOnlyCollection<string> actual = source.OrderByDescending(x => x[^1]);
        Assert.AreEqual(3, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, "kciuq", "xof", "nworb");

        source.Add("depmuj");
        Assert.AreEqual(4, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, "kciuq", "depmuj", "xof", "nworb");
    }

    [TestMethod]
    public void OrderByDescending_Comparer()
    {
        // Null arguments
        Assert.ThrowsException<ArgumentNullException>(() => Collection.OrderByDescending<string, string>(null!, x => x, Comparer<string>.Default));
        Assert.ThrowsException<ArgumentNullException>(() => Collection.OrderByDescending(Array.Empty<string>(), null!, Comparer<string>.Default));

        // Lazily evaluate the ordering and its resulting count
        IOrderedReadOnlyCollection<string> actual;

        List<string> source = new List<string> { "apple", "tuna", "spinach" };
        actual = source.OrderByDescending(x => x.Length, Comparer<int>.Create((x, y) => Comparer<int>.Default.Compare(y, x))); // Reverse
        Assert.AreEqual(3, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, "tuna", "apple", "spinach");

        source.Add("pistachio");
        Assert.AreEqual(4, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, "tuna", "apple", "spinach", "pistachio");

        // Null comparer uses default
        actual = source.OrderByDescending(x => x.Length, null);
        CodeCoverageAssert.AreSequencesEqual(actual, "pistachio", "spinach", "apple", "tuna");
    }

    [TestMethod]
    public void ThenByDescending()
    {
        // Null arguments
        Assert.ThrowsException<ArgumentNullException>(() => Collection.ThenByDescending<string, string>(null!, x => x));
        Assert.ThrowsException<ArgumentNullException>(() => Collection.ThenByDescending<string, string>(Array.Empty<string>().OrderBy(x => x), null!));

        // Lazily evaluate the ordering and its resulting count
        List<string> source = new List<string> { "banana", "apple", "alpaca" };
        IOrderedReadOnlyCollection<string> orderedSource = source.OrderBy(x => x[0]);
        IOrderedReadOnlyCollection<string> actual = orderedSource.ThenByDescending(x => x.Length);
        Assert.AreEqual(3, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, "alpaca", "apple", "banana");

        source.Add("apricot");
        Assert.AreEqual(4, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, "apricot", "alpaca", "apple", "banana");
    }

    [TestMethod]
    public void ThenByDescending_Comparer()
    {
        // Null arguments
        Assert.ThrowsException<ArgumentNullException>(() => Collection.ThenByDescending<string, string>(null!, x => x, Comparer<string>.Default));
        Assert.ThrowsException<ArgumentNullException>(() => Collection.ThenByDescending(Array.Empty<string>().OrderBy(x => x), null!, Comparer<string>.Default));

        // Lazily evaluate the ordering and its resulting count
        List<string> source = new List<string> { "banana", "apple", "alpaca" };
        IOrderedReadOnlyCollection<string> orderedSource = source.OrderBy(x => x[0]);
        IOrderedReadOnlyCollection<string> actual = orderedSource.ThenByDescending(x => x.Length, Comparer<int>.Create((x, y) => Comparer<int>.Default.Compare(y, x)));
        Assert.AreEqual(3, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, "apple", "alpaca", "banana");

        source.Add("apricot");
        Assert.AreEqual(4, actual.Count);
        CodeCoverageAssert.AreSequencesEqual(actual, "apple", "alpaca", "apricot", "banana");

        // Null comparer uses default
        actual = orderedSource.ThenByDescending(x => x.Length, null);
        CodeCoverageAssert.AreSequencesEqual(actual, "apricot", "alpaca", "apple", "banana");
    }

    [TestMethod]
    public void CreateOrderedEnumerable()
    {
        IOrderedEnumerable<(int, char)> actual;
        IOrderedReadOnlyCollection<(int, char)> source = new List<(int, char)> { (2, 'b'), (1, 'a'), (3, 'b') }.OrderBy(x => x.Item2);

        // No Comparer; Ascending
        actual = source.CreateOrderedEnumerable(x => x.Item1, null, descending: false);
        Assert.That.AreSequencesEqual(actual, (1, 'a'), (2, 'b'), (3, 'b'));

        // No Comparer; Descending
        actual = source.CreateOrderedEnumerable(x => x.Item1, null, descending: true);
        Assert.That.AreSequencesEqual(actual, (1, 'a'), (3, 'b'), (2, 'b'));

        // With Comparer; Ascending
        actual = source.CreateOrderedEnumerable(x => x.Item1, Comparer<int>.Create((x, y) => Comparer<int>.Default.Compare(y, x)), descending: false);
        Assert.That.AreSequencesEqual(actual, (1, 'a'), (3, 'b'), (2, 'b'));

        // With Comparer; Descending
        actual = source.CreateOrderedEnumerable(x => x.Item1, Comparer<int>.Create((x, y) => Comparer<int>.Default.Compare(y, x)), descending: true);
        Assert.That.AreSequencesEqual(actual, (1, 'a'), (2, 'b'), (3, 'b'));
    }
}
