// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.IO.Test
{
    [TestClass]
    public class SegmentedStreamTest
    {
        [TestMethod]
        public void Read()
        {
            byte[][] input = new byte[][]
            {
                new byte[] { 0, 1, 2 },
                new byte[] { 3 },
                new byte[] { 4, 5 },
                new byte[] { 6, 7, 8, 9 },
            };

            using SegmentedStream segmentedStream = new SegmentedStream(input);

            byte[] buffer = new byte[10];
            Assert.AreEqual(10, segmentedStream.Read(buffer, 0, 10));
            Assert.IsTrue(buffer.Select((x, i) => (Value: x, Index: i)).All(x => x.Value == x.Index));
        }
    }
}
