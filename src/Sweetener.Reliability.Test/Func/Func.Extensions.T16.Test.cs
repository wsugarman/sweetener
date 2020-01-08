// Generated from Func.Extensions.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    // Define type aliases for the various generic types used below as they can become pretty cumbersome
    using TestFunc                        = Func     <int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, int>;
    using InterruptableTestFunc           = Func     <int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int>;
    using AsyncTestFunc                   = Func     <int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Task<int>>;
    using InterruptableAsyncTestFunc      = Func     <int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, Task<int>>;
    using TestFuncProxy                   = FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, int>;
    using InterruptableTestFuncProxy      = FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int>;
    using AsyncTestFuncProxy              = FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Task<int>>;
    using InterruptableAsyncTestFuncProxy = FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, Task<int>>;
    using DelayPolicyProxy                = FuncProxy<int, TimeSpan>;
    using ComplexDelayPolicyProxy         = FuncProxy<int, int, Exception, TimeSpan>;

    partial class FuncExtensionsTest
    {
        [TestMethod]
        public void WithRetryT16_DelayPolicy()
        {
            TestFunc nullFunc = null;
            TestFunc func     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => 12345;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => f(CancellationToken.None));
            Func<TimeSpan, DelayPolicyProxy> delayPolicyFactory = t => new DelayPolicyProxy((i) => t);
            Func<TestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, TimeSpan>, TestFunc> withRetry = (f, m, r, e, d) => f.WithRetry(m, e, d.Invoke);
            Func<TestFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => f(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);

            Action<TestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(Arguments.Validate, delay);

            // Success
            WithRetryT16_Success        (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: false);
            WithRetryT16_EventualSuccess(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);

            // Failure (Exception)
            WithRetryT16_Failure_Exception         (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: false);
            WithRetryT16_EventualFailure_Exception (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);
            WithRetryT16_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);
        }

        [TestMethod]
        public void WithRetryT16_ComplexDelayPolicy()
        {
            TestFunc nullFunc = null;
            TestFunc func     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => 12345;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                       , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy<int>)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => f(CancellationToken.None));
            Func<TimeSpan, ComplexDelayPolicyProxy> delayPolicyFactory = t => new ComplexDelayPolicyProxy((i, r, e) => t);
            Func<TestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, int, Exception, TimeSpan>, TestFunc> withRetry = (f, m, r, e, d) => f.WithRetry(m, e, d.Invoke);
            Func<TestFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => f(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);

            Action<TestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(Arguments.Validate, delay);

            // Success
            WithRetryT16_Success        (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: false);
            WithRetryT16_EventualSuccess(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);

            // Failure (Exception)
            WithRetryT16_Failure_Exception         (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: false);
            WithRetryT16_EventualFailure_Exception (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);
            WithRetryT16_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);
        }

        [TestMethod]
        public void WithRetryT16_ResultPolicy_DelayPolicy()
        {
            TestFunc nullFunc = null;
            TestFunc func     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => 12345;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, r => ResultKind.Successful, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                      , ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (DelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => f(CancellationToken.None));
            Func<TimeSpan, DelayPolicyProxy> delayPolicyFactory = t => new DelayPolicyProxy((i) => t);
            Func<TestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, TimeSpan>, TestFunc> withRetry = (f, m, r, e, d) => f.WithRetry(m, r, e, d.Invoke);
            Func<TestFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => f(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);

            Action<TestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(Arguments.Validate, delay);

            // Success
            WithRetryT16_Success        (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: true);
            WithRetryT16_EventualSuccess(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);

            // Failure (Result)
            WithRetryT16_Failure_Result         (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>());
            WithRetryT16_EventualFailure_Result (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());
            WithRetryT16_RetriesExhausted_Result(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            WithRetryT16_Failure_Exception         (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: true);
            WithRetryT16_EventualFailure_Exception (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);
            WithRetryT16_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);
        }

        [TestMethod]
        public void WithRetryT16_ResultPolicy_ComplexDelayPolicy()
        {
            TestFunc nullFunc = null;
            TestFunc func     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => 12345;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, r => ResultKind.Successful, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                      , ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, null                       , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (ComplexDelayPolicy<int>)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => f(CancellationToken.None));
            Func<TimeSpan, ComplexDelayPolicyProxy> delayPolicyFactory = t => new ComplexDelayPolicyProxy((i, r, e) => t);
            Func<TestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, int, Exception, TimeSpan>, TestFunc> withRetry = (f, m, r, e, d) => f.WithRetry(m, r, e, d.Invoke);
            Func<TestFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => f(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);

            Action<TestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(Arguments.Validate, delay);

            // Success
            WithRetryT16_Success        (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: true);
            WithRetryT16_EventualSuccess(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);

            // Failure (Result)
            WithRetryT16_Failure_Result         (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>());
            WithRetryT16_EventualFailure_Result (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));
            WithRetryT16_RetriesExhausted_Result(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));

            // Failure (Exception)
            WithRetryT16_Failure_Exception         (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: true);
            WithRetryT16_EventualFailure_Exception (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);
            WithRetryT16_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);
        }

        [TestMethod]
        public void WithRetryT16_WithToken_DelayPolicy()
        {
            InterruptableTestFunc nullFunc = null;
            InterruptableTestFunc func     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => 12345;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => f(token));
            Func<TimeSpan, DelayPolicyProxy> delayPolicyFactory = t => new DelayPolicyProxy((i) => t);
            Func<InterruptableTestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, TimeSpan>, InterruptableTestFunc> withRetry = (f, m, r, e, d) => f.WithRetry(m, e, d.Invoke);
            Func<InterruptableTestFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => f(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token);

            Action<InterruptableTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT16_Success        (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: false);
            WithRetryT16_EventualSuccess(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);

            // Failure (Exception)
            WithRetryT16_Failure_Exception         (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: false);
            WithRetryT16_EventualFailure_Exception (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);
            WithRetryT16_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);

            // Cancel
            WithRetryT16_Canceled_Func (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);
            WithRetryT16_Canceled_Delay(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);
        }

        [TestMethod]
        public void WithRetryT16_WithToken_ComplexDelayPolicy()
        {
            InterruptableTestFunc nullFunc = null;
            InterruptableTestFunc func     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => 12345;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                       , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy<int>)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => f(token));
            Func<TimeSpan, ComplexDelayPolicyProxy> delayPolicyFactory = t => new ComplexDelayPolicyProxy((i, r, e) => t);
            Func<InterruptableTestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, int, Exception, TimeSpan>, InterruptableTestFunc> withRetry = (f, m, r, e, d) => f.WithRetry(m, e, d.Invoke);
            Func<InterruptableTestFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => f(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token);

            Action<InterruptableTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT16_Success        (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: false);
            WithRetryT16_EventualSuccess(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);

            // Failure (Exception)
            WithRetryT16_Failure_Exception         (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: false);
            WithRetryT16_EventualFailure_Exception (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);
            WithRetryT16_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);

            // Cancel
            WithRetryT16_Canceled_Func (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);
            WithRetryT16_Canceled_Delay(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);
        }

        [TestMethod]
        public void WithRetryT16_WithToken_ResultPolicy_DelayPolicy()
        {
            InterruptableTestFunc nullFunc = null;
            InterruptableTestFunc func     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => 12345;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, r => ResultKind.Successful, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                      , ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (DelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => f(token));
            Func<TimeSpan, DelayPolicyProxy> delayPolicyFactory = t => new DelayPolicyProxy((i) => t);
            Func<InterruptableTestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, TimeSpan>, InterruptableTestFunc> withRetry = (f, m, r, e, d) => f.WithRetry(m, r, e, d.Invoke);
            Func<InterruptableTestFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => f(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token);

            Action<InterruptableTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT16_Success        (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: true);
            WithRetryT16_EventualSuccess(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);

            // Failure (Result)
            WithRetryT16_Failure_Result         (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>());
            WithRetryT16_EventualFailure_Result (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());
            WithRetryT16_RetriesExhausted_Result(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            WithRetryT16_Failure_Exception         (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: true);
            WithRetryT16_EventualFailure_Exception (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);
            WithRetryT16_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);

            // Cancel
            WithRetryT16_Canceled_Func (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);
            WithRetryT16_Canceled_Delay(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);
        }

        [TestMethod]
        public void WithRetryT16_WithToken_ResultPolicy_ComplexDelayPolicy()
        {
            InterruptableTestFunc nullFunc = null;
            InterruptableTestFunc func     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => 12345;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, r => ResultKind.Successful, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                      , ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, null                       , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (ComplexDelayPolicy<int>)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => f(token));
            Func<TimeSpan, ComplexDelayPolicyProxy> delayPolicyFactory = t => new ComplexDelayPolicyProxy((i, r, e) => t);
            Func<InterruptableTestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, int, Exception, TimeSpan>, InterruptableTestFunc> withRetry = (f, m, r, e, d) => f.WithRetry(m, r, e, d.Invoke);
            Func<InterruptableTestFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => f(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token);

            Action<InterruptableTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT16_Success        (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: true);
            WithRetryT16_EventualSuccess(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);

            // Failure (Result)
            WithRetryT16_Failure_Result         (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>());
            WithRetryT16_EventualFailure_Result (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));
            WithRetryT16_RetriesExhausted_Result(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));

            // Failure (Exception)
            WithRetryT16_Failure_Exception         (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: true);
            WithRetryT16_EventualFailure_Exception (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);
            WithRetryT16_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);

            // Cancel
            WithRetryT16_Canceled_Func (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);
            WithRetryT16_Canceled_Delay(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);
        }

        [TestMethod]
        public void WithAsyncRetryT16_Async_DelayPolicy()
        {
            AsyncTestFunc nullFunc = null;
            AsyncTestFunc func     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => await Task.Run(() => 12345).ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, AsyncTestFuncProxy> funcFactory = f => new AsyncTestFuncProxy(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => { await Task.CompletedTask.ConfigureAwait(false); return f(CancellationToken.None); });
            Func<TimeSpan, DelayPolicyProxy> delayPolicyFactory = t => new DelayPolicyProxy((i) => t);
            Func<AsyncTestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, TimeSpan>, AsyncTestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, e, d.Invoke);
            Func<AsyncTestFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => f(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15).Result;

            Action<AsyncTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(Arguments.Validate);
            Action<AsyncTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(Arguments.Validate, delay);

            // Success
            WithRetryT16_Success        (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: false);
            WithRetryT16_EventualSuccess(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);

            // Failure (Exception)
            WithRetryT16_Failure_Exception         (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: false);
            WithRetryT16_EventualFailure_Exception (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);
            WithRetryT16_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);
        }

        [TestMethod]
        public void WithAsyncRetryT16_Async_ComplexDelayPolicy()
        {
            AsyncTestFunc nullFunc = null;
            AsyncTestFunc func     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => await Task.Run(() => 12345).ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                       , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy<int>)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, AsyncTestFuncProxy> funcFactory = f => new AsyncTestFuncProxy(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => { await Task.CompletedTask.ConfigureAwait(false); return f(CancellationToken.None); });
            Func<TimeSpan, ComplexDelayPolicyProxy> delayPolicyFactory = t => new ComplexDelayPolicyProxy((i, r, e) => t);
            Func<AsyncTestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, int, Exception, TimeSpan>, AsyncTestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, e, d.Invoke);
            Func<AsyncTestFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => f(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15).Result;

            Action<AsyncTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(Arguments.Validate);
            Action<AsyncTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(Arguments.Validate, delay);

            // Success
            WithRetryT16_Success        (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: false);
            WithRetryT16_EventualSuccess(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);

            // Failure (Exception)
            WithRetryT16_Failure_Exception         (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: false);
            WithRetryT16_EventualFailure_Exception (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);
            WithRetryT16_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);
        }

        [TestMethod]
        public void WithAsyncRetryT16_Async_ResultPolicy_DelayPolicy()
        {
            AsyncTestFunc nullFunc = null;
            AsyncTestFunc func     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => await Task.Run(() => 12345).ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, r => ResultKind.Successful, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                      , ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (DelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, AsyncTestFuncProxy> funcFactory = f => new AsyncTestFuncProxy(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => { await Task.CompletedTask.ConfigureAwait(false); return f(CancellationToken.None); });
            Func<TimeSpan, DelayPolicyProxy> delayPolicyFactory = t => new DelayPolicyProxy((i) => t);
            Func<AsyncTestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, TimeSpan>, AsyncTestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, r, e, d.Invoke);
            Func<AsyncTestFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => f(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15).Result;

            Action<AsyncTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(Arguments.Validate);
            Action<AsyncTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(Arguments.Validate, delay);

            // Success
            WithRetryT16_Success        (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: true);
            WithRetryT16_EventualSuccess(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);

            // Failure (Result)
            WithRetryT16_Failure_Result         (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>());
            WithRetryT16_EventualFailure_Result (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());
            WithRetryT16_RetriesExhausted_Result(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            WithRetryT16_Failure_Exception         (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: true);
            WithRetryT16_EventualFailure_Exception (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);
            WithRetryT16_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);
        }

        [TestMethod]
        public void WithAsyncRetryT16_Async_ResultPolicy_ComplexDelayPolicy()
        {
            AsyncTestFunc nullFunc = null;
            AsyncTestFunc func     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => await Task.Run(() => 12345).ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, r => ResultKind.Successful, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                      , ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, null                       , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (ComplexDelayPolicy<int>)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, AsyncTestFuncProxy> funcFactory = f => new AsyncTestFuncProxy(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => { await Task.CompletedTask.ConfigureAwait(false); return f(CancellationToken.None); });
            Func<TimeSpan, ComplexDelayPolicyProxy> delayPolicyFactory = t => new ComplexDelayPolicyProxy((i, r, e) => t);
            Func<AsyncTestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, int, Exception, TimeSpan>, AsyncTestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, r, e, d.Invoke);
            Func<AsyncTestFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => f(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15).Result;

            Action<AsyncTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(Arguments.Validate);
            Action<AsyncTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(Arguments.Validate, delay);

            // Success
            WithRetryT16_Success        (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: true);
            WithRetryT16_EventualSuccess(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);

            // Failure (Result)
            WithRetryT16_Failure_Result         (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>());
            WithRetryT16_EventualFailure_Result (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));
            WithRetryT16_RetriesExhausted_Result(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));

            // Failure (Exception)
            WithRetryT16_Failure_Exception         (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: true);
            WithRetryT16_EventualFailure_Exception (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);
            WithRetryT16_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);
        }

        [TestMethod]
        public void WithAsyncRetryT16_Async_WithToken_DelayPolicy()
        {
            InterruptableAsyncTestFunc nullFunc = null;
            InterruptableAsyncTestFunc func     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => await Task.Run(() => 12345).ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableAsyncTestFuncProxy> funcFactory = f => new InterruptableAsyncTestFuncProxy(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => { await Task.CompletedTask.ConfigureAwait(false); return f(token); });
            Func<TimeSpan, DelayPolicyProxy> delayPolicyFactory = t => new DelayPolicyProxy((i) => t);
            Func<InterruptableAsyncTestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, TimeSpan>, InterruptableAsyncTestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, e, d.Invoke);
            Func<InterruptableAsyncTestFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => f(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token).Result;

            Action<InterruptableAsyncTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken>(Arguments.Validate);
            Action<InterruptableAsyncTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT16_Success        (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: false);
            WithRetryT16_EventualSuccess(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);

            // Failure (Exception)
            WithRetryT16_Failure_Exception         (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: false);
            WithRetryT16_EventualFailure_Exception (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);
            WithRetryT16_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);

            // Cancel
            WithRetryT16_Canceled_Func (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);
            WithRetryT16_Canceled_Delay(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);
        }

        [TestMethod]
        public void WithAsyncRetryT16_Async_WithToken_ComplexDelayPolicy()
        {
            InterruptableAsyncTestFunc nullFunc = null;
            InterruptableAsyncTestFunc func     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => await Task.Run(() => 12345).ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                       , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy<int>)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableAsyncTestFuncProxy> funcFactory = f => new InterruptableAsyncTestFuncProxy(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => { await Task.CompletedTask.ConfigureAwait(false); return f(token); });
            Func<TimeSpan, ComplexDelayPolicyProxy> delayPolicyFactory = t => new ComplexDelayPolicyProxy((i, r, e) => t);
            Func<InterruptableAsyncTestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, int, Exception, TimeSpan>, InterruptableAsyncTestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, e, d.Invoke);
            Func<InterruptableAsyncTestFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => f(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token).Result;

            Action<InterruptableAsyncTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken>(Arguments.Validate);
            Action<InterruptableAsyncTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT16_Success        (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: false);
            WithRetryT16_EventualSuccess(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);

            // Failure (Exception)
            WithRetryT16_Failure_Exception         (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: false);
            WithRetryT16_EventualFailure_Exception (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);
            WithRetryT16_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);

            // Cancel
            WithRetryT16_Canceled_Func (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);
            WithRetryT16_Canceled_Delay(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);
        }

        [TestMethod]
        public void WithAsyncRetryT16_Async_WithToken_ResultPolicy_DelayPolicy()
        {
            InterruptableAsyncTestFunc nullFunc = null;
            InterruptableAsyncTestFunc func     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => await Task.Run(() => 12345).ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, r => ResultKind.Successful, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                      , ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (DelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableAsyncTestFuncProxy> funcFactory = f => new InterruptableAsyncTestFuncProxy(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => { await Task.CompletedTask.ConfigureAwait(false); return f(token); });
            Func<TimeSpan, DelayPolicyProxy> delayPolicyFactory = t => new DelayPolicyProxy((i) => t);
            Func<InterruptableAsyncTestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, TimeSpan>, InterruptableAsyncTestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, r, e, d.Invoke);
            Func<InterruptableAsyncTestFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => f(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token).Result;

            Action<InterruptableAsyncTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken>(Arguments.Validate);
            Action<InterruptableAsyncTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT16_Success        (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: true);
            WithRetryT16_EventualSuccess(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);

            // Failure (Result)
            WithRetryT16_Failure_Result         (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>());
            WithRetryT16_EventualFailure_Result (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());
            WithRetryT16_RetriesExhausted_Result(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            WithRetryT16_Failure_Exception         (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: true);
            WithRetryT16_EventualFailure_Exception (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);
            WithRetryT16_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);

            // Cancel
            WithRetryT16_Canceled_Func (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);
            WithRetryT16_Canceled_Delay(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);
        }

        [TestMethod]
        public void WithAsyncRetryT16_Async_WithToken_ResultPolicy_ComplexDelayPolicy()
        {
            InterruptableAsyncTestFunc nullFunc = null;
            InterruptableAsyncTestFunc func     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => await Task.Run(() => 12345).ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, r => ResultKind.Successful, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                      , ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, null                       , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (ComplexDelayPolicy<int>)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableAsyncTestFuncProxy> funcFactory = f => new InterruptableAsyncTestFuncProxy(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => { await Task.CompletedTask.ConfigureAwait(false); return f(token); });
            Func<TimeSpan, ComplexDelayPolicyProxy> delayPolicyFactory = t => new ComplexDelayPolicyProxy((i, r, e) => t);
            Func<InterruptableAsyncTestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, int, Exception, TimeSpan>, InterruptableAsyncTestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, r, e, d.Invoke);
            Func<InterruptableAsyncTestFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => f(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token).Result;

            Action<InterruptableAsyncTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken>(Arguments.Validate);
            Action<InterruptableAsyncTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT16_Success        (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: true);
            WithRetryT16_EventualSuccess(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);

            // Failure (Result)
            WithRetryT16_Failure_Result         (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>());
            WithRetryT16_EventualFailure_Result (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));
            WithRetryT16_RetriesExhausted_Result(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));

            // Failure (Exception)
            WithRetryT16_Failure_Exception         (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: true);
            WithRetryT16_EventualFailure_Exception (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);
            WithRetryT16_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);

            // Cancel
            WithRetryT16_Canceled_Func (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);
            WithRetryT16_Canceled_Delay(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);
        }

        #region WithRetryT16_Success

        private void WithRetryT16_Success<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TFunc, int, ResultPolicy<int>, ExceptionPolicy, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke,
            Action<TFuncProxy> observeFunc,
            Action<TDelayPolicyProxy> observeDelayPolicy,
            bool passResultPolicy)
            where TFunc             : Delegate
            where TDelayPolicy      : Delegate
            where TFuncProxy        : DelegateProxy<TFunc>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create a "successful" user-defined function
            TFuncProxy func = funcFactory(t => 200);

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy    = new FuncProxy<int, ResultKind>(n => n == 200 ? ResultKind.Successful : ResultKind.Fatal);
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>();
            TDelayPolicyProxy delayPolicy = delayPolicyFactory(TimeSpan.Zero);

            // Create the reliable function
            TFunc reliableFunc = withRetry(
                func.Proxy,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy.Proxy);

            // Define expectations
            resultPolicy   .Invoking += Expect.Result(200);
            exceptionPolicy.Invoking += Expect.Nothing<Exception>();

            observeFunc       ?.Invoke(func);
            observeDelayPolicy?.Invoke(delayPolicy);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.AreEqual(200, invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, tokenSource.Token));

            // Validate the number of calls
            int x = passResultPolicy ? 1 : 0;
            Assert.AreEqual(1, func           .Calls);
            Assert.AreEqual(x, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT16_Failure_Result

        private void WithRetryT16_Failure_Result<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TFunc, int, ResultPolicy<int>, ExceptionPolicy, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke,
            Action<TFuncProxy> observeFunc,
            Action<TDelayPolicyProxy> observeDelayPolicy)
            where TFunc             : Delegate
            where TDelayPolicy      : Delegate
            where TFuncProxy        : DelegateProxy<TFunc>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create an "unsuccessful" user-defined func
            TFuncProxy func = funcFactory(t => 500);

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy    = new FuncProxy<int, ResultKind>(n => n == 200 ? ResultKind.Successful : ResultKind.Fatal);
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>();
            TDelayPolicyProxy delayPolicy = delayPolicyFactory(TimeSpan.Zero);

            // Create the reliable function
            TFunc reliableFunc = withRetry(
                func.Proxy,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy.Proxy);

            // Define expectations
            resultPolicy   .Invoking += Expect.Result(500);
            exceptionPolicy.Invoking += Expect.Nothing<Exception>();

            observeFunc       ?.Invoke(func);
            observeDelayPolicy?.Invoke(delayPolicy);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.AreEqual(500, invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(1, func           .Calls);
            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT16_Failure_Exception

        private void WithRetryT16_Failure_Exception<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TFunc, int, ResultPolicy<int>, ExceptionPolicy, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke,
            Action<TFuncProxy> observeFunc,
            Action<TDelayPolicyProxy> observeDelayPolicy,
            bool passResultPolicy)
            where TFunc             : Delegate
            where TDelayPolicy      : Delegate
            where TFuncProxy        : DelegateProxy<TFunc>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create an "unsuccessful" user-defined func
            TFuncProxy func = funcFactory(t => throw new InvalidOperationException());

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy    = new FuncProxy<int, ResultKind>();
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>();
            TDelayPolicyProxy delayPolicy = delayPolicyFactory(TimeSpan.Zero);

            // Create the reliable function
            TFunc reliableFunc = withRetry(
                func.Proxy,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy.Proxy);

            // Define expectations
            resultPolicy   .Invoking += Expect.Nothing<int>();
            exceptionPolicy.Invoking += Expect.Exception(typeof(InvalidOperationException));

            observeFunc       ?.Invoke(func);
            observeDelayPolicy?.Invoke(delayPolicy);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(1, func           .Calls);
            Assert.AreEqual(0, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT16_EventualSuccess

        private void WithRetryT16_EventualSuccess<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TFunc, int, ResultPolicy<int>, ExceptionPolicy, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke,
            Action<TFuncProxy, TimeSpan> observeFunc,
            Action<TDelayPolicyProxy, int, Type> observeDelayPolicy,
            bool passResultPolicy)
            where TFunc             : Delegate
            where TDelayPolicy      : Delegate
            where TFuncProxy        : DelegateProxy<TFunc>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create a "successful" user-defined action that completes after either
            // (1) 2 IOExceptions OR
            // (2) an IOException and a transient 418 result
            Func<int> flakyFunc = passResultPolicy
                ? FlakyFunc.Create<int, IOException>(418, 200, 2)
                : FlakyFunc.Create<int, IOException>(     200, 2);
            TFuncProxy func = funcFactory(t => flakyFunc());

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy = new FuncProxy<int, ResultKind>(r =>
                r switch
                {
                    418 => ResultKind.Transient,
                    200 => ResultKind.Successful,
                    _   => ResultKind.Fatal,
                });
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            TDelayPolicyProxy delayPolicy = delayPolicyFactory(Constants.Delay);

            // Create the reliable function
            TFunc reliableFunc = withRetry(
                func.Proxy,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy.Proxy);

            // Define expectations
            resultPolicy   .Invoking += Expect.Results(418, 200, 1);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            observeFunc       ?.Invoke(func, Constants.MinDelay);
            observeDelayPolicy?.Invoke(delayPolicy, 418, typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.AreEqual(200, invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, tokenSource.Token));

            // Validate the number of calls
            int x = passResultPolicy ? 2 : 0;
            int y = passResultPolicy ? 1 : 2;
            Assert.AreEqual(3, func           .Calls);
            Assert.AreEqual(x, resultPolicy   .Calls);
            Assert.AreEqual(y, exceptionPolicy.Calls);
            Assert.AreEqual(2, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT16_EventualFailure_Result

        private void WithRetryT16_EventualFailure_Result<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TFunc, int, ResultPolicy<int>, ExceptionPolicy, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke,
            Action<TFuncProxy, TimeSpan> observeFunc,
            Action<TDelayPolicyProxy, int, Type> observeDelayPolicy)
            where TFunc             : Delegate
            where TDelayPolicy      : Delegate
            where TFuncProxy        : DelegateProxy<TFunc>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create an "unsuccessful" user-defined action that completes after a transient result and exception
            Func<int> flakyFunc = FlakyFunc.Create<int, IOException>(418, 500, 2);
            TFuncProxy func = funcFactory(t => flakyFunc());

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy = new FuncProxy<int, ResultKind>(r =>
                r switch
                {
                    418 => ResultKind.Transient,
                    500 => ResultKind.Fatal,
                    _   => ResultKind.Successful,
                });
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            TDelayPolicyProxy delayPolicy = delayPolicyFactory(Constants.Delay);

            // Create the reliable function
            TFunc reliableFunc = withRetry(
                func.Proxy,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy.Proxy);

            // Define expectations
            resultPolicy   .Invoking += Expect.Results(418, 500, 1);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            observeFunc       ?.Invoke(func, Constants.MinDelay);
            observeDelayPolicy?.Invoke(delayPolicy, 418, typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.AreEqual(500, invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, func           .Calls);
            Assert.AreEqual(2, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(2, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT16_EventualFailure_Exception

        private void WithRetryT16_EventualFailure_Exception<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TFunc, int, ResultPolicy<int>, ExceptionPolicy, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke,
            Action<TFuncProxy, TimeSpan> observeFunc,
            Action<TDelayPolicyProxy, int, Type> observeDelayPolicy,
            bool passResultPolicy)
            where TFunc             : Delegate
            where TDelayPolicy      : Delegate
            where TFuncProxy        : DelegateProxy<TFunc>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create an "unsuccessful" user-defined action that completes after either
            // (1) 2 IOExceptions OR
            // (2) an IOException and a transient 418 result
            Func<int> flakyFunc = passResultPolicy
                ? FlakyFunc.Create<int, IOException, InvalidOperationException>(418, 2)
                : FlakyFunc.Create<int, IOException, InvalidOperationException>(     2);
            TFuncProxy func = funcFactory(t => flakyFunc());

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy    = new FuncProxy<int, ResultKind>(r => r == 418 ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            TDelayPolicyProxy delayPolicy = delayPolicyFactory(Constants.Delay);

            // Create the reliable function
            TFunc reliableFunc = withRetry(
                func.Proxy,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy.Proxy);

            // Define expectations
            resultPolicy   .Invoking += Expect.Result(418);
            exceptionPolicy.Invoking += Expect.Exceptions(typeof(IOException), typeof(InvalidOperationException), passResultPolicy ? 1 : 2);

            observeFunc       ?.Invoke(func, Constants.MinDelay);
            observeDelayPolicy?.Invoke(delayPolicy, 418, typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, tokenSource.Token));

            // Validate the number of calls
            int x = passResultPolicy ? 1 : 0;
            int y = passResultPolicy ? 2 : 3;
            Assert.AreEqual(3, func           .Calls);
            Assert.AreEqual(x, resultPolicy   .Calls);
            Assert.AreEqual(y, exceptionPolicy.Calls);
            Assert.AreEqual(2, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT16_RetriesExhausted_Result

        private void WithRetryT16_RetriesExhausted_Result<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TFunc, int, ResultPolicy<int>, ExceptionPolicy, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke,
            Action<TFuncProxy, TimeSpan> observeFunc,
            Action<TDelayPolicyProxy, int, Type> observeDelayPolicy)
            where TFunc             : Delegate
            where TDelayPolicy      : Delegate
            where TFuncProxy        : DelegateProxy<TFunc>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create an "unsuccessful" user-defined action that eventually exhausts all of its retries
            Func<int> flakyFunc = FlakyFunc.Create<int, IOException>(418);
            TFuncProxy func = funcFactory(t => flakyFunc());

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy = new FuncProxy<int, ResultKind>(r => r == 418 ? ResultKind.Transient : ResultKind.Fatal);
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            TDelayPolicyProxy delayPolicy = delayPolicyFactory(Constants.Delay);

            // Create the reliable function
            TFunc reliableFunc = withRetry(
                func.Proxy,
                3,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy.Proxy);

            // Define expectations
            resultPolicy   .Invoking += Expect.Result(418);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            observeFunc       ?.Invoke(func, Constants.MinDelay);
            observeDelayPolicy?.Invoke(delayPolicy, 418, typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.AreEqual(418, invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(4, func           .Calls);
            Assert.AreEqual(2, resultPolicy   .Calls);
            Assert.AreEqual(2, exceptionPolicy.Calls);
            Assert.AreEqual(3, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT16_RetriesExhausted_Exception

        private void WithRetryT16_RetriesExhausted_Exception<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TFunc, int, ResultPolicy<int>, ExceptionPolicy, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke,
            Action<TFuncProxy, TimeSpan> observeFunc,
            Action<TDelayPolicyProxy, int, Type> observeDelayPolicy,
            bool passResultPolicy)
            where TFunc             : Delegate
            where TDelayPolicy      : Delegate
            where TFuncProxy        : DelegateProxy<TFunc>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create an "unsuccessful" user-defined action that eventually exhausts all of its retries
            Func<int> flakyFunc = passResultPolicy
                ? FlakyFunc.Create<int, IOException>(418)
                : () => throw new IOException();
            TFuncProxy func = funcFactory(t => flakyFunc());

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy    = new FuncProxy<int, ResultKind>(r => r == 418 ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            TDelayPolicyProxy delayPolicy = delayPolicyFactory(Constants.Delay);

            // Create the reliable function
            TFunc reliableFunc = withRetry(
                func.Proxy,
                2,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy.Proxy);

            // Define expectations
            resultPolicy   .Invoking += Expect.Result(418);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            observeFunc       ?.Invoke(func, Constants.MinDelay);
            observeDelayPolicy?.Invoke(delayPolicy, 418, typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<IOException>(() => invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, tokenSource.Token));

            // Validate the number of calls
            int x = passResultPolicy ? 1 : 0;
            int y = passResultPolicy ? 2 : 3;
            Assert.AreEqual(3, func           .Calls);
            Assert.AreEqual(x, resultPolicy   .Calls);
            Assert.AreEqual(y, exceptionPolicy.Calls);
            Assert.AreEqual(2, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT16_Canceled_Func

        private void WithRetryT16_Canceled_Func<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TFunc, int, ResultPolicy<int>, ExceptionPolicy, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke,
            Action<TFuncProxy, TimeSpan> observeFunc,
            Action<TDelayPolicyProxy, int, Type> observeDelayPolicy,
            bool passResultPolicy)
            where TFunc             : Delegate
            where TDelayPolicy      : Delegate
            where TFuncProxy        : DelegateProxy<TFunc>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create a user-defined action that will throw an exception depending on whether it's canceled
            Func<int> flakyFunc = passResultPolicy ? FlakyFunc.Create<int, IOException>(418) : () => throw new IOException();
            TFuncProxy func = funcFactory(t =>
            {
                t.ThrowIfCancellationRequested();
                return flakyFunc();
            });

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy    = new FuncProxy<int, ResultKind>(r => r == 418 ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Transient.Invoke);
            TDelayPolicyProxy delayPolicy = delayPolicyFactory(Constants.Delay);

            // Create the reliable function
            TFunc reliableFunc = withRetry(
                func.Proxy,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy.Proxy);

            // Define expectations
            resultPolicy   .Invoking += Expect.Result(418);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            observeFunc       ?.Invoke(func, Constants.MinDelay);
            observeDelayPolicy?.Invoke(delayPolicy, 418, typeof(IOException));

            // Cancel the function after 2 or 3 invocations
            int i = passResultPolicy ? 3 : 2;

            func.Invoking += c =>
            {
                if (c.Calls == i)
                    tokenSource.Cancel();
            };

            // Begin the invocation
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            int r = i - 1;
            int x = passResultPolicy ? r / 2 : 0;
            int y = passResultPolicy ? r / 2 : r;
            Assert.AreEqual(i, func           .Calls);
            Assert.AreEqual(x, resultPolicy   .Calls);
            Assert.AreEqual(y, exceptionPolicy.Calls);
            Assert.AreEqual(r, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT16_Canceled_Delay

        private void WithRetryT16_Canceled_Delay<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TFunc, int, ResultPolicy<int>, ExceptionPolicy, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken, int> invoke,
            Action<TFuncProxy, TimeSpan> observeFunc,
            Action<TDelayPolicyProxy, int, Type> observeDelayPolicy,
            bool passResultPolicy)
            where TFunc             : Delegate
            where TDelayPolicy      : Delegate
            where TFuncProxy        : DelegateProxy<TFunc>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined func that continues to fail with transient exceptions until it's canceled
            Func<int> flakyFunc = passResultPolicy ? FlakyFunc.Create<int, IOException>(418) : () => throw new IOException();
            TFuncProxy func = funcFactory(t => flakyFunc());

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy    = new FuncProxy<int, ResultKind>(r => r == 418 ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Transient.Invoke);
            TDelayPolicyProxy delayPolicy = delayPolicyFactory(Constants.Delay);

            // Create the reliable function
            TFunc reliableFunc = withRetry(
                func.Proxy,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy.Proxy);

            // Define expectations
            resultPolicy   .Invoking += Expect.Result(418);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            observeFunc       ?.Invoke(func, Constants.MinDelay);
            observeDelayPolicy?.Invoke(delayPolicy, 418, typeof(IOException));

            // Cancel the delay after 1 or 2 invocations
            int r = passResultPolicy ? 2 : 1;

            delayPolicy.Invoking += c =>
            {
                if (c.Calls == r)
                    tokenSource.Cancel();
            };

            // Begin the invocation
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            int x = passResultPolicy ? r / 2 : 0;
            int y = passResultPolicy ? r / 2 : r;
            Assert.AreEqual(r, func           .Calls);
            Assert.AreEqual(x, resultPolicy   .Calls);
            Assert.AreEqual(y, exceptionPolicy.Calls);
            Assert.AreEqual(r, delayPolicy    .Calls);
            
        }

        #endregion

    }
}
