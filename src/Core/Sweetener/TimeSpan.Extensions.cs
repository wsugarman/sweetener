// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;

namespace Sweetener;

/// <summary>
/// Provides a set of additional methods for <see cref="TimeSpan"/> structures.
/// </summary>
public static class TimeSpanExtensions
{
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
    {
        if (granularity < TimeSpan.Zero)
            throw new ArgumentNegativeException(nameof(granularity));

        return granularity == TimeSpan.Zero ? value : new TimeSpan(value.Ticks - (value.Ticks % granularity.Ticks));
    }
}
