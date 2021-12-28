// Copyright © William Sugarman.
// Licensed under the MIT License.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Test;

[TestClass]
public class TryFuncTest
{
    [TestMethod]
    public void Defined()
    {
        Assert.IsNotNull(typeof(TryFunc<>));
        Assert.IsNotNull(typeof(TryFunc<,>));
        Assert.IsNotNull(typeof(TryFunc<,,>));
        Assert.IsNotNull(typeof(TryFunc<,,,>));
        Assert.IsNotNull(typeof(TryFunc<,,,,>));
        Assert.IsNotNull(typeof(TryFunc<,,,,,>));
        Assert.IsNotNull(typeof(TryFunc<,,,,,,>));
        Assert.IsNotNull(typeof(TryFunc<,,,,,,,>));
        Assert.IsNotNull(typeof(TryFunc<,,,,,,,,>));
        Assert.IsNotNull(typeof(TryFunc<,,,,,,,,,>));
        Assert.IsNotNull(typeof(TryFunc<,,,,,,,,,,>));
        Assert.IsNotNull(typeof(TryFunc<,,,,,,,,,,,>));
        Assert.IsNotNull(typeof(TryFunc<,,,,,,,,,,,,>));
        Assert.IsNotNull(typeof(TryFunc<,,,,,,,,,,,,,>));
        Assert.IsNotNull(typeof(TryFunc<,,,,,,,,,,,,,,>));
        Assert.IsNotNull(typeof(TryFunc<,,,,,,,,,,,,,,,>));
        Assert.IsNotNull(typeof(TryFunc<,,,,,,,,,,,,,,,,>));
    }
}
