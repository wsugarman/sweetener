using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Logging.Test
{
    [TestClass]
    public class ContextualNullLoggerTest
    {
        [TestMethod]
        public void Constructor()
        {
            using (Logger<int> logger = new NullLogger<int>())
            {
                Assert.AreEqual(LogLevel.Trace            , logger.MinLevel      );
                Assert.AreEqual(CultureInfo.CurrentCulture, logger.FormatProvider);
            }
        }

        [TestMethod]
        public void IsSynchronized()
        {
            Assert.IsTrue(new NullLogger<DateTime>().IsSynchronized);
        }

        [TestMethod]
        public void Dispose()
        {
            Logger<char> logger = new NullLogger<char>();

            // Assert that we can call Dispose multiple times without issue
            logger.Info('1', "Foo");
            logger.Dispose();
            logger.Warn('2', "Bar");
            logger.Dispose();
            logger.Fatal('3', "Baz");
        }

        [TestMethod]
        public void Log()
        {
            using (Logger<long> logger = new NullLogger<long>())
            {
                // Log can be called without issue for each level
                foreach (LogLevel level in Enum.GetValues(typeof(LogLevel)))
                    logger.Log(new LogEntry<long>(DateTime.UtcNow, level, (long)level, level.ToString("F")));
            }
        }
    }
}
