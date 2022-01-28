// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Test;

[TestClass]
public class DateSpanTest
{
    [TestMethod]
    public void Empty()
    {
        Assert.AreEqual(DateTime.MinValue       , DateSpan.Empty.Start   );
        Assert.AreEqual(DateTime.MinValue       , DateSpan.Empty.End     );
        Assert.AreEqual(TimeSpan.Zero           , DateSpan.Empty.Duration);
        Assert.AreEqual(DateTimeKind.Unspecified, DateSpan.Empty.Kind    );

        Assert.AreEqual(default, DateSpan.Empty);
    }

    // For many of the ctor tests, we'll rely on the underlying DateTime ctor to
    // throw ArgumentOutOfRangeExceptions for invalid years, months, etc.

    [TestMethod]
    public void Ctor_Seconds()
    {
        // Negative Duration
        Assert.ThrowsException<ArgumentNegativeException>(() => new DateSpan(2020, 1, 2, 10, 25, 10, -TimeSpan.FromHours(4)));

        DateSpan actual;
        DateTime expectedStart = new DateTime(2020, 1, 2, 10, 25, 10);

        // Empty
        actual = new DateSpan(2020, 1, 2, 10, 25, 10, TimeSpan.Zero);
        Assert.AreEqual(DateTime.MinValue       , actual.Start   );
        Assert.AreEqual(DateTime.MinValue       , actual.End     );
        Assert.AreEqual(TimeSpan.Zero           , actual.Duration);
        Assert.AreEqual(DateTimeKind.Unspecified, actual.Kind    );

        // Normal Use-Case
        actual = new DateSpan(2020, 1, 2, 10, 25, 10, TimeSpan.FromHours(4));
        Assert.AreEqual(expectedStart            , actual.Start   );
        Assert.AreEqual(expectedStart.AddHours(4), actual.End     );
        Assert.AreEqual(TimeSpan.FromHours(4)    , actual.Duration);
        Assert.AreEqual(DateTimeKind.Unspecified , actual.Kind    );
    }

    [TestMethod]
    public void Ctor_Seconds_Kind()
    {
        // Invalid Kind
        Assert.ThrowsException<ArgumentException>(() => new DateSpan(2020, 1, 2, 10, 25, 10, (DateTimeKind)100, TimeSpan.FromHours(4)));

        // Negative Duration
        Assert.ThrowsException<ArgumentNegativeException>(() => new DateSpan(2020, 1, 2, 10, 25, 10, DateTimeKind.Local, -TimeSpan.FromHours(4)));

        DateSpan actual;
        DateTime expectedStart = new DateTime(2020, 1, 2, 10, 25, 10, DateTimeKind.Local);

        // Empty
        actual = new DateSpan(2020, 1, 2, 10, 25, 10, DateTimeKind.Local, TimeSpan.Zero);
        Assert.AreEqual(DateTime.MinValue       , actual.Start   );
        Assert.AreEqual(DateTime.MinValue       , actual.End     );
        Assert.AreEqual(TimeSpan.Zero           , actual.Duration);
        Assert.AreEqual(DateTimeKind.Unspecified, actual.Kind    );

        // Normal Use-Case
        actual = new DateSpan(2020, 1, 2, 10, 25, 10, DateTimeKind.Local, TimeSpan.FromHours(4));
        Assert.AreEqual(expectedStart            , actual.Start   );
        Assert.AreEqual(expectedStart.AddHours(4), actual.End     );
        Assert.AreEqual(TimeSpan.FromHours(4)    , actual.Duration);
        Assert.AreEqual(DateTimeKind.Local       , actual.Kind    );
    }

