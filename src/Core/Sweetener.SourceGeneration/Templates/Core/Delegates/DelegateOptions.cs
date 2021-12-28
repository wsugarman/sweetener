// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Globalization;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Sweetener.SourceGeneration.Templates.Core.Delegates;

internal sealed class DelegateOptions
{
    public int TypeOverloads { get; }

    public DelegateOptions(AnalyzerConfigOptions globalOptions)
    {
        if (!globalOptions.TryGetValue("build_property.DelegateTypeOverloads", out string? overloads))
            throw new ArgumentException($"Cannot find 'DelegateTypeOverloads' property.");

        if (!int.TryParse(overloads, NumberStyles.Integer, CultureInfo.InvariantCulture, out int typeOverloads))
            throw new ArgumentException($"Cannot parse 'DelegateTypeOverloads' property value '{overloads}' as an integer.");

        if (typeOverloads < 0)
            throw new ArgumentOutOfRangeException("'DelegateTypeOverloads' property cannot be negative.", (Exception?)null);

        TypeOverloads = typeOverloads;
    }
}
