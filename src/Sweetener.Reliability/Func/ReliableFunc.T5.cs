// Generated from ReliableFunc.tt
using System;
using System.Threading;

namespace Sweetener.Reliability
{
    /// <summary>
    /// A wrapper to reliably invoke a function despite transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
    public sealed class ReliableFunc<T1, T2, T3, T4, TResult> : ReliableDelegate<TResult>
    {
        private readonly Func<T1, T2, T3, T4, TResult> _func;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableFunc(Func<T1, T2, T3, T4, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableFunc(Func<T1, T2, T3, T4, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="func">The underlying function to invoke.</param>
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
        public ReliableFunc(Func<T1, T2, T3, T4, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="func">The underlying function to invoke.</param>
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
        public ReliableFunc(Func<T1, T2, T3, T4, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            => Invoke(arg1, arg2, arg3, arg4, CancellationToken.None);

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, CancellationToken cancellationToken)
        {
            TResult result;
            int attempt = 0;

        Attempt:
            attempt++;
            try
            {
                result = _func(arg1, arg2, arg3, arg4);
            }
            catch (Exception exception)
            {
                if (CanRetry(attempt, exception, cancellationToken))
                    goto Attempt;

                throw;
            }

            ResultKind kind = _validate(result);
            if (kind == ResultKind.Successful || !CanRetry(attempt, result, kind, cancellationToken))
                return result;

            goto Attempt;
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="result">
        /// When this method returns, contains the result of the underlying delegate,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in result will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed successfully
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, out TResult result)
            => TryInvoke(arg1, arg2, arg3, arg4, CancellationToken.None, out result);

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
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
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, CancellationToken cancellationToken, out TResult result)
        {
            int attempt = 0;
            bool retry = false;
            do
            {
                attempt++;
                try
                {
                    result = _func(arg1, arg2, arg3, arg4);
                }
                catch (Exception exception)
                {
                    retry = CanRetry(attempt, exception, cancellationToken);
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
