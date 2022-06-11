// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Sweetener.Linq;

static partial class Collection
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
        => new TakeCollection<TSource>(source, Enumerable.Take(source, count), count);

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
        => new TakeCollection<TSource>(source, Enumerable.TakeLast(source, count), count);
#endif

    private sealed class TakeCollection<TElement> : ICollection<TElement>, IReadOnlyCollection<TElement>
    {
        public int Count => Math.Min(_source.Count, _count);

        [ExcludeFromCodeCoverage]
        bool ICollection<TElement>.IsReadOnly => true;

        private readonly IReadOnlyCollection<TElement> _source;
        private readonly IEnumerable<TElement> _transformation;
        private readonly int _count;

        public TakeCollection(IReadOnlyCollection<TElement> source, IEnumerable<TElement> transformation, int count)
        {
            _source         = source;
            _transformation = transformation;
            _count          = count <= 0 ? 0 : count;
        }

        public IEnumerator<TElement> GetEnumerator()
            => _transformation.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        [ExcludeFromCodeCoverage]
        void ICollection<TElement>.Add(TElement item)
            => throw new NotSupportedException();

        [ExcludeFromCodeCoverage]
        void ICollection<TElement>.Clear()
            => throw new NotSupportedException();

        [ExcludeFromCodeCoverage]
        bool ICollection<TElement>.Contains(TElement item)
            => throw new NotSupportedException();

        [ExcludeFromCodeCoverage]
        void ICollection<TElement>.CopyTo(TElement[] array, int arrayIndex)
            => throw new NotSupportedException();

        [ExcludeFromCodeCoverage]
        bool ICollection<TElement>.Remove(TElement item)
            => throw new NotSupportedException();
    }
}
