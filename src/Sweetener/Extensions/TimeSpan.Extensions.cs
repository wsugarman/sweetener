using System;

namespace Sweetener.Extensions
{
    /// <summary>
    /// Provides a set of additional methods for <see cref="TimeSpan"/> structures.
    /// </summary>
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Returns a new <see cref="TimeSpan"/> that adds the specified number of days to the <paramref name="value"/>.
        /// </summary>
        /// <param name="value">A <see cref="TimeSpan"/> instance.</param>
        /// <param name="days">
        /// A number of whole and fractional days. The <paramref name="days"/> parameter can be negative or positive.
        /// </param>
        /// <returns>
        /// An object whose time is the sum of the <paramref name="value"/> and the specified number of <paramref name="days"/>.
        /// </returns>
        /// <exception cref="ArgumentException"><paramref name="days"/> is equal to <see cref="double.NaN"/>.</exception>
        /// <exception cref="OverflowException">
        /// <para>
        /// <paramref name="days"/> is <see cref="double.PositiveInfinity"/>.
        /// </para>
        /// <para>-or-</para>
        /// <para>
        /// <paramref name="days"/> is <see cref="double.NegativeInfinity"/>.
        /// </para>
        /// <para>-or-</para>
        /// <para>
        /// The resulting <see cref="TimeSpan"/> is less than <see cref="TimeSpan.MinValue"/> or
        /// greater than <see cref="TimeSpan.MaxValue"/>.
        /// </para>
        /// </exception>
        public static TimeSpan AddDays(this TimeSpan value, double days)
            => value.Add(TimeSpan.FromDays(days));

        /// <summary>
        /// Returns a new <see cref="TimeSpan"/> that adds the specified number of hours to the <paramref name="value"/>.
        /// </summary>
        /// <param name="value">A <see cref="TimeSpan"/> instance.</param>
        /// <param name="hours">
        /// A number of whole and fractional hours. The <paramref name="hours"/> parameter can be negative or positive.
        /// </param>
        /// <returns>
        /// An object whose time is the sum of the <paramref name="value"/> and the specified number of <paramref name="hours"/>.
        /// </returns>
        /// <exception cref="ArgumentException"><paramref name="hours"/> is equal to <see cref="double.NaN"/>.</exception>
        /// <exception cref="OverflowException">
        /// <para>
        /// <paramref name="hours"/> is <see cref="double.PositiveInfinity"/>.
        /// </para>
        /// <para>-or-</para>
        /// <para>
        /// <paramref name="hours"/> is <see cref="double.NegativeInfinity"/>.
        /// </para>
        /// <para>-or-</para>
        /// <para>
        /// The resulting <see cref="TimeSpan"/> is less than <see cref="TimeSpan.MinValue"/> or
        /// greater than <see cref="TimeSpan.MaxValue"/>.
        /// </para>
        /// </exception>
        public static TimeSpan AddHours(this TimeSpan value, double hours)
            => value.Add(TimeSpan.FromHours(hours));

        /// <summary>
        /// Returns a new <see cref="TimeSpan"/> that adds the specified number of minutes to the <paramref name="value"/>.
        /// </summary>
        /// <param name="value">A <see cref="TimeSpan"/> instance.</param>
        /// <param name="minutes">
        /// A number of whole and fractional minutes. The <paramref name="minutes"/> parameter can be negative or positive.
        /// </param>
        /// <returns>
        /// An object whose time is the sum of the <paramref name="value"/> and the specified number of <paramref name="minutes"/>.
        /// </returns>
        /// <exception cref="ArgumentException"><paramref name="minutes"/> is equal to <see cref="double.NaN"/>.</exception>
        /// <exception cref="OverflowException">
        /// <para>
        /// <paramref name="minutes"/> is <see cref="double.PositiveInfinity"/>.
        /// </para>
        /// <para>-or-</para>
        /// <para>
        /// <paramref name="minutes"/> is <see cref="double.NegativeInfinity"/>.
        /// </para>
        /// <para>-or-</para>
        /// <para>
        /// The resulting <see cref="TimeSpan"/> is less than <see cref="TimeSpan.MinValue"/> or
        /// greater than <see cref="TimeSpan.MaxValue"/>.
        /// </para>
        /// </exception>
        public static TimeSpan AddMinutes(this TimeSpan value, double minutes)
            => value.Add(TimeSpan.FromMinutes(minutes));

