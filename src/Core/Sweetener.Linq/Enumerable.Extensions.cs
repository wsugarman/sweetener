// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Sweetener.Linq;

/// <summary>
/// Provides a set of <see langword="static"/> methods for querying objects that implement <see cref="IEnumerable{T}"/>
/// beyond those provided by <see cref="Enumerable"/>.
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Projects each element of a collection into a new form based on a predicate.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <typeparam name="TResult">The type of the value returned by <paramref name="selectorPredicate"/>.</typeparam>
    /// <param name="source">A collection of values to invoke a transform function on.</param>
    /// <param name="selectorPredicate">
    /// A function to test each element for a condition and apply a transformation if it returns <see langword="true"/>.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyCollection{T}"/> whose elements are the result of invoking the transform function
    /// on each element of <paramref name="source"/> that satisfy the condition.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> or <paramref name="selectorPredicate"/> is <see langword="null"/>.
    /// </exception>
    public static IEnumerable<TResult> SelectWhere<TSource, TResult>(this IEnumerable<TSource> source, TryFunc<TSource, TResult> selectorPredicate)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        if (selectorPredicate is null)
            throw new ArgumentNullException(nameof(selectorPredicate));

        return source is IInternalEnumerable<TSource> existing
            ? existing.SelectWhere(selectorPredicate)
            : new SelectWhereEnumerable<TSource, TResult>(source, selectorPredicate);
    }

    /// <summary>
    /// Projects each element of a collection into a new form based on a predicate
    /// by incorporating the element's index.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <typeparam name="TResult">The type of the value returned by <paramref name="selectorPredicate"/>.</typeparam>
    /// <param name="source">A collection of values to invoke a transform function on.</param>
    /// <param name="selectorPredicate">
    /// A function to test each element for a condition and apply a transformation if it returns <see langword="true"/>;
    /// the second parameter of the function represents the index of the source element.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyCollection{T}"/> whose elements are the result of invoking the transform function
    /// on each element of <paramref name="source"/> that satisfy the condition.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> or <paramref name="selectorPredicate"/> is <see langword="null"/>.
    /// </exception>
    public static IEnumerable<TResult> SelectWhere<TSource, TResult>(this IEnumerable<TSource> source, TryFunc<TSource, int, TResult> selectorPredicate)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        if (selectorPredicate is null)
            throw new ArgumentNullException(nameof(selectorPredicate));

        return SelectWhereIterator(source, selectorPredicate);
    }

    private static IEnumerable<TResult> SelectWhereIterator<TSource, TResult>(IEnumerable<TSource> source, TryFunc<TSource, int, TResult> selectorPredicate)
    {
        // Note: The BCLs do not optimize the index delegates
        int i = -1;
        foreach (TSource element in source)
        {
            checked
            {
                i++;
            }

            if (selectorPredicate(element, i, out TResult? result))
                yield return result;
        }
    }

    // Note: This class is meant to represent our own Enumerables defined in Sweetener to help optimize.
    // TODO: It may be advantageous to mirror the internal Iterator<TSource> class in the BCL
    [SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Class is not a collection.")]
    private interface IInternalEnumerable<TSource> : IEnumerable<TSource>
    {
        IEnumerable<TResult> SelectWhere<TResult>(TryFunc<TSource, TResult> selectorPredicate);
    }

    [SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Class is not a collection.")]
    private sealed class SelectWhereEnumerable<TSource, TResult> : IInternalEnumerable<TResult>
    {
        private readonly IEnumerable<TSource> _source;
        private readonly TryFunc<TSource, TResult> _selectorPredicate;

        public SelectWhereEnumerable(IEnumerable<TSource> source, TryFunc<TSource, TResult> selectorPredicate)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _selectorPredicate = selectorPredicate ?? throw new ArgumentNullException(nameof(selectorPredicate));
        }

        public IEnumerator<TResult> GetEnumerator()
        {
            foreach (TSource element in _source)
            {
                if (_selectorPredicate(element, out TResult? result))
                    yield return result;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public IEnumerable<TResult2> SelectWhere<TResult2>(TryFunc<TResult, TResult2> selectorPredicate)
            => new SelectWhereEnumerable<TSource, TResult2>(_source, CombineSelectorPredicates(_selectorPredicate, selectorPredicate));
    }

    private static TryFunc<TSource, TResult> CombineSelectorPredicates<TSource, TMiddle, TResult>(
        TryFunc<TSource, TMiddle> selectorPredicate1,
        TryFunc<TMiddle, TResult> selectorPredicate2)
        => (TSource x, [MaybeNullWhen(false)] out TResult z) =>
        {
            if (!selectorPredicate1(x, out TMiddle? y))
            {
                z = default;
                return false;
            }

            return selectorPredicate2(y, out z);
        };
}
