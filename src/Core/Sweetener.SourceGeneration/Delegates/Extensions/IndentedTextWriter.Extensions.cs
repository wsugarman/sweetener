// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Sweetener.SourceGeneration.Extensions;

namespace Sweetener.SourceGeneration.Delegates.Extensions
{
    internal static class IndentedTextWriterExtensions
    {
        public static IndentedTextWriter WriteXmlParams(this IndentedTextWriter sourceWriter, int count, string nameFormat = "arg{0}")
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
                sourceWriter.WriteXmlParam(
                    string.Format(CultureInfo.InvariantCulture, nameFormat, suffix),
                    $"The {qualifier}parameter of the method that this delegate encapsulates.");

            return sourceWriter;
        }


        public static IndentedTextWriter WriteXmlTypeParams(this IndentedTextWriter sourceWriter, int count, string nameFormat = "T{0}")
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
                sourceWriter.WriteXmlTypeParam(
                    string.Format(CultureInfo.InvariantCulture, nameFormat, suffix),
                    $"The type of the {qualifier}parameter of the method that this delegate encapsulates.");

            return sourceWriter;
        }
    }
}
