// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Test;

[TestClass]
public class StringExtensionsTest
{
    [TestMethod]
    public void IsWhiteSpace()
    {
        Assert.ThrowsException<ArgumentNullException>(() => StringExtensions.IsWhiteSpace(null!));

        Assert.IsFalse("foo".IsWhiteSpace());
        Assert.IsFalse(" bar ".IsWhiteSpace());
        Assert.IsTrue("".IsWhiteSpace());
        Assert.IsTrue(" ".IsWhiteSpace());
        Assert.IsTrue("\t\r\n ".IsWhiteSpace());
    }
}
