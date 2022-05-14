// Copyright © William Sugarman.
// Licensed under the MIT License.

// This file defines the Range class used to represent a sequence subset (E.g. a[2..4])
//
// Documentation: https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/ranges-indexes
// Source: https://github.com/dotnet/runtime/blob/v6.0.0/src/libraries/System.Private.CoreLib/src/System/Range.cs

#if NETSTANDARD2_0

using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System;

[EditorBrowsable(EditorBrowsableState.Never)]
[SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types"      , Justification = "BCL does not overwrite operator.")]
[SuppressMessage("Usage"      , "CA2231:Overload operator equals on overriding value type Equals", Justification = "BCL does not overwrite operator.")]
internal readonly struct Range : IEquatable<Range>
{
    public Index Start { get; }

    public Index End { get; }

    public Range(Index start, Index end)
    {
        Start = start;
        End   = end;
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is Range r && r.Start.Equals(Start) && r.End.Equals(End);

    public bool Equals(Range other)
        => other.Start.Equals(Start) && other.End.Equals(End);

    public override int GetHashCode()
        => Combine(Start.GetHashCode(), End.GetHashCode());

    public override string ToString()
        => Start.ToString() + ".." + End.ToString();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public (int Offset, int Length) GetOffsetAndLength(int length)
    {
        int start;
        Index startIndex = Start;
        start = startIndex.IsFromEnd ? length - startIndex.Value : startIndex.Value;

        int end;
        Index endIndex = End;
        end = endIndex.IsFromEnd ? length - endIndex.Value : endIndex.Value;

        if ((uint)end > (uint)length || (uint)start > (uint)end)
            throw new ArgumentOutOfRangeException(nameof(length));

        return (start, end - start);
    }

    public static Range StartAt(Index start)
        => new Range(start, Index.End);

    public static Range EndAt(Index end)
        => new Range(Index.Start, end);

    public static Range All
        => new Range(Index.Start, Index.End);

    // TODO: Move to a common HashHelpers class if necessary
    private static int Combine(int h1, int h2)
    {
        uint rol5 = ((uint)h1 << 5) | ((uint)h1 >> 27);
        return ((int)rol5 + h1) ^ h2;
    }
}

#endif
