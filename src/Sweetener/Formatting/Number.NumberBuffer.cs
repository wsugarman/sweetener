// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Sweetener
{
    static partial class Number
    {
        // We need 1 additional byte, per length, for the terminating null
        internal const int Int128NumberBufferLength  = 39 + 1; // 39 for the longest input: 170,141,183,460,469,231,731,687,303,715,884,105,727
        internal const int UInt128NumberBufferLength = 39 + 1; // 39 for the longest input: 340,282,366,920,938,463,463,374,607,431,768,211,455

        internal unsafe ref struct NumberBuffer
        {
            public int DigitsCount;
            public int Scale;
            public bool IsNegative;
            public bool HasNonZeroTail;
            public NumberBufferKind Kind;
            public Span<byte> Digits;

            public NumberBuffer(NumberBufferKind kind, byte* digits, int digitsLength)
            {
                Debug.Assert(digits != null);
                Debug.Assert(digitsLength > 0);

                DigitsCount = 0;
                Scale = 0;
                IsNegative = false;
                HasNonZeroTail = false;
                Kind = kind;
                Digits = new Span<byte>(digits, digitsLength);

#if DEBUG
                Digits.Fill(0xCC);
#endif

                Digits[0] = (byte)('\0');
                CheckConsistency();
            }

            [Conditional("DEBUG")]
            public void CheckConsistency()
            {
#if DEBUG
                Debug.Assert((Kind == NumberBufferKind.Integer) || (Kind == NumberBufferKind.Decimal) || (Kind == NumberBufferKind.FloatingPoint));
                Debug.Assert(Digits[0] != '0', "Leading zeros should never be stored in a Number");

                int numDigits;
                for (numDigits = 0; numDigits < Digits.Length; numDigits++)
                {
                    byte digit = Digits[numDigits];

                    if (digit == 0)
                    {
                        break;
                    }

                    Debug.Assert((digit >= '0') && (digit <= '9'), "Unexpected character found in Number");
                }

                Debug.Assert(numDigits == DigitsCount, "Null terminator found in unexpected location in Number");
                Debug.Assert(numDigits < Digits.Length, "Null terminator not found in Number");
#endif // DEBUG
            }

            public byte* GetDigitsPointer()
            {
                // This is safe to do since we are a ref struct
                return (byte*)(Unsafe.AsPointer(ref Digits[0]));
            }

            //
            // Code coverage note: This only exists so that Number displays nicely in the VS watch window. So yes, I know it works.
            //
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();

                sb.Append('[');
                sb.Append('"');

                for (int i = 0; i < Digits.Length; i++)
                {
                    byte digit = Digits[i];

                    if (digit == 0)
                    {
                        break;
                    }

                    sb.Append((char)(digit));
                }

                sb.Append('"');
                sb.Append(", Length = ").Append(DigitsCount);
                sb.Append(", Scale = ").Append(Scale);
                sb.Append(", IsNegative = ").Append(IsNegative);
                sb.Append(", HasNonZeroTail = ").Append(HasNonZeroTail);
                sb.Append(", Kind = ").Append(Kind);
                sb.Append(']');

                return sb.ToString();
            }
        }

        internal enum NumberBufferKind : byte
        {
            Unknown = 0,
            Integer = 1,
            Decimal = 2,
            FloatingPoint = 3,
        }
    }
}
