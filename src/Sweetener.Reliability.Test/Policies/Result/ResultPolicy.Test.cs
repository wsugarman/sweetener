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

#nullable disable

            Assert.AreEqual(ResultKind.Successful, resultPolicy(null         ));
            Assert.AreEqual(ResultKind.Successful, resultPolicy(string.Empty ));
            Assert.AreEqual(ResultKind.Successful, resultPolicy("Hello World"));

#nullable enable

            // Only one instance
            Assert.AreSame(ResultPolicy.Default<int>(), ResultPolicy.Default<int>());
        }
    }
}
