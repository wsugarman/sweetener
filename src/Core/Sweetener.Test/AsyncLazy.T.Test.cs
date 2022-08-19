// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Test;

[TestClass]
public class AsyncLazyTest
{
    [TestMethod]
    public async void Ctor_ValueFactory()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new AsyncLazy<object>(null!));

        using AsyncLazy<int> lazy = new AsyncLazy<int>(t => Task.FromResult(42));
        Assert.AreEqual(42, await lazy.GetValueAsync(default));
    }

    [TestMethod]
    public async void Ctor_ValueFactory_IsThreadSafe()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new AsyncLazy<object>(null!, false));

        using AsyncLazy<int> lazy = new AsyncLazy<int>(t => Task.FromResult(42), true);
        Assert.AreEqual(42, await lazy.GetValueAsync(default));
    }

    [TestMethod]
    public async void Ctor_ValueFactory_Mode()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new AsyncLazy<object>(null!, LazyThreadSafetyMode.None));

        using AsyncLazy<int> lazy = new AsyncLazy<int>(t => Task.FromResult(42), LazyThreadSafetyMode.ExecutionAndPublication);
        Assert.AreEqual(42, await lazy.GetValueAsync(default));
    }
}
