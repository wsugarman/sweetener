// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using LinqEnumerable = System.Linq.Enumerable;

namespace Sweetener.Linq;

static partial class Collection
{
    // The Enumerable class provides optimized calls to the BCL's corresponding methods
    private static class Enumerable
    {
        public static IEnumerable<TSource> Append<TSource>(IReadOnlyCollection<TSource> source, TSource element)
            => LinqEnumerable.Append(
                source is DecoratorCollection<TSource> decorator ? decorator.Elements : source,
                element);

        public static IEnumerable<TSource> Concat<TSource>(IReadOnlyCollection<TSource> first, IReadOnlyCollection<TSource> second)
            => LinqEnumerable.Concat(
                first  is DecoratorCollection<TSource> d1 ? d1.Elements : first,
                second is DecoratorCollection<TSource> d2 ? d2.Elements : second);

        public static System.Linq.IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(IReadOnlyCollection<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey>? comparer)
            => LinqEnumerable.OrderBy(
                source is DecoratorCollection<TSource> decorator ? decorator.Elements : source,
                keySelector,
                comparer);

        public static System.Linq.IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(IReadOnlyCollection<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey>? comparer)
            => LinqEnumerable.OrderByDescending(
                source is DecoratorCollection<TSource> decorator ? decorator.Elements : source,
                keySelector,
                comparer);

        public static IEnumerable<TSource> Prepend<TSource>(IReadOnlyCollection<TSource> source, TSource element)
            => LinqEnumerable.Prepend(
                source is DecoratorCollection<TSource> decorator ? decorator.Elements : source,
                element);

        public static IEnumerable<TSource> Reverse<TSource>(IReadOnlyCollection<TSource> source)
            => LinqEnumerable.Reverse(source is DecoratorCollection<TSource> decorator ? decorator.Elements : source);

        public static IEnumerable<TResult> Select<TSource, TResult>(IReadOnlyCollection<TSource> source, Func<TSource, TResult> selector)
            => LinqEnumerable.Select(
                source is DecoratorCollection<TSource> decorator ? decorator.Elements : source,
                selector);

        public static IEnumerable<TResult> Select<TSource, TResult>(IReadOnlyCollection<TSource> source, Func<TSource, int, TResult> selector)
            => LinqEnumerable.Select(
                source is DecoratorCollection<TSource> decorator ? decorator.Elements : source,
                selector);

        public static IEnumerable<TSource> Skip<TSource>(IReadOnlyCollection<TSource> source, int count)
            => LinqEnumerable.Skip(
                source is DecoratorCollection<TSource> decorator ? decorator.Elements : source,
                count);

#if NETCOREAPP2_0_OR_GREATER
        public static IEnumerable<TSource> SkipLast<TSource>(IReadOnlyCollection<TSource> source, int count)
            => LinqEnumerable.SkipLast(
                source is DecoratorCollection<TSource> decorator ? decorator.Elements : source,
                count);
#endif

        public static IEnumerable<TSource> Take<TSource>(IReadOnlyCollection<TSource> source, int count)
            => LinqEnumerable.Take(
                source is DecoratorCollection<TSource> decorator ? decorator.Elements : source,
                count);

#if NETCOREAPP2_0_OR_GREATER
        public static IEnumerable<TSource> TakeLast<TSource>(IReadOnlyCollection<TSource> source, int count)
            => LinqEnumerable.TakeLast(
                source is DecoratorCollection<TSource> decorator ? decorator.Elements : source,
                count);
#endif
    }
}
