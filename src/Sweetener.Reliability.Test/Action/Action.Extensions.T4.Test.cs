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
        public void WithRetryT4_DelayPolicy()
        {
            Action<int, string, double, long> nullAction = null;
            Action<int, string, double, long> action     = (arg1, arg2, arg3, arg4) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null ));

            Action<InterruptableAction<int, string, double, long>, int, string, double, long, CancellationToken> invoke;
            Func<Action<int, string, double, long>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string, double, long>> withRetry = (a, r, e, d) => a.WithRetry(r, e, d);

            // Without Token
            invoke = (action, arg1, arg2, arg3, arg4, token) => action(arg1, arg2, arg3, arg4);

            WithRetryT4_Success         (withRetry, invoke);
            WithRetryT4_Failure         (withRetry, invoke);
            WithRetryT4_EventualSuccess (withRetry, invoke);
            WithRetryT4_EventualFailure (withRetry, invoke);
            WithRetryT4_RetriesExhausted(withRetry, invoke);

            // With Token
            invoke = (action, arg1, arg2, arg3, arg4, token) => action(arg1, arg2, arg3, arg4, token);

            WithRetryT4_Success         (withRetry, invoke);
            WithRetryT4_Failure         (withRetry, invoke);
            WithRetryT4_EventualSuccess (withRetry, invoke);
            WithRetryT4_EventualFailure (withRetry, invoke);
            WithRetryT4_RetriesExhausted(withRetry, invoke);
            WithRetryT4_Canceled_Delay  (withRetry, invoke);
        }

        [TestMethod]
        public void WithRetryT4_ComplexDelayPolicy()
        {
            Action<int, string, double, long> nullAction = null;
            Action<int, string, double, long> action     = (arg1, arg2, arg3, arg4) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy)null));

            Action<InterruptableAction<int, string, double, long>, int, string, double, long, CancellationToken> invoke;
            Func<Action<int, string, double, long>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string, double, long>> withRetry = (a, r, e, d) => a.WithRetry(r, e, d);

            // Without Token
            invoke = (action, arg1, arg2, arg3, arg4, token) => action(arg1, arg2, arg3, arg4);

            WithRetryT4_Success         (withRetry, invoke);
            WithRetryT4_Failure         (withRetry, invoke);
            WithRetryT4_EventualSuccess (withRetry, invoke);
            WithRetryT4_EventualFailure (withRetry, invoke);
            WithRetryT4_RetriesExhausted(withRetry, invoke);

            // With Token
            invoke = (action, arg1, arg2, arg3, arg4, token) => action(arg1, arg2, arg3, arg4, token);

            WithRetryT4_Success         (withRetry, invoke);
            WithRetryT4_Failure         (withRetry, invoke);
            WithRetryT4_EventualSuccess (withRetry, invoke);
            WithRetryT4_EventualFailure (withRetry, invoke);
            WithRetryT4_RetriesExhausted(withRetry, invoke);
            WithRetryT4_Canceled_Delay  (withRetry, invoke);
        }

        [TestMethod]
        public void WithAsyncRetryT4_DelayPolicy()
        {
            Action<int, string, double, long> nullAction = null;
            Action<int, string, double, long> action     = (arg1, arg2, arg3, arg4) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithAsyncRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithAsyncRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null ));

            Action<InterruptableAsyncAction<int, string, double, long>, int, string, double, long, CancellationToken> invoke;
            Func<Action<int, string, double, long>, int, ExceptionPolicy, DelayPolicy, InterruptableAsyncAction<int, string, double, long>> withAsyncRetry = (a, r, e, d) => a.WithAsyncRetry(r, e, d);

            // Without Token
            invoke = (action, arg1, arg2, arg3, arg4, token) => action(arg1, arg2, arg3, arg4).Wait();

            WithRetryT4_Success         (withAsyncRetry, invoke);
            WithRetryT4_Failure         (withAsyncRetry, invoke);
            WithRetryT4_EventualSuccess (withAsyncRetry, invoke);
            WithRetryT4_EventualFailure (withAsyncRetry, invoke);
            WithRetryT4_RetriesExhausted(withAsyncRetry, invoke);

            // With Token
            invoke = (action, arg1, arg2, arg3, arg4, token) => action(arg1, arg2, arg3, arg4, token).Wait();

            WithRetryT4_Success         (withAsyncRetry, invoke);
            WithRetryT4_Failure         (withAsyncRetry, invoke);
            WithRetryT4_EventualSuccess (withAsyncRetry, invoke);
            WithRetryT4_EventualFailure (withAsyncRetry, invoke);
            WithRetryT4_RetriesExhausted(withAsyncRetry, invoke);
            WithRetryT4_Canceled_Delay  (withAsyncRetry, invoke);
        }

        [TestMethod]
        public void WithAsyncRetryT4_ComplexDelayPolicy()
        {
            Action<int, string, double, long> nullAction = null;
            Action<int, string, double, long> action     = (arg1, arg2, arg3, arg4) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithAsyncRetry( 4, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithAsyncRetry(-2, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, null                       , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy)null));

            Action<InterruptableAsyncAction<int, string, double, long>, int, string, double, long, CancellationToken> invoke;
            Func<Action<int, string, double, long>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAsyncAction<int, string, double, long>> withAsyncRetry = (a, r, e, d) => a.WithAsyncRetry(r, e, d);

            // Without Token
            invoke = (action, arg1, arg2, arg3, arg4, token) => action(arg1, arg2, arg3, arg4).Wait();

            WithRetryT4_Success         (withAsyncRetry, invoke);
            WithRetryT4_Failure         (withAsyncRetry, invoke);
            WithRetryT4_EventualSuccess (withAsyncRetry, invoke);
            WithRetryT4_EventualFailure (withAsyncRetry, invoke);
            WithRetryT4_RetriesExhausted(withAsyncRetry, invoke);

            // With Token
            invoke = (action, arg1, arg2, arg3, arg4, token) => action(arg1, arg2, arg3, arg4, token).Wait();

            WithRetryT4_Success         (withAsyncRetry, invoke);
            WithRetryT4_Failure         (withAsyncRetry, invoke);
            WithRetryT4_EventualSuccess (withAsyncRetry, invoke);
            WithRetryT4_EventualFailure (withAsyncRetry, invoke);
            WithRetryT4_RetriesExhausted(withAsyncRetry, invoke);
            WithRetryT4_Canceled_Delay  (withAsyncRetry, invoke);
        }

        #region WithRetryT4_Success

        private void WithRetryT4_Success<T>(
            Func<Action<int, string, double, long>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, CancellationToken> invoke)
            => WithRetryT4_Success(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                });

        private void WithRetryT4_Success<T>(
            Func<Action<int, string, double, long>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, CancellationToken> invoke)
            => WithRetryT4_Success(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, Exception>();
                    return delayPolicy;
                });

        private void WithRetryT4_Success<TDelayPolicy, TAction>(
            Func<Action<int, string, double, long>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create a "successful" user-defined action
            ActionProxy<int, string, double, long> action = new ActionProxy<int, string, double, long>((arg1, arg2, arg3, arg4) => Operation.Null());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>();
            TDelayPolicy delayPolicy = delayPolicyFactory();

            // Create the reliable action
            TAction reliableAction = withRetry(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.Arguments<int, string, double, long>(Arguments.Validate);
            exceptionPolicy.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                invoke(reliableAction, 42, "foo", 3.14D, 1000L, tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(1, action         .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT4_Failure

        private void WithRetryT4_Failure<T>(
            Func<Action<int, string, double, long>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, CancellationToken> invoke)
            => WithRetryT4_Failure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                });

        private void WithRetryT4_Failure<T>(
            Func<Action<int, string, double, long>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, CancellationToken> invoke)
            => WithRetryT4_Failure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, Exception>();
                    return delayPolicy;
                });

        private void WithRetryT4_Failure<TDelayPolicy, TAction>(
            Func<Action<int, string, double, long>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action
            ActionProxy<int, string, double, long> action = new ActionProxy<int, string, double, long>((arg1, arg2, arg3, arg4) => throw new InvalidOperationException());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Fail<InvalidOperationException>().Invoke);
            TDelayPolicy delayPolicy = delayPolicyFactory();

            // Create the reliable action
            TAction reliableAction = withRetry(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.Arguments<int, string, double, long>(Arguments.Validate);
            exceptionPolicy.Invoking += Expect.Exception(typeof(InvalidOperationException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(1, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT4_EventualSuccess

        private void WithRetryT4_EventualSuccess<T>(
            Func<Action<int, string, double, long>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, CancellationToken> invoke)
            => WithRetryT4_EventualSuccess(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT4_EventualSuccess<T>(
            Func<Action<int, string, double, long>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, CancellationToken> invoke)
            => WithRetryT4_EventualSuccess(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT4_EventualSuccess<TDelayPolicy, TAction>(
            Func<Action<int, string, double, long>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create a "successful" user-defined action that completes after 1 IOException
            Action flakyAction = FlakyAction.Create<IOException>(1);
            ActionProxy<int, string, double, long> action = new ActionProxy<int, string, double, long>((arg1, arg2, arg3, arg4) => flakyAction());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            TDelayPolicy delayPolicy = delayPolicyFactory();

            // Create the reliable action
            TAction reliableAction = withRetry(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                invoke(reliableAction, 42, "foo", 3.14D, 1000L, tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(2, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(1, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT4_EventualFailure

        private void WithRetryT4_EventualFailure<T>(
            Func<Action<int, string, double, long>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, CancellationToken> invoke)
            => WithRetryT4_EventualFailure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT4_EventualFailure<T>(
            Func<Action<int, string, double, long>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, CancellationToken> invoke)
            => WithRetryT4_EventualFailure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT4_EventualFailure<TDelayPolicy, TAction>(
            Func<Action<int, string, double, long>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that fails after 2 transient exceptions
            Action flakyAction = FlakyAction.Create<IOException, InvalidOperationException>(2);
            ActionProxy<int, string, double, long> action = new ActionProxy<int, string, double, long>((arg1, arg2, arg3, arg4) => flakyAction());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            TDelayPolicy delayPolicy = delayPolicyFactory();

            // Create the reliable action
            TAction reliableAction = withRetry(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exceptions(typeof(IOException), typeof(InvalidOperationException), 2);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion

        #region WithRetryT4_RetriesExhausted

        private void WithRetryT4_RetriesExhausted<T>(
            Func<Action<int, string, double, long>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, CancellationToken> invoke)
            => WithRetryT4_RetriesExhausted(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT4_RetriesExhausted<T>(
            Func<Action<int, string, double, long>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, CancellationToken> invoke)
            => WithRetryT4_RetriesExhausted(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT4_RetriesExhausted<TDelayPolicy, TAction>(
            Func<Action<int, string, double, long>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that exhausts the configured number of retries
            ActionProxy<int, string, double, long> action = new ActionProxy<int, string, double, long>((arg1, arg2, arg3, arg4) => throw new IOException());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            TDelayPolicy delayPolicy = delayPolicyFactory();

            // Create the reliable action
            TAction reliableAction = withRetry(
                action.Invoke,
                2,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<IOException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion

        #region WithRetryT4_Canceled_Delay

        private void WithRetryT4_Canceled_Delay<T>(
            Func<Action<int, string, double, long>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, CancellationToken> invoke)
            => WithRetryT4_Canceled_Delay(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT4_Canceled_Delay<T>(
            Func<Action<int, string, double, long>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, CancellationToken> invoke)
            => WithRetryT4_Canceled_Delay(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT4_Canceled_Delay<TDelayPolicy, TAction>(
            Func<Action<int, string, double, long>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined action that continues to fail with transient exceptions until it's canceled
            ActionProxy<int, string, double, long> action = new ActionProxy<int, string, double, long>((arg1, arg2, arg3, arg4) => throw new IOException());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            TDelayPolicy delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAction
            TAction reliableAction = withRetry(
                action.Invoke,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Cancel the delay on its 2nd invocation
            // (We use the exception policy because there's no Invoking event on TDelay)
            exceptionPolicy.Invoking += (e, c) =>
            {
                if (c.Calls == 2)
                    tokenSource.Cancel();
            };

            // Begin the invocation
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            Assert.AreEqual(2, action          .Calls);
            Assert.AreEqual(2, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion
    }
}
