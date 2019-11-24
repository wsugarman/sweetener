// Generated from ReliableAction.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    [TestClass]
    public sealed class ReliableAction7Test : ReliableDelegateTest
    {
        private static readonly Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, Action<int, string, double, long, ushort, byte, TimeSpan>> s_getAction = DynamicGetter.ForField<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, Action<int, string, double, long, ushort, byte, TimeSpan>>("_action");

        [TestMethod]
        public void Ctor_DelayPolicy()
            => Ctor_DelayPolicy((a, m, d, e) => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(a, m, d, e));

        [TestMethod]
        public void Ctor_ComplexDelayPolicy()
            => Ctor_ComplexDelayPolicy((a, m, d, e) => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(a, m, d, e));

        [TestMethod]
        public void Create_DelayPolicy()
            => Ctor_DelayPolicy((a, m, d, e) => ReliableAction.Create(a, m, d, e));

        [TestMethod]
        public void Create_ComplexDelayPolicy()
            => Ctor_ComplexDelayPolicy((a, m, d, e) => ReliableAction.Create(a, m, d, e));

        [TestMethod]
        public void Invoke_NoCancellationToken()
            => Invoke((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7) => r.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7) => r.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, tokenSource.Token));

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, token) => r.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, token), addEventHandlers: false);
            Invoke_Canceled((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, token) => r.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, token), addEventHandlers: true );
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7) => r.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                TryInvoke((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7) => r.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, tokenSource.Token));

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, token) => r.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, token), addEventHandlers: false);
            Invoke_Canceled((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, token) => r.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, token), addEventHandlers: true );
        }

        #region Ctor

        private void Ctor_DelayPolicy(Func<Action<int, string, double, long, ushort, byte, TimeSpan>, int, ExceptionPolicy, DelayPolicy, ReliableAction<int, string, double, long, ushort, byte, TimeSpan>> factory)
        {
            Action<int, string, double, long, ushort, byte, TimeSpan> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Operation.Null();
            ExceptionPolicy          exceptionPolicy = ExceptionPolicies.Fatal;
            FuncProxy<int, TimeSpan> delayPolicy     = new FuncProxy<int, TimeSpan>(i => Constants.Delay);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null  , Retries.Infinite, exceptionPolicy, delayPolicy.Invoke));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(action, -2              , exceptionPolicy, delayPolicy.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action, Retries.Infinite, null           , delayPolicy.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action, Retries.Infinite, exceptionPolicy, null              ));

            // Create a ReliableAction and validate
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan> actual = factory(action, 37, exceptionPolicy, delayPolicy.Invoke);

            // DelayPolicies are wrapped in ComplexDelayPolicies, so we can only validate the correct assignment by invoking the policy
            Ctor(actual, action, 37, exceptionPolicy, actualPolicy =>
            {
                delayPolicy.Invoking += (i, c) => Assert.AreEqual(i, 42);
                Assert.AreEqual(Constants.Delay, actualPolicy(42, new ArgumentOutOfRangeException()));
                Assert.AreEqual(1, delayPolicy.Calls);
            });
        }

        private void Ctor_ComplexDelayPolicy(Func<Action<int, string, double, long, ushort, byte, TimeSpan>, int, ExceptionPolicy, ComplexDelayPolicy, ReliableAction<int, string, double, long, ushort, byte, TimeSpan>> factory)
        {
            Action<int, string, double, long, ushort, byte, TimeSpan> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Operation.Null();
            ExceptionPolicy    exceptionPolicy    = ExceptionPolicies.Fatal;
            ComplexDelayPolicy complexDelayPolicy = (i, e) => TimeSpan.FromHours(1);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null  , Retries.Infinite, exceptionPolicy, complexDelayPolicy));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(action, -2              , exceptionPolicy, complexDelayPolicy));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action, Retries.Infinite, null           , complexDelayPolicy));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(action, Retries.Infinite, exceptionPolicy, (ComplexDelayPolicy)null));

            // Create a ReliableAction and validate
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan> actual = factory(action, 37, exceptionPolicy, complexDelayPolicy);
            Ctor(actual, action, 37, exceptionPolicy, complexDelayPolicy);
        }

        private void Ctor(ReliableAction<int, string, double, long, ushort, byte, TimeSpan> reliableAction, Action<int, string, double, long, ushort, byte, TimeSpan> expectedAction, int expectedMaxRetries, ExceptionPolicy expectedExceptionPolicy, ComplexDelayPolicy expectedDelayPolicy)
            => Ctor(reliableAction, expectedAction, expectedMaxRetries, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        private void Ctor(ReliableAction<int, string, double, long, ushort, byte, TimeSpan> reliableAction, Action<int, string, double, long, ushort, byte, TimeSpan> expectedAction, int expectedMaxRetries, ExceptionPolicy expectedExceptionPolicy, Action<ComplexDelayPolicy> validateDelayPolicy)
        {
            Assert.AreSame (expectedAction         , s_getAction(reliableAction)         );
            Assert.AreEqual(expectedMaxRetries     , reliableAction.MaxRetries           );
            Assert.AreSame (expectedExceptionPolicy, s_getExceptionPolicy(reliableAction));

            validateDelayPolicy(s_getDelayPolicy(reliableAction));
        }

        #endregion

        #region Invoke

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, int, string, double, long, ushort, byte, TimeSpan> invoke)
        {
            // Callers may optionally include event handlers
            foreach (bool addEventHandlers in new bool[] { false, true })
            {
                Invoke_Success        (invoke, addEventHandlers);
                Invoke_EventualSuccess(invoke, addEventHandlers);

                Invoke_Failure         ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, e) => Assert.That.ThrowsException(() => invoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7), e), addEventHandlers);
                Invoke_EventualFailure ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, e) => Assert.That.ThrowsException(() => invoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7), e), addEventHandlers);
                Invoke_RetriesExhausted((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, e) => Assert.That.ThrowsException(() => invoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7), e), addEventHandlers);
            }
        }

        #endregion

        #region TryInvoke

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, int, string, double, long, ushort, byte, TimeSpan, bool> tryInvoke)
        {
            // Callers may optionally include event handlers
            foreach (bool addEventHandlers in new bool[] { false, true })
            {
                Invoke_Success         ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7   ) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7)), addEventHandlers);
                Invoke_EventualSuccess ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7   ) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7)), addEventHandlers);
                Invoke_Failure         ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, e) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7)), addEventHandlers);
                Invoke_EventualFailure ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, e) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7)), addEventHandlers);
                Invoke_RetriesExhausted((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, e) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7)), addEventHandlers);
            }
        }

        #endregion

        #region Invoke_Success

        private void Invoke_Success(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, int, string, double, long, ushort, byte, TimeSpan> assertInvoke, bool addEventHandlers)
        {
            // Create a "successful" user-defined action
            ActionProxy<int, string, double, long, ushort, byte, TimeSpan> action = new ActionProxy<int, string, double, long, ushort, byte, TimeSpan>((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Operation.Null());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool>          exceptionPolicy  = new FuncProxy<Exception, bool>();
            FuncProxy<int, Exception, TimeSpan> delayPolicy      = new FuncProxy<int, Exception, TimeSpan>();

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAction
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            if (addEventHandlers)
            {
                reliableAction.Retrying         += retryHandler    .Invoke;
                reliableAction.Failed           += failedHandler   .Invoke;
                reliableAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan>(Arguments.Validate);
            exceptionPolicy .Invoking += Expect.Nothing<Exception>();
            delayPolicy     .Invoking += Expect.Nothing<int, Exception>();
            retryHandler    .Invoking += Expect.Nothing<int, Exception>();
            failedHandler   .Invoking += Expect.Nothing<Exception>();
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Invoke
            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30));

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

        private void Invoke_Failure(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, int, string, double, long, ushort, byte, TimeSpan, Type> assertInvoke, bool addEventHandlers)
        {
            // Create an "unsuccessful" user-defined action
            ActionProxy<int, string, double, long, ushort, byte, TimeSpan> action = new ActionProxy<int, string, double, long, ushort, byte, TimeSpan>((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => throw new InvalidOperationException());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool>          exceptionPolicy  = new FuncProxy<Exception, bool>(ExceptionPolicies.Fail<InvalidOperationException>().Invoke);
            FuncProxy<int, Exception, TimeSpan> delayPolicy      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAction
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            if (addEventHandlers)
            {
                reliableAction.Retrying         += retryHandler    .Invoke;
                reliableAction.Failed           += failedHandler   .Invoke;
                reliableAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan>(Arguments.Validate);
            exceptionPolicy .Invoking += Expect.Exception(typeof(InvalidOperationException));
            delayPolicy     .Invoking += Expect.Nothing<int, Exception>();
            retryHandler    .Invoking += Expect.Nothing<int, Exception>();
            failedHandler   .Invoking += Expect.Exception(typeof(InvalidOperationException));
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Invoke
            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), typeof(InvalidOperationException));

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

        private void Invoke_EventualSuccess(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, int, string, double, long, ushort, byte, TimeSpan> assertInvoke, bool addEventHandlers)
        {
            // Create a "successful" user-defined action that completes after 1 IOException
            Action flakyAction = FlakyAction.Create<IOException>(1);
            ActionProxy<int, string, double, long, ushort, byte, TimeSpan> action = new ActionProxy<int, string, double, long, ushort, byte, TimeSpan>((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => flakyAction());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool>          exceptionPolicy  = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            FuncProxy<int, Exception, TimeSpan> delayPolicy      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAction
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            if (addEventHandlers)
            {
                reliableAction.Retrying         += retryHandler    .Invoke;
                reliableAction.Failed           += failedHandler   .Invoke;
                reliableAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy .Invoking += Expect.Exception(typeof(IOException));
            delayPolicy     .Invoking += Expect.ExceptionAsc(typeof(IOException));
            retryHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<Exception>();
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Invoke
            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30));

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

        private void Invoke_EventualFailure(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, int, string, double, long, ushort, byte, TimeSpan, Type> assertInvoke, bool addEventHandlers)
        {
            // Create an "unsuccessful" user-defined action that fails after 2 transient exceptions
            Action flakyAction = FlakyAction.Create<IOException, InvalidOperationException>(2);
            ActionProxy<int, string, double, long, ushort, byte, TimeSpan> action = new ActionProxy<int, string, double, long, ushort, byte, TimeSpan>((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => flakyAction());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool>          exceptionPolicy  = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            FuncProxy<int, Exception, TimeSpan> delayPolicy      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAction
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            if (addEventHandlers)
            {
                reliableAction.Retrying         += retryHandler    .Invoke;
                reliableAction.Failed           += failedHandler   .Invoke;
                reliableAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy .Invoking += Expect.Exceptions(typeof(IOException), typeof(InvalidOperationException), 2);
            delayPolicy     .Invoking += Expect.ExceptionAsc(typeof(IOException));
            retryHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            failedHandler   .Invoking += Expect.Exception(typeof(InvalidOperationException));
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Invoke
            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), typeof(InvalidOperationException));

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

        private void Invoke_RetriesExhausted(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, int, string, double, long, ushort, byte, TimeSpan, Type> assertInvoke, bool addEventHandlers)
        {
            // Create an "unsuccessful" user-defined action that exhausts the configured number of retries
            ActionProxy<int, string, double, long, ushort, byte, TimeSpan> action = new ActionProxy<int, string, double, long, ushort, byte, TimeSpan>((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => throw new IOException());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool>          exceptionPolicy  = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            FuncProxy<int, Exception, TimeSpan> delayPolicy      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAction
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(
                action.Invoke,
                2,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            if (addEventHandlers)
            {
                reliableAction.Retrying         += retryHandler    .Invoke;
                reliableAction.Failed           += failedHandler   .Invoke;
                reliableAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy .Invoking += Expect.Exception(typeof(IOException));
            delayPolicy     .Invoking += Expect.ExceptionAsc(typeof(IOException));
            retryHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<Exception>();
            exhaustedHandler.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), typeof(IOException));

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

        #region Invoke_Canceled

        private void Invoke_Canceled(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, int, string, double, long, ushort, byte, TimeSpan, CancellationToken> assertInvoke, bool addEventHandlers)
        {
            using ManualResetEvent        cancellationTrigger = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource         = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined action that continues to fail with transient exceptions until it's canceled
            ActionProxy<int, string, double, long, ushort, byte, TimeSpan> action = new ActionProxy<int, string, double, long, ushort, byte, TimeSpan>((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => throw new IOException());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool>          exceptionPolicy  = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            FuncProxy<int, Exception, TimeSpan> delayPolicy      = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);

            ActionProxy<int, Exception>         retryHandler     = new ActionProxy<int, Exception>();
            ActionProxy<Exception>              failedHandler    = new ActionProxy<Exception>();
            ActionProxy<Exception>              exhaustedHandler = new ActionProxy<Exception>();

            // Create ReliableAction
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            if (addEventHandlers)
            {
                reliableAction.Retrying         += retryHandler    .Invoke;
                reliableAction.Failed           += failedHandler   .Invoke;
                reliableAction.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            action          .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy .Invoking += Expect.Exception(typeof(IOException));
            delayPolicy     .Invoking += Expect.ExceptionAsc(typeof(IOException));
            retryHandler    .Invoking += Expect.ExceptionAsc(typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<Exception>();
            exhaustedHandler.Invoking += Expect.Nothing<Exception>();

            // Trigger the event upon retry
            action          .Invoking += (arg1, arg2, arg3, arg4, arg5, arg6, arg7, c) =>
            {
                if (c.Calls > 1)
                    cancellationTrigger.Set();
            };

            // Create a task whose job is to cancel the invocation after at least 1 retry
            Task cancellationTask = Task.Factory.StartNew((state) =>
            {
                (ManualResetEvent e, CancellationTokenSource s) = ((ManualResetEvent, CancellationTokenSource))state;
                e.WaitOne();
                s.Cancel();

            }, (cancellationTrigger, tokenSource));

            // Begin the invocation
            Assert.That.ThrowsException<OperationCanceledException>(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), tokenSource.Token));

            // Validate the number of calls
            int calls = action.Calls;
            Assert.IsTrue(calls > 1);

            Assert.AreEqual(calls    , action         .Calls);
            Assert.AreEqual(calls    , exceptionPolicy.Calls);
            Assert.AreEqual(calls    , delayPolicy    .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(calls - 1, retryHandler    .Calls);
                Assert.AreEqual(0        , failedHandler   .Calls);
                Assert.AreEqual(0        , exhaustedHandler.Calls);
            }
        }

        #endregion
    }
}
