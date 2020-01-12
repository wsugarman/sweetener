namespace Sweetener.Reliability
{
    /// <summary>
    /// Contains common <see cref="ResultHandler{TResult}"/> implementations.
    /// </summary>
    public static class ResultPolicy
    {
        /// <summary>
        /// Returns a default <see cref="ResultHandler{TResult}"/> that always indicates success.
        /// </summary>
        /// <typeparam name="TResult">The type of the return value of the method.</typeparam>
        /// <returns>A default <see cref="ResultHandler{TResult}"/>.</returns>
        public static ResultHandler<TResult> Default<TResult>()
            => DefaultResultHandler<TResult>.Value;

        private static class DefaultResultHandler<TResult>
        {
            public static readonly ResultHandler<TResult> Value = r => ResultKind.Successful;
        }
    }
}
