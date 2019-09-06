// Generated from ReliableAction.tt
using System;
using System.Threading;

namespace Sweetener.Reliability
{
    #region ReliableAction

    /// <summary>
    /// A wrapper to reliably invoke a delegate in case of transient issues.
    /// </summary>
    public sealed class ReliableAction
    {
        /// <summary>
        /// Occurs when the action must be retried due to a transient exception.
        /// </summary>
        public event Action Retrying;

        /// <summary>
        /// Gets the maximum number of retry attempts.
        /// </summary>
        /// <value>
        /// The maximum number of attempts the underlying <see cref="Action"/>
        /// should be invoked if there are transient exceptions.
        /// </value>
        public int MaxRetries { get; }

        /// <summary>
        /// Gets the policy that determines how long wait to wait between retries.
        /// </summary>
        /// <value>
        /// The <see cref="DelayPolicy"/> that determines how long wait to wait between retries.
        /// </value>
        public DelayPolicy DelayPolicy { get; }

        /// <summary>
        /// Gets the policy that determines which errors are transient.
        /// </summary>
        /// <value>
        /// The <see cref="RetryPolicy"/> that indicates which errors are transient.
        /// </value>
        public RetryPolicy RetryPolicy { get; }

        private readonly Action _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction"/>
        /// class that executes the given <see cref="Action"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="action">The underlying action to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <param name="retryPolicy">The policy that determines which errors are transient.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries"/> is a negative number other than -1, which represents an infinite number of retries.
        /// </exception>
        public ReliableAction(Action action, int maxRetries, DelayPolicy delayPolicy, RetryPolicy retryPolicy)
        {
            if (maxRetries < -1)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            _action     = action;
            MaxRetries  = maxRetries;
            DelayPolicy = delayPolicy;
            RetryPolicy = retryPolicy;
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
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
            {
                Thread.Sleep(delay);
                Retrying?.Invoke();
            }

            return continueRetry;
        }
    }

    #endregion

    #region ReliableAction<T>

    /// <summary>
    /// A wrapper to reliably invoke a delegate in case of transient issues.
    /// </summary>
    /// <typeparam name="T">The type of the parameter of the underlying delegate.</typeparam>
    public sealed class ReliableAction<T>
    {
        /// <summary>
        /// Occurs when the action must be retried due to a transient exception.
        /// </summary>
        public event Action Retrying;

        /// <summary>
        /// Gets the maximum number of retry attempts.
        /// </summary>
        /// <value>
        /// The maximum number of attempts the underlying <see cref="Action{T}"/>
        /// should be invoked if there are transient exceptions.
        /// </value>
        public int MaxRetries { get; }

        /// <summary>
        /// Gets the policy that determines how long wait to wait between retries.
        /// </summary>
        /// <value>
        /// The <see cref="DelayPolicy"/> that determines how long wait to wait between retries.
        /// </value>
        public DelayPolicy DelayPolicy { get; }

        /// <summary>
        /// Gets the policy that determines which errors are transient.
        /// </summary>
        /// <value>
        /// The <see cref="RetryPolicy"/> that indicates which errors are transient.
        /// </value>
        public RetryPolicy RetryPolicy { get; }

        private readonly Action<T> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction{T}"/>
        /// class that executes the given <see cref="Action{T}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="action">The underlying action to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <param name="retryPolicy">The policy that determines which errors are transient.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries"/> is a negative number other than -1, which represents an infinite number of retries.
        /// </exception>
        public ReliableAction(Action<T> action, int maxRetries, DelayPolicy delayPolicy, RetryPolicy retryPolicy)
        {
            if (maxRetries < -1)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            _action     = action;
            MaxRetries  = maxRetries;
            DelayPolicy = delayPolicy;
            RetryPolicy = retryPolicy;
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate.
        /// </summary>
        /// <param name="arg">The argument for the underlying delegate.</param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
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
            {
                Thread.Sleep(delay);
                Retrying?.Invoke();
            }

            return continueRetry;
        }
    }

    #endregion

    #region ReliableAction<T1, T2>

