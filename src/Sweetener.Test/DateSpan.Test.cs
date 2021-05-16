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

        [TestMethod]
        public void CompareTo_Object()
        {
            // Wrong Type
            Assert.ThrowsException<ArgumentException>(() => DateSpan.MinValue.CompareTo("hello world"));

            Assert.AreEqual(1, DateSpan.MinValue.CompareTo(null!));
            CompareTo((d1, d2) => d1.CompareTo((object)d2));
        }

        [TestMethod]
        public void CompareTo_DateSpan()
            => CompareTo((d1, d2) => d1.CompareTo(d2));

        private static void CompareTo(Func<DateSpan, DateSpan, int> compareTo)
        {
            // In the following examples, note that the Kind doesn't make a difference
            DateTime utcNow = DateTime.UtcNow;
            DateTime localNow = new DateTime(utcNow.Ticks, DateTimeKind.Local);

            // Equal
            Assert.AreEqual(0, compareTo(new DateSpan(utcNow, TimeSpan.FromDays(3)), new DateSpan(localNow, TimeSpan.FromDays(3))));

            // Less Than
            Assert.AreEqual(-1, compareTo(new DateSpan(utcNow, 100), new DateSpan(localNow.AddTicks(10), 100)));
            Assert.AreEqual(-1, compareTo(new DateSpan(utcNow, 100), new DateSpan(localNow             , 150)));

            // Greater Than
            Assert.AreEqual(1, compareTo(new DateSpan(utcNow.AddTicks(3), 100), new DateSpan(localNow, 100)));
            Assert.AreEqual(1, compareTo(new DateSpan(utcNow            , 125), new DateSpan(localNow, 100)));
        }

        [TestMethod]
        public void Contains()
        {
            DateSpan interval = DateSpan.FromMonth(1000, 10);

            // False
            Assert.IsFalse(DateSpan.MinValue.Contains(DateTime.UtcNow));
            Assert.IsFalse(interval.Contains(DateTime.MinValue));
            Assert.IsFalse(interval.Contains(new DateTime( 900, 4, 16, 0, 0, 0, DateTimeKind.Utc  )));
            Assert.IsFalse(interval.Contains(new DateTime(2000, 3,  4, 5, 6, 7, DateTimeKind.Local)));
            Assert.IsFalse(interval.Contains(DateTime.MaxValue));

            // True
            Assert.IsTrue(interval.Contains(new DateTime(1000, 10, 1, 0, 0, 0, DateTimeKind.Unspecified)));
            Assert.IsTrue(interval.Contains(new DateTime(1000, 10, 9, 8, 7, 6, DateTimeKind.Local)));
            Assert.IsTrue(interval.Contains(new DateTime(1000, 11, 1, 0, 0, 0, DateTimeKind.Utc).AddTicks(-1)));
        }

        [TestMethod]
        public void Equals_Object()
        {
            Assert.IsFalse(DateSpan.FromYear(2012).Equals(null!));
            Assert.IsFalse(DateSpan.FromYear(2012).Equals("Foo"));

            Equals((d1, d2) => d1.Equals((object)d2));
        }

        [TestMethod]
        public void Equals_DateSpan()
            => Equals((d1, d2) => d1.Equals(d2));

        private static void Equals(Func<DateSpan, DateSpan, bool> equals)
        {
            // In the following examples, note that the Kind doesn't make a difference
            DateTime utcNow = DateTime.UtcNow;
            DateTime localNow = new DateTime(utcNow.Ticks, DateTimeKind.Local);

            // Equal
            Assert.IsTrue(equals(DateSpan.FromYear(9000), DateSpan.FromYear(9000)));
            Assert.IsTrue(equals(DateSpan.FromDateTime(utcNow), DateSpan.FromDateTime(localNow)));

            // Not Equal
            Assert.IsFalse(equals(new DateSpan(utcNow            , 123), new DateSpan(utcNow.AddSeconds(4) , 123)));
            Assert.IsFalse(equals(new DateSpan(localNow          , 123), new DateSpan(utcNow               , 456)));
            Assert.IsFalse(equals(new DateSpan(utcNow.AddHours(1), 123), new DateSpan(localNow.AddDays(-10), 456)));
        }

        [TestMethod]
        public void IntersectWith()
        {
            DateTime utcNow = DateTime.UtcNow;

            // Empty Input
            Assert.AreEqual(default, DateSpan.FromYear(4000).IntersectWith(default));
            Assert.AreEqual(default, DateSpan.MinValue      .IntersectWith(DateSpan.FromYear(3000)));
            Assert.AreEqual(default, DateSpan.MinValue      .IntersectWith(default));

            // Empty Results
            Assert.AreEqual(default, DateSpan.FromYear(3000)   .IntersectWith(DateSpan.FromDay(2000, 10, 10)));
            Assert.AreEqual(default, DateSpan.FromDay(10, 9, 8).IntersectWith(DateSpan.FromDay(10, 9, 9)));
            Assert.AreEqual(default, DateSpan.FromDay(10, 9, 8).IntersectWith(DateSpan.FromDay(10, 9, 7)));

            // Single DateTime
            Assert.AreEqual(DateSpan.FromDateTime(utcNow), DateSpan.FromMonth(utcNow.Year, utcNow.Month).IntersectWith(DateSpan.FromDateTime(utcNow)));

            // Subset
            Assert.AreEqual(DateSpan.FromMonth(1234, 5), DateSpan.FromMonth(1234, 5).IntersectWith(DateSpan.FromYear(1234)));

            // Complex Intersection
            DateSpan d1       = DateSpan.FromDay(1456, 7, 8);
            DateSpan d2       = new DateSpan(new DateTime(1456, 7, 8, 9, 0, 0), TimeSpan.FromDays(2));
            DateSpan expected = new DateSpan(new DateTime(1456, 7, 8, 9, 0, 0), TimeSpan.FromHours(15));
            Assert.AreEqual(expected, d1.IntersectWith(d2));
            Assert.AreEqual(expected, d2.IntersectWith(d1));
        }

        [TestMethod]
        public void IsProperSubsetOf()
            => IsSubsetOf((d1, d2) => d1.IsProperSubsetOf(d2), strict: true);

        [TestMethod]
        public void IsProperSupersetOf()
            => IsSubsetOf((d1, d2) => d2.IsProperSupersetOf(d1), strict: true);

        [TestMethod]
        public void IsSubsetOf()
            => IsSubsetOf((d1, d2) => d1.IsSubsetOf(d2), strict: false);

        [TestMethod]
        public void IsSupersetOf()
            => IsSubsetOf((d1, d2) => d2.IsSupersetOf(d1), strict: false);

        private static void IsSubsetOf(Func<DateSpan, DateSpan, bool> isSubset, bool strict)
        {
            // Subsets
            Assert.AreEqual(!strict, isSubset(default , default)); // Same value
            Assert.AreEqual(!strict, isSubset(DateSpan.FromYear(468), DateSpan.FromYear(468)));

            Assert.IsTrue(isSubset(default                     , DateSpan.FromYear(2000)));
            Assert.IsTrue(isSubset(DateSpan.FromMonth(2020,  1), DateSpan.FromYear(2020))); // Overlap Start
            Assert.IsTrue(isSubset(DateSpan.FromMonth(2020, 12), DateSpan.FromYear(2020))); // Overlap end
            Assert.IsTrue(isSubset(DateSpan.FromMonth(2020,  3), DateSpan.FromYear(2020))); // Proper subset

            // Supersets
            Assert.IsFalse(isSubset(DateSpan.FromYear(2000), default));
            Assert.IsFalse(isSubset(DateSpan.FromYear(2020), DateSpan.FromMonth(2020,  1))); // Overlap Start
            Assert.IsFalse(isSubset(DateSpan.FromYear(2020), DateSpan.FromMonth(2020, 12))); // Overlap end
            Assert.IsFalse(isSubset(DateSpan.FromYear(2020), DateSpan.FromMonth(2020,  3))); // Proper superset

            // Neither
            Assert.IsFalse(isSubset(DateSpan.FromMonth(1000,     2), DateSpan.FromDay  (2000, 3, 4))); // No overlap
            Assert.IsFalse(isSubset(DateSpan.FromDay  (2000, 3,  4), DateSpan.FromMonth(1000, 2   )));
            Assert.IsFalse(isSubset(DateSpan.FromMonth(2345,     6), DateSpan.FromMonth(2345, 7   ))); // Adjacent intervals
            Assert.IsFalse(isSubset(DateSpan.FromMonth(2345,     7), DateSpan.FromMonth(2345, 6   )));
            Assert.IsFalse(isSubset(DateSpan.FromDay  (3000, 1, 10), new DateSpan(new DateTime(3000, 1, 10, 7, 0, 0), TimeSpan.FromDays(3)))); // Overlap
            Assert.IsFalse(isSubset(new DateSpan(new DateTime(3000, 1, 10, 7, 0, 0), TimeSpan.FromDays(3)), DateSpan.FromDay(3000, 1, 10)));
        }

        [TestMethod]
        public void EqualsOperator()
            => Equals((d1, d2) => d1 == d2);

        [TestMethod]
        public void NotEqualsOperator()
            => Equals((d1, d2) => !(d1 != d2));
    }
}
