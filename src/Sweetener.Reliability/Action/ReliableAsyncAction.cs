// Generated from ReliableAsyncAction.tt
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Sweetener.Reliability
{
    /// <summary>
    /// A wrapper to reliably invoke an asynchronous action despite transient issues.
    /// </summary>
    public sealed partial class ReliableAsyncAction : ReliableDelegate
    {
        private readonly Func<CancellationToken, Task> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncAction"/>
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
        public ReliableAsyncAction(Func<Task> action, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            : this(action.IgnoreInterruption(), maxRetries, exceptionHandler, delayHandler)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncAction"/>
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
        public ReliableAsyncAction(Func<Task> action, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler delayHandler)
            : this(action.IgnoreInterruption(), maxRetries, exceptionHandler, delayHandler)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncAction"/>
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
        public ReliableAsyncAction(Func<CancellationToken, Task> action, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            : base(maxRetries, exceptionHandler, delayHandler)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncAction"/>
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
        public ReliableAsyncAction(Func<CancellationToken, Task> action, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler delayHandler)
            : base(maxRetries, exceptionHandler, delayHandler)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        /// <summary>
        /// Asynchronously invokes the encapsulated method despite transient errors.
        /// </summary>
        /// <returns>A task that represents the asynchronous invoke operation.</returns>
        /// <exception cref="InvalidOperationException">
        /// The encapsulated method returned <see langword="null"/> instead of a valid <see cref="Task"/>.
        /// </exception>
        public Task InvokeAsync()
            => InvokeAsync(CancellationToken.None);

        /// <summary>
        /// Asynchronously invokes the encapsulated method despite transient errors.
        /// </summary>
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
        public async Task InvokeAsync(CancellationToken cancellationToken)
        {
            // Check for cancellation before invoking
            cancellationToken.ThrowIfCancellationRequested();

            int attempt = 0;

        Attempt:
            attempt++;

            try
            {
                Task t = _action(cancellationToken);
                if (t == null)
                    goto Invalid;

                await t.ConfigureAwait(false);
                return;
            }
            catch (OperationCanceledException oce) when (cancellationToken.IsCancellationRequested && oce.CancellationToken == cancellationToken)
            {
                throw;
            }
            catch (Exception e)
            {
                if (!await CanRetryAsync(attempt, e, cancellationToken).ConfigureAwait(false))
                    throw;

                goto Attempt;
            }

        Invalid:
            throw new InvalidOperationException(SR.InvalidTaskResult);
        }

        /// <summary>
        /// Asynchronously attempts to successfully invoke the encapsulated method despite transient errors.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter contains <see langword="true"/> if the encapsulated method completed without throwing
        /// an exception within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// The encapsulated method returned <see langword="null"/> instead of a valid <see cref="Task"/>.
        /// </exception>
        public Task<bool> TryInvokeAsync()
            => TryInvokeAsync(CancellationToken.None);

        /// <summary>
        /// Asynchronously attempts to successfully invoke the encapsulated method despite transient errors.
        /// </summary>
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
        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "All exceptions must be caught so they can be tested by the exception handler")]
        public async Task<bool> TryInvokeAsync(CancellationToken cancellationToken)
        {
            // Check for cancellation before invoking
            cancellationToken.ThrowIfCancellationRequested();

            int attempt = 0;

        Attempt:
            attempt++;

            try
            {
                Task t = _action(cancellationToken);
                if (t == null)
                    goto Invalid;

                await t.ConfigureAwait(false);
                return true;
            }
            catch (OperationCanceledException oce) when (cancellationToken.IsCancellationRequested && oce.CancellationToken == cancellationToken)
            {
                throw;
            }
            catch (Exception e)
            {
                if (!await CanRetryAsync(attempt, e, cancellationToken).ConfigureAwait(false))
                    return false;

                goto Attempt;
            }

        Invalid:
            throw new InvalidOperationException(SR.InvalidTaskResult);
        }
    }
}
