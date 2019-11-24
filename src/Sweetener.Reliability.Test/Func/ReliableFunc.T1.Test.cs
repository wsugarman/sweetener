// Generated from ReliableFunc.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    [TestClass]
    public sealed class ReliableFuncTest : ReliableDelegateTest<string>
    {
        private static readonly Func<ReliableFunc<string>, Func<string>> s_getFunc = DynamicGetter.ForField<ReliableFunc<string>, Func<string>>("_func");

        [TestMethod]
        public void Ctor_DelayPolicy()
            => Ctor_DelayPolicy((f, m, e, d) => new ReliableFunc<string>(f, m, e, d));

        [TestMethod]
        public void Ctor_ComplexDelayPolicy()
            => Ctor_ComplexDelayPolicy((f, m, e, d) => new ReliableFunc<string>(f, m, e, d));

        [TestMethod]
        public void Ctor_ResultPolicy_DelayPolicy()
            => Ctor_ResultPolicy_DelayPolicy((f, m, r, e, d) => new ReliableFunc<string>(f, m, r, e, d));

        [TestMethod]
        public void Ctor_ResultPolicy_ComplexDelayPolicy()
            => Ctor_ResultPolicy_ComplexDelayPolicy((f, m, r, e, d) => new ReliableFunc<string>(f, m, r, e, d));

        [TestMethod]
        public void Create_DelayPolicy()
            => Ctor_DelayPolicy((f, m, e, d) => ReliableFunc.Create(f, m, e, d));

        [TestMethod]
        public void Create_ComplexDelayPolicy()
            => Ctor_ComplexDelayPolicy((f, m, e, d) => ReliableFunc.Create(f, m, e, d));

        [TestMethod]
        public void Create_ResultPolicy_DelayPolicy()
            => Ctor_ResultPolicy_DelayPolicy((f, m, r, e, d) => ReliableFunc.Create(f, m, r, e, d));

        [TestMethod]
        public void Create_ResultPolicy_ComplexDelayPolicy()
            => Ctor_ResultPolicy_ComplexDelayPolicy((f, m, r, e, d) => ReliableFunc.Create(f, m, r, e, d));

        [TestMethod]
        public void Invoke_NoCancellationToken()
            => Invoke((reliableFunc) => reliableFunc.Invoke());
        
        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableFunc) => reliableFunc.Invoke(tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, token) => reliableFunc.Invoke(token), addEventHandlers: false);
            Invoke_Canceled((reliableFunc, token) => reliableFunc.Invoke(token), addEventHandlers: true );
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
        {
            TryInvoke(TryInvokeFunc);

            static bool TryInvokeFunc(ReliableFunc<string> reliableFunc, out string result)
                => reliableFunc.TryInvoke(out result);
        }

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            TryInvoke(TryInvokeFunc);

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, token) => reliableFunc.TryInvoke(token, out string _), addEventHandlers: false);
            Invoke_Canceled((reliableFunc, token) => reliableFunc.TryInvoke(token, out string _), addEventHandlers: true );

            bool TryInvokeFunc(ReliableFunc<string> reliableFunc, out string result)
                => reliableFunc.TryInvoke(tokenSource.Token, out result);
        }

        #region Ctor

        private void Ctor_DelayPolicy(Func<Func<string>, int, ExceptionPolicy, DelayPolicy, ReliableFunc<string>> factory)
        {
            Func<string> func = () => "Hello World";
            ExceptionPolicy          exceptionPolicy = ExceptionPolicies.Retry<IOException>();
            FuncProxy<int, TimeSpan> delayPolicy     = new FuncProxy<int, TimeSpan>(i => Constants.Delay);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, exceptionPolicy, delayPolicy.Invoke));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(func, -2              , exceptionPolicy, delayPolicy.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, null           , delayPolicy.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, exceptionPolicy, null              ));

            // Create a ReliableFunc and validate
            ReliableFunc<string> actual = factory(func, 37, exceptionPolicy, delayPolicy.Invoke);

            // DelayPolicies are wrapped in ComplexDelayPolicies, so we can only validate the correct assignment by invoking the policy
            Ctor(actual, func, 37, ReliableDelegate<string>.DefaultResultPolicy, exceptionPolicy, actualPolicy =>
            {
                delayPolicy.Invoking += (i, c) => Assert.AreEqual(i, 42);
                Assert.AreEqual(Constants.Delay, actualPolicy(42, "foo", new ArgumentOutOfRangeException()));
                Assert.AreEqual(1, delayPolicy.Calls);
            });
        }

        private void Ctor_ComplexDelayPolicy(Func<Func<string>, int, ExceptionPolicy, ComplexDelayPolicy<string>, ReliableFunc<string>> factory)
        {
            Func<string> func = () => "Hello World";
            ExceptionPolicy            exceptionPolicy    = ExceptionPolicies.Retry<IOException>();
            ComplexDelayPolicy<string> complexDelayPolicy = (i, r, e) => TimeSpan.FromSeconds(3);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, exceptionPolicy, complexDelayPolicy));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(func, -2              , exceptionPolicy, complexDelayPolicy));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, null           , complexDelayPolicy));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, exceptionPolicy, null              ));

            // Create a ReliableFunc and validate
            ReliableFunc<string> actual = factory(func, 37, exceptionPolicy, complexDelayPolicy);
            Ctor(actual, func, 37, ReliableDelegate<string>.DefaultResultPolicy, exceptionPolicy, complexDelayPolicy);
        }

        private void Ctor_ResultPolicy_DelayPolicy(Func<Func<string>, int, ResultPolicy<string>, ExceptionPolicy, DelayPolicy, ReliableFunc<string>> factory)
        {
            Func<string> func = () => "Hello World";
            ResultPolicy<string>     resultPolicy    = r => ResultKind.Transient;
            ExceptionPolicy          exceptionPolicy = ExceptionPolicies.Retry<IOException>();
            FuncProxy<int, TimeSpan> delayPolicy     = new FuncProxy<int, TimeSpan>(i => Constants.Delay);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, resultPolicy, exceptionPolicy, delayPolicy.Invoke));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(func, -2              , resultPolicy, exceptionPolicy, delayPolicy.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, null        , exceptionPolicy, delayPolicy.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, resultPolicy, null           , delayPolicy.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, resultPolicy, exceptionPolicy, null              ));

            // Create a ReliableFunc and validate
            ReliableFunc<string> actual = factory(func, 37, resultPolicy, exceptionPolicy, delayPolicy.Invoke);

            // DelayPolicies are wrapped in ComplexDelayPolicies, so we can only validate the correct assignment by invoking the policy
            Ctor(actual, func, 37, resultPolicy, exceptionPolicy, actualPolicy =>
            {
                delayPolicy.Invoking += (i, c) => Assert.AreEqual(i, 42);
                Assert.AreEqual(Constants.Delay, actualPolicy(42, "foo", new ArgumentOutOfRangeException()));
                Assert.AreEqual(1, delayPolicy.Calls);
            });
        }

        private void Ctor_ResultPolicy_ComplexDelayPolicy(Func<Func<string>, int, ResultPolicy<string>, ExceptionPolicy, ComplexDelayPolicy<string>, ReliableFunc<string>> factory)
        {
            Func<string> func = () => "Hello World";
            ResultPolicy<string>       resultPolicy       = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            ExceptionPolicy            exceptionPolicy    = ExceptionPolicies.Fail<FormatException>();
            ComplexDelayPolicy<string> complexDelayPolicy = (i, r, e) => TimeSpan.Zero;

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, resultPolicy, exceptionPolicy, complexDelayPolicy));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(func, -2              , resultPolicy, exceptionPolicy, complexDelayPolicy));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, null        , exceptionPolicy, complexDelayPolicy));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, resultPolicy, null           , complexDelayPolicy));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, resultPolicy, exceptionPolicy, null              ));

            // Create a ReliableFunc and validate
            ReliableFunc<string> actual = factory(func, 37, resultPolicy, exceptionPolicy, complexDelayPolicy);
            Ctor(actual, func, 37, resultPolicy, exceptionPolicy, complexDelayPolicy);
        }

        private void Ctor(
            ReliableFunc<string> reliableFunc,
            Func<string>         expectedFunc,
            int                        expectedMaxRetries,
            ResultPolicy<string>       expectedResultPolicy,
            ExceptionPolicy            expectedExceptionPolicy,
            ComplexDelayPolicy<string> expectedDelayPolicy)
            => Ctor(reliableFunc, expectedFunc, expectedMaxRetries, expectedResultPolicy, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        private void Ctor(
            ReliableFunc<string> reliableFunc,
            Func<string>         expectedFunc,
            int                                expectedMaxRetries,
            ResultPolicy<string>               expectedResultPolicy,
            ExceptionPolicy                    expectedExceptionPolicy,
            Action<ComplexDelayPolicy<string>> validateDelayPolicy)
        {
            Assert.AreEqual(expectedMaxRetries, reliableFunc.MaxRetries);

            Assert.AreSame(expectedFunc           , s_getFunc           (reliableFunc));
            Assert.AreSame(expectedResultPolicy   , s_getResultPolicy   (reliableFunc));
            Assert.AreSame(expectedExceptionPolicy, s_getExceptionPolicy(reliableFunc));

            validateDelayPolicy(s_getDelayPolicy(reliableFunc));
        }

        #endregion

        #region Invoke

        private void Invoke(Func<ReliableFunc<string>, string> invoke)
        {
            foreach (bool addEventHandlers in new bool[] { false, true })
            {
                // Success
                Invoke_Success                ((f, r) => Assert.AreEqual(r, invoke(f)), addEventHandlers);
                Invoke_EventualSuccess        ((f, r) => Assert.AreEqual(r, invoke(f)), addEventHandlers);

                // Failure (Result)
                Invoke_Failure_Result         ((f, r) => Assert.AreEqual(r, invoke(f)), addEventHandlers);
                Invoke_EventualFailure_Result ((f, r) => Assert.AreEqual(r, invoke(f)), addEventHandlers);
                Invoke_RetriesExhausted_Result((f, r) => Assert.AreEqual(r, invoke(f)), addEventHandlers);

                // Failure (Exception)
                Invoke_Failure_Exception         ((f, t) => Assert.That.ThrowsException(() => invoke(f), t), addEventHandlers);
                Invoke_EventualFailure_Exception ((f, t) => Assert.That.ThrowsException(() => invoke(f), t), addEventHandlers);
                Invoke_RetriesExhausted_Exception((f, t) => Assert.That.ThrowsException(() => invoke(f), t), addEventHandlers);
            }
        }

        #endregion

        #region TryInvoke

        private void TryInvoke(TryFunc<ReliableFunc<string>, string> tryInvoke)
        {
            Action<ReliableFunc<string>, string> assertSuccess =
                (f, r) =>
                {
                    Assert.IsTrue(tryInvoke(f, out string actual));
                    Assert.AreEqual(r, actual);
                };

            Action<ReliableFunc<string>, string> assertResultFailure =
                (f, r) =>
                {
                    Assert.IsFalse(tryInvoke(f, out string actual));
                    Assert.AreEqual(default, actual);
                };

            Action<ReliableFunc<string>, Type> assertExceptionFailure =
                (f, r) =>
                {
                    Assert.IsFalse(tryInvoke(f, out string actual));
                    Assert.AreEqual(default, actual);
                };

            foreach (bool addEventHandlers in new bool[] { false, true })
            {
                // Success
                Invoke_Success                (assertSuccess, addEventHandlers);
                Invoke_EventualSuccess        (assertSuccess, addEventHandlers);

                // Failure (Result)
                Invoke_Failure_Result         (assertResultFailure, addEventHandlers);
                Invoke_EventualFailure_Result (assertResultFailure, addEventHandlers);
                Invoke_RetriesExhausted_Result(assertResultFailure, addEventHandlers);

                // Failure (Exception)
                Invoke_Failure_Exception         (assertExceptionFailure, addEventHandlers);
                Invoke_EventualFailure_Exception (assertExceptionFailure, addEventHandlers);
                Invoke_RetriesExhausted_Exception(assertExceptionFailure, addEventHandlers);
            }
        }

        #endregion

        #region Invoke_Success

        private void Invoke_Success(Action<ReliableFunc<string>, string> assertInvoke, bool addEventHandlers)
        {
            // Create a "successful" user-defined function
            FuncProxy<string> func = new FuncProxy<string>(() => "Success");

            // Declare the various policy and event handler proxies
            FuncProxy<string, ResultKind>               resultPolicy    = new FuncProxy<string, ResultKind>(r => r == "Success" ? ResultKind.Successful : ResultKind.Fatal);
            FuncProxy<Exception, bool>                  exceptionPolicy = new FuncProxy<Exception, bool>();
            FuncProxy<int, string, Exception, TimeSpan> delayPolicy     = new FuncProxy<int, string, Exception, TimeSpan>();

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableFunc
            ReliableFunc<string> reliableFunc = new ReliableFunc<string>(
                func.Invoke,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            if (addEventHandlers)
            {
                reliableFunc.Retrying         += retryHandler    .Invoke;
                reliableFunc.Failed           += failedHandler   .Invoke;
                reliableFunc.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            resultPolicy    .Invoking += Expect.Result("Success");
            exceptionPolicy .Invoking += Expect.Nothing<Exception>();
            delayPolicy     .Invoking += Expect.Nothing<int, string, Exception>();
            retryHandler    .Invoking += Expect.Nothing<int, string, Exception>();
            failedHandler   .Invoking += Expect.Nothing<string, Exception>();
            exhaustedHandler.Invoking += Expect.Nothing<string, Exception>();

            // Invoke
            assertInvoke(reliableFunc, "Success");

            // Validate the number of calls
            Assert.AreEqual(1, func            .Calls);
            Assert.AreEqual(1, resultPolicy    .Calls);
            Assert.AreEqual(0, exceptionPolicy .Calls);
            Assert.AreEqual(0, delayPolicy     .Calls);
            Assert.AreEqual(0, retryHandler    .Calls);
            Assert.AreEqual(0, failedHandler   .Calls);
            Assert.AreEqual(0, exhaustedHandler.Calls);
        }

        #endregion

        #region Invoke_Failure_Result

        private void Invoke_Failure_Result(Action<ReliableFunc<string>, string> assertInvoke, bool addEventHandlers)
        {
            // Create an "unsuccessful" user-defined function that returns a fatal result
            FuncProxy<string> func = new FuncProxy<string>(() => "Failure");

            // Declare the various policy and event handler proxies
            FuncProxy<string, ResultKind>               resultPolicy    = new FuncProxy<string, ResultKind>(r => r == "Failure" ? ResultKind.Fatal : ResultKind.Successful);
            FuncProxy<Exception, bool>                  exceptionPolicy = new FuncProxy<Exception, bool>();
            FuncProxy<int, string, Exception, TimeSpan> delayPolicy     = new FuncProxy<int, string, Exception, TimeSpan>();

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableFunc
            ReliableFunc<string> reliableFunc = new ReliableFunc<string>(
                func.Invoke,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            if (addEventHandlers)
            {
                reliableFunc.Retrying         += retryHandler    .Invoke;
                reliableFunc.Failed           += failedHandler   .Invoke;
                reliableFunc.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            resultPolicy    .Invoking += Expect.Result("Failure");
            exceptionPolicy .Invoking += Expect.Nothing<Exception>();
            delayPolicy     .Invoking += Expect.Nothing<int, string, Exception>();
            retryHandler    .Invoking += Expect.Nothing<int, string, Exception>();
            failedHandler   .Invoking += Expect.OnlyResult("Failure");
            exhaustedHandler.Invoking += Expect.Nothing<string, Exception>();

            // Invoke
            assertInvoke(reliableFunc, "Failure");

            // Validate the number of calls
            Assert.AreEqual(1, func           .Calls);
            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(0, retryHandler    .Calls);
                Assert.AreEqual(1, failedHandler   .Calls);
                Assert.AreEqual(0, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_Failure_Exception

        private void Invoke_Failure_Exception(Action<ReliableFunc<string>, Type> assertInvoke, bool addEventHandlers)
        {
            // Create an "unsuccessful" user-defined function that throws a fatal exception
            FuncProxy<string> func = new FuncProxy<string>(() => throw new InvalidOperationException());

            // Declare the various policy and event handler proxies
            FuncProxy<string, ResultKind>               resultPolicy    = new FuncProxy<string, ResultKind>();
            FuncProxy<Exception, bool>                  exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Fail<InvalidOperationException>().Invoke);
            FuncProxy<int, string, Exception, TimeSpan> delayPolicy     = new FuncProxy<int, string, Exception, TimeSpan>();

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableFunc
            ReliableFunc<string> reliableFunc = new ReliableFunc<string>(
                func.Invoke,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            if (addEventHandlers)
            {
                reliableFunc.Retrying         += retryHandler    .Invoke;
                reliableFunc.Failed           += failedHandler   .Invoke;
                reliableFunc.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            resultPolicy    .Invoking += Expect.Nothing<string>();
            exceptionPolicy .Invoking += Expect.Exception(typeof(InvalidOperationException));
            delayPolicy     .Invoking += Expect.Nothing<int, string, Exception>();
            retryHandler    .Invoking += Expect.Nothing<int, string, Exception>();
            failedHandler   .Invoking += Expect.OnlyException<string>(typeof(InvalidOperationException));
            exhaustedHandler.Invoking += Expect.Nothing<string, Exception>();

            // Invoke
            assertInvoke(reliableFunc, typeof(InvalidOperationException));

            // Validate the number of calls
            Assert.AreEqual(1, func            .Calls);
            Assert.AreEqual(0, resultPolicy    .Calls);
            Assert.AreEqual(1, exceptionPolicy .Calls);
            Assert.AreEqual(0, delayPolicy     .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(0, retryHandler    .Calls);
                Assert.AreEqual(1, failedHandler   .Calls);
                Assert.AreEqual(0, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_EventualSuccess

        private void Invoke_EventualSuccess(Action<ReliableFunc<string>, string> assertInvoke, bool addEventHandlers)
        {
            // Create a user-defined function that eventually succeeds after a transient result and exception
            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>("Retry", "Success", 2);
            FuncProxy<string> func = new FuncProxy<string>(() => flakyFunc());

            // Declare the various policy and event handler proxies
            FuncProxy<string, ResultKind>               resultPolicy    = new FuncProxy<string, ResultKind>(r =>
                r switch
                {
                    "Retry"   => ResultKind.Transient,
                    "Success" => ResultKind.Successful,
                    _         => ResultKind.Fatal,
                });
            FuncProxy<Exception, bool>                  exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            FuncProxy<int, string, Exception, TimeSpan> delayPolicy     = new FuncProxy<int, string, Exception, TimeSpan>((i, r, e) => Constants.Delay);

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableFunc
            ReliableFunc<string> reliableFunc = new ReliableFunc<string>(
                func.Invoke,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            if (addEventHandlers)
            {
                reliableFunc.Retrying         += retryHandler    .Invoke;
                reliableFunc.Failed           += failedHandler   .Invoke;
                reliableFunc.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            func            .Invoking += Expect.AfterDelay(Constants.MinDelay);
            resultPolicy    .Invoking += Expect.Results("Retry", "Success", 1);
            exceptionPolicy .Invoking += Expect.Exception(typeof(IOException));
            delayPolicy     .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            retryHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<string, Exception>();
            exhaustedHandler.Invoking += Expect.Nothing<string, Exception>();

            // Invoke
            assertInvoke(reliableFunc, "Success");

            // Validate the number of calls
            Assert.AreEqual(3, func            .Calls);
            Assert.AreEqual(2, resultPolicy    .Calls);
            Assert.AreEqual(1, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(2, retryHandler    .Calls);
                Assert.AreEqual(0, failedHandler   .Calls);
                Assert.AreEqual(0, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_EventualFailure_Result

        private void Invoke_EventualFailure_Result(Action<ReliableFunc<string>, string> assertInvoke, bool addEventHandlers)
        {
            // Create a user-defined function that eventually fails after a transient result and exception
            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>("Retry", "Failure", 2);
            FuncProxy<string> func = new FuncProxy<string>(() => flakyFunc());

            // Declare the various policy and event handler proxies
            FuncProxy<string, ResultKind>               resultPolicy    = new FuncProxy<string, ResultKind>(r =>
                r switch
                {
                    "Retry"   => ResultKind.Transient,
                    "Failure" => ResultKind.Fatal,
                    _         => ResultKind.Successful,
                });
            FuncProxy<Exception, bool>                  exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            FuncProxy<int, string, Exception, TimeSpan> delayPolicy     = new FuncProxy<int, string, Exception, TimeSpan>((i, r, e) => Constants.Delay);

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableFunc
            ReliableFunc<string> reliableFunc = new ReliableFunc<string>(
                func.Invoke,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            if (addEventHandlers)
            {
                reliableFunc.Retrying         += retryHandler    .Invoke;
                reliableFunc.Failed           += failedHandler   .Invoke;
                reliableFunc.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            func            .Invoking += Expect.AfterDelay(Constants.MinDelay);
            resultPolicy    .Invoking += Expect.Results("Retry", "Failure", 1);
            exceptionPolicy .Invoking += Expect.Exception(typeof(IOException));
            delayPolicy     .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            retryHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            failedHandler   .Invoking += Expect.OnlyResult("Failure");
            exhaustedHandler.Invoking += Expect.Nothing<string, Exception>();

            // Invoke
            assertInvoke(reliableFunc, "Failure");

            // Validate the number of calls
            Assert.AreEqual(3, func            .Calls);
            Assert.AreEqual(2, resultPolicy    .Calls);
            Assert.AreEqual(1, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(2, retryHandler    .Calls);
                Assert.AreEqual(1, failedHandler   .Calls);
                Assert.AreEqual(0, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_EventualFailure_Exception

        private void Invoke_EventualFailure_Exception(Action<ReliableFunc<string>, Type> assertInvoke, bool addEventHandlers)
        {
            // Create a user-defined function that eventually fails after a transient result and exception
            Func<string> flakyFunc = FlakyFunc.Create<string, IOException, InvalidOperationException>("Retry", 2);
            FuncProxy<string> func = new FuncProxy<string>(() => flakyFunc());

            // Declare the various policy and event handler proxies
            FuncProxy<string, ResultKind>               resultPolicy    = new FuncProxy<string, ResultKind>(r => r == "Retry" ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool>                  exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            FuncProxy<int, string, Exception, TimeSpan> delayPolicy     = new FuncProxy<int, string, Exception, TimeSpan>((i, r, e) => Constants.Delay);

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableFunc
            ReliableFunc<string> reliableFunc = new ReliableFunc<string>(
                func.Invoke,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            if (addEventHandlers)
            {
                reliableFunc.Retrying         += retryHandler    .Invoke;
                reliableFunc.Failed           += failedHandler   .Invoke;
                reliableFunc.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            func            .Invoking += Expect.AfterDelay(Constants.MinDelay);
            resultPolicy    .Invoking += Expect.Result("Retry");
            exceptionPolicy .Invoking += Expect.Exceptions(typeof(IOException), typeof(InvalidOperationException), 1);
            delayPolicy     .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            retryHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            failedHandler   .Invoking += Expect.OnlyException<string>(typeof(InvalidOperationException));
            exhaustedHandler.Invoking += Expect.Nothing<string, Exception>();

            // Invoke
            assertInvoke(reliableFunc, typeof(InvalidOperationException));

            // Validate the number of calls
            Assert.AreEqual(3, func            .Calls);
            Assert.AreEqual(1, resultPolicy    .Calls);
            Assert.AreEqual(2, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(2, retryHandler    .Calls);
                Assert.AreEqual(1, failedHandler   .Calls);
                Assert.AreEqual(0, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_RetriesExhausted_Result

        private void Invoke_RetriesExhausted_Result(Action<ReliableFunc<string>, string> assertInvoke, bool addEventHandlers)
        {
            // Create a user-defined function that eventually exhausts the maximum number of retries after transient results and exceptions
            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>("Retry");
            FuncProxy<string> func = new FuncProxy<string>(() => flakyFunc());

            // Declare the various policy and event handler proxies
            FuncProxy<string, ResultKind>               resultPolicy    = new FuncProxy<string, ResultKind>(r => r == "Retry" ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool>                  exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            FuncProxy<int, string, Exception, TimeSpan> delayPolicy     = new FuncProxy<int, string, Exception, TimeSpan>((i, r, e) => Constants.Delay);

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableFunc
            ReliableFunc<string> reliableFunc = new ReliableFunc<string>(
                func.Invoke,
                3, // Exception, Result, Exception, Result, ...
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            if (addEventHandlers)
            {
                reliableFunc.Retrying         += retryHandler    .Invoke;
                reliableFunc.Failed           += failedHandler   .Invoke;
                reliableFunc.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            func            .Invoking += Expect.AfterDelay(Constants.MinDelay);
            resultPolicy    .Invoking += Expect.Result("Retry");
            exceptionPolicy .Invoking += Expect.Exception(typeof(IOException));
            delayPolicy     .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            retryHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<string, Exception>();
            exhaustedHandler.Invoking += Expect.OnlyResult("Retry");

            // Invoke
            assertInvoke(reliableFunc, "Retry");

            // Validate the number of calls
            Assert.AreEqual(4, func            .Calls);
            Assert.AreEqual(2, resultPolicy    .Calls);
            Assert.AreEqual(2, exceptionPolicy .Calls);
            Assert.AreEqual(3, delayPolicy     .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(3, retryHandler    .Calls);
                Assert.AreEqual(0, failedHandler   .Calls);
                Assert.AreEqual(1, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_RetriesExhausted_Exception

        private void Invoke_RetriesExhausted_Exception(Action<ReliableFunc<string>, Type> assertInvoke, bool addEventHandlers)
        {
            // Create a user-defined function that eventually exhausts the maximum number of retries after transient results and exceptions
            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>("Retry");
            FuncProxy<string> func = new FuncProxy<string>(() => flakyFunc());

            // Declare the various policy and event handler proxies
            FuncProxy<string, ResultKind>               resultPolicy    = new FuncProxy<string, ResultKind>(r => r == "Retry" ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool>                  exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            FuncProxy<int, string, Exception, TimeSpan> delayPolicy     = new FuncProxy<int, string, Exception, TimeSpan>((i, r, e) => Constants.Delay);

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableFunc
            ReliableFunc<string> reliableFunc = new ReliableFunc<string>(
                func.Invoke,
                2, // Exception, Result, Exception, ...
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            if (addEventHandlers)
            {
                reliableFunc.Retrying         += retryHandler    .Invoke;
                reliableFunc.Failed           += failedHandler   .Invoke;
                reliableFunc.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            func            .Invoking += Expect.AfterDelay(Constants.MinDelay);
            resultPolicy    .Invoking += Expect.Result("Retry");
            exceptionPolicy .Invoking += Expect.Exception(typeof(IOException));
            delayPolicy     .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            retryHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<string, Exception>();
            exhaustedHandler.Invoking += Expect.OnlyException<string>(typeof(IOException));

            // Invoke
            assertInvoke(reliableFunc, typeof(IOException));

            // Validate the number of calls
            Assert.AreEqual(3, func            .Calls);
            Assert.AreEqual(1, resultPolicy    .Calls);
            Assert.AreEqual(2, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(2, retryHandler    .Calls);
                Assert.AreEqual(0, failedHandler   .Calls);
                Assert.AreEqual(1, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_Canceled

        private void Invoke_Canceled(Action<ReliableFunc<string>, CancellationToken> assertInvoke, bool addEventHandlers)
        {
            using ManualResetEvent        cancellationTrigger = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource         = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined function that continues to fail with transient results and exceptions until it's canceled
            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>("Retry");
            FuncProxy<string> func = new FuncProxy<string>(() => flakyFunc());

            // Declare the various policy and event handler proxies
            FuncProxy<string, ResultKind>               resultPolicy    = new FuncProxy<string, ResultKind>(r => r == "Retry" ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool>                  exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            FuncProxy<int, string, Exception, TimeSpan> delayPolicy     = new FuncProxy<int, string, Exception, TimeSpan>((i, r, e) => Constants.Delay);

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableFunc
            ReliableFunc<string> reliableFunc = new ReliableFunc<string>(
                func.Invoke,
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            if (addEventHandlers)
            {
                reliableFunc.Retrying         += retryHandler    .Invoke;
                reliableFunc.Failed           += failedHandler   .Invoke;
                reliableFunc.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            func            .Invoking += Expect.AfterDelay(Constants.MinDelay);
            resultPolicy    .Invoking += Expect.Result("Retry");
            exceptionPolicy .Invoking += Expect.Exception(typeof(IOException));
            delayPolicy     .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            retryHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<string, Exception>();
            exhaustedHandler.Invoking += Expect.Nothing<string, Exception>();

            // Trigger the event upon retry
            func            .Invoking += (c) =>
            {
                if (c.Calls > 1)
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
            Assert.That.ThrowsException<OperationCanceledException>(() => assertInvoke(reliableFunc, tokenSource.Token));

            // Validate the number of calls
            int calls      = func.Calls;
            int results    = calls / 2;
            int exceptions = calls - results;
            Assert.IsTrue(calls > 1);

            Assert.AreEqual(results   , resultPolicy    .Calls);
            Assert.AreEqual(exceptions, exceptionPolicy .Calls);
            Assert.AreEqual(calls     , delayPolicy     .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(calls - 1 , retryHandler    .Calls);
                Assert.AreEqual(0         , failedHandler   .Calls);
                Assert.AreEqual(0         , exhaustedHandler.Calls);
            }
        }

        #endregion
    }
}
