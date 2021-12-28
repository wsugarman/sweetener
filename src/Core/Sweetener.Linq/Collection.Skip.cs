// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sweetener.Linq;

static partial class Collection
{
    /// <summary>
    /// Bypasses a specified number of elements in a collection and then returns the remaining elements.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <param name="source">An <see cref="ICollection{T}"/> to return elements from.</param>
    /// <param name="count">The number of elements to skip before returning the remaining elements.</param>
    /// <returns>
    /// An <see cref="IReadOnlyCollection{T}"/> that contains the elements that occur after
    /// the specified index in the input collection.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
    public static IReadOnlyCollection<TSource> Skip<TSource>(this IReadOnlyCollection<TSource> source, int count)
        => new SkipCollection<TSource>(source, Enumerable.Skip(source, count), count);

    // TODO: Implement SkipLast when moving to .NET Core

    private sealed class SkipCollection<TElement> : IReadOnlyCollection<TElement>
    {
        public int Count => Math.Max(0, _source.Count - _skip);

        private readonly IReadOnlyCollection<TElement> _source;
        private readonly IEnumerable<TElement> _transformation;
        private readonly int _skip;

        public SkipCollection(IReadOnlyCollection<TElement> source, IEnumerable<TElement> transformation, int skip)
        {
            _source         = source;
            _transformation = transformation;
            _skip           = skip <= 0 ? 0 : skip;
        }

        public IEnumerator<TElement> GetEnumerator()
            => _transformation.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
