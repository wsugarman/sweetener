// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace Sweetener.Linq;

public static partial class Collection
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

    private sealed class ConcatenatedCollection<T> : DecoratorCollection<T>
    {
        public override int Count => Source1.Count + Source2.Count;

        public override IEnumerable<T> Enumerable { get; }

        public IReadOnlyCollection<T> Source1 { get; }

        public IReadOnlyCollection<T> Source2 { get; }

        public ConcatenatedCollection(IReadOnlyCollection<T> first, IReadOnlyCollection<T> second)
        {
            Enumerable = Collection.EnumerableDecorator.Concat(first, second);
            Source1 = first;
            Source2 = second;
        }
    }
}
