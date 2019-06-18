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
        public void Constructor()
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

                    // Default log entry
                    LogEntryHelperTest.AssertLogEntry<string>(default, default, default, default, default, default);

                    // Various message types
                    min = DateTime.UtcNow;
                    LogEntry<string> stringEntry = new LogEntry<string>(LogLevel.Warn, "Foo");
                    LogEntryHelperTest.AssertLogEntry(tid, tn, min, DateTime.UtcNow, LogLevel.Warn, "Foo", stringEntry);

                    min = DateTime.UtcNow;
                    LogEntry<int> intEntry = new LogEntry<int>(LogLevel.Fatal, 42);
                    LogEntryHelperTest.AssertLogEntry(tid, tn, min, DateTime.UtcNow, LogLevel.Fatal, 42, intEntry);

                    min = DateTime.UtcNow;
                    LogEntry<LogEntryTest> testEntry = new LogEntry<LogEntryTest>(LogLevel.Debug, this);
                    LogEntryHelperTest.AssertLogEntry(tid, tn, min, DateTime.UtcNow, LogLevel.Debug, this, testEntry);

                    min = DateTime.UtcNow;
                    LogEntry<DateTime> dateTimeEntry = new LogEntry<DateTime>((LogLevel)42, min); // Use invalid LogLevel
                    LogEntryHelperTest.AssertLogEntry(tid, tn, min, DateTime.UtcNow, (LogLevel)42, min, dateTimeEntry);

                    // Various message types with a DateTime
                    DateTime dt = new DateTime(1999, 12, 31);
                    LogEntryHelperTest.AssertLogEntry(tid, tn, dt, LogLevel.Warn , "Foo", new LogEntry<string>      (dt, LogLevel.Warn, "Foo"));
                    LogEntryHelperTest.AssertLogEntry(tid, tn, dt, LogLevel.Fatal, 42   , new LogEntry<int>         (dt, LogLevel.Fatal, 42  ));
                    LogEntryHelperTest.AssertLogEntry(tid, tn, dt, LogLevel.Debug, this , new LogEntry<LogEntryTest>(dt, LogLevel.Debug, this));
                    LogEntryHelperTest.AssertLogEntry(tid, tn, dt, (LogLevel)42  , dt   , new LogEntry<DateTime>    (dt, (LogLevel)42  , dt  ));
                });
            }

            Task.WaitAll(tasks);
        }
    }
}
