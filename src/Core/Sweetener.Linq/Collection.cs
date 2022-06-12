// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Sweetener.Linq;

/// <summary>
/// Provides a set of <see langword="static"/> methods for transforming collections.
/// </summary>
[SuppressMessage("Naming", "CA1711:Identifiers should not have incorrect suffix", Justification = "Collection class is meant to parallel Enumerable.")]
public static partial class Collection
{
    private interface IProjectable<T>
    {
        [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Name matches method in System.Linq.Enumerable.")]
        IReadOnlyCollection<TResult> Select<TResult>(Func<T, TResult> selector);

        [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Name matches method in System.Linq.Enumerable.")]
        IReadOnlyCollection<TResult> Select<TResult>(Func<T, int, TResult> selector);
    }

    private abstract class DecoratorCollection<T> : ICollection<T>, IEnumerable<T>, IReadOnlyCollection<T>
    {
        public abstract int Count { get; }

        public abstract IEnumerable<T> Enumerable { get; }

        [ExcludeFromCodeCoverage]
        bool ICollection<T>.IsReadOnly => true;

        public IEnumerator<T> GetEnumerator()
            => Enumerable.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        #region ICollection<T> Implementation

        [ExcludeFromCodeCoverage]
        void ICollection<T>.Add(T item)
            => throw new NotSupportedException();

        [ExcludeFromCodeCoverage]
        void ICollection<T>.Clear()
            => throw new NotSupportedException();

        [ExcludeFromCodeCoverage]
        bool ICollection<T>.Contains(T item)
            => throw new NotSupportedException();

        [ExcludeFromCodeCoverage]
        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
            => throw new NotSupportedException();

        [ExcludeFromCodeCoverage]
        bool ICollection<T>.Remove(T item)
            => throw new NotSupportedException();

        #endregion
    }

    private sealed class GeneratedDecoratorCollection<T> : DecoratorCollection<T>
    {
        public sealed override int Count { get; }

        public sealed override IEnumerable<T> Enumerable { get; }

        public GeneratedDecoratorCollection(IEnumerable<T> elements, int count)
        {
            Count    = count;
            Enumerable = elements;
        }
    }

    private class DecoratorTransformationCollection<TSource, TResult> : DecoratorCollection<TResult>
    {
        public override int Count => Source.Count;

        public sealed override IEnumerable<TResult> Enumerable { get; }

        public IReadOnlyCollection<TSource> Source { get; }

        public DecoratorTransformationCollection(IReadOnlyCollection<TSource> source, IEnumerable<TResult> result)
        {
            Enumerable = result;
            Source   = source;
        }
    }

    private class DecoratorTransformationCollection<T> : DecoratorTransformationCollection<T, T>
    {
        public DecoratorTransformationCollection(IReadOnlyCollection<T> source, IEnumerable<T> result)
            : base(source, result)
        { }
    }

    private class FixedDecoratorTransformationCollection<TSource, TResult> : DecoratorTransformationCollection<TSource, TResult>, IProjectable<TResult>
    {
        public sealed override int Count => Source.Count;

        public FixedDecoratorTransformationCollection(IReadOnlyCollection<TSource> source, IEnumerable<TResult> result)
            : base(source, result)
        { }

        public IReadOnlyCollection<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
            => new FixedDecoratorTransformationCollection<TSource, TResult2>(Source, Collection.EnumerableDecorator.Select(this, selector));

        public IReadOnlyCollection<TResult2> Select<TResult2>(Func<TResult, int, TResult2> selector)
            => new FixedDecoratorTransformationCollection<TSource, TResult2>(Source, Collection.EnumerableDecorator.Select(this, selector));
    }

    private class FixedDecoratorTransformationCollection<T> : FixedDecoratorTransformationCollection<T, T>
    {
        public FixedDecoratorTransformationCollection(IReadOnlyCollection<T> source, IEnumerable<T> result)
            : base(source, result)
        { }
    }
}
