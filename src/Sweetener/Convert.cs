// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;

namespace Sweetener
{
    internal class Convert
    {
        public bool ToBoolean(Int128 value)
            => _upper != 0L && _lower != 0L;


        public char IConvertible.ToChar(IFormatProvider? provider)
        {
            if (_upper != 0 || _lower > char.MaxValue)
                throw new OverflowException(SR.OverflowCharMessage);

            return (char)_lower;
        }

        public sbyte IConvertible.ToSByte(IFormatProvider? provider)
        {
            if (value < sbyte.MinValue || value > sbyte.MaxValue)
                ThrowSByteOverflowException();
            return (sbyte)value;
        }

        public byte IConvertible.ToByte(IFormatProvider? provider)
        {
            if (value > byte.MaxValue)
                ThrowByteOverflowException();
            return (byte)value;
        }

        public short IConvertible.ToInt16(IFormatProvider? provider)
        {
            if (value < short.MinValue || value > short.MaxValue)
                ThrowInt16OverflowException();
            return (short)value;
        }

        public ushort IConvertible.ToUInt16(IFormatProvider? provider)
        {
            if (value > ushort.MaxValue)
                ThrowUInt16OverflowException();
            return (ushort)value;
        }

        public int IConvertible.ToInt32(IFormatProvider? provider)
        {
            if (value < int.MinValue || value > int.MaxValue)
                ThrowInt32OverflowException();
            return (int)value;
        }

        public uint IConvertible.ToUInt32(IFormatProvider? provider)
        {
            if (value > uint.MaxValue)
                ThrowUInt32OverflowException();
            return (uint)value;
        }

        public long IConvertible.ToInt64(IFormatProvider? provider)
        {
            return m_value;
        }

        public ulong IConvertible.ToUInt64(IFormatProvider? provider)
        {
            if (value < 0)
                ThrowUInt64OverflowException();
            return (ulong)value;
        }

        public float IConvertible.ToSingle(IFormatProvider? provider)
        {
            return value;
        }

        public double IConvertible.ToDouble(IFormatProvider? provider)
        {
            return value;
        }

        public decimal IConvertible.ToDecimal(IFormatProvider? provider)
        {
            return value;
        }
    }
}
