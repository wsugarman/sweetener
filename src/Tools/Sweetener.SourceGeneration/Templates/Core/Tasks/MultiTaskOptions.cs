﻿// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Globalization;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Sweetener.SourceGeneration.Templates.Core.Tasks;

internal sealed class MultiTaskOptions
{
    public int TypeOverloads { get; }

    public MultiTaskOptions(AnalyzerConfigOptions globalOptions)
    {
        if (!globalOptions.TryGetValue("build_property.TaskTypeOverloads", out string? overloads))
            throw new ArgumentException($"Cannot find 'TaskTypeOverloads' property.");

        if (!int.TryParse(overloads, NumberStyles.Integer, CultureInfo.InvariantCulture, out int typeOverloads))
            throw new ArgumentException($"Cannot parse 'TaskTypeOverloads' property value '{overloads}' as an integer.");

        if (typeOverloads < 0)
            throw new ArgumentOutOfRangeException("'TaskTypeOverloads' property cannot be negative.", (Exception?)null);

        TypeOverloads = typeOverloads;
    }
}
