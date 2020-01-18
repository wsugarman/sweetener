// Generated from AsyncFunc.Extensions.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    // Define type aliases for the various generic types used below as they can become pretty cumbersome
    using TestFunc                   = Func     <int, string, double, long, ushort, byte, TimeSpan, uint, Task<int>>;
    using InterruptableTestFunc      = Func     <int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, Task<int>>;
    using TestFuncProxy              = FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Task<int>>;
    using InterruptableTestFuncProxy = FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, Task<int>>;
    using DelayHandlerProxy          = FuncProxy<int, TimeSpan>;
    using ComplexDelayHandlerProxy   = FuncProxy<int, int, Exception, TimeSpan>;

    partial class AsyncFuncExtensionsTest : FuncExtensionsTestBase
    {
        [TestMethod]
        public void WithAsyncRetryT9_Async_DelayHandler()
        {
            TestFunc nullFunc = null;
            TestFunc func     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => await Task.FromResult(12345);
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, ExceptionPolicy.Transient, (DelayHandler)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => await Task.FromResult(f(CancellationToken.None)));
            Func<TimeSpan, DelayHandlerProxy> delayHandlerFactory = t => new DelayHandlerProxy((i) => t);
            Func<TestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, TimeSpan>, TestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, e, d.Invoke);
            Func<TestFunc, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => f(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8).Result;

            Action<TestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint>(Arguments.Validate, delay);

            // Success
            WithRetryT9_Success        (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultHandler: false);
            WithRetryT9_EventualSuccess(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);

            // Failure (Exception)
            WithRetryT9_Failure_Exception         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultHandler: false);
            WithRetryT9_EventualFailure_Exception (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);
            WithRetryT9_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);
        }

        [TestMethod]
        public void WithAsyncRetryT9_Async_ComplexDelayHandler()
        {
            TestFunc nullFunc = null;
            TestFunc func     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => await Task.FromResult(12345);
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                     , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, ExceptionPolicy.Transient, (ComplexDelayHandler<int>)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => await Task.FromResult(f(CancellationToken.None)));
            Func<TimeSpan, ComplexDelayHandlerProxy> delayHandlerFactory = t => new ComplexDelayHandlerProxy((i, r, e) => t);
            Func<TestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, int, Exception, TimeSpan>, TestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, e, d.Invoke);
            Func<TestFunc, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => f(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8).Result;

            Action<TestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint>(Arguments.Validate, delay);

            // Success
            WithRetryT9_Success        (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultHandler: false);
            WithRetryT9_EventualSuccess(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);

            // Failure (Exception)
            WithRetryT9_Failure_Exception         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultHandler: false);
            WithRetryT9_EventualFailure_Exception (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);
            WithRetryT9_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);
        }

        [TestMethod]
        public void WithAsyncRetryT9_Async_ResultPolicy_DelayHandler()
        {
            TestFunc nullFunc = null;
            TestFunc func     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => await Task.FromResult(12345);
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, r => ResultKind.Successful, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                      , ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, (DelayHandler)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => await Task.FromResult(f(CancellationToken.None)));
            Func<TimeSpan, DelayHandlerProxy> delayHandlerFactory = t => new DelayHandlerProxy((i) => t);
            Func<TestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, TimeSpan>, TestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, r, e, d.Invoke);
            Func<TestFunc, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => f(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8).Result;

            Action<TestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint>(Arguments.Validate, delay);

            // Success
            WithRetryT9_Success        (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultHandler: true);
            WithRetryT9_EventualSuccess(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);

            // Failure (Result)
            WithRetryT9_Failure_Result         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>());
            WithRetryT9_EventualFailure_Result (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());
            WithRetryT9_RetriesExhausted_Result(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            WithRetryT9_Failure_Exception         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultHandler: true);
            WithRetryT9_EventualFailure_Exception (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);
            WithRetryT9_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);
        }

        [TestMethod]
        public void WithAsyncRetryT9_Async_ResultPolicy_ComplexDelayHandler()
        {
            TestFunc nullFunc = null;
            TestFunc func     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => await Task.FromResult(12345);
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, r => ResultKind.Successful, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                      , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, null                     , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, (ComplexDelayHandler<int>)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => await Task.FromResult(f(CancellationToken.None)));
            Func<TimeSpan, ComplexDelayHandlerProxy> delayHandlerFactory = t => new ComplexDelayHandlerProxy((i, r, e) => t);
            Func<TestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, int, Exception, TimeSpan>, TestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, r, e, d.Invoke);
            Func<TestFunc, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => f(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8).Result;

            Action<TestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint>(Arguments.Validate, delay);

            // Success
            WithRetryT9_Success        (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultHandler: true);
            WithRetryT9_EventualSuccess(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);

            // Failure (Result)
            WithRetryT9_Failure_Result         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>());
            WithRetryT9_EventualFailure_Result (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));
            WithRetryT9_RetriesExhausted_Result(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));

            // Failure (Exception)
            WithRetryT9_Failure_Exception         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultHandler: true);
            WithRetryT9_EventualFailure_Exception (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);
            WithRetryT9_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);
        }

        [TestMethod]
        public void WithAsyncRetryT9_Async_WithToken_DelayHandler()
        {
            InterruptableTestFunc nullFunc = null;
            InterruptableTestFunc func     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => await Task.FromResult(12345);
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, ExceptionPolicy.Transient, (DelayHandler)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => await Task.FromResult(f(token)));
            Func<TimeSpan, DelayHandlerProxy> delayHandlerFactory = t => new DelayHandlerProxy((i) => t);
            Func<InterruptableTestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, TimeSpan>, InterruptableTestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, e, d.Invoke);
            Func<InterruptableTestFunc, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => f(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token).Result;

            Action<InterruptableTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT9_Success        (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultHandler: false);
            WithRetryT9_EventualSuccess(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);

            // Failure (Exception)
            WithRetryT9_Failure_Exception         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultHandler: false);
            WithRetryT9_EventualFailure_Exception (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);
            WithRetryT9_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);

            // Cancel
            WithRetryT9_Canceled_Func (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);
            WithRetryT9_Canceled_Delay(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);

            // Cancel (Synchronous)
            funcFactory = f => new InterruptableTestFuncProxy((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => Task.FromResult(f(token)));
            WithRetryT9_Canceled_Func (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);
        }
        [TestMethod]
        public void WithAsyncRetryT9_Async_WithToken_ComplexDelayHandler()
        {
            InterruptableTestFunc nullFunc = null;
            InterruptableTestFunc func     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => await Task.FromResult(12345);
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                     , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, ExceptionPolicy.Transient, (ComplexDelayHandler<int>)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => await Task.FromResult(f(token)));
            Func<TimeSpan, ComplexDelayHandlerProxy> delayHandlerFactory = t => new ComplexDelayHandlerProxy((i, r, e) => t);
            Func<InterruptableTestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, int, Exception, TimeSpan>, InterruptableTestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, e, d.Invoke);
            Func<InterruptableTestFunc, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => f(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token).Result;

            Action<InterruptableTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT9_Success        (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultHandler: false);
            WithRetryT9_EventualSuccess(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);

            // Failure (Exception)
            WithRetryT9_Failure_Exception         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultHandler: false);
            WithRetryT9_EventualFailure_Exception (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);
            WithRetryT9_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);

            // Cancel
            WithRetryT9_Canceled_Func (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);
            WithRetryT9_Canceled_Delay(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);

            // Cancel (Synchronous)
            funcFactory = f => new InterruptableTestFuncProxy((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => Task.FromResult(f(token)));
            WithRetryT9_Canceled_Func (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);
        }
        [TestMethod]
        public void WithAsyncRetryT9_Async_WithToken_ResultPolicy_DelayHandler()
        {
            InterruptableTestFunc nullFunc = null;
            InterruptableTestFunc func     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => await Task.FromResult(12345);
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, r => ResultKind.Successful, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                      , ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, (DelayHandler)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => await Task.FromResult(f(token)));
            Func<TimeSpan, DelayHandlerProxy> delayHandlerFactory = t => new DelayHandlerProxy((i) => t);
            Func<InterruptableTestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, TimeSpan>, InterruptableTestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, r, e, d.Invoke);
            Func<InterruptableTestFunc, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => f(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token).Result;

            Action<InterruptableTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT9_Success        (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultHandler: true);
            WithRetryT9_EventualSuccess(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);

            // Failure (Result)
            WithRetryT9_Failure_Result         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>());
            WithRetryT9_EventualFailure_Result (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());
            WithRetryT9_RetriesExhausted_Result(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            WithRetryT9_Failure_Exception         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultHandler: true);
            WithRetryT9_EventualFailure_Exception (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);
            WithRetryT9_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);

            // Cancel
            WithRetryT9_Canceled_Func (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);
            WithRetryT9_Canceled_Delay(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);

            // Cancel (Synchronous)
            funcFactory = f => new InterruptableTestFuncProxy((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => Task.FromResult(f(token)));
            WithRetryT9_Canceled_Func (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);
        }
        [TestMethod]
        public void WithAsyncRetryT9_Async_WithToken_ResultPolicy_ComplexDelayHandler()
        {
            InterruptableTestFunc nullFunc = null;
            InterruptableTestFunc func     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => await Task.FromResult(12345);
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, r => ResultKind.Successful, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                      , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, null                     , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, (ComplexDelayHandler<int>)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => await Task.FromResult(f(token)));
            Func<TimeSpan, ComplexDelayHandlerProxy> delayHandlerFactory = t => new ComplexDelayHandlerProxy((i, r, e) => t);
            Func<InterruptableTestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, int, Exception, TimeSpan>, InterruptableTestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, r, e, d.Invoke);
            Func<InterruptableTestFunc, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, int> invoke = (f, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => f(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token).Result;

            Action<InterruptableTestFuncProxy>           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan> observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT9_Success        (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultHandler: true);
            WithRetryT9_EventualSuccess(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);

            // Failure (Result)
            WithRetryT9_Failure_Result         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>());
            WithRetryT9_EventualFailure_Result (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));
            WithRetryT9_RetriesExhausted_Result(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));

            // Failure (Exception)
            WithRetryT9_Failure_Exception         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultHandler: true);
            WithRetryT9_EventualFailure_Exception (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);
            WithRetryT9_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);

            // Cancel
            WithRetryT9_Canceled_Func (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);
            WithRetryT9_Canceled_Delay(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);

            // Cancel (Synchronous)
            funcFactory = f => new InterruptableTestFuncProxy((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => Task.FromResult(f(token)));
            WithRetryT9_Canceled_Func (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);
        }
    }
}
