// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;

namespace Sweetener
{
    /// <summary>
    /// Represents an <see cref="IClock"/> for retrieving the current date and time based on a computer.
    /// </summary>
    public sealed class Clock : IClock
    {
        /// <summary>
        /// Gets a clock based on this computer.
        /// </summary>
        /// <value>The system <see cref="Clock"/> for this computer.</value>
        public static Clock System { get; } = new Clock();

        /// <summary>
        /// Gets a <see cref="DateTime"/> object that is set to the current date and time on this computer,
        /// expressed as the local time.
        /// </summary>
        /// <value>An object whose value is the current local date and time.</value>
        public DateTime Now => DateTime.Now;

        /// <summary>
        /// Gets a <see cref="DateTime"/> object that is set to the current date and time on this computer,
        /// expressed as the Coordinated Universal Time (UTC).
        /// </summary>
        /// <value>An object whose value is the current UTC date and time.</value>
        public DateTime UtcNow => DateTime.UtcNow;

        private Clock()
        { }
    }
}
