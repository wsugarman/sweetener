using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Logging.Test
{
    [TestClass]
    public class LogEntryHelperTest
    {
        [TestMethod]
        public void CreateExceptions()
        {
            DateTime dt = new DateTime(1999, 12, 31);

            // LogLevel out-of-range
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => LogEntry.Create(    (LogLevel)(-1), "Foo"         ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => LogEntry.Create(dt, (LogLevel)(-1), "Foo"         ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => LogEntry.Create(    (LogLevel)42  , Guid.NewGuid()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => LogEntry.Create(dt, (LogLevel)42  , Guid.NewGuid()));
        }

        [TestMethod]
        public void Create()
        {
            Task[] tasks = new Task[5];
            for (int i = 0; i < tasks.Length; i++)
            {
                string threadName = $"Task #{i}";
                tasks[i] = Task.Run(() =>
                {
                    DateTime min;
                    Thread thread = Thread.CurrentThread;
                    lock (thread)
                    {
                        if (thread.Name == null)
                            thread.Name = threadName;
                    }

                    int    tid = thread.ManagedThreadId;
                    string tn  = thread.Name;

                    // Various message types
                    min = DateTime.UtcNow;
                    LogEntry<string> stringEntry = LogEntry.Create(LogLevel.Warn, "Foo");
                    AssertLogEntry(tid, tn, min, DateTime.UtcNow, LogLevel.Warn, "Foo", stringEntry);

                    min = DateTime.UtcNow;
                    LogEntry<int> intEntry = LogEntry.Create(LogLevel.Fatal, 42);
                    AssertLogEntry(tid, tn, min, DateTime.UtcNow, LogLevel.Fatal, 42, intEntry);

                    min = DateTime.UtcNow;
                    LogEntry<LogEntryHelperTest> testEntry = LogEntry.Create(LogLevel.Debug, this);
                    AssertLogEntry(tid, tn, min, DateTime.UtcNow, LogLevel.Debug, this, testEntry);

                    // Various message types with a DateTime
                    DateTime dt = new DateTime(1999, 12, 31);
                    AssertLogEntry(tid, tn, dt, LogLevel.Warn , "Foo", LogEntry.Create(dt, LogLevel.Warn, "Foo"));
                    AssertLogEntry(tid, tn, dt, LogLevel.Fatal, 42   , LogEntry.Create(dt, LogLevel.Fatal, 42  ));
                    AssertLogEntry(tid, tn, dt, LogLevel.Debug, this , LogEntry.Create(dt, LogLevel.Debug, this));
                });
            }

            Task.WaitAll(tasks);
        }

        internal static void AssertLogEntry<T>(
            int         threadId,
            string      threadName,
            DateTime    timestamp,
            LogLevel    level,
            T           message,
            LogEntry<T> actual)
            => AssertLogEntry(threadId, threadName, timestamp, timestamp, level, message, actual);

        internal static void AssertLogEntry<T>(
            int         threadId,
            string      threadName,
            DateTime    min,
            DateTime    max,
            LogLevel    level,
            T           message,
            LogEntry<T> actual)
        {
            Assert.AreEqual(threadId  , actual.ThreadId  );
            Assert.AreEqual(threadName, actual.ThreadName);
            Assert.AreEqual(level     , actual.Level     );
            Assert.AreEqual(message   , actual.Message   );

            // Range is necessary for when we test the assignment of "now"
            Assert.IsTrue(min <= actual.Timestamp && actual.Timestamp <= max);
        }
    }
}
