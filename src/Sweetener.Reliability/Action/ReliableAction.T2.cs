// Generated from ReliableAction.tt
using System;
using System.Threading;

namespace Sweetener.Reliability
{
    /// <summary>
    /// A wrapper to reliably invoke an action despite transient issues.
    /// </summary>
    /// <typeparam name="T">The type of the parameter of the underlying delegate.</typeparam>
    public sealed class ReliableAction<T> : ReliableDelegate
    {
        private readonly Action<T> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction{T}"/>
        /// class that executes the given <see cref="Action{T}"/> at most a
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
        public ReliableAction(Action<T> action, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction{T}"/>
        /// class that executes the given <see cref="Action{T}"/> at most a
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
        public ReliableAction(Action<T> action, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        /// <summary>
        /// Invokes the underlying delegate and attempts to retry if it encounters transient exceptions.
        /// </summary>
        /// <param name="arg">The argument for the underlying delegate.</param>
        public void Invoke(T arg)
            => Invoke(arg, CancellationToken.None);

        /// <summary>
        /// Invokes the underlying delegate and attempts to retry if it encounters transient exceptions.
        /// </summary>
        /// <param name="arg">The argument for the underlying delegate.</param>
        /// <param name="cancellationToken">
        /// A cancellation token to observe while waiting for the operation to complete.
        /// </param>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public void Invoke(T arg, CancellationToken cancellationToken)
        {
            int attempt = 0;
            Exception lastException;
            do
            {
                attempt++;
                try
                {
                    _action(arg);
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
        /// <param name="arg">The argument for the underlying delegate.</param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T arg)
            => TryInvoke(arg, CancellationToken.None);

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient exceptions.
        /// </summary>
        /// <param name="arg">The argument for the underlying delegate.</param>
        /// <param name="cancellationToken">
        /// A cancellation token to observe while waiting for the operation to complete.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public bool TryInvoke(T arg, CancellationToken cancellationToken)
        {
            int attempt = 0;
            Exception lastException;
            do
            {
                attempt++;
                try
                {
                    _action(arg);
                    return true;
                }
                catch (Exception e)
                {
                    lastException = e;
                }
            } while (CanRetry(attempt, lastException, cancellationToken));

            return false;
        }

        /// <summary>
        /// Implicitly converts the <paramref name="reliableAction"/> to an
        /// <see cref="Action{T}"/>.
        /// </summary>
        /// <remarks>
        /// The resulting action is equivalent to <see cref="Invoke(T)"/>.
        /// </remarks>
        /// <param name="reliableAction">An operation that may be retried due to transient failures.</param>
        public static implicit operator Action<T>(ReliableAction<T> reliableAction)
            => reliableAction.Invoke;
    }
}
