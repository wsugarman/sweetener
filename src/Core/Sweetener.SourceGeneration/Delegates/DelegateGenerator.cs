// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Sweetener.SourceGeneration.Extensions;

namespace Sweetener.SourceGeneration.Delegates
{
    internal abstract class DelegateGenerator : ISourceGenerator
    {
        public abstract string FileName { get; }

        public virtual IReadOnlyCollection<string> ImportedNamespaces => Array.Empty<string>();

        public void Execute(GeneratorExecutionContext context)
        {
            AnalyzerConfigOptions assemblyOptions = context.AnalyzerConfigOptions.GetOptions(context.Compilation.SyntaxTrees.FirstOrDefault());
            DelegateGeneratorOptions options = new DelegateGeneratorOptions(assemblyOptions, context.AnalyzerConfigOptions.GlobalOptions);

            using StringWriter buffer = new StringWriter();
            using IndentedTextWriter sourceWriter = new IndentedTextWriter(buffer, "    ");

            sourceWriter.WriteXmlComments(options.Copyright);
            sourceWriter.WriteLine();
            sourceWriter.WriteXmlComments(options.GeneratedHeader);
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

        protected abstract void WriteDelegate(IndentedTextWriter sourceWriter, int i);

        private void WriteDelegates(IndentedTextWriter sourceWriter, DelegateGeneratorOptions options)
        {
            for (int i = 0; i <= options.TypeOverloads; i++)
            {
                WriteDelegate(sourceWriter, i);

                // Newline between methods
                if (i != options.TypeOverloads)
                    sourceWriter.WriteLineNoTabs(string.Empty);
            }
        }

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
