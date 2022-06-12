// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweetener.Test.Linq;

namespace Sweetener.Linq.Test;

[TestClass]
public class EnumerableExtensionsTest
{
    [TestMethod]
    public void SelectWhere()
    {
        Assert.ThrowsException<ArgumentNullException>(() => EnumerableExtensions.SelectWhere<string, int>(null!, int.TryParse));
        Assert.ThrowsException<ArgumentNullException>(() => EnumerableExtensions.SelectWhere(Array.Empty<string>(), (TryFunc<string, double>)null!));

        // Single Transform
        IEnumerable<int> numbers = new string?[] { "1", "2", "foo", "3", "four", "4", "5", null }
            .SelectWhere<string?, int>(int.TryParse);

        CodeCoverageAssert.AreSequencesEqual(numbers, 1, 2, 3, 4, 5);

        // Multiple transformations
        IEnumerable<long> moreNumbers = new string?[] { "1", "2", "foo", "3", "four", "4", "5", null }
            .SelectWhere<string?, int>(int.TryParse)
            .SelectWhere<int, long>(TryDouble);

        CodeCoverageAssert.AreSequencesEqual(moreNumbers, 4, 8);

        static bool TryDouble(int n, out long value)
        {
            if (n % 2 == 0)
            {
                value = n * 2;
                return true;
            }

            value = default;
            return false;
        }
    }

    [TestMethod]
    public void SelectWhere_Index()
    {
        Assert.ThrowsException<ArgumentNullException>(() => EnumerableExtensions.SelectWhere<string, double>(null!, TryParse));
        Assert.ThrowsException<ArgumentNullException>(() => EnumerableExtensions.SelectWhere(Array.Empty<string>(), (TryFunc<string, int, TimeSpan>)null!));

        // Single Transform
        IEnumerable<double> numbers = new string?[] { "1.1", "2.2", "foo", "3.3", "four", "4.4", "5.5", null }
            .SelectWhere<string?, double>(TryParse);

        CodeCoverageAssert.AreSequencesEqual(numbers, 0.0d, 2.2d, 9.9d, 22.0d, 33.0d);

        // Multiple transformations
        IEnumerable<int> moreNumbers = new string?[] { "1.1", "2.2", "foo", "3.3", "four", "4.4", "5.5", null }
            .SelectWhere<string?, double>(TryParse)
            .SelectWhere<double, int>(TryGetInteger);

        CodeCoverageAssert.AreSequencesEqual(moreNumbers, 0, 25, 37);

        static bool TryParse(string? s, int i, out double value)
        {
            if (double.TryParse(s, out value))
            {
                value = Math.Round(value * i, 1);
                return true;
            }

            return false;
        }

        static bool TryGetInteger(double d, int i, out int value)
        {
            if (d == (int)d)
            {
                value = (int)d + i;
                return true;
            }

            value = default;
            return false;
        }
    }
}
