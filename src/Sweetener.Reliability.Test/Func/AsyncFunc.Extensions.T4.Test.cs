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
    using DelayHandlerProxy          = FuncProxy<int, TimeSpan>;
    using ComplexDelayHandlerProxy   = FuncProxy<int, int, Exception?, TimeSpan>;

    partial class AsyncFuncExtensionsTest : FuncExtensionsTestBase
    {
        [TestMethod]
        public void WithAsyncRetryT4_Async_DelayHandler()
        {
            TestFunc? nullFunc = null;
            TestFunc  func     = async (arg1, arg2, arg3) => await Task.FromResult(12345);

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, ExceptionPolicy.Transient, (DelayHandler)null));

            // Test a function that returns a null Task
            TestFunc nullTaskFunc         = (arg1, arg2, arg3) => null;
            TestFunc nullTaskReliableFunc = nullTaskFunc.WithAsyncRetry(Retries.Infinite, ExceptionPolicy.Transient, DelayPolicy.None);
            Assert.ThrowsExceptionAsync<InvalidOperationException>(() => nullTaskReliableFunc(42, "foo", 3.14D)).Wait();

#nullable enable

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy(async (arg1, arg2, arg3) => await Task.FromResult(f(CancellationToken.None)));
            Func<TimeSpan, DelayHandlerProxy> delayHandlerFactory = t => new DelayHandlerProxy((i) => t);
            Func<TestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, TimeSpan>, TestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, e, d.Invoke);
            Func<TestFunc, int, string, double, CancellationToken, int> invoke = (f, arg1, arg2, arg3, token) => f(arg1, arg2, arg3).Result;

            Action<TestFuncProxy>?           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan>? observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double>(Arguments.Validate, delay);

            // Success
            WithRetryT4_Success        (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultHandler: false);
            WithRetryT4_EventualSuccess(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);

            // Failure (Exception)
            WithRetryT4_Failure_Exception         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>());
            WithRetryT4_EventualFailure_Exception (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);
            WithRetryT4_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);
        }

        [TestMethod]
        public void WithAsyncRetryT4_Async_ComplexDelayHandler()
        {
            TestFunc? nullFunc = null;
            TestFunc  func     = async (arg1, arg2, arg3) => await Task.FromResult(12345);

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                     , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, ExceptionPolicy.Transient, (ComplexDelayHandler<int>)null));

            // Test a function that returns a null Task
            TestFunc nullTaskFunc         = (arg1, arg2, arg3) => null;
            TestFunc nullTaskReliableFunc = nullTaskFunc.WithAsyncRetry(Retries.Infinite, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero);
            Assert.ThrowsExceptionAsync<InvalidOperationException>(() => nullTaskReliableFunc(42, "foo", 3.14D)).Wait();

#nullable enable

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy(async (arg1, arg2, arg3) => await Task.FromResult(f(CancellationToken.None)));
            Func<TimeSpan, ComplexDelayHandlerProxy> delayHandlerFactory = t => new ComplexDelayHandlerProxy((i, r, e) => t);
            Func<TestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, int, Exception?, TimeSpan>, TestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, e, d.Invoke);
            Func<TestFunc, int, string, double, CancellationToken, int> invoke = (f, arg1, arg2, arg3, token) => f(arg1, arg2, arg3).Result;

            Action<TestFuncProxy>?           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan>? observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double>(Arguments.Validate, delay);

            // Success
            WithRetryT4_Success        (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultHandler: false);
            WithRetryT4_EventualSuccess(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);

            // Failure (Exception)
            WithRetryT4_Failure_Exception         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>());
            WithRetryT4_EventualFailure_Exception (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);
            WithRetryT4_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);
        }

        [TestMethod]
        public void WithAsyncRetryT4_Async_ResultPolicy_DelayHandler()
        {
            TestFunc? nullFunc = null;
            TestFunc  func     = async (arg1, arg2, arg3) => await Task.FromResult(12345);

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, r => ResultKind.Successful, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                      , ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, (DelayHandler)null));

            // Test a function that returns a null Task
            TestFunc nullTaskFunc         = (arg1, arg2, arg3) => null;
            TestFunc nullTaskReliableFunc = nullTaskFunc.WithAsyncRetry(Retries.Infinite, ResultPolicy.Default<int>(), ExceptionPolicy.Transient, DelayPolicy.None);
            Assert.ThrowsExceptionAsync<InvalidOperationException>(() => nullTaskReliableFunc(42, "foo", 3.14D)).Wait();

