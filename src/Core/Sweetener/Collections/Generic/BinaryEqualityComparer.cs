// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO.Hashing;

namespace Sweetener.Collections.Generic;

/// <summary>
/// Represents an <see cref="IEqualityComparer{T}"/> for contiguous regions of memory.
/// </summary>
/// <remarks>
/// <see cref="ReadOnlySpan{T}"/> is a ref struct and cannot be used as a type argument for
/// <see cref="IEqualityComparer{T}"/>. Instead, the equality comparer exposes <see langword="static"/> methods
/// for operating upon <see cref="ReadOnlySpan{T}"/> structures.
/// </remarks>
public class BinaryEqualityComparer : IEqualityComparer<byte[]>
{
    /// <summary>
    /// Gets the singleton <see cref="BinaryEqualityComparer"/> instance for comparing byte arrays.
    /// </summary>
    /// <value>The singleton instance.</value>
    public static BinaryEqualityComparer Instance { get; } = new BinaryEqualityComparer();

    private BinaryEqualityComparer()
    { }

    /// <summary>
    /// Determines whether the specified arrays are equal.
    /// </summary>
    /// <param name="x">The first array to compare.</param>
    /// <param name="y">The second array to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the specified arrays are equal; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(byte[] x, byte[] y)
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
    public int GetHashCode(byte[] obj)
        => obj is null ? 0 : GetHashCode(new ReadOnlySpan<byte>(obj));

    /// <summary>
    /// Determines whether the specified regions of memory are equal.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Be careful of implicit type conversions.
    /// </para>
    /// <para>For example, <see cref="byte"/>[] can be implicitly cast to
    /// <see cref="ReadOnlySpan{T}"/>. The <see cref="Equals(byte[], byte[])"/> method will return
    /// <see langword="false"/> given both a <see langword="null"/> and empty argument, but when cast to
    /// <see cref="ReadOnlySpan{T}"/> the <see cref="Equals(ReadOnlySpan{byte}, ReadOnlySpan{byte})"/> method
    /// these same arguments will return <see langword="true"/>.
    /// </para>
    /// </remarks>
    /// <param name="x">The first region to compare.</param>
    /// <param name="y">The second region to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the specified regions are equal; otherwise, <see langword="false"/>.
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
    /// for <see langword="null"/> and empty arguments, but when cast to <see cref="ReadOnlySpan{T}"/>
    /// the <see cref="GetHashCode(ReadOnlySpan{byte})"/> method will return the equivalent values.
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

    private static unsafe bool Equals(uint* x, uint* y, int count)
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

    private static unsafe bool Equals(ulong* x, ulong* y, int count)
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
