// Copyright © William Sugarman.
// Licensed under the MIT License.

namespace Sweetener;

/// <summary>
/// Specifies the byte order in which data is stored.
/// </summary>
public enum Endianness
{
    /// <summary>
    /// Specifies the least significant byte is present at the smallest memory address.
    /// </summary>
    LittleEndian,

    /// <summary>
    /// Specifies the most significant byte is present at the smallest memory address.
    /// </summary>
    BigEndian,
}
