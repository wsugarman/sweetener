// Generated from ReliableAction.tt
using System;
using System.Threading;

namespace Sweetener.Reliability
{
    /// <summary>
    /// A wrapper to reliably invoke an action despite transient issues.
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
    public sealed class ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : ReliableDelegate
    {
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10}"/>
        /// class that executes the given <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
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
        public ReliableAction(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10}"/>
        /// class that executes the given <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10}"/> at most a
        /// specific number of times based on the provided policies.
        /// </summary>
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
        public ReliableAction(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        /// <summary>
        /// Invokes the underlying delegate and attempts to retry if it encounters transient exceptions.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <param name="arg7">The seventh argument for the underlying delegate.</param>
        /// <param name="arg8">The eighth argument for the underlying delegate.</param>
        /// <param name="arg9">The ninth argument for the underlying delegate.</param>
        /// <param name="arg10">The tenth argument for the underlying delegate.</param>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
            => Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, CancellationToken.None);

        /// <summary>
        /// Invokes the underlying delegate and attempts to retry if it encounters transient exceptions.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <param name="arg7">The seventh argument for the underlying delegate.</param>
        /// <param name="arg8">The eighth argument for the underlying delegate.</param>
        /// <param name="arg9">The ninth argument for the underlying delegate.</param>
        /// <param name="arg10">The tenth argument for the underlying delegate.</param>
        /// <param name="cancellationToken">
        /// A cancellation token to observe while waiting for the operation to complete.
        /// </param>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, CancellationToken cancellationToken)
        {
            int attempt = 0;
            Exception lastException;
            do
            {
                attempt++;
                try
                {
                    _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                    return;
                }
                catch (Exception e)
                {
                    lastException = e;
                }
            } while (CanRetry(attempt, lastException, cancellationToken));

            throw lastException;
        }

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient exceptions.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <param name="arg7">The seventh argument for the underlying delegate.</param>
        /// <param name="arg8">The eighth argument for the underlying delegate.</param>
        /// <param name="arg9">The ninth argument for the underlying delegate.</param>
        /// <param name="arg10">The tenth argument for the underlying delegate.</param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
            => TryInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, CancellationToken.None);

        /// <summary>
        /// Attempts to successfully invoke the underlying delegate despite transient exceptions.
        /// </summary>
        /// <param name="arg1">The first argument for the underlying delegate.</param>
        /// <param name="arg2">The second argument for the underlying delegate.</param>
        /// <param name="arg3">The third argument for the underlying delegate.</param>
        /// <param name="arg4">The fourth argument for the underlying delegate.</param>
        /// <param name="arg5">The fifth argument for the underlying delegate.</param>
        /// <param name="arg6">The sixth argument for the underlying delegate.</param>
        /// <param name="arg7">The seventh argument for the underlying delegate.</param>
        /// <param name="arg8">The eighth argument for the underlying delegate.</param>
        /// <param name="arg9">The ninth argument for the underlying delegate.</param>
        /// <param name="arg10">The tenth argument for the underlying delegate.</param>
        /// <param name="cancellationToken">
        /// A cancellation token to observe while waiting for the operation to complete.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, CancellationToken cancellationToken)
        {
            int attempt = 0;
            Exception lastException;
            do
            {
                attempt++;
                try
                {
                    _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                    return true;
                }
                catch (Exception e)
                {
                    lastException = e;
                }
            } while (CanRetry(attempt, lastException, cancellationToken));

            return false;
        }
    }
}
