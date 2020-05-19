// Generated from Reliably.Async.Test.tt
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    partial class ReliablyTest
    {
        #region InvokeAsync

        [TestMethod]
        public async Task InvokeAsync_AsyncAction_DelayHandler()
        {
#nullable disable

            Func<Task> nullAction = null;
            Func<Task> testAction = () => Task.CompletedTask;
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testAction, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, 10, null                                        , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Task> invoke = (a, t, n, e, d) =>
            {
                Func<Task> inputAction = () =>
                {
                    a();
                    return Task.CompletedTask;
                };

                return Reliably.InvokeAsync(inputAction, n, e, d.Invoke);
            };
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => invoke(a, t, n, e, d).Wait();
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Type> assertError   = (a, t, n, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(a, t, n, e, d), x).Wait();

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_Failure         (assertError  , t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_EventualFailure (assertError  , t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_RetriesExhausted(assertError  , t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public async Task InvokeAsync_AsyncAction_ComplexDelayHandler()
        {
#nullable disable

            Func<Task> nullAction = null;
            Func<Task> testAction = () => Task.CompletedTask;
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testAction, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, 10, null                                        , (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Task> invoke = (a, t, n, e, d) =>
            {
                Func<Task> inputAction = () =>
                {
                    a();
                    return Task.CompletedTask;
                };

                return Reliably.InvokeAsync(inputAction, n, e, d.Invoke);
            };
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => invoke(a, t, n, e, d).Wait();
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Type> assertError   = (a, t, n, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(a, t, n, e, d), x).Wait();

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_Failure         (assertError  , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_EventualFailure (assertError  , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_RetriesExhausted(assertError  , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
        }

        [TestMethod]
        public async Task InvokeAsync_AsyncAction_Interruptable_DelayHandler()
        {
#nullable disable

            Func<Task> nullAction = null;
            Func<Task> testAction = () => Task.CompletedTask;
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullAction, CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testAction, CancellationToken.None, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, CancellationToken.None, 10, null                                        , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Task> invoke = (a, t, n, e, d) =>
            {
                Func<Task> inputAction = () =>
                {
                    a();
                    return Task.CompletedTask;
                };

                return Reliably.InvokeAsync(inputAction, t, n, e, d.Invoke);
            };
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => invoke(a, t, n, e, d).Wait();
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Type> assertError   = (a, t, n, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(a, t, n, e, d), x).Wait();

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_Failure         (assertError  , t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_EventualFailure (assertError  , t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_RetriesExhausted(assertError  , t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());

            Invoke_Action_Canceled_Delegate(assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_Canceled_Delay   (assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public async Task InvokeAsync_AsyncAction_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Func<Task> nullAction = null;
            Func<Task> testAction = () => Task.CompletedTask;
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullAction, CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testAction, CancellationToken.None, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, CancellationToken.None, 10, null                                        , (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Task> invoke = (a, t, n, e, d) =>
            {
                Func<Task> inputAction = () =>
                {
                    a();
                    return Task.CompletedTask;
                };

                return Reliably.InvokeAsync(inputAction, t, n, e, d.Invoke);
            };
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => invoke(a, t, n, e, d).Wait();
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Type> assertError   = (a, t, n, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(a, t, n, e, d), x).Wait();

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_Failure         (assertError  , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_EventualFailure (assertError  , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_RetriesExhausted(assertError  , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));

            Invoke_Action_Canceled_Delegate(assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_Canceled_Delay   (assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
        }

        [TestMethod]
        public async Task InvokeAsync_AsyncAction_Stateful_DelayHandler()
        {
#nullable disable

            Func<object, Task> nullAction = null;
            Func<object, Task> testAction = s => Task.CompletedTask;
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testAction, new object(), -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, new object(), 10, null                                        , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Task> invoke = (a, t, n, e, d) =>
            {
                object state = new object();
                Func<object, Task> inputAction = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    a();
                    return Task.CompletedTask;
                };

                return Reliably.InvokeAsync(inputAction, state, n, e, d.Invoke);
            };
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => invoke(a, t, n, e, d).Wait();
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Type> assertError   = (a, t, n, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(a, t, n, e, d), x).Wait();

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_Failure         (assertError  , t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_EventualFailure (assertError  , t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_RetriesExhausted(assertError  , t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public async Task InvokeAsync_AsyncAction_Stateful_ComplexDelayHandler()
        {
#nullable disable

            Func<object, Task> nullAction = null;
            Func<object, Task> testAction = s => Task.CompletedTask;
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testAction, new object(), -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, new object(), 10, null                                        , (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Task> invoke = (a, t, n, e, d) =>
            {
                object state = new object();
                Func<object, Task> inputAction = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    a();
                    return Task.CompletedTask;
                };

                return Reliably.InvokeAsync(inputAction, state, n, e, d.Invoke);
            };
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => invoke(a, t, n, e, d).Wait();
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Type> assertError   = (a, t, n, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(a, t, n, e, d), x).Wait();

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_Failure         (assertError  , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_EventualFailure (assertError  , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_RetriesExhausted(assertError  , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
        }

        [TestMethod]
        public async Task InvokeAsync_AsyncAction_Stateful_Interruptable_DelayHandler()
        {
#nullable disable

            Func<object, Task> nullAction = null;
            Func<object, Task> testAction = s => Task.CompletedTask;
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullAction, new object(), CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testAction, new object(), CancellationToken.None, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, new object(), CancellationToken.None, 10, null                                        , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, new object(), CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Task> invoke = (a, t, n, e, d) =>
            {
                object state = new object();
                Func<object, Task> inputAction = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    a();
                    return Task.CompletedTask;
                };

                return Reliably.InvokeAsync(inputAction, state, t, n, e, d.Invoke);
            };
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => invoke(a, t, n, e, d).Wait();
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Type> assertError   = (a, t, n, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(a, t, n, e, d), x).Wait();

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_Failure         (assertError  , t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_EventualFailure (assertError  , t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_RetriesExhausted(assertError  , t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());

            Invoke_Action_Canceled_Delegate(assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_Canceled_Delay   (assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public async Task InvokeAsync_AsyncAction_Stateful_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Func<object, Task> nullAction = null;
            Func<object, Task> testAction = s => Task.CompletedTask;
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullAction, new object(), CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testAction, new object(), CancellationToken.None, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, new object(), CancellationToken.None, 10, null                                        , (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, new object(), CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Task> invoke = (a, t, n, e, d) =>
            {
                object state = new object();
                Func<object, Task> inputAction = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    a();
                    return Task.CompletedTask;
                };

                return Reliably.InvokeAsync(inputAction, state, t, n, e, d.Invoke);
            };
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => invoke(a, t, n, e, d).Wait();
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Type> assertError   = (a, t, n, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(a, t, n, e, d), x).Wait();

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_Failure         (assertError  , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_EventualFailure (assertError  , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_RetriesExhausted(assertError  , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));

            Invoke_Action_Canceled_Delegate(assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_Canceled_Delay   (assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
        }

        #endregion

        #region TryInvokeAsync

        [TestMethod]
        public async Task TryInvokeAsync_AsyncAction_DelayHandler()
        {
#nullable disable

            Func<Task> nullAction = null;
            Func<Task> testAction = () => Task.CompletedTask;
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testAction, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, 10, null                                        , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Task<bool>> tryInvoke = (a, t, n, e, d) =>
            {
                Func<Task> inputAction = () =>
                {
                    a();
                    return Task.CompletedTask;
                };

                return Reliably.TryInvokeAsync(inputAction, n, e, d.Invoke);
            };
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => Assert.IsTrue (tryInvoke(a, t, n, e, d).Result);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Type> assertFailure = (a, t, n, e, d, x) => Assert.IsFalse(tryInvoke(a, t, n, e, d).Result);

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_Failure         (assertFailure, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_EventualFailure (assertFailure, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_RetriesExhausted(assertFailure, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public async Task TryInvokeAsync_AsyncAction_ComplexDelayHandler()
        {
#nullable disable

            Func<Task> nullAction = null;
            Func<Task> testAction = () => Task.CompletedTask;
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testAction, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, 10, null                                        , (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Task<bool>> tryInvoke = (a, t, n, e, d) =>
            {
                Func<Task> inputAction = () =>
                {
                    a();
                    return Task.CompletedTask;
                };

                return Reliably.TryInvokeAsync(inputAction, n, e, d.Invoke);
            };
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => Assert.IsTrue (tryInvoke(a, t, n, e, d).Result);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Type> assertFailure = (a, t, n, e, d, x) => Assert.IsFalse(tryInvoke(a, t, n, e, d).Result);

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_Failure         (assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_EventualFailure (assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_RetriesExhausted(assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
        }

        [TestMethod]
        public async Task TryInvokeAsync_AsyncAction_Interruptable_DelayHandler()
        {
#nullable disable

            Func<Task> nullAction = null;
            Func<Task> testAction = () => Task.CompletedTask;
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullAction, CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testAction, CancellationToken.None, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, CancellationToken.None, 10, null                                        , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Task<bool>> tryInvoke = (a, t, n, e, d) =>
            {
                Func<Task> inputAction = () =>
                {
                    a();
                    return Task.CompletedTask;
                };

                return Reliably.TryInvokeAsync(inputAction, t, n, e, d.Invoke);
            };
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => Assert.IsTrue (tryInvoke(a, t, n, e, d).Result);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Type> assertFailure = (a, t, n, e, d, x) => Assert.IsFalse(tryInvoke(a, t, n, e, d).Result);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Type> assertError   = (a, t, n, e, d, x) => Assert.That.ThrowsExceptionAsync(() => tryInvoke(a, t, n, e, d), x).Wait();

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_Failure         (assertFailure, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_EventualFailure (assertFailure, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_RetriesExhausted(assertFailure, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());

            Invoke_Action_Canceled_Delegate(assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_Canceled_Delay   (assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public async Task TryInvokeAsync_AsyncAction_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Func<Task> nullAction = null;
            Func<Task> testAction = () => Task.CompletedTask;
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullAction, CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testAction, CancellationToken.None, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, CancellationToken.None, 10, null                                        , (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Task<bool>> tryInvoke = (a, t, n, e, d) =>
            {
                Func<Task> inputAction = () =>
                {
                    a();
                    return Task.CompletedTask;
                };

                return Reliably.TryInvokeAsync(inputAction, t, n, e, d.Invoke);
            };
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => Assert.IsTrue (tryInvoke(a, t, n, e, d).Result);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Type> assertFailure = (a, t, n, e, d, x) => Assert.IsFalse(tryInvoke(a, t, n, e, d).Result);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Type> assertError   = (a, t, n, e, d, x) => Assert.That.ThrowsExceptionAsync(() => tryInvoke(a, t, n, e, d), x).Wait();

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_Failure         (assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_EventualFailure (assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_RetriesExhausted(assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));

            Invoke_Action_Canceled_Delegate(assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_Canceled_Delay   (assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
        }

        [TestMethod]
        public async Task TryInvokeAsync_AsyncAction_Stateful_DelayHandler()
        {
#nullable disable

            Func<object, Task> nullAction = null;
            Func<object, Task> testAction = s => Task.CompletedTask;
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testAction, new object(), -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, new object(), 10, null                                        , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Task<bool>> tryInvoke = (a, t, n, e, d) =>
            {
                object state = new object();
                Func<object, Task> inputAction = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    a();
                    return Task.CompletedTask;
                };

                return Reliably.TryInvokeAsync(inputAction, state, n, e, d.Invoke);
            };
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => Assert.IsTrue (tryInvoke(a, t, n, e, d).Result);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Type> assertFailure = (a, t, n, e, d, x) => Assert.IsFalse(tryInvoke(a, t, n, e, d).Result);

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_Failure         (assertFailure, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_EventualFailure (assertFailure, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_RetriesExhausted(assertFailure, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public async Task TryInvokeAsync_AsyncAction_Stateful_ComplexDelayHandler()
        {
#nullable disable

            Func<object, Task> nullAction = null;
            Func<object, Task> testAction = s => Task.CompletedTask;
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testAction, new object(), -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, new object(), 10, null                                        , (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Task<bool>> tryInvoke = (a, t, n, e, d) =>
            {
                object state = new object();
                Func<object, Task> inputAction = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    a();
                    return Task.CompletedTask;
                };

                return Reliably.TryInvokeAsync(inputAction, state, n, e, d.Invoke);
            };
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => Assert.IsTrue (tryInvoke(a, t, n, e, d).Result);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Type> assertFailure = (a, t, n, e, d, x) => Assert.IsFalse(tryInvoke(a, t, n, e, d).Result);

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_Failure         (assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_EventualFailure (assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_RetriesExhausted(assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
        }

        [TestMethod]
        public async Task TryInvokeAsync_AsyncAction_Stateful_Interruptable_DelayHandler()
        {
#nullable disable

            Func<object, Task> nullAction = null;
            Func<object, Task> testAction = s => Task.CompletedTask;
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullAction, new object(), CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testAction, new object(), CancellationToken.None, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, new object(), CancellationToken.None, 10, null                                        , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, new object(), CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Task<bool>> tryInvoke = (a, t, n, e, d) =>
            {
                object state = new object();
                Func<object, Task> inputAction = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    a();
                    return Task.CompletedTask;
                };

                return Reliably.TryInvokeAsync(inputAction, state, t, n, e, d.Invoke);
            };
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => Assert.IsTrue (tryInvoke(a, t, n, e, d).Result);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Type> assertFailure = (a, t, n, e, d, x) => Assert.IsFalse(tryInvoke(a, t, n, e, d).Result);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Type> assertError   = (a, t, n, e, d, x) => Assert.That.ThrowsExceptionAsync(() => tryInvoke(a, t, n, e, d), x).Wait();

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_Failure         (assertFailure, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_EventualFailure (assertFailure, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_RetriesExhausted(assertFailure, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());

            Invoke_Action_Canceled_Delegate(assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_Canceled_Delay   (assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public async Task TryInvokeAsync_AsyncAction_Stateful_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Func<object, Task> nullAction = null;
            Func<object, Task> testAction = s => Task.CompletedTask;
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullAction, new object(), CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testAction, new object(), CancellationToken.None, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, new object(), CancellationToken.None, 10, null                                        , (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, new object(), CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Task<bool>> tryInvoke = (a, t, n, e, d) =>
            {
                object state = new object();
                Func<object, Task> inputAction = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    a();
                    return Task.CompletedTask;
                };

                return Reliably.TryInvokeAsync(inputAction, state, t, n, e, d.Invoke);
            };
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => Assert.IsTrue (tryInvoke(a, t, n, e, d).Result);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Type> assertFailure = (a, t, n, e, d, x) => Assert.IsFalse(tryInvoke(a, t, n, e, d).Result);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Type> assertError   = (a, t, n, e, d, x) => Assert.That.ThrowsExceptionAsync(() => tryInvoke(a, t, n, e, d), x).Wait();

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_Failure         (assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_EventualFailure (assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_RetriesExhausted(assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));

            Invoke_Action_Canceled_Delegate(assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_Canceled_Delay   (assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
        }

        #endregion
    }
}

