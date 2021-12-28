// Copyright © William Sugarman.
// Licensed under the MIT License.

using System.CodeDom.Compiler;
using Microsoft.CodeAnalysis;

namespace Sweetener.SourceGeneration.Templates.Core.Delegates;

internal abstract class DelegateSourceTemplate : SourceTemplate
{
    protected override void WriteBody(IndentedTextWriter sourceWriter, GeneratorExecutionContext context)
    {
        DelegateOptions options = new DelegateOptions(context.AnalyzerConfigOptions.GlobalOptions);
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
