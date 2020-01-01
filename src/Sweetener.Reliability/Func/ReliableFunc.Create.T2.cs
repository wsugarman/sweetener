// Generated from ReliableFunc.Create.tt
using System;
using System.Threading;

namespace Sweetener.Reliability
{
    static partial class ReliableFunc
    {
        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T, TResult}"/>
        /// that executes the given function at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this reliable delegate encapsulates.</typeparam>
        /// <param name="func">The function to encapsulate.</param>
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
        public static ReliableFunc<T, TResult> Create<T, TResult>(
            Func<T, TResult> func,
            int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T, TResult}"/>
        /// that executes the given function at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this reliable delegate encapsulates.</typeparam>
        /// <param name="func">The function to encapsulate.</param>
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
        public static ReliableFunc<T, TResult> Create<T, TResult>(
            Func<T, TResult> func,
            int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T, TResult}"/>
        /// that executes the given function at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this reliable delegate encapsulates.</typeparam>
        /// <param name="func">The function to encapsulate.</param>
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
        public static ReliableFunc<T, TResult> Create<T, TResult>(
            Func<T, TResult> func,
            int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T, TResult}"/>
        /// that executes the given function at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this reliable delegate encapsulates.</typeparam>
        /// <param name="func">The function to encapsulate.</param>
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
        public static ReliableFunc<T, TResult> Create<T, TResult>(
            Func<T, TResult> func,
            int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T, TResult}"/>
        /// that executes the given function at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this reliable delegate encapsulates.</typeparam>
        /// <param name="func">The function to encapsulate.</param>
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
        public static ReliableFunc<T, TResult> Create<T, TResult>(
            Func<T, CancellationToken, TResult> func,
            int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T, TResult}"/>
        /// that executes the given function at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this reliable delegate encapsulates.</typeparam>
        /// <param name="func">The function to encapsulate.</param>
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
        public static ReliableFunc<T, TResult> Create<T, TResult>(
            Func<T, CancellationToken, TResult> func,
            int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T, TResult}"/>
        /// that executes the given function at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this reliable delegate encapsulates.</typeparam>
        /// <param name="func">The function to encapsulate.</param>
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
        public static ReliableFunc<T, TResult> Create<T, TResult>(
            Func<T, CancellationToken, TResult> func,
            int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T, TResult}"/>
        /// that executes the given function at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this reliable delegate encapsulates.</typeparam>
        /// <param name="func">The function to encapsulate.</param>
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
        public static ReliableFunc<T, TResult> Create<T, TResult>(
            Func<T, CancellationToken, TResult> func,
            int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

    }
}
