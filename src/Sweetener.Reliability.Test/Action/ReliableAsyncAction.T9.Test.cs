// Generated from ReliableAsyncAction.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    [TestClass]
    public sealed class ReliableAsyncAction9Test : ReliableDelegateTest
    {
        private static readonly Func<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> s_getAction = DynamicGetter.ForField<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>>("_action");

        [TestMethod]
        public void Ctor_DelayPolicy()
            => Ctor_DelayPolicy((a, m, d, e) => new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(a, m, d, e));

        [TestMethod]
        public void Ctor_ComplexDelayPolicy()
            => Ctor_ComplexDelayPolicy((a, m, d, e) => new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(a, m, d, e));

        [TestMethod]
        public void Ctor_Interruptable_DelayPolicy()
            => Ctor_Interruptable_DelayPolicy((a, m, d, e) => new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(a, m, d, e));

        [TestMethod]
        public void Ctor_Interruptable_ComplexDelayPolicy()
            => Ctor_Interruptable_ComplexDelayPolicy((a, m, d, e) => new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(a, m, d, e));

        [TestMethod]
        public void Create_DelayPolicy()
            => Ctor_DelayPolicy((a, m, d, e) => ReliableAsyncAction.Create(a, m, d, e));

        [TestMethod]
        public void Create_ComplexDelayPolicy()
            => Ctor_ComplexDelayPolicy((a, m, d, e) => ReliableAsyncAction.Create(a, m, d, e));

        [TestMethod]
        public void Create_Interruptable_DelayPolicy()
            => Ctor_Interruptable_DelayPolicy((a, m, d, e) => ReliableAsyncAction.Create(a, m, d, e));

        [TestMethod]
        public void Create_Interruptable_ComplexDelayPolicy()
            => Ctor_Interruptable_ComplexDelayPolicy((a, m, d, e) => ReliableAsyncAction.Create(a, m, d, e));

        [TestMethod]
        public void InvokeAsync_NoCancellationToken()
            => InvokeAsync(passToken: false);

        [TestMethod]
        public void InvokeAsync_CancellationToken()
            => InvokeAsync(passToken: true);

        #region Ctor

        private void Ctor_DelayPolicy(Func<AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, DelayPolicy, ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> factory)
        {
            AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> action = new AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>();
            ExceptionPolicy          exceptionPolicy = ExceptionPolicies.Fatal;
            FuncProxy<int, TimeSpan> delayPolicy     = new FuncProxy<int, TimeSpan>(i => Constants.Delay);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, exceptionPolicy, delayPolicy.Invoke));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(action.InvokeAsync, -2              , exceptionPolicy, delayPolicy.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action.InvokeAsync, Retries.Infinite, null           , delayPolicy.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action.InvokeAsync, Retries.Infinite, exceptionPolicy, null              ));

            // Create a ReliableAsyncAction and validate
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> actual = factory(action.InvokeAsync, 37, exceptionPolicy, delayPolicy.Invoke);

            // Validate wrapped action
            InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> actualAction = s_getAction(actual);
            action.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(Arguments.Validate);
            Assert.AreEqual(0, action.Calls);
            actualAction(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL));
            Assert.AreEqual(1, action.Calls);

            Ctor(actual, 37, exceptionPolicy, delayPolicy);
        }

        private void Ctor_ComplexDelayPolicy(Func<AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, ComplexDelayPolicy, ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> factory)
        {
            AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> action = new AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>();
            ExceptionPolicy    exceptionPolicy    = ExceptionPolicies.Fatal;
            ComplexDelayPolicy complexDelayPolicy = (i, e) => TimeSpan.FromHours(1);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, exceptionPolicy, complexDelayPolicy));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(action.InvokeAsync, -2              , exceptionPolicy, complexDelayPolicy));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action.InvokeAsync, Retries.Infinite, null           , complexDelayPolicy));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action.InvokeAsync, Retries.Infinite, exceptionPolicy, null              ));

            // Create a ReliableAsyncAction and validate
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> actual = factory(action.InvokeAsync, 37, exceptionPolicy, complexDelayPolicy);

            // Validate wrapped action
            InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> actualAction = s_getAction(actual);
            action.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(Arguments.Validate);
            Assert.AreEqual(0, action.Calls);
            actualAction(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL));
            Assert.AreEqual(1, action.Calls);

            Ctor(actual, 37, exceptionPolicy, complexDelayPolicy);
        }

        private void Ctor_Interruptable_DelayPolicy(Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, DelayPolicy, ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> factory)
        {
            InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, t) => Operation.NullAsync();
            ExceptionPolicy          exceptionPolicy = ExceptionPolicies.Fatal;
            FuncProxy<int, TimeSpan> delayPolicy     = new FuncProxy<int, TimeSpan>(i => Constants.Delay);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null  , Retries.Infinite, exceptionPolicy, delayPolicy.Invoke));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(action, -2              , exceptionPolicy, delayPolicy.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action, Retries.Infinite, null           , delayPolicy.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action, Retries.Infinite, exceptionPolicy, null              ));

            // Create a ReliableAsyncAction and validate
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> actual = factory(action, 37, exceptionPolicy, delayPolicy.Invoke);

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, exceptionPolicy, delayPolicy);
        }

        private void Ctor_Interruptable_ComplexDelayPolicy(Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, ComplexDelayPolicy, ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> factory)
        {
            InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, t) => Operation.NullAsync();
            ExceptionPolicy    exceptionPolicy    = ExceptionPolicies.Fatal;
            ComplexDelayPolicy complexDelayPolicy = (i, e) => TimeSpan.FromHours(1);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null  , Retries.Infinite, exceptionPolicy, complexDelayPolicy));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(action, -2              , exceptionPolicy, complexDelayPolicy));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action, Retries.Infinite, null           , complexDelayPolicy));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action, Retries.Infinite, exceptionPolicy, null              ));

            // Create a ReliableAsyncAction and validate
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> actual = factory(action, 37, exceptionPolicy, complexDelayPolicy);

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, exceptionPolicy, complexDelayPolicy);
        }

        #endregion

        #region InvokeAsync

        private void InvokeAsync(bool passToken)
        {
            Action<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke;
            if (passToken)
                invoke = (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, t) => r.InvokeAsync(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, t).Wait();
            else
                invoke = (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, t) => r.InvokeAsync(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9).Wait();

            // Callers may optionally include event handlers
            foreach (bool addEventHandlers in new bool[] { false, true })
            {
                Invoke_Success        (invoke, addEventHandlers);
                Invoke_EventualSuccess(invoke, addEventHandlers);

                Invoke_Failure         ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, t, e) => Assert.That.ThrowsException(() => invoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, t), e), addEventHandlers);
                Invoke_EventualFailure ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, t, e) => Assert.That.ThrowsException(() => invoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, t), e), addEventHandlers);
                Invoke_RetriesExhausted((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, t, e) => Assert.That.ThrowsException(() => invoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, t), e), addEventHandlers);

                if (passToken)
                {
                    Invoke_Canceled_Action(invoke, addEventHandlers);
                    Invoke_Canceled_Delay (invoke, addEventHandlers);
                }
            }
        }

        #endregion

        #region Invoke_Success

        private void Invoke_Success(Action<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> assertInvoke, bool addEventHandlers)
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create a "successful" user-defined action
            AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> action = new AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => await Operation.NullAsync().ConfigureAwait(false));

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool>          exceptionPolicy  = new FuncProxy<Exception, bool>();
            FuncProxy<int, Exception, TimeSpan> delayPolicy      = new FuncProxy<int, Exception, TimeSpan>();

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAsyncAction
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> reliableAsyncAction = new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                action.InvokeAsync,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            if (addEventHandlers)
            {
                reliableAsyncAction.Retrying         += retryHandler    .Invoke;
                reliableAsyncAction.Failed           += failedHandler   .Invoke;
                reliableAsyncAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(Arguments.Validate);
            exceptionPolicy .Invoking += Expect.Nothing<Exception>();
            delayPolicy     .Invoking += Expect.Nothing<int, Exception>();
            retryHandler    .Invoking += Expect.Nothing<int, Exception>();
            failedHandler   .Invoking += Expect.Nothing<Exception>();
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Invoke
            assertInvoke(reliableAsyncAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(1, action          .Calls);
            Assert.AreEqual(0, exceptionPolicy .Calls);
            Assert.AreEqual(0, delayPolicy     .Calls);
            Assert.AreEqual(0, retryHandler    .Calls);
            Assert.AreEqual(0, failedHandler   .Calls);
            Assert.AreEqual(0, exhaustedHandler.Calls);
        }

        #endregion

        #region Invoke_Failure

        private void Invoke_Failure(Action<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken, Type> assertInvoke, bool addEventHandlers)
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined action
            AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> action = new AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => await Task.Run(() => throw new InvalidOperationException()).ConfigureAwait(false));

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool>          exceptionPolicy  = new FuncProxy<Exception, bool>(ExceptionPolicies.Fail<InvalidOperationException>().Invoke);
            FuncProxy<int, Exception, TimeSpan> delayPolicy      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAsyncAction
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> reliableAsyncAction = new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                action.InvokeAsync,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            if (addEventHandlers)
            {
                reliableAsyncAction.Retrying         += retryHandler    .Invoke;
                reliableAsyncAction.Failed           += failedHandler   .Invoke;
                reliableAsyncAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(Arguments.Validate);
            exceptionPolicy .Invoking += Expect.Exception(typeof(InvalidOperationException));
            delayPolicy     .Invoking += Expect.Nothing<int, Exception>();
            retryHandler    .Invoking += Expect.Nothing<int, Exception>();
            failedHandler   .Invoking += Expect.Exception(typeof(InvalidOperationException));
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Invoke
            assertInvoke(reliableAsyncAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), tokenSource.Token, typeof(InvalidOperationException));

            // Validate the number of calls
            Assert.AreEqual(1, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(0, retryHandler    .Calls);
                Assert.AreEqual(1, failedHandler   .Calls);
                Assert.AreEqual(0, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_EventualSuccess

        private void Invoke_EventualSuccess(Action<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> assertInvoke, bool addEventHandlers)
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create a "successful" user-defined action that completes after 1 IOException
            Action flakyAction = FlakyAction.Create<IOException>(1);
            AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> action = new AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => await Task.Run(flakyAction).ConfigureAwait(false));

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool>          exceptionPolicy  = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            FuncProxy<int, Exception, TimeSpan> delayPolicy      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAsyncAction
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> reliableAsyncAction = new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                action.InvokeAsync,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            if (addEventHandlers)
            {
                reliableAsyncAction.Retrying         += retryHandler    .Invoke;
                reliableAsyncAction.Failed           += failedHandler   .Invoke;
                reliableAsyncAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy .Invoking += Expect.Exception(typeof(IOException));
            delayPolicy     .Invoking += Expect.ExceptionAsc(typeof(IOException));
            retryHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<Exception>();
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Invoke
            assertInvoke(reliableAsyncAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(2, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(1, delayPolicy    .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(1, retryHandler    .Calls);
                Assert.AreEqual(0, failedHandler   .Calls);
                Assert.AreEqual(0, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_EventualFailure

        private void Invoke_EventualFailure(Action<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken, Type> assertInvoke, bool addEventHandlers)
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined action that fails after 2 transient exceptions
            Action flakyAction = FlakyAction.Create<IOException, InvalidOperationException>(2);
            AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> action = new AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => await Task.Run(flakyAction).ConfigureAwait(false));

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool>          exceptionPolicy  = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            FuncProxy<int, Exception, TimeSpan> delayPolicy      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAsyncAction
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> reliableAsyncAction = new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                action.InvokeAsync,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            if (addEventHandlers)
            {
                reliableAsyncAction.Retrying         += retryHandler    .Invoke;
                reliableAsyncAction.Failed           += failedHandler   .Invoke;
                reliableAsyncAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy .Invoking += Expect.Exceptions(typeof(IOException), typeof(InvalidOperationException), 2);
            delayPolicy     .Invoking += Expect.ExceptionAsc(typeof(IOException));
            retryHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            failedHandler   .Invoking += Expect.Exception(typeof(InvalidOperationException));
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Invoke
            assertInvoke(reliableAsyncAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), tokenSource.Token, typeof(InvalidOperationException));

            // Validate the number of calls
            Assert.AreEqual(3, action         .Calls);
            Assert.AreEqual(3, exceptionPolicy.Calls);
            Assert.AreEqual(2, delayPolicy    .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(2, retryHandler    .Calls);
                Assert.AreEqual(1, failedHandler   .Calls);
                Assert.AreEqual(0, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_RetriesExhausted

        private void Invoke_RetriesExhausted(Action<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken, Type> assertInvoke, bool addEventHandlers)
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined action that exhausts the configured number of retries
            AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> action = new AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => await Task.Run(() => throw new IOException()).ConfigureAwait(false));

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool>          exceptionPolicy  = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            FuncProxy<int, Exception, TimeSpan> delayPolicy      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAsyncAction
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> reliableAsyncAction = new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                action.InvokeAsync,
                2,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            if (addEventHandlers)
            {
                reliableAsyncAction.Retrying         += retryHandler    .Invoke;
                reliableAsyncAction.Failed           += failedHandler   .Invoke;
                reliableAsyncAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy .Invoking += Expect.Exception(typeof(IOException));
            delayPolicy     .Invoking += Expect.ExceptionAsc(typeof(IOException));
            retryHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<Exception>();
            exhaustedHandler.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            assertInvoke(reliableAsyncAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), tokenSource.Token, typeof(IOException));

            // Validate the number of calls
            Assert.AreEqual(3, action         .Calls);
            Assert.AreEqual(3, exceptionPolicy.Calls);
            Assert.AreEqual(2, delayPolicy    .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(2, retryHandler    .Calls);
                Assert.AreEqual(0, failedHandler   .Calls);
                Assert.AreEqual(1, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_Canceled_Action

        private void Invoke_Canceled_Action(Action<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke, bool addEventHandlers)
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create a user-defined action that will throw an exception depending on whether its canceled
            AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> action = new AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token) =>
            {
                await Task.CompletedTask;
                token.ThrowIfCancellationRequested();
                throw new IOException();
            });

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool>          exceptionPolicy  = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            FuncProxy<int, Exception, TimeSpan> delayPolicy      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAsyncAction
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> reliableAsyncAction = new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                action.InvokeAsync,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            if (addEventHandlers)
            {
                reliableAsyncAction.Retrying         += retryHandler    .Invoke;
                reliableAsyncAction.Failed           += failedHandler   .Invoke;
                reliableAsyncAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy .Invoking += Expect.Exception(typeof(IOException));
            delayPolicy     .Invoking += Expect.ExceptionAsc(typeof(IOException));
            retryHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<Exception>();
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Cancel the action on its 2nd attempt
            action          .Invoking += (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, t, c) =>
            {
                if (c.Calls == 2)
                    tokenSource.Cancel();
            };

            // Invoke, retry, and cancel
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableAsyncAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            Assert.AreEqual(2, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(1, delayPolicy    .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(1, retryHandler    .Calls);
                Assert.AreEqual(0, failedHandler   .Calls);
                Assert.AreEqual(0, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_Canceled_Delay

        private void Invoke_Canceled_Delay(Action<ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke, bool addEventHandlers)
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined action that continues to fail with transient exceptions until it's canceled
            AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> action = new AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => await Task.Run(() => throw new IOException()).ConfigureAwait(false));

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool>          exceptionPolicy  = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            FuncProxy<int, Exception, TimeSpan> delayPolicy      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAsyncAction
            ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> reliableAsyncAction = new ReliableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                action.InvokeAsync,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            if (addEventHandlers)
            {
                reliableAsyncAction.Retrying         += retryHandler    .Invoke;
                reliableAsyncAction.Failed           += failedHandler   .Invoke;
                reliableAsyncAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy .Invoking += Expect.Exception(typeof(IOException));
            delayPolicy     .Invoking += Expect.ExceptionAsc(typeof(IOException));
            retryHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<Exception>();
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Cancel the delay on its 2nd invocation
            delayPolicy     .Invoking += (i, e, c) =>
            {
                if (c.Calls == 2)
                    tokenSource.Cancel();
            };

            // Invoke, retry, and cancel
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableAsyncAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            Assert.AreEqual(2, action         .Calls);
            Assert.AreEqual(2, exceptionPolicy.Calls);
            Assert.AreEqual(2, delayPolicy    .Calls);

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
