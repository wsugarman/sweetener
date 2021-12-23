// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;

namespace Sweetener
{
    /// <summary>
    /// Represents the disposal of no unmanaged resources. This class cannot be inherited.
    /// </summary>
    public sealed class NullDisposable : IDisposable
    {
        /// <summary>
        /// Gets the shared instance of <see cref="NullDisposable"/>
        /// </summary>
        /// <value>The <see cref="NullDisposable"/> instance.</value>
        public static NullDisposable Instance { get; } = new NullDisposable();

        private NullDisposable()
        { }

        /// <inheritdoc/>
        public void Dispose()
        { }
    }
}
