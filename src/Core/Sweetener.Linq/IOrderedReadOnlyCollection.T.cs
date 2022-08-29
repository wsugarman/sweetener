// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Sweetener.Linq;

/// <summary>
/// Represents a sorted read-only collection of elements.
/// </summary>
/// <typeparam name="TElement">The type of the elements.</typeparam>
#if NETCOREAPP2_0_OR_GREATER
public interface IOrderedReadOnlyCollection<out TElement> : IOrderedEnumerable<TElement>, IReadOnlyCollection<TElement>
#else
public interface IOrderedReadOnlyCollection<TElement> : IOrderedEnumerable<TElement>, IReadOnlyCollection<TElement>
#endif
{
    /// <summary>
    /// Performs a subsequent ordering on the elements of an <see cref="IOrderedReadOnlyCollection{TElement}"/>
    /// according to a key.
    /// </summary>
    /// <typeparam name="TKey">The type of the key produced by <paramref name="keySelector"/>.</typeparam>
    /// <param name="keySelector">
    /// The <see cref="Func{T, TResult}"/> used to extract the key for each element.
    /// </param>
    /// <param name="comparer">
    /// The <see cref="IComparer{T}"/> used to compare keys for placement in the returned sequence.
    /// </param>
    /// <param name="descending">
    /// <see langword="true"/> to sort the elements in descending order;
    /// <see langword="false"/> to sort the elements in ascending order.
    /// </param>
    /// <returns>
    /// An <see cref="IOrderedReadOnlyCollection{TElement}"/> whose elements are sorted according to a key.
    /// </returns>
    IOrderedReadOnlyCollection<TElement> CreateOrderedCollection<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending);
}
