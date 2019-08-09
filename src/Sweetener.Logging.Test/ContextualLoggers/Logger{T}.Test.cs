using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Diagnostics.Logging.Test
{
    [TestClass]
    public class ContextualLoggerTest
    {
        [TestMethod]
        public void IsThreadSafe()
        {
            using (Logger<char> logger = new MemoryLogger<char>(default, CultureInfo.InvariantCulture))
                Assert.IsFalse(logger.IsThreadSafe);
        }

        [TestMethod]
        public void Null()
        {
            using (Logger<Guid> logger = Logger<Guid>.Null)
            {
                Assert.IsNotNull(logger);
                Assert.AreEqual(typeof(NullLogger<Guid>), logger.GetType());
            }
        }

        [TestMethod]
        public void LogExceptions()
        {
            using (Logger<int> logger = new MemoryLogger<int>(default, CultureInfo.InvariantCulture))
            {
                object[] args = null;

                // ArgumentNullException
                Assert.ThrowsException<ArgumentNullException>(() => logger.Log(LogLevel.Trace, 0, null, 1         ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Log(LogLevel.Debug, 0, null, 1, 2      ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Log(LogLevel.Info , 0, null, 1, 2, 3   ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Log(LogLevel.Warn , 0, null, 1, 2, 3, 4));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Log(LogLevel.Fatal, 0, "{0}", args     ));

                // ArgumentOutOfRangeException
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => logger.Log((LogLevel)101, 0, "1"                             ));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => logger.Log((LogLevel)102, 0, "1 {0}"            , 2          ));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => logger.Log((LogLevel)103, 0, "1 {0} {1}"        , 2, 3       ));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => logger.Log((LogLevel)104, 0, "1 {0} {1} {2}"    , 2, 3, 4    ));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => logger.Log((LogLevel)105, 0, "1 {0} {1} {2} {3}", 2, 3, 4, 5 ));

                // FormatException
                Assert.ThrowsException<FormatException>(() => logger.Log(LogLevel.Fatal, 0, "{0:Y}", 1         ));
                Assert.ThrowsException<FormatException>(() => logger.Log(LogLevel.Error, 0, "{0:Y}", 1, 2      ));
                Assert.ThrowsException<FormatException>(() => logger.Log(LogLevel.Warn , 0, "{0:Y}", 1, 2, 3   ));
                Assert.ThrowsException<FormatException>(() => logger.Log(LogLevel.Info , 0, "{0:Y}", 1, 2, 3, 4));
            }
        }

        [TestMethod]
        public void LogIgnoreBelowMinLevel()
        {
            LogLevel[] levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
            for (int i = 0; i < levels.Length; i++)
            {
                using (MemoryLogger<long> logger = new MemoryLogger<long>(levels[i], CultureInfo.InvariantCulture))
                {
                    LogLevel maxLevel = levels[levels.Length - 1];
                    for (int j = 0; j < levels.Length; j++)
                    {
                        logger.Log(levels[j], 0, "1"                            );
                        logger.Log(levels[j], 0, "1 {0}"            , 2         );
                        logger.Log(levels[j], 0, "1 {0} {1}"        , 2, 3      );
                        logger.Log(levels[j], 0, "1 {0} {1} {2}"    , 2, 3, 4   );
                        logger.Log(levels[j], 0, "1 {0} {1} {2} {3}", 2, 3, 4, 5);
                    }

                    // As the minimum level increases, it reduces the number of fulfilled log requests
                    Assert.AreEqual((levels.Length - i) * 5, logger.Entries.Count);
                    for (int j = i; j < levels.Length; j++)
                    {
                        Assert.That.AreEqual(new LogEntry<long>(levels[j], 0, "1"        ), logger.Entries.Dequeue(), LogEntryEqualityComparer<long>.NoTimestamp);
                        Assert.That.AreEqual(new LogEntry<long>(levels[j], 0, "1 2"      ), logger.Entries.Dequeue(), LogEntryEqualityComparer<long>.NoTimestamp);
                        Assert.That.AreEqual(new LogEntry<long>(levels[j], 0, "1 2 3"    ), logger.Entries.Dequeue(), LogEntryEqualityComparer<long>.NoTimestamp);
                        Assert.That.AreEqual(new LogEntry<long>(levels[j], 0, "1 2 3 4"  ), logger.Entries.Dequeue(), LogEntryEqualityComparer<long>.NoTimestamp);
                        Assert.That.AreEqual(new LogEntry<long>(levels[j], 0, "1 2 3 4 5"), logger.Entries.Dequeue(), LogEntryEqualityComparer<long>.NoTimestamp);
                    }
                }
            }
        }

        [TestMethod]
        public void Log()
        {
            Guid userId = Guid.NewGuid();
            using (MemoryLogger<Guid> logger = new MemoryLogger<Guid>(default, CultureInfo.InvariantCulture))
            {
                logger.Log(LogLevel.Trace, userId, "Trace");
                logger.Log(LogLevel.Debug, userId, "Debug");
                logger.Log(LogLevel.Info , userId, "Info" );
                logger.Log(LogLevel.Warn , userId, "Warn" );
                logger.Log(LogLevel.Error, userId, "Error");
                logger.Log(LogLevel.Fatal, userId, "Fatal");

                Assert.AreEqual(6, logger.Entries.Count);
                LogEntry<Guid>[] expectedEntries = new LogEntry<Guid>[]
                {
                    new LogEntry<Guid>(LogLevel.Trace, userId, "Trace"),
                    new LogEntry<Guid>(LogLevel.Debug, userId, "Debug"),
                    new LogEntry<Guid>(LogLevel.Info , userId, "Info" ),
                    new LogEntry<Guid>(LogLevel.Warn , userId, "Warn" ),
                    new LogEntry<Guid>(LogLevel.Error, userId, "Error"),
                    new LogEntry<Guid>(LogLevel.Fatal, userId, "Fatal"),
                };

                for (int i = 0; i < logger.Entries.Count; i++)
                    Assert.That.AreEqual(expectedEntries[i], logger.Entries.Dequeue(), LogEntryEqualityComparer<Guid>.NoTimestamp);
            }
        }

        [TestMethod]
        public void LogFormatArg0()
        {
            CultureInfo jaJP = CultureInfo.GetCultureInfo("ja-JP");
            using (MemoryLogger<float> logger = new MemoryLogger<float>(default, jaJP))
            {
                string msg = "{0,8:C}";

                logger.Log(LogLevel.Trace, 1F, msg,   4321);
                logger.Log(LogLevel.Debug, 2F, msg,  11235);
                logger.Log(LogLevel.Info , 3F, msg,  81321);
                logger.Log(LogLevel.Warn , 4F, msg,   3455);
                logger.Log(LogLevel.Error, 5F, msg,  89144);
                logger.Log(LogLevel.Fatal, 6F, msg, 233377);

                Assert.AreEqual(6, logger.Entries.Count);
                LogEntry<float>[] expectedEntries = new LogEntry<float>[]
                {
                    new LogEntry<float>(LogLevel.Trace, 1F, string.Format(jaJP, msg,   4321)),
                    new LogEntry<float>(LogLevel.Debug, 2F, string.Format(jaJP, msg,  11235)),
                    new LogEntry<float>(LogLevel.Info , 3F, string.Format(jaJP, msg,  81321)),
                    new LogEntry<float>(LogLevel.Warn , 4F, string.Format(jaJP, msg,   3455)),
                    new LogEntry<float>(LogLevel.Error, 5F, string.Format(jaJP, msg,  89144)),
                    new LogEntry<float>(LogLevel.Fatal, 6F, string.Format(jaJP, msg, 233377)),
                };

                for (int i = 0; i < logger.Entries.Count; i++)
                    Assert.That.AreEqual(expectedEntries[i], logger.Entries.Dequeue(), LogEntryEqualityComparer<float>.NoTimestamp);
            }
        }

        [TestMethod]
        public void LogFormatArg0Arg1()
        {
            CultureInfo jaJP = CultureInfo.GetCultureInfo("ja-JP");
            using (MemoryLogger<string> logger = new MemoryLogger<string>(default, jaJP))
            {
                string msg = "On {0:d}, made {1,11:C}";

                DateTime dt = new DateTime(2019, 01, 15);
                logger.Log(LogLevel.Trace, "Shop #1", msg, dt.AddDays(0),      823);
                logger.Log(LogLevel.Debug, "Shop #2", msg, dt.AddDays(1),     1234);
                logger.Log(LogLevel.Info , "Shop #1", msg, dt.AddDays(2),     5678);
                logger.Log(LogLevel.Warn , "Shop #3", msg, dt.AddDays(3),    91011);
                logger.Log(LogLevel.Error, "Shop #3", msg, dt.AddDays(4),   121314);
                logger.Log(LogLevel.Fatal, "Shop #1", msg, dt.AddDays(5), 15161718);

                Assert.AreEqual(6, logger.Entries.Count);
                LogEntry<string>[] expectedEntries = new LogEntry<string>[]
                {
                    new LogEntry<string>(LogLevel.Trace, "Shop #1", string.Format(jaJP, msg, dt.AddDays(0),      823)),
                    new LogEntry<string>(LogLevel.Debug, "Shop #2", string.Format(jaJP, msg, dt.AddDays(1),     1234)),
                    new LogEntry<string>(LogLevel.Info , "Shop #1", string.Format(jaJP, msg, dt.AddDays(2),     5678)),
                    new LogEntry<string>(LogLevel.Warn , "Shop #3", string.Format(jaJP, msg, dt.AddDays(3),    91011)),
                    new LogEntry<string>(LogLevel.Error, "Shop #3", string.Format(jaJP, msg, dt.AddDays(4),   121314)),
                    new LogEntry<string>(LogLevel.Fatal, "Shop #1", string.Format(jaJP, msg, dt.AddDays(5), 15161718)),
                };

                for (int i = 0; i < logger.Entries.Count; i++)
                    Assert.That.AreEqual(expectedEntries[i], logger.Entries.Dequeue(), LogEntryEqualityComparer<string>.NoTimestamp);
            }
        }

        [TestMethod]
        public void LogFormatArg0Arg1Arg2()
        {
            CultureInfo esES = CultureInfo.GetCultureInfo("es-ES");
            using (MemoryLogger<long> logger = new MemoryLogger<long>(default, esES))
            {
                string boughtMsg = "On {0:d}, bought {1,7:F2} units for {2,11:C}";
                string soldMsg   = "On {0:d}, sold   {1,7:F2} units for {2,11:C}";

                DateTime dt = new DateTime(2019, 01, 15);
                logger.Log(LogLevel.Trace, 1L, boughtMsg, dt.AddDays(0),    0.42,    13.37);
                logger.Log(LogLevel.Debug, 2L, boughtMsg, dt.AddDays(1),   24.68,    -0.25);
                logger.Log(LogLevel.Info , 1L, soldMsg  , dt.AddDays(2),   10.12,     1.33);
                logger.Log(LogLevel.Warn , 3L, soldMsg  , dt.AddDays(3), 1416.00, 11975.31);
                logger.Log(LogLevel.Error, 3L, boughtMsg, dt.AddDays(4),   18.20,   -10.00);
                logger.Log(LogLevel.Fatal, 1L, soldMsg  , dt.AddDays(5), 2224.00,   115.00);

                Assert.AreEqual(6, logger.Entries.Count);
                LogEntry<long>[] expectedEntries = new LogEntry<long>[]
                {
                    new LogEntry<long>(LogLevel.Trace, 1L, string.Format(esES, boughtMsg, dt.AddDays(0),    0.42,    13.37)),
                    new LogEntry<long>(LogLevel.Debug, 2L, string.Format(esES, boughtMsg, dt.AddDays(1),   24.68,    -0.25)),
                    new LogEntry<long>(LogLevel.Info , 1L, string.Format(esES, soldMsg  , dt.AddDays(2),   10.12,     1.33)),
                    new LogEntry<long>(LogLevel.Warn , 3L, string.Format(esES, soldMsg  , dt.AddDays(3), 1416.00, 11975.31)),
                    new LogEntry<long>(LogLevel.Error, 3L, string.Format(esES, boughtMsg, dt.AddDays(4),   18.20,   -10.00)),
                    new LogEntry<long>(LogLevel.Fatal, 1L, string.Format(esES, soldMsg  , dt.AddDays(5), 2224.00,   115.00)),
                };

                for (int i = 0; i < logger.Entries.Count; i++)
                    Assert.That.AreEqual(expectedEntries[i], logger.Entries.Dequeue(), LogEntryEqualityComparer<long>.NoTimestamp);
            }
        }

        [TestMethod]
        public void LogFormatParams()
        {
            CultureInfo esES = CultureInfo.GetCultureInfo("es-ES");
            using (MemoryLogger<long> logger = new MemoryLogger<long>(default, esES))
            {
                string boughtMsg = "On {0:d}, bought {1,7:F2} units for {2,11:C} (Total = {3,15:C})";
                string soldMsg   = "On {0:d}, sold   {1,7:F2} units for {2,11:C} (Total = {3,15:C})";

                DateTime dt = new DateTime(2019, 01, 15);
                logger.Log(LogLevel.Trace, 1L, boughtMsg, dt.AddDays(0),    0.42,    13.37,        5.6154);
                logger.Log(LogLevel.Debug, 2L, boughtMsg, dt.AddDays(1),   24.68,    -0.25,       -6.1700);
                logger.Log(LogLevel.Info , 1L, soldMsg  , dt.AddDays(2),   10.12,     1.33,       13.4596);
                logger.Log(LogLevel.Warn , 3L, soldMsg  , dt.AddDays(3), 1416.00, 11975.31, 16957039.0000);
                logger.Log(LogLevel.Error, 3L, boughtMsg, dt.AddDays(4),   18.20,   -10.00,     -182.0000);
                logger.Log(LogLevel.Fatal, 1L, soldMsg  , dt.AddDays(5), 2224.00,   115.00,   255760.0000);

                Assert.AreEqual(6, logger.Entries.Count);
                LogEntry<long>[] expectedEntries = new LogEntry<long>[]
                {
                    new LogEntry<long>(LogLevel.Trace, 1L, string.Format(esES, boughtMsg, dt.AddDays(0),    0.42,    13.37,        5.6154)),
                    new LogEntry<long>(LogLevel.Debug, 2L, string.Format(esES, boughtMsg, dt.AddDays(1),   24.68,    -0.25,       -6.1700)),
                    new LogEntry<long>(LogLevel.Info , 1L, string.Format(esES, soldMsg  , dt.AddDays(2),   10.12,     1.33,       13.4596)),
                    new LogEntry<long>(LogLevel.Warn , 3L, string.Format(esES, soldMsg  , dt.AddDays(3), 1416.00, 11975.31, 16957039.0000)),
                    new LogEntry<long>(LogLevel.Error, 3L, string.Format(esES, boughtMsg, dt.AddDays(4),   18.20,   -10.00,     -182.0000)),
                    new LogEntry<long>(LogLevel.Fatal, 1L, string.Format(esES, soldMsg  , dt.AddDays(5), 2224.00,   115.00,   255760.0000)),
                };

                for (int i = 0; i < logger.Entries.Count; i++)
                    Assert.That.AreEqual(expectedEntries[i], logger.Entries.Dequeue(), LogEntryEqualityComparer<long>.NoTimestamp);
            }
        }
    }
}
