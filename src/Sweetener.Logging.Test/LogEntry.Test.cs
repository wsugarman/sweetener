using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Logging.Test
{
    [TestClass]
    public class LogEntryTest
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            AssertLogEntry(default, default, default, default, default, default);
        }

        [TestMethod]
        public void Constructor()
        {
            // Throw exception when level is out-of-range
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new LogEntry(DateTime.UtcNow, (LogLevel)(-1), "Foo"));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new LogEntry(DateTime.UtcNow, (LogLevel)  42, "Bar"));

            Task[] tasks = new Task[5];
            for (int i = 0; i < tasks.Length; i++)
            {
                string threadName = $"Task #{i}";
                tasks[i] = Task.Run(() =>
                {
                    Thread thread = Thread.CurrentThread;
                    lock (thread)
                    {
                        if (thread.Name == null)
                            thread.Name = threadName;
                    }

                    DateTime timestamp = new DateTime(1234, 5, 6);
                    AssertLogEntry(
                        thread.ManagedThreadId,
                        thread.Name,
                        timestamp,
                        LogLevel.Warn,
                        "Foo",
                        new LogEntry(timestamp, LogLevel.Warn, "Foo"));
                });
            }

            Task.WaitAll(tasks);
        }

        [TestMethod]
        public void InternalConstructor()
        {
            Task[] tasks = new Task[5];
            for (int i = 0; i < tasks.Length; i++)
            {
                string threadName = $"Task #{i}";
                tasks[i] = Task.Run(() =>
                {
                    Thread thread = Thread.CurrentThread;
                    lock (thread)
                    {
                        if (thread.Name == null)
                            thread.Name = threadName;
                    }

                    DateTime min = DateTime.UtcNow;
                    LogEntry actual = new LogEntry(LogLevel.Info, "Bar");
                    AssertLogEntry(
                        thread.ManagedThreadId,
                        thread.Name,
                        min,
                        DateTime.UtcNow,
                        LogLevel.Info,
                        "Bar",
                        actual);
                });
            }

            Task.WaitAll(tasks);
        }

        internal static void AssertLogEntry(
            int      threadId,
            string   threadName,
            DateTime timestamp,
            LogLevel level,
            string   message,
            LogEntry actual)
            => AssertLogEntry(threadId, threadName, timestamp, timestamp, level, message, actual);

        internal static void AssertLogEntry(
            int      threadId,
            string   threadName,
            DateTime min,
            DateTime max,
            LogLevel level,
            string   message,
            LogEntry actual)
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
