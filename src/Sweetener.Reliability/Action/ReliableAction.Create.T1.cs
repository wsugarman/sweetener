// Generated from ReliableAction.Create.tt
using System;
using System.Threading;

namespace Sweetener.Reliability
{
    partial class ReliableAction
    {
        /// <summary>
        /// Creates a new <see cref="ReliableAction{T}"/>
        /// that executes the given <see cref="Action{T}"/>
        /// at most a specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <param name="action">The action to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <returns>A reliable delegate that encapsulates the <paramref name="action" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableAction<T> Create<T>(
            Action<T> action,
            int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableAction<T>(action, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableAction{T}"/>
        /// that executes the given <see cref="Action{T}"/>
        /// at most a specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <param name="action">The action to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <returns>A reliable delegate that encapsulates the <paramref name="action" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableAction<T> Create<T>(
            Action<T> action,
            int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy delayPolicy)
            => new ReliableAction<T>(action, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableAction{T}"/>
        /// that executes the given <see cref="Action{T, CancellationToken}"/>
        /// at most a specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <param name="action">The action to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <returns>A reliable delegate that encapsulates the <paramref name="action" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableAction<T> Create<T>(
            Action<T, CancellationToken> action,
            int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableAction<T>(action, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableAction{T}"/>
        /// that executes the given <see cref="Action{T, CancellationToken}"/>
        /// at most a specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <param name="action">The action to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <returns>A reliable delegate that encapsulates the <paramref name="action" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableAction<T> Create<T>(
            Action<T, CancellationToken> action,
            int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy delayPolicy)
            => new ReliableAction<T>(action, maxRetries, exceptionPolicy, delayPolicy);
    }
}
