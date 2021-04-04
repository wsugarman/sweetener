// Copyright © William Sugarman.
// Licensed under the MIT License.

namespace Sweetener
{
    /// <summary>
    /// Provides methods for interacting with dictionaries.
    /// </summary>
    public static class Map
    {
        /// <summary>
        /// Returns an empty dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <returns>An empty dictionary.</returns>
        public static EmptyDictionary<TKey, TValue> Empty<TKey, TValue>()
            => EmptyDictionary<TKey, TValue>.Value;
    }
}
