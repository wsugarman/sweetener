// Generated from ReliableAsyncAction.Create.tt
using System;

namespace Sweetener.Reliability
{
    partial class ReliableAction
    {
        /// <summary>
        /// Creates a new <see cref="ReliableAsyncAction{T1, T2, T3, T4}"/>
        /// that executes the given <see cref="AsyncAction{T1, T2, T3, T4}"/>
        /// at most a specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this reliable delegate encapsulates.</typeparam>
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
        public static ReliableAsyncAction<T1, T2, T3, T4> Create<T1, T2, T3, T4>(
            AsyncAction<T1, T2, T3, T4> action,
            int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableAsyncAction<T1, T2, T3, T4>(action, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableAsyncAction{T1, T2, T3, T4}"/>
        /// that executes the given <see cref="AsyncAction{T1, T2, T3, T4}"/>
        /// at most a specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this reliable delegate encapsulates.</typeparam>
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
        public static ReliableAsyncAction<T1, T2, T3, T4> Create<T1, T2, T3, T4>(
            AsyncAction<T1, T2, T3, T4> action,
            int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy delayPolicy)
            => new ReliableAsyncAction<T1, T2, T3, T4>(action, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableAsyncAction{T1, T2, T3, T4}"/>
        /// that executes the given <see cref="InterruptableAsyncAction{T1, T2, T3, T4}"/>
        /// at most a specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this reliable delegate encapsulates.</typeparam>
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
        public static ReliableAsyncAction<T1, T2, T3, T4> Create<T1, T2, T3, T4>(
            InterruptableAsyncAction<T1, T2, T3, T4> action,
            int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableAsyncAction<T1, T2, T3, T4>(action, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableAsyncAction{T1, T2, T3, T4}"/>
        /// that executes the given <see cref="InterruptableAsyncAction{T1, T2, T3, T4}"/>
        /// at most a specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this reliable delegate encapsulates.</typeparam>
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
        public static ReliableAsyncAction<T1, T2, T3, T4> Create<T1, T2, T3, T4>(
            InterruptableAsyncAction<T1, T2, T3, T4> action,
            int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy delayPolicy)
            => new ReliableAsyncAction<T1, T2, T3, T4>(action, maxRetries, exceptionPolicy, delayPolicy);
    }
}
