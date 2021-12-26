// Copyright © William Sugarman.
// Licensed under the MIT License.

using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.CodeAnalysis;
using Sweetener.SourceGeneration.Delegates.Extensions;
using Sweetener.SourceGeneration.Extensions;

namespace Sweetener.SourceGeneration.Delegates
{
    [Generator]
    internal sealed class TryFuncGenerator : DelegateGenerator
    {
        public override string FileName => "TryFunc";

        public override IReadOnlyCollection<string> ImportedNamespaces { get; } = new string[] { "System.Diagnostics.CodeAnalysis" };

        protected override void WriteDelegate(IndentedTextWriter sourceWriter, int i)
        {
            sourceWriter.WriteXmlSummary($"Encapsulates a method that has {Numbers.GetWord(i)} parameter{ (i == 1 ? string.Empty : "s") } and returns a value indicating whether or not it succeeded.");
            sourceWriter.WriteXmlTypeParams(i);
            sourceWriter.WriteXmlTypeParam("TResult", "The type of the parameter assigned by the method if successful.");
            sourceWriter.WriteXmlParams(i);
            sourceWriter.WriteXmlParam(
                "result",
                "When the method returns, contains the value of type <typeparamref name=\"TResult\"/>,",
                "if the method succeeded, or default value if the method failed.");
            sourceWriter.WriteXmlReturns("<see langword=\"true\"/> if the function completed successfully; otherwise, <see langword=\"false\"/>.");

            sourceWriter.WriteLine(
                string.Format(
                    CultureInfo.InvariantCulture,
                    "public delegate bool TryFunc{0}({1});",
                    Input.GetTypeParameters(i, contravariant: true).Concat("TResult").Enclose(BracketType.AngleBrackets),
                    Input.GetParameters(i).Concat("[MaybeNullWhen(false)] out TResult result").ToCsv()));
        }
    }
}
