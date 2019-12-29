// Generated from InterruptableAction.Extensions.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    partial class InterruptableActionExtensionsTest
    {
        [TestMethod]
        public void WithRetryT3_DelayPolicy()
        {
            InterruptableAction<int, string, double> nullAction = null;
            InterruptableAction<int, string, double> action     = (arg1, arg2, arg3, token) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null ));

            Action<InterruptableAction<int, string, double>, int, string, double, CancellationToken> invoke;
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string, double>> withRetry = (a, r, e, d) => a.WithRetry(r, e, d);

            // Without Token
            invoke = (action, arg1, arg2, arg3, token) => action(arg1, arg2, arg3);

            WithRetryT3_Success         (withRetry, invoke);
            WithRetryT3_Failure         (withRetry, invoke);
            WithRetryT3_EventualSuccess (withRetry, invoke);
            WithRetryT3_EventualFailure (withRetry, invoke);
            WithRetryT3_RetriesExhausted(withRetry, invoke);

            // With Token
            invoke = (action, arg1, arg2, arg3, token) => action(arg1, arg2, arg3, token);

            WithRetryT3_Success         (withRetry, invoke);
            WithRetryT3_Failure         (withRetry, invoke);
            WithRetryT3_EventualSuccess (withRetry, invoke);
            WithRetryT3_EventualFailure (withRetry, invoke);
            WithRetryT3_RetriesExhausted(withRetry, invoke);
            WithRetryT3_Canceled_Action (withRetry, invoke);
            WithRetryT3_Canceled_Delay  (withRetry, invoke);
        }

        [TestMethod]
        public void WithRetryT3_ComplexDelayPolicy()
        {
            InterruptableAction<int, string, double> nullAction = null;
            InterruptableAction<int, string, double> action     = (arg1, arg2, arg3, token) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy)null));

            Action<InterruptableAction<int, string, double>, int, string, double, CancellationToken> invoke;
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string, double>> withRetry = (a, r, e, d) => a.WithRetry(r, e, d);

            // Without Token
            invoke = (action, arg1, arg2, arg3, token) => action(arg1, arg2, arg3);

            WithRetryT3_Success         (withRetry, invoke);
            WithRetryT3_Failure         (withRetry, invoke);
            WithRetryT3_EventualSuccess (withRetry, invoke);
            WithRetryT3_EventualFailure (withRetry, invoke);
            WithRetryT3_RetriesExhausted(withRetry, invoke);

            // With Token
            invoke = (action, arg1, arg2, arg3, token) => action(arg1, arg2, arg3, token);

            WithRetryT3_Success         (withRetry, invoke);
            WithRetryT3_Failure         (withRetry, invoke);
            WithRetryT3_EventualSuccess (withRetry, invoke);
            WithRetryT3_EventualFailure (withRetry, invoke);
            WithRetryT3_RetriesExhausted(withRetry, invoke);
            WithRetryT3_Canceled_Action (withRetry, invoke);
            WithRetryT3_Canceled_Delay  (withRetry, invoke);
        }

        [TestMethod]
        public void WithAsyncRetryT3_DelayPolicy()
        {
            InterruptableAction<int, string, double> nullAction = null;
            InterruptableAction<int, string, double> action     = (arg1, arg2, arg3, token) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithAsyncRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithAsyncRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null ));

            Action<InterruptableAsyncAction<int, string, double>, int, string, double, CancellationToken> invoke;
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, DelayPolicy, InterruptableAsyncAction<int, string, double>> withAsyncRetry = (a, r, e, d) => a.WithAsyncRetry(r, e, d);

            // Without Token
            invoke = (action, arg1, arg2, arg3, token) => action(arg1, arg2, arg3).Wait();

            WithRetryT3_Success         (withAsyncRetry, invoke);
            WithRetryT3_Failure         (withAsyncRetry, invoke);
            WithRetryT3_EventualSuccess (withAsyncRetry, invoke);
            WithRetryT3_EventualFailure (withAsyncRetry, invoke);
            WithRetryT3_RetriesExhausted(withAsyncRetry, invoke);

            // With Token
            invoke = (action, arg1, arg2, arg3, token) => action(arg1, arg2, arg3, token).Wait(token);

            WithRetryT3_Success         (withAsyncRetry, invoke);
            WithRetryT3_Failure         (withAsyncRetry, invoke);
            WithRetryT3_EventualSuccess (withAsyncRetry, invoke);
            WithRetryT3_EventualFailure (withAsyncRetry, invoke);
            WithRetryT3_RetriesExhausted(withAsyncRetry, invoke);
            WithRetryT3_Canceled_Action (withAsyncRetry, invoke);
            WithRetryT3_Canceled_Delay  (withAsyncRetry, invoke);
        }

        [TestMethod]
        public void WithAsyncRetryT3_ComplexDelayPolicy()
        {
            InterruptableAction<int, string, double> nullAction = null;
            InterruptableAction<int, string, double> action     = (arg1, arg2, arg3, token) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithAsyncRetry( 4, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithAsyncRetry(-2, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, null                       , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy)null));

            Action<InterruptableAsyncAction<int, string, double>, int, string, double, CancellationToken> invoke;
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAsyncAction<int, string, double>> withAsyncRetry = (a, r, e, d) => a.WithAsyncRetry(r, e, d);

            // Without Token
            invoke = (action, arg1, arg2, arg3, token) => action(arg1, arg2, arg3).Wait();

            WithRetryT3_Success         (withAsyncRetry, invoke);
            WithRetryT3_Failure         (withAsyncRetry, invoke);
            WithRetryT3_EventualSuccess (withAsyncRetry, invoke);
            WithRetryT3_EventualFailure (withAsyncRetry, invoke);
            WithRetryT3_RetriesExhausted(withAsyncRetry, invoke);

            // With Token
            invoke = (action, arg1, arg2, arg3, token) => action(arg1, arg2, arg3, token).Wait(token);

            WithRetryT3_Success         (withAsyncRetry, invoke);
            WithRetryT3_Failure         (withAsyncRetry, invoke);
            WithRetryT3_EventualSuccess (withAsyncRetry, invoke);
            WithRetryT3_EventualFailure (withAsyncRetry, invoke);
            WithRetryT3_RetriesExhausted(withAsyncRetry, invoke);
            WithRetryT3_Canceled_Action (withAsyncRetry, invoke);
            WithRetryT3_Canceled_Delay  (withAsyncRetry, invoke);
        }

        #region WithRetryT3_Success

        private void WithRetryT3_Success<T>(
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, CancellationToken> invoke)
            => WithRetryT3_Success(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                });

        private void WithRetryT3_Success<T>(
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, CancellationToken> invoke)
            => WithRetryT3_Success(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, Exception>();
                    return delayPolicy;
                });

        private void WithRetryT3_Success<TDelayPolicy, TAction>(
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create a "successful" user-defined action
            ActionProxy<int, string, double, CancellationToken> action = new ActionProxy<int, string, double, CancellationToken>((arg1, arg2, arg3, token) => Operation.Null());

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
            action         .Invoking += Expect.Arguments<int, string, double, CancellationToken>(Arguments.Validate);
            exceptionPolicy.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                invoke(reliableAction, 42, "foo", 3.14D, tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(1, action         .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT3_Failure

        private void WithRetryT3_Failure<T>(
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, CancellationToken> invoke)
            => WithRetryT3_Failure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                });

        private void WithRetryT3_Failure<T>(
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, CancellationToken> invoke)
            => WithRetryT3_Failure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, Exception>();
                    return delayPolicy;
                });

        private void WithRetryT3_Failure<TDelayPolicy, TAction>(
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action
            ActionProxy<int, string, double, CancellationToken> action = new ActionProxy<int, string, double, CancellationToken>((arg1, arg2, arg3, token) => throw new InvalidOperationException());

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
            action         .Invoking += Expect.Arguments<int, string, double, CancellationToken>(Arguments.Validate);
            exceptionPolicy.Invoking += Expect.Exception(typeof(InvalidOperationException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableAction, 42, "foo", 3.14D, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(1, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT3_EventualSuccess

        private void WithRetryT3_EventualSuccess<T>(
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, CancellationToken> invoke)
            => WithRetryT3_EventualSuccess(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT3_EventualSuccess<T>(
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, CancellationToken> invoke)
            => WithRetryT3_EventualSuccess(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT3_EventualSuccess<TDelayPolicy, TAction>(
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create a "successful" user-defined action that completes after 1 IOException
            Action flakyAction = FlakyAction.Create<IOException>(1);
            ActionProxy<int, string, double, CancellationToken> action = new ActionProxy<int, string, double, CancellationToken>((arg1, arg2, arg3, token) => flakyAction());

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
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                invoke(reliableAction, 42, "foo", 3.14D, tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(2, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(1, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT3_EventualFailure

        private void WithRetryT3_EventualFailure<T>(
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, CancellationToken> invoke)
            => WithRetryT3_EventualFailure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT3_EventualFailure<T>(
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, CancellationToken> invoke)
            => WithRetryT3_EventualFailure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT3_EventualFailure<TDelayPolicy, TAction>(
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that fails after 2 transient exceptions
            Action flakyAction = FlakyAction.Create<IOException, InvalidOperationException>(2);
            ActionProxy<int, string, double, CancellationToken> action = new ActionProxy<int, string, double, CancellationToken>((arg1, arg2, arg3, token) => flakyAction());

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
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exceptions(typeof(IOException), typeof(InvalidOperationException), 2);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableAction, 42, "foo", 3.14D, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion

        #region WithRetryT3_RetriesExhausted

        private void WithRetryT3_RetriesExhausted<T>(
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, CancellationToken> invoke)
            => WithRetryT3_RetriesExhausted(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT3_RetriesExhausted<T>(
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, CancellationToken> invoke)
            => WithRetryT3_RetriesExhausted(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT3_RetriesExhausted<TDelayPolicy, TAction>(
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that exhausts the configured number of retries
            ActionProxy<int, string, double, CancellationToken> action = new ActionProxy<int, string, double, CancellationToken>((arg1, arg2, arg3, token) => throw new IOException());

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
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<IOException>(() => invoke(reliableAction, 42, "foo", 3.14D, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion

        #region WithRetryT3_Canceled_Action

        private void WithRetryT3_Canceled_Action<T>(
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, CancellationToken> invoke)
            => WithRetryT3_Canceled_Action(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT3_Canceled_Action<T>(
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, CancellationToken> invoke)
            => WithRetryT3_Canceled_Action(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT3_Canceled_Action<TDelayPolicy, TAction>(
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create a user-defined action that will throw an exception depending on whether its canceled
            ActionProxy<int, string, double, CancellationToken> action = new ActionProxy<int, string, double, CancellationToken>((arg1, arg2, arg3, token) =>
            {
                token.ThrowIfCancellationRequested();
                throw new IOException();
            });

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
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Cancel the action on its 2nd attempt
            action         .Invoking += (arg1, arg2, arg3, t, c) =>
            {
                if (c.Calls == 2)
                    tokenSource.Cancel();
            };

            // Invoke
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableAction, 42, "foo", 3.14D, tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            Assert.AreEqual(2, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(1, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT3_Canceled_Delay

        private void WithRetryT3_Canceled_Delay<T>(
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, CancellationToken> invoke)
            => WithRetryT3_Canceled_Delay(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT3_Canceled_Delay<T>(
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, CancellationToken> invoke)
            => WithRetryT3_Canceled_Delay(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT3_Canceled_Delay<TDelayPolicy, TAction>(
            Func<InterruptableAction<int, string, double>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined action that continues to fail with transient exceptions until it's canceled
            ActionProxy<int, string, double, CancellationToken> action = new ActionProxy<int, string, double, CancellationToken>((arg1, arg2, arg3, token) => throw new IOException());

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
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Cancel the delay on its 2nd invocation
            // (We use the exception policy because there's no Invoking event on TDelay)
            exceptionPolicy.Invoking += (e, c) =>
            {
                if (c.Calls == 2)
                    tokenSource.Cancel();
            };

            // Begin the invocation
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableAction, 42, "foo", 3.14D, tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            Assert.AreEqual(2, action          .Calls);
            Assert.AreEqual(2, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion
    }
}
