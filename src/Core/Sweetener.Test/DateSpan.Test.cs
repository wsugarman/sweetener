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
        Assert.AreEqual(expectedStart   , actual.Start   );
        Assert.AreEqual(expectedStart   , actual.End     );
        Assert.AreEqual(TimeSpan.Zero   , actual.Duration);
        Assert.AreEqual(DateTimeKind.Utc, actual.Kind    );

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
        Assert.AreEqual(expectedStart   , actual.Start   );
        Assert.AreEqual(expectedStart   , actual.End     );
        Assert.AreEqual(TimeSpan.Zero   , actual.Duration);
        Assert.AreEqual(DateTimeKind.Utc, actual.Kind    );

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
        Assert.AreEqual(expectedStart, actual.Start   );
        Assert.AreEqual(expectedStart, actual.End     );
        Assert.AreEqual(TimeSpan.Zero, actual.Duration);

        // Normal Use-Case
        actual = new DateSpan(expectedStart, 10000);
        Assert.AreEqual(expectedStart                , actual.Start   );
        Assert.AreEqual(expectedStart.AddTicks(10000), actual.End     );
        Assert.AreEqual(TimeSpan.FromTicks(10000)    , actual.Duration);
    }

    [TestMethod]
    public void Extend()
        => Extend((d, v) => d.Extend(TimeSpan.FromHours(3) * v), (s, v) => s.AddHours(3 * v));

    [TestMethod]
    public void ExtendDays()
        => Extend((d, f) => d.ExtendDays(f), TimeSpan.FromDays(1));

    [TestMethod]
    public void ExtendHours()
        => Extend((d, f) => d.ExtendHours(f), TimeSpan.FromHours(1));

    [TestMethod]
    public void ExtendMilliseconds() // Avoid issues rounding in DateTime.AddMilliseconds(double)
        => Extend((d, f) => d.ExtendMilliseconds(100 * f), TimeSpan.FromMilliseconds(100));

    [TestMethod]
    public void ExtendMinutes()
        => Extend((d, f) => d.ExtendMinutes(f), TimeSpan.FromMinutes(1));

    [TestMethod]
    public void ExtendMonths()
        => Extend((d, v) => d.ExtendMonths(v), (s, v) => s.AddMonths(v));

    [TestMethod]
    public void ExtendSeconds()
        => Extend((d, f) => d.ExtendSeconds(f), TimeSpan.FromSeconds(1));

    [TestMethod]
    public void ExtendTicks()
        => Extend((d, v) => d.ExtendTicks(v), (s, v) => s.AddTicks(v));

    [TestMethod]
    public void ExtendYears()
        => Extend((d, v) => d.ExtendYears(v), (s, v) => s.AddYears(v));

    private static void Extend(Func<DateSpan, double, DateSpan> extend, TimeSpan unit)
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
        after = extend(before, 2.5);
        difference = unit * 2.5;
        Assert.AreEqual(before.Start                , after.Start   );
        Assert.AreEqual(before.End      + difference, after.End     );
        Assert.AreEqual(before.Duration + difference, after.Duration);
        Assert.AreEqual(before.Kind                 , after.Kind    );

        // Decrease
        after = extend(before, -37.1);
        difference = unit * -37.1;
        Assert.AreEqual(before.Start                , after.Start   );
        Assert.AreEqual(before.End      + difference, after.End     );
        Assert.AreEqual(before.Duration + difference, after.Duration);
        Assert.AreEqual(before.Kind                 , after.Kind    );
    }

    private static void Extend(Func<DateSpan, int, DateSpan> extend, Func<DateTime, int, DateTime> addFunc)
    {
        // Too Small
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => extend(DateSpan.MinValue, -12));

        // Too Large
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => extend(DateSpan.MaxValue, 5));

        // Normal Use-Cases
        DateTime newEnd;
        DateTime start = new DateTime(1991, 10, 12, 13, 14, 15, DateTimeKind.Local);

        DateSpan after;
        DateSpan before = new DateSpan(start, addFunc(start, 50));

        // Increase
        after = extend(before, 2);
        newEnd = addFunc(before.End, 2);
        Assert.AreEqual(before.Start         , after.Start   );
        Assert.AreEqual(newEnd               , after.End     );
        Assert.AreEqual(newEnd - before.Start, after.Duration);
        Assert.AreEqual(before.Kind          , after.Kind    );

        // Decrease
        after = extend(before, -37);
        newEnd = addFunc(before.End, -37);
        Assert.AreEqual(before.Start         , after.Start   );
        Assert.AreEqual(newEnd               , after.End     );
        Assert.AreEqual(newEnd - before.Start, after.Duration);
        Assert.AreEqual(before.Kind          , after.Kind    );
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
        Assert.AreEqual(default, DateSpan.FromDay(10, 9, 8).IntersectWith(DateSpan.FromDay(  10,  9,  9)));
        Assert.AreEqual(default, DateSpan.FromDay(10, 9, 8).IntersectWith(DateSpan.FromDay(  10,  9,  7)));

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
    public void EqualsOperator()
        => Equals((d1, d2) => d1 == d2);

    [TestMethod]
    public void NotEqualsOperator()
        => Equals((d1, d2) => !(d1 != d2));

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
}
