// Generated from AsyncFunc.Extensions.tt
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sweetener.Reliability
{
    /// <summary>
    /// A collection of methods to reliablty invoke asynchronous user-defined functions.
    /// </summary>
    public static partial class AsyncFuncExtensions
    {
        #region Func<Task<TResult>>

        /// <summary>
        /// Creates a reliable wrapper around the given asynchronous <paramref name="func" />
        /// that will retry the operation based on the provided policies.
        /// </summary>
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
        public static Func<Task<TResult>> WithAsyncRetry<TResult>(this Func<Task<TResult>> func, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            => WithAsyncRetry(func, maxRetries, ReliableDelegate<TResult>.DefaultResultPolicy, exceptionPolicy, DelayPolicies.Complex<TResult>(delayPolicy));

        /// <summary>
        /// Creates a reliable wrapper around the given asynchronous <paramref name="func" />
        /// that will retry the operation based on the provided policies.
        /// </summary>
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
        public static Func<Task<TResult>> WithAsyncRetry<TResult>(this Func<Task<TResult>> func, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            => WithAsyncRetry(func, maxRetries, ReliableDelegate<TResult>.DefaultResultPolicy, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a reliable wrapper around the given asynchronous <paramref name="func" />
        /// that will retry the operation based on the provided policies.
        /// </summary>
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
        public static Func<Task<TResult>> WithAsyncRetry<TResult>(
            this Func<Task<TResult>> func,
            int                   maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy       exceptionPolicy,
            DelayPolicy           delayPolicy)
            => WithAsyncRetry(func, maxRetries, resultPolicy, exceptionPolicy, DelayPolicies.Complex<TResult>(delayPolicy));

        /// <summary>
        /// Creates a reliable wrapper around the given asynchronous <paramref name="func" />
        /// that will retry the operation based on the provided policies.
        /// </summary>
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
        public static Func<Task<TResult>> WithAsyncRetry<TResult>(
            this Func<Task<TResult>> func,
            int                         maxRetries,
            ResultPolicy<TResult>       resultPolicy,
            ExceptionPolicy             exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            if (maxRetries < Retries.Infinite)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            if (resultPolicy == null)
                throw new ArgumentNullException(nameof(resultPolicy));

            if (exceptionPolicy == null)
                throw new ArgumentNullException(nameof(exceptionPolicy));

            if (delayPolicy == null)
                throw new ArgumentNullException(nameof(delayPolicy));

            return async () =>
            {
                Task<TResult> t = null;
                int attempt = 0;

            Attempt:
                attempt++;

                try
                {
                    t = func();
                    await t.ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    if (!exceptionPolicy(e) || (maxRetries != Retries.Infinite && attempt > maxRetries))
                        throw;

                    await Task.Delay(delayPolicy(attempt, default, e)).ConfigureAwait(false);
                    goto Attempt;
                }

                TResult result = t.Result;
                ResultKind kind = resultPolicy(result);
                if (kind != ResultKind.Transient || (maxRetries != Retries.Infinite && attempt > maxRetries))
                    return result;

                await Task.Delay(delayPolicy(attempt, result, default)).ConfigureAwait(false);
                goto Attempt;
            };
        }

        #endregion

        #region Func<CancellationToken, Task<TResult>>

        /// <summary>
        /// Creates a reliable wrapper around the given asynchronous <paramref name="func" />
        /// that will retry the operation based on the provided policies.
        /// </summary>
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
        public static Func<CancellationToken, Task<TResult>> WithAsyncRetry<TResult>(this Func<CancellationToken, Task<TResult>> func, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            => WithAsyncRetry(func, maxRetries, ReliableDelegate<TResult>.DefaultResultPolicy, exceptionPolicy, DelayPolicies.Complex<TResult>(delayPolicy));

        /// <summary>
        /// Creates a reliable wrapper around the given asynchronous <paramref name="func" />
        /// that will retry the operation based on the provided policies.
        /// </summary>
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
        public static Func<CancellationToken, Task<TResult>> WithAsyncRetry<TResult>(this Func<CancellationToken, Task<TResult>> func, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy<TResult> delayPolicy)
            => WithAsyncRetry(func, maxRetries, ReliableDelegate<TResult>.DefaultResultPolicy, exceptionPolicy, delayPolicy);

        /// <summary>
        /// Creates a reliable wrapper around the given asynchronous <paramref name="func" />
        /// that will retry the operation based on the provided policies.
        /// </summary>
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
        public static Func<CancellationToken, Task<TResult>> WithAsyncRetry<TResult>(
            this Func<CancellationToken, Task<TResult>> func,
            int                   maxRetries,
            ResultPolicy<TResult> resultPolicy,
            ExceptionPolicy       exceptionPolicy,
            DelayPolicy           delayPolicy)
            => WithAsyncRetry(func, maxRetries, resultPolicy, exceptionPolicy, DelayPolicies.Complex<TResult>(delayPolicy));

        /// <summary>
        /// Creates a reliable wrapper around the given asynchronous <paramref name="func" />
        /// that will retry the operation based on the provided policies.
        /// </summary>
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
        public static Func<CancellationToken, Task<TResult>> WithAsyncRetry<TResult>(
            this Func<CancellationToken, Task<TResult>> func,
            int                         maxRetries,
            ResultPolicy<TResult>       resultPolicy,
            ExceptionPolicy             exceptionPolicy,
            ComplexDelayPolicy<TResult> delayPolicy)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            if (maxRetries < Retries.Infinite)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            if (resultPolicy == null)
                throw new ArgumentNullException(nameof(resultPolicy));

            if (exceptionPolicy == null)
                throw new ArgumentNullException(nameof(exceptionPolicy));

            if (delayPolicy == null)
                throw new ArgumentNullException(nameof(delayPolicy));

            return async (cancellationToken) =>
            {
                Task<TResult> t = null;
                int attempt = 0;

            Attempt:
                attempt++;

                try
                {
                    t = func(cancellationToken);
                    await t.ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    if (t.IsCanceled || !exceptionPolicy(e) || (maxRetries != Retries.Infinite && attempt > maxRetries))
                        throw;

                    await Task.Delay(delayPolicy(attempt, default, e), cancellationToken).ConfigureAwait(false);
                    goto Attempt;
                }

                TResult result = t.Result;
                ResultKind kind = resultPolicy(result);
                if (kind != ResultKind.Transient || (maxRetries != Retries.Infinite && attempt > maxRetries))
                    return result;

                await Task.Delay(delayPolicy(attempt, result, default), cancellationToken).ConfigureAwait(false);
                goto Attempt;
            };
        }

        #endregion
    }
}
