// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Test
{
    [TestClass]
    public class EmptyDictionaryTest
    {
        [TestMethod]
        public void Count()
        {
            EmptyDictionary<string, int> elements = Map.Empty<string, int>();

            // ICollection<T>
            ICollection<KeyValuePair<string, int>> c = elements;
            Assert.AreEqual(0, c.Count);

            // IReadOnlyCollection<T>
            IReadOnlyCollection<KeyValuePair<string, int>> r = elements;
            Assert.AreEqual(0, r.Count);
        }

        [TestMethod]
        public void IsReadOnly()
        {
            ICollection<KeyValuePair<string, int>> c = Map.Empty<string, int>();
            Assert.IsTrue(c.IsReadOnly);
        }

        [TestMethod]
        public void Add()
        {
            EmptyDictionary<string, int> elements = Map.Empty<string, int>();

            // ICollection<T>
            ICollection<KeyValuePair<string, int>> c = elements;
            Assert.ThrowsException<NotSupportedException>(() => c.Add(KeyValuePair.Create("Uh-oh", 12345)));

            // IDictionary<TKey, TValue>
            IDictionary<string, int> d = elements;
            Assert.ThrowsException<NotSupportedException>(() => d.Add("Uh-oh", 12345));
        }

        [TestMethod]
        public void Clear()
        {
            ICollection<KeyValuePair<string, int>> c = Map.Empty<string, int>();
            Assert.ThrowsException<NotSupportedException>(() => c.Clear());
        }

        [TestMethod]
        public void Contains()
        {
            ICollection<KeyValuePair<string, int>> c = Map.Empty<string, int>();
            Assert.IsFalse(c.Contains(KeyValuePair.Create("Key", 1)));
        }

        [TestMethod]
        public void ContainsKey()
        {
            EmptyDictionary<string, int> elements = Map.Empty<string, int>();

            // IDictionary<TKey, TValue>
            IDictionary<string, int> d = elements;
            Assert.IsFalse(d.ContainsKey("Key"));

            // IReadOnlyDictionary<TKey, TValue>
            IReadOnlyDictionary<string, int> r = elements;
            Assert.IsFalse(r.ContainsKey("Key"));
        }

        [TestMethod]
        public void CopyTo()
        {
            ICollection<KeyValuePair<string, int>> source = Map.Empty<string, int>();
            KeyValuePair<string, int>[] dest = Array.Empty<KeyValuePair<string, int>>();
            source.CopyTo(dest, 0); // No error
        }

        [TestMethod]
        public void GetEnumerator()
        {
            EmptyDictionary<string, int> elements = Map.Empty<string, int>();

            // IEnumerable
            IEnumerable e1 = elements;
            Assert.AreEqual(0, e1.Cast<KeyValuePair<string, int>>().Count());

            // IEnumerable<T>
            IEnumerable<KeyValuePair<string, int>> e2 = elements;
            Assert.AreEqual(0, e2.Count());
        }

        [TestMethod]
        public void Indexer()
        {
            EmptyDictionary<string, int> elements = Map.Empty<string, int>();

            // IDictionary<TKey, TValue>
            IDictionary<string, int> d = elements;
            Assert.ThrowsException<KeyNotFoundException>(() => d["Hello"]);
            Assert.ThrowsException<NotSupportedException>(() => d["World"] = 42);

            // IReadOnlyDictionary<TKey, TValue>
            IReadOnlyDictionary<string, int> r = elements;
            Assert.ThrowsException<KeyNotFoundException>(() => r["Hello"]);
        }

        [TestMethod]
        public void Keys()
        {
            EmptyDictionary<string, int> elements = Map.Empty<string, int>();

            // IDictionary<TKey, TValue>
            IDictionary<string, int> d = elements;
            Assert.AreEqual(typeof(EmptyDictionary<string, int>.KeyCollection), d.Keys.GetType());

            // IReadOnlyDictionary<TKey, TValue>
            IReadOnlyDictionary<string, int> r = elements;
            Assert.AreEqual(typeof(EmptyDictionary<string, int>.KeyCollection), r.Keys.GetType());
        }

        [TestMethod]
        public void Remove()
        {
            EmptyDictionary<string, int> elements = Map.Empty<string, int>();

            // ICollection<T>
            ICollection<KeyValuePair<string, int>> c = elements;
            Assert.ThrowsException<NotSupportedException>(() => c.Remove(KeyValuePair.Create("Uh-oh", 12345)));

            // IDictionary<TKey, TValue>
            IDictionary<string, int> d = elements;
            Assert.ThrowsException<NotSupportedException>(() => d.Remove("Uh-oh"));
        }

        [TestMethod]
        public void TryGetValue()
        {
            EmptyDictionary<string, int> elements = Map.Empty<string, int>();

            // IDictionary<TKey, TValue>
            IDictionary<string, int> d = elements;
            Assert.IsFalse(d.TryGetValue("Key", out int actual));
            Assert.AreEqual(default, actual);

            // IReadOnlyDictionary<TKey, TValue>
            IReadOnlyDictionary<string, int> r = elements;
            Assert.IsFalse(r.TryGetValue("Key", out actual));
            Assert.AreEqual(default, actual);
        }

        [TestMethod]
        public void Values()
        {
            EmptyDictionary<string, int> elements = Map.Empty<string, int>();

            // IDictionary<TKey, TValue>
            IDictionary<string, int> d = elements;
            Assert.AreEqual(typeof(EmptyDictionary<string, int>.ValueCollection), d.Values.GetType());

            // IReadOnlyDictionary<TKey, TValue>
            IReadOnlyDictionary<string, int> r = elements;
            Assert.AreEqual(typeof(EmptyDictionary<string, int>.ValueCollection), r.Values.GetType());
        }
    }

    [TestClass]
    public class EmptyKeyCollectionTest
    {
        [TestMethod]
        public void Add()
            => Assert.ThrowsException<NotSupportedException>(() => EmptyDictionary<string, int>.KeyCollection.Value.Add("Nope"));

        [TestMethod]
        public void Clear()
            => Assert.ThrowsException<NotSupportedException>(() => EmptyDictionary<string, int>.KeyCollection.Value.Clear());

        [TestMethod]
        public void Contains()
            => Assert.IsFalse(EmptyDictionary<string, int>.KeyCollection.Value.Contains("Anyone?"));

        [TestMethod]
        public void Count()
            => Assert.AreEqual(0, EmptyDictionary<string, int>.KeyCollection.Value.Count);

        [TestMethod]
        public void CopyTo()
        {
            string[] dest = new string[5];
            EmptyDictionary<string, int>.KeyCollection.Value.CopyTo(dest, 2); // No error
        }

        [TestMethod]
        public void GetEnumerator()
        {
            // IEnumerable
            IEnumerable e1 = EmptyDictionary<string, int>.KeyCollection.Value;
            Assert.AreEqual(0, e1.Cast<string>().Count());

            // IEnumerable<T>
            IEnumerable<string> e2 = EmptyDictionary<string, int>.KeyCollection.Value;
            Assert.AreEqual(0, e2.Count());
        }

        [TestMethod]
        public void IsReadOnly()
            => Assert.IsTrue(EmptyDictionary<string, int>.KeyCollection.Value.IsReadOnly);

        [TestMethod]
        public void Remove()
            => Assert.ThrowsException<NotSupportedException>(() => EmptyDictionary<string, int>.KeyCollection.Value.Remove("No"));
    }

    [TestClass]
    public class EmptyValueCollectionTest
    {
        [TestMethod]
        public void Add()
            => Assert.ThrowsException<NotSupportedException>(() => EmptyDictionary<string, int>.ValueCollection.Value.Add(123));

        [TestMethod]
        public void Clear()
            => Assert.ThrowsException<NotSupportedException>(() => EmptyDictionary<string, int>.ValueCollection.Value.Clear());

        [TestMethod]
        public void Contains()
            => Assert.IsFalse(EmptyDictionary<string, int>.ValueCollection.Value.Contains(456));

        [TestMethod]
        public void Count()
            => Assert.AreEqual(0, EmptyDictionary<string, int>.ValueCollection.Value.Count);

        [TestMethod]
        public void CopyTo()
        {
            int[] dest = new int[7];
            EmptyDictionary<string, int>.ValueCollection.Value.CopyTo(dest, 3); // No error
        }

        [TestMethod]
        public void GetEnumerator()
        {
            // IEnumerable
            IEnumerable e1 = EmptyDictionary<string, int>.ValueCollection.Value;
            Assert.AreEqual(0, e1.Cast<string>().Count());

            // IEnumerable<T>
            IEnumerable<int> e2 = EmptyDictionary<string, int>.ValueCollection.Value;
            Assert.AreEqual(0, e2.Count());
        }

        [TestMethod]
        public void IsReadOnly()
            => Assert.IsTrue(EmptyDictionary<string, int>.ValueCollection.Value.IsReadOnly);

        [TestMethod]
        public void Remove()
            => Assert.ThrowsException<NotSupportedException>(() => EmptyDictionary<string, int>.ValueCollection.Value.Remove(789));
    }
}
