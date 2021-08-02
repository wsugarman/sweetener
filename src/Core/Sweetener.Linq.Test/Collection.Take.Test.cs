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
        public void Take()
        {
            // Invalid source
            Assert.ThrowsException<ArgumentNullException>(() => Collection.Take<long>(null!, 1));

            IReadOnlyCollection<long> actual;
            List<long> source = new List<long> { 1 };

            // Empty
            actual = source.Take(-3);
            Assert.AreEqual(0, actual.Count);
            CodeCoverageAssert.AreSequencesEqual(Array.Empty<long>(), actual);

            // Lazily evaluate the take and its resulting count
            actual = source.Take(2);
            Assert.AreEqual(1, actual.Count);
            CodeCoverageAssert.AreSequencesEqual(actual, 1);

            source.AddRange(new long[] { 2, 3, 4, 5 });
            Assert.AreEqual(2, actual.Count);
            CodeCoverageAssert.AreSequencesEqual(actual, 1, 2);
        }
    }
}
