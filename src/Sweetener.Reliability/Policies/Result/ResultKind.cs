namespace Sweetener.Reliability
{
    /// <summary>
    /// Specifies a classification for the result of a function.
    /// </summary>
    public enum ResultKind
    {
        /// <summary>
        /// The result of the function indicates failure, and subsequent invocations
        /// would also be unsuccessful.
        /// </summary>
        Fatal,

        /// <summary>
        /// The result of the function indicates failure, but subsequent invocations
        /// may be successful.
        /// </summary>
        Transient,

        /// <summary>
        /// The result of the function indicates success.
        /// </summary>
        Successful,
    }
}
