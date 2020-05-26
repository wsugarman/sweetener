// Generated from AsyncFunc.Extensions.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    // Define type aliases for the various generic types used below as they can become pretty cumbersome
    using TestFunc                   = Func     <int, Task<int>>;
    using InterruptableTestFunc      = Func     <int, CancellationToken, Task<int>>;
    using TestFuncProxy              = FuncProxy<int, Task<int>>;
    using InterruptableTestFuncProxy = FuncProxy<int, CancellationToken, Task<int>>;
    using DelayHandlerProxy          = FuncProxy<int, TimeSpan>;
    using ComplexDelayHandlerProxy   = FuncProxy<int, int, Exception?, TimeSpan>;

    partial class AsyncFuncExtensionsTest : FuncExtensionsTestBase
    {
        [TestMethod]
        public void WithAsyncRetryT2_Async_DelayHandler()
        {
            TestFunc? nullFunc = null;
            TestFunc  func     = async (arg) => await Task.FromResult(12345);

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, ExceptionPolicy.Transient, (DelayHandler)null));

            // Test a function that returns a null Task
            TestFunc nullTaskFunc         = (arg) => null;
            TestFunc nullTaskReliableFunc = nullTaskFunc.WithAsyncRetry(Retries.Infinite, ExceptionPolicy.Transient, DelayPolicy.None);
            Assert.ThrowsExceptionAsync<InvalidOperationException>(() => nullTaskReliableFunc(42)).Wait();

#nullable enable

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy(async (arg) => await Task.FromResult(f(CancellationToken.None)));
            Func<TimeSpan, DelayHandlerProxy> delayHandlerFactory = t => new DelayHandlerProxy((i) => t);
            Func<TestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, TimeSpan>, TestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, e, d.Invoke);
            Func<TestFunc, int, CancellationToken, int> invoke = (f, arg, token) => f(arg).Result;

            Action<TestFuncProxy>?           observeFunc      = f          => f.Invoking += Expect.Arguments<int>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan>? observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int>(Arguments.Validate, delay);

            // Success
            WithRetryT2_Success        (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultHandler: false);
            WithRetryT2_EventualSuccess(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);

            // Failure (Exception)
            WithRetryT2_Failure_Exception         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>());
            WithRetryT2_EventualFailure_Exception (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);
            WithRetryT2_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);
        }

        [TestMethod]
        public void WithAsyncRetryT2_Async_ComplexDelayHandler()
        {
            TestFunc? nullFunc = null;
            TestFunc  func     = async (arg) => await Task.FromResult(12345);

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                     , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, ExceptionPolicy.Transient, (ComplexDelayHandler<int>)null));

            // Test a function that returns a null Task
            TestFunc nullTaskFunc         = (arg) => null;
            TestFunc nullTaskReliableFunc = nullTaskFunc.WithAsyncRetry(Retries.Infinite, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero);
            Assert.ThrowsExceptionAsync<InvalidOperationException>(() => nullTaskReliableFunc(42)).Wait();

#nullable enable

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy(async (arg) => await Task.FromResult(f(CancellationToken.None)));
            Func<TimeSpan, ComplexDelayHandlerProxy> delayHandlerFactory = t => new ComplexDelayHandlerProxy((i, r, e) => t);
            Func<TestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, int, Exception?, TimeSpan>, TestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, e, d.Invoke);
            Func<TestFunc, int, CancellationToken, int> invoke = (f, arg, token) => f(arg).Result;

            Action<TestFuncProxy>?           observeFunc      = f          => f.Invoking += Expect.Arguments<int>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan>? observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int>(Arguments.Validate, delay);

            // Success
            WithRetryT2_Success        (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultHandler: false);
            WithRetryT2_EventualSuccess(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);

            // Failure (Exception)
            WithRetryT2_Failure_Exception         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>());
            WithRetryT2_EventualFailure_Exception (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);
            WithRetryT2_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);
        }

        [TestMethod]
        public void WithAsyncRetryT2_Async_ResultPolicy_DelayHandler()
        {
            TestFunc? nullFunc = null;
            TestFunc  func     = async (arg) => await Task.FromResult(12345);

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, r => ResultKind.Successful, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                      , ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, (DelayHandler)null));

            // Test a function that returns a null Task
            TestFunc nullTaskFunc         = (arg) => null;
            TestFunc nullTaskReliableFunc = nullTaskFunc.WithAsyncRetry(Retries.Infinite, ResultPolicy.Default<int>(), ExceptionPolicy.Transient, DelayPolicy.None);
            Assert.ThrowsExceptionAsync<InvalidOperationException>(() => nullTaskReliableFunc(42)).Wait();

