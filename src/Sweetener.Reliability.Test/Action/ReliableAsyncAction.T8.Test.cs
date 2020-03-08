// Generated from ReliableAsyncAction.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweetener.Reflection;

namespace Sweetener.Reliability.Test
{
    [TestClass]
    public sealed class ReliableAsyncAction8Test : ReliableDelegateTest
    {
        private static readonly Func<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>, Func<int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, Task>> s_getAction = DynamicGetter.ForField<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>, Func<int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, Task>>("_action");

        [TestMethod]
        public void Ctor_DelayHandler()
            => Ctor_DelayHandler((a, m, d, e) => new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>(a, m, d, e));

        [TestMethod]
        public void Ctor_ComplexDelayHandler()
            => Ctor_ComplexDelayHandler((a, m, d, e) => new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>(a, m, d, e));

        [TestMethod]
        public void Ctor_Interruptable_DelayHandler()
            => Ctor_Interruptable_DelayHandler((a, m, d, e) => new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>(a, m, d, e));

        [TestMethod]
        public void Ctor_Interruptable_ComplexDelayHandler()
            => Ctor_Interruptable_ComplexDelayHandler((a, m, d, e) => new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>(a, m, d, e));

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

        [TestMethod]
        public void TryInvokeAsync()
            => TryInvokeAsync(passToken: false);

        [TestMethod]
        public void TryInvokeAsync_CancellationToken()
            => TryInvokeAsync(passToken: true);

        #region Ctor

        private void Ctor_DelayHandler(Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Task>, int, ExceptionHandler, DelayHandler, ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>> factory)
        {
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Task> action = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Task>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => await Task.CompletedTask);
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            FuncProxy<int, TimeSpan> delayHandler = new FuncProxy<int, TimeSpan>(i => Constants.Delay);

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(action.Invoke, -2              , exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action.Invoke, Retries.Infinite, null            , delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action.Invoke, Retries.Infinite, exceptionHandler, null));

#nullable enable

            // Create a ReliableAsyncAction and validate
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint> actual = factory(action.Invoke, 37, exceptionHandler, delayHandler.Invoke);

            Ctor(actual, 37, exceptionHandler, delayHandler);
            CtorAction(actual, action);
        }

        private void Ctor_ComplexDelayHandler(Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Task>, int, ExceptionHandler, ComplexDelayHandler, ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>> factory)
        {
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Task> action = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Task>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => await Task.CompletedTask);
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            ComplexDelayHandler delayHandler = (i, e) => TimeSpan.FromHours(1);

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(action.Invoke, -2              , exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action.Invoke, Retries.Infinite, null            , delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action.Invoke, Retries.Infinite, exceptionHandler, null));

#nullable enable

            // Create a ReliableAsyncAction and validate
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint> actual = factory(action.Invoke, 37, exceptionHandler, delayHandler);

            Ctor(actual, 37, exceptionHandler, delayHandler);
            CtorAction(actual, action);
        }

        private void Ctor_Interruptable_DelayHandler(Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, Task>, int, ExceptionHandler, DelayHandler, ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>> factory)
        {
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, Task> action = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => await Task.CompletedTask;
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            FuncProxy<int, TimeSpan> delayHandler = new FuncProxy<int, TimeSpan>(i => Constants.Delay);

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(action, -2              , exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action, Retries.Infinite, null            , delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action, Retries.Infinite, exceptionHandler, null));

#nullable enable

            // Create a ReliableAsyncAction and validate
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint> actual = factory(action, 37, exceptionHandler, delayHandler.Invoke);

            Ctor(actual, 37, exceptionHandler, delayHandler);
            CtorAction(actual, action);
        }

        private void Ctor_Interruptable_ComplexDelayHandler(Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, Task>, int, ExceptionHandler, ComplexDelayHandler, ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>> factory)
        {
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, Task> action = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => await Task.CompletedTask;
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            ComplexDelayHandler delayHandler = (i, e) => TimeSpan.FromHours(1);

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(action, -2              , exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action, Retries.Infinite, null            , delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action, Retries.Infinite, exceptionHandler, null));

