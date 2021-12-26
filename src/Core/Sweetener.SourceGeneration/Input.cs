// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Sweetener.SourceGeneration
{
    internal static class Input
    {
        public static IEnumerable<string> GetArguments(int count, string nameFormat = "arg{0}")
            => GetIncrementingStrings(nameFormat, count); // arg1, arg2, arg3

        public static IEnumerable<string> GetParameters(int count, string typeFormat = "T{0}", string nameFormat = "arg{0}")
            => GetIncrementingStrings(typeFormat + " " + nameFormat, count); // T1 arg1, T2 arg2, T3 arg3

        public static IEnumerable<string> GetTypeParameters(int count, string typeFormat = "T{0}", bool contravariant = false)
        => GetIncrementingStrings(contravariant ? "in " + typeFormat : typeFormat, count); // T1, T2, T3

        private static IEnumerable<string> GetIncrementingStrings(string formatString, int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (string.IsNullOrWhiteSpace(formatString))
                throw new ArgumentNullException(nameof(formatString));

            if (count == 0)
                return Enumerable.Empty<string>();

            return Enumerable
                .Repeat(formatString, count)
                .Select((p, i) => string.Format(CultureInfo.InvariantCulture, p, count == 1 ? (int?)null : i + 1));
        }
    }
}
