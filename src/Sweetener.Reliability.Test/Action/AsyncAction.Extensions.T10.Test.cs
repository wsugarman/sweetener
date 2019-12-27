// Generated from AsyncAction.Extensions.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    partial class AsyncActionExtensionsTest
    {
        [TestMethod]
        public void WithAsyncRetryT10_DelayPolicy()
        {
            AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> nullAction = null;
            AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> action     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => Operation.NullAsync();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithAsyncRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithAsyncRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null ));

            Action<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken> invoke;
            Func<AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, ExceptionPolicy, DelayPolicy, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>> withRetry = (a, r, e, d) => a.WithAsyncRetry(r, e, d);

            // Without Token
            invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

            WithAsyncRetryT10_Success         (withRetry, invoke);
            WithAsyncRetryT10_Failure         (withRetry, invoke);
            WithAsyncRetryT10_EventualSuccess (withRetry, invoke);
            WithAsyncRetryT10_EventualFailure (withRetry, invoke);
            WithAsyncRetryT10_RetriesExhausted(withRetry, invoke);

            // With Token
            invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, token);

            WithAsyncRetryT10_Success         (withRetry, invoke);
            WithAsyncRetryT10_Failure         (withRetry, invoke);
            WithAsyncRetryT10_EventualSuccess (withRetry, invoke);
            WithAsyncRetryT10_EventualFailure (withRetry, invoke);
            WithAsyncRetryT10_RetriesExhausted(withRetry, invoke);
            WithAsyncRetryT10_Canceled        (withRetry);
        }

        [TestMethod]
        public void WithAsyncRetryT10_ComplexDelayPolicy()
        {
            AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> nullAction = null;
            AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> action     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => Operation.NullAsync();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithAsyncRetry( 4, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithAsyncRetry(-2, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, null                       , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy)null));

            Action<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken> invoke;
            Func<AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>> withRetry = (a, r, e, d) => a.WithAsyncRetry(r, e, d);

            // Without Token
            invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

            WithAsyncRetryT10_Success         (withRetry, invoke);
            WithAsyncRetryT10_Failure         (withRetry, invoke);
            WithAsyncRetryT10_EventualSuccess (withRetry, invoke);
            WithAsyncRetryT10_EventualFailure (withRetry, invoke);
            WithAsyncRetryT10_RetriesExhausted(withRetry, invoke);

            // With Token
            invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, token);

            WithAsyncRetryT10_Success         (withRetry, invoke);
            WithAsyncRetryT10_Failure         (withRetry, invoke);
            WithAsyncRetryT10_EventualSuccess (withRetry, invoke);
            WithAsyncRetryT10_EventualFailure (withRetry, invoke);
            WithAsyncRetryT10_RetriesExhausted(withRetry, invoke);
            WithAsyncRetryT10_Canceled        (withRetry);
        }

        #region WithAsyncRetryT10_Success

        private void WithAsyncRetryT10_Success(
            Func<AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, ExceptionPolicy, DelayPolicy, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>> withAsyncRetry,
            Action<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken> invoke)
            => WithAsyncRetryT10_Success(
                (a, r, e, d) => withAsyncRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                });

        private void WithAsyncRetryT10_Success(
            Func<AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>> withAsyncRetry,
            Action<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken> invoke)
            => WithAsyncRetryT10_Success(
                (a, r, e, d) => withAsyncRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, Exception>();
                    return delayPolicy;
                });

        private void WithAsyncRetryT10_Success<T>(
            Func<AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, ExceptionPolicy, T, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>> withAsyncRetry,
            Action<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create a "successful" user-defined action
            AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> action = new AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => await Operation.NullAsync().ConfigureAwait(false));

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>();
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAsyncAction
            InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> reliableAction = withAsyncRetry(
                action.InvokeAsync,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(Arguments.Validate);
            exceptionPolicy.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(1, action         .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithAsyncRetryT10_Failure

        private void WithAsyncRetryT10_Failure(
            Func<AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, ExceptionPolicy, DelayPolicy, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>> withAsyncRetry,
            Action<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken> invoke)
            => WithAsyncRetryT10_Failure(
                (a, r, e, d) => withAsyncRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                });

        private void WithAsyncRetryT10_Failure(
            Func<AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>> withAsyncRetry,
            Action<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken> invoke)
            => WithAsyncRetryT10_Failure(
                (a, r, e, d) => withAsyncRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, Exception>();
                    return delayPolicy;
                });

        private void WithAsyncRetryT10_Failure<T>(
            Func<AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, ExceptionPolicy, T, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>> withAsyncRetry,
            Action<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action
            AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> action = new AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => await Task.Run(() => throw new InvalidOperationException()).ConfigureAwait(false));

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Fail<InvalidOperationException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAsyncAction
            InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> reliableAction = withAsyncRetry(
                action.InvokeAsync,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(Arguments.Validate);
            exceptionPolicy.Invoking += Expect.Exception(typeof(InvalidOperationException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(1, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithAsyncRetryT10_EventualSuccess

        private void WithAsyncRetryT10_EventualSuccess(
            Func<AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, ExceptionPolicy, DelayPolicy, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>> withAsyncRetry,
            Action<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken> invoke)
            => WithAsyncRetryT10_EventualSuccess(
                (a, r, e, d) => withAsyncRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithAsyncRetryT10_EventualSuccess(
            Func<AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>> withAsyncRetry,
            Action<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken> invoke)
            => WithAsyncRetryT10_EventualSuccess(
                (a, r, e, d) => withAsyncRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithAsyncRetryT10_EventualSuccess<T>(
            Func<AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, ExceptionPolicy, T, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>> withAsyncRetry,
            Action<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create a "successful" user-defined action that completes after 1 IOException
            Action flakyAction = FlakyAction.Create<IOException>(1);
            AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> action = new AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => await Task.Run(flakyAction).ConfigureAwait(false));

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAsyncAction
            InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> reliableAction = withAsyncRetry(
                action.InvokeAsync,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(2, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(1, delayPolicy    .Calls);
        }

        #endregion

        #region WithAsyncRetryT10_EventualFailure

        private void WithAsyncRetryT10_EventualFailure(
            Func<AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, ExceptionPolicy, DelayPolicy, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>> withAsyncRetry,
            Action<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken> invoke)
            => WithAsyncRetryT10_EventualFailure(
                (a, r, e, d) => withAsyncRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithAsyncRetryT10_EventualFailure(
            Func<AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>> withAsyncRetry,
            Action<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken> invoke)
            => WithAsyncRetryT10_EventualFailure(
                (a, r, e, d) => withAsyncRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithAsyncRetryT10_EventualFailure<T>(
            Func<AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, ExceptionPolicy, T, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>> withAsyncRetry,
            Action<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that fails after 2 transient exceptions
            Action flakyAction = FlakyAction.Create<IOException, InvalidOperationException>(2);
            AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> action = new AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => await Task.Run(flakyAction).ConfigureAwait(false));

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAsyncAction
            InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> reliableAction = withAsyncRetry(
                action.InvokeAsync,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exceptions(typeof(IOException), typeof(InvalidOperationException), 2);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion

        #region WithAsyncRetryT10_RetriesExhausted

        private void WithAsyncRetryT10_RetriesExhausted(
            Func<AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, ExceptionPolicy, DelayPolicy, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>> withAsyncRetry,
            Action<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken> invoke)
            => WithAsyncRetryT10_RetriesExhausted(
                (a, r, e, d) => withAsyncRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithAsyncRetryT10_RetriesExhausted(
            Func<AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>> withAsyncRetry,
            Action<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken> invoke)
            => WithAsyncRetryT10_RetriesExhausted(
                (a, r, e, d) => withAsyncRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithAsyncRetryT10_RetriesExhausted<T>(
            Func<AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, ExceptionPolicy, T, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>> withAsyncRetry,
            Action<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that exhausts the configured number of retries
            AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> action = new AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => await Task.Run(() => throw new IOException()).ConfigureAwait(false));

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAsyncAction
            InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> reliableAction = withAsyncRetry(
                action.InvokeAsync,
                2,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<IOException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion

        #region WithAsyncRetryT10_Canceled

        private void WithAsyncRetryT10_Canceled(Func<AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, ExceptionPolicy, DelayPolicy, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>> withAsyncRetry)
            => WithAsyncRetryT10_Canceled(
                (a, r, e, d) => withAsyncRetry(a, r, e, d.Invoke),
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithAsyncRetryT10_Canceled(Func<AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>> withAsyncRetry)
            => WithAsyncRetryT10_Canceled(
                (a, r, e, d) => withAsyncRetry(a, r, e, d.Invoke),
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithAsyncRetryT10_Canceled<T>(Func<AsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, ExceptionPolicy, T, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>> withAsyncRetry, Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            using ManualResetEvent        cancellationTrigger = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource         = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined action that continues to fail with transient exceptions until it's canceled
            AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> action = new AsyncActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => await Task.Run(() => throw new IOException()).ConfigureAwait(false));

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableAction
            InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> reliableAction = withAsyncRetry(
                action.InvokeAsync,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Trigger the event upon retry
            action.Invoking += (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, c) =>
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
            Assert.That.ThrowsException<OperationCanceledException>(() => reliableAction(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), tokenSource.Token));

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
