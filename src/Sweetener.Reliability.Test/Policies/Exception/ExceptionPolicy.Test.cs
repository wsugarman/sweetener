using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    [TestClass]
    public sealed partial class ExceptionPolicyTest
    {
        [TestMethod]
        public void Fatal()
            => UniformExceptionPolicy(ExceptionPolicy.Fatal, expected: false);

        [TestMethod]
        public void Transient()
            => UniformExceptionPolicy(ExceptionPolicy.Transient, expected: true);

        private void UniformExceptionPolicy(ExceptionHandler isTransient, bool expected)
        {
#nullable disable

            Assert.ThrowsException<ArgumentNullException>(() => isTransient(null));

#nullable enable

            Assert.AreEqual(expected, isTransient(new ArgumentNullException     ()));
            Assert.AreEqual(expected, isTransient(new FormatException           ()));
            Assert.AreEqual(expected, isTransient(new InvalidCastException      ()));
            Assert.AreEqual(expected, isTransient(new OperationCanceledException()));
        }
    }
}
