// Generated from Func.Extensions.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    // Define type aliases for the various generic types used below as they can become pretty cumbersome
    using TestFunc                   = Func     <int, string, double, long, int>;
    using InterruptableTestFunc      = Func     <int, string, double, long, CancellationToken, int>;
    using TestFuncProxy              = FuncProxy<int, string, double, long, int>;
    using InterruptableTestFuncProxy = FuncProxy<int, string, double, long, CancellationToken, int>;
    using DelayPolicyProxy           = FuncProxy<int, TimeSpan>;
    using ComplexDelayPolicyProxy    = FuncProxy<int, int, Exception, TimeSpan>;

    partial class FuncExtensionsTest : FuncExtensionsTestBase
    {
        [TestMethod]
        public void WithRetryT5_DelayPolicy()
        {
            TestFunc nullFunc = null;
            TestFunc func     = (arg1, arg2, arg3, arg4) => 12345;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy((arg1, arg2, arg3, arg4) => f(CancellationToken.None));
            Func<TimeSpan, DelayPolicyProxy> delayPolicyFactory = t => new DelayPolicyProxy((i) => t);
            Func<TestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, TimeSpan>, TestFunc> withRetry = (f, m, r, e, d) => f.WithRetry(m, e, d.Invoke);
            Func<TestFunc, int, string, double, long, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, token) => f(arg1, arg2, arg3, arg4);

            Action<TestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long>(Arguments.Validate, delay);

            // Success
            WithRetryT5_Success        (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: false);
            WithRetryT5_EventualSuccess(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);

            // Failure (Exception)
            WithRetryT5_Failure_Exception         (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: false);
            WithRetryT5_EventualFailure_Exception (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);
            WithRetryT5_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);
        }

        [TestMethod]
        public void WithRetryT5_ComplexDelayPolicy()
        {
            TestFunc nullFunc = null;
            TestFunc func     = (arg1, arg2, arg3, arg4) => 12345;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                       , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy<int>)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy((arg1, arg2, arg3, arg4) => f(CancellationToken.None));
            Func<TimeSpan, ComplexDelayPolicyProxy> delayPolicyFactory = t => new ComplexDelayPolicyProxy((i, r, e) => t);
            Func<TestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, int, Exception, TimeSpan>, TestFunc> withRetry = (f, m, r, e, d) => f.WithRetry(m, e, d.Invoke);
            Func<TestFunc, int, string, double, long, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, token) => f(arg1, arg2, arg3, arg4);

            Action<TestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long>(Arguments.Validate, delay);

            // Success
            WithRetryT5_Success        (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: false);
            WithRetryT5_EventualSuccess(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);

            // Failure (Exception)
            WithRetryT5_Failure_Exception         (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: false);
            WithRetryT5_EventualFailure_Exception (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);
            WithRetryT5_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);
        }

        [TestMethod]
        public void WithRetryT5_ResultPolicy_DelayPolicy()
        {
            TestFunc nullFunc = null;
            TestFunc func     = (arg1, arg2, arg3, arg4) => 12345;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, r => ResultKind.Successful, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                      , ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (DelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy((arg1, arg2, arg3, arg4) => f(CancellationToken.None));
            Func<TimeSpan, DelayPolicyProxy> delayPolicyFactory = t => new DelayPolicyProxy((i) => t);
            Func<TestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, TimeSpan>, TestFunc> withRetry = (f, m, r, e, d) => f.WithRetry(m, r, e, d.Invoke);
            Func<TestFunc, int, string, double, long, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, token) => f(arg1, arg2, arg3, arg4);

            Action<TestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long>(Arguments.Validate, delay);

            // Success
            WithRetryT5_Success        (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: true);
            WithRetryT5_EventualSuccess(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);

            // Failure (Result)
            WithRetryT5_Failure_Result         (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>());
            WithRetryT5_EventualFailure_Result (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());
            WithRetryT5_RetriesExhausted_Result(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            WithRetryT5_Failure_Exception         (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: true);
            WithRetryT5_EventualFailure_Exception (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);
            WithRetryT5_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);
        }

        [TestMethod]
        public void WithRetryT5_ResultPolicy_ComplexDelayPolicy()
        {
            TestFunc nullFunc = null;
            TestFunc func     = (arg1, arg2, arg3, arg4) => 12345;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, r => ResultKind.Successful, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                      , ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, null                       , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (ComplexDelayPolicy<int>)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy((arg1, arg2, arg3, arg4) => f(CancellationToken.None));
            Func<TimeSpan, ComplexDelayPolicyProxy> delayPolicyFactory = t => new ComplexDelayPolicyProxy((i, r, e) => t);
            Func<TestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, int, Exception, TimeSpan>, TestFunc> withRetry = (f, m, r, e, d) => f.WithRetry(m, r, e, d.Invoke);
            Func<TestFunc, int, string, double, long, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, token) => f(arg1, arg2, arg3, arg4);

            Action<TestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long>(Arguments.Validate, delay);

            // Success
            WithRetryT5_Success        (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: true);
            WithRetryT5_EventualSuccess(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);

            // Failure (Result)
            WithRetryT5_Failure_Result         (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>());
            WithRetryT5_EventualFailure_Result (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));
            WithRetryT5_RetriesExhausted_Result(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));

            // Failure (Exception)
            WithRetryT5_Failure_Exception         (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: true);
            WithRetryT5_EventualFailure_Exception (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);
            WithRetryT5_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);
        }

        [TestMethod]
        public void WithRetryT5_WithToken_DelayPolicy()
        {
            InterruptableTestFunc nullFunc = null;
            InterruptableTestFunc func     = (arg1, arg2, arg3, arg4, token) => 12345;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy((arg1, arg2, arg3, arg4, token) => f(token));
            Func<TimeSpan, DelayPolicyProxy> delayPolicyFactory = t => new DelayPolicyProxy((i) => t);
            Func<InterruptableTestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, TimeSpan>, InterruptableTestFunc> withRetry = (f, m, r, e, d) => f.WithRetry(m, e, d.Invoke);
            Func<InterruptableTestFunc, int, string, double, long, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, token) => f(arg1, arg2, arg3, arg4, token);

            Action<InterruptableTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT5_Success        (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: false);
            WithRetryT5_EventualSuccess(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);

            // Failure (Exception)
            WithRetryT5_Failure_Exception         (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: false);
            WithRetryT5_EventualFailure_Exception (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);
            WithRetryT5_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);

            // Cancel
            WithRetryT5_Canceled_Func (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);
            WithRetryT5_Canceled_Delay(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);
        }
        [TestMethod]
        public void WithRetryT5_WithToken_ComplexDelayPolicy()
        {
            InterruptableTestFunc nullFunc = null;
            InterruptableTestFunc func     = (arg1, arg2, arg3, arg4, token) => 12345;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                       , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy<int>)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy((arg1, arg2, arg3, arg4, token) => f(token));
            Func<TimeSpan, ComplexDelayPolicyProxy> delayPolicyFactory = t => new ComplexDelayPolicyProxy((i, r, e) => t);
            Func<InterruptableTestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, int, Exception, TimeSpan>, InterruptableTestFunc> withRetry = (f, m, r, e, d) => f.WithRetry(m, e, d.Invoke);
            Func<InterruptableTestFunc, int, string, double, long, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, token) => f(arg1, arg2, arg3, arg4, token);

            Action<InterruptableTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT5_Success        (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: false);
            WithRetryT5_EventualSuccess(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);

            // Failure (Exception)
            WithRetryT5_Failure_Exception         (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: false);
            WithRetryT5_EventualFailure_Exception (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);
            WithRetryT5_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);

            // Cancel
            WithRetryT5_Canceled_Func (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);
            WithRetryT5_Canceled_Delay(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);
        }
        [TestMethod]
        public void WithRetryT5_WithToken_ResultPolicy_DelayPolicy()
        {
            InterruptableTestFunc nullFunc = null;
            InterruptableTestFunc func     = (arg1, arg2, arg3, arg4, token) => 12345;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, r => ResultKind.Successful, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                      , ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (DelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy((arg1, arg2, arg3, arg4, token) => f(token));
            Func<TimeSpan, DelayPolicyProxy> delayPolicyFactory = t => new DelayPolicyProxy((i) => t);
            Func<InterruptableTestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, TimeSpan>, InterruptableTestFunc> withRetry = (f, m, r, e, d) => f.WithRetry(m, r, e, d.Invoke);
            Func<InterruptableTestFunc, int, string, double, long, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, token) => f(arg1, arg2, arg3, arg4, token);

            Action<InterruptableTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT5_Success        (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: true);
            WithRetryT5_EventualSuccess(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);

            // Failure (Result)
            WithRetryT5_Failure_Result         (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>());
            WithRetryT5_EventualFailure_Result (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());
            WithRetryT5_RetriesExhausted_Result(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            WithRetryT5_Failure_Exception         (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: true);
            WithRetryT5_EventualFailure_Exception (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);
            WithRetryT5_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);

            // Cancel
            WithRetryT5_Canceled_Func (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);
            WithRetryT5_Canceled_Delay(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);
        }
        [TestMethod]
        public void WithRetryT5_WithToken_ResultPolicy_ComplexDelayPolicy()
        {
            InterruptableTestFunc nullFunc = null;
            InterruptableTestFunc func     = (arg1, arg2, arg3, arg4, token) => 12345;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, r => ResultKind.Successful, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                      , ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, null                       , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (ComplexDelayPolicy<int>)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy((arg1, arg2, arg3, arg4, token) => f(token));
            Func<TimeSpan, ComplexDelayPolicyProxy> delayPolicyFactory = t => new ComplexDelayPolicyProxy((i, r, e) => t);
            Func<InterruptableTestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, int, Exception, TimeSpan>, InterruptableTestFunc> withRetry = (f, m, r, e, d) => f.WithRetry(m, r, e, d.Invoke);
            Func<InterruptableTestFunc, int, string, double, long, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, token) => f(arg1, arg2, arg3, arg4, token);

            Action<InterruptableTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT5_Success        (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: true);
            WithRetryT5_EventualSuccess(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);

            // Failure (Result)
            WithRetryT5_Failure_Result         (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>());
            WithRetryT5_EventualFailure_Result (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));
            WithRetryT5_RetriesExhausted_Result(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));

            // Failure (Exception)
            WithRetryT5_Failure_Exception         (funcFactory, delayPolicyFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: true);
            WithRetryT5_EventualFailure_Exception (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);
            WithRetryT5_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);

            // Cancel
            WithRetryT5_Canceled_Func (funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);
            WithRetryT5_Canceled_Delay(funcFactory, delayPolicyFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);
        }
    }
}