#nullable enable

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy(async (arg) => await Task.FromResult(f(CancellationToken.None)));
            Func<TimeSpan, DelayHandlerProxy> delayHandlerFactory = t => new DelayHandlerProxy((i) => t);
            Func<TestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, TimeSpan>, TestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, r, e, d.Invoke);
            Func<TestFunc, int, CancellationToken, int> invoke = (f, arg, token) => f(arg).Result;

            Action<TestFuncProxy>?           observeFunc      = f          => f.Invoking += Expect.Arguments<int>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan>? observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int>(Arguments.Validate, delay);

            // Success
            WithRetryT2_Success        (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultHandler: true);
            WithRetryT2_EventualSuccess(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);

            // Failure (Result)
            WithRetryT2_Failure_Result         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>());
            WithRetryT2_EventualFailure_Result (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());
            WithRetryT2_RetriesExhausted_Result(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            WithRetryT2_Failure_Exception         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>());
            WithRetryT2_EventualFailure_Exception (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);
            WithRetryT2_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);
        }

        [TestMethod]
        public void WithAsyncRetryT2_Async_ResultPolicy_ComplexDelayHandler()
        {
            TestFunc? nullFunc = null;
            TestFunc  func     = async (arg) => await Task.FromResult(12345);

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, r => ResultKind.Successful, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                      , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, null                     , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, (ComplexDelayHandler<int>)null));

            // Test a function that returns a null Task
            TestFunc nullTaskFunc         = (arg) => null;
            TestFunc nullTaskReliableFunc = nullTaskFunc.WithAsyncRetry(Retries.Infinite, ResultPolicy.Default<int>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero);
            Assert.ThrowsExceptionAsync<InvalidOperationException>(() => nullTaskReliableFunc(42)).Wait();

#nullable enable

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, TestFuncProxy> funcFactory = f => new TestFuncProxy(async (arg) => await Task.FromResult(f(CancellationToken.None)));
            Func<TimeSpan, ComplexDelayHandlerProxy> delayHandlerFactory = t => new ComplexDelayHandlerProxy((i, r, e) => t);
            Func<TestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, int, Exception?, TimeSpan>, TestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, r, e, d.Invoke);
            Func<TestFunc, int, CancellationToken, int> invoke = (f, arg, token) => f(arg).Result;

            Action<TestFuncProxy>?           observeFunc      = f          => f.Invoking += Expect.Arguments<int>(Arguments.Validate);
            Action<TestFuncProxy, TimeSpan>? observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int>(Arguments.Validate, delay);

            // Success
            WithRetryT2_Success        (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultHandler: true);
            WithRetryT2_EventualSuccess(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);

            // Failure (Result)
            WithRetryT2_Failure_Result         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>());
            WithRetryT2_EventualFailure_Result (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));
            WithRetryT2_RetriesExhausted_Result(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));

            // Failure (Exception)
            WithRetryT2_Failure_Exception         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>());
            WithRetryT2_EventualFailure_Exception (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);
            WithRetryT2_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);
        }

        [TestMethod]
        public void WithAsyncRetryT2_Async_WithToken_DelayHandler()
        {
            InterruptableTestFunc? nullFunc = null;
            InterruptableTestFunc  func     = async (arg, token) => await Task.FromResult(12345);

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, ExceptionPolicy.Transient, (DelayHandler)null));

            // Test a function that returns a null Task
            InterruptableTestFunc nullTaskFunc         = (arg, token) => null;
            InterruptableTestFunc nullTaskReliableFunc = nullTaskFunc.WithAsyncRetry(Retries.Infinite, ExceptionPolicy.Transient, DelayPolicy.None);
            Assert.ThrowsExceptionAsync<InvalidOperationException>(() => nullTaskReliableFunc(42, CancellationToken.None)).Wait();

