using System;

namespace Sweetener.Reliability
{
    /// <summary>
    /// Contains common policies for determining whether a given <see cref="Exception" /> is transient.
    /// </summary>
    public static partial class ExceptionPolicies
    {
        /// <summary>
        /// An <see cref="ExceptionPolicy" /> that treats any <see cref="Exception" /> as fatal.
        /// </summary>
        public static readonly ExceptionPolicy Fatal = CreateUniformPolicy(isTransient: false);

        /// <summary>
        /// An <see cref="ExceptionPolicy" /> that treats any <see cref="Exception" /> as transient.
        /// </summary>
        public static readonly ExceptionPolicy Transient = CreateUniformPolicy(isTransient: true);

        private static ExceptionPolicy CreateUniformPolicy(bool isTransient)
            => (exception) =>
            {
                if (exception == null)
                    throw new ArgumentNullException(nameof(exception));

                return isTransient;
            };
    }
}
