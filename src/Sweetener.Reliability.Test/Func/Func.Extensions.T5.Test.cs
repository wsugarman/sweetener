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
    using DelayHandlerProxy          = FuncProxy<int, TimeSpan>;
    using ComplexDelayHandlerProxy   = FuncProxy<int, int, Exception, TimeSpan>;

    partial class FuncExtensionsTest : FuncExtensionsTestBase
    {
        [TestMethod]
        public void WithRetryT5_DelayHandler()
        {
            TestFunc nullFunc = null;
            TestFunc func     = (arg1, arg2, arg3, arg4) => 12345;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, ExceptionPolicy.Transient, (DelayHandler)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy((arg1, arg2, arg3, arg4) => f(CancellationToken.None));
            Func<TimeSpan, DelayHandlerProxy> delayHandlerFactory = t => new DelayHandlerProxy((i) => t);
            Func<TestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, TimeSpan>, TestFunc> withRetry = (f, m, r, e, d) => f.WithRetry(m, e, d.Invoke);
            Func<TestFunc, int, string, double, long, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, token) => f(arg1, arg2, arg3, arg4);

            Action<TestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long>(Arguments.Validate, delay);

            // Success
            WithRetryT5_Success        (funcFactory, delayHandlerFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultHandler: false);
            WithRetryT5_EventualSuccess(funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);

            // Failure (Exception)
            WithRetryT5_Failure_Exception         (funcFactory, delayHandlerFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultHandler: false);
            WithRetryT5_EventualFailure_Exception (funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);
            WithRetryT5_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);
        }

        [TestMethod]
        public void WithRetryT5_ComplexDelayHandler()
        {
            TestFunc nullFunc = null;
            TestFunc func     = (arg1, arg2, arg3, arg4) => 12345;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                     , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, ExceptionPolicy.Transient, (ComplexDelayHandler<int>)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy((arg1, arg2, arg3, arg4) => f(CancellationToken.None));
            Func<TimeSpan, ComplexDelayHandlerProxy> delayHandlerFactory = t => new ComplexDelayHandlerProxy((i, r, e) => t);
            Func<TestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, int, Exception, TimeSpan>, TestFunc> withRetry = (f, m, r, e, d) => f.WithRetry(m, e, d.Invoke);
            Func<TestFunc, int, string, double, long, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, token) => f(arg1, arg2, arg3, arg4);

            Action<TestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long>(Arguments.Validate, delay);

            // Success
            WithRetryT5_Success        (funcFactory, delayHandlerFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultHandler: false);
            WithRetryT5_EventualSuccess(funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);

            // Failure (Exception)
            WithRetryT5_Failure_Exception         (funcFactory, delayHandlerFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultHandler: false);
            WithRetryT5_EventualFailure_Exception (funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);
            WithRetryT5_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);
        }

        [TestMethod]
        public void WithRetryT5_ResultPolicy_DelayHandler()
        {
            TestFunc nullFunc = null;
            TestFunc func     = (arg1, arg2, arg3, arg4) => 12345;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, r => ResultKind.Successful, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                      , ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, (DelayHandler)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy((arg1, arg2, arg3, arg4) => f(CancellationToken.None));
            Func<TimeSpan, DelayHandlerProxy> delayHandlerFactory = t => new DelayHandlerProxy((i) => t);
            Func<TestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, TimeSpan>, TestFunc> withRetry = (f, m, r, e, d) => f.WithRetry(m, r, e, d.Invoke);
            Func<TestFunc, int, string, double, long, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, token) => f(arg1, arg2, arg3, arg4);

            Action<TestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long>(Arguments.Validate, delay);

            // Success
            WithRetryT5_Success        (funcFactory, delayHandlerFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultHandler: true);
            WithRetryT5_EventualSuccess(funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);

            // Failure (Result)
            WithRetryT5_Failure_Result         (funcFactory, delayHandlerFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>());
            WithRetryT5_EventualFailure_Result (funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());
            WithRetryT5_RetriesExhausted_Result(funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            WithRetryT5_Failure_Exception         (funcFactory, delayHandlerFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultHandler: true);
            WithRetryT5_EventualFailure_Exception (funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);
            WithRetryT5_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);
        }

        [TestMethod]
        public void WithRetryT5_ResultPolicy_ComplexDelayHandler()
        {
            TestFunc nullFunc = null;
            TestFunc func     = (arg1, arg2, arg3, arg4) => 12345;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, r => ResultKind.Successful, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                      , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, null                     , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, (ComplexDelayHandler<int>)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy((arg1, arg2, arg3, arg4) => f(CancellationToken.None));
            Func<TimeSpan, ComplexDelayHandlerProxy> delayHandlerFactory = t => new ComplexDelayHandlerProxy((i, r, e) => t);
            Func<TestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, int, Exception, TimeSpan>, TestFunc> withRetry = (f, m, r, e, d) => f.WithRetry(m, r, e, d.Invoke);
            Func<TestFunc, int, string, double, long, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, token) => f(arg1, arg2, arg3, arg4);

            Action<TestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long>(Arguments.Validate, delay);

            // Success
            WithRetryT5_Success        (funcFactory, delayHandlerFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultHandler: true);
            WithRetryT5_EventualSuccess(funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);

            // Failure (Result)
            WithRetryT5_Failure_Result         (funcFactory, delayHandlerFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>());
            WithRetryT5_EventualFailure_Result (funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));
            WithRetryT5_RetriesExhausted_Result(funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));

            // Failure (Exception)
            WithRetryT5_Failure_Exception         (funcFactory, delayHandlerFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultHandler: true);
            WithRetryT5_EventualFailure_Exception (funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);
            WithRetryT5_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);
        }

        [TestMethod]
        public void WithRetryT5_WithToken_DelayHandler()
        {
            InterruptableTestFunc nullFunc = null;
            InterruptableTestFunc func     = (arg1, arg2, arg3, arg4, token) => 12345;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, ExceptionPolicy.Transient, (DelayHandler)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy((arg1, arg2, arg3, arg4, token) => f(token));
            Func<TimeSpan, DelayHandlerProxy> delayHandlerFactory = t => new DelayHandlerProxy((i) => t);
            Func<InterruptableTestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, TimeSpan>, InterruptableTestFunc> withRetry = (f, m, r, e, d) => f.WithRetry(m, e, d.Invoke);
            Func<InterruptableTestFunc, int, string, double, long, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, token) => f(arg1, arg2, arg3, arg4, token);

            Action<InterruptableTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT5_Success        (funcFactory, delayHandlerFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultHandler: false);
            WithRetryT5_EventualSuccess(funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);

            // Failure (Exception)
            WithRetryT5_Failure_Exception         (funcFactory, delayHandlerFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultHandler: false);
            WithRetryT5_EventualFailure_Exception (funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);
            WithRetryT5_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);

            // Cancel
            WithRetryT5_Canceled_Func (funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);
            WithRetryT5_Canceled_Delay(funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);
        }

        [TestMethod]
        public void WithRetryT5_WithToken_ComplexDelayHandler()
        {
            InterruptableTestFunc nullFunc = null;
            InterruptableTestFunc func     = (arg1, arg2, arg3, arg4, token) => 12345;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                     , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, ExceptionPolicy.Transient, (ComplexDelayHandler<int>)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy((arg1, arg2, arg3, arg4, token) => f(token));
            Func<TimeSpan, ComplexDelayHandlerProxy> delayHandlerFactory = t => new ComplexDelayHandlerProxy((i, r, e) => t);
            Func<InterruptableTestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, int, Exception, TimeSpan>, InterruptableTestFunc> withRetry = (f, m, r, e, d) => f.WithRetry(m, e, d.Invoke);
            Func<InterruptableTestFunc, int, string, double, long, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, token) => f(arg1, arg2, arg3, arg4, token);

            Action<InterruptableTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT5_Success        (funcFactory, delayHandlerFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultHandler: false);
            WithRetryT5_EventualSuccess(funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);

            // Failure (Exception)
            WithRetryT5_Failure_Exception         (funcFactory, delayHandlerFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultHandler: false);
            WithRetryT5_EventualFailure_Exception (funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);
            WithRetryT5_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);

            // Cancel
            WithRetryT5_Canceled_Func (funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);
            WithRetryT5_Canceled_Delay(funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);
        }

        [TestMethod]
        public void WithRetryT5_WithToken_ResultPolicy_DelayHandler()
        {
            InterruptableTestFunc nullFunc = null;
            InterruptableTestFunc func     = (arg1, arg2, arg3, arg4, token) => 12345;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, r => ResultKind.Successful, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                      , ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, (DelayHandler)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy((arg1, arg2, arg3, arg4, token) => f(token));
            Func<TimeSpan, DelayHandlerProxy> delayHandlerFactory = t => new DelayHandlerProxy((i) => t);
            Func<InterruptableTestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, TimeSpan>, InterruptableTestFunc> withRetry = (f, m, r, e, d) => f.WithRetry(m, r, e, d.Invoke);
            Func<InterruptableTestFunc, int, string, double, long, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, token) => f(arg1, arg2, arg3, arg4, token);

            Action<InterruptableTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT5_Success        (funcFactory, delayHandlerFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultHandler: true);
            WithRetryT5_EventualSuccess(funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);

            // Failure (Result)
            WithRetryT5_Failure_Result         (funcFactory, delayHandlerFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>());
            WithRetryT5_EventualFailure_Result (funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());
            WithRetryT5_RetriesExhausted_Result(funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            WithRetryT5_Failure_Exception         (funcFactory, delayHandlerFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultHandler: true);
            WithRetryT5_EventualFailure_Exception (funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);
            WithRetryT5_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);

            // Cancel
            WithRetryT5_Canceled_Func (funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);
            WithRetryT5_Canceled_Delay(funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);
        }

        [TestMethod]
        public void WithRetryT5_WithToken_ResultPolicy_ComplexDelayHandler()
        {
            InterruptableTestFunc nullFunc = null;
            InterruptableTestFunc func     = (arg1, arg2, arg3, arg4, token) => 12345;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, r => ResultKind.Successful, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                      , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, null                     , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, (ComplexDelayHandler<int>)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy((arg1, arg2, arg3, arg4, token) => f(token));
            Func<TimeSpan, ComplexDelayHandlerProxy> delayHandlerFactory = t => new ComplexDelayHandlerProxy((i, r, e) => t);
            Func<InterruptableTestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, int, Exception, TimeSpan>, InterruptableTestFunc> withRetry = (f, m, r, e, d) => f.WithRetry(m, r, e, d.Invoke);
            Func<InterruptableTestFunc, int, string, double, long, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, token) => f(arg1, arg2, arg3, arg4, token);

            Action<InterruptableTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT5_Success        (funcFactory, delayHandlerFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultHandler: true);
            WithRetryT5_EventualSuccess(funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);

            // Failure (Result)
            WithRetryT5_Failure_Result         (funcFactory, delayHandlerFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>());
            WithRetryT5_EventualFailure_Result (funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));
            WithRetryT5_RetriesExhausted_Result(funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));

            // Failure (Exception)
            WithRetryT5_Failure_Exception         (funcFactory, delayHandlerFactory, withRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultHandler: true);
            WithRetryT5_EventualFailure_Exception (funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);
            WithRetryT5_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);

            // Cancel
            WithRetryT5_Canceled_Func (funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);
            WithRetryT5_Canceled_Delay(funcFactory, delayHandlerFactory, withRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);
        }
    }
}
