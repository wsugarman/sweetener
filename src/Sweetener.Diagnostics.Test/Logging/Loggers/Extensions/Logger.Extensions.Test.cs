using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweetener.Diagnostics.Logging.Extensions;
using Sweetener.Diagnostics.Test;

namespace Sweetener.Diagnostics.Logging.Test
{
    [TestClass]
    public class LoggerExtensionsTest
    {
        [TestMethod]
        public void LogThrowIfNullArgument()
        {
            using (Logger logger = new MemoryLogger(default, CultureInfo.InvariantCulture))
            {
                Logger invalidLogger = null;
                object[] args = null;

                // Trace
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Trace("1"                            ));
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Trace("1 {0}"            , 2         ));
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Trace("1 {0} {1}"        , 2, 3      ));
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Trace("1 {0} {1} {2}"    , 2, 3, 4   ));
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Trace("1 {0} {1} {2} {3}", 2, 3, 4, 5));

                Assert.ThrowsException<ArgumentNullException>(() => logger.Trace(null, 1         ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Trace(null, 1, 2      ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Trace(null, 1, 2, 3   ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Trace(null, 1, 2, 3, 4));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Trace("{0}", args     ));

                // Debug
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Debug("1"                            ));
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Debug("1 {0}"            , 2         ));
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Debug("1 {0} {1}"        , 2, 3      ));
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Debug("1 {0} {1} {2}"    , 2, 3, 4   ));
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Debug("1 {0} {1} {2} {3}", 2, 3, 4, 5));

                Assert.ThrowsException<ArgumentNullException>(() => logger.Debug(null, 1         ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Debug(null, 1, 2      ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Debug(null, 1, 2, 3   ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Debug(null, 1, 2, 3, 4));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Debug("{0}", args     ));

                // Info
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Info("1"                            ));
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Info("1 {0}"            , 2         ));
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Info("1 {0} {1}"        , 2, 3      ));
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Info("1 {0} {1} {2}"    , 2, 3, 4   ));
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Info("1 {0} {1} {2} {3}", 2, 3, 4, 5));

                Assert.ThrowsException<ArgumentNullException>(() => logger.Info(null, 1         ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Info(null, 1, 2      ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Info(null, 1, 2, 3   ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Info(null, 1, 2, 3, 4));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Info("{0}", args     ));

                // Warn
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Warn("1"                            ));
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Warn("1 {0}"            , 2         ));
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Warn("1 {0} {1}"        , 2, 3      ));
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Warn("1 {0} {1} {2}"    , 2, 3, 4   ));
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Warn("1 {0} {1} {2} {3}", 2, 3, 4, 5));

                Assert.ThrowsException<ArgumentNullException>(() => logger.Warn(null, 1         ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Warn(null, 1, 2      ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Warn(null, 1, 2, 3   ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Warn(null, 1, 2, 3, 4));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Warn("{0}", args     ));

                // Error
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Error("1"                            ));
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Error("1 {0}"            , 2         ));
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Error("1 {0} {1}"        , 2, 3      ));
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Error("1 {0} {1} {2}"    , 2, 3, 4   ));
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Error("1 {0} {1} {2} {3}", 2, 3, 4, 5));

                Assert.ThrowsException<ArgumentNullException>(() => logger.Error(null, 1         ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Error(null, 1, 2      ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Error(null, 1, 2, 3   ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Error(null, 1, 2, 3, 4));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Error("{0}", args     ));

                // Fatal
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Fatal("1"                            ));
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Fatal("1 {0}"            , 2         ));
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Fatal("1 {0} {1}"        , 2, 3      ));
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Fatal("1 {0} {1} {2}"    , 2, 3, 4   ));
                Assert.ThrowsException<ArgumentNullException>(() => invalidLogger.Fatal("1 {0} {1} {2} {3}", 2, 3, 4, 5));

                Assert.ThrowsException<ArgumentNullException>(() => logger.Fatal(null, 1         ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Fatal(null, 1, 2      ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Fatal(null, 1, 2, 3   ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Fatal(null, 1, 2, 3, 4));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Fatal("{0}", args     ));
            }
        }

        [TestMethod]
        public void LogThrowBadFormat()
        {
            // Use an unknown format specifier for numbers
            using (Logger logger = new MemoryLogger(default, CultureInfo.InvariantCulture))
            {
                // Trace
                Assert.ThrowsException<FormatException>(() => logger.Trace("{0:Y}", 1         ));
                Assert.ThrowsException<FormatException>(() => logger.Trace("{0:Y}", 1, 2      ));
                Assert.ThrowsException<FormatException>(() => logger.Trace("{0:Y}", 1, 2, 3   ));
                Assert.ThrowsException<FormatException>(() => logger.Trace("{0:Y}", 1, 2, 3, 4));

                // Debug
                Assert.ThrowsException<FormatException>(() => logger.Debug("{0:Y}", 1         ));
                Assert.ThrowsException<FormatException>(() => logger.Debug("{0:Y}", 1, 2      ));
                Assert.ThrowsException<FormatException>(() => logger.Debug("{0:Y}", 1, 2, 3   ));
                Assert.ThrowsException<FormatException>(() => logger.Debug("{0:Y}", 1, 2, 3, 4));

                // Info
                Assert.ThrowsException<FormatException>(() => logger.Info("{0:Y}", 1         ));
                Assert.ThrowsException<FormatException>(() => logger.Info("{0:Y}", 1, 2      ));
                Assert.ThrowsException<FormatException>(() => logger.Info("{0:Y}", 1, 2, 3   ));
                Assert.ThrowsException<FormatException>(() => logger.Info("{0:Y}", 1, 2, 3, 4));

                // Warn
                Assert.ThrowsException<FormatException>(() => logger.Warn("{0:Y}", 1         ));
                Assert.ThrowsException<FormatException>(() => logger.Warn("{0:Y}", 1, 2      ));
                Assert.ThrowsException<FormatException>(() => logger.Warn("{0:Y}", 1, 2, 3   ));
                Assert.ThrowsException<FormatException>(() => logger.Warn("{0:Y}", 1, 2, 3, 4));

                // Error
                Assert.ThrowsException<FormatException>(() => logger.Error("{0:Y}", 1         ));
                Assert.ThrowsException<FormatException>(() => logger.Error("{0:Y}", 1, 2      ));
                Assert.ThrowsException<FormatException>(() => logger.Error("{0:Y}", 1, 2, 3   ));
                Assert.ThrowsException<FormatException>(() => logger.Error("{0:Y}", 1, 2, 3, 4));

                // Fatal
                Assert.ThrowsException<FormatException>(() => logger.Fatal("{0:Y}", 1         ));
                Assert.ThrowsException<FormatException>(() => logger.Fatal("{0:Y}", 1, 2      ));
                Assert.ThrowsException<FormatException>(() => logger.Fatal("{0:Y}", 1, 2, 3   ));
                Assert.ThrowsException<FormatException>(() => logger.Fatal("{0:Y}", 1, 2, 3, 4));
            }
        }

        [TestMethod]
        public void IgnoreBelowMinLevel()
        {
            LogLevel[] levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
            for (int i = 0; i < levels.Length; i++)
            {
                using (MemoryLogger logger = new MemoryLogger(levels[i], CultureInfo.InvariantCulture))
                {
                    // Trace
                    logger.Trace("1"                            );
                    logger.Trace("1 {0}"            , 2         );
                    logger.Trace("1 {0} {1}"        , 2, 3      );
                    logger.Trace("1 {0} {1} {2}"    , 2, 3, 4   );
                    logger.Trace("1 {0} {1} {2} {3}", 2, 3, 4, 5);

                    // Debug
                    logger.Debug("1"                            );
                    logger.Debug("1 {0}"            , 2         );
                    logger.Debug("1 {0} {1}"        , 2, 3      );
                    logger.Debug("1 {0} {1} {2}"    , 2, 3, 4   );
                    logger.Debug("1 {0} {1} {2} {3}", 2, 3, 4, 5);

                    // Info
                    logger.Info("1"                            );
                    logger.Info("1 {0}"            , 2         );
                    logger.Info("1 {0} {1}"        , 2, 3      );
                    logger.Info("1 {0} {1} {2}"    , 2, 3, 4   );
                    logger.Info("1 {0} {1} {2} {3}", 2, 3, 4, 5);

                    // Warn
                    logger.Warn("1"                            );
                    logger.Warn("1 {0}"            , 2         );
                    logger.Warn("1 {0} {1}"        , 2, 3      );
                    logger.Warn("1 {0} {1} {2}"    , 2, 3, 4   );
                    logger.Warn("1 {0} {1} {2} {3}", 2, 3, 4, 5);

                    // Error
                    logger.Error("1"                            );
                    logger.Error("1 {0}"            , 2         );
                    logger.Error("1 {0} {1}"        , 2, 3      );
                    logger.Error("1 {0} {1} {2}"    , 2, 3, 4   );
                    logger.Error("1 {0} {1} {2} {3}", 2, 3, 4, 5);

                    // Fatal
                    logger.Fatal("1"                            );
                    logger.Fatal("1 {0}"            , 2         );
                    logger.Fatal("1 {0} {1}"        , 2, 3      );
                    logger.Fatal("1 {0} {1} {2}"    , 2, 3, 4   );
                    logger.Fatal("1 {0} {1} {2} {3}", 2, 3, 4, 5);

                    // As the minimum level increases, it reduces the number of log entries
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
                logger.Trace("Trace");
                logger.Debug("Debug");
                logger.Info ("Info" );
                logger.Warn ("Warn" );
                logger.Error("Error");
                logger.Fatal("Fatal");

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

                logger.Trace(msg,   4321);
                logger.Debug(msg,  11235);
                logger.Info (msg,  81321);
                logger.Warn (msg,   3455);
                logger.Error(msg,  89144);
                logger.Fatal(msg, 233377);

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
                logger.Trace(msg, dt.AddDays(0),      823);
                logger.Debug(msg, dt.AddDays(1),     1234);
                logger.Info (msg, dt.AddDays(2),     5678);
                logger.Warn (msg, dt.AddDays(3),    91011);
                logger.Error(msg, dt.AddDays(4),   121314);
                logger.Fatal(msg, dt.AddDays(5), 15161718);

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
                logger.Trace(boughtMsg, dt.AddDays(0),    0.42,    13.37);
                logger.Debug(boughtMsg, dt.AddDays(1),   24.68,    -0.25);
                logger.Info (soldMsg  , dt.AddDays(2),   10.12,     1.33);
                logger.Warn (soldMsg  , dt.AddDays(3), 1416.00, 11975.31);
                logger.Error(boughtMsg, dt.AddDays(4),   18.20,   -10.00);
                logger.Fatal(soldMsg  , dt.AddDays(5), 2224.00,   115.00);

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
                logger.Trace(boughtMsg, dt.AddDays(0),    0.42,    13.37,        5.6154);
                logger.Debug(boughtMsg, dt.AddDays(1),   24.68,    -0.25,       -6.1700);
                logger.Info (soldMsg  , dt.AddDays(2),   10.12,     1.33,       13.4596);
                logger.Warn (soldMsg  , dt.AddDays(3), 1416.00, 11975.31, 16957039.0000);
                logger.Error(boughtMsg, dt.AddDays(4),   18.20,   -10.00,     -182.0000);
                logger.Fatal(soldMsg  , dt.AddDays(5), 2224.00,   115.00,   255760.0000);

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
