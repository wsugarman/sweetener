using System;

namespace Sweetener.Reliability
{
    /// <summary>
    /// Determines whether an operation can be retried given the <see cref="Exception" />.
    /// </summary>
    /// <param name="exception">The exception thrown by the operation.</param>
    /// <returns><see langword="true" /> if the operation can be retried; otherwise <see langword="false" />.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="exception"/> is <see langword="null"/>.</exception>
    public delegate bool ExceptionHandler(Exception exception);
}
