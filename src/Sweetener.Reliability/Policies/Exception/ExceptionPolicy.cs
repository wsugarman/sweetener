using System;

namespace Sweetener.Reliability
{
    /// <summary>
    /// Contains common <see cref="ExceptionHandler"/> implementations.
    /// </summary>
    public static partial class ExceptionPolicy
    {
        /// <summary>
        /// An <see cref="ExceptionHandler" /> that treats any <see cref="Exception" /> as fatal.
        /// </summary>
        public static readonly ExceptionHandler Fatal = CreateUniformPolicy(isTransient: false);

        /// <summary>
        /// An <see cref="ExceptionHandler" /> that treats any <see cref="Exception" /> as transient.
        /// </summary>
        public static readonly ExceptionHandler Transient = CreateUniformPolicy(isTransient: true);

        private static ExceptionHandler CreateUniformPolicy(bool isTransient)
            => (exception) =>
            {
                if (exception == null)
                    throw new ArgumentNullException(nameof(exception));

                return isTransient;
            };
    }
}
