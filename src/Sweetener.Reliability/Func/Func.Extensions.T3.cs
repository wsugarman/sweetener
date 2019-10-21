// Generated from Func.Extensions.tt
using System;
using System.Threading.Tasks;

namespace Sweetener.Reliability
{
    static partial class FuncExtensions
    {
        /// <summary>
        /// Creates a reliable wrapper around the given <paramref name="func" />
        /// that will retry the operation based on the provided policies.
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
        public static Func<T1, T2, TResult> WithRetry<T1, T2, TResult>(
            this Func<T1, T2, TResult> func,
            int maxRetries,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => WithRetry(func, maxRetries, r => ResultKind.Successful, exceptionPolicy, (i, r, e) => delayPolicy(i));

        /// <summary>
        /// Creates a reliable wrapper around the given <paramref name="func" />
        /// that will retry the operation based on the provided policies.
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
        public static Func<T1, T2, TResult> WithRetry<T1, T2, TResult>(
            this Func<T1, T2, TResult> func,
            int maxRetries,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
            => WithRetry(func, maxRetries, r => ResultKind.Successful, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a reliable wrapper around the given <paramref name="func" />
        /// that will retry the operation based on the provided policies.
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
        public static Func<T1, T2, TResult> WithRetry<T1, T2, TResult>(
            this Func<T1, T2, TResult> func,
            int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            DelayPolicy delayPolicy)
            => WithRetry(func, maxRetries, resultPolicy, exceptionPolicy, (i, r, e) => delayPolicy(i));

        /// <summary>
        /// Creates a reliable wrapper around the given <paramref name="func" />
        /// that will retry the operation based on the provided policies.
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
        public static Func<T1, T2, TResult> WithRetry<T1, T2, TResult>(
            this Func<T1, T2, TResult> func,
            int maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            if (maxRetries < -1)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            if (resultPolicy == null)
                throw new ArgumentNullException(nameof(resultPolicy));

            if (exceptionPolicy == null)
                throw new ArgumentNullException(nameof(exceptionPolicy));

            if (delayPolicy == null)
                throw new ArgumentNullException(nameof(delayPolicy));

            return (T1 arg1, T2 arg2) =>
            {
                TResult result;
                int attempt = 0;
            Attempt:
                attempt++;
                try
                {
                    result = func(arg1, arg2);
                }
                catch (Exception e)
                {
                    if (!exceptionPolicy(e) || attempt > maxRetries)
                        throw e;

                    Task.Delay(delayPolicy(attempt, default, e)).Wait();
                    goto Attempt;
                }

                ResultKind kind = resultPolicy(result);
                if (kind != ResultKind.Retryable || attempt > maxRetries)
                    return result;

                Task.Delay(delayPolicy(attempt, result, null)).Wait();
                goto Attempt;
            };
        }
    }
}
