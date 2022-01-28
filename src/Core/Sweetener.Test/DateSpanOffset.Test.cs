// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Test;

[TestClass]
public class DateSpanOffsetTest
{
    [TestMethod]
    public void DateSpanProperty()
    {
        DateSpanOffset actual;
        DateSpan expected = new DateSpan(
            DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified),
            TimeSpan.FromHours(1));

        // Non-Utc
        actual = new DateSpanOffset(expected, TimeSpan.FromHours(-5));
        Assert.AreEqual(expected, actual.DateSpan);

        // Utc
        actual = new DateSpanOffset(expected, TimeSpan.Zero);
        Assert.AreEqual(expected, actual.DateSpan);
    }

    [TestMethod]
    public void UtcDateSpan()
    {
        DateSpanOffset actual;
        DateSpan interval = new DateSpan(
            DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified),
            TimeSpan.FromHours(1));

        // Non-Utc
        actual = new DateSpanOffset(interval, TimeSpan.FromHours(5));
        Assert.AreEqual(new DateSpan(interval.Start.AddHours(-5), interval.Duration), actual.UtcDateSpan);

        // Utc
        actual = new DateSpanOffset(interval, TimeSpan.Zero);
        Assert.AreEqual(interval, actual.UtcDateSpan);
    }

    [TestMethod]
    public void Empty()
    {
        Assert.AreEqual(DateTimeOffset.MinValue, DateSpanOffset.Empty.Start   );
        Assert.AreEqual(DateTimeOffset.MinValue, DateSpanOffset.Empty.End     );
        Assert.AreEqual(TimeSpan.Zero          , DateSpanOffset.Empty.Duration);
        Assert.AreEqual(TimeSpan.Zero          , DateSpanOffset.Empty.Offset  );

        Assert.AreEqual(default, DateSpanOffset.Empty);
    }

    // For many of the ctor tests, we'll rely on the underlying DateTimeOffset ctor to
    // throw ArgumentOutOfRangeExceptions for invalid years, months, etc.

    [TestMethod]
    public void Ctor_DateSpan()
    {
        DateSpanOffset actual;
        DateSpan expected;

        // Non-Utc
        expected = new DateSpan(DateTime.Now, TimeSpan.FromDays(1));

        actual = new DateSpanOffset(expected);
        Assert.AreEqual(expected.Start           , actual.Start   );
        Assert.AreEqual(expected.End             , actual.End     );
        Assert.AreEqual(expected.Duration        , actual.Duration);
        Assert.AreEqual(DateTimeOffset.Now.Offset, actual.Offset  );

        // Utc
        expected = new DateSpan(DateTime.UtcNow, TimeSpan.FromDays(1));

        actual = new DateSpanOffset(expected);
        Assert.AreEqual(expected.Start   , actual.Start   );
        Assert.AreEqual(expected.End     , actual.End     );
        Assert.AreEqual(expected.Duration, actual.Duration);
        Assert.AreEqual(TimeSpan.Zero    , actual.Offset  );
    }

    [TestMethod]
    public void Ctor_DateSpan_Offset()
    {
        // Invalid Offsets
        Assert.ThrowsException<ArgumentException>(
            () => new DateSpanOffset(new DateSpan(DateTime.UtcNow, TimeSpan.FromDays(1)), TimeSpan.FromHours(1)));
        Assert.ThrowsException<ArgumentException>(
            () => new DateSpanOffset(new DateSpan(DateTime.Now, TimeSpan.FromDays(1)), GetNonLocalOffset()));

        DateSpanOffset actual;
        DateSpan expected;

        // Non-Utc
        expected = DateSpan.SpecifyKind(new DateSpan(DateTime.Now, TimeSpan.FromDays(1)), DateTimeKind.Unspecified);

        actual = new DateSpanOffset(expected, TimeSpan.FromHours(6));
        Assert.AreEqual(new DateTimeOffset(expected.Start, TimeSpan.FromHours(6)), actual.Start   );
        Assert.AreEqual(new DateTimeOffset(expected.End  , TimeSpan.FromHours(6)), actual.End     );
        Assert.AreEqual(expected.Duration                                        , actual.Duration);
        Assert.AreEqual(TimeSpan.FromHours(6)                                    , actual.Offset  );

        // Utc
        expected = new DateSpan(DateTime.UtcNow, TimeSpan.FromDays(1));

        actual = new DateSpanOffset(expected, TimeSpan.Zero);
        Assert.AreEqual(expected.Start   , actual.Start   );
        Assert.AreEqual(expected.End     , actual.End     );
        Assert.AreEqual(expected.Duration, actual.Duration);
        Assert.AreEqual(TimeSpan.Zero    , actual.Offset  );
    }

    [TestMethod]
    public void Ctor_Seconds()
    {
        // Negative Duration
        Assert.ThrowsException<ArgumentNegativeException>(() => new DateSpanOffset(
            2020, 1, 2, 10, 25, 10, TimeSpan.Zero, -TimeSpan.FromHours(4)));

        DateSpanOffset actual;
        TimeSpan offset = TimeSpan.FromHours(-8);
        DateTimeOffset expectedStart = new DateTimeOffset(2020, 1, 2, 10, 25, 10, offset);

        // Empty
        actual = new DateSpanOffset(expectedStart, expectedStart);
        Assert.AreEqual(DateTimeOffset.MinValue , actual.Start   );
        Assert.AreEqual(DateTimeOffset.MinValue , actual.End     );
        Assert.AreEqual(TimeSpan.Zero           , actual.Duration);
        Assert.AreEqual(TimeSpan.Zero           , actual.Offset  );

        // Normal Use-Case
        actual = new DateSpanOffset(2020, 1, 2, 10, 25, 10, offset, TimeSpan.FromHours(4));
        Assert.AreEqual(expectedStart            , actual.Start   );
        Assert.AreEqual(expectedStart.AddHours(4), actual.End     );
        Assert.AreEqual(TimeSpan.FromHours(4)    , actual.Duration);
        Assert.AreEqual(offset                   , actual.Offset  );
    }

    [TestMethod]
    public void Ctor_Milliseconds()
    {
        // Negative Duration
        Assert.ThrowsException<ArgumentNegativeException>(() => new DateSpanOffset(
            2020, 1, 2, 10, 25, 10, 36, TimeSpan.Zero, -TimeSpan.FromHours(4)));

        DateSpanOffset actual;
        TimeSpan offset = TimeSpan.FromHours(-8);
        DateTimeOffset expectedStart = new DateTimeOffset(2020, 1, 2, 10, 25, 10, 36, offset);

        // Empty
        actual = new DateSpanOffset(expectedStart, expectedStart);
        Assert.AreEqual(DateTimeOffset.MinValue , actual.Start   );
        Assert.AreEqual(DateTimeOffset.MinValue , actual.End     );
        Assert.AreEqual(TimeSpan.Zero           , actual.Duration);
        Assert.AreEqual(TimeSpan.Zero           , actual.Offset  );

        // Normal Use-Case
        actual = new DateSpanOffset(2020, 1, 2, 10, 25, 10, 36, offset, TimeSpan.FromHours(4));
        Assert.AreEqual(expectedStart            , actual.Start   );
        Assert.AreEqual(expectedStart.AddHours(4), actual.End     );
        Assert.AreEqual(TimeSpan.FromHours(4)    , actual.Duration);
        Assert.AreEqual(offset                   , actual.Offset  );
    }

    [TestMethod]
    public void Ctor_Milliseconds_Calendar()
    {
        // Null Calendar
        Assert.ThrowsException<ArgumentNullException>(() => new DateSpanOffset(5782, 4, 29, 10, 25, 10, 36, null!, TimeSpan.Zero, TimeSpan.FromHours(4)));

        // Negative Duration
        Assert.ThrowsException<ArgumentNegativeException>(() => new DateSpanOffset(5782, 4, 29, 10, 25, 10, 36, new HebrewCalendar(), TimeSpan.Zero , - TimeSpan.FromHours(4)));

        DateSpanOffset actual;
        TimeSpan offset = TimeSpan.FromHours(-8);
        DateTimeOffset expectedStart = new DateTimeOffset(5782, 4, 29, 10, 25, 10, 36, new HebrewCalendar(), offset);

        // Empty
        actual = new DateSpanOffset(expectedStart, expectedStart);
        Assert.AreEqual(DateTimeOffset.MinValue , actual.Start   );
        Assert.AreEqual(DateTimeOffset.MinValue , actual.End     );
        Assert.AreEqual(TimeSpan.Zero           , actual.Duration);
        Assert.AreEqual(TimeSpan.Zero           , actual.Offset  );

        // Normal Use-Case
        actual = new DateSpanOffset(5782, 4, 29, 10, 25, 10, 36, new HebrewCalendar(), offset, TimeSpan.FromHours(4));
        Assert.AreEqual(expectedStart            , actual.Start   );
        Assert.AreEqual(expectedStart.AddHours(4), actual.End     );
        Assert.AreEqual(TimeSpan.FromHours(4)    , actual.Duration);
        Assert.AreEqual(offset                   , actual.Offset  );
    }

    [TestMethod]
    public void Ctor_DateTime_TimeSpan_TimeSpan()
    {
        // Invalid Offsets
        Assert.ThrowsException<ArgumentException>(
            () => new DateSpanOffset(DateTime.UtcNow, TimeSpan.FromDays(1), TimeSpan.FromHours(1)));
        Assert.ThrowsException<ArgumentException>(
            () => new DateSpanOffset(DateTime.Now, TimeSpan.FromDays(1), GetNonLocalOffset()));

        // Negative Duration
        Assert.ThrowsException<ArgumentNegativeException>(
            () => new DateSpanOffset(DateTime.UtcNow, TimeSpan.Zero, -TimeSpan.FromMinutes(10)));

        DateSpanOffset actual;
        TimeSpan offset = TimeSpan.FromHours(-8);
        DateTimeOffset expectedStart = new DateTimeOffset(new DateTime(2010, 11, 12), offset);

        // Empty
        actual = new DateSpanOffset(expectedStart, expectedStart);
        Assert.AreEqual(DateTimeOffset.MinValue , actual.Start   );
        Assert.AreEqual(DateTimeOffset.MinValue , actual.End     );
        Assert.AreEqual(TimeSpan.Zero           , actual.Duration);
        Assert.AreEqual(TimeSpan.Zero           , actual.Offset  );

        // Normal Use-Case
        actual = new DateSpanOffset(new DateTime(2010, 11, 12), offset, TimeSpan.FromHours(4));
        Assert.AreEqual(expectedStart            , actual.Start   );
        Assert.AreEqual(expectedStart.AddHours(4), actual.End     );
        Assert.AreEqual(TimeSpan.FromHours(4)    , actual.Duration);
        Assert.AreEqual(offset                   , actual.Offset  );
    }

    [TestMethod]
    public void Ctor_DateTime_DateTime()
    {
        // Wrong Argument Order
        Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => new DateSpanOffset(DateTimeOffset.Now, DateTimeOffset.Now.AddMinutes(-5)));

        DateSpanOffset actual;
        DateTimeOffset expectedStart = DateTimeOffset.Now;

        // Empty
        actual = new DateSpanOffset(expectedStart, expectedStart);
        Assert.AreEqual(DateTimeOffset.MinValue , actual.Start   );
        Assert.AreEqual(DateTimeOffset.MinValue , actual.End     );
        Assert.AreEqual(TimeSpan.Zero           , actual.Duration);
        Assert.AreEqual(TimeSpan.Zero           , actual.Offset  );

        // Normal Use-Case
        actual = new DateSpanOffset(expectedStart, expectedStart.AddDays(5));
        Assert.AreEqual(expectedStart           , actual.Start   );
        Assert.AreEqual(expectedStart.AddDays(5), actual.End     );
        Assert.AreEqual(TimeSpan.FromDays(5)    , actual.Duration);
        Assert.AreEqual(expectedStart.Offset    , actual.Offset  );
    }

    [TestMethod]
    public void Ctor_DateTime_TimeSpan()
    {
        // Negative Duration
        Assert.ThrowsException<ArgumentNegativeException>(
            () => new DateSpanOffset(DateTimeOffset.Now, TimeSpan.FromSeconds(-30)));

        // Duration Too Long
        Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => new DateSpanOffset(DateTimeOffset.MaxValue.AddSeconds(-3), TimeSpan.FromSeconds(4)));

        DateSpanOffset actual;
        DateTimeOffset expectedStart = new DateTimeOffset(2020, 05, 15, 1, 2, 3, TimeSpan.FromHours(-8));

        // Empty
        actual = new DateSpanOffset(expectedStart, TimeSpan.Zero);
        Assert.AreEqual(DateTimeOffset.MinValue , actual.Start   );
        Assert.AreEqual(DateTimeOffset.MinValue , actual.End     );
        Assert.AreEqual(TimeSpan.Zero           , actual.Duration);
        Assert.AreEqual(TimeSpan.Zero           , actual.Offset  );

        // Normal Use-Case
        actual = new DateSpanOffset(expectedStart, TimeSpan.FromDays(5));
        Assert.AreEqual(expectedStart           , actual.Start   );
        Assert.AreEqual(expectedStart.AddDays(5), actual.End     );
        Assert.AreEqual(TimeSpan.FromDays(5)    , actual.Duration);
        Assert.AreEqual(expectedStart.Offset    , actual.Offset  );
    }

    [TestMethod]
    public void Ctor_DateTime_Ticks()
    {
        // Negative Duration
        Assert.ThrowsException<ArgumentNegativeException>(() => new DateSpanOffset(DateTimeOffset.Now, -1));

        // Duration Too Long
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new DateSpanOffset(DateTimeOffset.MaxValue, 2));

        DateSpanOffset actual;
        DateTimeOffset expectedStart = new DateTimeOffset(2020, 05, 15, 1, 2, 3, TimeSpan.FromHours(-8));

        // Empty
        actual = new DateSpanOffset(expectedStart, 0L);
        Assert.AreEqual(DateTimeOffset.MinValue , actual.Start   );
        Assert.AreEqual(DateTimeOffset.MinValue , actual.End     );
        Assert.AreEqual(TimeSpan.Zero           , actual.Duration);
        Assert.AreEqual(TimeSpan.Zero           , actual.Offset  );

        // Normal Use-Case
        actual = new DateSpanOffset(expectedStart, 10000);
        Assert.AreEqual(expectedStart                , actual.Start   );
        Assert.AreEqual(expectedStart.AddTicks(10000), actual.End     );
        Assert.AreEqual(TimeSpan.FromTicks(10000)    , actual.Duration);
        Assert.AreEqual(expectedStart.Offset         , actual.Offset  );
    }

    [TestMethod]
    public void CompareTo_Object()
    {
        // Wrong Type
        Assert.ThrowsException<ArgumentException>(() => DateSpanOffset.Empty.CompareTo("hello world"));

        Assert.AreEqual(1, DateSpanOffset.Empty.CompareTo(null!));
        CompareTo((d1, d2) => d1.CompareTo((object)d2));
    }

    [TestMethod]
    public void CompareTo_DateSpan()
        => CompareTo((d1, d2) => d1.CompareTo(d2));

    private static void CompareTo(Func<DateSpanOffset, DateSpanOffset, int> compareTo)
        => CompareTo(compareTo, -1, 0, 1);

    private static void CompareTo<T>(
        Func<DateSpanOffset, DateSpanOffset, T> compareTo,
        T lessThan,
        T equal,
        T greaterThan)
    {
        // In the following examples, note that the offset is used to normalize
        DateTimeOffset utcNow = DateTimeOffset.UtcNow;
        DateTimeOffset localNow = utcNow.ToLocalTime();

        // Equal
        Assert.AreEqual(equal, compareTo(new DateSpanOffset(utcNow, TimeSpan.FromDays(3)), new DateSpanOffset(localNow, TimeSpan.FromDays(3))));

        // Less Than
        Assert.AreEqual(lessThan, compareTo(new DateSpanOffset(utcNow, 100), new DateSpanOffset(localNow.AddTicks(10), 100)));
        Assert.AreEqual(lessThan, compareTo(new DateSpanOffset(utcNow, 100), new DateSpanOffset(localNow             , 150)));

        // Greater Than
        Assert.AreEqual(greaterThan, compareTo(new DateSpanOffset(utcNow.AddTicks(3), 100), new DateSpanOffset(localNow, 100)));
        Assert.AreEqual(greaterThan, compareTo(new DateSpanOffset(utcNow            , 125), new DateSpanOffset(localNow, 100)));
    }

    [TestMethod]
    public void Contains_DateTime()
    {
        DateSpanOffset interval = DateSpanOffset.FromMonth(1000, 10, TimeSpan.Zero);

        // False
        Assert.IsFalse(DateSpanOffset.Empty.Contains(DateTimeOffset.UtcNow));
        Assert.IsFalse(interval.Contains(DateTimeOffset.MinValue));
        Assert.IsFalse(interval.Contains(new DateTimeOffset( 900,  4, 16, 0, 0, 0, TimeSpan.FromHours( 1))));
        Assert.IsFalse(interval.Contains(new DateTimeOffset(1000, 10,  1, 0, 0, 0, TimeSpan.FromHours( 1))));
        Assert.IsFalse(interval.Contains(new DateTimeOffset(2000,  3,  4, 5, 6, 7, TimeSpan.FromHours(-8))));
        Assert.IsFalse(interval.Contains(DateTimeOffset.MaxValue));

        // True
        Assert.IsTrue(interval.Contains(new DateTimeOffset(1000,  9, 30, 23, 0, 0, TimeSpan.FromHours(-1))));
        Assert.IsTrue(interval.Contains(new DateTimeOffset(1000, 10,  1,  0, 0, 0, TimeSpan.Zero)));
        Assert.IsTrue(interval.Contains(new DateTimeOffset(1000, 10,  9,  8, 7, 6, TimeSpan.FromHours(3))));
        Assert.IsTrue(interval.Contains(new DateTimeOffset(1000, 11,  1,  0, 0, 0, TimeSpan.Zero).AddTicks(-1)));
    }

    [TestMethod]
    public void Contains_DateSpan()
    {
        // Empty Instance
        Assert.IsTrue (DateSpanOffset.Empty.Contains(DateSpanOffset.Empty));
        Assert.IsFalse(DateSpanOffset.Empty.Contains(DateSpanOffset.FromYear(2022, TimeSpan.Zero)));

        // Non-Empty Instance
        DateSpanOffset interval = DateSpanOffset.FromMonth(2000, 06, TimeSpan.Zero);

        // Empty Argument
        Assert.IsTrue(interval.Contains(DateSpanOffset.Empty));

        // Moving around the interval
        Assert.IsFalse(interval.Contains(new DateSpanOffset(interval.Start.AddYears(-1), interval.Duration)));
        Assert.IsFalse(interval.Contains(new DateSpanOffset(interval.Start.AddDays(-15), interval.Duration)));
        Assert.IsFalse(interval.Contains(DateSpanOffset.FromMonth(2000, 06, TimeSpan.FromHours(5))));
        Assert.IsTrue (interval.Contains(interval));
        Assert.IsFalse(interval.Contains(new DateSpanOffset(interval.Start.AddDays(15), interval.Duration)));
        Assert.IsFalse(interval.Contains(new DateSpanOffset(interval.Start.AddYears(1), interval.Duration)));

        // Smaller interval
        Assert.IsTrue(interval.Contains(DateSpanOffset.FromDay(2000, 06, 15, TimeSpan.FromHours(1))));

        // Larger interval
        Assert.IsFalse(interval.Contains(DateSpanOffset.FromYear(2000, TimeSpan.FromHours(2))));
        Assert.IsFalse(interval.Contains(new DateSpanOffset(interval.Start              , interval.End.AddMonths( 1))));
        Assert.IsFalse(interval.Contains(new DateSpanOffset(interval.Start.AddMonths(-1), interval.End              )));
    }

    [TestMethod]
    public void Equals_Object()
    {
        Assert.IsFalse(DateSpanOffset.FromYear(2012, TimeSpan.Zero).Equals(null!));
        Assert.IsFalse(DateSpanOffset.FromYear(2012, TimeSpan.Zero).Equals("Foo"));

        Equals((d1, d2) => d1.Equals((object)d2));
    }

    [TestMethod]
    public void Equals_DateSpan()
        => Equals((d1, d2) => d1.Equals(d2));

    private static void Equals(Func<DateSpanOffset, DateSpanOffset, bool> equals)
    {
        // In the following examples, note that the Kind doesn't make a difference
        DateTimeOffset utcNow = DateTimeOffset.UtcNow;
        DateTimeOffset localNow = utcNow.ToLocalTime();

        // Equal
        Assert.IsTrue(equals(DateSpanOffset.FromYear(9000, TimeSpan.FromHours(1)), DateSpanOffset.FromYear(9000, TimeSpan.FromHours(1))));
        Assert.IsTrue(equals(DateSpanOffset.FromDateTimeOffset(utcNow), DateSpanOffset.FromDateTimeOffset(localNow)));

        // Not Equal
        Assert.IsFalse(equals(new DateSpanOffset(utcNow            , 123), new DateSpanOffset(utcNow.AddSeconds(4) , 123)));
        Assert.IsFalse(equals(new DateSpanOffset(localNow          , 123), new DateSpanOffset(utcNow               , 456)));
        Assert.IsFalse(equals(new DateSpanOffset(utcNow.AddHours(1), 123), new DateSpanOffset(localNow.AddDays(-10), 456)));
        Assert.IsFalse(equals(
            new DateSpanOffset(2000, 1, 2, 3, 4, 5, 6, TimeSpan.Zero        , TimeSpan.FromHours(1)),
            new DateSpanOffset(2000, 1, 2, 3, 4, 5, 6, TimeSpan.FromHours(1), TimeSpan.FromHours(1))));
    }

    [TestMethod]
    public void EqualsOperator()
        => Equals((d1, d2) => d1 == d2);

    [TestMethod]
    public void NotEqualsOperator()
        => Equals((d1, d2) => !(d1 != d2));

    [TestMethod]
    public void GetHashCodeTest()
        => Equals((d1, d2) => d1.GetHashCode() == d2.GetHashCode());

    [TestMethod]
    public void GreaterThanOperator()
        => CompareTo((d1, d2) => d1 > d2, false, false, true);

    [TestMethod]
    public void GreaterThanOrEqualOperator()
        => CompareTo((d1, d2) => d1 >= d2, false, true, true);

    [TestMethod]
    public void IntersectWith()
        => AssertIntersectionMethod((d1, d2) => d1.IntersectWith(d2), d => d);

    [TestMethod]
    public void Overlaps()
        => AssertIntersectionMethod((d1, d2) => d1.Overlaps(d2), d => d != default);

    private static void AssertIntersectionMethod<T>(
        Func<DateSpanOffset, DateSpanOffset, T> getActual,
        Func<DateSpanOffset, T> getExpected)
    {
        // Empty Input
        AssertSymmetric(default, default, DateSpanOffset.FromYear(4000, TimeSpan.Zero));
        Assert.AreEqual(getExpected(default), getActual(default, default));

        // Empty Results
        AssertSymmetric(default, DateSpanOffset.FromMonth(1991, 1, TimeSpan.FromHours(1)), DateSpanOffset.FromMonth(2000, 1, TimeSpan.Zero));
        Assert.AreEqual(getExpected(default), getActual(DateSpanOffset.FromDay(10, 9, 8, TimeSpan.FromHours(2)), DateSpanOffset.FromDay(10, 9, 9, TimeSpan.FromHours(2))));
        Assert.AreEqual(getExpected(default), getActual(DateSpanOffset.FromDay(10, 9, 8, TimeSpan.FromHours(2)), DateSpanOffset.FromDay(10, 9, 7, TimeSpan.FromHours(2))));

        // Single DateTimeOffset
        DateTimeOffset utcNow = DateTimeOffset.UtcNow;
        AssertSymmetric(
            DateSpanOffset.FromDateTimeOffset(utcNow),
            DateSpanOffset.FromMonth(utcNow.Year, utcNow.Month, TimeSpan.Zero),
            DateSpanOffset.FromDateTimeOffset(utcNow));

        // Subset
        AssertSymmetric(
            DateSpanOffset.FromMonth(1234, 5, TimeSpan.FromHours(-5)),
            DateSpanOffset.FromMonth(1234, 5, TimeSpan.FromHours(-5)),
            DateSpanOffset.FromYear(1234, TimeSpan.Zero));

        // Same Start
        AssertSymmetric(
            DateSpanOffset.FromDay(2010, 11, 12, TimeSpan.FromHours(1)),
            DateSpanOffset.FromDay(2010, 11, 12, TimeSpan.FromHours(1)),
            new DateSpanOffset(new DateTime(2010, 11, 12), TimeSpan.FromHours(1), TimeSpan.FromHours(36)));

        // Complex Intersection
        DateSpanOffset d1       = DateSpanOffset.FromDay(1456, 7, 8, TimeSpan.FromHours(2));
        DateSpanOffset d2       = new DateSpanOffset(1456, 7, 8, 9, 0, 0, TimeSpan.FromHours(2), TimeSpan.FromDays(2));
        DateSpanOffset expected = new DateSpanOffset(1456, 7, 8, 9, 0, 0, TimeSpan.FromHours(2), TimeSpan.FromHours(15));
        AssertSymmetric(expected, d1, d2);

        void AssertSymmetric(DateSpanOffset expected, DateSpanOffset first, DateSpanOffset second)
        {
            Assert.AreEqual(getExpected(expected), getActual(first , second));
            Assert.AreEqual(getExpected(expected), getActual(second, first ));
        }
    }

    [TestMethod]
    public void LessThanOperator()
        => CompareTo((d1, d2) => d1 < d2, true, false, false);

    [TestMethod]
    public void LessThanOrEqualOperator()
        => CompareTo((d1, d2) => d1 <= d2, true, true, false);

    [TestMethod]
    public void ImpicitDateSpanCast()
    {
        DateSpanOffset actual;
        DateSpan expected;

        // Non-Utc
        expected = new DateSpan(DateTime.Now, TimeSpan.FromDays(1));

        actual = expected;
        Assert.AreEqual(expected.Start           , actual.Start   );
        Assert.AreEqual(expected.End             , actual.End     );
        Assert.AreEqual(expected.Duration        , actual.Duration);
        Assert.AreEqual(DateTimeOffset.Now.Offset, actual.Offset  );

        // Utc
        expected = new DateSpan(DateTime.UtcNow, TimeSpan.FromDays(1));

        actual = expected;
        Assert.AreEqual(expected.Start   , actual.Start   );
        Assert.AreEqual(expected.End     , actual.End     );
        Assert.AreEqual(expected.Duration, actual.Duration);
        Assert.AreEqual(TimeSpan.Zero    , actual.Offset  );
    }

    [TestMethod]
    public void ExplicitDateTimeOffsetCast()
    {
        DateSpanOffset actual;

        DateTimeOffset utcNow = DateTimeOffset.UtcNow;
        actual = (DateSpanOffset)utcNow;
        Assert.AreEqual(utcNow               , actual.Start   );
        Assert.AreEqual(utcNow.AddTicks(1)   , actual.End     );
        Assert.AreEqual(TimeSpan.FromTicks(1), actual.Duration);
        Assert.AreEqual(TimeSpan.Zero        , actual.Offset  );

        DateTimeOffset now = DateTimeOffset.Now;
        actual = (DateSpanOffset)now;
        Assert.AreEqual(now                  , actual.Start   );
        Assert.AreEqual(now.AddTicks(1)      , actual.End     );
        Assert.AreEqual(TimeSpan.FromTicks(1), actual.Duration);
        Assert.AreEqual(now.Offset           , actual.Offset  );
    }

    [TestMethod]
    public void FromDateTimeOffset()
    {
        DateSpanOffset actual;

        DateTimeOffset utcNow = DateTimeOffset.UtcNow;
        actual = DateSpanOffset.FromDateTimeOffset(utcNow);
        Assert.AreEqual(utcNow               , actual.Start   );
        Assert.AreEqual(utcNow.AddTicks(1)   , actual.End     );
        Assert.AreEqual(TimeSpan.FromTicks(1), actual.Duration);
        Assert.AreEqual(TimeSpan.Zero        , actual.Offset  );

        DateTimeOffset now = DateTimeOffset.Now;
        actual = DateSpanOffset.FromDateTimeOffset(now);
        Assert.AreEqual(now                  , actual.Start   );
        Assert.AreEqual(now.AddTicks(1)      , actual.End     );
        Assert.AreEqual(TimeSpan.FromTicks(1), actual.Duration);
        Assert.AreEqual(now.Offset           , actual.Offset  );
    }

    [TestMethod]
    public void FromDay()
        => FromDay((y, m, d, o) => DateSpanOffset.FromDay(y, m, d, o));

    [TestMethod]
    public void FromDay_Calendar()
    {
        Assert.ThrowsException<ArgumentNullException>(() => DateSpanOffset.FromDay(1981, 02, 03, null!, TimeSpan.Zero));

        FromDay((y, m, d, o) => DateSpanOffset.FromDay(y, m, d, new HebrewCalendar(), o), 5782, 4, 10, new HebrewCalendar());
    }

    private static void FromDay(
        Func<int, int, int, TimeSpan, DateSpanOffset> fromDay,
        int year = 1981,
        int month = 02,
        int day = 03,
        Calendar? calendar = null)
    {
        TimeSpan offset = TimeSpan.FromHours(-8);
        DateSpanOffset actual = fromDay(year, month, day, offset);
        DateTimeOffset expectedStart = new DateTimeOffset(year, month, day, 0, 0, 0, 0, calendar ?? new GregorianCalendar(), offset);

        Assert.AreEqual(expectedStart           , actual.Start );
        Assert.AreEqual(expectedStart.AddDays(1), actual.End   );
        Assert.AreEqual(offset                  , actual.Offset);
    }

    [TestMethod]
    public void FromMonth()
        => FromMonth((y, m, o) => DateSpanOffset.FromMonth(y, m, o));

    [TestMethod]
    public void FromMonth_Calendar()
    {
        Assert.ThrowsException<ArgumentNullException>(() => DateSpanOffset.FromMonth(2022, 03, null!, TimeSpan.Zero));

        FromMonth((y, m, o) => DateSpanOffset.FromMonth(y, m, new HebrewCalendar(), o), 5757, 5, new HebrewCalendar());
    }

    private static void FromMonth(Func<int, int, TimeSpan, DateSpanOffset> fromMonth, int year = 2022, int month = 03, Calendar? calendar = null)
    {
        TimeSpan offset = TimeSpan.FromHours(-8);
        DateSpanOffset actual = fromMonth(year, month, offset);
        DateTimeOffset expectedStart = new DateTimeOffset(year, month, 01, 0, 0, 0, 0, calendar ?? new GregorianCalendar(), offset);

        Assert.AreEqual(expectedStart             , actual.Start );
        Assert.AreEqual(expectedStart.AddMonths(1), actual.End   );
        Assert.AreEqual(offset                    , actual.Offset);
    }

    [TestMethod]
    public void FromYear()
        => FromYear((y, o) => DateSpanOffset.FromYear(y, o));

    [TestMethod]
    public void FromYear_Calendar()
    {
        Assert.ThrowsException<ArgumentNullException>(() => DateSpanOffset.FromYear(2010, null!, TimeSpan.Zero));

        FromYear((y, o) => DateSpanOffset.FromYear(y, new HebrewCalendar(), o), 5757, new HebrewCalendar());
    }

    private static void FromYear(Func<int, TimeSpan, DateSpanOffset> fromYear, int year = 2010, Calendar? calendar = null)
    {
        TimeSpan offset = TimeSpan.FromHours(-8);
        DateSpanOffset actual = fromYear(year, offset);
        DateTimeOffset expectedStart = new DateTimeOffset(year, 01, 01, 0, 0, 0, 0, calendar ?? new GregorianCalendar(), offset);

        Assert.AreEqual(expectedStart            , actual.Start );
        Assert.AreEqual(expectedStart.AddYears(1), actual.End   );
        Assert.AreEqual(offset                   , actual.Offset);
    }

    private static TimeSpan GetNonLocalOffset()
        => DateTimeOffset.Now.Offset != TimeSpan.FromHours(-5) ? TimeSpan.FromHours(-5) : TimeSpan.FromHours(-8);
}
