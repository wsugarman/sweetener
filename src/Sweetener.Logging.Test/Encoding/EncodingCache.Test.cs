using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Logging.Test
{
    [TestClass]
    public class EncodingCacheTest
    {
        [TestMethod]
        public void UTF8NoBOM()
        {
            Encoding encoding = EncodingCache.UTF8NoBOM;

            // No Byte Order Mark (BOM)
            Assert.AreEqual(0, encoding.Preamble.Length);

            // Code points U+D800 through U+DFFF and greater than U+10FFFF are invalid
            Assert.ThrowsException<EncoderFallbackException>(() => encoding.GetByteCount(new char[] { '\uD801' }));
        }
    }
}
