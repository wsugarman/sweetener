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
    private abstract class DecoratorCollection<TElement> : ICollection<TElement>, IEnumerable<TElement>, IReadOnlyCollection<TElement>
    {
        public abstract int Count { get; }

        public IEnumerable<TElement> Elements { get; }

        [ExcludeFromCodeCoverage]
        bool ICollection<TElement>.IsReadOnly => true;

        protected DecoratorCollection(IEnumerable<TElement> elements)
            => Elements = elements;

        public IEnumerator<TElement> GetEnumerator()
            => Elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        [ExcludeFromCodeCoverage]
        void ICollection<TElement>.Add(TElement item)
            => throw new NotSupportedException();

        [ExcludeFromCodeCoverage]
        void ICollection<TElement>.Clear()
            => throw new NotSupportedException();

        [ExcludeFromCodeCoverage]
        bool ICollection<TElement>.Contains(TElement item)
            => throw new NotSupportedException();

        [ExcludeFromCodeCoverage]
        void ICollection<TElement>.CopyTo(TElement[] array, int arrayIndex)
            => throw new NotSupportedException();

        [ExcludeFromCodeCoverage]
        bool ICollection<TElement>.Remove(TElement item)
            => throw new NotSupportedException();
    }

    private class MutableDecoratorCollection<TElement> : DecoratorCollection<TElement>
    {
        public override int Count => Source.Count;

        protected IReadOnlyCollection<TElement> Source { get; }

        public MutableDecoratorCollection(IReadOnlyCollection<TElement> source, IEnumerable<TElement> result)
            : base(result)
            => Source = source;
    }

    private class MutableProjectionDecoratorCollection<TSource, TResult> : DecoratorCollection<TResult>
    {
        public override int Count => Source.Count;

        protected IReadOnlyCollection<TSource> Source { get; }

        public MutableProjectionDecoratorCollection(IReadOnlyCollection<TSource> source, IEnumerable<TResult> result)
            : base(result)
            => Source = source;
    }

    private sealed class ImmutableDecoratorCollection<TElement> : DecoratorCollection<TElement>
    {
        public override int Count { get; }

        public ImmutableDecoratorCollection(IEnumerable<TElement> elements, int count)
            : base(elements)
            => Count = count;
    }
}
