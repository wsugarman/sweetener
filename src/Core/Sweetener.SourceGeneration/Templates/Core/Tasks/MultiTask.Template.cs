// Copyright © William Sugarman.
// Licensed under the MIT License.

using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.CodeAnalysis;
using Sweetener.SourceGeneration.Extensions;

namespace Sweetener.SourceGeneration.Templates.Core.Tasks;

[Project("Sweetener")]
internal sealed class MultiTaskSourceFile : SourceTemplate
{
    protected override string Name => "MultiTask";

    protected override IReadOnlyCollection<string> ImportedNamespaces { get; } = new string[]
    {
            "System",
            "System.Threading.Tasks",
    };

    protected override string? ChildNamespace => "Threading.Tasks";

    protected override void WriteBody(IndentedTextWriter sourceWriter, GeneratorExecutionContext context)
    {
        MultiTaskOptions options = new MultiTaskOptions(context.AnalyzerConfigOptions.GlobalOptions);

        sourceWriter.WriteXmlSummary(
            "Provides a set of <see langword=\"static\"/> methods for interacting with multiple <see cref=\"Task{T}\"/> objects",
            "whose type arguments may differ.");
        sourceWriter.WriteLine("public static class MultiTask");

        using (sourceWriter.WriteNewBlockScope())
        {
            for (int i = 2; i <= options.TypeOverloads; i++)
            {
                WriteMethod(sourceWriter, i);

                // Newline between methods
                if (i != options.TypeOverloads)
                    sourceWriter.WriteLineNoTabs(string.Empty);
            }
        }
    }

    private static void WriteMethod(IndentedTextWriter sourceWriter, int i)
    {
        const string taskNameFormat = "task{0}";

        sourceWriter.WriteXmlSummary("Creates a task that will complete when all of the <see cref=\"Task{TResult}\"/> objects have completed.");
        sourceWriter.WriteXmlTypeParams(i, "T{0}", "The result type of the {0}task.");
        sourceWriter.WriteXmlParams(i, taskNameFormat, "The {0}task to wait on for completion.");
        sourceWriter.WriteXmlReturns("A task that represents the completion of all of the supplied tasks.");
        sourceWriter.WriteLine(
            string.Format(
                CultureInfo.InvariantCulture,
                "public static Task<({0})> WhenAll<{0}>({1})",
                Input.GetTypeParameters(i).ToCsv(),
                Input.GetParameters(i, "Task<T{0}>", taskNameFormat).ToCsv()));

        using (sourceWriter.WriteNewBlockScope())
        {
            // Write input validation
            for (int j = 1; j <= i; j++)
            {
                sourceWriter.WriteLine(string.Format(CultureInfo.InvariantCulture, $"if ({taskNameFormat} is null)", j));
                sourceWriter.WriteSingleLineBlock(string.Format(CultureInfo.InvariantCulture, $"throw new ArgumentNullException(nameof({taskNameFormat}));", j));
                sourceWriter.WriteLineNoTabs(string.Empty);
            }

            // Write WhenAll logic
            sourceWriter.WriteLine("return Task");
            sourceWriter.Indent++;
            sourceWriter.WriteLine($".WhenAll({Input.GetArguments(i, taskNameFormat).ToCsv()})");
            sourceWriter.WriteLine(".WithResultOnSuccess(");
            sourceWriter.Indent++;
            sourceWriter.WriteLine($"t => ({Input.GetArguments(i, "t.Task{0}.Result").ToCsv()}),");
            sourceWriter.WriteLine($"({Input.GetArguments(i, "Task{0}: " + taskNameFormat).ToCsv()}));");
            sourceWriter.Indent -= 2;
        }
    }
}