    [TestMethod]
    public void Ctor_Milliseconds()
    {
        // Negative Duration
        Assert.ThrowsException<ArgumentNegativeException>(() => new DateSpan(2020, 1, 2, 10, 25, 10, 36, -TimeSpan.FromHours(4)));

        DateSpan actual;
        DateTime expectedStart = new DateTime(2020, 1, 2, 10, 25, 10, 36);

        // Empty
        actual = new DateSpan(2020, 1, 2, 10, 25, 10, 36, TimeSpan.Zero);
        Assert.AreEqual(DateTime.MinValue       , actual.Start   );
        Assert.AreEqual(DateTime.MinValue       , actual.End     );
        Assert.AreEqual(TimeSpan.Zero           , actual.Duration);
        Assert.AreEqual(DateTimeKind.Unspecified, actual.Kind    );

        // Normal Use-Case
        actual = new DateSpan(2020, 1, 2, 10, 25, 10, 36, TimeSpan.FromHours(4));
        Assert.AreEqual(expectedStart            , actual.Start   );
        Assert.AreEqual(expectedStart.AddHours(4), actual.End     );
        Assert.AreEqual(TimeSpan.FromHours(4)    , actual.Duration);
        Assert.AreEqual(DateTimeKind.Unspecified , actual.Kind    );
    }

    [TestMethod]
    public void Ctor_Milliseconds_Calendar()
    {
        // Null Calendar
        Assert.ThrowsException<ArgumentNullException>(() => new DateSpan(5782, 4, 29, 10, 25, 10, 36, null!, TimeSpan.FromHours(4)));

        // Negative Duration
        Assert.ThrowsException<ArgumentNegativeException>(() => new DateSpan(5782, 4, 29, 10, 25, 10, 36, new HebrewCalendar(), -TimeSpan.FromHours(4)));

        DateSpan actual;
        DateTime expectedStart = new DateTime(5782, 4, 29, 10, 25, 10, 36, new HebrewCalendar());

        // Empty
        actual = new DateSpan(5782, 4, 29, 10, 25, 10, 36, new HebrewCalendar(), TimeSpan.Zero);
        Assert.AreEqual(DateTime.MinValue       , actual.Start   );
        Assert.AreEqual(DateTime.MinValue       , actual.End     );
        Assert.AreEqual(TimeSpan.Zero           , actual.Duration);
        Assert.AreEqual(DateTimeKind.Unspecified, actual.Kind    );

        // Normal Use-Case
        actual = new DateSpan(5782, 4, 29, 10, 25, 10, 36, new HebrewCalendar(), TimeSpan.FromHours(4));
        Assert.AreEqual(expectedStart            , actual.Start   );
        Assert.AreEqual(expectedStart.AddHours(4), actual.End     );
        Assert.AreEqual(TimeSpan.FromHours(4)    , actual.Duration);
        Assert.AreEqual(DateTimeKind.Unspecified , actual.Kind    );
    }

    [TestMethod]
    public void Ctor_Milliseconds_Kind()
    {
        // Invalid Kind
        Assert.ThrowsException<ArgumentException>(() => new DateSpan(2020, 1, 2, 10, 25, 10, 36, (DateTimeKind)100, TimeSpan.FromHours(4)));

        // Negative Duration
        Assert.ThrowsException<ArgumentNegativeException>(() => new DateSpan(2020, 1, 2, 10, 25, 10, 36, DateTimeKind.Local, -TimeSpan.FromHours(4)));

        DateSpan actual;
        DateTime expectedStart = new DateTime(2020, 1, 2, 10, 25, 10, 36, DateTimeKind.Utc);

        // Empty
        actual = new DateSpan(2020, 1, 2, 10, 25, 10, 36, DateTimeKind.Utc, TimeSpan.Zero);
        Assert.AreEqual(DateTime.MinValue       , actual.Start   );
        Assert.AreEqual(DateTime.MinValue       , actual.End     );
        Assert.AreEqual(TimeSpan.Zero           , actual.Duration);
        Assert.AreEqual(DateTimeKind.Unspecified, actual.Kind    );

        // Normal Use-Case
        actual = new DateSpan(2020, 1, 2, 10, 25, 10, 36, DateTimeKind.Utc, TimeSpan.FromHours(4));
        Assert.AreEqual(expectedStart            , actual.Start   );
        Assert.AreEqual(expectedStart.AddHours(4), actual.End     );
        Assert.AreEqual(TimeSpan.FromHours(4)    , actual.Duration);
        Assert.AreEqual(DateTimeKind.Utc         , actual.Kind    );
    }

