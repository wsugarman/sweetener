// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Test
{
    [TestClass]
    public class NullDisposableTest
    {
        [TestMethod]
        public void Dispose()
        {
            // Dispose can be invoked multiple times without issue
            IDisposable instance = NullDisposable.Instance;

            instance.Dispose();
            instance.Dispose();
        }
    }
}
