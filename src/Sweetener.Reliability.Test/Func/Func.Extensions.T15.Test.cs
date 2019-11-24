// Generated from Func.Extensions.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    partial class FuncExtensionsTest
    {
        [TestMethod]
        public void WithRetryT15_DelayPolicy()
        {
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> nullFunc = null;
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> func     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => 42;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null ));

            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke;
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, DelayPolicy, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry = (f, m, r, e, d) => f.WithRetry(m, e, d);

            // Without Token
            invoke = (func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);

            WithRetryT15_Success                   (withRetry, invoke, useResultPolicy: false);
            WithRetryT15_Failure_Exception         (withRetry, invoke); // ResultPolicy is never called
            WithRetryT15_EventualSuccess           (withRetry, invoke, useResultPolicy: false);
            WithRetryT15_EventualFailure_Exception (withRetry, invoke, useResultPolicy: false);
            WithRetryT15_RetriesExhausted_Exception(withRetry, invoke, useResultPolicy: false);

            // With Token
            invoke = (func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token);

            WithRetryT15_Success                   (withRetry, invoke, useResultPolicy: false);
            WithRetryT15_Failure_Exception         (withRetry, invoke); // ResultPolicy is never called
            WithRetryT15_EventualSuccess           (withRetry, invoke, useResultPolicy: false);
            WithRetryT15_EventualFailure_Exception (withRetry, invoke, useResultPolicy: false);
            WithRetryT15_RetriesExhausted_Exception(withRetry, invoke, useResultPolicy: false);
            WithRetryT15_Canceled                  (withRetry        , useResultPolicy: false);
        }

        [TestMethod]
        public void WithRetryT15_ComplexDelayPolicy()
        {
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> nullFunc = null;
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> func     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => 42;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero   ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero   ));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                       , (i, r, e) => TimeSpan.Zero   ));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy<int>)null));

            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke;
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, ComplexDelayPolicy<int>, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry = (f, m, r, e, d) => f.WithRetry(m, e, d);

            // Without Token
            invoke = (func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);

            WithRetryT15_Success                   (withRetry, invoke, useResultPolicy: false);
            WithRetryT15_Failure_Exception         (withRetry, invoke); // ResultPolicy is never called
            WithRetryT15_EventualSuccess           (withRetry, invoke, useResultPolicy: false);
            WithRetryT15_EventualFailure_Exception (withRetry, invoke, useResultPolicy: false);
            WithRetryT15_RetriesExhausted_Exception(withRetry, invoke, useResultPolicy: false);

            // With Token
            invoke = (func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token);

            WithRetryT15_Success                   (withRetry, invoke, useResultPolicy: false);
            WithRetryT15_Failure_Exception         (withRetry, invoke); // ResultPolicy is never called
            WithRetryT15_EventualSuccess           (withRetry, invoke, useResultPolicy: false);
            WithRetryT15_EventualFailure_Exception (withRetry, invoke, useResultPolicy: false);
            WithRetryT15_RetriesExhausted_Exception(withRetry, invoke, useResultPolicy: false);
            WithRetryT15_Canceled                  (withRetry        , useResultPolicy: false);
        }

        [TestMethod]
        public void WithRetryT15_ResultPolicy_DelayPolicy()
        {
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> nullFunc = null;
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> func     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => 42;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, r => ResultKind.Successful, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                      , ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (DelayPolicy)null ));

            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke;
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, DelayPolicy, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry = (f, m, r, e, d) => f.WithRetry(m, r, e, d);

            // Without Token
            invoke = (func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);

            WithRetryT15_Success                   (withRetry, invoke);
            WithRetryT15_Failure_Result            (withRetry, invoke);
            WithRetryT15_Failure_Exception         (withRetry, invoke);
            WithRetryT15_EventualSuccess           (withRetry, invoke);
            WithRetryT15_EventualFailure_Result    (withRetry, invoke);
            WithRetryT15_EventualFailure_Exception (withRetry, invoke);
            WithRetryT15_RetriesExhausted_Result   (withRetry, invoke);
            WithRetryT15_RetriesExhausted_Exception(withRetry, invoke);

            // With Token
            invoke = (func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token);

            WithRetryT15_Success                   (withRetry, invoke);
            WithRetryT15_Failure_Result            (withRetry, invoke);
            WithRetryT15_Failure_Exception         (withRetry, invoke);
            WithRetryT15_EventualSuccess           (withRetry, invoke);
            WithRetryT15_EventualFailure_Result    (withRetry, invoke);
            WithRetryT15_EventualFailure_Exception (withRetry, invoke);
            WithRetryT15_RetriesExhausted_Result   (withRetry, invoke);
            WithRetryT15_RetriesExhausted_Exception(withRetry, invoke);
            WithRetryT15_Canceled                  (withRetry);
        }

        [TestMethod]
        public void WithRetryT15_ResultPolicy_ComplexDelayPolicy()
        {
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> nullFunc = null;
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> func     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => 42;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero   ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, r => ResultKind.Successful, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero   ));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                      , ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero   ));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, null                       , (i, r, e) => TimeSpan.Zero   ));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (ComplexDelayPolicy<int>)null));

            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke;
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, ComplexDelayPolicy<int>, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry = (f, m, r, e, d) => f.WithRetry(m, r, e, d);

            // Without Token
            invoke = (func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);

            WithRetryT15_Success                   (withRetry, invoke);
            WithRetryT15_Failure_Result            (withRetry, invoke);
            WithRetryT15_Failure_Exception         (withRetry, invoke);
            WithRetryT15_EventualSuccess           (withRetry, invoke);
            WithRetryT15_EventualFailure_Result    (withRetry, invoke);
            WithRetryT15_EventualFailure_Exception (withRetry, invoke);
            WithRetryT15_RetriesExhausted_Result   (withRetry, invoke);
            WithRetryT15_RetriesExhausted_Exception(withRetry, invoke);

            // With Token
            invoke = (func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token);

            WithRetryT15_Success                   (withRetry, invoke);
            WithRetryT15_Failure_Result            (withRetry, invoke);
            WithRetryT15_Failure_Exception         (withRetry, invoke);
            WithRetryT15_EventualSuccess           (withRetry, invoke);
            WithRetryT15_EventualFailure_Result    (withRetry, invoke);
            WithRetryT15_EventualFailure_Exception (withRetry, invoke);
            WithRetryT15_RetriesExhausted_Result   (withRetry, invoke);
            WithRetryT15_RetriesExhausted_Exception(withRetry, invoke);
            WithRetryT15_Canceled                  (withRetry);
        }

        #region WithRetryT15_Success

        private void WithRetryT15_Success(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, DelayPolicy, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke,
            bool useResultPolicy = true)
            => WithRetryT15_Success(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                },
                useResultPolicy);

        private void WithRetryT15_Success(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, ComplexDelayPolicy<int>, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke,
            bool useResultPolicy = true)
            => WithRetryT15_Success(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, int, Exception>();
                    return delayPolicy;
                },
                useResultPolicy);

        private void WithRetryT15_Success<T>(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, T, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke,
            Func<T> delayPolicyFactory,
            bool useResultPolicy)
            where T : DelegateProxy
        {
            // Create a "successful" user-defined function
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> func = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => 200);

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy    = new FuncProxy<int, ResultKind>(n => n == 200 ? ResultKind.Successful : ResultKind.Fatal);
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>();
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableFunc
            InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> reliableFunc = withRetry(
                func.Invoke,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            func           .Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(Arguments.Validate);
            resultPolicy   .Invoking += Expect.Result(200);
            exceptionPolicy.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.AreEqual(200, invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', tokenSource.Token));

            // Validate the number of calls
            int x = useResultPolicy ? 1 : 0;
            Assert.AreEqual(1, func           .Calls);
            Assert.AreEqual(x, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT15_Failure_Result

        private void WithRetryT15_Failure_Result(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, DelayPolicy, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke)
            => WithRetryT15_Failure_Result(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                });

        private void WithRetryT15_Failure_Result(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, ComplexDelayPolicy<int>, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke)
            => WithRetryT15_Failure_Result(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, int, Exception>();
                    return delayPolicy;
                });

        private void WithRetryT15_Failure_Result<T>(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, T, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined func
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> func = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => 500);

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy    = new FuncProxy<int, ResultKind>(n => n == 200 ? ResultKind.Successful : ResultKind.Fatal);
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>();
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableFunc
            InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> reliableFunc = withRetry(
                func.Invoke,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            func           .Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(Arguments.Validate);
            resultPolicy   .Invoking += Expect.Result(500);
            exceptionPolicy.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.AreEqual(500, invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(1, func           .Calls);
            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT15_Failure_Exception

        private void WithRetryT15_Failure_Exception(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, DelayPolicy, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke)
            => WithRetryT15_Failure_Exception(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                });

        private void WithRetryT15_Failure_Exception(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, ComplexDelayPolicy<int>, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke)
            => WithRetryT15_Failure_Exception(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, int, Exception>();
                    return delayPolicy;
                });

        private void WithRetryT15_Failure_Exception<T>(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, T, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined func
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> func = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => throw new InvalidOperationException());

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy    = new FuncProxy<int, ResultKind>();
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>();
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableFunc
            InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> reliableFunc = withRetry(
                func.Invoke,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            func           .Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(Arguments.Validate);
            resultPolicy   .Invoking += Expect.Nothing<int>();
            exceptionPolicy.Invoking += Expect.Exception(typeof(InvalidOperationException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(1, func           .Calls);
            Assert.AreEqual(0, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT15_EventualSuccess

        private void WithRetryT15_EventualSuccess(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, DelayPolicy, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke,
            bool useResultPolicy = true)
            => WithRetryT15_EventualSuccess(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                },
                useResultPolicy);

        private void WithRetryT15_EventualSuccess(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, ComplexDelayPolicy<int>, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke,
            bool useResultPolicy = true)
            => WithRetryT15_EventualSuccess(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, int, Exception, TimeSpan>((i, r, e) => Constants.Delay);

                    if (useResultPolicy)
                        delayPolicy.Invoking += Expect.AlternatingAsc(418, typeof(IOException));
                    else
                        delayPolicy.Invoking += Expect.OnlyExceptionAsc<int>(typeof(IOException));

                    return delayPolicy;
                },
                useResultPolicy);

        private void WithRetryT15_EventualSuccess<T>(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, T, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke,
            Func<T> delayPolicyFactory,
            bool useResultPolicy)
            where T : DelegateProxy
        {
            // Create a "successful" user-defined action that completes after either
            // (1) 2 IOExceptions OR
            // (2) an IOException and a transient 418 result
            Func<int> flakyFunc = useResultPolicy
                ? FlakyFunc.Create<int, IOException>(418, 200, 2)
                : FlakyFunc.Create<int, IOException>(     200, 2);
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> func = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => flakyFunc());

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy = new FuncProxy<int, ResultKind>(r =>
                r switch
                {
                    418 => ResultKind.Retryable,
                    200 => ResultKind.Successful,
                    _   => ResultKind.Fatal,
                });
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableFunc
            InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> reliableFunc = withRetry(
                func.Invoke,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            func           .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(Arguments.Validate, Constants.MinDelay);
            resultPolicy   .Invoking += Expect.Results(418, 200, 1);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.AreEqual(200, invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', tokenSource.Token));

            // Validate the number of calls
            int x = useResultPolicy ? 2 : 0;
            int y = useResultPolicy ? 1 : 2;
            Assert.AreEqual(3, func           .Calls);
            Assert.AreEqual(x, resultPolicy   .Calls);
            Assert.AreEqual(y, exceptionPolicy.Calls);
            Assert.AreEqual(2, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT15_EventualFailure_Result

        private void WithRetryT15_EventualFailure_Result(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, DelayPolicy, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke)
            => WithRetryT15_EventualFailure_Result(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT15_EventualFailure_Result(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, ComplexDelayPolicy<int>, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke)
            => WithRetryT15_EventualFailure_Result(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, int, Exception, TimeSpan>((i, r, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.AlternatingAsc(418, typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT15_EventualFailure_Result<T>(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, T, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that completes after a transient result and exception
            Func<int> flakyFunc = FlakyFunc.Create<int, IOException>(418, 500, 2);
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> func = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => flakyFunc());

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy = new FuncProxy<int, ResultKind>(r =>
                r switch
                {
                    418 => ResultKind.Retryable,
                    500 => ResultKind.Fatal,
                    _   => ResultKind.Successful,
                });
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableFunc
            InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> reliableFunc = withRetry(
                func.Invoke,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            func           .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(Arguments.Validate, Constants.MinDelay);
            resultPolicy   .Invoking += Expect.Results(418, 500, 1);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.AreEqual(500, invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, func           .Calls);
            Assert.AreEqual(2, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(2, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT15_EventualFailure_Exception

        private void WithRetryT15_EventualFailure_Exception(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, DelayPolicy, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke,
            bool useResultPolicy = true)
            => WithRetryT15_EventualFailure_Exception(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                },
                useResultPolicy);

        private void WithRetryT15_EventualFailure_Exception(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, ComplexDelayPolicy<int>, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke,
            bool useResultPolicy = true)
            => WithRetryT15_EventualFailure_Exception(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, int, Exception, TimeSpan>((i, r, e) => Constants.Delay);

                    if (useResultPolicy)
                        delayPolicy.Invoking += Expect.AlternatingAsc(418, typeof(IOException));
                    else
                        delayPolicy.Invoking += Expect.OnlyExceptionAsc<int>(typeof(IOException));

                    return delayPolicy;
                },
                useResultPolicy);

        private void WithRetryT15_EventualFailure_Exception<T>(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, T, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke,
            Func<T> delayPolicyFactory,
            bool useResultPolicy)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that completes after either
            // (1) 2 IOExceptions OR
            // (2) an IOException and a transient 418 result
            Func<int> flakyFunc = useResultPolicy
                ? FlakyFunc.Create<int, IOException, InvalidOperationException>(418, 2)
                : FlakyFunc.Create<int, IOException, InvalidOperationException>(     2);
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> func = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => flakyFunc());

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy    = new FuncProxy<int, ResultKind>(r => r == 418 ? ResultKind.Retryable : ResultKind.Successful);
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableFunc
            InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> reliableFunc = withRetry(
                func.Invoke,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            func           .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(Arguments.Validate, Constants.MinDelay);
            resultPolicy   .Invoking += Expect.Result(418);
            exceptionPolicy.Invoking += Expect.Exceptions(typeof(IOException), typeof(InvalidOperationException), useResultPolicy ? 1 : 2);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', tokenSource.Token));

            // Validate the number of calls
            int x = useResultPolicy ? 1 : 0;
            int y = useResultPolicy ? 2 : 3;
            Assert.AreEqual(3, func           .Calls);
            Assert.AreEqual(x, resultPolicy   .Calls);
            Assert.AreEqual(y, exceptionPolicy.Calls);
            Assert.AreEqual(2, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT15_RetriesExhausted_Result

        private void WithRetryT15_RetriesExhausted_Result(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, DelayPolicy, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke)
            => WithRetryT15_RetriesExhausted_Result(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT15_RetriesExhausted_Result(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, ComplexDelayPolicy<int>, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke)
            => WithRetryT15_RetriesExhausted_Result(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, int, Exception, TimeSpan>((i, r, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.AlternatingAsc(418, typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT15_RetriesExhausted_Result<T>(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, T, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that eventually exhausts all of its retries
            Func<int> flakyFunc = FlakyFunc.Create<int, IOException>(418);
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> func = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => flakyFunc());

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy = new FuncProxy<int, ResultKind>(r => r == 418 ? ResultKind.Retryable : ResultKind.Fatal);
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableFunc
            InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> reliableFunc = withRetry(
                func.Invoke,
                3,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            func           .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(Arguments.Validate, Constants.MinDelay);
            resultPolicy   .Invoking += Expect.Result(418);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.AreEqual(418, invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(4, func           .Calls);
            Assert.AreEqual(2, resultPolicy   .Calls);
            Assert.AreEqual(2, exceptionPolicy.Calls);
            Assert.AreEqual(3, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT15_RetriesExhausted_Exception

        private void WithRetryT15_RetriesExhausted_Exception(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, DelayPolicy, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke,
            bool useResultPolicy = true)
            => WithRetryT15_RetriesExhausted_Exception(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                },
                useResultPolicy);

        private void WithRetryT15_RetriesExhausted_Exception(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, ComplexDelayPolicy<int>, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke,
            bool useResultPolicy = true)
            => WithRetryT15_RetriesExhausted_Exception(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, int, Exception, TimeSpan>((i, r, e) => Constants.Delay);

                    if (useResultPolicy)
                        delayPolicy.Invoking += Expect.AlternatingAsc(418, typeof(IOException));
                    else
                        delayPolicy.Invoking += Expect.OnlyExceptionAsc<int>(typeof(IOException));

                    return delayPolicy;
                },
                useResultPolicy);

        private void WithRetryT15_RetriesExhausted_Exception<T>(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, T, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken, int> invoke,
            Func<T> delayPolicyFactory,
            bool useResultPolicy)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that eventually exhausts all of its retries
            Func<int> flakyFunc = useResultPolicy
                ? FlakyFunc.Create<int, IOException>(418)
                : () => throw new IOException();
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> func = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => flakyFunc());

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy    = new FuncProxy<int, ResultKind>(r => r == 418 ? ResultKind.Retryable : ResultKind.Successful);
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableFunc
            InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> reliableFunc = withRetry(
                func.Invoke,
                2,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            func           .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(Arguments.Validate, Constants.MinDelay);
            resultPolicy   .Invoking += Expect.Result(418);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<IOException>(() => invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', tokenSource.Token));

            // Validate the number of calls
            int x = useResultPolicy ? 1 : 0;
            int y = useResultPolicy ? 2 : 3;
            Assert.AreEqual(3, func           .Calls);
            Assert.AreEqual(x, resultPolicy   .Calls);
            Assert.AreEqual(y, exceptionPolicy.Calls);
            Assert.AreEqual(2, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT15_Canceled

        private void WithRetryT15_Canceled(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, DelayPolicy, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            bool useResultPolicy = true)
            => WithRetryT15_Canceled(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                },
                useResultPolicy);

        private void WithRetryT15_Canceled(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, ComplexDelayPolicy<int>, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            bool useResultPolicy = true)
            => WithRetryT15_Canceled(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                () =>
                {
                    FuncProxy<int, int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, int, Exception, TimeSpan>((i, r, e) => Constants.Delay);

                    if (useResultPolicy)
                        delayPolicy.Invoking += Expect.AlternatingAsc(418, typeof(IOException));
                    else
                        delayPolicy.Invoking += Expect.OnlyExceptionAsc<int>(typeof(IOException));

                    return delayPolicy;
                },
                useResultPolicy);

        private void WithRetryT15_Canceled<T>(
            Func<Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>, int, ResultPolicy<int>, ExceptionPolicy, T, InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>> withRetry,
            Func<T> delayPolicyFactory,
            bool useResultPolicy)
            where T : DelegateProxy
        {
            using ManualResetEvent        cancellationTrigger = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource         = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined func that continues to fail with transient exceptions until it's canceled
            Func<int> flakyFunc = useResultPolicy
                ? FlakyFunc.Create<int, IOException>(418)
                : () => throw new IOException();
            FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> func = new FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => flakyFunc());

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy    = new FuncProxy<int, ResultKind>(r => r == 418 ? ResultKind.Retryable : ResultKind.Successful);
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableFunc
            InterruptableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, int> reliableFunc = withRetry(
                func.Invoke,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            func           .Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Trigger the event upon retry
            int minRetries = useResultPolicy ? 2 : 1;
            func.Invoking += (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, c) =>
            {
                if (c.Calls > minRetries)
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
            Assert.That.ThrowsException<OperationCanceledException>(() => reliableFunc(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', tokenSource.Token));

            // Validate the number of calls
            int calls      = func.Calls;
            int results    = useResultPolicy ? calls / 2 : 0;
            int exceptions = calls - results;
            Assert.IsTrue(calls > minRetries);

            Assert.AreEqual(results   , resultPolicy    .Calls);
            Assert.AreEqual(exceptions, exceptionPolicy .Calls);
            Assert.AreEqual(calls     , delayPolicy     .Calls);
        }

        #endregion

    }
}
