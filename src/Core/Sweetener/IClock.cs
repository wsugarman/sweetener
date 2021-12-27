// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;

namespace Sweetener;

/// <summary>
/// Represents an abstraction for retrieving the current date and time.
/// </summary>
public interface IClock
{
    /// <summary>
    /// Gets a <see cref="DateTime"/> object that is set to the current date and time, expressed as the local time.
    /// </summary>
    /// <value>An object whose value is the current local date and time.</value>
    DateTime Now { get; }

    /// <summary>
    /// Gets a <see cref="DateTime"/> object that is set to the current date and time, expressed as the
    /// Coordinated Universal Time (UTC).
    /// </summary>
    /// <value>An object whose value is the current UTC date and time.</value>
    DateTime UtcNow { get; }
}
