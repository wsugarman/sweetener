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
        public static IReadOnlyCollection<TSource> Concat<TSource>(this ICollection<TSource> first, ICollection<TSource> second)
            => new ConcatenatedCollection<TSource>(first, second);

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
            => new ReadOnlyConcatenatedCollection<TSource>(first, second);

        private sealed class ConcatenatedCollection<TSource> : IReadOnlyCollection<TSource>
        {
            public int Count => _first.Count + _second.Count;

            private readonly ICollection<TSource> _first;
            private readonly ICollection<TSource> _second;

            public ConcatenatedCollection(ICollection<TSource> first, ICollection<TSource> second)
            {
                _first = first ?? throw new ArgumentNullException(nameof(first));
                _second = second ?? throw new ArgumentNullException(nameof(second));
            }

            public IEnumerator<TSource> GetEnumerator()
                => _first.Concat(_second).GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();
        }

        private sealed class ReadOnlyConcatenatedCollection<TSource> : IReadOnlyCollection<TSource>
        {
            public int Count => _first.Count + _second.Count;

            private readonly IReadOnlyCollection<TSource> _first;
            private readonly IReadOnlyCollection<TSource> _second;

            public ReadOnlyConcatenatedCollection(IReadOnlyCollection<TSource> first, IReadOnlyCollection<TSource> second)
            {
                _first = first ?? throw new ArgumentNullException(nameof(first));
                _second = second ?? throw new ArgumentNullException(nameof(second));
            }

            public IEnumerator<TSource> GetEnumerator()
                => Enumerable.Concat(_first, _second).GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();
        }
    }
}
