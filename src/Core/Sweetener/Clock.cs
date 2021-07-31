// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;

namespace Sweetener
{
    /// <summary>
    /// Provides a set of <see langword="static"/> utilities for creating <see cref="IClock"/> objects.
    /// </summary>
    public static class Clock
    {
        /// <summary>
        /// Gets a clock based on this computer.
        /// </summary>
        /// <value>The system <see cref="Clock"/> for this computer.</value>
        public static IClock System { get; } = new SystemClock();

        /// <summary>
        /// Returns a new clock whose value is the specified local date and time.
        /// </summary>
        /// <remarks>
        /// If the specified <see cref="DateTime.Kind"/> is <see cref="DateTimeKind.Unspecified"/>, the value is
        /// assumed to be a local date and time.
        /// </remarks>
        /// <param name="value">A date and time.</param>
        /// <returns>An <see cref="IClock"/> whose current date and time is the specified value.</returns>
        public static IClock FromLocal(DateTime value)
            => new FixedClock(value.ToUniversalTime());

        /// <summary>
        /// Returns a new clock whose value is the specified Coordinated Universal Time (UTC) date and time.
        /// </summary>
        /// <remarks>
        /// If the specified <see cref="DateTime.Kind"/> is <see cref="DateTimeKind.Unspecified"/>, the value is
        /// assumed to be a UTC date and time.
        /// </remarks>
        /// <param name="value">A date and time.</param>
        /// <returns>An <see cref="IClock"/> whose current date and time is the specified value.</returns>
        public static IClock FromUtc(DateTime value)
            => new FixedClock(value.Kind == DateTimeKind.Local ? value.ToUniversalTime() : value);

        #region System Clock

        private sealed class SystemClock : IClock
        {
            public DateTime Now => DateTime.Now;

            public DateTime UtcNow => DateTime.UtcNow;

            public SystemClock()
            { }
        }

        #endregion

        #region Fixed Clock

        private sealed class FixedClock : IClock
        {
            public DateTime Now => UtcNow.ToLocalTime();

            public DateTime UtcNow { get; private set; }

            public FixedClock(DateTime utcNow)
                => UtcNow = utcNow;
        }

        #endregion
    }
}
