// Generated from InterruptableAsyncAction.Extensions.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    partial class InterruptableAsyncActionExtensionsTest
    {
        [TestMethod]
        public void WithAsyncRetryT5_DelayPolicy()
        {
            InterruptableAsyncAction<int, string, double, long, ushort> nullAction = null;
            InterruptableAsyncAction<int, string, double, long, ushort> action     = async (arg1, arg2, arg3, arg4, arg5, token) => await Operation.NullAsync().ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithAsyncRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithAsyncRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null ));

            Action<InterruptableAsyncAction<int, string, double, long, ushort>, int, string, double, long, ushort, CancellationToken> invoke;
            Func<InterruptableAsyncAction<int, string, double, long, ushort>, int, ExceptionPolicy, DelayPolicy, InterruptableAsyncAction<int, string, double, long, ushort>> withAsyncRetry = (a, r, e, d) => a.WithAsyncRetry(r, e, d);

            // Without Token
            invoke = (action, arg1, arg2, arg3, arg4, arg5, token) => action(arg1, arg2, arg3, arg4, arg5).Wait();

            WithRetryT5_Success         (withAsyncRetry, invoke);
            WithRetryT5_Failure         (withAsyncRetry, invoke);
            WithRetryT5_EventualSuccess (withAsyncRetry, invoke);
            WithRetryT5_EventualFailure (withAsyncRetry, invoke);
            WithRetryT5_RetriesExhausted(withAsyncRetry, invoke);

            // With Token
            invoke = (action, arg1, arg2, arg3, arg4, arg5, token) => action(arg1, arg2, arg3, arg4, arg5, token).Wait(token);

            WithRetryT5_Success         (withAsyncRetry, invoke);
            WithRetryT5_Failure         (withAsyncRetry, invoke);
            WithRetryT5_EventualSuccess (withAsyncRetry, invoke);
            WithRetryT5_EventualFailure (withAsyncRetry, invoke);
            WithRetryT5_RetriesExhausted(withAsyncRetry, invoke);
            WithRetryT5_Canceled_Action (withAsyncRetry, invoke);
            WithRetryT5_Canceled_Delay  (withAsyncRetry, invoke);
        }

        [TestMethod]
        public void WithAsyncRetryT5_ComplexDelayPolicy()
        {
            InterruptableAsyncAction<int, string, double, long, ushort> nullAction = null;
            InterruptableAsyncAction<int, string, double, long, ushort> action     = async (arg1, arg2, arg3, arg4, arg5, token) => await Operation.NullAsync().ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithAsyncRetry( 4, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithAsyncRetry(-2, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, null                       , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy)null));

            Action<InterruptableAsyncAction<int, string, double, long, ushort>, int, string, double, long, ushort, CancellationToken> invoke;
            Func<InterruptableAsyncAction<int, string, double, long, ushort>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAsyncAction<int, string, double, long, ushort>> withAsyncRetry = (a, r, e, d) => a.WithAsyncRetry(r, e, d);

            // Without Token
            invoke = (action, arg1, arg2, arg3, arg4, arg5, token) => action(arg1, arg2, arg3, arg4, arg5).Wait();

            WithRetryT5_Success         (withAsyncRetry, invoke);
            WithRetryT5_Failure         (withAsyncRetry, invoke);
            WithRetryT5_EventualSuccess (withAsyncRetry, invoke);
            WithRetryT5_EventualFailure (withAsyncRetry, invoke);
            WithRetryT5_RetriesExhausted(withAsyncRetry, invoke);

            // With Token
            invoke = (action, arg1, arg2, arg3, arg4, arg5, token) => action(arg1, arg2, arg3, arg4, arg5, token).Wait(token);

            WithRetryT5_Success         (withAsyncRetry, invoke);
            WithRetryT5_Failure         (withAsyncRetry, invoke);
            WithRetryT5_EventualSuccess (withAsyncRetry, invoke);
            WithRetryT5_EventualFailure (withAsyncRetry, invoke);
            WithRetryT5_RetriesExhausted(withAsyncRetry, invoke);
            WithRetryT5_Canceled_Action (withAsyncRetry, invoke);
            WithRetryT5_Canceled_Delay  (withAsyncRetry, invoke);
        }

        #region WithRetryT5_Success

        private void WithRetryT5_Success<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, CancellationToken> invoke)
            => WithRetryT5_Success(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                });

        private void WithRetryT5_Success<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, CancellationToken> invoke)
            => WithRetryT5_Success(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, Exception>();
                    return delayPolicy;
                });

        private void WithRetryT5_Success<TDelayPolicy, TAction>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create a "successful" user-defined action
            AsyncActionProxy<int, string, double, long, ushort, CancellationToken> action = new AsyncActionProxy<int, string, double, long, ushort, CancellationToken>(async (arg1, arg2, arg3, arg4, arg5, token) => await Operation.NullAsync().ConfigureAwait(false));

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>();
            TDelayPolicy delayPolicy = delayPolicyFactory();

            // Create the reliable action
            TAction reliableAction = withRetry(
                action.InvokeAsync,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.Arguments<int, string, double, long, ushort, CancellationToken>(Arguments.Validate);
            exceptionPolicy.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(1, action         .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT5_Failure

        private void WithRetryT5_Failure<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, CancellationToken> invoke)
            => WithRetryT5_Failure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                });

        private void WithRetryT5_Failure<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, CancellationToken> invoke)
            => WithRetryT5_Failure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, Exception>();
                    return delayPolicy;
                });

        private void WithRetryT5_Failure<TDelayPolicy, TAction>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action
            AsyncActionProxy<int, string, double, long, ushort, CancellationToken> action = new AsyncActionProxy<int, string, double, long, ushort, CancellationToken>(async (arg1, arg2, arg3, arg4, arg5, token) => await Task.Run(() => throw new InvalidOperationException()).ConfigureAwait(false));

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Fail<InvalidOperationException>().Invoke);
            TDelayPolicy delayPolicy = delayPolicyFactory();

            // Create the reliable action
            TAction reliableAction = withRetry(
                action.InvokeAsync,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.Arguments<int, string, double, long, ushort, CancellationToken>(Arguments.Validate);
            exceptionPolicy.Invoking += Expect.Exception(typeof(InvalidOperationException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(1, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT5_EventualSuccess

        private void WithRetryT5_EventualSuccess<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, CancellationToken> invoke)
            => WithRetryT5_EventualSuccess(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT5_EventualSuccess<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, CancellationToken> invoke)
            => WithRetryT5_EventualSuccess(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT5_EventualSuccess<TDelayPolicy, TAction>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create a "successful" user-defined action that completes after 1 IOException
            Action flakyAction = FlakyAction.Create<IOException>(1);
            AsyncActionProxy<int, string, double, long, ushort, CancellationToken> action = new AsyncActionProxy<int, string, double, long, ushort, CancellationToken>(async (arg1, arg2, arg3, arg4, arg5, token) => await Task.Run(flakyAction).ConfigureAwait(false));

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            TDelayPolicy delayPolicy = delayPolicyFactory();

            // Create the reliable action
            TAction reliableAction = withRetry(
                action.InvokeAsync,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(2, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(1, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT5_EventualFailure

        private void WithRetryT5_EventualFailure<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, CancellationToken> invoke)
            => WithRetryT5_EventualFailure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT5_EventualFailure<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, CancellationToken> invoke)
            => WithRetryT5_EventualFailure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT5_EventualFailure<TDelayPolicy, TAction>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that fails after 2 transient exceptions
            Action flakyAction = FlakyAction.Create<IOException, InvalidOperationException>(2);
            AsyncActionProxy<int, string, double, long, ushort, CancellationToken> action = new AsyncActionProxy<int, string, double, long, ushort, CancellationToken>(async (arg1, arg2, arg3, arg4, arg5, token) => await Task.Run(flakyAction).ConfigureAwait(false));

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            TDelayPolicy delayPolicy = delayPolicyFactory();

            // Create the reliable action
            TAction reliableAction = withRetry(
                action.InvokeAsync,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exceptions(typeof(IOException), typeof(InvalidOperationException), 2);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion

        #region WithRetryT5_RetriesExhausted

        private void WithRetryT5_RetriesExhausted<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, CancellationToken> invoke)
            => WithRetryT5_RetriesExhausted(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT5_RetriesExhausted<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, CancellationToken> invoke)
            => WithRetryT5_RetriesExhausted(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT5_RetriesExhausted<TDelayPolicy, TAction>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that exhausts the configured number of retries
            AsyncActionProxy<int, string, double, long, ushort, CancellationToken> action = new AsyncActionProxy<int, string, double, long, ushort, CancellationToken>(async (arg1, arg2, arg3, arg4, arg5, token) => await Task.Run(() => throw new IOException()).ConfigureAwait(false));

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            TDelayPolicy delayPolicy = delayPolicyFactory();

            // Create the reliable action
            TAction reliableAction = withRetry(
                action.InvokeAsync,
                2,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<IOException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion

        #region WithRetryT5_Canceled_Action

        private void WithRetryT5_Canceled_Action<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, CancellationToken> invoke)
            => WithRetryT5_Canceled_Action(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT5_Canceled_Action<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, CancellationToken> invoke)
            => WithRetryT5_Canceled_Action(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT5_Canceled_Action<TDelayPolicy, TAction>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create a user-defined action that will throw an exception depending on whether its canceled
            AsyncActionProxy<int, string, double, long, ushort, CancellationToken> action = new AsyncActionProxy<int, string, double, long, ushort, CancellationToken>(async (arg1, arg2, arg3, arg4, arg5, token) =>
            {
                await Task.CompletedTask;
                token.ThrowIfCancellationRequested();
                throw new IOException();
            });

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            TDelayPolicy delayPolicy = delayPolicyFactory();

            // Create the reliable action
            TAction reliableAction = withRetry(
                action.InvokeAsync,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Cancel the action on its 2nd attempt
            action         .Invoking += (arg1, arg2, arg3, arg4, arg5, t, c) =>
            {
                if (c.Calls == 2)
                    tokenSource.Cancel();
            };

            // Invoke
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            Assert.AreEqual(2, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(1, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT5_Canceled_Delay

        private void WithRetryT5_Canceled_Delay<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, CancellationToken> invoke)
            => WithRetryT5_Canceled_Delay(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT5_Canceled_Delay<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, CancellationToken> invoke)
            => WithRetryT5_Canceled_Delay(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT5_Canceled_Delay<TDelayPolicy, TAction>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined action that continues to fail with transient exceptions until it's canceled
            AsyncActionProxy<int, string, double, long, ushort, CancellationToken> action = new AsyncActionProxy<int, string, double, long, ushort, CancellationToken>(async (arg1, arg2, arg3, arg4, arg5, token) => await Task.Run(() => throw new IOException()).ConfigureAwait(false));

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            TDelayPolicy delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAction
            TAction reliableAction = withRetry(
                action.InvokeAsync,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Cancel the delay on its 2nd invocation
            // (We use the exception policy because there's no Invoking event on TDelay)
            exceptionPolicy.Invoking += (e, c) =>
            {
                if (c.Calls == 2)
                    tokenSource.Cancel();
            };

            // Begin the invocation
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            Assert.AreEqual(2, action          .Calls);
            Assert.AreEqual(2, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion
    }
}
