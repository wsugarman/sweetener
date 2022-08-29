// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Sweetener;

/// <summary>
/// Represents an interval between two instants in time, each expressed as a date and time of day.
/// </summary>
[Serializable]
internal readonly struct DateSpan : IComparable, IComparable<DateSpan>, IEquatable<DateSpan> //, IFormattable
{
    /// <summary>
    /// Gets the instant in time that starts the interval represented by this instance.
    /// </summary>
    /// <value>The <see cref="DateTime"/> that indicates the inclusive start of this interval.</value>
    public DateTime Start { get; }

    /// <summary>
    /// Gets the instant in time that ends the interval represented by this instance.
    /// </summary>
    /// <value>The <see cref="DateTime"/> that indicates the exclusive end of this interval.</value>
    public DateTime End => Start + Duration;

    /// <summary>
    /// Gets the length of time represented by this instance irrespective of when it starts and ends.
    /// This value is always non-negative.
    /// </summary>
    /// <value>The <see cref="TimeSpan"/> that indicates the length of time.</value>
    public TimeSpan Duration { get; }

    /// <summary>
    /// Gets a value that indicates whether the interval represented by this instance is based on local time,
    /// Coordinated Universal Time (UTC), or neither.
    /// </summary>
    /// <value>
    /// One of the enumeration values that indicates what the current time represents.
    /// The default is <see cref="DateTimeKind.Unspecified"/>.
    /// </value>
    public DateTimeKind Kind => Start.Kind;

    /// <summary>
    /// Represents a <see cref="DateSpan"/> that does not contain any instant in time. This field is read-only.
    /// </summary>
    /// <remarks>
    /// This value is equivalent to the default value for <see cref="DateSpan"/>.
    /// </remarks>
    public static readonly DateSpan Empty = new DateSpan(DateTime.MinValue, TimeSpan.Zero);

    /// <summary>
    /// Initializes a new instance of the <see cref="DateSpan"/> structure starting from the specified
    /// year, month, day, hour, minute, and second that spans the specified duration.
    /// </summary>
    /// <remarks>
    /// This constructor interprets <paramref name="startYear"/>, <paramref name="startMonth"/>,
    /// and <paramref name="startDay"/> as a year, month, and day in the Gregorian calendar.
    /// </remarks>
    /// <param name="startYear">The starting year (1 through 9999).</param>
    /// <param name="startMonth">The starting month (1 through 12).</param>
    /// <param name="startDay">The starting day (1 through the number of days in <paramref name="startMonth"/>).</param>
    /// <param name="startHour">The starting hours (0 through 23).</param>
    /// <param name="startMinute">The starting minutes (0 through 59).</param>
    /// <param name="startSecond">The starting seconds (0 through 59).</param>
    /// <param name="duration">The length of the interval.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="startYear"/> is less than 1 or greater than 9999.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startMonth"/> is less than 1 or greater than 12.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startDay"/> is less than 1 or greater than the number of days in <paramref name="startMonth"/>.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startHour"/> is less than 0 or greater than 23</para>
    /// <para>-or-</para>
    /// <para><paramref name="startMinute"/> is less than 0 or greater than 59.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startSecond"/> is less than 0 or greater than 59.</para>
    /// <para>-or-</para>
    /// <para><paramref name="duration"/> is negative.</para>
    /// </exception>
    public DateSpan(int startYear, int startMonth, int startDay, int startHour, int startMinute, int startSecond, TimeSpan duration)
        : this(new DateTime(startYear, startMonth, startDay, startHour, startMinute, startSecond), duration)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DateSpan"/> structure starting from the specified
    /// year, month, day, hour, minute, and second that spans the specified duration.
    /// </summary>
    /// <remarks>
    /// This constructor interprets <paramref name="startYear"/>, <paramref name="startMonth"/>,
    /// and <paramref name="startDay"/> as a year, month, and day in the Gregorian calendar.
    /// </remarks>
    /// <param name="startYear">The starting year (1 through 9999).</param>
    /// <param name="startMonth">The starting month (1 through 12).</param>
    /// <param name="startDay">The starting day (1 through the number of days in <paramref name="startMonth"/>).</param>
    /// <param name="startHour">The starting hours (0 through 23).</param>
    /// <param name="startMinute">The starting minutes (0 through 59).</param>
    /// <param name="startSecond">The starting seconds (0 through 59).</param>
    /// <param name="kind">
    /// One of the enumeration values that indicates whether <paramref name="startYear"/>,
    /// <paramref name="startMonth"/>, <paramref name="startDay"/>, <paramref name="startHour"/>,
    /// <paramref name="startMinute"/>, and <paramref name="startSecond"/>
    /// specify a local time, Coordinated Universal Time (UTC), or neither.
    /// </param>
    /// <param name="duration">The length of the interval.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="kind"/> is not one of the <see cref="DateTimeKind"/> values.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="startYear"/> is less than 1 or greater than 9999.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startMonth"/> is less than 1 or greater than 12.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startDay"/> is less than 1 or greater than the number of days in <paramref name="startMonth"/>.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startHour"/> is less than 0 or greater than 23</para>
    /// <para>-or-</para>
    /// <para><paramref name="startMinute"/> is less than 0 or greater than 59.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startSecond"/> is less than 0 or greater than 59.</para>
    /// <para>-or-</para>
    /// <para><paramref name="duration"/> is negative.</para>
    /// </exception>
    public DateSpan(int startYear, int startMonth, int startDay, int startHour, int startMinute, int startSecond, DateTimeKind kind, TimeSpan duration)
        : this(new DateTime(startYear, startMonth, startDay, startHour, startMinute, startSecond, kind), duration)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DateSpan"/> structure starting from the specified
    /// year, month, day, hour, minute, second, and millisecond that spans the specified duration.
    /// </summary>
    /// <remarks>
    /// This constructor interprets <paramref name="startYear"/>, <paramref name="startMonth"/>,
    /// and <paramref name="startDay"/> as a year, month, and day in the Gregorian calendar.
    /// </remarks>
    /// <param name="startYear">The starting year (1 through 9999).</param>
    /// <param name="startMonth">The starting month (1 through 12).</param>
    /// <param name="startDay">The starting day (1 through the number of days in <paramref name="startMonth"/>).</param>
    /// <param name="startHour">The starting hours (0 through 23).</param>
    /// <param name="startMinute">The starting minutes (0 through 59).</param>
    /// <param name="startSecond">The starting seconds (0 through 59).</param>
    /// <param name="startMillisecond">The starting milliseconds (0 through 999).</param>
    /// <param name="duration">The length of the interval.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="startYear"/> is less than 1 or greater than 9999.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startMonth"/> is less than 1 or greater than 12.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startDay"/> is less than 1 or greater than the number of days in <paramref name="startMonth"/>.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startHour"/> is less than 0 or greater than 23</para>
    /// <para>-or-</para>
    /// <para><paramref name="startMinute"/> is less than 0 or greater than 59.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startSecond"/> is less than 0 or greater than 59.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startMillisecond"/> is less than 0 or greater than 999.</para>
    /// <para>-or-</para>
    /// <para><paramref name="duration"/> is negative.</para>
    /// </exception>
    public DateSpan(int startYear, int startMonth, int startDay, int startHour, int startMinute, int startSecond, int startMillisecond, TimeSpan duration)
        : this(new DateTime(startYear, startMonth, startDay, startHour, startMinute, startSecond, startMillisecond), duration)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DateSpan"/> structure starting from the specified
    /// year, month, day, hour, minute, second, and millisecond that spans the specified duration.
    /// </summary>
    /// <remarks>
    /// This constructor interprets <paramref name="startYear"/>, <paramref name="startMonth"/>,
    /// and <paramref name="startDay"/> as a year, month, and day in the Gregorian calendar.
    /// </remarks>
    /// <param name="startYear">The starting year (1 through 9999).</param>
    /// <param name="startMonth">The starting month (1 through 12).</param>
    /// <param name="startDay">The starting day (1 through the number of days in <paramref name="startMonth"/>).</param>
    /// <param name="startHour">The starting hours (0 through 23).</param>
    /// <param name="startMinute">The starting minutes (0 through 59).</param>
    /// <param name="startSecond">The starting seconds (0 through 59).</param>
    /// <param name="startMillisecond">The starting milliseconds (0 through 999).</param>
    /// <param name="kind">
    /// One of the enumeration values that indicates whether <paramref name="startYear"/>,
    /// <paramref name="startMonth"/>, <paramref name="startDay"/>, <paramref name="startHour"/>,
    /// <paramref name="startMinute"/>, <paramref name="startSecond"/>, and <paramref name="startMillisecond"/>
    /// specify a local time, Coordinated Universal Time (UTC), or neither.
    /// </param>
    /// <param name="duration">The length of the interval.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="kind"/> is not one of the <see cref="DateTimeKind"/> values.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="startYear"/> is less than 1 or greater than 9999.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startMonth"/> is less than 1 or greater than 12.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startDay"/> is less than 1 or greater than the number of days in <paramref name="startMonth"/>.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startHour"/> is less than 0 or greater than 23</para>
    /// <para>-or-</para>
    /// <para><paramref name="startMinute"/> is less than 0 or greater than 59.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startSecond"/> is less than 0 or greater than 59.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startMillisecond"/> is less than 0 or greater than 999.</para>
    /// <para>-or-</para>
    /// <para><paramref name="duration"/> is negative.</para>
    /// </exception>
    public DateSpan(int startYear, int startMonth, int startDay, int startHour, int startMinute, int startSecond, int startMillisecond, DateTimeKind kind, TimeSpan duration)
        : this(new DateTime(startYear, startMonth, startDay, startHour, startMinute, startSecond, startMillisecond, kind), duration)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DateSpan"/> structure starting from the specified
    /// year, month, day, hour, minute, second, and millisecond for the calendar that spans the specified duration.
    /// </summary>
    /// <param name="startYear">The starting year (1 through the number of years in <paramref name="calendar"/>).</param>
    /// <param name="startMonth">The starting month (1 through the number of months in <paramref name="calendar"/>).</param>
    /// <param name="startDay">The starting day (1 through the number of days in <paramref name="startMonth"/>).</param>
    /// <param name="startHour">The starting hours (0 through 23).</param>
    /// <param name="startMinute">The starting minutes (0 through 59).</param>
    /// <param name="startSecond">The starting seconds (0 through 59).</param>
    /// <param name="startMillisecond">The starting milliseconds (0 through 999).</param>
    /// <param name="calendar">
    /// The calendar that is used to interpret <paramref name="startYear"/>, <paramref name="startMonth"/>,
    /// and <paramref name="startDay"/>.
    /// </param>
    /// <param name="duration">The length of the interval.</param>
    /// <exception cref="ArgumentNullException"><paramref name="calendar"/> cannot be <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="startYear"/> is not in the range supported by <paramref name="calendar"/>.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startMonth"/> is less than 1 or greater than the number of months in <paramref name="calendar"/>.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startDay"/> is less than 1 or greater than the number of days in <paramref name="startMonth"/>.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startHour"/> is less than 0 or greater than 23</para>
    /// <para>-or-</para>
    /// <para><paramref name="startMinute"/> is less than 0 or greater than 59.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startSecond"/> is less than 0 or greater than 59.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startMillisecond"/> is less than 0 or greater than 999.</para>
    /// <para>-or-</para>
    /// <para><paramref name="duration"/> is negative.</para>
    /// </exception>
    public DateSpan(int startYear, int startMonth, int startDay, int startHour, int startMinute, int startSecond, int startMillisecond, Calendar calendar, TimeSpan duration)
        : this(new DateTime(startYear, startMonth, startDay, startHour, startMinute, startSecond, startMillisecond, calendar), duration)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DateSpan"/> structure starting from the specified
    /// year, month, day, hour, minute, second, and millisecond for the calendar that spans the specified duration.
    /// </summary>
    /// <param name="startYear">The starting year (1 through the number of years in <paramref name="calendar"/>).</param>
    /// <param name="startMonth">The starting month (1 through the number of months in <paramref name="calendar"/>).</param>
    /// <param name="startDay">The starting day (1 through the number of days in <paramref name="startMonth"/>).</param>
    /// <param name="startHour">The starting hours (0 through 23).</param>
    /// <param name="startMinute">The starting minutes (0 through 59).</param>
    /// <param name="startSecond">The starting seconds (0 through 59).</param>
    /// <param name="startMillisecond">The starting milliseconds (0 through 999).</param>
    /// <param name="calendar">
    /// The calendar that is used to interpret <paramref name="startYear"/>, <paramref name="startMonth"/>,
    /// and <paramref name="startDay"/>.
    /// </param>
    /// <param name="kind">
    /// One of the enumeration values that indicates whether <paramref name="startYear"/>,
    /// <paramref name="startMonth"/>, <paramref name="startDay"/>, <paramref name="startHour"/>,
    /// <paramref name="startMinute"/>, <paramref name="startSecond"/>, and <paramref name="startMillisecond"/>
    /// specify a local time, Coordinated Universal Time (UTC), or neither.
    /// </param>
    /// <param name="duration">The length of the interval.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="kind"/> is not one of the <see cref="DateTimeKind"/> values.
    /// </exception>
    /// <exception cref="ArgumentNullException"><paramref name="calendar"/> cannot be <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="startYear"/> is not in the range supported by <paramref name="calendar"/>.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startMonth"/> is less than 1 or greater than the number of months in <paramref name="calendar"/>.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startDay"/> is less than 1 or greater than the number of days in <paramref name="startMonth"/>.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startHour"/> is less than 0 or greater than 23</para>
    /// <para>-or-</para>
    /// <para><paramref name="startMinute"/> is less than 0 or greater than 59.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startSecond"/> is less than 0 or greater than 59.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startMillisecond"/> is less than 0 or greater than 999.</para>
    /// <para>-or-</para>
    /// <para><paramref name="duration"/> is negative.</para>
    /// </exception>
    public DateSpan(int startYear, int startMonth, int startDay, int startHour, int startMinute, int startSecond, int startMillisecond, Calendar calendar, DateTimeKind kind, TimeSpan duration)
        : this(new DateTime(startYear, startMonth, startDay, startHour, startMinute, startSecond, startMillisecond, calendar, kind), duration)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DateSpan"/> structure that represents the interval
    /// between the specified <paramref name="start"/> and <paramref name="end"/> times.
    /// </summary>
    /// <remarks>
    /// If the interval is empty such that values represented by <paramref name="start"/> and
    /// <paramref name="end"/> are equivalent, then the resulting <see cref="Start"/> and
    /// <see cref="End"/> properties are normalized to <see cref="DateTime.MinValue"/>.
    /// </remarks>
    /// <param name="start">The inclusive start of the interval.</param>
    /// <param name="end">The exclusive end of the interval.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="start"/> and <paramref name="end"/> do not have the same <see cref="DateTime.Kind"/> value.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="end"/> occurs before <paramref name="start"/>.
    /// </exception>
    public DateSpan(DateTime start, DateTime end)
    {
        if (start.Kind != end.Kind)
            throw new ArgumentException(SR.KindMismatchMessage);

        TimeSpan duration = end - start;
        if (duration < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(end), SR.EndBeforeStartMessage);

        Start = duration == TimeSpan.Zero ? DateTime.MinValue : start;
        Duration = duration;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DateSpan"/> structure that represents the interval
    /// starting from the given instant of time for the given duration.
    /// </summary>
    /// <remarks>
    /// If the interval is empty such that the value represented by <paramref name="duration"/>
    /// is <see cref="TimeSpan.Zero"/>, then the resulting <see cref="Start"/> property
    /// is normalized to <see cref="DateTime.MinValue"/>.
    /// </remarks>
    /// <param name="start">The inclusive start of the interval.</param>
    /// <param name="duration">The length of the interval.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="duration"/> is negative.</para>
    /// <para>-or-</para>
    /// <para>
    /// The <see cref="End"/> extends beyond the range of valid <see cref="DateTime"/> values.
    /// </para>
    /// </exception>
    public DateSpan(DateTime start, TimeSpan duration)
    {
        if (duration < TimeSpan.Zero)
            throw new ArgumentNegativeException(nameof(duration));

        if (start.Ticks + duration.Ticks > DateTime.MaxValue.Ticks)
            throw new ArgumentOutOfRangeException(nameof(duration), SR.InvalidDateSpanRangeMessage);

        Start = duration == TimeSpan.Zero ? DateTime.MinValue : start;
        Duration = duration;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DateSpan"/> structure that represents the interval
    /// starting from the given instant of time for the given duration.
    /// </summary>
    /// <remarks>
    /// If the interval is empty such that the value represented by <paramref name="durationTicks"/>
    /// is <c>0</c>, then the resulting <see cref="Start"/> property
    /// is normalized to <see cref="DateTime.MinValue"/>.
    /// </remarks>
    /// <param name="start">The inclusive start of the interval.</param>
    /// <param name="durationTicks">
    /// The length of the interval expressed in the number of 100-nanosecond intervals that have
    /// elapsed since January 1, 0001 at 00:00:00.000 in the Gregorian calendar.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="durationTicks"/> is negative.</para>
    /// <para>-or-</para>
    /// <para>
    /// The <see cref="End"/> extends beyond the range of valid <see cref="DateTime"/> values.
    /// </para>
    /// </exception>
    public DateSpan(DateTime start, long durationTicks)
        : this(start, TimeSpan.FromTicks(durationTicks))
    { }

    /// <summary>
    /// Compares the value of this instance to a specified object that contains a specified <see cref="DateSpan"/>
    /// value, and returns an integer that indicates whether this instance is earlier than, the same as, or later
    /// than the specified <see cref="DateSpan"/> value.
    /// </summary>
    /// <remarks>
    /// To determine the relationship of the current instance to <paramref name="obj"/>, the
    /// <see cref="CompareTo(object)"/> method ignores the <see cref="Kind"/> property. Before comparing
    /// <see cref="DateSpan"/> objects, make sure that the objects represent times in the same time zone.
    /// You can do this by comparing the values of their <see cref="Kind"/> properties.
    /// </remarks>
    /// <param name="obj">A boxed object to compare, or <see langword="null"/>.</param>
    /// <returns>
    /// A signed number indicating the relative values of this instance and value.
    /// <list type="table">
    /// <listheader>
    /// <term>Value</term>
    /// <description>Description</description>
    /// </listheader>
    /// <item>
    /// <term>Less than zero</term>
    /// <description>This instance is earlier than <paramref name="obj"/>.</description>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <description>This instance is the same as <paramref name="obj"/>.</description>
    /// </item>
    /// <item>
    /// <term>Greater than zero</term>
    /// <description>
    /// This instance is later than <paramref name="obj"/>, or <paramref name="obj"/> is <see langword="null"/>.
    /// </description>
    /// </item>
    /// </list>
    /// </returns>
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is not DateSpan other)
            throw new ArgumentException(SR.Format(SR.InvalidTypeFormat, nameof(DateSpan)), nameof(obj));

        return CompareTo(other);
    }

    /// <summary>
    /// Compares the value of this instance to a specified <see cref="DateSpan"/> value and returns an integer
    /// that indicates whether this instance is earlier than, the same as, or later than the specified
    /// <see cref="DateSpan"/> value.
    /// </summary>
    /// <remarks>
    /// To determine the relationship of the current instance to <paramref name="other"/>, the
    /// <see cref="CompareTo(DateSpan)"/> method ignores the <see cref="Kind"/> property. Before comparing
    /// <see cref="DateSpan"/> objects, make sure that the objects represent times in the same time zone.
    /// You can do this by comparing the values of their <see cref="Kind"/> properties.
    /// </remarks>
    /// <param name="other">The object to compare to the current instance.</param>
    /// <returns>
    /// A signed number indicating the relative values of this instance and the <paramref name="other"/> parameter.
    /// <list type="table">
    /// <listheader>
    /// <term>Value</term>
    /// <description>Description</description>
    /// </listheader>
    /// <item>
    /// <term>Less than zero</term>
    /// <description>This instance is earlier than <paramref name="other"/>.</description>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <description>This instance is the same as <paramref name="other"/>.</description>
    /// </item>
    /// <item>
    /// <term>Greater than zero</term>
    /// <description>This instance is later than <paramref name="other"/>.</description>
    /// </item>
    /// </list>
    /// </returns>
    public int CompareTo(DateSpan other)
    {
        int cmp = Start.CompareTo(other.Start);
        return cmp == 0 ? Duration.CompareTo(other.Duration) : cmp;
    }

    /// <summary>
    /// Determines whether the <see cref="DateSpan"/> contains a specific <paramref name="value"/>.
    /// </summary>
    /// <remarks>
    /// To determine the whether the current instance contains the <paramref name="value"/>, the
    /// <see cref="Contains(DateTime)"/> method ignores the <see cref="Kind"/> property. Before
    /// invoking the method, make sure that the objects represent times in the same time zone.
    /// You can do this by comparing the values of their <see cref="Kind"/> properties.
    /// </remarks>
    /// <param name="value">The <see cref="DateTime"/> to locate in the <see cref="DateSpan"/>.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is within the
    /// interval; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Contains(DateTime value)
        => value >= Start && value < End;

    /// <summary>
    /// Determines whether the <see cref="DateSpan"/> contains another <see cref="DateSpan"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// To determine the whether the current instance contains the other <see cref="DateSpan"/>, the
    /// <see cref="Contains(DateSpan)"/> method ignores the <see cref="Kind"/> property. Before
    /// invoking the method, make sure that the objects represent times in the same time zone.
    /// You can do this by comparing the values of their <see cref="Kind"/> properties.
    /// </para>
    /// <para>All time intervals contain the <see cref="Empty"/> interval.</para>
    /// </remarks>
    /// <param name="other">Another <see cref="DateSpan"/> instance.</param>
    /// <returns>
    /// <see langword="true"/> if the values of the <see cref="Start"/> and <see cref="End"/> properties
    /// of the <paramref name="other"/> interval are within the values of the <see cref="Start"/> and
    /// <see cref="End"/> properties of the current instance; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Contains(DateSpan other)
        => other.Duration == TimeSpan.Zero || (other.Start >= Start && other.End <= End);

    /// <summary>
    /// Returns a value indicating whether this instance is equal to a specified object.
    /// </summary>
    /// <remarks>
    /// The <see cref="Kind"/> property values are not considered in the test for equality.
    /// </remarks>
    /// <param name="obj">The object to compare to this instance.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="obj"/> is an instance of <see cref="DateSpan"/> and
    /// equals the value of this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is DateSpan other && Equals(other);

    /// <summary>
    /// Returns a value indicating whether the value of this instance is equal to the value of the
    /// specified <see cref="DateSpan"/> instance.
    /// </summary>
    /// <remarks>
    /// The <see cref="Kind"/> property values are not considered in the test for equality.
    /// </remarks>
    /// <param name="other">The object to compare to this instance.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="other"/> parameter equals the
    /// value of this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(DateSpan other)
        => Start.Equals(other.Start) && Duration.Equals(other.Duration);

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode()
        => Start.GetHashCode() ^ Duration.GetHashCode();

    /// <summary>
    /// Returns a new <see cref="DateSpan"/> instance that represents the intersection of
    /// this <see cref="DateSpan"/> instance with the <paramref name="other"/> instance.
    /// </summary>
    /// <remarks>
    /// The <see cref="IntersectWith"/> method does not consider the value of the
    /// <see cref="Kind"/> property of the two <see cref="DateSpan"/> values when performing the intersection.
    /// Before intersecting <see cref="DateSpan"/> objects, ensure that the objects represent intervals in the
    /// same time zone. Otherwise, the result will include the difference between time zones.
    /// </remarks>
    /// <param name="other">The <see cref="DateSpan"/> to compare to the current interval.</param>
    /// <returns>
    /// The intersection of the intervals if they overlap; otherwise, the default value.
    /// </returns>
    public DateSpan IntersectWith(DateSpan other)
    {
        if (Duration == TimeSpan.Zero || other.Duration == TimeSpan.Zero)
            return default;

        (DateSpan first, DateSpan second) = GetAscendingOrder(this, other);

        long maxIntersection = first.End.Ticks - second.Start.Ticks;
        return maxIntersection > 0
            ? new DateSpan(second.Start, Math.Min(second.Duration.Ticks, maxIntersection))
            : default;
    }

    /// <summary>
    /// Determines whether the current interval overlaps with the specified interval.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="Overlaps"/> method does not consider the value of the
    /// <see cref="Kind"/> property of the two <see cref="DateSpan"/> values. Before invoking the method,
    /// ensure that the objects represent intervals in the same time zone. Otherwise, the result will not account
    /// for the difference between time zones.
    /// </para>
    /// <para>The <see cref="Empty"/> interval does not overlap with any other interval.</para>
    /// </remarks>
    /// <param name="other">The interval to compare to the current interval.</param>
    /// <returns>
    /// <see langword="true"/> if there is at least one instant of time that exists in both
    /// this and the <paramref name="other"/> interval; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Overlaps(DateSpan other)
    {
        if (Duration == TimeSpan.Zero || other.Duration == TimeSpan.Zero)
            return false;

        (DateSpan first, DateSpan second) = GetAscendingOrder(this, other);
        return first.Contains(second.Start);
    }

    // TODO: Write ToString and Parse/ParseExact
    // TODO: Should DateSpan have methods for extending or shifting the interval?

    /// <summary>
    /// Returns a <see cref="DateSpan"/> that represents the given instant in time.
    /// </summary>
    /// <remarks>
    /// The resulting interval only encompasses the given value, and as such its
    /// <see cref="Duration"/> is always 1 tick or 100-nanoseconds.
    /// </remarks>
    /// <param name="value">The <see cref="DateTime"/> value.</param>
    /// <returns>An object that represents the <paramref name="value"/>.</returns>
    public static DateSpan FromDateTime(DateTime value)
        => new DateSpan(value, 1);

    /// <summary>
    /// Returns a <see cref="DateSpan"/> that represents the given day in a particular month and year.
    /// </summary>
    /// <param name="year">The year (1 through 9999).</param>
    /// <param name="month">The month (1 through 12).</param>
    /// <param name="day">The day (1 through the number of days in <paramref name="month"/>).</param>
    /// <returns>
    /// An object that represents the <paramref name="year"/>, <paramref name="month"/>, and <paramref name="day"/>.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="year"/> is less than 1 or greater than 9999.</para>
    /// <para>-or-</para>
    /// <para><paramref name="month"/> is less than 1 or greater than 12.</para>
    /// <para>-or-</para>
    /// <para><paramref name="day"/> is less than 1 or greater than the number of days in <paramref name="month"/>.</para>
    /// </exception>
    public static DateSpan FromDay(int year, int month, int day)
        => FromDay(year, month, day, DateTimeKind.Unspecified);

    /// <summary>
    /// Returns a <see cref="DateSpan"/> that represents the given day in a particular month and year.
    /// </summary>
    /// <param name="year">The year (1 through 9999).</param>
    /// <param name="month">The month (1 through 12).</param>
    /// <param name="day">The day (1 through the number of days in <paramref name="month"/>).</param>
    /// <param name="kind">
    /// One of the enumeration values that indicates whether <paramref name="year"/>, <paramref name="month"/>,
    /// and <paramref name="day"/> specify a local time, Coordinated Universal Time (UTC), or neither.
    /// </param>
    /// <returns>
    /// An object that represents the <paramref name="year"/>, <paramref name="month"/>, and <paramref name="day"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="kind"/> is not one of the <see cref="DateTimeKind"/> values.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="year"/> is less than 1 or greater than 9999.</para>
    /// <para>-or-</para>
    /// <para><paramref name="month"/> is less than 1 or greater than 12.</para>
    /// <para>-or-</para>
    /// <para><paramref name="day"/> is less than 1 or greater than the number of days in <paramref name="month"/>.</para>
    /// </exception>
    public static DateSpan FromDay(int year, int month, int day, DateTimeKind kind)
    {
        DateTime start = new DateTime(year, month, day, 0, 0, 0, kind);
        return new DateSpan(start, TimeSpan.FromDays(1));
    }

    /// <summary>
    /// Returns a <see cref="DateSpan"/> that represents the given day in a particular month and year
    /// for the calendar.
    /// </summary>
    /// <param name="year">The year (1 through the number of years in <paramref name="calendar"/>).</param>
    /// <param name="month">The month (1 through the number of months in <paramref name="calendar"/>).</param>
    /// <param name="day">The day (1 through the number of days in <paramref name="month"/>).</param>
    /// <param name="calendar">
    /// The calendar that is used to interpret <paramref name="year"/>, <paramref name="month"/>,
    /// and <paramref name="day"/>.
    /// </param>
    /// <param name="kind">
    /// One of the enumeration values that indicates whether <paramref name="year"/>, <paramref name="month"/>,
    /// and <paramref name="day"/> specify a local time, Coordinated Universal Time (UTC), or neither.
    /// </param>
    /// <returns>
    /// An object that represents the <paramref name="year"/>, <paramref name="month"/>, and <paramref name="day"/>
    /// for a specified <paramref name="calendar"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="kind"/> is not one of the <see cref="DateTimeKind"/> values.
    /// </exception>
    /// <exception cref="ArgumentNullException"><paramref name="calendar"/> cannot be <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="year"/> is not in the range supported by <paramref name="calendar"/>.</para>
    /// <para>-or-</para>
    /// <para><paramref name="month"/> is less than 1 or greater than the number of months in <paramref name="calendar"/>.</para>
    /// <para>-or-</para>
    /// <para><paramref name="day"/> is less than 1 or greater than the number of days in <paramref name="month"/>.</para>
    /// </exception>
    public static DateSpan FromDay(int year, int month, int day, Calendar calendar, DateTimeKind kind)
    {
        DateTime start = new DateTime(year, month, day, 0, 0, 0, 0, calendar, kind);
        return new DateSpan(start, TimeSpan.FromDays(1));
    }

    /// <summary>
    /// Returns a <see cref="DateSpan"/> that represents the given month in a particular year.
    /// </summary>
    /// <param name="year">The year (1 through 9999).</param>
    /// <param name="month">The month (1 through 12).</param>
    /// <returns>
    /// An object that represents the <paramref name="year"/> and <paramref name="month"/>.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="year"/> is less than 1 or greater than 9999.</para>
    /// <para>-or-</para>
    /// <para><paramref name="month"/> is less than 1 or greater than 12.</para>
    /// </exception>
    public static DateSpan FromMonth(int year, int month)
        => FromMonth(year, month, DateTimeKind.Unspecified);

    /// <summary>
    /// Returns a <see cref="DateSpan"/> that represents the given month in a particular year.
    /// </summary>
    /// <param name="year">The year (1 through 9999).</param>
    /// <param name="month">The month (1 through 12).</param>
    /// <param name="kind">
    /// One of the enumeration values that indicates whether <paramref name="year"/> and <paramref name="month"/>,
    /// specify a local time, Coordinated Universal Time (UTC), or neither.
    /// </param>
    /// <returns>
    /// An object that represents the <paramref name="year"/> and <paramref name="month"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="kind"/> is not one of the <see cref="DateTimeKind"/> values.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="year"/> is less than 1 or greater than 9999.</para>
    /// <para>-or-</para>
    /// <para><paramref name="month"/> is less than 1 or greater than 12.</para>
    /// <para>-or-</para>
    /// <para><paramref name="month"/> is less than 1 or greater than 12.</para>
    /// </exception>
    public static DateSpan FromMonth(int year, int month, DateTimeKind kind)
    {
        DateTime start = new DateTime(year, month, 1, 0, 0, 0, kind);
        return new DateSpan(start, start.AddMonths(1));
    }

    /// <summary>
    /// Returns a <see cref="DateSpan"/> that represents the given month in a particular year for the calendar.
    /// </summary>
    /// <param name="year">The year (1 through the number of years in <paramref name="calendar"/>).</param>
    /// <param name="month">The month (1 through the number of months in <paramref name="calendar"/>).</param>
    /// <param name="calendar">
    /// The calendar that is used to interpret <paramref name="year"/> and <paramref name="month"/>.
    /// </param>
    /// <param name="kind">
    /// One of the enumeration values that indicates whether <paramref name="year"/> and <paramref name="month"/>
    /// specify a local time, Coordinated Universal Time (UTC), or neither.
    /// </param>
    /// <returns>
    /// An object that represents the <paramref name="year"/> and <paramref name="month"/>
    /// for a specified <paramref name="calendar"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="kind"/> is not one of the <see cref="DateTimeKind"/> values.
    /// </exception>
    /// <exception cref="ArgumentNullException"><paramref name="calendar"/> cannot be <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="year"/> is not in the range supported by <paramref name="calendar"/>.</para>
    /// <para>-or-</para>
    /// <para><paramref name="month"/> is less than 1 or greater than the number of months in <paramref name="calendar"/>.</para>
    /// </exception>
    public static DateSpan FromMonth(int year, int month, Calendar calendar, DateTimeKind kind)
    {
        DateTime start = new DateTime(year, month, 1, 0, 0, 0, 0, calendar, kind);
        return new DateSpan(start, start.AddMonths(1));
    }

    /// <summary>
    /// Returns a <see cref="DateSpan"/> that represents the given year.
    /// </summary>
    /// <param name="year">The year (1 through 9999).</param>
    /// <returns>An object that represents the <paramref name="year"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="year"/> is less than 1 or greater than 9999.
    /// </exception>
    public static DateSpan FromYear(int year)
        => FromYear(year, DateTimeKind.Unspecified);

    /// <summary>
    /// Returns a <see cref="DateSpan"/> that represents the given year.
    /// </summary>
    /// <param name="year">The year (1 through 9999).</param>
    /// <param name="kind">
    /// One of the enumeration values that indicates whether <paramref name="year"/>
    /// specifies a local time, Coordinated Universal Time (UTC), or neither.
    /// </param>
    /// <returns>An object that represents the <paramref name="year"/>.</returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="kind"/> is not one of the <see cref="DateTimeKind"/> values.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="year"/> is less than 1 or greater than 9998.
    /// </exception>
    public static DateSpan FromYear(int year, DateTimeKind kind)
    {
        DateTime start = new DateTime(year, 1, 1, 0, 0, 0, kind);
        return new DateSpan(start, start.AddYears(1));
    }

    /// <summary>
    /// Returns a <see cref="DateSpan"/> that represents the given year for the calendar.
    /// </summary>
    /// <param name="year">The year (1 through the number of years in <paramref name="calendar"/>).</param>
    /// <param name="calendar">The calendar that is used to interpret <paramref name="year"/>.</param>
    /// <param name="kind">
    /// One of the enumeration values that indicates whether <paramref name="year"/>
    /// specifies a local time, Coordinated Universal Time (UTC), or neither.
    /// </param>
    /// <returns>
    /// An object that represents the <paramref name="year"/> for a specified <paramref name="calendar"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="kind"/> is not one of the <see cref="DateTimeKind"/> values.
    /// </exception>
    /// <exception cref="ArgumentNullException"><paramref name="calendar"/> cannot be <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="year"/> is not in the range supported by <paramref name="calendar"/>.
    /// </exception>
    public static DateSpan FromYear(int year, Calendar calendar, DateTimeKind kind)
    {
        DateTime start = new DateTime(year, 1, 1, 0, 0, 0, 0, calendar, kind);
        return new DateSpan(start, start.AddYears(1));
    }

    /// <summary>
    /// Creates a new <see cref="DateSpan"/> object that has the same value as the specified
    /// <see cref="DateSpan"/>, but its values for the <see cref="Start"/> and <see cref="End"/>
    /// properties are designated as either local time, Coordinated Universal Time (UTC), or neither,
    /// as indicated by the specified <see cref="DateTimeKind"/> value.
    /// </summary>
    /// <remarks>
    /// The <see cref="SpecifyKind"/> method leaves the times unchanged, and only sets the
    /// <see cref="Kind"/> property to <paramref name="kind"/>.
    /// </remarks>
    /// <param name="value">A time interval.</param>
    /// <param name="kind">
    /// One of the enumeration values that indicates whether the new object represents local time, UTC, or neither.
    /// </param>
    /// <returns>
    /// A new object that has the same value for the <see cref="Start"/> and <see cref="End"/>
    /// properties as the <paramref name="value"/> parameter, and the <see cref="DateTimeKind"/>
    /// value specified by the <paramref name="kind"/> parameter.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="kind"/> is not one of the <see cref="DateTimeKind"/> values.
    /// </exception>
    public static DateSpan SpecifyKind(DateSpan value, DateTimeKind kind)
        => new DateSpan(DateTime.SpecifyKind(value.Start, kind), value.Duration);

    /// <summary>
    /// Determines whether two specified instances of <see cref="DateSpan"/> are equal.
    /// </summary>
    /// <remarks>
    /// The <see cref="Kind"/> property values are not considered in the test for equality.
    /// </remarks>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> and <paramref name="right"/>
    /// represent the interval; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(DateSpan left, DateSpan right)
        => left.Equals(right);

    /// <summary>
    /// Determines whether two specified instances of <see cref="DateSpan"/> are not equal.
    /// </summary>
    /// <remarks>
    /// The <see cref="Kind"/> property values are not considered in the test for equality.
    /// </remarks>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> and <paramref name="right"/>
    /// do not represent the interval; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(DateSpan left, DateSpan right)
        => !left.Equals(right);

    /// <summary>
    /// Determines whether one specified <see cref="DateSpan"/> is earlier than another specified <see cref="DateSpan"/>.
    /// </summary>
    /// <remarks>
    /// The <see cref="operator {(DateSpan, DateSpan)"/> operator determines the relationship between two
    /// <see cref="DateSpan"/> values by comparing the number of ticks represented by their <see cref="Start"/>
    /// properties and their durations. Before comparing <see cref="DateSpan"/> objects, make sure that the objects
    /// represent times in the same time zone. You can do this by comparing the values of their
    /// <see cref="Kind"/> properties.
    /// </remarks>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is earlier than <paramref name="right"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <(DateSpan left, DateSpan right)
        => left.CompareTo(right) < 0;

    /// <summary>
    /// Determines whether one specified <see cref="DateSpan"/> represents an interval that is the same as or
    /// earlier than another specified <see cref="DateSpan"/>.
    /// </summary>
    /// <remarks>
    /// The <see cref="operator {=(DateSpan, DateSpan)"/> operator determines the relationship between two
    /// <see cref="DateSpan"/> values by comparing the number of ticks represented by their <see cref="Start"/>
    /// properties and their durations. Before comparing <see cref="DateSpan"/> objects, make sure that the objects
    /// represent times in the same time zone. You can do this by comparing the values of their
    /// <see cref="Kind"/> properties.
    /// </remarks>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is the same as or earlier than <paramref name="right"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <=(DateSpan left, DateSpan right)
        => left.CompareTo(right) <= 0;

    /// <summary>
    /// Determines whether one specified <see cref="DateSpan"/> is later than another specified <see cref="DateSpan"/>.
    /// </summary>
    /// <remarks>
    /// The <see cref="operator }(DateSpan, DateSpan)"/> operator determines the relationship between two
    /// <see cref="DateSpan"/> values by comparing the number of ticks represented by their <see cref="Start"/>
    /// properties and their durations. Before comparing <see cref="DateSpan"/> objects, make sure that the objects
    /// represent times in the same time zone. You can do this by comparing the values of their
    /// <see cref="Kind"/> properties.
    /// </remarks>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is later than <paramref name="right"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >(DateSpan left, DateSpan right)
        => left.CompareTo(right) > 0;

    /// <summary>
    /// Determines whether one specified <see cref="DateSpan"/> represents an interval that is the same as or
    /// later than another specified <see cref="DateSpan"/>.
    /// </summary>
    /// <remarks>
    /// The <see cref="operator }=(DateSpan, DateSpan)"/> operator determines the relationship between two
    /// <see cref="DateSpan"/> values by comparing the number of ticks represented by their <see cref="Start"/>
    /// properties and their durations. Before comparing <see cref="DateSpan"/> objects, make sure that the objects
    /// represent times in the same time zone. You can do this by comparing the values of their
    /// <see cref="Kind"/> properties.
    /// </remarks>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is the same as or later than <paramref name="right"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >=(DateSpan left, DateSpan right)
        => left.CompareTo(right) >= 0;

    /// <summary>
    /// Defines an explicit conversion of a <see cref="DateTime"/> instance to an equivalent
    /// <see cref="DateSpan"/> instance that represents the given instant in time.
    /// </summary>
    /// <remarks>
    /// The resulting interval only encompasses the given value, and as such its
    /// <see cref="Duration"/> is always 1 tick or 100-nanoseconds.
    /// </remarks>
    /// <param name="value">A <see cref="DateTime"/> value.</param>
    /// <returns>An object that represents the <paramref name="value"/>.</returns>
    public static explicit operator DateSpan(DateTime value)
        => FromDateTime(value);

    private static (DateSpan First, DateSpan Second) GetAscendingOrder(DateSpan x, DateSpan y)
        // Order by smallest start to help with other methods like Overlaps.
        // If they are equivalent, then the order doesn't matter
        => x.Start <= y.Start ? (x, y) : (y, x);
}
