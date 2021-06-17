// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;

namespace Sweetener
{
    /// <summary>
    /// Represents an <see cref="IClock"/> whose date and time cannot be changed.
    /// </summary>
    public sealed class FixedClock : IClock
    {
        /// <inheritdoc/>
        public DateTime Now => UtcNow.ToLocalTime();

        /// <inheritdoc/>
        public DateTime UtcNow { get; }

        private FixedClock(DateTime utcNow)
            => UtcNow = utcNow;

        /// <summary>
        /// Returns a new clock whose value is the specified local date and time.
        /// </summary>
        /// <remarks>
        /// If the specified <see cref="DateTime.Kind"/> is <see cref="DateTimeKind.Unspecified"/>, the value is
        /// assumed to be a local date and time.
        /// </remarks>
        /// <param name="value">A date and time.</param>
        /// <returns>A <see cref="FixedClock"/> whose current date and time is the specified value.</returns>
        public static FixedClock FromLocal(DateTime value)
            => new FixedClock(value.ToUniversalTime());

        /// <summary>
        /// Returns a new clock whose value is the specified Coordinated Universal Time (UTC) date and time.
        /// </summary>
        /// <remarks>
        /// If the specified <see cref="DateTime.Kind"/> is <see cref="DateTimeKind.Unspecified"/>, the value is
        /// assumed to be a UTC date and time.
        /// </remarks>
        /// <param name="value">A date and time.</param>
        /// <returns>A <see cref="FixedClock"/> whose current date and time is the specified value.</returns>
        public static FixedClock FromUtc(DateTime value)
            => new FixedClock(value.Kind == DateTimeKind.Local ? value.ToUniversalTime() : value);
    }
}
