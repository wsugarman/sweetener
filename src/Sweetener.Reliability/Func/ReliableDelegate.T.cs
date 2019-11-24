using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Sweetener.Reliability
{
    /// <summary>
    /// Represents the method that will handle an operation failure event.
    /// </summary>
    /// <param name="result">
    /// The result that caused the operation to fail, if returned; otherwise the default value.
    /// </param>
    /// <param name="exception">
    /// The exception that caused the operation to fail, if thrown; otherwise <see langword="null" />.
    /// </param>
    public delegate void FailureHandler<T>(T result, Exception exception);

    /// <summary>
    /// Represents the method that will handle an exhausted retries event.
    /// </summary>
    /// <param name="result">
    /// The result that caused the operation to retry, if returned; otherwise the default value.
    /// </param>
    /// <param name="exception">
    /// The exception that caused the operation to retry, if thrown; otherwise <see langword="null" />.
    /// </param>
    public delegate void ExhaustedRetriesHandler<T>(T result, Exception exception);

    /// <summary>
    /// Represents the method that will handle an operation retry event.
    /// </summary>
    /// <param name="attempt">The number of the attempt.</param>
    /// <param name="result">
    /// The result that caused the operation to retry, if returned; otherwise the default value.
    /// </param>
    /// <param name="exception">
    /// The exception that caused the operation to retry, if thrown; otherwise <see langword="null" />.
    /// </param>
    public delegate void RetryHandler<T>(int attempt, T result, Exception exception);

    /// <summary>
    /// A base class that has a number of events to monitor the execution of an operation.
    /// </summary>
    public abstract class ReliableDelegate<T>
    {
        /// <summary>
        /// Occurs when the operation has failed due to a fatal result or exception.
        /// </summary>
        public event FailureHandler<T> Failed;

        /// <summary>
        /// Occurs when the operation cannot be retried because
        /// the maxiumum number of attempts has been made.
        /// </summary>
        public event ExhaustedRetriesHandler<T> RetriesExhausted;

        /// <summary>
        /// Occurs when the operation must be retried due to an invalid result or transient exception.
        /// </summary>
        public event RetryHandler<T> Retrying;

        /// <summary>
        /// Gets the maximum number of retry attempts.
        /// </summary>
        /// <value>
        /// The maximum number of attempts that the underlying delegate should be invoked
        /// if there are transient exceptions.
        /// </value>
        public int MaxRetries { get; }

        internal readonly ResultPolicy<T>       _validate;
        private  readonly ExceptionPolicy       _canRetry;
        private  readonly ComplexDelayPolicy<T> _getDelay;

        internal static readonly ResultPolicy<T> DefaultResultPolicy = r => ResultKind.Successful;

        internal ReliableDelegate(int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : this(maxRetries, DefaultResultPolicy, exceptionPolicy, DelayPolicies.Complex<T>(delayPolicy))
        { }

        internal ReliableDelegate(int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<T> delayPolicy)
            : this(maxRetries, DefaultResultPolicy, exceptionPolicy, delayPolicy)
        { }

        internal ReliableDelegate(int maxRetries, ResultPolicy<T> resultPolicy, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : this(maxRetries, resultPolicy, exceptionPolicy, delayPolicy != null ? (i, r, e) => delayPolicy(i) : (ComplexDelayPolicy<T>)null)
        { }

        internal ReliableDelegate(int maxRetries, ResultPolicy<T> resultPolicy, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<T> delayPolicy)
        {
            if (maxRetries < Retries.Infinite)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            MaxRetries = maxRetries;
            _validate  = resultPolicy    ?? throw new ArgumentNullException(nameof(resultPolicy   ));
            _canRetry  = exceptionPolicy ?? throw new ArgumentNullException(nameof(exceptionPolicy));
            _getDelay  = delayPolicy     ?? throw new ArgumentNullException(nameof(delayPolicy    ));
        }

        internal bool CanRetry(int attempt, T result, ResultKind kind, CancellationToken cancellationToken)
        {
            Debug.Assert(kind != ResultKind.Successful, "Successful results should not attempt to retry.");

            if (kind == ResultKind.Retryable)
            {
                if (MaxRetries == Retries.Infinite || attempt <= MaxRetries)
                {
                    Task.Delay(_getDelay(attempt, result, null), cancellationToken).Wait(cancellationToken);
                    OnRetry(attempt, result, null);
                    return true;
                }

                OnRetriesExhausted(result, null);
                return false;
            }
            else
            {
                OnFailure(result, null);
                return false;
            }
        }

        internal bool CanRetry(int attempt, Exception exception, CancellationToken cancellationToken)
        {
            if (!_canRetry(exception))
            {
                OnFailure(default, exception);
                return false;
            }
            else if (MaxRetries == Retries.Infinite || attempt <= MaxRetries)
            {
                Task.Delay(_getDelay(attempt, default, exception), cancellationToken).Wait(cancellationToken);
                OnRetry(attempt, default, exception);
                return true;
            }
            else
            {
                OnRetriesExhausted(default, exception);
                return false;
            }
        }

        internal virtual void OnFailure(T result, Exception exception)
            => Failed?.Invoke(result, exception);

        internal virtual void OnRetriesExhausted(T result, Exception exception)
            => RetriesExhausted?.Invoke(result, exception);

        internal virtual void OnRetry(int attempt, T result, Exception exception)
            => Retrying?.Invoke(attempt, result, exception);
    }
}
