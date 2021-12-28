// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;

namespace Sweetener;

/// <summary>
/// Represents an abstraction for retrieving the current date and time.
/// </summary>
public interface IClock
{
    /// <inheritdoc cref="DateTimeOffset.Now"/>
    DateTimeOffset Now { get; }

    /// <inheritdoc cref="DateTimeOffset.UtcNow"/>
    DateTimeOffset UtcNow { get; }
}
