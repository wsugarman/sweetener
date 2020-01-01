// Generated from ReliableAction.tt
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sweetener.Reliability
{
    /// <summary>
    /// A wrapper to reliably invoke an action despite transient issues.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this reliable delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this reliable delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this reliable delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the method that this reliable delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the method that this reliable delegate encapsulates.</typeparam>
    public sealed class ReliableAction<T1, T2, T3, T4, T5> : ReliableDelegate
    {
        private readonly Action<T1, T2, T3, T4, T5, CancellationToken> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction{T1, T2, T3, T4, T5}"/>
        /// class that executes the given action at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <param name="action">The action to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableAction(Action<T1, T2, T3, T4, T5> action, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : this(action.IgnoreInterruption(), maxRetries, exceptionPolicy, delayPolicy)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction{T1, T2, T3, T4, T5}"/>
        /// class that executes the given action at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <param name="action">The action to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableAction(Action<T1, T2, T3, T4, T5> action, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy delayPolicy)
            : this(action.IgnoreInterruption(), maxRetries, exceptionPolicy, delayPolicy)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction{T1, T2, T3, T4, T5}"/>
        /// class that executes the given action at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <param name="action">The action to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableAction(Action<T1, T2, T3, T4, T5, CancellationToken> action, int maxRetries, ExceptionPolicy exceptionPolicy, DelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReliableAction{T1, T2, T3, T4, T5}"/>
        /// class that executes the given action at most a specific number of times
        /// based on the provided policies.
        /// </summary>
        /// <param name="action">The action to encapsulate.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="exceptionPolicy">The policy that determines which errors are transient.</param>
        /// <param name="delayPolicy">The policy that determines how long wait to wait between retries.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" />, <paramref name="exceptionPolicy" />, or <paramref name="delayPolicy" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxRetries" /> is a negative number other than <c>-1</c>, which represents an infinite number of retries.
        /// </exception>
        public ReliableAction(Action<T1, T2, T3, T4, T5, CancellationToken> action, int maxRetries, ExceptionPolicy exceptionPolicy, ComplexDelayPolicy delayPolicy)
            : base(maxRetries, exceptionPolicy, delayPolicy)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        /// <summary>
        /// Invokes the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="arg1">The first parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this reliable delegate encapsulates.</param>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            => Invoke(arg1, arg2, arg3, arg4, arg5, CancellationToken.None);

        /// <summary>
        /// Invokes the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="arg1">The first parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="cancellationToken">
        /// A cancellation token to observe while waiting for the operation to complete.
        /// </param>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, CancellationToken cancellationToken)
        {
            int attempt = 0;
            do
            {
                attempt++;
                try
                {
                    _action(arg1, arg2, arg3, arg4, arg5, cancellationToken);
                    return;
                }
                catch (Exception e)
                {
                    if (e.IsCancellation(cancellationToken) || !CanRetry(attempt, e, cancellationToken))
                        throw;
                }
            } while (true);
        }

        /// <summary>
        /// Asynchronously invokes the encapsulated method despite transient errors.
        /// </summary>
        /// <remarks>
        /// If the encapsulated method succeeds without retrying, the method executes synchronously.
        /// </remarks>
        /// <param name="arg1">The first parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this reliable delegate encapsulates.</param>
        public async Task InvokeAsync(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            => await InvokeAsync(arg1, arg2, arg3, arg4, arg5, CancellationToken.None).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes the encapsulated method despite transient errors.
        /// </summary>
        /// <remarks>
        /// If the encapsulated method succeeds without retrying, the method executes synchronously.
        /// </remarks>
        /// <param name="arg1">The first parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="cancellationToken">
        /// A cancellation token to observe while waiting for the operation to complete.
        /// </param>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public async Task InvokeAsync(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, CancellationToken cancellationToken)
        {
            int attempt = 0;
            do
            {
                attempt++;
                try
                {
                    _action(arg1, arg2, arg3, arg4, arg5, cancellationToken);
                    return;
                }
                catch (Exception e)
                {
                    if (e.IsCancellation(cancellationToken) || !await CanRetryAsync(attempt, e, cancellationToken).ConfigureAwait(false))
                        throw;
                }
            } while (true);
        }

        /// <summary>
        /// Attempts to successfully invoke the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="arg1">The first parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this reliable delegate encapsulates.</param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            => TryInvoke(arg1, arg2, arg3, arg4, arg5, CancellationToken.None);

        /// <summary>
        /// Attempts to successfully invoke the encapsulated method despite transient errors.
        /// </summary>
        /// <param name="arg1">The first parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this reliable delegate encapsulates.</param>
        /// <param name="cancellationToken">
        /// A cancellation token to observe while waiting for the operation to complete.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the delegate completed without throwing an exception
        /// within the maximum number of retries; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ObjectDisposedException">
        /// The underlying <see cref="CancellationTokenSource" /> has already been disposed.
        /// </exception>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, CancellationToken cancellationToken)
        {
            int attempt = 0;
            Exception lastException;
            do
            {
                attempt++;
                try
                {
                    _action(arg1, arg2, arg3, arg4, arg5, cancellationToken);
                    return true;
                }
                catch (Exception e)
                {
                    if (e.IsCancellation(cancellationToken))
                        throw;

                    lastException = e;
                }
            } while (CanRetry(attempt, lastException, cancellationToken));

            return false;
        }
    }
}
