// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace Sweetener.Linq;

static partial class Collection
{
    /// <summary>
    /// Bypasses a specified number of elements in a collection and then returns the remaining elements.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <param name="source">An <see cref="IReadOnlyCollection{T}"/> to return elements from.</param>
    /// <param name="count">The number of elements to skip before returning the remaining elements.</param>
    /// <returns>
    /// An <see cref="IReadOnlyCollection{T}"/> that contains the elements that occur after
    /// the specified index in the input collection.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
    public static IReadOnlyCollection<TSource> Skip<TSource>(this IReadOnlyCollection<TSource> source, int count)
        => new SkipCollection<TSource>(source, EnumerableDecorator.Skip(source, count), count);

#if NETCOREAPP2_0_OR_GREATER
    /// <summary>
    /// Returns a new collection that contains the elements from <paramref name="source"/> with the last
    /// <paramref name="count"/> elements of the source collection omitted.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements in the collection.</typeparam>
    /// <param name="source">A collection instance.</param>
    /// <param name="count">The number of elements to omit from the end of the collection.</param>
    /// <returns>
    /// A new collection that contains the elements from <paramref name="source"/> minus <paramref name="count"/>
    /// elements from the end of the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
    public static IReadOnlyCollection<TSource> SkipLast<TSource>(this IReadOnlyCollection<TSource> source, int count)
        => new SkipCollection<TSource>(source, EnumerableDecorator.SkipLast(source, count), count);
#endif

    private sealed class SkipCollection<T> : DecoratorTransformationCollection<T>
    {
        public override int Count => Math.Max(0, Source.Count - _skip);

        private readonly int _skip;

        public SkipCollection(IReadOnlyCollection<T> source, IEnumerable<T> transformation, int skip)
            : base(source, transformation)
            => _skip = skip <= 0 ? 0 : skip;
    }
}
