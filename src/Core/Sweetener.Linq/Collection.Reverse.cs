// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Sweetener.Linq
{
    static partial class Collection
    {
        /// <summary>
        /// Inverts the order of the elements in a collection.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A collection of values to reverse.</param>
        /// <returns>
        /// A collection whose elements correspond to those of the input collection in reverse order.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        public static IReadOnlyCollection<TSource> Reverse<TSource>(this IReadOnlyCollection<TSource> source)
            => new EnumerableReadOnlyCollection<TSource>(source, Enumerable.Reverse(source));
    }
}
