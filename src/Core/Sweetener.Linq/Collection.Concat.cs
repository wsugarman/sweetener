// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace Sweetener.Linq;

static partial class Collection
{
    /// <summary>
    /// Concatenates two collections.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of the input collections.</typeparam>
    /// <param name="first">The first collection to concatenate.</param>
    /// <param name="second">The collection to concatenate to the first sequence.</param>
    /// <returns>
    /// An <see cref="IReadOnlyCollection{T}"/> that contains the concatenated elements of the two input collections.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="first"/> or <paramref name="second"/> <see langword="null"/>.
    /// </exception>
    public static IReadOnlyCollection<TSource> Concat<TSource>(this IReadOnlyCollection<TSource> first, IReadOnlyCollection<TSource> second)
        => new ConcatenatedCollection<TSource>(first, second);

    private sealed class ConcatenatedCollection<T> : MutableDecoratorCollection<T>
    {
        public override int Count => Source.Count + _source2.Count;

        private readonly IReadOnlyCollection<T> _source2;

        public ConcatenatedCollection(IReadOnlyCollection<T> first, IReadOnlyCollection<T> second)
            : base(first, Enumerable.Concat(first, second))
            => _source2 = second;
    }
}
