// Copyright © William Sugarman.
// Licensed under the MIT License.

using System.CodeDom.Compiler;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Sweetener.SourceGeneration.Delegates
{
    internal abstract class DelegateGenerator : SourceGenerator<DelegateGeneratorOptions>
    {
        protected override DelegateGeneratorOptions CreateOptions(AnalyzerConfigOptions assemblyOptions, AnalyzerConfigOptions globalOptions)
            => new DelegateGeneratorOptions(assemblyOptions, globalOptions);

        protected override void Execute(IndentedTextWriter sourceWriter, DelegateGeneratorOptions options)
        {
            for (int i = 0; i <= options.TypeOverloads; i++)
            {
                WriteDelegate(sourceWriter, i);

                // Newline between delegates
                if (i != options.TypeOverloads)
                    sourceWriter.WriteLineNoTabs(string.Empty);
            }
        }

        protected abstract void WriteDelegate(IndentedTextWriter sourceWriter, int i);
    }
}
