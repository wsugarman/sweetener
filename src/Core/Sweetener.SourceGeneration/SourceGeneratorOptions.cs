﻿// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Sweetener.SourceGeneration
{
    internal abstract class SourceGeneratorOptions
    {
        public string Namespace { get; }

        public IReadOnlyCollection<string> Copyright { get; }

        public IReadOnlyCollection<string> GeneratedHeader { get; }

        protected SourceGeneratorOptions(AnalyzerConfigOptions assemblyOptions, AnalyzerConfigOptions globalOptions)
        {
            if (!globalOptions.TryGetValue("build_property.RootNamespace", out string? @namespace))
                throw new ArgumentException("Cannot find 'RootNamespace' property.");

            if (!assemblyOptions.TryGetValue("file_header_template", out string? copyright))
                throw new ArgumentException("Cannot find 'file_header_template' key in .editorconfig.");

            if (!assemblyOptions.TryGetValue("generated_file_header_template", out string? generated))
                throw new ArgumentException("Cannot find 'generated_file_header_template' key in .editorconfig.");

            Namespace = @namespace;
            Copyright = SplitLines(copyright);
            GeneratedHeader = SplitLines(generated);
        }

        private static string[] SplitLines(string value)
            => value
                .Split(new[] { "\\r\\n" }, StringSplitOptions.None)
                .SelectMany(line => line.Split(new[] { "\\n" }, StringSplitOptions.None))
                .ToArray();
    }
}