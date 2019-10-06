// Generated from ReliableFunc.tt
using System;
using System.Threading;

namespace Sweetener.Reliability
{
    #region ReliableFunc<TResult>

    /// <summary>
    /// A wrapper to reliably invoke a function despite transient issues.
    /// </summary>
    /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
    public sealed class ReliableFunc<TResult> : ReliableDelegate<TResult>
    {
        private readonly Func<TResult> _func;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{TResult}"/>
        /// class that executes the given <see cref="Func{TResult}"/> at most a
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
        public ReliableFunc(Func<TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{TResult}"/>
        /// class that executes the given <see cref="Func{TResult}"/> at most a
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
        public ReliableFunc(Func<TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{TResult}"/>
        /// class that executes the given <see cref="Func{TResult}"/> at most a
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
        public ReliableFunc(Func<TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{TResult}"/>
        /// class that executes the given <see cref="Func{TResult}"/> at most a
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
        public ReliableFunc(Func<TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
        /// </summary>
        /// <param name="result">
        /// When this method returns, contains the result of the underlying delegate,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in result will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed successfully
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(out TResult result)
            => TryInvoke(CancellationToken.None, out result);

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
        /// </summary>
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
        public bool TryInvoke(CancellationToken cancellationToken, out TResult result)
        {
            int attempt = 0;
            bool retry = false;
            do
            {
                attempt++;
                try
                {
                    result = _func();
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

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
        /// </summary>
        /// <returns>The return value of the underlying delegate.</returns>
        public TResult Invoke()
            => Invoke(CancellationToken.None);

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public TResult Invoke(CancellationToken cancellationToken)
        {
            TResult result;
            int attempt = 0;
        Attempt:
            attempt++;
            try
            {
                result = _func();
            }
            catch (Exception exception)
            {
                if (CanRetry(attempt, exception, cancellationToken))
                    goto Attempt;

                throw exception;
            }

            ResultKind kind = _validate(result);
            if (kind == ResultKind.Successful || !CanRetry(attempt, result, kind, cancellationToken))
                return result;

            goto Attempt;
        }

        /// <summary>
        /// Implicitly converts the <paramref name="reliableFunc"/> to an
        /// <see cref="Func{TResult}"/>.
        /// </summary>
        /// <remarks>
        /// The resulting function is equivalent to <see cref="Invoke()"/>.
        /// </remarks>
        /// <param name="reliableFunc">An operation that may be retried due to transient failures.</param>
        public static implicit operator Func<TResult>(ReliableFunc<TResult> reliableFunc)
            => reliableFunc.Invoke;
    }

    #endregion

    #region ReliableFunc<T, TResult>

    /// <summary>
    /// A wrapper to reliably invoke a function despite transient issues.
    /// </summary>
    /// <typeparam name="T">The type of the parameter of the underlying delegate.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
    public sealed class ReliableFunc<T, TResult> : ReliableDelegate<TResult>
    {
        private readonly Func<T, TResult> _func;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T, TResult}"/>
        /// class that executes the given <see cref="Func{T, TResult}"/> at most a
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
        public ReliableFunc(Func<T, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T, TResult}"/>
        /// class that executes the given <see cref="Func{T, TResult}"/> at most a
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
        public ReliableFunc(Func<T, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T, TResult}"/>
        /// class that executes the given <see cref="Func{T, TResult}"/> at most a
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
        public ReliableFunc(Func<T, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T, TResult}"/>
        /// class that executes the given <see cref="Func{T, TResult}"/> at most a
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
        public ReliableFunc(Func<T, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
        /// </summary>
        /// <param name="arg">The argument for the underlying delegate.</param>
        /// <param name="result">
        /// When this method returns, contains the result of the underlying delegate,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in result will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed successfully
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T arg, out TResult result)
            => TryInvoke(arg, CancellationToken.None, out result);

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
        /// </summary>
        /// <param name="arg">The argument for the underlying delegate.</param>
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
        public bool TryInvoke(T arg, CancellationToken cancellationToken, out TResult result)
        {
            int attempt = 0;
            bool retry = false;
            do
            {
                attempt++;
                try
                {
                    result = _func(arg);
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

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
        /// </summary>
        /// <param name="arg">The argument for the underlying delegate.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        public TResult Invoke(T arg)
            => Invoke(arg, CancellationToken.None);

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
        /// </summary>
        /// <param name="arg">The argument for the underlying delegate.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public TResult Invoke(T arg, CancellationToken cancellationToken)
        {
            TResult result;
            int attempt = 0;
        Attempt:
            attempt++;
            try
            {
                result = _func(arg);
            }
            catch (Exception exception)
            {
                if (CanRetry(attempt, exception, cancellationToken))
                    goto Attempt;

                throw exception;
            }

            ResultKind kind = _validate(result);
            if (kind == ResultKind.Successful || !CanRetry(attempt, result, kind, cancellationToken))
                return result;

            goto Attempt;
        }

        /// <summary>
        /// Implicitly converts the <paramref name="reliableFunc"/> to an
        /// <see cref="Func{T, TResult}"/>.
        /// </summary>
        /// <remarks>
        /// The resulting function is equivalent to <see cref="Invoke(T)"/>.
        /// </remarks>
        /// <param name="reliableFunc">An operation that may be retried due to transient failures.</param>
        public static implicit operator Func<T, TResult>(ReliableFunc<T, TResult> reliableFunc)
            => reliableFunc.Invoke;
    }

    #endregion

    #region ReliableFunc<T1, T2, TResult>

    /// <summary>
    /// A wrapper to reliably invoke a function despite transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
    public sealed class ReliableFunc<T1, T2, TResult> : ReliableDelegate<TResult>
    {
        private readonly Func<T1, T2, TResult> _func;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
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
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
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
        public bool TryInvoke(T1 arg1, T2 arg2, CancellationToken cancellationToken, out TResult result)
        {
            int attempt = 0;
            bool retry = false;
            do
            {
                attempt++;
                try
                {
                    result = _func(arg1, arg2);
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

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        public TResult Invoke(T1 arg1, T2 arg2)
            => Invoke(arg1, arg2, CancellationToken.None);

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public TResult Invoke(T1 arg1, T2 arg2, CancellationToken cancellationToken)
        {
            TResult result;
            int attempt = 0;
        Attempt:
            attempt++;
            try
            {
                result = _func(arg1, arg2);
            }
            catch (Exception exception)
            {
                if (CanRetry(attempt, exception, cancellationToken))
                    goto Attempt;

                throw exception;
            }

            ResultKind kind = _validate(result);
            if (kind == ResultKind.Successful || !CanRetry(attempt, result, kind, cancellationToken))
                return result;

            goto Attempt;
        }

        /// <summary>
        /// Implicitly converts the <paramref name="reliableFunc"/> to an
        /// <see cref="Func{T1, T2, TResult}"/>.
        /// </summary>
        /// <remarks>
        /// The resulting function is equivalent to <see cref="Invoke(T1, T2)"/>.
        /// </remarks>
        /// <param name="reliableFunc">An operation that may be retried due to transient failures.</param>
        public static implicit operator Func<T1, T2, TResult>(ReliableFunc<T1, T2, TResult> reliableFunc)
            => reliableFunc.Invoke;
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, TResult>

    /// <summary>
    /// A wrapper to reliably invoke a function despite transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
    public sealed class ReliableFunc<T1, T2, T3, TResult> : ReliableDelegate<TResult>
    {
        private readonly Func<T1, T2, T3, TResult> _func;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="result">
        /// When this method returns, contains the result of the underlying delegate,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in result will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed successfully
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, out TResult result)
            => TryInvoke(arg1, arg2, arg3, CancellationToken.None, out result);

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
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
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, CancellationToken cancellationToken, out TResult result)
        {
            int attempt = 0;
            bool retry = false;
            do
            {
                attempt++;
                try
                {
                    result = _func(arg1, arg2, arg3);
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

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3)
            => Invoke(arg1, arg2, arg3, CancellationToken.None);

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, CancellationToken cancellationToken)
        {
            TResult result;
            int attempt = 0;
        Attempt:
            attempt++;
            try
            {
                result = _func(arg1, arg2, arg3);
            }
            catch (Exception exception)
            {
                if (CanRetry(attempt, exception, cancellationToken))
                    goto Attempt;

                throw exception;
            }

            ResultKind kind = _validate(result);
            if (kind == ResultKind.Successful || !CanRetry(attempt, result, kind, cancellationToken))
                return result;

            goto Attempt;
        }

        /// <summary>
        /// Implicitly converts the <paramref name="reliableFunc"/> to an
        /// <see cref="Func{T1, T2, T3, TResult}"/>.
        /// </summary>
        /// <remarks>
        /// The resulting function is equivalent to <see cref="Invoke(T1, T2, T3)"/>.
        /// </remarks>
        /// <param name="reliableFunc">An operation that may be retried due to transient failures.</param>
        public static implicit operator Func<T1, T2, T3, TResult>(ReliableFunc<T1, T2, T3, TResult> reliableFunc)
            => reliableFunc.Invoke;
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, TResult>

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

                throw exception;
            }

            ResultKind kind = _validate(result);
            if (kind == ResultKind.Successful || !CanRetry(attempt, result, kind, cancellationToken))
                return result;

            goto Attempt;
        }

        /// <summary>
        /// Implicitly converts the <paramref name="reliableFunc"/> to an
        /// <see cref="Func{T1, T2, T3, T4, TResult}"/>.
        /// </summary>
        /// <remarks>
        /// The resulting function is equivalent to <see cref="Invoke(T1, T2, T3, T4)"/>.
        /// </remarks>
        /// <param name="reliableFunc">An operation that may be retried due to transient failures.</param>
        public static implicit operator Func<T1, T2, T3, T4, TResult>(ReliableFunc<T1, T2, T3, T4, TResult> reliableFunc)
            => reliableFunc.Invoke;
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, T5, TResult>

    /// <summary>
    /// A wrapper to reliably invoke a function despite transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
    public sealed class ReliableFunc<T1, T2, T3, T4, T5, TResult> : ReliableDelegate<TResult>
    {
        private readonly Func<T1, T2, T3, T4, T5, TResult> _func;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="result">
        /// When this method returns, contains the result of the underlying delegate,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in result will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed successfully
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, out TResult result)
            => TryInvoke(arg1, arg2, arg3, arg4, arg5, CancellationToken.None, out result);

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
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
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, CancellationToken cancellationToken, out TResult result)
        {
            int attempt = 0;
            bool retry = false;
            do
            {
                attempt++;
                try
                {
                    result = _func(arg1, arg2, arg3, arg4, arg5);
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

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            => Invoke(arg1, arg2, arg3, arg4, arg5, CancellationToken.None);

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, CancellationToken cancellationToken)
        {
            TResult result;
            int attempt = 0;
        Attempt:
            attempt++;
            try
            {
                result = _func(arg1, arg2, arg3, arg4, arg5);
            }
            catch (Exception exception)
            {
                if (CanRetry(attempt, exception, cancellationToken))
                    goto Attempt;

                throw exception;
            }

            ResultKind kind = _validate(result);
            if (kind == ResultKind.Successful || !CanRetry(attempt, result, kind, cancellationToken))
                return result;

            goto Attempt;
        }

        /// <summary>
        /// Implicitly converts the <paramref name="reliableFunc"/> to an
        /// <see cref="Func{T1, T2, T3, T4, T5, TResult}"/>.
        /// </summary>
        /// <remarks>
        /// The resulting function is equivalent to <see cref="Invoke(T1, T2, T3, T4, T5)"/>.
        /// </remarks>
        /// <param name="reliableFunc">An operation that may be retried due to transient failures.</param>
        public static implicit operator Func<T1, T2, T3, T4, T5, TResult>(ReliableFunc<T1, T2, T3, T4, T5, TResult> reliableFunc)
            => reliableFunc.Invoke;
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, T5, T6, TResult>

    /// <summary>
    /// A wrapper to reliably invoke a function despite transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
    public sealed class ReliableFunc<T1, T2, T3, T4, T5, T6, TResult> : ReliableDelegate<TResult>
    {
        private readonly Func<T1, T2, T3, T4, T5, T6, TResult> _func;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <param name="result">
        /// When this method returns, contains the result of the underlying delegate,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in result will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed successfully
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, out TResult result)
            => TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, CancellationToken.None, out result);

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
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
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, CancellationToken cancellationToken, out TResult result)
        {
            int attempt = 0;
            bool retry = false;
            do
            {
                attempt++;
                try
                {
                    result = _func(arg1, arg2, arg3, arg4, arg5, arg6);
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

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
            => Invoke(arg1, arg2, arg3, arg4, arg5, arg6, CancellationToken.None);

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, CancellationToken cancellationToken)
        {
            TResult result;
            int attempt = 0;
        Attempt:
            attempt++;
            try
            {
                result = _func(arg1, arg2, arg3, arg4, arg5, arg6);
            }
            catch (Exception exception)
            {
                if (CanRetry(attempt, exception, cancellationToken))
                    goto Attempt;

                throw exception;
            }

            ResultKind kind = _validate(result);
            if (kind == ResultKind.Successful || !CanRetry(attempt, result, kind, cancellationToken))
                return result;

            goto Attempt;
        }

        /// <summary>
        /// Implicitly converts the <paramref name="reliableFunc"/> to an
        /// <see cref="Func{T1, T2, T3, T4, T5, T6, TResult}"/>.
        /// </summary>
        /// <remarks>
        /// The resulting function is equivalent to <see cref="Invoke(T1, T2, T3, T4, T5, T6)"/>.
        /// </remarks>
        /// <param name="reliableFunc">An operation that may be retried due to transient failures.</param>
        public static implicit operator Func<T1, T2, T3, T4, T5, T6, TResult>(ReliableFunc<T1, T2, T3, T4, T5, T6, TResult> reliableFunc)
            => reliableFunc.Invoke;
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, T5, T6, T7, TResult>

    /// <summary>
    /// A wrapper to reliably invoke a function despite transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
    public sealed class ReliableFunc<T1, T2, T3, T4, T5, T6, T7, TResult> : ReliableDelegate<TResult>
    {
        private readonly Func<T1, T2, T3, T4, T5, T6, T7, TResult> _func;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <param name="arg7">The seventh argument for the underlying delegate.</param>
        /// <param name="result">
        /// When this method returns, contains the result of the underlying delegate,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in result will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed successfully
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, out TResult result)
            => TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, CancellationToken.None, out result);

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <param name="arg7">The seventh argument for the underlying delegate.</param>
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
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, CancellationToken cancellationToken, out TResult result)
        {
            int attempt = 0;
            bool retry = false;
            do
            {
                attempt++;
                try
                {
                    result = _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
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

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <param name="arg7">The seventh argument for the underlying delegate.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
            => Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, CancellationToken.None);

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <param name="arg7">The seventh argument for the underlying delegate.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, CancellationToken cancellationToken)
        {
            TResult result;
            int attempt = 0;
        Attempt:
            attempt++;
            try
            {
                result = _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            }
            catch (Exception exception)
            {
                if (CanRetry(attempt, exception, cancellationToken))
                    goto Attempt;

                throw exception;
            }

            ResultKind kind = _validate(result);
            if (kind == ResultKind.Successful || !CanRetry(attempt, result, kind, cancellationToken))
                return result;

            goto Attempt;
        }

        /// <summary>
        /// Implicitly converts the <paramref name="reliableFunc"/> to an
        /// <see cref="Func{T1, T2, T3, T4, T5, T6, T7, TResult}"/>.
        /// </summary>
        /// <remarks>
        /// The resulting function is equivalent to <see cref="Invoke(T1, T2, T3, T4, T5, T6, T7)"/>.
        /// </remarks>
        /// <param name="reliableFunc">An operation that may be retried due to transient failures.</param>
        public static implicit operator Func<T1, T2, T3, T4, T5, T6, T7, TResult>(ReliableFunc<T1, T2, T3, T4, T5, T6, T7, TResult> reliableFunc)
            => reliableFunc.Invoke;
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult>

    /// <summary>
    /// A wrapper to reliably invoke a function despite transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
    public sealed class ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult> : ReliableDelegate<TResult>
    {
        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> _func;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <param name="arg7">The seventh argument for the underlying delegate.</param>
        /// <param name="arg8">The eighth argument for the underlying delegate.</param>
        /// <param name="result">
        /// When this method returns, contains the result of the underlying delegate,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in result will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed successfully
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, out TResult result)
            => TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, CancellationToken.None, out result);

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <param name="arg7">The seventh argument for the underlying delegate.</param>
        /// <param name="arg8">The eighth argument for the underlying delegate.</param>
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
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, CancellationToken cancellationToken, out TResult result)
        {
            int attempt = 0;
            bool retry = false;
            do
            {
                attempt++;
                try
                {
                    result = _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
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

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <param name="arg7">The seventh argument for the underlying delegate.</param>
        /// <param name="arg8">The eighth argument for the underlying delegate.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
            => Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, CancellationToken.None);

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <param name="arg7">The seventh argument for the underlying delegate.</param>
        /// <param name="arg8">The eighth argument for the underlying delegate.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, CancellationToken cancellationToken)
        {
            TResult result;
            int attempt = 0;
        Attempt:
            attempt++;
            try
            {
                result = _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            }
            catch (Exception exception)
            {
                if (CanRetry(attempt, exception, cancellationToken))
                    goto Attempt;

                throw exception;
            }

            ResultKind kind = _validate(result);
            if (kind == ResultKind.Successful || !CanRetry(attempt, result, kind, cancellationToken))
                return result;

            goto Attempt;
        }

        /// <summary>
        /// Implicitly converts the <paramref name="reliableFunc"/> to an
        /// <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/>.
        /// </summary>
        /// <remarks>
        /// The resulting function is equivalent to <see cref="Invoke(T1, T2, T3, T4, T5, T6, T7, T8)"/>.
        /// </remarks>
        /// <param name="reliableFunc">An operation that may be retried due to transient failures.</param>
        public static implicit operator Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult> reliableFunc)
            => reliableFunc.Invoke;
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>

    /// <summary>
    /// A wrapper to reliably invoke a function despite transient issues.
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
    /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
    public sealed class ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> : ReliableDelegate<TResult>
    {
        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> _func;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
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
        /// <param name="result">
        /// When this method returns, contains the result of the underlying delegate,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in result will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed successfully
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, out TResult result)
            => TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, CancellationToken.None, out result);

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
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
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, CancellationToken cancellationToken, out TResult result)
        {
            int attempt = 0;
            bool retry = false;
            do
            {
                attempt++;
                try
                {
                    result = _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
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

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
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
        /// <returns>The return value of the underlying delegate.</returns>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
            => Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, CancellationToken.None);

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
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
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, CancellationToken cancellationToken)
        {
            TResult result;
            int attempt = 0;
        Attempt:
            attempt++;
            try
            {
                result = _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            }
            catch (Exception exception)
            {
                if (CanRetry(attempt, exception, cancellationToken))
                    goto Attempt;

                throw exception;
            }

            ResultKind kind = _validate(result);
            if (kind == ResultKind.Successful || !CanRetry(attempt, result, kind, cancellationToken))
                return result;

            goto Attempt;
        }

        /// <summary>
        /// Implicitly converts the <paramref name="reliableFunc"/> to an
        /// <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult}"/>.
        /// </summary>
        /// <remarks>
        /// The resulting function is equivalent to <see cref="Invoke(T1, T2, T3, T4, T5, T6, T7, T8, T9)"/>.
        /// </remarks>
        /// <param name="reliableFunc">An operation that may be retried due to transient failures.</param>
        public static implicit operator Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> reliableFunc)
            => reliableFunc.Invoke;
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>

    /// <summary>
    /// A wrapper to reliably invoke a function despite transient issues.
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
    /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
    public sealed class ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> : ReliableDelegate<TResult>
    {
        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> _func;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
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
        /// <param name="result">
        /// When this method returns, contains the result of the underlying delegate,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in result will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed successfully
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, out TResult result)
            => TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, CancellationToken.None, out result);

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
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
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, CancellationToken cancellationToken, out TResult result)
        {
            int attempt = 0;
            bool retry = false;
            do
            {
                attempt++;
                try
                {
                    result = _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
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

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
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
        /// <returns>The return value of the underlying delegate.</returns>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
            => Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, CancellationToken.None);

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
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
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, CancellationToken cancellationToken)
        {
            TResult result;
            int attempt = 0;
        Attempt:
            attempt++;
            try
            {
                result = _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            }
            catch (Exception exception)
            {
                if (CanRetry(attempt, exception, cancellationToken))
                    goto Attempt;

                throw exception;
            }

            ResultKind kind = _validate(result);
            if (kind == ResultKind.Successful || !CanRetry(attempt, result, kind, cancellationToken))
                return result;

            goto Attempt;
        }

        /// <summary>
        /// Implicitly converts the <paramref name="reliableFunc"/> to an
        /// <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult}"/>.
        /// </summary>
        /// <remarks>
        /// The resulting function is equivalent to <see cref="Invoke(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)"/>.
        /// </remarks>
        /// <param name="reliableFunc">An operation that may be retried due to transient failures.</param>
        public static implicit operator Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> reliableFunc)
            => reliableFunc.Invoke;
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>

    /// <summary>
    /// A wrapper to reliably invoke a function despite transient issues.
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
    /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
    public sealed class ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> : ReliableDelegate<TResult>
    {
        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> _func;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
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
        /// <param name="result">
        /// When this method returns, contains the result of the underlying delegate,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in result will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed successfully
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, out TResult result)
            => TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, CancellationToken.None, out result);

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
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
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, CancellationToken cancellationToken, out TResult result)
        {
            int attempt = 0;
            bool retry = false;
            do
            {
                attempt++;
                try
                {
                    result = _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
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

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
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
        /// <returns>The return value of the underlying delegate.</returns>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
            => Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, CancellationToken.None);

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
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
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, CancellationToken cancellationToken)
        {
            TResult result;
            int attempt = 0;
        Attempt:
            attempt++;
            try
            {
                result = _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
            }
            catch (Exception exception)
            {
                if (CanRetry(attempt, exception, cancellationToken))
                    goto Attempt;

                throw exception;
            }

            ResultKind kind = _validate(result);
            if (kind == ResultKind.Successful || !CanRetry(attempt, result, kind, cancellationToken))
                return result;

            goto Attempt;
        }

        /// <summary>
        /// Implicitly converts the <paramref name="reliableFunc"/> to an
        /// <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult}"/>.
        /// </summary>
        /// <remarks>
        /// The resulting function is equivalent to <see cref="Invoke(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)"/>.
        /// </remarks>
        /// <param name="reliableFunc">An operation that may be retried due to transient failures.</param>
        public static implicit operator Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> reliableFunc)
            => reliableFunc.Invoke;
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>

    /// <summary>
    /// A wrapper to reliably invoke a function despite transient issues.
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
    /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
    public sealed class ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> : ReliableDelegate<TResult>
    {
        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> _func;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
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
        /// <param name="result">
        /// When this method returns, contains the result of the underlying delegate,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in result will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed successfully
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, out TResult result)
            => TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, CancellationToken.None, out result);

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
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
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, CancellationToken cancellationToken, out TResult result)
        {
            int attempt = 0;
            bool retry = false;
            do
            {
                attempt++;
                try
                {
                    result = _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
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

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
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
        /// <returns>The return value of the underlying delegate.</returns>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
            => Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, CancellationToken.None);

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
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
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, CancellationToken cancellationToken)
        {
            TResult result;
            int attempt = 0;
        Attempt:
            attempt++;
            try
            {
                result = _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
            }
            catch (Exception exception)
            {
                if (CanRetry(attempt, exception, cancellationToken))
                    goto Attempt;

                throw exception;
            }

            ResultKind kind = _validate(result);
            if (kind == ResultKind.Successful || !CanRetry(attempt, result, kind, cancellationToken))
                return result;

            goto Attempt;
        }

        /// <summary>
        /// Implicitly converts the <paramref name="reliableFunc"/> to an
        /// <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult}"/>.
        /// </summary>
        /// <remarks>
        /// The resulting function is equivalent to <see cref="Invoke(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)"/>.
        /// </remarks>
        /// <param name="reliableFunc">An operation that may be retried due to transient failures.</param>
        public static implicit operator Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> reliableFunc)
            => reliableFunc.Invoke;
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>

    /// <summary>
    /// A wrapper to reliably invoke a function despite transient issues.
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
    /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
    public sealed class ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> : ReliableDelegate<TResult>
    {
        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> _func;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
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
        /// <param name="result">
        /// When this method returns, contains the result of the underlying delegate,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in result will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed successfully
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, out TResult result)
            => TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, CancellationToken.None, out result);

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
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
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, CancellationToken cancellationToken, out TResult result)
        {
            int attempt = 0;
            bool retry = false;
            do
            {
                attempt++;
                try
                {
                    result = _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
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

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
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
        /// <returns>The return value of the underlying delegate.</returns>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
            => Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, CancellationToken.None);

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
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
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, CancellationToken cancellationToken)
        {
            TResult result;
            int attempt = 0;
        Attempt:
            attempt++;
            try
            {
                result = _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
            }
            catch (Exception exception)
            {
                if (CanRetry(attempt, exception, cancellationToken))
                    goto Attempt;

                throw exception;
            }

            ResultKind kind = _validate(result);
            if (kind == ResultKind.Successful || !CanRetry(attempt, result, kind, cancellationToken))
                return result;

            goto Attempt;
        }

        /// <summary>
        /// Implicitly converts the <paramref name="reliableFunc"/> to an
        /// <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult}"/>.
        /// </summary>
        /// <remarks>
        /// The resulting function is equivalent to <see cref="Invoke(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)"/>.
        /// </remarks>
        /// <param name="reliableFunc">An operation that may be retried due to transient failures.</param>
        public static implicit operator Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> reliableFunc)
            => reliableFunc.Invoke;
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>

