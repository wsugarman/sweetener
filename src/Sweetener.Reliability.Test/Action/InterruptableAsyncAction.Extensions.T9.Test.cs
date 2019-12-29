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
        public void WithAsyncRetryT9_DelayPolicy()
        {
            InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> nullAction = null;
            InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> action     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token) => await Operation.NullAsync().ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithAsyncRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithAsyncRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null ));

            Action<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke;
            Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, DelayPolicy, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> withAsyncRetry = (a, r, e, d) => a.WithAsyncRetry(r, e, d);

            // Without Token
            invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9).Wait();

            WithRetryT9_Success         (withAsyncRetry, invoke);
            WithRetryT9_Failure         (withAsyncRetry, invoke);
            WithRetryT9_EventualSuccess (withAsyncRetry, invoke);
            WithRetryT9_EventualFailure (withAsyncRetry, invoke);
            WithRetryT9_RetriesExhausted(withAsyncRetry, invoke);

            // With Token
            invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token).Wait(token);

            WithRetryT9_Success         (withAsyncRetry, invoke);
            WithRetryT9_Failure         (withAsyncRetry, invoke);
            WithRetryT9_EventualSuccess (withAsyncRetry, invoke);
            WithRetryT9_EventualFailure (withAsyncRetry, invoke);
            WithRetryT9_RetriesExhausted(withAsyncRetry, invoke);
            WithRetryT9_Canceled_Action (withAsyncRetry, invoke);
            WithRetryT9_Canceled_Delay  (withAsyncRetry, invoke);
        }

        [TestMethod]
        public void WithAsyncRetryT9_ComplexDelayPolicy()
        {
            InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> nullAction = null;
            InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> action     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token) => await Operation.NullAsync().ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithAsyncRetry( 4, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithAsyncRetry(-2, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, null                       , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy)null));

            Action<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke;
            Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> withAsyncRetry = (a, r, e, d) => a.WithAsyncRetry(r, e, d);

            // Without Token
            invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9).Wait();

            WithRetryT9_Success         (withAsyncRetry, invoke);
            WithRetryT9_Failure         (withAsyncRetry, invoke);
            WithRetryT9_EventualSuccess (withAsyncRetry, invoke);
            WithRetryT9_EventualFailure (withAsyncRetry, invoke);
            WithRetryT9_RetriesExhausted(withAsyncRetry, invoke);

            // With Token
            invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token).Wait(token);

            WithRetryT9_Success         (withAsyncRetry, invoke);
            WithRetryT9_Failure         (withAsyncRetry, invoke);
            WithRetryT9_EventualSuccess (withAsyncRetry, invoke);
            WithRetryT9_EventualFailure (withAsyncRetry, invoke);
            WithRetryT9_RetriesExhausted(withAsyncRetry, invoke);
            WithRetryT9_Canceled_Action (withAsyncRetry, invoke);
            WithRetryT9_Canceled_Delay  (withAsyncRetry, invoke);
        }

        #region WithRetryT9_Success

        private void WithRetryT9_Success<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke)
            => WithRetryT9_Success(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                });

        private void WithRetryT9_Success<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke)
            => WithRetryT9_Success(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, Exception>();
                    return delayPolicy;
                });

        private void WithRetryT9_Success<TDelayPolicy, TAction>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create a "successful" user-defined action
            AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> action = new AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token) => await Operation.NullAsync().ConfigureAwait(false));

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
            action         .Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken>(Arguments.Validate);
            exceptionPolicy.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(1, action         .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT9_Failure

        private void WithRetryT9_Failure<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke)
            => WithRetryT9_Failure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                });

        private void WithRetryT9_Failure<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke)
            => WithRetryT9_Failure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, Exception>();
                    return delayPolicy;
                });

        private void WithRetryT9_Failure<TDelayPolicy, TAction>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action
            AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> action = new AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token) => await Task.Run(() => throw new InvalidOperationException()).ConfigureAwait(false));

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
            action         .Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken>(Arguments.Validate);
            exceptionPolicy.Invoking += Expect.Exception(typeof(InvalidOperationException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(1, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT9_EventualSuccess

        private void WithRetryT9_EventualSuccess<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke)
            => WithRetryT9_EventualSuccess(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT9_EventualSuccess<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke)
            => WithRetryT9_EventualSuccess(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT9_EventualSuccess<TDelayPolicy, TAction>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create a "successful" user-defined action that completes after 1 IOException
            Action flakyAction = FlakyAction.Create<IOException>(1);
            AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> action = new AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token) => await Task.Run(flakyAction).ConfigureAwait(false));

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
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(2, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(1, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT9_EventualFailure

        private void WithRetryT9_EventualFailure<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke)
            => WithRetryT9_EventualFailure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT9_EventualFailure<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke)
            => WithRetryT9_EventualFailure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT9_EventualFailure<TDelayPolicy, TAction>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that fails after 2 transient exceptions
            Action flakyAction = FlakyAction.Create<IOException, InvalidOperationException>(2);
            AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> action = new AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token) => await Task.Run(flakyAction).ConfigureAwait(false));

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
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exceptions(typeof(IOException), typeof(InvalidOperationException), 2);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion

        #region WithRetryT9_RetriesExhausted

        private void WithRetryT9_RetriesExhausted<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke)
            => WithRetryT9_RetriesExhausted(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT9_RetriesExhausted<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke)
            => WithRetryT9_RetriesExhausted(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT9_RetriesExhausted<TDelayPolicy, TAction>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that exhausts the configured number of retries
            AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> action = new AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token) => await Task.Run(() => throw new IOException()).ConfigureAwait(false));

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
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<IOException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion

        #region WithRetryT9_Canceled_Action

        private void WithRetryT9_Canceled_Action<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke)
            => WithRetryT9_Canceled_Action(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT9_Canceled_Action<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke)
            => WithRetryT9_Canceled_Action(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT9_Canceled_Action<TDelayPolicy, TAction>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
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
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            TDelayPolicy delayPolicy = delayPolicyFactory();

            // Create the reliable action
            TAction reliableAction = withRetry(
                action.InvokeAsync,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Cancel the action on its 2nd attempt
            action         .Invoking += (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, t, c) =>
            {
                if (c.Calls == 2)
                    tokenSource.Cancel();
            };

            // Invoke
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            Assert.AreEqual(2, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(1, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT9_Canceled_Delay

        private void WithRetryT9_Canceled_Delay<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke)
            => WithRetryT9_Canceled_Delay(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT9_Canceled_Delay<T>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke)
            => WithRetryT9_Canceled_Delay(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT9_Canceled_Delay<TDelayPolicy, TAction>(
            Func<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined action that continues to fail with transient exceptions until it's canceled
            AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> action = new AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token) => await Task.Run(() => throw new IOException()).ConfigureAwait(false));

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
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Cancel the delay on its 2nd invocation
            // (We use the exception policy because there's no Invoking event on TDelay)
            exceptionPolicy.Invoking += (e, c) =>
            {
                if (c.Calls == 2)
                    tokenSource.Cancel();
            };

            // Begin the invocation
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            Assert.AreEqual(2, action          .Calls);
            Assert.AreEqual(2, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion
    }
}
