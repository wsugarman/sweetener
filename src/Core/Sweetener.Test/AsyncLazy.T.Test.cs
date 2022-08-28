// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
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
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new AsyncLazy<object>(t => Task.FromResult<object>(null!), (AsyncLazyThreadSafetyMode)42));

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

        // Even if created, disposal resets the instance
        using AsyncLazy<int> lazy = new AsyncLazy<int>(t => Task.FromResult(10), AsyncLazyThreadSafetyMode.None);

        Assert.AreEqual(10, await lazy.GetValueAsync(default).ConfigureAwait(false));
        Assert.IsTrue(lazy.IsValueCreated);
        lazy.Dispose();

        await Assert.ThrowsExceptionAsync<ObjectDisposedException>(() => lazy.GetValueAsync(default)).ConfigureAwait(false);
        Assert.IsFalse(lazy.IsValueCreated);
        Assert.IsFalse(lazy.IsValueFaulted);
        Assert.AreEqual(default, lazy.ValueForDebugDisplay);
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
        await GetValueAsync_ConcurrentSuccess(tokenSource, AsyncLazyThreadSafetyMode.None).ConfigureAwait(false);

        // Concurrent Failure
        await GetValueAsync_ConcurrentFailure(tokenSource, AsyncLazyThreadSafetyMode.None).ConfigureAwait(false);

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
        await GetValueAsync_ConcurrentSuccess(tokenSource, AsyncLazyThreadSafetyMode.PublicationOnly).ConfigureAwait(false);

        // Concurrent Failure
        await GetValueAsync_ConcurrentFailure(tokenSource, AsyncLazyThreadSafetyMode.PublicationOnly).ConfigureAwait(false);

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
        await GetValueAsync_ConcurrentSuccess(tokenSource, AsyncLazyThreadSafetyMode.ExecutionAndPublication).ConfigureAwait(false);

        // Concurrent Failure
        await GetValueAsync_ConcurrentFailure(tokenSource, AsyncLazyThreadSafetyMode.ExecutionAndPublication).ConfigureAwait(false);

        // Cancellation
        await GetValueAsync_Cancelled(tokenSource, AsyncLazyThreadSafetyMode.ExecutionAndPublication).ConfigureAwait(false);
    }

    private static async Task GetValueAsync_ConcurrentSuccess(CancellationTokenSource tokenSource, AsyncLazyThreadSafetyMode mode)
    {
        int calls = 0;

        int executionThreads = mode is AsyncLazyThreadSafetyMode.ExecutionAndPublication ? 1 : 2;
        using EventCollection enteredEvents = new EventCollection(executionThreads);
        using EventCollection completeEvents = new EventCollection(executionThreads);
        using AsyncLazy<string> concurrentSuccess = new AsyncLazy<string>(
            t => Task.Run(
                () =>
                {
                    int call = Interlocked.Increment(ref calls);

                    int i = Math.Min(call, executionThreads) - 1;
                    enteredEvents.Set(i);
                    completeEvents.Wait(i);

                    return $"concurrent success #{call}";
                },
                t),
            mode);

        (Task<string> t1, Task<string> t2) = (concurrentSuccess.GetValueAsync(tokenSource.Token), concurrentSuccess.GetValueAsync(tokenSource.Token));
        enteredEvents.WaitAll();

        Assert.IsFalse(concurrentSuccess.IsValueCreated);
        Assert.IsFalse(t1.IsCompleted);
        Assert.IsFalse(t2.IsCompleted);

        completeEvents.SetAll();

        string[] results = await Task.WhenAll(t1, t2).ConfigureAwait(false);
        Assert.IsTrue(concurrentSuccess.IsValueCreated);

        switch (mode)
        {
            case AsyncLazyThreadSafetyMode.None:
                Assert.IsTrue(results.Contains("concurrent success #1", StringComparer.Ordinal));
                Assert.IsTrue(results.Contains("concurrent success #2", StringComparer.Ordinal));
                break;
            case AsyncLazyThreadSafetyMode.PublicationOnly:
                Assert.IsTrue(results[0] is "concurrent success #1" or "concurrent success #2");
                Assert.AreSame(results[0], results[1]);
                break;
            default:
                Assert.AreEqual("concurrent success #1", results[0]);
                Assert.AreSame(results[0], results[1]);
                break;
        }
    }

    private static async Task GetValueAsync_ConcurrentFailure(CancellationTokenSource tokenSource, AsyncLazyThreadSafetyMode mode)
    {
        int calls = 0;

        int executionThreads = mode is AsyncLazyThreadSafetyMode.ExecutionAndPublication ? 1 : 2;
        using EventCollection enteredEvents = new EventCollection(executionThreads);
        using EventCollection completeEvents = new EventCollection(executionThreads);
        using AsyncLazy<string> concurrentFailure = new AsyncLazy<string>(
            t => Task.Run((Func<string>)
                (() =>
                {
                    int call = Interlocked.Increment(ref calls);

                    int i = Math.Min(call, executionThreads) - 1;
                    enteredEvents.Set(i);
                    completeEvents.Wait(i);

                    throw new ArgumentOutOfRangeException(null, call, null);
                }),
                t),
            mode);

        (Task<string> t1, Task<string> t2) = (concurrentFailure.GetValueAsync(tokenSource.Token), concurrentFailure.GetValueAsync(tokenSource.Token));
        enteredEvents.WaitAll();

        Assert.IsFalse(concurrentFailure.IsValueCreated);
        Assert.IsFalse(concurrentFailure.IsValueFaulted);
        Assert.IsFalse(t1.IsCompleted);
        Assert.IsFalse(t2.IsCompleted);

        completeEvents.SetAll();

        ArgumentOutOfRangeException e1 = await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => t1).ConfigureAwait(false);
        ArgumentOutOfRangeException e2 = await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => t2).ConfigureAwait(false);
        Assert.IsFalse(concurrentFailure.IsValueCreated);
        Assert.AreEqual(mode != AsyncLazyThreadSafetyMode.PublicationOnly, concurrentFailure.IsValueFaulted);

        switch (mode)
        {
            case AsyncLazyThreadSafetyMode.None or AsyncLazyThreadSafetyMode.PublicationOnly when e1.ActualValue!.Equals(1):
                Assert.AreEqual(2, e2.ActualValue);
                break;
            case AsyncLazyThreadSafetyMode.None or AsyncLazyThreadSafetyMode.PublicationOnly:
                Assert.AreEqual(2, e1.ActualValue);
                Assert.AreEqual(1, e2.ActualValue);
                break;
            default:
                Assert.AreEqual(1, e1.ActualValue);
                Assert.AreEqual(e1.ActualValue, e2.ActualValue);
                break;
        }
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

    private sealed class EventCollection : IDisposable, IEnumerable<ManualResetEventSlim>
    {
        private readonly List<ManualResetEventSlim> _events;

        public EventCollection(int count)
            => _events = Enumerable
            .Range(0, count)
            .Select(x => new ManualResetEventSlim(false))
            .ToList();

        public void Dispose()
        {
            foreach (ManualResetEventSlim e in _events)
                e.Dispose();
        }

        public IEnumerator<ManualResetEventSlim> GetEnumerator()
            => _events.GetEnumerator();

        public void Set(int i)
            => _events[i].Set();

        public void SetAll()
        {
            foreach (ManualResetEventSlim e in _events)
                e.Set();
        }

        public void Wait(int i)
            => _events[i].Wait();

        public void WaitAll()
        {
            foreach (ManualResetEventSlim e in _events)
                e.Wait();
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
