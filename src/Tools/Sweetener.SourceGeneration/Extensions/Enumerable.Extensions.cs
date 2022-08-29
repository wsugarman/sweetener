// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace Sweetener.SourceGeneration.Extensions;

internal static class EnumerableExtensions
{
    public static IEnumerable<T> Concat<T>(this IEnumerable<T> source, T next)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        foreach (T item in source)
            yield return item;

        yield return next;
    }

    public static string Enclose(this IEnumerable<string> source, BracketType bracketType, string separator = ", ")
    {
        string value = string.Join(separator, source);
        if (string.IsNullOrEmpty(value))
            return value;

        return bracketType switch
        {
            BracketType.None => value,
            BracketType.CurlyBraces => "{" + value + "}",
            BracketType.AngleBrackets => "<" + value + ">",
            BracketType.SquareBrackets => "[" + value + "]",
            BracketType.Parentheses => "(" + value + ")",
            _ => throw new ArgumentOutOfRangeException(nameof(bracketType)),
        };
    }

    public static string ToCsv(this IEnumerable<string> source)
        => source.Enclose(BracketType.None, separator: ", ");
}
