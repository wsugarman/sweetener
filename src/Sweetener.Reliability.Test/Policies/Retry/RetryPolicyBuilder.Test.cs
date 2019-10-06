using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    [TestClass]
    public class RetryPolicyBuilderTest
    {
        [TestMethod]
        public void FailExceptionsByDefault()
        {
            IRetryPolicy policy = Retry.NewPolicy().FailByDefault();

            // Test a number of exceptions
            // (We could try to enumerate all Exception types in the assembly... but that sounds excessive)
            Assert.IsFalse(policy.CanRetry(new ArgumentNullException()    ));
            Assert.IsFalse(policy.CanRetry(new DivideByZeroException()    ));
            Assert.IsFalse(policy.CanRetry(new Exception()                ));
            Assert.IsFalse(policy.CanRetry(new FormatException()          ));
            Assert.IsFalse(policy.CanRetry(new InvalidOperationException()));
            Assert.IsFalse(policy.CanRetry(new OutOfMemoryException()     ));
        }

        [TestMethod]
        public void RetryExceptionsByDefault()
        {
            IRetryPolicy policy = Retry.NewPolicy().RetryByDefault();

            // Test a number of exceptions
            // (We could try to enumerate all Exception types in the assembly... but again that sounds excessive)
            Assert.IsTrue(policy.CanRetry(new ArgumentNullException()    ));
            Assert.IsTrue(policy.CanRetry(new DivideByZeroException()    ));
            Assert.IsTrue(policy.CanRetry(new Exception()                ));
            Assert.IsTrue(policy.CanRetry(new FormatException()          ));
            Assert.IsTrue(policy.CanRetry(new InvalidOperationException()));
            Assert.IsTrue(policy.CanRetry(new OutOfMemoryException()     ));
        }

        [TestMethod]
        public void FailException()
        {
            IRetryPolicy policy = new RetryPolicyBuilder()
                .Fail<ArgumentNullException    >()
                .Fail<FormatException          >()
                .Fail<InvalidOperationException>()
                .Create()

            Assert.IsFalse(policy.CanRetry(new ArgumentNullException()    ));
            Assert.IsTrue (policy.CanRetry(new DivideByZeroException()    ));
            Assert.IsTrue (policy.CanRetry(new Exception()                ));
            Assert.IsFalse(policy.CanRetry(new FormatException()          ));
            Assert.IsFalse(policy.CanRetry(new InvalidOperationException()));
            Assert.IsTrue (policy.CanRetry(new OutOfMemoryException()     ));
        }

        [TestMethod]
        public void RetryException()
        {
            IRetryPolicy policy = Retry.NewPolicy()
                .Retry<DivideByZeroException>()
                .Retry<Exception            >()
                .Retry<OutOfMemoryException >()
                .FailByDefault();

            Assert.IsFalse(policy.CanRetry(new ArgumentNullException()    ));
            Assert.IsTrue (policy.CanRetry(new DivideByZeroException()    ));
            Assert.IsTrue (policy.CanRetry(new Exception()                ));
            Assert.IsFalse(policy.CanRetry(new FormatException()          ));
            Assert.IsFalse(policy.CanRetry(new InvalidOperationException()));
            Assert.IsTrue (policy.CanRetry(new OutOfMemoryException()     ));
        }

        [TestMethod]
        public void BuildComplexPolicy()
        {

        }
    }
}
