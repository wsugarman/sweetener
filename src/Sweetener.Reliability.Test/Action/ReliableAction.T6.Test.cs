// Generated from ReliableAction.Test.tt
using System;
using System.IO;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweetener.Reflection;

namespace Sweetener.Reliability.Test
{
    [TestClass]
    public sealed class ReliableAction6Test : ReliableDelegateTest
    {
        private static readonly Func<ReliableAction<int, string, double, long, ushort, byte>, Action<int, string, double, long, ushort, byte, CancellationToken>> s_getAction = DynamicGetter.ForField<ReliableAction<int, string, double, long, ushort, byte>, Action<int, string, double, long, ushort, byte, CancellationToken>>("_action");

        [TestMethod]
        public void Ctor_DelayHandler()
            => Ctor_DelayHandler((a, m, d, e) => new ReliableAction<int, string, double, long, ushort, byte>(a, m, d, e));

        [TestMethod]
        public void Ctor_ComplexDelayHandler()
            => Ctor_ComplexDelayHandler((a, m, d, e) => new ReliableAction<int, string, double, long, ushort, byte>(a, m, d, e));

        [TestMethod]
        public void Ctor_Interruptable_DelayHandler()
            => Ctor_Interruptable_DelayHandler((a, m, d, e) => new ReliableAction<int, string, double, long, ushort, byte>(a, m, d, e));

        [TestMethod]
        public void Ctor_Interruptable_ComplexDelayHandler()
            => Ctor_Interruptable_ComplexDelayHandler((a, m, d, e) => new ReliableAction<int, string, double, long, ushort, byte>(a, m, d, e));

        [TestMethod]
        public void Create_DelayHandler()
            => Ctor_DelayHandler((a, m, d, e) => ReliableAction.Create(a, m, d, e));

        [TestMethod]
        public void Create_ComplexDelayHandler()
            => Ctor_ComplexDelayHandler((a, m, d, e) => ReliableAction.Create(a, m, d, e));

        [TestMethod]
        public void Create_Interruptable_DelayHandler()
            => Ctor_Interruptable_DelayHandler((a, m, d, e) => ReliableAction.Create(a, m, d, e));

        [TestMethod]
        public void Create_Interruptable_ComplexDelayHandler()
            => Ctor_Interruptable_ComplexDelayHandler((a, m, d, e) => ReliableAction.Create(a, m, d, e));

        [TestMethod]
        public void Invoke()
            => Invoke(passToken: false);

        [TestMethod]
        public void Invoke_CancellationToken()
            => Invoke(passToken: true);

        [TestMethod]
        public void InvokeAsync()
            => InvokeAsync(passToken: false);

        [TestMethod]
        public void InvokeAsync_CancellationToken()
            => InvokeAsync(passToken: true);

        [TestMethod]
        public void TryInvoke()
            => TryInvoke(passToken: false);

        [TestMethod]
        public void TryInvoke_CancellationToken()
            => TryInvoke(passToken: true);

        [TestMethod]
        public void TryInvokeAsync()
            => TryInvokeAsync(passToken: false);

        [TestMethod]
        public void TryInvokeAsync_CancellationToken()
            => TryInvokeAsync(passToken: true);

        #region Ctor

        private void Ctor_DelayHandler(Func<Action<int, string, double, long, ushort, byte>, int, ExceptionHandler, DelayHandler, ReliableAction<int, string, double, long, ushort, byte>> factory)
        {
            ActionProxy<int, string, double, long, ushort, byte> action = new ActionProxy<int, string, double, long, ushort, byte>();
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            FuncProxy<int, TimeSpan> delayHandler = new FuncProxy<int, TimeSpan>(i => Constants.Delay);

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(action.Invoke, -2              , exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action.Invoke, Retries.Infinite, null            , delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action.Invoke, Retries.Infinite, exceptionHandler, null));

#nullable enable

            // Create a ReliableAction and validate
            ReliableAction<int, string, double, long, ushort, byte> actual = factory(action.Invoke, 37, exceptionHandler, delayHandler.Invoke);

            Ctor(actual, 37, exceptionHandler, delayHandler);
            CtorAction(actual, action);
        }

        private void Ctor_ComplexDelayHandler(Func<Action<int, string, double, long, ushort, byte>, int, ExceptionHandler, ComplexDelayHandler, ReliableAction<int, string, double, long, ushort, byte>> factory)
        {
            ActionProxy<int, string, double, long, ushort, byte> action = new ActionProxy<int, string, double, long, ushort, byte>();
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            ComplexDelayHandler delayHandler = (i, e) => TimeSpan.FromHours(1);

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(action.Invoke, -2              , exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action.Invoke, Retries.Infinite, null            , delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action.Invoke, Retries.Infinite, exceptionHandler, null));

#nullable enable

