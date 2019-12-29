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
        public void WithRetryT12_DelayPolicy()
        {
            InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> nullAction = null;
            InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> action     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null ));

            Action<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke;
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>> withRetry = (a, r, e, d) => a.WithRetry(r, e, d);

            // Without Token
            invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);

            WithRetryT12_Success         (withRetry, invoke);
            WithRetryT12_Failure         (withRetry, invoke);
            WithRetryT12_EventualSuccess (withRetry, invoke);
            WithRetryT12_EventualFailure (withRetry, invoke);
            WithRetryT12_RetriesExhausted(withRetry, invoke);

            // With Token
            invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token);

            WithRetryT12_Success         (withRetry, invoke);
            WithRetryT12_Failure         (withRetry, invoke);
            WithRetryT12_EventualSuccess (withRetry, invoke);
            WithRetryT12_EventualFailure (withRetry, invoke);
            WithRetryT12_RetriesExhausted(withRetry, invoke);
            WithRetryT12_Canceled_Action (withRetry, invoke);
            WithRetryT12_Canceled_Delay  (withRetry, invoke);
        }

        [TestMethod]
        public void WithRetryT12_ComplexDelayPolicy()
        {
            InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> nullAction = null;
            InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> action     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy)null));

            Action<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke;
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>> withRetry = (a, r, e, d) => a.WithRetry(r, e, d);

            // Without Token
            invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);

            WithRetryT12_Success         (withRetry, invoke);
            WithRetryT12_Failure         (withRetry, invoke);
            WithRetryT12_EventualSuccess (withRetry, invoke);
            WithRetryT12_EventualFailure (withRetry, invoke);
            WithRetryT12_RetriesExhausted(withRetry, invoke);

            // With Token
            invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token);

            WithRetryT12_Success         (withRetry, invoke);
            WithRetryT12_Failure         (withRetry, invoke);
            WithRetryT12_EventualSuccess (withRetry, invoke);
            WithRetryT12_EventualFailure (withRetry, invoke);
            WithRetryT12_RetriesExhausted(withRetry, invoke);
            WithRetryT12_Canceled_Action (withRetry, invoke);
            WithRetryT12_Canceled_Delay  (withRetry, invoke);
        }

        [TestMethod]
        public void WithAsyncRetryT12_DelayPolicy()
        {
            InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> nullAction = null;
            InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> action     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithAsyncRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithAsyncRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null ));

            Action<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke;
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, DelayPolicy, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>> withAsyncRetry = (a, r, e, d) => a.WithAsyncRetry(r, e, d);

            // Without Token
            invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12).Wait();

            WithRetryT12_Success         (withAsyncRetry, invoke);
            WithRetryT12_Failure         (withAsyncRetry, invoke);
            WithRetryT12_EventualSuccess (withAsyncRetry, invoke);
            WithRetryT12_EventualFailure (withAsyncRetry, invoke);
            WithRetryT12_RetriesExhausted(withAsyncRetry, invoke);

            // With Token
            invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token).Wait(token);

            WithRetryT12_Success         (withAsyncRetry, invoke);
            WithRetryT12_Failure         (withAsyncRetry, invoke);
            WithRetryT12_EventualSuccess (withAsyncRetry, invoke);
            WithRetryT12_EventualFailure (withAsyncRetry, invoke);
            WithRetryT12_RetriesExhausted(withAsyncRetry, invoke);
            WithRetryT12_Canceled_Action (withAsyncRetry, invoke);
            WithRetryT12_Canceled_Delay  (withAsyncRetry, invoke);
        }

        [TestMethod]
        public void WithAsyncRetryT12_ComplexDelayPolicy()
        {
            InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> nullAction = null;
            InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> action     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithAsyncRetry( 4, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithAsyncRetry(-2, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, null                       , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy)null));

            Action<InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke;
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAsyncAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>> withAsyncRetry = (a, r, e, d) => a.WithAsyncRetry(r, e, d);

            // Without Token
            invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12).Wait();

            WithRetryT12_Success         (withAsyncRetry, invoke);
            WithRetryT12_Failure         (withAsyncRetry, invoke);
            WithRetryT12_EventualSuccess (withAsyncRetry, invoke);
            WithRetryT12_EventualFailure (withAsyncRetry, invoke);
            WithRetryT12_RetriesExhausted(withAsyncRetry, invoke);

            // With Token
            invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token).Wait(token);

            WithRetryT12_Success         (withAsyncRetry, invoke);
            WithRetryT12_Failure         (withAsyncRetry, invoke);
            WithRetryT12_EventualSuccess (withAsyncRetry, invoke);
            WithRetryT12_EventualFailure (withAsyncRetry, invoke);
            WithRetryT12_RetriesExhausted(withAsyncRetry, invoke);
            WithRetryT12_Canceled_Action (withAsyncRetry, invoke);
            WithRetryT12_Canceled_Delay  (withAsyncRetry, invoke);
        }

        #region WithRetryT12_Success

        private void WithRetryT12_Success<T>(
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke)
            => WithRetryT12_Success(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                });

        private void WithRetryT12_Success<T>(
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke)
            => WithRetryT12_Success(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, Exception>();
                    return delayPolicy;
                });

        private void WithRetryT12_Success<TDelayPolicy, TAction>(
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create a "successful" user-defined action
            ActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> action = new ActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => Operation.Null());

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
            action         .Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken>(Arguments.Validate);
            exceptionPolicy.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(1, action         .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT12_Failure

        private void WithRetryT12_Failure<T>(
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke)
            => WithRetryT12_Failure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                });

        private void WithRetryT12_Failure<T>(
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke)
            => WithRetryT12_Failure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, Exception>();
                    return delayPolicy;
                });

        private void WithRetryT12_Failure<TDelayPolicy, TAction>(
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action
            ActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> action = new ActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => throw new InvalidOperationException());

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
            action         .Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken>(Arguments.Validate);
            exceptionPolicy.Invoking += Expect.Exception(typeof(InvalidOperationException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(1, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT12_EventualSuccess

        private void WithRetryT12_EventualSuccess<T>(
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke)
            => WithRetryT12_EventualSuccess(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT12_EventualSuccess<T>(
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke)
            => WithRetryT12_EventualSuccess(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT12_EventualSuccess<TDelayPolicy, TAction>(
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create a "successful" user-defined action that completes after 1 IOException
            Action flakyAction = FlakyAction.Create<IOException>(1);
            ActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> action = new ActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => flakyAction());

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
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(2, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(1, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT12_EventualFailure

        private void WithRetryT12_EventualFailure<T>(
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke)
            => WithRetryT12_EventualFailure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT12_EventualFailure<T>(
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke)
            => WithRetryT12_EventualFailure(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT12_EventualFailure<TDelayPolicy, TAction>(
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that fails after 2 transient exceptions
            Action flakyAction = FlakyAction.Create<IOException, InvalidOperationException>(2);
            ActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> action = new ActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => flakyAction());

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
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exceptions(typeof(IOException), typeof(InvalidOperationException), 2);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion

        #region WithRetryT12_RetriesExhausted

        private void WithRetryT12_RetriesExhausted<T>(
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke)
            => WithRetryT12_RetriesExhausted(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT12_RetriesExhausted<T>(
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke)
            => WithRetryT12_RetriesExhausted(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>((i, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT12_RetriesExhausted<TDelayPolicy, TAction>(
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that exhausts the configured number of retries
            ActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> action = new ActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => throw new IOException());

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
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<IOException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion

        #region WithRetryT12_Canceled_Action

        private void WithRetryT12_Canceled_Action<T>(
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke)
            => WithRetryT12_Canceled_Action(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT12_Canceled_Action<T>(
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke)
            => WithRetryT12_Canceled_Action(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT12_Canceled_Action<TDelayPolicy, TAction>(
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create a user-defined action that will throw an exception depending on whether its canceled
            ActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> action = new ActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) =>
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
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Cancel the action on its 2nd attempt
            action         .Invoking += (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, t, c) =>
            {
                if (c.Calls == 2)
                    tokenSource.Cancel();
            };

            // Invoke
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            Assert.AreEqual(2, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(1, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT12_Canceled_Delay

        private void WithRetryT12_Canceled_Delay<T>(
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, DelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke)
            => WithRetryT12_Canceled_Delay(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT12_Canceled_Delay<T>(
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, ComplexDelayPolicy, T> withRetry,
            Action<T, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke)
            => WithRetryT12_Canceled_Delay(
                (a, r, e, d) => withRetry(a, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.ExceptionAsc(typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT12_Canceled_Delay<TDelayPolicy, TAction>(
            Func<InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke,
            Func<TDelayPolicy> delayPolicyFactory)
            where TDelayPolicy : DelegateProxy
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined action that continues to fail with transient exceptions until it's canceled
            ActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> action = new ActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => throw new IOException());

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
            action         .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Cancel the delay on its 2nd invocation
            // (We use the exception policy because there's no Invoking event on TDelay)
            exceptionPolicy.Invoking += (e, c) =>
            {
                if (c.Calls == 2)
                    tokenSource.Cancel();
            };

            // Begin the invocation
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            Assert.AreEqual(2, action          .Calls);
            Assert.AreEqual(2, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion
    }
}
