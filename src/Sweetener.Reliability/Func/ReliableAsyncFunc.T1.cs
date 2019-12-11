// Generated from ReliableAsyncFunc.tt
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sweetener.Reliability
{
    /// <summary>
    /// A wrapper to reliably invoke an asynchronous function despite transient issues.
    /// </summary>
    /// <typeparam name="TResult">The type of the return value of the method that this reliable delegate encapsulates.</typeparam>
    public sealed class ReliableAsyncFunc<TResult> : ReliableDelegate<TResult>
    {
        private readonly AsyncFunc<TResult> _func;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncFunc{TResult}"/>
        /// class that executes the given <see cref="AsyncFunc{TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="func">The function to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableAsyncFunc(AsyncFunc<TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncFunc{TResult}"/>
        /// class that executes the given <see cref="AsyncFunc{TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="func">The function to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableAsyncFunc(AsyncFunc<TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncFunc{TResult}"/>
        /// class that executes the given <see cref="AsyncFunc{TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="func">The function to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableAsyncFunc(AsyncFunc<TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncFunc{TResult}"/>
        /// class that executes the given <see cref="AsyncFunc{TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="func">The function to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableAsyncFunc(AsyncFunc<TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Asynchronously invokes the encapsulated method despite transient errors.
        /// </summary>
        /// <returns>The return value of the method that this reliable delegate encapsulates.</returns>
        public async Task<TResult> InvokeAsync()
            => await InvokeAsync(CancellationToken.None).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <returns>The return value of the method that this reliable delegate encapsulates.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The provided <paramref name="cancellationToken"/> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public async Task<TResult> InvokeAsync(CancellationToken cancellationToken)
        {
            TResult result;
            int attempt = 0;

        Attempt:
            attempt++;
            try
            {
                result = await _func().ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                if (await CanRetryAsync(attempt, exception, cancellationToken).ConfigureAwait(false))
                    goto Attempt;

                throw;
            }

            ResultKind kind = _validate(result);
            if (kind == ResultKind.Successful || !await CanRetryAsync(attempt, result, kind, cancellationToken).ConfigureAwait(false))
                return result;

            goto Attempt;
        }
    }
}
