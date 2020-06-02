// Generated from Reliably.tt
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Sweetener.Reliability
{
    static partial class Reliably
    {
        #region Invoke

        /// <summary>
        /// Invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>The return value of the <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static TResult Invoke<TResult>(Func<TResult> func, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => Invoke(func, CancellationToken.None, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler.ToComplex<TResult>());

        /// <summary>
        /// Invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>The return value of the <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static TResult Invoke<TResult>(Func<TResult> func, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            => Invoke(func, CancellationToken.None, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler);

        /// <summary>
        /// Invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>The return value of the <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static TResult Invoke<TResult>(Func<TResult> func, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => Invoke(func, CancellationToken.None, maxRetries, resultHandler, exceptionHandler, delayHandler.ToComplex<TResult>());

        /// <summary>
        /// Invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>The return value of the <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static TResult Invoke<TResult>(Func<TResult> func, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            => Invoke(func, CancellationToken.None, maxRetries, resultHandler, exceptionHandler, delayHandler);

        /// <summary>
        /// Invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>The return value of the <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static TResult Invoke<TResult>(Func<TResult> func, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => Invoke(func, cancellationToken, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler.ToComplex<TResult>());

        /// <summary>
        /// Invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>The return value of the <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static TResult Invoke<TResult>(Func<TResult> func, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            => Invoke(func, cancellationToken, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler);

        /// <summary>
        /// Invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>The return value of the <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static TResult Invoke<TResult>(Func<TResult> func, CancellationToken cancellationToken, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => Invoke(func, cancellationToken, maxRetries, resultHandler, exceptionHandler, delayHandler.ToComplex<TResult>());

        /// <summary>
        /// Invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>The return value of the <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static TResult Invoke<TResult>(Func<TResult> func, CancellationToken cancellationToken, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
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

            int attempt = 0;

        Attempt:
            TResult result;
            attempt++;

            try
            {
                result = func();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                if (!exceptionHandler(e) || (maxRetries != Retries.Infinite && attempt > maxRetries))
                    throw;

                Task.Delay(delayHandler(attempt, default, e), cancellationToken).Wait();
                goto Attempt;
            }

            ResultKind kind = resultHandler(result);
            if (kind != ResultKind.Transient || (maxRetries != Retries.Infinite && attempt > maxRetries))
                return result;

            Task.Delay(delayHandler(attempt, result, default), cancellationToken).Wait();
            goto Attempt;
        }

        /// <summary>
        /// Invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="func" /> delegate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>The return value of the <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static TResult Invoke<TState, TResult>(Func<TState, TResult> func, TState state, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => Invoke(func, state, CancellationToken.None, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler.ToComplex<TResult>());

        /// <summary>
        /// Invokes the given <paramref name="func" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="func" /> delegate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>The return value of the <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static TResult Invoke<TState, TResult>(Func<TState, TResult> func, TState state, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            => Invoke(func, state, CancellationToken.None, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler);

        /// <summary>
        /// Invokes the given <paramref name="func" /> despite transient problems
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
        /// <returns>The return value of the <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static TResult Invoke<TState, TResult>(Func<TState, TResult> func, TState state, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => Invoke(func, state, CancellationToken.None, maxRetries, resultHandler, exceptionHandler, delayHandler.ToComplex<TResult>());

        /// <summary>
        /// Invokes the given <paramref name="func" /> despite transient problems
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
        /// <returns>The return value of the <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static TResult Invoke<TState, TResult>(Func<TState, TResult> func, TState state, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            => Invoke(func, state, CancellationToken.None, maxRetries, resultHandler, exceptionHandler, delayHandler);

        /// <summary>
        /// Invokes the given <paramref name="func" /> despite transient problems
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
        /// <returns>The return value of the <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static TResult Invoke<TState, TResult>(Func<TState, TResult> func, TState state, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => Invoke(func, state, cancellationToken, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler.ToComplex<TResult>());

        /// <summary>
        /// Invokes the given <paramref name="func" /> despite transient problems
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
        /// <returns>The return value of the <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static TResult Invoke<TState, TResult>(Func<TState, TResult> func, TState state, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
            => Invoke(func, state, cancellationToken, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler);

        /// <summary>
        /// Invokes the given <paramref name="func" /> despite transient problems
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
        /// <returns>The return value of the <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static TResult Invoke<TState, TResult>(Func<TState, TResult> func, TState state, CancellationToken cancellationToken, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => Invoke(func, state, cancellationToken, maxRetries, resultHandler, exceptionHandler, delayHandler.ToComplex<TResult>());

        /// <summary>
        /// Invokes the given <paramref name="func" /> despite transient problems
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
        /// <returns>The return value of the <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static TResult Invoke<TState, TResult>(Func<TState, TResult> func, TState state, CancellationToken cancellationToken, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
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

            int attempt = 0;

        Attempt:
            TResult result;
            attempt++;

            try
            {
                result = func(state);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                if (!exceptionHandler(e) || (maxRetries != Retries.Infinite && attempt > maxRetries))
                    throw;

                Task.Delay(delayHandler(attempt, default, e), cancellationToken).Wait();
                goto Attempt;
            }

            ResultKind kind = resultHandler(result);
            if (kind != ResultKind.Transient || (maxRetries != Retries.Infinite && attempt > maxRetries))
                return result;

            Task.Delay(delayHandler(attempt, result, default), cancellationToken).Wait();
            goto Attempt;
        }

        #endregion

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
        public static Task<TResult> InvokeAsync<TResult>(Func<TResult> func, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
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
        public static Task<TResult> InvokeAsync<TResult>(Func<TResult> func, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
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
        public static Task<TResult> InvokeAsync<TResult>(Func<TResult> func, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
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
        public static Task<TResult> InvokeAsync<TResult>(Func<TResult> func, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
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
        public static Task<TResult> InvokeAsync<TResult>(Func<TResult> func, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
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
        public static Task<TResult> InvokeAsync<TResult>(Func<TResult> func, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
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
        public static Task<TResult> InvokeAsync<TResult>(Func<TResult> func, CancellationToken cancellationToken, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
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
        public static async Task<TResult> InvokeAsync<TResult>(Func<TResult> func, CancellationToken cancellationToken, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
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

            
            int attempt = 0;

        Attempt:
            TResult result;
            attempt++;

            try
            {
                result = func();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                if (!exceptionHandler(e) || (maxRetries != Retries.Infinite && attempt > maxRetries))
                    throw;

                await Task.Delay(delayHandler(attempt, default, e), cancellationToken).ConfigureAwait(false);
                goto Attempt;
            }

            ResultKind kind = resultHandler(result);
            if (kind != ResultKind.Transient || (maxRetries != Retries.Infinite && attempt > maxRetries))
                return result;

            await Task.Delay(delayHandler(attempt, result, default), cancellationToken).ConfigureAwait(false);
            goto Attempt;
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
        public static Task<TResult> InvokeAsync<TState, TResult>(Func<TState, TResult> func, TState state, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
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
        public static Task<TResult> InvokeAsync<TState, TResult>(Func<TState, TResult> func, TState state, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
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
        public static Task<TResult> InvokeAsync<TState, TResult>(Func<TState, TResult> func, TState state, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
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
        public static Task<TResult> InvokeAsync<TState, TResult>(Func<TState, TResult> func, TState state, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
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
        public static Task<TResult> InvokeAsync<TState, TResult>(Func<TState, TResult> func, TState state, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
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
        public static Task<TResult> InvokeAsync<TState, TResult>(Func<TState, TResult> func, TState state, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
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
        public static Task<TResult> InvokeAsync<TState, TResult>(Func<TState, TResult> func, TState state, CancellationToken cancellationToken, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
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
        public static async Task<TResult> InvokeAsync<TState, TResult>(Func<TState, TResult> func, TState state, CancellationToken cancellationToken, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
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

            
            int attempt = 0;

        Attempt:
            TResult result;
            attempt++;

            try
            {
                result = func(state);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                if (!exceptionHandler(e) || (maxRetries != Retries.Infinite && attempt > maxRetries))
                    throw;

                await Task.Delay(delayHandler(attempt, default, e), cancellationToken).ConfigureAwait(false);
                goto Attempt;
            }

            ResultKind kind = resultHandler(result);
            if (kind != ResultKind.Transient || (maxRetries != Retries.Infinite && attempt > maxRetries))
                return result;

            await Task.Delay(delayHandler(attempt, result, default), cancellationToken).ConfigureAwait(false);
            goto Attempt;
        }

        #endregion

        #region TryInvoke

        /// <summary>
        /// Attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <param name="result">
        /// When this method returns, contains the result of the <paramref name="func" />,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="func" /> was invoked sucessfully;
        /// otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static bool TryInvoke<TResult>(Func<TResult> func, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler, out TResult result)
            => TryInvoke(func, CancellationToken.None, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler.ToComplex<TResult>(), out result);

        /// <summary>
        /// Attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <param name="result">
        /// When this method returns, contains the result of the <paramref name="func" />,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="func" /> was invoked sucessfully;
        /// otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static bool TryInvoke<TResult>(Func<TResult> func, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler, out TResult result)
            => TryInvoke(func, CancellationToken.None, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler, out result);

        /// <summary>
        /// Attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <param name="result">
        /// When this method returns, contains the result of the <paramref name="func" />,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="func" /> was invoked sucessfully;
        /// otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static bool TryInvoke<TResult>(Func<TResult> func, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler, out TResult result)
            => TryInvoke(func, CancellationToken.None, maxRetries, resultHandler, exceptionHandler, delayHandler.ToComplex<TResult>(), out result);

        /// <summary>
        /// Attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <param name="result">
        /// When this method returns, contains the result of the <paramref name="func" />,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="func" /> was invoked sucessfully;
        /// otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static bool TryInvoke<TResult>(Func<TResult> func, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler, out TResult result)
            => TryInvoke(func, CancellationToken.None, maxRetries, resultHandler, exceptionHandler, delayHandler, out result);

        /// <summary>
        /// Attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <param name="result">
        /// When this method returns, contains the result of the <paramref name="func" />,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="func" /> was invoked sucessfully;
        /// otherwise, <see langword="false" />.
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
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static bool TryInvoke<TResult>(Func<TResult> func, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler, out TResult result)
            => TryInvoke(func, cancellationToken, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler.ToComplex<TResult>(), out result);

        /// <summary>
        /// Attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <param name="result">
        /// When this method returns, contains the result of the <paramref name="func" />,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="func" /> was invoked sucessfully;
        /// otherwise, <see langword="false" />.
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
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static bool TryInvoke<TResult>(Func<TResult> func, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler, out TResult result)
            => TryInvoke(func, cancellationToken, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler, out result);

        /// <summary>
        /// Attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <param name="result">
        /// When this method returns, contains the result of the <paramref name="func" />,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="func" /> was invoked sucessfully;
        /// otherwise, <see langword="false" />.
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
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static bool TryInvoke<TResult>(Func<TResult> func, CancellationToken cancellationToken, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler, out TResult result)
            => TryInvoke(func, cancellationToken, maxRetries, resultHandler, exceptionHandler, delayHandler.ToComplex<TResult>(), out result);

        /// <summary>
        /// Attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <param name="result">
        /// When this method returns, contains the result of the <paramref name="func" />,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="func" /> was invoked sucessfully;
        /// otherwise, <see langword="false" />.
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
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        [SuppressMessage("Design", "CA1031:Do not catch general exception types"       , Justification = "All exceptions must be caught so they can be tested by the exception handler"   )]
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static bool TryInvoke<TResult>(Func<TResult> func, CancellationToken cancellationToken, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler, out TResult result)
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

            int attempt = 0;

        Attempt:
            attempt++;

            try
            {
                result = func();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                if (!exceptionHandler(e) || (maxRetries != Retries.Infinite && attempt > maxRetries))
                    goto Fail;

                Task.Delay(delayHandler(attempt, default, e), cancellationToken).Wait();
                goto Attempt;
            }

            switch (resultHandler(result))
            {
                case ResultKind.Successful:
                    return true;
                case ResultKind.Fatal:
                    goto Fail;
                default:
                    if (maxRetries != Retries.Infinite && attempt > maxRetries)
                        goto Fail;

                    Task.Delay(delayHandler(attempt, result, default), cancellationToken).Wait();
                    goto Attempt;
            }

        Fail:
            result = default!;
            return false;
        }

        /// <summary>
        /// Attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="func" /> delegate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <param name="result">
        /// When this method returns, contains the result of the <paramref name="func" />,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="func" /> was invoked sucessfully;
        /// otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static bool TryInvoke<TState, TResult>(Func<TState, TResult> func, TState state, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler, out TResult result)
            => TryInvoke(func, state, CancellationToken.None, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler.ToComplex<TResult>(), out result);

        /// <summary>
        /// Attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="func" /> delegate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <param name="result">
        /// When this method returns, contains the result of the <paramref name="func" />,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="func" /> was invoked sucessfully;
        /// otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static bool TryInvoke<TState, TResult>(Func<TState, TResult> func, TState state, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler, out TResult result)
            => TryInvoke(func, state, CancellationToken.None, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler, out result);

        /// <summary>
        /// Attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="func" /> delegate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <param name="result">
        /// When this method returns, contains the result of the <paramref name="func" />,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="func" /> was invoked sucessfully;
        /// otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static bool TryInvoke<TState, TResult>(Func<TState, TResult> func, TState state, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler, out TResult result)
            => TryInvoke(func, state, CancellationToken.None, maxRetries, resultHandler, exceptionHandler, delayHandler.ToComplex<TResult>(), out result);

        /// <summary>
        /// Attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="func" /> delegate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultHandler">A function that determines which results are valid.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <param name="result">
        /// When this method returns, contains the result of the <paramref name="func" />,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="func" /> was invoked sucessfully;
        /// otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultHandler" /> <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static bool TryInvoke<TState, TResult>(Func<TState, TResult> func, TState state, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler, out TResult result)
            => TryInvoke(func, state, CancellationToken.None, maxRetries, resultHandler, exceptionHandler, delayHandler, out result);

        /// <summary>
        /// Attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="func" /> delegate.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <param name="result">
        /// When this method returns, contains the result of the <paramref name="func" />,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="func" /> was invoked sucessfully;
        /// otherwise, <see langword="false" />.
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
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static bool TryInvoke<TState, TResult>(Func<TState, TResult> func, TState state, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler, out TResult result)
            => TryInvoke(func, state, cancellationToken, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler.ToComplex<TResult>(), out result);

        /// <summary>
        /// Attempts to successfully invoke the <paramref name="func" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result produced by the <paramref name="func"/>.</typeparam>
        /// <param name="func">The func to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="func" /> delegate.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="func" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <param name="result">
        /// When this method returns, contains the result of the <paramref name="func" />,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="func" /> was invoked sucessfully;
        /// otherwise, <see langword="false" />.
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
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static bool TryInvoke<TState, TResult>(Func<TState, TResult> func, TState state, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler, out TResult result)
            => TryInvoke(func, state, cancellationToken, maxRetries, ResultPolicy.Default<TResult>(), exceptionHandler, delayHandler, out result);

        /// <summary>
        /// Attempts to successfully invoke the <paramref name="func" /> despite transient problems.
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
        /// <param name="result">
        /// When this method returns, contains the result of the <paramref name="func" />,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="func" /> was invoked sucessfully;
        /// otherwise, <see langword="false" />.
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
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static bool TryInvoke<TState, TResult>(Func<TState, TResult> func, TState state, CancellationToken cancellationToken, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler, out TResult result)
            => TryInvoke(func, state, cancellationToken, maxRetries, resultHandler, exceptionHandler, delayHandler.ToComplex<TResult>(), out result);

        /// <summary>
        /// Attempts to successfully invoke the <paramref name="func" /> despite transient problems.
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
        /// <param name="result">
        /// When this method returns, contains the result of the <paramref name="func" />,
        /// if it completed successfully, or the default value if it failed. The parameter
        /// is passed unitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="func" /> was invoked sucessfully;
        /// otherwise, <see langword="false" />.
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
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        [SuppressMessage("Design", "CA1031:Do not catch general exception types"       , Justification = "All exceptions must be caught so they can be tested by the exception handler"   )]
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static bool TryInvoke<TState, TResult>(Func<TState, TResult> func, TState state, CancellationToken cancellationToken, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler, out TResult result)
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

            int attempt = 0;

        Attempt:
            attempt++;

            try
            {
                result = func(state);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                if (!exceptionHandler(e) || (maxRetries != Retries.Infinite && attempt > maxRetries))
                    goto Fail;

                Task.Delay(delayHandler(attempt, default, e), cancellationToken).Wait();
                goto Attempt;
            }

            switch (resultHandler(result))
            {
                case ResultKind.Successful:
                    return true;
                case ResultKind.Fatal:
                    goto Fail;
                default:
                    if (maxRetries != Retries.Infinite && attempt > maxRetries)
                        goto Fail;

                    Task.Delay(delayHandler(attempt, result, default), cancellationToken).Wait();
                    goto Attempt;
            }

        Fail:
            result = default!;
            return false;
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
        public static Task<Optional<TResult>> TryInvokeAsync<TResult>(Func<TResult> func, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
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
        public static Task<Optional<TResult>> TryInvokeAsync<TResult>(Func<TResult> func, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
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
        public static Task<Optional<TResult>> TryInvokeAsync<TResult>(Func<TResult> func, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
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
        public static Task<Optional<TResult>> TryInvokeAsync<TResult>(Func<TResult> func, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
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
        public static Task<Optional<TResult>> TryInvokeAsync<TResult>(Func<TResult> func, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
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
        public static Task<Optional<TResult>> TryInvokeAsync<TResult>(Func<TResult> func, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
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
        public static Task<Optional<TResult>> TryInvokeAsync<TResult>(Func<TResult> func, CancellationToken cancellationToken, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
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
        [SuppressMessage("Design", "CA1031:Do not catch general exception types"       , Justification = "All exceptions must be caught so they can be tested by the exception handler"   )]
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static async Task<Optional<TResult>> TryInvokeAsync<TResult>(Func<TResult> func, CancellationToken cancellationToken, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
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

            int attempt = 0;

        Attempt:
            TResult result;
            attempt++;

            try
            {
                result = func();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                if (!exceptionHandler(e) || (maxRetries != Retries.Infinite && attempt > maxRetries))
                    goto Fail;

                await Task.Delay(delayHandler(attempt, default, e), cancellationToken).ConfigureAwait(false);
                goto Attempt;
            }

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
        public static Task<Optional<TResult>> TryInvokeAsync<TState, TResult>(Func<TState, TResult> func, TState state, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
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
        public static Task<Optional<TResult>> TryInvokeAsync<TState, TResult>(Func<TState, TResult> func, TState state, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
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
        public static Task<Optional<TResult>> TryInvokeAsync<TState, TResult>(Func<TState, TResult> func, TState state, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
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
        public static Task<Optional<TResult>> TryInvokeAsync<TState, TResult>(Func<TState, TResult> func, TState state, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
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
        public static Task<Optional<TResult>> TryInvokeAsync<TState, TResult>(Func<TState, TResult> func, TState state, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
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
        public static Task<Optional<TResult>> TryInvokeAsync<TState, TResult>(Func<TState, TResult> func, TState state, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
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
        public static Task<Optional<TResult>> TryInvokeAsync<TState, TResult>(Func<TState, TResult> func, TState state, CancellationToken cancellationToken, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
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
        [SuppressMessage("Design", "CA1031:Do not catch general exception types"       , Justification = "All exceptions must be caught so they can be tested by the exception handler"   )]
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static async Task<Optional<TResult>> TryInvokeAsync<TState, TResult>(Func<TState, TResult> func, TState state, CancellationToken cancellationToken, int maxRetries, ResultHandler<TResult> resultHandler, ExceptionHandler exceptionHandler, ComplexDelayHandler<TResult> delayHandler)
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

            int attempt = 0;

        Attempt:
            TResult result;
            attempt++;

            try
            {
                result = func(state);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                if (!exceptionHandler(e) || (maxRetries != Retries.Infinite && attempt > maxRetries))
                    goto Fail;

                await Task.Delay(delayHandler(attempt, default, e), cancellationToken).ConfigureAwait(false);
                goto Attempt;
            }

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
        }

        #endregion
    }
}

