// Generated from ReliableAsyncAction.Create.tt
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sweetener.Reliability
{
    partial class ReliableAsyncAction
    {
        /// <summary>
        /// Creates a new <see cref="ReliableAsyncAction{T}"/>
        /// that executes the given asynchronous action at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <param name="action">The action to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which errors are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between retries.</param>
        /// <returns>A reliable delegate that encapsulates the <paramref name="action" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableAsyncAction<T> Create<T>(
            Func<T, Task> action,
            int              maxRetries,
            ExceptionHandler exceptionHandler,
            DelayHandler     delayHandler)
            => new ReliableAsyncAction<T>(action, maxRetries, exceptionHandler, delayHandler);

        /// <summary>
        /// Creates a new <see cref="ReliableAsyncAction{T}"/>
        /// that executes the given asynchronous action at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <param name="action">The action to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which errors are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between retries.</param>
        /// <returns>A reliable delegate that encapsulates the <paramref name="action" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableAsyncAction<T> Create<T>(
            Func<T, Task> action,
            int                 maxRetries,
            ExceptionHandler    exceptionHandler,
            ComplexDelayHandler delayHandler)
            => new ReliableAsyncAction<T>(action, maxRetries, exceptionHandler, delayHandler);

        /// <summary>
        /// Creates a new <see cref="ReliableAsyncAction{T}"/>
        /// that executes the given asynchronous action at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <param name="action">The action to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which errors are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between retries.</param>
        /// <returns>A reliable delegate that encapsulates the <paramref name="action" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableAsyncAction<T> Create<T>(
            Func<T, CancellationToken, Task> action,
            int              maxRetries,
            ExceptionHandler exceptionHandler,
            DelayHandler     delayHandler)
            => new ReliableAsyncAction<T>(action, maxRetries, exceptionHandler, delayHandler);

        /// <summary>
        /// Creates a new <see cref="ReliableAsyncAction{T}"/>
        /// that executes the given asynchronous action at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this reliable delegate encapsulates.</typeparam>
        /// <param name="action">The action to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which errors are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between retries.</param>
        /// <returns>A reliable delegate that encapsulates the <paramref name="action" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public static ReliableAsyncAction<T> Create<T>(
            Func<T, CancellationToken, Task> action,
            int                 maxRetries,
            ExceptionHandler    exceptionHandler,
            ComplexDelayHandler delayHandler)
            => new ReliableAsyncAction<T>(action, maxRetries, exceptionHandler, delayHandler);
    }
}
