using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    public class ReliableDelegateTest
    {
        private static readonly Func<ReliableDelegate, ExceptionPolicy   > s_getExceptionPolicy = DynamicGetter.ForField<ReliableDelegate, ExceptionPolicy   >("_canRetry");
        private static readonly Func<ReliableDelegate, ComplexDelayPolicy> s_getDelayPolicy     = DynamicGetter.ForField<ReliableDelegate, ComplexDelayPolicy>("_getDelay");

        // We cannot assert that the reference to the DelayPolicy is the same in the ReliableDelegate as we wrap it,
        // so we can only validate the functionality using some user-defined action
        protected void Ctor(ReliableDelegate reliableAction, int expectedMaxRetries, ExceptionPolicy expectedExceptionPolicy, ComplexDelayPolicy expectedDelayPolicy)
            => Ctor(reliableAction, expectedMaxRetries, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        protected void Ctor(ReliableDelegate reliableAction, int expectedMaxRetries, ExceptionPolicy expectedExceptionPolicy, Action<ComplexDelayPolicy> validateDelayPolicy)
        {
            Assert.AreEqual(expectedMaxRetries     , reliableAction.MaxRetries);
            Assert.AreSame (expectedExceptionPolicy, s_getExceptionPolicy(reliableAction));
            validateDelayPolicy(s_getDelayPolicy(reliableAction));
        }

        protected void Invoke_Success<T>(Func<int, ExceptionPolicy, DelayPolicy, T> actionFactory, Action<T> invoke)
            where T : ReliableDelegate
            => TryInvoke_Success(actionFactory, x => { invoke(x); return true; });

        protected void TryInvoke_Success<T>(Func<int, ExceptionPolicy, DelayPolicy, T> actionFactory, Func<T, bool> tryInvoke)
            where T : ReliableDelegate
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create(ExceptionPolicies.Fatal);
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            T reliableAction = actionFactory(Retries.Infinite, exceptionPolicy.Invoke, delayPolicy.Invoke);

            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            Assert.IsTrue(tryInvoke(reliableAction));

            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        protected void Invoke_Failure<TDelegate, TException>(Func<int, ExceptionPolicy, ComplexDelayPolicy, TDelegate> actionFactory, Action<TDelegate> invoke)
            where TDelegate  : ReliableDelegate
            where TException : Exception
            => TryInvoke_Failure<TDelegate, TException>(actionFactory, x =>
                {
                    try
                    {
                        invoke(x);
                        Assert.Fail();
                    }
                    catch (TException e)
                    {
                        Assert.AreEqual(typeof(TException), e.GetType());
                        return false;
                    }
                    catch (AssertFailedException)
                    {
                        throw;
                    }
                    catch (Exception)
                    {
                        Assert.Fail();
                    }

                    // This return is never hit
                    return true;
                });

        protected void TryInvoke_Failure<TDelegate, TException>(Func<int, ExceptionPolicy, ComplexDelayPolicy, TDelegate> actionFactory, Func<TDelegate, bool> tryInvoke)
            where TDelegate  : ReliableDelegate
            where TException : Exception
        {
            ObservableFunc<Exception, bool>          exceptionPolicy = PolicyValidator.Create<TException>(ExceptionPolicies.Fail<TException>());
            ObservableFunc<int, Exception, TimeSpan> delayPolicy     = PolicyValidator.Create< TException>((i, e) => TimeSpan.Zero);

            TDelegate reliableAction = actionFactory(Retries.Infinite, exceptionPolicy.Invoke, delayPolicy.Invoke);

            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();
            reliableAction.Failed           +=     e  => Assert.AreEqual(typeof(TException), e.GetType());

            Assert.IsFalse(tryInvoke(reliableAction));

            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        protected void Invoke_EventualSuccess<TDelegate, TException>(Func<int, ExceptionPolicy, DelayPolicy, TDelegate> actionFactory, Action<TDelegate> invoke, int expectedAttempts)
            where TDelegate  : ReliableDelegate
            where TException : Exception
            => TryInvoke_EventualSuccess<TDelegate, TException>(actionFactory, x => { invoke(x); return true; }, expectedAttempts);

        protected void TryInvoke_EventualSuccess<TDelegate, TException>(Func<int, ExceptionPolicy, DelayPolicy, TDelegate> actionFactory, Func<TDelegate, bool> tryInvoke, int expectedAttempts)
            where TDelegate  : ReliableDelegate
            where TException : Exception
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TException>(ExceptionPolicies.Retry<TException>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            TDelegate reliableAction = actionFactory(Retries.Infinite, exceptionPolicy.Invoke, delayPolicy.Invoke);

            int attempts = 0;
            reliableAction.Retrying         += (i, e) => Assert.AreEqual(++attempts, i);
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            Assert.IsTrue(tryInvoke(reliableAction));
            Assert.AreEqual(expectedAttempts, attempts);

            Assert.AreEqual(expectedAttempts - 1, exceptionPolicy.Calls);
            Assert.AreEqual(expectedAttempts - 1, delayPolicy    .Calls);
        }

        protected void Invoke_EventualFailure<TDelegate, TTransient, TFatal>(Func<int, ExceptionPolicy, DelayPolicy, TDelegate> actionFactory, Action<TDelegate> invoke, int expectedAttempts)
            where TDelegate  : ReliableDelegate
            where TTransient : Exception
            where TFatal     : Exception
            => Invoke_EventuallyUnsuccessful<TDelegate, TTransient, TFatal>(actionFactory, invoke, expectedAttempts, retriesExhausted: false);

        protected void TryInvoke_EventualFailure<TDelegate, TTransient, TFatal>(Func<int, ExceptionPolicy, DelayPolicy, TDelegate> actionFactory, Func<TDelegate, bool> tryInvoke, int expectedAttempts)
            where TDelegate  : ReliableDelegate
            where TTransient : Exception
            where TFatal     : Exception
            => TryInvoke_Unsuccessful<TDelegate, TTransient, TFatal>(actionFactory, tryInvoke, expectedAttempts, retriesExhausted: false);

        protected void Invoke_RetriesExhausted<TDelegate, TException>(Func<int, ExceptionPolicy, DelayPolicy, TDelegate> actionFactory, Action<TDelegate> invoke, int expectedAttempts)
            where TDelegate  : ReliableDelegate
            where TException : Exception
            => Invoke_EventuallyUnsuccessful<TDelegate, TException, TException>(actionFactory, invoke, expectedAttempts, retriesExhausted: true);

        protected void TryInvoke_RetriesExhausted<TDelegate, TException>(Func<int, ExceptionPolicy, DelayPolicy, TDelegate> actionFactory, Func<TDelegate, bool> tryInvoke, int expectedAttempts)
            where TDelegate  : ReliableDelegate
            where TException : Exception
            => TryInvoke_Unsuccessful<TDelegate, TException, TException>(actionFactory, tryInvoke, expectedAttempts, retriesExhausted: true);

        private void Invoke_EventuallyUnsuccessful<TDelegate, TTransient, TFatal>(Func<int, ExceptionPolicy, DelayPolicy, TDelegate> actionFactory, Action<TDelegate> invoke, int expectedAttempts, bool retriesExhausted)
            where TDelegate  : ReliableDelegate
            where TTransient : Exception
            where TFatal     : Exception
            => TryInvoke_Unsuccessful<TDelegate, TTransient, TFatal>(actionFactory, x =>
                {
                    try
                    {
                        invoke(x);
                        Assert.Fail();
                    }
                    catch (TFatal e)
                    {
                        Assert.AreEqual(typeof(TFatal), e.GetType());
                        return false;
                    }
                    catch (AssertFailedException)
                    {
                        throw;
                    }
                    catch (Exception)
                    {
                        Assert.Fail();
                    }
                
                    // This return is never hit
                    return true;
                },
                expectedAttempts,
                retriesExhausted);

        private void TryInvoke_Unsuccessful<TDelegate, TTransient, TFatal>(Func<int, ExceptionPolicy, DelayPolicy, TDelegate> actionFactory, Func<TDelegate, bool> tryInvoke, int expectedAttempts, bool retriesExhausted)
            where TDelegate  : ReliableDelegate
            where TTransient : Exception
            where TFatal     : Exception
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TTransient>(ExceptionPolicies.Retry<TTransient>());
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.None);

            TDelegate reliableAction = actionFactory(retriesExhausted ? expectedAttempts : expectedAttempts * 2, exceptionPolicy.Invoke, delayPolicy.Invoke);

            int attempts = 0;
            reliableAction.Retrying += (i, e) => Assert.AreEqual(++attempts, i);

            if (retriesExhausted)
            {
                reliableAction.Failed           += e => Assert.Fail();
                reliableAction.RetriesExhausted += e => Assert.AreEqual(typeof(TTransient), e.GetType());
            }
            else
            {
                reliableAction.Failed           += e => Assert.AreEqual(typeof(TFatal), e.GetType());
                reliableAction.RetriesExhausted += e => Assert.Fail();
            }

            Assert.IsFalse(tryInvoke(reliableAction));
            Assert.AreEqual(expectedAttempts, attempts);

            Assert.AreEqual(expectedAttempts    , exceptionPolicy.Calls);
            Assert.AreEqual(expectedAttempts - 1, delayPolicy    .Calls);
        }

        protected void Invoke_Canceled<TDelegate, TException>(Func<int, ExceptionPolicy, DelayPolicy, TDelegate> actionFactory, Action<TDelegate> invoke, CancellationTokenSource cancellationTokenSource)
            where TDelegate  : ReliableDelegate
            where TException : Exception
            => TryInvoke_Canceled<TDelegate, TException>(actionFactory, x => { invoke(x); return false; }, cancellationTokenSource);

        protected void TryInvoke_Canceled<TDelegate, TException>(Func<int, ExceptionPolicy, DelayPolicy, TDelegate> actionFactory, Func<TDelegate, bool> tryInvoke, CancellationTokenSource cancellationTokenSource)
            where TDelegate  : ReliableDelegate
            where TException : Exception
        {
            ObservableFunc<Exception, bool> exceptionPolicy = PolicyValidator.Create<TException>(ExceptionPolicies.Transient);
            ObservableFunc<int, TimeSpan>   delayPolicy     = PolicyValidator.Create(DelayPolicies.Constant(10));

            TDelegate reliableAction = actionFactory(Retries.Infinite, exceptionPolicy.Invoke, delayPolicy.Invoke);

            int attempts = 0;
            reliableAction.Retrying         += (i, e) => Assert.AreEqual(++attempts, i);
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            // We expect a bit of a delay with this operation, so we'll spin it up in a separate thread
            Task invocation = Task.Run(() => tryInvoke(reliableAction), cancellationTokenSource.Token);

            while (invocation.Status != TaskStatus.Running)
            { }

            Task.Delay((int)TimeSpan.FromHours(1).TotalMilliseconds);
            cancellationTokenSource.Cancel();

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
                        Assert.IsTrue(attempts              > 0);
                        Assert.IsTrue(exceptionPolicy.Calls > 0);
                        Assert.IsTrue(delayPolicy    .Calls > 0);
                        Assert.AreEqual(exceptionPolicy.Calls, delayPolicy.Calls);
                        return; // Successfully canceled
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
