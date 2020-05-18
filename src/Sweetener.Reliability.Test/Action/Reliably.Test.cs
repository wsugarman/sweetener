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
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testAction, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testAction, 10, null                                        , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null));

#nullable enable

            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>>       invoke      = (a, t, n, e, d)    => Reliably.Invoke(a, t, n, e, d.Invoke);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, TimeSpan>, Type> assertError = (a, t, n, e, d, x) => Assert.That.ThrowsException(() => invoke(a, t, n, e, d), x);
            Invoke_Action_Success         (invoke     , t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_Failure         (assertError, t => new FuncProxy<int, TimeSpan>(i => t),  d     => d.Invoking += Expect.Nothing<int>());
            Invoke_Action_EventualSuccess (invoke     , t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_EventualFailure (assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_RetriesExhausted(assertError, t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());

            Invoke_Action_Canceled_Delegate(invoke     , t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_Canceled_Delay   (invoke     , t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void Invoke_Action_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Action nullAction = null;
            Action testAction = Operation.Null;
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testAction, -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testAction, 10, null                                        , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testAction, 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null));

#nullable enable

            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>>       invoke      = (a, t, n, e, d)    => Reliably.Invoke(a, t, n, e, d.Invoke);
            Action<Action, CancellationToken, int, ExceptionHandler, Func<int, Exception, TimeSpan>, Type> assertError = (a, t, n, e, d, x) => Assert.That.ThrowsException(() => invoke(a, t, n, e, d), x);
            Invoke_Action_Success         (invoke     , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_Failure         (assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t),  d     => d.Invoking += Expect.Nothing<int, Exception>());
            Invoke_Action_EventualSuccess (invoke     , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_EventualFailure (assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_RetriesExhausted(assertError, t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));

            Invoke_Action_Canceled_Delegate(invoke     , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_Canceled_Delay   (invoke     , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
        }

        [TestMethod]
        public void Invoke_Action_Stateful_DelayHandler()
        {
#nullable disable

            Action<object> nullAction = null;
            Action<object> testAction = (s) => Operation.Null();
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
            Action<object> testAction = (s) => Operation.Null();
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
            Action<object> testAction = (s) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testAction, new object(), -3, ExceptionPolicy.Fail<OutOfMemoryException>(), DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testAction, new object(), 10, null                                        , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (DelayHandler)null));

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

            Invoke_Action_Canceled_Delegate(invoke     , t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
            Invoke_Action_Canceled_Delay   (invoke     , t => new FuncProxy<int, TimeSpan>(i => t), (d, t) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void Invoke_Action_Stateful_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Action<object> nullAction = null;
            Action<object> testAction = (s) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testAction, new object(), -3, ExceptionPolicy.Fail<OutOfMemoryException>(), (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testAction, new object(), 10, null                                        , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testAction, new object(), 10, ExceptionPolicy.Fail<OutOfMemoryException>(), (ComplexDelayHandler)null));

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

            Invoke_Action_Canceled_Delegate(invoke     , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
            Invoke_Action_Canceled_Delay   (invoke     , t => new FuncProxy<int, Exception, TimeSpan>((i, e) => t), (d, t) => d.Invoking += Expect.ExceptionAsc(t));
        }

    #endregion

    }
}

