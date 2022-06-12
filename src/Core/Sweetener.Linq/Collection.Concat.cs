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

    private sealed class ConcatenatedCollection<TElement> : DecoratorCollection<TElement>
    {
        public override int Count => _first.Count + _second.Count;

        private readonly IReadOnlyCollection<TElement> _first;
        private readonly IReadOnlyCollection<TElement> _second;

        public ConcatenatedCollection(IReadOnlyCollection<TElement> first, IReadOnlyCollection<TElement> second)
            : base(Enumerable.Concat(first, second))
        {
            _first  = first  ?? throw new ArgumentNullException(nameof(first));
            _second = second ?? throw new ArgumentNullException(nameof(second));
        }
    }
}
