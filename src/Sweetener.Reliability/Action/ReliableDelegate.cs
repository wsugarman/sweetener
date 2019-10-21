using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sweetener.Reliability
{
    /// <summary>
    /// Represents the method that will handle an operation failure event.
    /// </summary>
    /// <param name="exception">The exception that caused the operation to fail.</param>
    public delegate void FailureHandler(Exception exception);

    /// <summary>
    /// Represents the method that will handle an exhausted retries event.
    /// </summary>
    /// <param name="exception">The last exception that caused the operation to retry.</param>
    public delegate void ExhaustedRetriesHandler(Exception exception);

    /// <summary>
    /// Represents the method that will handle an operation retry event.
    /// </summary>
    /// <param name="attempt">The number of the attempt.</param>
    /// <param name="exception">The exception that caused the operation to retry.</param>
    public delegate void RetryHandler(int attempt, Exception exception);

    /// <summary>
    /// A base class that has a number of events to monitor the execution of an operation.
    /// </summary>
    public abstract class ReliableDelegate
    {
        /// <summary>
        /// Occurs when the operation has failed due to a fatal exception.
        /// </summary>
        public event FailureHandler Failed;

        /// <summary>
        /// Occurs when the operation cannot be retried because
        /// the maxiumum number of attempts has been made.
        /// </summary>
        public event ExhaustedRetriesHandler RetriesExhausted;

        /// <summary>
        /// Occurs when the operation must be retried due to a transient exception.
        /// </summary>
        public event RetryHandler Retrying;

        /// <summary>
        /// Gets the maximum number of retry attempts.
        /// </summary>
        /// <value>
        /// The maximum number of attempts that the underlying delegate should be invoked
        /// if there are transient exceptions.
        /// </value>
        public int MaxRetries { get; }

        private readonly ExceptionPolicy    _canRetry;
        private readonly ComplexDelayPolicy _getDelay;

        internal ReliableDelegate(int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : this(maxRetries, exceptionPolicy, delayPolicy != null ? (i, e) => delayPolicy(i) : (ComplexDelayPolicy)null)
        { }

        internal ReliableDelegate(int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy delayPolicy)
        {
            if (maxRetries < Retries.Infinite)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            MaxRetries = maxRetries;
            _canRetry  = exceptionPolicy ?? throw new ArgumentNullException(nameof(exceptionPolicy));
            _getDelay  = delayPolicy     ?? throw new ArgumentNullException(nameof(delayPolicy    ));
        }

        internal bool CanRetry(int attempt, Exception exception, CancellationToken cancellationToken)
        {
            if (!_canRetry(exception))
            {
                OnFailure(exception);
                return false;
            }
            else if (MaxRetries == Retries.Infinite || attempt <= MaxRetries)
            {
                Task.Delay(_getDelay(attempt, exception), cancellationToken).Wait(cancellationToken);
                OnRetry(attempt, exception);
                return true;
            }
            else
            {
                OnRetriesExhausted(exception);
                return false;
            }
        }

        internal virtual void OnFailure(Exception exception)
            => Failed?.Invoke(exception);

        internal virtual void OnRetriesExhausted(Exception exception)
            => RetriesExhausted?.Invoke(exception);

        internal virtual void OnRetry(int attempt, Exception exception)
            => Retrying?.Invoke(attempt, exception);
    }
}
