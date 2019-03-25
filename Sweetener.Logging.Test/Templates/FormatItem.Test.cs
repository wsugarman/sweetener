using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Logging.Test
{
    [TestClass]
    public class FormatItemTest
    {
        [TestMethod]
        public void Constructor()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new FormatItem(-1, null, null));

            FormatItem actual;

            // No Alignment or Format
            actual = new FormatItem(1, null, null);
            Assert.AreEqual(1   , actual.Index    );
            Assert.AreEqual(null, actual.Alignment);
            Assert.AreEqual(null, actual.Format   );

            // Only Alignment
            actual = new FormatItem(2, -7, null);
            Assert.AreEqual(2   , actual.Index    );
            Assert.AreEqual(-7  , actual.Alignment);
            Assert.AreEqual(null, actual.Format   );

            // Only Format
            actual = new FormatItem(3, null, "yyyyMMdd");
            Assert.AreEqual(3         , actual.Index    );
            Assert.AreEqual(null      , actual.Alignment);
            Assert.AreEqual("yyyyMMdd", actual.Format   );

            // Both Alignment and Format
            actual = new FormatItem(4, 6, "C");
            Assert.AreEqual(4  , actual.Index    );
            Assert.AreEqual(6  , actual.Alignment);
            Assert.AreEqual("C", actual.Format   );
        }

        [TestMethod]
        public void ToStringOverride()
        {
            // No Alignment or Format
            Assert.AreEqual("{1}", new FormatItem(1, null, null).ToString());

            // Only Alignment
            Assert.AreEqual("{2,-7}", new FormatItem(2, -7, null).ToString());

            // Only Format
            Assert.AreEqual("{3:yyyyMMdd}", new FormatItem(3, null, "yyyyMMdd").ToString());

            // Both Alignment and Format
            Assert.AreEqual("{4,5:C}", new FormatItem(4, 5, "C").ToString());
        }

        [TestMethod]
        public void ToStringOverload()
        {
            // No Alignment or Format
            Assert.AreEqual("{5}", new FormatItem(1, null, null).ToString(5));

            // Only Alignment
            Assert.AreEqual("{6,-7}", new FormatItem(2, -7, null).ToString(6));

            // Only Format
            Assert.AreEqual("{7:yyyyMMdd}", new FormatItem(3, null, "yyyyMMdd").ToString(7));

            // Both Alignment and Format
            Assert.AreEqual("{8,5:C}", new FormatItem(4, 5, "C").ToString(8));
        }
    }
}