    [TestMethod]
    public void Ctor_Milliseconds_Calendar_Kind()
    {
        // Null Calendar
        Assert.ThrowsException<ArgumentNullException>(() => new DateSpan(5782, 4, 29, 10, 25, 10, 36, null!, DateTimeKind.Utc, TimeSpan.FromHours(4)));

        // Invalid Kind
        Assert.ThrowsException<ArgumentException>(() => new DateSpan(5782, 4, 29, 10, 25, 10, 36, new HebrewCalendar(), (DateTimeKind)100, TimeSpan.FromHours(4)));

        // Negative Duration
        Assert.ThrowsException<ArgumentNegativeException>(() => new DateSpan(5782, 4, 29, 10, 25, 10, 36, new HebrewCalendar(), DateTimeKind.Utc, - TimeSpan.FromHours(4)));

        DateSpan actual;
        DateTime expectedStart = new DateTime(5782, 4, 29, 10, 25, 10, 36, new HebrewCalendar(), DateTimeKind.Utc);

        // Empty
        actual = new DateSpan(5782, 4, 29, 10, 25, 10, 36, new HebrewCalendar(), DateTimeKind.Utc, TimeSpan.Zero);
        Assert.AreEqual(DateTime.MinValue       , actual.Start   );
        Assert.AreEqual(DateTime.MinValue       , actual.End     );
        Assert.AreEqual(TimeSpan.Zero           , actual.Duration);
        Assert.AreEqual(DateTimeKind.Unspecified, actual.Kind    );

        // Normal Use-Case
        actual = new DateSpan(5782, 4, 29, 10, 25, 10, 36, new HebrewCalendar(), DateTimeKind.Utc, TimeSpan.FromHours(4));
        Assert.AreEqual(expectedStart            , actual.Start   );
        Assert.AreEqual(expectedStart.AddHours(4), actual.End     );
        Assert.AreEqual(TimeSpan.FromHours(4)    , actual.Duration);
        Assert.AreEqual(DateTimeKind.Utc         , actual.Kind    );
    }

    [TestMethod]
    public void Ctor_DateTime_DateTime()
    {
        // DateTimeKind Mismatch
        Assert.ThrowsException<ArgumentException>(
            () => new DateSpan(
                new DateTime( 12345, DateTimeKind.Unspecified),
                new DateTime(678910, DateTimeKind.Local)));

        // Wrong Argument Order
        Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => new DateSpan(new DateTime(678910), new DateTime(12345)));

        DateSpan actual;
        DateTime expectedStart = new DateTime(2020, 05, 15, 1, 2, 3, DateTimeKind.Utc);

        // Empty
        actual = new DateSpan(expectedStart, expectedStart);
        Assert.AreEqual(DateTime.MinValue       , actual.Start   );
        Assert.AreEqual(DateTime.MinValue       , actual.End     );
        Assert.AreEqual(TimeSpan.Zero           , actual.Duration);
        Assert.AreEqual(DateTimeKind.Unspecified, actual.Kind    );

