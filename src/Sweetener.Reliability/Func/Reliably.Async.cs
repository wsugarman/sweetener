// Generated from Reliably.Async.tt
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Sweetener.Reliability
{
    static partial class Reliably
    {
        #region InvokeAsync

        /// <summary>
        /// Asynchronously invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter contains the return value of the <paramref name="func" />
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> func, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => InvokeAsync(func, CancellationToken.None, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler.ToComplex<TResult>());

        /// <summary>
        /// Asynchronously invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter contains the return value of the <paramref name="func" />
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> func, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            => InvokeAsync(func, CancellationToken.None, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler);

        /// <summary>
        /// Asynchronously invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter contains the return value of the <paramref name="func" />
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> func, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => InvokeAsync(func, CancellationToken.None, maxRetries, resultHandler, exceptionHandler, delayHandler.ToComplex<TResult>());

        /// <summary>
        /// Asynchronously invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter contains the return value of the <paramref name="func" />
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> func, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            => InvokeAsync(func, CancellationToken.None, maxRetries, resultHandler, exceptionHandler, delayHandler);

        /// <summary>
        /// Asynchronously invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter contains the return value of the <paramref name="func" />
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> func, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => InvokeAsync(func, cancellationToken, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler.ToComplex<TResult>());

        /// <summary>
        /// Asynchronously invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter contains the return value of the <paramref name="func" />
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> func, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            => InvokeAsync(func, cancellationToken, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler);

        /// <summary>
        /// Asynchronously invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter contains the return value of the <paramref name="func" />
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> func, CancellationToken cancellationToken, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => InvokeAsync(func, cancellationToken, maxRetries, resultHandler, exceptionHandler, delayHandler.ToComplex<TResult>());

        /// <summary>
        /// Asynchronously invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter contains the return value of the <paramref name="func" />
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static async Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> func, CancellationToken cancellationToken, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            if (maxRetries < Retries.Infinite)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            if (resultHandler == null)
                throw new ArgumentNullException(nameof(resultHandler));

            if (exceptionHandler == null)
                throw new ArgumentNullException(nameof(exceptionHandler));

            if (delayHandler == null)
                throw new ArgumentNullException(nameof(delayHandler));

            // Check for cancellation before invoking
            cancellationToken.ThrowIfCancellationRequested();

            Task<TResult>? t;
            int attempt = 0;

        Attempt:
            t = null;
            attempt++;

            try
            {
                t = func();
                if (t == null)
                    goto Invalid;

                await t.ConfigureAwait(false);
            }
            catch (Exception e)
            {
                bool isCanceled = t != null ? t.IsCanceled : e is OperationCanceledException;
                if (isCanceled || !exceptionHandler(e) || (maxRetries != Retries.Infinite && attempt > maxRetries))
                    throw;

                await Task.Delay(delayHandler(attempt, default, e), cancellationToken).ConfigureAwait(false);
                goto Attempt;
            }

            TResult result = t.Result;
            ResultKind kind = resultHandler(result);
            if (kind != ResultKind.Transient || (maxRetries != Retries.Infinite && attempt > maxRetries))
                return result;

            await Task.Delay(delayHandler(attempt, result, default), cancellationToken).ConfigureAwait(false);
            goto Attempt;

        Invalid:
            throw new InvalidOperationException(SR.InvalidTaskResult);
        }

        /// <summary>
        /// Asynchronously invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="func" /> delegate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter contains the return value of the <paramref name="func" />
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static Task<TResult> InvokeAsync<TState, TResult>(Func<TState, Task<TResult>> func, TState state, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => InvokeAsync(func, state, CancellationToken.None, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler.ToComplex<TResult>());

        /// <summary>
        /// Asynchronously invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="func" /> delegate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter contains the return value of the <paramref name="func" />
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static Task<TResult> InvokeAsync<TState, TResult>(Func<TState, Task<TResult>> func, TState state, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            => InvokeAsync(func, state, CancellationToken.None, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler);

        /// <summary>
        /// Asynchronously invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="func" /> delegate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter contains the return value of the <paramref name="func" />
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static Task<TResult> InvokeAsync<TState, TResult>(Func<TState, Task<TResult>> func, TState state, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => InvokeAsync(func, state, CancellationToken.None, maxRetries, resultHandler, exceptionHandler, delayHandler.ToComplex<TResult>());

        /// <summary>
        /// Asynchronously invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="func" /> delegate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter contains the return value of the <paramref name="func" />
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static Task<TResult> InvokeAsync<TState, TResult>(Func<TState, Task<TResult>> func, TState state, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            => InvokeAsync(func, state, CancellationToken.None, maxRetries, resultHandler, exceptionHandler, delayHandler);

        /// <summary>
        /// Asynchronously invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="func" /> delegate.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter contains the return value of the <paramref name="func" />
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static Task<TResult> InvokeAsync<TState, TResult>(Func<TState, Task<TResult>> func, TState state, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => InvokeAsync(func, state, cancellationToken, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler.ToComplex<TResult>());

        /// <summary>
        /// Asynchronously invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="func" /> delegate.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter contains the return value of the <paramref name="func" />
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static Task<TResult> InvokeAsync<TState, TResult>(Func<TState, Task<TResult>> func, TState state, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            => InvokeAsync(func, state, cancellationToken, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler);

        /// <summary>
        /// Asynchronously invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="func" /> delegate.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter contains the return value of the <paramref name="func" />
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static Task<TResult> InvokeAsync<TState, TResult>(Func<TState, Task<TResult>> func, TState state, CancellationToken cancellationToken, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => InvokeAsync(func, state, cancellationToken, maxRetries, resultHandler, exceptionHandler, delayHandler.ToComplex<TResult>());

        /// <summary>
        /// Asynchronously invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="func" /> delegate.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter contains the return value of the <paramref name="func" />
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static async Task<TResult> InvokeAsync<TState, TResult>(Func<TState, Task<TResult>> func, TState state, CancellationToken cancellationToken, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            if (maxRetries < Retries.Infinite)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            if (resultHandler == null)
                throw new ArgumentNullException(nameof(resultHandler));

            if (exceptionHandler == null)
                throw new ArgumentNullException(nameof(exceptionHandler));

            if (delayHandler == null)
                throw new ArgumentNullException(nameof(delayHandler));

            // Check for cancellation before invoking
            cancellationToken.ThrowIfCancellationRequested();

            Task<TResult>? t;
            int attempt = 0;

        Attempt:
            t = null;
            attempt++;

            try
            {
                t = func(state);
                if (t == null)
                    goto Invalid;

                await t.ConfigureAwait(false);
            }
            catch (Exception e)
            {
                bool isCanceled = t != null ? t.IsCanceled : e is OperationCanceledException;
                if (isCanceled || !exceptionHandler(e) || (maxRetries != Retries.Infinite && attempt > maxRetries))
                    throw;

                await Task.Delay(delayHandler(attempt, default, e), cancellationToken).ConfigureAwait(false);
                goto Attempt;
            }

            TResult result = t.Result;
            ResultKind kind = resultHandler(result);
            if (kind != ResultKind.Transient || (maxRetries != Retries.Infinite && attempt > maxRetries))
                return result;

            await Task.Delay(delayHandler(attempt, result, default), cancellationToken).ConfigureAwait(false);
            goto Attempt;

        Invalid:
            throw new InvalidOperationException(SR.InvalidTaskResult);
        }

        #endregion

        #region TryInvokeAsync

        /// <summary>
        /// Asynchronously attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter optionally contains the return value of the <paramref name="func" />.
        /// if the delegate was invoked successfully.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static Task<Optional<TResult>> TryInvokeAsync<TResult>(Func<Task<TResult>> func, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => TryInvokeAsync(func, CancellationToken.None, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler.ToComplex<TResult>());

        /// <summary>
        /// Asynchronously attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter optionally contains the return value of the <paramref name="func" />.
        /// if the delegate was invoked successfully.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static Task<Optional<TResult>> TryInvokeAsync<TResult>(Func<Task<TResult>> func, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            => TryInvokeAsync(func, CancellationToken.None, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler);

        /// <summary>
        /// Asynchronously attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter optionally contains the return value of the <paramref name="func" />.
        /// if the delegate was invoked successfully.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static Task<Optional<TResult>> TryInvokeAsync<TResult>(Func<Task<TResult>> func, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => TryInvokeAsync(func, CancellationToken.None, maxRetries, resultHandler, exceptionHandler, delayHandler.ToComplex<TResult>());

        /// <summary>
        /// Asynchronously attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter optionally contains the return value of the <paramref name="func" />.
        /// if the delegate was invoked successfully.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static Task<Optional<TResult>> TryInvokeAsync<TResult>(Func<Task<TResult>> func, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            => TryInvokeAsync(func, CancellationToken.None, maxRetries, resultHandler, exceptionHandler, delayHandler);

        /// <summary>
        /// Asynchronously attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter optionally contains the return value of the <paramref name="func" />.
        /// if the delegate was invoked successfully.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static Task<Optional<TResult>> TryInvokeAsync<TResult>(Func<Task<TResult>> func, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => TryInvokeAsync(func, cancellationToken, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler.ToComplex<TResult>());

        /// <summary>
        /// Asynchronously attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter optionally contains the return value of the <paramref name="func" />.
        /// if the delegate was invoked successfully.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static Task<Optional<TResult>> TryInvokeAsync<TResult>(Func<Task<TResult>> func, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            => TryInvokeAsync(func, cancellationToken, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler);

        /// <summary>
        /// Asynchronously attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter optionally contains the return value of the <paramref name="func" />.
        /// if the delegate was invoked successfully.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static Task<Optional<TResult>> TryInvokeAsync<TResult>(Func<Task<TResult>> func, CancellationToken cancellationToken, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => TryInvokeAsync(func, cancellationToken, maxRetries, resultHandler, exceptionHandler, delayHandler.ToComplex<TResult>());

        /// <summary>
        /// Asynchronously attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter optionally contains the return value of the <paramref name="func" />.
        /// if the delegate was invoked successfully.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static async Task<Optional<TResult>> TryInvokeAsync<TResult>(Func<Task<TResult>> func, CancellationToken cancellationToken, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            if (maxRetries < Retries.Infinite)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            if (resultHandler == null)
                throw new ArgumentNullException(nameof(resultHandler));

            if (exceptionHandler == null)
                throw new ArgumentNullException(nameof(exceptionHandler));

            if (delayHandler == null)
                throw new ArgumentNullException(nameof(delayHandler));

            // Check for cancellation before invoking
            cancellationToken.ThrowIfCancellationRequested();

            Task<TResult>? t;
            int attempt = 0;

        Attempt:
            t = null;
            attempt++;

            try
            {
                t = func();
                if (t == null)
                    goto Invalid;

                await t.ConfigureAwait(false);
            }
            catch (Exception e)
            {
                bool isCanceled = t != null ? t.IsCanceled : e is OperationCanceledException;
                if (isCanceled)
                    throw;

                if (!exceptionHandler(e) || (maxRetries != Retries.Infinite && attempt > maxRetries))
                    goto Fail;

                await Task.Delay(delayHandler(attempt, default, e), cancellationToken).ConfigureAwait(false);
                goto Attempt;
            }

            TResult result = t.Result;
            switch (resultHandler(result))
            {
                case ResultKind.Successful:
                    return result;
                case ResultKind.Fatal:
                    goto Fail;
                default:
                    if (maxRetries != Retries.Infinite && attempt > maxRetries)
                        goto Fail;

                    Task.Delay(delayHandler(attempt, result, default), cancellationToken).Wait();
                    goto Attempt;
            }

        Fail:
            return Optional<TResult>.Undefined;

        Invalid:
            throw new InvalidOperationException(SR.InvalidTaskResult);
        }

        /// <summary>
        /// Asynchronously attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="func" /> delegate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter optionally contains the return value of the <paramref name="func" />.
        /// if the delegate was invoked successfully.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static Task<Optional<TResult>> TryInvokeAsync<TState, TResult>(Func<TState, Task<TResult>> func, TState state, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => TryInvokeAsync(func, state, CancellationToken.None, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler.ToComplex<TResult>());

        /// <summary>
        /// Asynchronously attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="func" /> delegate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter optionally contains the return value of the <paramref name="func" />.
        /// if the delegate was invoked successfully.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static Task<Optional<TResult>> TryInvokeAsync<TState, TResult>(Func<TState, Task<TResult>> func, TState state, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            => TryInvokeAsync(func, state, CancellationToken.None, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler);

        /// <summary>
        /// Asynchronously attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="func" /> delegate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter optionally contains the return value of the <paramref name="func" />.
        /// if the delegate was invoked successfully.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static Task<Optional<TResult>> TryInvokeAsync<TState, TResult>(Func<TState, Task<TResult>> func, TState state, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => TryInvokeAsync(func, state, CancellationToken.None, maxRetries, resultHandler, exceptionHandler, delayHandler.ToComplex<TResult>());

        /// <summary>
        /// Asynchronously attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="func" /> delegate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter optionally contains the return value of the <paramref name="func" />.
        /// if the delegate was invoked successfully.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static Task<Optional<TResult>> TryInvokeAsync<TState, TResult>(Func<TState, Task<TResult>> func, TState state, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            => TryInvokeAsync(func, state, CancellationToken.None, maxRetries, resultHandler, exceptionHandler, delayHandler);

        /// <summary>
        /// Asynchronously attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="func" /> delegate.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter optionally contains the return value of the <paramref name="func" />.
        /// if the delegate was invoked successfully.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static Task<Optional<TResult>> TryInvokeAsync<TState, TResult>(Func<TState, Task<TResult>> func, TState state, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => TryInvokeAsync(func, state, cancellationToken, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler.ToComplex<TResult>());

        /// <summary>
        /// Asynchronously attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="func" /> delegate.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter optionally contains the return value of the <paramref name="func" />.
        /// if the delegate was invoked successfully.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static Task<Optional<TResult>> TryInvokeAsync<TState, TResult>(Func<TState, Task<TResult>> func, TState state, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            => TryInvokeAsync(func, state, cancellationToken, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler);

        /// <summary>
        /// Asynchronously attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="func" /> delegate.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter optionally contains the return value of the <paramref name="func" />.
        /// if the delegate was invoked successfully.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static Task<Optional<TResult>> TryInvokeAsync<TState, TResult>(Func<TState, Task<TResult>> func, TState state, CancellationToken cancellationToken, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => TryInvokeAsync(func, state, cancellationToken, maxRetries, resultHandler, exceptionHandler, delayHandler.ToComplex<TResult>());

        /// <summary>
        /// Asynchronously attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="func" /> delegate.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter optionally contains the return value of the <paramref name="func" />.
        /// if the delegate was invoked successfully.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static async Task<Optional<TResult>> TryInvokeAsync<TState, TResult>(Func<TState, Task<TResult>> func, TState state, CancellationToken cancellationToken, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            if (maxRetries < Retries.Infinite)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            if (resultHandler == null)
                throw new ArgumentNullException(nameof(resultHandler));

            if (exceptionHandler == null)
                throw new ArgumentNullException(nameof(exceptionHandler));

            if (delayHandler == null)
                throw new ArgumentNullException(nameof(delayHandler));

            // Check for cancellation before invoking
            cancellationToken.ThrowIfCancellationRequested();

            Task<TResult>? t;
            int attempt = 0;

        Attempt:
            t = null;
            attempt++;

            try
            {
                t = func(state);
                if (t == null)
                    goto Invalid;

                await t.ConfigureAwait(false);
            }
            catch (Exception e)
            {
                bool isCanceled = t != null ? t.IsCanceled : e is OperationCanceledException;
                if (isCanceled)
                    throw;

                if (!exceptionHandler(e) || (maxRetries != Retries.Infinite && attempt > maxRetries))
                    goto Fail;

                await Task.Delay(delayHandler(attempt, default, e), cancellationToken).ConfigureAwait(false);
                goto Attempt;
            }

            TResult result = t.Result;
            switch (resultHandler(result))
            {
                case ResultKind.Successful:
                    return result;
                case ResultKind.Fatal:
                    goto Fail;
                default:
                    if (maxRetries != Retries.Infinite && attempt > maxRetries)
                        goto Fail;

                    Task.Delay(delayHandler(attempt, result, default), cancellationToken).Wait();
                    goto Attempt;
            }

        Fail:
            return Optional<TResult>.Undefined;

        Invalid:
            throw new InvalidOperationException(SR.InvalidTaskResult);
        }

        #endregion
    }
}

