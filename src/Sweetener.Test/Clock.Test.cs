// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Test
{
    [TestClass]
    public class ClockTest
    {
        [TestMethod]
        public void Now()
        {
            DateTime before = DateTime.Now;
            DateTime now    = Clock.System.Now;
            DateTime after  = DateTime.Now;

            Assert.AreEqual(DateTimeKind.Local, now.Kind);
            Assert.IsTrue(before <= now);
            Assert.IsTrue(now <= after);
        }

        [TestMethod]
        public void UtcNow()
        {
            DateTime before = DateTime.UtcNow;
            DateTime utcNow = Clock.System.UtcNow;
            DateTime after  = DateTime.UtcNow;

            Assert.AreEqual(DateTimeKind.Utc, utcNow.Kind);
            Assert.IsTrue(before <= utcNow);
            Assert.IsTrue(utcNow <= after);
        }
    }
}
