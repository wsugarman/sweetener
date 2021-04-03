// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Diagnostics.CodeAnalysis;

using System.Globalization;
using System.Runtime.InteropServices;

namespace Sweetener
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    internal readonly struct Int128 : IComparable, IConvertible, IFormattable, IComparable<Int128>, IEquatable<Int128>
    {
        private readonly long _upper;
        private readonly long _lower;

        public static readonly Int128 MaxValue = new Int128(long.MaxValue, -1L); // 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF
        public static readonly Int128 MinValue = new Int128(long.MinValue,  0L); // 0x80000000000000000000000000000000

        public Int128(long value)
            : this(0, value)
        { }

        internal Int128(long upper, long lower)
        {
            _upper = upper;
            _lower = lower;
        }

        public int CompareTo(Int128 obj)
        {
            int cmp = _upper.CompareTo(obj._upper);
            return cmp == 0 ? _lower.CompareTo(obj._lower) : cmp;
        }

        public int CompareTo(object? obj)
            => obj is Int128 other ? CompareTo(other) : throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.ArgumentMustBeTypeFormatString, nameof(Int128)));

        public bool Equals(Int128 other)
            => _lower.Equals(other._lower) && _upper.Equals(other._upper);

        public override bool Equals([NotNullWhen(true)] object? obj)
            => obj is Int128 other && Equals(other);

        public override int GetHashCode()
        {
            int hashCode = (int)_lower;
            hashCode ^= (int)(_lower >> 32);
            hashCode ^= (int)_upper;
            hashCode ^= (int)(_upper >> 32);

            return hashCode;
        }

        public override string ToString()
        {
            return Number.Int64ToDecStr(m_value);
        }

        public string ToString(IFormatProvider? provider)
        {
            return Number.FormatInt64(m_value, null, provider);
        }

        public string ToString(string? format)
        {
            return Number.FormatInt64(m_value, format, null);
        }

        public string ToString(string? format, IFormatProvider? provider)
        {
            return Number.FormatInt64(m_value, format, provider);
        }

        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
        {
            return Number.TryFormatInt64(m_value, format, provider, destination, out charsWritten);
        }

        public static long Parse(string s)
        {
            if (s == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
            return Number.ParseInt64(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
        }

        public static long Parse(string s, NumberStyles style)
        {
            NumberFormatInfo.ValidateParseStyleInteger(style);
            if (s == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
            return Number.ParseInt64(s, style, NumberFormatInfo.CurrentInfo);
        }

        public static long Parse(string s, IFormatProvider? provider)
        {
            if (s == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
            return Number.ParseInt64(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
        }

        // Parses a long from a String in the given style.  If
        // a NumberFormatInfo isn't specified, the current culture's
        // NumberFormatInfo is assumed.
        //
        public static long Parse(string s, NumberStyles style, IFormatProvider? provider)
        {
            NumberFormatInfo.ValidateParseStyleInteger(style);
            if (s == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
            return Number.ParseInt64(s, style, NumberFormatInfo.GetInstance(provider));
        }

        public static long Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null)
        {
            NumberFormatInfo.ValidateParseStyleInteger(style);
            return Number.ParseInt64(s, style, NumberFormatInfo.GetInstance(provider));
        }

        public static bool TryParse([NotNullWhen(true)] string? s, out long result)
        {
            if (s == null)
            {
                result = 0;
                return false;
            }

            return Number.TryParseInt64IntegerStyle(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result) == Number.ParsingStatus.OK;
        }

        public static bool TryParse(ReadOnlySpan<char> s, out long result)
        {
            return Number.TryParseInt64IntegerStyle(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result) == Number.ParsingStatus.OK;
        }

        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, out long result)
        {
            NumberFormatInfo.ValidateParseStyleInteger(style);

            if (s == null)
            {
                result = 0;
                return false;
            }

            return Number.TryParseInt64(s, style, NumberFormatInfo.GetInstance(provider), out result) == Number.ParsingStatus.OK;
        }

        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out long result)
        {
            NumberFormatInfo.ValidateParseStyleInteger(style);
            return Number.TryParseInt64(s, style, NumberFormatInfo.GetInstance(provider), out result) == Number.ParsingStatus.OK;
        }

        #region IConvertible

        public TypeCode GetTypeCode()
            => TypeCode.Object;

        bool IConvertible.ToBoolean(IFormatProvider? provider)
            => _upper != 0 && _lower != 0;

        char IConvertible.ToChar(IFormatProvider? provider)
            => _upper != 0 || _lower > char.MaxValue ? throw new OverflowException(SR.OverflowCharMessage) : (char)_lower;

        sbyte IConvertible.ToSByte(IFormatProvider? provider)
            => _upper < -1 || (_upper == -1 && _lower < sbyte.MinValue) || _upper > 0 || (_upper == 0 && _lower > sbyte.MaxValue) ? throw new OverflowException(SR.OverflowSByteMessage) : (sbyte)_lower;

        byte IConvertible.ToByte(IFormatProvider? provider)
            => _upper != 0 || _lower > byte.MaxValue ? throw new OverflowException(SR.OverflowByteMessage) : (byte)_lower;

        short IConvertible.ToInt16(IFormatProvider? provider)
            => _upper < -1L || (_upper == -1 && _lower < short.MinValue) || _upper > 0 || (_upper == 0 && _lower > short.MaxValue) ? throw new OverflowException(SR.OverflowInt16Message) : (short)_lower;

        ushort IConvertible.ToUInt16(IFormatProvider? provider)
            => _upper != 0 || _lower > ushort.MaxValue ? throw new OverflowException(SR.OverflowUInt16Message) : (ushort)_lower;

        int IConvertible.ToInt32(IFormatProvider? provider)
            => _upper < -1L || (_upper == -1 && _lower < int.MinValue) || _upper > 0 || (_upper == 0 && _lower > int.MaxValue) ? throw new OverflowException(SR.OverflowInt32Message) : (int)_lower;

        uint IConvertible.ToUInt32(IFormatProvider? provider)
            => _upper != 0 || _lower > uint.MaxValue ? throw new OverflowException(SR.OverflowUInt32Message) : (uint)_lower;

        long IConvertible.ToInt64(IFormatProvider? provider)
            => _upper < -1L || _upper > 0 ? throw new OverflowException(SR.OverflowInt64Message) : _lower;

        ulong IConvertible.ToUInt64(IFormatProvider? provider)
            => _upper != 0 ? throw new OverflowException(SR.OverflowUInt64Message) : (ulong)_lower;

        float IConvertible.ToSingle(IFormatProvider? provider)
        {
            if (_upper == 0 || _upper == -1)
                return _lower;

            // TODO: Should we do more here?
            return _lower + ((float)_upper * ulong.MaxValue * 2);
        }

        double IConvertible.ToDouble(IFormatProvider? provider)
        {
            if (_upper == 0 || _upper == -1)
                return _lower;

            // TODO: Should we do more here?
            return 0; // We'll have to convert manually
        }

        decimal IConvertible.ToDecimal(IFormatProvider? provider)
        {
            if (_upper == 0 || _upper == -1)
                return _lower;

            // TODO: Should we do more here?
            return 0; // We'll have to convert manually
        }

        DateTime IConvertible.ToDateTime(IFormatProvider? provider)
            => throw new InvalidCastException(string.Format(CultureInfo.CurrentCulture, SR.InvalidCastFormatString, nameof(Int128), nameof(DateTime)));

        object IConvertible.ToType(Type type, IFormatProvider? provider)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (ReferenceEquals(type, typeof(Int128)))
                return this;

            IConvertible value = this;
            if (ReferenceEquals(type, typeof(bool)))
                return value.ToBoolean(provider);
            if (ReferenceEquals(type, typeof(char)))
                return value.ToChar(provider);
            if (ReferenceEquals(type, typeof(sbyte)))
                return value.ToSByte(provider);
            if (ReferenceEquals(type, typeof(byte)))
                return value.ToByte(provider);
            if (ReferenceEquals(type, typeof(short)))
                return value.ToInt16(provider);
            if (ReferenceEquals(type, typeof(ushort)))
                return value.ToUInt16(provider);
            if (ReferenceEquals(type, typeof(int)))
                return value.ToInt32(provider);
            if (ReferenceEquals(type, typeof(uint)))
                return value.ToUInt32(provider);
            if (ReferenceEquals(type, typeof(long)))
                return value.ToInt64(provider);
            if (ReferenceEquals(type, typeof(ulong)))
                return value.ToUInt64(provider);
            if (ReferenceEquals(type, typeof(float)))
                return value.ToSingle(provider);
            if (ReferenceEquals(type, typeof(double)))
                return value.ToDouble(provider);
            if (ReferenceEquals(type, typeof(decimal)))
                return value.ToDecimal(provider);
            if (ReferenceEquals(type, typeof(DateTime)))
                return value.ToDateTime(provider);
            if (ReferenceEquals(type, typeof(string)))
                return ToString(provider);
            if (ReferenceEquals(type, typeof(object)))
                return value;
            // Need to special case Enum because typecode will be underlying type, e.g. Int32
            if (ReferenceEquals(type, typeof(Enum)))
                return (Enum)value;
            if (ReferenceEquals(type, typeof(DBNull)))
                throw new InvalidCastException(SR.InvalidCast_DBNull);

            // Empty Type is not public, so we'll lump it into this statement
            throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, value.GetType().FullName, targetType.FullName));
        }

        #endregion

        #region Operators

        public static bool operator ==(Int128 left, Int128 right)
            => left.Equals(right);

        public static bool operator !=(Int128 left, Int128 right)
            => !left.Equals(right);

        public static bool operator <(Int128 left, Int128 right)
            => left.CompareTo(right) < 0;

        public static bool operator <=(Int128 left, Int128 right)
            => left.CompareTo(right) <= 0;

        public static bool operator >(Int128 left, Int128 right)
            => left.CompareTo(right) > 0;

        public static bool operator >=(Int128 left, Int128 right)
            => left.CompareTo(right) >= 0;

        #endregion
    }
}
