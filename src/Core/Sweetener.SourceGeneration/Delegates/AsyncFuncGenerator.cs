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
    internal sealed class AsyncFuncGenerator : DelegateGenerator
    {
        public override string FileName => "AsyncFunc";

        public override IReadOnlyCollection<string> ImportedNamespaces { get; } = new string[] { "System.Threading.Tasks" };

        protected override void WriteDelegate(IndentedTextWriter sourceWriter, int i)
        {
            sourceWriter.WriteXmlSummary(
                $"Encapsulates an asynchronous method that has {Numbers.GetWord(i)} parameter{ (i == 1 ? string.Empty : "s") } and returns a value",
                "of the type specified by the <typeparamref name=\"TResult\"/> parameter.");
            sourceWriter.WriteXmlRemarks(
                "The <see cref=\"Task\"/> returned by the encapsulated method is expected to have been started.",
                "Otherwise, callers will not be able to properly <see langword=\"await\"/> the results of the operation.");
            sourceWriter.WriteXmlTypeParams(i);
            sourceWriter.WriteXmlTypeParam("TResult", "The type of the return value of the method that this delegate encapsulates.");
            sourceWriter.WriteXmlParams(i);
            sourceWriter.WriteXmlReturns(
                "A task that represents the asynchronous operation.The value of its <see cref=\"Task{TResult}.Result\"/>",
                "property contains the return value of the method that this delegate encapsulates.");

            sourceWriter.WriteLine(
                string.Format(
                    CultureInfo.InvariantCulture,
                    "public delegate Task<TResult> AsyncFunc{0}({1});",
                    Input.GetTypeParameters(i, contravariant: true).Concat("TResult").Enclose(BracketType.AngleBrackets),
                    Input.GetParameters(i).Enclose(BracketType.None)));
        }
    }
}
