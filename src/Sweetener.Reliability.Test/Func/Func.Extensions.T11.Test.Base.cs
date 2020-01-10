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
        #region WithRetryT11_Success

        internal void WithRetryT11_Success<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TFunc, int, ResultPolicy<int>, ExceptionPolicy, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken, int> invoke,
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
                Assert.AreEqual(200, invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), tokenSource.Token));

            // Validate the number of calls
            int x = passResultPolicy ? 1 : 0;
            Assert.AreEqual(1, func           .Calls);
            Assert.AreEqual(x, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT11_Failure_Result

        internal void WithRetryT11_Failure_Result<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TFunc, int, ResultPolicy<int>, ExceptionPolicy, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken, int> invoke,
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
                Assert.AreEqual(500, invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(1, func           .Calls);
            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT11_Failure_Exception

        internal void WithRetryT11_Failure_Exception<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TFunc, int, ResultPolicy<int>, ExceptionPolicy, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken, int> invoke,
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
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(1, func           .Calls);
            Assert.AreEqual(0, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT11_EventualSuccess

        internal void WithRetryT11_EventualSuccess<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TFunc, int, ResultPolicy<int>, ExceptionPolicy, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken, int> invoke,
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
                Assert.AreEqual(200, invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), tokenSource.Token));

            // Validate the number of calls
            int x = passResultPolicy ? 2 : 0;
            int y = passResultPolicy ? 1 : 2;
            Assert.AreEqual(3, func           .Calls);
            Assert.AreEqual(x, resultPolicy   .Calls);
            Assert.AreEqual(y, exceptionPolicy.Calls);
            Assert.AreEqual(2, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT11_EventualFailure_Result

        internal void WithRetryT11_EventualFailure_Result<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TFunc, int, ResultPolicy<int>, ExceptionPolicy, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken, int> invoke,
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
                Assert.AreEqual(500, invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, func           .Calls);
            Assert.AreEqual(2, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(2, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT11_EventualFailure_Exception

        internal void WithRetryT11_EventualFailure_Exception<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TFunc, int, ResultPolicy<int>, ExceptionPolicy, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken, int> invoke,
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
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), tokenSource.Token));

            // Validate the number of calls
            int x = passResultPolicy ? 1 : 0;
            int y = passResultPolicy ? 2 : 3;
            Assert.AreEqual(3, func           .Calls);
            Assert.AreEqual(x, resultPolicy   .Calls);
            Assert.AreEqual(y, exceptionPolicy.Calls);
            Assert.AreEqual(2, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT11_RetriesExhausted_Result

        internal void WithRetryT11_RetriesExhausted_Result<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TFunc, int, ResultPolicy<int>, ExceptionPolicy, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken, int> invoke,
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
                Assert.AreEqual(418, invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(4, func           .Calls);
            Assert.AreEqual(2, resultPolicy   .Calls);
            Assert.AreEqual(2, exceptionPolicy.Calls);
            Assert.AreEqual(3, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT11_RetriesExhausted_Exception

        internal void WithRetryT11_RetriesExhausted_Exception<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TFunc, int, ResultPolicy<int>, ExceptionPolicy, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken, int> invoke,
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
                Assert.That.ThrowsException<IOException>(() => invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), tokenSource.Token));

            // Validate the number of calls
            int x = passResultPolicy ? 1 : 0;
            int y = passResultPolicy ? 2 : 3;
            Assert.AreEqual(3, func           .Calls);
            Assert.AreEqual(x, resultPolicy   .Calls);
            Assert.AreEqual(y, exceptionPolicy.Calls);
            Assert.AreEqual(2, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT11_Canceled_Func

        internal void WithRetryT11_Canceled_Func<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TFunc, int, ResultPolicy<int>, ExceptionPolicy, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken, int> invoke,
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
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), tokenSource.Token), allowedDerivedTypes: true);

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

        #region WithRetryT11_Canceled_Delay

        internal void WithRetryT11_Canceled_Delay<TFunc, TDelayPolicy, TFuncProxy, TDelayPolicyProxy>(
            Func<Func<CancellationToken, int>, TFuncProxy> funcFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TFunc, int, ResultPolicy<int>, ExceptionPolicy, TDelayPolicy, TFunc> withRetry,
            Func<TFunc, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken, int> invoke,
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
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), tokenSource.Token), allowedDerivedTypes: true);

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
