// Copyright © William Sugarman.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sweetener.Linq
{
    /// <summary>
    /// Provides a set of <see langword="static"/> methods for transforming collections.
    /// </summary>
    [SuppressMessage("Naming", "CA1711:Identifiers should not have incorrect suffix", Justification = "Collection class is meant to parallel Enumerable. ")]
    public static partial class Collection
    {
        private sealed class EnumerableCollection<TElement> : IReadOnlyCollection<TElement>
        {
            public int Count => _source.Count;

            private readonly ICollection<TElement> _source;
            private readonly IEnumerable<TElement> _transformation;

            public EnumerableCollection(ICollection<TElement> source, IEnumerable<TElement> transformation)
            {
                _source = source;
                _transformation = transformation;
            }

            public IEnumerator<TElement> GetEnumerator()
                => _transformation.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();
        }

        private sealed class EnumerableReadOnlyCollection<TElement> : IReadOnlyCollection<TElement>
        {
            public int Count => _source.Count;

            private readonly IReadOnlyCollection<TElement> _source;
            private readonly IEnumerable<TElement> _transformation;

            public EnumerableReadOnlyCollection(IReadOnlyCollection<TElement> source, IEnumerable<TElement> transformation)
            {
                _source = source;
                _transformation = transformation;
            }

            public IEnumerator<TElement> GetEnumerator()
                => _transformation.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();
        }

        private sealed class ReadOnlyCollection<TElement> : IReadOnlyCollection<TElement>
        {
            public int Count { get; }

            private readonly IEnumerable<TElement> _elements;

            public ReadOnlyCollection(IEnumerable<TElement> elements, int count)
            {
                Count = count;
                _elements = elements;
            }

            public IEnumerator<TElement> GetEnumerator()
                => _elements.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();
        }
    }
}
