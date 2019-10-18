// Generated from ReliableAction.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    #region ReliableAction

    [TestClass]
    public sealed class ReliableActionTest : ReliableDelegateTest
    {
        private static readonly Func<ReliableAction, Action> s_getAction = DynamicGetter.ForField<ReliableAction, Action>("_action");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction(null, Retries.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction(() => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction(() => Console.WriteLine("Hello World"), Retries.Infinite, null                   , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction(() => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

            Action action = () => Console.WriteLine("Hello World");
            ReliableAction actual = new ReliableAction(action, 37, ExceptionPolicies.Transient, DelayPolicies.Constant(115));

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, actualPolicy =>
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
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction(() => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction(() => Console.WriteLine("Hello World"), Retries.Infinite, null                   , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction(() => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));

            ComplexDelayPolicy delayPolicy = (i, e) => TimeSpan.Zero;
            Action action = () => Console.WriteLine("Hello World");
            ReliableAction actual = new ReliableAction(action, 37, ExceptionPolicies.Transient, delayPolicy);

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, delayPolicy);
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
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

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
            ObservableFunc<Exception, bool>          exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
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
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            ReliableAction reliableAction = new ReliableAction(
                () =>
                {
                    eventualSuccess();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(IOException), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction);

            Assert.AreEqual(3, retries);
            Assert.AreEqual(retries, exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure<TTransient, TFatal>(Action<ReliableAction> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualFailure = FlakyAction.Create<TTransient, TFatal>(4);
            ReliableAction reliableAction = new ReliableAction(
                () =>
                {
                    eventualFailure();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, failures = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(TTransient), e.GetType()); };
            reliableAction.Failed           +=     e  => { failures++;                    Assert.AreEqual(typeof(TFatal    ), e.GetType()); };
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction);

            Assert.AreEqual(4, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted<T>(Action<ReliableAction> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction reliableAction = new ReliableAction(
                () =>
                {
                    throw new T();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, exhausted = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(T), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => { exhausted++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction);

            Assert.AreEqual(5, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableAction, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

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

    #endregion

    #region ReliableAction<T>

    [TestClass]
    public sealed class ReliableActionTest1 : ReliableDelegateTest
    {
        private static readonly Func<ReliableAction<int>, Action<int>> s_getAction = DynamicGetter.ForField<ReliableAction<int>, Action<int>>("_action");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int>(null, Retries.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int>((arg) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int>((arg) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int>((arg) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

            Action<int> action = (arg) => Console.WriteLine("Hello World");
            ReliableAction<int> actual = new ReliableAction<int>(action, 37, ExceptionPolicies.Transient, DelayPolicies.Constant(115));

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, actualPolicy =>
            {
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 1, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 2, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy(10, new Exception()));
            });
        }

        [TestMethod]
        public void Ctor_ComplexDelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int>(null, Retries.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int>((arg) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int>((arg) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int>((arg) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));

            ComplexDelayPolicy delayPolicy = (i, e) => TimeSpan.Zero;
            Action<int> action = (arg) => Console.WriteLine("Hello World");
            ReliableAction<int> actual = new ReliableAction<int>(action, 37, ExceptionPolicies.Transient, delayPolicy);

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, delayPolicy);
        }

        [TestMethod]
        public void Invoke_NoCancellationToken()
            => Invoke((reliableAction, arg) => reliableAction.Invoke(arg));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableAction, arg) => reliableAction.Invoke(arg, tokenSource.Token));

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg, token) => reliableAction.Invoke(arg, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke((reliableAction, arg) => reliableAction.TryInvoke(arg));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                TryInvoke((reliableAction, arg) => reliableAction.TryInvoke(arg, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg, token) => reliableAction.TryInvoke(arg, token));
        }

        private void Invoke(Action<ReliableAction<int>, int> invoke)
        {
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            Invoke_Failure         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableAction<int>, int> GetFailureAssertion<T>()
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
                    catch (Exception)
                    {
                        Assert.Fail();
                    }
                };
            }
        }

        private void TryInvoke(Func<ReliableAction<int>, int, bool> tryInvoke)
        {
            Invoke_Success                                                 ((r, arg) => Assert.IsTrue (tryInvoke(r, arg)));
            Invoke_EventualSuccess                                         ((r, arg) => Assert.IsTrue (tryInvoke(r, arg)));
            Invoke_Failure         <             InvalidOperationException>((r, arg) => Assert.IsFalse(tryInvoke(r, arg)));
            Invoke_EventualFailure <IOException, InvalidOperationException>((r, arg) => Assert.IsFalse(tryInvoke(r, arg)));
            Invoke_RetriesExhausted<IOException                           >((r, arg) => Assert.IsFalse(tryInvoke(r, arg)));
        }

        private void Invoke_Success(Action<ReliableAction<int>, int> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int> reliableAction = new ReliableAction<int>(
                (arg) => AssertDelegateParameters(arg),
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42);

            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure<T>(Action<ReliableAction<int>, int> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool>          exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<T>((i, e) => TimeSpan.Zero);

            ReliableAction<int> reliableAction = new ReliableAction<int>(
                (arg) =>
                {
                    AssertDelegateParameters(arg);
                    throw new T();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();
            reliableAction.Failed           +=     e  => { failures++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Action<ReliableAction<int>, int> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            ReliableAction<int> reliableAction = new ReliableAction<int>(
                (arg) =>
                {
                    AssertDelegateParameters(arg);
                    eventualSuccess();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(IOException), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42);

            Assert.AreEqual(3, retries);
            Assert.AreEqual(retries, exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure<TTransient, TFatal>(Action<ReliableAction<int>, int> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualFailure = FlakyAction.Create<TTransient, TFatal>(4);
            ReliableAction<int> reliableAction = new ReliableAction<int>(
                (arg) =>
                {
                    AssertDelegateParameters(arg);
                    eventualFailure();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, failures = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(TTransient), e.GetType()); };
            reliableAction.Failed           +=     e  => { failures++;                    Assert.AreEqual(typeof(TFatal    ), e.GetType()); };
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42);

            Assert.AreEqual(4, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted<T>(Action<ReliableAction<int>, int> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int> reliableAction = new ReliableAction<int>(
                (arg) =>
                {
                    AssertDelegateParameters(arg);
                    throw new T();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, exhausted = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(T), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => { exhausted++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42);

            Assert.AreEqual(5, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableAction<int>, int, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int> reliableAction = new ReliableAction<int>(
                (arg) =>
                {
                    AssertDelegateParameters(arg);
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, tokenSource.Token), tokenSource.Token);

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

        private static void AssertDelegateParameters(int arg)
        {
            Assert.AreEqual(42, arg);
        }
    }

    #endregion

    #region ReliableAction<T1, T2>

    [TestClass]
    public sealed class ReliableActionTest2 : ReliableDelegateTest
    {
        private static readonly Func<ReliableAction<int, string>, Action<int, string>> s_getAction = DynamicGetter.ForField<ReliableAction<int, string>, Action<int, string>>("_action");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string>((arg1, arg2) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string>((arg1, arg2) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string>((arg1, arg2) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

            Action<int, string> action = (arg1, arg2) => Console.WriteLine("Hello World");
            ReliableAction<int, string> actual = new ReliableAction<int, string>(action, 37, ExceptionPolicies.Transient, DelayPolicies.Constant(115));

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, actualPolicy =>
            {
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 1, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 2, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy(10, new Exception()));
            });
        }

        [TestMethod]
        public void Ctor_ComplexDelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string>(null, Retries.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string>((arg1, arg2) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string>((arg1, arg2) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string>((arg1, arg2) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));

            ComplexDelayPolicy delayPolicy = (i, e) => TimeSpan.Zero;
            Action<int, string> action = (arg1, arg2) => Console.WriteLine("Hello World");
            ReliableAction<int, string> actual = new ReliableAction<int, string>(action, 37, ExceptionPolicies.Transient, delayPolicy);

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, delayPolicy);
        }

        [TestMethod]
        public void Invoke_NoCancellationToken()
            => Invoke((reliableAction, arg1, arg2) => reliableAction.Invoke(arg1, arg2));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableAction, arg1, arg2) => reliableAction.Invoke(arg1, arg2, tokenSource.Token));

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, token) => reliableAction.Invoke(arg1, arg2, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke((reliableAction, arg1, arg2) => reliableAction.TryInvoke(arg1, arg2));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                TryInvoke((reliableAction, arg1, arg2) => reliableAction.TryInvoke(arg1, arg2, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, token) => reliableAction.TryInvoke(arg1, arg2, token));
        }

        private void Invoke(Action<ReliableAction<int, string>, int, string> invoke)
        {
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            Invoke_Failure         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableAction<int, string>, int, string> GetFailureAssertion<T>()
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
                    catch (Exception)
                    {
                        Assert.Fail();
                    }
                };
            }
        }

        private void TryInvoke(Func<ReliableAction<int, string>, int, string, bool> tryInvoke)
        {
            Invoke_Success                                                 ((r, arg1, arg2) => Assert.IsTrue (tryInvoke(r, arg1, arg2)));
            Invoke_EventualSuccess                                         ((r, arg1, arg2) => Assert.IsTrue (tryInvoke(r, arg1, arg2)));
            Invoke_Failure         <             InvalidOperationException>((r, arg1, arg2) => Assert.IsFalse(tryInvoke(r, arg1, arg2)));
            Invoke_EventualFailure <IOException, InvalidOperationException>((r, arg1, arg2) => Assert.IsFalse(tryInvoke(r, arg1, arg2)));
            Invoke_RetriesExhausted<IOException                           >((r, arg1, arg2) => Assert.IsFalse(tryInvoke(r, arg1, arg2)));
        }

        private void Invoke_Success(Action<ReliableAction<int, string>, int, string> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string> reliableAction = new ReliableAction<int, string>(
                (arg1, arg2) => AssertDelegateParameters(arg1, arg2),
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo");

            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure<T>(Action<ReliableAction<int, string>, int, string> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool>          exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<T>((i, e) => TimeSpan.Zero);

            ReliableAction<int, string> reliableAction = new ReliableAction<int, string>(
                (arg1, arg2) =>
                {
                    AssertDelegateParameters(arg1, arg2);
                    throw new T();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();
            reliableAction.Failed           +=     e  => { failures++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo");

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Action<ReliableAction<int, string>, int, string> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            ReliableAction<int, string> reliableAction = new ReliableAction<int, string>(
                (arg1, arg2) =>
                {
                    AssertDelegateParameters(arg1, arg2);
                    eventualSuccess();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(IOException), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo");

            Assert.AreEqual(3, retries);
            Assert.AreEqual(retries, exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure<TTransient, TFatal>(Action<ReliableAction<int, string>, int, string> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualFailure = FlakyAction.Create<TTransient, TFatal>(4);
            ReliableAction<int, string> reliableAction = new ReliableAction<int, string>(
                (arg1, arg2) =>
                {
                    AssertDelegateParameters(arg1, arg2);
                    eventualFailure();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, failures = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(TTransient), e.GetType()); };
            reliableAction.Failed           +=     e  => { failures++;                    Assert.AreEqual(typeof(TFatal    ), e.GetType()); };
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo");

            Assert.AreEqual(4, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted<T>(Action<ReliableAction<int, string>, int, string> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string> reliableAction = new ReliableAction<int, string>(
                (arg1, arg2) =>
                {
                    AssertDelegateParameters(arg1, arg2);
                    throw new T();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, exhausted = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(T), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => { exhausted++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo");

            Assert.AreEqual(5, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableAction<int, string>, int, string, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string> reliableAction = new ReliableAction<int, string>(
                (arg1, arg2) =>
                {
                    AssertDelegateParameters(arg1, arg2);
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", tokenSource.Token), tokenSource.Token);

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

        private static void AssertDelegateParameters(int arg1, string arg2)
        {
            Assert.AreEqual(42   , arg1);
            Assert.AreEqual("foo", arg2);
        }
    }

    #endregion

    #region ReliableAction<T1, T2, T3>

    [TestClass]
    public sealed class ReliableActionTest3 : ReliableDelegateTest
    {
        private static readonly Func<ReliableAction<int, string, double>, Action<int, string, double>> s_getAction = DynamicGetter.ForField<ReliableAction<int, string, double>, Action<int, string, double>>("_action");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double>(null, Retries.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double>((arg1, arg2, arg3) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double>((arg1, arg2, arg3) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double>((arg1, arg2, arg3) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

            Action<int, string, double> action = (arg1, arg2, arg3) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double> actual = new ReliableAction<int, string, double>(action, 37, ExceptionPolicies.Transient, DelayPolicies.Constant(115));

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, actualPolicy =>
            {
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 1, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 2, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy(10, new Exception()));
            });
        }

        [TestMethod]
        public void Ctor_ComplexDelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double>(null, Retries.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double>((arg1, arg2, arg3) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double>((arg1, arg2, arg3) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double>((arg1, arg2, arg3) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));

            ComplexDelayPolicy delayPolicy = (i, e) => TimeSpan.Zero;
            Action<int, string, double> action = (arg1, arg2, arg3) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double> actual = new ReliableAction<int, string, double>(action, 37, ExceptionPolicies.Transient, delayPolicy);

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, delayPolicy);
        }

        [TestMethod]
        public void Invoke_NoCancellationToken()
            => Invoke((reliableAction, arg1, arg2, arg3) => reliableAction.Invoke(arg1, arg2, arg3));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableAction, arg1, arg2, arg3) => reliableAction.Invoke(arg1, arg2, arg3, tokenSource.Token));

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, token) => reliableAction.Invoke(arg1, arg2, arg3, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke((reliableAction, arg1, arg2, arg3) => reliableAction.TryInvoke(arg1, arg2, arg3));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                TryInvoke((reliableAction, arg1, arg2, arg3) => reliableAction.TryInvoke(arg1, arg2, arg3, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, token) => reliableAction.TryInvoke(arg1, arg2, arg3, token));
        }

        private void Invoke(Action<ReliableAction<int, string, double>, int, string, double> invoke)
        {
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            Invoke_Failure         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableAction<int, string, double>, int, string, double> GetFailureAssertion<T>()
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
                    catch (Exception)
                    {
                        Assert.Fail();
                    }
                };
            }
        }

        private void TryInvoke(Func<ReliableAction<int, string, double>, int, string, double, bool> tryInvoke)
        {
            Invoke_Success                                                 ((r, arg1, arg2, arg3) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3)));
            Invoke_EventualSuccess                                         ((r, arg1, arg2, arg3) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3)));
            Invoke_Failure         <             InvalidOperationException>((r, arg1, arg2, arg3) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3)));
            Invoke_EventualFailure <IOException, InvalidOperationException>((r, arg1, arg2, arg3) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3)));
            Invoke_RetriesExhausted<IOException                           >((r, arg1, arg2, arg3) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3)));
        }

        private void Invoke_Success(Action<ReliableAction<int, string, double>, int, string, double> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double> reliableAction = new ReliableAction<int, string, double>(
                (arg1, arg2, arg3) => AssertDelegateParameters(arg1, arg2, arg3),
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D);

            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure<T>(Action<ReliableAction<int, string, double>, int, string, double> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool>          exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<T>((i, e) => TimeSpan.Zero);

            ReliableAction<int, string, double> reliableAction = new ReliableAction<int, string, double>(
                (arg1, arg2, arg3) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3);
                    throw new T();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();
            reliableAction.Failed           +=     e  => { failures++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Action<ReliableAction<int, string, double>, int, string, double> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            ReliableAction<int, string, double> reliableAction = new ReliableAction<int, string, double>(
                (arg1, arg2, arg3) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3);
                    eventualSuccess();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(IOException), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D);

            Assert.AreEqual(3, retries);
            Assert.AreEqual(retries, exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure<TTransient, TFatal>(Action<ReliableAction<int, string, double>, int, string, double> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualFailure = FlakyAction.Create<TTransient, TFatal>(4);
            ReliableAction<int, string, double> reliableAction = new ReliableAction<int, string, double>(
                (arg1, arg2, arg3) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3);
                    eventualFailure();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, failures = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(TTransient), e.GetType()); };
            reliableAction.Failed           +=     e  => { failures++;                    Assert.AreEqual(typeof(TFatal    ), e.GetType()); };
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D);

            Assert.AreEqual(4, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted<T>(Action<ReliableAction<int, string, double>, int, string, double> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double> reliableAction = new ReliableAction<int, string, double>(
                (arg1, arg2, arg3) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3);
                    throw new T();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, exhausted = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(T), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => { exhausted++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D);

            Assert.AreEqual(5, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableAction<int, string, double>, int, string, double, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double> reliableAction = new ReliableAction<int, string, double>(
                (arg1, arg2, arg3) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3);
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, tokenSource.Token), tokenSource.Token);

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

        private static void AssertDelegateParameters(int arg1, string arg2, double arg3)
        {
            Assert.AreEqual(42   , arg1);
            Assert.AreEqual("foo", arg2);
            Assert.AreEqual(3.14D, arg3);
        }
    }

    #endregion

    #region ReliableAction<T1, T2, T3, T4>

    [TestClass]
    public sealed class ReliableActionTest4 : ReliableDelegateTest
    {
        private static readonly Func<ReliableAction<int, string, double, long>, Action<int, string, double, long>> s_getAction = DynamicGetter.ForField<ReliableAction<int, string, double, long>, Action<int, string, double, long>>("_action");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long>(null, Retries.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long>((arg1, arg2, arg3, arg4) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long>((arg1, arg2, arg3, arg4) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long>((arg1, arg2, arg3, arg4) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

            Action<int, string, double, long> action = (arg1, arg2, arg3, arg4) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long> actual = new ReliableAction<int, string, double, long>(action, 37, ExceptionPolicies.Transient, DelayPolicies.Constant(115));

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, actualPolicy =>
            {
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 1, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 2, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy(10, new Exception()));
            });
        }

        [TestMethod]
        public void Ctor_ComplexDelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long>(null, Retries.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long>((arg1, arg2, arg3, arg4) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long>((arg1, arg2, arg3, arg4) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long>((arg1, arg2, arg3, arg4) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));

            ComplexDelayPolicy delayPolicy = (i, e) => TimeSpan.Zero;
            Action<int, string, double, long> action = (arg1, arg2, arg3, arg4) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long> actual = new ReliableAction<int, string, double, long>(action, 37, ExceptionPolicies.Transient, delayPolicy);

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, delayPolicy);
        }

        [TestMethod]
        public void Invoke_NoCancellationToken()
            => Invoke((reliableAction, arg1, arg2, arg3, arg4) => reliableAction.Invoke(arg1, arg2, arg3, arg4));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableAction, arg1, arg2, arg3, arg4) => reliableAction.Invoke(arg1, arg2, arg3, arg4, tokenSource.Token));

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, token) => reliableAction.Invoke(arg1, arg2, arg3, arg4, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke((reliableAction, arg1, arg2, arg3, arg4) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                TryInvoke((reliableAction, arg1, arg2, arg3, arg4) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, token) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, token));
        }

        private void Invoke(Action<ReliableAction<int, string, double, long>, int, string, double, long> invoke)
        {
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            Invoke_Failure         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableAction<int, string, double, long>, int, string, double, long> GetFailureAssertion<T>()
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
                    catch (Exception)
                    {
                        Assert.Fail();
                    }
                };
            }
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long>, int, string, double, long, bool> tryInvoke)
        {
            Invoke_Success                                                 ((r, arg1, arg2, arg3, arg4) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4)));
            Invoke_EventualSuccess                                         ((r, arg1, arg2, arg3, arg4) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4)));
            Invoke_Failure         <             InvalidOperationException>((r, arg1, arg2, arg3, arg4) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4)));
            Invoke_EventualFailure <IOException, InvalidOperationException>((r, arg1, arg2, arg3, arg4) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4)));
            Invoke_RetriesExhausted<IOException                           >((r, arg1, arg2, arg3, arg4) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4)));
        }

        private void Invoke_Success(Action<ReliableAction<int, string, double, long>, int, string, double, long> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long> reliableAction = new ReliableAction<int, string, double, long>(
                (arg1, arg2, arg3, arg4) => AssertDelegateParameters(arg1, arg2, arg3, arg4),
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L);

            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure<T>(Action<ReliableAction<int, string, double, long>, int, string, double, long> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool>          exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<T>((i, e) => TimeSpan.Zero);

            ReliableAction<int, string, double, long> reliableAction = new ReliableAction<int, string, double, long>(
                (arg1, arg2, arg3, arg4) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4);
                    throw new T();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();
            reliableAction.Failed           +=     e  => { failures++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Action<ReliableAction<int, string, double, long>, int, string, double, long> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            ReliableAction<int, string, double, long> reliableAction = new ReliableAction<int, string, double, long>(
                (arg1, arg2, arg3, arg4) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4);
                    eventualSuccess();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(IOException), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L);

            Assert.AreEqual(3, retries);
            Assert.AreEqual(retries, exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure<TTransient, TFatal>(Action<ReliableAction<int, string, double, long>, int, string, double, long> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualFailure = FlakyAction.Create<TTransient, TFatal>(4);
            ReliableAction<int, string, double, long> reliableAction = new ReliableAction<int, string, double, long>(
                (arg1, arg2, arg3, arg4) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4);
                    eventualFailure();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, failures = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(TTransient), e.GetType()); };
            reliableAction.Failed           +=     e  => { failures++;                    Assert.AreEqual(typeof(TFatal    ), e.GetType()); };
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L);

            Assert.AreEqual(4, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted<T>(Action<ReliableAction<int, string, double, long>, int, string, double, long> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long> reliableAction = new ReliableAction<int, string, double, long>(
                (arg1, arg2, arg3, arg4) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4);
                    throw new T();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, exhausted = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(T), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => { exhausted++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L);

            Assert.AreEqual(5, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableAction<int, string, double, long>, int, string, double, long, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long> reliableAction = new ReliableAction<int, string, double, long>(
                (arg1, arg2, arg3, arg4) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4);
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, tokenSource.Token), tokenSource.Token);

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

        private static void AssertDelegateParameters(int arg1, string arg2, double arg3, long arg4)
        {
            Assert.AreEqual(42   , arg1);
            Assert.AreEqual("foo", arg2);
            Assert.AreEqual(3.14D, arg3);
            Assert.AreEqual(1000L, arg4);
        }
    }

    #endregion

    #region ReliableAction<T1, T2, T3, T4, T5>

    [TestClass]
    public sealed class ReliableActionTest5 : ReliableDelegateTest
    {
        private static readonly Func<ReliableAction<int, string, double, long, ushort>, Action<int, string, double, long, ushort>> s_getAction = DynamicGetter.ForField<ReliableAction<int, string, double, long, ushort>, Action<int, string, double, long, ushort>>("_action");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort>(null, Retries.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort>((arg1, arg2, arg3, arg4, arg5) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort>((arg1, arg2, arg3, arg4, arg5) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort>((arg1, arg2, arg3, arg4, arg5) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

            Action<int, string, double, long, ushort> action = (arg1, arg2, arg3, arg4, arg5) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort> actual = new ReliableAction<int, string, double, long, ushort>(action, 37, ExceptionPolicies.Transient, DelayPolicies.Constant(115));

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, actualPolicy =>
            {
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 1, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 2, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy(10, new Exception()));
            });
        }

        [TestMethod]
        public void Ctor_ComplexDelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort>(null, Retries.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort>((arg1, arg2, arg3, arg4, arg5) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort>((arg1, arg2, arg3, arg4, arg5) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort>((arg1, arg2, arg3, arg4, arg5) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));

            ComplexDelayPolicy delayPolicy = (i, e) => TimeSpan.Zero;
            Action<int, string, double, long, ushort> action = (arg1, arg2, arg3, arg4, arg5) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort> actual = new ReliableAction<int, string, double, long, ushort>(action, 37, ExceptionPolicies.Transient, delayPolicy);

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, delayPolicy);
        }

        [TestMethod]
        public void Invoke_NoCancellationToken()
            => Invoke((reliableAction, arg1, arg2, arg3, arg4, arg5) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableAction, arg1, arg2, arg3, arg4, arg5) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, tokenSource.Token));

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, arg5, token) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke((reliableAction, arg1, arg2, arg3, arg4, arg5) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                TryInvoke((reliableAction, arg1, arg2, arg3, arg4, arg5) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, arg5, token) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, token));
        }

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort>, int, string, double, long, ushort> invoke)
        {
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            Invoke_Failure         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableAction<int, string, double, long, ushort>, int, string, double, long, ushort> GetFailureAssertion<T>()
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
                    catch (Exception)
                    {
                        Assert.Fail();
                    }
                };
            }
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort>, int, string, double, long, ushort, bool> tryInvoke)
        {
            Invoke_Success                                                 ((r, arg1, arg2, arg3, arg4, arg5) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5)));
            Invoke_EventualSuccess                                         ((r, arg1, arg2, arg3, arg4, arg5) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5)));
            Invoke_Failure         <             InvalidOperationException>((r, arg1, arg2, arg3, arg4, arg5) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5)));
            Invoke_EventualFailure <IOException, InvalidOperationException>((r, arg1, arg2, arg3, arg4, arg5) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5)));
            Invoke_RetriesExhausted<IOException                           >((r, arg1, arg2, arg3, arg4, arg5) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5)));
        }

        private void Invoke_Success(Action<ReliableAction<int, string, double, long, ushort>, int, string, double, long, ushort> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort> reliableAction = new ReliableAction<int, string, double, long, ushort>(
                (arg1, arg2, arg3, arg4, arg5) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5),
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1);

            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure<T>(Action<ReliableAction<int, string, double, long, ushort>, int, string, double, long, ushort> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool>          exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<T>((i, e) => TimeSpan.Zero);

            ReliableAction<int, string, double, long, ushort> reliableAction = new ReliableAction<int, string, double, long, ushort>(
                (arg1, arg2, arg3, arg4, arg5) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5);
                    throw new T();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();
            reliableAction.Failed           +=     e  => { failures++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Action<ReliableAction<int, string, double, long, ushort>, int, string, double, long, ushort> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            ReliableAction<int, string, double, long, ushort> reliableAction = new ReliableAction<int, string, double, long, ushort>(
                (arg1, arg2, arg3, arg4, arg5) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5);
                    eventualSuccess();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(IOException), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1);

            Assert.AreEqual(3, retries);
            Assert.AreEqual(retries, exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure<TTransient, TFatal>(Action<ReliableAction<int, string, double, long, ushort>, int, string, double, long, ushort> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualFailure = FlakyAction.Create<TTransient, TFatal>(4);
            ReliableAction<int, string, double, long, ushort> reliableAction = new ReliableAction<int, string, double, long, ushort>(
                (arg1, arg2, arg3, arg4, arg5) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5);
                    eventualFailure();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, failures = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(TTransient), e.GetType()); };
            reliableAction.Failed           +=     e  => { failures++;                    Assert.AreEqual(typeof(TFatal    ), e.GetType()); };
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1);

            Assert.AreEqual(4, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted<T>(Action<ReliableAction<int, string, double, long, ushort>, int, string, double, long, ushort> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort> reliableAction = new ReliableAction<int, string, double, long, ushort>(
                (arg1, arg2, arg3, arg4, arg5) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5);
                    throw new T();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, exhausted = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(T), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => { exhausted++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1);

            Assert.AreEqual(5, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableAction<int, string, double, long, ushort>, int, string, double, long, ushort, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort> reliableAction = new ReliableAction<int, string, double, long, ushort>(
                (arg1, arg2, arg3, arg4, arg5) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5);
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, tokenSource.Token), tokenSource.Token);

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

    #region ReliableAction<T1, T2, T3, T4, T5, T6>

    [TestClass]
    public sealed class ReliableActionTest6 : ReliableDelegateTest
    {
        private static readonly Func<ReliableAction<int, string, double, long, ushort, byte>, Action<int, string, double, long, ushort, byte>> s_getAction = DynamicGetter.ForField<ReliableAction<int, string, double, long, ushort, byte>, Action<int, string, double, long, ushort, byte>>("_action");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte>(null, Retries.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte>((arg1, arg2, arg3, arg4, arg5, arg6) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte>((arg1, arg2, arg3, arg4, arg5, arg6) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte>((arg1, arg2, arg3, arg4, arg5, arg6) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

            Action<int, string, double, long, ushort, byte> action = (arg1, arg2, arg3, arg4, arg5, arg6) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort, byte> actual = new ReliableAction<int, string, double, long, ushort, byte>(action, 37, ExceptionPolicies.Transient, DelayPolicies.Constant(115));

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, actualPolicy =>
            {
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 1, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 2, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy(10, new Exception()));
            });
        }

        [TestMethod]
        public void Ctor_ComplexDelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte>(null, Retries.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte>((arg1, arg2, arg3, arg4, arg5, arg6) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte>((arg1, arg2, arg3, arg4, arg5, arg6) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte>((arg1, arg2, arg3, arg4, arg5, arg6) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));

            ComplexDelayPolicy delayPolicy = (i, e) => TimeSpan.Zero;
            Action<int, string, double, long, ushort, byte> action = (arg1, arg2, arg3, arg4, arg5, arg6) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort, byte> actual = new ReliableAction<int, string, double, long, ushort, byte>(action, 37, ExceptionPolicies.Transient, delayPolicy);

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, delayPolicy);
        }

        [TestMethod]
        public void Invoke_NoCancellationToken()
            => Invoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, tokenSource.Token));

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, token) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                TryInvoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, token) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, token));
        }

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte> invoke)
        {
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            Invoke_Failure         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte> GetFailureAssertion<T>()
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
                    catch (Exception)
                    {
                        Assert.Fail();
                    }
                };
            }
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, bool> tryInvoke)
        {
            Invoke_Success                                                 ((r, arg1, arg2, arg3, arg4, arg5, arg6) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6)));
            Invoke_EventualSuccess                                         ((r, arg1, arg2, arg3, arg4, arg5, arg6) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6)));
            Invoke_Failure         <             InvalidOperationException>((r, arg1, arg2, arg3, arg4, arg5, arg6) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6)));
            Invoke_EventualFailure <IOException, InvalidOperationException>((r, arg1, arg2, arg3, arg4, arg5, arg6) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6)));
            Invoke_RetriesExhausted<IOException                           >((r, arg1, arg2, arg3, arg4, arg5, arg6) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6)));
        }

        private void Invoke_Success(Action<ReliableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte> reliableAction = new ReliableAction<int, string, double, long, ushort, byte>(
                (arg1, arg2, arg3, arg4, arg5, arg6) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6),
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255);

            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure<T>(Action<ReliableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool>          exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<T>((i, e) => TimeSpan.Zero);

            ReliableAction<int, string, double, long, ushort, byte> reliableAction = new ReliableAction<int, string, double, long, ushort, byte>(
                (arg1, arg2, arg3, arg4, arg5, arg6) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6);
                    throw new T();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();
            reliableAction.Failed           +=     e  => { failures++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Action<ReliableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            ReliableAction<int, string, double, long, ushort, byte> reliableAction = new ReliableAction<int, string, double, long, ushort, byte>(
                (arg1, arg2, arg3, arg4, arg5, arg6) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6);
                    eventualSuccess();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(IOException), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255);

            Assert.AreEqual(3, retries);
            Assert.AreEqual(retries, exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure<TTransient, TFatal>(Action<ReliableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualFailure = FlakyAction.Create<TTransient, TFatal>(4);
            ReliableAction<int, string, double, long, ushort, byte> reliableAction = new ReliableAction<int, string, double, long, ushort, byte>(
                (arg1, arg2, arg3, arg4, arg5, arg6) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6);
                    eventualFailure();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, failures = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(TTransient), e.GetType()); };
            reliableAction.Failed           +=     e  => { failures++;                    Assert.AreEqual(typeof(TFatal    ), e.GetType()); };
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255);

            Assert.AreEqual(4, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted<T>(Action<ReliableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte> reliableAction = new ReliableAction<int, string, double, long, ushort, byte>(
                (arg1, arg2, arg3, arg4, arg5, arg6) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6);
                    throw new T();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, exhausted = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(T), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => { exhausted++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255);

            Assert.AreEqual(5, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableAction<int, string, double, long, ushort, byte>, int, string, double, long, ushort, byte, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte> reliableAction = new ReliableAction<int, string, double, long, ushort, byte>(
                (arg1, arg2, arg3, arg4, arg5, arg6) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6);
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, tokenSource.Token), tokenSource.Token);

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

    #region ReliableAction<T1, T2, T3, T4, T5, T6, T7>

    [TestClass]
    public sealed class ReliableActionTest7 : ReliableDelegateTest
    {
        private static readonly Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, Action<int, string, double, long, ushort, byte, TimeSpan>> s_getAction = DynamicGetter.ForField<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, Action<int, string, double, long, ushort, byte, TimeSpan>>("_action");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(null, Retries.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

            Action<int, string, double, long, ushort, byte, TimeSpan> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan> actual = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(action, 37, ExceptionPolicies.Transient, DelayPolicies.Constant(115));

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, actualPolicy =>
            {
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 1, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 2, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy(10, new Exception()));
            });
        }

        [TestMethod]
        public void Ctor_ComplexDelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(null, Retries.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));

            ComplexDelayPolicy delayPolicy = (i, e) => TimeSpan.Zero;
            Action<int, string, double, long, ushort, byte, TimeSpan> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan> actual = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(action, 37, ExceptionPolicies.Transient, delayPolicy);

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, delayPolicy);
        }

        [TestMethod]
        public void Invoke_NoCancellationToken()
            => Invoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, tokenSource.Token));

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, token) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                TryInvoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, token) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, token));
        }

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, int, string, double, long, ushort, byte, TimeSpan> invoke)
        {
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            Invoke_Failure         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, int, string, double, long, ushort, byte, TimeSpan> GetFailureAssertion<T>()
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
                    catch (Exception)
                    {
                        Assert.Fail();
                    }
                };
            }
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, int, string, double, long, ushort, byte, TimeSpan, bool> tryInvoke)
        {
            Invoke_Success                                                 ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7)));
            Invoke_EventualSuccess                                         ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7)));
            Invoke_Failure         <             InvalidOperationException>((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7)));
            Invoke_EventualFailure <IOException, InvalidOperationException>((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7)));
            Invoke_RetriesExhausted<IOException                           >((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7)));
        }

        private void Invoke_Success(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, int, string, double, long, ushort, byte, TimeSpan> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7),
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30));

            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure<T>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, int, string, double, long, ushort, byte, TimeSpan> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool>          exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<T>((i, e) => TimeSpan.Zero);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                    throw new T();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();
            reliableAction.Failed           +=     e  => { failures++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30));

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, int, string, double, long, ushort, byte, TimeSpan> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                    eventualSuccess();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(IOException), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30));

            Assert.AreEqual(3, retries);
            Assert.AreEqual(retries, exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure<TTransient, TFatal>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, int, string, double, long, ushort, byte, TimeSpan> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualFailure = FlakyAction.Create<TTransient, TFatal>(4);
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                    eventualFailure();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, failures = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(TTransient), e.GetType()); };
            reliableAction.Failed           +=     e  => { failures++;                    Assert.AreEqual(typeof(TFatal    ), e.GetType()); };
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30));

            Assert.AreEqual(4, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted<T>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, int, string, double, long, ushort, byte, TimeSpan> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                    throw new T();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, exhausted = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(T), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => { exhausted++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30));

            Assert.AreEqual(5, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, int, string, double, long, ushort, byte, TimeSpan, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), tokenSource.Token), tokenSource.Token);

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

    #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8>

    [TestClass]
    public sealed class ReliableActionTest8 : ReliableDelegateTest
    {
        private static readonly Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>, Action<int, string, double, long, ushort, byte, TimeSpan, uint>> s_getAction = DynamicGetter.ForField<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>, Action<int, string, double, long, ushort, byte, TimeSpan, uint>>("_action");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(null, Retries.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

            Action<int, string, double, long, ushort, byte, TimeSpan, uint> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint> actual = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(action, 37, ExceptionPolicies.Transient, DelayPolicies.Constant(115));

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, actualPolicy =>
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
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));

            ComplexDelayPolicy delayPolicy = (i, e) => TimeSpan.Zero;
            Action<int, string, double, long, ushort, byte, TimeSpan, uint> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint> actual = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(action, 37, ExceptionPolicies.Transient, delayPolicy);

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, delayPolicy);
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
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

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
            ObservableFunc<Exception, bool>          exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
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
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualSuccess = FlakyAction.Create<IOException>(3);
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
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(IOException), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U);

            Assert.AreEqual(3, retries);
            Assert.AreEqual(retries, exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure<TTransient, TFatal>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>, int, string, double, long, ushort, byte, TimeSpan, uint> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualFailure = FlakyAction.Create<TTransient, TFatal>(4);
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                    eventualFailure();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, failures = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(TTransient), e.GetType()); };
            reliableAction.Failed           +=     e  => { failures++;                    Assert.AreEqual(typeof(TFatal    ), e.GetType()); };
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U);

            Assert.AreEqual(4, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted<T>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>, int, string, double, long, ushort, byte, TimeSpan, uint> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                    throw new T();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, exhausted = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(T), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => { exhausted++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U);

            Assert.AreEqual(5, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>, int, string, double, long, ushort, byte, TimeSpan, uint, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

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

    #endregion

    #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9>

    [TestClass]
    public sealed class ReliableActionTest9 : ReliableDelegateTest
    {
        private static readonly Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> s_getAction = DynamicGetter.ForField<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>>("_action");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(null, Retries.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

            Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> actual = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(action, 37, ExceptionPolicies.Transient, DelayPolicies.Constant(115));

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, actualPolicy =>
            {
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 1, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 2, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy(10, new Exception()));
            });
        }

        [TestMethod]
        public void Ctor_ComplexDelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(null, Retries.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));

            ComplexDelayPolicy delayPolicy = (i, e) => TimeSpan.Zero;
            Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> actual = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(action, 37, ExceptionPolicies.Transient, delayPolicy);

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, delayPolicy);
        }

        [TestMethod]
        public void Invoke_NoCancellationToken()
            => Invoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, tokenSource.Token));

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                TryInvoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token));
        }

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> invoke)
        {
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            Invoke_Failure         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> GetFailureAssertion<T>()
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
                    catch (Exception)
                    {
                        Assert.Fail();
                    }
                };
            }
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, bool> tryInvoke)
        {
            Invoke_Success                                                 ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9)));
            Invoke_EventualSuccess                                         ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9)));
            Invoke_Failure         <             InvalidOperationException>((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9)));
            Invoke_EventualFailure <IOException, InvalidOperationException>((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9)));
            Invoke_RetriesExhausted<IOException                           >((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9)));
        }

        private void Invoke_Success(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9),
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL));

            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure<T>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool>          exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<T>((i, e) => TimeSpan.Zero);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                    throw new T();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();
            reliableAction.Failed           +=     e  => { failures++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL));

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                    eventualSuccess();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(IOException), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL));

            Assert.AreEqual(3, retries);
            Assert.AreEqual(retries, exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure<TTransient, TFatal>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualFailure = FlakyAction.Create<TTransient, TFatal>(4);
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                    eventualFailure();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, failures = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(TTransient), e.GetType()); };
            reliableAction.Failed           +=     e  => { failures++;                    Assert.AreEqual(typeof(TFatal    ), e.GetType()); };
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL));

            Assert.AreEqual(4, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted<T>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                    throw new T();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, exhausted = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(T), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => { exhausted++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL));

            Assert.AreEqual(5, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), tokenSource.Token), tokenSource.Token);

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

    #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>

    [TestClass]
    public sealed class ReliableActionTest10 : ReliableDelegateTest
    {
        private static readonly Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>> s_getAction = DynamicGetter.ForField<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>>("_action");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(null, Retries.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

            Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> actual = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(action, 37, ExceptionPolicies.Transient, DelayPolicies.Constant(115));

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, actualPolicy =>
            {
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 1, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 2, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy(10, new Exception()));
            });
        }

        [TestMethod]
        public void Ctor_ComplexDelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(null, Retries.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));

            ComplexDelayPolicy delayPolicy = (i, e) => TimeSpan.Zero;
            Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> actual = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(action, 37, ExceptionPolicies.Transient, delayPolicy);

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, delayPolicy);
        }

        [TestMethod]
        public void Invoke_NoCancellationToken()
            => Invoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, tokenSource.Token));

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, token) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                TryInvoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, token) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, token));
        }

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> invoke)
        {
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            Invoke_Failure         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> GetFailureAssertion<T>()
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
                    catch (Exception)
                    {
                        Assert.Fail();
                    }
                };
            }
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, bool> tryInvoke)
        {
            Invoke_Success                                                 ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10)));
            Invoke_EventualSuccess                                         ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10)));
            Invoke_Failure         <             InvalidOperationException>((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10)));
            Invoke_EventualFailure <IOException, InvalidOperationException>((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10)));
            Invoke_RetriesExhausted<IOException                           >((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10)));
        }

        private void Invoke_Success(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10),
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06));

            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure<T>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool>          exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<T>((i, e) => TimeSpan.Zero);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                    throw new T();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();
            reliableAction.Failed           +=     e  => { failures++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06));

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                    eventualSuccess();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(IOException), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06));

            Assert.AreEqual(3, retries);
            Assert.AreEqual(retries, exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure<TTransient, TFatal>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualFailure = FlakyAction.Create<TTransient, TFatal>(4);
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                    eventualFailure();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, failures = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(TTransient), e.GetType()); };
            reliableAction.Failed           +=     e  => { failures++;                    Assert.AreEqual(typeof(TFatal    ), e.GetType()); };
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06));

            Assert.AreEqual(4, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted<T>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                    throw new T();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, exhausted = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(T), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => { exhausted++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06));

            Assert.AreEqual(5, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), tokenSource.Token), tokenSource.Token);

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

    #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>

    [TestClass]
    public sealed class ReliableActionTest11 : ReliableDelegateTest
    {
        private static readonly Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>, Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>> s_getAction = DynamicGetter.ForField<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>, Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>>("_action");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>(null, Retries.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

            Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> actual = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>(action, 37, ExceptionPolicies.Transient, DelayPolicies.Constant(115));

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, actualPolicy =>
            {
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 1, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 2, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy(10, new Exception()));
            });
        }

        [TestMethod]
        public void Ctor_ComplexDelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>(null, Retries.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));

            ComplexDelayPolicy delayPolicy = (i, e) => TimeSpan.Zero;
            Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> actual = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>(action, 37, ExceptionPolicies.Transient, delayPolicy);

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, delayPolicy);
        }

        [TestMethod]
        public void Invoke_NoCancellationToken()
            => Invoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, tokenSource.Token));

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, token) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                TryInvoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, token) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, token));
        }

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> invoke)
        {
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            Invoke_Failure         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> GetFailureAssertion<T>()
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
                    catch (Exception)
                    {
                        Assert.Fail();
                    }
                };
            }
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, bool> tryInvoke)
        {
            Invoke_Success                                                 ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11)));
            Invoke_EventualSuccess                                         ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11)));
            Invoke_Failure         <             InvalidOperationException>((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11)));
            Invoke_EventualFailure <IOException, InvalidOperationException>((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11)));
            Invoke_RetriesExhausted<IOException                           >((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11)));
        }

        private void Invoke_Success(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11),
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL);

            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure<T>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool>          exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<T>((i, e) => TimeSpan.Zero);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                    throw new T();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();
            reliableAction.Failed           +=     e  => { failures++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                    eventualSuccess();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(IOException), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL);

            Assert.AreEqual(3, retries);
            Assert.AreEqual(retries, exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure<TTransient, TFatal>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualFailure = FlakyAction.Create<TTransient, TFatal>(4);
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                    eventualFailure();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, failures = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(TTransient), e.GetType()); };
            reliableAction.Failed           +=     e  => { failures++;                    Assert.AreEqual(typeof(TFatal    ), e.GetType()); };
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL);

            Assert.AreEqual(4, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted<T>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                    throw new T();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, exhausted = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(T), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => { exhausted++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL);

            Assert.AreEqual(5, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, tokenSource.Token), tokenSource.Token);

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

    #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>

    [TestClass]
    public sealed class ReliableActionTest12 : ReliableDelegateTest
    {
        private static readonly Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>> s_getAction = DynamicGetter.ForField<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>>("_action");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(null, Retries.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

            Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> actual = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(action, 37, ExceptionPolicies.Transient, DelayPolicies.Constant(115));

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, actualPolicy =>
            {
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 1, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 2, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy(10, new Exception()));
            });
        }

        [TestMethod]
        public void Ctor_ComplexDelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(null, Retries.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));

            ComplexDelayPolicy delayPolicy = (i, e) => TimeSpan.Zero;
            Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> actual = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(action, 37, ExceptionPolicies.Transient, delayPolicy);

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, delayPolicy);
        }

        [TestMethod]
        public void Invoke_NoCancellationToken()
            => Invoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, tokenSource.Token));

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                TryInvoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token));
        }

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> invoke)
        {
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            Invoke_Failure         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> GetFailureAssertion<T>()
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
                    catch (Exception)
                    {
                        Assert.Fail();
                    }
                };
            }
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, bool> tryInvoke)
        {
            Invoke_Success                                                 ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12)));
            Invoke_EventualSuccess                                         ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12)));
            Invoke_Failure         <             InvalidOperationException>((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12)));
            Invoke_EventualFailure <IOException, InvalidOperationException>((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12)));
            Invoke_RetriesExhausted<IOException                           >((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12)));
        }

        private void Invoke_Success(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12),
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7);

            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure<T>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool>          exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<T>((i, e) => TimeSpan.Zero);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
                    throw new T();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();
            reliableAction.Failed           +=     e  => { failures++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
                    eventualSuccess();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(IOException), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7);

            Assert.AreEqual(3, retries);
            Assert.AreEqual(retries, exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure<TTransient, TFatal>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualFailure = FlakyAction.Create<TTransient, TFatal>(4);
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
                    eventualFailure();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, failures = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(TTransient), e.GetType()); };
            reliableAction.Failed           +=     e  => { failures++;                    Assert.AreEqual(typeof(TFatal    ), e.GetType()); };
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7);

            Assert.AreEqual(4, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted<T>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
                    throw new T();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, exhausted = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(T), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => { exhausted++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7);

            Assert.AreEqual(5, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, tokenSource.Token), tokenSource.Token);

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

    #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>

    [TestClass]
    public sealed class ReliableActionTest13 : ReliableDelegateTest
    {
        private static readonly Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>> s_getAction = DynamicGetter.ForField<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>>("_action");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(null, Retries.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

            Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> actual = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(action, 37, ExceptionPolicies.Transient, DelayPolicies.Constant(115));

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, actualPolicy =>
            {
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 1, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 2, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy(10, new Exception()));
            });
        }

        [TestMethod]
        public void Ctor_ComplexDelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(null, Retries.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));

            ComplexDelayPolicy delayPolicy = (i, e) => TimeSpan.Zero;
            Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> actual = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(action, 37, ExceptionPolicies.Transient, delayPolicy);

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, delayPolicy);
        }

        [TestMethod]
        public void Invoke_NoCancellationToken()
            => Invoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, tokenSource.Token));

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                TryInvoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token));
        }

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> invoke)
        {
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            Invoke_Failure         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> GetFailureAssertion<T>()
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
                    catch (Exception)
                    {
                        Assert.Fail();
                    }
                };
            }
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, bool> tryInvoke)
        {
            Invoke_Success                                                 ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13)));
            Invoke_EventualSuccess                                         ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13)));
            Invoke_Failure         <             InvalidOperationException>((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13)));
            Invoke_EventualFailure <IOException, InvalidOperationException>((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13)));
            Invoke_RetriesExhausted<IOException                           >((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13)));
        }

        private void Invoke_Success(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13),
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M);

            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure<T>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool>          exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<T>((i, e) => TimeSpan.Zero);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                    throw new T();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();
            reliableAction.Failed           +=     e  => { failures++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                    eventualSuccess();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(IOException), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M);

            Assert.AreEqual(3, retries);
            Assert.AreEqual(retries, exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure<TTransient, TFatal>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualFailure = FlakyAction.Create<TTransient, TFatal>(4);
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                    eventualFailure();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, failures = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(TTransient), e.GetType()); };
            reliableAction.Failed           +=     e  => { failures++;                    Assert.AreEqual(typeof(TFatal    ), e.GetType()); };
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M);

            Assert.AreEqual(4, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted<T>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                    throw new T();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, exhausted = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(T), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => { exhausted++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M);

            Assert.AreEqual(5, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, tokenSource.Token), tokenSource.Token);

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

    #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>

    [TestClass]
    public sealed class ReliableActionTest14 : ReliableDelegateTest
    {
        private static readonly Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>, Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>> s_getAction = DynamicGetter.ForField<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>, Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>>("_action");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(null, Retries.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

            Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> actual = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(action, 37, ExceptionPolicies.Transient, DelayPolicies.Constant(115));

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, actualPolicy =>
            {
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 1, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 2, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy(10, new Exception()));
            });
        }

        [TestMethod]
        public void Ctor_ComplexDelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(null, Retries.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));

            ComplexDelayPolicy delayPolicy = (i, e) => TimeSpan.Zero;
            Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> actual = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(action, 37, ExceptionPolicies.Transient, delayPolicy);

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, delayPolicy);
        }

        [TestMethod]
        public void Invoke_NoCancellationToken()
            => Invoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, tokenSource.Token));

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                TryInvoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token));
        }

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> invoke)
        {
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            Invoke_Failure         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> GetFailureAssertion<T>()
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
                    catch (Exception)
                    {
                        Assert.Fail();
                    }
                };
            }
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, bool> tryInvoke)
        {
            Invoke_Success                                                 ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14)));
            Invoke_EventualSuccess                                         ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14)));
            Invoke_Failure         <             InvalidOperationException>((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14)));
            Invoke_EventualFailure <IOException, InvalidOperationException>((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14)));
            Invoke_RetriesExhausted<IOException                           >((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14)));
        }

        private void Invoke_Success(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14),
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!');

            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure<T>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool>          exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<T>((i, e) => TimeSpan.Zero);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                    throw new T();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();
            reliableAction.Failed           +=     e  => { failures++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!');

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                    eventualSuccess();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(IOException), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!');

            Assert.AreEqual(3, retries);
            Assert.AreEqual(retries, exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure<TTransient, TFatal>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualFailure = FlakyAction.Create<TTransient, TFatal>(4);
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                    eventualFailure();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, failures = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(TTransient), e.GetType()); };
            reliableAction.Failed           +=     e  => { failures++;                    Assert.AreEqual(typeof(TFatal    ), e.GetType()); };
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!');

            Assert.AreEqual(4, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted<T>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                    throw new T();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, exhausted = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(T), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => { exhausted++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!');

            Assert.AreEqual(5, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', tokenSource.Token), tokenSource.Token);

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

    #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>

    [TestClass]
    public sealed class ReliableActionTest15 : ReliableDelegateTest
    {
        private static readonly Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>, Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>> s_getAction = DynamicGetter.ForField<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>, Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>>("_action");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(null, Retries.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

            Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> actual = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(action, 37, ExceptionPolicies.Transient, DelayPolicies.Constant(115));

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, actualPolicy =>
            {
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 1, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 2, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy(10, new Exception()));
            });
        }

        [TestMethod]
        public void Ctor_ComplexDelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(null, Retries.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));

            ComplexDelayPolicy delayPolicy = (i, e) => TimeSpan.Zero;
            Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> actual = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(action, 37, ExceptionPolicies.Transient, delayPolicy);

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, delayPolicy);
        }

        [TestMethod]
        public void Invoke_NoCancellationToken()
            => Invoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, tokenSource.Token));

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                TryInvoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token));
        }

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> invoke)
        {
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            Invoke_Failure         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> GetFailureAssertion<T>()
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
                    catch (Exception)
                    {
                        Assert.Fail();
                    }
                };
            }
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, bool> tryInvoke)
        {
            Invoke_Success                                                 ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15)));
            Invoke_EventualSuccess                                         ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15)));
            Invoke_Failure         <             InvalidOperationException>((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15)));
            Invoke_EventualFailure <IOException, InvalidOperationException>((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15)));
            Invoke_RetriesExhausted<IOException                           >((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15)));
        }

        private void Invoke_Success(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15),
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F);

            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure<T>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool>          exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<T>((i, e) => TimeSpan.Zero);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                    throw new T();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();
            reliableAction.Failed           +=     e  => { failures++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F);

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                    eventualSuccess();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(IOException), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F);

            Assert.AreEqual(3, retries);
            Assert.AreEqual(retries, exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure<TTransient, TFatal>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualFailure = FlakyAction.Create<TTransient, TFatal>(4);
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                    eventualFailure();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, failures = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(TTransient), e.GetType()); };
            reliableAction.Failed           +=     e  => { failures++;                    Assert.AreEqual(typeof(TFatal    ), e.GetType()); };
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F);

            Assert.AreEqual(4, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted<T>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                    throw new T();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, exhausted = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(T), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => { exhausted++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F);

            Assert.AreEqual(5, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, tokenSource.Token), tokenSource.Token);

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

    #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>

    [TestClass]
    public sealed class ReliableActionTest16 : ReliableDelegateTest
    {
        private static readonly Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>, Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>> s_getAction = DynamicGetter.ForField<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>, Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>>("_action");

        [TestMethod]
        public void Ctor_DelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(null, Retries.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

            Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> actual = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(action, 37, ExceptionPolicies.Transient, DelayPolicies.Constant(115));

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, actualPolicy =>
            {
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 1, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy( 2, new Exception()));
                Assert.AreEqual(TimeSpan.FromMilliseconds(115), actualPolicy(10, new Exception()));
            });
        }

        [TestMethod]
        public void Ctor_ComplexDelayPolicy()
        {
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(null, Retries.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => Console.WriteLine("Hello World"), Retries.Infinite, null                   , (i, e) => TimeSpan.Zero ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => Console.WriteLine("Hello World"), Retries.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));

            ComplexDelayPolicy delayPolicy = (i, e) => TimeSpan.Zero;
            Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> action = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => Console.WriteLine("Hello World");
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> actual = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(action, 37, ExceptionPolicies.Transient, delayPolicy);

            Assert.AreSame(action, s_getAction(actual));
            Ctor(actual, 37, ExceptionPolicies.Transient, delayPolicy);
        }

        [TestMethod]
        public void Invoke_NoCancellationToken()
            => Invoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Invoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, tokenSource.Token));

            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, token) => reliableAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, token));
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                TryInvoke((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, tokenSource.Token));
        
            // Ensure CancellationToken prevents additional retry
            Invoke_Canceled((reliableAction, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, token) => reliableAction.TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, token));
        }

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> invoke)
        {
            Invoke_Success        (invoke);
            Invoke_EventualSuccess(invoke);

            Invoke_Failure         <             InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_EventualFailure <IOException, InvalidOperationException>(GetFailureAssertion<InvalidOperationException>());
            Invoke_RetriesExhausted<IOException                           >(GetFailureAssertion<IOException              >());

            Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> GetFailureAssertion<T>()
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
                    catch (Exception)
                    {
                        Assert.Fail();
                    }
                };
            }
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, bool> tryInvoke)
        {
            Invoke_Success                                                 ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16)));
            Invoke_EventualSuccess                                         ((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => Assert.IsTrue (tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16)));
            Invoke_Failure         <             InvalidOperationException>((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16)));
            Invoke_EventualFailure <IOException, InvalidOperationException>((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16)));
            Invoke_RetriesExhausted<IOException                           >((r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => Assert.IsFalse(tryInvoke(r, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16)));
        }

        private void Invoke_Success(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16),
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, Guid.Parse("53710ff0-eaa3-4fac-a068-e5be641d446b"));

            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_Failure<T>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool>          exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Fail<T>());
            ObservableFunc<int, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create<T>((i, e) => TimeSpan.Zero);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
                    throw new T();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int failures = 0;
            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();
            reliableAction.Failed           +=     e  => { failures++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, Guid.Parse("53710ff0-eaa3-4fac-a068-e5be641d446b"));

            Assert.AreEqual(1, failures);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void Invoke_EventualSuccess(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> assertInvoke)
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
                    eventualSuccess();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(IOException), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, Guid.Parse("53710ff0-eaa3-4fac-a068-e5be641d446b"));

            Assert.AreEqual(3, retries);
            Assert.AreEqual(retries, exceptionPolicy.Calls);
            Assert.AreEqual(retries, delayPolicy    .Calls);
        }

        private void Invoke_EventualFailure<TTransient, TFatal>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> assertInvoke)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TTransient, TFatal>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            Action eventualFailure = FlakyAction.Create<TTransient, TFatal>(4);
            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
                    eventualFailure();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, failures = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(TTransient), e.GetType()); };
            reliableAction.Failed           +=     e  => { failures++;                    Assert.AreEqual(typeof(TFatal    ), e.GetType()); };
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, Guid.Parse("53710ff0-eaa3-4fac-a068-e5be641d446b"));

            Assert.AreEqual(4, retries );
            Assert.AreEqual(1, failures);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_RetriesExhausted<T>(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> assertInvoke)
            where T : Exception, new()
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<T>(ExceptionPolicies.Retry<T>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
                    throw new T();
                },
                5,
                exceptionPolicy.Invoke,
                delayPolicy    .Invoke);

            int retries = 0, exhausted = 0;
            reliableAction.Retrying         += (i, e) => { Assert.AreEqual(++retries, i); Assert.AreEqual(typeof(T), e.GetType()); };
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => { exhausted++; Assert.AreEqual(typeof(T), e.GetType()); };

            assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, Guid.Parse("53710ff0-eaa3-4fac-a068-e5be641d446b"));

            Assert.AreEqual(5, retries  );
            Assert.AreEqual(1, exhausted);
            Assert.AreEqual(retries + 1, exceptionPolicy.Calls);
            Assert.AreEqual(retries    , delayPolicy    .Calls);
        }

        private void Invoke_Canceled(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, CancellationToken> assertInvoke)
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<IOException>(ExceptionPolicies.Retry<IOException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid> reliableAction = new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                {
                    AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
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
            Task invocation = Task.Run(() => assertInvoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, Guid.Parse("53710ff0-eaa3-4fac-a068-e5be641d446b"), tokenSource.Token), tokenSource.Token);

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
