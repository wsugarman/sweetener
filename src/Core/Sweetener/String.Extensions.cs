// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;

namespace Sweetener;

/// <summary>
/// Provides a set of additional methods for <see cref="string"/> instances.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Indicates whether a specified string is <see cref="string.Empty"/>
    /// or consists only of white-space characters.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="value"/> parameter is <see cref="string.Empty"/>
    /// or consists exclusively of white-space characters; otherwise, <see langword="false"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
    public static bool IsWhiteSpace(this string value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        for (int i = 0; i < value.Length; i++)
        {
            if (!char.IsWhiteSpace(value[i]))
                return false;
        }

        return true;
    }
}
