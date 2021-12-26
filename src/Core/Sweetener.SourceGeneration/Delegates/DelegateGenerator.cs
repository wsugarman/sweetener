// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Sweetener.SourceGeneration.Extensions;

namespace Sweetener.SourceGeneration.Delegates
{
    internal abstract class DelegateGenerator : ISourceGenerator
    {
        public abstract string FileName { get; }

        public void Execute(GeneratorExecutionContext context)
        {
            DelegateGeneratorOptions options = GetOptions(context.Compilation.AssemblyName, context.AnalyzerConfigOptions.GlobalOptions);

            using StringWriter buffer = new StringWriter();
            using IndentedTextWriter sourceWriter = new IndentedTextWriter(buffer, "    ");

            sourceWriter.WriteXmlComments(Headers.Copyright);
            sourceWriter.WriteLine();
            sourceWriter.WriteXmlComments(Headers.GeneratedCode);
            sourceWriter.WriteLine();

            AppendImports(sourceWriter);

            sourceWriter.WriteLine("namespace " + options.Namespace);
            sourceWriter.WriteLine("{");

            sourceWriter.Indent++;
            WriteDelegates(sourceWriter, options);
            sourceWriter.Indent--;

            sourceWriter.WriteLine("}");
            sourceWriter.WriteLine();

            sourceWriter.Flush();
            context.AddSource($"{FileName}.g.cs", buffer.ToString());
        }

        public virtual void Initialize(GeneratorInitializationContext context)
        { }

        protected virtual IReadOnlyCollection<string> GetImports()
            => Array.Empty<string>();

        protected abstract void WriteDelegates(IndentedTextWriter sourceWriter, DelegateGeneratorOptions options);

        private static DelegateGeneratorOptions GetOptions(string? assemblyName, AnalyzerConfigOptions configOptions)
        {
            if (!configOptions.TryGetValue("build_property.RootNamespace", out string? @namespace))
                throw new InvalidOperationException($"Cannot find 'RootNamespace' for assembly '{assemblyName}'.");

            if (!configOptions.TryGetValue("build_property.DelegateTypeOverloads", out string? overloads))
                throw new InvalidOperationException($"Cannot find 'DelegateTypeOverloads' for assembly '{assemblyName}'.");

            return new DelegateGeneratorOptions(@namespace, int.Parse(overloads, CultureInfo.InvariantCulture));
        }

        private void AppendImports(IndentedTextWriter sourceWriter)
        {
            IReadOnlyCollection<string> imports = GetImports();
            if (imports.Count > 0)
            {
                foreach (string @namespace in imports)
                    sourceWriter.WriteLine($"using {@namespace};");

                sourceWriter.WriteLine();
            }
        }
    }
}
