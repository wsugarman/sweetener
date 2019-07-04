using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Logging.Test
{
    [TestClass]
    public class ContextualLoggerTest
    {
        [TestMethod]
        public void Constructor()
        {
            // Argument Validation
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MemoryLogger<int>((LogLevel)27)      );
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MemoryLogger<int>((LogLevel)27, null));

            // Constructor Overloads
            using (Logger<byte> logger = new MemoryLogger<byte>())
            {
                Assert.AreEqual(LogLevel.Trace            , logger.MinLevel      );
                Assert.AreEqual(CultureInfo.CurrentCulture, logger.FormatProvider);
            }

            using (Logger<string> logger = new MemoryLogger<string>(LogLevel.Warn))
            {
                Assert.AreEqual(LogLevel.Warn             , logger.MinLevel      );
                Assert.AreEqual(CultureInfo.CurrentCulture, logger.FormatProvider);
            }

            CultureInfo frFR = CultureInfo.GetCultureInfo("fr-FR");
            using (Logger<Guid> logger = new MemoryLogger<Guid>(LogLevel.Info, frFR))
            {
                Assert.AreEqual(LogLevel.Info, logger.MinLevel      );
                Assert.AreEqual(frFR         , logger.FormatProvider);
            }
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
        public void IsSynchronized()
        {
            using (Logger<char> logger = new MemoryLogger<char>())
                Assert.IsFalse(logger.IsSynchronized);

            using (Logger<char> logger = new MemoryLogger<char>(LogLevel.Info))
                Assert.IsFalse(logger.IsSynchronized);

            using (Logger<char> logger = new MemoryLogger<char>(LogLevel.Warn, CultureInfo.GetCultureInfo("es-ES")))
                Assert.IsFalse(logger.IsSynchronized);
        }

        [TestMethod]
        public void SyncRoot()
        {
            using (Logger<ulong> logger = new MemoryLogger<ulong>())
                Assert.AreEqual(logger, logger.SyncRoot);

            using (Logger<ulong> logger = new MemoryLogger<ulong>(LogLevel.Info))
                Assert.AreEqual(logger, logger.SyncRoot);

            using (Logger<ulong> logger = new MemoryLogger<ulong>(LogLevel.Warn, CultureInfo.GetCultureInfo("es-ES")))
                Assert.AreEqual(logger, logger.SyncRoot);
        }

        [TestMethod]
        public void ThrowObjectDisposedException()
        {
            Logger<int> logger = new MemoryLogger<int>();
            logger.Dispose();

            // Trace
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Trace(0, "1"                                   ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Trace(0, "1 {0}"                , 2            ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Trace(0, "1 {0} {1}"            , 2, 3         ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Trace(0, "1 {0} {1} {2}"        , 2, 3, 4      ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Trace(0, "1 {0} {1} {2} {3}"    , 2, 3, 4, 5   ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Trace(0, "1 {0} {1} {2} {3} {4}", 2, 3, 4, 5, 6));

            // Debug
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Debug(0, "1"                                   ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Debug(0, "1 {0}"                , 2            ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Debug(0, "1 {0} {1}"            , 2, 3         ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Debug(0, "1 {0} {1} {2}"        , 2, 3, 4      ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Debug(0, "1 {0} {1} {2} {3}"    , 2, 3, 4, 5   ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Debug(0, "1 {0} {1} {2} {3} {4}", 2, 3, 4, 5, 6));

            // Info
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Info(0, "1"                                   ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Info(0, "1 {0}"                , 2            ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Info(0, "1 {0} {1}"            , 2, 3         ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Info(0, "1 {0} {1} {2}"        , 2, 3, 4      ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Info(0, "1 {0} {1} {2} {3}"    , 2, 3, 4, 5   ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Info(0, "1 {0} {1} {2} {3} {4}", 2, 3, 4, 5, 6));

            // Warn
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Warn(0, "1"                                   ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Warn(0, "1 {0}"                , 2            ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Warn(0, "1 {0} {1}"            , 2, 3         ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Warn(0, "1 {0} {1} {2}"        , 2, 3, 4      ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Warn(0, "1 {0} {1} {2} {3}"    , 2, 3, 4, 5   ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Warn(0, "1 {0} {1} {2} {3} {4}", 2, 3, 4, 5, 6));

            // Error
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Error(0, "1"                                   ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Error(0, "1 {0}"                , 2            ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Error(0, "1 {0} {1}"            , 2, 3         ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Error(0, "1 {0} {1} {2}"        , 2, 3, 4      ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Error(0, "1 {0} {1} {2} {3}"    , 2, 3, 4, 5   ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Error(0, "1 {0} {1} {2} {3} {4}", 2, 3, 4, 5, 6));

            // Fatal
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Fatal(0, "1"                                   ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Fatal(0, "1 {0}"                , 2            ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Fatal(0, "1 {0} {1}"            , 2, 3         ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Fatal(0, "1 {0} {1} {2}"        , 2, 3, 4      ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Fatal(0, "1 {0} {1} {2} {3}"    , 2, 3, 4, 5   ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Fatal(0, "1 {0} {1} {2} {3} {4}", 2, 3, 4, 5, 6));
        }

        [TestMethod]
        public void LogThrowIfNullArgument()
        {
            using (Logger<char> logger = new MemoryLogger<char>())
            {
                object[] args = null;

                // Trace
                Assert.ThrowsException<ArgumentNullException>(() => logger.Trace('>', null, 1            ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Trace('>', null, 1, 2         ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Trace('>', null, 1, 2, 3      ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Trace('>', null, 1, 2, 3, 4   ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Trace('>', null, 1, 2, 3, 4, 5));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Trace('>', "{0}", args        ));

                // Debug
                Assert.ThrowsException<ArgumentNullException>(() => logger.Debug('>', null, 1            ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Debug('>', null, 1, 2         ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Debug('>', null, 1, 2, 3      ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Debug('>', null, 1, 2, 3, 4   ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Debug('>', null, 1, 2, 3, 4, 5));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Debug('>', "{0}", args        ));

                // Info
                Assert.ThrowsException<ArgumentNullException>(() => logger.Info('>', null, 1            ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Info('>', null, 1, 2         ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Info('>', null, 1, 2, 3      ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Info('>', null, 1, 2, 3, 4   ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Info('>', null, 1, 2, 3, 4, 5));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Info('>', "{0}", args        ));

                // Warn
                Assert.ThrowsException<ArgumentNullException>(() => logger.Warn('>', null, 1            ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Warn('>', null, 1, 2         ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Warn('>', null, 1, 2, 3      ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Warn('>', null, 1, 2, 3, 4   ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Warn('>', null, 1, 2, 3, 4, 5));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Warn('>', "{0}", args        ));

                // Error
                Assert.ThrowsException<ArgumentNullException>(() => logger.Error('>', null, 1            ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Error('>', null, 1, 2         ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Error('>', null, 1, 2, 3      ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Error('>', null, 1, 2, 3, 4   ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Error('>', null, 1, 2, 3, 4, 5));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Error('>', "{0}", args        ));

                // Fatal
                Assert.ThrowsException<ArgumentNullException>(() => logger.Fatal('>', null, 1            ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Fatal('>', null, 1, 2         ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Fatal('>', null, 1, 2, 3      ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Fatal('>', null, 1, 2, 3, 4   ));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Fatal('>', null, 1, 2, 3, 4, 5));
                Assert.ThrowsException<ArgumentNullException>(() => logger.Fatal('>', "{0}", args        ));
            }
        }

        [TestMethod]
        public void LogThrowBadFormat()
        {
            // Use an unknown format specifier for numbers
            using (Logger<double> logger = new MemoryLogger<double>())
            {
                // Trace
                Assert.ThrowsException<FormatException>(() => logger.Trace(3.14, "{0:Y}", 1            ));
                Assert.ThrowsException<FormatException>(() => logger.Trace(3.14, "{0:Y}", 1, 2         ));
                Assert.ThrowsException<FormatException>(() => logger.Trace(3.14, "{0:Y}", 1, 2, 3      ));
                Assert.ThrowsException<FormatException>(() => logger.Trace(3.14, "{0:Y}", 1, 2, 3, 4   ));
                Assert.ThrowsException<FormatException>(() => logger.Trace(3.14, "{0:Y}", 1, 2, 3, 4, 5));

                // Debug
                Assert.ThrowsException<FormatException>(() => logger.Debug(3.14, "{0:Y}", 1            ));
                Assert.ThrowsException<FormatException>(() => logger.Debug(3.14, "{0:Y}", 1, 2         ));
                Assert.ThrowsException<FormatException>(() => logger.Debug(3.14, "{0:Y}", 1, 2, 3      ));
                Assert.ThrowsException<FormatException>(() => logger.Debug(3.14, "{0:Y}", 1, 2, 3, 4   ));
                Assert.ThrowsException<FormatException>(() => logger.Debug(3.14, "{0:Y}", 1, 2, 3, 4, 5));

                // Info
                Assert.ThrowsException<FormatException>(() => logger.Info(3.14, "{0:Y}", 1            ));
                Assert.ThrowsException<FormatException>(() => logger.Info(3.14, "{0:Y}", 1, 2         ));
                Assert.ThrowsException<FormatException>(() => logger.Info(3.14, "{0:Y}", 1, 2, 3      ));
                Assert.ThrowsException<FormatException>(() => logger.Info(3.14, "{0:Y}", 1, 2, 3, 4   ));
                Assert.ThrowsException<FormatException>(() => logger.Info(3.14, "{0:Y}", 1, 2, 3, 4, 5));

                // Warn
                Assert.ThrowsException<FormatException>(() => logger.Warn(3.14, "{0:Y}", 1            ));
                Assert.ThrowsException<FormatException>(() => logger.Warn(3.14, "{0:Y}", 1, 2         ));
                Assert.ThrowsException<FormatException>(() => logger.Warn(3.14, "{0:Y}", 1, 2, 3      ));
                Assert.ThrowsException<FormatException>(() => logger.Warn(3.14, "{0:Y}", 1, 2, 3, 4   ));
                Assert.ThrowsException<FormatException>(() => logger.Warn(3.14, "{0:Y}", 1, 2, 3, 4, 5));

                // Error
                Assert.ThrowsException<FormatException>(() => logger.Error(3.14, "{0:Y}", 1            ));
                Assert.ThrowsException<FormatException>(() => logger.Error(3.14, "{0:Y}", 1, 2         ));
                Assert.ThrowsException<FormatException>(() => logger.Error(3.14, "{0:Y}", 1, 2, 3      ));
                Assert.ThrowsException<FormatException>(() => logger.Error(3.14, "{0:Y}", 1, 2, 3, 4   ));
                Assert.ThrowsException<FormatException>(() => logger.Error(3.14, "{0:Y}", 1, 2, 3, 4, 5));

                // Fatal
                Assert.ThrowsException<FormatException>(() => logger.Fatal(3.14, "{0:Y}", 1            ));
                Assert.ThrowsException<FormatException>(() => logger.Fatal(3.14, "{0:Y}", 1, 2         ));
                Assert.ThrowsException<FormatException>(() => logger.Fatal(3.14, "{0:Y}", 1, 2, 3      ));
                Assert.ThrowsException<FormatException>(() => logger.Fatal(3.14, "{0:Y}", 1, 2, 3, 4   ));
                Assert.ThrowsException<FormatException>(() => logger.Fatal(3.14, "{0:Y}", 1, 2, 3, 4, 5));
            }
        }

        [TestMethod]
        public void IgnoreBelowMinLevel()
        {
            LogLevel[] levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
            foreach (LogLevel minLevel in levels)
            {
                using (MemoryLogger<long> logger = new MemoryLogger<long>(minLevel))
                {
                    // Trace
                    logger.Trace(0, "1"                                   );
                    logger.Trace(0, "1 {0}"                , 2            );
                    logger.Trace(0, "1 {0} {1}"            , 2, 3         );
                    logger.Trace(0, "1 {0} {1} {2}"        , 2, 3, 4      );
                    logger.Trace(0, "1 {0} {1} {2} {3}"    , 2, 3, 4, 5   );
                    logger.Trace(0, "1 {0} {1} {2} {3} {4}", 2, 3, 4, 5, 6);

                    // Debug
                    logger.Debug(0, "1"                                   );
                    logger.Debug(0, "1 {0}"                , 2            );
                    logger.Debug(0, "1 {0} {1}"            , 2, 3         );
                    logger.Debug(0, "1 {0} {1} {2}"        , 2, 3, 4      );
                    logger.Debug(0, "1 {0} {1} {2} {3}"    , 2, 3, 4, 5   );
                    logger.Debug(0, "1 {0} {1} {2} {3} {4}", 2, 3, 4, 5, 6);

                    // Info
                    logger.Info(0, "1"                                   );
                    logger.Info(0, "1 {0}"                , 2            );
                    logger.Info(0, "1 {0} {1}"            , 2, 3         );
                    logger.Info(0, "1 {0} {1} {2}"        , 2, 3, 4      );
                    logger.Info(0, "1 {0} {1} {2} {3}"    , 2, 3, 4, 5   );
                    logger.Info(0, "1 {0} {1} {2} {3} {4}", 2, 3, 4, 5, 6);

                    // Warn
                    logger.Warn(0, "1"                                   );
                    logger.Warn(0, "1 {0}"                , 2            );
                    logger.Warn(0, "1 {0} {1}"            , 2, 3         );
                    logger.Warn(0, "1 {0} {1} {2}"        , 2, 3, 4      );
                    logger.Warn(0, "1 {0} {1} {2} {3}"    , 2, 3, 4, 5   );
                    logger.Warn(0, "1 {0} {1} {2} {3} {4}", 2, 3, 4, 5, 6);

                    // Error
                    logger.Error(0, "1"                                   );
                    logger.Error(0, "1 {0}"                , 2            );
                    logger.Error(0, "1 {0} {1}"            , 2, 3         );
                    logger.Error(0, "1 {0} {1} {2}"        , 2, 3, 4      );
                    logger.Error(0, "1 {0} {1} {2} {3}"    , 2, 3, 4, 5   );
                    logger.Error(0, "1 {0} {1} {2} {3} {4}", 2, 3, 4, 5, 6);

                    // Fatal
                    logger.Fatal(0, "1"                                   );
                    logger.Fatal(0, "1 {0}"                , 2            );
                    logger.Fatal(0, "1 {0} {1}"            , 2, 3         );
                    logger.Fatal(0, "1 {0} {1} {2}"        , 2, 3, 4      );
                    logger.Fatal(0, "1 {0} {1} {2} {3}"    , 2, 3, 4, 5   );
                    logger.Fatal(0, "1 {0} {1} {2} {3} {4}", 2, 3, 4, 5, 6);

                    // As the minimum level increases, it reduces the number of log entries
                    LogLevel maxLevel = levels[levels.Length - 1];
                    int expectedCount = ((int)maxLevel - (int)minLevel + 1) * 6;
                    Assert.AreEqual(expectedCount, logger.Entries.Count);

                    for (LogLevel level = minLevel; level <= maxLevel; level++)
                    {
                        AssertLogEntry(level, 0L, "1"          , logger.Entries.Dequeue());
                        AssertLogEntry(level, 0L, "1 2"        , logger.Entries.Dequeue());
                        AssertLogEntry(level, 0L, "1 2 3"      , logger.Entries.Dequeue());
                        AssertLogEntry(level, 0L, "1 2 3 4"    , logger.Entries.Dequeue());
                        AssertLogEntry(level, 0L, "1 2 3 4 5"  , logger.Entries.Dequeue());
                        AssertLogEntry(level, 0L, "1 2 3 4 5 6", logger.Entries.Dequeue());
                    }
                }
            }
        }

        [TestMethod]
        public void Log()
        {
            Guid userId = Guid.NewGuid();
            using (MemoryLogger<Guid> logger = new MemoryLogger<Guid>())
            {
                logger.Trace(userId, "Trace");
                logger.Debug(userId, "Debug");
                logger.Info (userId, "Info" );
                logger.Warn (userId, "Warn" );
                logger.Error(userId, "Error");
                logger.Fatal(userId, "Fatal");

                Assert.AreEqual(6, logger.Entries.Count);
                AssertLogEntry(LogLevel.Trace, userId, "Trace", logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Debug, userId, "Debug", logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Info , userId, "Info" , logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Warn , userId, "Warn" , logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Error, userId, "Error", logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Fatal, userId, "Fatal", logger.Entries.Dequeue());
            }
}

        [TestMethod]
        public void LogFormatArg0()
        {
            CultureInfo jaJP = CultureInfo.GetCultureInfo("ja-JP");
            using (MemoryLogger<float> logger = new MemoryLogger<float>(default, jaJP))
            {
                string msg = "{0,8:C}";

                logger.Trace(1F, msg,   4321);
                logger.Debug(2F, msg,  11235);
                logger.Info (3F, msg,  81321);
                logger.Warn (4F, msg,   3455);
                logger.Error(5F, msg,  89144);
                logger.Fatal(6F, msg, 233377);

                Assert.AreEqual(6, logger.Entries.Count);
                AssertLogEntry(LogLevel.Trace, 1F, string.Format(jaJP, msg,   4321), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Debug, 2F, string.Format(jaJP, msg,  11235), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Info , 3F, string.Format(jaJP, msg,  81321), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Warn , 4F, string.Format(jaJP, msg,   3455), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Error, 5F, string.Format(jaJP, msg,  89144), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Fatal, 6F, string.Format(jaJP, msg, 233377), logger.Entries.Dequeue());
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
                logger.Trace("Shop #1", msg, dt.AddDays(0),      823);
                logger.Debug("Shop #2", msg, dt.AddDays(1),     1234);
                logger.Info ("Shop #1", msg, dt.AddDays(2),     5678);
                logger.Warn ("Shop #3", msg, dt.AddDays(3),    91011);
                logger.Error("Shop #3", msg, dt.AddDays(4),   121314);
                logger.Fatal("Shop #1", msg, dt.AddDays(5), 15161718);

                Assert.AreEqual(6, logger.Entries.Count);
                AssertLogEntry(LogLevel.Trace, "Shop #1", string.Format(jaJP, msg, dt.AddDays(0),      823), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Debug, "Shop #2", string.Format(jaJP, msg, dt.AddDays(1),     1234), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Info , "Shop #1", string.Format(jaJP, msg, dt.AddDays(2),     5678), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Warn , "Shop #3", string.Format(jaJP, msg, dt.AddDays(3),    91011), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Error, "Shop #3", string.Format(jaJP, msg, dt.AddDays(4),   121314), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Fatal, "Shop #1", string.Format(jaJP, msg, dt.AddDays(5), 15161718), logger.Entries.Dequeue());
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
                logger.Trace(1L, boughtMsg, dt.AddDays(0),    0.42,    13.37);
                logger.Debug(2L, boughtMsg, dt.AddDays(1),   24.68,    -0.25);
                logger.Info (1L, soldMsg  , dt.AddDays(2),   10.12,     1.33);
                logger.Warn (3L, soldMsg  , dt.AddDays(3), 1416.00, 11975.31);
                logger.Error(3L, boughtMsg, dt.AddDays(4),   18.20,   -10.00);
                logger.Fatal(1L, soldMsg  , dt.AddDays(5), 2224.00,   115.00);

                Assert.AreEqual(6, logger.Entries.Count);
                AssertLogEntry(LogLevel.Trace, 1L, string.Format(esES, boughtMsg, dt.AddDays(0),    0.42,    13.37), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Debug, 2L, string.Format(esES, boughtMsg, dt.AddDays(1),   24.68,    -0.25), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Info , 1L, string.Format(esES, soldMsg  , dt.AddDays(2),   10.12,     1.33), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Warn , 3L, string.Format(esES, soldMsg  , dt.AddDays(3), 1416.00, 11975.31), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Error, 3L, string.Format(esES, boughtMsg, dt.AddDays(4),   18.20,   -10.00), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Fatal, 1L, string.Format(esES, soldMsg  , dt.AddDays(5), 2224.00,   115.00), logger.Entries.Dequeue());
            }
}

        [TestMethod]
        public void LogFormatArg0Arg1Arg2Arg3()
        {
            CultureInfo esES = CultureInfo.GetCultureInfo("es-ES");
            using (MemoryLogger<long> logger = new MemoryLogger<long>(default, esES))
            {
                string boughtMsg = "On {0:d}, bought {1,7:F2} units for {2,11:C} (Total = {3,15:C})";
                string soldMsg   = "On {0:d}, sold   {1,7:F2} units for {2,11:C} (Total = {3,15:C})";

                DateTime dt = new DateTime(2019, 01, 15);
                logger.Trace(1L, boughtMsg, dt.AddDays(0),    0.42,    13.37,        5.6154);
                logger.Debug(2L, boughtMsg, dt.AddDays(1),   24.68,    -0.25,       -6.1700);
                logger.Info (1L, soldMsg  , dt.AddDays(2),   10.12,     1.33,       13.4596);
                logger.Warn (3L, soldMsg  , dt.AddDays(3), 1416.00, 11975.31, 16957039.0000);
                logger.Error(3L, boughtMsg, dt.AddDays(4),   18.20,   -10.00,     -182.0000);
                logger.Fatal(1L, soldMsg  , dt.AddDays(5), 2224.00,   115.00,   255760.0000);

                Assert.AreEqual(6, logger.Entries.Count);
                AssertLogEntry(LogLevel.Trace, 1L, string.Format(esES, boughtMsg, dt.AddDays(0),    0.42,    13.37,        5.6154), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Debug, 2L, string.Format(esES, boughtMsg, dt.AddDays(1),   24.68,    -0.25,       -6.1700), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Info , 1L, string.Format(esES, soldMsg  , dt.AddDays(2),   10.12,     1.33,       13.4596), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Warn , 3L, string.Format(esES, soldMsg  , dt.AddDays(3), 1416.00, 11975.31, 16957039.0000), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Error, 3L, string.Format(esES, boughtMsg, dt.AddDays(4),   18.20,   -10.00,     -182.0000), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Fatal, 1L, string.Format(esES, soldMsg  , dt.AddDays(5), 2224.00,   115.00,   255760.0000), logger.Entries.Dequeue());
            }
}

        [TestMethod]
        public void LogFormatParams()
        {
            CultureInfo esES = CultureInfo.GetCultureInfo("es-ES");
            using (MemoryLogger<long> logger = new MemoryLogger<long>(default, esES))
            {
                string boughtMsg = "On {0:d} {1:g}, bought {2,7:F2} units for {3,11:C} (Total = {4,15:C})";
                string soldMsg   = "On {0:d} {1:g}, sold   {2,7:F2} units for {3,11:C} (Total = {4,15:C})";

                DateTime dt = new DateTime(2019, 01, 15, 16, 17, 18, 19);
                logger.Trace(1L, boughtMsg, dt.AddDays(0), dt.AddHours(0).TimeOfDay,    0.42,    13.37,        5.6154);
                logger.Debug(2L, boughtMsg, dt.AddDays(1), dt.AddHours(1).TimeOfDay,   24.68,    -0.25,       -6.1700);
                logger.Info (1L, soldMsg  , dt.AddDays(2), dt.AddHours(2).TimeOfDay,   10.12,     1.33,       13.4596);
                logger.Warn (3L, soldMsg  , dt.AddDays(3), dt.AddHours(3).TimeOfDay, 1416.00, 11975.31, 16957039.0000);
                logger.Error(3L, boughtMsg, dt.AddDays(4), dt.AddHours(4).TimeOfDay,   18.20,   -10.00,     -182.0000);
                logger.Fatal(1L, soldMsg  , dt.AddDays(5), dt.AddHours(5).TimeOfDay, 2224.00,   115.00,   255760.0000);
                 
                Assert.AreEqual(6, logger.Entries.Count);
                AssertLogEntry(LogLevel.Trace, 1L, string.Format(esES, boughtMsg, dt.AddDays(0), dt.AddHours(0).TimeOfDay,    0.42,    13.37,        5.6154), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Debug, 2L, string.Format(esES, boughtMsg, dt.AddDays(1), dt.AddHours(1).TimeOfDay,   24.68,    -0.25,       -6.1700), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Info , 1L, string.Format(esES, soldMsg  , dt.AddDays(2), dt.AddHours(2).TimeOfDay,   10.12,     1.33,       13.4596), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Warn , 3L, string.Format(esES, soldMsg  , dt.AddDays(3), dt.AddHours(3).TimeOfDay, 1416.00, 11975.31, 16957039.0000), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Error, 3L, string.Format(esES, boughtMsg, dt.AddDays(4), dt.AddHours(4).TimeOfDay,   18.20,   -10.00,     -182.0000), logger.Entries.Dequeue());
                AssertLogEntry(LogLevel.Fatal, 1L, string.Format(esES, soldMsg  , dt.AddDays(5), dt.AddHours(5).TimeOfDay, 2224.00,   115.00,   255760.0000), logger.Entries.Dequeue());
            }
}

        private static void AssertLogEntry<T>(LogLevel expectedLevel, T expectedContext, string expectedMessage, LogEntry<T> actual)
        {
            // We assert the validity of the time in the TemplateBuilder.Test.cs, so here we'll
            // instead assert that the value is a time in the appropriate format
            Assert.AreNotEqual(default        , actual.Timestamp);
            Assert.AreEqual   (expectedLevel  , actual.Level    );
            Assert.AreEqual   (expectedContext, actual.Context  );
            Assert.AreEqual   (expectedMessage, actual.Message  );
        }
    }
}
