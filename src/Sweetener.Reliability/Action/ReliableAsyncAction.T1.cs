// Generated from ReliableAsyncAction.tt
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sweetener.Reliability
{
    /// <summary>
    /// A wrapper to reliably invoke an asynchronous action despite transient issues.
    /// </summary>
    /// <typeparam name="T">The type of the parameter of the method that this reliable delegate encapsulates.</typeparam>
    public sealed class ReliableAsyncAction<T> : ReliableDelegate
    {
        private readonly Func<T, CancellationToken, Task> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncAction{T}"/>
        /// class that executes the given asynchronous action at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <param name="action">The action to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which errors are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableAsyncAction(Func<T, Task> action, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            : this(action.IgnoreInterruption(), maxRetries, exceptionHandler, delayHandler)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncAction{T}"/>
        /// class that executes the given asynchronous action at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <param name="action">The action to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which errors are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableAsyncAction(Func<T, Task> action, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler delayHandler)
            : this(action.IgnoreInterruption(), maxRetries, exceptionHandler, delayHandler)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncAction{T}"/>
        /// class that executes the given asynchronous action at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <param name="action">The action to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which errors are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableAsyncAction(Func<T, CancellationToken, Task> action, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            : base(maxRetries, exceptionHandler, delayHandler)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncAction{T}"/>
        /// class that executes the given asynchronous action at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <param name="action">The action to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which errors are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableAsyncAction(Func<T, CancellationToken, Task> action, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler delayHandler)
            : base(maxRetries, exceptionHandler, delayHandler)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        /// <summary>
        /// Asynchronously invokes the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="arg">The parameter of the method that this reliable delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous invoke operation.</returns>
        /// <exception cref="InvalidOperationException">
        /// The encapsulated method returned <see langword="null"/> instead of a valid <see cref="Task"/>.
        /// </exception>
        public async Task InvokeAsync(T arg)
            => await InvokeAsync(arg, CancellationToken.None).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="arg">The parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="cancellationToken">
        /// A cancellation token to observe while waiting for the operation to complete.
        /// </param>
        /// <returns>A task that represents the asynchronous invoke operation.</returns>
        /// <exception cref="InvalidOperationException">
        /// The encapsulated method returned <see langword="null"/> instead of a valid <see cref="Task"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public async Task InvokeAsync(T arg, CancellationToken cancellationToken)
        {
            Task t;
            int attempt = 0;

        Attempt:
            t = null;
            attempt++;

            try
            {
                t = _action(arg, cancellationToken);
                if (t == null)
                    goto Invalid;

                await t.ConfigureAwait(false);
                return;
            }
            catch (Exception e)
            {
                bool isCanceled = t != null ? t.IsCanceled : e.IsCancellation(cancellationToken);
                if (isCanceled || !await CanRetryAsync(attempt, e, cancellationToken).ConfigureAwait(false))
                    throw;

                goto Attempt;
            }

        Invalid:
            throw new InvalidOperationException("Method resulted in an invalid Task.");
        }

        /// <summary>
        /// Asynchronously attempts to successfully invoke the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="arg">The parameter of the method that this reliable delegate encapsulates.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter contains <see langword="true"/> if the encapsulated method completed without throwing
        /// an exception within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// The encapsulated method returned <see langword="null"/> instead of a valid <see cref="Task"/>.
        /// </exception>
        public async Task<bool> TryInvokeAsync(T arg)
            => await TryInvokeAsync(arg, CancellationToken.None).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously attempts to successfully invoke the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="arg">The parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="cancellationToken">
        /// A cancellation token to observe while waiting for the operation to complete.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter contains <see langword="true"/> if the encapsulated method completed without throwing
        /// an exception within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// The encapsulated method returned <see langword="null"/> instead of a valid <see cref="Task"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public async Task<bool> TryInvokeAsync(T arg, CancellationToken cancellationToken)
        {
            Task t;
            int attempt = 0;

        Attempt:
            t = null;
            attempt++;

            try
            {
                t = _action(arg, cancellationToken);
                if (t == null)
                    goto Invalid;

                await t.ConfigureAwait(false);
                return true;
            }
            catch (Exception e)
            {
                if (e.IsCancellation(cancellationToken))
                    throw;

                if (!await CanRetryAsync(attempt, e, cancellationToken).ConfigureAwait(false))
                    return false;

                goto Attempt;
            }

        Invalid:
            throw new InvalidOperationException("Method resulted in an invalid Task.");
        }
    }
}
