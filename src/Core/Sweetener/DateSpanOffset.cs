// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Sweetener;

/// <summary>
/// Represents an interval between two instants in time, each expressed as a date and time of day,
/// relative to Coordinated Universal Time (UTC).
/// </summary>
[Serializable]
public readonly struct DateSpanOffset : IComparable, IComparable<DateSpanOffset>, IEquatable<DateSpanOffset> //, IFormattable
{
    /// <summary>
    /// Gets the instant in time that starts the interval represented by this instance.
    /// </summary>
    /// <value>The <see cref="DateTimeOffset"/> that indicates the inclusive start of this interval.</value>
    public DateTimeOffset Start { get; }

    /// <summary>
    /// Gets a <see cref="Sweetener.DateSpan"/> value that represents the interval portion of
    /// the current <see cref="DateSpanOffset"/> object.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="DateSpan"/> property is not affected by the value of the <see cref="Offset"/> property.
    /// </para>
    /// <para>
    /// The value of the <see cref="DateSpan.Kind"/> property of the returned <see cref="Sweetener.DateSpan"/> object is
    /// <see cref="DateTimeKind.Unspecified"/>.
    /// </para>
    /// </remarks>
    /// <value>The interval portion of the current <see cref="DateSpanOffset"/> object.</value>
    public DateSpan DateSpan => new DateSpan(Start.DateTime, Duration);

    /// <summary>
    /// Gets the length of time represented by this instance irrespective of when it starts and ends.
    /// This value is always non-negative.
    /// </summary>
    /// <value>The <see cref="TimeSpan"/> that indicates the length of time.</value>
    public TimeSpan Duration { get; }

    /// <summary>
    /// Gets the instant in time that ends the interval represented by this instance.
    /// </summary>
    /// <value>The <see cref="DateTimeOffset"/> that indicates the exclusive end of this interval.</value>
    public DateTimeOffset End => Start + Duration;

    /// <summary>
    /// Gets the interval's offset from Coordinated Universal Time (UTC).
    /// </summary>
    /// <remarks>
    /// <para>
    /// The value of the <see cref="TimeSpan.Hours"/> property of the returned <see cref="TimeSpan"/>
    /// object can range from -14 hours to 14 hours.
    /// </para>
    /// <para>
    /// The value of the <see cref="Offset"/> property is precise to the minute.
    /// </para>
    /// </remarks>
    /// <value>
    /// The difference between the current <see cref="DateSpanOffset"/> object's time values
    /// and Coordinated Universal Time (UTC).
    /// </value>
    public TimeSpan Offset => Start.Offset;

    /// <summary>
    /// Gets a <see cref="Sweetener.DateSpan"/> value that represents the Coordinated Universal Time (UTC)
    /// interval portion of the current <see cref="DateSpanOffset"/> object.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="DateSpan"/> property is not affected by the value of the <see cref="Offset"/> property.
    /// </para>
    /// <para>
    /// The resulting <see cref="DateSpan.Start"/> and <see cref="DateSpan.End"/> properties are converted
    /// to Coordinated Universal Time (UTC) by using the <see cref="DateTimeOffset.UtcDateTime"/> property.
    /// </para>
    /// <para>
    /// The value of the <see cref="DateSpan.Kind"/> property of the returned <see cref="Sweetener.DateSpan"/> object is
    /// <see cref="DateTimeKind.Utc"/>.
    /// </para>
    /// </remarks>
    /// <value>
    /// The Coordinated Universal Time (UTC) interval portion of the current <see cref="DateSpanOffset"/> object.
    /// </value>
    public DateSpan UtcDateSpan => new DateSpan(Start.UtcDateTime, Duration);

    /// <summary>
    /// Represents a <see cref="DateSpanOffset"/> that does not contain any instant in time. This field is read-only.
    /// </summary>
    /// <remarks>
    /// This value is equivalent to the default value for <see cref="DateSpanOffset"/>.
    /// </remarks>
    public static readonly DateSpanOffset Empty = new DateSpanOffset(DateTimeOffset.MinValue, TimeSpan.Zero);

    /// <summary>
    /// Initializes a new instance of the <see cref="DateSpanOffset"/> structure using the specified
    /// <see cref="Sweetener.DateSpan"/> value.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This constructor's behavior depends on the value of the <see cref="Sweetener.DateSpan.Kind"/> property of the
    /// <paramref name="dateSpan"/> parameter:
    /// </para>
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// If the value of <see cref="Sweetener.DateSpan.Kind"/> is <see cref="DateTimeKind.Utc"/>, the
    /// <see cref="Offset"/> property is set equal to <see cref="TimeSpan.Zero"/>.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// If the value of <see cref="DateSpan.Kind"/> is <see cref="DateTimeKind.Local"/> or
    /// <see cref="DateTimeKind.Unspecified"/>, the <see cref="Offset"/> property is set equal to the
    /// offset of the local system's current time zone.
    /// </description>
    /// </item>
    /// </list>
    /// </remarks>
    /// <param name="dateSpan">A time interval.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para>
    /// The Coordinated Universal Time (UTC) date and time values for the <see cref="Start"/> or
    /// <see cref="End"/> properties that results from applying the offset are earlier than
    /// <see cref="DateTimeOffset.MinValue"/>.
    /// </para>
    /// <para>-or-</para>
    /// <para>
    /// The Coordinated Universal Time (UTC) date and time values for the <see cref="Start"/> or
    /// <see cref="End"/> properties that results from applying the offset are later than
    /// <see cref="DateTimeOffset.MaxValue"/>.
    /// </para>
    /// </exception>
    public DateSpanOffset(DateSpan dateSpan)
        : this(new DateTimeOffset(dateSpan.Start), dateSpan.Duration)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DateSpanOffset"/> structure using the specified
    /// <see cref="Sweetener.DateSpan"/> value and offset.
    /// </summary>
    /// <param name="dateSpan">A date and time.</param>
    /// <param name="offset">The time's offset from Coordinated Universal Time (UTC).</param>
    /// <exception cref="ArgumentException">
    /// <para>
    /// The value of the <see cref="DateSpan.Kind"/> property for the <paramref name="dateSpan"/> parameter
    /// equals <see cref="DateTimeKind.Utc"/> and <paramref name="offset"/> does not equal <see cref="TimeSpan.Zero"/>.
    /// </para>
    /// <para>-or-</para>
    /// <para>
    /// The value of the <see cref="Sweetener.DateSpan.Kind"/> property for the <paramref name="dateSpan"/> parameter
    /// equals <see cref="DateTimeKind.Local"/> and duration represented by the <paramref name="offset"/>
    /// parameter does not equal the offset of the system's local time zone.
    /// </para>
    /// <para>-or-</para>
    /// <para><paramref name="offset"/> is not specified in whole minutes.</para>
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="offset"/> is less than -14 hours or greater than 14 hours.</para>
    /// <para>-or-</para>
    /// <para>
    /// The resulting value for the <see cref="Start"/> or <see cref="End"/> property is less than
    /// <see cref="DateTime.MinValue"/> or greater than <see cref="DateTime.MaxValue"/> when converted
    /// to Coordinated Universal Time (UTC) via the <see cref="DateTimeOffset.UtcDateTime"/> property.
    /// </para>
    /// </exception>
    public DateSpanOffset(DateSpan dateSpan, TimeSpan offset)
        : this(new DateTimeOffset(dateSpan.Start, offset), dateSpan.Duration)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DateSpanOffset"/> structure using the specified
    /// starting times, offset, and duration.
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
    /// <param name="offset">The offset from Coordinated Universal Time (UTC) for the start and end.</param>
    /// <param name="duration">The length of the interval.</param>
    /// <exception cref="ArgumentException"><paramref name="offset"/> is not specified in whole minutes.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="startYear"/> is less than one or greater than 9999.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startMonth"/> is less than one or greater than 12.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startDay"/> is less than one or greater than the number of days in <paramref name="startMonth"/>.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startHour"/> is less than zero or greater than 23.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startMinute"/> is less than 0 or greater than 59.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startSecond"/> is less than 0 or greater than 59.</para>
    /// <para>-or-</para>
    /// <para><paramref name="offset"/> is less than -14 hours or greater than 14 hours.</para>
    /// <para>-or-</para>
    /// <para><paramref name="duration"/> is negative.</para>
    /// <para>-or-</para>
    /// <para>
    /// The resulting value for the <see cref="Start"/> or <see cref="End"/> property is less than
    /// <see cref="DateTime.MinValue"/> or greater than <see cref="DateTime.MaxValue"/> when converted
    /// to Coordinated Universal Time (UTC) via the <see cref="DateTimeOffset.UtcDateTime"/> property.
    /// </para>
    /// </exception>
    public DateSpanOffset(int startYear, int startMonth, int startDay, int startHour, int startMinute, int startSecond, TimeSpan offset, TimeSpan duration)
        : this(new DateTimeOffset(startYear, startMonth, startDay, startHour, startMinute, startSecond, offset), duration)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DateSpanOffset"/> structure using the specified
    /// starting times, offset, and duration.
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
    /// <param name="offset">The offset from Coordinated Universal Time (UTC) for the start and end.</param>
    /// <param name="duration">The length of the interval.</param>
    /// <exception cref="ArgumentException"><paramref name="offset"/> is not specified in whole minutes.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="startYear"/> is less than one or greater than 9999.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startMonth"/> is less than one or greater than 12.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startDay"/> is less than one or greater than the number of days in <paramref name="startMonth"/>.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startHour"/> is less than zero or greater than 23.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startMinute"/> is less than 0 or greater than 59.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startSecond"/> is less than 0 or greater than 59.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startMillisecond"/> is less than 0 or greater than 999.</para>
    /// <para>-or-</para>
    /// <para><paramref name="offset"/> is less than -14 hours or greater than 14 hours.</para>
    /// <para>-or-</para>
    /// <para><paramref name="duration"/> is negative.</para>
    /// <para>-or-</para>
    /// <para>
    /// The resulting value for the <see cref="Start"/> or <see cref="End"/> property is less than
    /// <see cref="DateTime.MinValue"/> or greater than <see cref="DateTime.MaxValue"/> when converted
    /// to Coordinated Universal Time (UTC) via the <see cref="DateTimeOffset.UtcDateTime"/> property.
    /// </para>
    /// </exception>
    public DateSpanOffset(int startYear, int startMonth, int startDay, int startHour, int startMinute, int startSecond, int startMillisecond, TimeSpan offset, TimeSpan duration)
        : this(new DateTimeOffset(startYear, startMonth, startDay, startHour, startMinute, startSecond, startMillisecond, offset), duration)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DateSpanOffset"/> structure using the specified
    /// starting times, offset, and duration.
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
    /// <param name="offset">The offset from Coordinated Universal Time (UTC) for the start and end.</param>
    /// <param name="duration">The length of the interval.</param>
    /// <exception cref="ArgumentException"><paramref name="offset"/> is not specified in whole minutes.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="calendar"/> cannot be <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="startYear"/> is not in the range supported by <paramref name="calendar"/>.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startMonth"/> is less than 1 or greater than the number of months in <paramref name="calendar"/>.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startDay"/> is less than 1 or greater than the number of days in <paramref name="startMonth"/>.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startHour"/> is less than zero or greater than 23.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startMinute"/> is less than 0 or greater than 59.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startSecond"/> is less than 0 or greater than 59.</para>
    /// <para>-or-</para>
    /// <para><paramref name="startMillisecond"/> is less than 0 or greater than 999.</para>
    /// <para>-or-</para>
    /// <para><paramref name="offset"/> is less than -14 hours or greater than 14 hours.</para>
    /// <para>-or-</para>
    /// <para><paramref name="duration"/> is negative.</para>
    /// <para>-or-</para>
    /// <para>
    /// The <paramref name="startYear"/>, <paramref name="startMonth"/>, and <paramref name="startDay"/>
    /// parameters cannot be represented as a date and time value.
    /// </para>
    /// <para>-or-</para>
    /// <para>
    /// The resulting value for the <see cref="Start"/> or <see cref="End"/> property is less than
    /// <see cref="DateTime.MinValue"/> or greater than <see cref="DateTime.MaxValue"/> when converted
    /// to Coordinated Universal Time (UTC) via the <see cref="DateTimeOffset.UtcDateTime"/> property.
    /// </para>
    /// </exception>
    public DateSpanOffset(int startYear, int startMonth, int startDay, int startHour, int startMinute, int startSecond, int startMillisecond, Calendar calendar, TimeSpan offset, TimeSpan duration)
        : this(new DateTimeOffset(startYear, startMonth, startDay, startHour, startMinute, startSecond, startMillisecond, calendar, offset), duration)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DateSpanOffset"/> using the specified
    /// starting time, offset, and duration.
    /// </summary>
    /// <remarks>
    /// If the interval is empty such that the value represented by <paramref name="duration"/>
    /// is <see cref="TimeSpan.Zero"/>, then the resulting <see cref="Start"/> property
    /// is normalized to <see cref="DateTimeOffset.MinValue"/>.
    /// </remarks>
    /// <param name="start">The inclusive start of the interval.</param>
    /// <param name="offset">The offset from Coordinated Universal Time (UTC) for the start and end.</param>
    /// <param name="duration">The length of the interval.</param>
    /// <exception cref="ArgumentException">
    /// <para>
    /// The value of the <see cref="DateSpan.Kind"/> property for the <paramref name="start"/> parameter
    /// equals <see cref="DateTimeKind.Utc"/> and <paramref name="offset"/> does not equal <see cref="TimeSpan.Zero"/>.
    /// </para>
    /// <para>-or-</para>
    /// <para>
    /// The value of the <see cref="Sweetener.DateSpan.Kind"/> property for the <paramref name="start"/> parameter
    /// equals <see cref="DateTimeKind.Local"/> and duration represented by the <paramref name="offset"/>
    /// parameter does not equal the offset of the system's local time zone.
    /// </para>
    /// <para>-or-</para>
    /// <para><paramref name="offset"/> is not specified in whole minutes.</para>
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="offset"/> is less than -14 hours or greater than 14 hours.</para>
    /// <para>-or-</para>
    /// <para>
    /// The resulting value for the <see cref="Start"/> or <see cref="End"/> property is less than
    /// <see cref="DateTime.MinValue"/> or greater than <see cref="DateTime.MaxValue"/> when converted
    /// to Coordinated Universal Time (UTC) via the <see cref="DateTimeOffset.UtcDateTime"/> property.
    /// </para>
    /// </exception>
    public DateSpanOffset(DateTime start, TimeSpan offset, TimeSpan duration)
        : this(new DateTimeOffset(start, offset), duration)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DateSpanOffset"/> structure that represents the interval
    /// between the specified <paramref name="start"/> and <paramref name="end"/> times.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the interval is empty such that values represented by <paramref name="start"/> and
    /// <paramref name="end"/> are equivalent, then the resulting <see cref="Start"/> and
    /// <see cref="End"/> properties are normalized to <see cref="DateTimeOffset.MinValue"/>.
    /// </para>
    /// <para>
    /// If the arguments have different values for their respective <see cref="DateTimeOffset.Offset"/>
    /// properties, the <paramref name="end"/> is adjusted to use the <see cref="DateTimeOffset.Offset"/>
    /// from <paramref name="start"/>.
    /// </para>
    /// </remarks>
    /// <param name="start">The inclusive start of the interval.</param>
    /// <param name="end">The exclusive end of the interval.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="end"/> occurs before <paramref name="start"/>.
    /// </exception>
    public DateSpanOffset(DateTimeOffset start, DateTimeOffset end)
    {
        TimeSpan duration = end - start;
        if (duration < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(end), SR.EndBeforeStartMessage);

        Start    = duration == TimeSpan.Zero ? DateTimeOffset.MinValue : start;
        Duration = duration;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DateSpanOffset"/> structure that represents the interval
    /// starting from the given instant of time for the given duration.
    /// </summary>
    /// <remarks>
    /// If the interval is empty such that the value represented by <paramref name="duration"/>
    /// is <see cref="TimeSpan.Zero"/>, then the resulting <see cref="Start"/> property
    /// is normalized to <see cref="DateTimeOffset.MinValue"/>.
    /// </remarks>
    /// <param name="start">The inclusive start of the interval.</param>
    /// <param name="duration">The length of the interval.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="duration"/> is negative.</para>
    /// <para>-or-</para>
    /// <para>
    /// The <see cref="End"/> extends beyond the range of valid <see cref="DateTimeOffset"/> values.
    /// </para>
    /// </exception>
    public DateSpanOffset(DateTimeOffset start, TimeSpan duration)
    {
        if (duration < TimeSpan.Zero)
            throw new ArgumentNegativeException(nameof(duration));

        if (start.UtcDateTime.Ticks + duration.Ticks > DateTime.MaxValue.Ticks)
            throw new ArgumentOutOfRangeException(nameof(duration), SR.InvalidDateSpanRangeMessage);

        Start    = duration == TimeSpan.Zero ? DateTimeOffset.MinValue : start;
        Duration = duration;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DateSpanOffset"/> structure that represents the interval
    /// starting from the given instant of time for the given duration.
    /// </summary>
    /// <remarks>
    /// If the interval is empty such that the value represented by <paramref name="durationTicks"/>
    /// is <c>0</c>, then the resulting <see cref="Start"/> property
    /// is normalized to <see cref="DateTimeOffset.MinValue"/>.
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
    /// The <see cref="End"/> extends beyond the range of valid <see cref="DateTimeOffset"/> values.
    /// </para>
    /// </exception>
    public DateSpanOffset(DateTimeOffset start, long durationTicks)
        : this(start, TimeSpan.FromTicks(durationTicks))
    { }

    /// <summary>
    /// Compares the value of this instance to a specified object that contains a specified <see cref="DateSpanOffset"/>
    /// value, and returns an integer that indicates whether this instance is earlier than, the same as, or later
    /// than the specified <see cref="DateSpanOffset"/> value.
    /// </summary>
    /// <remarks>
    /// This method compares <see cref="DateSpanOffset"/> objects by comparing their <see cref="UtcDateSpan"/> values.
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

        if (obj is not DateSpanOffset other)
            throw new ArgumentException(SR.Format(SR.InvalidTypeFormat, nameof(DateSpanOffset)), nameof(obj));

        return CompareTo(other);
    }

    /// <summary>
    /// Compares the value of this instance to a specified <see cref="DateSpanOffset"/> value and returns an integer
    /// that indicates whether this instance is earlier than, the same as, or later than the specified
    /// <see cref="DateSpanOffset"/> value.
    /// </summary>
    /// <remarks>
    /// This method compares <see cref="DateSpanOffset"/> objects by comparing their <see cref="UtcDateSpan"/> values.
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
    public int CompareTo(DateSpanOffset other)
    {
        int cmp = Start.CompareTo(other.Start);
        return cmp == 0 ? Duration.CompareTo(other.Duration) : cmp;
    }

    /// <summary>
    /// Determines whether the <see cref="DateSpanOffset"/> contains a specific <paramref name="value"/>.
    /// </summary>
    /// <remarks>
    /// This method compares the endpoint <see cref="DateTimeOffset"/> objects by comparing their
    /// <see cref="DateTimeOffset.UtcDateTime"/> values.
    /// </remarks>
    /// <param name="value">The <see cref="DateTimeOffset"/> to locate in the <see cref="DateSpanOffset"/>.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is within the
    /// interval; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Contains(DateTimeOffset value)
        => value >= Start && value < End;

    /// <summary>
    /// Determines whether the <see cref="DateSpanOffset"/> contains another <see cref="DateSpanOffset"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method compares <see cref="DateSpanOffset"/> objects by comparing their <see cref="UtcDateSpan"/> values.
    /// </para>
    /// <para>All time intervals contain the <see cref="Empty"/> interval.</para>
    /// </remarks>
    /// <param name="other">Another <see cref="DateSpanOffset"/> instance.</param>
    /// <returns>
    /// <see langword="true"/> if the values of the <see cref="Start"/> and <see cref="End"/> properties
    /// of the <paramref name="other"/> interval are within the values of the <see cref="Start"/> and
    /// <see cref="End"/> properties of the current instance; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Contains(DateSpanOffset other)
        => other.Duration == TimeSpan.Zero || (other.Start >= Start && other.End <= End);

    /// <summary>
    /// Returns a value indicating whether this instance is equal to a specified object.
    /// </summary>
    /// <remarks>
    /// This method compares <see cref="DateSpanOffset"/> objects by comparing their <see cref="UtcDateSpan"/> values.
    /// </remarks>
    /// <param name="obj">The object to compare to this instance.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="obj"/> is an instance of <see cref="DateSpanOffset"/> and
    /// equals the value of this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is DateSpanOffset other && Equals(other);

    /// <summary>
    /// Returns a value indicating whether the value of this instance is equal to the value of the
    /// specified <see cref="DateSpanOffset"/> instance.
    /// </summary>
    /// <remarks>
    /// This method compares <see cref="DateSpanOffset"/> objects by comparing their <see cref="UtcDateSpan"/> values.
    /// </remarks>
    /// <param name="other">The object to compare to this instance.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="other"/> parameter equals the
    /// value of this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(DateSpanOffset other)
        => Start.Equals(other.Start) && Duration.Equals(other.Duration);

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode()
        => Start.GetHashCode() ^ Duration.GetHashCode();

    /// <summary>
    /// Returns a new <see cref="DateSpanOffset"/> instance that represents the intersection of
    /// this <see cref="DateSpanOffset"/> instance with the <paramref name="other"/> instance.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method compares <see cref="DateSpanOffset"/> objects by comparing their <see cref="UtcDateSpan"/> values.
    /// </para>
    /// <para>
    /// In the event that the current instance and the <paramref name="other"/> instance have different
    /// offsets from Coordinated Universal Time (UTC), the resulting value, if not empty,
    /// uses the value of the offset from the later <see cref="DateSpanOffset"/> instance.
    /// </para>
    /// </remarks>
    /// <param name="other">The <see cref="DateSpanOffset"/> to compare to the current interval.</param>
    /// <returns>
    /// The intersection of the intervals if they overlap; otherwise, the default value.
    /// </returns>
    public DateSpanOffset IntersectWith(DateSpanOffset other)
    {
        if (Duration == TimeSpan.Zero || other.Duration == TimeSpan.Zero)
            return default;

        (DateSpanOffset first, DateSpanOffset second) = GetAscendingOrder(this, other);

        long maxIntersection = (first.End - second.Start).Ticks;
        return maxIntersection > 0
            ? new DateSpanOffset(second.Start, Math.Min(second.Duration.Ticks, maxIntersection))
            : default;
    }

    /// <summary>
    /// Determines whether the current interval overlaps with the specified interval.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method compares <see cref="DateSpanOffset"/> objects by comparing their <see cref="UtcDateSpan"/> values.
    /// </para>
    /// <para>The <see cref="Empty"/> interval does not overlap with any other interval.</para>
    /// </remarks>
    /// <param name="other">The interval to compare to the current interval.</param>
    /// <returns>
    /// <see langword="true"/> if there is at least one instant of time that exists in both
    /// this and the <paramref name="other"/> interval; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Overlaps(DateSpanOffset other)
    {
        if (Duration == TimeSpan.Zero || other.Duration == TimeSpan.Zero)
            return false;

        (DateSpanOffset first, DateSpanOffset second) = GetAscendingOrder(this, other);
        return first.Contains(second.Start);
    }

    // TODO: Write ToString and Parse/ParseExact
    // TODO: Should DateSpanOffset have methods for extending or shifting the interval?

    /// <summary>
    /// Returns a <see cref="DateSpanOffset"/> that represents the given instant in time.
    /// </summary>
    /// <remarks>
    /// The resulting interval only encompasses the given value, and as such its
    /// <see cref="Duration"/> is always 1 tick or 100-nanoseconds.
    /// </remarks>
    /// <param name="value">The <see cref="DateTimeOffset"/> value.</param>
    /// <returns>An object that represents the <paramref name="value"/>.</returns>
    public static DateSpanOffset FromDateTimeOffset(DateTimeOffset value)
        => new DateSpanOffset(value, 1);

    /// <summary>
    /// Returns a <see cref="DateSpanOffset"/> that represents the given day in a particular month and year
    /// offset from Coordinated Universal Time (UTC).
    /// </summary>
    /// <param name="year">The year (1 through 9999).</param>
    /// <param name="month">The month (1 through 12).</param>
    /// <param name="day">The day (1 through the number of days in <paramref name="month"/>).</param>
    /// <param name="offset">The offset from Coordinated Universal Time (UTC) for the start and end.</param>
    /// <returns>
    /// An object that represents the <paramref name="year"/>, <paramref name="month"/>, and <paramref name="day"/>
    /// offset from Coordinated Universal Time (UTC).
    /// </returns>
    /// <exception cref="ArgumentException"><paramref name="offset"/> is not specified in whole minutes.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="year"/> is less than 1 or greater than 9999.</para>
    /// <para>-or-</para>
    /// <para><paramref name="month"/> is less than 1 or greater than 12.</para>
    /// <para>-or-</para>
    /// <para><paramref name="day"/> is less than 1 or greater than the number of days in <paramref name="month"/>.</para>
    /// <para>-or-</para>
    /// <para><paramref name="offset"/> is less than -14 hours or greater than 14 hours.</para>
    /// </exception>
    public static DateSpanOffset FromDay(int year, int month, int day, TimeSpan offset)
    {
        DateTimeOffset start = new DateTimeOffset(year, month, day, 0, 0, 0, 0, offset);
        return new DateSpanOffset(start, TimeSpan.FromDays(1));
    }

    /// <summary>
    /// Returns a <see cref="DateSpanOffset"/> that represents the given day in a particular month and year
    /// offset from Coordinated Universal Time (UTC) for a specified <paramref name="calendar"/>.
    /// </summary>
    /// <param name="year">The year (1 through the number of years in <paramref name="calendar"/>).</param>
    /// <param name="month">The month (1 through the number of months in <paramref name="calendar"/>).</param>
    /// <param name="day">The day (1 through the number of days in <paramref name="month"/>).</param>
    /// <param name="calendar">
    /// The calendar that is used to interpret <paramref name="year"/>, <paramref name="month"/>,
    /// and <paramref name="day"/>.
    /// </param>
    /// <param name="offset">The offset from Coordinated Universal Time (UTC) for the start and end.</param>
    /// <returns>
    /// An object that represents the <paramref name="year"/>, <paramref name="month"/>, and <paramref name="day"/>
    /// offset from Coordinated Universal Time (UTC) for a specified <paramref name="calendar"/>.
    /// </returns>
    /// <exception cref="ArgumentException"><paramref name="offset"/> is not specified in whole minutes.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="calendar"/> cannot be <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="year"/> is not in the range supported by <paramref name="calendar"/>.</para>
    /// <para>-or-</para>
    /// <para><paramref name="month"/> is less than 1 or greater than the number of months in <paramref name="calendar"/>.</para>
    /// <para>-or-</para>
    /// <para><paramref name="day"/> is less than 1 or greater than the number of days in <paramref name="month"/>.</para>
    /// <para>-or-</para>
    /// <para>
    /// The <paramref name="year"/>, <paramref name="month"/>, and <paramref name="day"/>
    /// parameters cannot be represented as a date and time value.
    /// </para>
    /// <para>-or-</para>
    /// <para>
    /// The resulting value for the <see cref="Start"/> or <see cref="End"/> property is less than
    /// <see cref="DateTime.MinValue"/> or greater than <see cref="DateTime.MaxValue"/> when converted
    /// to Coordinated Universal Time (UTC) via the <see cref="DateTimeOffset.UtcDateTime"/> property.
    /// </para>
    /// </exception>
    public static DateSpanOffset FromDay(int year, int month, int day, Calendar calendar, TimeSpan offset)
    {
        DateTimeOffset start = new DateTimeOffset(year, month, day, 0, 0, 0, 0, calendar, offset);
        return new DateSpanOffset(start, TimeSpan.FromDays(1));
    }

    /// <summary>
    /// Returns a <see cref="DateSpanOffset"/> that represents the given month in a particular year
    /// offset from Coordinated Universal Time (UTC).
    /// </summary>
    /// <param name="year">The year (1 through 9999).</param>
    /// <param name="month">The month (1 through 12).</param>
    /// <param name="offset">The offset from Coordinated Universal Time (UTC) for the start and end.</param>
    /// <returns>
    /// An object that represents the <paramref name="year"/> and <paramref name="month"/>
    /// offset from Coordinated Universal Time (UTC).
    /// </returns>
    /// <exception cref="ArgumentException"><paramref name="offset"/> is not specified in whole minutes.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="year"/> is less than 1 or greater than 9999.</para>
    /// <para>-or-</para>
    /// <para><paramref name="month"/> is less than 1 or greater than 12.</para>
    /// <para>-or-</para>
    /// <para><paramref name="offset"/> is less than -14 hours or greater than 14 hours.</para>
    /// <para>-or-</para>
    /// <para>
    /// The resulting value for the <see cref="Start"/> or <see cref="End"/> property is less than
    /// <see cref="DateTime.MinValue"/> or greater than <see cref="DateTime.MaxValue"/> when converted
    /// to Coordinated Universal Time (UTC) via the <see cref="DateTimeOffset.UtcDateTime"/> property.
    /// </para>
    /// </exception>
    public static DateSpanOffset FromMonth(int year, int month, TimeSpan offset)
    {
        DateTimeOffset start = new DateTimeOffset(year, month, 1, 0, 0, 0, 0, offset);
        return new DateSpanOffset(start, start.AddMonths(1));
    }

    /// <summary>
    /// Returns a <see cref="DateSpanOffset"/> that represents the given month in a particular year
    /// offset from Coordinated Universal Time (UTC) for a specified <paramref name="calendar"/>.
    /// </summary>
    /// <param name="year">The  year (1 through the number of years in <paramref name="calendar"/>).</param>
    /// <param name="month">The month (1 through the number of months in <paramref name="calendar"/>).</param>
    /// <param name="calendar">
    /// The calendar that is used to interpret <paramref name="year"/> and <paramref name="month"/>.
    /// </param>
    /// <param name="offset">The offset from Coordinated Universal Time (UTC) for the start and end.</param>
    /// <returns>
    /// An object that represents the <paramref name="year"/> and <paramref name="month"/>
    /// offset from Coordinated Universal Time (UTC) for a specified <paramref name="calendar"/>.
    /// </returns>
    /// <exception cref="ArgumentException"><paramref name="offset"/> is not specified in whole minutes.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="calendar"/> cannot be <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="year"/> is not in the range supported by <paramref name="calendar"/>.</para>
    /// <para>-or-</para>
    /// <para><paramref name="month"/> is less than 1 or greater than the number of months in <paramref name="calendar"/>.</para>
    /// <para>-or-</para>
    /// <para>
    /// The <paramref name="year"/> and <paramref name="month"/>
    /// parameters cannot be represented as a date and time value.
    /// </para>
    /// <para>-or-</para>
    /// <para>
    /// The resulting value for the <see cref="Start"/> or <see cref="End"/> property is less than
    /// <see cref="DateTime.MinValue"/> or greater than <see cref="DateTime.MaxValue"/> when converted
    /// to Coordinated Universal Time (UTC) via the <see cref="DateTimeOffset.UtcDateTime"/> property.
    /// </para>
    /// </exception>
    public static DateSpanOffset FromMonth(int year, int month, Calendar calendar, TimeSpan offset)
    {
        DateTimeOffset start = new DateTimeOffset(year, month, 1, 0, 0, 0, 0, calendar, offset);
        return new DateSpanOffset(start, start.AddMonths(1));
    }

    /// <summary>
    /// Returns a <see cref="DateSpanOffset"/> that represents the given year
    /// offset from Coordinated Universal Time (UTC).
    /// </summary>
    /// <param name="year">The year (1 through 9999).</param>
    /// <param name="offset">The offset from Coordinated Universal Time (UTC) for the start and end.</param>
    /// <returns>
    /// An object that represents the <paramref name="year"/> offset from Coordinated Universal Time (UTC).
    /// </returns>
    /// <exception cref="ArgumentException"><paramref name="offset"/> is not specified in whole minutes.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="year"/> is less than 1 or greater than 9999.</para>
    /// <para>-or-</para>
    /// <para><paramref name="offset"/> is less than -14 hours or greater than 14 hours.</para>
    /// </exception>
    public static DateSpanOffset FromYear(int year, TimeSpan offset)
    {
        DateTimeOffset start = new DateTimeOffset(year, 1, 1, 0, 0, 0, 0, offset);
        return new DateSpanOffset(start, start.AddYears(1));
    }

    /// <summary>
    /// Returns a <see cref="DateSpanOffset"/> that represents the given year
    /// offset from Coordinated Universal Time (UTC) for a specified <paramref name="calendar"/>.
    /// </summary>
    /// <param name="year">The year (1 through the number of years in <paramref name="calendar"/>).</param>
    /// <param name="calendar">The calendar that is used to interpret <paramref name="year"/>.</param>
    /// <param name="offset">The offset from Coordinated Universal Time (UTC) for the start and end.</param>
    /// <returns>
    /// An object that represents the <paramref name="year"/>
    /// offset from Coordinated Universal Time (UTC) for a specified <paramref name="calendar"/>.
    /// </returns>
    /// <exception cref="ArgumentException"><paramref name="offset"/> is not specified in whole minutes.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="calendar"/> cannot be <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="year"/> is not in the range supported by <paramref name="calendar"/>.</para>
    /// <para>-or-</para>
    /// <para>
    /// The <paramref name="year"/> parameter cannot be represented as a date and time value.
    /// </para>
    /// <para>-or-</para>
    /// <para>
    /// The resulting value for the <see cref="Start"/> or <see cref="End"/> property is less than
    /// <see cref="DateTime.MinValue"/> or greater than <see cref="DateTime.MaxValue"/> when converted
    /// to Coordinated Universal Time (UTC) via the <see cref="DateTimeOffset.UtcDateTime"/> property.
    /// </para>
    /// </exception>
    public static DateSpanOffset FromYear(int year, Calendar calendar, TimeSpan offset)
    {
        DateTimeOffset start = new DateTimeOffset(year, 1, 1, 0, 0, 0, 0, calendar, offset);
        return new DateSpanOffset(start, start.AddYears(1));
    }

    /// <summary>
    /// Determines whether two specified instances of <see cref="DateSpanOffset"/> are equal.
    /// </summary>
    /// <remarks>
    /// This method compares <see cref="DateSpanOffset"/> objects by comparing their <see cref="UtcDateSpan"/> values.
    /// </remarks>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> and <paramref name="right"/>
    /// represent the interval; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(DateSpanOffset left, DateSpanOffset right)
        => left.Equals(right);

    /// <summary>
    /// Determines whether two specified instances of <see cref="DateSpanOffset"/> are not equal.
    /// </summary>
    /// <remarks>
    /// This method compares <see cref="DateSpanOffset"/> objects by comparing their <see cref="UtcDateSpan"/> values.
    /// </remarks>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> and <paramref name="right"/>
    /// do not represent the interval; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(DateSpanOffset left, DateSpanOffset right)
        => !left.Equals(right);

    /// <summary>
    /// Determines whether one specified <see cref="DateSpanOffset"/> is earlier than another specified <see cref="DateSpanOffset"/>.
    /// </summary>
    /// <remarks>
    /// This method compares <see cref="DateSpanOffset"/> objects by comparing their <see cref="UtcDateSpan"/> values.
    /// </remarks>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is earlier than <paramref name="right"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <(DateSpanOffset left, DateSpanOffset right)
        => left.CompareTo(right) < 0;

    /// <summary>
    /// Determines whether one specified <see cref="DateSpanOffset"/> represents an interval that is the same as or
    /// earlier than another specified <see cref="DateSpanOffset"/>.
    /// </summary>
    /// <remarks>
    /// This method compares <see cref="DateSpanOffset"/> objects by comparing their <see cref="UtcDateSpan"/> values.
    /// </remarks>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is the same as or earlier than <paramref name="right"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <=(DateSpanOffset left, DateSpanOffset right)
        => left.CompareTo(right) <= 0;

    /// <summary>
    /// Determines whether one specified <see cref="DateSpanOffset"/> is later than another specified <see cref="DateSpanOffset"/>.
    /// </summary>
    /// <remarks>
    /// This method compares <see cref="DateSpanOffset"/> objects by comparing their <see cref="UtcDateSpan"/> values.
    /// </remarks>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is later than <paramref name="right"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >(DateSpanOffset left, DateSpanOffset right)
        => left.CompareTo(right) > 0;

    /// <summary>
    /// Determines whether one specified <see cref="DateSpanOffset"/> represents an interval that is the same as or
    /// later than another specified <see cref="DateSpanOffset"/>.
    /// </summary>
    /// <remarks>
    /// This method compares <see cref="DateSpanOffset"/> objects by comparing their <see cref="UtcDateSpan"/> values.
    /// </remarks>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is the same as or later than <paramref name="right"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >=(DateSpanOffset left, DateSpanOffset right)
        => left.CompareTo(right) >= 0;

    /// <summary>
    /// Defines an explicit conversion of a <see cref="DateTimeOffset"/> instance to an equivalent
    /// <see cref="DateSpanOffset"/> instance that represents the given instant in time.
    /// </summary>
    /// <remarks>
    /// The resulting interval only encompasses the given value, and as such its
    /// <see cref="Duration"/> is always 1 tick or 100-nanoseconds.
    /// </remarks>
    /// <param name="value">A <see cref="DateTimeOffset"/> value.</param>
    /// <returns>A converted object that represents the <paramref name="value"/>.</returns>
    public static explicit operator DateSpanOffset(DateTimeOffset value)
        => FromDateTimeOffset(value);

    /// <summary>
    /// Defines an implicit conversion of a <see cref="Sweetener.DateSpan"/> instance to an equivalent
    /// <see cref="DateSpanOffset"/> instance.
    /// </summary>
    /// <remarks>
    /// The behavior of the implicit conversion is equivalent to the
    /// <see cref="DateSpanOffset(DateSpan)"/> constructor.
    /// </remarks>
    /// <param name="value">A <see cref="Sweetener.DateSpan"/> value.</param>
    /// <returns>A converted object that represents the <paramref name="value"/>.</returns>
    [SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "Constructor provides alternative.")]
    public static implicit operator DateSpanOffset(DateSpan value)
        => new DateSpanOffset(value);

    private static (DateSpanOffset First, DateSpanOffset Second) GetAscendingOrder(DateSpanOffset x, DateSpanOffset y)
        // Order by smallest start to help with other methods like Overlaps.
        // If they are equivalent, then the order doesn't matter
        => x.Start <= y.Start ? (x, y) : (y, x);
}
