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
            foreach (ExceptionArgument arg in Enum.GetValues(typeof(ExceptionArgument)))
            {
                Assert.ThrowsException<ArgumentNullException>(() =>
                {
                    try
                    {
                        ThrowHelper.ThrowArgumentNullException(arg);
                    }
                    catch (ArgumentNullException ane)
                    {
                        Assert.AreEqual(arg.ToString(), ane.ParamName);
                        throw ane;
                    }
                });
            }
        }
    }
}
