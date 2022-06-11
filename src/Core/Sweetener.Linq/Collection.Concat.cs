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

    private sealed class ConcatenatedCollection<TElement> : ICollection<TElement>, IReadOnlyCollection<TElement>
    {
        public int Count => _first.Count + _second.Count;

        [ExcludeFromCodeCoverage]
        bool ICollection<TElement>.IsReadOnly => true;

        private readonly IReadOnlyCollection<TElement> _first;
        private readonly IReadOnlyCollection<TElement> _second;

        public ConcatenatedCollection(IReadOnlyCollection<TElement> first, IReadOnlyCollection<TElement> second)
        {
            _first  = first  ?? throw new ArgumentNullException(nameof(first));
            _second = second ?? throw new ArgumentNullException(nameof(second));
        }

        public IEnumerator<TElement> GetEnumerator()
            => Enumerable.Concat(_first, _second).GetEnumerator();

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
