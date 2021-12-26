// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace Sweetener.SourceGeneration.Extensions
{
    internal static class IndentedTextWriterExtensions
    {
        public static IndentedTextWriter WriteXmlComments(this IndentedTextWriter sourceWriter, IEnumerable<string> comments)
        {
            if (sourceWriter is null)
                throw new ArgumentNullException(nameof(sourceWriter));

            if (comments is null)
                throw new ArgumentNullException(nameof(comments));

            foreach (string line in comments)
                sourceWriter.WriteLine("// " + line);

            return sourceWriter;
        }

        public static IndentedTextWriter WriteXmlParam(
            this IndentedTextWriter sourceWriter,
            string name,
            params string[] description)
            => sourceWriter.WriteXmlDocElement("param", name, description);

        public static IndentedTextWriter WriteXmlRemarks(this IndentedTextWriter sourceWriter, params string[] summary)
            => sourceWriter.WriteXmlDocElement("remarks", summary);

        public static IndentedTextWriter WriteXmlReturns(this IndentedTextWriter sourceWriter, params string[] summary)
            => sourceWriter.WriteXmlDocElement("returns", summary);

        public static IndentedTextWriter WriteXmlSummary(this IndentedTextWriter sourceWriter, params string[] summary)
            => sourceWriter.WriteXmlDocElement("summary", summary, forceMultiline: true);

        public static IndentedTextWriter WriteXmlTypeParam(
            this IndentedTextWriter sourceWriter,
            string name,
            params string[] description)
            => sourceWriter.WriteXmlDocElement("typeparam", name, description);

        private static IndentedTextWriter WriteXmlDocElement(
            this IndentedTextWriter sourceWriter,
            string tag,
            string[] description,
            bool forceMultiline = false)
            => sourceWriter.WriteXmlDocElement(tag, null, description, forceMultiline);

        private static IndentedTextWriter WriteXmlDocElement(
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

                return sourceWriter;
            }
        }
    }
}
