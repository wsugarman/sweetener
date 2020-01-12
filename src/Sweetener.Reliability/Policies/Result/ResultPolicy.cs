namespace Sweetener.Reliability
{
    /// <summary>
    /// Contains common <see cref="ResultHandler{T}"/> implementations.
    /// </summary>
    public static class ResultPolicy
    {
        /// <summary>
        /// Returns a default <see cref="ResultHandler{T}"/> that always indicates success.
        /// </summary>
        /// <typeparam name="T">The type of the return value of the method.</typeparam>
        /// <returns>A default <see cref="ResultHandler{T}"/>.</returns>
        public static ResultHandler<T> Default<T>()
            => DefaultResultHandler<T>.Value;

        private static class DefaultResultHandler<T>
        {
            public static readonly ResultHandler<T> Value = r => ResultKind.Successful;
        }
    }
}
