// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;

namespace Sweetener.Generators
{
    internal static class Numbers
    {
        public static string GetNthWord(int i)
            => i switch
            {
                1 => "first",
                2 => "second",
                3 => "third",
                4 => "fourth",
                5 => "fifth",
                6 => "sixth",
                7 => "seventh",
                8 => "eighth",
                9 => "ninth",
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
                0 => "zero",
                1 => "one",
                2 => "two",
                3 => "three",
                4 => "four",
                5 => "five",
                6 => "six",
                7 => "seven",
                8 => "eight",
                9 => "nine",
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
}
