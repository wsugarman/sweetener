// Generated from ReliableAction.tt
using System;
using System.Threading;

namespace Sweetener.Reliability
{
    /// <summary>
    /// A wrapper to reliably invoke an action despite transient issues.
    /// </summary>
    public sealed partial class ReliableAction : ReliableDelegate
    {
        private readonly Action _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction"/>
        /// class that executes the given <see cref="Action"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="action">The underlying action to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableAction(Action action, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction"/>
        /// class that executes the given <see cref="Action"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <param name="action">The underlying action to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableAction(Action action, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        /// <summary>
        /// Invokes the underlying delegate and attempts to retry if it encounters transient exceptions.
        /// </summary>
        public void Invoke()
            => Invoke(CancellationToken.None);

        /// <summary>
        /// Invokes the underlying delegate and attempts to retry if it encounters transient exceptions.
        /// </summary>
        /// <param name="cancellationToken">
        /// A cancellation token to observe while waiting for the operation to complete.
        /// </param>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public void Invoke(CancellationToken cancellationToken)
        {
            int attempt = 0;
            Exception lastException;
            do
            {
                attempt++;
                try
                {
                    _action();
                    return;
                }
                catch (Exception e)
                {
                    lastException = e;
                }
            } while (CanRetry(attempt, lastException, cancellationToken));

            throw lastException;
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient exceptions.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke()
            => TryInvoke(CancellationToken.None);

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient exceptions.
        /// </summary>
        /// <param name="cancellationToken">
        /// A cancellation token to observe while waiting for the operation to complete.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public bool TryInvoke(CancellationToken cancellationToken)
        {
            int attempt = 0;
            Exception lastException;
            do
            {
                attempt++;
                try
                {
                    _action();
                    return true;
                }
                catch (Exception e)
                {
                    lastException = e;
                }
            } while (CanRetry(attempt, lastException, cancellationToken));

            return false;
        }
    }
}
