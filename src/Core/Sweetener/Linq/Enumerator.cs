// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;

namespace Sweetener.Linq
{
    /// <summary>
    /// Provides a set of <see langword="static"/> methods for iterating over elements with an <see cref="IEnumerator{T}"/>.
    /// </summary>
    public static class Enumerator
    {
        /// <summary>
        /// Returns an empty <see cref="IEnumerator{T}"/> that has the specified type argument.
        /// </summary>
        /// <typeparam name="T">The type to assign to the type parameter of the returned generic <see cref="IEnumerator{T}"/>.</typeparam>
        /// <returns>An empty <see cref="IEnumerator{T}"/> whose type argument is <typeparamref name="T"/>.</returns>
        public static IEnumerator<T> Empty<T>()
            => EmptyEnumerator<T>.Value;

        private sealed class EmptyEnumerator<T> : IEnumerator<T>
        {
            public static readonly EmptyEnumerator<T> Value = new EmptyEnumerator<T>();

            private EmptyEnumerator()
            { }

            // Current is undefined if MoveNext() has not been called.
            // For the sake of performance, we'll skip any state and simply throw.
            public T Current => throw new NotSupportedException(SR.EmptyEnumeratorMessage);

            object IEnumerator.Current => Current!;

            // There is precedence for this behavior.
            // The List<T>.Enumerator class can be disposed without affecting any of its operations.
            public void Dispose()
            { }

            public bool MoveNext()
                => false;

            public void Reset()
            { }
        }
    }
}
