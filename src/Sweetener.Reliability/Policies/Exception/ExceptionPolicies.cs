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
        public static readonly ExceptionPolicy Fatal = _ => false;

        /// <summary>
        /// An <see cref="ExceptionPolicy" /> that treats any <see cref="Exception" /> as transient.
        /// </summary>
        public static readonly ExceptionPolicy Transient = _ => true;
    }
}
