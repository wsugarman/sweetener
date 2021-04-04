// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Sweetener.Linq;

namespace Sweetener
{
    /// <summary>
    /// Represents an empty and immutable collection of keys and values.
    /// </summary>
    /// <remarks>
    /// Instances of this class can be fetched using the method <see cref="Map.Empty{TKey, TValue}"/>.
    /// </remarks>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class EmptyDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>
    {
        internal static readonly EmptyDictionary<TKey, TValue> Value = new EmptyDictionary<TKey, TValue>();

        private EmptyDictionary()
        { }

        #region ICollection<KeyValuePair<TKey, TValue>>

        int ICollection<KeyValuePair<TKey, TValue>>.Count => 0;

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => true;

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
            => throw new NotSupportedException(SR.CollectionFixedSizeMessage);

        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
            => throw new NotSupportedException(SR.CollectionReadOnlyMessage);

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
            => false;

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        { }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
            => throw new NotSupportedException(SR.CollectionFixedSizeMessage);

        #endregion

        #region IDictionary<TKey, TValue>

        TValue IDictionary<TKey, TValue>.this[TKey key]
        {
            get => throw new KeyNotFoundException();
            set => throw new NotSupportedException(SR.CollectionFixedSizeMessage);
        }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys => KeyCollection.Value;

        ICollection<TValue> IDictionary<TKey, TValue>.Values => ValueCollection.Value;

        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
            => throw new NotSupportedException(SR.CollectionFixedSizeMessage);

        bool IDictionary<TKey, TValue>.ContainsKey(TKey key)
            => false;

        bool IDictionary<TKey, TValue>.Remove(TKey key)
            => throw new NotSupportedException(SR.CollectionFixedSizeMessage);

        bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value)
        {
#nullable disable
            // The IDictionary<TKey, TValue> interface in .NET Standard 2.0 doesn't have nullability attributes
            // Otherwise, we'd include [MaybeNullWhen(false)] for "value"
            value = default;
#nullable enable
            return false;
        }

        #endregion

        #region IEnumerable

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
            => Enumerator.Empty<KeyValuePair<TKey, TValue>>();

        IEnumerator IEnumerable.GetEnumerator()
            => Enumerator.Empty<KeyValuePair<TKey, TValue>>();

        #endregion

        #region IReadOnlyCollection<T>

        int IReadOnlyCollection<KeyValuePair<TKey, TValue>>.Count => 0;

        #endregion

        #region IReadOnlyDictionary<TKey, TValue>

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => KeyCollection.Value;

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => ValueCollection.Value;

        TValue IReadOnlyDictionary<TKey, TValue>.this[TKey key] => throw new KeyNotFoundException();

        bool IReadOnlyDictionary<TKey, TValue>.ContainsKey(TKey key)
            => false;

        bool IReadOnlyDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value)
        {
#nullable disable
            // The IDictionary<TKey, TValue> interface in .NET Standard 2.0 doesn't have nullability attributes
            // Otherwise, we'd include [MaybeNullWhen(false)] for "value"
            value = default;
#nullable enable
            return false;
        }

        #endregion

        #region Key/Value Collections

        internal class KeyCollection : ICollection<TKey>
        {
            public static readonly KeyCollection Value = new KeyCollection();

            private KeyCollection()
            { }

            public int Count => 0;

            public bool IsReadOnly => true;

            public void Add(TKey item)
                => throw new NotSupportedException(SR.MutateKeyCollectionMessage);

            public void Clear()
                => throw new NotSupportedException(SR.MutateKeyCollectionMessage);

            public bool Contains(TKey item)
                => false;

            public void CopyTo(TKey[] array, int arrayIndex)
            { }

            public IEnumerator<TKey> GetEnumerator()
                => Enumerator.Empty<TKey>();

            public bool Remove(TKey item)
                => throw new NotSupportedException(SR.MutateKeyCollectionMessage);

            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();
        }

        internal class ValueCollection : ICollection<TValue>
        {
            public static readonly ValueCollection Value = new ValueCollection();

            private ValueCollection()
            { }

            public int Count => 0;

            public bool IsReadOnly => true;

            public void Add(TValue item)
                => throw new NotSupportedException(SR.MutateValueCollectionMessage);

            public void Clear()
                => throw new NotSupportedException(SR.MutateValueCollectionMessage);

            public bool Contains(TValue item)
                => false;

            public void CopyTo(TValue[] array, int arrayIndex)
            { }

            public IEnumerator<TValue> GetEnumerator()
                => Enumerator.Empty<TValue>();

            public bool Remove(TValue item)
                => throw new NotSupportedException(SR.MutateValueCollectionMessage);

            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();
        }

        #endregion
    }
}