            // Create a ReliableAction and validate
            ReliableAction<int, string, double, long, ushort, byte> actual = factory(action.Invoke, 37, exceptionHandler, delayHandler);

            Ctor(actual, 37, exceptionHandler, delayHandler);
            CtorAction(actual, action);
        }

        private void Ctor_Interruptable_DelayHandler(Func<Action<int, string, double, long, ushort, byte, CancellationToken>, int, ExceptionHandler, DelayHandler, ReliableAction<int, string, double, long, ushort, byte>> factory)
        {
            Action<int, string, double, long, ushort, byte, CancellationToken> action = (arg1, arg2, arg3, arg4, arg5, arg6, token) => Operation.Null();
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            FuncProxy<int, TimeSpan> delayHandler = new FuncProxy<int, TimeSpan>(i => Constants.Delay);

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(action, -2              , exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action, Retries.Infinite, null            , delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action, Retries.Infinite, exceptionHandler, null));

#nullable enable

            // Create a ReliableAction and validate
            ReliableAction<int, string, double, long, ushort, byte> actual = factory(action, 37, exceptionHandler, delayHandler.Invoke);

            Ctor(actual, 37, exceptionHandler, delayHandler);
            CtorAction(actual, action);
        }

        private void Ctor_Interruptable_ComplexDelayHandler(Func<Action<int, string, double, long, ushort, byte, CancellationToken>, int, ExceptionHandler, ComplexDelayHandler, ReliableAction<int, string, double, long, ushort, byte>> factory)
        {
            Action<int, string, double, long, ushort, byte, CancellationToken> action = (arg1, arg2, arg3, arg4, arg5, arg6, token) => Operation.Null();
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            ComplexDelayHandler delayHandler = (i, e) => TimeSpan.FromHours(1);

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(action, -2              , exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action, Retries.Infinite, null            , delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action, Retries.Infinite, exceptionHandler, null));

