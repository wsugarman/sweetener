// Generated from Action.Extensions.tt
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sweetener.Reliability
{
    /// <summary>
    /// A collection of methods to reliablty invoke actions.
    /// </summary>
    public static partial class ActionExtensions
    {
        /// <summary>
        /// Creates a reliable wrapper around the given <paramref name="action" />
        /// that will retry the operation based on the provided policies.
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
        public static InterruptableAction WithRetry(this Action action, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            => WithRetry(action, maxRetries, exceptionPolicy, (i, e) => delayPolicy(i));

        /// <summary>
        /// Creates a reliable wrapper around the given <paramref name="action" />
        /// that will retry the operation based on the provided policies.
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
        public static InterruptableAction WithRetry(this Action action, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy delayPolicy)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (maxRetries < Retries.Infinite)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            if (exceptionPolicy == null)
                throw new ArgumentNullException(nameof(exceptionPolicy));

            if (delayPolicy == null)
                throw new ArgumentNullException(nameof(delayPolicy));

            return (cancellationToken) =>
            {
                int attempt = 0;

            Attempt:
                attempt++;
                try
                {
                    action();
                    return;
                }
                catch (Exception e)
                {
                    if (!exceptionPolicy(e) || (maxRetries != Retries.Infinite && attempt > maxRetries))
                        throw e;

                    Task.Delay(delayPolicy(attempt, e), cancellationToken).Wait(cancellationToken);
                    goto Attempt;
                }
            };
        }
    }
}