#nullable enable

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy(async (arg1, arg2, arg3) => await Task.FromResult(f(CancellationToken.None)));
            Func<TimeSpan, DelayHandlerProxy> delayHandlerFactory = t => new DelayHandlerProxy((i) => t);
            Func<TestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, TimeSpan>, TestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, r, e, d.Invoke);
            Func<TestFunc, int, string, double, CancellationToken, int> invoke = (f, arg1, arg2, arg3, token) => f(arg1, arg2, arg3).Result;

            Action<TestFuncProxy>?           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan>? observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double>(Arguments.Validate, delay);

            // Success
            WithRetryT4_Success        (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultHandler: true);
            WithRetryT4_EventualSuccess(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);

            // Failure (Result)
            WithRetryT4_Failure_Result         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>());
            WithRetryT4_EventualFailure_Result (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());
            WithRetryT4_RetriesExhausted_Result(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            WithRetryT4_Failure_Exception         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>());
            WithRetryT4_EventualFailure_Exception (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);
            WithRetryT4_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);
        }

        [TestMethod]
        public void WithAsyncRetryT4_Async_ResultPolicy_ComplexDelayHandler()
        {
            TestFunc? nullFunc = null;
            TestFunc  func     = async (arg1, arg2, arg3) => await Task.FromResult(12345);

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, r => ResultKind.Successful, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                      , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, null                     , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, (ComplexDelayHandler<int>)null));

            // Test a function that returns a null Task
            TestFunc nullTaskFunc         = (arg1, arg2, arg3) => null;
            TestFunc nullTaskReliableFunc = nullTaskFunc.WithAsyncRetry(Retries.Infinite, ResultPolicy.Default<int>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero);
            Assert.ThrowsExceptionAsync<InvalidOperationException>(() => nullTaskReliableFunc(42, "foo", 3.14D)).Wait();

#nullable enable

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy(async (arg1, arg2, arg3) => await Task.FromResult(f(CancellationToken.None)));
            Func<TimeSpan, ComplexDelayHandlerProxy> delayHandlerFactory = t => new ComplexDelayHandlerProxy((i, r, e) => t);
            Func<TestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, int, Exception?, TimeSpan>, TestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, r, e, d.Invoke);
            Func<TestFunc, int, string, double, CancellationToken, int> invoke = (f, arg1, arg2, arg3, token) => f(arg1, arg2, arg3).Result;

            Action<TestFuncProxy>?           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan>? observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double>(Arguments.Validate, delay);

            // Success
            WithRetryT4_Success        (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultHandler: true);
            WithRetryT4_EventualSuccess(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);

            // Failure (Result)
            WithRetryT4_Failure_Result         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>());
            WithRetryT4_EventualFailure_Result (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));
            WithRetryT4_RetriesExhausted_Result(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));

            // Failure (Exception)
            WithRetryT4_Failure_Exception         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>());
            WithRetryT4_EventualFailure_Exception (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);
            WithRetryT4_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);
        }

        [TestMethod]
        public void WithAsyncRetryT4_Async_WithToken_DelayHandler()
        {
            InterruptableTestFunc? nullFunc = null;
            InterruptableTestFunc  func     = async (arg1, arg2, arg3, token) => await Task.FromResult(12345);

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, ExceptionPolicy.Transient, (DelayHandler)null));

            // Test a function that returns a null Task
            InterruptableTestFunc nullTaskFunc         = (arg1, arg2, arg3, token) => null;
            InterruptableTestFunc nullTaskReliableFunc = nullTaskFunc.WithAsyncRetry(Retries.Infinite, ExceptionPolicy.Transient, DelayPolicy.None);
            Assert.ThrowsExceptionAsync<InvalidOperationException>(() => nullTaskReliableFunc(42, "foo", 3.14D, CancellationToken.None)).Wait();

