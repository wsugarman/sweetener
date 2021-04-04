// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Test
{
    [TestClass]
    public class MapTest
    {
        [TestMethod]
        public void Empty()
        {
            EmptyDictionary<int, TimeSpan> d1 = Map.Empty<int, TimeSpan>();
            EmptyDictionary<int, TimeSpan> d2 = Map.Empty<int, TimeSpan>();
            EmptyDictionary<int, DateTime> d3 = Map.Empty<int, DateTime>();

            Assert.AreSame(d1, d2);
            Assert.AreNotSame(d1, d3);

            // The behavior for this object is tested separately as the type is exposed
        }
    }
}
