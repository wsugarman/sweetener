using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    public class BaseReliableActionTest
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

        protected void Invoke_Success<T>(T reliableAction, Action<T> invoke)
            where T : ReliableDelegate
            => TryInvoke_Success(reliableAction, x => { invoke(x); return true; });

        protected void TryInvoke_Success<T>(T reliableAction, Func<T, bool> invoke)
            where T : ReliableDelegate
        {
            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            Assert.IsTrue(invoke(reliableAction));
        }

        protected void Invoke_Failure<TDelegate, TException>(TDelegate reliableAction, Action<TDelegate> invoke)
            where TDelegate : ReliableDelegate
            where TException : Exception
            => TryInvoke_Failure<TDelegate, TException>(reliableAction, x =>
                {
                    try
                    {
                        invoke(x);
                        Assert.Fail();
                    }
                    catch (TException)
                    {
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

        protected void TryInvoke_Failure<TDelegate, TException>(TDelegate reliableAction, Func<TDelegate, bool> invoke)
            where TDelegate  : ReliableDelegate
            where TException : Exception
        {
            reliableAction.Retrying         += (i, e) => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            Assert.IsFalse(invoke(reliableAction));
        }

        protected void Invoke_EventualSuccess<T>(T reliableAction, Action<T> invoke, int expectedAttempts)
            where T : ReliableDelegate
            => TryInvoke_EventualSuccess(reliableAction, x => { invoke(x); return true; }, expectedAttempts);

        protected void TryInvoke_EventualSuccess<T>(T reliableAction, Func<T, bool> invoke, int expectedAttempts)
            where T : ReliableDelegate
        {
            int attempts = -1;
            reliableAction.Retrying         += (i, e) => attempts = i;
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            Assert.IsTrue(invoke(reliableAction));
            Assert.AreEqual(expectedAttempts, attempts);
        }

        protected void Invoke_EventualFailure<TDelegate, TException>(TDelegate reliableAction, Action<TDelegate> invoke, int expectedAttempts)
            where TDelegate : ReliableDelegate
            where TException : Exception
            => Invoke_EventualFailure<TDelegate, TException>(reliableAction, invoke, expectedAttempts, retriesExhausted: false);

        protected void TryInvoke_EventualFailure<TDelegate, TException>(TDelegate reliableAction, Func<TDelegate, bool> invoke, int expectedAttempts)
            where TDelegate : ReliableDelegate
            where TException : Exception
            => TryInvoke_EventualFailure<TDelegate, TException>(reliableAction, invoke, expectedAttempts, retriesExhausted: false);

        protected void Invoke_RetriesExhausted<TDelegate, TException>(TDelegate reliableAction, Action<TDelegate> invoke, int expectedAttempts)
            where TDelegate  : ReliableDelegate
            where TException : Exception
            => Invoke_EventualFailure<TDelegate, TException>(reliableAction, invoke, expectedAttempts, retriesExhausted: true);

        protected void TryInvoke_RetriesExhausted<TDelegate, TException>(TDelegate reliableAction, Func<TDelegate, bool> invoke, int expectedAttempts)
            where TDelegate  : ReliableDelegate
            where TException : Exception
            => TryInvoke_EventualFailure<TDelegate, TException>(reliableAction, invoke, expectedAttempts, retriesExhausted: true);

        private void Invoke_EventualFailure<TDelegate, TException>(TDelegate reliableAction, Action<TDelegate> invoke, int expectedAttempts, bool retriesExhausted)
            where TDelegate  : ReliableDelegate
            where TException : Exception
            => TryInvoke_EventualFailure<TDelegate, TException>(reliableAction, x =>
                {
                    try
                    {
                        invoke(x);
                        Assert.Fail();
                    }
                    catch (TException)
                    {
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

        private void TryInvoke_EventualFailure<TDelegate, TException>(TDelegate reliableAction, Func<TDelegate, bool> invoke, int expectedAttempts, bool retriesExhausted)
            where TDelegate  : ReliableDelegate
            where TException : Exception
        {
            int attempts = -1;
            reliableAction.Retrying += (i, e) => attempts = i;

            if (retriesExhausted)
                reliableAction.Failed += e => Assert.Fail();
            else
                reliableAction.RetriesExhausted += e => Assert.Fail();

            Assert.IsFalse(invoke(reliableAction));
            Assert.AreEqual(expectedAttempts, attempts);
        }

        protected void Invoke_Canceled<T>(T reliableAction, Action<T> invoke, CancellationTokenSource cancellationTokenSource)
            where T : ReliableDelegate
            => TryInvoke_Canceled(reliableAction, x => { invoke(x); return false; }, cancellationTokenSource);

        protected void TryInvoke_Canceled<T>(T reliableAction, Func<T, bool> invoke, CancellationTokenSource cancellationTokenSource)
            where T : ReliableDelegate
        {
            reliableAction.Retrying         += (i, e) => Assert.AreEqual(1, i);
            reliableAction.Failed           +=     e  => Assert.Fail();
            reliableAction.RetriesExhausted +=     e  => Assert.Fail();

            // We expect a bit of a delay with this operation, so we'll spin it up in a separate thread
            Task invocation = Task.Run(() => invoke(reliableAction), cancellationTokenSource.Token);

            Task.Yield();
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
