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
        /// Appends a value to the end of the collection.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A collection of values.</param>
        /// <param name="element">The value to append to source.</param>
        /// <returns>A new collection that ends with <paramref name="element"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        public static IReadOnlyCollection<TSource> Append<TSource>(this IReadOnlyCollection<TSource> source, TSource element)
            => new AdditionalCollection<TSource>(source, Enumerable.Append(source, element));

        /// <summary>
        /// Adds a value to the beginning of the collection.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A collection of values.</param>
        /// <param name="element">The value to prepend to source.</param>
        /// <returns>A new collection that begins with <paramref name="element"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        public static IReadOnlyCollection<TSource> Prepend<TSource>(this IReadOnlyCollection<TSource> source, TSource element)
            => new AdditionalCollection<TSource>(source, Enumerable.Prepend(source, element));

        private sealed class AdditionalCollection<TElement> : IReadOnlyCollection<TElement>
        {
            public int Count => _source.Count + 1;

            private readonly IReadOnlyCollection<TElement> _source;
            private readonly IEnumerable<TElement> _transformation;

            public AdditionalCollection(IReadOnlyCollection<TElement> source, IEnumerable<TElement> transformation)
            {
                _source = source;
                _transformation = transformation;
            }

            public IEnumerator<TElement> GetEnumerator()
                => _transformation.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();
        }
    }
}