#nullable enable

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy(async (arg1, arg2, arg3, token) => await Task.FromResult(f(token)));
            Func<TimeSpan, DelayHandlerProxy> delayHandlerFactory = t => new DelayHandlerProxy((i) => t);
            Func<InterruptableTestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, TimeSpan>, InterruptableTestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, e, d.Invoke);
            Func<InterruptableTestFunc, int, string, double, CancellationToken, int> invoke = (f, arg1, arg2, arg3, token) => f(arg1, arg2, arg3, token).Result;

            Action<InterruptableTestFuncProxy>?           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan>? observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT4_Success        (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultHandler: false);
            WithRetryT4_EventualSuccess(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);

            // Failure (Exception)
            WithRetryT4_Failure_Exception         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>());
            WithRetryT4_EventualFailure_Exception (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);
            WithRetryT4_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);

            // Cancel
            WithRetryT4_Canceled      (funcFactory, delayHandlerFactory, withAsyncRetry, invoke);
            WithRetryT4_Canceled_Func (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);
            WithRetryT4_Canceled_Delay(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);

            // Cancel (Synchronous)
            funcFactory = f => new InterruptableTestFuncProxy((arg1, arg2, arg3, token) => Task.FromResult(f(token)));
            WithRetryT4_Canceled_Func (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);
        }

        [TestMethod]
        public void WithAsyncRetryT4_Async_WithToken_ComplexDelayHandler()
        {
            InterruptableTestFunc? nullFunc = null;
            InterruptableTestFunc  func     = async (arg1, arg2, arg3, token) => await Task.FromResult(12345);

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                     , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, ExceptionPolicy.Transient, (ComplexDelayHandler<int>)null));

            // Test a function that returns a null Task
            InterruptableTestFunc nullTaskFunc         = (arg1, arg2, arg3, token) => null;
            InterruptableTestFunc nullTaskReliableFunc = nullTaskFunc.WithAsyncRetry(Retries.Infinite, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero);
            Assert.ThrowsExceptionAsync<InvalidOperationException>(() => nullTaskReliableFunc(42, "foo", 3.14D, CancellationToken.None)).Wait();

#nullable enable

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy(async (arg1, arg2, arg3, token) => await Task.FromResult(f(token)));
            Func<TimeSpan, ComplexDelayHandlerProxy> delayHandlerFactory = t => new ComplexDelayHandlerProxy((i, r, e) => t);
            Func<InterruptableTestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, int, Exception?, TimeSpan>, InterruptableTestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, e, d.Invoke);
            Func<InterruptableTestFunc, int, string, double, CancellationToken, int> invoke = (f, arg1, arg2, arg3, token) => f(arg1, arg2, arg3, token).Result;

            Action<InterruptableTestFuncProxy>?           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan>? observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT4_Success        (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultHandler: false);
            WithRetryT4_EventualSuccess(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);

            // Failure (Exception)
            WithRetryT4_Failure_Exception         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>());
            WithRetryT4_EventualFailure_Exception (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);
            WithRetryT4_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);

            // Cancel
            WithRetryT4_Canceled      (funcFactory, delayHandlerFactory, withAsyncRetry, invoke);
            WithRetryT4_Canceled_Func (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);
            WithRetryT4_Canceled_Delay(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);

            // Cancel (Synchronous)
            funcFactory = f => new InterruptableTestFuncProxy((arg1, arg2, arg3, token) => Task.FromResult(f(token)));
            WithRetryT4_Canceled_Func (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);
        }

        [TestMethod]
        public void WithAsyncRetryT4_Async_WithToken_ResultPolicy_DelayHandler()
        {
            InterruptableTestFunc? nullFunc = null;
            InterruptableTestFunc  func     = async (arg1, arg2, arg3, token) => await Task.FromResult(12345);

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, r => ResultKind.Successful, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                      , ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, (DelayHandler)null));

            // Test a function that returns a null Task
            InterruptableTestFunc nullTaskFunc         = (arg1, arg2, arg3, token) => null;
            InterruptableTestFunc nullTaskReliableFunc = nullTaskFunc.WithAsyncRetry(Retries.Infinite, ResultPolicy.Default<int>(), ExceptionPolicy.Transient, DelayPolicy.None);
            Assert.ThrowsExceptionAsync<InvalidOperationException>(() => nullTaskReliableFunc(42, "foo", 3.14D, CancellationToken.None)).Wait();

