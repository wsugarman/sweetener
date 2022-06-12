// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sweetener.Linq;

/// <summary>
/// Provides a set of <see langword="static"/> methods for transforming collections.
/// </summary>
[SuppressMessage("Naming", "CA1711:Identifiers should not have incorrect suffix", Justification = "Collection class is meant to parallel Enumerable.")]
public static partial class Collection
{
    private abstract class DecoratorCollection<T> : ICollection<T>, IEnumerable<T>, IReadOnlyCollection<T>
    {
        public abstract int Count { get; }

        public abstract IEnumerable<T> Elements { get; }

        [ExcludeFromCodeCoverage]
        bool ICollection<T>.IsReadOnly => true;

        public IEnumerator<T> GetEnumerator()
            => Elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

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
    }

    private class MutableDecoratorCollection<T> : DecoratorCollection<T>
    {
        public override int Count => Source.Count;

        public override IEnumerable<T> Elements { get; }

        protected IReadOnlyCollection<T> Source { get; }

        public MutableDecoratorCollection(IReadOnlyCollection<T> source, IEnumerable<T> result)
        {
            Elements = result;
            Source   = source;
        }
    }

    private class MutableProjectionDecoratorCollection<TSource, TResult> : DecoratorCollection<TResult>
    {
        public override int Count => Source.Count;

        public override IEnumerable<TResult> Elements { get; }

        protected IReadOnlyCollection<TSource> Source { get; }

        public MutableProjectionDecoratorCollection(IReadOnlyCollection<TSource> source, IEnumerable<TResult> result)
        {
            Elements = result;
            Source   = source;
        }
    }

    private sealed class ImmutableDecoratorCollection<T> : DecoratorCollection<T>
    {
        public override int Count { get; }

        public override IEnumerable<T> Elements { get; }

        public ImmutableDecoratorCollection(IEnumerable<T> elements, int count)
        {
            Count    = count;
            Elements = elements;
        }
    }
}
