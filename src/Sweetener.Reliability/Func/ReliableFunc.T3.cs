// Generated from ReliableFunc.tt
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sweetener.Reliability
{
    /// <summary>
    /// A wrapper to reliably invoke a function despite transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this reliable delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this reliable delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this reliable delegate encapsulates.</typeparam>
    public sealed class ReliableFunc<T1, T2, TResult> : ReliableDelegate<TResult>
    {
        private readonly Func<T1, T2, CancellationToken, TResult> _func;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, TResult}"/>
        /// class that executes the given function at most a specific number of times
        /// based on the provided policies.
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
        public ReliableFunc(Func<T1, T2, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : this(func.IgnoreInterruption(), maxRetries, exceptionPolicy, delayPolicy)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, TResult}"/>
        /// class that executes the given function at most a specific number of times
        /// based on the provided policies.
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
        public ReliableFunc(Func<T1, T2, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : this(func.IgnoreInterruption(), maxRetries, exceptionPolicy, delayPolicy)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, TResult}"/>
        /// class that executes the given function at most a specific number of times
        /// based on the provided policies.
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
        public ReliableFunc(Func<T1, T2, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : this(func.IgnoreInterruption(), maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, TResult}"/>
        /// class that executes the given function at most a specific number of times
        /// based on the provided policies.
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
        public ReliableFunc(Func<T1, T2, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : this(func.IgnoreInterruption(), maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, TResult}"/>
        /// class that executes the given function at most a specific number of times
        /// based on the provided policies.
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
        public ReliableFunc(Func<T1, T2, CancellationToken, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, TResult}"/>
        /// class that executes the given function at most a specific number of times
        /// based on the provided policies.
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
        public ReliableFunc(Func<T1, T2, CancellationToken, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, TResult}"/>
        /// class that executes the given function at most a specific number of times
        /// based on the provided policies.
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
        public ReliableFunc(Func<T1, T2, CancellationToken, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, TResult}"/>
        /// class that executes the given function at most a specific number of times
        /// based on the provided policies.
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
        public ReliableFunc(Func<T1, T2, CancellationToken, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Invokes the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="arg1">The first parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this reliable delegate encapsulates.</param>
        /// <returns>The return value of the method that this reliable delegate encapsulates.</returns>
        public TResult Invoke(T1 arg1, T2 arg2)
            => Invoke(arg1, arg2, CancellationToken.None);

        /// <summary>
        /// Invokes the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="arg1">The first parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <returns>The return value of the method that this reliable delegate encapsulates.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public TResult Invoke(T1 arg1, T2 arg2, CancellationToken cancellationToken)
        {
            TResult result;
            int attempt = 0;

        Attempt:
            attempt++;
            try
            {
                result = _func(arg1, arg2, cancellationToken);
            }
            catch (Exception e)
            {
                if (e.IsCancellation(cancellationToken) || !CanRetry(attempt, e, cancellationToken))
                    throw;

                goto Attempt;
            }

            ResultKind kind = _validate(result);
            if (kind == ResultKind.Successful || !CanRetry(attempt, result, kind, cancellationToken))
                return result;

            goto Attempt;
        }

        /// <summary>
        /// Asynchronously invokes the encapsulated method despite transient errors.
        /// </summary>
        /// <remarks>
        /// If the encapsulated method succeeds without retrying, the method executes synchronously.
        /// </remarks>
        /// <param name="arg1">The first parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this reliable delegate encapsulates.</param>
        /// <returns>The return value of the method that this reliable delegate encapsulates.</returns>
        public async Task<TResult> InvokeAsync(T1 arg1, T2 arg2)
            => await InvokeAsync(arg1, arg2, CancellationToken.None).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes the encapsulated method despite transient errors.
        /// </summary>
        /// <remarks>
        /// If the encapsulated method succeeds without retrying, the method executes synchronously.
        /// </remarks>
        /// <param name="arg1">The first parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <returns>The return value of the method that this reliable delegate encapsulates.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public async Task<TResult> InvokeAsync(T1 arg1, T2 arg2, CancellationToken cancellationToken)
        {
            TResult result;
            int attempt = 0;

        Attempt:
            attempt++;
            try
            {
                result = _func(arg1, arg2, cancellationToken);
            }
            catch (Exception e)
            {
                if (e.IsCancellation(cancellationToken) || !await CanRetryAsync(attempt, e, cancellationToken).ConfigureAwait(false))
                    throw;

                goto Attempt;
            }

            ResultKind kind = _validate(result);
            if (kind == ResultKind.Successful || !await CanRetryAsync(attempt, result, kind, cancellationToken).ConfigureAwait(false))
                return result;

            goto Attempt;
        }

        /// <summary>
        /// Attempts to successfully invoke the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="arg1">The first parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="result">
        /// When this method returns, contains the result of the underlying delegate,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in result will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed successfully
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, out TResult result)
            => TryInvoke(arg1, arg2, CancellationToken.None, out result);

        /// <summary>
        /// Attempts to successfully invoke the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="arg1">The first parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <param name="result">
        /// When this method returns, contains the result of the underlying delegate,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in result will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed successfully
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public bool TryInvoke(T1 arg1, T2 arg2, CancellationToken cancellationToken, out TResult result)
        {
            int attempt = 0;
            bool retry = false;
            do
            {
                attempt++;
                try
                {
                    result = _func(arg1, arg2, cancellationToken);
                }
                catch (Exception e)
                {
                    if (e.IsCancellation(cancellationToken))
                        throw;

                    retry = CanRetry(attempt, e, cancellationToken);
                    continue;
                }

                ResultKind kind = _validate(result);
                if (kind == ResultKind.Successful)
                    return true;

                retry = CanRetry(attempt, result, kind, cancellationToken);
            } while (retry);

            result = default;
            return false;
        }
    }
}