        // Normal Use-Case
        actual = new DateSpan(expectedStart, expectedStart.AddDays(5));
        Assert.AreEqual(expectedStart           , actual.Start   );
        Assert.AreEqual(expectedStart.AddDays(5), actual.End     );
        Assert.AreEqual(TimeSpan.FromDays(5)    , actual.Duration);
        Assert.AreEqual(DateTimeKind.Utc        , actual.Kind    );
    }

    [TestMethod]
    public void Ctor_DateTime_TimeSpan()
    {
        // Negative Duration
        Assert.ThrowsException<ArgumentNegativeException>(
            () => new DateSpan(DateTime.Now, TimeSpan.FromSeconds(-30)));

        // Duration Too Long
        Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => new DateSpan(DateTime.MaxValue.AddSeconds(-3), TimeSpan.FromSeconds(4)));

        DateSpan actual;
        DateTime expectedStart = new DateTime(2020, 05, 15, 1, 2, 3, DateTimeKind.Utc);

        // Empty
        actual = new DateSpan(expectedStart, TimeSpan.Zero);
        Assert.AreEqual(DateTime.MinValue       , actual.Start   );
        Assert.AreEqual(DateTime.MinValue       , actual.End     );
        Assert.AreEqual(TimeSpan.Zero           , actual.Duration);
        Assert.AreEqual(DateTimeKind.Unspecified, actual.Kind    );

        // Normal Use-Case
        actual = new DateSpan(expectedStart, TimeSpan.FromDays(5));
        Assert.AreEqual(expectedStart           , actual.Start   );
        Assert.AreEqual(expectedStart.AddDays(5), actual.End     );
        Assert.AreEqual(TimeSpan.FromDays(5)    , actual.Duration);
        Assert.AreEqual(DateTimeKind.Utc        , actual.Kind    );
    }

    [TestMethod]
    public void Ctor_DateTime_Ticks()
    {
        // Negative Duration
        Assert.ThrowsException<ArgumentNegativeException>(() => new DateSpan(DateTime.Now, -1));

        // Duration Too Long
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new DateSpan(DateTime.MaxValue, 2));

        DateSpan actual;
        DateTime expectedStart = new DateTime(2020, 05, 15, 1, 2, 3, DateTimeKind.Utc);

        // Empty
        actual = new DateSpan(expectedStart, 0L);
        Assert.AreEqual(DateTime.MinValue       , actual.Start   );
        Assert.AreEqual(DateTime.MinValue       , actual.End     );
        Assert.AreEqual(TimeSpan.Zero           , actual.Duration);
        Assert.AreEqual(DateTimeKind.Unspecified, actual.Kind    );

        // Normal Use-Case
        actual = new DateSpan(expectedStart, 10000);
        Assert.AreEqual(expectedStart                , actual.Start   );
        Assert.AreEqual(expectedStart.AddTicks(10000), actual.End     );
        Assert.AreEqual(TimeSpan.FromTicks(10000)    , actual.Duration);
        Assert.AreEqual(DateTimeKind.Utc             , actual.Kind    );
    }

