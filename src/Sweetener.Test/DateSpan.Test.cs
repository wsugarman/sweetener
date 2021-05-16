// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Test
{
    [TestClass]
    public class DateSpanTest
    {
        [TestMethod]
        public void MaxValue()
        {
            Assert.AreEqual(DateTime.MinValue, DateSpan.MaxValue.Start);
            Assert.ThrowsException<InvalidOperationException>(() => DateSpan.MinValue.End);
            Assert.AreEqual(DateTime.MaxValue - DateTime.MinValue + TimeSpan.FromTicks(1), DateSpan.MaxValue.Duration);
            Assert.IsFalse(DateSpan.MaxValue.IsEmpty);
        }

        [TestMethod]
        public void MinValue()
        {
            Assert.ThrowsException<InvalidOperationException>(() => DateSpan.MinValue.Start);
            Assert.ThrowsException<InvalidOperationException>(() => DateSpan.MinValue.End  );
            Assert.AreEqual(TimeSpan.Zero, DateSpan.MinValue.Duration);
            Assert.IsTrue(DateSpan.MinValue.IsEmpty);

            Assert.AreEqual(default, DateSpan.MinValue);
        }

        [TestMethod]
        public void Ctor_DateTime_DateTime()
        {
            // DateTimeKind Mismatch
            Assert.ThrowsException<ArgumentException>(
                () => new DateSpan(new DateTime( 12345, DateTimeKind.Unspecified), new DateTime(678910, DateTimeKind.Local)));

            // Wrong Argument Order
            Assert.ThrowsException<ArgumentException>(
                () => new DateSpan(new DateTime(678910), new DateTime(12345)));

            DateSpan actual;
            DateTime expectedStart = new DateTime(2020, 05, 15);

            // Empty
            actual = new DateSpan(expectedStart, expectedStart);
            Assert.AreEqual(default, actual);

            // Normal Use-Case
            actual = new DateSpan(expectedStart, expectedStart.AddDays(5));
            Assert.AreEqual(expectedStart           , actual.Start   );
            Assert.AreEqual(expectedStart.AddDays(5), actual.End     );
            Assert.AreEqual(TimeSpan.FromDays(5)    , actual.Duration);
            Assert.IsFalse(actual.IsEmpty);
        }

        [TestMethod]
        public void Ctor_DateTime_TimeSpan()
        {
            // Negative Duration
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => new DateSpan(DateTime.Now, TimeSpan.FromSeconds(-30)));

            // Duration Too Long
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => new DateSpan(DateTime.MaxValue.AddSeconds(-3), TimeSpan.FromSeconds(4)));

            DateSpan actual;
            DateTime expectedStart = new DateTime(2020, 05, 15);

            // Empty
            actual = new DateSpan(expectedStart, TimeSpan.Zero);
            Assert.AreEqual(default, actual);

            // Normal Use-Case
            actual = new DateSpan(expectedStart, TimeSpan.FromDays(5));
            Assert.AreEqual(expectedStart           , actual.Start   );
            Assert.AreEqual(expectedStart.AddDays(5), actual.End     );
            Assert.AreEqual(TimeSpan.FromDays(5)    , actual.Duration);
            Assert.IsFalse(actual.IsEmpty);
        }

        [TestMethod]
        public void Ctor_DateTime_Ticks()
        {
            // Negative Duration
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new DateSpan(DateTime.Now, -1));

            // Duration Too Long
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new DateSpan(DateTime.MaxValue, 2));

            DateSpan actual;
            DateTime expectedStart = new DateTime(2020, 05, 15);

            // Empty
            actual = new DateSpan(expectedStart, 0L);
            Assert.AreEqual(default, actual);

            // Normal Use-Case
            actual = new DateSpan(expectedStart, 10000);
            Assert.AreEqual(expectedStart                , actual.Start   );
            Assert.AreEqual(expectedStart.AddTicks(10000), actual.End     );
            Assert.AreEqual(TimeSpan.FromTicks(10000)    , actual.Duration);
            Assert.IsFalse(actual.IsEmpty);
        }
    }
}
