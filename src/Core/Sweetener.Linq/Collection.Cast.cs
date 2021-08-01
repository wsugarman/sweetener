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
        /// Casts the elements of an <see cref="ICollection"/> to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to cast the elements of <paramref name="source"/> to.</typeparam>
        /// <param name="source">
        /// The <see cref="ICollection"/> that contains the elements to be cast to type TResult.
        /// </param>
        /// <returns>
        /// An <see cref="IReadOnlyCollection{T}"/> that contains each element of the source collection
        /// cast to the specified type.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidCastException">
        /// An element in the collection cannot be cast to type <typeparamref name="TResult"/>.
        /// </exception>
        public static IReadOnlyCollection<
#nullable disable // Like Enumerable.Cast<TResult>, we cannot relate the nullability of the result to that of the source
                TResult
#nullable restore
                > Cast<TResult>(this ICollection source)
            => source is IReadOnlyCollection<TResult> typedSource
                ? typedSource
                : new CastCollection<TResult>(source);

        private sealed class CastCollection<TElement> : IReadOnlyCollection<TElement>
        {
            public int Count => _source.Count;

            private readonly ICollection _source;

            public CastCollection(ICollection source)
                => _source = source ?? throw new ArgumentNullException(nameof(source));

            public IEnumerator<TElement> GetEnumerator()
                => Enumerable.Cast<TElement>(_source).GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();
        }
    }
}