    /// <summary>
    /// A wrapper to reliably invoke a delegate in case of transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    public sealed class ReliableAction<T1, T2>
    {
        /// <summary>
        /// Occurs when the action must be retried due to a transient exception.
        /// </summary>
        public event Action Retrying;

        /// <summary>
        /// Gets the maximum number of retry attempts.
        /// </summary>
        /// <value>
        /// The maximum number of attempts the underlying <see cref="Action{T1, T2}"/>
        /// should be invoked if there are transient exceptions.
        /// </value>
        public int MaxRetries { get; }

        /// <summary>
        /// Gets the policy that determines how long wait to wait between retries.
        /// </summary>
        /// <value>
        /// The <see cref="DelayPolicy"/> that determines how long wait to wait between retries.
        /// </value>
        public DelayPolicy DelayPolicy { get; }

        /// <summary>
        /// Gets the policy that determines which errors are transient.
        /// </summary>
        /// <value>
        /// The <see cref="RetryPolicy"/> that indicates which errors are transient.
        /// </value>
        public RetryPolicy RetryPolicy { get; }

        private readonly Action<T1, T2> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction{T1, T2}"/>
        /// class that executes the given <see cref="Action{T1, T2}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="action">The underlying action to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <param name="retryPolicy">The policy that determines which errors are transient.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries"/> is a negative number other than -1, which represents an infinite number of retries.
        /// </exception>
        public ReliableAction(Action<T1, T2> action, int maxRetries, DelayPolicy delayPolicy, RetryPolicy retryPolicy)
        {
            if (maxRetries < -1)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            _action     = action;
            MaxRetries  = maxRetries;
            DelayPolicy = delayPolicy;
            RetryPolicy = retryPolicy;
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2)
        {
            TimeSpan delay;
            int attempt = 0;
            do
            {
                attempt++;
                try
                {
                    _action(arg1, arg2);
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
            {
                Thread.Sleep(delay);
                Retrying?.Invoke();
            }

            return continueRetry;
        }
    }

    #endregion

    #region ReliableAction<T1, T2, T3>

    /// <summary>
    /// A wrapper to reliably invoke a delegate in case of transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    public sealed class ReliableAction<T1, T2, T3>
    {
        /// <summary>
        /// Occurs when the action must be retried due to a transient exception.
        /// </summary>
        public event Action Retrying;

        /// <summary>
        /// Gets the maximum number of retry attempts.
        /// </summary>
        /// <value>
        /// The maximum number of attempts the underlying <see cref="Action{T1, T2, T3}"/>
        /// should be invoked if there are transient exceptions.
        /// </value>
        public int MaxRetries { get; }

        /// <summary>
        /// Gets the policy that determines how long wait to wait between retries.
        /// </summary>
        /// <value>
        /// The <see cref="DelayPolicy"/> that determines how long wait to wait between retries.
        /// </value>
        public DelayPolicy DelayPolicy { get; }

        /// <summary>
        /// Gets the policy that determines which errors are transient.
        /// </summary>
        /// <value>
        /// The <see cref="RetryPolicy"/> that indicates which errors are transient.
        /// </value>
        public RetryPolicy RetryPolicy { get; }

        private readonly Action<T1, T2, T3> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction{T1, T2, T3}"/>
        /// class that executes the given <see cref="Action{T1, T2, T3}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="action">The underlying action to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <param name="retryPolicy">The policy that determines which errors are transient.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries"/> is a negative number other than -1, which represents an infinite number of retries.
        /// </exception>
        public ReliableAction(Action<T1, T2, T3> action, int maxRetries, DelayPolicy delayPolicy, RetryPolicy retryPolicy)
        {
            if (maxRetries < -1)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            _action     = action;
            MaxRetries  = maxRetries;
            DelayPolicy = delayPolicy;
            RetryPolicy = retryPolicy;
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3)
        {
            TimeSpan delay;
            int attempt = 0;
            do
            {
                attempt++;
                try
                {
                    _action(arg1, arg2, arg3);
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
            {
                Thread.Sleep(delay);
                Retrying?.Invoke();
            }

            return continueRetry;
        }
    }

    #endregion

    #region ReliableAction<T1, T2, T3, T4>

    /// <summary>
    /// A wrapper to reliably invoke a delegate in case of transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    public sealed class ReliableAction<T1, T2, T3, T4>
    {
        /// <summary>
        /// Occurs when the action must be retried due to a transient exception.
        /// </summary>
        public event Action Retrying;

        /// <summary>
        /// Gets the maximum number of retry attempts.
        /// </summary>
        /// <value>
        /// The maximum number of attempts the underlying <see cref="Action{T1, T2, T3, T4}"/>
        /// should be invoked if there are transient exceptions.
        /// </value>
        public int MaxRetries { get; }

        /// <summary>
        /// Gets the policy that determines how long wait to wait between retries.
        /// </summary>
        /// <value>
        /// The <see cref="DelayPolicy"/> that determines how long wait to wait between retries.
        /// </value>
        public DelayPolicy DelayPolicy { get; }

        /// <summary>
        /// Gets the policy that determines which errors are transient.
        /// </summary>
        /// <value>
        /// The <see cref="RetryPolicy"/> that indicates which errors are transient.
        /// </value>
        public RetryPolicy RetryPolicy { get; }

        private readonly Action<T1, T2, T3, T4> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction{T1, T2, T3, T4}"/>
        /// class that executes the given <see cref="Action{T1, T2, T3, T4}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="action">The underlying action to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <param name="retryPolicy">The policy that determines which errors are transient.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries"/> is a negative number other than -1, which represents an infinite number of retries.
        /// </exception>
        public ReliableAction(Action<T1, T2, T3, T4> action, int maxRetries, DelayPolicy delayPolicy, RetryPolicy retryPolicy)
        {
            if (maxRetries < -1)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            _action     = action;
            MaxRetries  = maxRetries;
            DelayPolicy = delayPolicy;
            RetryPolicy = retryPolicy;
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            TimeSpan delay;
            int attempt = 0;
            do
            {
                attempt++;
                try
                {
                    _action(arg1, arg2, arg3, arg4);
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
            {
                Thread.Sleep(delay);
                Retrying?.Invoke();
            }

            return continueRetry;
        }
    }

    #endregion

    #region ReliableAction<T1, T2, T3, T4, T5>

    /// <summary>
    /// A wrapper to reliably invoke a delegate in case of transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    public sealed class ReliableAction<T1, T2, T3, T4, T5>
    {
        /// <summary>
        /// Occurs when the action must be retried due to a transient exception.
        /// </summary>
        public event Action Retrying;

        /// <summary>
        /// Gets the maximum number of retry attempts.
        /// </summary>
        /// <value>
        /// The maximum number of attempts the underlying <see cref="Action{T1, T2, T3, T4, T5}"/>
        /// should be invoked if there are transient exceptions.
        /// </value>
        public int MaxRetries { get; }

        /// <summary>
        /// Gets the policy that determines how long wait to wait between retries.
        /// </summary>
        /// <value>
        /// The <see cref="DelayPolicy"/> that determines how long wait to wait between retries.
        /// </value>
        public DelayPolicy DelayPolicy { get; }

        /// <summary>
        /// Gets the policy that determines which errors are transient.
        /// </summary>
        /// <value>
        /// The <see cref="RetryPolicy"/> that indicates which errors are transient.
        /// </value>
        public RetryPolicy RetryPolicy { get; }

        private readonly Action<T1, T2, T3, T4, T5> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction{T1, T2, T3, T4, T5}"/>
        /// class that executes the given <see cref="Action{T1, T2, T3, T4, T5}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="action">The underlying action to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <param name="retryPolicy">The policy that determines which errors are transient.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries"/> is a negative number other than -1, which represents an infinite number of retries.
        /// </exception>
        public ReliableAction(Action<T1, T2, T3, T4, T5> action, int maxRetries, DelayPolicy delayPolicy, RetryPolicy retryPolicy)
        {
            if (maxRetries < -1)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            _action     = action;
            MaxRetries  = maxRetries;
            DelayPolicy = delayPolicy;
            RetryPolicy = retryPolicy;
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            TimeSpan delay;
            int attempt = 0;
            do
            {
                attempt++;
                try
                {
                    _action(arg1, arg2, arg3, arg4, arg5);
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
            {
                Thread.Sleep(delay);
                Retrying?.Invoke();
            }

            return continueRetry;
        }
    }

    #endregion

    #region ReliableAction<T1, T2, T3, T4, T5, T6>

    /// <summary>
    /// A wrapper to reliably invoke a delegate in case of transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    public sealed class ReliableAction<T1, T2, T3, T4, T5, T6>
    {
        /// <summary>
        /// Occurs when the action must be retried due to a transient exception.
        /// </summary>
        public event Action Retrying;

        /// <summary>
        /// Gets the maximum number of retry attempts.
        /// </summary>
        /// <value>
        /// The maximum number of attempts the underlying <see cref="Action{T1, T2, T3, T4, T5, T6}"/>
        /// should be invoked if there are transient exceptions.
        /// </value>
        public int MaxRetries { get; }

        /// <summary>
        /// Gets the policy that determines how long wait to wait between retries.
        /// </summary>
        /// <value>
        /// The <see cref="DelayPolicy"/> that determines how long wait to wait between retries.
        /// </value>
        public DelayPolicy DelayPolicy { get; }

        /// <summary>
        /// Gets the policy that determines which errors are transient.
        /// </summary>
        /// <value>
        /// The <see cref="RetryPolicy"/> that indicates which errors are transient.
        /// </value>
        public RetryPolicy RetryPolicy { get; }

        private readonly Action<T1, T2, T3, T4, T5, T6> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction{T1, T2, T3, T4, T5, T6}"/>
        /// class that executes the given <see cref="Action{T1, T2, T3, T4, T5, T6}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="action">The underlying action to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <param name="retryPolicy">The policy that determines which errors are transient.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries"/> is a negative number other than -1, which represents an infinite number of retries.
        /// </exception>
        public ReliableAction(Action<T1, T2, T3, T4, T5, T6> action, int maxRetries, DelayPolicy delayPolicy, RetryPolicy retryPolicy)
        {
            if (maxRetries < -1)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            _action     = action;
            MaxRetries  = maxRetries;
            DelayPolicy = delayPolicy;
            RetryPolicy = retryPolicy;
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            TimeSpan delay;
            int attempt = 0;
            do
            {
                attempt++;
                try
                {
                    _action(arg1, arg2, arg3, arg4, arg5, arg6);
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
            {
                Thread.Sleep(delay);
                Retrying?.Invoke();
            }

            return continueRetry;
        }
    }

    #endregion

    #region ReliableAction<T1, T2, T3, T4, T5, T6, T7>

    /// <summary>
    /// A wrapper to reliably invoke a delegate in case of transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
    public sealed class ReliableAction<T1, T2, T3, T4, T5, T6, T7>
    {
        /// <summary>
        /// Occurs when the action must be retried due to a transient exception.
        /// </summary>
        public event Action Retrying;

        /// <summary>
        /// Gets the maximum number of retry attempts.
        /// </summary>
        /// <value>
        /// The maximum number of attempts the underlying <see cref="Action{T1, T2, T3, T4, T5, T6, T7}"/>
        /// should be invoked if there are transient exceptions.
        /// </value>
        public int MaxRetries { get; }

        /// <summary>
        /// Gets the policy that determines how long wait to wait between retries.
        /// </summary>
        /// <value>
        /// The <see cref="DelayPolicy"/> that determines how long wait to wait between retries.
        /// </value>
        public DelayPolicy DelayPolicy { get; }

        /// <summary>
        /// Gets the policy that determines which errors are transient.
        /// </summary>
        /// <value>
        /// The <see cref="RetryPolicy"/> that indicates which errors are transient.
        /// </value>
        public RetryPolicy RetryPolicy { get; }

        private readonly Action<T1, T2, T3, T4, T5, T6, T7> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction{T1, T2, T3, T4, T5, T6, T7}"/>
        /// class that executes the given <see cref="Action{T1, T2, T3, T4, T5, T6, T7}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="action">The underlying action to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <param name="retryPolicy">The policy that determines which errors are transient.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries"/> is a negative number other than -1, which represents an infinite number of retries.
        /// </exception>
        public ReliableAction(Action<T1, T2, T3, T4, T5, T6, T7> action, int maxRetries, DelayPolicy delayPolicy, RetryPolicy retryPolicy)
        {
            if (maxRetries < -1)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            _action     = action;
            MaxRetries  = maxRetries;
            DelayPolicy = delayPolicy;
            RetryPolicy = retryPolicy;
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <param name="arg7">The seventh argument for the underlying delegate.</param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            TimeSpan delay;
            int attempt = 0;
            do
            {
                attempt++;
                try
                {
                    _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
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
            {
                Thread.Sleep(delay);
                Retrying?.Invoke();
            }

            return continueRetry;
        }
    }

    #endregion

    #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8>

    /// <summary>
    /// A wrapper to reliably invoke a delegate in case of transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
    public sealed class ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8>
    {
        /// <summary>
        /// Occurs when the action must be retried due to a transient exception.
        /// </summary>
        public event Action Retrying;

        /// <summary>
        /// Gets the maximum number of retry attempts.
        /// </summary>
        /// <value>
        /// The maximum number of attempts the underlying <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8}"/>
        /// should be invoked if there are transient exceptions.
        /// </value>
        public int MaxRetries { get; }

        /// <summary>
        /// Gets the policy that determines how long wait to wait between retries.
        /// </summary>
        /// <value>
        /// The <see cref="DelayPolicy"/> that determines how long wait to wait between retries.
        /// </value>
        public DelayPolicy DelayPolicy { get; }

        /// <summary>
        /// Gets the policy that determines which errors are transient.
        /// </summary>
        /// <value>
        /// The <see cref="RetryPolicy"/> that indicates which errors are transient.
        /// </value>
        public RetryPolicy RetryPolicy { get; }

        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction{T1, T2, T3, T4, T5, T6, T7, T8}"/>
        /// class that executes the given <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="action">The underlying action to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <param name="retryPolicy">The policy that determines which errors are transient.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries"/> is a negative number other than -1, which represents an infinite number of retries.
        /// </exception>
        public ReliableAction(Action<T1, T2, T3, T4, T5, T6, T7, T8> action, int maxRetries, DelayPolicy delayPolicy, RetryPolicy retryPolicy)
        {
            if (maxRetries < -1)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            _action     = action;
            MaxRetries  = maxRetries;
            DelayPolicy = delayPolicy;
            RetryPolicy = retryPolicy;
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <param name="arg7">The seventh argument for the underlying delegate.</param>
        /// <param name="arg8">The eighth argument for the underlying delegate.</param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            TimeSpan delay;
            int attempt = 0;
            do
            {
                attempt++;
                try
                {
                    _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
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
            {
                Thread.Sleep(delay);
                Retrying?.Invoke();
            }

            return continueRetry;
        }
    }

    #endregion

    #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9>

    /// <summary>
    /// A wrapper to reliably invoke a delegate in case of transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
    public sealed class ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        /// <summary>
        /// Occurs when the action must be retried due to a transient exception.
        /// </summary>
        public event Action Retrying;

        /// <summary>
        /// Gets the maximum number of retry attempts.
        /// </summary>
        /// <value>
        /// The maximum number of attempts the underlying <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9}"/>
        /// should be invoked if there are transient exceptions.
        /// </value>
        public int MaxRetries { get; }

        /// <summary>
        /// Gets the policy that determines how long wait to wait between retries.
        /// </summary>
        /// <value>
        /// The <see cref="DelayPolicy"/> that determines how long wait to wait between retries.
        /// </value>
        public DelayPolicy DelayPolicy { get; }

        /// <summary>
        /// Gets the policy that determines which errors are transient.
        /// </summary>
        /// <value>
        /// The <see cref="RetryPolicy"/> that indicates which errors are transient.
        /// </value>
        public RetryPolicy RetryPolicy { get; }

        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction{T1, T2, T3, T4, T5, T6, T7, T8, T9}"/>
        /// class that executes the given <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="action">The underlying action to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <param name="retryPolicy">The policy that determines which errors are transient.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries"/> is a negative number other than -1, which represents an infinite number of retries.
        /// </exception>
        public ReliableAction(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action, int maxRetries, DelayPolicy delayPolicy, RetryPolicy retryPolicy)
        {
            if (maxRetries < -1)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            _action     = action;
            MaxRetries  = maxRetries;
            DelayPolicy = delayPolicy;
            RetryPolicy = retryPolicy;
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <param name="arg7">The seventh argument for the underlying delegate.</param>
        /// <param name="arg8">The eighth argument for the underlying delegate.</param>
        /// <param name="arg9">The ninth argument for the underlying delegate.</param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            TimeSpan delay;
            int attempt = 0;
            do
            {
                attempt++;
                try
                {
                    _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
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
            {
                Thread.Sleep(delay);
                Retrying?.Invoke();
            }

            return continueRetry;
        }
    }

    #endregion

    #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>

    /// <summary>
    /// A wrapper to reliably invoke a delegate in case of transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
    public sealed class ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
    {
        /// <summary>
        /// Occurs when the action must be retried due to a transient exception.
        /// </summary>
        public event Action Retrying;

        /// <summary>
        /// Gets the maximum number of retry attempts.
        /// </summary>
        /// <value>
        /// The maximum number of attempts the underlying <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10}"/>
        /// should be invoked if there are transient exceptions.
        /// </value>
        public int MaxRetries { get; }

        /// <summary>
        /// Gets the policy that determines how long wait to wait between retries.
        /// </summary>
        /// <value>
        /// The <see cref="DelayPolicy"/> that determines how long wait to wait between retries.
        /// </value>
        public DelayPolicy DelayPolicy { get; }

        /// <summary>
        /// Gets the policy that determines which errors are transient.
        /// </summary>
        /// <value>
        /// The <see cref="RetryPolicy"/> that indicates which errors are transient.
        /// </value>
        public RetryPolicy RetryPolicy { get; }

        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10}"/>
        /// class that executes the given <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="action">The underlying action to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <param name="retryPolicy">The policy that determines which errors are transient.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries"/> is a negative number other than -1, which represents an infinite number of retries.
        /// </exception>
        public ReliableAction(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action, int maxRetries, DelayPolicy delayPolicy, RetryPolicy retryPolicy)
        {
            if (maxRetries < -1)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            _action     = action;
            MaxRetries  = maxRetries;
            DelayPolicy = delayPolicy;
            RetryPolicy = retryPolicy;
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <param name="arg7">The seventh argument for the underlying delegate.</param>
        /// <param name="arg8">The eighth argument for the underlying delegate.</param>
        /// <param name="arg9">The ninth argument for the underlying delegate.</param>
        /// <param name="arg10">The tenth argument for the underlying delegate.</param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            TimeSpan delay;
            int attempt = 0;
            do
            {
                attempt++;
                try
                {
                    _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
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
            {
                Thread.Sleep(delay);
                Retrying?.Invoke();
            }

            return continueRetry;
        }
    }

    #endregion

    #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>

    /// <summary>
    /// A wrapper to reliably invoke a delegate in case of transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
    public sealed class ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
    {
        /// <summary>
        /// Occurs when the action must be retried due to a transient exception.
        /// </summary>
        public event Action Retrying;

        /// <summary>
        /// Gets the maximum number of retry attempts.
        /// </summary>
        /// <value>
        /// The maximum number of attempts the underlying <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11}"/>
        /// should be invoked if there are transient exceptions.
        /// </value>
        public int MaxRetries { get; }

        /// <summary>
        /// Gets the policy that determines how long wait to wait between retries.
        /// </summary>
        /// <value>
        /// The <see cref="DelayPolicy"/> that determines how long wait to wait between retries.
        /// </value>
        public DelayPolicy DelayPolicy { get; }

        /// <summary>
        /// Gets the policy that determines which errors are transient.
        /// </summary>
        /// <value>
        /// The <see cref="RetryPolicy"/> that indicates which errors are transient.
        /// </value>
        public RetryPolicy RetryPolicy { get; }

        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11}"/>
        /// class that executes the given <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="action">The underlying action to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <param name="retryPolicy">The policy that determines which errors are transient.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries"/> is a negative number other than -1, which represents an infinite number of retries.
        /// </exception>
        public ReliableAction(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action, int maxRetries, DelayPolicy delayPolicy, RetryPolicy retryPolicy)
        {
            if (maxRetries < -1)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            _action     = action;
            MaxRetries  = maxRetries;
            DelayPolicy = delayPolicy;
            RetryPolicy = retryPolicy;
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <param name="arg7">The seventh argument for the underlying delegate.</param>
        /// <param name="arg8">The eighth argument for the underlying delegate.</param>
        /// <param name="arg9">The ninth argument for the underlying delegate.</param>
        /// <param name="arg10">The tenth argument for the underlying delegate.</param>
        /// <param name="arg11">The eleventh argument for the underlying delegate.</param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            TimeSpan delay;
            int attempt = 0;
            do
            {
                attempt++;
                try
                {
                    _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
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
            {
                Thread.Sleep(delay);
                Retrying?.Invoke();
            }

            return continueRetry;
        }
    }

    #endregion

    #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>

    /// <summary>
    /// A wrapper to reliably invoke a delegate in case of transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
    public sealed class ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
    {
        /// <summary>
        /// Occurs when the action must be retried due to a transient exception.
        /// </summary>
        public event Action Retrying;

        /// <summary>
        /// Gets the maximum number of retry attempts.
        /// </summary>
        /// <value>
        /// The maximum number of attempts the underlying <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12}"/>
        /// should be invoked if there are transient exceptions.
        /// </value>
        public int MaxRetries { get; }

        /// <summary>
        /// Gets the policy that determines how long wait to wait between retries.
        /// </summary>
        /// <value>
        /// The <see cref="DelayPolicy"/> that determines how long wait to wait between retries.
        /// </value>
        public DelayPolicy DelayPolicy { get; }

        /// <summary>
        /// Gets the policy that determines which errors are transient.
        /// </summary>
        /// <value>
        /// The <see cref="RetryPolicy"/> that indicates which errors are transient.
        /// </value>
        public RetryPolicy RetryPolicy { get; }

        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12}"/>
        /// class that executes the given <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="action">The underlying action to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <param name="retryPolicy">The policy that determines which errors are transient.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries"/> is a negative number other than -1, which represents an infinite number of retries.
        /// </exception>
        public ReliableAction(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action, int maxRetries, DelayPolicy delayPolicy, RetryPolicy retryPolicy)
        {
            if (maxRetries < -1)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            _action     = action;
            MaxRetries  = maxRetries;
            DelayPolicy = delayPolicy;
            RetryPolicy = retryPolicy;
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <param name="arg7">The seventh argument for the underlying delegate.</param>
        /// <param name="arg8">The eighth argument for the underlying delegate.</param>
        /// <param name="arg9">The ninth argument for the underlying delegate.</param>
        /// <param name="arg10">The tenth argument for the underlying delegate.</param>
        /// <param name="arg11">The eleventh argument for the underlying delegate.</param>
        /// <param name="arg12">The twelfth argument for the underlying delegate.</param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            TimeSpan delay;
            int attempt = 0;
            do
            {
                attempt++;
                try
                {
                    _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
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
            {
                Thread.Sleep(delay);
                Retrying?.Invoke();
            }

            return continueRetry;
        }
    }

    #endregion

    #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>

    /// <summary>
    /// A wrapper to reliably invoke a delegate in case of transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T13">The type of the thirteenth parameter of the underlying delegate.</typeparam>
    public sealed class ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
    {
        /// <summary>
        /// Occurs when the action must be retried due to a transient exception.
        /// </summary>
        public event Action Retrying;

        /// <summary>
        /// Gets the maximum number of retry attempts.
        /// </summary>
        /// <value>
        /// The maximum number of attempts the underlying <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13}"/>
        /// should be invoked if there are transient exceptions.
        /// </value>
        public int MaxRetries { get; }

        /// <summary>
        /// Gets the policy that determines how long wait to wait between retries.
        /// </summary>
        /// <value>
        /// The <see cref="DelayPolicy"/> that determines how long wait to wait between retries.
        /// </value>
        public DelayPolicy DelayPolicy { get; }

        /// <summary>
        /// Gets the policy that determines which errors are transient.
        /// </summary>
        /// <value>
        /// The <see cref="RetryPolicy"/> that indicates which errors are transient.
        /// </value>
        public RetryPolicy RetryPolicy { get; }

        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13}"/>
        /// class that executes the given <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="action">The underlying action to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <param name="retryPolicy">The policy that determines which errors are transient.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries"/> is a negative number other than -1, which represents an infinite number of retries.
        /// </exception>
        public ReliableAction(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action, int maxRetries, DelayPolicy delayPolicy, RetryPolicy retryPolicy)
        {
            if (maxRetries < -1)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            _action     = action;
            MaxRetries  = maxRetries;
            DelayPolicy = delayPolicy;
            RetryPolicy = retryPolicy;
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <param name="arg7">The seventh argument for the underlying delegate.</param>
        /// <param name="arg8">The eighth argument for the underlying delegate.</param>
        /// <param name="arg9">The ninth argument for the underlying delegate.</param>
        /// <param name="arg10">The tenth argument for the underlying delegate.</param>
        /// <param name="arg11">The eleventh argument for the underlying delegate.</param>
        /// <param name="arg12">The twelfth argument for the underlying delegate.</param>
        /// <param name="arg13">The thirteenth argument for the underlying delegate.</param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            TimeSpan delay;
            int attempt = 0;
            do
            {
                attempt++;
                try
                {
                    _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
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
            {
                Thread.Sleep(delay);
                Retrying?.Invoke();
            }

            return continueRetry;
        }
    }

    #endregion

    #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>

    /// <summary>
    /// A wrapper to reliably invoke a delegate in case of transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T13">The type of the thirteenth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T14">The type of the fourteenth parameter of the underlying delegate.</typeparam>
    public sealed class ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
    {
        /// <summary>
        /// Occurs when the action must be retried due to a transient exception.
        /// </summary>
        public event Action Retrying;

        /// <summary>
        /// Gets the maximum number of retry attempts.
        /// </summary>
        /// <value>
        /// The maximum number of attempts the underlying <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14}"/>
        /// should be invoked if there are transient exceptions.
        /// </value>
        public int MaxRetries { get; }

        /// <summary>
        /// Gets the policy that determines how long wait to wait between retries.
        /// </summary>
        /// <value>
        /// The <see cref="DelayPolicy"/> that determines how long wait to wait between retries.
        /// </value>
        public DelayPolicy DelayPolicy { get; }

        /// <summary>
        /// Gets the policy that determines which errors are transient.
        /// </summary>
        /// <value>
        /// The <see cref="RetryPolicy"/> that indicates which errors are transient.
        /// </value>
        public RetryPolicy RetryPolicy { get; }

        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14}"/>
        /// class that executes the given <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="action">The underlying action to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <param name="retryPolicy">The policy that determines which errors are transient.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries"/> is a negative number other than -1, which represents an infinite number of retries.
        /// </exception>
        public ReliableAction(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action, int maxRetries, DelayPolicy delayPolicy, RetryPolicy retryPolicy)
        {
            if (maxRetries < -1)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            _action     = action;
            MaxRetries  = maxRetries;
            DelayPolicy = delayPolicy;
            RetryPolicy = retryPolicy;
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <param name="arg7">The seventh argument for the underlying delegate.</param>
        /// <param name="arg8">The eighth argument for the underlying delegate.</param>
        /// <param name="arg9">The ninth argument for the underlying delegate.</param>
        /// <param name="arg10">The tenth argument for the underlying delegate.</param>
        /// <param name="arg11">The eleventh argument for the underlying delegate.</param>
        /// <param name="arg12">The twelfth argument for the underlying delegate.</param>
        /// <param name="arg13">The thirteenth argument for the underlying delegate.</param>
        /// <param name="arg14">The fourteenth argument for the underlying delegate.</param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            TimeSpan delay;
            int attempt = 0;
            do
            {
                attempt++;
                try
                {
                    _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
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
            {
                Thread.Sleep(delay);
                Retrying?.Invoke();
            }

            return continueRetry;
        }
    }

    #endregion

    #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>

    /// <summary>
    /// A wrapper to reliably invoke a delegate in case of transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T13">The type of the thirteenth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T14">The type of the fourteenth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T15">The type of the fifteenth parameter of the underlying delegate.</typeparam>
    public sealed class ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
    {
        /// <summary>
        /// Occurs when the action must be retried due to a transient exception.
        /// </summary>
        public event Action Retrying;

        /// <summary>
        /// Gets the maximum number of retry attempts.
        /// </summary>
        /// <value>
        /// The maximum number of attempts the underlying <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15}"/>
        /// should be invoked if there are transient exceptions.
        /// </value>
        public int MaxRetries { get; }

        /// <summary>
        /// Gets the policy that determines how long wait to wait between retries.
        /// </summary>
        /// <value>
        /// The <see cref="DelayPolicy"/> that determines how long wait to wait between retries.
        /// </value>
        public DelayPolicy DelayPolicy { get; }

        /// <summary>
        /// Gets the policy that determines which errors are transient.
        /// </summary>
        /// <value>
        /// The <see cref="RetryPolicy"/> that indicates which errors are transient.
        /// </value>
        public RetryPolicy RetryPolicy { get; }

        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15}"/>
        /// class that executes the given <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="action">The underlying action to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <param name="retryPolicy">The policy that determines which errors are transient.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries"/> is a negative number other than -1, which represents an infinite number of retries.
        /// </exception>
        public ReliableAction(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action, int maxRetries, DelayPolicy delayPolicy, RetryPolicy retryPolicy)
        {
            if (maxRetries < -1)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            _action     = action;
            MaxRetries  = maxRetries;
            DelayPolicy = delayPolicy;
            RetryPolicy = retryPolicy;
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <param name="arg7">The seventh argument for the underlying delegate.</param>
        /// <param name="arg8">The eighth argument for the underlying delegate.</param>
        /// <param name="arg9">The ninth argument for the underlying delegate.</param>
        /// <param name="arg10">The tenth argument for the underlying delegate.</param>
        /// <param name="arg11">The eleventh argument for the underlying delegate.</param>
        /// <param name="arg12">The twelfth argument for the underlying delegate.</param>
        /// <param name="arg13">The thirteenth argument for the underlying delegate.</param>
        /// <param name="arg14">The fourteenth argument for the underlying delegate.</param>
        /// <param name="arg15">The fifteenth argument for the underlying delegate.</param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            TimeSpan delay;
            int attempt = 0;
            do
            {
                attempt++;
                try
                {
                    _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
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
            {
                Thread.Sleep(delay);
                Retrying?.Invoke();
            }

            return continueRetry;
        }
    }

    #endregion

    #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>

    /// <summary>
    /// A wrapper to reliably invoke a delegate in case of transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T13">The type of the thirteenth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T14">The type of the fourteenth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T15">The type of the fifteenth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T16">The type of the sixteenth parameter of the underlying delegate.</typeparam>
    public sealed class ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
    {
        /// <summary>
        /// Occurs when the action must be retried due to a transient exception.
        /// </summary>
        public event Action Retrying;

        /// <summary>
        /// Gets the maximum number of retry attempts.
        /// </summary>
        /// <value>
        /// The maximum number of attempts the underlying <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16}"/>
        /// should be invoked if there are transient exceptions.
        /// </value>
        public int MaxRetries { get; }

        /// <summary>
        /// Gets the policy that determines how long wait to wait between retries.
        /// </summary>
        /// <value>
        /// The <see cref="DelayPolicy"/> that determines how long wait to wait between retries.
        /// </value>
        public DelayPolicy DelayPolicy { get; }

        /// <summary>
        /// Gets the policy that determines which errors are transient.
        /// </summary>
        /// <value>
        /// The <see cref="RetryPolicy"/> that indicates which errors are transient.
        /// </value>
        public RetryPolicy RetryPolicy { get; }

        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16}"/>
        /// class that executes the given <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="action">The underlying action to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <param name="retryPolicy">The policy that determines which errors are transient.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries"/> is a negative number other than -1, which represents an infinite number of retries.
        /// </exception>
        public ReliableAction(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action, int maxRetries, DelayPolicy delayPolicy, RetryPolicy retryPolicy)
        {
            if (maxRetries < -1)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            _action     = action;
            MaxRetries  = maxRetries;
            DelayPolicy = delayPolicy;
            RetryPolicy = retryPolicy;
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <param name="arg7">The seventh argument for the underlying delegate.</param>
        /// <param name="arg8">The eighth argument for the underlying delegate.</param>
        /// <param name="arg9">The ninth argument for the underlying delegate.</param>
        /// <param name="arg10">The tenth argument for the underlying delegate.</param>
        /// <param name="arg11">The eleventh argument for the underlying delegate.</param>
        /// <param name="arg12">The twelfth argument for the underlying delegate.</param>
        /// <param name="arg13">The thirteenth argument for the underlying delegate.</param>
        /// <param name="arg14">The fourteenth argument for the underlying delegate.</param>
        /// <param name="arg15">The fifteenth argument for the underlying delegate.</param>
        /// <param name="arg16">The sixteenth argument for the underlying delegate.</param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            TimeSpan delay;
            int attempt = 0;
            do
            {
                attempt++;
                try
                {
                    _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
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
            {
                Thread.Sleep(delay);
                Retrying?.Invoke();
            }

            return continueRetry;
        }
    }

    #endregion

}
