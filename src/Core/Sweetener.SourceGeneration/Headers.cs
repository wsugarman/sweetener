// Copyright © William Sugarman.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Sweetener.SourceGeneration
{
    internal static class Headers
    {
        public static IReadOnlyCollection<string> Copyright { get; } = new string[]
        {
            "Copyright © William Sugarman.",
            "Licensed under the MIT License.",
        };

        public static IReadOnlyCollection<string> GeneratedCode { get; } = new string[]
        {
            "This file is auto-generated.",
            "It cannot be modified.",
        };
    }
}
