// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Sweetener.Generators.Extensions;

namespace Sweetener.Generators;

internal abstract class SourceTemplate
{
    public string FileName => Name + Extension;

    protected abstract string Name { get; }

    protected virtual string Extension => ".g.cs";

    protected virtual string? ChildNamespace => null;

    protected virtual IReadOnlyCollection<string> ImportedNamespaces => Array.Empty<string>();

    public void Write(IndentedTextWriter sourceWriter, SourceTemplateOptions options, GeneratorExecutionContext context)
    {
        if (sourceWriter is null)
            throw new ArgumentNullException(nameof(sourceWriter));

        sourceWriter.WriteSingleLineComments(options.FileHeader);
        sourceWriter.WriteLine();
        sourceWriter.WriteSingleLineComments(options.GeneratedHeader);
        sourceWriter.WriteLine();

        AppendImports(sourceWriter);

        string @namespace = options.RootNamespace + (string.IsNullOrWhiteSpace(ChildNamespace) ? null : '.' + ChildNamespace);
        sourceWriter.WriteLine($"namespace {@namespace}");

        using (sourceWriter.WriteNewBlockScope())
            WriteBody(sourceWriter, context);
    }

    protected abstract void WriteBody(IndentedTextWriter sourceWriter, GeneratorExecutionContext context);

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
