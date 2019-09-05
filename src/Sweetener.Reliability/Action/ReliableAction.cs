 // Generated from ReliableAction.tt
using System;
using System.Threading;

namespace Sweetener.Reliability
{
    #region ReliableAction

    /// <summary>
    /// A wrapper to reliably invoke an <see cref="Action"/> in case of transient issues.
    /// </summary>
    public class ReliableAction
    {
        /// <summary>
        /// Gets the maximum number of retry attempts.
        /// </summary>
        /// <value>
        /// The maximum number of attempts the underlying <see cref="Action"/>
        /// should be invoked if there are transient exceptions.
        /// </value>
        public int MaxRetries { get; }

        /// <summary>
        /// Gets the policy that determines which exceptions are transient.
        /// </summary>
        /// <value>
        /// The <see cref="DelayPolicy"/> that indicates when
        /// </value>
        public DelayPolicy DelayPolicy { get; }

        public RetryPolicy RetryPolicy { get; }

        private readonly Action _action;

        public ReliableAction(Action action, int maxRetries, DelayPolicy delayPolicy, RetryPolicy retryPolicy)
        {
            _action     = action;
            MaxRetries  = maxRetries;
            DelayPolicy = delayPolicy;
            RetryPolicy = retryPolicy;
        }

        public bool TryInvoke()
        {
            TimeSpan delay;
            int attempt = 0;
            do
            {
                attempt++;
                try
                {
                    _action();
                    return true;
                }
                catch (Exception e)
                {
                    if (!RetryPolicy.IsTransient(e))
                        return false;

                    delay = DelayPolicy.GetDelay(attempt, e);
                }
            } while (ContinueRetry(attempt, delay));

            return false;
        }

        private bool ContinueRetry(int attempt, TimeSpan delay)
        {
            bool continueRetry = attempt <= MaxRetries;
            if (continueRetry)
                Thread.Sleep(delay);

            return continueRetry;
        }
    }

    #endregion

    #region ReliableAction<T>

    /// <summary>
    /// A wrapper to reliably invoke an <see cref="Action{T}"/> in case of transient issues.
    /// </summary>
    public class ReliableAction<T>
    {
        /// <summary>
        /// Gets the maximum number of retry attempts.
        /// </summary>
        /// <value>
        /// The maximum number of attempts the underlying <see cref="Action{T}"/>
        /// should be invoked if there are transient exceptions.
        /// </value>
        public int MaxRetries { get; }

        /// <summary>
        /// Gets the policy that determines which exceptions are transient.
        /// </summary>
        /// <value>
        /// The <see cref="DelayPolicy"/> that indicates when
        /// </value>
        public DelayPolicy DelayPolicy { get; }

        public RetryPolicy RetryPolicy { get; }

        private readonly Action<T> _action;

        public ReliableAction(Action<T> action, int maxRetries, DelayPolicy delayPolicy, RetryPolicy retryPolicy)
        {
            _action     = action;
            MaxRetries  = maxRetries;
            DelayPolicy = delayPolicy;
            RetryPolicy = retryPolicy;
        }

        public bool TryInvoke(T arg)
        {
            TimeSpan delay;
            int attempt = 0;
            do
            {
                attempt++;
                try
                {
                    _action(arg);
                    return true;
                }
                catch (Exception e)
                {
                    if (!RetryPolicy.IsTransient(e))
                        return false;

                    delay = DelayPolicy.GetDelay(attempt, e);
                }
            } while (ContinueRetry(attempt, delay));

            return false;
        }

        private bool ContinueRetry(int attempt, TimeSpan delay)
        {
            bool continueRetry = attempt <= MaxRetries;
            if (continueRetry)
                Thread.Sleep(delay);

            return continueRetry;
        }
    }

    #endregion

}
