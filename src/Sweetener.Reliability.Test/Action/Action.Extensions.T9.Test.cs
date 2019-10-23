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
        public void WithRetryT9_DelayPolicy()
        {
            Func<Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> withRetry = (a, r, e, d) => a.WithRetry(r, e, d);

            WithRetryT9_DelayPolicy_Success         (withRetry);
            WithRetryT9_DelayPolicy_Failure         (withRetry);
            WithRetryT9_DelayPolicy_EventualSuccess (withRetry);
            WithRetryT9_DelayPolicy_EventualFailure (withRetry);
            WithRetryT9_DelayPolicy_RetriesExhausted(withRetry);
        }

        [TestMethod]
        public void WithRetryT9_DelayPolicy_CancellationToken()
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            Func<Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> withRetry = (a, r, e, d) => a.WithRetry(r, e, d);

            WithRetryT9_DelayPolicy_Success         (withRetry, useToken: true);
            WithRetryT9_DelayPolicy_Failure         (withRetry, useToken: true);
            WithRetryT9_DelayPolicy_EventualSuccess (withRetry, useToken: true);
            WithRetryT9_DelayPolicy_EventualFailure (withRetry, useToken: true);
            WithRetryT9_DelayPolicy_RetriesExhausted(withRetry, useToken: true);
            WithRetryT9_DelayPolicy_Canceled        (withRetry);
        }

        [TestMethod]
        public void WithRetryT9_ComplexDelayPolicy()
        {
            Func<Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> withRetry = (a, r, e, d) => a.WithRetry(r, e, d);

            WithRetryT9_ComplexDelayPolicy_Success         (withRetry);
            WithRetryT9_ComplexDelayPolicy_Failure         (withRetry);
            WithRetryT9_ComplexDelayPolicy_EventualSuccess (withRetry);
            WithRetryT9_ComplexDelayPolicy_EventualFailure (withRetry);
            WithRetryT9_ComplexDelayPolicy_RetriesExhausted(withRetry);
        }

        [TestMethod]
        public void WithRetryT9_ComplexDelayPolicy_CancellationToken()
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            Func<Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> withRetry = (a, r, e, d) => a.WithRetry(r, e, d);

            WithRetryT9_ComplexDelayPolicy_Success         (withRetry, useToken: true);
            WithRetryT9_ComplexDelayPolicy_Failure         (withRetry, useToken: true);
            WithRetryT9_ComplexDelayPolicy_EventualSuccess (withRetry, useToken: true);
            WithRetryT9_ComplexDelayPolicy_EventualFailure (withRetry, useToken: true);
            WithRetryT9_ComplexDelayPolicy_RetriesExhausted(withRetry, useToken: true);
            WithRetryT9_ComplexDelayPolicy_Canceled        (withRetry);
        }

        private void WithRetryT9_DelayPolicy_Success(Func<Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> withRetry, bool useToken = false)
            => WithRetryT9_Success((a, r, e, d) => withRetry(a, r, e, d.Invoke), () => PolicyValidator.IgnoreDelayPolicy(), useToken);

        private void WithRetryT9_ComplexDelayPolicy_Success(Func<Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> withRetry, bool useToken = false)
            => WithRetryT9_Success((a, r, e, d) => withRetry(a, r, e, d.Invoke), () => PolicyValidator.IgnoreComplexDelayPolicy(), useToken);

        private void WithRetryT9_Success<T>(Func<Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, T, InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> withRetry, Func<T> delayPolicyFactory, bool useToken = false)
            where T : ObservableFunc
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.IgnoreExceptionPolicy();
            T delayPolicy = delayPolicyFactory();

            InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> reliableAction = withRetry(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => Arguments.Validate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9),
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            if (useToken)
            {
                using CancellationTokenSource tokenSource = new CancellationTokenSource();
                reliableAction(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), tokenSource.Token);
            }
            else
            {
                reliableAction(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL));
            }

            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        private void WithRetryT9_DelayPolicy_Failure(Func<Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> withRetry, bool useToken = false)
            => WithRetryT9_Failure((a, r, e, d) => withRetry(a, r, e, d.Invoke), () => PolicyValidator.IgnoreDelayPolicy(), useToken);

        private void WithRetryT9_ComplexDelayPolicy_Failure(Func<Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> withRetry, bool useToken = false)
            => WithRetryT9_Failure((a, r, e, d) => withRetry(a, r, e, d.Invoke), () => PolicyValidator.IgnoreComplexDelayPolicy(), useToken);

        private void WithRetryT9_Failure<T>(Func<Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, T, InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> withRetry, Func<T> delayPolicyFactory, bool useToken = false)
            where T : ObservableFunc
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<FormatException>(ExceptionPolicies.Fail<FormatException>());
            T delayPolicy = delayPolicyFactory();

            InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> reliableAction = withRetry(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                {
                    Arguments.Validate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
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
                    reliableAction(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), tokenSource.Token);
                }
                else
                {
                    reliableAction(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL));
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

        private void WithRetryT9_DelayPolicy_EventualSuccess(Func<Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> withRetry, bool useToken = false)
            => WithRetryT9_EventualSuccess<IOException, ObservableFunc<int, TimeSpan>>((a, r, e, d) => withRetry(a, r, e, d.Invoke), t => PolicyValidator.Create(DelayPolicies.Constant(t)), useToken);

        private void WithRetryT9_ComplexDelayPolicy_EventualSuccess(Func<Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> withRetry, bool useToken = false)
            => WithRetryT9_EventualSuccess<IOException, ObservableFunc<int, Exception, TimeSpan>>((a, r, e, d) => withRetry(a, r, e, d.Invoke), t => PolicyValidator.Create<IOException>((i, e) => t), useToken);

        private void WithRetryT9_EventualSuccess<TException, TFunc>(Func<Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, TFunc, InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> withRetry, Func<TimeSpan, TFunc> delayPolicyFactory, bool useToken = false)
            where TException : Exception, new()
            where TFunc      : ObservableFunc
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TException>(ExceptionPolicies.Retry<TException>());
            TFunc delayPolicy = delayPolicyFactory(Constants.Delay);

            Action eventualSuccess = FlakyAction.Create<TException>(1);
            InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> reliableAction = withRetry(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                {
                    Arguments.Validate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                    eventualSuccess();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            DateTime delayStartUtc = DateTime.UtcNow;
            if (useToken)
            {
                using CancellationTokenSource tokenSource = new CancellationTokenSource();
                reliableAction(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), tokenSource.Token);
            }
            else
            {
                reliableAction(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL));
            }
            TimeSpan delay = DateTime.UtcNow - delayStartUtc;

            Assert.IsTrue(delay > Constants.MinDelay);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(1, delayPolicy    .Calls);
        }

        private void WithRetryT9_DelayPolicy_EventualFailure(Func<Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> withRetry, bool useToken = false)
            => WithRetryT9_EventualFailure<IOException, ObservableFunc<int, TimeSpan>>((a, r, e, d) => withRetry(a, r, e, d.Invoke), t => PolicyValidator.Create(DelayPolicies.Constant(t)), useToken);

        private void WithRetryT9_ComplexDelayPolicy_EventualFailure(Func<Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> withRetry, bool useToken = false)
            => WithRetryT9_EventualFailure<IOException, ObservableFunc<int, Exception, TimeSpan>>((a, r, e, d) => withRetry(a, r, e, d.Invoke), t => PolicyValidator.Create<IOException>((i, e) => t), useToken);

        private void WithRetryT9_EventualFailure<TTransient, TFunc>(Func<Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, TFunc, InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> withRetry, Func<TimeSpan, TFunc> delayPolicyFactory, bool useToken = false)
            where TTransient : Exception, new()
            where TFunc      : ObservableFunc
        {
            Assert.AreNotEqual(typeof(TTransient), typeof(InvalidOperationException));

            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TTransient, InvalidOperationException>(ExceptionPolicies.Retry<TTransient>());
            TFunc delayPolicy = delayPolicyFactory(Constants.Delay);

            Action eventualFailure = FlakyAction.Create<TTransient, InvalidOperationException>(2);
            InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> reliableAction = withRetry(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                {
                    Arguments.Validate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
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
                    reliableAction(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), tokenSource.Token);
                }
                else
                {
                    reliableAction(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL));
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

        private void WithRetryT9_DelayPolicy_RetriesExhausted(Func<Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> withRetry, bool useToken = false)
            => WithRetryT9_RetriesExhausted<IOException, ObservableFunc<int, TimeSpan>>((a, r, e, d) => withRetry(a, r, e, d.Invoke), t => PolicyValidator.Create(DelayPolicies.Constant(t)), useToken);

        private void WithRetryT9_ComplexDelayPolicy_RetriesExhausted(Func<Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> withRetry, bool useToken = false)
            => WithRetryT9_RetriesExhausted<IOException, ObservableFunc<int, Exception, TimeSpan>>((a, r, e, d) => withRetry(a, r, e, d.Invoke), t => PolicyValidator.Create<IOException>((i, e) => t), useToken);

        private void WithRetryT9_RetriesExhausted<TException, TFunc>(Func<Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, TFunc, InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> withRetry, Func<TimeSpan, TFunc> delayPolicyFactory, bool useToken = false)
            where TException : Exception, new()
            where TFunc      : ObservableFunc
        {
            DateTime delayStartUtc = DateTime.MinValue;
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TException>(ExceptionPolicies.Retry<TException>());
            TFunc delayPolicy = delayPolicyFactory(Constants.Delay);

            InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> reliableAction = withRetry(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                {
                    Arguments.Validate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
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
                    reliableAction(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), tokenSource.Token);
                }
                else
                {
                    reliableAction(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL));
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

        private void WithRetryT9_DelayPolicy_Canceled(Func<Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, DelayPolicy, InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> withRetry)
            => WithRetryT9_Canceled<IOException, ObservableFunc<int, TimeSpan>>((a, r, e, d) => withRetry(a, r, e, d.Invoke), t => PolicyValidator.Create(DelayPolicies.Constant(t)));

        private void WithRetryT9_ComplexDelayPolicy_Canceled(Func<Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, ComplexDelayPolicy, InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> withRetry)
            => WithRetryT9_Canceled<IOException, ObservableFunc<int, Exception, TimeSpan>>((a, r, e, d) => withRetry(a, r, e, d.Invoke), t => PolicyValidator.Create<IOException>((i, e) => t));

        private void WithRetryT9_Canceled<TException, TFunc>(Func<Action<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>, int, ExceptionPolicy, TFunc, InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>>> withRetry, Func<TimeSpan, TFunc> delayPolicyFactory)
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

            InterruptableAction<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>> reliableAction = withRetry(
                (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
                {
                    Arguments.Validate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                    throw new TException();
                },
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy);

            // While waiting for the reliable action to complete, we'll cancel it
            Task invocation = Task.Run(() => reliableAction(42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), tokenSource.Token), tokenSource.Token);

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
