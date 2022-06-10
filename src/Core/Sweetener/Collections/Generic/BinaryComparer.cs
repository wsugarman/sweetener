// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Hashing;
using System.Runtime.InteropServices;

namespace Sweetener.Collections.Generic;

/// <summary>
/// Represents both an <see cref="IComparer{T}"/> and <see cref="IEqualityComparer{T}"/> for contiguous regions of memory.
/// </summary>
/// <remarks>
/// <see cref="ReadOnlySpan{T}"/> is a ref struct and cannot be used as a type argument for
/// <see cref="IComparer{T}"/> and <see cref="IEqualityComparer{T}"/>. Instead, the comparer exposes
/// <see langword="static"/> methods for operating upon <see cref="ReadOnlySpan{T}"/> structures.
/// </remarks>
public sealed class BinaryComparer : IComparer<byte[]>, IComparer<Stream>, IEqualityComparer<byte[]>, IEqualityComparer<Stream>
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
    public int Compare(ReadOnlySpan<byte> x, ReadOnlySpan<byte> y)
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
    /// Compares two streams and returns a value indicating whether one is less than, equal to, or greater than the other.
    /// </summary>
    /// <remarks>
    /// Streams are first compared by their byte values, starting from their current position, followed by their
    /// respective lengths such that shorter streams are considered smaller values.
    /// </remarks>
    /// <param name="x">The first stream to compare.</param>
    /// <param name="y">The second stream to compare.</param>
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
    public int Compare(Stream? x, Stream? y)
    {
        if (ReferenceEquals(x, y))
            return 0;
        else if (x is null)
            return -1;
        else if (y is null)
            return 1;

        return IntPtr.Size == sizeof(uint) ? Compare32(x, y) : Compare64(x, y);
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
    public bool Equals(ReadOnlySpan<byte> x, ReadOnlySpan<byte> y)
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
    /// Determines whether the specified streams are equal.
    /// </summary>
    /// <param name="x">The first stream to compare.</param>
    /// <param name="y">The second stream to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the specified streams are either both <see langword="null"/> or of equal length
    /// and contain the same byte values in the same order; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(Stream? x, Stream? y)
    {
        if (ReferenceEquals(x, y))
            return true;

        // We can safely short-circuit as the above predicate checks if they're both null
        if (x is null || y is null)
            return false;

        return IntPtr.Size == sizeof(uint) ? Equals32(x, y) : Equals64(x, y);
    }

    /// <summary>
    /// Returns a hash code for the specified array.
    /// </summary>
    /// <param name="obj">The array for which a hash code is to be returned.</param>
    /// <returns>A hash code for the specified array.</returns>
    public int GetHashCode(
#if NETSTANDARD2_0
        byte[]? obj)
#else
        byte[] obj)
#endif
        => obj is null ? 0 : GetHashCode(new ReadOnlySpan<byte>(obj));


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
    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "ReadOnlySpan<T> cannot be used as type argument.")]
    public int GetHashCode(ReadOnlySpan<byte> obj)
    {
        unsafe
        {
            int hash;
            XxHash32.Hash(obj, new Span<byte>(&hash, 4));
            return hash;
        }
    }

    /// <summary>
    /// Returns a hash code for the specified stream.
    /// </summary>
    /// <param name="obj">The stream for which a hash code is to be returned.</param>
    /// <returns>A hash code for the specified stream.</returns>
    public int GetHashCode(
#if NETSTANDARD2_0
        Stream? obj)
#else
        Stream obj)
