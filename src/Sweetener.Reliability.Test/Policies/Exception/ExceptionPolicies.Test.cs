using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    [TestClass]
    public partial class ExceptionPoliciesTest
    {
        [TestMethod]
        public void Fatal()
            => UniformExceptionPolicy(ExceptionPolicies.Fatal, expected: false);

        [TestMethod]
        public void Transient()
            => UniformExceptionPolicy(ExceptionPolicies.Transient, expected: true);

        private void UniformExceptionPolicy(ExceptionPolicy isTransient, bool expected)
        {
            Assert.ThrowsException<ArgumentNullException>(() => isTransient(null));

            Assert.AreEqual(expected, isTransient(new ArgumentNullException     ()));
            Assert.AreEqual(expected, isTransient(new FormatException           ()));
            Assert.AreEqual(expected, isTransient(new InvalidCastException      ()));
            Assert.AreEqual(expected, isTransient(new OperationCanceledException()));
        }
    }
}