#nullable enable

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy(async (arg, token) => await Task.FromResult(f(token)));
            Func<TimeSpan, DelayHandlerProxy> delayHandlerFactory = t => new DelayHandlerProxy((i) => t);
            Func<InterruptableTestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, TimeSpan>, InterruptableTestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, e, d.Invoke);
            Func<InterruptableTestFunc, int, CancellationToken, int> invoke = (f, arg, token) => f(arg, token).Result;

            Action<InterruptableTestFuncProxy>?           observeFunc      = f          => f.Invoking += Expect.Arguments<int, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan>? observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT2_Success        (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultHandler: false);
            WithRetryT2_EventualSuccess(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);

            // Failure (Exception)
            WithRetryT2_Failure_Exception         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>());
            WithRetryT2_EventualFailure_Exception (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);
            WithRetryT2_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);

            // Cancel
            WithRetryT2_Canceled      (funcFactory, delayHandlerFactory, withAsyncRetry, invoke);
            WithRetryT2_Canceled_Func (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);
            WithRetryT2_Canceled_Delay(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);

            // Cancel (Synchronous)
            funcFactory = f => new InterruptableTestFuncProxy((arg, token) => Task.FromResult(f(token)));
            WithRetryT2_Canceled_Func (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: false);
        }

        [TestMethod]
        public void WithAsyncRetryT2_Async_WithToken_ComplexDelayHandler()
        {
            InterruptableTestFunc? nullFunc = null;
            InterruptableTestFunc  func     = async (arg, token) => await Task.FromResult(12345);

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                     , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, ExceptionPolicy.Transient, (ComplexDelayHandler<int>)null));

            // Test a function that returns a null Task
            InterruptableTestFunc nullTaskFunc         = (arg, token) => null;
            InterruptableTestFunc nullTaskReliableFunc = nullTaskFunc.WithAsyncRetry(Retries.Infinite, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero);
            Assert.ThrowsExceptionAsync<InvalidOperationException>(() => nullTaskReliableFunc(42, CancellationToken.None)).Wait();

#nullable enable

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy(async (arg, token) => await Task.FromResult(f(token)));
            Func<TimeSpan, ComplexDelayHandlerProxy> delayHandlerFactory = t => new ComplexDelayHandlerProxy((i, r, e) => t);
            Func<InterruptableTestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, int, Exception?, TimeSpan>, InterruptableTestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, e, d.Invoke);
            Func<InterruptableTestFunc, int, CancellationToken, int> invoke = (f, arg, token) => f(arg, token).Result;

            Action<InterruptableTestFuncProxy>?           observeFunc      = f          => f.Invoking += Expect.Arguments<int, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan>? observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT2_Success        (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultHandler: false);
            WithRetryT2_EventualSuccess(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);

            // Failure (Exception)
            WithRetryT2_Failure_Exception         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>());
            WithRetryT2_EventualFailure_Exception (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);
            WithRetryT2_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);

            // Cancel
            WithRetryT2_Canceled      (funcFactory, delayHandlerFactory, withAsyncRetry, invoke);
            WithRetryT2_Canceled_Func (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);
            WithRetryT2_Canceled_Delay(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);

            // Cancel (Synchronous)
            funcFactory = f => new InterruptableTestFuncProxy((arg, token) => Task.FromResult(f(token)));
            WithRetryT2_Canceled_Func (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.OnlyExceptionAsc<int>(e), passResultHandler: false);
        }

        [TestMethod]
        public void WithAsyncRetryT2_Async_WithToken_ResultPolicy_DelayHandler()
        {
            InterruptableTestFunc? nullFunc = null;
            InterruptableTestFunc  func     = async (arg, token) => await Task.FromResult(12345);

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, r => ResultKind.Successful, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                      , ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, (DelayHandler)null));

            // Test a function that returns a null Task
            InterruptableTestFunc nullTaskFunc         = (arg, token) => null;
            InterruptableTestFunc nullTaskReliableFunc = nullTaskFunc.WithAsyncRetry(Retries.Infinite, ResultPolicy.Default<int>(), ExceptionPolicy.Transient, DelayPolicy.None);
            Assert.ThrowsExceptionAsync<InvalidOperationException>(() => nullTaskReliableFunc(42, CancellationToken.None)).Wait();

