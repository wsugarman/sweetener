using System;

namespace Sweetener.Reliability
{
    /// <summary>
    /// Gets the delay that an operation should wait before attempting to execute again.
    /// </summary>
    /// <param name="attempt">The number of the next attempt.</param>
    /// <returns>The <see cref="TimeSpan"/> that represents the delay in milliseconds.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="attempt"/> is less than <c>1</c>.</exception>
    /// <exception cref="OverflowException">Delay exceeded <see cref="int.MaxValue"/> milliseconds.</exception>
    public delegate TimeSpan DelayHandler(int attempt);

    /// <summary>
    /// Gets the delay that an operation should wait before attempting to execute again.
    /// </summary>
    /// <param name="attempt">The number of the next attempt.</param>
    /// <param name="exception">The transient exception that caused the operation to retry.</param>
    /// <returns>The <see cref="TimeSpan"/> that represents the delay in milliseconds.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="attempt"/> is less than <c>1</c>.</exception>
    /// <exception cref="OverflowException">Delay exceeded <see cref="int.MaxValue"/> milliseconds.</exception>
    public delegate TimeSpan ComplexDelayHandler(int attempt, Exception exception);

    /// <summary>
    /// Gets the delay that an operation should wait before attempting to execute again.
    /// </summary>
    /// <typeparam name="TResult">The type of the return value of the method.</typeparam>
    /// <param name="attempt">The number of the next attempt.</param>
    /// <param name="result">The invalid result that caused the operation to retry; otherwise the default value.</param>
    /// <param name="exception">The transient exception that caused the operation to retry; otherwise <see langword="null" /></param>
    /// <returns>The <see cref="TimeSpan"/> that represents the delay in milliseconds.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="attempt"/> is less than <c>1</c>.</exception>
    /// <exception cref="OverflowException">Delay exceeded <see cref="int.MaxValue"/> milliseconds.</exception>
    public delegate TimeSpan ComplexDelayHandler<in TResult>(int attempt, TResult result, Exception exception);
}
