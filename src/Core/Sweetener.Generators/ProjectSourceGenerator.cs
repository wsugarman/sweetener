// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Sweetener.Generators.Extensions;

namespace Sweetener.Generators;

[Generator]
internal sealed class ProjectSourceGenerator : ISourceGenerator
{
    private const string TabString = "    ";

    public void Execute(GeneratorExecutionContext context)
    {
        SourceTemplateOptions options = new SourceTemplateOptions(context);

        foreach (SourceTemplate template in GetSourceTemplates(context))
        {
            using StringWriter buffer = new StringWriter();
#pragma warning disable CA2000 // False positive
            using (IndentedTextWriter sourceWriter = new IndentedTextWriter(buffer, TabString))
                template.Write(sourceWriter, options, context);
#pragma warning restore CA2000

            context.AddSource(template.FileName, buffer.ToString());
        }
    }

    private static IEnumerable<SourceTemplate> GetSourceTemplates(GeneratorExecutionContext context)
    {
        if (string.IsNullOrWhiteSpace(context.Compilation.AssemblyName))
            throw new ArgumentException("Cannot find assembly name.");

        string assemblyName = context.Compilation.AssemblyName!;
        return typeof(ProjectSourceGenerator).Assembly
            .GetTypes()
            .Where(t => t.InheritsFrom(typeof(SourceTemplate)) && t.HasDefaultCtor())
            .Where(t => MatchesCurrentProject(t, assemblyName))
            .Select(t => (SourceTemplate)Activator.CreateInstance(t));
    }

    public void Initialize(GeneratorInitializationContext context)
    { }

    private static bool MatchesCurrentProject(Type type, string project)
    {
        ProjectAttribute? attribute = Attribute.GetCustomAttribute(type, typeof(ProjectAttribute)) as ProjectAttribute;
        return attribute is not null && attribute.Name == project;
    }
}
