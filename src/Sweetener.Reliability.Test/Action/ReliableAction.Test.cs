// Generated from ReliableAction.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    [TestClass]
    public sealed class ReliableActionTest : ReliableDelegateTest
    {
        private static readonly Func<ReliableAction, Action> s_getAction = DynamicGetter.ForField<ReliableAction, Action>("_action");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction(null, Retries.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction(() => Console.WriteLine("Hello World"), -2              , ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction(() => Console.WriteLine("Hello World"), Retries.Infinite, null                   , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction(() => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

            Action action = () => Console.WriteLine("Hello World");
            ReliableAction actual = new ReliableAction(action, 37, ExceptionPolicies.Transient, DelayPolicies.Constant(115));

            // DelayPolicies are wrapped in ComplexDelayPolicies, so we can only validate the correct assignment by invoking the policy
            Ctor(actual, action, 37, ExceptionPolicies.Transient, actualPolicy =>
            {
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 1, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 2, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy(10, new Exception()));
            });
        }

        [TestMethod]
        public void Ctor_ComplexDelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction(null, Retries.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction(() => Console.WriteLine("Hello World"), -2              , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction(() => Console.WriteLine("Hello World"), Retries.Infinite, null                   , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction(() => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));

            ComplexDelayPolicy delayPolicy = (i, e) => TimeSpan.Zero;
            Action action = () => Console.WriteLine("Hello World");
            ReliableAction actual = new ReliableAction(action, 37, ExceptionPolicies.Transient, delayPolicy);

            Ctor(actual, action, 37, ExceptionPolicies.Transient, delayPolicy);
        }

        private void Ctor(ReliableAction reliableAction, Action expectedAction, int expectedMaxRetries, ExceptionPolicy expectedExceptionPolicy, ComplexDelayPolicy expectedDelayPolicy)
            => Ctor(reliableAction, expectedAction, expectedMaxRetries, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        private void Ctor(ReliableAction reliableAction, Action expectedAction, int expectedMaxRetries, ExceptionPolicy expectedExceptionPolicy, Action<ComplexDelayPolicy> validateDelayPolicy)
        {
            Assert.AreSame (expectedAction, s_getAction(reliableAction));
            Assert.AreEqual(expectedMaxRetries, reliableAction.MaxRetries);
            Assert.AreSame (expectedExceptionPolicy, s_getExceptionPolicy(reliableAction));

            validateDelayPolicy(s_getDelayPolicy(reliableAction));
        }

        [TestMethod]
        public void Invoke_NoCancellationToken()
            => Invoke((reliableAction) => reliableAction.Invoke());

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableAction) => reliableAction.Invoke(tokenSource.Token));

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, token) => reliableAction.Invoke(token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke((reliableAction) => reliableAction.TryInvoke());

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                TryInvoke((reliableAction) => reliableAction.TryInvoke(tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, token) => reliableAction.TryInvoke(token));
        }

        private void Invoke(Action<ReliableAction> invoke)
        {
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            Invoke_Failure         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableAction> GetFailureAssertion<T>()
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
                    catch (Exception)
                    {
                        Assert.Fail();
                    }
                };
            }
        }

        private void TryInvoke(Func<ReliableAction, bool> tryInvoke)
        {
            Invoke_Success                                                 ((r) => Assert.IsTrue (tryInvoke(r)));
            Invoke_EventualSuccess                                         ((r) => Assert.IsTrue (tryInvoke(r)));
            Invoke_Failure         <             InvalidOperationException>((r) => Assert.IsFalse(tryInvoke(r)));
            Invoke_EventualFailure <IOException, InvalidOperationException>((r) => Assert.IsFalse(tryInvoke(r)));
            Invoke_RetriesExhausted<IOException                           >((r) => Assert.IsFalse(tryInvoke(r)));
        }

        private void Invoke_Success(Action<ReliableAction> assertInvoke)
        {
            ObservableFunc<Exception, bool    > exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int      , TimeSpan> delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction reliableAction = new ReliableAction(
                () => Console.WriteLine("Success"),
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction);

            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure<T>(Action<ReliableAction> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception     , bool    > exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<T>((i, e) => TimeSpan.Zero);

            ReliableAction reliableAction = new ReliableAction(
                () =>
                {
                    throw new T();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();
            reliableAction.Failed           +=     e  => { failures++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Action<ReliableAction> assertInvoke)
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<Exception, bool    > exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int      , TimeSpan> delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action eventualSuccess = FlakyAction.Create<IOException>(1);
            ReliableAction reliableAction = new ReliableAction(
                () =>
                {
                    eventualSuccess();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying += (i, e) =>
            {
                Assert.AreEqual(++retries, i);
                Assert.AreEqual(typeof(IOException), e.GetType());

                TimeSpan actual = DateTime.UtcNow - delayStartUtc;
                Assert.IsTrue(actual > MinDelay, $"Actual delay {actual} less than allowed minimum delay {MinDelay}");
            };
            reliableAction.Failed           += e => Assert.Fail();
            reliableAction.RetriesExhausted += e => Assert.Fail();

            assertInvoke(reliableAction);

            Assert.AreEqual(1, retries);
            Assert.AreEqual(retries, exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure<TTransient, TFatal>(Action<ReliableAction> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<Exception, bool    > exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int      , TimeSpan> delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action eventualFailure = FlakyAction.Create<TTransient, TFatal>(2);
            ReliableAction reliableAction = new ReliableAction(
                () =>
                {
                    eventualFailure();
                },
                4,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, failures = 0;
            reliableAction.Retrying += (i, e) =>
            {
                Assert.AreEqual(++retries, i);
                Assert.AreEqual(typeof(TTransient), e.GetType());

                TimeSpan actual = DateTime.UtcNow - delayStartUtc;
                Assert.IsTrue(actual > MinDelay, $"Actual delay {actual} less than allowed minimum delay {MinDelay}");
            };
            reliableAction.Failed           += e => { failures++; Assert.AreEqual(typeof(TFatal), e.GetType()); };
            reliableAction.RetriesExhausted += e => Assert.Fail();

            assertInvoke(reliableAction);

            Assert.AreEqual(2, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted<T>(Action<ReliableAction> assertInvoke)
            where T : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<Exception     , bool    > exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<T>((i, e) => ConstantDelay); // A "complex" delay policy

            delayPolicy.Invoked += (i, e, d) => delayStartUtc = DateTime.UtcNow;

            ReliableAction reliableAction = new ReliableAction(
                () =>
                {
                    throw new T();
                },
                2,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, exhausted = 0;
            reliableAction.Retrying += (i, e) =>
            {
                Assert.AreEqual(++retries, i);
                Assert.AreEqual(typeof(T), e.GetType());

                TimeSpan actual = DateTime.UtcNow - delayStartUtc;
                Assert.IsTrue(actual > MinDelay, $"Actual delay {actual} less than allowed minimum delay {MinDelay}");
            };
            reliableAction.Failed           += e => Assert.Fail();
            reliableAction.RetriesExhausted += e => { exhausted++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction);

            Assert.AreEqual(2, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableAction, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<Exception, bool    > exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int      , TimeSpan> delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction reliableAction = new ReliableAction(
                () =>
                {
                    throw new IOException();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying += (i, e) =>
            {
                Assert.AreEqual(++retries, i);
                Assert.AreEqual(typeof(IOException), e.GetType());
                retryEvent.Set();
            };
            reliableAction.Failed           += e => Assert.Fail();
            reliableAction.RetriesExhausted += e => Assert.Fail();

            // While waiting for the reliable action to complete, we'll cancel it
            Task invocation = Task.Run(() => assertInvoke(reliableAction, tokenSource.Token), tokenSource.Token);

            // Cancel after at least 1 retry has occurred
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
                        Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
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
}