        /// <summary>
        /// Returns a new <see cref="TimeSpan"/> that adds the specified number of seconds to the <paramref name="value"/>.
        /// </summary>
        /// <param name="value">A <see cref="TimeSpan"/> instance.</param>
        /// <param name="seconds">
        /// A number of whole and fractional seconds. The <paramref name="seconds"/> parameter can be negative or positive.
        /// </param>
        /// <returns>
        /// An object whose time is the sum of the <paramref name="value"/> and the specified number of <paramref name="seconds"/>.
        /// </returns>
        /// <exception cref="ArgumentException"><paramref name="seconds"/> is equal to <see cref="double.NaN"/>.</exception>
        /// <exception cref="OverflowException">
        /// <para>
        /// <paramref name="seconds"/> is <see cref="double.PositiveInfinity"/>.
        /// </para>
        /// <para>-or-</para>
        /// <para>
        /// <paramref name="seconds"/> is <see cref="double.NegativeInfinity"/>.
        /// </para>
        /// <para>-or-</para>
        /// <para>
        /// The resulting <see cref="TimeSpan"/> is less than <see cref="TimeSpan.MinValue"/> or
        /// greater than <see cref="TimeSpan.MaxValue"/>.
        /// </para>
        /// </exception>
        public static TimeSpan AddSeconds(this TimeSpan value, double seconds)
            => value.Add(TimeSpan.FromSeconds(seconds));

        /// <summary>
        /// Returns a new <see cref="TimeSpan"/> that adds the specified number of milliseconds to the <paramref name="value"/>.
        /// </summary>
        /// <param name="value">A <see cref="TimeSpan"/> instance.</param>
        /// <param name="milliseconds">
        /// A number of whole and fractional milliseconds. The <paramref name="milliseconds"/> parameter can be negative or positive.
        /// </param>
        /// <returns>
        /// An object whose time is the sum of the <paramref name="value"/> and the specified number of <paramref name="milliseconds"/>.
        /// </returns>
        /// <exception cref="ArgumentException"><paramref name="milliseconds"/> is equal to <see cref="double.NaN"/>.</exception>
        /// <exception cref="OverflowException">
        /// <para>
        /// <paramref name="milliseconds"/> is <see cref="double.PositiveInfinity"/>.
        /// </para>
        /// <para>-or-</para>
        /// <para>
        /// <paramref name="milliseconds"/> is <see cref="double.NegativeInfinity"/>.
        /// </para>
        /// <para>-or-</para>
        /// <para>
        /// The resulting <see cref="TimeSpan"/> is less than <see cref="TimeSpan.MinValue"/> or
        /// greater than <see cref="TimeSpan.MaxValue"/>.
        /// </para>
        /// </exception>
        public static TimeSpan AddMilliseconds(this TimeSpan value, double milliseconds)
            => value.Add(TimeSpan.FromMilliseconds(milliseconds));

        /// <summary>
        /// Returns a new <see cref="TimeSpan"/> that adds the specified number of ticks to the <paramref name="value"/>.
        /// </summary>
        /// <param name="value">A <see cref="TimeSpan"/> instance.</param>
        /// <param name="ticks">
        /// A time period expressed in 100-nanosecond units. The <paramref name="ticks"/> parameter can be negative or positive.
        /// </param>
        /// <returns>
        /// An object whose time is the sum of the <paramref name="value"/> and the specified number of <paramref name="ticks"/>.
        /// </returns>
        /// <exception cref="OverflowException">
        /// The resulting <see cref="TimeSpan"/> is less than <see cref="TimeSpan.MinValue"/> or
        /// greater than <see cref="TimeSpan.MaxValue"/>.
        /// </exception>
        public static TimeSpan AddTicks(this TimeSpan value, long ticks)
            => value.Add(new TimeSpan(ticks));

        /// <summary>
        /// Returns a new <see cref="TimeSpan"/> whose value has been truncated to the specified <paramref name="granularity"/>.
        /// </summary>
        /// <param name="value">A <see cref="TimeSpan"/> instance to truncate.</param>
        /// <param name="granularity">
        /// A <see cref="TimeSpan"/> that represents the desired granularity for the <paramref name="value"/>.
        /// </param>
        /// <returns>
        /// An object whose value has been truncated to the <paramref name="granularity"/>.
        /// </returns>
        /// <exception cref="ArgumentNegativeException"><paramref name="value"/> cannot be negative.</exception>
        public static TimeSpan Truncate(this TimeSpan value, TimeSpan granularity)
            => granularity < TimeSpan.Zero
            ? throw new ArgumentNegativeException(nameof(granularity))
            : new TimeSpan(value.Ticks - (value.Ticks % granularity.Ticks));
    }
}
