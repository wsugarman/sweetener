// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO.Hashing;

namespace Sweetener.Collections.Generic;

/// <summary>
/// Represents both an <see cref="IComparer{T}"/> and <see cref="IEqualityComparer{T}"/> for contiguous regions of memory.
/// </summary>
/// <remarks>
/// <see cref="ReadOnlySpan{T}"/> is a ref struct and cannot be used as a type argument for
/// <see cref="IComparer{T}"/> and <see cref="IEqualityComparer{T}"/>. Instead, the comparer exposes
/// <see langword="static"/> methods for operating upon <see cref="ReadOnlySpan{T}"/> structures.
/// </remarks>
public class BinaryComparer : IComparer<byte[]?>, IEqualityComparer<byte[]?>
{
    /// <summary>
    /// Gets the singleton <see cref="BinaryComparer"/> instance for comparing byte arrays.
    /// </summary>
    /// <value>The singleton instance.</value>
    public static BinaryComparer Instance { get; } = new BinaryComparer();

    private BinaryComparer()
    { }

    /// <summary>
    /// Compares two arrays and returns a value indicating whether one is less than, equal to, or greater than the other.
    /// </summary>
    /// <remarks>
    /// Array values are first compared by their byte values, starting with first index, followed by their respective
    /// lengths such that shorter arrays are considered smaller values.
    /// </remarks>
    /// <param name="x">The first array to compare.</param>
    /// <param name="y">The second array to compare.</param>
    /// <returns>
    /// A signed integer that indicates the relative values of <paramref name="x"/> and <paramref name="y"/>,
    /// as shown in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term>Return Value</term>
    /// <description>Description</description>
    /// </listheader>
    /// <item>
    /// <term>Less than zero</term>
    /// <description>
    /// <paramref name="x"/> is less than <paramref name="y"/>, or <paramref name="x"/> is <see langword="null"/>
    /// and <paramref name="y"/> is not <see langword="null"/>.
    /// </description>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <description>
    /// <paramref name="x"/> is equal to <paramref name="y"/>, or <paramref name="x"/> and <paramref name="y"/>
    /// are both <see langword="null"/>.
    /// </description>
    /// </item>
    /// <item>
    /// <term>Greater than zero</term>
    /// <description>
    /// <paramref name="x"/> is greater than <paramref name="y"/>, or <paramref name="y"/> is <see langword="null"/>
    /// and <paramref name="x"/> is not <see langword="null"/>.
    /// </description>
    /// </item>
    /// </list>
    /// </returns>
    public int Compare(byte[]? x, byte[]? y)
    {
        if (ReferenceEquals(x, y))
            return 0;
        else if (x is null)
            return -1;
        else if (y is null)
            return 1;

        return Compare(new ReadOnlySpan<byte>(x), new ReadOnlySpan<byte>(y));
    }

    /// <summary>
    /// Determines whether the specified arrays are equal.
    /// </summary>
    /// <param name="x">The first array to compare.</param>
    /// <param name="y">The second array to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the specified arrays are either both <see langword="null"/> or of equal length
    /// and contain the same byte values in the same order; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(byte[]? x, byte[]? y)
    {
        if (ReferenceEquals(x, y))
            return true;

        // We can safely short-circuit as the above predicate checks if they're both null
        if (x is null || y is null)
            return false;

        return Equals(new ReadOnlySpan<byte>(x), new ReadOnlySpan<byte>(y));
    }

    /// <summary>
    /// Returns a hash code for the specified array.
    /// </summary>
    /// <param name="obj">The array for which a hash code is to be returned.</param>
    /// <returns>A hash code for the specified array.</returns>
    public int GetHashCode(byte[]? obj)
        => obj is null ? 0 : GetHashCode(new ReadOnlySpan<byte>(obj));

    /// <summary>
    /// Compares two regions of memory and returns a value indicating whether one is less than, equal to,
    /// or greater than the other.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Regions are first compared by their byte values, starting with first index, followed by their respective
    /// lengths such that shorter arrays are considered smaller values.
    /// </para>
    /// <para>
    /// Be careful of implicit type conversions. For example, <see cref="byte"/>[] can be implicitly cast to
    /// <see cref="ReadOnlySpan{T}"/>. The <see cref="Compare(byte[], byte[])"/> method will return a
    /// non-zero result given both a <see langword="null"/> value and an empty array, but when cast to
    /// <see cref="ReadOnlySpan{T}"/> the <see cref="Compare(ReadOnlySpan{byte}, ReadOnlySpan{byte})"/> method
    /// will return <c>0</c> instead.
    /// </para>
    /// </remarks>
    /// <param name="x">The first region to compare.</param>
    /// <param name="y">The second region to compare.</param>
    /// <returns>
    /// A signed integer that indicates the relative values of <paramref name="x"/> and <paramref name="y"/>,
    /// as shown in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term>Return Value</term>
    /// <description>Description</description>
    /// </listheader>
    /// <item>
    /// <term>Less than zero</term>
    /// <description>
    /// <paramref name="x"/> is less than <paramref name="y"/>.
    /// </description>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <description>
    /// <paramref name="x"/> is equal to <paramref name="y"/>.
    /// </description>
    /// </item>
    /// <item>
    /// <term>Greater than zero</term>
    /// <description>
    /// <paramref name="x"/> is greater than <paramref name="y"/>.
    /// </description>
    /// </item>
    /// </list>
    /// </returns>
    public static int Compare(ReadOnlySpan<byte> x, ReadOnlySpan<byte> y)
    {
        unsafe
        {
            fixed (byte* xPtr = x)
            fixed (byte* yPtr = y)
            {
                return IntPtr.Size == sizeof(uint)
                    ? Compare((uint*)xPtr, (uint*)yPtr, x.Length, y.Length)
                    : Compare((ulong*)xPtr, (ulong*)yPtr, x.Length, y.Length);
            }
        }
    }

