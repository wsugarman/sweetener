// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Sweetener.Linq;

partial class Collection
{
    /// <summary>
    /// Projects each element of a collection into a new form.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <typeparam name="TResult">The type of the value returned by <paramref name="selector"/>.</typeparam>
    /// <param name="source">A collection of values to invoke a transform function on.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>
    /// An <see cref="IReadOnlyCollection{T}"/> whose elements are the result of invoking the transform function
    /// on each element of <paramref name="source"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.
    /// </exception>
    public static IReadOnlyCollection<TResult> Select<TSource, TResult>(this IReadOnlyCollection<TSource> source, Func<TSource, TResult> selector)
        => new SelectCollection<TSource, TResult>(source, Enumerable.Select(source, selector));

    /// <summary>
    /// Projects each element of a collection into a new form by incorporating the element's index.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <typeparam name="TResult">The type of the value returned by <paramref name="selector"/>.</typeparam>
    /// <param name="source">A collection of values to invoke a transform function on.</param>
    /// <param name="selector">
    /// A transform function to apply to each source element; the second parameter of the function represents
    /// the index of the source element.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyCollection{T}"/> whose elements are the result of invoking the transform function
    /// on each element of <paramref name="source"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.
    /// </exception>
    public static IReadOnlyCollection<TResult> Select<TSource, TResult>(this IReadOnlyCollection<TSource> source, Func<TSource, int, TResult> selector)
        => new SelectCollection<TSource, TResult>(source, Enumerable.Select(source, selector));

    private sealed class SelectCollection<TSource, TResult> : ICollection<TResult>, DecoratorCollection<TSource>, IReadOnlyCollection<TResult>
    {
        public int Count => _source.Count;

        [ExcludeFromCodeCoverage]
        bool ICollection<TResult>.IsReadOnly => true;

        private readonly IReadOnlyCollection<TSource> _source;
        private readonly IEnumerable<TResult> _projection;

        public SelectCollection(IReadOnlyCollection<TSource> source, IEnumerable<TResult> projection)
        {
            _source     = source;
            _projection = projection;
        }

        public IEnumerator<TResult> GetEnumerator()
            => _projection.GetEnumerator();

        public IReadOnlyCollection<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
            => new SelectCollection<TSource, TResult2>(_source, _projection.Select(selector));

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        [ExcludeFromCodeCoverage]
        void ICollection<TResult>.Add(TResult item)
            => throw new NotSupportedException();

        [ExcludeFromCodeCoverage]
        void ICollection<TResult>.Clear()
            => throw new NotSupportedException();

        [ExcludeFromCodeCoverage]
        bool ICollection<TResult>.Contains(TResult item)
            => throw new NotSupportedException();

        [ExcludeFromCodeCoverage]
        void ICollection<TResult>.CopyTo(TResult[] array, int arrayIndex)
            => throw new NotSupportedException();

        [ExcludeFromCodeCoverage]
        bool ICollection<TResult>.Remove(TResult item)
            => throw new NotSupportedException();

        IEnumerator<TSource> IEnumerable<TSource>.GetEnumerator() => throw new NotImplementedException();
    }
}
