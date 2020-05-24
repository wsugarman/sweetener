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
        /// Asynchronously invokes the given <paramref name="action" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <param name="action">The action to reliably invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>A task that represents the asynchronous invoke operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static Task InvokeAsync(Func<Task> action, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => InvokeAsync(action, CancellationToken.None, maxRetries, exceptionHandler, delayHandler.ToComplex());

        /// <summary>
        /// Asynchronously invokes the given <paramref name="action" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <param name="action">The action to reliably invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>A task that represents the asynchronous invoke operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static Task InvokeAsync(Func<Task> action, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler delayHandler)
            => InvokeAsync(action, CancellationToken.None, maxRetries, exceptionHandler, delayHandler);

        /// <summary>
        /// Asynchronously invokes the given <paramref name="action" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <param name="action">The action to reliably invoke.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="action" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>A task that represents the asynchronous invoke operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static Task InvokeAsync(Func<Task> action, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => InvokeAsync(action, cancellationToken, maxRetries, exceptionHandler, delayHandler.ToComplex());

        /// <summary>
        /// Asynchronously invokes the given <paramref name="action" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <param name="action">The action to reliably invoke.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="action" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>A task that represents the asynchronous invoke operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static async Task InvokeAsync(Func<Task> action, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler delayHandler)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (maxRetries < Retries.Infinite)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            if (exceptionHandler == null)
                throw new ArgumentNullException(nameof(exceptionHandler));

            if (delayHandler == null)
                throw new ArgumentNullException(nameof(delayHandler));

            int attempt = 0;

        Attempt:
            Task? t = null;
            attempt++;

            try
            {
                t = action();
                if (t == null)
                    goto Invalid;

                await t.ConfigureAwait(false);
                return;
            }
            catch (Exception e)
            {
                bool isCanceled = t != null ? t.IsCanceled : e.IsCancellation(cancellationToken);
                if (isCanceled || !exceptionHandler(e) || (maxRetries != Retries.Infinite && attempt > maxRetries))
                    throw;

                await Task.Delay(delayHandler(attempt, e), cancellationToken).ConfigureAwait(false);
                goto Attempt;
            }

        Invalid:
            throw new InvalidOperationException(SR.InvalidTaskResult);
        }

        /// <summary>
        /// Asynchronously invokes the given <paramref name="action" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="action"/>.</typeparam>
        /// <param name="action">The action to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="action" /> delegate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>A task that represents the asynchronous invoke operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static Task InvokeAsync<TState>(Func<TState, Task> action, TState state, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => InvokeAsync(action, state, CancellationToken.None, maxRetries, exceptionHandler, delayHandler.ToComplex());

        /// <summary>
        /// Asynchronously invokes the given <paramref name="action" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="action"/>.</typeparam>
        /// <param name="action">The action to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="action" /> delegate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>A task that represents the asynchronous invoke operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static Task InvokeAsync<TState>(Func<TState, Task> action, TState state, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler delayHandler)
            => InvokeAsync(action, state, CancellationToken.None, maxRetries, exceptionHandler, delayHandler);

        /// <summary>
        /// Asynchronously invokes the given <paramref name="action" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="action"/>.</typeparam>
        /// <param name="action">The action to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="action" /> delegate.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="action" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>A task that represents the asynchronous invoke operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static Task InvokeAsync<TState>(Func<TState, Task> action, TState state, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => InvokeAsync(action, state, cancellationToken, maxRetries, exceptionHandler, delayHandler.ToComplex());

        /// <summary>
        /// Asynchronously invokes the given <paramref name="action" /> despite transient problems
        /// using the provided execution policies.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="action"/>.</typeparam>
        /// <param name="action">The action to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="action" /> delegate.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="action" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>A task that represents the asynchronous invoke operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static async Task InvokeAsync<TState>(Func<TState, Task> action, TState state, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler delayHandler)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (maxRetries < Retries.Infinite)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            if (exceptionHandler == null)
                throw new ArgumentNullException(nameof(exceptionHandler));

            if (delayHandler == null)
                throw new ArgumentNullException(nameof(delayHandler));

            int attempt = 0;

        Attempt:
            Task? t = null;
            attempt++;

            try
            {
                t = action(state);
                if (t == null)
                    goto Invalid;

                await t.ConfigureAwait(false);
                return;
            }
            catch (Exception e)
            {
                bool isCanceled = t != null ? t.IsCanceled : e.IsCancellation(cancellationToken);
                if (isCanceled || !exceptionHandler(e) || (maxRetries != Retries.Infinite && attempt > maxRetries))
                    throw;

                await Task.Delay(delayHandler(attempt, e), cancellationToken).ConfigureAwait(false);
                goto Attempt;
            }

        Invalid:
            throw new InvalidOperationException(SR.InvalidTaskResult);
        }

        #endregion

        #region TryInvokeAsync

        /// <summary>
        /// Attempts to successfully invoke the <paramref name="action" /> despite transient problems.
        /// </summary>
        /// <param name="action">The action to reliably invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="action" /> was invoked sucessfully;
        /// otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static Task<bool> TryInvokeAsync(Func<Task> action, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => TryInvokeAsync(action, CancellationToken.None, maxRetries, exceptionHandler, delayHandler.ToComplex());

        /// <summary>
        /// Attempts to successfully invoke the <paramref name="action" /> despite transient problems.
        /// </summary>
        /// <param name="action">The action to reliably invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="action" /> was invoked sucessfully;
        /// otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static Task<bool> TryInvokeAsync(Func<Task> action, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler delayHandler)
            => TryInvokeAsync(action, CancellationToken.None, maxRetries, exceptionHandler, delayHandler);

        /// <summary>
        /// Attempts to successfully invoke the <paramref name="action" /> despite transient problems.
        /// </summary>
        /// <param name="action">The action to reliably invoke.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="action" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="action" /> was invoked sucessfully;
        /// otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static Task<bool> TryInvokeAsync(Func<Task> action, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => TryInvokeAsync(action, cancellationToken, maxRetries, exceptionHandler, delayHandler.ToComplex());

        /// <summary>
        /// Attempts to successfully invoke the <paramref name="action" /> despite transient problems.
        /// </summary>
        /// <param name="action">The action to reliably invoke.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="action" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="action" /> was invoked sucessfully;
        /// otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static async Task<bool> TryInvokeAsync(Func<Task> action, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler delayHandler)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (maxRetries < Retries.Infinite)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            if (exceptionHandler == null)
                throw new ArgumentNullException(nameof(exceptionHandler));

            if (delayHandler == null)
                throw new ArgumentNullException(nameof(delayHandler));

            Task? t;
            int attempt = 0;

        Attempt:
            t = null;
            attempt++;

            try
            {
                t = action();
                if (t == null)
                    goto Invalid;

                await t.ConfigureAwait(false);
                return true;
            }
            catch (Exception e)
            {
                bool isCanceled = t != null ? t.IsCanceled : e.IsCancellation(cancellationToken);
                if (isCanceled)
                    throw;

                if (!exceptionHandler(e) || (maxRetries != Retries.Infinite && attempt > maxRetries))
                    return false;

                await Task.Delay(delayHandler(attempt, e), cancellationToken).ConfigureAwait(false);
                goto Attempt;
            }

        Invalid:
            throw new InvalidOperationException(SR.InvalidTaskResult);
        }

        /// <summary>
        /// Attempts to successfully invoke the <paramref name="action" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="action"/>.</typeparam>
        /// <param name="action">The action to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="action" /> delegate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="action" /> was invoked sucessfully;
        /// otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static Task<bool> TryInvokeAsync<TState>(Func<TState, Task> action, TState state, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => TryInvokeAsync(action, state, CancellationToken.None, maxRetries, exceptionHandler, delayHandler.ToComplex());

        /// <summary>
        /// Attempts to successfully invoke the <paramref name="action" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="action"/>.</typeparam>
        /// <param name="action">The action to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="action" /> delegate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="action" /> was invoked sucessfully;
        /// otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static Task<bool> TryInvokeAsync<TState>(Func<TState, Task> action, TState state, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler delayHandler)
            => TryInvokeAsync(action, state, CancellationToken.None, maxRetries, exceptionHandler, delayHandler);

        /// <summary>
        /// Attempts to successfully invoke the <paramref name="action" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="action"/>.</typeparam>
        /// <param name="action">The action to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="action" /> delegate.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="action" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="action" /> was invoked sucessfully;
        /// otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static Task<bool> TryInvokeAsync<TState>(Func<TState, Task> action, TState state, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            => TryInvokeAsync(action, state, cancellationToken, maxRetries, exceptionHandler, delayHandler.ToComplex());

        /// <summary>
        /// Attempts to successfully invoke the <paramref name="action" /> despite transient problems.
        /// </summary>
        /// <typeparam name="TState">The type of the state object passed to the <paramref name="action"/>.</typeparam>
        /// <param name="action">The action to reliably invoke.</param>
        /// <param name="state">An object containing data to be used by the <paramref name="action" /> delegate.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to determine whether the <paramref name="action" /> was canceled.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which exceptions are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between attempts.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="action" /> was invoked sucessfully;
        /// otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        [SuppressMessage("Design", "CA1068:CancellationToken parameters must come last", Justification = "Token is not used for canceling operation, and API mirrors TaskFactory.StartNew")]
        public static async Task<bool> TryInvokeAsync<TState>(Func<TState, Task> action, TState state, CancellationToken cancellationToken, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler delayHandler)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (maxRetries < Retries.Infinite)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            if (exceptionHandler == null)
                throw new ArgumentNullException(nameof(exceptionHandler));

            if (delayHandler == null)
                throw new ArgumentNullException(nameof(delayHandler));

            Task? t;
            int attempt = 0;

        Attempt:
            t = null;
            attempt++;

            try
            {
                t = action(state);
                if (t == null)
                    goto Invalid;

                await t.ConfigureAwait(false);
                return true;
            }
            catch (Exception e)
            {
                bool isCanceled = t != null ? t.IsCanceled : e.IsCancellation(cancellationToken);
                if (isCanceled)
                    throw;

                if (!exceptionHandler(e) || (maxRetries != Retries.Infinite && attempt > maxRetries))
                    return false;

                await Task.Delay(delayHandler(attempt, e), cancellationToken).ConfigureAwait(false);
                goto Attempt;
            }

        Invalid:
            throw new InvalidOperationException(SR.InvalidTaskResult);
        }

        #endregion
    }
}

