// Generated from Action.Extensions.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    partial class ActionExtensionsTest
    {
        [TestMethod]
        public void WithRetryT6_DelayPolicy()
        {
            Action<int, string, double, long, ushort, byte> nullAction = null;
            Action<int, string, double, long, ushort, byte> action     = (arg1, arg2, arg3, arg4, arg5, arg6) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null ));

            Action<InterruptableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken> invoke;
            Func<Action<int, string, double, long, ushort, byte>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string, double, long, ushort, byte>> withRetry = (a, r, e, d) => a.WithRetry(r, e, d);

            // Without Token
            invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, token) => action(arg1, arg2, arg3, arg4, arg5, arg6);

            WithRetryT6_Success         (withRetry, invoke);
            WithRetryT6_Failure         (withRetry, invoke);
            WithRetryT6_EventualSuccess (withRetry, invoke);
            WithRetryT6_EventualFailure (withRetry, invoke);
            WithRetryT6_RetriesExhausted(withRetry, invoke);

            // With Token
            invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, token);

            WithRetryT6_Success         (withRetry, invoke);
            WithRetryT6_Failure         (withRetry, invoke);
            WithRetryT6_EventualSuccess (withRetry, invoke);
            WithRetryT6_EventualFailure (withRetry, invoke);
            WithRetryT6_RetriesExhausted(withRetry, invoke);
            WithRetryT6_Canceled        (withRetry);
        }

        [TestMethod]
        public void WithRetryT6_ComplexDelayPolicy()
        {
            Action<int, string, double, long, ushort, byte> nullAction = null;
            Action<int, string, double, long, ushort, byte> action     = (arg1, arg2, arg3, arg4, arg5, arg6) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy)null));

            Action<InterruptableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken> invoke;
            Func<Action<int, string, double, long, ushort, byte>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string, double, long, ushort, byte>> withRetry = (a, r, e, d) => a.WithRetry(r, e, d);

            // Without Token
            invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, token) => action(arg1, arg2, arg3, arg4, arg5, arg6);

            WithRetryT6_Success         (withRetry, invoke);
            WithRetryT6_Failure         (withRetry, invoke);
            WithRetryT6_EventualSuccess (withRetry, invoke);
            WithRetryT6_EventualFailure (withRetry, invoke);
            WithRetryT6_RetriesExhausted(withRetry, invoke);

            // With Token
            invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, token);

            WithRetryT6_Success         (withRetry, invoke);
            WithRetryT6_Failure         (withRetry, invoke);
            WithRetryT6_EventualSuccess (withRetry, invoke);
            WithRetryT6_EventualFailure (withRetry, invoke);
            WithRetryT6_RetriesExhausted(withRetry, invoke);
            WithRetryT6_Canceled        (withRetry);
        }

        #region WithRetryT6_Success

        private void WithRetryT6_Success(
            Func<Action<int, string, double, long, ushort, byte>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string, double, long, ushort, byte>> withRetry,
            Action<InterruptableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken> invoke)
            => WithRetryT6_Success(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                });

        private void WithRetryT6_Success(
            Func<Action<int, string, double, long, ushort, byte>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string, double, long, ushort, byte>> withRetry,
            Action<InterruptableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken> invoke)
            => WithRetryT6_Success(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, Exception>();
                    return delayPolicy;
                });

        private void WithRetryT6_Success<T>(
            Func<Action<int, string, double, long, ushort, byte>, int, ExceptionPolicy, T, InterruptableAction<int, string, double, long, ushort, byte>> withRetry,
            Action<InterruptableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create a "successful" user-defined action
            ActionProxy<int, string, double, long, ushort, byte> action = new ActionProxy<int, string, double, long, ushort, byte>((arg1, arg2, arg3, arg4, arg5, arg6) => Operation.Null());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>();
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAction
            InterruptableAction<int, string, double, long, ushort, byte> reliableAction = withRetry(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.Arguments<int, string, double, long, ushort, byte>(Arguments.Validate);
            exceptionPolicy.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(1, action         .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT6_Failure

        private void WithRetryT6_Failure(
            Func<Action<int, string, double, long, ushort, byte>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string, double, long, ushort, byte>> withRetry,
            Action<InterruptableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken> invoke)
            => WithRetryT6_Failure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                });

        private void WithRetryT6_Failure(
            Func<Action<int, string, double, long, ushort, byte>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string, double, long, ushort, byte>> withRetry,
            Action<InterruptableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken> invoke)
            => WithRetryT6_Failure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, Exception>();
                    return delayPolicy;
                });

        private void WithRetryT6_Failure<T>(
            Func<Action<int, string, double, long, ushort, byte>, int, ExceptionPolicy, T, InterruptableAction<int, string, double, long, ushort, byte>> withRetry,
            Action<InterruptableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action
            ActionProxy<int, string, double, long, ushort, byte> action = new ActionProxy<int, string, double, long, ushort, byte>((arg1, arg2, arg3, arg4, arg5, arg6) => throw new InvalidOperationException());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Fail<InvalidOperationException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAction
            InterruptableAction<int, string, double, long, ushort, byte> reliableAction = withRetry(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.Arguments<int, string, double, long, ushort, byte>(Arguments.Validate);
            exceptionPolicy.Invoking += Expect.Exception(typeof(InvalidOperationException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(1, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT6_EventualSuccess

        private void WithRetryT6_EventualSuccess(
            Func<Action<int, string, double, long, ushort, byte>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string, double, long, ushort, byte>> withRetry,
            Action<InterruptableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken> invoke)
            => WithRetryT6_EventualSuccess(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT6_EventualSuccess(
            Func<Action<int, string, double, long, ushort, byte>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string, double, long, ushort, byte>> withRetry,
            Action<InterruptableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken> invoke)
            => WithRetryT6_EventualSuccess(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT6_EventualSuccess<T>(
            Func<Action<int, string, double, long, ushort, byte>, int, ExceptionPolicy, T, InterruptableAction<int, string, double, long, ushort, byte>> withRetry,
            Action<InterruptableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create a "successful" user-defined action that completes after 1 IOException
            Action flakyAction = FlakyAction.Create<IOException>(1);
            ActionProxy<int, string, double, long, ushort, byte> action = new ActionProxy<int, string, double, long, ushort, byte>((arg1, arg2, arg3, arg4, arg5, arg6) => flakyAction());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAction
            InterruptableAction<int, string, double, long, ushort, byte> reliableAction = withRetry(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(2, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(1, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT6_EventualFailure

        private void WithRetryT6_EventualFailure(
            Func<Action<int, string, double, long, ushort, byte>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string, double, long, ushort, byte>> withRetry,
            Action<InterruptableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken> invoke)
            => WithRetryT6_EventualFailure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT6_EventualFailure(
            Func<Action<int, string, double, long, ushort, byte>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string, double, long, ushort, byte>> withRetry,
            Action<InterruptableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken> invoke)
            => WithRetryT6_EventualFailure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT6_EventualFailure<T>(
            Func<Action<int, string, double, long, ushort, byte>, int, ExceptionPolicy, T, InterruptableAction<int, string, double, long, ushort, byte>> withRetry,
            Action<InterruptableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that fails after 2 transient exceptions
            Action flakyAction = FlakyAction.Create<IOException, InvalidOperationException>(2);
            ActionProxy<int, string, double, long, ushort, byte> action = new ActionProxy<int, string, double, long, ushort, byte>((arg1, arg2, arg3, arg4, arg5, arg6) => flakyAction());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAction
            InterruptableAction<int, string, double, long, ushort, byte> reliableAction = withRetry(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exceptions(typeof(IOException), typeof(InvalidOperationException), 2);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion

        #region WithRetryT6_RetriesExhausted

        private void WithRetryT6_RetriesExhausted(
            Func<Action<int, string, double, long, ushort, byte>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string, double, long, ushort, byte>> withRetry,
            Action<InterruptableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken> invoke)
            => WithRetryT6_RetriesExhausted(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT6_RetriesExhausted(
            Func<Action<int, string, double, long, ushort, byte>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string, double, long, ushort, byte>> withRetry,
            Action<InterruptableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken> invoke)
            => WithRetryT6_RetriesExhausted(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT6_RetriesExhausted<T>(
            Func<Action<int, string, double, long, ushort, byte>, int, ExceptionPolicy, T, InterruptableAction<int, string, double, long, ushort, byte>> withRetry,
            Action<InterruptableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that exhausts the configured number of retries
            ActionProxy<int, string, double, long, ushort, byte> action = new ActionProxy<int, string, double, long, ushort, byte>((arg1, arg2, arg3, arg4, arg5, arg6) => throw new IOException());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAction
            InterruptableAction<int, string, double, long, ushort, byte> reliableAction = withRetry(
                action.Invoke,
                2,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<IOException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion

        #region WithRetryT6_Canceled

        private void WithRetryT6_Canceled(Func<Action<int, string, double, long, ushort, byte>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string, double, long, ushort, byte>> withRetry)
            => WithRetryT6_Canceled(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT6_Canceled(Func<Action<int, string, double, long, ushort, byte>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string, double, long, ushort, byte>> withRetry)
            => WithRetryT6_Canceled(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT6_Canceled<T>(Func<Action<int, string, double, long, ushort, byte>, int, ExceptionPolicy, T, InterruptableAction<int, string, double, long, ushort, byte>> withRetry, Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            using ManualResetEvent        cancellationTrigger = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource         = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined action that continues to fail with transient exceptions until it's canceled
            ActionProxy<int, string, double, long, ushort, byte> action = new ActionProxy<int, string, double, long, ushort, byte>((arg1, arg2, arg3, arg4, arg5, arg6) => throw new IOException());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAction
            InterruptableAction<int, string, double, long, ushort, byte> reliableAction = withRetry(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Trigger the event upon retry
            action.Invoking += (arg1, arg2, arg3, arg4, arg5, arg6, c) =>
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
            Assert.That.ThrowsException<OperationCanceledException>(() => reliableAction(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, tokenSource.Token));

            // Validate the number of calls
            int calls = action.Calls;
            Assert.IsTrue(calls > 1);

            Assert.AreEqual(calls, action          .Calls);
            Assert.AreEqual(calls, exceptionPolicy .Calls);
            Assert.AreEqual(calls, delayPolicy     .Calls);
        }

        #endregion
    }
}