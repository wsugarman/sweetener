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
        public void Repeat()
        {
            // Invalid count
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Collection.Repeat(1, -2));

            IReadOnlyCollection<double> actual;

            // Empty
            actual = Collection.Repeat(1.23d, 0);
            Assert.AreEqual(0, actual.Count);
            Assert.AreSame(Array.Empty<double>(), actual);

            // Count > 0
            actual = Collection.Repeat(3.14d, 3);
            Assert.AreEqual(3, actual.Count);
            CodeCoverageAssert.AreSequencesEqual(actual, 3.14d, 3.14d, 3.14d);
        }
    }
}
