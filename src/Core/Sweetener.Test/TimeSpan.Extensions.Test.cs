// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Test;

[TestClass]
public class TimeSpanExtensionsTest
{
    [TestMethod]
    public void Truncate()
    {
        // Invalid Negative Input
        Assert.ThrowsException<ArgumentNegativeException>(() => TimeSpan.FromHours(7).Truncate(TimeSpan.FromMinutes(-3)));

        // TimeSpan.Zero graularity, a TimeSpan.Zero value, or trying to truncate a value that's already at
        // the desired granularity result in the identity function
        Assert.AreEqual(TimeSpan.MaxValue, TimeSpan.MaxValue.Truncate(TimeSpan.Zero));
        Assert.AreEqual(TimeSpan.Zero, TimeSpan.Zero.Truncate(TimeSpan.FromHours(2)));
        Assert.AreEqual(TimeSpan.FromDays(10), TimeSpan.FromDays(10).Truncate(TimeSpan.FromDays(5)));

        // Set to TimeSpan.Zero when truncation would result in a smaller value
        Assert.AreEqual(TimeSpan.Zero, TimeSpan.Zero.Add(TimeSpan.FromMinutes(15)).Truncate(TimeSpan.FromHours(1)));

        // Positive scenario
        TimeSpan positive = new TimeSpan(7, 6, 5, 4, 3);
        Assert.AreEqual(new TimeSpan(7, 6, 5, 0, 0), positive.Truncate(new TimeSpan(0, 2, 30)));

        // Negative scenario (value moves towards TimeSpan.Zero)
        TimeSpan negative = new TimeSpan(1, 2, 3, 4, 5).Negate();
        Assert.AreEqual(new TimeSpan(1, 2, 0, 0, 0).Negate(), negative.Truncate(TimeSpan.FromHours(1)));
    }
}
