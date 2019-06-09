using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Logging.Test
{
    [TestClass]
    public class ThrowHelperTest
    {
        [TestMethod]
        public void ThrowUnknownArgumentNullException()
        {
            // Our "Throwing" method shouldn't throw different exceptions for unknown values
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                try
                {
                    ThrowHelper.ThrowArgumentNullException((ExceptionArgument)12345);
                }
                catch (ArgumentNullException ane)
                {
                    Assert.AreEqual("12345", ane.ParamName);
                    throw ane;
                }
            });
        }

        [TestMethod]
        public void ThrowArgumentNullException()
        {
            // args
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                try
                {
                    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.args);
                }
                catch (ArgumentNullException ane)
                {
                    Assert.AreEqual(ExceptionArgument.args.ToString(), ane.ParamName);
                    throw ane;
                }
            });

            // format
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                try
                {
                    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);
                }
                catch (ArgumentNullException ane)
                {
                    Assert.AreEqual(ExceptionArgument.format.ToString(), ane.ParamName);
                    throw ane;
                }
            });
        }
    }
}
