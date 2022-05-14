// Copyright © William Sugarman.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Test.Linq;

internal static class CodeCoverageAssert
{
    public static void AreSequencesEqual<T>(IEnumerable<T> actual, params T[] expected)
        => AreSequencesEqual(expected, actual);

    public static void AreSequencesEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual)
    {
        // Enumerate using both IEnumerable<T> and IEnumerable for code coverage
        Assert.That.AreSequencesEqual(expected, actual);
        Assert.That.AreSequencesEqual((IEnumerable)expected, actual);
    }
}
