using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Diagnostics.Test
{
    internal static class AssertExtensions
    {
        public static void AreEqual<T>(this Assert assert, T expected, T actual, IEqualityComparer<T> comparer)
        {
            if (!comparer.Equals(expected, actual))
                throw new AssertFailedException($"Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}
