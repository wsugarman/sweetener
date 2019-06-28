using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Logging.Test
{
    [TestClass]
    public class NullLoggerTest
    {
        [TestMethod]
        public void Constructor()
        {
            using (Logger logger = new NullLogger())
            {
                Assert.AreEqual(LogLevel.Trace            , logger.MinLevel      );
                Assert.AreEqual(CultureInfo.CurrentCulture, logger.FormatProvider);
            }
        }

        [TestMethod]
        public void IsSynchronized()
        {
            Assert.IsTrue(new NullLogger().IsSynchronized);
        }

        [TestMethod]
        public void Dispose()
        {
            Logger logger = new NullLogger();

            // Assert that we can call Dispose multiple times without issue
            logger.Info("Foo");
            logger.Dispose();
            logger.Warn("Bar");
            logger.Dispose();
            logger.Fatal("Baz");
        }

        [TestMethod]
        public void Log()
        {
            using (Logger logger = new NullLogger())
            {
                // Log can be called without issue for each level
                foreach (LogLevel level in Enum.GetValues(typeof(LogLevel)))
                    logger.Log(new LogEntry(DateTime.UtcNow, level, level.ToString("F")));
            }
        }
    }
}
