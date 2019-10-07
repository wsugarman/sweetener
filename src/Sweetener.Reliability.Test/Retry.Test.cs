using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    [TestClass]
    public class RetryTest
    {
        [TestMethod]
        public void Infinite()
            => Assert.AreEqual(-1, Retry.Infinite); // Some methods may rely on the specific value for their behavior
    }
}
