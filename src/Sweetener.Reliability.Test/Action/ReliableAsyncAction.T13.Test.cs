// Generated from ReliableAsyncAction.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    [TestClass]
    public sealed class ReliableAsyncAction13Test : ReliableDelegateTest
    {
        private static readonly Func<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken, Task>> s_getAction = DynamicGetter.ForField<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken, Task>>("_action");

        [TestMethod]
        public void Ctor_DelayHandler()
            => Ctor_DelayHandler((a, m, d, e) => new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(a, m, d, e));

        [TestMethod]
        public void Ctor_ComplexDelayHandler()
            => Ctor_ComplexDelayHandler((a, m, d, e) => new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(a, m, d, e));

        [TestMethod]
        public void Ctor_Interruptable_DelayHandler()
            => Ctor_Interruptable_DelayHandler((a, m, d, e) => new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(a, m, d, e));

        [TestMethod]
        public void Ctor_Interruptable_ComplexDelayHandler()
            => Ctor_Interruptable_ComplexDelayHandler((a, m, d, e) => new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(a, m, d, e));

        [TestMethod]
        public void Create_DelayHandler()
            => Ctor_DelayHandler((a, m, d, e) => ReliableAsyncAction.Create(a, m, d, e));

        [TestMethod]
        public void Create_ComplexDelayHandler()
            => Ctor_ComplexDelayHandler((a, m, d, e) => ReliableAsyncAction.Create(a, m, d, e));

        [TestMethod]
        public void Create_Interruptable_DelayHandler()
            => Ctor_Interruptable_DelayHandler((a, m, d, e) => ReliableAsyncAction.Create(a, m, d, e));

        [TestMethod]
        public void Create_Interruptable_ComplexDelayHandler()
            => Ctor_Interruptable_ComplexDelayHandler((a, m, d, e) => ReliableAsyncAction.Create(a, m, d, e));

        [TestMethod]
        public void InvokeAsync()
            => InvokeAsync(passToken: false);

        [TestMethod]
        public void InvokeAsync_CancellationToken()
            => InvokeAsync(passToken: true);

        #region Ctor

        private void Ctor_DelayHandler(Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, Task>, int, ExceptionHandler, DelayHandler, ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>> factory)
        {
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, Task> action = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, Task>();
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            FuncProxy<int, TimeSpan> delayHandler = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(action.Invoke, -2              , exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action.Invoke, Retries.Infinite, null            , delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action.Invoke, Retries.Infinite, exceptionHandler, null));

            // Create a ReliableAsyncAction and validate
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> actual = factory(action.Invoke, 37, exceptionHandler, delayHandler.Invoke);

            Ctor(actual, 37, exceptionHandler, delayHandler);
            CtorAction(actual, action);
        }

        private void Ctor_ComplexDelayHandler(Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, Task>, int, ExceptionHandler, ComplexDelayHandler, ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>> factory)
        {
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, Task> action = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, Task>();
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            ComplexDelayHandler delayHandler = (i, e) => TimeSpan.FromHours(1);
            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(action.Invoke, -2              , exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action.Invoke, Retries.Infinite, null            , delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action.Invoke, Retries.Infinite, exceptionHandler, null));

            // Create a ReliableAsyncAction and validate
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> actual = factory(action.Invoke, 37, exceptionHandler, delayHandler);

            Ctor(actual, 37, exceptionHandler, delayHandler);
            CtorAction(actual, action);
        }

        private void Ctor_Interruptable_DelayHandler(Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken, Task>, int, ExceptionHandler, DelayHandler, ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>> factory)
        {
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken, Task> action = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => await Task.CompletedTask;
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            FuncProxy<int, TimeSpan> delayHandler = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(action, -2              , exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action, Retries.Infinite, null            , delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action, Retries.Infinite, exceptionHandler, null));

            // Create a ReliableAsyncAction and validate
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> actual = factory(action, 37, exceptionHandler, delayHandler.Invoke);

            Ctor(actual, 37, exceptionHandler, delayHandler);
            CtorAction(actual, action);
        }

        private void Ctor_Interruptable_ComplexDelayHandler(Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken, Task>, int, ExceptionHandler, ComplexDelayHandler, ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>> factory)
        {
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken, Task> action = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => await Task.CompletedTask;
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            ComplexDelayHandler delayHandler = (i, e) => TimeSpan.FromHours(1);
            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(action, -2              , exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action, Retries.Infinite, null            , delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action, Retries.Infinite, exceptionHandler, null));

            // Create a ReliableAsyncAction and validate
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> actual = factory(action, 37, exceptionHandler, delayHandler);

            Ctor(actual, 37, exceptionHandler, delayHandler);
            CtorAction(actual, action);
        }

        private void CtorAction(ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> reliableAction, FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, Task> expected)
            => CtorAction(reliableAction, actual =>
            {
                expected.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(Arguments.Validate);

                Assert.AreEqual(0, expected.Calls);
                actual(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, default);
                Assert.AreEqual(1, expected.Calls);
            });

        private void CtorAction(ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> reliableAction, Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken, Task> expected)
            => CtorAction(reliableAction, (Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken, Task> actual) => Assert.AreSame(expected, actual));

        private void CtorAction(ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> reliableAction, Action<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken, Task>> validateAction)
            => validateAction(s_getAction(reliableAction));

        #endregion

        #region InvokeAsync

        private void InvokeAsync(bool passToken)
        {
            Action<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken> invoke;
            if (passToken)
                invoke = (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, t) => r.InvokeAsync(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, t).Wait();
            else
                invoke = (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, t) => r.InvokeAsync(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13).Wait();

            // Callers may optionally include event handlers
            foreach (bool addEventHandlers in new bool[] { false, true })
            {
                Invoke_Success        (invoke, addEventHandlers);
                Invoke_EventualSuccess(invoke, addEventHandlers);

                Invoke_Failure         ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, t, e) => Assert.That.ThrowsException(() => invoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, t), e), addEventHandlers);
                Invoke_EventualFailure ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, t, e) => Assert.That.ThrowsException(() => invoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, t), e), addEventHandlers);
                Invoke_RetriesExhausted((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, t, e) => Assert.That.ThrowsException(() => invoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, t), e), addEventHandlers);

                if (passToken)
                {
                    Invoke_Canceled_Action(invoke, addEventHandlers, useSynchronousAction: false);
                    Invoke_Canceled_Action(invoke, addEventHandlers, useSynchronousAction: true );
                    Invoke_Canceled_Delay (invoke, addEventHandlers);
                }
            }
        }

        #endregion

        #region Invoke_Success

        private void Invoke_Success(Action<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken> assertInvoke, bool addEventHandlers)
        {
            // Create a "successful" user-defined action
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, Task> action = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, Task>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => await Task.CompletedTask);

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool>          exceptionHandler  = new FuncProxy<Exception, bool>();
            FuncProxy<int, Exception, TimeSpan> delayHandler      = new FuncProxy<int, Exception, TimeSpan>();

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAsyncAction
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> reliableAsyncAction = new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                action.Invoke,
                Retries.Infinite,
                exceptionHandler.Invoke,
                delayHandler    .Invoke);

            if (addEventHandlers)
            {
                reliableAsyncAction.Retrying         += retryHandler    .Invoke;
                reliableAsyncAction.Failed           += failedHandler   .Invoke;
                reliableAsyncAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(Arguments.Validate);
            exceptionHandler.Invoking += Expect.Nothing<Exception>();
            delayHandler    .Invoking += Expect.Nothing<int, Exception>();
            retryHandler    .Invoking += Expect.Nothing<int, Exception>();
            failedHandler   .Invoking += Expect.Nothing<Exception>();
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableAsyncAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, tokenSource.Token);

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

        private void Invoke_Failure(Action<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken, Type> assertInvoke, bool addEventHandlers)
        {
            // Create an "unsuccessful" user-defined action
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, Task> action = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, Task>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => await Task.FromException(new InvalidOperationException()));

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool>          exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Fail<InvalidOperationException>().Invoke);
            FuncProxy<int, Exception, TimeSpan> delayHandler      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAsyncAction
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> reliableAsyncAction = new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                action.Invoke,
                Retries.Infinite,
                exceptionHandler.Invoke,
                delayHandler    .Invoke);

            if (addEventHandlers)
            {
                reliableAsyncAction.Retrying         += retryHandler    .Invoke;
                reliableAsyncAction.Failed           += failedHandler   .Invoke;
                reliableAsyncAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(Arguments.Validate);
            exceptionHandler.Invoking += Expect.Exception(typeof(InvalidOperationException));
            delayHandler    .Invoking += Expect.Nothing<int, Exception>();
            retryHandler    .Invoking += Expect.Nothing<int, Exception>();
            failedHandler   .Invoking += Expect.Exception(typeof(InvalidOperationException));
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableAsyncAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, tokenSource.Token, typeof(InvalidOperationException));

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

        private void Invoke_EventualSuccess(Action<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken> assertInvoke, bool addEventHandlers)
        {
            // Create a "successful" user-defined action that completes after 1 IOException
            Action flakyAction = FlakyAction.Create<IOException>(1);
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, Task> action = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, Task>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => { flakyAction(); await Task.CompletedTask; });

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool>          exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            FuncProxy<int, Exception, TimeSpan> delayHandler      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAsyncAction
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> reliableAsyncAction = new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                action.Invoke,
                Retries.Infinite,
                exceptionHandler.Invoke,
                delayHandler    .Invoke);

            if (addEventHandlers)
            {
                reliableAsyncAction.Retrying         += retryHandler    .Invoke;
                reliableAsyncAction.Failed           += failedHandler   .Invoke;
                reliableAsyncAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(Arguments.Validate, Constants.MinDelay);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            delayHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            retryHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<Exception>();
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableAsyncAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, tokenSource.Token);

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

        private void Invoke_EventualFailure(Action<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken, Type> assertInvoke, bool addEventHandlers)
        {
            // Create an "unsuccessful" user-defined action that fails after 2 transient exceptions
            Action flakyAction = FlakyAction.Create<IOException, InvalidOperationException>(2);
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, Task> action = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, Task>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => { flakyAction(); await Task.CompletedTask; });

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool>          exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            FuncProxy<int, Exception, TimeSpan> delayHandler      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAsyncAction
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> reliableAsyncAction = new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                action.Invoke,
                Retries.Infinite,
                exceptionHandler.Invoke,
                delayHandler    .Invoke);

            if (addEventHandlers)
            {
                reliableAsyncAction.Retrying         += retryHandler    .Invoke;
                reliableAsyncAction.Failed           += failedHandler   .Invoke;
                reliableAsyncAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(Arguments.Validate, Constants.MinDelay);
            exceptionHandler.Invoking += Expect.Exceptions(typeof(IOException), typeof(InvalidOperationException), 2);
            delayHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            retryHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            failedHandler   .Invoking += Expect.Exception(typeof(InvalidOperationException));
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableAsyncAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, tokenSource.Token, typeof(InvalidOperationException));

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

        private void Invoke_RetriesExhausted(Action<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken, Type> assertInvoke, bool addEventHandlers)
        {
            // Create an "unsuccessful" user-defined action that exhausts the configured number of retries
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, Task> action = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, Task>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => await Task.FromException(new IOException()));

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool>          exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            FuncProxy<int, Exception, TimeSpan> delayHandler      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAsyncAction
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> reliableAsyncAction = new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                action.Invoke,
                2,
                exceptionHandler.Invoke,
                delayHandler    .Invoke);

            if (addEventHandlers)
            {
                reliableAsyncAction.Retrying         += retryHandler    .Invoke;
                reliableAsyncAction.Failed           += failedHandler   .Invoke;
                reliableAsyncAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(Arguments.Validate, Constants.MinDelay);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            delayHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            retryHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<Exception>();
            exhaustedHandler.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableAsyncAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, tokenSource.Token, typeof(IOException));

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

        private void Invoke_Canceled_Action(Action<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken> invoke, bool addEventHandlers, bool useSynchronousAction)
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create a user-defined action that will throw an exception depending on whether its canceled
            // Note: We need to separately check the use of asynchronous and synchronous methods when checking cancellation
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken, Task> action = useSynchronousAction
                ? new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken, Task>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) =>
                    {
                        token.ThrowIfCancellationRequested();
                        throw new IOException();
                    })
                : new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken, Task>(
                    async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) =>
                    {
                        await Task.CompletedTask;
                        token.ThrowIfCancellationRequested();
                        throw new IOException();
                    });

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool>          exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Transient.Invoke);
            FuncProxy<int, Exception, TimeSpan> delayHandler      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAsyncAction
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> reliableAsyncAction = new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                action.Invoke,
                Retries.Infinite,
                exceptionHandler.Invoke,
                delayHandler    .Invoke);

            if (addEventHandlers)
            {
                reliableAsyncAction.Retrying         += retryHandler    .Invoke;
                reliableAsyncAction.Failed           += failedHandler   .Invoke;
                reliableAsyncAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            delayHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            retryHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<Exception>();
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Cancel the action on its 2nd attempt
            action          .Invoking += (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, t, c) =>
            {
                if (c.Calls == 2)
                    tokenSource.Cancel();
            };

            // Invoke, retry, and cancel
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableAsyncAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, tokenSource.Token), allowedDerivedTypes: true);

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

        private void Invoke_Canceled_Delay(Action<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken> invoke, bool addEventHandlers)
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined action that continues to fail with transient exceptions until it's canceled
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, Task> action = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, Task>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => await Task.FromException(new IOException()));

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool>          exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Transient.Invoke);
            FuncProxy<int, Exception, TimeSpan> delayHandler      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAsyncAction
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> reliableAsyncAction = new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                action.Invoke,
                Retries.Infinite,
                exceptionHandler.Invoke,
                delayHandler    .Invoke);

            if (addEventHandlers)
            {
                reliableAsyncAction.Retrying         += retryHandler    .Invoke;
                reliableAsyncAction.Failed           += failedHandler   .Invoke;
                reliableAsyncAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(Arguments.Validate, Constants.MinDelay);
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
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableAsyncAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, tokenSource.Token), allowedDerivedTypes: true);

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
