// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace Sweetener.Linq;

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
        => source is AdditionalCollection<TSource> decorator
            ? new AdditionalCollection<TSource>(decorator, EnumerableDecorator.Append(decorator, element))
            : new AdditionalCollection<TSource>(source   , EnumerableDecorator.Append(source   , element));

    /// <summary>
    /// Adds a value to the beginning of the collection.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <param name="source">A collection of values.</param>
    /// <param name="element">The value to prepend to source.</param>
    /// <returns>A new collection that begins with <paramref name="element"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
    public static IReadOnlyCollection<TSource> Prepend<TSource>(this IReadOnlyCollection<TSource> source, TSource element)
        => source is AdditionalCollection<TSource> decorator
            ? new AdditionalCollection<TSource>(decorator, EnumerableDecorator.Prepend(decorator, element))
            : new AdditionalCollection<TSource>(source   , EnumerableDecorator.Prepend(source   , element));

    private sealed class AdditionalCollection<T> : DecoratorTransformationCollection<T>
    {
        public override int Count => Source.Count + _extraCount;

        private readonly int _extraCount;

        public AdditionalCollection(IReadOnlyCollection<T> source, IEnumerable<T> results)
            : this(source, results, 1)
        { }

        public AdditionalCollection(AdditionalCollection<T> source, IEnumerable<T> results)
            : this(source.Source, results, source._extraCount + 1)
        { }

        private AdditionalCollection(IReadOnlyCollection<T> source, IEnumerable<T> results, int extraCount)
            : base(source, results)
            => _extraCount = extraCount;
    }
}
