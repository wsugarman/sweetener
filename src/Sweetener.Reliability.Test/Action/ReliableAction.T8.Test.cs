// Generated from ReliableAction.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    [TestClass]
    public sealed class ReliableAction8Test : ReliableDelegateTest
    {
        private static readonly Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>, Action<int, string, double, long, ushort, byte, TimeSpan, uint>> s_getAction = DynamicGetter.ForField<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>, Action<int, string, double, long, ushort, byte, TimeSpan, uint>>("_action");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(null, Retries.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Console.WriteLine("Hello World"), -2              , ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

            Action<int, string, double, long, ushort, byte, TimeSpan, uint> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint> actual = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(action, 37, ExceptionPolicies.Transient, DelayPolicies.Constant(115));

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
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(null, Retries.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Console.WriteLine("Hello World"), -2              , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));

            ComplexDelayPolicy delayPolicy = (i, e) => TimeSpan.Zero;
            Action<int, string, double, long, ushort, byte, TimeSpan, uint> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint> actual = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(action, 37, ExceptionPolicies.Transient, delayPolicy);

            Ctor(actual, action, 37, ExceptionPolicies.Transient, delayPolicy);
        }

        private void Ctor(ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint> reliableAction, Action<int, string, double, long, ushort, byte, TimeSpan, uint> expectedAction, int expectedMaxRetries, ExceptionPolicy expectedExceptionPolicy, ComplexDelayPolicy expectedDelayPolicy)
            => Ctor(reliableAction, expectedAction, expectedMaxRetries, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        private void Ctor(ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint> reliableAction, Action<int, string, double, long, ushort, byte, TimeSpan, uint> expectedAction, int expectedMaxRetries, ExceptionPolicy expectedExceptionPolicy, Action<ComplexDelayPolicy> validateDelayPolicy)
        {
            Assert.AreSame (expectedAction, s_getAction(reliableAction));
            Assert.AreEqual(expectedMaxRetries, reliableAction.MaxRetries);
            Assert.AreSame (expectedExceptionPolicy, s_getExceptionPolicy(reliableAction));

            validateDelayPolicy(s_getDelayPolicy(reliableAction));
        }

        [TestMethod]
        public void Invoke_NoCancellationToken()
            => Invoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, tokenSource.Token));

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                TryInvoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token));
        }

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>, int, string, double, long, ushort, byte, TimeSpan, uint> invoke)
        {
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            Invoke_Failure         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>, int, string, double, long, ushort, byte, TimeSpan, uint> GetFailureAssertion<T>()
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
                    catch (Exception)
                    {
                        Assert.Fail();
                    }
                };
            }
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>, int, string, double, long, ushort, byte, TimeSpan, uint, bool> tryInvoke)
        {
            Invoke_Success                                                 ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8)));
            Invoke_EventualSuccess                                         ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8)));
            Invoke_Failure         <             InvalidOperationException>((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8)));
            Invoke_EventualFailure <IOException, InvalidOperationException>((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8)));
            Invoke_RetriesExhausted<IOException                           >((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8)));
        }

        private void Invoke_Success(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>, int, string, double, long, ushort, byte, TimeSpan, uint> assertInvoke)
        {
            ObservableFunc<Exception, bool    > exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int      , TimeSpan> delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8),
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U);

            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure<T>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>, int, string, double, long, ushort, byte, TimeSpan, uint> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception     , bool    > exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<T>((i, e) => TimeSpan.Zero);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                    throw new T();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();
            reliableAction.Failed           +=     e  => { failures++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>, int, string, double, long, ushort, byte, TimeSpan, uint> assertInvoke)
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<Exception, bool    > exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int      , TimeSpan> delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action eventualSuccess = FlakyAction.Create<IOException>(1);
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
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

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U);

            Assert.AreEqual(1, retries);
            Assert.AreEqual(retries, exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure<TTransient, TFatal>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>, int, string, double, long, ushort, byte, TimeSpan, uint> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<Exception, bool    > exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int      , TimeSpan> delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(ConstantDelay));

            delayPolicy.Invoked += (i, d) => delayStartUtc = DateTime.UtcNow;

            Action eventualFailure = FlakyAction.Create<TTransient, TFatal>(2);
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
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

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U);

            Assert.AreEqual(2, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted<T>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>, int, string, double, long, ushort, byte, TimeSpan, uint> assertInvoke)
            where T : Exception, new()
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<Exception     , bool    > exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<T>((i, e) => ConstantDelay); // A "complex" delay policy

            delayPolicy.Invoked += (i, e, d) => delayStartUtc = DateTime.UtcNow;

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
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

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U);

            Assert.AreEqual(2, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<Exception, bool    > exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int      , TimeSpan> delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, tokenSource.Token), tokenSource.Token);

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
}
