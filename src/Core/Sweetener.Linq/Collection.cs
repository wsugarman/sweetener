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
[SuppressMessage("Naming", "CA1711:Identifiers should not have incorrect suffix", Justification = "Collection class is meant to parallel Enumerable. ")]
public static partial class Collection
{
    private sealed class EnumerableCollection<TElement> : ICollection<TElement>, IReadOnlyCollection<TElement>
    {
        public int Count => _source.Count;

        [ExcludeFromCodeCoverage]
        bool ICollection<TElement>.IsReadOnly => true;

        private readonly IReadOnlyCollection<TElement> _source;
        private readonly IEnumerable<TElement> _transformation;

        public EnumerableCollection(IReadOnlyCollection<TElement> source, IEnumerable<TElement> transformation)
        {
            _source         = source;
            _transformation = transformation;
        }

        public IEnumerator<TElement> GetEnumerator()
            => _transformation.GetEnumerator();

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

    private sealed class ReadOnlyCollection<TElement> : ICollection<TElement>, IReadOnlyCollection<TElement>
    {
        public int Count { get; }

        [ExcludeFromCodeCoverage]
        bool ICollection<TElement>.IsReadOnly => true;

        private readonly IEnumerable<TElement> _elements;

        public ReadOnlyCollection(IEnumerable<TElement> elements, int count)
        {
            Count     = count;
            _elements = elements;
        }

        public IEnumerator<TElement> GetEnumerator()
            => _elements.GetEnumerator();

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
}
