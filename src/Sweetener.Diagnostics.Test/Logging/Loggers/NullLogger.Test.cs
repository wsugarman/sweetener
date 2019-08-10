using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Diagnostics.Logging.Test
{
    [TestClass]
    public class NullLoggerTest
    {
        [TestMethod]
        public void FormatProvider()
        {
            using (Logger logger = new NullLogger())
                Assert.AreEqual(CultureInfo.InvariantCulture, logger.FormatProvider);
        }

        [TestMethod]
        public void IsThreadSafe()
        {
            using (Logger logger = new NullLogger())
                Assert.IsTrue(logger.IsThreadSafe);
        }

        [TestMethod]
        public void MinLevel()
        {
            using (Logger logger = new NullLogger())
                Assert.AreEqual(LogLevel.Trace, logger.MinLevel);
        }

        [TestMethod]
        public void Dispose()
        {
            Logger logger = new NullLogger();

            // Assert that we can call Dispose multiple times without issue
            logger.Dispose();
            logger.Log(LogLevel.Trace, "1");

            logger.Dispose();
            logger.Log(LogLevel.Debug, "1 {0}", 2);

            logger.Dispose();
            logger.Log(LogLevel.Info , "1 {0} {1}", 2, 3);

            logger.Dispose();
            logger.Log(LogLevel.Warn , "1 {0} {1} {2}", 2, 3, 4);

            logger.Dispose();
            logger.Log(LogLevel.Error, "1 {0} {1} {2} {3}", 2, 3, 4, 5);
        }

        [TestMethod]
        public void Log()
        {
            using (Logger logger = new NullLogger())
            {
                // Nothing to validate other than ensuring we can call the methods without issue
                logger.Log(LogLevel.Debug, "Hello"                                                   );
                logger.Log(LogLevel.Info , "Hello {0}"            , "World"                          );
                logger.Log(LogLevel.Fatal, "Hello {0} {1}"        , "World", "from"                  );
                logger.Log(LogLevel.Trace, "Hello {0} {1} {2}"    , "World", "from", "Null"          );
                logger.Log(LogLevel.Warn , "Hello {0} {1} {2} {3}", "World", "from", "Null", "Logger");
            }
        }
    }
}
