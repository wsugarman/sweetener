// Generated from ReliableAsyncAction.tt
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sweetener.Reliability
{
    /// <summary>
    /// A wrapper to reliably invoke an asynchronous action despite transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this reliable delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this reliable delegate encapsulates.</typeparam>
    public sealed class ReliableAsyncAction<T1, T2> : ReliableDelegate
    {
        private readonly AsyncAction<T1, T2> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncAction{T1, T2}"/>
        /// class that executes the given <see cref="AsyncAction{T1, T2}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="action">The action to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableAsyncAction(AsyncAction<T1, T2> action, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncAction{T1, T2}"/>
        /// class that executes the given <see cref="AsyncAction{T1, T2}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="action">The action to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableAsyncAction(AsyncAction<T1, T2> action, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        /// <summary>
        /// Asynchronously invokes the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="arg1">The first parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this reliable delegate encapsulates.</param>
        public async Task InvokeAsync(T1 arg1, T2 arg2)
            => await InvokeAsync(arg1, arg2, CancellationToken.None).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="arg1">The first parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="cancellationToken">
        /// A cancellation token to observe while waiting for the operation to complete.
        /// </param>
        /// <exception cref="ObjectDisposedException">
        /// The provided <paramref name="cancellationToken"/> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public async Task InvokeAsync(T1 arg1, T2 arg2, CancellationToken cancellationToken)
        {
            int attempt = 0;
            do
            {
                attempt++;
                try
                {
                    await _action(arg1, arg2).ConfigureAwait(false);
                    return;
                }
                catch (Exception e)
                {
                    if (!await CanRetryAsync(attempt, e, cancellationToken).ConfigureAwait(false))
                        throw;
                }
            } while (true);
        }
    }
}