#nullable enable

            // Create a ReliableAsyncAction and validate
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint> actual = factory(action, 37, exceptionHandler, delayHandler);

            Ctor(actual, 37, exceptionHandler, delayHandler);
            CtorAction(actual, action);
        }

        private void CtorAction(ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint> reliableAction, FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Task> expected)
            => CtorAction(reliableAction, actual =>
            {
                expected.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint>(Arguments.Validate);

                Assert.AreEqual(0, expected.Calls);
                actual(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, default);
                Assert.AreEqual(1, expected.Calls);
            });

        private void CtorAction(ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint> reliableAction, Func<int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, Task> expected)
            => CtorAction(reliableAction, (Func<int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, Task> actual) => Assert.AreSame(expected, actual));

        private void CtorAction(ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint> reliableAction, Action<Func<int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, Task>> validateAction)
            => validateAction(s_getAction(reliableAction));

        #endregion

        #region InvokeAsync

        private void InvokeAsync(bool passToken)
        {
            Action<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken> invokeAsync;
            if (passToken)
                invokeAsync = (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t) => r.InvokeAsync(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t).Wait();
            else
                invokeAsync = (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t) => r.InvokeAsync(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8).Wait();

#nullable disable

            // Test an action that returns a null Task
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint> badAction = new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => null, Retries.Infinite, ExceptionPolicy.Transient, DelayPolicy.None);
            Assert.That.ThrowsException<InvalidOperationException>(() => invokeAsync(badAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, CancellationToken.None));

#nullable enable

            // Callers may optionally include event handlers
            foreach (bool addEventHandlers in new bool[] { false, true })
            {
                Invoke_Success        (invokeAsync, addEventHandlers);
                Invoke_EventualSuccess(invokeAsync, addEventHandlers);

                Invoke_Failure         ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t, e) => Assert.That.ThrowsException(() => invokeAsync(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t), e), addEventHandlers);
                Invoke_EventualFailure ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t, e) => Assert.That.ThrowsException(() => invokeAsync(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t), e), addEventHandlers);
                Invoke_RetriesExhausted((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t, e) => Assert.That.ThrowsException(() => invokeAsync(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t), e), addEventHandlers);

                if (passToken)
                {
                    Invoke_Canceled_Action(invokeAsync, addEventHandlers, useSynchronousAction: false);
                    Invoke_Canceled_Action(invokeAsync, addEventHandlers, useSynchronousAction: true );
                    Invoke_Canceled_Delay (invokeAsync, addEventHandlers);
                }
            }
        }

        #endregion

        #region TryInvokeAsync

        private void TryInvokeAsync(bool passToken)
        {
            Func<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, bool> tryInvokeAsync;
            if (passToken)
                tryInvokeAsync = (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t) => r.TryInvokeAsync(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t).Result;
            else
                tryInvokeAsync = (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t) => r.TryInvokeAsync(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8).Result;

#nullable disable

            // Test an action that returns a null Task
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint> badAction = new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => null, Retries.Infinite, ExceptionPolicy.Transient, DelayPolicy.None);
            Assert.That.ThrowsException<InvalidOperationException>(() => tryInvokeAsync(badAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, CancellationToken.None));

