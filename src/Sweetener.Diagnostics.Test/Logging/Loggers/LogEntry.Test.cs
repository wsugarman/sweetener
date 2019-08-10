using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Diagnostics.Logging.Test
{
    [TestClass]
    public class ContextualLogEntryTest
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            AssertLogEntry<Guid>(default, default, default, default, default, default, default);
        }

        [TestMethod]
        public void Constructor()
        {
            // Throw exception when level is out-of-range
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new LogEntry<int>(DateTime.UtcNow, (LogLevel)(-1), 115, "Foo"));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new LogEntry<int>(DateTime.UtcNow, (LogLevel)  42, 115, "Bar"));

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
                        12345L,
                        "Foo",
                        new LogEntry<long>(timestamp, LogLevel.Warn, 12345L, "Foo"));
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
                    LogEntry<long> actual = new LogEntry<long>(LogLevel.Info, 12345L, "Bar");
                    AssertLogEntry(
                        thread.ManagedThreadId,
                        thread.Name,
                        min,
                        DateTime.UtcNow,
                        LogLevel.Info,
                        12345L,
                        "Bar",
                        actual);
                });
            }

            Task.WaitAll(tasks);
        }

        internal static void AssertLogEntry<T>(
            int         threadId,
            string      threadName,
            DateTime    timestamp,
            LogLevel    level,
            T           context,
            string      message,
            LogEntry<T> actual)
            => AssertLogEntry(threadId, threadName, timestamp, timestamp, level, context, message, actual);

        internal static void AssertLogEntry<T>(
            int         threadId,
            string      threadName,
            DateTime    min,
            DateTime    max,
            LogLevel    level,
            T           context,
            string      message,
            LogEntry<T> actual)
        {
            Assert.AreEqual(threadId  , actual.ThreadId  );
            Assert.AreEqual(threadName, actual.ThreadName);
            Assert.AreEqual(level     , actual.Level     );
            Assert.AreEqual(context   , actual.Context   );
            Assert.AreEqual(message   , actual.Message   );

            // Range is necessary for when we test the assignment of "now"
            Assert.IsTrue(min <= actual.Timestamp && actual.Timestamp <= max);
        }
    }
}
