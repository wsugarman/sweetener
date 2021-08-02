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
        public void Skip()
        {
            // Invalid source
            Assert.ThrowsException<ArgumentNullException>(() => Collection.Skip<long>(null!, 1));

            // Lazily evaluate the reversal and its resulting count
            List<long> source = new List<long> { 1 };
            IReadOnlyCollection<long> actual = source.Skip(2);
            Assert.AreEqual(0, actual.Count);
            CodeCoverageAssert.AreSequencesEqual(Array.Empty<long>(), actual);

            source.AddRange(new long[] { 2, 3, 4, 5 });
            Assert.AreEqual(3, actual.Count);
            CodeCoverageAssert.AreSequencesEqual(actual, 3, 4, 5);
        }
    }
}
