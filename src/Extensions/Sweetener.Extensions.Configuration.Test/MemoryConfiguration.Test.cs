// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Extensions.Configuration.Test;

[TestClass]
public class MemoryConfigurationTest
{
    [TestMethod]
    public void Get()
    {
        IConfiguration config = new MemoryConfiguration
        {
            { "Key1"         , "Value1" },
            { "Key2"         , null     },
            { "Section1:Key1", "Value2" },
        };

        Assert.AreEqual("Value1", config["Key1"]);
        Assert.AreEqual(null    , config["Key2"]);
        Assert.AreEqual(null    , config["Key3"]);

        Assert.AreEqual("Value2", config["Section1:Key1"]);
        Assert.AreEqual(null    , config["Section1:Key2"]);
        Assert.AreEqual(null    , config["Section1:Key3"]);
    }
}
