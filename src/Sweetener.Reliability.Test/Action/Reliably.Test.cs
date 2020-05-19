// Generated from Reliably.Test.tt
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    partial class ReliablyTest
    {
        #region Invoke

        [TestMethod]
        public void Invoke_Action_DelayHandler()
        {
#nullable disable

            Action nullAction = null;
            Action testAction = Operation.Null;
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testAction, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testAction, 10, null                                        , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null));

#nullable enable

            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>>       invoke      = (a, t, n, e, d)    => Reliably.Invoke(a, n, e, d.Invoke);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Type> assertError = (a, t, n, e, d, x) => Assert.That.ThrowsException(() => invoke(a, t, n, e, d), x);

            Invoke_Action_Success         (invoke     , t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_Failure         (assertError, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_EventualSuccess (invoke     , t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_EventualFailure (assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_RetriesExhausted(assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void Invoke_Action_ComplexDelayHandler()
        {
#nullable disable

            Action nullAction = null;
            Action testAction = Operation.Null;
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testAction, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testAction, 10, null                                        , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null));

#nullable enable

            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>>       invoke      = (a, t, n, e, d)    => Reliably.Invoke(a, n, e, d.Invoke);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Type> assertError = (a, t, n, e, d, x) => Assert.That.ThrowsException(() => invoke(a, t, n, e, d), x);

            Invoke_Action_Success         (invoke     , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_Failure         (assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_EventualSuccess (invoke     , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_EventualFailure (assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_RetriesExhausted(assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
        }

        [TestMethod]
        public void Invoke_Action_Interruptable_DelayHandler()
        {
#nullable disable

            Action nullAction = null;
            Action testAction = Operation.Null;
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullAction, CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testAction, CancellationToken.None, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testAction, CancellationToken.None, 10, null                                        , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testAction, CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null));

#nullable enable

            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>>       invoke      = (a, t, n, e, d)    => Reliably.Invoke(a, t, n, e, d.Invoke);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Type> assertError = (a, t, n, e, d, x) => Assert.That.ThrowsException(() => invoke(a, t, n, e, d), x);

            Invoke_Action_Success         (invoke     , t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_Failure         (assertError, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_EventualSuccess (invoke     , t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_EventualFailure (assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_RetriesExhausted(assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());

            Invoke_Action_Canceled_Delegate(assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_Canceled_Delay   (assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void Invoke_Action_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Action nullAction = null;
            Action testAction = Operation.Null;
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullAction, CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testAction, CancellationToken.None, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testAction, CancellationToken.None, 10, null                                        , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testAction, CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null));

#nullable enable

            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>>       invoke      = (a, t, n, e, d)    => Reliably.Invoke(a, t, n, e, d.Invoke);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Type> assertError = (a, t, n, e, d, x) => Assert.That.ThrowsException(() => invoke(a, t, n, e, d), x);

            Invoke_Action_Success         (invoke     , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_Failure         (assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_EventualSuccess (invoke     , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_EventualFailure (assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_RetriesExhausted(assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));

            Invoke_Action_Canceled_Delegate(assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_Canceled_Delay   (assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
        }

        [TestMethod]
        public void Invoke_Action_Stateful_DelayHandler()
        {
#nullable disable

            Action<object> nullAction = null;
            Action<object> testAction = s => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testAction, new object(), -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testAction, new object(), 10, null                                        , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null));

#nullable enable

            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>> invoke = (a, t, n, e, d) =>
            {
                object state = new object();
                Reliably.Invoke(obj => { Assert.AreSame(obj, state); a(); }, state, n, e, d.Invoke);
            };
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Type> assertError = (a, t, n, e, d, x) => Assert.That.ThrowsException(() => invoke(a, t, n, e, d), x);

            Invoke_Action_Success         (invoke     , t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_Failure         (assertError, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_EventualSuccess (invoke     , t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_EventualFailure (assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_RetriesExhausted(assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void Invoke_Action_Stateful_ComplexDelayHandler()
        {
#nullable disable

            Action<object> nullAction = null;
            Action<object> testAction = s => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testAction, new object(), -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testAction, new object(), 10, null                                        , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null));

#nullable enable

            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>> invoke = (a, t, n, e, d) =>
            {
                object state = new object();
                Reliably.Invoke(obj => { Assert.AreSame(obj, state); a(); }, state, n, e, d.Invoke);
            };
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Type> assertError = (a, t, n, e, d, x) => Assert.That.ThrowsException(() => invoke(a, t, n, e, d), x);

            Invoke_Action_Success         (invoke     , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_Failure         (assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_EventualSuccess (invoke     , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_EventualFailure (assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_RetriesExhausted(assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
        }

        [TestMethod]
        public void Invoke_Action_Stateful_Interruptable_DelayHandler()
        {
#nullable disable

            Action<object> nullAction = null;
            Action<object> testAction = s => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullAction, new object(), CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testAction, new object(), CancellationToken.None, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testAction, new object(), CancellationToken.None, 10, null                                        , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testAction, new object(), CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null));

#nullable enable

            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>> invoke = (a, t, n, e, d) =>
            {
                object state = new object();
                Reliably.Invoke(obj => { Assert.AreSame(obj, state); a(); }, state, t, n, e, d.Invoke);
            };
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Type> assertError = (a, t, n, e, d, x) => Assert.That.ThrowsException(() => invoke(a, t, n, e, d), x);

            Invoke_Action_Success         (invoke     , t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_Failure         (assertError, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_EventualSuccess (invoke     , t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_EventualFailure (assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_RetriesExhausted(assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());

            Invoke_Action_Canceled_Delegate(assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_Canceled_Delay   (assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void Invoke_Action_Stateful_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Action<object> nullAction = null;
            Action<object> testAction = s => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullAction, new object(), CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testAction, new object(), CancellationToken.None, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testAction, new object(), CancellationToken.None, 10, null                                        , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testAction, new object(), CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null));

#nullable enable

            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>> invoke = (a, t, n, e, d) =>
            {
                object state = new object();
                Reliably.Invoke(obj => { Assert.AreSame(obj, state); a(); }, state, t, n, e, d.Invoke);
            };
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Type> assertError = (a, t, n, e, d, x) => Assert.That.ThrowsException(() => invoke(a, t, n, e, d), x);

            Invoke_Action_Success         (invoke     , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_Failure         (assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_EventualSuccess (invoke     , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_EventualFailure (assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_RetriesExhausted(assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));

            Invoke_Action_Canceled_Delegate(assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_Canceled_Delay   (assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
        }

    #endregion

        #region InvokeAsync

        [TestMethod]
        public async Task InvokeAsync_Action_DelayHandler()
        {
#nullable disable

            Action nullAction = null;
            Action testAction = Operation.Null;
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testAction, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, 10, null                                        , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func  <Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Task> invoke        = (a, t, n, e, d)    => Reliably.InvokeAsync(a, n, e, d.Invoke);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => invoke(a, t, n, e, d).Wait();
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Type> assertError   = (a, t, n, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(a, t, n, e, d), x).Wait();

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_Failure         (assertError  , t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_EventualFailure (assertError  , t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_RetriesExhausted(assertError  , t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public async Task InvokeAsync_Action_ComplexDelayHandler()
        {
#nullable disable

            Action nullAction = null;
            Action testAction = Operation.Null;
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testAction, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, 10, null                                        , (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func  <Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Task> invoke        = (a, t, n, e, d)    => Reliably.InvokeAsync(a, n, e, d.Invoke);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => invoke(a, t, n, e, d).Wait();
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Type> assertError   = (a, t, n, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(a, t, n, e, d), x).Wait();

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_Failure         (assertError  , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_EventualFailure (assertError  , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_RetriesExhausted(assertError  , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
        }

        [TestMethod]
        public async Task InvokeAsync_Action_Interruptable_DelayHandler()
        {
#nullable disable

            Action nullAction = null;
            Action testAction = Operation.Null;
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullAction, CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testAction, CancellationToken.None, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, CancellationToken.None, 10, null                                        , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func  <Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Task> invoke        = (a, t, n, e, d)    => Reliably.InvokeAsync(a, t, n, e, d.Invoke);
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
        public async Task InvokeAsync_Action_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Action nullAction = null;
            Action testAction = Operation.Null;
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullAction, CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testAction, CancellationToken.None, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, CancellationToken.None, 10, null                                        , (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func  <Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Task> invoke        = (a, t, n, e, d)    => Reliably.InvokeAsync(a, t, n, e, d.Invoke);
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
        public async Task InvokeAsync_Action_Stateful_DelayHandler()
        {
#nullable disable

            Action<object> nullAction = null;
            Action<object> testAction = s => Operation.Null();
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testAction, new object(), -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, new object(), 10, null                                        , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Task> invoke = (a, t, n, e, d) =>
            {
                object state = new object();
                Action<object> inputAction = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    a();
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
        public async Task InvokeAsync_Action_Stateful_ComplexDelayHandler()
        {
#nullable disable

            Action<object> nullAction = null;
            Action<object> testAction = s => Operation.Null();
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testAction, new object(), -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, new object(), 10, null                                        , (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Task> invoke = (a, t, n, e, d) =>
            {
                object state = new object();
                Action<object> inputAction = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    a();
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
        public async Task InvokeAsync_Action_Stateful_Interruptable_DelayHandler()
        {
#nullable disable

            Action<object> nullAction = null;
            Action<object> testAction = s => Operation.Null();
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullAction, new object(), CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testAction, new object(), CancellationToken.None, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, new object(), CancellationToken.None, 10, null                                        , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, new object(), CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Task> invoke = (a, t, n, e, d) =>
            {
                object state = new object();
                Action<object> inputAction = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    a();
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
        public async Task InvokeAsync_Action_Stateful_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Action<object> nullAction = null;
            Action<object> testAction = s => Operation.Null();
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullAction, new object(), CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testAction, new object(), CancellationToken.None, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, new object(), CancellationToken.None, 10, null                                        , (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testAction, new object(), CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Task> invoke = (a, t, n, e, d) =>
            {
                object state = new object();
                Action<object> inputAction = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    a();
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

        #region TryInvoke

        [TestMethod]
        public void TryInvoke_Action_DelayHandler()
        {
#nullable disable

            Action nullAction = null;
            Action testAction = Operation.Null;
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(nullAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.TryInvoke(testAction, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testAction, 10, null                                        , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null));

#nullable enable

            Func  <Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, bool> tryInvoke = (a, t, n, e, d) => Reliably.TryInvoke(a, n, e, d.Invoke);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => Assert.IsTrue (tryInvoke(a, t, n, e, d));
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Type> assertFailure = (a, t, n, e, d, x) => Assert.IsFalse(tryInvoke(a, t, n, e, d));

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_Failure         (assertFailure, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_EventualFailure (assertFailure, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_RetriesExhausted(assertFailure, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void TryInvoke_Action_ComplexDelayHandler()
        {
#nullable disable

            Action nullAction = null;
            Action testAction = Operation.Null;
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(nullAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.TryInvoke(testAction, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testAction, 10, null                                        , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null));

#nullable enable

            Func  <Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, bool> tryInvoke = (a, t, n, e, d) => Reliably.TryInvoke(a, n, e, d.Invoke);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => Assert.IsTrue (tryInvoke(a, t, n, e, d));
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Type> assertFailure = (a, t, n, e, d, x) => Assert.IsFalse(tryInvoke(a, t, n, e, d));

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_Failure         (assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_EventualFailure (assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_RetriesExhausted(assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
        }

        [TestMethod]
        public void TryInvoke_Action_Interruptable_DelayHandler()
        {
#nullable disable

            Action nullAction = null;
            Action testAction = Operation.Null;
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(nullAction, CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.TryInvoke(testAction, CancellationToken.None, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testAction, CancellationToken.None, 10, null                                        , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testAction, CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null));

#nullable enable

            Func  <Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, bool> tryInvoke = (a, t, n, e, d) => Reliably.TryInvoke(a, t, n, e, d.Invoke);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => Assert.IsTrue (tryInvoke(a, t, n, e, d));
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Type> assertFailure = (a, t, n, e, d, x) => Assert.IsFalse(tryInvoke(a, t, n, e, d));
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Type> assertError   = (a, t, n, e, d, x) => Assert.That.ThrowsException(() => tryInvoke(a, t, n, e, d), x);

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_Failure         (assertFailure, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_EventualFailure (assertFailure, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_RetriesExhausted(assertFailure, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());

            Invoke_Action_Canceled_Delegate(assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_Canceled_Delay   (assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void TryInvoke_Action_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Action nullAction = null;
            Action testAction = Operation.Null;
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(nullAction, CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.TryInvoke(testAction, CancellationToken.None, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testAction, CancellationToken.None, 10, null                                        , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testAction, CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null));

#nullable enable

            Func  <Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, bool> tryInvoke = (a, t, n, e, d) => Reliably.TryInvoke(a, t, n, e, d.Invoke);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => Assert.IsTrue (tryInvoke(a, t, n, e, d));
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Type> assertFailure = (a, t, n, e, d, x) => Assert.IsFalse(tryInvoke(a, t, n, e, d));
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Type> assertError   = (a, t, n, e, d, x) => Assert.That.ThrowsException(() => tryInvoke(a, t, n, e, d), x);

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_Failure         (assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_EventualFailure (assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_RetriesExhausted(assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));

            Invoke_Action_Canceled_Delegate(assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_Canceled_Delay   (assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
        }

        [TestMethod]
        public void TryInvoke_Action_Stateful_DelayHandler()
        {
#nullable disable

            Action<object> nullAction = null;
            Action<object> testAction = s => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(nullAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.TryInvoke(testAction, new object(), -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testAction, new object(), 10, null                                        , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null));

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, bool> tryInvoke = (a, t, n, e, d) =>
            {
                object state = new object();
                return Reliably.TryInvoke(obj => { Assert.AreSame(obj, state); a(); }, state, n, e, d.Invoke);
            };
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => Assert.IsTrue (tryInvoke(a, t, n, e, d));
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Type> assertFailure = (a, t, n, e, d, x) => Assert.IsFalse(tryInvoke(a, t, n, e, d));

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_Failure         (assertFailure, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_EventualFailure (assertFailure, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_RetriesExhausted(assertFailure, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void TryInvoke_Action_Stateful_ComplexDelayHandler()
        {
#nullable disable

            Action<object> nullAction = null;
            Action<object> testAction = s => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(nullAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.TryInvoke(testAction, new object(), -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testAction, new object(), 10, null                                        , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null));

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, bool> tryInvoke = (a, t, n, e, d) =>
            {
                object state = new object();
                return Reliably.TryInvoke(obj => { Assert.AreSame(obj, state); a(); }, state, n, e, d.Invoke);
            };
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => Assert.IsTrue (tryInvoke(a, t, n, e, d));
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Type> assertFailure = (a, t, n, e, d, x) => Assert.IsFalse(tryInvoke(a, t, n, e, d));

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_Failure         (assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_EventualFailure (assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_RetriesExhausted(assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
        }

        [TestMethod]
        public void TryInvoke_Action_Stateful_Interruptable_DelayHandler()
        {
#nullable disable

            Action<object> nullAction = null;
            Action<object> testAction = s => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(nullAction, new object(), CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.TryInvoke(testAction, new object(), CancellationToken.None, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testAction, new object(), CancellationToken.None, 10, null                                        , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testAction, new object(), CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null));

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, bool> tryInvoke = (a, t, n, e, d) =>
            {
                object state = new object();
                return Reliably.TryInvoke(obj => { Assert.AreSame(obj, state); a(); }, state, t, n, e, d.Invoke);
            };
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => Assert.IsTrue (tryInvoke(a, t, n, e, d));
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Type> assertFailure = (a, t, n, e, d, x) => Assert.IsFalse(tryInvoke(a, t, n, e, d));
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Type> assertError   = (a, t, n, e, d, x) => Assert.That.ThrowsException(() => tryInvoke(a, t, n, e, d), x);

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_Failure         (assertFailure, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_EventualFailure (assertFailure, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_RetriesExhausted(assertFailure, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());

            Invoke_Action_Canceled_Delegate(assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_Canceled_Delay   (assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void TryInvoke_Action_Stateful_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Action<object> nullAction = null;
            Action<object> testAction = s => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(nullAction, new object(), CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.TryInvoke(testAction, new object(), CancellationToken.None, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testAction, new object(), CancellationToken.None, 10, null                                        , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testAction, new object(), CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null));

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, bool> tryInvoke = (a, t, n, e, d) =>
            {
                object state = new object();
                return Reliably.TryInvoke(obj => { Assert.AreSame(obj, state); a(); }, state, t, n, e, d.Invoke);
            };
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => Assert.IsTrue (tryInvoke(a, t, n, e, d));
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Type> assertFailure = (a, t, n, e, d, x) => Assert.IsFalse(tryInvoke(a, t, n, e, d));
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Type> assertError   = (a, t, n, e, d, x) => Assert.That.ThrowsException(() => tryInvoke(a, t, n, e, d), x);

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_Failure         (assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_EventualFailure (assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_RetriesExhausted(assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));

            Invoke_Action_Canceled_Delegate(assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_Canceled_Delay   (assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
        }

        #endregion

        #region TryInvokeAsync

        [TestMethod]
        public async Task TryInvokeAsync_Action_DelayHandler()
        {
#nullable disable

            Action nullAction = null;
            Action testAction = Operation.Null;
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testAction, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, 10, null                                        , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func  <Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Task<bool>> tryInvoke = (a, t, n, e, d)    => Reliably.TryInvokeAsync(a, n, e, d.Invoke);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => Assert.IsTrue (tryInvoke(a, t, n, e, d).Result);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Type> assertFailure = (a, t, n, e, d, x) => Assert.IsFalse(tryInvoke(a, t, n, e, d).Result);

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_Failure         (assertFailure, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_EventualFailure (assertFailure, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_RetriesExhausted(assertFailure, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public async Task TryInvokeAsync_Action_ComplexDelayHandler()
        {
#nullable disable

            Action nullAction = null;
            Action testAction = Operation.Null;
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testAction, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, 10, null                                        , (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func  <Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Task<bool>> tryInvoke = (a, t, n, e, d)    => Reliably.TryInvokeAsync(a, n, e, d.Invoke);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>>       assertSuccess = (a, t, n, e, d)    => Assert.IsTrue (tryInvoke(a, t, n, e, d).Result);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Type> assertFailure = (a, t, n, e, d, x) => Assert.IsFalse(tryInvoke(a, t, n, e, d).Result);

            Invoke_Action_Success         (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_Failure         (assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_EventualSuccess (assertSuccess, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_EventualFailure (assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_RetriesExhausted(assertFailure, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
        }

        [TestMethod]
        public async Task TryInvokeAsync_Action_Interruptable_DelayHandler()
        {
#nullable disable

            Action nullAction = null;
            Action testAction = Operation.Null;
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullAction, CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testAction, CancellationToken.None, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, CancellationToken.None, 10, null                                        , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func  <Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Task<bool>> tryInvoke = (a, t, n, e, d)    => Reliably.TryInvokeAsync(a, t, n, e, d.Invoke);
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
        public async Task TryInvokeAsync_Action_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Action nullAction = null;
            Action testAction = Operation.Null;
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullAction, CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testAction, CancellationToken.None, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, CancellationToken.None, 10, null                                        , (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func  <Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Task<bool>> tryInvoke = (a, t, n, e, d)    => Reliably.TryInvokeAsync(a, t, n, e, d.Invoke);
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
        public async Task TryInvokeAsync_Action_Stateful_DelayHandler()
        {
#nullable disable

            Action<object> nullAction = null;
            Action<object> testAction = s => Operation.Null();
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testAction, new object(), -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, new object(), 10, null                                        , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Task<bool>> tryInvoke = (a, t, n, e, d) =>
            {
                object state = new object();
                Action<object> inputAction = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    a();
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
        public async Task TryInvokeAsync_Action_Stateful_ComplexDelayHandler()
        {
#nullable disable

            Action<object> nullAction = null;
            Action<object> testAction = s => Operation.Null();
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testAction, new object(), -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, new object(), 10, null                                        , (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Task<bool>> tryInvoke = (a, t, n, e, d) =>
            {
                object state = new object();
                Action<object> inputAction = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    a();
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
        public async Task TryInvokeAsync_Action_Stateful_Interruptable_DelayHandler()
        {
#nullable disable

            Action<object> nullAction = null;
            Action<object> testAction = s => Operation.Null();
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullAction, new object(), CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testAction, new object(), CancellationToken.None, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, new object(), CancellationToken.None, 10, null                                        , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, new object(), CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Task<bool>> tryInvoke = (a, t, n, e, d) =>
            {
                object state = new object();
                Action<object> inputAction = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    a();
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
        public async Task TryInvokeAsync_Action_Stateful_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Action<object> nullAction = null;
            Action<object> testAction = s => Operation.Null();
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullAction, new object(), CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testAction, new object(), CancellationToken.None, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, new object(), CancellationToken.None, 10, null                                        , (i, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testAction, new object(), CancellationToken.None, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Task<bool>> tryInvoke = (a, t, n, e, d) =>
            {
                object state = new object();
                Action<object> inputAction = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    a();
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

