// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sweetener.Linq
{
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

        // TODO: Implement TakeLast when moving to .NET Core

        private sealed class TakeCollection<TElement> : IReadOnlyCollection<TElement>
        {
            public int Count => Math.Min(_source.Count, _count);

            private readonly IReadOnlyCollection<TElement> _source;
            private readonly IEnumerable<TElement> _transformation;
            private readonly int _count;

            public TakeCollection(IReadOnlyCollection<TElement> source, IEnumerable<TElement> transformation, int count)
            {
                _source = source;
                _transformation = transformation;
                _count = count <= 0 ? 0 : count;
            }

            public IEnumerator<TElement> GetEnumerator()
                => _transformation.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();
        }
    }
}
