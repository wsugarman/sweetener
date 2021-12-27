﻿// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Sweetener.SourceGeneration.Extensions;

namespace Sweetener.SourceGeneration
{
    internal abstract class SourceGenerator<TOptions> : ISourceGenerator
        where TOptions : SourceGeneratorOptions
    {
        public abstract string FileName { get; }

        public virtual IReadOnlyCollection<string> ImportedNamespaces => Array.Empty<string>();

        public string TabString { get; protected set; } = "    ";

        public void Execute(GeneratorExecutionContext context)
        {
            AnalyzerConfigOptions assemblyOptions = context.AnalyzerConfigOptions.GetOptions(context.Compilation.SyntaxTrees.FirstOrDefault());
            TOptions options = CreateOptions(assemblyOptions, context.AnalyzerConfigOptions.GlobalOptions);

            using StringWriter buffer = new StringWriter();
            using IndentedTextWriter sourceWriter = new IndentedTextWriter(buffer, TabString);

            sourceWriter.WriteXmlComments(options.FileHeader);
            sourceWriter.WriteLine();
            sourceWriter.WriteXmlComments(options.GeneratedHeader);
            sourceWriter.WriteLine();

            AppendImports(sourceWriter);

            sourceWriter.WriteLine("namespace " + options.Namespace);

            using (sourceWriter.WriteNewBlockScope())
                Execute(sourceWriter, options);

            sourceWriter.Flush();
            context.AddSource($"{FileName}.g.cs", buffer.ToString());
        }

        public virtual void Initialize(GeneratorInitializationContext context)
        { }

        protected abstract TOptions CreateOptions(AnalyzerConfigOptions assemblyOptions, AnalyzerConfigOptions globalOptions);

        protected abstract void Execute(IndentedTextWriter sourceWriter, TOptions options);

        private void AppendImports(IndentedTextWriter sourceWriter)
        {
            if (ImportedNamespaces.Count > 0)
            {
                foreach (string @namespace in ImportedNamespaces)
                    sourceWriter.WriteLine($"using {@namespace};");

                sourceWriter.WriteLine();
            }
        }
    }
}
