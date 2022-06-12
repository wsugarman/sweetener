// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Sweetener.Linq;

static partial class Collection
{
    /// <summary>
    /// Generates a collection of integral numbers within a specified range.
    /// </summary>
    /// <param name="start">The value of the first integer in the collection.</param>
    /// <param name="count">The number of sequential integers to generate.</param>
    /// <returns>
    /// An <see cref="IReadOnlyCollection{T}"/> that contains a range of sequential integral numbers.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <para><paramref name="count"/> is less than 0.</para>
    /// <para>-or-</para>
    /// <para><paramref name="start"/> + <paramref name="count"/> -1 is larger than <see cref="int.MaxValue"/>.</para>
    /// </exception>
    public static IReadOnlyCollection<int> Range(int start, int count)
        => count == 0 ? Array.Empty<int>() : new ImmutableDecoratorCollection<int>(Enumerable.Range(start, count), count);
}
