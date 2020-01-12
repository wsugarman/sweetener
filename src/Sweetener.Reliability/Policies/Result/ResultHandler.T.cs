namespace Sweetener.Reliability
{
    /// <summary>
    /// Determines whether the <paramref name="result" /> indicates success, and if not
    /// whether the operation could produce a successful result if it was retried.
    /// </summary>
    /// <remarks>
    /// Any unknown <see cref="ResultKind"/> returned by <see cref="ResultHandler{TResult}" />
    /// is treated the same as <see cref="ResultKind.Fatal"/>.
    /// </remarks>
    /// <typeparam name="TResult">The type of the return value of the method.</typeparam>
    /// <param name="result">The result of an operation.</param>
    /// <returns>
    /// A value indicating the validity of the result and whether the operation
    /// should be retried if invalid.
    /// </returns>
    public delegate ResultKind ResultHandler<in TResult>(TResult result);
}
