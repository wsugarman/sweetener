// Copyright © William Sugarman.
// Licensed under the MIT License.

using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Microsoft.CodeAnalysis;
using Sweetener.SourceGeneration.Extensions;
using Sweetener.SourceGeneration.Templates.Core.Tasks;

namespace Sweetener.SourceGeneration.Templates.Core.Tests.Tasks;

[Project("Sweetener.Test")]
[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Templates are loaded via reflection.")]
internal sealed class MultiTaskTestSourceFile : SourceTemplate
{
    protected override string Name => "MultiTask.Test";

    protected override IReadOnlyCollection<string> ImportedNamespaces { get; } = new string[]
    {
            "System",
            "System.Threading.Tasks",
            "Microsoft.VisualStudio.TestTools.UnitTesting",
            "Sweetener.Threading.Tasks",
    };

    protected override string? ChildNamespace => "Threading.Tasks";

    private static readonly IReadOnlyList<(string TaskType, string ValueType, string Value)> Arguments = new List<(string, string, string)>
    {
        ("Task<int>"     , "int"     , "42"),
        ("Task<string>"  , "string"  , "\"Hello World\""),
        ("Task<TimeSpan>", "TimeSpan", "TimeSpan.FromHours(3)"),
        ("Task<double>"  , "double"  , "3.14d"),
        ("Task<long>"    , "long"    , "100L"),
        ("Task<char>"    , "char"    , "'a'"),
        ("Task<Guid>"    , "Guid"    , "Guid.Parse(\"56128c75-379f-4c24-ac02-7ceb335807af\")"),
        ("Task<sbyte>"   , "sbyte"   , "(sbyte)-3")
    };

    protected override void WriteBody(IndentedTextWriter sourceWriter, GeneratorExecutionContext context)
    {
        MultiTaskOptions options = new MultiTaskOptions(context.AnalyzerConfigOptions.GlobalOptions);

        sourceWriter.WriteLine("[TestClass]");
        sourceWriter.WriteLine("public class MultiTaskTest");

        using (sourceWriter.WriteNewBlockScope())
        {
            for (int i = 2; i <= options.TypeOverloads; i++)
            {
                WriteTestMethod(sourceWriter, i);

                // Newline between methods
                if (i != options.TypeOverloads)
                    sourceWriter.WriteLineNoTabs(string.Empty);
            }
        }
    }

    private static void WriteTestMethod(IndentedTextWriter sourceWriter, int i)
    {
        sourceWriter.WriteLine("[TestMethod]");
        sourceWriter.WriteLine(string.Format(CultureInfo.InvariantCulture, "public async Task WhenAllT{0}()", i));

        using (sourceWriter.WriteNewBlockScope())
        {
            // Test the various null input cases
            WriteInputValidation(sourceWriter, i);

            // Now show a successful invocation
            WriteSuccessfulValidation(sourceWriter, i);
        }
    }

    private static void WriteInputValidation(IndentedTextWriter sourceWriter, int i)
    {
        sourceWriter.WriteSingleLineComments("Bad Input");
        for (int j = 1; j <= i; j++)
        {
            sourceWriter.WriteLine("await Assert.ThrowsExceptionAsync<ArgumentNullException>(");
            sourceWriter.Indent++;
            sourceWriter.WriteLine("() => MultiTask.WhenAll(");

            // Replace the j-th element with 'null'
            IEnumerable<string> input = Enumerable
                .Range(0, i)
                .Select(k => Arguments[k].Value)
                .Select((value, k) => k == j - 1 ? $"({Arguments[k].TaskType})null!" : $"Task.FromResult({value})")
                .Select((task, k) => task + (k == i - 1 ? ")).ConfigureAwait(false);" : ","));

            sourceWriter.Indent++;
            foreach (string line in input)
                sourceWriter.WriteLine(line);

            sourceWriter.Indent -= 2;
            sourceWriter.WriteLineNoTabs(string.Empty);
        }
    }

    private static void WriteSuccessfulValidation(IndentedTextWriter sourceWriter, int i)
    {
        sourceWriter.WriteSingleLineComments("Success");

        string resultTuple = Arguments
            .Take(i)
            .Select((x, i) => x.ValueType + " value" + (i + 1))
            .Enclose(BracketType.Parentheses);
        sourceWriter.WriteLine($"{resultTuple} = await MultiTask.WhenAll(");

        sourceWriter.Indent++;
        for (int j = 0; j < i; j++)
        {
            string suffix = j == i - 1 ? ").ConfigureAwait(false);" : ",";
            sourceWriter.WriteLine($"Task.FromResult({Arguments[j].Value})" + suffix);
        }
        sourceWriter.Indent--;

        // Assert (and align those assertions)
        sourceWriter.WriteLineNoTabs(string.Empty);

        int maxSize = Arguments.Take(i).Select(x => x.Value.Length).Max();
        for (int j = 1; j <= i; j++)
        {
            sourceWriter.WriteLine(
                string.Format(
                    CultureInfo.InvariantCulture,
                    "Assert.AreEqual({0}, value{1});",
                    Arguments[j - 1].Value.PadRight(maxSize),
                    j));
        }
    }
}
