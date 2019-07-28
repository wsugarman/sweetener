﻿using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Logging.Test
{
    [TestClass]
    public class ContextualNullLoggerTest
    {
        [TestMethod]
        public void FormatProvider()
        {
            using (Logger<Guid> logger = new NullLogger<Guid>())
                Assert.AreEqual(CultureInfo.InvariantCulture, logger.FormatProvider);
        }

        [TestMethod]
        public void IsSynchronized()
        {
            using (Logger<DateTime> logger = new NullLogger<DateTime>())
                Assert.IsTrue(logger.IsSynchronized);
        }

        [TestMethod]
        public void MinLevel()
        {
            using (Logger<decimal> logger = new NullLogger<decimal>())
                Assert.AreEqual(LogLevel.Trace, logger.MinLevel);
        }

        [TestMethod]
        public void SyncRoot()
        {
            using (Logger<short> logger = new NullLogger<short>())
                Assert.AreEqual(logger, logger.SyncRoot);
        }

        [TestMethod]
        public void Dispose()
        {
            Logger<char> logger = new NullLogger<char>();

            // Assert that we can call Dispose multiple times without issue
            logger.Dispose();
            logger.Log(LogLevel.Trace, '0', "1");

            logger.Dispose();
            logger.Log(LogLevel.Debug, '0', "1 {0}", 2);

            logger.Dispose();
            logger.Log(LogLevel.Info , '0', "1 {0} {1}", 2, 3);

            logger.Dispose();
            logger.Log(LogLevel.Warn , '0', "1 {0} {1} {2}", 2, 3, 4);

            logger.Dispose();
            logger.Log(LogLevel.Error, '0', "1 {0} {1} {2} {3}", 2, 3, 4, 5);
        }

        [TestMethod]
        public void Log()
        {
            using (Logger<string> logger = new NullLogger<string>())
            {
                // Nothing to validate other than ensuring we can call the methods without issue
                logger.Log(LogLevel.Debug, "Why", "Hello"                                                   );
                logger.Log(LogLevel.Info , "Why", "Hello {0}"            , "World"                          );
                logger.Log(LogLevel.Fatal, "Why", "Hello {0} {1}"        , "World", "from"                  );
                logger.Log(LogLevel.Trace, "Why", "Hello {0} {1} {2}"    , "World", "from", "Null"          );
                logger.Log(LogLevel.Warn , "Why", "Hello {0} {1} {2} {3}", "World", "from", "Null", "Logger");
            }
        }
    }
}
