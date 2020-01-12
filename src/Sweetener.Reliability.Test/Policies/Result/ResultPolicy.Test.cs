using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    [TestClass]
    public class ResultPolicyTest
    {
        [TestMethod]
        public void Default()
        {
            ResultHandler<string> resultPolicy = ResultPolicy.Default<string>();
            Assert.AreEqual(ResultKind.Successful, resultPolicy(null         ));
            Assert.AreEqual(ResultKind.Successful, resultPolicy(string.Empty ));
            Assert.AreEqual(ResultKind.Successful, resultPolicy("Hello World"));

            // Only one instance
            Assert.AreSame(ResultPolicy.Default<int>(), ResultPolicy.Default<int>());
        }
    }
}
