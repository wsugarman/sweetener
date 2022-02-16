// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Test;

[TestClass]
public class DateSpanFormatTest
{
    [TestMethod]
    public void Format()
    {
        DateSpan dateSpan = new DateSpan(DateTime.Now, TimeSpan.FromDays(37));
        string v1 = dateSpan.ToString("i");
        string v2 = dateSpan.ToString("I");
        string v3 = dateSpan.ToString("s");
        string v4 = dateSpan.ToString("S");
        string v5 = dateSpan.ToString("e");
        string v6 = dateSpan.ToString("E");

        Assert.AreNotEqual(v1, v2);
    }
}
