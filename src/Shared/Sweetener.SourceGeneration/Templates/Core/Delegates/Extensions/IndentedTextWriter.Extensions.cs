// Copyright © William Sugarman.
// Licensed under the MIT License.

using System.CodeDom.Compiler;
using Sweetener.SourceGeneration.Extensions;

namespace Sweetener.SourceGeneration.Templates.Core.Delegates.Extensions;

internal static class IndentedTextWriterExtensions
{
    public static void WriteXmlParams(this IndentedTextWriter sourceWriter, int count)
        => sourceWriter.WriteXmlParams(
            count,
            nameFormat: "arg{0}",
            descriptionFormat: "The {0}parameter of the method that this delegate encapsulates.");

    public static void WriteXmlTypeParams(this IndentedTextWriter sourceWriter, int count)
        => sourceWriter.WriteXmlTypeParams(
            count,
            nameFormat: "T{0}",
            descriptionFormat: "The type of the {0}parameter of the method that this delegate encapsulates.");
}