    /// <summary>
    /// Determines whether the specified regions of memory are equal.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Be careful of implicit type conversions.
    /// </para>
    /// <para>For example, <see cref="byte"/>[] can be implicitly cast to
    /// <see cref="ReadOnlySpan{T}"/>. The <see cref="Equals(byte[], byte[])"/> method will return
    /// <see langword="false"/> given both a <see langword="null"/> value and an empty array, but when cast to
    /// <see cref="ReadOnlySpan{T}"/> the <see cref="Equals(ReadOnlySpan{byte}, ReadOnlySpan{byte})"/> method
    /// will return <see langword="true"/> instead.
    /// </para>
    /// </remarks>
    /// <param name="x">The first region to compare.</param>
    /// <param name="y">The second region to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the specified regions are of equal length
    /// and contain the same byte values in the same order; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool Equals(ReadOnlySpan<byte> x, ReadOnlySpan<byte> y)
    {
        if (x.Length != y.Length)
            return false;

        unsafe
        {
            fixed (byte* xPtr = x)
            fixed (byte* yPtr = y)
            {
                return IntPtr.Size == sizeof(uint)
                    ? Equals((uint*)xPtr, (uint*)yPtr, x.Length)
                    : Equals((ulong*)xPtr, (ulong*)yPtr, x.Length);
            }
        }
    }

    /// <summary>
    /// Returns a hash code for the specified region of memory.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Be careful of implicit type conversions.
    /// </para>
    /// <para>For example, <see cref="byte"/>[] can be implicitly cast to
    /// <see cref="ReadOnlySpan{T}"/>. The <see cref="GetHashCode(byte[])"/> method will return different values
    /// for <see langword="null"/> values and empty arrays, but when cast to <see cref="ReadOnlySpan{T}"/>
    /// the <see cref="GetHashCode(ReadOnlySpan{byte})"/> method will return equivalent values.
    /// </para>
    /// </remarks>
    /// <param name="obj">The region for which a hash code is to be returned.</param>
    /// <returns>A hash code for the specified region.</returns>
    public static int GetHashCode(ReadOnlySpan<byte> obj)
    {
        unsafe
        {
            int hash;
            XxHash32.Hash(obj, new Span<byte>(&hash, 4));
            return hash;
        }
    }

    internal static unsafe int Compare(uint* x, uint* y, int xCount, int yCount)
    {
        int cmp = 0;
        while (xCount >= sizeof(uint) && yCount >= sizeof(uint))
        {
            // Note: Parentheses left for clarity
            cmp = (x++)->CompareTo(*(y++));
            if (cmp != 0)
                return cmp;

            xCount -= sizeof(uint);
            yCount -= sizeof(uint);
        }

        return Compare((byte*)x, (byte*)y, xCount, yCount);
    }

    internal static unsafe int Compare(ulong* x, ulong* y, int xCount, int yCount)
    {
        int cmp = 0;
        while (xCount >= sizeof(ulong) && yCount >= sizeof(ulong))
        {
            // Note: Parentheses left for clarity
            cmp = (x++)->CompareTo(*(y++));
            if (cmp != 0)
                return cmp;

            xCount -= sizeof(ulong);
            yCount -= sizeof(ulong);
        }

        return Compare((byte*)x, (byte*)y, xCount, yCount);
    }

    internal static unsafe bool Equals(uint* x, uint* y, int count)
    {
        while (count >= sizeof(uint))
        {
            // Note: Parentheses left for clarity
            if (*(x++) != *(y++))
                return false;

            count -= sizeof(uint);
        }

        return Equals((byte*)x, (byte*)y, count);
    }

    internal static unsafe bool Equals(ulong* x, ulong* y, int count)
    {
        while (count >= sizeof(ulong))
        {
            // Note: Parentheses left for clarity
            if (*(x++) != *(y++))
                return false;

            count -= sizeof(ulong);
        }

        return Equals((byte*)x, (byte*)y, count);
    }

    private static unsafe int Compare(byte* x, byte* y, int xCount, int yCount)
    {
        // TODO: Unroll loop for better performance as we know count will always be < 8
        int cmp = 0;
        int minCount = xCount < yCount ? xCount : yCount;
        for (int i = 0; i < minCount; i++)
        {
            cmp = (x + i)->CompareTo(*(y + i));
            if (cmp != 0)
                return cmp;
        }

        return xCount.CompareTo(yCount);
    }

    private static unsafe bool Equals(byte* x, byte* y, int count)
    {
        // TODO: Unroll loop for better performance as we know count will always be < 8
        for (int i = 0; i < count; i++)
        {
            if (*(x + i) != *(y + i))
                return false;
        }

        return true;
    }
}
