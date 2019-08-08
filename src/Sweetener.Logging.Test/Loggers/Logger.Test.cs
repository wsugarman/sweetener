using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Logging.Test
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public void IsThreadSafe()
        {
            using (Logger logger = new MemoryLogger(default, CultureInfo.InvariantCulture))
                Assert.IsFalse(logger.IsThreadSafe);
        }

        [TestMethod]
        public void Null()
        {
            using (Logger logger = Logger.Null)
            {
                Assert.IsNotNull(logger);
                Assert.AreEqual(typeof(NullLogger), logger.GetType());
            }
        }

        [TestMethod]
        public void LogExceptions()
        {
            using (Logger logger = new MemoryLogger(default, CultureInfo.InvariantCulture))
            {
                object[] args = null;

                // ArgumentNullException
                Assert.ThrowsException<ArgumentNullException>(() => logger.Log(LogLevel.Trace, null, 1         ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Log(LogLevel.Debug, null, 1, 2      ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Log(LogLevel.Info , null, 1, 2, 3   ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Log(LogLevel.Warn , null, 1, 2, 3, 4));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Log(LogLevel.Fatal, "{0}", args     ));

                // ArgumentOutOfRangeException
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => logger.Log((LogLevel)101, "1"                             ));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => logger.Log((LogLevel)102, "1 {0}"            , 2          ));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => logger.Log((LogLevel)103, "1 {0} {1}"        , 2, 3       ));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => logger.Log((LogLevel)104, "1 {0} {1} {2}"    , 2, 3, 4    ));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => logger.Log((LogLevel)105, "1 {0} {1} {2} {3}", 2, 3, 4, 5 ));

                // FormatException
                Assert.ThrowsException<FormatException>(() => logger.Log(LogLevel.Fatal, "{0:Y}", 1         ));
                Assert.ThrowsException<FormatException>(() => logger.Log(LogLevel.Error, "{0:Y}", 1, 2      ));
                Assert.ThrowsException<FormatException>(() => logger.Log(LogLevel.Warn , "{0:Y}", 1, 2, 3   ));
                Assert.ThrowsException<FormatException>(() => logger.Log(LogLevel.Info , "{0:Y}", 1, 2, 3, 4));
            }
        }

        [TestMethod]
        public void LogIgnoreBelowMinLevel()
        {
            LogLevel[] levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
            for (int i = 0; i < levels.Length; i++)
            {
                using (MemoryLogger logger = new MemoryLogger(levels[i], CultureInfo.InvariantCulture))
                {
                    LogLevel maxLevel = levels[levels.Length - 1];
                    for (int j = 0; j < levels.Length; j++)
                    {
                        logger.Log(levels[j], "1"                            );
                        logger.Log(levels[j], "1 {0}"            , 2         );
                        logger.Log(levels[j], "1 {0} {1}"        , 2, 3      );
                        logger.Log(levels[j], "1 {0} {1} {2}"    , 2, 3, 4   );
                        logger.Log(levels[j], "1 {0} {1} {2} {3}", 2, 3, 4, 5);
                    }

                    // As the minimum level increases, it reduces the number of fulfilled log requests
                    Assert.AreEqual((levels.Length - i) * 5, logger.Entries.Count);
                    for (int j = i; j < levels.Length; j++)
                    {
                        Assert.That.AreEqual(new LogEntry(levels[j], "1"        ), logger.Entries.Dequeue(), LogEntryEqualityComparer.NoTimestamp);
                        Assert.That.AreEqual(new LogEntry(levels[j], "1 2"      ), logger.Entries.Dequeue(), LogEntryEqualityComparer.NoTimestamp);
                        Assert.That.AreEqual(new LogEntry(levels[j], "1 2 3"    ), logger.Entries.Dequeue(), LogEntryEqualityComparer.NoTimestamp);
                        Assert.That.AreEqual(new LogEntry(levels[j], "1 2 3 4"  ), logger.Entries.Dequeue(), LogEntryEqualityComparer.NoTimestamp);
                        Assert.That.AreEqual(new LogEntry(levels[j], "1 2 3 4 5"), logger.Entries.Dequeue(), LogEntryEqualityComparer.NoTimestamp);
                    }
                }
            }
        }

        [TestMethod]
        public void Log()
        {
            using (MemoryLogger logger = new MemoryLogger(default, CultureInfo.InvariantCulture))
            {
                logger.Log(LogLevel.Trace, "Trace");
                logger.Log(LogLevel.Debug, "Debug");
                logger.Log(LogLevel.Info , "Info" );
                logger.Log(LogLevel.Warn , "Warn" );
                logger.Log(LogLevel.Error, "Error");
                logger.Log(LogLevel.Fatal, "Fatal");

                Assert.AreEqual(6, logger.Entries.Count);
                LogEntry[] expectedEntries = new LogEntry[]
                {
                    new LogEntry(LogLevel.Trace, "Trace"),
                    new LogEntry(LogLevel.Debug, "Debug"),
                    new LogEntry(LogLevel.Info , "Info" ),
                    new LogEntry(LogLevel.Warn , "Warn" ),
                    new LogEntry(LogLevel.Error, "Error"),
                    new LogEntry(LogLevel.Fatal, "Fatal"),
                };

                for (int i = 0; i < logger.Entries.Count; i++)
                    Assert.That.AreEqual(expectedEntries[i], logger.Entries.Dequeue(), LogEntryEqualityComparer.NoTimestamp);
            }
        }

        [TestMethod]
        public void LogFormatArg0()
        {
            CultureInfo jaJP = CultureInfo.GetCultureInfo("ja-JP");
            using (MemoryLogger logger = new MemoryLogger(default, jaJP))
            {
                string msg = "{0,8:C}";

                logger.Log(LogLevel.Trace, msg,   4321);
                logger.Log(LogLevel.Debug, msg,  11235);
                logger.Log(LogLevel.Info , msg,  81321);
                logger.Log(LogLevel.Warn , msg,   3455);
                logger.Log(LogLevel.Error, msg,  89144);
                logger.Log(LogLevel.Fatal, msg, 233377);

                Assert.AreEqual(6, logger.Entries.Count);
                LogEntry[] expectedEntries = new LogEntry[]
                {
                    new LogEntry(LogLevel.Trace, string.Format(jaJP, msg,   4321)),
                    new LogEntry(LogLevel.Debug, string.Format(jaJP, msg,  11235)),
                    new LogEntry(LogLevel.Info , string.Format(jaJP, msg,  81321)),
                    new LogEntry(LogLevel.Warn , string.Format(jaJP, msg,   3455)),
                    new LogEntry(LogLevel.Error, string.Format(jaJP, msg,  89144)),
                    new LogEntry(LogLevel.Fatal, string.Format(jaJP, msg, 233377)),
                };

                for (int i = 0; i < logger.Entries.Count; i++)
                    Assert.That.AreEqual(expectedEntries[i], logger.Entries.Dequeue(), LogEntryEqualityComparer.NoTimestamp);
            }
        }

        [TestMethod]
        public void LogFormatArg0Arg1()
        {
            CultureInfo jaJP = CultureInfo.GetCultureInfo("ja-JP");
            using (MemoryLogger logger = new MemoryLogger(default, jaJP))
            {
                string msg = "On {0:d}, made {1,11:C}";

                DateTime dt = new DateTime(2019, 01, 15);
                logger.Log(LogLevel.Trace, msg, dt.AddDays(0),      823);
                logger.Log(LogLevel.Debug, msg, dt.AddDays(1),     1234);
                logger.Log(LogLevel.Info , msg, dt.AddDays(2),     5678);
                logger.Log(LogLevel.Warn , msg, dt.AddDays(3),    91011);
                logger.Log(LogLevel.Error, msg, dt.AddDays(4),   121314);
                logger.Log(LogLevel.Fatal, msg, dt.AddDays(5), 15161718);

                Assert.AreEqual(6, logger.Entries.Count);
                LogEntry[] expectedEntries = new LogEntry[]
                {
                    new LogEntry(LogLevel.Trace, string.Format(jaJP, msg, dt.AddDays(0),      823)),
                    new LogEntry(LogLevel.Debug, string.Format(jaJP, msg, dt.AddDays(1),     1234)),
                    new LogEntry(LogLevel.Info , string.Format(jaJP, msg, dt.AddDays(2),     5678)),
                    new LogEntry(LogLevel.Warn , string.Format(jaJP, msg, dt.AddDays(3),    91011)),
                    new LogEntry(LogLevel.Error, string.Format(jaJP, msg, dt.AddDays(4),   121314)),
                    new LogEntry(LogLevel.Fatal, string.Format(jaJP, msg, dt.AddDays(5), 15161718)),
                };

                for (int i = 0; i < logger.Entries.Count; i++)
                    Assert.That.AreEqual(expectedEntries[i], logger.Entries.Dequeue(), LogEntryEqualityComparer.NoTimestamp);
            }
        }

        [TestMethod]
        public void LogFormatArg0Arg1Arg2()
        {
            CultureInfo esES = CultureInfo.GetCultureInfo("es-ES");
            using (MemoryLogger logger = new MemoryLogger(default, esES))
            {
                string boughtMsg = "On {0:d}, bought {1,7:F2} units for {2,11:C}";
                string soldMsg   = "On {0:d}, sold   {1,7:F2} units for {2,11:C}";

                DateTime dt = new DateTime(2019, 01, 15);
                logger.Log(LogLevel.Trace, boughtMsg, dt.AddDays(0),    0.42,    13.37);
                logger.Log(LogLevel.Debug, boughtMsg, dt.AddDays(1),   24.68,    -0.25);
                logger.Log(LogLevel.Info , soldMsg  , dt.AddDays(2),   10.12,     1.33);
                logger.Log(LogLevel.Warn , soldMsg  , dt.AddDays(3), 1416.00, 11975.31);
                logger.Log(LogLevel.Error, boughtMsg, dt.AddDays(4),   18.20,   -10.00);
                logger.Log(LogLevel.Fatal, soldMsg  , dt.AddDays(5), 2224.00,   115.00);

                Assert.AreEqual(6, logger.Entries.Count);
                LogEntry[] expectedEntries = new LogEntry[]
                {
                    new LogEntry(LogLevel.Trace, string.Format(esES, boughtMsg, dt.AddDays(0),    0.42,    13.37)),
                    new LogEntry(LogLevel.Debug, string.Format(esES, boughtMsg, dt.AddDays(1),   24.68,    -0.25)),
                    new LogEntry(LogLevel.Info , string.Format(esES, soldMsg  , dt.AddDays(2),   10.12,     1.33)),
                    new LogEntry(LogLevel.Warn , string.Format(esES, soldMsg  , dt.AddDays(3), 1416.00, 11975.31)),
                    new LogEntry(LogLevel.Error, string.Format(esES, boughtMsg, dt.AddDays(4),   18.20,   -10.00)),
                    new LogEntry(LogLevel.Fatal, string.Format(esES, soldMsg  , dt.AddDays(5), 2224.00,   115.00)),
                };

                for (int i = 0; i < logger.Entries.Count; i++)
                    Assert.That.AreEqual(expectedEntries[i], logger.Entries.Dequeue(), LogEntryEqualityComparer.NoTimestamp);
            }
        }

        [TestMethod]
        public void LogFormatParams()
        {
            CultureInfo esES = CultureInfo.GetCultureInfo("es-ES");
            using (MemoryLogger logger = new MemoryLogger(default, esES))
            {
                string boughtMsg = "On {0:d}, bought {1,7:F2} units for {2,11:C} (Total = {3,15:C})";
                string soldMsg   = "On {0:d}, sold   {1,7:F2} units for {2,11:C} (Total = {3,15:C})";

                DateTime dt = new DateTime(2019, 01, 15);
                logger.Log(LogLevel.Trace, boughtMsg, dt.AddDays(0),    0.42,    13.37,        5.6154);
                logger.Log(LogLevel.Debug, boughtMsg, dt.AddDays(1),   24.68,    -0.25,       -6.1700);
                logger.Log(LogLevel.Info , soldMsg  , dt.AddDays(2),   10.12,     1.33,       13.4596);
                logger.Log(LogLevel.Warn , soldMsg  , dt.AddDays(3), 1416.00, 11975.31, 16957039.0000);
                logger.Log(LogLevel.Error, boughtMsg, dt.AddDays(4),   18.20,   -10.00,     -182.0000);
                logger.Log(LogLevel.Fatal, soldMsg  , dt.AddDays(5), 2224.00,   115.00,   255760.0000);

                Assert.AreEqual(6, logger.Entries.Count);
                LogEntry[] expectedEntries = new LogEntry[]
                {
                    new LogEntry(LogLevel.Trace, string.Format(esES, boughtMsg, dt.AddDays(0),    0.42,    13.37,        5.6154)),
                    new LogEntry(LogLevel.Debug, string.Format(esES, boughtMsg, dt.AddDays(1),   24.68,    -0.25,       -6.1700)),
                    new LogEntry(LogLevel.Info , string.Format(esES, soldMsg  , dt.AddDays(2),   10.12,     1.33,       13.4596)),
                    new LogEntry(LogLevel.Warn , string.Format(esES, soldMsg  , dt.AddDays(3), 1416.00, 11975.31, 16957039.0000)),
                    new LogEntry(LogLevel.Error, string.Format(esES, boughtMsg, dt.AddDays(4),   18.20,   -10.00,     -182.0000)),
                    new LogEntry(LogLevel.Fatal, string.Format(esES, soldMsg  , dt.AddDays(5), 2224.00,   115.00,   255760.0000)),
                };

                for (int i = 0; i < logger.Entries.Count; i++)
                    Assert.That.AreEqual(expectedEntries[i], logger.Entries.Dequeue(), LogEntryEqualityComparer.NoTimestamp);
            }
        }
    }
}
