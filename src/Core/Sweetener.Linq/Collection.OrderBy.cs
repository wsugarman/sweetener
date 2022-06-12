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
    /// Sorts the elements of a collection in ascending order according to a key.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
    /// <param name="source">A collection of values to order.</param>
    /// <param name="keySelector">A function to extract a key from an element.</param>
    /// <returns>
    /// An <see cref="IReadOnlyCollection{TElement}"/> whose elements are sorted according to a key.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> or <paramref name="keySelector"/> <see langword="null"/>.
    /// </exception>
    public static IOrderedReadOnlyCollection<TSource> OrderBy<TSource, TKey>(this IReadOnlyCollection<TSource> source, Func<TSource, TKey> keySelector)
        => source.OrderBy(keySelector, null);

    /// <summary>
    /// Sorts the elements of a collection in ascending order by using a specified comparer.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
    /// <param name="source">A collection of values to order.</param>
    /// <param name="keySelector">A function to extract a key from an element.</param>
    /// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
    /// <returns>
    /// An <see cref="IReadOnlyCollection{TElement}"/> whose elements are sorted according to a key.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> or <paramref name="keySelector"/> <see langword="null"/>.
    /// </exception>
    public static IOrderedReadOnlyCollection<TSource> OrderBy<TSource, TKey>(this IReadOnlyCollection<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey>? comparer)
        => new OrderedCollection<TSource>(source, Enumerable.OrderBy(source, keySelector, comparer));

    /// <summary>
    /// Sorts the elements of a collection in descending order according to a key.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
    /// <param name="source">A collection of values to order.</param>
    /// <param name="keySelector">A function to extract a key from an element.</param>
    /// <returns>
    /// An <see cref="IReadOnlyCollection{TElement}"/> whose elements are sorted in descending order
    /// according to a key.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> or <paramref name="keySelector"/> <see langword="null"/>.
    /// </exception>
    public static IOrderedReadOnlyCollection<TSource> OrderByDescending<TSource, TKey>(this IReadOnlyCollection<TSource> source, Func<TSource, TKey> keySelector)
        => source.OrderByDescending(keySelector, null);

    /// <summary>
    /// Sorts the elements of a collection in descending order by using a specified comparer.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
    /// <param name="source">A collection of values to order.</param>
    /// <param name="keySelector">A function to extract a key from an element.</param>
    /// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
    /// <returns>
    /// An <see cref="IReadOnlyCollection{TElement}"/> whose elements are sorted in descending order
    /// according to a key.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> or <paramref name="keySelector"/> <see langword="null"/>.
    /// </exception>
    public static IOrderedReadOnlyCollection<TSource> OrderByDescending<TSource, TKey>(this IReadOnlyCollection<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey>? comparer)
        => new OrderedCollection<TSource>(source, Enumerable.OrderByDescending(source, keySelector, comparer));

    /// <summary>
    /// Performs a subsequent ordering of the elements in a collection in ascending order according to a key.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
    /// <param name="source">
    /// An <see cref="IOrderedReadOnlyCollection{TElement}"/> that contains elements to sort.
    /// </param>
    /// <param name="keySelector">A function to extract a key from an element.</param>
    /// <returns>
    /// An <see cref="IOrderedReadOnlyCollection{TElement}"/> whose elements are sorted according to a key.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> or <paramref name="keySelector"/> <see langword="null"/>.
    /// </exception>
    public static IOrderedReadOnlyCollection<TSource> ThenBy<TSource, TKey>(this IOrderedReadOnlyCollection<TSource> source, Func<TSource, TKey> keySelector)
        => source.ThenBy(keySelector, null);

    /// <summary>
    /// Performs a subsequent ordering of the elements in a collection in ascending order by using a specified comparer.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
    /// <param name="source">
    /// An <see cref="IOrderedReadOnlyCollection{TElement}"/> that contains elements to sort.
    /// </param>
    /// <param name="keySelector">A function to extract a key from an element.</param>
    /// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
    /// <returns>
    /// An <see cref="IOrderedReadOnlyCollection{TElement}"/> whose elements are sorted according to a key.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> or <paramref name="keySelector"/> <see langword="null"/>.
    /// </exception>
    public static IOrderedReadOnlyCollection<TSource> ThenBy<TSource, TKey>(this IOrderedReadOnlyCollection<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey>? comparer)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        return source.CreateOrderedCollection(keySelector, comparer ?? Comparer<TKey>.Default, descending: false);
    }

    /// <summary>
    /// Performs a subsequent ordering of the elements in a collection in descending order, according to a key.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
    /// <param name="source">
    /// An <see cref="IOrderedReadOnlyCollection{TElement}"/> that contains elements to sort.
    /// </param>
    /// <param name="keySelector">A function to extract a key from an element.</param>
    /// <returns>
    /// An <see cref="IOrderedReadOnlyCollection{TElement}"/> whose elements are sorted in descending order
    /// according to a key.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> or <paramref name="keySelector"/> <see langword="null"/>.
    /// </exception>
    public static IOrderedReadOnlyCollection<TSource> ThenByDescending<TSource, TKey>(this IOrderedReadOnlyCollection<TSource> source, Func<TSource, TKey> keySelector)
        => source.ThenByDescending(keySelector, null);

    /// <summary>
    /// Performs a subsequent ordering of the elements in a collection in descending order by using a specified comparer.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
    /// <param name="source">
    /// An <see cref="IOrderedReadOnlyCollection{TElement}"/> that contains elements to sort.
    /// </param>
    /// <param name="keySelector">A function to extract a key from an element.</param>
    /// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
    /// <returns>
    /// An <see cref="IOrderedReadOnlyCollection{TElement}"/> whose elements are sorted in descending order
    /// according to a key.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> or <paramref name="keySelector"/> <see langword="null"/>.
    /// </exception>
    public static IOrderedReadOnlyCollection<TSource> ThenByDescending<TSource, TKey>(
        this IOrderedReadOnlyCollection<TSource> source,
        Func<TSource, TKey> keySelector,
        IComparer<TKey>? comparer)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        return source.CreateOrderedCollection(keySelector, comparer ?? Comparer<TKey>.Default, descending: true);
    }

    private sealed class OrderedCollection<TElement> : DecoratorCollection<TElement>, IOrderedReadOnlyCollection<TElement>
    {
        public int Count => _source.Count;


        public OrderedCollection(IReadOnlyCollection<TElement> source, IOrderedEnumerable<TElement> ordered)
            : base(source, ordered)
        { }

        public IOrderedReadOnlyCollection<TElement> CreateOrderedCollection<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey>? comparer, bool descending)
            => new OrderedCollection<TElement>(_source, _ordered.CreateOrderedEnumerable(keySelector, comparer, descending));

        public IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey>? comparer, bool descending)
            => _ordered.CreateOrderedEnumerable(keySelector, comparer, descending);
    }
}
