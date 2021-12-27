// Copyright © William Sugarman.
// Licensed under the MIT License.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Test
{
    [TestClass]
    public class AsyncActionTest
    {
        [TestMethod]
        public void Defined()
        {
            Assert.IsNotNull(typeof(AsyncAction));
            Assert.IsNotNull(typeof(AsyncAction<>));
            Assert.IsNotNull(typeof(AsyncAction<,>));
            Assert.IsNotNull(typeof(AsyncAction<,,>));
            Assert.IsNotNull(typeof(AsyncAction<,,,>));
            Assert.IsNotNull(typeof(AsyncAction<,,,,>));
            Assert.IsNotNull(typeof(AsyncAction<,,,,,>));
            Assert.IsNotNull(typeof(AsyncAction<,,,,,,>));
            Assert.IsNotNull(typeof(AsyncAction<,,,,,,,>));
            Assert.IsNotNull(typeof(AsyncAction<,,,,,,,,>));
            Assert.IsNotNull(typeof(AsyncAction<,,,,,,,,,>));
            Assert.IsNotNull(typeof(AsyncAction<,,,,,,,,,,>));
            Assert.IsNotNull(typeof(AsyncAction<,,,,,,,,,,,>));
            Assert.IsNotNull(typeof(AsyncAction<,,,,,,,,,,,,>));
            Assert.IsNotNull(typeof(AsyncAction<,,,,,,,,,,,,,>));
            Assert.IsNotNull(typeof(AsyncAction<,,,,,,,,,,,,,,>));
            Assert.IsNotNull(typeof(AsyncAction<,,,,,,,,,,,,,,,>));
        }
    }
}
