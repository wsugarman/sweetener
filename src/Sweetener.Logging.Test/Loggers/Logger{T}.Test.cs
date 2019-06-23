using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Logging.Test
{
    [TestClass]
    public class GenericLoggerTest
    {
        [TestMethod]
        public void Constructor()
        {
            // Argument Validation
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MemoryLogger<int>((LogLevel)27));

            // Constructor Overloads
            using (Logger<int> logger = new MemoryLogger<int>())
                Assert.AreEqual(LogLevel.Trace, logger.MinLevel);

            using (Logger<int> logger = new MemoryLogger<int>(LogLevel.Warn))
                Assert.AreEqual(LogLevel.Warn, logger.MinLevel);
        }

        [TestMethod]
        public void IsSynchronized()
        {
            Assert.IsFalse(new MemoryLogger<DateTime>(             ).IsSynchronized);
            Assert.IsFalse(new MemoryLogger<DateTime>(LogLevel.Info).IsSynchronized);
        }

        [TestMethod]
        public void SyncRoot()
        {
            Logger<double> logger0 = new MemoryLogger<double>();
            Logger<double> logger1 = new MemoryLogger<double>(LogLevel.Info);

            Assert.IsNotNull(logger0.SyncRoot);
            Assert.IsNotNull(logger1.SyncRoot);

            Assert.AreEqual(typeof(object), logger0.SyncRoot.GetType());
            Assert.AreEqual(typeof(object), logger1.SyncRoot.GetType());
        }

        [TestMethod]
        public void LogThrowIfDisposed()
        {
            Logger<int> logger = new MemoryLogger<int>();
            logger.Dispose();

            Assert.ThrowsException<ObjectDisposedException>(() => logger.Trace(0));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Debug(1));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Info (1));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Warn (2));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Error(3));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Fatal(5));
        }

        [TestMethod]
        public void IgnoreBelowMinLevel()
        {
            foreach (LogLevel minLevel in (LogLevel[])Enum.GetValues(typeof(LogLevel)))
            {
                using (MemoryLogger<char> logger = new MemoryLogger<char>(minLevel))
                {
                    logger.Trace('#');
                    logger.Debug('#');
                    logger.Info ('#');
                    logger.Warn ('#');
                    logger.Error('#');
                    logger.Fatal('#');

                    // As the minimum level increases, it reduces the number of log entries
                    int expectedCount = Enum.GetValues(typeof(LogLevel)).Length - (int)minLevel;
                    Assert.AreEqual(expectedCount, logger.Entries.Count);

                    for (LogLevel level = minLevel; logger.Entries.Count > 0; level++)
                        AssertLogEntry(level, '#', logger.Entries.Dequeue());
                }
            }
        }

        [TestMethod]
        public void Log()
        {
            using (MemoryLogger<string> logger = new MemoryLogger<string>())
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

        internal static void AssertLogEntry<T>(LogLevel expectedLevel, T expectedMessage, LogEntry<T> actual)
        {
            // We assert the validity of the time in the TemplateBuilder.Test.cs, so here we'll
            // instead assert that the value is a time in the appropriate format
            Assert.AreNotEqual(default        , actual.Timestamp);
            Assert.AreEqual   (expectedLevel  , actual.Level    );
            Assert.AreEqual   (expectedMessage, actual.Message  );
        }
    }
}
