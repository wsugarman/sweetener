// Generated from ReliableFunc.Create.tt
using System;

namespace Sweetener.Reliability
{
    /// <summary>
    /// Provides a set of methods for creating reliable functions.
    /// </summary>
    public static class ReliableFunc
    {
        #region Create<TResult>

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{TResult}"/>
        /// that executes the given <see cref="Func{TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<TResult> Create<TResult>(
            Func<TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{TResult}"/>
        /// that executes the given <see cref="Func{TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<TResult> Create<TResult>(
            Func<TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{TResult}"/>
        /// that executes the given <see cref="Func{TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<TResult> Create<TResult>(
            Func<TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{TResult}"/>
        /// that executes the given <see cref="Func{TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<TResult> Create<TResult>(
            Func<TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        #endregion

        #region Create<T, TResult>

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T, TResult}"/>
        /// that executes the given <see cref="Func{T, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T, TResult> Create<T, TResult>(
            Func<T, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T, TResult}"/>
        /// that executes the given <see cref="Func{T, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T, TResult> Create<T, TResult>(
            Func<T, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T, TResult}"/>
        /// that executes the given <see cref="Func{T, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T, TResult> Create<T, TResult>(
            Func<T, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T, TResult}"/>
        /// that executes the given <see cref="Func{T, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T, TResult> Create<T, TResult>(
            Func<T, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        #endregion

        #region Create<T1, T2, TResult>

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, TResult> Create<T1, T2, TResult>(
            Func<T1, T2, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, TResult> Create<T1, T2, TResult>(
            Func<T1, T2, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, TResult> Create<T1, T2, TResult>(
            Func<T1, T2, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, TResult> Create<T1, T2, TResult>(
            Func<T1, T2, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        #endregion

        #region Create<T1, T2, T3, TResult>

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, TResult> Create<T1, T2, T3, TResult>(
            Func<T1, T2, T3, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, TResult> Create<T1, T2, T3, TResult>(
            Func<T1, T2, T3, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, TResult> Create<T1, T2, T3, TResult>(
            Func<T1, T2, T3, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, TResult> Create<T1, T2, T3, TResult>(
            Func<T1, T2, T3, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        #endregion

        #region Create<T1, T2, T3, T4, TResult>

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, TResult> Create<T1, T2, T3, T4, TResult>(
            Func<T1, T2, T3, T4, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, TResult> Create<T1, T2, T3, T4, TResult>(
            Func<T1, T2, T3, T4, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, TResult> Create<T1, T2, T3, T4, TResult>(
            Func<T1, T2, T3, T4, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, TResult> Create<T1, T2, T3, T4, TResult>(
            Func<T1, T2, T3, T4, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        #endregion

        #region Create<T1, T2, T3, T4, T5, TResult>

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, TResult> Create<T1, T2, T3, T4, T5, TResult>(
            Func<T1, T2, T3, T4, T5, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, TResult> Create<T1, T2, T3, T4, T5, TResult>(
            Func<T1, T2, T3, T4, T5, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, TResult> Create<T1, T2, T3, T4, T5, TResult>(
            Func<T1, T2, T3, T4, T5, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, TResult> Create<T1, T2, T3, T4, T5, TResult>(
            Func<T1, T2, T3, T4, T5, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        #endregion

        #region Create<T1, T2, T3, T4, T5, T6, TResult>

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, TResult> Create<T1, T2, T3, T4, T5, T6, TResult>(
            Func<T1, T2, T3, T4, T5, T6, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, TResult> Create<T1, T2, T3, T4, T5, T6, TResult>(
            Func<T1, T2, T3, T4, T5, T6, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, TResult> Create<T1, T2, T3, T4, T5, T6, TResult>(
            Func<T1, T2, T3, T4, T5, T6, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, TResult> Create<T1, T2, T3, T4, T5, T6, TResult>(
            Func<T1, T2, T3, T4, T5, T6, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        #endregion

        #region Create<T1, T2, T3, T4, T5, T6, T7, TResult>

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, TResult> Create<T1, T2, T3, T4, T5, T6, T7, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, TResult> Create<T1, T2, T3, T4, T5, T6, T7, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, TResult> Create<T1, T2, T3, T4, T5, T6, T7, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, TResult> Create<T1, T2, T3, T4, T5, T6, T7, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        #endregion

        #region Create<T1, T2, T3, T4, T5, T6, T7, T8, TResult>

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        #endregion

        #region Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        #endregion

        #region Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        #endregion

        #region Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        #endregion

        #region Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        #endregion

        #region Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        #endregion

        #region Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        #endregion

        #region Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        #endregion

        #region Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T16">The type of the sixteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T16">The type of the sixteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func, int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(func, maxRetries, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T16">The type of the sixteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a new <see cref="ReliableFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult}"/>
        /// that executes the given <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="T16">The type of the sixteenth parameter of the underlying delegate.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the underlying delegate.</typeparam>
        /// <param name="func">The underlying function to invoke.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="resultPolicy">The policy that determines which results are valid.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" />, <paramref name="resultPolicy" /> <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func, int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => new ReliableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(func, maxRetries, resultPolicy, exceptionPolicy, delayPolicy);

        #endregion

    }
}
