// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Sweetener
{
    internal static partial class Number
    {
        private const int Int128Precision = 39;
        private const int UInt128Precision = 39;

        private static unsafe bool TryNumberToInt128(ref NumberBuffer number, ref long value)
        {
            number.CheckConsistency();

            int i = number.Scale;
            if (i > Int128Precision || i < number.DigitsCount)
                return false;

            byte* p = number.GetDigitsPointer();
            Debug.Assert(p != null);

            long n = 0;
            long upper = 0, lower = 0;
            while (--i >= 0)
            {
                if ((ulong)n > (0x7FFFFFFFFFFFFFFF / 10))
                    return false;

                n *= 10;
                if (*p != '\0')
                    n += *p++ - '0';
            }

            if (number.IsNegative)
            {
                n = -n;
                if (n > 0)
                {
                    return false;
                }
            }
            else
            {
                if (n < 0)
                {
                    return false;
                }
            }

            value = n;
            return true;
        }

        private static unsafe bool TryNumberToInt128(ref NumberBuffer number, ref long value, ref int index)
        {
            number.CheckConsistency();

            int i = number.Scale;
            if (i > Int128Precision || i < number.DigitsCount)
                return false;

            byte* p = number.GetDigitsPointer();
            Debug.Assert(p != null);

            long n = 0;
            long upper = 0, lower = 0;
            while (--i >= 0)
            {
                if ((ulong)n > (0x7FFFFFFFFFFFFFFF / 10))
                    return false;

                n *= 10;
                if (*p != '\0')
                    n += *p++ - '0';
            }

            if (number.IsNegative)
            {
                n = -n;
                if (n > 0)
                {
                    return false;
                }
            }
            else
            {
                if (n < 0)
                {
                    return false;
                }
            }

            value = n;
            return true;
        }

        internal static long ParseInt128(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info)
        {
            ParsingStatus status = TryParseInt64(value, styles, info, out long result);
            if (status != ParsingStatus.OK)
            {
                ThrowOverflowOrFormatException(status, TypeCode.Int64);
            }

            return result;
        }

        /// <summary>Parses long inputs limited to styles that make up NumberStyles.Integer.</summary>
        internal static ParsingStatus TryParseInt64IntegerStyle(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info, out long result)
        {
            Debug.Assert((styles & ~NumberStyles.Integer) == 0, "Only handles subsets of Integer format");

            if (value.IsEmpty)
                goto FalseExit;

            int index = 0;
            int num = value[0];

            // Skip past any whitespace at the beginning.
            if ((styles & NumberStyles.AllowLeadingWhite) != 0 && IsWhite(num))
            {
                do
                {
                    index++;
                    if ((uint)index >= (uint)value.Length)
                        goto FalseExit;
                    num = value[index];
                }
                while (IsWhite(num));
            }

            // Parse leading sign.
            int sign = 1;
            if ((styles & NumberStyles.AllowLeadingSign) != 0)
            {
                if (info.HasInvariantNumberSigns)
                {
                    if (num == '-')
                    {
                        sign = -1;
                        index++;
                        if ((uint)index >= (uint)value.Length)
                            goto FalseExit;
                        num = value[index];
                    }
                    else if (num == '+')
                    {
                        index++;
                        if ((uint)index >= (uint)value.Length)
                            goto FalseExit;
                        num = value[index];
                    }
                }
                else if (info.AllowHyphenDuringParsing && num == '-')
                {
                    sign = -1;
                    index++;
                    if ((uint)index >= (uint)value.Length)
                        goto FalseExit;
                    num = value[index];
                }
                else
                {
                    value = value.Slice(index);
                    index = 0;
                    string positiveSign = info.PositiveSign, negativeSign = info.NegativeSign;
                    if (!string.IsNullOrEmpty(positiveSign) && value.StartsWith(positiveSign))
                    {
                        index += positiveSign.Length;
                        if ((uint)index >= (uint)value.Length)
                            goto FalseExit;
                        num = value[index];
                    }
                    else if (!string.IsNullOrEmpty(negativeSign) && value.StartsWith(negativeSign))
                    {
                        sign = -1;
                        index += negativeSign.Length;
                        if ((uint)index >= (uint)value.Length)
                            goto FalseExit;
                        num = value[index];
                    }
                }
            }

            bool overflow = false;
            long answer = 0;

            if (IsDigit(num))
            {
                // Skip past leading zeros.
                if (num == '0')
                {
                    do
                    {
                        index++;
                        if ((uint)index >= (uint)value.Length)
                            goto DoneAtEnd;
                        num = value[index];
                    } while (num == '0');
                    if (!IsDigit(num))
                        goto HasTrailingChars;
                }

                // Parse most digits, up to the potential for overflow, which can't happen until after 18 digits.
                answer = num - '0'; // first digit
                index++;
                for (int i = 0; i < 17; i++) // next 17 digits can't overflow
                {
                    if ((uint)index >= (uint)value.Length)
                        goto DoneAtEnd;
                    num = value[index];
                    if (!IsDigit(num))
                        goto HasTrailingChars;
                    index++;
                    answer = 10 * answer + num - '0';
                }

                if ((uint)index >= (uint)value.Length)
                    goto DoneAtEnd;
                num = value[index];
                if (!IsDigit(num))
                    goto HasTrailingChars;
                index++;
                // Potential overflow now processing the 19th digit.
                overflow = answer > long.MaxValue / 10;
                answer = answer * 10 + num - '0';
                overflow |= (ulong)answer > (ulong)long.MaxValue + (((uint)sign) >> 31);
                if ((uint)index >= (uint)value.Length)
                    goto DoneAtEndButPotentialOverflow;

                // At this point, we're either overflowing or hitting a formatting error.
                // Format errors take precedence for compatibility.
                num = value[index];
                while (IsDigit(num))
                {
                    overflow = true;
                    index++;
                    if ((uint)index >= (uint)value.Length)
                        goto OverflowExit;
                    num = value[index];
                }
                goto HasTrailingChars;
            }
            goto FalseExit;

            DoneAtEndButPotentialOverflow:
            if (overflow)
            {
                goto OverflowExit;
            }
            DoneAtEnd:
            result = answer * sign;
            ParsingStatus status = ParsingStatus.OK;
            Exit:
            return status;

            FalseExit: // parsing failed
            result = 0;
            status = ParsingStatus.Failed;
            goto Exit;
            OverflowExit:
            result = 0;
            status = ParsingStatus.Overflow;
            goto Exit;

            HasTrailingChars: // we've successfully parsed, but there are still remaining characters in the span
            // Skip past trailing whitespace, then past trailing zeros, and if anything else remains, fail.
            if (IsWhite(num))
            {
                if ((styles & NumberStyles.AllowTrailingWhite) == 0)
                    goto FalseExit;
                for (index++; index < value.Length; index++)
                {
                    if (!IsWhite(value[index]))
                        break;
                }
                if ((uint)index >= (uint)value.Length)
                    goto DoneAtEndButPotentialOverflow;
            }

            if (!TrailingZeros(value, index))
                goto FalseExit;

            goto DoneAtEndButPotentialOverflow;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ParsingStatus TryParseInt64(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info, out long result)
        {
            if ((styles & ~NumberStyles.Integer) == 0)
            {
                // Optimized path for the common case of anything that's allowed for integer style.
                return TryParseInt64IntegerStyle(value, styles, info, out result);
            }

            if ((styles & NumberStyles.AllowHexSpecifier) != 0)
            {
                result = 0;
                return TryParseUInt64HexNumberStyle(value, styles, out Unsafe.As<long, ulong>(ref result));
            }

            return TryParseInt64Number(value, styles, info, out result);
        }

        private static unsafe ParsingStatus TryParseInt64Number(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info, out long result)
        {
            result = 0;
            byte* pDigits = stackalloc byte[Int64NumberBufferLength];
            NumberBuffer number = new NumberBuffer(NumberBufferKind.Integer, pDigits, Int64NumberBufferLength);

            if (!TryStringToNumber(value, styles, ref number, info))
            {
                return ParsingStatus.Failed;
            }

            if (!TryNumberToInt64(ref number, ref result))
            {
                return ParsingStatus.Overflow;
            }

            return ParsingStatus.OK;
        }

        // Ternary op is a workaround for https://github.com/dotnet/runtime/issues/4207
        private static bool IsWhite(int ch) => ch == 0x20 || (uint)(ch - 0x09) <= (0x0D - 0x09) ? true : false;

        private static bool IsDigit(int ch) => ((uint)ch - '0') <= 9;

        internal enum ParsingStatus
        {
            OK,
            Failed,
            Overflow
        }
    }
}
