// Generated from ReliableAction.Extensions.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    partial class ActionExtensionsTest
    {
        [TestMethod]
        public void WithRetryT1_DelayPolicy()
        {
            Func<Action<int>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int>> withRetry = (a, r, e, d) => a.WithRetry(r, e, d);

            WithRetryT1_DelayPolicy_Success         (withRetry);
            WithRetryT1_DelayPolicy_Failure         (withRetry);
            WithRetryT1_DelayPolicy_EventualSuccess (withRetry);
            WithRetryT1_DelayPolicy_EventualFailure (withRetry);
            WithRetryT1_DelayPolicy_RetriesExhausted(withRetry);
        }

        [TestMethod]
        public void WithRetryT1_DelayPolicy_CancellationToken()
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            Func<Action<int>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int>> withRetry = (a, r, e, d) => a.WithRetry(r, e, d);

            WithRetryT1_DelayPolicy_Success         (withRetry, useToken: true);
            WithRetryT1_DelayPolicy_Failure         (withRetry, useToken: true);
            WithRetryT1_DelayPolicy_EventualSuccess (withRetry, useToken: true);
            WithRetryT1_DelayPolicy_EventualFailure (withRetry, useToken: true);
            WithRetryT1_DelayPolicy_RetriesExhausted(withRetry, useToken: true);
            WithRetryT1_DelayPolicy_Canceled        (withRetry);
        }

        [TestMethod]
        public void WithRetryT1_ComplexDelayPolicy()
        {
            Func<Action<int>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int>> withRetry = (a, r, e, d) => a.WithRetry(r, e, d);

            WithRetryT1_ComplexDelayPolicy_Success         (withRetry);
            WithRetryT1_ComplexDelayPolicy_Failure         (withRetry);
            WithRetryT1_ComplexDelayPolicy_EventualSuccess (withRetry);
            WithRetryT1_ComplexDelayPolicy_EventualFailure (withRetry);
            WithRetryT1_ComplexDelayPolicy_RetriesExhausted(withRetry);
        }

        [TestMethod]
        public void WithRetryT1_ComplexDelayPolicy_CancellationToken()
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            Func<Action<int>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int>> withRetry = (a, r, e, d) => a.WithRetry(r, e, d);

            WithRetryT1_ComplexDelayPolicy_Success         (withRetry, useToken: true);
            WithRetryT1_ComplexDelayPolicy_Failure         (withRetry, useToken: true);
            WithRetryT1_ComplexDelayPolicy_EventualSuccess (withRetry, useToken: true);
            WithRetryT1_ComplexDelayPolicy_EventualFailure (withRetry, useToken: true);
            WithRetryT1_ComplexDelayPolicy_RetriesExhausted(withRetry, useToken: true);
            WithRetryT1_ComplexDelayPolicy_Canceled        (withRetry);
        }

        private void WithRetryT1_DelayPolicy_Success(Func<Action<int>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int>> withRetry, bool useToken = false)
            => WithRetryT1_Success((a, r, e, d) => withRetry(a, r, e, d.Invoke), () => PolicyValidator.IgnoreDelayPolicy(), useToken);

        private void WithRetryT1_ComplexDelayPolicy_Success(Func<Action<int>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int>> withRetry, bool useToken = false)
            => WithRetryT1_Success((a, r, e, d) => withRetry(a, r, e, d.Invoke), () => PolicyValidator.IgnoreComplexDelayPolicy(), useToken);

        private void WithRetryT1_Success<T>(Func<Action<int>, int, ExceptionPolicy, T, InterruptableAction<int>> withRetry, Func<T> delayPolicyFactory, bool useToken = false)
            where T : ObservableFunc
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.IgnoreExceptionPolicy();
            T delayPolicy = delayPolicyFactory();

            InterruptableAction<int> reliableAction = withRetry(
                (arg) => Arguments.Validate(arg),
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            if (useToken)
            {
                using CancellationTokenSource tokenSource = new CancellationTokenSource();
                reliableAction(42, tokenSource.Token);
            }
            else
            {
                reliableAction(42);
            }

            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void WithRetryT1_DelayPolicy_Failure(Func<Action<int>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int>> withRetry, bool useToken = false)
            => WithRetryT1_Failure((a, r, e, d) => withRetry(a, r, e, d.Invoke), () => PolicyValidator.IgnoreDelayPolicy(), useToken);

        private void WithRetryT1_ComplexDelayPolicy_Failure(Func<Action<int>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int>> withRetry, bool useToken = false)
            => WithRetryT1_Failure((a, r, e, d) => withRetry(a, r, e, d.Invoke), () => PolicyValidator.IgnoreComplexDelayPolicy(), useToken);

        private void WithRetryT1_Failure<T>(Func<Action<int>, int, ExceptionPolicy, T, InterruptableAction<int>> withRetry, Func<T> delayPolicyFactory, bool useToken = false)
            where T : ObservableFunc
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<FormatException>(ExceptionPolicies.Fail<FormatException>());
            T delayPolicy = delayPolicyFactory();

            InterruptableAction<int> reliableAction = withRetry(
                (arg) =>
                {
                    Arguments.Validate(arg);
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
                    reliableAction(42, tokenSource.Token);
                }
                else
                {
                    reliableAction(42);
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

        private void WithRetryT1_DelayPolicy_EventualSuccess(Func<Action<int>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int>> withRetry, bool useToken = false)
            => WithRetryT1_EventualSuccess<IOException, ObservableFunc<int, TimeSpan>>((a, r, e, d) => withRetry(a, r, e, d.Invoke), t => PolicyValidator.Create(DelayPolicies.Constant(t)), useToken);

        private void WithRetryT1_ComplexDelayPolicy_EventualSuccess(Func<Action<int>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int>> withRetry, bool useToken = false)
            => WithRetryT1_EventualSuccess<IOException, ObservableFunc<int, Exception, TimeSpan>>((a, r, e, d) => withRetry(a, r, e, d.Invoke), t => PolicyValidator.Create<IOException>((i, e) => t), useToken);

        private void WithRetryT1_EventualSuccess<TException, TFunc>(Func<Action<int>, int, ExceptionPolicy, TFunc, InterruptableAction<int>> withRetry, Func<TimeSpan, TFunc> delayPolicyFactory, bool useToken = false)
            where TException : Exception, new()
            where TFunc      : ObservableFunc
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TException>(ExceptionPolicies.Retry<TException>());
            TFunc delayPolicy = delayPolicyFactory(Constants.Delay);

            Action eventualSuccess = FlakyAction.Create<TException>(1);
            InterruptableAction<int> reliableAction = withRetry(
                (arg) =>
                {
                    Arguments.Validate(arg);
                    eventualSuccess();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            DateTime delayStartUtc = DateTime.UtcNow;
            if (useToken)
            {
                using CancellationTokenSource tokenSource = new CancellationTokenSource();
                reliableAction(42, tokenSource.Token);
            }
            else
            {
                reliableAction(42);
            }
            TimeSpan delay = DateTime.UtcNow - delayStartUtc;

            Assert.IsTrue(delay > Constants.MinDelay);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(1, delayPolicy    .Calls);
        }

        private void WithRetryT1_DelayPolicy_EventualFailure(Func<Action<int>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int>> withRetry, bool useToken = false)
            => WithRetryT1_EventualFailure<IOException, ObservableFunc<int, TimeSpan>>((a, r, e, d) => withRetry(a, r, e, d.Invoke), t => PolicyValidator.Create(DelayPolicies.Constant(t)), useToken);

        private void WithRetryT1_ComplexDelayPolicy_EventualFailure(Func<Action<int>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int>> withRetry, bool useToken = false)
            => WithRetryT1_EventualFailure<IOException, ObservableFunc<int, Exception, TimeSpan>>((a, r, e, d) => withRetry(a, r, e, d.Invoke), t => PolicyValidator.Create<IOException>((i, e) => t), useToken);

        private void WithRetryT1_EventualFailure<TTransient, TFunc>(Func<Action<int>, int, ExceptionPolicy, TFunc, InterruptableAction<int>> withRetry, Func<TimeSpan, TFunc> delayPolicyFactory, bool useToken = false)
            where TTransient : Exception, new()
            where TFunc      : ObservableFunc
        {
            Assert.AreNotEqual(typeof(TTransient), typeof(InvalidOperationException));

            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TTransient, InvalidOperationException>(ExceptionPolicies.Retry<TTransient>());
            TFunc delayPolicy = delayPolicyFactory(Constants.Delay);

            Action eventualFailure = FlakyAction.Create<TTransient, InvalidOperationException>(2);
            InterruptableAction<int> reliableAction = withRetry(
                (arg) =>
                {
                    Arguments.Validate(arg);
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
                    reliableAction(42, tokenSource.Token);
                }
                else
                {
                    reliableAction(42);
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

        private void WithRetryT1_DelayPolicy_RetriesExhausted(Func<Action<int>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int>> withRetry, bool useToken = false)
            => WithRetryT1_RetriesExhausted<IOException, ObservableFunc<int, TimeSpan>>((a, r, e, d) => withRetry(a, r, e, d.Invoke), t => PolicyValidator.Create(DelayPolicies.Constant(t)), useToken);

        private void WithRetryT1_ComplexDelayPolicy_RetriesExhausted(Func<Action<int>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int>> withRetry, bool useToken = false)
            => WithRetryT1_RetriesExhausted<IOException, ObservableFunc<int, Exception, TimeSpan>>((a, r, e, d) => withRetry(a, r, e, d.Invoke), t => PolicyValidator.Create<IOException>((i, e) => t), useToken);

        private void WithRetryT1_RetriesExhausted<TException, TFunc>(Func<Action<int>, int, ExceptionPolicy, TFunc, InterruptableAction<int>> withRetry, Func<TimeSpan, TFunc> delayPolicyFactory, bool useToken = false)
            where TException : Exception, new()
            where TFunc      : ObservableFunc
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TException>(ExceptionPolicies.Retry<TException>());
            TFunc delayPolicy = delayPolicyFactory(Constants.Delay);

            InterruptableAction<int> reliableAction = withRetry(
                (arg) =>
                {
                    Arguments.Validate(arg);
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
                    reliableAction(42, tokenSource.Token);
                }
                else
                {
                    reliableAction(42);
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

        private void WithRetryT1_DelayPolicy_Canceled(Func<Action<int>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int>> withRetry)
            => WithRetryT1_Canceled<IOException, ObservableFunc<int, TimeSpan>>((a, r, e, d) => withRetry(a, r, e, d.Invoke), t => PolicyValidator.Create(DelayPolicies.Constant(t)));

        private void WithRetryT1_ComplexDelayPolicy_Canceled(Func<Action<int>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int>> withRetry)
            => WithRetryT1_Canceled<IOException, ObservableFunc<int, Exception, TimeSpan>>((a, r, e, d) => withRetry(a, r, e, d.Invoke), t => PolicyValidator.Create<IOException>((i, e) => t));

        private void WithRetryT1_Canceled<TException, TFunc>(Func<Action<int>, int, ExceptionPolicy, TFunc, InterruptableAction<int>> withRetry, Func<TimeSpan, TFunc> delayPolicyFactory)
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

            InterruptableAction<int> reliableAction = withRetry(
                (arg) =>
                {
                    Arguments.Validate(arg);
                    throw new TException();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // While waiting for the reliable action to complete, we'll cancel it
            Task invocation = Task.Run(() => reliableAction(42, tokenSource.Token), tokenSource.Token);

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
