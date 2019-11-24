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
        public void WithRetryT2_DelayPolicy()
        {
            Action<int, string> nullAction = null;
            Action<int, string> action     = (arg1, arg2) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null ));

            Action<InterruptableAction<int, string>, int, string, CancellationToken> invoke;
            Func<Action<int, string>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string>> withRetry = (a, r, e, d) => a.WithRetry(r, e, d);

            // Without Token
            invoke = (action, arg1, arg2, token) => action(arg1, arg2);

            WithRetryT2_Success         (withRetry, invoke);
            WithRetryT2_Failure         (withRetry, invoke);
            WithRetryT2_EventualSuccess (withRetry, invoke);
            WithRetryT2_EventualFailure (withRetry, invoke);
            WithRetryT2_RetriesExhausted(withRetry, invoke);

            // With Token
            invoke = (action, arg1, arg2, token) => action(arg1, arg2, token);

            WithRetryT2_Success         (withRetry, invoke);
            WithRetryT2_Failure         (withRetry, invoke);
            WithRetryT2_EventualSuccess (withRetry, invoke);
            WithRetryT2_EventualFailure (withRetry, invoke);
            WithRetryT2_RetriesExhausted(withRetry, invoke);
            WithRetryT2_Canceled        (withRetry);
        }

        [TestMethod]
        public void WithRetryT2_ComplexDelayPolicy()
        {
            Action<int, string> nullAction = null;
            Action<int, string> action     = (arg1, arg2) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy)null));

            Action<InterruptableAction<int, string>, int, string, CancellationToken> invoke;
            Func<Action<int, string>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string>> withRetry = (a, r, e, d) => a.WithRetry(r, e, d);

            // Without Token
            invoke = (action, arg1, arg2, token) => action(arg1, arg2);

            WithRetryT2_Success         (withRetry, invoke);
            WithRetryT2_Failure         (withRetry, invoke);
            WithRetryT2_EventualSuccess (withRetry, invoke);
            WithRetryT2_EventualFailure (withRetry, invoke);
            WithRetryT2_RetriesExhausted(withRetry, invoke);

            // With Token
            invoke = (action, arg1, arg2, token) => action(arg1, arg2, token);

            WithRetryT2_Success         (withRetry, invoke);
            WithRetryT2_Failure         (withRetry, invoke);
            WithRetryT2_EventualSuccess (withRetry, invoke);
            WithRetryT2_EventualFailure (withRetry, invoke);
            WithRetryT2_RetriesExhausted(withRetry, invoke);
            WithRetryT2_Canceled        (withRetry);
        }

        #region WithRetryT2_Success

        private void WithRetryT2_Success(
            Func<Action<int, string>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string>> withRetry,
            Action<InterruptableAction<int, string>, int, string, CancellationToken> invoke)
            => WithRetryT2_Success(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                });

        private void WithRetryT2_Success(
            Func<Action<int, string>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string>> withRetry,
            Action<InterruptableAction<int, string>, int, string, CancellationToken> invoke)
            => WithRetryT2_Success(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, Exception>();
                    return delayPolicy;
                });

        private void WithRetryT2_Success<T>(
            Func<Action<int, string>, int, ExceptionPolicy, T, InterruptableAction<int, string>> withRetry,
            Action<InterruptableAction<int, string>, int, string, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create a "successful" user-defined action
            ActionProxy<int, string> action = new ActionProxy<int, string>((arg1, arg2) => Operation.Null());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>();
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAction
            InterruptableAction<int, string> reliableAction = withRetry(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.Arguments<int, string>(Arguments.Validate);
            exceptionPolicy.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                invoke(reliableAction, 42, "foo", tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(1, action         .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT2_Failure

        private void WithRetryT2_Failure(
            Func<Action<int, string>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string>> withRetry,
            Action<InterruptableAction<int, string>, int, string, CancellationToken> invoke)
            => WithRetryT2_Failure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                });

        private void WithRetryT2_Failure(
            Func<Action<int, string>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string>> withRetry,
            Action<InterruptableAction<int, string>, int, string, CancellationToken> invoke)
            => WithRetryT2_Failure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, Exception>();
                    return delayPolicy;
                });

        private void WithRetryT2_Failure<T>(
            Func<Action<int, string>, int, ExceptionPolicy, T, InterruptableAction<int, string>> withRetry,
            Action<InterruptableAction<int, string>, int, string, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action
            ActionProxy<int, string> action = new ActionProxy<int, string>((arg1, arg2) => throw new InvalidOperationException());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Fail<InvalidOperationException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAction
            InterruptableAction<int, string> reliableAction = withRetry(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.Arguments<int, string>(Arguments.Validate);
            exceptionPolicy.Invoking += Expect.Exception(typeof(InvalidOperationException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableAction, 42, "foo", tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(1, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT2_EventualSuccess

        private void WithRetryT2_EventualSuccess(
            Func<Action<int, string>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string>> withRetry,
            Action<InterruptableAction<int, string>, int, string, CancellationToken> invoke)
            => WithRetryT2_EventualSuccess(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT2_EventualSuccess(
            Func<Action<int, string>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string>> withRetry,
            Action<InterruptableAction<int, string>, int, string, CancellationToken> invoke)
            => WithRetryT2_EventualSuccess(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT2_EventualSuccess<T>(
            Func<Action<int, string>, int, ExceptionPolicy, T, InterruptableAction<int, string>> withRetry,
            Action<InterruptableAction<int, string>, int, string, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create a "successful" user-defined action that completes after 1 IOException
            Action flakyAction = FlakyAction.Create<IOException>(1);
            ActionProxy<int, string> action = new ActionProxy<int, string>((arg1, arg2) => flakyAction());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAction
            InterruptableAction<int, string> reliableAction = withRetry(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                invoke(reliableAction, 42, "foo", tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(2, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(1, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT2_EventualFailure

        private void WithRetryT2_EventualFailure(
            Func<Action<int, string>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string>> withRetry,
            Action<InterruptableAction<int, string>, int, string, CancellationToken> invoke)
            => WithRetryT2_EventualFailure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT2_EventualFailure(
            Func<Action<int, string>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string>> withRetry,
            Action<InterruptableAction<int, string>, int, string, CancellationToken> invoke)
            => WithRetryT2_EventualFailure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT2_EventualFailure<T>(
            Func<Action<int, string>, int, ExceptionPolicy, T, InterruptableAction<int, string>> withRetry,
            Action<InterruptableAction<int, string>, int, string, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that fails after 2 transient exceptions
            Action flakyAction = FlakyAction.Create<IOException, InvalidOperationException>(2);
            ActionProxy<int, string> action = new ActionProxy<int, string>((arg1, arg2) => flakyAction());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAction
            InterruptableAction<int, string> reliableAction = withRetry(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exceptions(typeof(IOException), typeof(InvalidOperationException), 2);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableAction, 42, "foo", tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion

        #region WithRetryT2_RetriesExhausted

        private void WithRetryT2_RetriesExhausted(
            Func<Action<int, string>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string>> withRetry,
            Action<InterruptableAction<int, string>, int, string, CancellationToken> invoke)
            => WithRetryT2_RetriesExhausted(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT2_RetriesExhausted(
            Func<Action<int, string>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string>> withRetry,
            Action<InterruptableAction<int, string>, int, string, CancellationToken> invoke)
            => WithRetryT2_RetriesExhausted(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT2_RetriesExhausted<T>(
            Func<Action<int, string>, int, ExceptionPolicy, T, InterruptableAction<int, string>> withRetry,
            Action<InterruptableAction<int, string>, int, string, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that exhausts the configured number of retries
            ActionProxy<int, string> action = new ActionProxy<int, string>((arg1, arg2) => throw new IOException());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAction
            InterruptableAction<int, string> reliableAction = withRetry(
                action.Invoke,
                2,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<IOException>(() => invoke(reliableAction, 42, "foo", tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion

        #region WithRetryT2_Canceled

        private void WithRetryT2_Canceled(Func<Action<int, string>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string>> withRetry)
            => WithRetryT2_Canceled(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT2_Canceled(Func<Action<int, string>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string>> withRetry)
            => WithRetryT2_Canceled(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT2_Canceled<T>(Func<Action<int, string>, int, ExceptionPolicy, T, InterruptableAction<int, string>> withRetry, Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            using ManualResetEvent        cancellationTrigger = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource         = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined action that continues to fail with transient exceptions until it's canceled
            ActionProxy<int, string> action = new ActionProxy<int, string>((arg1, arg2) => throw new IOException());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAction
            InterruptableAction<int, string> reliableAction = withRetry(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Trigger the event upon retry
            action.Invoking += (arg1, arg2, c) =>
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
            Assert.That.ThrowsException<OperationCanceledException>(() => reliableAction(42, "foo", tokenSource.Token));

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
