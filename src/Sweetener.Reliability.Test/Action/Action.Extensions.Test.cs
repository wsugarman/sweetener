// Generated from ReliableAction.Extensions.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    [TestClass]
    public partial class ActionExtensionsTest
    {
        [TestMethod]
        public void WithRetry_DelayPolicy()
        {
            Func<Action, int, ExceptionPolicy, DelayPolicy, InterruptableAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d);

            WithRetry_DelayPolicy_Success         (withRetry);
            WithRetry_DelayPolicy_Failure         (withRetry);
            WithRetry_DelayPolicy_EventualSuccess (withRetry);
            WithRetry_DelayPolicy_EventualFailure (withRetry);
            WithRetry_DelayPolicy_RetriesExhausted(withRetry);
        }

        [TestMethod]
        public void WithRetry_DelayPolicy_CancellationToken()
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            Func<Action, int, ExceptionPolicy, DelayPolicy, InterruptableAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d);

            WithRetry_DelayPolicy_Success         (withRetry, useToken: true);
            WithRetry_DelayPolicy_Failure         (withRetry, useToken: true);
            WithRetry_DelayPolicy_EventualSuccess (withRetry, useToken: true);
            WithRetry_DelayPolicy_EventualFailure (withRetry, useToken: true);
            WithRetry_DelayPolicy_RetriesExhausted(withRetry, useToken: true);
            WithRetry_DelayPolicy_Canceled        (withRetry);
        }

        [TestMethod]
        public void WithRetry_ComplexDelayPolicy()
        {
            Func<Action, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d);

            WithRetry_ComplexDelayPolicy_Success         (withRetry);
            WithRetry_ComplexDelayPolicy_Failure         (withRetry);
            WithRetry_ComplexDelayPolicy_EventualSuccess (withRetry);
            WithRetry_ComplexDelayPolicy_EventualFailure (withRetry);
            WithRetry_ComplexDelayPolicy_RetriesExhausted(withRetry);
        }

        [TestMethod]
        public void WithRetry_ComplexDelayPolicy_CancellationToken()
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            Func<Action, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d);

            WithRetry_ComplexDelayPolicy_Success         (withRetry, useToken: true);
            WithRetry_ComplexDelayPolicy_Failure         (withRetry, useToken: true);
            WithRetry_ComplexDelayPolicy_EventualSuccess (withRetry, useToken: true);
            WithRetry_ComplexDelayPolicy_EventualFailure (withRetry, useToken: true);
            WithRetry_ComplexDelayPolicy_RetriesExhausted(withRetry, useToken: true);
            WithRetry_ComplexDelayPolicy_Canceled        (withRetry);
        }

        private void WithRetry_DelayPolicy_Success(Func<Action, int, ExceptionPolicy, DelayPolicy, InterruptableAction> withRetry, bool useToken = false)
            => WithRetry_Success((a, r, e, d) => withRetry(a, r, e, d.Invoke), () => PolicyValidator.IgnoreDelayPolicy(), useToken);

        private void WithRetry_ComplexDelayPolicy_Success(Func<Action, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction> withRetry, bool useToken = false)
            => WithRetry_Success((a, r, e, d) => withRetry(a, r, e, d.Invoke), () => PolicyValidator.IgnoreComplexDelayPolicy(), useToken);

        private void WithRetry_Success<T>(Func<Action, int, ExceptionPolicy, T, InterruptableAction> withRetry, Func<T> delayPolicyFactory, bool useToken = false)
            where T : ObservableFunc
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.IgnoreExceptionPolicy();
            T delayPolicy = delayPolicyFactory();

            InterruptableAction reliableAction = withRetry(
                () => Operation.Null(),
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            if (useToken)
            {
                using CancellationTokenSource tokenSource = new CancellationTokenSource();
                reliableAction(tokenSource.Token);
            }
            else
            {
                reliableAction();
            }

            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void WithRetry_DelayPolicy_Failure(Func<Action, int, ExceptionPolicy, DelayPolicy, InterruptableAction> withRetry, bool useToken = false)
            => WithRetry_Failure((a, r, e, d) => withRetry(a, r, e, d.Invoke), () => PolicyValidator.IgnoreDelayPolicy(), useToken);

        private void WithRetry_ComplexDelayPolicy_Failure(Func<Action, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction> withRetry, bool useToken = false)
            => WithRetry_Failure((a, r, e, d) => withRetry(a, r, e, d.Invoke), () => PolicyValidator.IgnoreComplexDelayPolicy(), useToken);

        private void WithRetry_Failure<T>(Func<Action, int, ExceptionPolicy, T, InterruptableAction> withRetry, Func<T> delayPolicyFactory, bool useToken = false)
            where T : ObservableFunc
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<FormatException>(ExceptionPolicies.Fail<FormatException>());
            T delayPolicy = delayPolicyFactory();

            InterruptableAction reliableAction = withRetry(
                () =>
                {
                    throw new FormatException();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            try
            {
                if (useToken)
                {
                    using CancellationTokenSource tokenSource = new CancellationTokenSource();
                    reliableAction(tokenSource.Token);
                }
                else
                {
                    reliableAction();
                }

                Assert.Fail();
            }
            catch (FormatException)
            { }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void WithRetry_DelayPolicy_EventualSuccess(Func<Action, int, ExceptionPolicy, DelayPolicy, InterruptableAction> withRetry, bool useToken = false)
            => WithRetry_EventualSuccess<IOException, ObservableFunc<int, TimeSpan>>((a, r, e, d) => withRetry(a, r, e, d.Invoke), t => PolicyValidator.Create(DelayPolicies.Constant(t)), useToken);

        private void WithRetry_ComplexDelayPolicy_EventualSuccess(Func<Action, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction> withRetry, bool useToken = false)
            => WithRetry_EventualSuccess<IOException, ObservableFunc<int, Exception, TimeSpan>>((a, r, e, d) => withRetry(a, r, e, d.Invoke), t => PolicyValidator.Create<IOException>((i, e) => t), useToken);

        private void WithRetry_EventualSuccess<TException, TFunc>(Func<Action, int, ExceptionPolicy, TFunc, InterruptableAction> withRetry, Func<TimeSpan, TFunc> delayPolicyFactory, bool useToken = false)
            where TException : Exception, new()
            where TFunc      : ObservableFunc
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TException>(ExceptionPolicies.Retry<TException>());
            TFunc delayPolicy = delayPolicyFactory(Constants.Delay);

            Action eventualSuccess = FlakyAction.Create<TException>(1);
            InterruptableAction reliableAction = withRetry(
                () =>
                {
                    eventualSuccess();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            DateTime delayStartUtc = DateTime.UtcNow;
            if (useToken)
            {
                using CancellationTokenSource tokenSource = new CancellationTokenSource();
                reliableAction(tokenSource.Token);
            }
            else
            {
                reliableAction();
            }
            TimeSpan delay = DateTime.UtcNow - delayStartUtc;

            Assert.IsTrue(delay > Constants.MinDelay);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(1, delayPolicy    .Calls);
        }

        private void WithRetry_DelayPolicy_EventualFailure(Func<Action, int, ExceptionPolicy, DelayPolicy, InterruptableAction> withRetry, bool useToken = false)
            => WithRetry_EventualFailure<IOException, ObservableFunc<int, TimeSpan>>((a, r, e, d) => withRetry(a, r, e, d.Invoke), t => PolicyValidator.Create(DelayPolicies.Constant(t)), useToken);

        private void WithRetry_ComplexDelayPolicy_EventualFailure(Func<Action, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction> withRetry, bool useToken = false)
            => WithRetry_EventualFailure<IOException, ObservableFunc<int, Exception, TimeSpan>>((a, r, e, d) => withRetry(a, r, e, d.Invoke), t => PolicyValidator.Create<IOException>((i, e) => t), useToken);

        private void WithRetry_EventualFailure<TTransient, TFunc>(Func<Action, int, ExceptionPolicy, TFunc, InterruptableAction> withRetry, Func<TimeSpan, TFunc> delayPolicyFactory, bool useToken = false)
            where TTransient : Exception, new()
            where TFunc      : ObservableFunc
        {
            Assert.AreNotEqual(typeof(TTransient), typeof(InvalidOperationException));

            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TTransient, InvalidOperationException>(ExceptionPolicies.Retry<TTransient>());
            TFunc delayPolicy = delayPolicyFactory(Constants.Delay);

            Action eventualFailure = FlakyAction.Create<TTransient, InvalidOperationException>(2);
            InterruptableAction reliableAction = withRetry(
                () =>
                {
                    eventualFailure();
                },
                4,
                exceptionPolicy.Invoke,
                delayPolicy);

            DateTime delayStartUtc = DateTime.UtcNow;

            try
            {
                if (useToken)
                {
                    using CancellationTokenSource tokenSource = new CancellationTokenSource();
                    reliableAction(tokenSource.Token);
                }
                else
                {
                    reliableAction();
                }

                Assert.Fail();
            }
            catch (InvalidOperationException)
            { }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            TimeSpan delay = DateTime.UtcNow - delayStartUtc;
            Assert.IsTrue(delay > TimeSpan.FromMilliseconds(Constants.Delay.TotalMilliseconds * 2 * Constants.MinFactor));

            Assert.AreEqual(3, exceptionPolicy.Calls);
            Assert.AreEqual(2, delayPolicy    .Calls);
        }

        private void WithRetry_DelayPolicy_RetriesExhausted(Func<Action, int, ExceptionPolicy, DelayPolicy, InterruptableAction> withRetry, bool useToken = false)
            => WithRetry_RetriesExhausted<IOException, ObservableFunc<int, TimeSpan>>((a, r, e, d) => withRetry(a, r, e, d.Invoke), t => PolicyValidator.Create(DelayPolicies.Constant(t)), useToken);

        private void WithRetry_ComplexDelayPolicy_RetriesExhausted(Func<Action, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction> withRetry, bool useToken = false)
            => WithRetry_RetriesExhausted<IOException, ObservableFunc<int, Exception, TimeSpan>>((a, r, e, d) => withRetry(a, r, e, d.Invoke), t => PolicyValidator.Create<IOException>((i, e) => t), useToken);

        private void WithRetry_RetriesExhausted<TException, TFunc>(Func<Action, int, ExceptionPolicy, TFunc, InterruptableAction> withRetry, Func<TimeSpan, TFunc> delayPolicyFactory, bool useToken = false)
            where TException : Exception, new()
            where TFunc      : ObservableFunc
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TException>(ExceptionPolicies.Retry<TException>());
            TFunc delayPolicy = delayPolicyFactory(Constants.Delay);

            InterruptableAction reliableAction = withRetry(
                () =>
                {
                    throw new TException();
                },
                2,
                exceptionPolicy.Invoke,
                delayPolicy);

            try
            {
                if (useToken)
                {
                    using CancellationTokenSource tokenSource = new CancellationTokenSource();
                    reliableAction(tokenSource.Token);
                }
                else
                {
                    reliableAction();
                }

                Assert.Fail();
            }
            catch (TException)
            { }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            TimeSpan delay = DateTime.UtcNow - delayStartUtc;
            Assert.IsTrue(delay > TimeSpan.FromMilliseconds(Constants.Delay.TotalMilliseconds * 2 * Constants.MinFactor));

            Assert.AreEqual(3, exceptionPolicy.Calls);
            Assert.AreEqual(2, delayPolicy    .Calls);
        }

        private void WithRetry_DelayPolicy_Canceled(Func<Action, int, ExceptionPolicy, DelayPolicy, InterruptableAction> withRetry)
            => WithRetry_Canceled<IOException, ObservableFunc<int, TimeSpan>>((a, r, e, d) => withRetry(a, r, e, d.Invoke), t => PolicyValidator.Create(DelayPolicies.Constant(t)));

        private void WithRetry_ComplexDelayPolicy_Canceled(Func<Action, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction> withRetry)
            => WithRetry_Canceled<IOException, ObservableFunc<int, Exception, TimeSpan>>((a, r, e, d) => withRetry(a, r, e, d.Invoke), t => PolicyValidator.Create<IOException>((i, e) => t));

        private void WithRetry_Canceled<TException, TFunc>(Func<Action, int, ExceptionPolicy, TFunc, InterruptableAction> withRetry, Func<TimeSpan, TFunc> delayPolicyFactory)
            where TException : Exception, new()
            where TFunc      : ObservableFunc
        {
            using ManualResetEvent        retryEvent  = new ManualResetEvent(false);
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TException>(ExceptionPolicies.Retry<TException>());
            TFunc delayPolicy = delayPolicyFactory(Constants.Delay);

            exceptionPolicy.Invoking += e =>
            {
                if (exceptionPolicy.Calls > 1)
                    retryEvent.Set();
            };

            InterruptableAction reliableAction = withRetry(
                () =>
                {
                    throw new TException();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // While waiting for the reliable action to complete, we'll cancel it
            Task invocation = Task.Run(() => reliableAction(tokenSource.Token), tokenSource.Token);

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
                        Assert.IsTrue(exceptionPolicy.Calls > 0);
                        Assert.IsTrue(delayPolicy    .Calls > 0);
                        Assert.AreEqual(exceptionPolicy.Calls, delayPolicy.Calls);
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
