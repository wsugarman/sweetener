// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Sweetener.SourceGeneration.Extensions;

internal static class IndentedTextWriterExtensions
{
    public static IDisposable WriteNewBlockScope(this IndentedTextWriter sourceWriter)
        => new BlockScope(sourceWriter);

    public static void WriteSingleLineBlock(this IndentedTextWriter sourceWriter, string line)
    {
        if (sourceWriter is null)
            throw new ArgumentNullException(nameof(sourceWriter));

        sourceWriter.Indent++;
        sourceWriter.WriteLine(line);
        sourceWriter.Indent--;
    }

    public static void WriteSingleLineComments(this IndentedTextWriter sourceWriter, params string[] comments)
        => sourceWriter.WriteSingleLineComments((IEnumerable<string>)comments);

    public static void WriteSingleLineComments(this IndentedTextWriter sourceWriter, IEnumerable<string> comments)
    {
        if (sourceWriter is null)
            throw new ArgumentNullException(nameof(sourceWriter));

        if (comments is null)
            throw new ArgumentNullException(nameof(comments));

        foreach (string line in comments)
            sourceWriter.WriteLine("// " + line);
    }

    public static void WriteXmlParams(
        this IndentedTextWriter sourceWriter,
        int count,
        string nameFormat,
        string descriptionFormat)
        => sourceWriter.WriteXmlDocPattern(XmlDoc.ParamTag, count, nameFormat, descriptionFormat);

    public static void WriteXmlTypeParams(
        this IndentedTextWriter sourceWriter,
        int count,
        string nameFormat,
        string descriptionFormat)
        => sourceWriter.WriteXmlDocPattern(XmlDoc.TypeParamTag, count, nameFormat, descriptionFormat);

    public static void WriteXmlParam(
        this IndentedTextWriter sourceWriter,
        string name,
        params string[] description)
        => sourceWriter.WriteXmlDocElement(XmlDoc.ParamTag, name, description);

    public static void WriteXmlRemarks(this IndentedTextWriter sourceWriter, params string[] summary)
        => sourceWriter.WriteXmlDocElement(XmlDoc.RemarksTag, summary);

    public static void WriteXmlReturns(this IndentedTextWriter sourceWriter, params string[] summary)
        => sourceWriter.WriteXmlDocElement(XmlDoc.ReturnsTag, summary);

    public static void WriteXmlSummary(this IndentedTextWriter sourceWriter, params string[] summary)
        => sourceWriter.WriteXmlDocElement(XmlDoc.SummaryTag, summary, forceMultiline: true);

    public static void WriteXmlTypeParam(
        this IndentedTextWriter sourceWriter,
        string name,
        params string[] description)
        => sourceWriter.WriteXmlDocElement("typeparam", name, description);

    private static void WriteXmlDocElement(
        this IndentedTextWriter sourceWriter,
        string tag,
        string[] description,
        bool forceMultiline = false)
        => sourceWriter.WriteXmlDocElement(tag, null, description, forceMultiline);

    private static void WriteXmlDocElement(
        this IndentedTextWriter sourceWriter,
        string tag,
        string? name,
        string[] description,
        bool forceMultiline = false)
    {
        {
            if (sourceWriter is null)
                throw new ArgumentNullException(nameof(sourceWriter));

            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            if (description is null)
                throw new ArgumentNullException(nameof(description));

            string nameProperty = name is null ? string.Empty : $" name=\"{name}\"";
            if (description.Length == 1 && !forceMultiline)
            {
                sourceWriter.WriteLine($"/// <{tag}{nameProperty}>{description[0]}</{tag}>");
            }
            else
            {
                sourceWriter.WriteLine($"/// <{tag}{nameProperty}>");
                foreach (string line in description)
                {
                    sourceWriter.Write("/// ");
                    sourceWriter.WriteLine(line);
                }
                sourceWriter.WriteLine($"/// </{tag}>");
            }
        }
    }

    private static void WriteXmlDocPattern(
        this IndentedTextWriter sourceWriter,
        string tag,
        int count,
        string nameFormat,
        string descriptionFormat)
    {
        if (sourceWriter is null)
            throw new ArgumentNullException(nameof(sourceWriter));

        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));

        IEnumerable<(string, string)> parameters = count == 1
            ? new (string, string)[] { ("", "") }
            : Enumerable
                .Range(1, count)
                .Select(i => (i.ToString(CultureInfo.InvariantCulture), Numbers.GetNthWord(i) + " "));

        foreach ((string suffix, string qualifier) in parameters)
            sourceWriter.WriteXmlDocElement(
                tag,
                string.Format(CultureInfo.InvariantCulture, nameFormat, suffix),
                new string[] { string.Format(CultureInfo.InvariantCulture, descriptionFormat, qualifier) });
    }

    private sealed class BlockScope : IDisposable
    {
        private readonly int _expectedIndent;
        private readonly IndentedTextWriter _sourceWriter;

        public BlockScope(IndentedTextWriter sourceWriter)
        {
            _sourceWriter = sourceWriter ?? throw new ArgumentNullException(nameof(sourceWriter));

            _sourceWriter.WriteLine("{");
            _expectedIndent = ++sourceWriter.Indent;
        }

        public void Dispose()
        {
            if (_sourceWriter.Indent != _expectedIndent)
                throw new InvalidOperationException($"Expected indent at {_expectedIndent}, but found {_sourceWriter.Indent}");

            _sourceWriter.Indent--;
            _sourceWriter.WriteLine("}");
        }
    }
}
