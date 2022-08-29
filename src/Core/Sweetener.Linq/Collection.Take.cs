// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace Sweetener.Linq;

public static partial class Collection
{
    /// <summary>
    /// Returns a specified number of contiguous elements from the start of a sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <param name="source">The collection to return elements from.</param>
    /// <param name="count">The number of elements to return.</param>
    /// <returns>
    /// An <see cref="IReadOnlyCollection{T}"/> that contains the specified number of elements
    /// from the start of the input collection.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
    public static IReadOnlyCollection<TSource> Take<TSource>(this IReadOnlyCollection<TSource> source, int count)
        => new TakeCollection<TSource>(source, EnumerableDecorator.Take(source, count), count);

#if NETCOREAPP2_0_OR_GREATER
    /// <summary>
    /// Returns a new collection that contains the last <paramref name="count"/> elements from <paramref name="source"/>.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements in the collection.</typeparam>
    /// <param name="source">A collection instance.</param>
    /// <param name="count">The number of elements to take from the end of the collection.</param>
    /// <returns>
    /// A new collection that contains the last <paramref name="count"/> elements from <paramref name="source"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
    public static IReadOnlyCollection<TSource> TakeLast<TSource>(this IReadOnlyCollection<TSource> source, int count)
        => new TakeCollection<TSource>(source, EnumerableDecorator.TakeLast(source, count), count);
#endif

    private sealed class TakeCollection<T> : DecoratorTransformationCollection<T>
    {
        public override int Count => Math.Min(Source.Count, _take);

        private readonly int _take;

        public TakeCollection(IReadOnlyCollection<T> source, IEnumerable<T> transformation, int count)
            : base(source, transformation)
            => _take = count <= 0 ? 0 : count;
    }
}