[TestMethod]
    public void CompareTo_Object()
    {
        // Wrong Type
        Assert.ThrowsException<ArgumentException>(() => DateSpan.Empty.CompareTo("hello world"));

        Assert.AreEqual(1, DateSpan.Empty.CompareTo(null!));
        CompareTo((d1, d2) => d1.CompareTo((object)d2));
    }

    [TestMethod]
    public void CompareTo_DateSpan()
        => CompareTo((d1, d2) => d1.CompareTo(d2));

    private static void CompareTo(Func<DateSpan, DateSpan, int> compareTo)
        => CompareTo(compareTo, -1, 0, 1);

    private static void CompareTo<T>(
        Func<DateSpan, DateSpan, T> compareTo,
        T lessThan,
        T equal,
        T greaterThan)
    {
        // In the following examples, note that the Kind doesn't make a difference
        DateTime utcNow = DateTime.UtcNow;
        DateTime localNow = new DateTime(utcNow.Ticks, DateTimeKind.Local);

        // Equal
        Assert.AreEqual(equal, compareTo(new DateSpan(utcNow, TimeSpan.FromDays(3)), new DateSpan(localNow, TimeSpan.FromDays(3))));

        // Less Than
        Assert.AreEqual(lessThan, compareTo(new DateSpan(utcNow, 100), new DateSpan(localNow.AddTicks(10), 100)));
        Assert.AreEqual(lessThan, compareTo(new DateSpan(utcNow, 100), new DateSpan(localNow             , 150)));

        // Greater Than
        Assert.AreEqual(greaterThan, compareTo(new DateSpan(utcNow.AddTicks(3), 100), new DateSpan(localNow, 100)));
        Assert.AreEqual(greaterThan, compareTo(new DateSpan(utcNow            , 125), new DateSpan(localNow, 100)));
    }

    [TestMethod]
    public void Contains_DateTime()
    {
        DateSpan interval = DateSpan.FromMonth(1000, 10);

        // False
        Assert.IsFalse(DateSpan.Empty.Contains(DateTime.UtcNow));
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
    public void Contains_DateSpan()
    {
        // Empty Instance
        Assert.IsTrue (DateSpan.Empty.Contains(DateSpan.Empty));
        Assert.IsFalse(DateSpan.Empty.Contains(DateSpan.FromYear(2022)));

        // Non-Empty Instance
        DateSpan interval = DateSpan.FromMonth(2000, 06);

        // Empty Argument
        Assert.IsTrue(interval.Contains(DateSpan.Empty));

        // Moving around the interval
        Assert.IsFalse(interval.Contains(new DateSpan(interval.Start.AddYears(-1), interval.Duration)));
        Assert.IsFalse(interval.Contains(new DateSpan(interval.Start.AddDays(-15), interval.Duration)));
        Assert.IsTrue (interval.Contains(interval));
        Assert.IsFalse(interval.Contains(new DateSpan(interval.Start.AddDays(15), interval.Duration)));
        Assert.IsFalse(interval.Contains(new DateSpan(interval.Start.AddYears(1), interval.Duration)));

        // Smaller interval
        Assert.IsTrue(interval.Contains(DateSpan.FromDay(2000, 06, 15, DateTimeKind.Local)));

        // Larger interval
        Assert.IsFalse(interval.Contains(DateSpan.FromYear(2000, DateTimeKind.Utc)));
        Assert.IsFalse(interval.Contains(new DateSpan(interval.Start              , interval.End.AddMonths( 1))));
        Assert.IsFalse(interval.Contains(new DateSpan(interval.Start.AddMonths(-1), interval.End              )));
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
        Func<DateSpan, DateSpan, T> getActual,
        Func<DateSpan, T> getExpected)
    {
        // Empty Input
        AssertSymmetric(default, default, DateSpan.FromYear(4000));
        Assert.AreEqual(getExpected(default), getActual(default, default));

        // Empty Results
        AssertSymmetric(default, DateSpan.FromMonth(1991, 1), DateSpan.FromMonth(2000, 1));
        Assert.AreEqual(getExpected(default), getActual(DateSpan.FromDay(10, 9, 8), DateSpan.FromDay(10, 9, 9)));
        Assert.AreEqual(getExpected(default), getActual(DateSpan.FromDay(10, 9, 8), DateSpan.FromDay(10, 9, 7)));

        // Single DateTime
        DateTime utcNow = DateTime.UtcNow;
        AssertSymmetric(
            DateSpan.FromDateTime(utcNow),
            DateSpan.FromMonth(utcNow.Year, utcNow.Month),
            DateSpan.FromDateTime(utcNow));

        // Subset
        AssertSymmetric(DateSpan.FromMonth(1234, 5), DateSpan.FromMonth(1234, 5), DateSpan.FromYear(1234));

        // Same Start
        AssertSymmetric(
            DateSpan.FromDay(2010, 11, 12),
            DateSpan.FromDay(2010, 11, 12),
            new DateSpan(new DateTime(2010, 11, 12), TimeSpan.FromHours(36)));

        // Complex Intersection
        DateSpan d1       = DateSpan.FromDay(1456, 7, 8);
        DateSpan d2       = new DateSpan(new DateTime(1456, 7, 8, 9, 0, 0), TimeSpan.FromDays(2));
        DateSpan expected = new DateSpan(new DateTime(1456, 7, 8, 9, 0, 0), TimeSpan.FromHours(15));
        AssertSymmetric(expected, d1, d2);

        void AssertSymmetric(DateSpan expected, DateSpan first, DateSpan second)
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
    public void CastFromDateTimeOperator()
    {
        DateSpan actual;

        DateTime utcNow = DateTime.UtcNow;
        actual = (DateSpan)utcNow;
        Assert.AreEqual(utcNow               , actual.Start   );
        Assert.AreEqual(utcNow.AddTicks(1)   , actual.End     );
        Assert.AreEqual(TimeSpan.FromTicks(1), actual.Duration);
        Assert.AreEqual(DateTimeKind.Utc     , actual.Kind    );

        DateTime now = DateTime.Now;
        actual = (DateSpan)now;
        Assert.AreEqual(now                  , actual.Start   );
        Assert.AreEqual(now.AddTicks(1)      , actual.End     );
        Assert.AreEqual(TimeSpan.FromTicks(1), actual.Duration);
        Assert.AreEqual(DateTimeKind.Local   , actual.Kind    );
    }

    [TestMethod]
    public void FromDateTime()
    {
        DateSpan actual;

        DateTime utcNow = DateTime.UtcNow;
        actual = DateSpan.FromDateTime(utcNow);
        Assert.AreEqual(utcNow               , actual.Start   );
        Assert.AreEqual(utcNow.AddTicks(1)   , actual.End     );
        Assert.AreEqual(TimeSpan.FromTicks(1), actual.Duration);
        Assert.AreEqual(DateTimeKind.Utc     , actual.Kind    );

        DateTime now = DateTime.Now;
        actual = DateSpan.FromDateTime(now);
        Assert.AreEqual(now                  , actual.Start   );
        Assert.AreEqual(now.AddTicks(1)      , actual.End     );
        Assert.AreEqual(TimeSpan.FromTicks(1), actual.Duration);
        Assert.AreEqual(DateTimeKind.Local   , actual.Kind    );
    }

    [TestMethod]
    public void FromDay()
        => FromDay((y, m, d) => DateSpan.FromDay(y, m, d));

    [TestMethod]
    public void FromDay_Kind()
    {
        Assert.ThrowsException<ArgumentException>(() => DateSpan.FromDay(1981, 02, 03, (DateTimeKind)47));

        FromDay((y, m, d) => DateSpan.FromDay(y, m, d, DateTimeKind.Local), kind: DateTimeKind.Local);
    }

    [TestMethod]
    public void FromDay_Calendar_Kind()
    {
        Assert.ThrowsException<ArgumentNullException>(() => DateSpan.FromDay(1981, 02, 03, null!, (DateTimeKind)47));
        Assert.ThrowsException<ArgumentException>(() => DateSpan.FromDay(1981, 02, 03, new HebrewCalendar(), (DateTimeKind)47));

        FromDay((y, m, d) => DateSpan.FromDay(y, m, d, new HebrewCalendar(), DateTimeKind.Local), 5782, 4, 10, new HebrewCalendar(), DateTimeKind.Local);
    }

    private static void FromDay(
        Func<int, int, int, DateSpan> fromDay,
        int year = 1981,
        int month = 02,
        int day = 03,
        Calendar? calendar = null,
        DateTimeKind kind = DateTimeKind.Unspecified)
    {
        DateSpan actual = fromDay(year, month, day);
        DateTime expectedStart = new DateTime(year, month, day, 0, 0, 0, 0, calendar ?? new GregorianCalendar(), kind);

        Assert.AreEqual(expectedStart           , actual.Start);
        Assert.AreEqual(expectedStart.AddDays(1), actual.End  );
        Assert.AreEqual(kind                    , actual.Kind );
    }

    [TestMethod]
    public void FromMonth()
        => FromMonth((y, m) => DateSpan.FromMonth(y, m));

    [TestMethod]
    public void FromMonth_Kind()
    {
        Assert.ThrowsException<ArgumentException>(() => DateSpan.FromMonth(2022, 01, (DateTimeKind)47));

        FromMonth((y, m) => DateSpan.FromMonth(y, m, DateTimeKind.Local), kind: DateTimeKind.Local);
    }

    [TestMethod]
    public void FromMonth_Calendar_Kind()
    {
        Assert.ThrowsException<ArgumentNullException>(() => DateSpan.FromMonth(2022, 03, null!, DateTimeKind.Local));
        Assert.ThrowsException<ArgumentException>(() => DateSpan.FromMonth(2022, 03, new HebrewCalendar(), (DateTimeKind)47));

        FromMonth((y, m) => DateSpan.FromMonth(y, m, new HebrewCalendar(), DateTimeKind.Local), 5757, 5, new HebrewCalendar(), DateTimeKind.Local);
    }

    private static void FromMonth(
        Func<int, int, DateSpan> fromMonth,
        int year = 2022,
        int month = 03,
        Calendar? calendar = null,
        DateTimeKind kind = DateTimeKind.Unspecified)
    {
        DateSpan actual = fromMonth(year, month);
        DateTime expectedStart = new DateTime(year, month, 01, 0, 0, 0, 0, calendar ?? new GregorianCalendar(), kind);

        Assert.AreEqual(expectedStart             , actual.Start);
        Assert.AreEqual(expectedStart.AddMonths(1), actual.End  );
        Assert.AreEqual(kind                      , actual.Kind );
    }

    [TestMethod]
    public void FromYear()
        => FromYear(y => DateSpan.FromYear(y));

    [TestMethod]
    public void FromYear_Kind()
    {
        Assert.ThrowsException<ArgumentException>(() => DateSpan.FromYear(2010, (DateTimeKind)47));

        FromYear(y => DateSpan.FromYear(y, DateTimeKind.Local), kind: DateTimeKind.Local);
    }

    [TestMethod]
    public void FromYear_Calendar_Kind()
    {
        Assert.ThrowsException<ArgumentNullException>(() => DateSpan.FromYear(2010, null!, DateTimeKind.Local));
        Assert.ThrowsException<ArgumentException>(() => DateSpan.FromYear(2010, new HebrewCalendar(), (DateTimeKind)47));

        FromYear(y => DateSpan.FromYear(y, new HebrewCalendar(), DateTimeKind.Local), 5757, new HebrewCalendar(), DateTimeKind.Local);
    }

    private static void FromYear(Func<int, DateSpan> fromYear, int year = 2010, Calendar? calendar = null, DateTimeKind kind = DateTimeKind.Unspecified)
    {
        DateSpan actual = fromYear(year);
        DateTime expectedStart = new DateTime(year, 01, 01, 0, 0, 0, 0, calendar ?? new GregorianCalendar(), kind);

        Assert.AreEqual(expectedStart            , actual.Start);
        Assert.AreEqual(expectedStart.AddYears(1), actual.End  );
        Assert.AreEqual(kind                     , actual.Kind );
    }

    [TestMethod]
    public void SpecifyKind()
    {
        Assert.ThrowsException<ArgumentException>(() => DateSpan.SpecifyKind(DateSpan.Empty, (DateTimeKind)47));

        DateTime utcNow = DateTime.UtcNow;
        DateSpan before = DateSpan.FromDateTime(utcNow);
        DateSpan after = DateSpan.SpecifyKind(before, DateTimeKind.Local);

        Assert.AreEqual(before.Start      , after.Start   );
        Assert.AreEqual(before.End        , after.End     );
        Assert.AreEqual(before.Duration   , after.Duration);
        Assert.AreEqual(DateTimeKind.Local, after.Kind    );
    }
}
