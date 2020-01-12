using System;

namespace Sweetener.Reliability
{
    /// <summary>
    /// Gets the delay that an operation should wait before attempting to execute again.
    /// </summary>
    /// <param name="attempt">The number of the next attempt.</param>
    /// <returns>The <see cref="TimeSpan"/> that represents the delay in milliseconds.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="attempt"/> is less than <c>1</c>.</exception>
    public delegate TimeSpan DelayHandler(int attempt);

    /// <summary>
    /// Gets the delay that an operation should wait before attempting to execute again.
    /// </summary>
    /// <param name="attempt">The number of the next attempt.</param>
    /// <param name="exception">The transient exception that caused the operation to retry.</param>
    /// <returns>The <see cref="TimeSpan"/> that represents the delay in milliseconds.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="attempt"/> is less than <c>1</c>.</exception>
    public delegate TimeSpan ComplexDelayHandler(int attempt, Exception exception);

    /// <summary>
    /// Gets the delay that an operation should wait before attempting to execute again.
    /// </summary>
    /// <param name="attempt">The number of the next attempt.</param>
    /// <param name="result">The invalid result that caused the operation to retry; otherwise <see langword="default"/>.</param>
    /// <param name="exception">The transient exception that caused the operation to retry; otherwise <see langword="null"/></param>
    /// <returns>The <see cref="TimeSpan"/> that represents the delay in milliseconds.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="attempt"/> is less than <c>1</c>.</exception>
    public delegate TimeSpan ComplexDelayHandler<in T>(int attempt, T result, Exception exception);
}
