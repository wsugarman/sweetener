// Generated from ReliableAsyncFunc.tt
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sweetener.Reliability
{
    /// <summary>
    /// A wrapper to reliably invoke an asynchronous function despite transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this reliable delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this reliable delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this reliable delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this reliable delegate encapsulates.</typeparam>
    public sealed class ReliableAsyncFunc<T1, T2, T3, TResult> : ReliableDelegate<TResult>
    {
        private readonly Func<T1, T2, T3, CancellationToken, Task<TResult>> _func;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncFunc{T1, T2, T3, TResult}"/>
        /// class that executes the given asynchronous function at most a specific number of times
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
        public ReliableAsyncFunc(Func<T1, T2, T3, Task<TResult>> func, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            : this(func.IgnoreInterruption(), maxRetries, exceptionHandler, delayHandler)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncFunc{T1, T2, T3, TResult}"/>
        /// class that executes the given asynchronous function at most a specific number of times
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
        public ReliableAsyncFunc(Func<T1, T2, T3, Task<TResult>> func, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            : this(func.IgnoreInterruption(), maxRetries, exceptionHandler, delayHandler)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncFunc{T1, T2, T3, TResult}"/>
        /// class that executes the given asynchronous function at most a specific number of times
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
        public ReliableAsyncFunc(Func<T1, T2, T3, Task<TResult>> func, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            : this(func.IgnoreInterruption(), maxRetries, resultHandler, exceptionHandler, delayHandler)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncFunc{T1, T2, T3, TResult}"/>
        /// class that executes the given asynchronous function at most a specific number of times
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
        public ReliableAsyncFunc(Func<T1, T2, T3, Task<TResult>> func, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            : this(func.IgnoreInterruption(), maxRetries, resultHandler, exceptionHandler, delayHandler)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncFunc{T1, T2, T3, TResult}"/>
        /// class that executes the given asynchronous function at most a specific number of times
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
        public ReliableAsyncFunc(Func<T1, T2, T3, CancellationToken, Task<TResult>> func, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            : base(maxRetries, exceptionHandler, delayHandler)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncFunc{T1, T2, T3, TResult}"/>
        /// class that executes the given asynchronous function at most a specific number of times
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
        public ReliableAsyncFunc(Func<T1, T2, T3, CancellationToken, Task<TResult>> func, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            : base(maxRetries, exceptionHandler, delayHandler)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncFunc{T1, T2, T3, TResult}"/>
        /// class that executes the given asynchronous function at most a specific number of times
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
        public ReliableAsyncFunc(Func<T1, T2, T3, CancellationToken, Task<TResult>> func, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            : base(maxRetries, resultHandler, exceptionHandler, delayHandler)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncFunc{T1, T2, T3, TResult}"/>
        /// class that executes the given asynchronous function at most a specific number of times
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
        public ReliableAsyncFunc(Func<T1, T2, T3, CancellationToken, Task<TResult>> func, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            : base(maxRetries, resultHandler, exceptionHandler, delayHandler)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// Asynchronously invokes the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="arg1">The first parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this reliable delegate encapsulates.</param>
        /// <returns>The return value of the method that this reliable delegate encapsulates.</returns>
        /// <exception cref="InvalidOperationException">
        /// The encapsulated method returns <see langword="null"/> instead of a valid <see cref="Task"/>.
        /// </exception>
        public async Task<TResult> InvokeAsync(T1 arg1, T2 arg2, T3 arg3)
            => await InvokeAsync(arg1, arg2, arg3, CancellationToken.None).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="arg1">The first parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
        /// <returns>The return value of the method that this reliable delegate encapsulates.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The encapsulated method returns <see langword="null"/> instead of a valid <see cref="Task"/>.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public async Task<TResult> InvokeAsync(T1 arg1, T2 arg2, T3 arg3, CancellationToken cancellationToken)
        {
            Task<TResult> t;
            int attempt = 0;

        Attempt:
            t = null;
            attempt++;

            try
            {
                t = _func(arg1, arg2, arg3, cancellationToken);
                if (t == null)
                    goto Invalid;

                await t.ConfigureAwait(false);
            }
            catch (Exception e)
            {
                bool isCanceled = t != null ? t.IsCanceled : e.IsCancellation(cancellationToken);
                if (isCanceled || !await CanRetryAsync(attempt, e, cancellationToken).ConfigureAwait(false))
                    throw;

                goto Attempt;
            }

            TResult result = t.Result;
            if (await MoveNextAsync(attempt, result, cancellationToken).ConfigureAwait(false) != FunctionState.Retry)
                return result;

            goto Attempt;

        Invalid:
            throw new InvalidOperationException("Method resulted in an invalid Task.");
        }
    }
}
