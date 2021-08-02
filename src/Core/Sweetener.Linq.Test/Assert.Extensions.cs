// Copyright © William Sugarman.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Linq.Test
{
    internal static class AssertExtensions
    {
        public static void AreSequencesEqual<T>(this Assert assert, IEnumerable<T> actual, params T[] expected)
            => assert.AreSequencesEqual(expected, actual);

        public static void AreSequencesEqual<T>(this Assert assert, IEnumerable<T> expected, IEnumerable<T> actual)
        {
            IEnumerator<T> f = expected.GetEnumerator();
            IEnumerator<T> s = actual.GetEnumerator();

            while (f.MoveNext())
            {
                Assert.IsTrue(s.MoveNext(), "First sequence is larger than the second.");
                Assert.AreEqual(f.Current, s.Current);
            }

            Assert.IsFalse(s.MoveNext(), "Second sequence is larger than the first.");
        }

        public static void AreSequencesEqual(this Assert assert, IEnumerable actual, params object[] expected)
            => assert.AreSequencesEqual((IEnumerable)expected, actual);

        public static void AreSequencesEqual(this Assert assert, IEnumerable expected, IEnumerable actual)
        {
            IEnumerator f = expected.GetEnumerator();
            IEnumerator s = actual.GetEnumerator();

            while (f.MoveNext())
            {
                Assert.IsTrue(s.MoveNext(), "First sequence is larger than the second.");
                Assert.AreEqual(f.Current, s.Current);
            }

            Assert.IsFalse(s.MoveNext(), "Second sequence is larger than the first.");
        }
    }
}
