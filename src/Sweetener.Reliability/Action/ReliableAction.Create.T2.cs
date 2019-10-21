// Generated from ReliableAction.Create.tt
using System;

namespace Sweetener.Reliability
{
    partial class ReliableAction
    {
        /// <summary>
        /// Creates a new <see cref="ReliableAction{T1, T2}"/>
        /// that executes the given <see cref="Action{T1, T2}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
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
        public static ReliableAction<T1, T2> Create<T1, T2>(
            Action<T1, T2> action,
            int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableAction<T1, T2>(action, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableAction{T1, T2}"/>
        /// that executes the given <see cref="Action{T1, T2}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
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
        public static ReliableAction<T1, T2> Create<T1, T2>(
            Action<T1, T2> action,
            int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy delayPolicy)
            => new ReliableAction<T1, T2>(action, maxRetries, exceptionPolicy, delayPolicy);
    }
}
