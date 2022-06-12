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
    /// Appends a value to the end of the collection.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <param name="source">A collection of values.</param>
    /// <param name="element">The value to append to source.</param>
    /// <returns>A new collection that ends with <paramref name="element"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
    public static IReadOnlyCollection<TSource> Append<TSource>(this IReadOnlyCollection<TSource> source, TSource element)
        => new AdditionalCollection<TSource>(
            source,
            Enumerable.Append(
                source is DecoratorCollection<TSource> decorator ? decorator.Elements : source,
                element));

    /// <summary>
    /// Adds a value to the beginning of the collection.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <param name="source">A collection of values.</param>
    /// <param name="element">The value to prepend to source.</param>
    /// <returns>A new collection that begins with <paramref name="element"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
    public static IReadOnlyCollection<TSource> Prepend<TSource>(this IReadOnlyCollection<TSource> source, TSource element)
        => new AdditionalCollection<TSource>(
            source,
            Enumerable.Prepend(
                source is DecoratorCollection<TSource> decorator ? decorator.Elements : source,
                element));

    private sealed class AdditionalCollection<TElement> : MutableDecoratorCollection<TElement>
    {
        public override int Count => Source.Count + 1;

        public AdditionalCollection(IReadOnlyCollection<TElement> source, IEnumerable<TElement> results)
            : base(source, results)
        { }
    }
}