#nullable enable

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy(async (arg, token) => await Task.FromResult(f(token)));
            Func<TimeSpan, DelayHandlerProxy> delayHandlerFactory = t => new DelayHandlerProxy((i) => t);
            Func<InterruptableTestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, TimeSpan>, InterruptableTestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, r, e, d.Invoke);
            Func<InterruptableTestFunc, int, CancellationToken, int> invoke = (f, arg, token) => f(arg, token).Result;

            Action<InterruptableTestFuncProxy>?           observeFunc      = f          => f.Invoking += Expect.Arguments<int, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan>? observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT2_Success        (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>(), passResultHandler: true);
            WithRetryT2_EventualSuccess(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);

            // Failure (Result)
            WithRetryT2_Failure_Result         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>());
            WithRetryT2_EventualFailure_Result (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());
            WithRetryT2_RetriesExhausted_Result(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            WithRetryT2_Failure_Exception         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int>());
            WithRetryT2_EventualFailure_Exception (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);
            WithRetryT2_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);

            // Cancel
            WithRetryT2_Canceled      (funcFactory, delayHandlerFactory, withAsyncRetry, invoke);
            WithRetryT2_Canceled_Func (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);
            WithRetryT2_Canceled_Delay(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);

            // Cancel (Synchronous)
            funcFactory = f => new InterruptableTestFuncProxy((arg, token) => Task.FromResult(f(token)));
            WithRetryT2_Canceled_Func (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.Asc(), passResultHandler: true);
        }

        [TestMethod]
        public void WithAsyncRetryT2_Async_WithToken_ResultPolicy_ComplexDelayHandler()
        {
            InterruptableTestFunc? nullFunc = null;
            InterruptableTestFunc  func     = async (arg, token) => await Task.FromResult(12345);

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithAsyncRetry(-2, r => ResultKind.Successful, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, null                      , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, null                     , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithAsyncRetry( 4, r => ResultKind.Successful, ExceptionPolicy.Transient, (ComplexDelayHandler<int>)null));

            // Test a function that returns a null Task
            InterruptableTestFunc nullTaskFunc         = (arg, token) => null;
            InterruptableTestFunc nullTaskReliableFunc = nullTaskFunc.WithAsyncRetry(Retries.Infinite, ResultPolicy.Default<int>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero);
            Assert.ThrowsExceptionAsync<InvalidOperationException>(() => nullTaskReliableFunc(42, CancellationToken.None)).Wait();

#nullable enable

            // Create the delegates necessary to test the WithRetry overload
            Func<Func<CancellationToken, int>, InterruptableTestFuncProxy> funcFactory = f => new InterruptableTestFuncProxy(async (arg, token) => await Task.FromResult(f(token)));
            Func<TimeSpan, ComplexDelayHandlerProxy> delayHandlerFactory = t => new ComplexDelayHandlerProxy((i, r, e) => t);
            Func<InterruptableTestFunc, int, ResultHandler<int>, ExceptionHandler, Func<int, int, Exception?, TimeSpan>, InterruptableTestFunc> withAsyncRetry = (f, m, r, e, d) => f.WithAsyncRetry(m, r, e, d.Invoke);
            Func<InterruptableTestFunc, int, CancellationToken, int> invoke = (f, arg, token) => f(arg, token).Result;

            Action<InterruptableTestFuncProxy>?           observeFunc      = f          => f.Invoking += Expect.Arguments<int, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestFuncProxy, TimeSpan>? observeFuncDelay = (f, delay) => f.Invoking += Expect.ArgumentsAfterDelay<int, CancellationToken>(Arguments.Validate, delay);

            // Success
            WithRetryT2_Success        (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>(), passResultHandler: true);
            WithRetryT2_EventualSuccess(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);

            // Failure (Result)
            WithRetryT2_Failure_Result         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>());
            WithRetryT2_EventualFailure_Result (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));
            WithRetryT2_RetriesExhausted_Result(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e));

            // Failure (Exception)
            WithRetryT2_Failure_Exception         (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFunc     ,  d        => d.Invoking += Expect.Nothing<int, int, Exception>());
            WithRetryT2_EventualFailure_Exception (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);
            WithRetryT2_RetriesExhausted_Exception(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);

            // Cancel
            WithRetryT2_Canceled      (funcFactory, delayHandlerFactory, withAsyncRetry, invoke);
            WithRetryT2_Canceled_Func (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);
            WithRetryT2_Canceled_Delay(funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);

            // Cancel (Synchronous)
            funcFactory = f => new InterruptableTestFuncProxy((arg, token) => Task.FromResult(f(token)));
            WithRetryT2_Canceled_Func (funcFactory, delayHandlerFactory, withAsyncRetry, invoke, observeFuncDelay, (d, r, e) => d.Invoking += Expect.AlternatingAsc(r, e), passResultHandler: true);
        }
    }
}
