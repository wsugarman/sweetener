// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Linq.Test
{
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
        }
    }
}
