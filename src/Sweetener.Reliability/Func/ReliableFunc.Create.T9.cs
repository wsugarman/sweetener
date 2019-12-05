// Generated from ReliableFunc.Create.tt
using System;

namespace Sweetener.Reliability
{
    static partial class ReliableFunc
    {
        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this reliable delegate encapsulates.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <returns>A reliable delegate that encapsulates the <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func,
            int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this reliable delegate encapsulates.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <returns>A reliable delegate that encapsulates the <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func,
            int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this reliable delegate encapsulates.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <returns>A reliable delegate that encapsulates the <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func,
            int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this reliable delegate encapsulates.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <returns>A reliable delegate that encapsulates the <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func,
            int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);
    }
}
