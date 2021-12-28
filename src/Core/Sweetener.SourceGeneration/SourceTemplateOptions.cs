// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Sweetener.SourceGeneration;

internal sealed class SourceTemplateOptions
{
    public string RootNamespace { get; }

    public IReadOnlyCollection<string> FileHeader { get; }

    public IReadOnlyCollection<string> GeneratedHeader { get; }

    public bool IsUnitTest { get; }

    public SourceTemplateOptions(GeneratorExecutionContext context)
    {
        AnalyzerConfigOptions assemblyOptions = context.AnalyzerConfigOptions.GetOptions(context.Compilation.SyntaxTrees.FirstOrDefault());
        AnalyzerConfigOptions globalOptions   = context.AnalyzerConfigOptions.GlobalOptions;

        if (!globalOptions.TryGetValue("build_property.RootNamespace", out string? rootNamespace))
            throw new ArgumentException("Cannot find 'RootNamespace' property.");

        if (!assemblyOptions.TryGetValue("file_header_template", out string? header))
            throw new ArgumentException("Cannot find 'file_header_template' key in .editorconfig.");

        if (!assemblyOptions.TryGetValue("generated_file_header_template", out string? generated))
            throw new ArgumentException("Cannot find 'generated_file_header_template' key in .editorconfig.");

        RootNamespace   = rootNamespace;
        FileHeader      = SplitLines(header);
        GeneratedHeader = SplitLines(generated);
        IsUnitTest      = (context.Compilation.AssemblyName?.EndsWith(".Test", StringComparison.InvariantCulture)).GetValueOrDefault();
    }

    private static string[] SplitLines(string value)
        => value
            .Split(new[] { "\\r\\n" }, StringSplitOptions.None)
            .SelectMany(line => line.Split(new[] { "\\n" }, StringSplitOptions.None))
            .ToArray();
}
