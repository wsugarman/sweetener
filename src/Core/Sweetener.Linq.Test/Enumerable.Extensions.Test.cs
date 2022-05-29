// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        List<int> numbers = new string?[] { "1", "2", "foo", "3", "four", "4", "5", null }
            .SelectWhere<string?, int>(int.TryParse)
            .ToList();

        Assert.AreEqual(5, numbers.Count);

        Assert.AreEqual(1, numbers[0]);
        Assert.AreEqual(2, numbers[1]);
        Assert.AreEqual(3, numbers[2]);
        Assert.AreEqual(4, numbers[3]);
        Assert.AreEqual(5, numbers[4]);

        // Multiple transformations
        List<long> moreNumbers = new string?[] { "1", "2", "foo", "3", "four", "4", "5", null }
            .SelectWhere<string?, int>(int.TryParse)
            .SelectWhere<int, long>(TryDouble)
            .ToList();

        Assert.AreEqual(2, moreNumbers.Count);

        Assert.AreEqual(4, moreNumbers[0]);
        Assert.AreEqual(8, moreNumbers[1]);

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

        List<double> numbers = new string?[] { "1.1", "2.2", "foo", "3.3", "four", "4.4", "5.5", null }
            .SelectWhere<string?, double>(TryParse)
            .ToList();

        Assert.AreEqual(5, numbers.Count);

        Assert.AreEqual( 0.0d, numbers[0]);
        Assert.AreEqual( 2.2d, numbers[1]);
        Assert.AreEqual( 9.9d, numbers[2]);
        Assert.AreEqual(22.0d, numbers[3]);
        Assert.AreEqual(33.0d, numbers[4]);

        List<int> moreNumbers = new string?[] { "1.1", "2.2", "foo", "3.3", "four", "4.4", "5.5", null }
            .SelectWhere<string?, double>(TryParse)
            .SelectWhere<double, int>(TryGetInteger)
            .ToList();

        Assert.AreEqual(3, moreNumbers.Count);

        Assert.AreEqual( 0, moreNumbers[0]);
        Assert.AreEqual(25, moreNumbers[1]);
        Assert.AreEqual(37, moreNumbers[2]);

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
