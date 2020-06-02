// Generated from Func.Extensions.Test.Base.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    partial class FuncExtensionsTestBase
    {
        #region WithRetryT9_Success

        internal void WithRetryT9_Success<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayHandlerFactory,
            Func<TFunc, int, ResultHandler<int>, ExceptionHandler, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, int> invoke,
            Action<TFuncProxy>? observeFunc = null,
            Action<TDelayPolicyProxy>? observeDelayPolicy = null,
            bool passResultHandler = true)
            where TFunc             : Delegate
            where TDelayPolicy      : Delegate
            where TFuncProxy        : DelegateProxy<TFunc>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create a "successful" user-defined function
            TFuncProxy func = funcFactory(t => 200);

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<int, ResultKind> resultHandler    = new FuncProxy<int, ResultKind>(n => n == 200 ? ResultKind.Successful : ResultKind.Fatal);
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Fatal.Invoke);
            TDelayPolicyProxy delayHandler = delayHandlerFactory(TimeSpan.Zero);

            // Create the reliable function
            TFunc reliableFunc = withRetry(
                func.Proxy,
                Retries.Infinite,
                resultHandler   .Invoke,
                exceptionHandler.Invoke,
                delayHandler.Proxy);

            // Define expectations
            resultHandler   .Invoking += Expect.Result(200);
            exceptionHandler.Invoking += Expect.Nothing<Exception>();

            observeFunc       ?.Invoke(func);
            observeDelayPolicy?.Invoke(delayHandler);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.AreEqual(200, invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, tokenSource.Token));

            // Validate the number of calls
            int x = passResultHandler ? 1 : 0;
            Assert.AreEqual(1, func            .Calls);
            Assert.AreEqual(x, resultHandler   .Calls);
            Assert.AreEqual(0, exceptionHandler.Calls);
            Assert.AreEqual(0, delayHandler    .Calls);
        }

        #endregion

        #region WithRetryT9_Failure_Result

        internal void WithRetryT9_Failure_Result<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayHandlerFactory,
            Func<TFunc, int, ResultHandler<int>, ExceptionHandler, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, int> invoke,
            Action<TFuncProxy>? observeFunc = null,
            Action<TDelayPolicyProxy>? observeDelayPolicy = null)
            where TFunc             : Delegate
            where TDelayPolicy      : Delegate
            where TFuncProxy        : DelegateProxy<TFunc>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create an "unsuccessful" user-defined func
            TFuncProxy func = funcFactory(t => 500);

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<int, ResultKind> resultHandler    = new FuncProxy<int, ResultKind>(n => n == 200 ? ResultKind.Successful : ResultKind.Fatal);
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Fatal.Invoke);
            TDelayPolicyProxy delayHandler = delayHandlerFactory(TimeSpan.Zero);

            // Create the reliable function
            TFunc reliableFunc = withRetry(
                func.Proxy,
                Retries.Infinite,
                resultHandler   .Invoke,
                exceptionHandler.Invoke,
                delayHandler.Proxy);

            // Define expectations
            resultHandler   .Invoking += Expect.Result(500);
            exceptionHandler.Invoking += Expect.Nothing<Exception>();

            observeFunc       ?.Invoke(func);
            observeDelayPolicy?.Invoke(delayHandler);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.AreEqual(500, invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(1, func            .Calls);
            Assert.AreEqual(1, resultHandler   .Calls);
            Assert.AreEqual(0, exceptionHandler.Calls);
            Assert.AreEqual(0, delayHandler    .Calls);
        }

        #endregion

        #region WithRetryT9_Failure_Exception

        internal void WithRetryT9_Failure_Exception<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayHandlerFactory,
            Func<TFunc, int, ResultHandler<int>, ExceptionHandler, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, int> invoke,
            Action<TFuncProxy>? observeFunc = null,
            Action<TDelayPolicyProxy>? observeDelayPolicy = null)
            where TFunc             : Delegate
            where TDelayPolicy      : Delegate
            where TFuncProxy        : DelegateProxy<TFunc>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create an "unsuccessful" user-defined func
            TFuncProxy func = funcFactory(t => throw new OutOfMemoryException());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<int, ResultKind> resultHandler    = new FuncProxy<int, ResultKind>(r => ResultKind.Successful);
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Fail<OutOfMemoryException>().Invoke);
            TDelayPolicyProxy delayHandler = delayHandlerFactory(TimeSpan.Zero);

            // Create the reliable function
            TFunc reliableFunc = withRetry(
                func.Proxy,
                Retries.Infinite,
                resultHandler   .Invoke,
                exceptionHandler.Invoke,
                delayHandler.Proxy);

            // Define expectations
            resultHandler   .Invoking += Expect.Nothing<int>();
            exceptionHandler.Invoking += Expect.Exception(typeof(OutOfMemoryException));

            observeFunc       ?.Invoke(func);
            observeDelayPolicy?.Invoke(delayHandler);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<OutOfMemoryException>(() => invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(1, func            .Calls);
            Assert.AreEqual(0, resultHandler   .Calls);
            Assert.AreEqual(1, exceptionHandler.Calls);
            Assert.AreEqual(0, delayHandler    .Calls);
        }

        #endregion

        #region WithRetryT9_EventualSuccess

        internal void WithRetryT9_EventualSuccess<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayHandlerFactory,
            Func<TFunc, int, ResultHandler<int>, ExceptionHandler, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, int> invoke,
            Action<TFuncProxy, TimeSpan>? observeFunc = null,
            Action<TDelayPolicyProxy, int, Type>? observeDelayPolicy = null,
            bool passResultHandler = true)
            where TFunc             : Delegate
            where TDelayPolicy      : Delegate
            where TFuncProxy        : DelegateProxy<TFunc>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create a "successful" user-defined action that completes after either
            // (1) 2 IOExceptions OR
            // (2) an IOException and a transient 418 result
            Func<int> flakyFunc = passResultHandler
                ? FlakyFunc.Create<int, IOException>(418, 200, 2)
                : FlakyFunc.Create<int, IOException>(     200, 2);
            TFuncProxy func = funcFactory(t => flakyFunc());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<int, ResultKind> resultHandler = new FuncProxy<int, ResultKind>(r =>
                r switch
                {
                    418 => ResultKind.Transient,
                    200 => ResultKind.Successful,
                    _   => ResultKind.Fatal,
                });
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            TDelayPolicyProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Create the reliable function
            TFunc reliableFunc = withRetry(
                func.Proxy,
                Retries.Infinite,
                resultHandler   .Invoke,
                exceptionHandler.Invoke,
                delayHandler.Proxy);

            // Define expectations
            resultHandler   .Invoking += Expect.Results(418, 200, 1);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));

            observeFunc       ?.Invoke(func, Constants.MinDelay);
            observeDelayPolicy?.Invoke(delayHandler, 418, typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.AreEqual(200, invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, tokenSource.Token));

            // Validate the number of calls
            int x = passResultHandler ? 2 : 0;
            int y = passResultHandler ? 1 : 2;
            Assert.AreEqual(3, func            .Calls);
            Assert.AreEqual(x, resultHandler   .Calls);
            Assert.AreEqual(y, exceptionHandler.Calls);
            Assert.AreEqual(2, delayHandler    .Calls);
        }

        #endregion

        #region WithRetryT9_EventualFailure_Result

        internal void WithRetryT9_EventualFailure_Result<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayHandlerFactory,
            Func<TFunc, int, ResultHandler<int>, ExceptionHandler, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, int> invoke,
            Action<TFuncProxy, TimeSpan>? observeFunc = null,
            Action<TDelayPolicyProxy, int, Type>? observeDelayPolicy = null)
            where TFunc             : Delegate
            where TDelayPolicy      : Delegate
            where TFuncProxy        : DelegateProxy<TFunc>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create an "unsuccessful" user-defined action that completes after a transient result and exception
            Func<int> flakyFunc = FlakyFunc.Create<int, IOException>(418, 500, 2);
            TFuncProxy func = funcFactory(t => flakyFunc());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<int, ResultKind> resultHandler = new FuncProxy<int, ResultKind>(r =>
                r switch
                {
                    418 => ResultKind.Transient,
                    500 => ResultKind.Fatal,
                    _   => ResultKind.Successful,
                });
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            TDelayPolicyProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Create the reliable function
            TFunc reliableFunc = withRetry(
                func.Proxy,
                Retries.Infinite,
                resultHandler   .Invoke,
                exceptionHandler.Invoke,
                delayHandler.Proxy);

            // Define expectations
            resultHandler   .Invoking += Expect.Results(418, 500, 1);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));

            observeFunc       ?.Invoke(func, Constants.MinDelay);
            observeDelayPolicy?.Invoke(delayHandler, 418, typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.AreEqual(500, invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, func            .Calls);
            Assert.AreEqual(2, resultHandler   .Calls);
            Assert.AreEqual(1, exceptionHandler.Calls);
            Assert.AreEqual(2, delayHandler    .Calls);
        }

        #endregion

        #region WithRetryT9_EventualFailure_Exception

        internal void WithRetryT9_EventualFailure_Exception<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayHandlerFactory,
            Func<TFunc, int, ResultHandler<int>, ExceptionHandler, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, int> invoke,
            Action<TFuncProxy, TimeSpan>? observeFunc = null,
            Action<TDelayPolicyProxy, int, Type>? observeDelayPolicy = null,
            bool passResultHandler = true)
            where TFunc             : Delegate
            where TDelayPolicy      : Delegate
            where TFuncProxy        : DelegateProxy<TFunc>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create an "unsuccessful" user-defined action that completes after either
            // (1) 2 IOExceptions OR
            // (2) an IOException and a transient 418 result
            Func<int> flakyFunc = passResultHandler
                ? FlakyFunc.Create<int, IOException, OutOfMemoryException>(418, 2)
                : FlakyFunc.Create<int, IOException, OutOfMemoryException>(     2);
            TFuncProxy func = funcFactory(t => flakyFunc());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<int, ResultKind> resultHandler    = new FuncProxy<int, ResultKind>(r => r == 418 ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            TDelayPolicyProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Create the reliable function
            TFunc reliableFunc = withRetry(
                func.Proxy,
                Retries.Infinite,
                resultHandler   .Invoke,
                exceptionHandler.Invoke,
                delayHandler.Proxy);

            // Define expectations
            resultHandler   .Invoking += Expect.Result(418);
            exceptionHandler.Invoking += Expect.Exceptions(typeof(IOException), typeof(OutOfMemoryException), passResultHandler ? 1 : 2);

            observeFunc       ?.Invoke(func, Constants.MinDelay);
            observeDelayPolicy?.Invoke(delayHandler, 418, typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<OutOfMemoryException>(() => invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, tokenSource.Token));

            // Validate the number of calls
            int x = passResultHandler ? 1 : 0;
            int y = passResultHandler ? 2 : 3;
            Assert.AreEqual(3, func            .Calls);
            Assert.AreEqual(x, resultHandler   .Calls);
            Assert.AreEqual(y, exceptionHandler.Calls);
            Assert.AreEqual(2, delayHandler    .Calls);
        }

        #endregion

        #region WithRetryT9_RetriesExhausted_Result

        internal void WithRetryT9_RetriesExhausted_Result<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayHandlerFactory,
            Func<TFunc, int, ResultHandler<int>, ExceptionHandler, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, int> invoke,
            Action<TFuncProxy, TimeSpan>? observeFunc = null,
            Action<TDelayPolicyProxy, int, Type>? observeDelayPolicy = null)
            where TFunc             : Delegate
            where TDelayPolicy      : Delegate
            where TFuncProxy        : DelegateProxy<TFunc>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create an "unsuccessful" user-defined action that eventually exhausts all of its retries
            Func<int> flakyFunc = FlakyFunc.Create<int, IOException>(418);
            TFuncProxy func = funcFactory(t => flakyFunc());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<int, ResultKind> resultHandler = new FuncProxy<int, ResultKind>(r => r == 418 ? ResultKind.Transient : ResultKind.Fatal);
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            TDelayPolicyProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Create the reliable function
            TFunc reliableFunc = withRetry(
                func.Proxy,
                3,
                resultHandler   .Invoke,
                exceptionHandler.Invoke,
                delayHandler.Proxy);

            // Define expectations
            resultHandler   .Invoking += Expect.Result(418);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));

            observeFunc       ?.Invoke(func, Constants.MinDelay);
            observeDelayPolicy?.Invoke(delayHandler, 418, typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.AreEqual(418, invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(4, func            .Calls);
            Assert.AreEqual(2, resultHandler   .Calls);
            Assert.AreEqual(2, exceptionHandler.Calls);
            Assert.AreEqual(3, delayHandler    .Calls);
        }

        #endregion

        #region WithRetryT9_RetriesExhausted_Exception

        internal void WithRetryT9_RetriesExhausted_Exception<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayHandlerFactory,
            Func<TFunc, int, ResultHandler<int>, ExceptionHandler, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, int> invoke,
            Action<TFuncProxy, TimeSpan>? observeFunc = null,
            Action<TDelayPolicyProxy, int, Type>? observeDelayPolicy = null,
            bool passResultHandler = true)
            where TFunc             : Delegate
            where TDelayPolicy      : Delegate
            where TFuncProxy        : DelegateProxy<TFunc>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create an "unsuccessful" user-defined action that eventually exhausts all of its retries
            Func<int> flakyFunc = passResultHandler
                ? FlakyFunc.Create<int, IOException>(418)
                : () => throw new IOException();
            TFuncProxy func = funcFactory(t => flakyFunc());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<int, ResultKind> resultHandler    = new FuncProxy<int, ResultKind>(r => r == 418 ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            TDelayPolicyProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Create the reliable function
            TFunc reliableFunc = withRetry(
                func.Proxy,
                2,
                resultHandler   .Invoke,
                exceptionHandler.Invoke,
                delayHandler.Proxy);

            // Define expectations
            resultHandler   .Invoking += Expect.Result(418);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));

            observeFunc       ?.Invoke(func, Constants.MinDelay);
            observeDelayPolicy?.Invoke(delayHandler, 418, typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<IOException>(() => invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, tokenSource.Token));

            // Validate the number of calls
            int x = passResultHandler ? 1 : 0;
            int y = passResultHandler ? 2 : 3;
            Assert.AreEqual(3, func            .Calls);
            Assert.AreEqual(x, resultHandler   .Calls);
            Assert.AreEqual(y, exceptionHandler.Calls);
            Assert.AreEqual(2, delayHandler    .Calls);
        }

        #endregion

        #region WithRetryT9_Canceled

        internal void WithRetryT9_Canceled<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayHandlerFactory,
            Func<TFunc, int, ResultHandler<int>, ExceptionHandler, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, int> invoke)
            where TFunc             : Delegate
            where TDelayPolicy      : Delegate
            where TFuncProxy        : DelegateProxy<TFunc>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            tokenSource.Cancel();

            // Create an unused user-defined function
            TFuncProxy func = funcFactory(t => throw new InvalidOperationException());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<int, ResultKind> resultHandler    = new FuncProxy<int, ResultKind>(ResultPolicy.Default<int>().Invoke);
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Transient.Invoke);
            TDelayPolicyProxy delayHandler = delayHandlerFactory(TimeSpan.Zero);

            // Create the reliable function
            TFunc reliableFunc = withRetry(
                func.Proxy,
                Retries.Infinite,
                resultHandler   .Invoke,
                exceptionHandler.Invoke,
                delayHandler.Proxy);

            // Begin the invocation
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            Assert.AreEqual(0, func            .Calls);
            Assert.AreEqual(0, resultHandler   .Calls);
            Assert.AreEqual(0, exceptionHandler.Calls);
            Assert.AreEqual(0, delayHandler    .Calls);
        }

        #endregion

        #region WithRetryT9_Canceled_Func

        internal void WithRetryT9_Canceled_Func<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayHandlerFactory,
            Func<TFunc, int, ResultHandler<int>, ExceptionHandler, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, int> invoke,
            Action<TFuncProxy, TimeSpan>? observeFunc = null,
            Action<TDelayPolicyProxy, int, Type>? observeDelayPolicy = null,
            bool passResultHandler = true)
            where TFunc             : Delegate
            where TDelayPolicy      : Delegate
            where TFuncProxy        : DelegateProxy<TFunc>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create a user-defined function that will throw an exception depending on whether it's canceled
            Func<int> flakyFunc = passResultHandler ? FlakyFunc.Create<int, IOException>(418) : () => throw new IOException();
            TFuncProxy func = funcFactory(t =>
            {
                t.ThrowIfCancellationRequested();
                return flakyFunc();
            });

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<int, ResultKind> resultHandler    = new FuncProxy<int, ResultKind>(r => r == 418 ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Transient.Invoke);
            TDelayPolicyProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Create the reliable function
            TFunc reliableFunc = withRetry(
                func.Proxy,
                Retries.Infinite,
                resultHandler   .Invoke,
                exceptionHandler.Invoke,
                delayHandler.Proxy);

            // Define expectations
            resultHandler   .Invoking += Expect.Result(418);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));

            observeFunc       ?.Invoke(func, Constants.MinDelay);
            observeDelayPolicy?.Invoke(delayHandler, 418, typeof(IOException));

            // Cancel the function after 2 or 3 invocations
            int i = passResultHandler ? 3 : 2;

            func.Invoking += c =>
            {
                if (c.Calls == i)
                    tokenSource.Cancel();
            };

            // Begin the invocation
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            int r = i - 1;
            int x = passResultHandler ? r / 2 : 0;
            int y = passResultHandler ? r / 2 : r;
            Assert.AreEqual(i, func            .Calls);
            Assert.AreEqual(x, resultHandler   .Calls);
            Assert.AreEqual(y, exceptionHandler.Calls);
            Assert.AreEqual(r, delayHandler    .Calls);
        }

        #endregion

        #region WithRetryT9_Canceled_Delay

        internal void WithRetryT9_Canceled_Delay<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayHandlerFactory,
            Func<TFunc, int, ResultHandler<int>, ExceptionHandler, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken, int> invoke,
            Action<TFuncProxy, TimeSpan>? observeFunc = null,
            Action<TDelayPolicyProxy, int, Type>? observeDelayPolicy = null,
            bool passResultHandler = true)
            where TFunc             : Delegate
            where TDelayPolicy      : Delegate
            where TFuncProxy        : DelegateProxy<TFunc>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined function that continues to fail with transient exceptions until it's canceled
            Func<int> flakyFunc = passResultHandler ? FlakyFunc.Create<int, IOException>(418) : () => throw new IOException();
            TFuncProxy func = funcFactory(t => flakyFunc());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<int, ResultKind> resultHandler    = new FuncProxy<int, ResultKind>(r => r == 418 ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Transient.Invoke);
            TDelayPolicyProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Create the reliable function
            TFunc reliableFunc = withRetry(
                func.Proxy,
                Retries.Infinite,
                resultHandler   .Invoke,
                exceptionHandler.Invoke,
                delayHandler.Proxy);

            // Define expectations
            resultHandler   .Invoking += Expect.Result(418);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));

            observeFunc       ?.Invoke(func, Constants.MinDelay);
            observeDelayPolicy?.Invoke(delayHandler, 418, typeof(IOException));

            // Cancel the delay after 1 or 2 invocations
            int r = passResultHandler ? 2 : 1;

            delayHandler.Invoking += c =>
            {
                if (c.Calls == r)
                    tokenSource.Cancel();
            };

            // Begin the invocation
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            int x = passResultHandler ? r / 2 : 0;
            int y = passResultHandler ? r / 2 : r;
            Assert.AreEqual(r, func            .Calls);
            Assert.AreEqual(x, resultHandler   .Calls);
            Assert.AreEqual(y, exceptionHandler.Calls);
            Assert.AreEqual(r, delayHandler    .Calls);
            
        }

        #endregion
    }
}
