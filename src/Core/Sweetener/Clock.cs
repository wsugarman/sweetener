// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;

namespace Sweetener;

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
    /// Returns a new clock whose value is the specified date and time.
    /// </summary>
    /// <param name="value">A date and time.</param>
    /// <returns>An <see cref="IClock"/> whose current date and time is the specified value.</returns>
    public static IClock From(DateTimeOffset value)
        => new FixedClock(value.ToUniversalTime());

    #region System Clock

    private sealed class SystemClock : IClock
    {
        public DateTimeOffset Now => DateTimeOffset.Now;

        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;

        public SystemClock()
        { }
    }

    #endregion

    #region Fixed Clock

    private sealed class FixedClock : IClock
    {
        public DateTimeOffset Now => UtcNow.ToLocalTime();

        public DateTimeOffset UtcNow { get; private set; }

        public FixedClock(DateTimeOffset utcNow)
            => UtcNow = utcNow;
    }

    #endregion
}