#nullable enable

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy(async (arg1, arg2, arg3, token) => await Task.FromResult(f(token)));
            Func<TimeSpan, DelayHandlerProxy> delayHandlerFactory = t => new DelayHandlerProxy((i) => t);
            Func<InterruptableTestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, TimeSpan>, InterruptableTestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, r, e, d.Invoke);
            Func<InterruptableTestFunc, int, string, double, CancellationToken, int> invoke = (f, arg1, arg2, arg3, token) => f(arg1, arg2, arg3, token).Result;

            Action<InterruptableTestFuncProxy>?           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan>? observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT4_Success        (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultHandler: true);
            WithRetryT4_EventualSuccess(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);

            // Failure (Result)
            WithRetryT4_Failure_Result         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>());
            WithRetryT4_EventualFailure_Result (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());
            WithRetryT4_RetriesExhausted_Result(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            WithRetryT4_Failure_Exception         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>());
            WithRetryT4_EventualFailure_Exception (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);
            WithRetryT4_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);

            // Cancel
            WithRetryT4_Canceled      (funcFactory, delayHandlerFactory, withAsyncRetry, invoke);
            WithRetryT4_Canceled_Func (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);
            WithRetryT4_Canceled_Delay(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);

            // Cancel (Synchronous)
            funcFactory = f => new InterruptableTestFuncProxy((arg1, arg2, arg3, token) => Task.FromResult(f(token)));
            WithRetryT4_Canceled_Func (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);
        }

        [TestMethod]
        public void WithAsyncRetryT4_Async_WithToken_ResultPolicy_ComplexDelayHandler()
        {
            InterruptableTestFunc? nullFunc = null;
            InterruptableTestFunc  func     = async (arg1, arg2, arg3, token) => await Task.FromResult(12345);

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, r => ResultKind.Successful, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                      , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, null                     , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, (ComplexDelayHandler<int>)null));

            // Test a function that returns a null Task
            InterruptableTestFunc nullTaskFunc         = (arg1, arg2, arg3, token) => null;
            InterruptableTestFunc nullTaskReliableFunc = nullTaskFunc.WithAsyncRetry(Retries.Infinite, ResultPolicy.Default<int>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero);
            Assert.ThrowsExceptionAsync<InvalidOperationException>(() => nullTaskReliableFunc(42, "foo", 3.14D, CancellationToken.None)).Wait();

#nullable enable

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy(async (arg1, arg2, arg3, token) => await Task.FromResult(f(token)));
            Func<TimeSpan, ComplexDelayHandlerProxy> delayHandlerFactory = t => new ComplexDelayHandlerProxy((i, r, e) => t);
            Func<InterruptableTestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, int, Exception?, TimeSpan>, InterruptableTestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, r, e, d.Invoke);
            Func<InterruptableTestFunc, int, string, double, CancellationToken, int> invoke = (f, arg1, arg2, arg3, token) => f(arg1, arg2, arg3, token).Result;

            Action<InterruptableTestFuncProxy>?           observeFunc      = f          => f.Invoking += Expect.Arguments<int, string, double, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan>? observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, string, double, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT4_Success        (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultHandler: true);
            WithRetryT4_EventualSuccess(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);

            // Failure (Result)
            WithRetryT4_Failure_Result         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>());
            WithRetryT4_EventualFailure_Result (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));
            WithRetryT4_RetriesExhausted_Result(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));

            // Failure (Exception)
            WithRetryT4_Failure_Exception         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>());
            WithRetryT4_EventualFailure_Exception (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);
            WithRetryT4_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);

            // Cancel
            WithRetryT4_Canceled      (funcFactory, delayHandlerFactory, withAsyncRetry, invoke);
            WithRetryT4_Canceled_Func (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);
            WithRetryT4_Canceled_Delay(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);

            // Cancel (Synchronous)
            funcFactory = f => new InterruptableTestFuncProxy((arg1, arg2, arg3, token) => Task.FromResult(f(token)));
            WithRetryT4_Canceled_Func (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);
        }
    }
}
