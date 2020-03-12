// Generated from ReliableAsyncAction.tt
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sweetener.Reliability
{
    /// <summary>
    /// A wrapper to reliably invoke an asynchronous action despite transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this reliable delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this reliable delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this reliable delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the method that this reliable delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the method that this reliable delegate encapsulates.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the method that this reliable delegate encapsulates.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the method that this reliable delegate encapsulates.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the method that this reliable delegate encapsulates.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter of the method that this reliable delegate encapsulates.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter of the method that this reliable delegate encapsulates.</typeparam>
    /// <typeparam name="T11">The type of the eleventh parameter of the method that this reliable delegate encapsulates.</typeparam>
    /// <typeparam name="T12">The type of the twelfth parameter of the method that this reliable delegate encapsulates.</typeparam>
    /// <typeparam name="T13">The type of the thirteenth parameter of the method that this reliable delegate encapsulates.</typeparam>
    /// <typeparam name="T14">The type of the fourteenth parameter of the method that this reliable delegate encapsulates.</typeparam>
    /// <typeparam name="T15">The type of the fifteenth parameter of the method that this reliable delegate encapsulates.</typeparam>
    public sealed class ReliableAsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : ReliableDelegate
    {
        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, CancellationToken, Task> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15}"/>
        /// class that executes the given asynchronous action at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <param name="action">The action to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which errors are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableAsyncAction(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task> action, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            : this(action.IgnoreInterruption(), maxRetries, exceptionHandler, delayHandler)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15}"/>
        /// class that executes the given asynchronous action at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <param name="action">The action to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which errors are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableAsyncAction(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task> action, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler delayHandler)
            : this(action.IgnoreInterruption(), maxRetries, exceptionHandler, delayHandler)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15}"/>
        /// class that executes the given asynchronous action at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <param name="action">The action to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which errors are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableAsyncAction(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, CancellationToken, Task> action, int maxRetries, ExceptionHandler exceptionHandler, DelayHandler delayHandler)
            : base(maxRetries, exceptionHandler, delayHandler)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAsyncAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15}"/>
        /// class that executes the given asynchronous action at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <param name="action">The action to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionHandler">A function that determines which errors are transient.</param>
        /// <param name="delayHandler">A function that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionHandler" />, or <paramref name="delayHandler" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableAsyncAction(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, CancellationToken, Task> action, int maxRetries, ExceptionHandler exceptionHandler, ComplexDelayHandler delayHandler)
            : base(maxRetries, exceptionHandler, delayHandler)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        /// <summary>
        /// Asynchronously invokes the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="arg1">The first parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg6">The sixth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg7">The seventh parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg8">The eighth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg9">The ninth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg10">The tenth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg11">The eleventh parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg12">The twelfth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg13">The thirteenth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg14">The fourteenth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg15">The fifteenth parameter of the method that this reliable delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous invoke operation.</returns>
        /// <exception cref="InvalidOperationException">
        /// The encapsulated method returned <see langword="null"/> instead of a valid <see cref="Task"/>.
        /// </exception>
        public Task InvokeAsync(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
            => InvokeAsync(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, CancellationToken.None);

        /// <summary>
        /// Asynchronously invokes the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="arg1">The first parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg6">The sixth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg7">The seventh parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg8">The eighth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg9">The ninth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg10">The tenth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg11">The eleventh parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg12">The twelfth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg13">The thirteenth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg14">The fourteenth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg15">The fifteenth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="cancellationToken">
        /// A cancellation token to observe while waiting for the operation to complete.
        /// </param>
        /// <returns>A task that represents the asynchronous invoke operation.</returns>
        /// <exception cref="InvalidOperationException">
        /// The encapsulated method returned <see langword="null"/> instead of a valid <see cref="Task"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public async Task InvokeAsync(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, CancellationToken cancellationToken)
        {
            Task? t;
            int attempt = 0;

        Attempt:
            t = null;
            attempt++;

            try
            {
                t = _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, cancellationToken);
                if (t == null)
                    goto Invalid;

                await t.ConfigureAwait(false);
                return;
            }
            catch (Exception e)
            {
                bool isCanceled = t != null ? t.IsCanceled : e.IsCancellation(cancellationToken);
                if (isCanceled || !await CanRetryAsync(attempt, e, cancellationToken).ConfigureAwait(false))
                    throw;

                goto Attempt;
            }

        Invalid:
            throw new InvalidOperationException(SR.InvalidTaskResult);
        }

        /// <summary>
        /// Asynchronously attempts to successfully invoke the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="arg1">The first parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg6">The sixth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg7">The seventh parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg8">The eighth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg9">The ninth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg10">The tenth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg11">The eleventh parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg12">The twelfth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg13">The thirteenth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg14">The fourteenth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg15">The fifteenth parameter of the method that this reliable delegate encapsulates.</param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter contains <see langword="true"/> if the encapsulated method completed without throwing
        /// an exception within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// The encapsulated method returned <see langword="null"/> instead of a valid <see cref="Task"/>.
        /// </exception>
        public Task<bool> TryInvokeAsync(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
            => TryInvokeAsync(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, CancellationToken.None);

        /// <summary>
        /// Asynchronously attempts to successfully invoke the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="arg1">The first parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg6">The sixth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg7">The seventh parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg8">The eighth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg9">The ninth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg10">The tenth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg11">The eleventh parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg12">The twelfth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg13">The thirteenth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg14">The fourteenth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg15">The fifteenth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="cancellationToken">
        /// A cancellation token to observe while waiting for the operation to complete.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous invoke operation. The value of the <c>TResult</c>
        /// parameter contains <see langword="true"/> if the encapsulated method completed without throwing
        /// an exception within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// The encapsulated method returned <see langword="null"/> instead of a valid <see cref="Task"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public async Task<bool> TryInvokeAsync(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, CancellationToken cancellationToken)
        {
            Task? t;
            int attempt = 0;

        Attempt:
            t = null;
            attempt++;

            try
            {
                t = _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, cancellationToken);
                if (t == null)
                    goto Invalid;

                await t.ConfigureAwait(false);
                return true;
            }
            catch (Exception e)
            {
                if (e.IsCancellation(cancellationToken))
                    throw;

                if (!await CanRetryAsync(attempt, e, cancellationToken).ConfigureAwait(false))
                    return false;

                goto Attempt;
            }

        Invalid:
            throw new InvalidOperationException(SR.InvalidTaskResult);
        }
    }
}
