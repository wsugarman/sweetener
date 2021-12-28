// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Sweetener.Linq;

static partial class Collection
{
    /// <summary>
    /// Generates a collection that contains one repeated value.
    /// </summary>
    /// <typeparam name="TResult">The type of the value to be repeated in the result collection.</typeparam>
    /// <param name="element">The value to be repeated.</param>
    /// <param name="count">The number of times to repeat the value in the generated collection.</param>
    /// <returns>An <see cref="IReadOnlyCollection{T}"/> that contains a repeated value.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than 0.</exception>
    public static IReadOnlyCollection<TResult> Repeat<TResult>(TResult element, int count)
        => count == 0 ? Array.Empty<TResult>() : new ReadOnlyCollection<TResult>(Enumerable.Repeat(element, count), count);
}
