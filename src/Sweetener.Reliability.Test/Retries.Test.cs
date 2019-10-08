using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    [TestClass]
    public sealed class RetryTest
    {
        [TestMethod]
        public void Infinite()
            => Assert.AreEqual(-1, Retries.Infinite); // Some methods may rely on the specific value for their behavior
    }
}
