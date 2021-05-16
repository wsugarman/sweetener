// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Sweetener
{
    /// <summary>
    /// Represents an interval between two instants in time, typically expressed as a date and time of day.
    /// </summary>
    [Serializable]
    [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
    internal readonly struct DateSpan : IComparable, IComparable<DateSpan>, IEquatable<DateSpan>
    {
        /// <summary>
        /// Gets a value that indicates whether the interval represented by this instance is based on local time,
        /// Coordinated Universal Time (UTC), or neither.
        /// </summary>
        /// <value>
        /// One of the enumeration values that indicates what the current time represents.
        /// The default is <see cref="DateTimeKind.Unspecified"/>.
        /// </value>
        public DateTimeKind Kind => _start.Kind;

        /// <summary>
        /// Gets the instant in time that starts the interval represented by this instance.
        /// </summary>
        /// <value>The <see cref="DateTime"/> that indicates the inclusive start of this interval.</value>
        /// <exception cref="InvalidOperationException">The interval is empty.</exception>
        public DateTime Start => IsEmpty ? throw new InvalidOperationException(SR.EmptyDateSpanMessage) : _start;

        /// <summary>
        /// Gets the instant in time that ends the interval represented by this instance.
        /// </summary>
        /// <value>The <see cref="DateTime"/> that indicates the exclusive end of this interval.</value>
        /// <exception cref="InvalidOperationException">
        /// <para>The interval is empty.</para>
        /// <para>-or-</para>
        /// <para>The value is greater than <see cref="DateTime.MaxValue"/>.</para>
        /// </exception>
        public DateTime End
        {
            get
            {
                if (IsEmpty)
                    throw new InvalidOperationException(SR.EmptyDateSpanMessage);

                long endTicks = _start.Ticks + _durationTicks;
                if (endTicks - 1 == MaxDateTimeTicks)
                    throw new InvalidOperationException(SR.InvalidDateSpanEndMessage);

                return new DateTime(endTicks, Kind);
            }
        }

        /// <summary>
        /// Gets the length of time represented by this instance irrespective of when it starts and ends.
        /// This value is always non-negative.
        /// </summary>
        /// <value>The <see cref="TimeSpan"/> that indicates the length of time.</value>
        public TimeSpan Duration => new TimeSpan(_durationTicks);

        /// <summary>
        /// Gets a value that indicates whether the interval does not contain any instant of time.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the <see cref="Duration"/> property returns <see cref="TimeSpan.Zero"/>;
        /// otherwise <see langword="false"/>.
        /// </value>
        public bool IsEmpty => _durationTicks == 0;

        private readonly DateTime _start;
        private readonly long _durationTicks;

        // Must be resolved before MaxValue and MinValue
        private static readonly long MaxDateTimeTicks = DateTime.MaxValue.Ticks;

        /// <summary>
        /// Represents the largest possible value of <see cref="DateSpan"/>. This field is read-only.
        /// </summary>
        public static readonly DateSpan MaxValue = new DateSpan(DateTime.MinValue, (DateTime.MaxValue - DateTime.MinValue).Ticks + 1L);

        /// <summary>
        /// Represents the smallest possible value of <see cref="DateSpan"/>. This field is read-only.
        /// </summary>
        /// <remarks>
        /// This value is equivalent to the default value for <see cref="DateSpan"/>. The value of the
        /// <see cref="Duration"/> property is <see cref="TimeSpan.Zero"/> and the <see cref="IsEmpty"/>
        /// property returns <see langword="true"/>.
        /// </remarks>
        public static readonly DateSpan MinValue = new DateSpan(DateTime.MinValue, 0L);

        /// <summary>
        /// Initializes a new instance of the <see cref="DateSpan"/> structure that represents the interval
        /// between the specified <paramref name="start"/> and <paramref name="end"/> times.
        /// </summary>
        /// <param name="start">The inclusive start of the interval.</param>
        /// <param name="end">The exclusive end of the interval.</param>
        /// <exception cref="ArgumentException">
        /// <para>
        /// <paramref name="start"/> and <paramref name="end"/> do not have the same <see cref="DateTime.Kind"/> value.
        /// </para>
        /// <para>-or-</para>
        /// <para><paramref name="end"/> takes place before <paramref name="start"/>.</para>
        /// </exception>
        public DateSpan(DateTime start, DateTime end)
        {
            if (start.Kind != end.Kind)
                throw new ArgumentException(SR.KindMismatchMessage);

            long durationTicks = (end - start).Ticks;
            if (durationTicks < 0)
            {
                throw new ArgumentException(SR.EndBeforeStartMessage, nameof(end));
            }

            _start = durationTicks == 0 ? DateTime.MinValue : start; // normalize empty span
            _durationTicks = durationTicks;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateSpan"/> structure that represents the interval
        /// starting from the given instant of time for the given duration.
        /// </summary>
        /// <remarks>
        /// If the <paramref name="duration"/> is less than <see cref="TimeSpan.Zero"/>, the value of the
        /// <see cref="Start"/> property will be shifted such that the resulting value of the <see cref="Duration"/>
        /// property is positive.
        /// </remarks>
        /// <param name="start">The inclusive start of the interval.</param>
        /// <param name="duration">The length of the interval.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The interval extends beyond the range of valid <see cref="DateTime"/> values.
        /// </exception>
        public DateSpan(DateTime start, TimeSpan duration)
            : this(start, duration.Ticks)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateSpan"/> structure that represents the interval
        /// starting from the given instant of time for the given duration.
        /// </summary>
        /// <remarks>
        /// If the <paramref name="durationTicks"/> is less than <c>0</c>, the value of the
        /// <see cref="Start"/> property will be shifted such that the resulting value of the <see cref="Duration"/>
        /// property is positive.
        /// </remarks>
        /// <param name="start">The inclusive start of the interval.</param>
        /// <param name="durationTicks">
        /// The length of the interval expressed in the number of 100-nanosecond intervals that have
        /// elapsed since January 1, 0001 at 00:00:00.000 in the Gregorian calendar.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="durationTicks"/> is negative.</para>
        /// <para>-or-</para>
        /// <para>The interval extends beyond the range of valid <see cref="DateTime"/> values.</para>
        /// </exception>
        public DateSpan(DateTime start, long durationTicks)
        {
            if (durationTicks < 0)
                throw new ArgumentOutOfRangeException(nameof(durationTicks), SR.EndBeforeStartMessage);

            if (MaxDateTimeTicks - start.Ticks + 1 < durationTicks)
                throw new ArgumentOutOfRangeException(nameof(durationTicks), SR.InvalidDateSpanRangeMessage);

            _start = durationTicks == 0 ? DateTime.MinValue : start; // normalize empty span
            _durationTicks = durationTicks;
        }

        /// <summary>
        /// Compares the value of this instance to a specified object that contains a specified <see cref="DateSpan"/>
        /// value, and returns an integer that indicates whether this instance is earlier than, the same as, or later
        /// than the specified <see cref="DateSpan"/> value.
        /// </summary>
        /// <remarks>
        /// To determine the relationship of the current instance to <paramref name="obj"/>, the
        /// <see cref="CompareTo(object)"/> method ignores the Kind property. Before comparing
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
        public int CompareTo(object obj)
        {
            if (obj is null)
                return 1;

            if (obj is not DateSpan other)
                throw new ArgumentException("Argument must be a DateSpan");

            return CompareTo(other);
        }

        /// <summary>
        /// Compares the value of this instance to a specified <see cref="DateSpan"/> value and returns an integer
        /// that indicates whether this instance is earlier than, the same as, or later than the specified
        /// <see cref="DateSpan"/> value.
        /// </summary>
        /// <remarks>
        /// To determine the relationship of the current instance to <paramref name="other"/>, the
        /// <see cref="CompareTo(DateSpan)"/> method ignores the Kind property. Before comparing
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
            int cmp = _start.CompareTo(other._start);
            return cmp == 0 ? _durationTicks.CompareTo(other._durationTicks) : cmp;
        }

        /// <summary>
        /// Determines whether the <see cref="DateSpan"/> contains a specific <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/> to locate in the <see cref="DateSpan"/>.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> is within the
        /// interval; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Contains(DateTime value)
            => !IsEmpty && value >= _start && value.Ticks < _start.Ticks + _durationTicks;

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
        public override bool Equals(object obj)
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
            => _start.Equals(other._start) && _durationTicks.Equals(other._durationTicks);

        /// <summary>
        /// Returns a new <see cref="DateSpan"/> instance that represents the intersection of
        /// this <see cref="DateSpan"/> instance with the <paramref name="other"/> instance.
        /// </summary>
        /// <remarks>
        /// The <see cref="IntersectWith(DateSpan)"/> method does not consider the value of the
        /// <see cref="Kind"/> property of the two <see cref="DateSpan"/> values when performing the intersection.
        /// Before intersecting <see cref="DateSpan"/> objects, ensure that the objects represent intervals in the
        /// same time zone. Otherwise, the result will include the difference between time zones.
        /// </remarks>
        /// <param name="other">The <see cref="DateSpan"/> to compare to the current interval.</param>
        /// <returns>
        /// The intersection of the intervals if they overlap; otherwise <see cref="MinValue"/>.
        /// </returns>
        public DateSpan IntersectWith(DateSpan other)
        {
            if (IsEmpty || other.IsEmpty)
                return default;

            DateSpan earlier, later;
            if (_start <= other._start)
            {
                earlier = this;
                later = other;
            }
            else
            {
                earlier = other;
                later = this;
            }

            long maxIntersectionTicks = earlier._start.Ticks + earlier._durationTicks - later._start.Ticks;
            return maxIntersectionTicks >= 0
                ? new DateSpan(later._start, Math.Min(later._durationTicks, maxIntersectionTicks))
                : default;
        }

        /// <summary>
        /// Determines whether an interval is a proper (strict) subset of a specified interval.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the current interval is a proper subset of <paramref name="other"/>, <paramref name="other"/>
        /// must have at least one instant of time that the current interval does not have.
        /// </para>
        /// <para>
        /// The <see cref="IsProperSubsetOf(DateSpan)"/> method does not consider the value of the
        /// <see cref="Kind"/> property of the two <see cref="DateSpan"/> values. Before invoking the method,
        /// ensure that the objects represent intervals in the same time zone. Otherwise, the result will not account
        /// for the difference between time zones.
        /// </para>
        /// </remarks>
        /// <param name="other">The interval to compare to the current interval.</param>
        /// <returns>
        /// <see langword="true"/> if the current interval is a proper subset of <paramref name="other"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public bool IsProperSubsetOf(DateSpan other)
            => IsSubsetOf(other) && _durationTicks != other._durationTicks;

        /// <summary>
        /// Determines whether an interval is a proper (strict) superset of a specified interval.
        /// </summary>
        /// <remarks>
        /// The <see cref="IsProperSupersetOf(DateSpan)"/> method does not consider the value of the
        /// <see cref="Kind"/> property of the two <see cref="DateSpan"/> values. Before invoking the method,
        /// ensure that the objects represent intervals in the same time zone. Otherwise, the result will not account
        /// for the difference between time zones.
        /// </remarks>
        /// <param name="other">The interval to compare to the current interval.</param>
        /// <returns>
        /// <see langword="true"/> if the current interval is a proper superset of <paramref name="other"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public bool IsProperSupersetOf(DateSpan other)
            => other.IsSubsetOf(this);

        /// <summary>
        /// Determines whether an interval is a subset of a specified interval.
        /// </summary>
        /// <remarks>
        /// The <see cref="IsSubsetOf(DateSpan)"/> method does not consider the value of the
        /// <see cref="Kind"/> property of the two <see cref="DateSpan"/> values. Before invoking the method,
        /// ensure that the objects represent intervals in the same time zone. Otherwise, the result will not account
        /// for the difference between time zones.
        /// </remarks>
        /// <param name="other">The interval to compare to the current interval.</param>
        /// <returns>
        /// <see langword="true"/> if the current interval is a subset of <paramref name="other"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public bool IsSubsetOf(DateSpan other)
        {
            if (other.IsEmpty)
                return IsEmpty;

            return IsEmpty || (_start >= other._start && (_start.Ticks + _durationTicks) < (other._start.Ticks + other._durationTicks));
        }

        /// <summary>
        /// Determines whether an interval is a superset of a specified interval.
        /// </summary>
        /// <remarks>
        /// The <see cref="IsSupersetOf(DateSpan)"/> method does not consider the value of the
        /// <see cref="Kind"/> property of the two <see cref="DateSpan"/> values. Before invoking the method,
        /// ensure that the objects represent intervals in the same time zone. Otherwise, the result will not account
        /// for the difference between time zones.
        /// </remarks>
        /// <param name="other">The interval to compare to the current interval.</param>
        /// <returns>
        /// <see langword="true"/> if the current interval is a superset of <paramref name="other"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public bool IsSupersetOf(DateSpan other)
            => other.IsSubsetOf(this);

        /// <summary>
        /// Determines whether the current interval overlaps with the specified interval.
        /// </summary>
        /// <remarks>
        /// The <see cref="Overlaps(DateSpan)"/> method does not consider the value of the
        /// <see cref="Kind"/> property of the two <see cref="DateSpan"/> values. Before invoking the method,
        /// ensure that the objects represent intervals in the same time zone. Otherwise, the result will not account
        /// for the difference between time zones.
        /// </remarks>
        /// <param name="other">The interval to compare to the current interval.</param>
        /// <returns>
        /// <see langword="true"/> if there is at least one instant of time that exists in both
        /// this and the <paramref name="other"/> interval; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Overlaps(DateSpan other)
        {
            if (IsEmpty || other.IsEmpty)
                return default;

            DateSpan earlier, later;
            if (_start <= other._start)
            {
                earlier = this;
                later = other;
            }
            else
            {
                earlier = other;
                later = this;
            }

            return earlier._start.Ticks + earlier._durationTicks - later._start.Ticks > 0;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal string GetDebuggerDisplay()
        {
            if (IsEmpty)
                return "{}";

            return _durationTicks == 1
                ? $"{{{_start}}}"
                : $"[{_start}, {_start.AddTicks(_durationTicks)})";
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
            => _start.GetHashCode() ^ _durationTicks.GetHashCode();

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
            return new DateSpan(start, start.AddMonths(1));
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
        /// </exception>
        public static DateSpan FromMonth(int year, int month, DateTimeKind kind)
        {
            DateTime start = new DateTime(year, month, 1, 0, 0, 0, kind);
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
        /// One of the enumeration values that indicates whether <paramref name="year"/>,
        /// specifies a local time, Coordinated Universal Time (UTC), or neither.
        /// </param>
        /// <returns>An object that represents the <paramref name="year"/>.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="kind"/> is not one of the <see cref="DateTimeKind"/> values.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="year"/> is less than 1 or greater than 9999.
        /// </exception>
        public static DateSpan FromYear(int year, DateTimeKind kind)
        {
            // Avoid creating a DateTime object for the year 10,000 if necessary
            DateTime start = new DateTime(year, 1, 1, 0, 0, 0, kind);
            return year == DateTime.MaxValue.Year
                ? new DateSpan(start, MaxDateTimeTicks + 1L - start.Ticks)
                : new DateSpan(start, start.AddYears(1));
        }

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
    }
}
