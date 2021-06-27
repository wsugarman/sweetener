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
        public void System()
        {
            // Now
            DateTime before = DateTime.Now;
            DateTime now    = Clock.System.Now;
            DateTime after  = DateTime.Now;

            Assert.AreEqual(DateTimeKind.Local, now.Kind);
            Assert.IsTrue(before <= now);
            Assert.IsTrue(now <= after);

            // UtcNow
            DateTime utcBefore = DateTime.UtcNow;
            DateTime utcNow    = Clock.System.UtcNow;
            DateTime utcAfter  = DateTime.UtcNow;

            Assert.AreEqual(DateTimeKind.Utc, utcNow.Kind);
            Assert.IsTrue(utcBefore <= utcNow);
            Assert.IsTrue(utcNow <= utcAfter);
        }

        [TestMethod]
        public void FromLocal()
        {
            IClock clock;

            // Unspecified
            DateTime unspecifiedNow = new DateTime(2021, 6, 16, 1, 2, 3, 4, DateTimeKind.Unspecified);
            clock = Clock.FromLocal(unspecifiedNow);
            Assert.AreEqual(unspecifiedNow                  , clock.Now);
            Assert.AreEqual(unspecifiedNow.ToUniversalTime(), clock.UtcNow);

            // Local
            DateTime now = DateTime.Now;
            clock = Clock.FromLocal(now);
            Assert.AreEqual(now                  , clock.Now);
            Assert.AreEqual(now.ToUniversalTime(), clock.UtcNow);

            // UTC
            DateTime utcNow = DateTime.UtcNow;
            clock = Clock.FromLocal(utcNow);
            Assert.AreEqual(utcNow.ToLocalTime(), clock.Now);
            Assert.AreEqual(utcNow              , clock.UtcNow);
        }

        [TestMethod]
        public void FromUtc()
        {
            IClock clock;

            // Unspecified
            DateTime unspecifiedNow = new DateTime(2021, 6, 16, 1, 2, 3, 4, DateTimeKind.Unspecified);
            clock = Clock.FromUtc(unspecifiedNow);
            Assert.AreEqual(unspecifiedNow.ToLocalTime(), clock.Now);
            Assert.AreEqual(unspecifiedNow              , clock.UtcNow);

            // Local
            DateTime now = DateTime.Now;
            clock = Clock.FromUtc(now);
            Assert.AreEqual(now                  , clock.Now);
            Assert.AreEqual(now.ToUniversalTime(), clock.UtcNow);

            // UTC
            DateTime utcNow = DateTime.UtcNow;
            clock = Clock.FromUtc(utcNow);
            Assert.AreEqual(utcNow.ToLocalTime(), clock.Now);
            Assert.AreEqual(utcNow              , clock.UtcNow);
        }
    }
}