#nullable enable

            // Callers may optionally include event handlers
            foreach (bool addEventHandlers in new bool[] { false, true })
            {
                Invoke_Success         ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t   ) => Assert.IsTrue (tryInvokeAsync(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t)), addEventHandlers);
                Invoke_EventualSuccess ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t   ) => Assert.IsTrue (tryInvokeAsync(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t)), addEventHandlers);
                Invoke_Failure         ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t, e) => Assert.IsFalse(tryInvokeAsync(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t)), addEventHandlers);
                Invoke_EventualFailure ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t, e) => Assert.IsFalse(tryInvokeAsync(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t)), addEventHandlers);
                Invoke_RetriesExhausted((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t, e) => Assert.IsFalse(tryInvokeAsync(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t)), addEventHandlers);

                if (passToken)
                {
                    Invoke_Canceled_Action((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t) => r.TryInvokeAsync(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t).Wait(), addEventHandlers, useSynchronousAction: false);
                    Invoke_Canceled_Action((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t) => r.TryInvokeAsync(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t).Wait(), addEventHandlers, useSynchronousAction: true );
                    Invoke_Canceled_Delay ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t) => r.TryInvokeAsync(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t).Wait(), addEventHandlers);
                }
            }
        }

        #endregion

        #region Invoke_Success

        private void Invoke_Success(Action<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken> assertInvoke, bool addEventHandlers)
        {
            // Create a "successful" user-defined action
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Task> action = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Task>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => await Task.CompletedTask);

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool>          exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Fatal.Invoke);
            FuncProxy<int, Exception, TimeSpan> delayHandler      = new FuncProxy<int, Exception, TimeSpan>((i, e) => TimeSpan.Zero);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAsyncAction
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint> reliableAsyncAction = new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
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
            action          .Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint>(Arguments.Validate);
            exceptionHandler.Invoking += Expect.Nothing<Exception>();
            delayHandler    .Invoking += Expect.Nothing<int, Exception>();
            retryHandler    .Invoking += Expect.Nothing<int, Exception>();
            failedHandler   .Invoking += Expect.Nothing<Exception>();
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableAsyncAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, tokenSource.Token);

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

        private void Invoke_Failure(Action<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, Type> assertInvoke, bool addEventHandlers)
        {
            // Create an "unsuccessful" user-defined action
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Task> action = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Task>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => await Task.FromException(new OutOfMemoryException()));

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool>          exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Fail<OutOfMemoryException>().Invoke);
            FuncProxy<int, Exception, TimeSpan> delayHandler      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAsyncAction
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint> reliableAsyncAction = new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
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
            action          .Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint>(Arguments.Validate);
            exceptionHandler.Invoking += Expect.Exception(typeof(OutOfMemoryException));
            delayHandler    .Invoking += Expect.Nothing<int, Exception>();
            retryHandler    .Invoking += Expect.Nothing<int, Exception>();
            failedHandler   .Invoking += Expect.Exception(typeof(OutOfMemoryException));
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableAsyncAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, tokenSource.Token, typeof(OutOfMemoryException));

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

        private void Invoke_EventualSuccess(Action<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken> assertInvoke, bool addEventHandlers)
        {
            // Create a "successful" user-defined action that completes after 1 IOException
            Action flakyAction = FlakyAction.Create<IOException>(1);
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Task> action = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Task>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => { flakyAction(); await Task.CompletedTask; });

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool>          exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            FuncProxy<int, Exception, TimeSpan> delayHandler      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAsyncAction
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint> reliableAsyncAction = new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
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
            action          .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint>(Arguments.Validate, Constants.MinDelay);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            delayHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            retryHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<Exception>();
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableAsyncAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, tokenSource.Token);

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

        private void Invoke_EventualFailure(Action<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, Type> assertInvoke, bool addEventHandlers)
        {
            // Create an "unsuccessful" user-defined action that fails after 2 transient exceptions
            Action flakyAction = FlakyAction.Create<IOException, OutOfMemoryException>(2);
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Task> action = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Task>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => { flakyAction(); await Task.CompletedTask; });

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool>          exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            FuncProxy<int, Exception, TimeSpan> delayHandler      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAsyncAction
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint> reliableAsyncAction = new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
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
            action          .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint>(Arguments.Validate, Constants.MinDelay);
            exceptionHandler.Invoking += Expect.Exceptions(typeof(IOException), typeof(OutOfMemoryException), 2);
            delayHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            retryHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            failedHandler   .Invoking += Expect.Exception(typeof(OutOfMemoryException));
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableAsyncAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, tokenSource.Token, typeof(OutOfMemoryException));

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

        private void Invoke_RetriesExhausted(Action<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, Type> assertInvoke, bool addEventHandlers)
        {
            // Create an "unsuccessful" user-defined action that exhausts the configured number of retries
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Task> action = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Task>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => await Task.FromException(new IOException()));

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool>          exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            FuncProxy<int, Exception, TimeSpan> delayHandler      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAsyncAction
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint> reliableAsyncAction = new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
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
            action          .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint>(Arguments.Validate, Constants.MinDelay);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            delayHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            retryHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<Exception>();
            exhaustedHandler.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableAsyncAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, tokenSource.Token, typeof(IOException));

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

        private void Invoke_Canceled_Action(Action<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken> invoke, bool addEventHandlers, bool useSynchronousAction)
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create a user-defined action that will throw an exception depending on whether its canceled
            // Note: We need to separately check the use of asynchronous and synchronous methods when checking cancellation
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, Task> action = useSynchronousAction
                ? new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, Task>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) =>
                    {
                        token.ThrowIfCancellationRequested();
                        throw new IOException();
                    })
                : new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, Task>(
                    async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) =>
                    {
                        token.ThrowIfCancellationRequested();
                        await Task.FromException(new IOException());
                    });

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool>          exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Transient.Invoke);
            FuncProxy<int, Exception, TimeSpan> delayHandler      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAsyncAction
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint> reliableAsyncAction = new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
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
            action          .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            delayHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            retryHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<Exception>();
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Cancel the action on its 2nd attempt
            action          .Invoking += (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, t, c) =>
            {
                if (c.Calls == 2)
                    tokenSource.Cancel();
            };

            // Invoke, retry, and cancel
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableAsyncAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, tokenSource.Token), allowedDerivedTypes: true);

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

        private void Invoke_Canceled_Delay(Action<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken> invoke, bool addEventHandlers)
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined action that continues to fail with transient exceptions until it's canceled
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Task> action = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Task>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => await Task.FromException(new IOException()));

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool>          exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Transient.Invoke);
            FuncProxy<int, Exception, TimeSpan> delayHandler      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAsyncAction
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint> reliableAsyncAction = new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
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
            action          .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint>(Arguments.Validate, Constants.MinDelay);
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
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableAsyncAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, tokenSource.Token), allowedDerivedTypes: true);

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
