using System;
using System.Diagnostics.CodeAnalysis;
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
    public delegate void FailureHandler<T>([AllowNull] T result, Exception? exception);

    /// <summary>
    /// Represents the method that will handle an exhausted retries event.
    /// </summary>
    /// <param name="result">
    /// The result that caused the operation to retry, if returned; otherwise the default value.
    /// </param>
    /// <param name="exception">
    /// The exception that caused the operation to retry, if thrown; otherwise <see langword="null" />.
    /// </param>
    public delegate void ExhaustedRetriesHandler<T>([AllowNull] T result, Exception? exception);

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
    public delegate void RetryHandler<T>(int attempt, [AllowNull] T result, Exception? exception);

    /// <summary>
    /// A base class that has a number of events to monitor the execution of an operation.
    /// </summary>
    public abstract class ReliableDelegate<T>
    {
        /// <summary>
        /// Occurs when the operation has failed due to a fatal result or exception.
        /// </summary>
        public event FailureHandler<T>? Failed;

        /// <summary>
        /// Occurs when the operation cannot be retried because
        /// the maxiumum number of attempts has been made.
        /// </summary>
        public event ExhaustedRetriesHandler<T>? RetriesExhausted;

        /// <summary>
        /// Occurs when the operation must be retried due to an invalid result or transient exception.
        /// </summary>
        public event RetryHandler<T>? Retrying;

        /// <summary>
        /// Gets the maximum number of retry attempts.
        /// </summary>
        /// <value>
        /// The maximum number of attempts that the underlying delegate should be invoked
        /// if there are transient exceptions.
        /// </value>
        public int MaxRetries { get; }

        private readonly ResultHandler<T>       _validate;
        private readonly ExceptionHandler       _canRetry;
        private readonly ComplexDelayHandler<T> _getDelay;

        internal ReliableDelegate(int maxRetries, ExceptionHandler exceptionPolicy, DelayHandler delayPolicy)
            : this(maxRetries, ResultPolicy.Default<T>(), exceptionPolicy, delayPolicy.ToComplex<T>())
        { }

        internal ReliableDelegate(int maxRetries, ExceptionHandler exceptionPolicy, ComplexDelayHandler<T> delayPolicy)
            : this(maxRetries, ResultPolicy.Default<T>(), exceptionPolicy, delayPolicy)
        { }

        internal ReliableDelegate(int maxRetries, ResultHandler<T> resultPolicy, ExceptionHandler exceptionPolicy, DelayHandler delayPolicy)
            : this(maxRetries, resultPolicy, exceptionPolicy, delayPolicy.ToComplex<T>())
        { }

        internal ReliableDelegate(int maxRetries, ResultHandler<T> resultPolicy, ExceptionHandler exceptionPolicy, ComplexDelayHandler<T> delayPolicy)
        {
            if (maxRetries < Retries.Infinite)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            MaxRetries = maxRetries;
            _validate  = resultPolicy    ?? throw new ArgumentNullException(nameof(resultPolicy   ));
            _canRetry  = exceptionPolicy ?? throw new ArgumentNullException(nameof(exceptionPolicy));
            _getDelay  = delayPolicy     ?? throw new ArgumentNullException(nameof(delayPolicy    ));
        }

        internal FunctionState MoveNext(int attempt, T result, CancellationToken cancellationToken)
        {
            switch (_validate(result))
            {
                case ResultKind.Successful:
                    return FunctionState.ReturnSuccess;
                case ResultKind.Transient:
                    if (MaxRetries == Retries.Infinite || attempt <= MaxRetries)
                    {
                        Task.Delay(_getDelay(attempt, result, default), cancellationToken).Wait(cancellationToken);
                        OnRetry(attempt, result, default);
                        return FunctionState.Retry;
                    }

                    OnRetriesExhausted(result, default);
                    return FunctionState.ReturnFailure;
                default:
                    OnFailure(result, default);
                    return FunctionState.ReturnFailure;
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

        internal async Task<FunctionState> MoveNextAsync(int attempt, T result, CancellationToken cancellationToken)
        {
            switch (_validate(result))
            {
                case ResultKind.Successful:
                    return FunctionState.ReturnSuccess;
                case ResultKind.Transient:
                    if (MaxRetries == Retries.Infinite || attempt <= MaxRetries)
                    {
                        await Task.Delay(_getDelay(attempt, result, default), cancellationToken).ConfigureAwait(false);
                        OnRetry(attempt, result, default);
                        return FunctionState.Retry;
                    }

                    OnRetriesExhausted(result, default);
                    return FunctionState.ReturnFailure;
                default:
                    OnFailure(result, default);
                    return FunctionState.ReturnFailure;
            }
        }

        internal async Task<bool> CanRetryAsync(int attempt, Exception exception, CancellationToken cancellationToken)
        {
            if (!_canRetry(exception))
            {
                OnFailure(default, exception);
                return false;
            }
            else if (MaxRetries == Retries.Infinite || attempt <= MaxRetries)
            {
                await Task.Delay(_getDelay(attempt, default, exception), cancellationToken).ConfigureAwait(false);
                OnRetry(attempt, default, exception);
                return true;
            }
            else
            {
                OnRetriesExhausted(default, exception);
                return false;
            }
        }

        internal virtual void OnFailure([AllowNull] T result, Exception? exception)
            => Failed?.Invoke(result, exception);

        internal virtual void OnRetriesExhausted([AllowNull] T result, Exception? exception)
            => RetriesExhausted?.Invoke(result, exception);

        internal virtual void OnRetry(int attempt, [AllowNull] T result, Exception? exception)
            => Retrying?.Invoke(attempt, result, exception);

        internal enum FunctionState
        {
            ReturnFailure = 0,
            Retry         = 1,
            ReturnSuccess = 2,
        }
    }
}
