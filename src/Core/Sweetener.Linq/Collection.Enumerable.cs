// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Sweetener.Linq;

public static partial class Collection
{
    // The Enumerable class provides optimized calls to the BCL's corresponding methods
    private static class EnumerableDecorator
    {
        public static IEnumerable<TSource> Append<TSource>(IReadOnlyCollection<TSource> source, TSource element)
            => source is DecoratorCollection<TSource> decorator ? Append(decorator, element) : Enumerable.Append(source, element);

        public static IEnumerable<TSource> Append<TSource>(DecoratorCollection<TSource> source, TSource element)
            => Enumerable.Append(source.Enumerable, element);

        public static IEnumerable<TSource> Concat<TSource>(IReadOnlyCollection<TSource> first, IReadOnlyCollection<TSource> second)
            => Enumerable.Concat(
                first is DecoratorCollection<TSource> d1 ? d1.Enumerable : first,
                second is DecoratorCollection<TSource> d2 ? d2.Enumerable : second);

        public static System.Linq.IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(IReadOnlyCollection<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey>? comparer)
            => Enumerable.OrderBy(
                source is DecoratorCollection<TSource> decorator ? decorator.Enumerable : source,
                keySelector,
                comparer);

        public static System.Linq.IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(IReadOnlyCollection<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey>? comparer)
            => Enumerable.OrderByDescending(
                source is DecoratorCollection<TSource> decorator ? decorator.Enumerable : source,
                keySelector,
                comparer);

        public static IEnumerable<TSource> Prepend<TSource>(IReadOnlyCollection<TSource> source, TSource element)
            => source is DecoratorCollection<TSource> decorator ? Prepend(decorator, element) : Enumerable.Prepend(source, element);

        public static IEnumerable<TSource> Prepend<TSource>(DecoratorCollection<TSource> source, TSource element)
            => Enumerable.Prepend(source.Enumerable, element);

        public static IEnumerable<TSource> Reverse<TSource>(IReadOnlyCollection<TSource> source)
            => Enumerable.Reverse(source is DecoratorCollection<TSource> decorator ? decorator.Enumerable : source);

        public static IEnumerable<TResult> Select<TSource, TResult>(IReadOnlyCollection<TSource> source, Func<TSource, TResult> selector)
            => source is DecoratorCollection<TSource> decorator ? Select(decorator, selector) : Enumerable.Select(source, selector);

        public static IEnumerable<TResult> Select<TSource, TResult>(DecoratorCollection<TSource> source, Func<TSource, TResult> selector)
            => Enumerable.Select(source.Enumerable, selector);

        public static IEnumerable<TResult> Select<TSource, TResult>(IReadOnlyCollection<TSource> source, Func<TSource, int, TResult> selector)
            => source is DecoratorCollection<TSource> decorator ? Select(decorator, selector) : Enumerable.Select(source, selector);

        public static IEnumerable<TResult> Select<TSource, TResult>(DecoratorCollection<TSource> source, Func<TSource, int, TResult> selector)
            => Enumerable.Select(source.Enumerable, selector);

        public static IEnumerable<TSource> Skip<TSource>(IReadOnlyCollection<TSource> source, int count)
            => Enumerable.Skip(
                source is DecoratorCollection<TSource> decorator ? decorator.Enumerable : source,
                count);

#if NETCOREAPP2_0_OR_GREATER
        public static IEnumerable<TSource> SkipLast<TSource>(IReadOnlyCollection<TSource> source, int count)
            => Enumerable.SkipLast(
                source is DecoratorCollection<TSource> decorator ? decorator.Enumerable : source,
                count);
#endif

        public static IEnumerable<TSource> Take<TSource>(IReadOnlyCollection<TSource> source, int count)
            => Enumerable.Take(
                source is DecoratorCollection<TSource> decorator ? decorator.Enumerable : source,
                count);

#if NETCOREAPP2_0_OR_GREATER
        public static IEnumerable<TSource> TakeLast<TSource>(IReadOnlyCollection<TSource> source, int count)
            => Enumerable.TakeLast(
                source is DecoratorCollection<TSource> decorator ? decorator.Enumerable : source,
                count);
#endif
    }
}
