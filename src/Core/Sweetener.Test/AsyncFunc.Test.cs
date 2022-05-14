// Copyright © William Sugarman.
// Licensed under the MIT License.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Test;

[TestClass]
public class AsyncFuncTest
{
    [TestMethod]
    public void Defined()
    {
        Assert.IsNotNull(typeof(AsyncFunc<>));
        Assert.IsNotNull(typeof(AsyncFunc<,>));
        Assert.IsNotNull(typeof(AsyncFunc<,,>));
        Assert.IsNotNull(typeof(AsyncFunc<,,,>));
        Assert.IsNotNull(typeof(AsyncFunc<,,,,>));
        Assert.IsNotNull(typeof(AsyncFunc<,,,,,>));
        Assert.IsNotNull(typeof(AsyncFunc<,,,,,,>));
        Assert.IsNotNull(typeof(AsyncFunc<,,,,,,,>));
        Assert.IsNotNull(typeof(AsyncFunc<,,,,,,,,>));
        Assert.IsNotNull(typeof(AsyncFunc<,,,,,,,,,>));
        Assert.IsNotNull(typeof(AsyncFunc<,,,,,,,,,,>));
        Assert.IsNotNull(typeof(AsyncFunc<,,,,,,,,,,,>));
        Assert.IsNotNull(typeof(AsyncFunc<,,,,,,,,,,,,>));
        Assert.IsNotNull(typeof(AsyncFunc<,,,,,,,,,,,,,>));
        Assert.IsNotNull(typeof(AsyncFunc<,,,,,,,,,,,,,,>));
        Assert.IsNotNull(typeof(AsyncFunc<,,,,,,,,,,,,,,,>));
        Assert.IsNotNull(typeof(AsyncFunc<,,,,,,,,,,,,,,,,>));
    }
}
