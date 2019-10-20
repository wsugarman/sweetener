// Generated from ReliableFunc.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    #region ReliableFunc<TResult>

    [TestClass]
    public sealed class ReliableFuncTest : ReliableDelegateTest<string>
    {
        private static readonly Func<ReliableFunc<string>, Func<string>> s_getFunc = DynamicGetter.ForField<ReliableFunc<string>, Func<string>>("_func");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            DelayPolicy delayPolicy = DelayPolicies.Constant(115);
            Func<string> func = () => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<string>(null, Retries.Infinite, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<string>(func, -2              , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<string>(func, Retries.Infinite, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<string> actual = new ReliableFunc<string>(func, 37, ExceptionPolicies.Transient, delayPolicy);

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
            Func<string> func = () => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<string>(null, Retries.Infinite, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<string>(func, -2              , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<string>(func, Retries.Infinite, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<string> actual = new ReliableFunc<string>(func, 37, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, ReliableDelegate<string>.DefaultResultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        [TestMethod]
        public void Ctor_ResultPolicy_DelayPolicy()
        {
            Func<string> func = () => "Hello World";
            ResultPolicy<string> resultPolicy = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            DelayPolicy          delayPolicy  = DelayPolicies.Constant(115);

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<string>(func, Retries.Infinite, resultPolicy, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<string> actual = new ReliableFunc<string>(func, 37, resultPolicy, ExceptionPolicies.Transient, delayPolicy);

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
            Func<string> func = () => "Hello World";
            ResultPolicy<string>       resultPolicy       = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            ComplexDelayPolicy<string> complexDelayPolicy = (i, r, e) => TimeSpan.Zero;

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<string>(func, Retries.Infinite, resultPolicy, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<string> actual = new ReliableFunc<string>(func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
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

        [TestMethod]
        public void Invoke_NoCancellationToken()
            => Invoke((reliableFunc) => reliableFunc.Invoke());
        
        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableFunc) => reliableFunc.Invoke(tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, token) => reliableFunc.Invoke(token));
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
            Invoke_Canceled((reliableFunc, token) => Assert.IsFalse(reliableFunc.TryInvoke(token, out string result)));

            bool TryInvokeFunc(ReliableFunc<string> reliableFunc, out string result)
                => reliableFunc.TryInvoke(tokenSource.Token, out result);
        }

        private void Invoke(Func<ReliableFunc<string>, string> invoke)
        {
            // Success
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            // Failure (Bad Result)
            Action<ReliableFunc<string>> assertFailureResult =
                (reliableFunc) => Assert.AreEqual("Bad Result", reliableFunc.Invoke());

            Invoke_Failure_Result         (assertFailureResult, "Bad Result");
            Invoke_EventualFailure_Result (assertFailureResult, "Bad Result");
            Invoke_RetriesExhausted_Result(assertFailureResult, "Bad Result");

            // Failure (Exception)
            Invoke_Failure_Exception         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure_Exception <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted_Exception<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableFunc<string>> GetFailureAssertion<T>()
                where T : Exception
            {
                return (r) =>
                {
                    try
                    {
                        invoke(r);
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

        private void TryInvoke(TryFunc<ReliableFunc<string>, string> tryInvoke)
        {
            // Success
            Func<ReliableFunc<string>, string> assertSuccess =
                (r) =>
                {
                    Assert.IsTrue(tryInvoke(r, out string result));
                    return result;
                };

            Invoke_Success        (assertSuccess);
            Invoke_EventualSuccess(assertSuccess);

            // Failure (Bad Result)
            Action<ReliableFunc<string>> assertFailure =
                (reliableFunc) =>
                {
                    Assert.IsFalse(reliableFunc.TryInvoke(out string result));
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

        private void Invoke_Success(Func<ReliableFunc<string>, string> assertInvoke)
        {
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Success" ? ResultKind.Successful : ResultKind.Fatal, "Success");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableFunc<string> reliableFunc = new ReliableFunc<string>(
                () =>
                {
                    return "Success";
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.Failed           += (   r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();

            Assert.AreEqual("Success", assertInvoke(reliableFunc));

            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Exception<T>(Action<ReliableFunc<string>> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create<string>(r => ResultKind.Successful);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<string> reliableFunc = new ReliableFunc<string>(
                () =>
                {
                    throw new T();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(0, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Result(Action<ReliableFunc<string>> assertInvoke, string expectedResult = "Failure")
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Fatal : ResultKind.Successful, expectedResult);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Transient);
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<string> reliableFunc = new ReliableFunc<string>(
                () =>
                {
                    return expectedResult;
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Func<ReliableFunc<string>, string> assertInvoke)
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Success" ? ResultKind.Successful : ResultKind.Retryable);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls == 1)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual("Success", r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualSuccess = FlakyFunc.Create(1, "Transient", "Success");
            ReliableFunc<string> reliableFunc = new ReliableFunc<string>(
                () =>
                {
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

            Assert.AreEqual("Success", assertInvoke(reliableFunc));

            Assert.AreEqual(2, retries);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Exception<TTransient, TFatal>(Action<ReliableFunc<string>> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<TTransient>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, TFatal>(1, "Transient", transientError: false);
            ReliableFunc<string> reliableFunc = new ReliableFunc<string>(
                () =>
                {
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(TFatal), e.GetType());
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc);

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(3      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Result(Action<ReliableFunc<string>> assertInvoke, string expectedResult = "Failure")
        {
            Assert.AreNotEqual("Transient", expectedResult);

            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

             resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls < 3)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual(expectedResult, r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualFailure = FlakyFunc.Create(2, "Transient", expectedResult);
            ReliableFunc<string> reliableFunc = new ReliableFunc<string>(
                () =>
                {
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc);

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(3      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Exception<T>(Action<ReliableFunc<string>> assertInvoke)
            where T : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<T>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, T>(1, "Transient", transientError: false);
            ReliableFunc<string> reliableFunc = new ReliableFunc<string>(
                () =>
                {
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc);

            Assert.AreEqual(4, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(4      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Result(Action<ReliableFunc<string>> assertInvoke, string expectedResult = "Transient")
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Retryable : ResultKind.Fatal, expectedResult);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            ReliableFunc<string> reliableFunc = new ReliableFunc<string>(
                () =>
                {
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc);

            Assert.AreEqual(2, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableFunc<string>, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>(1, "Transient");
            ReliableFunc<string> reliableAction = new ReliableFunc<string>(
                () =>
                {
                    return flakyFunc();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying +=
                (i, r, e) =>
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, tokenSource.Token), tokenSource.Token);

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
    }

    #endregion

    #region ReliableFunc<T, TResult>

    [TestClass]
    public sealed class ReliableFunc1Test : ReliableDelegateTest<string>
    {
        private static readonly Func<ReliableFunc<int, string>, Func<int, string>> s_getFunc = DynamicGetter.ForField<ReliableFunc<int, string>, Func<int, string>>("_func");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            DelayPolicy delayPolicy = DelayPolicies.Constant(115);
            Func<int, string> func = (arg) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string>(func, -2              , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string>(func, Retries.Infinite, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string> actual = new ReliableFunc<int, string>(func, 37, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string> func = (arg) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string>(func, -2              , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string>(func, Retries.Infinite, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string> actual = new ReliableFunc<int, string>(func, 37, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, ReliableDelegate<string>.DefaultResultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        [TestMethod]
        public void Ctor_ResultPolicy_DelayPolicy()
        {
            Func<int, string> func = (arg) => "Hello World";
            ResultPolicy<string> resultPolicy = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            DelayPolicy          delayPolicy  = DelayPolicies.Constant(115);

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string>(func, Retries.Infinite, resultPolicy, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string> actual = new ReliableFunc<int, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string> func = (arg) => "Hello World";
            ResultPolicy<string>       resultPolicy       = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            ComplexDelayPolicy<string> complexDelayPolicy = (i, r, e) => TimeSpan.Zero;

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string>(func, Retries.Infinite, resultPolicy, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string> actual = new ReliableFunc<int, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        private void Ctor(
            ReliableFunc<int, string> reliableFunc,
            Func<int, string>         expectedFunc,
            int                        expectedMaxRetries,
            ResultPolicy<string>       expectedResultPolicy,
            ExceptionPolicy            expectedExceptionPolicy,
            ComplexDelayPolicy<string> expectedDelayPolicy)
            => Ctor(reliableFunc, expectedFunc, expectedMaxRetries, expectedResultPolicy, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        private void Ctor(
            ReliableFunc<int, string> reliableFunc,
            Func<int, string>         expectedFunc,
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
            => Invoke((reliableFunc, arg) => reliableFunc.Invoke(arg));
        
        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableFunc, arg) => reliableFunc.Invoke(arg, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg, token) => reliableFunc.Invoke(arg, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
        {
            TryInvoke(TryInvokeFunc);

            static bool TryInvokeFunc(ReliableFunc<int, string> reliableFunc, int arg, out string result)
                => reliableFunc.TryInvoke(arg, out result);
        }

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            TryInvoke(TryInvokeFunc);

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg, token) => Assert.IsFalse(reliableFunc.TryInvoke(arg, token, out string result)));

            bool TryInvokeFunc(ReliableFunc<int, string> reliableFunc, int arg, out string result)
                => reliableFunc.TryInvoke(arg, tokenSource.Token, out result);
        }

        private void Invoke(Func<ReliableFunc<int, string>, int, string> invoke)
        {
            // Success
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string>, int> assertFailureResult =
                (reliableFunc, arg) => Assert.AreEqual("Bad Result", reliableFunc.Invoke(arg));

            Invoke_Failure_Result         (assertFailureResult, "Bad Result");
            Invoke_EventualFailure_Result (assertFailureResult, "Bad Result");
            Invoke_RetriesExhausted_Result(assertFailureResult, "Bad Result");

            // Failure (Exception)
            Invoke_Failure_Exception         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure_Exception <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted_Exception<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableFunc<int, string>, int> GetFailureAssertion<T>()
                where T : Exception
            {
                return (r, arg) =>
                {
                    try
                    {
                        invoke(r, arg);
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

        private void TryInvoke(TryFunc<ReliableFunc<int, string>, int, string> tryInvoke)
        {
            // Success
            Func<ReliableFunc<int, string>, int, string> assertSuccess =
                (r, arg) =>
                {
                    Assert.IsTrue(tryInvoke(r, arg, out string result));
                    return result;
                };

            Invoke_Success        (assertSuccess);
            Invoke_EventualSuccess(assertSuccess);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string>, int> assertFailure =
                (reliableFunc, arg) =>
                {
                    Assert.IsFalse(reliableFunc.TryInvoke(arg, out string result));
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

        private void Invoke_Success(Func<ReliableFunc<int, string>, int, string> assertInvoke)
        {
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Success" ? ResultKind.Successful : ResultKind.Fatal, "Success");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableFunc<int, string> reliableFunc = new ReliableFunc<int, string>(
                (arg) =>
                {
                    AssertDelegateParameters(arg);
                    return "Success";
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.Failed           += (   r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42));

            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Exception<T>(Action<ReliableFunc<int, string>, int> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create<string>(r => ResultKind.Successful);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string> reliableFunc = new ReliableFunc<int, string>(
                (arg) =>
                {
                    AssertDelegateParameters(arg);
                    throw new T();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(0, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Result(Action<ReliableFunc<int, string>, int> assertInvoke, string expectedResult = "Failure")
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Fatal : ResultKind.Successful, expectedResult);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Transient);
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string> reliableFunc = new ReliableFunc<int, string>(
                (arg) =>
                {
                    AssertDelegateParameters(arg);
                    return expectedResult;
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Func<ReliableFunc<int, string>, int, string> assertInvoke)
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Success" ? ResultKind.Successful : ResultKind.Retryable);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls == 1)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual("Success", r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualSuccess = FlakyFunc.Create(1, "Transient", "Success");
            ReliableFunc<int, string> reliableFunc = new ReliableFunc<int, string>(
                (arg) =>
                {
                    AssertDelegateParameters(arg);
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

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42));

            Assert.AreEqual(2, retries);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Exception<TTransient, TFatal>(Action<ReliableFunc<int, string>, int> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<TTransient>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, TFatal>(1, "Transient", transientError: false);
            ReliableFunc<int, string> reliableFunc = new ReliableFunc<int, string>(
                (arg) =>
                {
                    AssertDelegateParameters(arg);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(TFatal), e.GetType());
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42);

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(3      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Result(Action<ReliableFunc<int, string>, int> assertInvoke, string expectedResult = "Failure")
        {
            Assert.AreNotEqual("Transient", expectedResult);

            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

             resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls < 3)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual(expectedResult, r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualFailure = FlakyFunc.Create(2, "Transient", expectedResult);
            ReliableFunc<int, string> reliableFunc = new ReliableFunc<int, string>(
                (arg) =>
                {
                    AssertDelegateParameters(arg);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42);

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(3      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Exception<T>(Action<ReliableFunc<int, string>, int> assertInvoke)
            where T : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<T>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, T>(1, "Transient", transientError: false);
            ReliableFunc<int, string> reliableFunc = new ReliableFunc<int, string>(
                (arg) =>
                {
                    AssertDelegateParameters(arg);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42);

            Assert.AreEqual(4, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(4      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Result(Action<ReliableFunc<int, string>, int> assertInvoke, string expectedResult = "Transient")
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Retryable : ResultKind.Fatal, expectedResult);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            ReliableFunc<int, string> reliableFunc = new ReliableFunc<int, string>(
                (arg) =>
                {
                    AssertDelegateParameters(arg);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42);

            Assert.AreEqual(2, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableFunc<int, string>, int, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>(1, "Transient");
            ReliableFunc<int, string> reliableAction = new ReliableFunc<int, string>(
                (arg) =>
                {
                    AssertDelegateParameters(arg);
                    return flakyFunc();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying +=
                (i, r, e) =>
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, tokenSource.Token), tokenSource.Token);

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

        private static void AssertDelegateParameters(int arg)
        {
            Assert.AreEqual(42, arg);
        }
    }

    #endregion

    #region ReliableFunc<T1, T2, TResult>

    [TestClass]
    public sealed class ReliableFunc2Test : ReliableDelegateTest<string>
    {
        private static readonly Func<ReliableFunc<int, string, string>, Func<int, string, string>> s_getFunc = DynamicGetter.ForField<ReliableFunc<int, string, string>, Func<int, string, string>>("_func");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            DelayPolicy delayPolicy = DelayPolicies.Constant(115);
            Func<int, string, string> func = (arg1, arg2) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, string>(func, -2              , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, string>(func, Retries.Infinite, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, string> actual = new ReliableFunc<int, string, string>(func, 37, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, string> func = (arg1, arg2) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, string>(func, -2              , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, string>(func, Retries.Infinite, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, string> actual = new ReliableFunc<int, string, string>(func, 37, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, ReliableDelegate<string>.DefaultResultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        [TestMethod]
        public void Ctor_ResultPolicy_DelayPolicy()
        {
            Func<int, string, string> func = (arg1, arg2) => "Hello World";
            ResultPolicy<string> resultPolicy = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            DelayPolicy          delayPolicy  = DelayPolicies.Constant(115);

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, string>(func, Retries.Infinite, resultPolicy, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, string> actual = new ReliableFunc<int, string, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, string> func = (arg1, arg2) => "Hello World";
            ResultPolicy<string>       resultPolicy       = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            ComplexDelayPolicy<string> complexDelayPolicy = (i, r, e) => TimeSpan.Zero;

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, string>(func, Retries.Infinite, resultPolicy, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, string> actual = new ReliableFunc<int, string, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        private void Ctor(
            ReliableFunc<int, string, string> reliableFunc,
            Func<int, string, string>         expectedFunc,
            int                        expectedMaxRetries,
            ResultPolicy<string>       expectedResultPolicy,
            ExceptionPolicy            expectedExceptionPolicy,
            ComplexDelayPolicy<string> expectedDelayPolicy)
            => Ctor(reliableFunc, expectedFunc, expectedMaxRetries, expectedResultPolicy, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        private void Ctor(
            ReliableFunc<int, string, string> reliableFunc,
            Func<int, string, string>         expectedFunc,
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
            => Invoke((reliableFunc, arg1, arg2) => reliableFunc.Invoke(arg1, arg2));
        
        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableFunc, arg1, arg2) => reliableFunc.Invoke(arg1, arg2, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, token) => reliableFunc.Invoke(arg1, arg2, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
        {
            TryInvoke(TryInvokeFunc);

            static bool TryInvokeFunc(ReliableFunc<int, string, string> reliableFunc, int arg1, string arg2, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, out result);
        }

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            TryInvoke(TryInvokeFunc);

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, token) => Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, token, out string result)));

            bool TryInvokeFunc(ReliableFunc<int, string, string> reliableFunc, int arg1, string arg2, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, tokenSource.Token, out result);
        }

        private void Invoke(Func<ReliableFunc<int, string, string>, int, string, string> invoke)
        {
            // Success
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, string>, int, string> assertFailureResult =
                (reliableFunc, arg1, arg2) => Assert.AreEqual("Bad Result", reliableFunc.Invoke(arg1, arg2));

            Invoke_Failure_Result         (assertFailureResult, "Bad Result");
            Invoke_EventualFailure_Result (assertFailureResult, "Bad Result");
            Invoke_RetriesExhausted_Result(assertFailureResult, "Bad Result");

            // Failure (Exception)
            Invoke_Failure_Exception         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure_Exception <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted_Exception<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableFunc<int, string, string>, int, string> GetFailureAssertion<T>()
                where T : Exception
            {
                return (r, arg1, arg2) =>
                {
                    try
                    {
                        invoke(r, arg1, arg2);
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

        private void TryInvoke(TryFunc<ReliableFunc<int, string, string>, int, string, string> tryInvoke)
        {
            // Success
            Func<ReliableFunc<int, string, string>, int, string, string> assertSuccess =
                (r, arg1, arg2) =>
                {
                    Assert.IsTrue(tryInvoke(r, arg1, arg2, out string result));
                    return result;
                };

            Invoke_Success        (assertSuccess);
            Invoke_EventualSuccess(assertSuccess);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, string>, int, string> assertFailure =
                (reliableFunc, arg1, arg2) =>
                {
                    Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, out string result));
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

        private void Invoke_Success(Func<ReliableFunc<int, string, string>, int, string, string> assertInvoke)
        {
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Success" ? ResultKind.Successful : ResultKind.Fatal, "Success");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableFunc<int, string, string> reliableFunc = new ReliableFunc<int, string, string>(
                (arg1, arg2) =>
                {
                    AssertDelegateParameters(arg1, arg2);
                    return "Success";
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.Failed           += (   r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo"));

            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Exception<T>(Action<ReliableFunc<int, string, string>, int, string> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create<string>(r => ResultKind.Successful);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, string> reliableFunc = new ReliableFunc<int, string, string>(
                (arg1, arg2) =>
                {
                    AssertDelegateParameters(arg1, arg2);
                    throw new T();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo");

            Assert.AreEqual(1, failures);
            Assert.AreEqual(0, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Result(Action<ReliableFunc<int, string, string>, int, string> assertInvoke, string expectedResult = "Failure")
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Fatal : ResultKind.Successful, expectedResult);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Transient);
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, string> reliableFunc = new ReliableFunc<int, string, string>(
                (arg1, arg2) =>
                {
                    AssertDelegateParameters(arg1, arg2);
                    return expectedResult;
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo");

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Func<ReliableFunc<int, string, string>, int, string, string> assertInvoke)
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Success" ? ResultKind.Successful : ResultKind.Retryable);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls == 1)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual("Success", r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualSuccess = FlakyFunc.Create(1, "Transient", "Success");
            ReliableFunc<int, string, string> reliableFunc = new ReliableFunc<int, string, string>(
                (arg1, arg2) =>
                {
                    AssertDelegateParameters(arg1, arg2);
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

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo"));

            Assert.AreEqual(2, retries);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Exception<TTransient, TFatal>(Action<ReliableFunc<int, string, string>, int, string> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<TTransient>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, TFatal>(1, "Transient", transientError: false);
            ReliableFunc<int, string, string> reliableFunc = new ReliableFunc<int, string, string>(
                (arg1, arg2) =>
                {
                    AssertDelegateParameters(arg1, arg2);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(TFatal), e.GetType());
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo");

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(3      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Result(Action<ReliableFunc<int, string, string>, int, string> assertInvoke, string expectedResult = "Failure")
        {
            Assert.AreNotEqual("Transient", expectedResult);

            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

             resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls < 3)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual(expectedResult, r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualFailure = FlakyFunc.Create(2, "Transient", expectedResult);
            ReliableFunc<int, string, string> reliableFunc = new ReliableFunc<int, string, string>(
                (arg1, arg2) =>
                {
                    AssertDelegateParameters(arg1, arg2);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo");

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(3      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Exception<T>(Action<ReliableFunc<int, string, string>, int, string> assertInvoke)
            where T : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<T>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, T>(1, "Transient", transientError: false);
            ReliableFunc<int, string, string> reliableFunc = new ReliableFunc<int, string, string>(
                (arg1, arg2) =>
                {
                    AssertDelegateParameters(arg1, arg2);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo");

            Assert.AreEqual(4, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(4      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Result(Action<ReliableFunc<int, string, string>, int, string> assertInvoke, string expectedResult = "Transient")
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Retryable : ResultKind.Fatal, expectedResult);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            ReliableFunc<int, string, string> reliableFunc = new ReliableFunc<int, string, string>(
                (arg1, arg2) =>
                {
                    AssertDelegateParameters(arg1, arg2);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo");

            Assert.AreEqual(2, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableFunc<int, string, string>, int, string, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>(1, "Transient");
            ReliableFunc<int, string, string> reliableAction = new ReliableFunc<int, string, string>(
                (arg1, arg2) =>
                {
                    AssertDelegateParameters(arg1, arg2);
                    return flakyFunc();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying +=
                (i, r, e) =>
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", tokenSource.Token), tokenSource.Token);

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

        private static void AssertDelegateParameters(int arg1, string arg2)
        {
            Assert.AreEqual(42   , arg1);
            Assert.AreEqual("foo", arg2);
        }
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, TResult>

    [TestClass]
    public sealed class ReliableFunc3Test : ReliableDelegateTest<string>
    {
        private static readonly Func<ReliableFunc<int, string, double, string>, Func<int, string, double, string>> s_getFunc = DynamicGetter.ForField<ReliableFunc<int, string, double, string>, Func<int, string, double, string>>("_func");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            DelayPolicy delayPolicy = DelayPolicies.Constant(115);
            Func<int, string, double, string> func = (arg1, arg2, arg3) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, string>(func, -2              , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, string>(func, Retries.Infinite, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, string> actual = new ReliableFunc<int, string, double, string>(func, 37, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, string> func = (arg1, arg2, arg3) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, string>(func, -2              , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, string>(func, Retries.Infinite, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, string> actual = new ReliableFunc<int, string, double, string>(func, 37, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, ReliableDelegate<string>.DefaultResultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        [TestMethod]
        public void Ctor_ResultPolicy_DelayPolicy()
        {
            Func<int, string, double, string> func = (arg1, arg2, arg3) => "Hello World";
            ResultPolicy<string> resultPolicy = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            DelayPolicy          delayPolicy  = DelayPolicies.Constant(115);

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, string>(func, Retries.Infinite, resultPolicy, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, string> actual = new ReliableFunc<int, string, double, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, string> func = (arg1, arg2, arg3) => "Hello World";
            ResultPolicy<string>       resultPolicy       = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            ComplexDelayPolicy<string> complexDelayPolicy = (i, r, e) => TimeSpan.Zero;

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, string>(func, Retries.Infinite, resultPolicy, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, string> actual = new ReliableFunc<int, string, double, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        private void Ctor(
            ReliableFunc<int, string, double, string> reliableFunc,
            Func<int, string, double, string>         expectedFunc,
            int                        expectedMaxRetries,
            ResultPolicy<string>       expectedResultPolicy,
            ExceptionPolicy            expectedExceptionPolicy,
            ComplexDelayPolicy<string> expectedDelayPolicy)
            => Ctor(reliableFunc, expectedFunc, expectedMaxRetries, expectedResultPolicy, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        private void Ctor(
            ReliableFunc<int, string, double, string> reliableFunc,
            Func<int, string, double, string>         expectedFunc,
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
            => Invoke((reliableFunc, arg1, arg2, arg3) => reliableFunc.Invoke(arg1, arg2, arg3));
        
        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableFunc, arg1, arg2, arg3) => reliableFunc.Invoke(arg1, arg2, arg3, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, token) => reliableFunc.Invoke(arg1, arg2, arg3, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
        {
            TryInvoke(TryInvokeFunc);

            static bool TryInvokeFunc(ReliableFunc<int, string, double, string> reliableFunc, int arg1, string arg2, double arg3, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, out result);
        }

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            TryInvoke(TryInvokeFunc);

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, token) => Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, token, out string result)));

            bool TryInvokeFunc(ReliableFunc<int, string, double, string> reliableFunc, int arg1, string arg2, double arg3, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, tokenSource.Token, out result);
        }

        private void Invoke(Func<ReliableFunc<int, string, double, string>, int, string, double, string> invoke)
        {
            // Success
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, string>, int, string, double> assertFailureResult =
                (reliableFunc, arg1, arg2, arg3) => Assert.AreEqual("Bad Result", reliableFunc.Invoke(arg1, arg2, arg3));

            Invoke_Failure_Result         (assertFailureResult, "Bad Result");
            Invoke_EventualFailure_Result (assertFailureResult, "Bad Result");
            Invoke_RetriesExhausted_Result(assertFailureResult, "Bad Result");

            // Failure (Exception)
            Invoke_Failure_Exception         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure_Exception <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted_Exception<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableFunc<int, string, double, string>, int, string, double> GetFailureAssertion<T>()
                where T : Exception
            {
                return (r, arg1, arg2, arg3) =>
                {
                    try
                    {
                        invoke(r, arg1, arg2, arg3);
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

        private void TryInvoke(TryFunc<ReliableFunc<int, string, double, string>, int, string, double, string> tryInvoke)
        {
            // Success
            Func<ReliableFunc<int, string, double, string>, int, string, double, string> assertSuccess =
                (r, arg1, arg2, arg3) =>
                {
                    Assert.IsTrue(tryInvoke(r, arg1, arg2, arg3, out string result));
                    return result;
                };

            Invoke_Success        (assertSuccess);
            Invoke_EventualSuccess(assertSuccess);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, string>, int, string, double> assertFailure =
                (reliableFunc, arg1, arg2, arg3) =>
                {
                    Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, out string result));
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

        private void Invoke_Success(Func<ReliableFunc<int, string, double, string>, int, string, double, string> assertInvoke)
        {
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Success" ? ResultKind.Successful : ResultKind.Fatal, "Success");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableFunc<int, string, double, string> reliableFunc = new ReliableFunc<int, string, double, string>(
                (arg1, arg2, arg3) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3);
                    return "Success";
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.Failed           += (   r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D));

            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Exception<T>(Action<ReliableFunc<int, string, double, string>, int, string, double> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create<string>(r => ResultKind.Successful);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, string> reliableFunc = new ReliableFunc<int, string, double, string>(
                (arg1, arg2, arg3) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3);
                    throw new T();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(0, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Result(Action<ReliableFunc<int, string, double, string>, int, string, double> assertInvoke, string expectedResult = "Failure")
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Fatal : ResultKind.Successful, expectedResult);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Transient);
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, string> reliableFunc = new ReliableFunc<int, string, double, string>(
                (arg1, arg2, arg3) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3);
                    return expectedResult;
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Func<ReliableFunc<int, string, double, string>, int, string, double, string> assertInvoke)
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Success" ? ResultKind.Successful : ResultKind.Retryable);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls == 1)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual("Success", r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualSuccess = FlakyFunc.Create(1, "Transient", "Success");
            ReliableFunc<int, string, double, string> reliableFunc = new ReliableFunc<int, string, double, string>(
                (arg1, arg2, arg3) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3);
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

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D));

            Assert.AreEqual(2, retries);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Exception<TTransient, TFatal>(Action<ReliableFunc<int, string, double, string>, int, string, double> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<TTransient>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, TFatal>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, string> reliableFunc = new ReliableFunc<int, string, double, string>(
                (arg1, arg2, arg3) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(TFatal), e.GetType());
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D);

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(3      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Result(Action<ReliableFunc<int, string, double, string>, int, string, double> assertInvoke, string expectedResult = "Failure")
        {
            Assert.AreNotEqual("Transient", expectedResult);

            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

             resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls < 3)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual(expectedResult, r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualFailure = FlakyFunc.Create(2, "Transient", expectedResult);
            ReliableFunc<int, string, double, string> reliableFunc = new ReliableFunc<int, string, double, string>(
                (arg1, arg2, arg3) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D);

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(3      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Exception<T>(Action<ReliableFunc<int, string, double, string>, int, string, double> assertInvoke)
            where T : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<T>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, T>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, string> reliableFunc = new ReliableFunc<int, string, double, string>(
                (arg1, arg2, arg3) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D);

            Assert.AreEqual(4, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(4      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Result(Action<ReliableFunc<int, string, double, string>, int, string, double> assertInvoke, string expectedResult = "Transient")
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Retryable : ResultKind.Fatal, expectedResult);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            ReliableFunc<int, string, double, string> reliableFunc = new ReliableFunc<int, string, double, string>(
                (arg1, arg2, arg3) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D);

            Assert.AreEqual(2, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableFunc<int, string, double, string>, int, string, double, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>(1, "Transient");
            ReliableFunc<int, string, double, string> reliableAction = new ReliableFunc<int, string, double, string>(
                (arg1, arg2, arg3) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3);
                    return flakyFunc();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying +=
                (i, r, e) =>
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, tokenSource.Token), tokenSource.Token);

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

        private static void AssertDelegateParameters(int arg1, string arg2, double arg3)
        {
            Assert.AreEqual(42   , arg1);
            Assert.AreEqual("foo", arg2);
            Assert.AreEqual(3.14D, arg3);
        }
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, TResult>

    [TestClass]
    public sealed class ReliableFunc4Test : ReliableDelegateTest<string>
    {
        private static readonly Func<ReliableFunc<int, string, double, long, string>, Func<int, string, double, long, string>> s_getFunc = DynamicGetter.ForField<ReliableFunc<int, string, double, long, string>, Func<int, string, double, long, string>>("_func");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            DelayPolicy delayPolicy = DelayPolicies.Constant(115);
            Func<int, string, double, long, string> func = (arg1, arg2, arg3, arg4) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, string>(func, -2              , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, string>(func, Retries.Infinite, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, string> actual = new ReliableFunc<int, string, double, long, string>(func, 37, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, long, string> func = (arg1, arg2, arg3, arg4) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, string>(func, -2              , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, string>(func, Retries.Infinite, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, string> actual = new ReliableFunc<int, string, double, long, string>(func, 37, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, ReliableDelegate<string>.DefaultResultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        [TestMethod]
        public void Ctor_ResultPolicy_DelayPolicy()
        {
            Func<int, string, double, long, string> func = (arg1, arg2, arg3, arg4) => "Hello World";
            ResultPolicy<string> resultPolicy = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            DelayPolicy          delayPolicy  = DelayPolicies.Constant(115);

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, string>(func, Retries.Infinite, resultPolicy, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, string> actual = new ReliableFunc<int, string, double, long, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, long, string> func = (arg1, arg2, arg3, arg4) => "Hello World";
            ResultPolicy<string>       resultPolicy       = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            ComplexDelayPolicy<string> complexDelayPolicy = (i, r, e) => TimeSpan.Zero;

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, string>(func, Retries.Infinite, resultPolicy, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, string> actual = new ReliableFunc<int, string, double, long, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        private void Ctor(
            ReliableFunc<int, string, double, long, string> reliableFunc,
            Func<int, string, double, long, string>         expectedFunc,
            int                        expectedMaxRetries,
            ResultPolicy<string>       expectedResultPolicy,
            ExceptionPolicy            expectedExceptionPolicy,
            ComplexDelayPolicy<string> expectedDelayPolicy)
            => Ctor(reliableFunc, expectedFunc, expectedMaxRetries, expectedResultPolicy, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        private void Ctor(
            ReliableFunc<int, string, double, long, string> reliableFunc,
            Func<int, string, double, long, string>         expectedFunc,
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
            => Invoke((reliableFunc, arg1, arg2, arg3, arg4) => reliableFunc.Invoke(arg1, arg2, arg3, arg4));
        
        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableFunc, arg1, arg2, arg3, arg4) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, token) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
        {
            TryInvoke(TryInvokeFunc);

            static bool TryInvokeFunc(ReliableFunc<int, string, double, long, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, out result);
        }

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            TryInvoke(TryInvokeFunc);

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, token) => Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, token, out string result)));

            bool TryInvokeFunc(ReliableFunc<int, string, double, long, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, tokenSource.Token, out result);
        }

        private void Invoke(Func<ReliableFunc<int, string, double, long, string>, int, string, double, long, string> invoke)
        {
            // Success
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, string>, int, string, double, long> assertFailureResult =
                (reliableFunc, arg1, arg2, arg3, arg4) => Assert.AreEqual("Bad Result", reliableFunc.Invoke(arg1, arg2, arg3, arg4));

            Invoke_Failure_Result         (assertFailureResult, "Bad Result");
            Invoke_EventualFailure_Result (assertFailureResult, "Bad Result");
            Invoke_RetriesExhausted_Result(assertFailureResult, "Bad Result");

            // Failure (Exception)
            Invoke_Failure_Exception         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure_Exception <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted_Exception<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableFunc<int, string, double, long, string>, int, string, double, long> GetFailureAssertion<T>()
                where T : Exception
            {
                return (r, arg1, arg2, arg3, arg4) =>
                {
                    try
                    {
                        invoke(r, arg1, arg2, arg3, arg4);
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

        private void TryInvoke(TryFunc<ReliableFunc<int, string, double, long, string>, int, string, double, long, string> tryInvoke)
        {
            // Success
            Func<ReliableFunc<int, string, double, long, string>, int, string, double, long, string> assertSuccess =
                (r, arg1, arg2, arg3, arg4) =>
                {
                    Assert.IsTrue(tryInvoke(r, arg1, arg2, arg3, arg4, out string result));
                    return result;
                };

            Invoke_Success        (assertSuccess);
            Invoke_EventualSuccess(assertSuccess);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, string>, int, string, double, long> assertFailure =
                (reliableFunc, arg1, arg2, arg3, arg4) =>
                {
                    Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, out string result));
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

        private void Invoke_Success(Func<ReliableFunc<int, string, double, long, string>, int, string, double, long, string> assertInvoke)
        {
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Success" ? ResultKind.Successful : ResultKind.Fatal, "Success");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableFunc<int, string, double, long, string> reliableFunc = new ReliableFunc<int, string, double, long, string>(
                (arg1, arg2, arg3, arg4) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4);
                    return "Success";
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.Failed           += (   r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L));

            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Exception<T>(Action<ReliableFunc<int, string, double, long, string>, int, string, double, long> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create<string>(r => ResultKind.Successful);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, string> reliableFunc = new ReliableFunc<int, string, double, long, string>(
                (arg1, arg2, arg3, arg4) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4);
                    throw new T();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(0, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Result(Action<ReliableFunc<int, string, double, long, string>, int, string, double, long> assertInvoke, string expectedResult = "Failure")
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Fatal : ResultKind.Successful, expectedResult);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Transient);
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, string> reliableFunc = new ReliableFunc<int, string, double, long, string>(
                (arg1, arg2, arg3, arg4) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4);
                    return expectedResult;
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Func<ReliableFunc<int, string, double, long, string>, int, string, double, long, string> assertInvoke)
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Success" ? ResultKind.Successful : ResultKind.Retryable);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls == 1)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual("Success", r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualSuccess = FlakyFunc.Create(1, "Transient", "Success");
            ReliableFunc<int, string, double, long, string> reliableFunc = new ReliableFunc<int, string, double, long, string>(
                (arg1, arg2, arg3, arg4) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4);
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

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L));

            Assert.AreEqual(2, retries);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Exception<TTransient, TFatal>(Action<ReliableFunc<int, string, double, long, string>, int, string, double, long> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<TTransient>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, TFatal>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, string> reliableFunc = new ReliableFunc<int, string, double, long, string>(
                (arg1, arg2, arg3, arg4) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(TFatal), e.GetType());
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L);

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(3      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Result(Action<ReliableFunc<int, string, double, long, string>, int, string, double, long> assertInvoke, string expectedResult = "Failure")
        {
            Assert.AreNotEqual("Transient", expectedResult);

            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

             resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls < 3)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual(expectedResult, r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualFailure = FlakyFunc.Create(2, "Transient", expectedResult);
            ReliableFunc<int, string, double, long, string> reliableFunc = new ReliableFunc<int, string, double, long, string>(
                (arg1, arg2, arg3, arg4) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L);

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(3      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Exception<T>(Action<ReliableFunc<int, string, double, long, string>, int, string, double, long> assertInvoke)
            where T : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<T>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, T>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, string> reliableFunc = new ReliableFunc<int, string, double, long, string>(
                (arg1, arg2, arg3, arg4) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L);

            Assert.AreEqual(4, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(4      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Result(Action<ReliableFunc<int, string, double, long, string>, int, string, double, long> assertInvoke, string expectedResult = "Transient")
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Retryable : ResultKind.Fatal, expectedResult);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            ReliableFunc<int, string, double, long, string> reliableFunc = new ReliableFunc<int, string, double, long, string>(
                (arg1, arg2, arg3, arg4) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L);

            Assert.AreEqual(2, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableFunc<int, string, double, long, string>, int, string, double, long, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>(1, "Transient");
            ReliableFunc<int, string, double, long, string> reliableAction = new ReliableFunc<int, string, double, long, string>(
                (arg1, arg2, arg3, arg4) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4);
                    return flakyFunc();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying +=
                (i, r, e) =>
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, tokenSource.Token), tokenSource.Token);

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

        private static void AssertDelegateParameters(int arg1, string arg2, double arg3, long arg4)
        {
            Assert.AreEqual(42   , arg1);
            Assert.AreEqual("foo", arg2);
            Assert.AreEqual(3.14D, arg3);
            Assert.AreEqual(1000L, arg4);
        }
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, T5, TResult>

    [TestClass]
    public sealed class ReliableFunc5Test : ReliableDelegateTest<string>
    {
        private static readonly Func<ReliableFunc<int, string, double, long, ushort, string>, Func<int, string, double, long, ushort, string>> s_getFunc = DynamicGetter.ForField<ReliableFunc<int, string, double, long, ushort, string>, Func<int, string, double, long, ushort, string>>("_func");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            DelayPolicy delayPolicy = DelayPolicies.Constant(115);
            Func<int, string, double, long, ushort, string> func = (arg1, arg2, arg3, arg4, arg5) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, string>(func, -2              , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, string>(func, Retries.Infinite, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, ushort, string> actual = new ReliableFunc<int, string, double, long, ushort, string>(func, 37, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, long, ushort, string> func = (arg1, arg2, arg3, arg4, arg5) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, string>(func, -2              , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, string>(func, Retries.Infinite, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, ushort, string> actual = new ReliableFunc<int, string, double, long, ushort, string>(func, 37, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, ReliableDelegate<string>.DefaultResultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        [TestMethod]
        public void Ctor_ResultPolicy_DelayPolicy()
        {
            Func<int, string, double, long, ushort, string> func = (arg1, arg2, arg3, arg4, arg5) => "Hello World";
            ResultPolicy<string> resultPolicy = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            DelayPolicy          delayPolicy  = DelayPolicies.Constant(115);

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, string>(func, Retries.Infinite, resultPolicy, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, ushort, string> actual = new ReliableFunc<int, string, double, long, ushort, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, long, ushort, string> func = (arg1, arg2, arg3, arg4, arg5) => "Hello World";
            ResultPolicy<string>       resultPolicy       = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            ComplexDelayPolicy<string> complexDelayPolicy = (i, r, e) => TimeSpan.Zero;

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, string>(func, Retries.Infinite, resultPolicy, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, ushort, string> actual = new ReliableFunc<int, string, double, long, ushort, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        private void Ctor(
            ReliableFunc<int, string, double, long, ushort, string> reliableFunc,
            Func<int, string, double, long, ushort, string>         expectedFunc,
            int                        expectedMaxRetries,
            ResultPolicy<string>       expectedResultPolicy,
            ExceptionPolicy            expectedExceptionPolicy,
            ComplexDelayPolicy<string> expectedDelayPolicy)
            => Ctor(reliableFunc, expectedFunc, expectedMaxRetries, expectedResultPolicy, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        private void Ctor(
            ReliableFunc<int, string, double, long, ushort, string> reliableFunc,
            Func<int, string, double, long, ushort, string>         expectedFunc,
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
            => Invoke((reliableFunc, arg1, arg2, arg3, arg4, arg5) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5));
        
        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableFunc, arg1, arg2, arg3, arg4, arg5) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, arg5, token) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
        {
            TryInvoke(TryInvokeFunc);

            static bool TryInvokeFunc(ReliableFunc<int, string, double, long, ushort, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, ushort arg5, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, out result);
        }

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            TryInvoke(TryInvokeFunc);

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, arg5, token) => Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, token, out string result)));

            bool TryInvokeFunc(ReliableFunc<int, string, double, long, ushort, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, ushort arg5, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, tokenSource.Token, out result);
        }

        private void Invoke(Func<ReliableFunc<int, string, double, long, ushort, string>, int, string, double, long, ushort, string> invoke)
        {
            // Success
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, string>, int, string, double, long, ushort> assertFailureResult =
                (reliableFunc, arg1, arg2, arg3, arg4, arg5) => Assert.AreEqual("Bad Result", reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5));

            Invoke_Failure_Result         (assertFailureResult, "Bad Result");
            Invoke_EventualFailure_Result (assertFailureResult, "Bad Result");
            Invoke_RetriesExhausted_Result(assertFailureResult, "Bad Result");

            // Failure (Exception)
            Invoke_Failure_Exception         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure_Exception <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted_Exception<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableFunc<int, string, double, long, ushort, string>, int, string, double, long, ushort> GetFailureAssertion<T>()
                where T : Exception
            {
                return (r, arg1, arg2, arg3, arg4, arg5) =>
                {
                    try
                    {
                        invoke(r, arg1, arg2, arg3, arg4, arg5);
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

        private void TryInvoke(TryFunc<ReliableFunc<int, string, double, long, ushort, string>, int, string, double, long, ushort, string> tryInvoke)
        {
            // Success
            Func<ReliableFunc<int, string, double, long, ushort, string>, int, string, double, long, ushort, string> assertSuccess =
                (r, arg1, arg2, arg3, arg4, arg5) =>
                {
                    Assert.IsTrue(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, out string result));
                    return result;
                };

            Invoke_Success        (assertSuccess);
            Invoke_EventualSuccess(assertSuccess);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, string>, int, string, double, long, ushort> assertFailure =
                (reliableFunc, arg1, arg2, arg3, arg4, arg5) =>
                {
                    Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, out string result));
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

        private void Invoke_Success(Func<ReliableFunc<int, string, double, long, ushort, string>, int, string, double, long, ushort, string> assertInvoke)
        {
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Success" ? ResultKind.Successful : ResultKind.Fatal, "Success");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableFunc<int, string, double, long, ushort, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, string>(
                (arg1, arg2, arg3, arg4, arg5) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5);
                    return "Success";
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.Failed           += (   r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1));

            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Exception<T>(Action<ReliableFunc<int, string, double, long, ushort, string>, int, string, double, long, ushort> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create<string>(r => ResultKind.Successful);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, ushort, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, string>(
                (arg1, arg2, arg3, arg4, arg5) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5);
                    throw new T();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(0, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Result(Action<ReliableFunc<int, string, double, long, ushort, string>, int, string, double, long, ushort> assertInvoke, string expectedResult = "Failure")
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Fatal : ResultKind.Successful, expectedResult);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Transient);
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, ushort, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, string>(
                (arg1, arg2, arg3, arg4, arg5) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5);
                    return expectedResult;
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Func<ReliableFunc<int, string, double, long, ushort, string>, int, string, double, long, ushort, string> assertInvoke)
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Success" ? ResultKind.Successful : ResultKind.Retryable);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls == 1)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual("Success", r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualSuccess = FlakyFunc.Create(1, "Transient", "Success");
            ReliableFunc<int, string, double, long, ushort, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, string>(
                (arg1, arg2, arg3, arg4, arg5) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5);
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

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1));

            Assert.AreEqual(2, retries);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Exception<TTransient, TFatal>(Action<ReliableFunc<int, string, double, long, ushort, string>, int, string, double, long, ushort> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<TTransient>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, TFatal>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, ushort, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, string>(
                (arg1, arg2, arg3, arg4, arg5) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(TFatal), e.GetType());
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1);

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(3      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Result(Action<ReliableFunc<int, string, double, long, ushort, string>, int, string, double, long, ushort> assertInvoke, string expectedResult = "Failure")
        {
            Assert.AreNotEqual("Transient", expectedResult);

            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

             resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls < 3)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual(expectedResult, r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualFailure = FlakyFunc.Create(2, "Transient", expectedResult);
            ReliableFunc<int, string, double, long, ushort, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, string>(
                (arg1, arg2, arg3, arg4, arg5) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1);

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(3      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Exception<T>(Action<ReliableFunc<int, string, double, long, ushort, string>, int, string, double, long, ushort> assertInvoke)
            where T : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<T>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, T>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, ushort, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, string>(
                (arg1, arg2, arg3, arg4, arg5) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1);

            Assert.AreEqual(4, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(4      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Result(Action<ReliableFunc<int, string, double, long, ushort, string>, int, string, double, long, ushort> assertInvoke, string expectedResult = "Transient")
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Retryable : ResultKind.Fatal, expectedResult);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            ReliableFunc<int, string, double, long, ushort, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, string>(
                (arg1, arg2, arg3, arg4, arg5) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1);

            Assert.AreEqual(2, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableFunc<int, string, double, long, ushort, string>, int, string, double, long, ushort, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>(1, "Transient");
            ReliableFunc<int, string, double, long, ushort, string> reliableAction = new ReliableFunc<int, string, double, long, ushort, string>(
                (arg1, arg2, arg3, arg4, arg5) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5);
                    return flakyFunc();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying +=
                (i, r, e) =>
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, tokenSource.Token), tokenSource.Token);

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

        private static void AssertDelegateParameters(int arg1, string arg2, double arg3, long arg4, ushort arg5)
        {
            Assert.AreEqual(42       , arg1);
            Assert.AreEqual("foo"    , arg2);
            Assert.AreEqual(3.14D    , arg3);
            Assert.AreEqual(1000L    , arg4);
            Assert.AreEqual((ushort)1, arg5);
        }
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, T5, T6, TResult>

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
            Action<ReliableFunc<int, string, double, long, ushort, byte, string>, int, string, double, long, ushort, byte> assertFailureResult =
                (reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6) => Assert.AreEqual("Bad Result", reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6));

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
            Func<ReliableFunc<int, string, double, long, ushort, byte, string>, int, string, double, long, ushort, byte, string> assertSuccess =
                (r, arg1, arg2, arg3, arg4, arg5, arg6) =>
                {
                    Assert.IsTrue(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, out string result));
                    return result;
                };

            Invoke_Success        (assertSuccess);
            Invoke_EventualSuccess(assertSuccess);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, byte, string>, int, string, double, long, ushort, byte> assertFailure =
                (reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6) =>
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
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Success" ? ResultKind.Successful : ResultKind.Fatal, "Success");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

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
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create<string>(r => ResultKind.Successful);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

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
            reliableFunc.Failed           +=
                (r, e) =>
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
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Fatal : ResultKind.Successful, expectedResult);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Transient);
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

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
            reliableFunc.Failed           +=
                (r, e) =>
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
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Success" ? ResultKind.Successful : ResultKind.Retryable);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            resultPolicy.Invoking +=
                r =>
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
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

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
            reliableFunc.Failed +=
                (r, e) =>
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
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

             resultPolicy.Invoking +=
                r =>
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
            reliableFunc.Failed +=
                (r, e) =>
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
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
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
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Retryable : ResultKind.Fatal, expectedResult);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
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

            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

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
            reliableAction.Retrying +=
                (i, r, e) =>
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

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, T5, T6, T7, TResult>

    [TestClass]
    public sealed class ReliableFunc7Test : ReliableDelegateTest<string>
    {
        private static readonly Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>, Func<int, string, double, long, ushort, byte, TimeSpan, string>> s_getFunc = DynamicGetter.ForField<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>, Func<int, string, double, long, ushort, byte, TimeSpan, string>>("_func");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            DelayPolicy delayPolicy = DelayPolicies.Constant(115);
            Func<int, string, double, long, ushort, byte, TimeSpan, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(func, -2              , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(func, Retries.Infinite, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(func, 37, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, long, ushort, byte, TimeSpan, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(func, -2              , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(func, Retries.Infinite, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(func, 37, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, ReliableDelegate<string>.DefaultResultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        [TestMethod]
        public void Ctor_ResultPolicy_DelayPolicy()
        {
            Func<int, string, double, long, ushort, byte, TimeSpan, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => "Hello World";
            ResultPolicy<string> resultPolicy = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            DelayPolicy          delayPolicy  = DelayPolicies.Constant(115);

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(func, Retries.Infinite, resultPolicy, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, long, ushort, byte, TimeSpan, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => "Hello World";
            ResultPolicy<string>       resultPolicy       = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            ComplexDelayPolicy<string> complexDelayPolicy = (i, r, e) => TimeSpan.Zero;

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(func, Retries.Infinite, resultPolicy, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        private void Ctor(
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string> reliableFunc,
            Func<int, string, double, long, ushort, byte, TimeSpan, string>         expectedFunc,
            int                        expectedMaxRetries,
            ResultPolicy<string>       expectedResultPolicy,
            ExceptionPolicy            expectedExceptionPolicy,
            ComplexDelayPolicy<string> expectedDelayPolicy)
            => Ctor(reliableFunc, expectedFunc, expectedMaxRetries, expectedResultPolicy, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        private void Ctor(
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string> reliableFunc,
            Func<int, string, double, long, ushort, byte, TimeSpan, string>         expectedFunc,
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
            => Invoke((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        
        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, token) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
        {
            TryInvoke(TryInvokeFunc);

            static bool TryInvokeFunc(ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, out result);
        }

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            TryInvoke(TryInvokeFunc);

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, token) => Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, token, out string result)));

            bool TryInvokeFunc(ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, tokenSource.Token, out result);
        }

        private void Invoke(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>, int, string, double, long, ushort, byte, TimeSpan, string> invoke)
        {
            // Success
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>, int, string, double, long, ushort, byte, TimeSpan> assertFailureResult =
                (reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Assert.AreEqual("Bad Result", reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7));

            Invoke_Failure_Result         (assertFailureResult, "Bad Result");
            Invoke_EventualFailure_Result (assertFailureResult, "Bad Result");
            Invoke_RetriesExhausted_Result(assertFailureResult, "Bad Result");

            // Failure (Exception)
            Invoke_Failure_Exception         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure_Exception <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted_Exception<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>, int, string, double, long, ushort, byte, TimeSpan> GetFailureAssertion<T>()
                where T : Exception
            {
                return (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                {
                    try
                    {
                        invoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
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

        private void TryInvoke(TryFunc<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>, int, string, double, long, ushort, byte, TimeSpan, string> tryInvoke)
        {
            // Success
            Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>, int, string, double, long, ushort, byte, TimeSpan, string> assertSuccess =
                (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                {
                    Assert.IsTrue(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, out string result));
                    return result;
                };

            Invoke_Success        (assertSuccess);
            Invoke_EventualSuccess(assertSuccess);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>, int, string, double, long, ushort, byte, TimeSpan> assertFailure =
                (reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                {
                    Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, out string result));
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

        private void Invoke_Success(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>, int, string, double, long, ushort, byte, TimeSpan, string> assertInvoke)
        {
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Success" ? ResultKind.Successful : ResultKind.Fatal, "Success");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                    return "Success";
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.Failed           += (   r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30)));

            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Exception<T>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>, int, string, double, long, ushort, byte, TimeSpan> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create<string>(r => ResultKind.Successful);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                    throw new T();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30));

            Assert.AreEqual(1, failures);
            Assert.AreEqual(0, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>, int, string, double, long, ushort, byte, TimeSpan> assertInvoke, string expectedResult = "Failure")
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Fatal : ResultKind.Successful, expectedResult);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Transient);
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                    return expectedResult;
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30));

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>, int, string, double, long, ushort, byte, TimeSpan, string> assertInvoke)
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Success" ? ResultKind.Successful : ResultKind.Retryable);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls == 1)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual("Success", r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualSuccess = FlakyFunc.Create(1, "Transient", "Success");
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
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

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30)));

            Assert.AreEqual(2, retries);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Exception<TTransient, TFatal>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>, int, string, double, long, ushort, byte, TimeSpan> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<TTransient>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, TFatal>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(TFatal), e.GetType());
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30));

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(3      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>, int, string, double, long, ushort, byte, TimeSpan> assertInvoke, string expectedResult = "Failure")
        {
            Assert.AreNotEqual("Transient", expectedResult);

            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

             resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls < 3)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual(expectedResult, r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualFailure = FlakyFunc.Create(2, "Transient", expectedResult);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30));

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(3      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Exception<T>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>, int, string, double, long, ushort, byte, TimeSpan> assertInvoke)
            where T : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<T>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, T>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30));

            Assert.AreEqual(4, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(4      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>, int, string, double, long, ushort, byte, TimeSpan> assertInvoke, string expectedResult = "Transient")
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Retryable : ResultKind.Fatal, expectedResult);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30));

            Assert.AreEqual(2, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>, int, string, double, long, ushort, byte, TimeSpan, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>(1, "Transient");
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string> reliableAction = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                    return flakyFunc();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying +=
                (i, r, e) =>
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), tokenSource.Token), tokenSource.Token);

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

        private static void AssertDelegateParameters(int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7)
        {
            Assert.AreEqual(42                   , arg1);
            Assert.AreEqual("foo"                , arg2);
            Assert.AreEqual(3.14D                , arg3);
            Assert.AreEqual(1000L                , arg4);
            Assert.AreEqual((ushort)1            , arg5);
            Assert.AreEqual((byte)255            , arg6);
            Assert.AreEqual(TimeSpan.FromDays(30), arg7);
        }
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult>

    [TestClass]
    public sealed class ReliableFunc8Test : ReliableDelegateTest<string>
    {
        private static readonly Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>, Func<int, string, double, long, ushort, byte, TimeSpan, uint, string>> s_getFunc = DynamicGetter.ForField<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>, Func<int, string, double, long, ushort, byte, TimeSpan, uint, string>>("_func");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            DelayPolicy delayPolicy = DelayPolicies.Constant(115);
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(func, -2              , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(func, Retries.Infinite, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(func, 37, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(func, -2              , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(func, Retries.Infinite, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(func, 37, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, ReliableDelegate<string>.DefaultResultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        [TestMethod]
        public void Ctor_ResultPolicy_DelayPolicy()
        {
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => "Hello World";
            ResultPolicy<string> resultPolicy = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            DelayPolicy          delayPolicy  = DelayPolicies.Constant(115);

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(func, Retries.Infinite, resultPolicy, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => "Hello World";
            ResultPolicy<string>       resultPolicy       = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            ComplexDelayPolicy<string> complexDelayPolicy = (i, r, e) => TimeSpan.Zero;

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(func, Retries.Infinite, resultPolicy, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        private void Ctor(
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string> reliableFunc,
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, string>         expectedFunc,
            int                        expectedMaxRetries,
            ResultPolicy<string>       expectedResultPolicy,
            ExceptionPolicy            expectedExceptionPolicy,
            ComplexDelayPolicy<string> expectedDelayPolicy)
            => Ctor(reliableFunc, expectedFunc, expectedMaxRetries, expectedResultPolicy, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        private void Ctor(
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string> reliableFunc,
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, string>         expectedFunc,
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
            => Invoke((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        
        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
        {
            TryInvoke(TryInvokeFunc);

            static bool TryInvokeFunc(ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, out result);
        }

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            TryInvoke(TryInvokeFunc);

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token, out string result)));

            bool TryInvokeFunc(ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, tokenSource.Token, out result);
        }

        private void Invoke(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>, int, string, double, long, ushort, byte, TimeSpan, uint, string> invoke)
        {
            // Success
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>, int, string, double, long, ushort, byte, TimeSpan, uint> assertFailureResult =
                (reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Assert.AreEqual("Bad Result", reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));

            Invoke_Failure_Result         (assertFailureResult, "Bad Result");
            Invoke_EventualFailure_Result (assertFailureResult, "Bad Result");
            Invoke_RetriesExhausted_Result(assertFailureResult, "Bad Result");

            // Failure (Exception)
            Invoke_Failure_Exception         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure_Exception <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted_Exception<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>, int, string, double, long, ushort, byte, TimeSpan, uint> GetFailureAssertion<T>()
                where T : Exception
            {
                return (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                {
                    try
                    {
                        invoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
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

        private void TryInvoke(TryFunc<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>, int, string, double, long, ushort, byte, TimeSpan, uint, string> tryInvoke)
        {
            // Success
            Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>, int, string, double, long, ushort, byte, TimeSpan, uint, string> assertSuccess =
                (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                {
                    Assert.IsTrue(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, out string result));
                    return result;
                };

            Invoke_Success        (assertSuccess);
            Invoke_EventualSuccess(assertSuccess);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>, int, string, double, long, ushort, byte, TimeSpan, uint> assertFailure =
                (reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                {
                    Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, out string result));
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

        private void Invoke_Success(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>, int, string, double, long, ushort, byte, TimeSpan, uint, string> assertInvoke)
        {
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Success" ? ResultKind.Successful : ResultKind.Fatal, "Success");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                    return "Success";
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.Failed           += (   r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U));

            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Exception<T>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>, int, string, double, long, ushort, byte, TimeSpan, uint> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create<string>(r => ResultKind.Successful);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                    throw new T();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(0, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>, int, string, double, long, ushort, byte, TimeSpan, uint> assertInvoke, string expectedResult = "Failure")
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Fatal : ResultKind.Successful, expectedResult);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Transient);
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                    return expectedResult;
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>, int, string, double, long, ushort, byte, TimeSpan, uint, string> assertInvoke)
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Success" ? ResultKind.Successful : ResultKind.Retryable);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls == 1)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual("Success", r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualSuccess = FlakyFunc.Create(1, "Transient", "Success");
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
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

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U));

            Assert.AreEqual(2, retries);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Exception<TTransient, TFatal>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>, int, string, double, long, ushort, byte, TimeSpan, uint> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<TTransient>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, TFatal>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(TFatal), e.GetType());
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U);

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(3      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>, int, string, double, long, ushort, byte, TimeSpan, uint> assertInvoke, string expectedResult = "Failure")
        {
            Assert.AreNotEqual("Transient", expectedResult);

            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

             resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls < 3)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual(expectedResult, r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualFailure = FlakyFunc.Create(2, "Transient", expectedResult);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U);

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(3      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Exception<T>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>, int, string, double, long, ushort, byte, TimeSpan, uint> assertInvoke)
            where T : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<T>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, T>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U);

            Assert.AreEqual(4, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(4      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>, int, string, double, long, ushort, byte, TimeSpan, uint> assertInvoke, string expectedResult = "Transient")
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Retryable : ResultKind.Fatal, expectedResult);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U);

            Assert.AreEqual(2, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>(1, "Transient");
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string> reliableAction = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                    return flakyFunc();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying +=
                (i, r, e) =>
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, tokenSource.Token), tokenSource.Token);

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

        private static void AssertDelegateParameters(int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8)
        {
            Assert.AreEqual(42                   , arg1);
            Assert.AreEqual("foo"                , arg2);
            Assert.AreEqual(3.14D                , arg3);
            Assert.AreEqual(1000L                , arg4);
            Assert.AreEqual((ushort)1            , arg5);
            Assert.AreEqual((byte)255            , arg6);
            Assert.AreEqual(TimeSpan.FromDays(30), arg7);
            Assert.AreEqual(112U                 , arg8);
        }
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>

    [TestClass]
    public sealed class ReliableFunc9Test : ReliableDelegateTest<string>
    {
        private static readonly Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>, Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>> s_getFunc = DynamicGetter.ForField<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>, Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>>("_func");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            DelayPolicy delayPolicy = DelayPolicies.Constant(115);
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(func, -2              , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(func, Retries.Infinite, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(func, 37, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(func, -2              , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(func, Retries.Infinite, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(func, 37, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, ReliableDelegate<string>.DefaultResultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        [TestMethod]
        public void Ctor_ResultPolicy_DelayPolicy()
        {
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => "Hello World";
            ResultPolicy<string> resultPolicy = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            DelayPolicy          delayPolicy  = DelayPolicies.Constant(115);

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(func, Retries.Infinite, resultPolicy, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => "Hello World";
            ResultPolicy<string>       resultPolicy       = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            ComplexDelayPolicy<string> complexDelayPolicy = (i, r, e) => TimeSpan.Zero;

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(func, Retries.Infinite, resultPolicy, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        private void Ctor(
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> reliableFunc,
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>         expectedFunc,
            int                        expectedMaxRetries,
            ResultPolicy<string>       expectedResultPolicy,
            ExceptionPolicy            expectedExceptionPolicy,
            ComplexDelayPolicy<string> expectedDelayPolicy)
            => Ctor(reliableFunc, expectedFunc, expectedMaxRetries, expectedResultPolicy, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        private void Ctor(
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> reliableFunc,
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>         expectedFunc,
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
            => Invoke((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        
        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
        {
            TryInvoke(TryInvokeFunc);

            static bool TryInvokeFunc(ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, Tuple<bool, ulong> arg9, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, out result);
        }

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            TryInvoke(TryInvokeFunc);

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token) => Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token, out string result)));

            bool TryInvokeFunc(ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, Tuple<bool, ulong> arg9, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, tokenSource.Token, out result);
        }

        private void Invoke(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> invoke)
        {
            // Success
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> assertFailureResult =
                (reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => Assert.AreEqual("Bad Result", reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));

            Invoke_Failure_Result         (assertFailureResult, "Bad Result");
            Invoke_EventualFailure_Result (assertFailureResult, "Bad Result");
            Invoke_RetriesExhausted_Result(assertFailureResult, "Bad Result");

            // Failure (Exception)
            Invoke_Failure_Exception         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure_Exception <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted_Exception<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> GetFailureAssertion<T>()
                where T : Exception
            {
                return (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                {
                    try
                    {
                        invoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
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

        private void TryInvoke(TryFunc<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> tryInvoke)
        {
            // Success
            Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> assertSuccess =
                (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                {
                    Assert.IsTrue(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, out string result));
                    return result;
                };

            Invoke_Success        (assertSuccess);
            Invoke_EventualSuccess(assertSuccess);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> assertFailure =
                (reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                {
                    Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, out string result));
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

        private void Invoke_Success(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> assertInvoke)
        {
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Success" ? ResultKind.Successful : ResultKind.Fatal, "Success");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                    return "Success";
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.Failed           += (   r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL)));

            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Exception<T>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create<string>(r => ResultKind.Successful);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                    throw new T();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL));

            Assert.AreEqual(1, failures);
            Assert.AreEqual(0, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> assertInvoke, string expectedResult = "Failure")
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Fatal : ResultKind.Successful, expectedResult);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Transient);
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                    return expectedResult;
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL));

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> assertInvoke)
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Success" ? ResultKind.Successful : ResultKind.Retryable);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls == 1)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual("Success", r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualSuccess = FlakyFunc.Create(1, "Transient", "Success");
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
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

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL)));

            Assert.AreEqual(2, retries);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Exception<TTransient, TFatal>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<TTransient>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, TFatal>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(TFatal), e.GetType());
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL));

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(3      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> assertInvoke, string expectedResult = "Failure")
        {
            Assert.AreNotEqual("Transient", expectedResult);

            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

             resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls < 3)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual(expectedResult, r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualFailure = FlakyFunc.Create(2, "Transient", expectedResult);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL));

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(3      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Exception<T>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> assertInvoke)
            where T : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<T>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, T>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL));

            Assert.AreEqual(4, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(4      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> assertInvoke, string expectedResult = "Transient")
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Retryable : ResultKind.Fatal, expectedResult);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL));

            Assert.AreEqual(2, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>(1, "Transient");
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string> reliableAction = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                    return flakyFunc();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying +=
                (i, r, e) =>
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), tokenSource.Token), tokenSource.Token);

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

        private static void AssertDelegateParameters(int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, Tuple<bool, ulong> arg9)
        {
            Assert.AreEqual(42                      , arg1);
            Assert.AreEqual("foo"                   , arg2);
            Assert.AreEqual(3.14D                   , arg3);
            Assert.AreEqual(1000L                   , arg4);
            Assert.AreEqual((ushort)1               , arg5);
            Assert.AreEqual((byte)255               , arg6);
            Assert.AreEqual(TimeSpan.FromDays(30)   , arg7);
            Assert.AreEqual(112U                    , arg8);
            Assert.AreEqual(Tuple.Create(true, 64UL), arg9);
        }
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>

    [TestClass]
    public sealed class ReliableFunc10Test : ReliableDelegateTest<string>
    {
        private static readonly Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>, Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>> s_getFunc = DynamicGetter.ForField<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>, Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>>("_func");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            DelayPolicy delayPolicy = DelayPolicies.Constant(115);
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(func, -2              , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(func, Retries.Infinite, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(func, 37, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(func, -2              , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(func, Retries.Infinite, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(func, 37, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, ReliableDelegate<string>.DefaultResultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        [TestMethod]
        public void Ctor_ResultPolicy_DelayPolicy()
        {
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => "Hello World";
            ResultPolicy<string> resultPolicy = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            DelayPolicy          delayPolicy  = DelayPolicies.Constant(115);

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(func, Retries.Infinite, resultPolicy, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => "Hello World";
            ResultPolicy<string>       resultPolicy       = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            ComplexDelayPolicy<string> complexDelayPolicy = (i, r, e) => TimeSpan.Zero;

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(func, Retries.Infinite, resultPolicy, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        private void Ctor(
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> reliableFunc,
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>         expectedFunc,
            int                        expectedMaxRetries,
            ResultPolicy<string>       expectedResultPolicy,
            ExceptionPolicy            expectedExceptionPolicy,
            ComplexDelayPolicy<string> expectedDelayPolicy)
            => Ctor(reliableFunc, expectedFunc, expectedMaxRetries, expectedResultPolicy, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        private void Ctor(
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> reliableFunc,
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>         expectedFunc,
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
            => Invoke((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
        
        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, token) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
        {
            TryInvoke(TryInvokeFunc);

            static bool TryInvokeFunc(ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, Tuple<bool, ulong> arg9, DateTime arg10, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, out result);
        }

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            TryInvoke(TryInvokeFunc);

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, token) => Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, token, out string result)));

            bool TryInvokeFunc(ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, Tuple<bool, ulong> arg9, DateTime arg10, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, tokenSource.Token, out result);
        }

        private void Invoke(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> invoke)
        {
            // Success
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> assertFailureResult =
                (reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => Assert.AreEqual("Bad Result", reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));

            Invoke_Failure_Result         (assertFailureResult, "Bad Result");
            Invoke_EventualFailure_Result (assertFailureResult, "Bad Result");
            Invoke_RetriesExhausted_Result(assertFailureResult, "Bad Result");

            // Failure (Exception)
            Invoke_Failure_Exception         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure_Exception <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted_Exception<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> GetFailureAssertion<T>()
                where T : Exception
            {
                return (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                {
                    try
                    {
                        invoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
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

        private void TryInvoke(TryFunc<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> tryInvoke)
        {
            // Success
            Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> assertSuccess =
                (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                {
                    Assert.IsTrue(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, out string result));
                    return result;
                };

            Invoke_Success        (assertSuccess);
            Invoke_EventualSuccess(assertSuccess);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> assertFailure =
                (reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                {
                    Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, out string result));
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

        private void Invoke_Success(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> assertInvoke)
        {
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Success" ? ResultKind.Successful : ResultKind.Fatal, "Success");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                    return "Success";
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.Failed           += (   r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06)));

            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Exception<T>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create<string>(r => ResultKind.Successful);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                    throw new T();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06));

            Assert.AreEqual(1, failures);
            Assert.AreEqual(0, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> assertInvoke, string expectedResult = "Failure")
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Fatal : ResultKind.Successful, expectedResult);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Transient);
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                    return expectedResult;
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06));

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> assertInvoke)
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Success" ? ResultKind.Successful : ResultKind.Retryable);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls == 1)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual("Success", r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualSuccess = FlakyFunc.Create(1, "Transient", "Success");
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
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

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06)));

            Assert.AreEqual(2, retries);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Exception<TTransient, TFatal>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<TTransient>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, TFatal>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(TFatal), e.GetType());
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06));

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(3      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> assertInvoke, string expectedResult = "Failure")
        {
            Assert.AreNotEqual("Transient", expectedResult);

            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

             resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls < 3)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual(expectedResult, r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualFailure = FlakyFunc.Create(2, "Transient", expectedResult);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06));

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(3      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Exception<T>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> assertInvoke)
            where T : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<T>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, T>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06));

            Assert.AreEqual(4, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(4      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> assertInvoke, string expectedResult = "Transient")
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Retryable : ResultKind.Fatal, expectedResult);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06));

            Assert.AreEqual(2, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>(1, "Transient");
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string> reliableAction = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                    return flakyFunc();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying +=
                (i, r, e) =>
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), tokenSource.Token), tokenSource.Token);

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

        private static void AssertDelegateParameters(int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, Tuple<bool, ulong> arg9, DateTime arg10)
        {
            Assert.AreEqual(42                        , arg1);
            Assert.AreEqual("foo"                     , arg2);
            Assert.AreEqual(3.14D                     , arg3);
            Assert.AreEqual(1000L                     , arg4);
            Assert.AreEqual((ushort)1                 , arg5);
            Assert.AreEqual((byte)255                 , arg6);
            Assert.AreEqual(TimeSpan.FromDays(30)     , arg7);
            Assert.AreEqual(112U                      , arg8);
            Assert.AreEqual(Tuple.Create(true, 64UL)  , arg9);
            Assert.AreEqual(new DateTime(2019, 10, 06), arg10);
        }
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>

    [TestClass]
    public sealed class ReliableFunc11Test : ReliableDelegateTest<string>
    {
        private static readonly Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>, Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>> s_getFunc = DynamicGetter.ForField<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>, Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>>("_func");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            DelayPolicy delayPolicy = DelayPolicies.Constant(115);
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(func, -2              , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(func, Retries.Infinite, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(func, 37, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(func, -2              , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(func, Retries.Infinite, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(func, 37, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, ReliableDelegate<string>.DefaultResultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        [TestMethod]
        public void Ctor_ResultPolicy_DelayPolicy()
        {
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => "Hello World";
            ResultPolicy<string> resultPolicy = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            DelayPolicy          delayPolicy  = DelayPolicies.Constant(115);

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(func, Retries.Infinite, resultPolicy, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => "Hello World";
            ResultPolicy<string>       resultPolicy       = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            ComplexDelayPolicy<string> complexDelayPolicy = (i, r, e) => TimeSpan.Zero;

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(func, Retries.Infinite, resultPolicy, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        private void Ctor(
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> reliableFunc,
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>         expectedFunc,
            int                        expectedMaxRetries,
            ResultPolicy<string>       expectedResultPolicy,
            ExceptionPolicy            expectedExceptionPolicy,
            ComplexDelayPolicy<string> expectedDelayPolicy)
            => Ctor(reliableFunc, expectedFunc, expectedMaxRetries, expectedResultPolicy, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        private void Ctor(
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> reliableFunc,
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>         expectedFunc,
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
            => Invoke((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));
        
        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, token) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
        {
            TryInvoke(TryInvokeFunc);

            static bool TryInvokeFunc(ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, Tuple<bool, ulong> arg9, DateTime arg10, ulong arg11, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, out result);
        }

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            TryInvoke(TryInvokeFunc);

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, token) => Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, token, out string result)));

            bool TryInvokeFunc(ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, Tuple<bool, ulong> arg9, DateTime arg10, ulong arg11, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, tokenSource.Token, out result);
        }

        private void Invoke(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> invoke)
        {
            // Success
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> assertFailureResult =
                (reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => Assert.AreEqual("Bad Result", reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));

            Invoke_Failure_Result         (assertFailureResult, "Bad Result");
            Invoke_EventualFailure_Result (assertFailureResult, "Bad Result");
            Invoke_RetriesExhausted_Result(assertFailureResult, "Bad Result");

            // Failure (Exception)
            Invoke_Failure_Exception         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure_Exception <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted_Exception<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> GetFailureAssertion<T>()
                where T : Exception
            {
                return (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                {
                    try
                    {
                        invoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
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

        private void TryInvoke(TryFunc<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> tryInvoke)
        {
            // Success
            Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> assertSuccess =
                (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                {
                    Assert.IsTrue(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, out string result));
                    return result;
                };

            Invoke_Success        (assertSuccess);
            Invoke_EventualSuccess(assertSuccess);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> assertFailure =
                (reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                {
                    Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, out string result));
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

        private void Invoke_Success(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> assertInvoke)
        {
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Success" ? ResultKind.Successful : ResultKind.Fatal, "Success");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                    return "Success";
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.Failed           += (   r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL));

            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Exception<T>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create<string>(r => ResultKind.Successful);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                    throw new T();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(0, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> assertInvoke, string expectedResult = "Failure")
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Fatal : ResultKind.Successful, expectedResult);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Transient);
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                    return expectedResult;
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> assertInvoke)
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Success" ? ResultKind.Successful : ResultKind.Retryable);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls == 1)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual("Success", r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualSuccess = FlakyFunc.Create(1, "Transient", "Success");
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
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

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL));

            Assert.AreEqual(2, retries);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Exception<TTransient, TFatal>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<TTransient>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, TFatal>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(TFatal), e.GetType());
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL);

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(3      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> assertInvoke, string expectedResult = "Failure")
        {
            Assert.AreNotEqual("Transient", expectedResult);

            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

             resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls < 3)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual(expectedResult, r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualFailure = FlakyFunc.Create(2, "Transient", expectedResult);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL);

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(3      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Exception<T>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> assertInvoke)
            where T : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<T>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, T>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL);

            Assert.AreEqual(4, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(4      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> assertInvoke, string expectedResult = "Transient")
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Retryable : ResultKind.Fatal, expectedResult);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL);

            Assert.AreEqual(2, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>(1, "Transient");
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string> reliableAction = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                    return flakyFunc();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying +=
                (i, r, e) =>
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, tokenSource.Token), tokenSource.Token);

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

        private static void AssertDelegateParameters(int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, Tuple<bool, ulong> arg9, DateTime arg10, ulong arg11)
        {
            Assert.AreEqual(42                        , arg1);
            Assert.AreEqual("foo"                     , arg2);
            Assert.AreEqual(3.14D                     , arg3);
            Assert.AreEqual(1000L                     , arg4);
            Assert.AreEqual((ushort)1                 , arg5);
            Assert.AreEqual((byte)255                 , arg6);
            Assert.AreEqual(TimeSpan.FromDays(30)     , arg7);
            Assert.AreEqual(112U                      , arg8);
            Assert.AreEqual(Tuple.Create(true, 64UL)  , arg9);
            Assert.AreEqual(new DateTime(2019, 10, 06), arg10);
            Assert.AreEqual(321UL                     , arg11);
        }
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>

    [TestClass]
    public sealed class ReliableFunc12Test : ReliableDelegateTest<string>
    {
        private static readonly Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>, Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>> s_getFunc = DynamicGetter.ForField<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>, Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>>("_func");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            DelayPolicy delayPolicy = DelayPolicies.Constant(115);
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(func, -2              , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(func, Retries.Infinite, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(func, 37, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(func, -2              , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(func, Retries.Infinite, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(func, 37, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, ReliableDelegate<string>.DefaultResultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        [TestMethod]
        public void Ctor_ResultPolicy_DelayPolicy()
        {
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => "Hello World";
            ResultPolicy<string> resultPolicy = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            DelayPolicy          delayPolicy  = DelayPolicies.Constant(115);

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(func, Retries.Infinite, resultPolicy, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => "Hello World";
            ResultPolicy<string>       resultPolicy       = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            ComplexDelayPolicy<string> complexDelayPolicy = (i, r, e) => TimeSpan.Zero;

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(func, Retries.Infinite, resultPolicy, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        private void Ctor(
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> reliableFunc,
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>         expectedFunc,
            int                        expectedMaxRetries,
            ResultPolicy<string>       expectedResultPolicy,
            ExceptionPolicy            expectedExceptionPolicy,
            ComplexDelayPolicy<string> expectedDelayPolicy)
            => Ctor(reliableFunc, expectedFunc, expectedMaxRetries, expectedResultPolicy, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        private void Ctor(
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> reliableFunc,
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>         expectedFunc,
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
            => Invoke((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));
        
        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
        {
            TryInvoke(TryInvokeFunc);

            static bool TryInvokeFunc(ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, Tuple<bool, ulong> arg9, DateTime arg10, ulong arg11, sbyte arg12, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, out result);
        }

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            TryInvoke(TryInvokeFunc);

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token, out string result)));

            bool TryInvokeFunc(ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, Tuple<bool, ulong> arg9, DateTime arg10, ulong arg11, sbyte arg12, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, tokenSource.Token, out result);
        }

        private void Invoke(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> invoke)
        {
            // Success
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> assertFailureResult =
                (reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => Assert.AreEqual("Bad Result", reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));

            Invoke_Failure_Result         (assertFailureResult, "Bad Result");
            Invoke_EventualFailure_Result (assertFailureResult, "Bad Result");
            Invoke_RetriesExhausted_Result(assertFailureResult, "Bad Result");

            // Failure (Exception)
            Invoke_Failure_Exception         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure_Exception <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted_Exception<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> GetFailureAssertion<T>()
                where T : Exception
            {
                return (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                {
                    try
                    {
                        invoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
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

        private void TryInvoke(TryFunc<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> tryInvoke)
        {
            // Success
            Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> assertSuccess =
                (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                {
                    Assert.IsTrue(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, out string result));
                    return result;
                };

            Invoke_Success        (assertSuccess);
            Invoke_EventualSuccess(assertSuccess);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> assertFailure =
                (reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                {
                    Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, out string result));
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

        private void Invoke_Success(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> assertInvoke)
        {
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Success" ? ResultKind.Successful : ResultKind.Fatal, "Success");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
                    return "Success";
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.Failed           += (   r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7));

            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Exception<T>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create<string>(r => ResultKind.Successful);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
                    throw new T();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(0, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> assertInvoke, string expectedResult = "Failure")
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Fatal : ResultKind.Successful, expectedResult);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Transient);
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
                    return expectedResult;
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> assertInvoke)
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Success" ? ResultKind.Successful : ResultKind.Retryable);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls == 1)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual("Success", r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualSuccess = FlakyFunc.Create(1, "Transient", "Success");
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
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

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7));

            Assert.AreEqual(2, retries);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Exception<TTransient, TFatal>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<TTransient>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, TFatal>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(TFatal), e.GetType());
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7);

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(3      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> assertInvoke, string expectedResult = "Failure")
        {
            Assert.AreNotEqual("Transient", expectedResult);

            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

             resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls < 3)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual(expectedResult, r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualFailure = FlakyFunc.Create(2, "Transient", expectedResult);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7);

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(3      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Exception<T>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> assertInvoke)
            where T : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<T>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, T>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7);

            Assert.AreEqual(4, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(4      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> assertInvoke, string expectedResult = "Transient")
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Retryable : ResultKind.Fatal, expectedResult);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7);

            Assert.AreEqual(2, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>(1, "Transient");
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string> reliableAction = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
                    return flakyFunc();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying +=
                (i, r, e) =>
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, tokenSource.Token), tokenSource.Token);

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

        private static void AssertDelegateParameters(int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, Tuple<bool, ulong> arg9, DateTime arg10, ulong arg11, sbyte arg12)
        {
            Assert.AreEqual(42                        , arg1);
            Assert.AreEqual("foo"                     , arg2);
            Assert.AreEqual(3.14D                     , arg3);
            Assert.AreEqual(1000L                     , arg4);
            Assert.AreEqual((ushort)1                 , arg5);
            Assert.AreEqual((byte)255                 , arg6);
            Assert.AreEqual(TimeSpan.FromDays(30)     , arg7);
            Assert.AreEqual(112U                      , arg8);
            Assert.AreEqual(Tuple.Create(true, 64UL)  , arg9);
            Assert.AreEqual(new DateTime(2019, 10, 06), arg10);
            Assert.AreEqual(321UL                     , arg11);
            Assert.AreEqual((sbyte)-7                 , arg12);
        }
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>

    [TestClass]
    public sealed class ReliableFunc13Test : ReliableDelegateTest<string>
    {
        private static readonly Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>, Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>> s_getFunc = DynamicGetter.ForField<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>, Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>>("_func");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            DelayPolicy delayPolicy = DelayPolicies.Constant(115);
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(func, -2              , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(func, Retries.Infinite, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(func, 37, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(func, -2              , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(func, Retries.Infinite, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(func, 37, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, ReliableDelegate<string>.DefaultResultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        [TestMethod]
        public void Ctor_ResultPolicy_DelayPolicy()
        {
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => "Hello World";
            ResultPolicy<string> resultPolicy = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            DelayPolicy          delayPolicy  = DelayPolicies.Constant(115);

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(func, Retries.Infinite, resultPolicy, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => "Hello World";
            ResultPolicy<string>       resultPolicy       = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            ComplexDelayPolicy<string> complexDelayPolicy = (i, r, e) => TimeSpan.Zero;

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(func, Retries.Infinite, resultPolicy, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        private void Ctor(
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> reliableFunc,
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>         expectedFunc,
            int                        expectedMaxRetries,
            ResultPolicy<string>       expectedResultPolicy,
            ExceptionPolicy            expectedExceptionPolicy,
            ComplexDelayPolicy<string> expectedDelayPolicy)
            => Ctor(reliableFunc, expectedFunc, expectedMaxRetries, expectedResultPolicy, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        private void Ctor(
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> reliableFunc,
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>         expectedFunc,
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
            => Invoke((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));
        
        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
        {
            TryInvoke(TryInvokeFunc);

            static bool TryInvokeFunc(ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, Tuple<bool, ulong> arg9, DateTime arg10, ulong arg11, sbyte arg12, decimal arg13, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, out result);
        }

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            TryInvoke(TryInvokeFunc);

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token, out string result)));

            bool TryInvokeFunc(ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, Tuple<bool, ulong> arg9, DateTime arg10, ulong arg11, sbyte arg12, decimal arg13, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, tokenSource.Token, out result);
        }

        private void Invoke(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> invoke)
        {
            // Success
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> assertFailureResult =
                (reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => Assert.AreEqual("Bad Result", reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));

            Invoke_Failure_Result         (assertFailureResult, "Bad Result");
            Invoke_EventualFailure_Result (assertFailureResult, "Bad Result");
            Invoke_RetriesExhausted_Result(assertFailureResult, "Bad Result");

            // Failure (Exception)
            Invoke_Failure_Exception         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure_Exception <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted_Exception<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> GetFailureAssertion<T>()
                where T : Exception
            {
                return (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                {
                    try
                    {
                        invoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
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

        private void TryInvoke(TryFunc<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> tryInvoke)
        {
            // Success
            Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> assertSuccess =
                (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                {
                    Assert.IsTrue(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, out string result));
                    return result;
                };

            Invoke_Success        (assertSuccess);
            Invoke_EventualSuccess(assertSuccess);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> assertFailure =
                (reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                {
                    Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, out string result));
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

        private void Invoke_Success(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> assertInvoke)
        {
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Success" ? ResultKind.Successful : ResultKind.Fatal, "Success");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                    return "Success";
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.Failed           += (   r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M));

            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Exception<T>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create<string>(r => ResultKind.Successful);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                    throw new T();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(0, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> assertInvoke, string expectedResult = "Failure")
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Fatal : ResultKind.Successful, expectedResult);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Transient);
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                    return expectedResult;
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> assertInvoke)
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Success" ? ResultKind.Successful : ResultKind.Retryable);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls == 1)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual("Success", r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualSuccess = FlakyFunc.Create(1, "Transient", "Success");
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
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

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M));

            Assert.AreEqual(2, retries);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Exception<TTransient, TFatal>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<TTransient>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, TFatal>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(TFatal), e.GetType());
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M);

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(3      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> assertInvoke, string expectedResult = "Failure")
        {
            Assert.AreNotEqual("Transient", expectedResult);

            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

             resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls < 3)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual(expectedResult, r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualFailure = FlakyFunc.Create(2, "Transient", expectedResult);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M);

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(3      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Exception<T>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> assertInvoke)
            where T : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<T>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, T>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M);

            Assert.AreEqual(4, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(4      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> assertInvoke, string expectedResult = "Transient")
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Retryable : ResultKind.Fatal, expectedResult);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M);

            Assert.AreEqual(2, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>(1, "Transient");
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string> reliableAction = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                    return flakyFunc();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying +=
                (i, r, e) =>
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, tokenSource.Token), tokenSource.Token);

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

        private static void AssertDelegateParameters(int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, Tuple<bool, ulong> arg9, DateTime arg10, ulong arg11, sbyte arg12, decimal arg13)
        {
            Assert.AreEqual(42                        , arg1);
            Assert.AreEqual("foo"                     , arg2);
            Assert.AreEqual(3.14D                     , arg3);
            Assert.AreEqual(1000L                     , arg4);
            Assert.AreEqual((ushort)1                 , arg5);
            Assert.AreEqual((byte)255                 , arg6);
            Assert.AreEqual(TimeSpan.FromDays(30)     , arg7);
            Assert.AreEqual(112U                      , arg8);
            Assert.AreEqual(Tuple.Create(true, 64UL)  , arg9);
            Assert.AreEqual(new DateTime(2019, 10, 06), arg10);
            Assert.AreEqual(321UL                     , arg11);
            Assert.AreEqual((sbyte)-7                 , arg12);
            Assert.AreEqual(-24.68M                   , arg13);
        }
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>

    [TestClass]
    public sealed class ReliableFunc14Test : ReliableDelegateTest<string>
    {
        private static readonly Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>, Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>> s_getFunc = DynamicGetter.ForField<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>, Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>>("_func");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            DelayPolicy delayPolicy = DelayPolicies.Constant(115);
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(func, -2              , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(func, Retries.Infinite, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(func, 37, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(func, -2              , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(func, Retries.Infinite, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(func, 37, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, ReliableDelegate<string>.DefaultResultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        [TestMethod]
        public void Ctor_ResultPolicy_DelayPolicy()
        {
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => "Hello World";
            ResultPolicy<string> resultPolicy = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            DelayPolicy          delayPolicy  = DelayPolicies.Constant(115);

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(func, Retries.Infinite, resultPolicy, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => "Hello World";
            ResultPolicy<string>       resultPolicy       = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            ComplexDelayPolicy<string> complexDelayPolicy = (i, r, e) => TimeSpan.Zero;

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(func, Retries.Infinite, resultPolicy, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        private void Ctor(
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> reliableFunc,
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>         expectedFunc,
            int                        expectedMaxRetries,
            ResultPolicy<string>       expectedResultPolicy,
            ExceptionPolicy            expectedExceptionPolicy,
            ComplexDelayPolicy<string> expectedDelayPolicy)
            => Ctor(reliableFunc, expectedFunc, expectedMaxRetries, expectedResultPolicy, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        private void Ctor(
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> reliableFunc,
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>         expectedFunc,
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
            => Invoke((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));
        
        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
        {
            TryInvoke(TryInvokeFunc);

            static bool TryInvokeFunc(ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, Tuple<bool, ulong> arg9, DateTime arg10, ulong arg11, sbyte arg12, decimal arg13, char arg14, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, out result);
        }

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            TryInvoke(TryInvokeFunc);

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token) => Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token, out string result)));

            bool TryInvokeFunc(ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, Tuple<bool, ulong> arg9, DateTime arg10, ulong arg11, sbyte arg12, decimal arg13, char arg14, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, tokenSource.Token, out result);
        }

        private void Invoke(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> invoke)
        {
            // Success
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> assertFailureResult =
                (reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => Assert.AreEqual("Bad Result", reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));

            Invoke_Failure_Result         (assertFailureResult, "Bad Result");
            Invoke_EventualFailure_Result (assertFailureResult, "Bad Result");
            Invoke_RetriesExhausted_Result(assertFailureResult, "Bad Result");

            // Failure (Exception)
            Invoke_Failure_Exception         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure_Exception <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted_Exception<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> GetFailureAssertion<T>()
                where T : Exception
            {
                return (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                {
                    try
                    {
                        invoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
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

        private void TryInvoke(TryFunc<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> tryInvoke)
        {
            // Success
            Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> assertSuccess =
                (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                {
                    Assert.IsTrue(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, out string result));
                    return result;
                };

            Invoke_Success        (assertSuccess);
            Invoke_EventualSuccess(assertSuccess);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> assertFailure =
                (reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                {
                    Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, out string result));
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

        private void Invoke_Success(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> assertInvoke)
        {
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Success" ? ResultKind.Successful : ResultKind.Fatal, "Success");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                    return "Success";
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.Failed           += (   r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!'));

            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Exception<T>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create<string>(r => ResultKind.Successful);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                    throw new T();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!');

            Assert.AreEqual(1, failures);
            Assert.AreEqual(0, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> assertInvoke, string expectedResult = "Failure")
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Fatal : ResultKind.Successful, expectedResult);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Transient);
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                    return expectedResult;
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!');

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> assertInvoke)
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Success" ? ResultKind.Successful : ResultKind.Retryable);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls == 1)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual("Success", r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualSuccess = FlakyFunc.Create(1, "Transient", "Success");
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
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

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!'));

            Assert.AreEqual(2, retries);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Exception<TTransient, TFatal>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<TTransient>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, TFatal>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(TFatal), e.GetType());
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!');

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(3      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> assertInvoke, string expectedResult = "Failure")
        {
            Assert.AreNotEqual("Transient", expectedResult);

            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

             resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls < 3)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual(expectedResult, r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualFailure = FlakyFunc.Create(2, "Transient", expectedResult);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!');

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(3      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Exception<T>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> assertInvoke)
            where T : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<T>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, T>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!');

            Assert.AreEqual(4, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(4      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> assertInvoke, string expectedResult = "Transient")
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Retryable : ResultKind.Fatal, expectedResult);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!');

            Assert.AreEqual(2, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>(1, "Transient");
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string> reliableAction = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                    return flakyFunc();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying +=
                (i, r, e) =>
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', tokenSource.Token), tokenSource.Token);

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

        private static void AssertDelegateParameters(int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, Tuple<bool, ulong> arg9, DateTime arg10, ulong arg11, sbyte arg12, decimal arg13, char arg14)
        {
            Assert.AreEqual(42                        , arg1);
            Assert.AreEqual("foo"                     , arg2);
            Assert.AreEqual(3.14D                     , arg3);
            Assert.AreEqual(1000L                     , arg4);
            Assert.AreEqual((ushort)1                 , arg5);
            Assert.AreEqual((byte)255                 , arg6);
            Assert.AreEqual(TimeSpan.FromDays(30)     , arg7);
            Assert.AreEqual(112U                      , arg8);
            Assert.AreEqual(Tuple.Create(true, 64UL)  , arg9);
            Assert.AreEqual(new DateTime(2019, 10, 06), arg10);
            Assert.AreEqual(321UL                     , arg11);
            Assert.AreEqual((sbyte)-7                 , arg12);
            Assert.AreEqual(-24.68M                   , arg13);
            Assert.AreEqual('!'                       , arg14);
        }
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>

    [TestClass]
    public sealed class ReliableFunc15Test : ReliableDelegateTest<string>
    {
        private static readonly Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>, Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>> s_getFunc = DynamicGetter.ForField<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>, Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>>("_func");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            DelayPolicy delayPolicy = DelayPolicies.Constant(115);
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(func, -2              , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(func, Retries.Infinite, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(func, 37, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(func, -2              , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(func, Retries.Infinite, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(func, 37, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, ReliableDelegate<string>.DefaultResultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        [TestMethod]
        public void Ctor_ResultPolicy_DelayPolicy()
        {
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => "Hello World";
            ResultPolicy<string> resultPolicy = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            DelayPolicy          delayPolicy  = DelayPolicies.Constant(115);

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(func, Retries.Infinite, resultPolicy, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => "Hello World";
            ResultPolicy<string>       resultPolicy       = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            ComplexDelayPolicy<string> complexDelayPolicy = (i, r, e) => TimeSpan.Zero;

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(func, Retries.Infinite, resultPolicy, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        private void Ctor(
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> reliableFunc,
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>         expectedFunc,
            int                        expectedMaxRetries,
            ResultPolicy<string>       expectedResultPolicy,
            ExceptionPolicy            expectedExceptionPolicy,
            ComplexDelayPolicy<string> expectedDelayPolicy)
            => Ctor(reliableFunc, expectedFunc, expectedMaxRetries, expectedResultPolicy, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        private void Ctor(
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> reliableFunc,
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>         expectedFunc,
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
            => Invoke((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));
        
        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
        {
            TryInvoke(TryInvokeFunc);

            static bool TryInvokeFunc(ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, Tuple<bool, ulong> arg9, DateTime arg10, ulong arg11, sbyte arg12, decimal arg13, char arg14, float arg15, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, out result);
        }

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            TryInvoke(TryInvokeFunc);

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token, out string result)));

            bool TryInvokeFunc(ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, Tuple<bool, ulong> arg9, DateTime arg10, ulong arg11, sbyte arg12, decimal arg13, char arg14, float arg15, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, tokenSource.Token, out result);
        }

        private void Invoke(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> invoke)
        {
            // Success
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> assertFailureResult =
                (reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => Assert.AreEqual("Bad Result", reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));

            Invoke_Failure_Result         (assertFailureResult, "Bad Result");
            Invoke_EventualFailure_Result (assertFailureResult, "Bad Result");
            Invoke_RetriesExhausted_Result(assertFailureResult, "Bad Result");

            // Failure (Exception)
            Invoke_Failure_Exception         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure_Exception <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted_Exception<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> GetFailureAssertion<T>()
                where T : Exception
            {
                return (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                {
                    try
                    {
                        invoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
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

        private void TryInvoke(TryFunc<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> tryInvoke)
        {
            // Success
            Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> assertSuccess =
                (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                {
                    Assert.IsTrue(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, out string result));
                    return result;
                };

            Invoke_Success        (assertSuccess);
            Invoke_EventualSuccess(assertSuccess);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> assertFailure =
                (reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                {
                    Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, out string result));
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

        private void Invoke_Success(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> assertInvoke)
        {
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Success" ? ResultKind.Successful : ResultKind.Fatal, "Success");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                    return "Success";
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.Failed           += (   r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F));

            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Exception<T>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create<string>(r => ResultKind.Successful);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                    throw new T();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(0, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> assertInvoke, string expectedResult = "Failure")
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Fatal : ResultKind.Successful, expectedResult);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Transient);
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                    return expectedResult;
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> assertInvoke)
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Success" ? ResultKind.Successful : ResultKind.Retryable);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls == 1)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual("Success", r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualSuccess = FlakyFunc.Create(1, "Transient", "Success");
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
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

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F));

            Assert.AreEqual(2, retries);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Exception<TTransient, TFatal>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<TTransient>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, TFatal>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(TFatal), e.GetType());
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F);

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(3      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> assertInvoke, string expectedResult = "Failure")
        {
            Assert.AreNotEqual("Transient", expectedResult);

            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

             resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls < 3)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual(expectedResult, r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualFailure = FlakyFunc.Create(2, "Transient", expectedResult);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F);

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(3      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Exception<T>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> assertInvoke)
            where T : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<T>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, T>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F);

            Assert.AreEqual(4, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(4      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> assertInvoke, string expectedResult = "Transient")
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Retryable : ResultKind.Fatal, expectedResult);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F);

            Assert.AreEqual(2, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>(1, "Transient");
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string> reliableAction = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                    return flakyFunc();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying +=
                (i, r, e) =>
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, tokenSource.Token), tokenSource.Token);

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

        private static void AssertDelegateParameters(int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, Tuple<bool, ulong> arg9, DateTime arg10, ulong arg11, sbyte arg12, decimal arg13, char arg14, float arg15)
        {
            Assert.AreEqual(42                        , arg1);
            Assert.AreEqual("foo"                     , arg2);
            Assert.AreEqual(3.14D                     , arg3);
            Assert.AreEqual(1000L                     , arg4);
            Assert.AreEqual((ushort)1                 , arg5);
            Assert.AreEqual((byte)255                 , arg6);
            Assert.AreEqual(TimeSpan.FromDays(30)     , arg7);
            Assert.AreEqual(112U                      , arg8);
            Assert.AreEqual(Tuple.Create(true, 64UL)  , arg9);
            Assert.AreEqual(new DateTime(2019, 10, 06), arg10);
            Assert.AreEqual(321UL                     , arg11);
            Assert.AreEqual((sbyte)-7                 , arg12);
            Assert.AreEqual(-24.68M                   , arg13);
            Assert.AreEqual('!'                       , arg14);
            Assert.AreEqual(0.1F                      , arg15);
        }
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>

    [TestClass]
    public sealed class ReliableFunc16Test : ReliableDelegateTest<string>
    {
        private static readonly Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>, Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>> s_getFunc = DynamicGetter.ForField<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>, Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>>("_func");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            DelayPolicy delayPolicy = DelayPolicies.Constant(115);
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(func, -2              , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(func, Retries.Infinite, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(func, 37, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => "Hello World";

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(func, -2              , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(func, Retries.Infinite, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(func, Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(func, 37, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, ReliableDelegate<string>.DefaultResultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        [TestMethod]
        public void Ctor_ResultPolicy_DelayPolicy()
        {
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => "Hello World";
            ResultPolicy<string> resultPolicy = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            DelayPolicy          delayPolicy  = DelayPolicies.Constant(115);

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(func, Retries.Infinite, resultPolicy, null                   , delayPolicy      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (DelayPolicy)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, delayPolicy);

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
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => "Hello World";
            ResultPolicy<string>       resultPolicy       = r => r == "foo" ? ResultKind.Successful : ResultKind.Fatal;
            ComplexDelayPolicy<string> complexDelayPolicy = (i, r, e) => TimeSpan.Zero;

            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(null, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(func, -2              , resultPolicy, ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(func, Retries.Infinite, null        , ExceptionPolicies.Fatal, complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(func, Retries.Infinite, resultPolicy, null                   , complexDelayPolicy              ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(func, Retries.Infinite, resultPolicy, ExceptionPolicies.Fatal, (ComplexDelayPolicy<string>)null));

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> actual = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);

            Ctor(actual, func, 37, resultPolicy, ExceptionPolicies.Transient, complexDelayPolicy);
        }

        private void Ctor(
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> reliableFunc,
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>         expectedFunc,
            int                        expectedMaxRetries,
            ResultPolicy<string>       expectedResultPolicy,
            ExceptionPolicy            expectedExceptionPolicy,
            ComplexDelayPolicy<string> expectedDelayPolicy)
            => Ctor(reliableFunc, expectedFunc, expectedMaxRetries, expectedResultPolicy, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        private void Ctor(
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> reliableFunc,
            Func<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>         expectedFunc,
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
            => Invoke((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));
        
        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, token) => reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
        {
            TryInvoke(TryInvokeFunc);

            static bool TryInvokeFunc(ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, Tuple<bool, ulong> arg9, DateTime arg10, ulong arg11, sbyte arg12, decimal arg13, char arg14, float arg15, Guid arg16, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, out result);
        }

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            TryInvoke(TryInvokeFunc);

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, token) => Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, token, out string result)));

            bool TryInvokeFunc(ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> reliableFunc, int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, Tuple<bool, ulong> arg9, DateTime arg10, ulong arg11, sbyte arg12, decimal arg13, char arg14, float arg15, Guid arg16, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, tokenSource.Token, out result);
        }

        private void Invoke(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> invoke)
        {
            // Success
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> assertFailureResult =
                (reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => Assert.AreEqual("Bad Result", reliableFunc.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));

            Invoke_Failure_Result         (assertFailureResult, "Bad Result");
            Invoke_EventualFailure_Result (assertFailureResult, "Bad Result");
            Invoke_RetriesExhausted_Result(assertFailureResult, "Bad Result");

            // Failure (Exception)
            Invoke_Failure_Exception         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure_Exception <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted_Exception<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> GetFailureAssertion<T>()
                where T : Exception
            {
                return (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                {
                    try
                    {
                        invoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
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

        private void TryInvoke(TryFunc<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> tryInvoke)
        {
            // Success
            Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> assertSuccess =
                (r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                {
                    Assert.IsTrue(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, out string result));
                    return result;
                };

            Invoke_Success        (assertSuccess);
            Invoke_EventualSuccess(assertSuccess);

            // Failure (Bad Result)
            Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> assertFailure =
                (reliableFunc, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                {
                    Assert.IsFalse(reliableFunc.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, out string result));
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

        private void Invoke_Success(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> assertInvoke)
        {
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Success" ? ResultKind.Successful : ResultKind.Fatal, "Success");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
                    return "Success";
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.Failed           += (   r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, Guid.Parse("53710ff0-eaa3-4fac-a068-e5be641d446b")));

            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Exception<T>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create<string>(r => ResultKind.Successful);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
                    throw new T();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, Guid.Parse("53710ff0-eaa3-4fac-a068-e5be641d446b"));

            Assert.AreEqual(1, failures);
            Assert.AreEqual(0, resultPolicy   .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> assertInvoke, string expectedResult = "Failure")
        {
            ObservableFunc<string, ResultKind>               resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Fatal : ResultKind.Successful, expectedResult);
            ObservableFunc<Exception, bool>                  exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Transient);
            ObservableFunc<int, string, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<string>((i, r, e) => TimeSpan.Zero);

            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
                    return expectedResult;
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableFunc.Retrying         += (i, r, e) => Assert.Fail();
            reliableFunc.RetriesExhausted += (   r, e) => Assert.Fail();
            reliableFunc.Failed           +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, Guid.Parse("53710ff0-eaa3-4fac-a068-e5be641d446b"));

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, resultPolicy   .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Func<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> assertInvoke)
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Success" ? ResultKind.Successful : ResultKind.Retryable);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls == 1)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual("Success", r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualSuccess = FlakyFunc.Create(1, "Transient", "Success");
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
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

            Assert.AreEqual("Success", assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, Guid.Parse("53710ff0-eaa3-4fac-a068-e5be641d446b")));

            Assert.AreEqual(2, retries);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Exception<TTransient, TFatal>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<TTransient>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, TFatal>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(TFatal), e.GetType());
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, Guid.Parse("53710ff0-eaa3-4fac-a068-e5be641d446b"));

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(3      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> assertInvoke, string expectedResult = "Failure")
        {
            Assert.AreNotEqual("Transient", expectedResult);

            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = new ObservableFunc<string, ResultKind>(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

             resultPolicy.Invoking +=
                r =>
                {
                    if (resultPolicy.Calls < 3)
                        Assert.AreEqual("Transient", r);
                    else
                        Assert.AreEqual(expectedResult, r);
                };
            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            Func<string> eventualFailure = FlakyFunc.Create(2, "Transient", expectedResult);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
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
            reliableFunc.Failed +=
                (r, e) =>
                {
                    failures++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };
            reliableFunc.RetriesExhausted += (r, e) => Assert.Fail();

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, Guid.Parse("53710ff0-eaa3-4fac-a068-e5be641d446b"));

            Assert.AreEqual(3, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(3      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Exception<T>(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> assertInvoke)
            where T : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => ResultKind.Retryable, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<T>(2);
            Func<string> eventualFailure = FlakyFunc.Create<string, T>(1, "Transient", transientError: false);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.IsNull(r);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, Guid.Parse("53710ff0-eaa3-4fac-a068-e5be641d446b"));

            Assert.AreEqual(4, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(1      , resultPolicy   .Calls);
            Assert.AreEqual(4      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted_Result(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> assertInvoke, string expectedResult = "Transient")
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == expectedResult ? ResultKind.Retryable : ResultKind.Fatal, expectedResult);
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action flakyAction = FlakyAction.Create<IOException>(1);
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> reliableFunc = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
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
            reliableFunc.RetriesExhausted +=
                (r, e) =>
                {
                    exhausted++;

                    Assert.AreEqual(expectedResult, r);
                    Assert.IsNull(e);
                };

            assertInvoke(reliableFunc, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, Guid.Parse("53710ff0-eaa3-4fac-a068-e5be641d446b"));

            Assert.AreEqual(2, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(2      , resultPolicy   .Calls);
            Assert.AreEqual(1      , exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<string, ResultKind> resultPolicy    = PolicyValidator.Create(r => r == "Transient" ? ResultKind.Retryable : ResultKind.Fatal, "Transient");
            ObservableFunc<Exception, bool>    exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>      delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>(1, "Transient");
            ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string> reliableAction = new ReliableFunc<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, string>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
                    return flakyFunc();
                },
                Retries.Infinite,
                resultPolicy   .Invoke,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying +=
                (i, r, e) =>
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, Guid.Parse("53710ff0-eaa3-4fac-a068-e5be641d446b"), tokenSource.Token), tokenSource.Token);

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

        private static void AssertDelegateParameters(int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, Tuple<bool, ulong> arg9, DateTime arg10, ulong arg11, sbyte arg12, decimal arg13, char arg14, float arg15, Guid arg16)
        {
            Assert.AreEqual(42                                                , arg1);
            Assert.AreEqual("foo"                                             , arg2);
            Assert.AreEqual(3.14D                                             , arg3);
            Assert.AreEqual(1000L                                             , arg4);
            Assert.AreEqual((ushort)1                                         , arg5);
            Assert.AreEqual((byte)255                                         , arg6);
            Assert.AreEqual(TimeSpan.FromDays(30)                             , arg7);
            Assert.AreEqual(112U                                              , arg8);
            Assert.AreEqual(Tuple.Create(true, 64UL)                          , arg9);
            Assert.AreEqual(new DateTime(2019, 10, 06)                        , arg10);
            Assert.AreEqual(321UL                                             , arg11);
            Assert.AreEqual((sbyte)-7                                         , arg12);
            Assert.AreEqual(-24.68M                                           , arg13);
            Assert.AreEqual('!'                                               , arg14);
            Assert.AreEqual(0.1F                                              , arg15);
            Assert.AreEqual(Guid.Parse("53710ff0-eaa3-4fac-a068-e5be641d446b"), arg16);
        }
    }

    #endregion

}
