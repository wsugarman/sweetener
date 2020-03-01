// Generated from ReliableFunc.tt
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Sweetener.Reliability
{
    /// <summary>
    /// A wrapper to reliably invoke a function despite transient issues.
    /// </summary>
    /// <typeparam name="TResult">The type of the return value of the method that this reliable delegate encapsulates.</typeparam>
    public sealed class ReliableFunc<TResult> : ReliableDelegate<TResult>
    {
        private readonly Func<CancellationToken, TResult> _func;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{TResult}"/>
        /// class that executes the given function at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <param name="func">The function to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which errors are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableFunc(Func<TResult> func, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            : this(func.IgnoreInterruption(), maxRetries, exceptionHandler, delayHandler)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{TResult}"/>
        /// class that executes the given function at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <param name="func">The function to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which errors are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableFunc(Func<TResult> func, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            : this(func.IgnoreInterruption(), maxRetries, exceptionHandler, delayHandler)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{TResult}"/>
        /// class that executes the given function at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <param name="func">The function to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which errors are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableFunc(Func<TResult> func, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            : this(func.IgnoreInterruption(), maxRetries, resultHandler, exceptionHandler, delayHandler)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{TResult}"/>
        /// class that executes the given function at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <param name="func">The function to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which errors are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableFunc(Func<TResult> func, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            : this(func.IgnoreInterruption(), maxRetries, resultHandler, exceptionHandler, delayHandler)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{TResult}"/>
        /// class that executes the given function at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <param name="func">The function to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which errors are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableFunc(Func<CancellationToken, TResult> func, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            : base(maxRetries, exceptionHandler, delayHandler)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{TResult}"/>
        /// class that executes the given function at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <param name="func">The function to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which errors are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableFunc(Func<CancellationToken, TResult> func, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            : base(maxRetries, exceptionHandler, delayHandler)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{TResult}"/>
        /// class that executes the given function at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <param name="func">The function to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which errors are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableFunc(Func<CancellationToken, TResult> func, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            : base(maxRetries, resultHandler, exceptionHandler, delayHandler)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{TResult}"/>
        /// class that executes the given function at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <param name="func">The function to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which errors are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableFunc(Func<CancellationToken, TResult> func, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            : base(maxRetries, resultHandler, exceptionHandler, delayHandler)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Invokes the encapsulated method despite transient errors.
        /// </summary>
        /// <returns>The return value of the method that this reliable delegate encapsulates.</returns>
        public TResult Invoke()
            => Invoke(CancellationToken.None);

        /// <summary>
        /// Invokes the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <returns>The return value of the method that this reliable delegate encapsulates.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public TResult Invoke(CancellationToken cancellationToken)
        {
            TResult result;
            int attempt = 0;

        Attempt:
            attempt++;

            try
            {
                result = _func(cancellationToken);
            }
            catch (Exception e)
            {
                if (e.IsCancellation(cancellationToken) || !CanRetry(attempt, e, cancellationToken))
                    throw;

                goto Attempt;
            }

            if (MoveNext(attempt, result, cancellationToken) != FunctionState.Retry)
                return result;

            goto Attempt;
        }

        /// <summary>
        /// Asynchronously invokes the encapsulated method despite transient errors.
        /// </summary>
        /// <remarks>
        /// If the encapsulated method succeeds without retrying, the method executes synchronously.
        /// </remarks>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter contains the return value of the method that this reliable delegate encapsulates.
        /// </returns>
        public async Task<TResult> InvokeAsync()
            => await InvokeAsync(CancellationToken.None).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes the encapsulated method despite transient errors.
        /// </summary>
        /// <remarks>
        /// If the encapsulated method succeeds without retrying, the method executes synchronously.
        /// </remarks>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter contains the return value of the method that this reliable delegate encapsulates.
        /// </returns>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
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
                result = _func(cancellationToken);
            }
            catch (Exception e)
            {
                if (e.IsCancellation(cancellationToken) || !await CanRetryAsync(attempt, e, cancellationToken).ConfigureAwait(false))
                    throw;

                goto Attempt;
            }

            if (await MoveNextAsync(attempt, result, cancellationToken).ConfigureAwait(false) != FunctionState.Retry)
                return result;

            goto Attempt;
        }

        /// <summary>
        /// Attempts to successfully invoke the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="result">
        /// When this method returns, contains the result of the underlying delegate,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in result will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the encapsulated method completed successfully
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke([MaybeNullWhen(false)] out TResult result)
            => TryInvoke(CancellationToken.None, out result);

        /// <summary>
        /// Attempts to successfully invoke the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <param name="result">
        /// When this method returns, contains the result of the underlying delegate,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in result will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the encapsulated method completed successfully
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public bool TryInvoke(CancellationToken cancellationToken, [MaybeNullWhen(false)] out TResult result)
        {
            int attempt = 0;

        Attempt:
            attempt++;

            try
            {
                result = _func(cancellationToken);
            }
            catch (Exception e)
            {
                if (e.IsCancellation(cancellationToken))
                    throw;

                if (!CanRetry(attempt, e, cancellationToken))
                    goto Fail;

                goto Attempt;
            }

            switch (MoveNext(attempt, result, cancellationToken))
            {
                case FunctionState.ReturnSuccess:
                    return true;
                case FunctionState.ReturnFailure:
                    goto Fail;
                default:
                    goto Attempt;
            }

        Fail:
            result = default!;
            return false;
        }

        /// <summary>
        /// Asynchronously attempts to successfully invoke the encapsulated method despite transient errors.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter optionally contains the result of the encapsulated method if it succeeded.
        /// Otherwise the value is left undefined if the encapsulated method failed.
        /// </returns>
        public async Task<Optional<TResult>> TryInvokeAsync()
            => await TryInvokeAsync(CancellationToken.None).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously attempts to successfully invoke the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter optionally contains the result of the encapsulated method if it succeeded.
        /// Otherwise the value is left undefined if the encapsulated method failed.
        /// </returns>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public async Task<Optional<TResult>> TryInvokeAsync(CancellationToken cancellationToken)
        {
            int attempt = 0;

        Attempt:
            TResult result;
            attempt++;

            try
            {
                result = _func(cancellationToken);
            }
            catch (Exception e)
            {
                if (e.IsCancellation(cancellationToken))
                    throw;

                if (!await CanRetryAsync(attempt, e, cancellationToken).ConfigureAwait(false))
                    goto Fail;

                goto Attempt;
            }

            switch (await MoveNextAsync(attempt, result, cancellationToken).ConfigureAwait(false))
            {
                case FunctionState.ReturnSuccess:
                    return result;
                case FunctionState.ReturnFailure:
                    goto Fail;
                default:
                    goto Attempt;
            }

        Fail:
            return Optional<TResult>.Undefined;
        }
    }
}
