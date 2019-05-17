using System;
using System.Globalization;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Logging.Test
{
    [TestClass]
    public partial class LoggerTest
    {
        [TestMethod]
        public void ConstructorDefaults()
        {
            MemoryLogger logger;
            CultureInfo frenchFrench   = CultureInfo.GetCultureInfo("fr-FR");
            CultureInfo spanishSpanish = CultureInfo.GetCultureInfo("es-ES");

            logger = new MemoryLogger();
            logger.Fatal("Some Message");
            AssertDefaultLogMessage(LogLevel.Fatal, "Some Message", logger.Log.Dequeue());
            Assert.AreEqual(LogLevel.Debug            , logger.MinimumLevel  );
            Assert.AreEqual(CultureInfo.CurrentCulture, logger.FormatProvider);

            logger = new MemoryLogger(LogLevel.Warn);
            logger.Fatal("Another Message");
            AssertDefaultLogMessage(LogLevel.Fatal, "Another Message", logger.Log.Dequeue());
            Assert.AreEqual(LogLevel.Warn             , logger.MinimumLevel  );
            Assert.AreEqual(CultureInfo.CurrentCulture, logger.FormatProvider);

            logger = new MemoryLogger(LogLevel.Info, frenchFrench);
            logger.Fatal("Yet Another Message");
            AssertDefaultLogMessage(LogLevel.Fatal, "Yet Another Message", logger.Log.Dequeue());
            Assert.AreEqual(LogLevel.Info, logger.MinimumLevel  );
            Assert.AreEqual(frenchFrench , logger.FormatProvider);

            logger = new MemoryLogger(LogLevel.Error, spanishSpanish, "[{tid}] {msg}");
            logger.Fatal("Final Message");
            Assert.AreEqual($"[{Thread.CurrentThread.ManagedThreadId}] Final Message", logger.Log.Dequeue());
            Assert.AreEqual(LogLevel.Error, logger.MinimumLevel  );
            Assert.AreEqual(spanishSpanish  , logger.FormatProvider);
        }

        [TestMethod]
        public void LogThrowIfDisposed()
        {
            MemoryLogger logger = new MemoryLogger();
            logger.Dispose();

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
            MemoryLogger logger = new MemoryLogger();
            object[]     args   = null;

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

        [TestMethod]
        public void LogThrowBadFormat()
        {
            // Use an unknown format specifier for numbers
            MemoryLogger logger = new MemoryLogger();

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

        [TestMethod]
        public void IgnoreBelowMinimumLevel()
        {
            MemoryLogger logger;
            foreach (LogLevel minimumLevel in (LogLevel[])Enum.GetValues(typeof(LogLevel)))
            {
                logger = new MemoryLogger(minimumLevel);

                // Debug
                logger.Debug("1 2 3 4 5"                          );
                logger.Debug("{0}"                 , 1            );
                logger.Debug("{0} {1}"             , 1, 2         );
                logger.Debug("{0} {1} {2}"         , 1, 2, 3      );
                logger.Debug("{0} {1} {2} {3}"     , 1, 2, 3, 4   );
                logger.Debug("{0} {1} {2} {3}, {4}", 1, 2, 3, 4, 5);

                // Info
                logger.Info("1 2 3 4 5"                          );
                logger.Info("{0}"                 , 1            );
                logger.Info("{0} {1}"             , 1, 2         );
                logger.Info("{0} {1} {2}"         , 1, 2, 3      );
                logger.Info("{0} {1} {2} {3}"     , 1, 2, 3, 4   );
                logger.Info("{0} {1} {2} {3}, {4}", 1, 2, 3, 4, 5);

                // Warn
                logger.Warn("1 2 3 4 5"                          );
                logger.Warn("{0}"                 , 1            );
                logger.Warn("{0} {1}"             , 1, 2         );
                logger.Warn("{0} {1} {2}"         , 1, 2, 3      );
                logger.Warn("{0} {1} {2} {3}"     , 1, 2, 3, 4   );
                logger.Warn("{0} {1} {2} {3}, {4}", 1, 2, 3, 4, 5);

                // Error
                logger.Error("1 2 3 4 5"                          );
                logger.Error("{0}"                 , 1            );
                logger.Error("{0} {1}"             , 1, 2         );
                logger.Error("{0} {1} {2}"         , 1, 2, 3      );
                logger.Error("{0} {1} {2} {3}"     , 1, 2, 3, 4   );
                logger.Error("{0} {1} {2} {3}, {4}", 1, 2, 3, 4, 5);

                // Fatal
                logger.Fatal("1 2 3 4 5"                          );
                logger.Fatal("{0}"                 , 1            );
                logger.Fatal("{0} {1}"             , 1, 2         );
                logger.Fatal("{0} {1} {2}"         , 1, 2, 3      );
                logger.Fatal("{0} {1} {2} {3}"     , 1, 2, 3, 4   );
                logger.Fatal("{0} {1} {2} {3}, {4}", 1, 2, 3, 4, 5);

                // As the minimum level increases, it reduces the number of log entries
                int expectedCount = (5 - (int)minimumLevel) * 6;
                Assert.AreEqual(expectedCount, logger.Log.Count);
            }
        }

        [TestMethod]
        public void LogMessage()
        {
            MemoryLogger logger = new MemoryLogger(LogLevel.Debug, CultureInfo.InvariantCulture, "{msg} @ {l:F}");

            logger.Debug("Debug");
            logger.Info ("Info" );
            logger.Warn ("Warn" );
            logger.Error("Error");
            logger.Fatal("Fatal");

            Assert.AreEqual(5, logger.Log.Count);
            Assert.AreEqual("Debug @ Debug", logger.Log.Dequeue());
            Assert.AreEqual("Info @ Info"  , logger.Log.Dequeue());
            Assert.AreEqual("Warn @ Warn"  , logger.Log.Dequeue());
            Assert.AreEqual("Error @ Error", logger.Log.Dequeue());
            Assert.AreEqual("Fatal @ Fatal", logger.Log.Dequeue());
        }

        [TestMethod]
        public void LogFormatArg0()
        {
            MemoryLogger logger = new MemoryLogger(LogLevel.Debug, CultureInfo.GetCultureInfo("ja-JP"), "{msg} @ {l:F}");

            logger.Debug("{0,8:C}", 11235 );
            logger.Info ("{0,8:C}", 81321 );
            logger.Warn ("{0,8:C}", 3455  );
            logger.Error("{0,8:C}", 89144 );
            logger.Fatal("{0,8:C}", 233377);

            Assert.AreEqual(5, logger.Log.Count);
            Assert.AreEqual(" ¥11,235 @ Debug", logger.Log.Dequeue());
            Assert.AreEqual(" ¥81,321 @ Info" , logger.Log.Dequeue());
            Assert.AreEqual("  ¥3,455 @ Warn" , logger.Log.Dequeue());
            Assert.AreEqual(" ¥89,144 @ Error", logger.Log.Dequeue());
            Assert.AreEqual("¥233,377 @ Fatal", logger.Log.Dequeue());
        }

        [TestMethod]
        public void LogFormatArg0Arg1()
        {
            MemoryLogger logger = new MemoryLogger(LogLevel.Debug, CultureInfo.GetCultureInfo("ja-JP"), "{msg} @ {l:F}");

            DateTime dateTime = new DateTime(2019, 01, 15);
            logger.Debug("On {0:d}, made {1,11:C}", dateTime.AddDays(0),     1234);
            logger.Info ("On {0:d}, made {1,11:C}", dateTime.AddDays(1),     5678);
            logger.Warn ("On {0:d}, made {1,11:C}", dateTime.AddDays(2),    91011);
            logger.Error("On {0:d}, made {1,11:C}", dateTime.AddDays(3),   121314);
            logger.Fatal("On {0:d}, made {1,11:C}", dateTime.AddDays(4), 15161718);

            Assert.AreEqual(5, logger.Log.Count);
            Assert.AreEqual("On 2019/01/15, made      ¥1,234 @ Debug", logger.Log.Dequeue());
            Assert.AreEqual("On 2019/01/16, made      ¥5,678 @ Info" , logger.Log.Dequeue());
            Assert.AreEqual("On 2019/01/17, made     ¥91,011 @ Warn" , logger.Log.Dequeue());
            Assert.AreEqual("On 2019/01/18, made    ¥121,314 @ Error", logger.Log.Dequeue());
            Assert.AreEqual("On 2019/01/19, made ¥15,161,718 @ Fatal", logger.Log.Dequeue());
        }

        [TestMethod]
        public void LogFormatArg0Arg1Arg2()
        {
            MemoryLogger logger = new MemoryLogger(LogLevel.Debug, CultureInfo.GetCultureInfo("es-ES"), "{msg} @ {l:F}");

            DateTime dateTime = new DateTime(2019, 01, 15);
            logger.Debug("On {0:d}, bought {1,7:F2} units for {2,11:C}", dateTime.AddDays(0),   24.68,    -0.25);
            logger.Info ("On {0:d}, sold   {1,7:F2} units for {2,11:C}", dateTime.AddDays(1),   10.12,     1.33);
            logger.Warn ("On {0:d}, sold   {1,7:F2} units for {2,11:C}", dateTime.AddDays(2), 1416   , 11975.31);
            logger.Error("On {0:d}, bought {1,7:F2} units for {2,11:C}", dateTime.AddDays(3),   18.20,   -10   );
            logger.Fatal("On {0:d}, sold   {1,7:F2} units for {2,11:C}", dateTime.AddDays(4), 2224   ,   115   );

            Assert.AreEqual(5, logger.Log.Count);
            Assert.AreEqual("On 15/01/2019, bought   24,68 units for     -0,25 € @ Debug", logger.Log.Dequeue());
            Assert.AreEqual("On 16/01/2019, sold     10,12 units for      1,33 € @ Info" , logger.Log.Dequeue());
            Assert.AreEqual("On 17/01/2019, sold   1416,00 units for 11.975,31 € @ Warn" , logger.Log.Dequeue());
            Assert.AreEqual("On 18/01/2019, bought   18,20 units for    -10,00 € @ Error", logger.Log.Dequeue());
            Assert.AreEqual("On 19/01/2019, sold   2224,00 units for    115,00 € @ Fatal", logger.Log.Dequeue());
        }

        [TestMethod]
        public void LogFormatArg0Arg1Arg2Arg3()
        {
            MemoryLogger logger = new MemoryLogger(LogLevel.Debug, CultureInfo.GetCultureInfo("es-ES"), "{msg} @ {l:F}");

            DateTime dateTime = new DateTime(2019, 01, 15);
            logger.Debug("On {0:d}, bought {1,7:F2} units for {2,11:C} (Total = {3,15:C})", dateTime.AddDays(0),   24.68,    -0.25,       -6.1700);
            logger.Info ("On {0:d}, sold   {1,7:F2} units for {2,11:C} (Total = {3,15:C})", dateTime.AddDays(1),   10.12,     1.33,       13.4596);
            logger.Warn ("On {0:d}, sold   {1,7:F2} units for {2,11:C} (Total = {3,15:C})", dateTime.AddDays(2), 1416.00, 11975.31, 16957039.0000);
            logger.Error("On {0:d}, bought {1,7:F2} units for {2,11:C} (Total = {3,15:C})", dateTime.AddDays(3),   18.20,   -10.00,     -182.0000);
            logger.Fatal("On {0:d}, sold   {1,7:F2} units for {2,11:C} (Total = {3,15:C})", dateTime.AddDays(4), 2224.00,   115.00,   255760.0000);

            Assert.AreEqual(5, logger.Log.Count);
            Assert.AreEqual("On 15/01/2019, bought   24,68 units for     -0,25 € (Total =         -6,17 €) @ Debug", logger.Log.Dequeue());
            Assert.AreEqual("On 16/01/2019, sold     10,12 units for      1,33 € (Total =         13,46 €) @ Info" , logger.Log.Dequeue());
            Assert.AreEqual("On 17/01/2019, sold   1416,00 units for 11.975,31 € (Total = 16.957.039,00 €) @ Warn" , logger.Log.Dequeue());
            Assert.AreEqual("On 18/01/2019, bought   18,20 units for    -10,00 € (Total =       -182,00 €) @ Error", logger.Log.Dequeue());
            Assert.AreEqual("On 19/01/2019, sold   2224,00 units for    115,00 € (Total =    255.760,00 €) @ Fatal", logger.Log.Dequeue());
        }

        [TestMethod]
        public void LogFormatParams()
        {
            MemoryLogger logger = new MemoryLogger(LogLevel.Debug, CultureInfo.GetCultureInfo("es-ES"), "{msg} @ {l:F}");

            DateTime dt = new DateTime(2019, 01, 15, 16, 17, 18, 19);
            logger.Debug("On {0:d} {1:g}, bought {2,7:F2} units for {3,11:C} (Total = {4,15:C})", dt.AddDays(0), dt.AddHours(0).TimeOfDay,   24.68,    -0.25,       -6.1700);
            logger.Info ("On {0:d} {1:g}, sold   {2,7:F2} units for {3,11:C} (Total = {4,15:C})", dt.AddDays(1), dt.AddHours(1).TimeOfDay,   10.12,     1.33,       13.4596);
            logger.Warn ("On {0:d} {1:g}, sold   {2,7:F2} units for {3,11:C} (Total = {4,15:C})", dt.AddDays(2), dt.AddHours(2).TimeOfDay, 1416.00, 11975.31, 16957039.0000);
            logger.Error("On {0:d} {1:g}, bought {2,7:F2} units for {3,11:C} (Total = {4,15:C})", dt.AddDays(3), dt.AddHours(3).TimeOfDay,   18.20,   -10.00,     -182.0000);
            logger.Fatal("On {0:d} {1:g}, sold   {2,7:F2} units for {3,11:C} (Total = {4,15:C})", dt.AddDays(4), dt.AddHours(4).TimeOfDay, 2224.00,   115.00,   255760.0000);

            Assert.AreEqual(5, logger.Log.Count);
            Assert.AreEqual("On 15/01/2019 16:17:18,019, bought   24,68 units for     -0,25 € (Total =         -6,17 €) @ Debug", logger.Log.Dequeue());
            Assert.AreEqual("On 16/01/2019 17:17:18,019, sold     10,12 units for      1,33 € (Total =         13,46 €) @ Info" , logger.Log.Dequeue());
            Assert.AreEqual("On 17/01/2019 18:17:18,019, sold   1416,00 units for 11.975,31 € (Total = 16.957.039,00 €) @ Warn" , logger.Log.Dequeue());
            Assert.AreEqual("On 18/01/2019 19:17:18,019, bought   18,20 units for    -10,00 € (Total =       -182,00 €) @ Error", logger.Log.Dequeue());
            Assert.AreEqual("On 19/01/2019 20:17:18,019, sold   2224,00 units for    115,00 € (Total =    255.760,00 €) @ Fatal", logger.Log.Dequeue());
        }

        private static void AssertDefaultLogMessage(LogLevel expectedLevel, string expectedMessage, string logEntry)
        {
            // We assert the validity of the time in the LogPattern.Test.cs, so here we'll
            // instead assert that the value is a time in the appropriate format
            DateTime timestamp = DateTime.ParseExact(logEntry.Substring(1, logEntry.IndexOf(']') - 1), "O", null);
            Assert.AreNotEqual(default, timestamp);

            Assert.AreEqual($"[{expectedLevel,-5:F}] {expectedMessage}", logEntry.Substring(logEntry.IndexOf(']') + 2));
        }
    }
}
