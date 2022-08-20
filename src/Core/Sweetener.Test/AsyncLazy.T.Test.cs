// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Linq;
using System.Threading;
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
    public async Task ValueForDebugDisplay()
    {
        using AsyncLazy<int> lazy = new AsyncLazy<int>(t => Task.FromResult(6));

        // No underlying task
        Assert.AreEqual(0, lazy.ValueForDebugDisplay);

        // Task completed
        Assert.AreEqual(6, await lazy.GetValueAsync(default).ConfigureAwait(false));
        Assert.AreEqual(6, lazy.ValueForDebugDisplay);

        // Task failed
        using AsyncLazy<int> error = new AsyncLazy<int>(t => Task.FromException<int>(new InvalidOperationException()));
        await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => error.GetValueAsync(default)).ConfigureAwait(false);
        Assert.AreEqual(0, error.ValueForDebugDisplay);
    }

    [TestMethod]
    public async Task Dispose()
    {
        // None
        AsyncLazy<int> none = new AsyncLazy<int>(t => Task.FromResult(7), AsyncLazyThreadSafetyMode.None);
        none.Dispose();

        await Assert.ThrowsExceptionAsync<ObjectDisposedException>(() => none.GetValueAsync(default)).ConfigureAwait(false);

        // PublicationOnly
        AsyncLazy<int> publicationOnly = new AsyncLazy<int>(t => Task.FromResult(8), AsyncLazyThreadSafetyMode.PublicationOnly);
        publicationOnly.Dispose();

        await Assert.ThrowsExceptionAsync<ObjectDisposedException>(() => publicationOnly.GetValueAsync(default)).ConfigureAwait(false);

        // ExecutionAndPublication
        AsyncLazy<int> executionAndPublication = new AsyncLazy<int>(t => Task.FromResult(9), AsyncLazyThreadSafetyMode.ExecutionAndPublication);
        executionAndPublication.Dispose();

        await Assert.ThrowsExceptionAsync<ObjectDisposedException>(() => executionAndPublication.GetValueAsync(default)).ConfigureAwait(false);

        // Value always available after caching
        using AsyncLazy<int> lazy = new AsyncLazy<int>(t => Task.FromResult(10), AsyncLazyThreadSafetyMode.None);

        Assert.AreEqual(10, await lazy.GetValueAsync(default).ConfigureAwait(false));
        Assert.IsTrue(lazy.IsValueCreated);
        lazy.Dispose();

        Assert.AreEqual(10, await lazy.GetValueAsync(default).ConfigureAwait(false));
    }

    [TestMethod]
    public async Task GetValueAsync()
    {
        // Common scenarios
        // Cancellation
        using CancellationTokenSource tokenSource = new CancellationTokenSource();
        using AsyncLazy<string> lazy = new AsyncLazy<string>(t => Task.FromResult("foo"));
        tokenSource.Cancel();

        await Assert.ThrowsExceptionAsync<OperationCanceledException>(() => lazy.GetValueAsync(tokenSource.Token)).ConfigureAwait(false);

        Assert.AreEqual("foo", await lazy.GetValueAsync(default).ConfigureAwait(false));
        await Assert.ThrowsExceptionAsync<OperationCanceledException>(() => lazy.GetValueAsync(tokenSource.Token)).ConfigureAwait(false);

        // Cached value
        Assert.AreEqual("foo", await lazy.GetValueAsync(default).ConfigureAwait(false));

        // Different AsyncLazyThreadSafetyModes
        await GetValueAsync_None().ConfigureAwait(false);
        await GetValueAsync_PublicationOnly().ConfigureAwait(false);
        await GetValueAsync_ExecutionAndPublication().ConfigureAwait(false);
    }

    private static async Task GetValueAsync_None()
    {
        using CancellationTokenSource tokenSource = new CancellationTokenSource();

        // Failure
        using AsyncLazy<string> failure = new AsyncLazy<string>(t => Task.Run((Func<string>)(() => throw new IOException())), AsyncLazyThreadSafetyMode.None);

        Assert.IsFalse(failure.IsValueCreated);
        await Assert.ThrowsExceptionAsync<IOException>(() => failure.GetValueAsync(tokenSource.Token)).ConfigureAwait(false);
        Assert.IsFalse(failure.IsValueCreated);

        // Failure is cached
        await Assert.ThrowsExceptionAsync<IOException>(() => failure.GetValueAsync(tokenSource.Token)).ConfigureAwait(false);
        Assert.IsFalse(failure.IsValueCreated);

        // Failure (before task)
        using AsyncLazy<string> preTaskFailure = new AsyncLazy<string>(t => throw new ArgumentNullException(), AsyncLazyThreadSafetyMode.None);

        Assert.IsFalse(preTaskFailure.IsValueCreated);
        await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => preTaskFailure.GetValueAsync(tokenSource.Token)).ConfigureAwait(false);
        Assert.IsFalse(preTaskFailure.IsValueCreated);

        // Failure is (still) cached
        await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => preTaskFailure.GetValueAsync(tokenSource.Token)).ConfigureAwait(false);
        Assert.IsFalse(preTaskFailure.IsValueCreated);

        // Success
        using AsyncLazy<string> success = new AsyncLazy<string>(t => Task.Run(() => "success", t), AsyncLazyThreadSafetyMode.None);

        Assert.IsFalse(success.IsValueCreated);
        Assert.AreEqual("success", await success.GetValueAsync(tokenSource.Token).ConfigureAwait(false));
        Assert.IsTrue(success.IsValueCreated);

        // Concurrent Success
        int calls = 0;
        using ManualResetEventSlim enteredEvent1 = new ManualResetEventSlim(false);
        using ManualResetEventSlim enteredEvent2 = new ManualResetEventSlim(false);
        using ManualResetEventSlim completeEvent1 = new ManualResetEventSlim(false);
        using ManualResetEventSlim completeEvent2 = new ManualResetEventSlim(false);
        using AsyncLazy<string> concurrentSuccess = new AsyncLazy<string>(
            t => Task.Run(
                () =>
                {
                    int call = Interlocked.Increment(ref calls);
                    if (call == 1)
                    {
                        enteredEvent1.Set();
                        completeEvent1.Wait();
                    }
                    else
                    {
                        enteredEvent2.Set();
                        completeEvent2.Wait();
                    }

                    return $"concurrent success #{call}";
                },
                t),
            AsyncLazyThreadSafetyMode.None);

        (Task<string> t1, Task<string> t2) = (concurrentSuccess.GetValueAsync(tokenSource.Token), concurrentSuccess.GetValueAsync(tokenSource.Token));
        enteredEvent1.Wait();
        enteredEvent2.Wait();

        Assert.IsFalse(concurrentSuccess.IsValueCreated);

        completeEvent1.Set();
        completeEvent2.Set();

        string[] results = await Task.WhenAll(t1, t2).ConfigureAwait(false);
        Assert.IsTrue(results.Contains("concurrent success #1", StringComparer.Ordinal));
        Assert.IsTrue(results.Contains("concurrent success #2", StringComparer.Ordinal));
        Assert.IsTrue(concurrentSuccess.IsValueCreated);

        // Cancellation
        await GetValueAsync_Cancelled(tokenSource, AsyncLazyThreadSafetyMode.None).ConfigureAwait(false);
    }

    private static async Task GetValueAsync_PublicationOnly()
    {
        using CancellationTokenSource tokenSource = new CancellationTokenSource();

        // Failure
        int calls = 0;
        using AsyncLazy<string> failure = new AsyncLazy<string>(
            t => Task.Run(() => calls++ == 0 ? throw new InvalidOperationException() : "transient failure", t),
            AsyncLazyThreadSafetyMode.PublicationOnly);

        Assert.IsFalse(failure.IsValueCreated);
        await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => failure.GetValueAsync(tokenSource.Token)).ConfigureAwait(false);
        Assert.IsFalse(failure.IsValueCreated);

        // Failure is not cached
        Assert.AreEqual("transient failure", await failure.GetValueAsync(tokenSource.Token).ConfigureAwait(false));
        Assert.IsTrue(failure.IsValueCreated);

        // Failure (before task)
        calls = 0;
        using AsyncLazy<string> preTaskFailure = new AsyncLazy<string>(
            t => calls++ == 0 ? throw new FormatException() : Task.Run(() => "transient failure", t),
            AsyncLazyThreadSafetyMode.PublicationOnly);

        Assert.IsFalse(preTaskFailure.IsValueCreated);
        await Assert.ThrowsExceptionAsync<FormatException>(() => preTaskFailure.GetValueAsync(tokenSource.Token)).ConfigureAwait(false);
        Assert.IsFalse(preTaskFailure.IsValueCreated);

        // Failure is (still) not cached
        Assert.AreEqual("transient failure", await failure.GetValueAsync(tokenSource.Token).ConfigureAwait(false));
        Assert.IsTrue(failure.IsValueCreated);

        // Success
        using AsyncLazy<string> success = new AsyncLazy<string>(t => Task.Run(() => "success"), AsyncLazyThreadSafetyMode.PublicationOnly);

        Assert.IsFalse(success.IsValueCreated);
        Assert.AreEqual("success", await success.GetValueAsync(tokenSource.Token).ConfigureAwait(false));
        Assert.IsTrue(success.IsValueCreated);

        // Concurrent Success
        calls = 0;
        using ManualResetEventSlim enteredEvent1 = new ManualResetEventSlim(false);
        using ManualResetEventSlim enteredEvent2 = new ManualResetEventSlim(false);
        using ManualResetEventSlim completeEvent1 = new ManualResetEventSlim(false);
        using ManualResetEventSlim completeEvent2 = new ManualResetEventSlim(false);
        using AsyncLazy<string> concurrentSuccess = new AsyncLazy<string>(
            t => Task.Run(
                () =>
                {
                    int call = Interlocked.Increment(ref calls);
                    if (call == 1)
                    {
                        enteredEvent1.Set();
                        completeEvent1.Wait();
                    }
                    else
                    {
                        enteredEvent2.Set();
                        completeEvent2.Wait();
                    }

                    return $"concurrent success #{call}";
                },
                t),
            AsyncLazyThreadSafetyMode.PublicationOnly);

        (Task<string> t1, Task<string> t2) = (concurrentSuccess.GetValueAsync(tokenSource.Token), concurrentSuccess.GetValueAsync(tokenSource.Token));
        enteredEvent1.Wait();
        enteredEvent2.Wait();

        Assert.IsFalse(concurrentSuccess.IsValueCreated);

        completeEvent1.Set();
        completeEvent2.Set();

        string[] results = await Task.WhenAll(t1, t2).ConfigureAwait(false);
        Assert.AreSame(results[0], results[1]);
        Assert.IsTrue(concurrentSuccess.IsValueCreated);

        // Cancellation
        await GetValueAsync_Cancelled(tokenSource, AsyncLazyThreadSafetyMode.PublicationOnly).ConfigureAwait(false);
    }

    private static async Task GetValueAsync_ExecutionAndPublication()
    {
        using CancellationTokenSource tokenSource = new CancellationTokenSource();

        // Failure
        using AsyncLazy<string> failure = new AsyncLazy<string>(t => Task.Run((Func<string>)(() => throw new NotSupportedException())), AsyncLazyThreadSafetyMode.ExecutionAndPublication);

        Assert.IsFalse(failure.IsValueCreated);
        await Assert.ThrowsExceptionAsync<NotSupportedException>(() => failure.GetValueAsync(tokenSource.Token)).ConfigureAwait(false);
        Assert.IsFalse(failure.IsValueCreated);

        // Failure is cached
        await Assert.ThrowsExceptionAsync<NotSupportedException>(() => failure.GetValueAsync(tokenSource.Token)).ConfigureAwait(false);
        Assert.IsFalse(failure.IsValueCreated);

        // Failure (before task)
        using AsyncLazy<string> preTaskFailure = new AsyncLazy<string>(t => throw new ArgumentOutOfRangeException(), AsyncLazyThreadSafetyMode.ExecutionAndPublication);

        Assert.IsFalse(preTaskFailure.IsValueCreated);
        await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => preTaskFailure.GetValueAsync(tokenSource.Token)).ConfigureAwait(false);
        Assert.IsFalse(preTaskFailure.IsValueCreated);

        // Failure is (still) cached
        await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => preTaskFailure.GetValueAsync(tokenSource.Token)).ConfigureAwait(false);
        Assert.IsFalse(preTaskFailure.IsValueCreated);

        // Success
        using AsyncLazy<string> success = new AsyncLazy<string>(t => Task.Run(() => "success", t), AsyncLazyThreadSafetyMode.ExecutionAndPublication);

        Assert.IsFalse(success.IsValueCreated);
        Assert.AreEqual("success", await success.GetValueAsync(tokenSource.Token).ConfigureAwait(false));
        Assert.IsTrue(success.IsValueCreated);

        // Concurrent Success
        int calls = 0;
        using ManualResetEventSlim enteredEvent = new ManualResetEventSlim(false);
        using ManualResetEventSlim completeEvent = new ManualResetEventSlim(false);
        using AsyncLazy<string> concurrentSuccess = new AsyncLazy<string>(
            t => Task.Run(
                () =>
                {
                    Interlocked.Increment(ref calls);
                    enteredEvent.Set();
                    completeEvent.Wait();
                    return "concurrent success";
                },
                t),
            AsyncLazyThreadSafetyMode.ExecutionAndPublication);

        (Task<string> t1, Task<string> t2) = (concurrentSuccess.GetValueAsync(tokenSource.Token), concurrentSuccess.GetValueAsync(tokenSource.Token));
        enteredEvent.Wait();

        Assert.IsFalse(t1.IsCompleted); // Ensure that we did not hit the cache _valueTask
        Assert.IsFalse(t2.IsCompleted);
        Assert.IsFalse(concurrentSuccess.IsValueCreated);
        Assert.IsFalse(concurrentSuccess.IsValueCreated);

        completeEvent.Set();

        string r1 = await t1.ConfigureAwait(false);
        string r2 = await t2.ConfigureAwait(false);

        Assert.AreEqual("concurrent success", r1);
        Assert.AreSame(r1, r2);
        Assert.AreEqual(1, calls);
        Assert.IsTrue(concurrentSuccess.IsValueCreated);

        // Cancellation
        await GetValueAsync_Cancelled(tokenSource, AsyncLazyThreadSafetyMode.ExecutionAndPublication).ConfigureAwait(false);
    }

    private static async Task GetValueAsync_Cancelled(CancellationTokenSource tokenSource, AsyncLazyThreadSafetyMode mode)
    {
        using ManualResetEventSlim enteredEvent = new ManualResetEventSlim(false);
        using ManualResetEventSlim continueEvent = new ManualResetEventSlim(false);
        using AsyncLazy<string> canceled = new AsyncLazy<string>(
            t => Task.Run(
                () =>
                {
                    enteredEvent.Set();
                    continueEvent.Wait();
                    t.ThrowIfCancellationRequested();
                    return "long-running operation";
                },
                t),
            mode);

        Task<string> getValueTask = canceled.GetValueAsync(tokenSource.Token);
        enteredEvent.Wait();
        Assert.IsFalse(canceled.IsValueCreated);

        tokenSource.Cancel();
        continueEvent.Set();

        await Assert.ThrowsExceptionAsync<OperationCanceledException>(() => getValueTask).ConfigureAwait(false);
        Assert.IsFalse(canceled.IsValueCreated);

        // Cancellation is not cached
        Assert.AreEqual("long-running operation", await canceled.GetValueAsync(default).ConfigureAwait(false));
        Assert.IsTrue(canceled.IsValueCreated);
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

    [TestMethod]
    public async Task AsyncLazyDebugView()
    {
        using AsyncLazy<string> lazy = new AsyncLazy<string>(t => Task.FromResult("debugging"));
        AsyncLazyDebugView<string> lazyView = new AsyncLazyDebugView<string>(lazy);

        // No underlying task
        Assert.IsFalse(lazyView.IsValueCreated);
        Assert.IsFalse(lazyView.IsValueFaulted);
        Assert.AreEqual(lazyView.Mode, AsyncLazyThreadSafetyMode.ExecutionAndPublication);
        Assert.AreEqual(null, lazyView.Value);

        // Most importantly, fetching "Value" does not resolve the underlying AsyncLazy<T>
        Assert.IsFalse(lazyView.IsValueCreated);

        // Task completed
        Assert.AreEqual("debugging", await lazy.GetValueAsync(default).ConfigureAwait(false));
        Assert.IsTrue(lazyView.IsValueCreated);
        Assert.IsFalse(lazyView.IsValueFaulted);
        Assert.AreEqual(lazyView.Mode, AsyncLazyThreadSafetyMode.ExecutionAndPublication);
        Assert.AreEqual("debugging", lazyView.Value);

        // Task failed
        using AsyncLazy<string> error = new AsyncLazy<string>(t => Task.FromException<string>(new FileNotFoundException()), AsyncLazyThreadSafetyMode.None);
        AsyncLazyDebugView<string> errorView = new AsyncLazyDebugView<string>(error);

        await Assert.ThrowsExceptionAsync<FileNotFoundException>(() => error.GetValueAsync(default)).ConfigureAwait(false);
        Assert.IsFalse(errorView.IsValueCreated);
        Assert.IsTrue(errorView.IsValueFaulted);
        Assert.AreEqual(errorView.Mode, AsyncLazyThreadSafetyMode.None);
        Assert.AreEqual(null, errorView.Value);
    }
}
