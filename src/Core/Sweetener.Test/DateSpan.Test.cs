// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Test;

[TestClass]
public class DateSpanTest
{
    [TestMethod]
    public void MaxValue()
    {
        Assert.AreEqual(DateTime.MinValue                    , DateSpan.MaxValue.Start   );
        Assert.AreEqual(DateTime.MaxValue                    , DateSpan.MaxValue.End     );
        Assert.AreEqual(DateTime.MaxValue - DateTime.MinValue, DateSpan.MaxValue.Duration);
    }

    [TestMethod]
    public void MinValue()
    {
        Assert.AreEqual(DateTime.MinValue, DateSpan.MinValue.Start   );
        Assert.AreEqual(DateTime.MinValue, DateSpan.MinValue.End     );
        Assert.AreEqual(TimeSpan.Zero    , DateSpan.MinValue.Duration);

        Assert.AreEqual(default, DateSpan.MinValue);
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
        Assert.ThrowsException<ArgumentException>(() => DateSpan.MinValue.CompareTo("hello world"));

        Assert.AreEqual(1, DateSpan.MinValue.CompareTo(null!));
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
    public void Contains_DateSpan()
    {
        // Empty Instance
        Assert.IsTrue (DateSpan.MinValue.Contains(DateSpan.MinValue));
        Assert.IsFalse(DateSpan.MinValue.Contains(DateSpan.FromYear(2022)));

        // Non-Empty Instance
        DateSpan interval = DateSpan.FromMonth(2000, 06);

        // Empty Argument
        Assert.IsTrue(interval.Contains(DateSpan.MinValue));

        // Moving around the interval
        Assert.IsFalse(interval.Contains(interval.ShiftYears(-1)));
        Assert.IsFalse(interval.Contains(interval.ShiftDays(-15)));
        Assert.IsTrue (interval.Contains(interval));
        Assert.IsFalse(interval.Contains(interval.ShiftDays(15)));
        Assert.IsFalse(interval.Contains(interval.ShiftYears(1)));

        // Smaller interval
        Assert.IsTrue(interval.Contains(DateSpan.FromDay(2000, 06, 15, DateTimeKind.Local)));

        // Larger interval
        Assert.IsFalse(interval.Contains(DateSpan.FromYear(2000, DateTimeKind.Utc)));
        Assert.IsFalse(interval.Contains(interval.AddMonths( 1, EndpointKind.End  )));
        Assert.IsFalse(interval.Contains(interval.AddMonths(-1, EndpointKind.Start)));
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
    public void Add()
        => Add((d, v, k) => d.Add(TimeSpan.FromHours(3) * v, k), (s, v) => s.AddHours(3 * v));

    [TestMethod]
    public void AddDays()
        => Add((d, f, k) => d.AddDays(f, k), TimeSpan.FromDays(1));

    [TestMethod]
    public void AddHours()
        => Add((d, f, k) => d.AddHours(f, k), TimeSpan.FromHours(1));

    [TestMethod]
    public void AddMilliseconds() // Avoid issues rounding in DateTime.AddMilliseconds(double)
        => Add((d, f, k) => d.AddMilliseconds(100 * f, k), TimeSpan.FromMilliseconds(100));

    [TestMethod]
    public void AddMinutes()
        => Add((d, f, k) => d.AddMinutes(f, k), TimeSpan.FromMinutes(1));

    [TestMethod]
    public void AddMonths()
        => Add((d, v, k) => d.AddMonths(v, k), (s, v) => s.AddMonths(v));

    [TestMethod]
    public void AddSeconds()
        => Add((d, f, k) => d.AddSeconds(f, k), TimeSpan.FromSeconds(1));

    [TestMethod]
    public void AddTicks()
        => Add((d, v, k) => d.AddTicks(v, k), (s, v) => s.AddTicks(v));

    [TestMethod]
    public void AddYears()
        => Add((d, v, k) => d.AddYears(v, k), (s, v) => s.AddYears(v));

    private static void Add(Func<DateSpan, double, EndpointKind, DateSpan> addFunc, TimeSpan unit)
    {
        // Negative Infinity
        Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => addFunc(new DateSpan(DateTime.Now, 1000), double.NegativeInfinity, EndpointKind.Start));
        Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => addFunc(new DateSpan(DateTime.Now, 1000), double.NegativeInfinity, EndpointKind.End));

        // Positive Infinity
        Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => addFunc(new DateSpan(DateTime.Now, 1000), double.PositiveInfinity, EndpointKind.Start));
        Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => addFunc(new DateSpan(DateTime.Now, 1000), double.PositiveInfinity, EndpointKind.End));

        // Too Small
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => addFunc(DateSpan.MinValue, -12.3, EndpointKind.Start));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => addFunc(DateSpan.MinValue, -12.3, EndpointKind.End  ));
        Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => addFunc(new DateSpan(new DateTime(1990, 09, 09), 1L), -10, EndpointKind.End));

        // Too Large
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => addFunc(new DateSpan(DateTime.MaxValue, 0L), 5.9, EndpointKind.Start));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => addFunc(DateSpan.MaxValue, 5.9, EndpointKind.End));
        Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => addFunc(new DateSpan(new DateTime(1990, 09, 09), 1L), 10, EndpointKind.Start));

        // Invalid Endpoint
        Assert.ThrowsException<ArgumentException>(() => addFunc(default, 10.7, (EndpointKind)42));

        // Normal Use-Cases
        TimeSpan difference;
        DateSpan after;
        DateSpan before = new DateSpan(
            new DateTime(1991, 10, 12, 13, 14, 15, DateTimeKind.Local),
            unit * 100);

        #region Start

        // Increase
        after = addFunc(before, 2.5, EndpointKind.Start);
        difference = unit * 2.5;
        Assert.AreEqual(before.Start    + difference, after.Start   );
        Assert.AreEqual(before.End                  , after.End     );
        Assert.AreEqual(before.Duration - difference, after.Duration);
        Assert.AreEqual(before.Kind                 , after.Kind    );

        // Decrease
        after = addFunc(before, -37.1, EndpointKind.Start);
        difference = unit * -37.1;
        Assert.AreEqual(before.Start    + difference, after.Start   );
        Assert.AreEqual(before.End                  , after.End     );
        Assert.AreEqual(before.Duration - difference, after.Duration);
        Assert.AreEqual(before.Kind                 , after.Kind    );

        #endregion

        #region End

        // Increase
        after = addFunc(before, 2.5, EndpointKind.End);
        difference = unit * 2.5;
        Assert.AreEqual(before.Start                , after.Start   );
        Assert.AreEqual(before.End      + difference, after.End     );
        Assert.AreEqual(before.Duration + difference, after.Duration);
        Assert.AreEqual(before.Kind                 , after.Kind    );

        // Decrease
        after = addFunc(before, -37.1, EndpointKind.End);
        difference = unit * -37.1;
        Assert.AreEqual(before.Start                , after.Start   );
        Assert.AreEqual(before.End      + difference, after.End     );
        Assert.AreEqual(before.Duration + difference, after.Duration);
        Assert.AreEqual(before.Kind                 , after.Kind    );

        #endregion
    }

    private static void Add(
        Func<DateSpan, int, EndpointKind, DateSpan> addFunc,
        Func<DateTime, int, DateTime> addTimeFunc)
    {
        // Too Small
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => addFunc(DateSpan.MinValue, -12, EndpointKind.Start));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => addFunc(DateSpan.MinValue, -12, EndpointKind.End  ));
        Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => addFunc(new DateSpan(new DateTime(1990, 09, 09), 1L), -10, EndpointKind.End));

        // Too Large
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => addFunc(new DateSpan(DateTime.MaxValue, 0L), 5, EndpointKind.Start));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => addFunc(DateSpan.MaxValue, 5, EndpointKind.End));
        Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => addFunc(new DateSpan(new DateTime(1990, 09, 09), 1L), 10, EndpointKind.Start));

        // Invalid Endpoint
        Assert.ThrowsException<ArgumentException>(() => addFunc(default, 10, (EndpointKind)42));

        // Normal Use-Cases
        DateTime newTime;
        DateTime start = new DateTime(1991, 10, 12, 13, 14, 15, DateTimeKind.Local);

        DateSpan after;
        DateSpan before = new DateSpan(start, addTimeFunc(start, 50));

        #region End

        // Increase
        after = addFunc(before, 2, EndpointKind.Start);
        newTime = addTimeFunc(before.Start, 2);
        Assert.AreEqual(newTime             , after.Start   );
        Assert.AreEqual(before.End          , after.End     );
        Assert.AreEqual(before.End - newTime, after.Duration);
        Assert.AreEqual(before.Kind         , after.Kind    );

        // Decrease
        after = addFunc(before, -37, EndpointKind.Start);
        newTime = addTimeFunc(before.Start, -37);
        Assert.AreEqual(newTime             , after.Start   );
        Assert.AreEqual(before.End          , after.End     );
        Assert.AreEqual(before.End - newTime, after.Duration);
        Assert.AreEqual(before.Kind         , after.Kind    );

        #endregion

        #region End

        // Increase
        after = addFunc(before, 2, EndpointKind.End);
        newTime = addTimeFunc(before.End, 2);
        Assert.AreEqual(before.Start          , after.Start   );
        Assert.AreEqual(newTime               , after.End     );
        Assert.AreEqual(newTime - before.Start, after.Duration);
        Assert.AreEqual(before.Kind           , after.Kind    );

        // Decrease
        after = addFunc(before, -37, EndpointKind.End);
        newTime = addTimeFunc(before.End, -37);
        Assert.AreEqual(before.Start          , after.Start   );
        Assert.AreEqual(newTime               , after.End     );
        Assert.AreEqual(newTime - before.Start, after.Duration);
        Assert.AreEqual(before.Kind           , after.Kind    );

        #endregion
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
    public void Shift()
        => Shift((d, v) => d.Shift(TimeSpan.FromMinutes(150) * v), (s, v) => s.AddMinutes(150 * v));

    [TestMethod]
    public void ShiftDays()
        => Shift((d, f) => d.ShiftDays(f), TimeSpan.FromDays(1));

    [TestMethod]
    public void ShiftHours()
        => Shift((d, f) => d.ShiftHours(f), TimeSpan.FromHours(1));

    [TestMethod]
    public void ShiftMilliseconds() // Avoid issues rounding in DateTime.AddMilliseconds(double)
        => Shift((d, f) => d.ShiftMilliseconds(100 * f), TimeSpan.FromMilliseconds(100));

    [TestMethod]
    public void ShiftMinutes()
        => Shift((d, f) => d.ShiftMinutes(f), TimeSpan.FromMinutes(1));

    [TestMethod]
    public void ShiftMonths()
        => Shift((d, v) => d.ShiftMonths(v), (s, v) => s.AddMonths(v));

    [TestMethod]
    public void ShiftSeconds()
        => Shift((d, f) => d.ShiftSeconds(f), TimeSpan.FromSeconds(1));

    [TestMethod]
    public void ShiftTicks()
        => Shift((d, v) => d.ShiftTicks(v), (s, v) => s.AddTicks(v));

    [TestMethod]
    public void ShiftYears()
        => Shift((d, v) => d.ShiftYears(v), (s, v) => s.AddYears(v));

    private static void Shift(Func<DateSpan, double, DateSpan> extend, TimeSpan unit)
    {
        // Negative Infinity
        Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => extend(new DateSpan(DateTime.Now, 1000), double.NegativeInfinity));

        // Positive Infinity
        Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => extend(new DateSpan(DateTime.Now, 1000), double.PositiveInfinity));

        // Too Small
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => extend(DateSpan.MinValue, -12));

        // Too Large
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => extend(DateSpan.MaxValue, 5));

        // Normal Use-Cases
        TimeSpan difference;
        DateSpan after;
        DateSpan before = new DateSpan(
            new DateTime(1991, 10, 12, 13, 14, 15, DateTimeKind.Local),
            unit * 100);

        // Increase
        after = extend(before, 10.6);
        difference = unit * 10.6;
        Assert.AreEqual(before.Start                   + difference, after.Start   );
        Assert.AreEqual(before.Start + before.Duration + difference, after.End     );
        Assert.AreEqual(before.Duration                            , after.Duration);
        Assert.AreEqual(before.Kind                                , after.Kind    );

        // Decrease
        after = extend(before, -8.2);
        difference = unit * -8.2;
        Assert.AreEqual(before.Start                   + difference, after.Start   );
        Assert.AreEqual(before.Start + before.Duration + difference, after.End     );
        Assert.AreEqual(before.Duration                            , after.Duration);
        Assert.AreEqual(before.Kind                                , after.Kind    );
    }

    private static void Shift(Func<DateSpan, int, DateSpan> extend, Func<DateTime, int, DateTime> addFunc)
    {
        // Too Small
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => extend(DateSpan.MinValue, -12));

        // Too Large
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => extend(DateSpan.MaxValue, 5));

        // Normal Use-Cases
        DateTime newStart;
        DateTime start = new DateTime(1991, 10, 12, 13, 14, 15, DateTimeKind.Local);

        DateSpan after;
        DateSpan before = new DateSpan(start, addFunc(start, 50));

        // Increase
        after = extend(before, 10);
        newStart = addFunc(before.Start, 10);
        Assert.AreEqual(newStart                     , after.Start   );
        Assert.AreEqual(newStart.Add(before.Duration), after.End     );
        Assert.AreEqual(before.Duration              , after.Duration);
        Assert.AreEqual(before.Kind                  , after.Kind    );

        // Decrease
        after = extend(before, -8);
        newStart = addFunc(before.Start, -8);
        Assert.AreEqual(newStart                     , after.Start   );
        Assert.AreEqual(newStart.Add(before.Duration), after.End     );
        Assert.AreEqual(before.Duration              , after.Duration);
        Assert.AreEqual(before.Kind                  , after.Kind    );
    }

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
    {
        DateSpan actual = DateSpan.FromDay(1981, 02, 03);
        Assert.AreEqual(new DateTime(1981, 02, 03), actual.Start   );
        Assert.AreEqual(new DateTime(1981, 02, 04), actual.End     );
        Assert.AreEqual(TimeSpan.FromDays(1)      , actual.Duration);
        Assert.AreEqual(DateTimeKind.Unspecified  , actual.Kind    );
    }

    [TestMethod]
    public void FromDay_Kind()
    {
        Assert.ThrowsException<ArgumentException>(() => DateSpan.FromDay(1981, 02, 03, (DateTimeKind)47));

        DateSpan actual = DateSpan.FromDay(1981, 02, 03, DateTimeKind.Local);
        Assert.AreEqual(new DateTime(1981, 02, 03), actual.Start   );
        Assert.AreEqual(new DateTime(1981, 02, 04), actual.End     );
        Assert.AreEqual(TimeSpan.FromDays(1)      , actual.Duration);
        Assert.AreEqual(DateTimeKind.Local        , actual.Kind    );
    }

    [TestMethod]
    public void FromMonth()
    {
        DateSpan actual = DateSpan.FromMonth(2022, 01);
        Assert.AreEqual(new DateTime(2022, 01, 01), actual.Start   );
        Assert.AreEqual(new DateTime(2022, 02, 01), actual.End     );
        Assert.AreEqual(TimeSpan.FromDays(31)     , actual.Duration);
        Assert.AreEqual(DateTimeKind.Unspecified  , actual.Kind    );
    }

    [TestMethod]
    public void FromMonth_Kind()
    {
        Assert.ThrowsException<ArgumentException>(() => DateSpan.FromMonth(2022, 01, (DateTimeKind)47));

        DateSpan actual = DateSpan.FromMonth(2022, 01, DateTimeKind.Local);
        Assert.AreEqual(new DateTime(2022, 01, 01), actual.Start   );
        Assert.AreEqual(new DateTime(2022, 02, 01), actual.End     );
        Assert.AreEqual(TimeSpan.FromDays(31)     , actual.Duration);
        Assert.AreEqual(DateTimeKind.Local        , actual.Kind    );
    }

    [TestMethod]
    public void FromYear()
    {
        DateSpan actual = DateSpan.FromYear(2010);
        Assert.AreEqual(new DateTime(2010, 01, 01), actual.Start   );
        Assert.AreEqual(new DateTime(2011, 01, 01), actual.End     );
        Assert.AreEqual(TimeSpan.FromDays(365)    , actual.Duration);
        Assert.AreEqual(DateTimeKind.Unspecified  , actual.Kind    );
    }

    [TestMethod]
    public void FromYear_Kind()
    {
        Assert.ThrowsException<ArgumentException>(() => DateSpan.FromYear(2010, (DateTimeKind)47));

        DateSpan actual = DateSpan.FromYear(2010, DateTimeKind.Local);
        Assert.AreEqual(new DateTime(2010, 01, 01), actual.Start   );
        Assert.AreEqual(new DateTime(2011, 01, 01), actual.End     );
        Assert.AreEqual(TimeSpan.FromDays(365)    , actual.Duration);
        Assert.AreEqual(DateTimeKind.Local        , actual.Kind    );
    }

    [TestMethod]
    public void SpecifyKind()
    {
        Assert.ThrowsException<ArgumentException>(() => DateSpan.SpecifyKind(DateSpan.MinValue, (DateTimeKind)47));

        DateTime utcNow = DateTime.UtcNow;
        DateSpan before = DateSpan.FromDateTime(utcNow);
        DateSpan after = DateSpan.SpecifyKind(before, DateTimeKind.Local);

        Assert.AreEqual(before.Start      , after.Start   );
        Assert.AreEqual(before.End        , after.End     );
        Assert.AreEqual(before.Duration   , after.Duration);
        Assert.AreEqual(DateTimeKind.Local, after.Kind    );
    }
}