    /// <summary>
    /// A wrapper to reliably invoke a function despite transient issues.
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
    /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
    public sealed class ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> : ReliableDelegate<TResult>
    {
        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> _func;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
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
        /// <param name="result">
        /// When this method returns, contains the result of the underlying delegate,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in result will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed successfully
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, out TResult result)
            => TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, CancellationToken.None, out result);

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
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
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, CancellationToken cancellationToken, out TResult result)
        {
            int attempt = 0;
            bool retry = false;
            do
            {
                attempt++;
                try
                {
                    result = _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
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

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
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
        /// <returns>The return value of the underlying delegate.</returns>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
            => Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, CancellationToken.None);

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
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
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, CancellationToken cancellationToken)
        {
            TResult result;
            int attempt = 0;
        Attempt:
            attempt++;
            try
            {
                result = _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
            }
            catch (Exception exception)
            {
                if (CanRetry(attempt, exception, cancellationToken))
                    goto Attempt;

                throw exception;
            }

            ResultKind kind = _validate(result);
            if (kind == ResultKind.Successful || !CanRetry(attempt, result, kind, cancellationToken))
                return result;

            goto Attempt;
        }

        /// <summary>
        /// Implicitly converts the <paramref name="reliableFunc"/> to an
        /// <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult}"/>.
        /// </summary>
        /// <remarks>
        /// The resulting function is equivalent to <see cref="Invoke(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)"/>.
        /// </remarks>
        /// <param name="reliableFunc">An operation that may be retried due to transient failures.</param>
        public static implicit operator Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> reliableFunc)
            => reliableFunc.Invoke;
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>

    /// <summary>
    /// A wrapper to reliably invoke a function despite transient issues.
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
    /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
    public sealed class ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> : ReliableDelegate<TResult>
    {
        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> _func;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
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
        /// <param name="result">
        /// When this method returns, contains the result of the underlying delegate,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in result will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed successfully
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, out TResult result)
            => TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, CancellationToken.None, out result);

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
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
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, CancellationToken cancellationToken, out TResult result)
        {
            int attempt = 0;
            bool retry = false;
            do
            {
                attempt++;
                try
                {
                    result = _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
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

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
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
        /// <returns>The return value of the underlying delegate.</returns>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
            => Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, CancellationToken.None);

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
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
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, CancellationToken cancellationToken)
        {
            TResult result;
            int attempt = 0;
        Attempt:
            attempt++;
            try
            {
                result = _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
            }
            catch (Exception exception)
            {
                if (CanRetry(attempt, exception, cancellationToken))
                    goto Attempt;

                throw exception;
            }

            ResultKind kind = _validate(result);
            if (kind == ResultKind.Successful || !CanRetry(attempt, result, kind, cancellationToken))
                return result;

            goto Attempt;
        }

        /// <summary>
        /// Implicitly converts the <paramref name="reliableFunc"/> to an
        /// <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult}"/>.
        /// </summary>
        /// <remarks>
        /// The resulting function is equivalent to <see cref="Invoke(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)"/>.
        /// </remarks>
        /// <param name="reliableFunc">An operation that may be retried due to transient failures.</param>
        public static implicit operator Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> reliableFunc)
            => reliableFunc.Invoke;
    }

    #endregion

    #region ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>

    /// <summary>
    /// A wrapper to reliably invoke a function despite transient issues.
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
    /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
    public sealed class ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> : ReliableDelegate<TResult>
    {
        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> _func;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult}"/>
        /// class that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult}"/> at most a
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
        public ReliableFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func, int maxRetries, ResultPolicy<TResult> resultPolicy, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            : base(maxRetries, resultPolicy, exceptionPolicy, delayPolicy)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
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
        /// <param name="result">
        /// When this method returns, contains the result of the underlying delegate,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in result will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed successfully
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, out TResult result)
            => TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, CancellationToken.None, out result);

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient errors.
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
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, CancellationToken cancellationToken, out TResult result)
        {
            int attempt = 0;
            bool retry = false;
            do
            {
                attempt++;
                try
                {
                    result = _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
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

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
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
        /// <returns>The return value of the underlying delegate.</returns>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
            => Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, CancellationToken.None);

        /// <summary>
        /// Invokes the underlying delegate and automatically if it encounters transient errors.
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
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <returns>The return value of the underlying delegate.</returns>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, CancellationToken cancellationToken)
        {
            TResult result;
            int attempt = 0;
        Attempt:
            attempt++;
            try
            {
                result = _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
            }
            catch (Exception exception)
            {
                if (CanRetry(attempt, exception, cancellationToken))
                    goto Attempt;

                throw exception;
            }

            ResultKind kind = _validate(result);
            if (kind == ResultKind.Successful || !CanRetry(attempt, result, kind, cancellationToken))
                return result;

            goto Attempt;
        }

        /// <summary>
        /// Implicitly converts the <paramref name="reliableFunc"/> to an
        /// <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult}"/>.
        /// </summary>
        /// <remarks>
        /// The resulting function is equivalent to <see cref="Invoke(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)"/>.
        /// </remarks>
        /// <param name="reliableFunc">An operation that may be retried due to transient failures.</param>
        public static implicit operator Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> reliableFunc)
            => reliableFunc.Invoke;
    }

    #endregion

}
