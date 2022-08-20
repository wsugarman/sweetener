// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweetener.Threading;

namespace Sweetener.Test;

[TestClass]
public class AsyncLazyTest
{
    [TestMethod]
    public async Task Ctor_ValueFactory()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new AsyncLazy<object>(null!));

        using AsyncLazy<int> lazy = new AsyncLazy<int>(t => Task.FromResult(1));
        Assert.AreEqual(AsyncLazyThreadSafetyMode.ExecutionAndPublication, lazy.Mode);
        Assert.AreEqual(1, await lazy.GetValueAsync(default).ConfigureAwait(false));
    }

    [TestMethod]
    public async Task Ctor_ValueFactory_IsThreadSafe()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new AsyncLazy<object>(null!, true));

        using AsyncLazy<int> lazy = new AsyncLazy<int>(t => Task.FromResult(2), false);
        Assert.AreEqual(AsyncLazyThreadSafetyMode.None, lazy.Mode);
        Assert.AreEqual(2, await lazy.GetValueAsync(default).ConfigureAwait(false));
    }

    [TestMethod]
    public async Task Ctor_ValueFactory_Mode()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new AsyncLazy<object>(null!, AsyncLazyThreadSafetyMode.None));

        using AsyncLazy<int> lazy = new AsyncLazy<int>(t => Task.FromResult(3), AsyncLazyThreadSafetyMode.PublicationOnly);
        Assert.AreEqual(AsyncLazyThreadSafetyMode.PublicationOnly, lazy.Mode);
        Assert.AreEqual(3, await lazy.GetValueAsync(default).ConfigureAwait(false));
    }

    [TestMethod]
    public async Task IsValueCreated()
    {
        using AsyncLazy<int> lazy = new AsyncLazy<int>(t => Task.FromResult(4));

        // No underlying task
        Assert.IsFalse(lazy.IsValueCreated);

        // Task completed
        Assert.AreEqual(4, await lazy.GetValueAsync(default).ConfigureAwait(false));
        Assert.IsTrue(lazy.IsValueCreated);

        // Task failed
        using AsyncLazy<int> error = new AsyncLazy<int>(t => Task.FromException<int>(new InvalidOperationException()));
        await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => error.GetValueAsync(default)).ConfigureAwait(false);
        Assert.IsFalse(error.IsValueCreated);
    }

    [TestMethod]
    public async Task IsValueFailed()
    {
        using AsyncLazy<int> lazy = new AsyncLazy<int>(t => Task.FromResult(5));

        // No underlying task
        Assert.IsFalse(lazy.IsValueFaulted);

        // Task completed
        Assert.AreEqual(5, await lazy.GetValueAsync(default).ConfigureAwait(false));
        Assert.IsFalse(lazy.IsValueFaulted);

        // Task failed
        using AsyncLazy<int> error = new AsyncLazy<int>(t => Task.FromException<int>(new InvalidOperationException()));
        await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => error.GetValueAsync(default)).ConfigureAwait(false);
        Assert.IsTrue(error.IsValueFaulted);
    }

    [TestMethod]
    public async Task Dispose()
    {
        // None
        AsyncLazy<int> none = new AsyncLazy<int>(t => Task.FromResult(6), AsyncLazyThreadSafetyMode.None);
        none.Dispose();

        await Assert.ThrowsExceptionAsync<ObjectDisposedException>(() => none.GetValueAsync(default)).ConfigureAwait(false);

        // PublicationOnly
        AsyncLazy<int> publicationOnly = new AsyncLazy<int>(t => Task.FromResult(7), AsyncLazyThreadSafetyMode.PublicationOnly);
        publicationOnly.Dispose();

        await Assert.ThrowsExceptionAsync<ObjectDisposedException>(() => publicationOnly.GetValueAsync(default)).ConfigureAwait(false);

        // ExecutionAndPublication
        AsyncLazy<int> executionAndPublication = new AsyncLazy<int>(t => Task.FromResult(8), AsyncLazyThreadSafetyMode.ExecutionAndPublication);
        executionAndPublication.Dispose();

        await Assert.ThrowsExceptionAsync<ObjectDisposedException>(() => executionAndPublication.GetValueAsync(default)).ConfigureAwait(false);

        // Value always available after caching
        using AsyncLazy<int> lazy = new AsyncLazy<int>(t => Task.FromResult(9), AsyncLazyThreadSafetyMode.None);

        Assert.AreEqual(9, await lazy.GetValueAsync(default).ConfigureAwait(false));
        Assert.IsTrue(lazy.IsValueCreated);
        lazy.Dispose();

        Assert.AreEqual(9, await lazy.GetValueAsync(default).ConfigureAwait(false));
    }

    [TestMethod]
    public async Task ToStringTest()
    {
        using AsyncLazy<long> lazy = new AsyncLazy<long>(t => Task.FromResult(12345L));

        // No underlying task
        Assert.AreEqual("Value is not created.", lazy.ToString());

        // Task completed
        Assert.AreEqual(12345L, await lazy.GetValueAsync(default).ConfigureAwait(false));
        Assert.AreEqual("12345", lazy.ToString());

        // Task failed
        using AsyncLazy<long> error = new AsyncLazy<long>(t => Task.FromException<long>(new InvalidOperationException()));
        await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => error.GetValueAsync(default)).ConfigureAwait(false);
        Assert.AreEqual("Value is not created.", error.ToString());

        // Null value
        using AsyncLazy<object?> nullLazy = new AsyncLazy<object?>(t => Task.FromResult<object?>(null));
        Assert.IsNull(await nullLazy.GetValueAsync(default).ConfigureAwait(false));
        Assert.ThrowsException<NullReferenceException>(() => nullLazy.ToString());
    }
}
