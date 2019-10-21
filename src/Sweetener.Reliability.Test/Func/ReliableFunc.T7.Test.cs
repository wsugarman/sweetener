// Generated from ReliableFunc.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    [TestClass]
    public sealed class ReliableFunc6Test : ReliableDelegateTest<string>
    {
        private static readonly Func<ReliableFunc<int, string, double, long, ushort, byte, string>, Func<int, string, double, long, ushort, byte, string>> s_getFunc = DynamicGetter.ForField<ReliableFunc<int, string, double, long, ushort, byte, string>, Func<int, string, double, long, ushort, byte, string>>("_func");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            DelayPolicy delayPolicy = DelayPolicies.Constant(115);
            Func<int, string, double, long, ushort, byte, string> func = (arg1, arg2, arg3, arg4, arg5, arg6) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, string>(func, -2              , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, string>(func, Retries.Infinite, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, ushort, byte, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, string>(func, 37, ExceptionPolicies.Transient, delayPolicy);

            // DelayPolicies are wrapped in ComplexDelayPolicies, so we can only validate the correct assignment by invoking the policy
            Ctor(actual, func, 37, ReliableDelegate<string>.DefaultResultPolicy, ExceptionPolicies.Transient, actualPolicy =>
            {
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 1, "foo", new ArgumentOutOfRangeException()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 2, "bar", new Exception                  ()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy(10, "baz", new FormatException            ()));
            });
        }

        [TestMethod]
        public void Ctor_ComplexDelayPolicy()
        {
            ComplexDelayPolicy<string> complexDelayPolicy = (i, r, e) => TimeSpan.Zero;
            Func<int, string, double, long, ushort, byte, string> func = (arg1, arg2, arg3, arg4, arg5, arg6) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, string>(func, -2              , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, string>(func, Retries.Infinite, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, ushort, byte, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, string>(func, 37, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, ReliableDelegate<string>.DefaultResultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        [TestMethod]
        public void Ctor_ResultPolicy_DelayPolicy()
        {
            Func<int, string, double, long, ushort, byte, string> func = (arg1, arg2, arg3, arg4, arg5, arg6) => "Hello World";
            ResultPolicy<string> resultPolicy = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            DelayPolicy          delayPolicy  = DelayPolicies.Constant(115);

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, string>(func, Retries.Infinite, resultPolicy, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, ushort, byte, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, delayPolicy);

            // DelayPolicies are wrapped in ComplexDelayPolicies, so we can only validate the correct assignment by invoking the policy
            Ctor(actual, func, 37, resultPolicy, ExceptionPolicies.Transient, actualPolicy =>
            {
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 1, "foo", new ArgumentOutOfRangeException()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 2, "bar", new Exception                  ()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy(10, "baz", new FormatException            ()));
            });
        }

        [TestMethod]
        public void Ctor_ResultPolicy_ComplexDelayPolicy()
        {
            Func<int, string, double, long, ushort, byte, string> func = (arg1, arg2, arg3, arg4, arg5, arg6) => "Hello World";
            ResultPolicy<string>       resultPolicy       = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            ComplexDelayPolicy<string> complexDelayPolicy = (i, r, e) => TimeSpan.Zero;

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, string>(func, Retries.Infinite, resultPolicy, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, ushort, byte, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        private void Ctor(
            ReliableFunc<int, string, double, long, ushort, byte, string> reliableFunc,
            Func<int, string, double, long, ushort, byte, string>         expectedFunc,
            int                        expectedMaxRetries,
            ResultPolicy<string>       expectedResultPolicy,
            ExceptionPolicy            expectedExceptionPolicy,
            ComplexDelayPolicy<string> expectedDelayPolicy)
            => Ctor(reliableFunc, expectedFunc, expectedMaxRetries, expectedResultPolicy, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        private void Ctor(
            ReliableFunc<int, string, double, long, ushort, byte, string> reliableFunc,
            Func<int, string, double, long, ushort, byte, string>         expectedFunc,
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

        [TestMethod]
        public void Invoke_NoCancellationToken()
            => Invoke((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6));
        
        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, token) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
        {
            TryInvoke(TryInvokeFunc);

            static bool TryInvokeFunc(ReliableFunc<int, string, double, long, ushort, byte, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, out result);
        }

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            TryInvoke(TryInvokeFunc);

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, token) => Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, token, out string result)));

            bool TryInvokeFunc(ReliableFunc<int, string, double, long, ushort, byte, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, tokenSource.Token, out result);
        }

        private void Invoke(Func<ReliableFunc<int, string, double, long, ushort, byte, string>, int, string, double, long, ushort, byte, string> invoke)
        {
            // Success
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, byte, string>, int, string, double, long, ushort, byte> assertFailureResult = (reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6)
                => Assert.AreEqual("Bad Result", reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6));

            Invoke_Failure_Result         (assertFailureResult, "Bad Result");
            Invoke_EventualFailure_Result (assertFailureResult, "Bad Result");
            Invoke_RetriesExhausted_Result(assertFailureResult, "Bad Result");

            // Failure (Exception)
            Invoke_Failure_Exception         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure_Exception <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted_Exception<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableFunc<int, string, double, long, ushort, byte, string>, int, string, double, long, ushort, byte> GetFailureAssertion<T>()
                where T : Exception
            {
                return (r, arg1, arg2, arg3, arg4, arg5, arg6) =>
                {
                    try
                    {
                        invoke(r, arg1, arg2, arg3, arg4, arg5, arg6);
                        Assert.Fail();
                    }
                    catch (T)
                    { }
                    catch (AssertFailedException)
                    {
                        throw;
                    }
                    catch (Exception)
                    {
                        Assert.Fail();
                    }
                };
            }
        }

        private void TryInvoke(TryFunc<ReliableFunc<int, string, double, long, ushort, byte, string>, int, string, double, long, ushort, byte, string> tryInvoke)
        {
            // Success
            Func<ReliableFunc<int, string, double, long, ushort, byte, string>, int, string, double, long, ushort, byte, string> assertSuccess = (r, arg1, arg2, arg3, arg4, arg5, arg6) =>
            {
                Assert.IsTrue(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, out string result));
                return result;
            };

            Invoke_Success        (assertSuccess);
            Invoke_EventualSuccess(assertSuccess);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, byte, string>, int, string, double, long, ushort, byte> assertFailure = (reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6) =>
            {
                Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, out string result));
                Assert.AreEqual(null, result);
            };

            Invoke_Failure_Result         (assertFailure);
            Invoke_EventualFailure_Result (assertFailure);
            Invoke_RetriesExhausted_Result(assertFailure);

            // Failure (Exception)
            Invoke_Failure_Exception         <             InvalidOperationException>(assertFailure);
            Invoke_EventualFailure_Exception <IOException, InvalidOperationException>(assertFailure);
            Invoke_RetriesExhausted_Exception<IOException                           >(assertFailure);
        }

        private void Invoke_Success(Func<ReliableFunc<int, string, double, long, ushort, byte, string>, int, string, double, long, ushort, byte, string> assertInvoke)
        {
            ObservableFunc<string   , ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Success" ? ResultKind.Successful : ResultKind.Fatal, "Success");
            ObservableFunc<Exception, bool      > exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int      , TimeSpan  > delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableFunc<int, string, double, long, ushort, byte, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6);
                    return "Success";
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.Failed           += (   r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255));

            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Exception<T>(Action<ReliableFunc<int, string, double, long, ushort, byte, string>, int, string, double, long, ushort, byte> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<string                , ResultKind> resultPolicy    = PolicyValidator.Create<string>(r => ResultKind.Successful);
            ObservableFunc<Exception             , bool      > exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, string, Exception, TimeSpan  > delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, ushort, byte, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6);
                    throw new T();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           += (   r, e) =>
            {
                failures++;

                Assert.IsNull(r);
                Assert.AreEqual(typeof(T), e.GetType());
            };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(0, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, string>, int, string, double, long, ushort, byte> assertInvoke, string expectedResult = "Failure")
        {
            ObservableFunc<string                , ResultKind> resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Fatal : ResultKind.Successful, expectedResult);
            ObservableFunc<Exception             , bool      > exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Transient);
            ObservableFunc<int, string, Exception, TimeSpan  > delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, ushort, byte, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6);
                    return expectedResult;
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           += (   r, e) =>
            {
                failures++;

                Assert.AreEqual(expectedResult, r);
                Assert.IsNull(e);
            };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Func<ReliableFunc<int, string, double, long, ushort, byte, string>, int, string, double, long, ushort, byte, string> assertInvoke)
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string   , ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Success" ? ResultKind.Successful : ResultKind.Retryable);
            ObservableFunc<Exception, bool      > exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int      , TimeSpan  > delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            resultPolicy.Invoking += r =>
            {
                if (resultPolicy.Calls == 1)
                    Assert.AreEqual("Transient", r);
                else
                    Assert.AreEqual("Success", r);
            };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualSuccess = FlakyFunc.Create(1, "Transient", "Success");
            ReliableFunc<int, string, double, long, ushort, byte, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6);
                    flakyAction();
                    return eventualSuccess();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableFunc.Retrying += (i, r, e) =>
            {
                Assert.AreEqual(++retries, i);

                if (i == 1)
                {
                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(IOException), e.GetType());
                }
                else if (i == 2)
                {
                    Assert.AreEqual("Transient", r);
                    Assert.IsNull(e);
                }
                else
                {
                    Assert.Fail();
                }

                TimeSpan actual = DateTime.UtcNow - delayStartUtc;
                Assert.IsTrue(actual > MinDelay, $"Actual delay {actual} less than allowed minimum delay {MinDelay}");
            };
            reliableFunc.Failed           += (r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255));

            Assert.AreEqual(2, retries);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Exception<TTransient, TFatal>(Action<ReliableFunc<int, string, double, long, ushort, byte, string>, int, string, double, long, ushort, byte> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string   , ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool      > exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int      , TimeSpan  > delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<TTransient>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, TFatal>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, ushort, byte, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6);
                    flakyAction();
                    return eventualFailure();
                },
                17,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, failures = 0;
            reliableFunc.Retrying += (i, r, e) =>
            {
                Assert.AreEqual(++retries, i);

                if (i < 3)
                {
                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(TTransient), e.GetType());
                }
                else if (i == 3)
                {
                    Assert.AreEqual("Transient", r);
                    Assert.IsNull(e);
                }
                else
                {
                    Assert.Fail();
                }

                TimeSpan actual = DateTime.UtcNow - delayStartUtc;
                Assert.IsTrue(actual > MinDelay, $"Actual delay {actual} less than allowed minimum delay {MinDelay}");
            };
            reliableFunc.Failed += (r, e) =>
            {
                failures++;

                Assert.IsNull(r);
                Assert.AreEqual(typeof(TFatal), e.GetType());
            };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255);

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(3      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, string>, int, string, double, long, ushort, byte> assertInvoke, string expectedResult = "Failure")
        {
            Assert.AreNotEqual("Transient", expectedResult);

            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string   , ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal);
            ObservableFunc<Exception, bool      > exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int      , TimeSpan  > delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            resultPolicy.Invoking += r =>
            {
                if (resultPolicy.Calls < 3)
                    Assert.AreEqual("Transient", r);
                else
                    Assert.AreEqual(expectedResult, r);
            };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualFailure = FlakyFunc.Create(2, "Transient", expectedResult);
            ReliableFunc<int, string, double, long, ushort, byte, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6);
                    flakyAction();
                    return eventualFailure();
                },
                17,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, failures = 0;
            reliableFunc.Retrying += (i, r, e) =>
            {
                Assert.AreEqual(++retries, i);

                if (i < 2)
                {
                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(IOException), e.GetType());
                }
                else if (i < 4)
                {
                    Assert.AreEqual("Transient", r);
                    Assert.IsNull(e);
                }
                else
                {
                    Assert.Fail();
                }

                TimeSpan actual = DateTime.UtcNow - delayStartUtc;
                Assert.IsTrue(actual > MinDelay, $"Actual delay {actual} less than allowed minimum delay {MinDelay}");
            };
            reliableFunc.Failed += (r, e) =>
            {
                failures++;

                Assert.AreEqual(expectedResult, r);
                Assert.IsNull(e);
            };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255);

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(3      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Exception<T>(Action<ReliableFunc<int, string, double, long, ushort, byte, string>, int, string, double, long, ushort, byte> assertInvoke)
            where T : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string   , ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool      > exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int      , TimeSpan  > delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<T>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, T>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, ushort, byte, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6);
                    flakyAction();
                    return eventualFailure();
                },
                4,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, exhausted = 0;
            reliableFunc.Retrying += (i, r, e) =>
            {
                Assert.AreEqual(++retries, i);

                if (i == 3)
                {
                    Assert.AreEqual("Transient", r);
                    Assert.IsNull(e);
                }
                else
                {
                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                }

                TimeSpan actual = DateTime.UtcNow - delayStartUtc;
                Assert.IsTrue(actual > MinDelay, $"Actual delay {actual} less than allowed minimum delay {MinDelay}");
            };
            reliableFunc.Failed           += (r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (r, e) =>
            {
                exhausted++;

                Assert.IsNull(r);
                Assert.AreEqual(typeof(T), e.GetType());
            };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255);

            Assert.AreEqual(4, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(4      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, string>, int, string, double, long, ushort, byte> assertInvoke, string expectedResult = "Transient")
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string   , ResultKind> resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Retryable : ResultKind.Fatal, expectedResult);
            ObservableFunc<Exception, bool      > exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int      , TimeSpan  > delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            ReliableFunc<int, string, double, long, ushort, byte, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6);
                    flakyAction();
                    return expectedResult;
                },
                2,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, exhausted = 0;
            reliableFunc.Retrying += (i, r, e) =>
            {
                Assert.AreEqual(++retries, i);

                if (i == 1)
                {
                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(IOException), e.GetType());
                }
                else if (i == 2)
                {
                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                }
                else
                {
                    Assert.Fail();
                }

                TimeSpan actual = DateTime.UtcNow - delayStartUtc;
                Assert.IsTrue(actual > MinDelay, $"Actual delay {actual} less than allowed minimum delay {MinDelay}");
            };
            reliableFunc.Failed           += (r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (r, e) =>
            {
                exhausted++;

                Assert.AreEqual(expectedResult, r);
                Assert.IsNull(e);
            };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255);

            Assert.AreEqual(2, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableFunc<int, string, double, long, ushort, byte, string>, int, string, double, long, ushort, byte, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<string   , ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal, "Transient");
            ObservableFunc<Exception, bool      > exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int      , TimeSpan  > delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>(1, "Transient");
            ReliableFunc<int, string, double, long, ushort, byte, string> reliableAction = new ReliableFunc<int, string, double, long, ushort, byte, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6);
                    return flakyFunc();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying += (i, r, e) =>
            {
                Assert.AreEqual(++retries, i);

                if (i == 1)
                {
                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(IOException), e.GetType());
                }
                else
                {
                    Assert.AreEqual("Transient", r);
                    Assert.IsNull(e);

                    retryEvent.Set();
                }
            };
            reliableAction.Failed           += (r, e) => Assert.Fail();
            reliableAction.RetriesExhausted += (r, e) => Assert.Fail();

            // While waiting for the reliable func to complete, we'll cancel it
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, tokenSource.Token), tokenSource.Token);

            // Cancel after at least 2 retries have occurred
            retryEvent.WaitOne();
            tokenSource.Cancel();

            // Try to get the result
            try
            {
                invocation.Wait();
                Assert.Fail();
            }
            catch (AggregateException agg)
            {
                Assert.AreEqual(1, agg.InnerExceptions.Count);
                switch (agg.InnerException)
                {
                    case AssertFailedException afe:
                        throw afe;
                    case TaskCanceledException _:
                        Assert.IsTrue(retries > 0);
                        Assert.AreEqual(retries    , resultPolicy   .Calls);
                        Assert.AreEqual(1          , exceptionPolicy.Calls);
                        Assert.AreEqual(retries + 1, delayPolicy    .Calls);
                        return; // Successfully cancelled
                    default:
                        Assert.Fail();
                        break;
                }
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        private static void AssertDelegateParameters(int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6)
        {
            Assert.AreEqual(42       , arg1);
            Assert.AreEqual("foo"    , arg2);
            Assert.AreEqual(3.14D    , arg3);
            Assert.AreEqual(1000L    , arg4);
            Assert.AreEqual((ushort)1, arg5);
            Assert.AreEqual((byte)255, arg6);
        }
    }
}
