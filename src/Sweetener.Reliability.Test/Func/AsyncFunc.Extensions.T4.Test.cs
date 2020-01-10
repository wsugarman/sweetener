// Generated from AsyncFunc.Extensions.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    // Define type aliases for the various generic types used below as they can become pretty cumbersome
    using TestFunc                   = Func     <int, string, double, Task<int>>;
    using InterruptableTestFunc      = Func     <int, string, double, CancellationToken, Task<int>>;
    using TestFuncProxy              = FuncProxy<int, string, double, Task<int>>;
    using InterruptableTestFuncProxy = FuncProxy<int, string, double, CancellationToken, Task<int>>;
    using DelayPolicyProxy           = FuncProxy<int, TimeSpan>;
    using ComplexDelayPolicyProxy    = FuncProxy<int, int, Exception, TimeSpan>;

    partial class AsyncFuncExtensionsTest : FuncExtensionsTestBase
    {
        [TestMethod]
        public void WithAsyncRetryT4_Async_DelayPolicy()
        {
            TestFunc nullFunc = null;
            TestFunc func     = async (arg1, arg2, arg3) => await Task.Run(() => 12345).ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy(async (arg1, arg2, arg3) => { await Task.CompletedTask.ConfigureAwait(false); return f(CancellationToken.None); });
            Func<TimeSpan, DelayPolicyProxy> delayPolicyFactory = t => new DelayPolicyProxy((i) => t);
            Func<TestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, TimeSpan>, TestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, e, d.Invoke);
            Func<TestFunc, int, string, double, CancellationToken, int> invoke = (f, arg1, arg2, arg3, token) => f(arg1, arg2, arg3).Result;

            Action<TestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double>(Arguments.Validate, delay);

            // Success
            WithRetryT4_Success        (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: false);
            WithRetryT4_EventualSuccess(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);

            // Failure (Exception)
            WithRetryT4_Failure_Exception         (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: false);
            WithRetryT4_EventualFailure_Exception (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);
            WithRetryT4_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);
        }

        [TestMethod]
        public void WithAsyncRetryT4_Async_ComplexDelayPolicy()
        {
            TestFunc nullFunc = null;
            TestFunc func     = async (arg1, arg2, arg3) => await Task.Run(() => 12345).ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                       , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy<int>)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy(async (arg1, arg2, arg3) => { await Task.CompletedTask.ConfigureAwait(false); return f(CancellationToken.None); });
            Func<TimeSpan, ComplexDelayPolicyProxy> delayPolicyFactory = t => new ComplexDelayPolicyProxy((i, r, e) => t);
            Func<TestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, int, Exception, TimeSpan>, TestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, e, d.Invoke);
            Func<TestFunc, int, string, double, CancellationToken, int> invoke = (f, arg1, arg2, arg3, token) => f(arg1, arg2, arg3).Result;

            Action<TestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double>(Arguments.Validate, delay);

            // Success
            WithRetryT4_Success        (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: false);
            WithRetryT4_EventualSuccess(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);

            // Failure (Exception)
            WithRetryT4_Failure_Exception         (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: false);
            WithRetryT4_EventualFailure_Exception (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);
            WithRetryT4_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);
        }

        [TestMethod]
        public void WithAsyncRetryT4_Async_ResultPolicy_DelayPolicy()
        {
            TestFunc nullFunc = null;
            TestFunc func     = async (arg1, arg2, arg3) => await Task.Run(() => 12345).ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, r => ResultKind.Successful, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                      , ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (DelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy(async (arg1, arg2, arg3) => { await Task.CompletedTask.ConfigureAwait(false); return f(CancellationToken.None); });
            Func<TimeSpan, DelayPolicyProxy> delayPolicyFactory = t => new DelayPolicyProxy((i) => t);
            Func<TestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, TimeSpan>, TestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, r, e, d.Invoke);
            Func<TestFunc, int, string, double, CancellationToken, int> invoke = (f, arg1, arg2, arg3, token) => f(arg1, arg2, arg3).Result;

            Action<TestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double>(Arguments.Validate, delay);

            // Success
            WithRetryT4_Success        (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: true);
            WithRetryT4_EventualSuccess(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);

            // Failure (Result)
            WithRetryT4_Failure_Result         (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>());
            WithRetryT4_EventualFailure_Result (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());
            WithRetryT4_RetriesExhausted_Result(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            WithRetryT4_Failure_Exception         (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: true);
            WithRetryT4_EventualFailure_Exception (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);
            WithRetryT4_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);
        }

        [TestMethod]
        public void WithAsyncRetryT4_Async_ResultPolicy_ComplexDelayPolicy()
        {
            TestFunc nullFunc = null;
            TestFunc func     = async (arg1, arg2, arg3) => await Task.Run(() => 12345).ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, r => ResultKind.Successful, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                      , ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, null                       , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (ComplexDelayPolicy<int>)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy(async (arg1, arg2, arg3) => { await Task.CompletedTask.ConfigureAwait(false); return f(CancellationToken.None); });
            Func<TimeSpan, ComplexDelayPolicyProxy> delayPolicyFactory = t => new ComplexDelayPolicyProxy((i, r, e) => t);
            Func<TestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, int, Exception, TimeSpan>, TestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, r, e, d.Invoke);
            Func<TestFunc, int, string, double, CancellationToken, int> invoke = (f, arg1, arg2, arg3, token) => f(arg1, arg2, arg3).Result;

            Action<TestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double>(Arguments.Validate, delay);

            // Success
            WithRetryT4_Success        (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: true);
            WithRetryT4_EventualSuccess(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);

            // Failure (Result)
            WithRetryT4_Failure_Result         (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>());
            WithRetryT4_EventualFailure_Result (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));
            WithRetryT4_RetriesExhausted_Result(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));

            // Failure (Exception)
            WithRetryT4_Failure_Exception         (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: true);
            WithRetryT4_EventualFailure_Exception (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);
            WithRetryT4_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);
        }

        [TestMethod]
        public void WithAsyncRetryT4_Async_WithToken_DelayPolicy()
        {
            InterruptableTestFunc nullFunc = null;
            InterruptableTestFunc func     = async (arg1, arg2, arg3, token) => await Task.Run(() => 12345).ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy(async (arg1, arg2, arg3, token) => { await Task.CompletedTask.ConfigureAwait(false); return f(token); });
            Func<TimeSpan, DelayPolicyProxy> delayPolicyFactory = t => new DelayPolicyProxy((i) => t);
            Func<InterruptableTestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, TimeSpan>, InterruptableTestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, e, d.Invoke);
            Func<InterruptableTestFunc, int, string, double, CancellationToken, int> invoke = (f, arg1, arg2, arg3, token) => f(arg1, arg2, arg3, token).Result;

            Action<InterruptableTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT4_Success        (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: false);
            WithRetryT4_EventualSuccess(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);

            // Failure (Exception)
            WithRetryT4_Failure_Exception         (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: false);
            WithRetryT4_EventualFailure_Exception (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);
            WithRetryT4_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);

            // Cancel
            WithRetryT4_Canceled_Func (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);
            WithRetryT4_Canceled_Delay(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: false);
        }
        [TestMethod]
        public void WithAsyncRetryT4_Async_WithToken_ComplexDelayPolicy()
        {
            InterruptableTestFunc nullFunc = null;
            InterruptableTestFunc func     = async (arg1, arg2, arg3, token) => await Task.Run(() => 12345).ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                       , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy<int>)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy(async (arg1, arg2, arg3, token) => { await Task.CompletedTask.ConfigureAwait(false); return f(token); });
            Func<TimeSpan, ComplexDelayPolicyProxy> delayPolicyFactory = t => new ComplexDelayPolicyProxy((i, r, e) => t);
            Func<InterruptableTestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, int, Exception, TimeSpan>, InterruptableTestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, e, d.Invoke);
            Func<InterruptableTestFunc, int, string, double, CancellationToken, int> invoke = (f, arg1, arg2, arg3, token) => f(arg1, arg2, arg3, token).Result;

            Action<InterruptableTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT4_Success        (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: false);
            WithRetryT4_EventualSuccess(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);

            // Failure (Exception)
            WithRetryT4_Failure_Exception         (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: false);
            WithRetryT4_EventualFailure_Exception (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);
            WithRetryT4_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);

            // Cancel
            WithRetryT4_Canceled_Func (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);
            WithRetryT4_Canceled_Delay(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultPolicy: false);
        }
        [TestMethod]
        public void WithAsyncRetryT4_Async_WithToken_ResultPolicy_DelayPolicy()
        {
            InterruptableTestFunc nullFunc = null;
            InterruptableTestFunc func     = async (arg1, arg2, arg3, token) => await Task.Run(() => 12345).ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, r => ResultKind.Successful, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                      , ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (DelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy(async (arg1, arg2, arg3, token) => { await Task.CompletedTask.ConfigureAwait(false); return f(token); });
            Func<TimeSpan, DelayPolicyProxy> delayPolicyFactory = t => new DelayPolicyProxy((i) => t);
            Func<InterruptableTestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, TimeSpan>, InterruptableTestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, r, e, d.Invoke);
            Func<InterruptableTestFunc, int, string, double, CancellationToken, int> invoke = (f, arg1, arg2, arg3, token) => f(arg1, arg2, arg3, token).Result;

            Action<InterruptableTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT4_Success        (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: true);
            WithRetryT4_EventualSuccess(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);

            // Failure (Result)
            WithRetryT4_Failure_Result         (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>());
            WithRetryT4_EventualFailure_Result (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());
            WithRetryT4_RetriesExhausted_Result(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            WithRetryT4_Failure_Exception         (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultPolicy: true);
            WithRetryT4_EventualFailure_Exception (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);
            WithRetryT4_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);

            // Cancel
            WithRetryT4_Canceled_Func (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);
            WithRetryT4_Canceled_Delay(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultPolicy: true);
        }
        [TestMethod]
        public void WithAsyncRetryT4_Async_WithToken_ResultPolicy_ComplexDelayPolicy()
        {
            InterruptableTestFunc nullFunc = null;
            InterruptableTestFunc func     = async (arg1, arg2, arg3, token) => await Task.Run(() => 12345).ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, r => ResultKind.Successful, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                      , ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, null                       , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (ComplexDelayPolicy<int>)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy(async (arg1, arg2, arg3, token) => { await Task.CompletedTask.ConfigureAwait(false); return f(token); });
            Func<TimeSpan, ComplexDelayPolicyProxy> delayPolicyFactory = t => new ComplexDelayPolicyProxy((i, r, e) => t);
            Func<InterruptableTestFunc, int, ResultPolicy<int>, ExceptionPolicy, Func<int, int, Exception, TimeSpan>, InterruptableTestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, r, e, d.Invoke);
            Func<InterruptableTestFunc, int, string, double, CancellationToken, int> invoke = (f, arg1, arg2, arg3, token) => f(arg1, arg2, arg3, token).Result;

            Action<InterruptableTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT4_Success        (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: true);
            WithRetryT4_EventualSuccess(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);

            // Failure (Result)
            WithRetryT4_Failure_Result         (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>());
            WithRetryT4_EventualFailure_Result (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));
            WithRetryT4_RetriesExhausted_Result(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));

            // Failure (Exception)
            WithRetryT4_Failure_Exception         (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultPolicy: true);
            WithRetryT4_EventualFailure_Exception (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);
            WithRetryT4_RetriesExhausted_Exception(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);

            // Cancel
            WithRetryT4_Canceled_Func (funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);
            WithRetryT4_Canceled_Delay(funcFactory, delayPolicyFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultPolicy: true);
        }
    }
}
