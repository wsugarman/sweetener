// Generated from ReliableAction.Test.tt
using System;
using System.IO;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    #region ReliableAction

    [TestClass]
    public sealed class ReliableActionTest : BaseReliableActionTest
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
            => Invoke(reliableAction => reliableAction.Invoke());

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Action<ReliableAction> invokeReliableAction = reliableAction => reliableAction.Invoke(cancellationTokenSource.Token);

                Invoke(invokeReliableAction);

                Invoke_Canceled(
                    new ReliableAction(
                        () =>
                        {
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    invokeReliableAction,
                    cancellationTokenSource);
            }
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke(reliableAction => reliableAction.TryInvoke());

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Func<ReliableAction, bool> tryInvokeReliableAction = reliableAction => reliableAction.TryInvoke(cancellationTokenSource.Token);

                TryInvoke(tryInvokeReliableAction);

                TryInvoke_Canceled(
                    new ReliableAction(
                        () =>
                        {
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    tryInvokeReliableAction,
                    cancellationTokenSource);
            }
        }

        private void Invoke(Action<ReliableAction> invoke)
        {
            // Immediate Success
            Invoke_Success(
                new ReliableAction(
                    () => Console.WriteLine("Success"),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            Invoke_Failure<ReliableAction, InvalidOperationException>(
                new ReliableAction(
                    () =>
                    {
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            Invoke_EventualSuccess(
                new ReliableAction(
                    () =>
                    {
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            Invoke_EventualFailure<ReliableAction, InvalidOperationException>(
                new ReliableAction(
                    () =>
                    {
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            Invoke_RetriesExhausted<ReliableAction, IOException>(
                new ReliableAction(
                    () =>
                    {
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
        }

        private void TryInvoke(Func<ReliableAction, bool> invoke)
        {
            // Immediate Success
            TryInvoke_Success(
                new ReliableAction(
                    () => Console.WriteLine("Success"),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            TryInvoke_Failure<ReliableAction, InvalidOperationException>(
                new ReliableAction(
                    () =>
                    {
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            TryInvoke_EventualSuccess(
                new ReliableAction(
                    () =>
                    {
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            TryInvoke_EventualFailure<ReliableAction, InvalidOperationException>(
                new ReliableAction(
                    () =>
                    {
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            TryInvoke_RetriesExhausted<ReliableAction, IOException>(
                new ReliableAction(
                    () =>
                    {
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
        }
    }

    #endregion

    #region ReliableAction<T>

    [TestClass]
    public sealed class ReliableActionTest1 : BaseReliableActionTest
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
            => Invoke(reliableAction => reliableAction.Invoke(42));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Action<ReliableAction<int>> invokeReliableAction = reliableAction => reliableAction.Invoke(42, cancellationTokenSource.Token);

                Invoke(invokeReliableAction);

                Invoke_Canceled(
                    new ReliableAction<int>(
                        (arg) =>
                        {
                            AssertDelegateParameters(arg);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    invokeReliableAction,
                    cancellationTokenSource);
            }
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke(reliableAction => reliableAction.TryInvoke(42));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Func<ReliableAction<int>, bool> tryInvokeReliableAction = reliableAction => reliableAction.TryInvoke(42, cancellationTokenSource.Token);

                TryInvoke(tryInvokeReliableAction);

                TryInvoke_Canceled(
                    new ReliableAction<int>(
                        (arg) =>
                        {
                            AssertDelegateParameters(arg);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    tryInvokeReliableAction,
                    cancellationTokenSource);
            }
        }

        private void Invoke(Action<ReliableAction<int>> invoke)
        {
            // Immediate Success
            Invoke_Success(
                new ReliableAction<int>(
                    (arg) => AssertDelegateParameters(arg),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            Invoke_Failure<ReliableAction<int>, InvalidOperationException>(
                new ReliableAction<int>(
                    (arg) =>
                    {
                        AssertDelegateParameters(arg);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            Invoke_EventualSuccess(
                new ReliableAction<int>(
                    (arg) =>
                    {
                        AssertDelegateParameters(arg);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            Invoke_EventualFailure<ReliableAction<int>, InvalidOperationException>(
                new ReliableAction<int>(
                    (arg) =>
                    {
                        AssertDelegateParameters(arg);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            Invoke_RetriesExhausted<ReliableAction<int>, IOException>(
                new ReliableAction<int>(
                    (arg) =>
                    {
                        AssertDelegateParameters(arg);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
        }

        private void TryInvoke(Func<ReliableAction<int>, bool> invoke)
        {
            // Immediate Success
            TryInvoke_Success(
                new ReliableAction<int>(
                    (arg) => AssertDelegateParameters(arg),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            TryInvoke_Failure<ReliableAction<int>, InvalidOperationException>(
                new ReliableAction<int>(
                    (arg) =>
                    {
                        AssertDelegateParameters(arg);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            TryInvoke_EventualSuccess(
                new ReliableAction<int>(
                    (arg) =>
                    {
                        AssertDelegateParameters(arg);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            TryInvoke_EventualFailure<ReliableAction<int>, InvalidOperationException>(
                new ReliableAction<int>(
                    (arg) =>
                    {
                        AssertDelegateParameters(arg);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            TryInvoke_RetriesExhausted<ReliableAction<int>, IOException>(
                new ReliableAction<int>(
                    (arg) =>
                    {
                        AssertDelegateParameters(arg);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
        }

        private static void AssertDelegateParameters(int arg)
        {
            Assert.AreEqual(42, arg);
        }
    }

    #endregion

    #region ReliableAction<T1, T2>

    [TestClass]
    public sealed class ReliableActionTest2 : BaseReliableActionTest
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
            => Invoke(reliableAction => reliableAction.Invoke(42, "foo"));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Action<ReliableAction<int, string>> invokeReliableAction = reliableAction => reliableAction.Invoke(42, "foo", cancellationTokenSource.Token);

                Invoke(invokeReliableAction);

                Invoke_Canceled(
                    new ReliableAction<int, string>(
                        (arg1, arg2) =>
                        {
                            AssertDelegateParameters(arg1, arg2);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    invokeReliableAction,
                    cancellationTokenSource);
            }
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke(reliableAction => reliableAction.TryInvoke(42, "foo"));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Func<ReliableAction<int, string>, bool> tryInvokeReliableAction = reliableAction => reliableAction.TryInvoke(42, "foo", cancellationTokenSource.Token);

                TryInvoke(tryInvokeReliableAction);

                TryInvoke_Canceled(
                    new ReliableAction<int, string>(
                        (arg1, arg2) =>
                        {
                            AssertDelegateParameters(arg1, arg2);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    tryInvokeReliableAction,
                    cancellationTokenSource);
            }
        }

        private void Invoke(Action<ReliableAction<int, string>> invoke)
        {
            // Immediate Success
            Invoke_Success(
                new ReliableAction<int, string>(
                    (arg1, arg2) => AssertDelegateParameters(arg1, arg2),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            Invoke_Failure<ReliableAction<int, string>, InvalidOperationException>(
                new ReliableAction<int, string>(
                    (arg1, arg2) =>
                    {
                        AssertDelegateParameters(arg1, arg2);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            Invoke_EventualSuccess(
                new ReliableAction<int, string>(
                    (arg1, arg2) =>
                    {
                        AssertDelegateParameters(arg1, arg2);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            Invoke_EventualFailure<ReliableAction<int, string>, InvalidOperationException>(
                new ReliableAction<int, string>(
                    (arg1, arg2) =>
                    {
                        AssertDelegateParameters(arg1, arg2);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            Invoke_RetriesExhausted<ReliableAction<int, string>, IOException>(
                new ReliableAction<int, string>(
                    (arg1, arg2) =>
                    {
                        AssertDelegateParameters(arg1, arg2);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
        }

        private void TryInvoke(Func<ReliableAction<int, string>, bool> invoke)
        {
            // Immediate Success
            TryInvoke_Success(
                new ReliableAction<int, string>(
                    (arg1, arg2) => AssertDelegateParameters(arg1, arg2),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            TryInvoke_Failure<ReliableAction<int, string>, InvalidOperationException>(
                new ReliableAction<int, string>(
                    (arg1, arg2) =>
                    {
                        AssertDelegateParameters(arg1, arg2);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            TryInvoke_EventualSuccess(
                new ReliableAction<int, string>(
                    (arg1, arg2) =>
                    {
                        AssertDelegateParameters(arg1, arg2);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            TryInvoke_EventualFailure<ReliableAction<int, string>, InvalidOperationException>(
                new ReliableAction<int, string>(
                    (arg1, arg2) =>
                    {
                        AssertDelegateParameters(arg1, arg2);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            TryInvoke_RetriesExhausted<ReliableAction<int, string>, IOException>(
                new ReliableAction<int, string>(
                    (arg1, arg2) =>
                    {
                        AssertDelegateParameters(arg1, arg2);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
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
    public sealed class ReliableActionTest3 : BaseReliableActionTest
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
            => Invoke(reliableAction => reliableAction.Invoke(42, "foo", 3.14D));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Action<ReliableAction<int, string, double>> invokeReliableAction = reliableAction => reliableAction.Invoke(42, "foo", 3.14D, cancellationTokenSource.Token);

                Invoke(invokeReliableAction);

                Invoke_Canceled(
                    new ReliableAction<int, string, double>(
                        (arg1, arg2, arg3) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    invokeReliableAction,
                    cancellationTokenSource);
            }
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke(reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Func<ReliableAction<int, string, double>, bool> tryInvokeReliableAction = reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, cancellationTokenSource.Token);

                TryInvoke(tryInvokeReliableAction);

                TryInvoke_Canceled(
                    new ReliableAction<int, string, double>(
                        (arg1, arg2, arg3) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    tryInvokeReliableAction,
                    cancellationTokenSource);
            }
        }

        private void Invoke(Action<ReliableAction<int, string, double>> invoke)
        {
            // Immediate Success
            Invoke_Success(
                new ReliableAction<int, string, double>(
                    (arg1, arg2, arg3) => AssertDelegateParameters(arg1, arg2, arg3),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            Invoke_Failure<ReliableAction<int, string, double>, InvalidOperationException>(
                new ReliableAction<int, string, double>(
                    (arg1, arg2, arg3) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            Invoke_EventualSuccess(
                new ReliableAction<int, string, double>(
                    (arg1, arg2, arg3) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            Invoke_EventualFailure<ReliableAction<int, string, double>, InvalidOperationException>(
                new ReliableAction<int, string, double>(
                    (arg1, arg2, arg3) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            Invoke_RetriesExhausted<ReliableAction<int, string, double>, IOException>(
                new ReliableAction<int, string, double>(
                    (arg1, arg2, arg3) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
        }

        private void TryInvoke(Func<ReliableAction<int, string, double>, bool> invoke)
        {
            // Immediate Success
            TryInvoke_Success(
                new ReliableAction<int, string, double>(
                    (arg1, arg2, arg3) => AssertDelegateParameters(arg1, arg2, arg3),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            TryInvoke_Failure<ReliableAction<int, string, double>, InvalidOperationException>(
                new ReliableAction<int, string, double>(
                    (arg1, arg2, arg3) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            TryInvoke_EventualSuccess(
                new ReliableAction<int, string, double>(
                    (arg1, arg2, arg3) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            TryInvoke_EventualFailure<ReliableAction<int, string, double>, InvalidOperationException>(
                new ReliableAction<int, string, double>(
                    (arg1, arg2, arg3) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            TryInvoke_RetriesExhausted<ReliableAction<int, string, double>, IOException>(
                new ReliableAction<int, string, double>(
                    (arg1, arg2, arg3) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
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
    public sealed class ReliableActionTest4 : BaseReliableActionTest
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
            => Invoke(reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Action<ReliableAction<int, string, double, long>> invokeReliableAction = reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, cancellationTokenSource.Token);

                Invoke(invokeReliableAction);

                Invoke_Canceled(
                    new ReliableAction<int, string, double, long>(
                        (arg1, arg2, arg3, arg4) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    invokeReliableAction,
                    cancellationTokenSource);
            }
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke(reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Func<ReliableAction<int, string, double, long>, bool> tryInvokeReliableAction = reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, cancellationTokenSource.Token);

                TryInvoke(tryInvokeReliableAction);

                TryInvoke_Canceled(
                    new ReliableAction<int, string, double, long>(
                        (arg1, arg2, arg3, arg4) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    tryInvokeReliableAction,
                    cancellationTokenSource);
            }
        }

        private void Invoke(Action<ReliableAction<int, string, double, long>> invoke)
        {
            // Immediate Success
            Invoke_Success(
                new ReliableAction<int, string, double, long>(
                    (arg1, arg2, arg3, arg4) => AssertDelegateParameters(arg1, arg2, arg3, arg4),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            Invoke_Failure<ReliableAction<int, string, double, long>, InvalidOperationException>(
                new ReliableAction<int, string, double, long>(
                    (arg1, arg2, arg3, arg4) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            Invoke_EventualSuccess(
                new ReliableAction<int, string, double, long>(
                    (arg1, arg2, arg3, arg4) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            Invoke_EventualFailure<ReliableAction<int, string, double, long>, InvalidOperationException>(
                new ReliableAction<int, string, double, long>(
                    (arg1, arg2, arg3, arg4) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            Invoke_RetriesExhausted<ReliableAction<int, string, double, long>, IOException>(
                new ReliableAction<int, string, double, long>(
                    (arg1, arg2, arg3, arg4) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long>, bool> invoke)
        {
            // Immediate Success
            TryInvoke_Success(
                new ReliableAction<int, string, double, long>(
                    (arg1, arg2, arg3, arg4) => AssertDelegateParameters(arg1, arg2, arg3, arg4),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            TryInvoke_Failure<ReliableAction<int, string, double, long>, InvalidOperationException>(
                new ReliableAction<int, string, double, long>(
                    (arg1, arg2, arg3, arg4) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            TryInvoke_EventualSuccess(
                new ReliableAction<int, string, double, long>(
                    (arg1, arg2, arg3, arg4) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            TryInvoke_EventualFailure<ReliableAction<int, string, double, long>, InvalidOperationException>(
                new ReliableAction<int, string, double, long>(
                    (arg1, arg2, arg3, arg4) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            TryInvoke_RetriesExhausted<ReliableAction<int, string, double, long>, IOException>(
                new ReliableAction<int, string, double, long>(
                    (arg1, arg2, arg3, arg4) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
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
    public sealed class ReliableActionTest5 : BaseReliableActionTest
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
            => Invoke(reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, (ushort)1));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Action<ReliableAction<int, string, double, long, ushort>> invokeReliableAction = reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, (ushort)1, cancellationTokenSource.Token);

                Invoke(invokeReliableAction);

                Invoke_Canceled(
                    new ReliableAction<int, string, double, long, ushort>(
                        (arg1, arg2, arg3, arg4, arg5) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    invokeReliableAction,
                    cancellationTokenSource);
            }
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke(reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, (ushort)1));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Func<ReliableAction<int, string, double, long, ushort>, bool> tryInvokeReliableAction = reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, (ushort)1, cancellationTokenSource.Token);

                TryInvoke(tryInvokeReliableAction);

                TryInvoke_Canceled(
                    new ReliableAction<int, string, double, long, ushort>(
                        (arg1, arg2, arg3, arg4, arg5) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    tryInvokeReliableAction,
                    cancellationTokenSource);
            }
        }

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort>> invoke)
        {
            // Immediate Success
            Invoke_Success(
                new ReliableAction<int, string, double, long, ushort>(
                    (arg1, arg2, arg3, arg4, arg5) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            Invoke_Failure<ReliableAction<int, string, double, long, ushort>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort>(
                    (arg1, arg2, arg3, arg4, arg5) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            Invoke_EventualSuccess(
                new ReliableAction<int, string, double, long, ushort>(
                    (arg1, arg2, arg3, arg4, arg5) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            Invoke_EventualFailure<ReliableAction<int, string, double, long, ushort>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort>(
                    (arg1, arg2, arg3, arg4, arg5) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            Invoke_RetriesExhausted<ReliableAction<int, string, double, long, ushort>, IOException>(
                new ReliableAction<int, string, double, long, ushort>(
                    (arg1, arg2, arg3, arg4, arg5) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort>, bool> invoke)
        {
            // Immediate Success
            TryInvoke_Success(
                new ReliableAction<int, string, double, long, ushort>(
                    (arg1, arg2, arg3, arg4, arg5) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            TryInvoke_Failure<ReliableAction<int, string, double, long, ushort>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort>(
                    (arg1, arg2, arg3, arg4, arg5) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            TryInvoke_EventualSuccess(
                new ReliableAction<int, string, double, long, ushort>(
                    (arg1, arg2, arg3, arg4, arg5) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            TryInvoke_EventualFailure<ReliableAction<int, string, double, long, ushort>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort>(
                    (arg1, arg2, arg3, arg4, arg5) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            TryInvoke_RetriesExhausted<ReliableAction<int, string, double, long, ushort>, IOException>(
                new ReliableAction<int, string, double, long, ushort>(
                    (arg1, arg2, arg3, arg4, arg5) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
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
    public sealed class ReliableActionTest6 : BaseReliableActionTest
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
            => Invoke(reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Action<ReliableAction<int, string, double, long, ushort, byte>> invokeReliableAction = reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, cancellationTokenSource.Token);

                Invoke(invokeReliableAction);

                Invoke_Canceled(
                    new ReliableAction<int, string, double, long, ushort, byte>(
                        (arg1, arg2, arg3, arg4, arg5, arg6) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    invokeReliableAction,
                    cancellationTokenSource);
            }
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke(reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Func<ReliableAction<int, string, double, long, ushort, byte>, bool> tryInvokeReliableAction = reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, cancellationTokenSource.Token);

                TryInvoke(tryInvokeReliableAction);

                TryInvoke_Canceled(
                    new ReliableAction<int, string, double, long, ushort, byte>(
                        (arg1, arg2, arg3, arg4, arg5, arg6) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    tryInvokeReliableAction,
                    cancellationTokenSource);
            }
        }

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort, byte>> invoke)
        {
            // Immediate Success
            Invoke_Success(
                new ReliableAction<int, string, double, long, ushort, byte>(
                    (arg1, arg2, arg3, arg4, arg5, arg6) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            Invoke_Failure<ReliableAction<int, string, double, long, ushort, byte>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte>(
                    (arg1, arg2, arg3, arg4, arg5, arg6) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            Invoke_EventualSuccess(
                new ReliableAction<int, string, double, long, ushort, byte>(
                    (arg1, arg2, arg3, arg4, arg5, arg6) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            Invoke_EventualFailure<ReliableAction<int, string, double, long, ushort, byte>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte>(
                    (arg1, arg2, arg3, arg4, arg5, arg6) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            Invoke_RetriesExhausted<ReliableAction<int, string, double, long, ushort, byte>, IOException>(
                new ReliableAction<int, string, double, long, ushort, byte>(
                    (arg1, arg2, arg3, arg4, arg5, arg6) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort, byte>, bool> invoke)
        {
            // Immediate Success
            TryInvoke_Success(
                new ReliableAction<int, string, double, long, ushort, byte>(
                    (arg1, arg2, arg3, arg4, arg5, arg6) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            TryInvoke_Failure<ReliableAction<int, string, double, long, ushort, byte>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte>(
                    (arg1, arg2, arg3, arg4, arg5, arg6) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            TryInvoke_EventualSuccess(
                new ReliableAction<int, string, double, long, ushort, byte>(
                    (arg1, arg2, arg3, arg4, arg5, arg6) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            TryInvoke_EventualFailure<ReliableAction<int, string, double, long, ushort, byte>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte>(
                    (arg1, arg2, arg3, arg4, arg5, arg6) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            TryInvoke_RetriesExhausted<ReliableAction<int, string, double, long, ushort, byte>, IOException>(
                new ReliableAction<int, string, double, long, ushort, byte>(
                    (arg1, arg2, arg3, arg4, arg5, arg6) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
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
    public sealed class ReliableActionTest7 : BaseReliableActionTest
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
            => Invoke(reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30)));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>> invokeReliableAction = reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), cancellationTokenSource.Token);

                Invoke(invokeReliableAction);

                Invoke_Canceled(
                    new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(
                        (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    invokeReliableAction,
                    cancellationTokenSource);
            }
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke(reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30)));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, bool> tryInvokeReliableAction = reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), cancellationTokenSource.Token);

                TryInvoke(tryInvokeReliableAction);

                TryInvoke_Canceled(
                    new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(
                        (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    tryInvokeReliableAction,
                    cancellationTokenSource);
            }
        }

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>> invoke)
        {
            // Immediate Success
            Invoke_Success(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            Invoke_Failure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            Invoke_EventualSuccess(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            Invoke_EventualFailure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            Invoke_RetriesExhausted<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, IOException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, bool> invoke)
        {
            // Immediate Success
            TryInvoke_Success(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            TryInvoke_Failure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            TryInvoke_EventualSuccess(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            TryInvoke_EventualFailure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            TryInvoke_RetriesExhausted<ReliableAction<int, string, double, long, ushort, byte, TimeSpan>, IOException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
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
    public sealed class ReliableActionTest8 : BaseReliableActionTest
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
            => Invoke(reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>> invokeReliableAction = reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, cancellationTokenSource.Token);

                Invoke(invokeReliableAction);

                Invoke_Canceled(
                    new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
                        (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    invokeReliableAction,
                    cancellationTokenSource);
            }
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke(reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>, bool> tryInvokeReliableAction = reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, cancellationTokenSource.Token);

                TryInvoke(tryInvokeReliableAction);

                TryInvoke_Canceled(
                    new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
                        (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    tryInvokeReliableAction,
                    cancellationTokenSource);
            }
        }

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>> invoke)
        {
            // Immediate Success
            Invoke_Success(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            Invoke_Failure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            Invoke_EventualSuccess(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            Invoke_EventualFailure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            Invoke_RetriesExhausted<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>, IOException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>, bool> invoke)
        {
            // Immediate Success
            TryInvoke_Success(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            TryInvoke_Failure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            TryInvoke_EventualSuccess(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            TryInvoke_EventualFailure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            TryInvoke_RetriesExhausted<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>, IOException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
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
    public sealed class ReliableActionTest9 : BaseReliableActionTest
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
            => Invoke(reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL)));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> invokeReliableAction = reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), cancellationTokenSource.Token);

                Invoke(invokeReliableAction);

                Invoke_Canceled(
                    new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                        (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    invokeReliableAction,
                    cancellationTokenSource);
            }
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke(reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL)));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, bool> tryInvokeReliableAction = reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), cancellationTokenSource.Token);

                TryInvoke(tryInvokeReliableAction);

                TryInvoke_Canceled(
                    new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                        (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    tryInvokeReliableAction,
                    cancellationTokenSource);
            }
        }

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> invoke)
        {
            // Immediate Success
            Invoke_Success(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            Invoke_Failure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            Invoke_EventualSuccess(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            Invoke_EventualFailure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            Invoke_RetriesExhausted<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, IOException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, bool> invoke)
        {
            // Immediate Success
            TryInvoke_Success(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            TryInvoke_Failure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            TryInvoke_EventualSuccess(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            TryInvoke_EventualFailure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            TryInvoke_RetriesExhausted<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, IOException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
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
    public sealed class ReliableActionTest10 : BaseReliableActionTest
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
            => Invoke(reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06)));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>> invokeReliableAction = reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), cancellationTokenSource.Token);

                Invoke(invokeReliableAction);

                Invoke_Canceled(
                    new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(
                        (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    invokeReliableAction,
                    cancellationTokenSource);
            }
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke(reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06)));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, bool> tryInvokeReliableAction = reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), cancellationTokenSource.Token);

                TryInvoke(tryInvokeReliableAction);

                TryInvoke_Canceled(
                    new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(
                        (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    tryInvokeReliableAction,
                    cancellationTokenSource);
            }
        }

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>> invoke)
        {
            // Immediate Success
            Invoke_Success(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            Invoke_Failure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            Invoke_EventualSuccess(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            Invoke_EventualFailure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            Invoke_RetriesExhausted<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, IOException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, bool> invoke)
        {
            // Immediate Success
            TryInvoke_Success(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            TryInvoke_Failure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            TryInvoke_EventualSuccess(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            TryInvoke_EventualFailure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            TryInvoke_RetriesExhausted<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>, IOException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
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
    public sealed class ReliableActionTest11 : BaseReliableActionTest
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
            => Invoke(reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>> invokeReliableAction = reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, cancellationTokenSource.Token);

                Invoke(invokeReliableAction);

                Invoke_Canceled(
                    new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>(
                        (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    invokeReliableAction,
                    cancellationTokenSource);
            }
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke(reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>, bool> tryInvokeReliableAction = reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, cancellationTokenSource.Token);

                TryInvoke(tryInvokeReliableAction);

                TryInvoke_Canceled(
                    new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>(
                        (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    tryInvokeReliableAction,
                    cancellationTokenSource);
            }
        }

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>> invoke)
        {
            // Immediate Success
            Invoke_Success(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            Invoke_Failure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            Invoke_EventualSuccess(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            Invoke_EventualFailure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            Invoke_RetriesExhausted<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>, IOException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>, bool> invoke)
        {
            // Immediate Success
            TryInvoke_Success(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            TryInvoke_Failure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            TryInvoke_EventualSuccess(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            TryInvoke_EventualFailure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            TryInvoke_RetriesExhausted<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>, IOException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
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
    public sealed class ReliableActionTest12 : BaseReliableActionTest
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
            => Invoke(reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>> invokeReliableAction = reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, cancellationTokenSource.Token);

                Invoke(invokeReliableAction);

                Invoke_Canceled(
                    new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(
                        (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    invokeReliableAction,
                    cancellationTokenSource);
            }
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke(reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, bool> tryInvokeReliableAction = reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, cancellationTokenSource.Token);

                TryInvoke(tryInvokeReliableAction);

                TryInvoke_Canceled(
                    new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(
                        (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    tryInvokeReliableAction,
                    cancellationTokenSource);
            }
        }

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>> invoke)
        {
            // Immediate Success
            Invoke_Success(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            Invoke_Failure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            Invoke_EventualSuccess(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            Invoke_EventualFailure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            Invoke_RetriesExhausted<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, IOException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, bool> invoke)
        {
            // Immediate Success
            TryInvoke_Success(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            TryInvoke_Failure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            TryInvoke_EventualSuccess(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            TryInvoke_EventualFailure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            TryInvoke_RetriesExhausted<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>, IOException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
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
    public sealed class ReliableActionTest13 : BaseReliableActionTest
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
            => Invoke(reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>> invokeReliableAction = reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, cancellationTokenSource.Token);

                Invoke(invokeReliableAction);

                Invoke_Canceled(
                    new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                        (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    invokeReliableAction,
                    cancellationTokenSource);
            }
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke(reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, bool> tryInvokeReliableAction = reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, cancellationTokenSource.Token);

                TryInvoke(tryInvokeReliableAction);

                TryInvoke_Canceled(
                    new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                        (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    tryInvokeReliableAction,
                    cancellationTokenSource);
            }
        }

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>> invoke)
        {
            // Immediate Success
            Invoke_Success(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            Invoke_Failure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            Invoke_EventualSuccess(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            Invoke_EventualFailure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            Invoke_RetriesExhausted<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, IOException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, bool> invoke)
        {
            // Immediate Success
            TryInvoke_Success(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            TryInvoke_Failure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            TryInvoke_EventualSuccess(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            TryInvoke_EventualFailure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            TryInvoke_RetriesExhausted<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>, IOException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
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
    public sealed class ReliableActionTest14 : BaseReliableActionTest
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
            => Invoke(reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!'));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>> invokeReliableAction = reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', cancellationTokenSource.Token);

                Invoke(invokeReliableAction);

                Invoke_Canceled(
                    new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(
                        (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    invokeReliableAction,
                    cancellationTokenSource);
            }
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke(reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!'));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>, bool> tryInvokeReliableAction = reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', cancellationTokenSource.Token);

                TryInvoke(tryInvokeReliableAction);

                TryInvoke_Canceled(
                    new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(
                        (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    tryInvokeReliableAction,
                    cancellationTokenSource);
            }
        }

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>> invoke)
        {
            // Immediate Success
            Invoke_Success(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            Invoke_Failure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            Invoke_EventualSuccess(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            Invoke_EventualFailure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            Invoke_RetriesExhausted<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>, IOException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>, bool> invoke)
        {
            // Immediate Success
            TryInvoke_Success(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            TryInvoke_Failure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            TryInvoke_EventualSuccess(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            TryInvoke_EventualFailure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            TryInvoke_RetriesExhausted<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>, IOException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
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
    public sealed class ReliableActionTest15 : BaseReliableActionTest
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
            => Invoke(reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>> invokeReliableAction = reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, cancellationTokenSource.Token);

                Invoke(invokeReliableAction);

                Invoke_Canceled(
                    new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(
                        (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    invokeReliableAction,
                    cancellationTokenSource);
            }
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke(reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>, bool> tryInvokeReliableAction = reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, cancellationTokenSource.Token);

                TryInvoke(tryInvokeReliableAction);

                TryInvoke_Canceled(
                    new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(
                        (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    tryInvokeReliableAction,
                    cancellationTokenSource);
            }
        }

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>> invoke)
        {
            // Immediate Success
            Invoke_Success(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            Invoke_Failure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            Invoke_EventualSuccess(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            Invoke_EventualFailure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            Invoke_RetriesExhausted<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>, IOException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>, bool> invoke)
        {
            // Immediate Success
            TryInvoke_Success(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            TryInvoke_Failure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            TryInvoke_EventualSuccess(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            TryInvoke_EventualFailure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            TryInvoke_RetriesExhausted<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>, IOException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
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
    public sealed class ReliableActionTest16 : BaseReliableActionTest
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
            => Invoke(reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, Guid.Parse("53710ff0-eaa3-4fac-a068-e5be641d446b")));

        [TestMethod]
        public void Invoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>> invokeReliableAction = reliableAction => reliableAction.Invoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, Guid.Parse("53710ff0-eaa3-4fac-a068-e5be641d446b"), cancellationTokenSource.Token);

                Invoke(invokeReliableAction);

                Invoke_Canceled(
                    new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(
                        (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    invokeReliableAction,
                    cancellationTokenSource);
            }
        }

        [TestMethod]
        public void TryInvoke_NoCancellationToken()
            => TryInvoke(reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, Guid.Parse("53710ff0-eaa3-4fac-a068-e5be641d446b")));

        [TestMethod]
        public void TryInvoke_CancellationToken()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>, bool> tryInvokeReliableAction = reliableAction => reliableAction.TryInvoke(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, '!', 0.1F, Guid.Parse("53710ff0-eaa3-4fac-a068-e5be641d446b"), cancellationTokenSource.Token);

                TryInvoke(tryInvokeReliableAction);

                TryInvoke_Canceled(
                    new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(
                        (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                        {
                            AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
                            throw new IOException();
                        },
                        Retries.Infinite,
                        ExceptionPolicies.Retry<IOException>(),
                        DelayPolicies.Constant(5)),
                    tryInvokeReliableAction,
                    cancellationTokenSource);
            }
        }

        private void Invoke(Action<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>> invoke)
        {
            // Immediate Success
            Invoke_Success(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            Invoke_Failure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            Invoke_EventualSuccess(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            Invoke_EventualFailure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            Invoke_RetriesExhausted<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>, IOException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
        }

        private void TryInvoke(Func<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>, bool> invoke)
        {
            // Immediate Success
            TryInvoke_Success(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16),
                    5,
                    ExceptionPolicies.Fatal,
                    DelayPolicies.None),
                invoke);

            // Immediate Failure
            TryInvoke_Failure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
                        throw new InvalidOperationException();
                    },
                    5,
                    ExceptionPolicies.Fail<InvalidOperationException>(),
                    DelayPolicies.None),
                invoke);

            // Eventual Success
            Action eventualSuccess = FlakyAction.Create<IOException>(3);
            TryInvoke_EventualSuccess(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
                        eventualSuccess();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                3);

            // Eventual Success
            Action eventualFailure = FlakyAction.Create<IOException, InvalidOperationException>(4);
            TryInvoke_EventualFailure<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>, InvalidOperationException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
                        eventualFailure();
                    },
                    5,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                4);

            // Retries Exhausted
            TryInvoke_RetriesExhausted<ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>, IOException>(
                new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(
                    (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
                    {
                        AssertDelegateParameters(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
                        throw new IOException();
                    },
                    7,
                    ExceptionPolicies.Retry<IOException>(),
                    DelayPolicies.None),
                invoke,
                7);
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
