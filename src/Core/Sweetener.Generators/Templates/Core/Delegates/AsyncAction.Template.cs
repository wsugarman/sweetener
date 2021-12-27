// Copyright © William Sugarman.
// Licensed under the MIT License.

using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using Sweetener.Generators.Extensions;
using Sweetener.Generators.Templates.Core.Delegates.Extensions;

namespace Sweetener.Generators.Templates.Core.Delegates
{
    [Project("Sweetener")]
    internal sealed class AsyncActionSourceFile : DelegateSourceFile
    {
        protected override string Name => "AsyncAction";

        protected override IReadOnlyCollection<string> ImportedNamespaces { get; } = new string[] { "System.Threading.Tasks" };

        protected override void WriteDelegate(IndentedTextWriter sourceWriter, int i)
        {
            sourceWriter.WriteXmlSummary($"Encapsulates an asynchronous method that has {Numbers.GetWord(i)} parameter{ (i == 1 ? string.Empty : "s") } and does not return a value.");
            sourceWriter.WriteXmlRemarks(
                "The <see cref=\"Task\"/> returned by the encapsulated method is expected to have been started.",
                "Otherwise, callers will not be able to properly <see langword=\"await\"/> the results of the operation.");
            sourceWriter.WriteXmlTypeParams(i);
            sourceWriter.WriteXmlParams(i);
            sourceWriter.WriteXmlReturns("A task that represents the asynchronous operation.");

            sourceWriter.WriteLine(
                string.Format(
                    CultureInfo.InvariantCulture,
                    "public delegate Task AsyncAction{0}({1});",
                    Input.GetTypeParameters(i, contravariant: true).Enclose(BracketType.AngleBrackets),
                    Input.GetParameters(i).ToCsv()));
        }
    }
}