#endif
    {
        if (obj is null)
            return 0;

        XxHash32 algo = new XxHash32();
        algo.Append(obj);

        unsafe
        {
            int hash;
            algo.GetCurrentHash(new Span<byte>(&hash, 4));
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

#if NETCOREAPP2_1_OR_GREATER
    internal static int Compare32(Stream x, Stream y)
    {
        unsafe
        {
            ulong xLong;
            ulong yLong;
            Span<byte> xBuffer = new Span<byte>(&xLong, 8);
            Span<byte> yBuffer = new Span<byte>(&yLong, 8);

            int cmp;
            int xCount;
            int yCount;

            do
            {
                xCount = FillBuffer(x, xBuffer);
                yCount = FillBuffer(y, yBuffer);

                if (xCount < sizeof(ulong) || yCount < sizeof(ulong))
                    break;

                cmp = xLong.CompareTo(yLong);
                if (cmp != 0)
                    return cmp;
            } while (true);

            return Compare((byte*)&xLong, (byte*)&yLong, xCount, yCount);
        }
    }

    internal static int Compare64(Stream x, Stream y)
    {
        unsafe
        {
            ulong xLong;
            ulong yLong;
            Span<byte> xBuffer = new Span<byte>(&xLong, 8);
            Span<byte> yBuffer = new Span<byte>(&yLong, 8);

            int cmp;
            int xCount;
            int yCount;

            do
            {
                xCount = FillBuffer(x, xBuffer);
                yCount = FillBuffer(y, yBuffer);

                if (xCount < sizeof(ulong) || yCount < sizeof(ulong))
                    break;

                cmp = xLong.CompareTo(yLong);
                if (cmp != 0)
                    return cmp;
            } while (true);

            return Compare((byte*)&xLong, (byte*)&yLong, xCount, yCount);
        }
    }
#else
    internal static int Compare32(Stream x, Stream y)
    {
        unsafe
        {
            byte[] xBuffer = ArrayPool<byte>.Shared.Rent(sizeof(uint));
            byte[] yBuffer = ArrayPool<byte>.Shared.Rent(sizeof(uint));

            try
            {
                fixed (byte* xPtr = xBuffer)
                fixed (byte* yPtr = yBuffer)
                {
                    int cmp;
                    int xCount;
                    int yCount;

                    do
                    {
                        xCount = FillBuffer(x, xBuffer, sizeof(uint));
                        yCount = FillBuffer(y, yBuffer, sizeof(uint));

                        if (xCount < sizeof(uint) || yCount < sizeof(uint))
                            break;

                        cmp = ((uint*)xPtr)->CompareTo(*(uint*)yPtr);
                        if (cmp != 0)
                            return cmp;
                    } while (true);

                    return Compare(xPtr, yPtr, xCount, yCount);
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(xBuffer);
                ArrayPool<byte>.Shared.Return(yBuffer);
            }
        }
    }

    internal static int Compare64(Stream x, Stream y)
    {
        unsafe
        {
            byte[] xBuffer = ArrayPool<byte>.Shared.Rent(sizeof(ulong));
            byte[] yBuffer = ArrayPool<byte>.Shared.Rent(sizeof(ulong));

            try
            {
                fixed (byte* xPtr = xBuffer)
                fixed (byte* yPtr = yBuffer)
                {
                    int cmp;
                    int xCount;
                    int yCount;

                    do
                    {
                        xCount = FillBuffer(x, xBuffer, sizeof(ulong));
                        yCount = FillBuffer(y, yBuffer, sizeof(ulong));

                        if (xCount < sizeof(ulong) || yCount < sizeof(ulong))
                            break;

                        cmp = ((ulong*)xPtr)->CompareTo(*(ulong*)yPtr);
                        if (cmp != 0)
                            return cmp;
                    } while (true);

                    return Compare(xPtr, yPtr, xCount, yCount);
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(xBuffer);
                ArrayPool<byte>.Shared.Return(yBuffer);
            }
        }
    }
#endif

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

#if NETCOREAPP2_1_OR_GREATER
    internal static bool Equals32(Stream x, Stream y)
    {
        unsafe
        {
            ulong xLong;
            ulong yLong;
            Span<byte> xBuffer = new Span<byte>(&xLong, 8);
            Span<byte> yBuffer = new Span<byte>(&yLong, 8);

            int xCount;
            int yCount;

            do
            {
                xCount = FillBuffer(x, xBuffer);
                yCount = FillBuffer(y, yBuffer);

                if (xCount < sizeof(ulong) || yCount < sizeof(ulong))
                    break;

                if (xLong != yLong)
                    return false;
            } while (true);

            return Equals((byte*)&xLong, (byte*)&yLong, xCount, yCount);
        }
    }

    internal static bool Equals64(Stream x, Stream y)
    {
        unsafe
        {
            ulong xLong;
            ulong yLong;
            Span<byte> xBuffer = new Span<byte>(&xLong, 8);
            Span<byte> yBuffer = new Span<byte>(&yLong, 8);

            int xCount;
            int yCount;

            do
            {
                xCount = FillBuffer(x, xBuffer);
                yCount = FillBuffer(y, yBuffer);

                if (xCount < sizeof(ulong) || yCount < sizeof(ulong))
                    break;

                if (xLong != yLong)
                    return false;
            } while (true);

            return Equals((byte*)&xLong, (byte*)&yLong, xCount, yCount);
        }
    }
#else
    internal static bool Equals32(Stream x, Stream y)
    {
        unsafe
        {
            byte[] xBuffer = ArrayPool<byte>.Shared.Rent(sizeof(uint));
            byte[] yBuffer = ArrayPool<byte>.Shared.Rent(sizeof(uint));

            try
            {
                fixed (byte* xPtr = xBuffer)
                fixed (byte* yPtr = yBuffer)
                {
                    int xCount;
                    int yCount;

                    do
                    {
                        xCount = FillBuffer(x, xBuffer, sizeof(uint));
                        yCount = FillBuffer(y, yBuffer, sizeof(uint));

                        if (xCount < sizeof(uint) || yCount < sizeof(uint))
                            break;

                        if ((*(uint*)xPtr) != (*(uint*)yPtr))
                            return false;
                    } while (true);

                    return Equals(xPtr, yPtr, xCount, yCount);
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(xBuffer);
                ArrayPool<byte>.Shared.Return(yBuffer);
            }
        }
    }

    internal static bool Equals64(Stream x, Stream y)
    {
        unsafe
        {
            byte[] xBuffer = ArrayPool<byte>.Shared.Rent(sizeof(ulong));
            byte[] yBuffer = ArrayPool<byte>.Shared.Rent(sizeof(ulong));

            try
            {
                fixed (byte* xPtr = xBuffer)
                fixed (byte* yPtr = yBuffer)
                {
                    int xCount;
                    int yCount;

                    do
                    {
                        xCount = FillBuffer(x, xBuffer, sizeof(ulong));
                        yCount = FillBuffer(y, yBuffer, sizeof(ulong));

                        if (xCount < sizeof(ulong) || yCount < sizeof(ulong))
                            break;

                        if ((*(uint*)xPtr) != (*(uint*)yPtr))
                            return false;
                    } while (true);

                    return Equals(xPtr, yPtr, xCount, yCount);
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(xBuffer);
                ArrayPool<byte>.Shared.Return(yBuffer);
            }
        }
    }
#endif

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

    private static unsafe bool Equals(byte* x, byte* y, int xCount, int yCount)
    {
        // TODO: Unroll loop for better performance as we know count will always be < 8
        int minCount = xCount < yCount ? xCount : yCount;
        for (int i = 0; i < minCount; i++)
        {
            if (*(x + i) != *(y + i))
                return false;
        }

        return xCount == yCount;
    }

#if NETCOREAPP2_1_OR_GREATER
    private static int FillBuffer(Stream stream, Span<byte> buffer)
    {
        int read = 0;
        while (read < buffer.Length)
        {
            int next = stream.Read(buffer);
            if (next == 0)
                break;

            read += next;
            buffer = buffer[read..];
        }

        return read;
    }
#else
    private static int FillBuffer(Stream stream, byte[] buffer, int length)
    {
        int read = 0;
        while (read < length)
        {
            int next = stream.Read(buffer, read, length - read);
            if (next == 0)
                break;

            read += next;
        }

        return read;
    }
#endif
}
