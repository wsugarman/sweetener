// Copyright © 2021 William Sugarman. All Rights Reserved.
// Licensed under the MIT License.

using System;
using Sweetener.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Test
{
    [TestClass]
    public class DateTimeExtensionsTest
    {
        [TestMethod]
        public void Truncate()
        {
            // Invalid Negative Input
            Assert.ThrowsException<ArgumentNegativeException>(() => DateTime.UtcNow.Truncate(TimeSpan.FromSeconds(-5)));

            // TimeSpan.Zero graularity, DateTime.MinValue, or trying to truncate a value that's already at
            // the desired granularity result in the identity function
            Assert.AreEqual(DateTime.MaxValue, DateTime.MaxValue.Truncate(TimeSpan.Zero));
            Assert.AreEqual(DateTime.MinValue, DateTime.MinValue.Truncate(TimeSpan.FromMinutes(3)));
            Assert.AreEqual(new DateTime(1413, 12, 11, 10, 9, 8), new DateTime(1413, 12, 11, 10, 9, 8).Truncate(TimeSpan.FromSeconds(1)));

            // Set to DateTime.MinValue when truncation would result in a smaller value
            Assert.AreEqual(DateTime.MinValue, DateTime.MinValue.AddHours(3).Truncate(TimeSpan.FromHours(5)));

            // A few normal cases using the different DateTimeKind values
            // (1) Truncate to the nearest hour with kind UTC
            DateTime date = new DateTime(2020, 7, 22, 10, 9, 8, DateTimeKind.Utc);
            Assert.AreEqual(new DateTime(2020, 7, 22, 10, 0, 0, DateTimeKind.Utc), date.Truncate(TimeSpan.FromHours(1)));

            // (2) Truncate to the nearest 5 minute interval with kind 'local'
            date = new DateTime(2000, 1, 1, 15, 14, 13, DateTimeKind.Local);
            Assert.AreEqual(new DateTime(2000, 1, 1, 15, 10, 0, DateTimeKind.Local), date.Truncate(TimeSpan.FromMinutes(5)));

            // (3) Truncate to the nearest day for an 'unspecified' kind
            date = new DateTime(1970, 4, 4, 1, 2, 3, DateTimeKind.Unspecified);
            Assert.AreEqual(new DateTime(1970, 4, 4, 0, 0, 0, DateTimeKind.Unspecified), date.Truncate(TimeSpan.FromDays(1)));
        }
    }
}
