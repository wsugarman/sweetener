// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Test;

[TestClass]
public class ClockTest
{
    [TestMethod]
    public void System()
    {
        // Now
        DateTimeOffset before = DateTimeOffset.Now;
        DateTimeOffset now = Clock.System.Now;
        DateTimeOffset after = DateTimeOffset.Now;

        Assert.IsTrue(before <= now);
        Assert.IsTrue(now <= after);

        // UtcNow
        DateTimeOffset utcBefore = DateTimeOffset.UtcNow;
        DateTimeOffset utcNow = Clock.System.UtcNow;
        DateTimeOffset utcAfter = DateTimeOffset.UtcNow;

        Assert.AreEqual(TimeSpan.Zero, utcNow.Offset);
        Assert.IsTrue(utcBefore <= utcNow);
        Assert.IsTrue(utcNow <= utcAfter);
    }

    [TestMethod]
    public void From()
    {
        IClock clock;

        // Offset from UTC
        DateTimeOffset offsetValue = new DateTimeOffset(2021, 6, 16, 1, 2, 3, 4, TimeSpan.FromHours(-8));
        clock = Clock.From(offsetValue);
        Assert.AreEqual(offsetValue.ToLocalTime(), clock.Now);
        Assert.AreEqual(offsetValue.ToUniversalTime(), clock.UtcNow);

        // UTC
        DateTimeOffset utcValue = new DateTimeOffset(2021, 6, 16, 1, 2, 3, 4, TimeSpan.Zero);
        clock = Clock.From(utcValue);
        Assert.AreEqual(utcValue.ToLocalTime(), clock.Now);
        Assert.AreEqual(utcValue, clock.UtcNow);
    }
}
