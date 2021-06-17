// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Test
{
    [TestClass]
    public class FixedClockTest
    {
        [TestMethod]
        public void FromLocal()
        {
            FixedClock clock;

            // Unspecified
            DateTime unspecifiedNow = new DateTime(2021, 6, 16, 1, 2, 3, 4, DateTimeKind.Unspecified);
            clock = FixedClock.FromLocal(unspecifiedNow);
            Assert.AreEqual(unspecifiedNow                  , clock.Now);
            Assert.AreEqual(unspecifiedNow.ToUniversalTime(), clock.UtcNow);

            // Local
            DateTime now = DateTime.Now;
            clock = FixedClock.FromLocal(now);
            Assert.AreEqual(now                  , clock.Now);
            Assert.AreEqual(now.ToUniversalTime(), clock.UtcNow);

            // UTC
            DateTime utcNow = DateTime.UtcNow;
            clock = FixedClock.FromLocal(utcNow);
            Assert.AreEqual(utcNow.ToLocalTime(), clock.Now);
            Assert.AreEqual(utcNow              , clock.UtcNow);
        }

        [TestMethod]
        public void FromUtc()
        {
            FixedClock clock;

            // Unspecified
            DateTime unspecifiedNow = new DateTime(2021, 6, 16, 1, 2, 3, 4, DateTimeKind.Unspecified);
            clock = FixedClock.FromUtc(unspecifiedNow);
            Assert.AreEqual(unspecifiedNow.ToLocalTime(), clock.Now);
            Assert.AreEqual(unspecifiedNow              , clock.UtcNow);

            // Local
            DateTime now = DateTime.Now;
            clock = FixedClock.FromUtc(now);
            Assert.AreEqual(now                  , clock.Now);
            Assert.AreEqual(now.ToUniversalTime(), clock.UtcNow);

            // UTC
            DateTime utcNow = DateTime.UtcNow;
            clock = FixedClock.FromUtc(utcNow);
            Assert.AreEqual(utcNow.ToLocalTime(), clock.Now);
            Assert.AreEqual(utcNow              , clock.UtcNow);
        }
    }
}