#nullable enable

            // Create a ReliableAction and validate
            ReliableAction<int, string, double, long, ushort, byte> actual = factory(action, 37, exceptionHandler, delayHandler);

            Ctor(actual, 37, exceptionHandler, delayHandler);
            CtorAction(actual, action);
        }

        private void CtorAction(ReliableAction<int, string, double, long, ushort, byte> reliableAction, ActionProxy<int, string, double, long, ushort, byte> expected)
            => CtorAction(reliableAction, actual =>
            {
                expected.Invoking += Expect.Arguments<int, string, double, long, ushort, byte>(Arguments.Validate);

                Assert.AreEqual(0, expected.Calls);
                actual(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, default);
                Assert.AreEqual(1, expected.Calls);
            });

        private void CtorAction(ReliableAction<int, string, double, long, ushort, byte> reliableAction, Action<int, string, double, long, ushort, byte, CancellationToken> expected)
            => CtorAction(reliableAction, (Action<int, string, double, long, ushort, byte, CancellationToken> actual) => Assert.AreSame(expected, actual));

        private void CtorAction(ReliableAction<int, string, double, long, ushort, byte> reliableAction, Action<Action<int, string, double, long, ushort, byte, CancellationToken>> validateAction)
            => validateAction(s_getAction(reliableAction));

        #endregion

        #region Invoke

        private void Invoke(bool passToken)
        {
            Action<ReliableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken> invoke;
            if (passToken)
                invoke = (r, arg1, arg2, arg3, arg4, arg5, arg6, t) => r.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, t);
            else
                invoke = (r, arg1, arg2, arg3, arg4, arg5, arg6, t) => r.Invoke(arg1, arg2, arg3, arg4, arg5, arg6);

            // Callers may optionally include event handlers
            foreach (bool addEventHandlers in new bool[] { false, true })
            {
                Invoke_Success        (invoke, addEventHandlers);
                Invoke_EventualSuccess(invoke, addEventHandlers);

                Invoke_Failure         ((r, arg1, arg2, arg3, arg4, arg5, arg6, t, e) => Assert.That.ThrowsException(() => invoke(r, arg1, arg2, arg3, arg4, arg5, arg6, t), e), addEventHandlers);
                Invoke_EventualFailure ((r, arg1, arg2, arg3, arg4, arg5, arg6, t, e) => Assert.That.ThrowsException(() => invoke(r, arg1, arg2, arg3, arg4, arg5, arg6, t), e), addEventHandlers);
                Invoke_RetriesExhausted((r, arg1, arg2, arg3, arg4, arg5, arg6, t, e) => Assert.That.ThrowsException(() => invoke(r, arg1, arg2, arg3, arg4, arg5, arg6, t), e), addEventHandlers);

                if (passToken)
                {
                    Invoke_Canceled_Action(invoke, addEventHandlers);
                    Invoke_Canceled_Delay (invoke, addEventHandlers);
                }
            }
        }

        #endregion

        #region InvokeAsync

        private void InvokeAsync(bool passToken)
        {
            Action<ReliableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken> invokeAsync;
            if (passToken)
                invokeAsync = (r, arg1, arg2, arg3, arg4, arg5, arg6, t) => r.InvokeAsync(arg1, arg2, arg3, arg4, arg5, arg6, t).Wait();
            else
                invokeAsync = (r, arg1, arg2, arg3, arg4, arg5, arg6, t) => r.InvokeAsync(arg1, arg2, arg3, arg4, arg5, arg6).Wait();

            // Callers may optionally include event handlers
            foreach (bool addEventHandlers in new bool[] { false, true })
            {
                Invoke_Success        (invokeAsync, addEventHandlers);
                Invoke_EventualSuccess(invokeAsync, addEventHandlers);

                Invoke_Failure         ((r, arg1, arg2, arg3, arg4, arg5, arg6, t, e) => Assert.That.ThrowsException(() => invokeAsync(r, arg1, arg2, arg3, arg4, arg5, arg6, t), e), addEventHandlers);
                Invoke_EventualFailure ((r, arg1, arg2, arg3, arg4, arg5, arg6, t, e) => Assert.That.ThrowsException(() => invokeAsync(r, arg1, arg2, arg3, arg4, arg5, arg6, t), e), addEventHandlers);
                Invoke_RetriesExhausted((r, arg1, arg2, arg3, arg4, arg5, arg6, t, e) => Assert.That.ThrowsException(() => invokeAsync(r, arg1, arg2, arg3, arg4, arg5, arg6, t), e), addEventHandlers);

                if (passToken)
                {
                    Invoke_Canceled_Action(invokeAsync, addEventHandlers);
                    Invoke_Canceled_Delay (invokeAsync, addEventHandlers);
                }
            }
        }

        #endregion

        #region TryInvoke

        private void TryInvoke(bool passToken)
        {
            Func<ReliableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken, bool> tryInvoke;
            if (passToken)
                tryInvoke = (r, arg1, arg2, arg3, arg4, arg5, arg6, t) => r.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, t);
            else
                tryInvoke = (r, arg1, arg2, arg3, arg4, arg5, arg6, t) => r.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6);

            // Callers may optionally include event handlers
            foreach (bool addEventHandlers in new bool[] { false, true })
            {
                Invoke_Success         ((r, arg1, arg2, arg3, arg4, arg5, arg6, t   ) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, t)), addEventHandlers);
                Invoke_EventualSuccess ((r, arg1, arg2, arg3, arg4, arg5, arg6, t   ) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, t)), addEventHandlers);
                Invoke_Failure         ((r, arg1, arg2, arg3, arg4, arg5, arg6, t, e) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, t)), addEventHandlers);
                Invoke_EventualFailure ((r, arg1, arg2, arg3, arg4, arg5, arg6, t, e) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, t)), addEventHandlers);
                Invoke_RetriesExhausted((r, arg1, arg2, arg3, arg4, arg5, arg6, t, e) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, t)), addEventHandlers);

                if (passToken)
                {
                    Invoke_Canceled_Action((r, arg1, arg2, arg3, arg4, arg5, arg6, t) => r.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, t), addEventHandlers);
                    Invoke_Canceled_Delay ((r, arg1, arg2, arg3, arg4, arg5, arg6, t) => r.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, t), addEventHandlers);
                }
            }
        }

        #endregion

        #region TryInvokeAsync

        private void TryInvokeAsync(bool passToken)
        {
            Func<ReliableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken, bool> tryInvokeAsync;
            if (passToken)
                tryInvokeAsync = (r, arg1, arg2, arg3, arg4, arg5, arg6, t) => r.TryInvokeAsync(arg1, arg2, arg3, arg4, arg5, arg6, t).Result;
            else
                tryInvokeAsync = (r, arg1, arg2, arg3, arg4, arg5, arg6, t) => r.TryInvokeAsync(arg1, arg2, arg3, arg4, arg5, arg6).Result;

            // Callers may optionally include event handlers
            foreach (bool addEventHandlers in new bool[] { false, true })
            {
                Invoke_Success         ((r, arg1, arg2, arg3, arg4, arg5, arg6, t   ) => Assert.IsTrue (tryInvokeAsync(r, arg1, arg2, arg3, arg4, arg5, arg6, t)), addEventHandlers);
                Invoke_EventualSuccess ((r, arg1, arg2, arg3, arg4, arg5, arg6, t   ) => Assert.IsTrue (tryInvokeAsync(r, arg1, arg2, arg3, arg4, arg5, arg6, t)), addEventHandlers);
                Invoke_Failure         ((r, arg1, arg2, arg3, arg4, arg5, arg6, t, e) => Assert.IsFalse(tryInvokeAsync(r, arg1, arg2, arg3, arg4, arg5, arg6, t)), addEventHandlers);
                Invoke_EventualFailure ((r, arg1, arg2, arg3, arg4, arg5, arg6, t, e) => Assert.IsFalse(tryInvokeAsync(r, arg1, arg2, arg3, arg4, arg5, arg6, t)), addEventHandlers);
                Invoke_RetriesExhausted((r, arg1, arg2, arg3, arg4, arg5, arg6, t, e) => Assert.IsFalse(tryInvokeAsync(r, arg1, arg2, arg3, arg4, arg5, arg6, t)), addEventHandlers);

                if (passToken)
                {
                    Invoke_Canceled_Action((r, arg1, arg2, arg3, arg4, arg5, arg6, t) => r.TryInvokeAsync(arg1, arg2, arg3, arg4, arg5, arg6, t).Wait(), addEventHandlers);
                    Invoke_Canceled_Delay ((r, arg1, arg2, arg3, arg4, arg5, arg6, t) => r.TryInvokeAsync(arg1, arg2, arg3, arg4, arg5, arg6, t).Wait(), addEventHandlers);
                }
            }
        }

        #endregion

        #region Invoke_Success

        private void Invoke_Success(Action<ReliableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken> assertInvoke, bool addEventHandlers)
        {
            // Create a "successful" user-defined action
            ActionProxy<int, string, double, long, ushort, byte> action = new ActionProxy<int, string, double, long, ushort, byte>((arg1, arg2, arg3, arg4, arg5, arg6) => Operation.Null());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool>          exceptionHandler  = FuncProxy<Exception, bool>.Unused;
            FuncProxy<int, Exception, TimeSpan> delayHandler      = FuncProxy<int, Exception, TimeSpan>.Unused;

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAction
            ReliableAction<int, string, double, long, ushort, byte> reliableAction = new ReliableAction<int, string, double, long, ushort, byte>(
                action.Invoke,
                Retries.Infinite,
                exceptionHandler.Invoke,
                delayHandler    .Invoke);

            if (addEventHandlers)
            {
                reliableAction.Retrying         += retryHandler    .Invoke;
                reliableAction.Failed           += failedHandler   .Invoke;
                reliableAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.Arguments<int, string, double, long, ushort, byte>(Arguments.Validate);
            exceptionHandler.Invoking += Expect.Nothing<Exception>();
            delayHandler    .Invoking += Expect.Nothing<int, Exception>();
            retryHandler    .Invoking += Expect.Nothing<int, Exception>();
            failedHandler   .Invoking += Expect.Nothing<Exception>();
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(1, action          .Calls);
            Assert.AreEqual(0, exceptionHandler.Calls);
            Assert.AreEqual(0, delayHandler    .Calls);
            Assert.AreEqual(0, retryHandler    .Calls);
            Assert.AreEqual(0, failedHandler   .Calls);
            Assert.AreEqual(0, exhaustedHandler.Calls);
        }

        #endregion

        #region Invoke_Failure

        private void Invoke_Failure(Action<ReliableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken, Type> assertInvoke, bool addEventHandlers)
        {
            // Create an "unsuccessful" user-defined action
            ActionProxy<int, string, double, long, ushort, byte> action = new ActionProxy<int, string, double, long, ushort, byte>((arg1, arg2, arg3, arg4, arg5, arg6) => throw new OutOfMemoryException());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool>          exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Fail<OutOfMemoryException>().Invoke);
            FuncProxy<int, Exception, TimeSpan> delayHandler      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAction
            ReliableAction<int, string, double, long, ushort, byte> reliableAction = new ReliableAction<int, string, double, long, ushort, byte>(
                action.Invoke,
                Retries.Infinite,
                exceptionHandler.Invoke,
                delayHandler    .Invoke);

            if (addEventHandlers)
            {
                reliableAction.Retrying         += retryHandler    .Invoke;
                reliableAction.Failed           += failedHandler   .Invoke;
                reliableAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.Arguments<int, string, double, long, ushort, byte>(Arguments.Validate);
            exceptionHandler.Invoking += Expect.Exception(typeof(OutOfMemoryException));
            delayHandler    .Invoking += Expect.Nothing<int, Exception>();
            retryHandler    .Invoking += Expect.Nothing<int, Exception>();
            failedHandler   .Invoking += Expect.Exception(typeof(OutOfMemoryException));
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, tokenSource.Token, typeof(OutOfMemoryException));

            // Validate the number of calls
            Assert.AreEqual(1, action          .Calls);
            Assert.AreEqual(1, exceptionHandler.Calls);
            Assert.AreEqual(0, delayHandler    .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(0, retryHandler    .Calls);
                Assert.AreEqual(1, failedHandler   .Calls);
                Assert.AreEqual(0, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_EventualSuccess

        private void Invoke_EventualSuccess(Action<ReliableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken> assertInvoke, bool addEventHandlers)
        {
            // Create a "successful" user-defined action that completes after 1 IOException
            Action flakyAction = FlakyAction.Create<IOException>(1);
            ActionProxy<int, string, double, long, ushort, byte> action = new ActionProxy<int, string, double, long, ushort, byte>((arg1, arg2, arg3, arg4, arg5, arg6) => flakyAction());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool>          exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            FuncProxy<int, Exception, TimeSpan> delayHandler      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAction
            ReliableAction<int, string, double, long, ushort, byte> reliableAction = new ReliableAction<int, string, double, long, ushort, byte>(
                action.Invoke,
                Retries.Infinite,
                exceptionHandler.Invoke,
                delayHandler    .Invoke);

            if (addEventHandlers)
            {
                reliableAction.Retrying         += retryHandler    .Invoke;
                reliableAction.Failed           += failedHandler   .Invoke;
                reliableAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte>(Arguments.Validate, Constants.MinDelay);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            delayHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            retryHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<Exception>();
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(2, action          .Calls);
            Assert.AreEqual(1, exceptionHandler.Calls);
            Assert.AreEqual(1, delayHandler    .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(1, retryHandler    .Calls);
                Assert.AreEqual(0, failedHandler   .Calls);
                Assert.AreEqual(0, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_EventualFailure

        private void Invoke_EventualFailure(Action<ReliableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken, Type> assertInvoke, bool addEventHandlers)
        {
            // Create an "unsuccessful" user-defined action that fails after 2 transient exceptions
            Action flakyAction = FlakyAction.Create<IOException, OutOfMemoryException>(2);
            ActionProxy<int, string, double, long, ushort, byte> action = new ActionProxy<int, string, double, long, ushort, byte>((arg1, arg2, arg3, arg4, arg5, arg6) => flakyAction());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool>          exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            FuncProxy<int, Exception, TimeSpan> delayHandler      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAction
            ReliableAction<int, string, double, long, ushort, byte> reliableAction = new ReliableAction<int, string, double, long, ushort, byte>(
                action.Invoke,
                Retries.Infinite,
                exceptionHandler.Invoke,
                delayHandler    .Invoke);

            if (addEventHandlers)
            {
                reliableAction.Retrying         += retryHandler    .Invoke;
                reliableAction.Failed           += failedHandler   .Invoke;
                reliableAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte>(Arguments.Validate, Constants.MinDelay);
            exceptionHandler.Invoking += Expect.Exceptions(typeof(IOException), typeof(OutOfMemoryException), 2);
            delayHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            retryHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            failedHandler   .Invoking += Expect.Exception(typeof(OutOfMemoryException));
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, tokenSource.Token, typeof(OutOfMemoryException));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionHandler.Calls);
            Assert.AreEqual(2, delayHandler    .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(2, retryHandler    .Calls);
                Assert.AreEqual(1, failedHandler   .Calls);
                Assert.AreEqual(0, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_RetriesExhausted

        private void Invoke_RetriesExhausted(Action<ReliableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken, Type> assertInvoke, bool addEventHandlers)
        {
            // Create an "unsuccessful" user-defined action that exhausts the configured number of retries
            ActionProxy<int, string, double, long, ushort, byte> action = new ActionProxy<int, string, double, long, ushort, byte>((arg1, arg2, arg3, arg4, arg5, arg6) => throw new IOException());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool>          exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            FuncProxy<int, Exception, TimeSpan> delayHandler      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAction
            ReliableAction<int, string, double, long, ushort, byte> reliableAction = new ReliableAction<int, string, double, long, ushort, byte>(
                action.Invoke,
                2,
                exceptionHandler.Invoke,
                delayHandler    .Invoke);

            if (addEventHandlers)
            {
                reliableAction.Retrying         += retryHandler    .Invoke;
                reliableAction.Failed           += failedHandler   .Invoke;
                reliableAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte>(Arguments.Validate, Constants.MinDelay);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            delayHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            retryHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<Exception>();
            exhaustedHandler.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, tokenSource.Token, typeof(IOException));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionHandler.Calls);
            Assert.AreEqual(2, delayHandler    .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(2, retryHandler    .Calls);
                Assert.AreEqual(0, failedHandler   .Calls);
                Assert.AreEqual(1, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_Canceled_Action

        private void Invoke_Canceled_Action(Action<ReliableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken> invoke, bool addEventHandlers)
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create a user-defined action that will throw an exception depending on whether its canceled
            ActionProxy<int, string, double, long, ushort, byte, CancellationToken> action = new ActionProxy<int, string, double, long, ushort, byte, CancellationToken>(
                (arg1, arg2, arg3, arg4, arg5, arg6, token) =>
                {
                    token.ThrowIfCancellationRequested();
                    throw new IOException();
                });

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool>          exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Transient.Invoke);
            FuncProxy<int, Exception, TimeSpan> delayHandler      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAction
            ReliableAction<int, string, double, long, ushort, byte> reliableAction = new ReliableAction<int, string, double, long, ushort, byte>(
                action.Invoke,
                Retries.Infinite,
                exceptionHandler.Invoke,
                delayHandler    .Invoke);

            if (addEventHandlers)
            {
                reliableAction.Retrying         += retryHandler    .Invoke;
                reliableAction.Failed           += failedHandler   .Invoke;
                reliableAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            delayHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            retryHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<Exception>();
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Cancel the action on its 2nd attempt
            action          .Invoking += (arg1, arg2, arg3, arg4, arg5, arg6, t, c) =>
            {
                if (c.Calls == 2)
                    tokenSource.Cancel();
            };

            // Invoke, retry, and cancel
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            Assert.AreEqual(2, action          .Calls);
            Assert.AreEqual(1, exceptionHandler.Calls);
            Assert.AreEqual(1, delayHandler    .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(1, retryHandler    .Calls);
                Assert.AreEqual(0, failedHandler   .Calls);
                Assert.AreEqual(0, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_Canceled_Delay

        private void Invoke_Canceled_Delay(Action<ReliableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken> invoke, bool addEventHandlers)
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined action that continues to fail with transient exceptions until it's canceled
            ActionProxy<int, string, double, long, ushort, byte> action = new ActionProxy<int, string, double, long, ushort, byte>((arg1, arg2, arg3, arg4, arg5, arg6) => throw new IOException());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool>          exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Transient.Invoke);
            FuncProxy<int, Exception, TimeSpan> delayHandler      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAction
            ReliableAction<int, string, double, long, ushort, byte> reliableAction = new ReliableAction<int, string, double, long, ushort, byte>(
                action.Invoke,
                Retries.Infinite,
                exceptionHandler.Invoke,
                delayHandler    .Invoke);

            if (addEventHandlers)
            {
                reliableAction.Retrying         += retryHandler    .Invoke;
                reliableAction.Failed           += failedHandler   .Invoke;
                reliableAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte>(Arguments.Validate, Constants.MinDelay);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            delayHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            retryHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<Exception>();
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Cancel the delay on its 2nd invocation
            delayHandler    .Invoking += (i, e, c) =>
            {
                if (c.Calls == 2)
                    tokenSource.Cancel();
            };

            // Invoke, retry, and cancel
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            Assert.AreEqual(2, action          .Calls);
            Assert.AreEqual(2, exceptionHandler.Calls);
            Assert.AreEqual(2, delayHandler    .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(1, retryHandler    .Calls);
                Assert.AreEqual(0, failedHandler   .Calls);
                Assert.AreEqual(0, exhaustedHandler.Calls);
            }
        }

        #endregion
    }
}
