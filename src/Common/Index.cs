// Copyright © William Sugarman.
// Licensed under the MIT License.

// This file defines the Index class used to implement indicies (E.g. a[^1]).
//
// Documentation: https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/ranges-indexes
// Source: https://github.com/dotnet/runtime/blob/v6.0.0/src/libraries/System.Private.CoreLib/src/System/Index.cs

#if NETSTANDARD2_0

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System;

[ExcludeFromCodeCoverage]
[EditorBrowsable(EditorBrowsableState.Never)]
[SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types"      , Justification = "BCL does not overwrite operator.")]
[SuppressMessage("Usage"      , "CA2231:Overload operator equals on overriding value type Equals", Justification = "BCL does not overwrite operator.")]
internal readonly struct Index : IEquatable<Index>
{
    public int Value => _value < 0 ? ~_value : _value;

    public bool IsFromEnd => _value < 0;

    public static Index Start => new Index(0);

    public static Index End => new Index(~0);

    private readonly int _value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Index(int value, bool fromEnd = false)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        _value = fromEnd ? ~value : value;
    }

    private Index(int value)
        => _value = value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetOffset(int length)
    {
        int offset = _value;
        if (IsFromEnd)
            offset += length + 1;

        return offset;
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is Index other && _value == other._value;

    public bool Equals(Index other)
        => _value == other._value;

    public override int GetHashCode()
        => _value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Index FromStart(int value)
        => value < 0 ? throw new ArgumentOutOfRangeException(nameof(value)) : new Index(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Index FromEnd(int value)
        => value < 0 ? throw new ArgumentOutOfRangeException(nameof(value)) : new Index(~value);

    [SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "BCL does not define alternative.")]
    public static implicit operator Index(int value)
        => FromStart(value);

    public override string ToString()
        => IsFromEnd ? ToStringFromEnd() : ((uint)Value).ToString(CultureInfo.InvariantCulture);

    private string ToStringFromEnd()
       => '^' + Value.ToString(CultureInfo.InvariantCulture);
}

#endif
