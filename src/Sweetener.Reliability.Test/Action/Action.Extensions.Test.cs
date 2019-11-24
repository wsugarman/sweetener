// Generated from Action.Extensions.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    [TestClass]
    public partial class ActionExtensionsTest
    {
        [TestMethod]
        public void WithRetry_DelayPolicy()
        {
            Action nullAction = null;
            Action action     = () => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null ));

            Action<InterruptableAction, CancellationToken> invoke;
            Func<Action, int, ExceptionPolicy, DelayPolicy, InterruptableAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d);

            // Without Token
            invoke = (action, token) => action();

            WithRetry_Success         (withRetry, invoke);
            WithRetry_Failure         (withRetry, invoke);
            WithRetry_EventualSuccess (withRetry, invoke);
            WithRetry_EventualFailure (withRetry, invoke);
            WithRetry_RetriesExhausted(withRetry, invoke);

            // With Token
            invoke = (action, token) => action(token);

            WithRetry_Success         (withRetry, invoke);
            WithRetry_Failure         (withRetry, invoke);
            WithRetry_EventualSuccess (withRetry, invoke);
            WithRetry_EventualFailure (withRetry, invoke);
            WithRetry_RetriesExhausted(withRetry, invoke);
            WithRetry_Canceled        (withRetry);
        }

        [TestMethod]
        public void WithRetry_ComplexDelayPolicy()
        {
            Action nullAction = null;
            Action action     = () => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy)null));

            Action<InterruptableAction, CancellationToken> invoke;
            Func<Action, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d);

            // Without Token
            invoke = (action, token) => action();

            WithRetry_Success         (withRetry, invoke);
            WithRetry_Failure         (withRetry, invoke);
            WithRetry_EventualSuccess (withRetry, invoke);
            WithRetry_EventualFailure (withRetry, invoke);
            WithRetry_RetriesExhausted(withRetry, invoke);

            // With Token
            invoke = (action, token) => action(token);

            WithRetry_Success         (withRetry, invoke);
            WithRetry_Failure         (withRetry, invoke);
            WithRetry_EventualSuccess (withRetry, invoke);
            WithRetry_EventualFailure (withRetry, invoke);
            WithRetry_RetriesExhausted(withRetry, invoke);
            WithRetry_Canceled        (withRetry);
        }

        #region WithRetry_Success

        private void WithRetry_Success(
            Func<Action, int, ExceptionPolicy, DelayPolicy, InterruptableAction> withRetry,
            Action<InterruptableAction, CancellationToken> invoke)
            => WithRetry_Success(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                });

        private void WithRetry_Success(
            Func<Action, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction> withRetry,
            Action<InterruptableAction, CancellationToken> invoke)
            => WithRetry_Success(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, Exception>();
                    return delayPolicy;
                });

        private void WithRetry_Success<T>(
            Func<Action, int, ExceptionPolicy, T, InterruptableAction> withRetry,
            Action<InterruptableAction, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create a "successful" user-defined action
            ActionProxy action = new ActionProxy(() => Operation.Null());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>();
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAction
            InterruptableAction reliableAction = withRetry(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            exceptionPolicy.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                invoke(reliableAction, tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(1, action         .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetry_Failure

        private void WithRetry_Failure(
            Func<Action, int, ExceptionPolicy, DelayPolicy, InterruptableAction> withRetry,
            Action<InterruptableAction, CancellationToken> invoke)
            => WithRetry_Failure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                });

        private void WithRetry_Failure(
            Func<Action, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction> withRetry,
            Action<InterruptableAction, CancellationToken> invoke)
            => WithRetry_Failure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, Exception>();
                    return delayPolicy;
                });

        private void WithRetry_Failure<T>(
            Func<Action, int, ExceptionPolicy, T, InterruptableAction> withRetry,
            Action<InterruptableAction, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action
            ActionProxy action = new ActionProxy(() => throw new InvalidOperationException());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Fail<InvalidOperationException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAction
            InterruptableAction reliableAction = withRetry(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            exceptionPolicy.Invoking += Expect.Exception(typeof(InvalidOperationException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableAction, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(1, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetry_EventualSuccess

        private void WithRetry_EventualSuccess(
            Func<Action, int, ExceptionPolicy, DelayPolicy, InterruptableAction> withRetry,
            Action<InterruptableAction, CancellationToken> invoke)
            => WithRetry_EventualSuccess(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetry_EventualSuccess(
            Func<Action, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction> withRetry,
            Action<InterruptableAction, CancellationToken> invoke)
            => WithRetry_EventualSuccess(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetry_EventualSuccess<T>(
            Func<Action, int, ExceptionPolicy, T, InterruptableAction> withRetry,
            Action<InterruptableAction, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create a "successful" user-defined action that completes after 1 IOException
            Action flakyAction = FlakyAction.Create<IOException>(1);
            ActionProxy action = new ActionProxy(() => flakyAction());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAction
            InterruptableAction reliableAction = withRetry(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.AfterDelay(Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                invoke(reliableAction, tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(2, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(1, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetry_EventualFailure

        private void WithRetry_EventualFailure(
            Func<Action, int, ExceptionPolicy, DelayPolicy, InterruptableAction> withRetry,
            Action<InterruptableAction, CancellationToken> invoke)
            => WithRetry_EventualFailure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetry_EventualFailure(
            Func<Action, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction> withRetry,
            Action<InterruptableAction, CancellationToken> invoke)
            => WithRetry_EventualFailure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetry_EventualFailure<T>(
            Func<Action, int, ExceptionPolicy, T, InterruptableAction> withRetry,
            Action<InterruptableAction, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that fails after 2 transient exceptions
            Action flakyAction = FlakyAction.Create<IOException, InvalidOperationException>(2);
            ActionProxy action = new ActionProxy(() => flakyAction());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAction
            InterruptableAction reliableAction = withRetry(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.AfterDelay(Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exceptions(typeof(IOException), typeof(InvalidOperationException), 2);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableAction, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion

        #region WithRetry_RetriesExhausted

        private void WithRetry_RetriesExhausted(
            Func<Action, int, ExceptionPolicy, DelayPolicy, InterruptableAction> withRetry,
            Action<InterruptableAction, CancellationToken> invoke)
            => WithRetry_RetriesExhausted(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetry_RetriesExhausted(
            Func<Action, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction> withRetry,
            Action<InterruptableAction, CancellationToken> invoke)
            => WithRetry_RetriesExhausted(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetry_RetriesExhausted<T>(
            Func<Action, int, ExceptionPolicy, T, InterruptableAction> withRetry,
            Action<InterruptableAction, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that exhausts the configured number of retries
            ActionProxy action = new ActionProxy(() => throw new IOException());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAction
            InterruptableAction reliableAction = withRetry(
                action.Invoke,
                2,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.AfterDelay(Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<IOException>(() => invoke(reliableAction, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion

        #region WithRetry_Canceled

        private void WithRetry_Canceled(Func<Action, int, ExceptionPolicy, DelayPolicy, InterruptableAction> withRetry)
            => WithRetry_Canceled(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetry_Canceled(Func<Action, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction> withRetry)
            => WithRetry_Canceled(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetry_Canceled<T>(Func<Action, int, ExceptionPolicy, T, InterruptableAction> withRetry, Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            using ManualResetEvent        cancellationTrigger = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource         = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined action that continues to fail with transient exceptions until it's canceled
            ActionProxy action = new ActionProxy(() => throw new IOException());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAction
            InterruptableAction reliableAction = withRetry(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.AfterDelay(Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Trigger the event upon retry
            action.Invoking += (c) =>
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
            Assert.That.ThrowsException<OperationCanceledException>(() => reliableAction(tokenSource.Token));

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
