// Generated from AsyncAction.Extensions.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    [TestClass]
    public partial class AsyncActionExtensionsTest
    {
        [TestMethod]
        public void WithAsyncRetry_DelayPolicy()
        {
            AsyncAction nullAction = null;
            AsyncAction action     = () => Operation.NullAsync();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithAsyncRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithAsyncRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null ));

            Action<InterruptableAsyncAction, CancellationToken> invoke;
            Func<AsyncAction, int, ExceptionPolicy, DelayPolicy, InterruptableAsyncAction> withRetry = (a, r, e, d) => a.WithAsyncRetry(r, e, d);

            // Without Token
            invoke = (action, token) => action();

            WithAsyncRetry_Success         (withRetry, invoke);
            WithAsyncRetry_Failure         (withRetry, invoke);
            WithAsyncRetry_EventualSuccess (withRetry, invoke);
            WithAsyncRetry_EventualFailure (withRetry, invoke);
            WithAsyncRetry_RetriesExhausted(withRetry, invoke);

            // With Token
            invoke = (action, token) => action(token);

            WithAsyncRetry_Success         (withRetry, invoke);
            WithAsyncRetry_Failure         (withRetry, invoke);
            WithAsyncRetry_EventualSuccess (withRetry, invoke);
            WithAsyncRetry_EventualFailure (withRetry, invoke);
            WithAsyncRetry_RetriesExhausted(withRetry, invoke);
            WithAsyncRetry_Canceled        (withRetry);
        }

        [TestMethod]
        public void WithAsyncRetry_ComplexDelayPolicy()
        {
            AsyncAction nullAction = null;
            AsyncAction action     = () => Operation.NullAsync();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithAsyncRetry( 4, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithAsyncRetry(-2, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, null                       , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy)null));

            Action<InterruptableAsyncAction, CancellationToken> invoke;
            Func<AsyncAction, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAsyncAction> withRetry = (a, r, e, d) => a.WithAsyncRetry(r, e, d);

            // Without Token
            invoke = (action, token) => action();

            WithAsyncRetry_Success         (withRetry, invoke);
            WithAsyncRetry_Failure         (withRetry, invoke);
            WithAsyncRetry_EventualSuccess (withRetry, invoke);
            WithAsyncRetry_EventualFailure (withRetry, invoke);
            WithAsyncRetry_RetriesExhausted(withRetry, invoke);

            // With Token
            invoke = (action, token) => action(token);

            WithAsyncRetry_Success         (withRetry, invoke);
            WithAsyncRetry_Failure         (withRetry, invoke);
            WithAsyncRetry_EventualSuccess (withRetry, invoke);
            WithAsyncRetry_EventualFailure (withRetry, invoke);
            WithAsyncRetry_RetriesExhausted(withRetry, invoke);
            WithAsyncRetry_Canceled        (withRetry);
        }

        #region WithAsyncRetry_Success

        private void WithAsyncRetry_Success(
            Func<AsyncAction, int, ExceptionPolicy, DelayPolicy, InterruptableAsyncAction> withAsyncRetry,
            Action<InterruptableAsyncAction, CancellationToken> invoke)
            => WithAsyncRetry_Success(
                (a, r, e, d) => withAsyncRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                });

        private void WithAsyncRetry_Success(
            Func<AsyncAction, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAsyncAction> withAsyncRetry,
            Action<InterruptableAsyncAction, CancellationToken> invoke)
            => WithAsyncRetry_Success(
                (a, r, e, d) => withAsyncRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, Exception>();
                    return delayPolicy;
                });

        private void WithAsyncRetry_Success<T>(
            Func<AsyncAction, int, ExceptionPolicy, T, InterruptableAsyncAction> withAsyncRetry,
            Action<InterruptableAsyncAction, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create a "successful" user-defined action
            AsyncActionProxy action = new AsyncActionProxy(async () => await Operation.NullAsync().ConfigureAwait(false));

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>();
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAsyncAction
            InterruptableAsyncAction reliableAction = withAsyncRetry(
                action.InvokeAsync,
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

        #region WithAsyncRetry_Failure

        private void WithAsyncRetry_Failure(
            Func<AsyncAction, int, ExceptionPolicy, DelayPolicy, InterruptableAsyncAction> withAsyncRetry,
            Action<InterruptableAsyncAction, CancellationToken> invoke)
            => WithAsyncRetry_Failure(
                (a, r, e, d) => withAsyncRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                });

        private void WithAsyncRetry_Failure(
            Func<AsyncAction, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAsyncAction> withAsyncRetry,
            Action<InterruptableAsyncAction, CancellationToken> invoke)
            => WithAsyncRetry_Failure(
                (a, r, e, d) => withAsyncRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, Exception>();
                    return delayPolicy;
                });

        private void WithAsyncRetry_Failure<T>(
            Func<AsyncAction, int, ExceptionPolicy, T, InterruptableAsyncAction> withAsyncRetry,
            Action<InterruptableAsyncAction, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action
            AsyncActionProxy action = new AsyncActionProxy(async () => await Task.Run(() => throw new InvalidOperationException()).ConfigureAwait(false));

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Fail<InvalidOperationException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAsyncAction
            InterruptableAsyncAction reliableAction = withAsyncRetry(
                action.InvokeAsync,
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

        #region WithAsyncRetry_EventualSuccess

        private void WithAsyncRetry_EventualSuccess(
            Func<AsyncAction, int, ExceptionPolicy, DelayPolicy, InterruptableAsyncAction> withAsyncRetry,
            Action<InterruptableAsyncAction, CancellationToken> invoke)
            => WithAsyncRetry_EventualSuccess(
                (a, r, e, d) => withAsyncRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithAsyncRetry_EventualSuccess(
            Func<AsyncAction, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAsyncAction> withAsyncRetry,
            Action<InterruptableAsyncAction, CancellationToken> invoke)
            => WithAsyncRetry_EventualSuccess(
                (a, r, e, d) => withAsyncRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithAsyncRetry_EventualSuccess<T>(
            Func<AsyncAction, int, ExceptionPolicy, T, InterruptableAsyncAction> withAsyncRetry,
            Action<InterruptableAsyncAction, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create a "successful" user-defined action that completes after 1 IOException
            Action flakyAction = FlakyAction.Create<IOException>(1);
            AsyncActionProxy action = new AsyncActionProxy(async () => await Task.Run(flakyAction).ConfigureAwait(false));

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAsyncAction
            InterruptableAsyncAction reliableAction = withAsyncRetry(
                action.InvokeAsync,
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

        #region WithAsyncRetry_EventualFailure

        private void WithAsyncRetry_EventualFailure(
            Func<AsyncAction, int, ExceptionPolicy, DelayPolicy, InterruptableAsyncAction> withAsyncRetry,
            Action<InterruptableAsyncAction, CancellationToken> invoke)
            => WithAsyncRetry_EventualFailure(
                (a, r, e, d) => withAsyncRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithAsyncRetry_EventualFailure(
            Func<AsyncAction, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAsyncAction> withAsyncRetry,
            Action<InterruptableAsyncAction, CancellationToken> invoke)
            => WithAsyncRetry_EventualFailure(
                (a, r, e, d) => withAsyncRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithAsyncRetry_EventualFailure<T>(
            Func<AsyncAction, int, ExceptionPolicy, T, InterruptableAsyncAction> withAsyncRetry,
            Action<InterruptableAsyncAction, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that fails after 2 transient exceptions
            Action flakyAction = FlakyAction.Create<IOException, InvalidOperationException>(2);
            AsyncActionProxy action = new AsyncActionProxy(async () => await Task.Run(flakyAction).ConfigureAwait(false));

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAsyncAction
            InterruptableAsyncAction reliableAction = withAsyncRetry(
                action.InvokeAsync,
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

        #region WithAsyncRetry_RetriesExhausted

        private void WithAsyncRetry_RetriesExhausted(
            Func<AsyncAction, int, ExceptionPolicy, DelayPolicy, InterruptableAsyncAction> withAsyncRetry,
            Action<InterruptableAsyncAction, CancellationToken> invoke)
            => WithAsyncRetry_RetriesExhausted(
                (a, r, e, d) => withAsyncRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithAsyncRetry_RetriesExhausted(
            Func<AsyncAction, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAsyncAction> withAsyncRetry,
            Action<InterruptableAsyncAction, CancellationToken> invoke)
            => WithAsyncRetry_RetriesExhausted(
                (a, r, e, d) => withAsyncRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithAsyncRetry_RetriesExhausted<T>(
            Func<AsyncAction, int, ExceptionPolicy, T, InterruptableAsyncAction> withAsyncRetry,
            Action<InterruptableAsyncAction, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that exhausts the configured number of retries
            AsyncActionProxy action = new AsyncActionProxy(async () => await Task.Run(() => throw new IOException()).ConfigureAwait(false));

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAsyncAction
            InterruptableAsyncAction reliableAction = withAsyncRetry(
                action.InvokeAsync,
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

        #region WithAsyncRetry_Canceled

        private void WithAsyncRetry_Canceled(Func<AsyncAction, int, ExceptionPolicy, DelayPolicy, InterruptableAsyncAction> withAsyncRetry)
            => WithAsyncRetry_Canceled(
                (a, r, e, d) => withAsyncRetry(a, r, e, d.Invoke),
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithAsyncRetry_Canceled(Func<AsyncAction, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAsyncAction> withAsyncRetry)
            => WithAsyncRetry_Canceled(
                (a, r, e, d) => withAsyncRetry(a, r, e, d.Invoke),
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithAsyncRetry_Canceled<T>(Func<AsyncAction, int, ExceptionPolicy, T, InterruptableAsyncAction> withAsyncRetry, Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            using ManualResetEvent        cancellationTrigger = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource         = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined action that continues to fail with transient exceptions until it's canceled
            AsyncActionProxy action = new AsyncActionProxy(async () => await Task.Run(() => throw new IOException()).ConfigureAwait(false));

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAction
            InterruptableAsyncAction reliableAction = withAsyncRetry(
                action.InvokeAsync,
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
