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
        public void WithRetryT2_DelayPolicy()
        {
            Func<int, int> nullFunc = null;
            Func<int, int> func     = (arg) => 42;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null ));

            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke;
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, DelayPolicy, InterruptableFunc<int, int>> withRetry = (f, m, r, e, d) => f.WithRetry(m, e, d);

            // Without Token
            invoke = (func, arg, token) => func(arg);

            WithRetryT2_Success                   (withRetry, invoke, useResultPolicy: false);
            WithRetryT2_Failure_Exception         (withRetry, invoke); // ResultPolicy is never called
            WithRetryT2_EventualSuccess           (withRetry, invoke, useResultPolicy: false);
            WithRetryT2_EventualFailure_Exception (withRetry, invoke, useResultPolicy: false);
            WithRetryT2_RetriesExhausted_Exception(withRetry, invoke, useResultPolicy: false);

            // With Token
            invoke = (func, arg, token) => func(arg, token);

            WithRetryT2_Success                   (withRetry, invoke, useResultPolicy: false);
            WithRetryT2_Failure_Exception         (withRetry, invoke); // ResultPolicy is never called
            WithRetryT2_EventualSuccess           (withRetry, invoke, useResultPolicy: false);
            WithRetryT2_EventualFailure_Exception (withRetry, invoke, useResultPolicy: false);
            WithRetryT2_RetriesExhausted_Exception(withRetry, invoke, useResultPolicy: false);
            WithRetryT2_Canceled                  (withRetry        , useResultPolicy: false);
        }

        [TestMethod]
        public void WithRetryT2_ComplexDelayPolicy()
        {
            Func<int, int> nullFunc = null;
            Func<int, int> func     = (arg) => 42;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero   ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero   ));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                       , (i, r, e) => TimeSpan.Zero   ));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy<int>)null));

            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke;
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, ComplexDelayPolicy<int>, InterruptableFunc<int, int>> withRetry = (f, m, r, e, d) => f.WithRetry(m, e, d);

            // Without Token
            invoke = (func, arg, token) => func(arg);

            WithRetryT2_Success                   (withRetry, invoke, useResultPolicy: false);
            WithRetryT2_Failure_Exception         (withRetry, invoke); // ResultPolicy is never called
            WithRetryT2_EventualSuccess           (withRetry, invoke, useResultPolicy: false);
            WithRetryT2_EventualFailure_Exception (withRetry, invoke, useResultPolicy: false);
            WithRetryT2_RetriesExhausted_Exception(withRetry, invoke, useResultPolicy: false);

            // With Token
            invoke = (func, arg, token) => func(arg, token);

            WithRetryT2_Success                   (withRetry, invoke, useResultPolicy: false);
            WithRetryT2_Failure_Exception         (withRetry, invoke); // ResultPolicy is never called
            WithRetryT2_EventualSuccess           (withRetry, invoke, useResultPolicy: false);
            WithRetryT2_EventualFailure_Exception (withRetry, invoke, useResultPolicy: false);
            WithRetryT2_RetriesExhausted_Exception(withRetry, invoke, useResultPolicy: false);
            WithRetryT2_Canceled                  (withRetry        , useResultPolicy: false);
        }

        [TestMethod]
        public void WithRetryT2_ResultPolicy_DelayPolicy()
        {
            Func<int, int> nullFunc = null;
            Func<int, int> func     = (arg) => 42;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, r => ResultKind.Successful, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                      , ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (DelayPolicy)null ));

            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke;
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, DelayPolicy, InterruptableFunc<int, int>> withRetry = (f, m, r, e, d) => f.WithRetry(m, r, e, d);

            // Without Token
            invoke = (func, arg, token) => func(arg);

            WithRetryT2_Success                   (withRetry, invoke);
            WithRetryT2_Failure_Result            (withRetry, invoke);
            WithRetryT2_Failure_Exception         (withRetry, invoke);
            WithRetryT2_EventualSuccess           (withRetry, invoke);
            WithRetryT2_EventualFailure_Result    (withRetry, invoke);
            WithRetryT2_EventualFailure_Exception (withRetry, invoke);
            WithRetryT2_RetriesExhausted_Result   (withRetry, invoke);
            WithRetryT2_RetriesExhausted_Exception(withRetry, invoke);

            // With Token
            invoke = (func, arg, token) => func(arg, token);

            WithRetryT2_Success                   (withRetry, invoke);
            WithRetryT2_Failure_Result            (withRetry, invoke);
            WithRetryT2_Failure_Exception         (withRetry, invoke);
            WithRetryT2_EventualSuccess           (withRetry, invoke);
            WithRetryT2_EventualFailure_Result    (withRetry, invoke);
            WithRetryT2_EventualFailure_Exception (withRetry, invoke);
            WithRetryT2_RetriesExhausted_Result   (withRetry, invoke);
            WithRetryT2_RetriesExhausted_Exception(withRetry, invoke);
            WithRetryT2_Canceled                  (withRetry);
        }

        [TestMethod]
        public void WithRetryT2_ResultPolicy_ComplexDelayPolicy()
        {
            Func<int, int> nullFunc = null;
            Func<int, int> func     = (arg) => 42;
            Assert.ThrowsException<ArgumentNullException      >(() => nullFunc.WithRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero   ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => func    .WithRetry(-2, r => ResultKind.Successful, ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero   ));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, null                      , ExceptionPolicies.Transient, (i, r, e) => TimeSpan.Zero   ));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, null                       , (i, r, e) => TimeSpan.Zero   ));
            Assert.ThrowsException<ArgumentNullException      >(() => func    .WithRetry( 4, r => ResultKind.Successful, ExceptionPolicies.Transient, (ComplexDelayPolicy<int>)null));

            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke;
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, ComplexDelayPolicy<int>, InterruptableFunc<int, int>> withRetry = (f, m, r, e, d) => f.WithRetry(m, r, e, d);

            // Without Token
            invoke = (func, arg, token) => func(arg);

            WithRetryT2_Success                   (withRetry, invoke);
            WithRetryT2_Failure_Result            (withRetry, invoke);
            WithRetryT2_Failure_Exception         (withRetry, invoke);
            WithRetryT2_EventualSuccess           (withRetry, invoke);
            WithRetryT2_EventualFailure_Result    (withRetry, invoke);
            WithRetryT2_EventualFailure_Exception (withRetry, invoke);
            WithRetryT2_RetriesExhausted_Result   (withRetry, invoke);
            WithRetryT2_RetriesExhausted_Exception(withRetry, invoke);

            // With Token
            invoke = (func, arg, token) => func(arg, token);

            WithRetryT2_Success                   (withRetry, invoke);
            WithRetryT2_Failure_Result            (withRetry, invoke);
            WithRetryT2_Failure_Exception         (withRetry, invoke);
            WithRetryT2_EventualSuccess           (withRetry, invoke);
            WithRetryT2_EventualFailure_Result    (withRetry, invoke);
            WithRetryT2_EventualFailure_Exception (withRetry, invoke);
            WithRetryT2_RetriesExhausted_Result   (withRetry, invoke);
            WithRetryT2_RetriesExhausted_Exception(withRetry, invoke);
            WithRetryT2_Canceled                  (withRetry);
        }

        #region WithRetryT2_Success

        private void WithRetryT2_Success(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, DelayPolicy, InterruptableFunc<int, int>> withRetry,
            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke,
            bool useResultPolicy = true)
            => WithRetryT2_Success(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                },
                useResultPolicy);

        private void WithRetryT2_Success(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, ComplexDelayPolicy<int>, InterruptableFunc<int, int>> withRetry,
            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke,
            bool useResultPolicy = true)
            => WithRetryT2_Success(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, int, Exception>();
                    return delayPolicy;
                },
                useResultPolicy);

        private void WithRetryT2_Success<T>(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, T, InterruptableFunc<int, int>> withRetry,
            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke,
            Func<T> delayPolicyFactory,
            bool useResultPolicy)
            where T : DelegateProxy
        {
            // Create a "successful" user-defined function
            FuncProxy<int, int> func = new FuncProxy<int, int>((arg) => 200);

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy    = new FuncProxy<int, ResultKind>(n => n == 200 ? ResultKind.Successful : ResultKind.Fatal);
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>();
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableFunc
            InterruptableFunc<int, int> reliableFunc = withRetry(
                func.Invoke,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            func           .Invoking += Expect.Arguments<int>(Arguments.Validate);
            resultPolicy   .Invoking += Expect.Result(200);
            exceptionPolicy.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.AreEqual(200, invoke(reliableFunc, 42, tokenSource.Token));

            // Validate the number of calls
            int x = useResultPolicy ? 1 : 0;
            Assert.AreEqual(1, func           .Calls);
            Assert.AreEqual(x, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT2_Failure_Result

        private void WithRetryT2_Failure_Result(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, DelayPolicy, InterruptableFunc<int, int>> withRetry,
            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke)
            => WithRetryT2_Failure_Result(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                });

        private void WithRetryT2_Failure_Result(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, ComplexDelayPolicy<int>, InterruptableFunc<int, int>> withRetry,
            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke)
            => WithRetryT2_Failure_Result(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, int, Exception>();
                    return delayPolicy;
                });

        private void WithRetryT2_Failure_Result<T>(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, T, InterruptableFunc<int, int>> withRetry,
            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined func
            FuncProxy<int, int> func = new FuncProxy<int, int>((arg) => 500);

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy    = new FuncProxy<int, ResultKind>(n => n == 200 ? ResultKind.Successful : ResultKind.Fatal);
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>();
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableFunc
            InterruptableFunc<int, int> reliableFunc = withRetry(
                func.Invoke,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            func           .Invoking += Expect.Arguments<int>(Arguments.Validate);
            resultPolicy   .Invoking += Expect.Result(500);
            exceptionPolicy.Invoking += Expect.Nothing<Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.AreEqual(500, invoke(reliableFunc, 42, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(1, func           .Calls);
            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT2_Failure_Exception

        private void WithRetryT2_Failure_Exception(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, DelayPolicy, InterruptableFunc<int, int>> withRetry,
            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke)
            => WithRetryT2_Failure_Exception(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int>();
                    return delayPolicy;
                });

        private void WithRetryT2_Failure_Exception(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, ComplexDelayPolicy<int>, InterruptableFunc<int, int>> withRetry,
            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke)
            => WithRetryT2_Failure_Exception(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, int, Exception, TimeSpan>();
                    delayPolicy.Invoking += Expect.Nothing<int, int, Exception>();
                    return delayPolicy;
                });

        private void WithRetryT2_Failure_Exception<T>(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, T, InterruptableFunc<int, int>> withRetry,
            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined func
            FuncProxy<int, int> func = new FuncProxy<int, int>((arg) => throw new InvalidOperationException());

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy    = new FuncProxy<int, ResultKind>();
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>();
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableFunc
            InterruptableFunc<int, int> reliableFunc = withRetry(
                func.Invoke,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            func           .Invoking += Expect.Arguments<int>(Arguments.Validate);
            resultPolicy   .Invoking += Expect.Nothing<int>();
            exceptionPolicy.Invoking += Expect.Exception(typeof(InvalidOperationException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableFunc, 42, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(1, func           .Calls);
            Assert.AreEqual(0, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT2_EventualSuccess

        private void WithRetryT2_EventualSuccess(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, DelayPolicy, InterruptableFunc<int, int>> withRetry,
            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke,
            bool useResultPolicy = true)
            => WithRetryT2_EventualSuccess(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                },
                useResultPolicy);

        private void WithRetryT2_EventualSuccess(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, ComplexDelayPolicy<int>, InterruptableFunc<int, int>> withRetry,
            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke,
            bool useResultPolicy = true)
            => WithRetryT2_EventualSuccess(
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

        private void WithRetryT2_EventualSuccess<T>(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, T, InterruptableFunc<int, int>> withRetry,
            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke,
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
            FuncProxy<int, int> func = new FuncProxy<int, int>((arg) => flakyFunc());

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy = new FuncProxy<int, ResultKind>(r =>
                r switch
                {
                    418 => ResultKind.Transient,
                    200 => ResultKind.Successful,
                    _   => ResultKind.Fatal,
                });
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableFunc
            InterruptableFunc<int, int> reliableFunc = withRetry(
                func.Invoke,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            func           .Invoking += Expect.ArgumentsAfterDelay<int>(Arguments.Validate, Constants.MinDelay);
            resultPolicy   .Invoking += Expect.Results(418, 200, 1);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.AreEqual(200, invoke(reliableFunc, 42, tokenSource.Token));

            // Validate the number of calls
            int x = useResultPolicy ? 2 : 0;
            int y = useResultPolicy ? 1 : 2;
            Assert.AreEqual(3, func           .Calls);
            Assert.AreEqual(x, resultPolicy   .Calls);
            Assert.AreEqual(y, exceptionPolicy.Calls);
            Assert.AreEqual(2, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT2_EventualFailure_Result

        private void WithRetryT2_EventualFailure_Result(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, DelayPolicy, InterruptableFunc<int, int>> withRetry,
            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke)
            => WithRetryT2_EventualFailure_Result(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT2_EventualFailure_Result(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, ComplexDelayPolicy<int>, InterruptableFunc<int, int>> withRetry,
            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke)
            => WithRetryT2_EventualFailure_Result(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, int, Exception, TimeSpan>((i, r, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.AlternatingAsc(418, typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT2_EventualFailure_Result<T>(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, T, InterruptableFunc<int, int>> withRetry,
            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that completes after a transient result and exception
            Func<int> flakyFunc = FlakyFunc.Create<int, IOException>(418, 500, 2);
            FuncProxy<int, int> func = new FuncProxy<int, int>((arg) => flakyFunc());

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy = new FuncProxy<int, ResultKind>(r =>
                r switch
                {
                    418 => ResultKind.Transient,
                    500 => ResultKind.Fatal,
                    _   => ResultKind.Successful,
                });
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableFunc
            InterruptableFunc<int, int> reliableFunc = withRetry(
                func.Invoke,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            func           .Invoking += Expect.ArgumentsAfterDelay<int>(Arguments.Validate, Constants.MinDelay);
            resultPolicy   .Invoking += Expect.Results(418, 500, 1);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.AreEqual(500, invoke(reliableFunc, 42, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, func           .Calls);
            Assert.AreEqual(2, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(2, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT2_EventualFailure_Exception

        private void WithRetryT2_EventualFailure_Exception(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, DelayPolicy, InterruptableFunc<int, int>> withRetry,
            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke,
            bool useResultPolicy = true)
            => WithRetryT2_EventualFailure_Exception(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                },
                useResultPolicy);

        private void WithRetryT2_EventualFailure_Exception(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, ComplexDelayPolicy<int>, InterruptableFunc<int, int>> withRetry,
            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke,
            bool useResultPolicy = true)
            => WithRetryT2_EventualFailure_Exception(
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

        private void WithRetryT2_EventualFailure_Exception<T>(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, T, InterruptableFunc<int, int>> withRetry,
            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke,
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
            FuncProxy<int, int> func = new FuncProxy<int, int>((arg) => flakyFunc());

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy    = new FuncProxy<int, ResultKind>(r => r == 418 ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableFunc
            InterruptableFunc<int, int> reliableFunc = withRetry(
                func.Invoke,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            func           .Invoking += Expect.ArgumentsAfterDelay<int>(Arguments.Validate, Constants.MinDelay);
            resultPolicy   .Invoking += Expect.Result(418);
            exceptionPolicy.Invoking += Expect.Exceptions(typeof(IOException), typeof(InvalidOperationException), useResultPolicy ? 1 : 2);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableFunc, 42, tokenSource.Token));

            // Validate the number of calls
            int x = useResultPolicy ? 1 : 0;
            int y = useResultPolicy ? 2 : 3;
            Assert.AreEqual(3, func           .Calls);
            Assert.AreEqual(x, resultPolicy   .Calls);
            Assert.AreEqual(y, exceptionPolicy.Calls);
            Assert.AreEqual(2, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT2_RetriesExhausted_Result

        private void WithRetryT2_RetriesExhausted_Result(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, DelayPolicy, InterruptableFunc<int, int>> withRetry,
            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke)
            => WithRetryT2_RetriesExhausted_Result(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                });

        private void WithRetryT2_RetriesExhausted_Result(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, ComplexDelayPolicy<int>, InterruptableFunc<int, int>> withRetry,
            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke)
            => WithRetryT2_RetriesExhausted_Result(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, int, Exception, TimeSpan> delayPolicy = new FuncProxy<int, int, Exception, TimeSpan>((i, r, e) => Constants.Delay);
                    delayPolicy.Invoking += Expect.AlternatingAsc(418, typeof(IOException));
                    return delayPolicy;
                });

        private void WithRetryT2_RetriesExhausted_Result<T>(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, T, InterruptableFunc<int, int>> withRetry,
            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke,
            Func<T> delayPolicyFactory)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that eventually exhausts all of its retries
            Func<int> flakyFunc = FlakyFunc.Create<int, IOException>(418);
            FuncProxy<int, int> func = new FuncProxy<int, int>((arg) => flakyFunc());

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy = new FuncProxy<int, ResultKind>(r => r == 418 ? ResultKind.Transient : ResultKind.Fatal);
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableFunc
            InterruptableFunc<int, int> reliableFunc = withRetry(
                func.Invoke,
                3,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            func           .Invoking += Expect.ArgumentsAfterDelay<int>(Arguments.Validate, Constants.MinDelay);
            resultPolicy   .Invoking += Expect.Result(418);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.AreEqual(418, invoke(reliableFunc, 42, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(4, func           .Calls);
            Assert.AreEqual(2, resultPolicy   .Calls);
            Assert.AreEqual(2, exceptionPolicy.Calls);
            Assert.AreEqual(3, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT2_RetriesExhausted_Exception

        private void WithRetryT2_RetriesExhausted_Exception(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, DelayPolicy, InterruptableFunc<int, int>> withRetry,
            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke,
            bool useResultPolicy = true)
            => WithRetryT2_RetriesExhausted_Exception(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                invoke,
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                },
                useResultPolicy);

        private void WithRetryT2_RetriesExhausted_Exception(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, ComplexDelayPolicy<int>, InterruptableFunc<int, int>> withRetry,
            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke,
            bool useResultPolicy = true)
            => WithRetryT2_RetriesExhausted_Exception(
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

        private void WithRetryT2_RetriesExhausted_Exception<T>(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, T, InterruptableFunc<int, int>> withRetry,
            Func<InterruptableFunc<int, int>, int, CancellationToken, int> invoke,
            Func<T> delayPolicyFactory,
            bool useResultPolicy)
            where T : DelegateProxy
        {
            // Create an "unsuccessful" user-defined action that eventually exhausts all of its retries
            Func<int> flakyFunc = useResultPolicy
                ? FlakyFunc.Create<int, IOException>(418)
                : () => throw new IOException();
            FuncProxy<int, int> func = new FuncProxy<int, int>((arg) => flakyFunc());

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy    = new FuncProxy<int, ResultKind>(r => r == 418 ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableFunc
            InterruptableFunc<int, int> reliableFunc = withRetry(
                func.Invoke,
                2,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            func           .Invoking += Expect.ArgumentsAfterDelay<int>(Arguments.Validate, Constants.MinDelay);
            resultPolicy   .Invoking += Expect.Result(418);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<IOException>(() => invoke(reliableFunc, 42, tokenSource.Token));

            // Validate the number of calls
            int x = useResultPolicy ? 1 : 0;
            int y = useResultPolicy ? 2 : 3;
            Assert.AreEqual(3, func           .Calls);
            Assert.AreEqual(x, resultPolicy   .Calls);
            Assert.AreEqual(y, exceptionPolicy.Calls);
            Assert.AreEqual(2, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT2_Canceled

        private void WithRetryT2_Canceled(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, DelayPolicy, InterruptableFunc<int, int>> withRetry,
            bool useResultPolicy = true)
            => WithRetryT2_Canceled(
                (f, m, r, e, d) => withRetry(f, m, r, e, d.Invoke),
                () =>
                {
                    FuncProxy<int, TimeSpan> delayPolicy = new FuncProxy<int, TimeSpan>(i => Constants.Delay);
                    delayPolicy.Invoking += Expect.Asc();
                    return delayPolicy;
                },
                useResultPolicy);

        private void WithRetryT2_Canceled(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, ComplexDelayPolicy<int>, InterruptableFunc<int, int>> withRetry,
            bool useResultPolicy = true)
            => WithRetryT2_Canceled(
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

        private void WithRetryT2_Canceled<T>(
            Func<Func<int, int>, int, ResultPolicy<int>, ExceptionPolicy, T, InterruptableFunc<int, int>> withRetry,
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
            FuncProxy<int, int> func = new FuncProxy<int, int>((arg) => flakyFunc());

            // Declare the various policy and event handler proxies
            FuncProxy<int, ResultKind> resultPolicy    = new FuncProxy<int, ResultKind>(r => r == 418 ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            T delayPolicy = delayPolicyFactory();

            // Create the reliable InterruptableFunc
            InterruptableFunc<int, int> reliableFunc = withRetry(
                func.Invoke,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy);

            // Define expectations
            func           .Invoking += Expect.ArgumentsAfterDelay<int>(Arguments.Validate, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));

            // Trigger the event upon retry
            int minRetries = useResultPolicy ? 2 : 1;
            func.Invoking += (arg, c) =>
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
            Assert.That.ThrowsException<OperationCanceledException>(() => reliableFunc(42, tokenSource.Token));

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
