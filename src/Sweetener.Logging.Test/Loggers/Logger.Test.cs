using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Logging.Test
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public void Constructor()
        {
            // Argument Validation
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MemoryLogger((LogLevel)27)      );
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MemoryLogger((LogLevel)27, null));

            // Constructor Overloads
            using (Logger logger = new MemoryLogger())
            {
                Assert.AreEqual(LogLevel.Trace            , logger.MinLevel      );
                Assert.AreEqual(CultureInfo.CurrentCulture, logger.FormatProvider);
            }

            using (Logger logger = new MemoryLogger(LogLevel.Warn))
            {
                Assert.AreEqual(LogLevel.Warn             , logger.MinLevel      );
                Assert.AreEqual(CultureInfo.CurrentCulture, logger.FormatProvider);
            }

            CultureInfo frFR = CultureInfo.GetCultureInfo("fr-FR");
            using (Logger logger = new MemoryLogger(LogLevel.Info, frFR))
            {
                Assert.AreEqual(LogLevel.Info, logger.MinLevel      );
                Assert.AreEqual(frFR         , logger.FormatProvider);
            }
        }

        [TestMethod]
        public void IsSynchronized()
        {
            Assert.IsFalse(new MemoryLogger(                                           ).IsSynchronized);
            Assert.IsFalse(new MemoryLogger(LogLevel.Info                              ).IsSynchronized);
            Assert.IsFalse(new MemoryLogger(LogLevel.Warn, CultureInfo.InvariantCulture).IsSynchronized);
        }

        [TestMethod]
        public void SyncRoot()
        {
            Logger logger0 = new MemoryLogger();
            Logger logger1 = new MemoryLogger(LogLevel.Info);
            Logger logger2 = new MemoryLogger(LogLevel.Warn, CultureInfo.InvariantCulture);

            Assert.IsNotNull(logger0.SyncRoot);
            Assert.IsNotNull(logger1.SyncRoot);
            Assert.IsNotNull(logger2.SyncRoot);

            Assert.AreEqual(typeof(object), logger0.SyncRoot.GetType());
            Assert.AreEqual(typeof(object), logger1.SyncRoot.GetType());
            Assert.AreEqual(typeof(object), logger2.SyncRoot.GetType());
        }

        [TestMethod]
        public void LogThrowIfDisposed()
        {
            Logger logger = new MemoryLogger();
            logger.Dispose();

            // Trace
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Trace("1 2 3 4 5"                          ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Trace("{0}"                 , 1            ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Trace("{0} {1}"             , 1, 2         ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Trace("{0} {1} {2}"         , 1, 2, 3      ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Trace("{0} {1} {2} {3}"     , 1, 2, 3, 4   ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Trace("{0} {1} {2} {3}, {4}", 1, 2, 3, 4, 5));

            // Debug
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Debug("1 2 3 4 5"                          ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Debug("{0}"                 , 1            ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Debug("{0} {1}"             , 1, 2         ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Debug("{0} {1} {2}"         , 1, 2, 3      ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Debug("{0} {1} {2} {3}"     , 1, 2, 3, 4   ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Debug("{0} {1} {2} {3}, {4}", 1, 2, 3, 4, 5));

            // Info
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Info("1 2 3 4 5"                          ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Info("{0}"                 , 1            ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Info("{0} {1}"             , 1, 2         ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Info("{0} {1} {2}"         , 1, 2, 3      ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Info("{0} {1} {2} {3}"     , 1, 2, 3, 4   ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Info("{0} {1} {2} {3}, {4}", 1, 2, 3, 4, 5));

            // Warn
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Warn("1 2 3 4 5"                          ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Warn("{0}"                 , 1            ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Warn("{0} {1}"             , 1, 2         ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Warn("{0} {1} {2}"         , 1, 2, 3      ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Warn("{0} {1} {2} {3}"     , 1, 2, 3, 4   ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Warn("{0} {1} {2} {3}, {4}", 1, 2, 3, 4, 5));

            // Error
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Error("1 2 3 4 5"                          ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Error("{0}"                 , 1            ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Error("{0} {1}"             , 1, 2         ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Error("{0} {1} {2}"         , 1, 2, 3      ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Error("{0} {1} {2} {3}"     , 1, 2, 3, 4   ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Error("{0} {1} {2} {3}, {4}", 1, 2, 3, 4, 5));

            // Fatal
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Fatal("1 2 3 4 5"                          ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Fatal("{0}"                 , 1            ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Fatal("{0} {1}"             , 1, 2         ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Fatal("{0} {1} {2}"         , 1, 2, 3      ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Fatal("{0} {1} {2} {3}"     , 1, 2, 3, 4   ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Fatal("{0} {1} {2} {3}, {4}", 1, 2, 3, 4, 5));
        }

        [TestMethod]
        public void LogThrowIfNullArgument()
        {
            using (Logger logger = new MemoryLogger())
            {
                object[] args = null;

                // Trace
                Assert.ThrowsException<ArgumentNullException>(() => logger.Trace(null               ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Trace(null, 1            ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Trace(null, 1, 2         ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Trace(null, 1, 2, 3      ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Trace(null, 1, 2, 3, 4   ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Trace(null, 1, 2, 3, 4, 5));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Trace("{0}", args        ));

                // Debug
                Assert.ThrowsException<ArgumentNullException>(() => logger.Debug(null               ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Debug(null, 1            ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Debug(null, 1, 2         ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Debug(null, 1, 2, 3      ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Debug(null, 1, 2, 3, 4   ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Debug(null, 1, 2, 3, 4, 5));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Debug("{0}", args        ));

                // Info
                Assert.ThrowsException<ArgumentNullException>(() => logger.Info(null               ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Info(null, 1            ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Info(null, 1, 2         ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Info(null, 1, 2, 3      ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Info(null, 1, 2, 3, 4   ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Info(null, 1, 2, 3, 4, 5));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Info("{0}", args        ));

                // Warn
                Assert.ThrowsException<ArgumentNullException>(() => logger.Warn(null               ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Warn(null, 1            ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Warn(null, 1, 2         ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Warn(null, 1, 2, 3      ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Warn(null, 1, 2, 3, 4   ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Warn(null, 1, 2, 3, 4, 5));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Warn("{0}", args        ));

                // Error
                Assert.ThrowsException<ArgumentNullException>(() => logger.Error(null               ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Error(null, 1            ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Error(null, 1, 2         ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Error(null, 1, 2, 3      ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Error(null, 1, 2, 3, 4   ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Error(null, 1, 2, 3, 4, 5));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Error("{0}", args        ));

                // Fatal
                Assert.ThrowsException<ArgumentNullException>(() => logger.Fatal(null               ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Fatal(null, 1            ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Fatal(null, 1, 2         ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Fatal(null, 1, 2, 3      ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Fatal(null, 1, 2, 3, 4   ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Fatal(null, 1, 2, 3, 4, 5));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Fatal("{0}", args        ));
            }
        }

        [TestMethod]
        public void LogThrowBadFormat()
        {
            // Use an unknown format specifier for numbers
            using (Logger logger = new MemoryLogger())
            {
                // Trace
                Assert.ThrowsException<FormatException>(() => logger.Trace("{0:Y}", 1            ));
                Assert.ThrowsException<FormatException>(() => logger.Trace("{0:Y}", 1, 2         ));
                Assert.ThrowsException<FormatException>(() => logger.Trace("{0:Y}", 1, 2, 3      ));
                Assert.ThrowsException<FormatException>(() => logger.Trace("{0:Y}", 1, 2, 3, 4   ));
                Assert.ThrowsException<FormatException>(() => logger.Trace("{0:Y}", 1, 2, 3, 4, 5));

                // Debug
                Assert.ThrowsException<FormatException>(() => logger.Debug("{0:Y}", 1            ));
                Assert.ThrowsException<FormatException>(() => logger.Debug("{0:Y}", 1, 2         ));
                Assert.ThrowsException<FormatException>(() => logger.Debug("{0:Y}", 1, 2, 3      ));
                Assert.ThrowsException<FormatException>(() => logger.Debug("{0:Y}", 1, 2, 3, 4   ));
                Assert.ThrowsException<FormatException>(() => logger.Debug("{0:Y}", 1, 2, 3, 4, 5));

                // Info
                Assert.ThrowsException<FormatException>(() => logger.Info("{0:Y}", 1            ));
                Assert.ThrowsException<FormatException>(() => logger.Info("{0:Y}", 1, 2         ));
                Assert.ThrowsException<FormatException>(() => logger.Info("{0:Y}", 1, 2, 3      ));
                Assert.ThrowsException<FormatException>(() => logger.Info("{0:Y}", 1, 2, 3, 4   ));
                Assert.ThrowsException<FormatException>(() => logger.Info("{0:Y}", 1, 2, 3, 4, 5));

                // Warn
                Assert.ThrowsException<FormatException>(() => logger.Warn("{0:Y}", 1            ));
                Assert.ThrowsException<FormatException>(() => logger.Warn("{0:Y}", 1, 2         ));
                Assert.ThrowsException<FormatException>(() => logger.Warn("{0:Y}", 1, 2, 3      ));
                Assert.ThrowsException<FormatException>(() => logger.Warn("{0:Y}", 1, 2, 3, 4   ));
                Assert.ThrowsException<FormatException>(() => logger.Warn("{0:Y}", 1, 2, 3, 4, 5));

                // Error
                Assert.ThrowsException<FormatException>(() => logger.Error("{0:Y}", 1            ));
                Assert.ThrowsException<FormatException>(() => logger.Error("{0:Y}", 1, 2         ));
                Assert.ThrowsException<FormatException>(() => logger.Error("{0:Y}", 1, 2, 3      ));
                Assert.ThrowsException<FormatException>(() => logger.Error("{0:Y}", 1, 2, 3, 4   ));
                Assert.ThrowsException<FormatException>(() => logger.Error("{0:Y}", 1, 2, 3, 4, 5));

                // Fatal
                Assert.ThrowsException<FormatException>(() => logger.Fatal("{0:Y}", 1            ));
                Assert.ThrowsException<FormatException>(() => logger.Fatal("{0:Y}", 1, 2         ));
                Assert.ThrowsException<FormatException>(() => logger.Fatal("{0:Y}", 1, 2, 3      ));
                Assert.ThrowsException<FormatException>(() => logger.Fatal("{0:Y}", 1, 2, 3, 4   ));
                Assert.ThrowsException<FormatException>(() => logger.Fatal("{0:Y}", 1, 2, 3, 4, 5));
            }
        }

        [TestMethod]
        public void IgnoreBelowMinLevel()
        {
            foreach (LogLevel minLevel in (LogLevel[])Enum.GetValues(typeof(LogLevel)))
            {
                using (MemoryLogger logger = new MemoryLogger(minLevel))
                {
                    // Trace
                    logger.Trace("0"                                   );
                    logger.Trace("0 {0}"                , 1            );
                    logger.Trace("0 {0} {1}"            , 1, 2         );
                    logger.Trace("0 {0} {1} {2}"        , 1, 2, 3      );
                    logger.Trace("0 {0} {1} {2} {3}"    , 1, 2, 3, 4   );
                    logger.Trace("0 {0} {1} {2} {3} {4}", 1, 2, 3, 4, 5);

                    // Debug
                    logger.Debug("0"                                   );
                    logger.Debug("0 {0}"                , 1            );
                    logger.Debug("0 {0} {1}"            , 1, 2         );
                    logger.Debug("0 {0} {1} {2}"        , 1, 2, 3      );
                    logger.Debug("0 {0} {1} {2} {3}"    , 1, 2, 3, 4   );
                    logger.Debug("0 {0} {1} {2} {3} {4}", 1, 2, 3, 4, 5);

                    // Info
                    logger.Info("0"                                   );
                    logger.Info("0 {0}"                , 1            );
                    logger.Info("0 {0} {1}"            , 1, 2         );
                    logger.Info("0 {0} {1} {2}"        , 1, 2, 3      );
                    logger.Info("0 {0} {1} {2} {3}"    , 1, 2, 3, 4   );
                    logger.Info("0 {0} {1} {2} {3} {4}", 1, 2, 3, 4, 5);

                    // Warn
                    logger.Warn("0"                                   );
                    logger.Warn("0 {0}"                , 1            );
                    logger.Warn("0 {0} {1}"            , 1, 2         );
                    logger.Warn("0 {0} {1} {2}"        , 1, 2, 3      );
                    logger.Warn("0 {0} {1} {2} {3}"    , 1, 2, 3, 4   );
                    logger.Warn("0 {0} {1} {2} {3} {4}", 1, 2, 3, 4, 5);

                    // Error
                    logger.Error("0"                                   );
                    logger.Error("0 {0}"                , 1            );
                    logger.Error("0 {0} {1}"            , 1, 2         );
                    logger.Error("0 {0} {1} {2}"        , 1, 2, 3      );
                    logger.Error("0 {0} {1} {2} {3}"    , 1, 2, 3, 4   );
                    logger.Error("0 {0} {1} {2} {3} {4}", 1, 2, 3, 4, 5);

                    // Fatal
                    logger.Fatal("0"                                   );
                    logger.Fatal("0 {0}"                , 1            );
                    logger.Fatal("0 {0} {1}"            , 1, 2         );
                    logger.Fatal("0 {0} {1} {2}"        , 1, 2, 3      );
                    logger.Fatal("0 {0} {1} {2} {3}"    , 1, 2, 3, 4   );
                    logger.Fatal("0 {0} {1} {2} {3} {4}", 1, 2, 3, 4, 5);

                    // As the minimum level increases, it reduces the number of log entries
                    int logLevelCount = Enum.GetValues(typeof(LogLevel)).Length;
                    int expectedCount = (logLevelCount - (int)minLevel) * logLevelCount;
                    Assert.AreEqual(expectedCount, logger.Entries.Count);

                    LogLevel level = minLevel;
                    while (logger.Entries.Count > 0)
                    {
                        Assert.IsTrue(logger.Entries.Count >= 6);
                        AssertLogEntry(level, "0"          , logger.Entries.Dequeue());
                        AssertLogEntry(level, "0 1"        , logger.Entries.Dequeue());
                        AssertLogEntry(level, "0 1 2"      , logger.Entries.Dequeue());
                        AssertLogEntry(level, "0 1 2 3"    , logger.Entries.Dequeue());
                        AssertLogEntry(level, "0 1 2 3 4"  , logger.Entries.Dequeue());
                        AssertLogEntry(level, "0 1 2 3 4 5", logger.Entries.Dequeue());

                        level++;
                    }
                }
            }
        }

        [TestMethod]
        public void LogMessage()
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
                AssertLogEntry(LogLevel.Trace, "Trace", logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Debug, "Debug", logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Info , "Info" , logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Warn , "Warn" , logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Error, "Error", logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Fatal, "Fatal", logger.Entries.Dequeue());
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
                AssertLogEntry(LogLevel.Trace, string.Format(jaJP, msg,   4321), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Debug, string.Format(jaJP, msg,  11235), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Info , string.Format(jaJP, msg,  81321), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Warn , string.Format(jaJP, msg,   3455), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Error, string.Format(jaJP, msg,  89144), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Fatal, string.Format(jaJP, msg, 233377), logger.Entries.Dequeue());
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
                AssertLogEntry(LogLevel.Trace, string.Format(jaJP, msg, dt.AddDays(0),      823), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Debug, string.Format(jaJP, msg, dt.AddDays(1),     1234), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Info , string.Format(jaJP, msg, dt.AddDays(2),     5678), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Warn , string.Format(jaJP, msg, dt.AddDays(3),    91011), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Error, string.Format(jaJP, msg, dt.AddDays(4),   121314), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Fatal, string.Format(jaJP, msg, dt.AddDays(5), 15161718), logger.Entries.Dequeue());
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
                AssertLogEntry(LogLevel.Trace, string.Format(esES, boughtMsg, dt.AddDays(0),    0.42,    13.37), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Debug, string.Format(esES, boughtMsg, dt.AddDays(1),   24.68,    -0.25), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Info , string.Format(esES, soldMsg  , dt.AddDays(2),   10.12,     1.33), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Warn , string.Format(esES, soldMsg  , dt.AddDays(3), 1416.00, 11975.31), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Error, string.Format(esES, boughtMsg, dt.AddDays(4),   18.20,   -10.00), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Fatal, string.Format(esES, soldMsg  , dt.AddDays(5), 2224.00,   115.00), logger.Entries.Dequeue());
            }
        }

        [TestMethod]
        public void LogFormatArg0Arg1Arg2Arg3()
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
                AssertLogEntry(LogLevel.Trace, string.Format(esES, boughtMsg, dt.AddDays(0),    0.42,    13.37,        5.6154), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Debug, string.Format(esES, boughtMsg, dt.AddDays(1),   24.68,    -0.25,       -6.1700), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Info , string.Format(esES, soldMsg  , dt.AddDays(2),   10.12,     1.33,       13.4596), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Warn , string.Format(esES, soldMsg  , dt.AddDays(3), 1416.00, 11975.31, 16957039.0000), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Error, string.Format(esES, boughtMsg, dt.AddDays(4),   18.20,   -10.00,     -182.0000), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Fatal, string.Format(esES, soldMsg  , dt.AddDays(5), 2224.00,   115.00,   255760.0000), logger.Entries.Dequeue());
            }
        }

        [TestMethod]
        public void LogFormatParams()
        {
            CultureInfo esES = CultureInfo.GetCultureInfo("es-ES");
            using (MemoryLogger logger = new MemoryLogger(default, esES))
            {
                string boughtMsg = "On {0:d} {1:g}, bought {2,7:F2} units for {3,11:C} (Total = {4,15:C})";
                string soldMsg   = "On {0:d} {1:g}, sold   {2,7:F2} units for {3,11:C} (Total = {4,15:C})";

                DateTime dt = new DateTime(2019, 01, 15, 16, 17, 18, 19);
                logger.Trace(boughtMsg, dt.AddDays(0), dt.AddHours(0).TimeOfDay,    0.42,    13.37,        5.6154);
                logger.Debug(boughtMsg, dt.AddDays(1), dt.AddHours(1).TimeOfDay,   24.68,    -0.25,       -6.1700);
                logger.Info (soldMsg  , dt.AddDays(2), dt.AddHours(2).TimeOfDay,   10.12,     1.33,       13.4596);
                logger.Warn (soldMsg  , dt.AddDays(3), dt.AddHours(3).TimeOfDay, 1416.00, 11975.31, 16957039.0000);
                logger.Error(boughtMsg, dt.AddDays(4), dt.AddHours(4).TimeOfDay,   18.20,   -10.00,     -182.0000);
                logger.Fatal(soldMsg  , dt.AddDays(5), dt.AddHours(5).TimeOfDay, 2224.00,   115.00,   255760.0000);
                 
                Assert.AreEqual(6, logger.Entries.Count);
                AssertLogEntry(LogLevel.Trace, string.Format(esES, boughtMsg, dt.AddDays(0), dt.AddHours(0).TimeOfDay,    0.42,    13.37,        5.6154), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Debug, string.Format(esES, boughtMsg, dt.AddDays(1), dt.AddHours(1).TimeOfDay,   24.68,    -0.25,       -6.1700), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Info , string.Format(esES, soldMsg  , dt.AddDays(2), dt.AddHours(2).TimeOfDay,   10.12,     1.33,       13.4596), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Warn , string.Format(esES, soldMsg  , dt.AddDays(3), dt.AddHours(3).TimeOfDay, 1416.00, 11975.31, 16957039.0000), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Error, string.Format(esES, boughtMsg, dt.AddDays(4), dt.AddHours(4).TimeOfDay,   18.20,   -10.00,     -182.0000), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Fatal, string.Format(esES, soldMsg  , dt.AddDays(5), dt.AddHours(5).TimeOfDay, 2224.00,   115.00,   255760.0000), logger.Entries.Dequeue());
            }
        }

        private static void AssertLogEntry(LogLevel expectedLevel, string expectedMessage, LogEntry<string> actual)
        {
            // We assert the validity of the time in the TemplateBuilder.Test.cs, so here we'll
            // instead assert that the value is a time in the appropriate format
            Assert.AreNotEqual(default        , actual.Timestamp);
            Assert.AreEqual   (expectedLevel  , actual.Level    );
            Assert.AreEqual   (expectedMessage, actual.Message  );
        }
    }
}
