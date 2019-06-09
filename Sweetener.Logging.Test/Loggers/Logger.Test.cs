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
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MemoryLogger((LogLevel)27));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MemoryLogger((LogLevel)27, null));

            // Constructor Overloads
            CultureInfo frenchFrench = CultureInfo.GetCultureInfo("fr-FR");

            using (MemoryLogger logger = new MemoryLogger())
            {
                Assert.AreEqual(LogLevel.Trace            , logger.MinimumLevel  );
                Assert.AreEqual(CultureInfo.CurrentCulture, logger.FormatProvider);
            }

            using (MemoryLogger logger = new MemoryLogger(LogLevel.Warn))
            {
                Assert.AreEqual(LogLevel.Warn             , logger.MinimumLevel  );
                Assert.AreEqual(CultureInfo.CurrentCulture, logger.FormatProvider);
            }

            using (MemoryLogger logger = new MemoryLogger(LogLevel.Info, frenchFrench))
            {
                Assert.AreEqual(LogLevel.Info, logger.MinimumLevel  );
                Assert.AreEqual(frenchFrench , logger.FormatProvider);
            }
        }

        [TestMethod]
        public void LogThrowIfDisposed()
        {
            MemoryLogger logger = new MemoryLogger();
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
            using (MemoryLogger logger = new MemoryLogger())
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
            using (MemoryLogger logger = new MemoryLogger())
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
        public void IgnoreBelowMinimumLevel()
        {
            foreach (LogLevel minimumLevel in (LogLevel[])Enum.GetValues(typeof(LogLevel)))
            {
                using (MemoryLogger logger = new MemoryLogger(minimumLevel))
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
                    int expectedCount = (logLevelCount - (int)minimumLevel) * logLevelCount;
                    Assert.AreEqual(expectedCount, logger.LogQueue.Count);

                    LogLevel level = minimumLevel;
                    while (logger.LogQueue.Count > 0)
                    {
                        Assert.IsTrue(logger.LogQueue.Count >= 6);
                        AssertLogEntry(level, "0"          , logger.LogQueue.Dequeue());
                        AssertLogEntry(level, "0 1"        , logger.LogQueue.Dequeue());
                        AssertLogEntry(level, "0 1 2"      , logger.LogQueue.Dequeue());
                        AssertLogEntry(level, "0 1 2 3"    , logger.LogQueue.Dequeue());
                        AssertLogEntry(level, "0 1 2 3 4"  , logger.LogQueue.Dequeue());
                        AssertLogEntry(level, "0 1 2 3 4 5", logger.LogQueue.Dequeue());

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

                Assert.AreEqual(6, logger.LogQueue.Count);
                AssertLogEntry(LogLevel.Trace, "Trace", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Debug, "Debug", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Info , "Info" , logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Warn , "Warn" , logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Error, "Error", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Fatal, "Fatal", logger.LogQueue.Dequeue());
            }
        }

        [TestMethod]
        public void LogFormatArg0()
        {
            using (MemoryLogger logger = new MemoryLogger(default, CultureInfo.GetCultureInfo("ja-JP")))
            {
                logger.Trace("{0,8:C}",   4321);
                logger.Debug("{0,8:C}",  11235);
                logger.Info ("{0,8:C}",  81321);
                logger.Warn ("{0,8:C}",   3455);
                logger.Error("{0,8:C}",  89144);
                logger.Fatal("{0,8:C}", 233377);

                Assert.AreEqual(6, logger.LogQueue.Count);
                AssertLogEntry(LogLevel.Trace, "  ¥4,321", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Debug, " ¥11,235", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Info , " ¥81,321", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Warn , "  ¥3,455", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Error, " ¥89,144", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Fatal, "¥233,377", logger.LogQueue.Dequeue());
            }
        }

        [TestMethod]
        public void LogFormatArg0Arg1()
        {
            using (MemoryLogger logger = new MemoryLogger(default, CultureInfo.GetCultureInfo("ja-JP")))
            {
                DateTime dt = new DateTime(2019, 01, 15);
                logger.Trace("On {0:d}, made {1,11:C}", dt.AddDays(0),      823);
                logger.Debug("On {0:d}, made {1,11:C}", dt.AddDays(1),     1234);
                logger.Info ("On {0:d}, made {1,11:C}", dt.AddDays(2),     5678);
                logger.Warn ("On {0:d}, made {1,11:C}", dt.AddDays(3),    91011);
                logger.Error("On {0:d}, made {1,11:C}", dt.AddDays(4),   121314);
                logger.Fatal("On {0:d}, made {1,11:C}", dt.AddDays(5), 15161718);

                Assert.AreEqual(6, logger.LogQueue.Count);
                AssertLogEntry(LogLevel.Trace, "On 2019/01/15, made        ¥823", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Debug, "On 2019/01/16, made      ¥1,234", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Info , "On 2019/01/17, made      ¥5,678", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Warn , "On 2019/01/18, made     ¥91,011", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Error, "On 2019/01/19, made    ¥121,314", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Fatal, "On 2019/01/20, made ¥15,161,718", logger.LogQueue.Dequeue());
            }
        }

        [TestMethod]
        public void LogFormatArg0Arg1Arg2()
        {
            using (MemoryLogger logger = new MemoryLogger(default, CultureInfo.GetCultureInfo("es-ES")))
            {
                DateTime dt = new DateTime(2019, 01, 15);
                logger.Trace("On {0:d}, bought {1,7:F2} units for {2,11:C}", dt.AddDays(0),    0.42,    13.37);
                logger.Debug("On {0:d}, bought {1,7:F2} units for {2,11:C}", dt.AddDays(1),   24.68,    -0.25);
                logger.Info ("On {0:d}, sold   {1,7:F2} units for {2,11:C}", dt.AddDays(2),   10.12,     1.33);
                logger.Warn ("On {0:d}, sold   {1,7:F2} units for {2,11:C}", dt.AddDays(3), 1416.00, 11975.31);
                logger.Error("On {0:d}, bought {1,7:F2} units for {2,11:C}", dt.AddDays(4),   18.20,   -10.00);
                logger.Fatal("On {0:d}, sold   {1,7:F2} units for {2,11:C}", dt.AddDays(5), 2224.00,   115.00);

                Assert.AreEqual(6, logger.LogQueue.Count);
                AssertLogEntry(LogLevel.Trace, "On 15/01/2019, bought    0,42 units for     13,37 €", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Debug, "On 16/01/2019, bought   24,68 units for     -0,25 €", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Info , "On 17/01/2019, sold     10,12 units for      1,33 €", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Warn , "On 18/01/2019, sold   1416,00 units for 11.975,31 €", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Error, "On 19/01/2019, bought   18,20 units for    -10,00 €", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Fatal, "On 20/01/2019, sold   2224,00 units for    115,00 €", logger.LogQueue.Dequeue());
            }
        }

        [TestMethod]
        public void LogFormatArg0Arg1Arg2Arg3()
        {
            using (MemoryLogger logger = new MemoryLogger(default, CultureInfo.GetCultureInfo("es-ES")))
            {
                DateTime dt = new DateTime(2019, 01, 15);
                logger.Trace("On {0:d}, bought {1,7:F2} units for {2,11:C} (Total = {3,15:C})", dt.AddDays(0),    0.42,    13.37,        5.6154);
                logger.Debug("On {0:d}, bought {1,7:F2} units for {2,11:C} (Total = {3,15:C})", dt.AddDays(1),   24.68,    -0.25,       -6.1700);
                logger.Info ("On {0:d}, sold   {1,7:F2} units for {2,11:C} (Total = {3,15:C})", dt.AddDays(2),   10.12,     1.33,       13.4596);
                logger.Warn ("On {0:d}, sold   {1,7:F2} units for {2,11:C} (Total = {3,15:C})", dt.AddDays(3), 1416.00, 11975.31, 16957039.0000);
                logger.Error("On {0:d}, bought {1,7:F2} units for {2,11:C} (Total = {3,15:C})", dt.AddDays(4),   18.20,   -10.00,     -182.0000);
                logger.Fatal("On {0:d}, sold   {1,7:F2} units for {2,11:C} (Total = {3,15:C})", dt.AddDays(5), 2224.00,   115.00,   255760.0000);

                Assert.AreEqual(6, logger.LogQueue.Count);
                AssertLogEntry(LogLevel.Trace, "On 15/01/2019, bought    0,42 units for     13,37 € (Total =          5,62 €)", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Debug, "On 16/01/2019, bought   24,68 units for     -0,25 € (Total =         -6,17 €)", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Info , "On 17/01/2019, sold     10,12 units for      1,33 € (Total =         13,46 €)", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Warn , "On 18/01/2019, sold   1416,00 units for 11.975,31 € (Total = 16.957.039,00 €)", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Error, "On 19/01/2019, bought   18,20 units for    -10,00 € (Total =       -182,00 €)", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Fatal, "On 20/01/2019, sold   2224,00 units for    115,00 € (Total =    255.760,00 €)", logger.LogQueue.Dequeue());
            }
        }

        [TestMethod]
        public void LogFormatParams()
        {
            using (MemoryLogger logger = new MemoryLogger(default, CultureInfo.GetCultureInfo("es-ES")))
            {
                DateTime dt = new DateTime(2019, 01, 15, 16, 17, 18, 19);
                logger.Trace("On {0:d} {1:g}, bought {2,7:F2} units for {3,11:C} (Total = {4,15:C})", dt.AddDays(0), dt.AddHours(0).TimeOfDay,    0.42,    13.37,        5.6154);
                logger.Debug("On {0:d} {1:g}, bought {2,7:F2} units for {3,11:C} (Total = {4,15:C})", dt.AddDays(1), dt.AddHours(1).TimeOfDay,   24.68,    -0.25,       -6.1700);
                logger.Info ("On {0:d} {1:g}, sold   {2,7:F2} units for {3,11:C} (Total = {4,15:C})", dt.AddDays(2), dt.AddHours(2).TimeOfDay,   10.12,     1.33,       13.4596);
                logger.Warn ("On {0:d} {1:g}, sold   {2,7:F2} units for {3,11:C} (Total = {4,15:C})", dt.AddDays(3), dt.AddHours(3).TimeOfDay, 1416.00, 11975.31, 16957039.0000);
                logger.Error("On {0:d} {1:g}, bought {2,7:F2} units for {3,11:C} (Total = {4,15:C})", dt.AddDays(4), dt.AddHours(4).TimeOfDay,   18.20,   -10.00,     -182.0000);
                logger.Fatal("On {0:d} {1:g}, sold   {2,7:F2} units for {3,11:C} (Total = {4,15:C})", dt.AddDays(5), dt.AddHours(5).TimeOfDay, 2224.00,   115.00,   255760.0000);

                Assert.AreEqual(6, logger.LogQueue.Count);
                AssertLogEntry(LogLevel.Trace, "On 15/01/2019 16:17:18,019, bought    0,42 units for     13,37 € (Total =          5,62 €)", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Debug, "On 16/01/2019 17:17:18,019, bought   24,68 units for     -0,25 € (Total =         -6,17 €)", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Info , "On 17/01/2019 18:17:18,019, sold     10,12 units for      1,33 € (Total =         13,46 €)", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Warn , "On 18/01/2019 19:17:18,019, sold   1416,00 units for 11.975,31 € (Total = 16.957.039,00 €)", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Error, "On 19/01/2019 20:17:18,019, bought   18,20 units for    -10,00 € (Total =       -182,00 €)", logger.LogQueue.Dequeue());
                AssertLogEntry(LogLevel.Fatal, "On 20/01/2019 21:17:18,019, sold   2224,00 units for    115,00 € (Total =    255.760,00 €)", logger.LogQueue.Dequeue());
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
