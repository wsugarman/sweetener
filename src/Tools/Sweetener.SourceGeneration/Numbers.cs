// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;

namespace Sweetener.SourceGeneration;

internal static class Numbers
{
    public static string GetNthWord(int i)
        => i switch
        {
            01 => "first",
            02 => "second",
            03 => "third",
            04 => "fourth",
            05 => "fifth",
            06 => "sixth",
            07 => "seventh",
            08 => "eighth",
            09 => "ninth",
            10 => "tenth",
            11 => "eleventh",
            12 => "twelfth",
            13 => "thirteenth",
            14 => "fourteenth",
            15 => "fifteenth",
            16 => "sixteenth",
            17 => "seventeenth",
            18 => "eighteenth",
            19 => "nineteenth",
            20 => "twentieth",
            _ => throw new ArgumentOutOfRangeException(nameof(i), $"No word configured for {i}"),
        };

    public static string GetWord(int i)
        => i switch
        {
            00 => "zero",
            01 => "one",
            02 => "two",
            03 => "three",
            04 => "four",
            05 => "five",
            06 => "six",
            07 => "seven",
            08 => "eight",
            09 => "nine",
            10 => "ten",
            11 => "eleven",
            12 => "twelve",
            13 => "thirteen",
            14 => "fourteen",
            15 => "fifteen",
            16 => "sixteen",
            17 => "seventeen",
            18 => "eighteen",
            19 => "nineteen",
            20 => "twenty",
            _ => throw new ArgumentOutOfRangeException(nameof(i), $"No word configured for {i}"),
        };
}
