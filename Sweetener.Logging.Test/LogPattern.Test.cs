using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Logging.Test
{
    [TestClass]
    public class LogPatternTest
    {
        [TestMethod]
        public void ResolveExceptions()
        {
            #region Invalid Patterns
            // Null Pattern
            Assert.ThrowsException<ArgumentNullException>(() => new LogPattern(null));

            // Unknown Parameter
            Assert.ThrowsException<FormatException>(() => new LogPattern("{msg} {foo}"));

            // Missing Message
            Assert.ThrowsException<FormatException>(() => new LogPattern("{level}"));

            // Gap In Name
            Assert.ThrowsException<FormatException>(() => new LogPattern("{ mess age }"));

            // Invalid White Space
            Assert.ThrowsException<FormatException>(() => new LogPattern("{ msg\t}"));

            // String Ends Prematurely
            Assert.ThrowsException<FormatException>(() => new LogPattern("{msg"          ));
            Assert.ThrowsException<FormatException>(() => new LogPattern("{msg} {tid,3"  ));
            Assert.ThrowsException<FormatException>(() => new LogPattern("{msg} {tid,3:X"));
            #endregion

            #region Invalid Formats in an Item
            // Message
            // Note: strings seem to ignore alignment and format
            //Assert.ThrowsException<FormatException>(() => new LogPattern("{msg,k}"));
            //Assert.ThrowsException<FormatException>(() => new LogPattern("{msg:x}"));

            // Timestamp
            Assert.ThrowsException<FormatException>(() => new LogPattern("{ts,k}"  ));
            Assert.ThrowsException<FormatException>(() => new LogPattern("{ts:abc}"));

            // Level
            Assert.ThrowsException<FormatException>(() => new LogPattern("{l,k}"));
            Assert.ThrowsException<FormatException>(() => new LogPattern("{l:x}"));

            // ProcessId
            Assert.ThrowsException<FormatException>(() => new LogPattern("{pid,k}"));
            Assert.ThrowsException<FormatException>(() => new LogPattern("{pid:x}"));

            // ProcessName
            Assert.ThrowsException<FormatException>(() => new LogPattern("{pn,k}"  ));
            //Assert.ThrowsException<FormatException>(() => new LogPattern("{pn:x}"));

            // ThreadId
            Assert.ThrowsException<FormatException>(() => new LogPattern("{tid,k}"));
            Assert.ThrowsException<FormatException>(() => new LogPattern("{tid:x}"));

            // ThreadName
            Assert.ThrowsException<FormatException>(() => new LogPattern("{tn,k}"  ));
            //Assert.ThrowsException<FormatException>(() => new LogPattern("{tn:x}"));
            #endregion
        }

        [TestMethod]
        public void ResolveEscapedCharacters()
        {
            string expected = $"}}}}{{{{escaped}}}} }}}}text{{{{ {{{(int)LogPattern.Parameter.Message}}}";

            Assert.AreEqual(expected, new LogPattern("}}{{escaped}} }}text{{ {msg}").CompositeFormatString);
        }

        [TestMethod]
        public void ResolveMessage()
        {
            string expected = $"{{{(int)LogPattern.Parameter.Message}}}";

            Assert.AreEqual(expected, new LogPattern("{msg}"    ).CompositeFormatString);
            Assert.AreEqual(expected, new LogPattern("{message}").CompositeFormatString);
        }

        [TestMethod]
        public void ResolveTimestamp()
        {
            string expected = $"{{{(int)LogPattern.Parameter.Timestamp}}} {{{(int)LogPattern.Parameter.Message}}}";

            Assert.AreEqual(expected, new LogPattern("{ts} {msg}"       ).CompositeFormatString);
            Assert.AreEqual(expected, new LogPattern("{timestamp} {msg}").CompositeFormatString);
        }

        [TestMethod]
        public void ResolveLevel()
        {
            string expected = $"{{{(int)LogPattern.Parameter.Level}}} {{{(int)LogPattern.Parameter.Message}}}";

            Assert.AreEqual(expected, new LogPattern("{l} {msg}"    ).CompositeFormatString);
            Assert.AreEqual(expected, new LogPattern("{level} {msg}").CompositeFormatString);
        }

        [TestMethod]
        public void ResolveProcessId()
        {
            string expected = $"{{{(int)LogPattern.Parameter.ProcessId}}} {{{(int)LogPattern.Parameter.Message}}}";

            Assert.AreEqual(expected, new LogPattern("{pid} {msg}"      ).CompositeFormatString);
            Assert.AreEqual(expected, new LogPattern("{processId} {msg}").CompositeFormatString);
        }

        [TestMethod]
        public void ResolveProcessName()
        {
            string expected = $"{{{(int)LogPattern.Parameter.ProcessName}}} {{{(int)LogPattern.Parameter.Message}}}";

            Assert.AreEqual(expected, new LogPattern("{pn} {msg}"         ).CompositeFormatString);
            Assert.AreEqual(expected, new LogPattern("{processName} {msg}").CompositeFormatString);
        }

        [TestMethod]
        public void ResolveThreadId()
        {
            string expected = $"{{{(int)LogPattern.Parameter.ThreadId}}} {{{(int)LogPattern.Parameter.Message}}}";

            Assert.AreEqual(expected, new LogPattern("{tid} {msg}"     ).CompositeFormatString);
            Assert.AreEqual(expected, new LogPattern("{threadId} {msg}").CompositeFormatString);
        }

        [TestMethod]
        public void ResolveThreadName()
        {
            string expected = $"{{{(int)LogPattern.Parameter.ThreadName}}} {{{(int)LogPattern.Parameter.Message}}}";

            Assert.AreEqual(expected, new LogPattern("{tn} {msg}"        ).CompositeFormatString);
            Assert.AreEqual(expected, new LogPattern("{threadName} {msg}").CompositeFormatString);
        }

        [TestMethod]
        public void ResolveMultipleParameters()
        {
            string expected = $"[{{{(int)LogPattern.Parameter.Timestamp}:yyyy-MM-ddTHH:mm:ss}}] [{{{(int)LogPattern.Parameter.Level},-5:F}}] ";
            expected       += $"[Process ({{{(int)LogPattern.Parameter.ProcessId}}}): {{{(int)LogPattern.Parameter.ProcessName}}}] ";
            expected       += $"[Thread ({{{(int)LogPattern.Parameter.ThreadId}}}): {{{(int)LogPattern.Parameter.ThreadName}}}] ";
            expected       += $"{{{(int)LogPattern.Parameter.Message}}}";

            LogPattern actual = new LogPattern("[{ts:yyyy-MM-ddTHH:mm:ss}] [{l,-5:F}] [Process ({pid}): {pn}] [Thread ({tid}): {tn}] {msg}");
            Assert.AreEqual(expected, actual.CompositeFormatString);
        }

        [TestMethod]
        public void ResolveRepeatParameters()
        {
            string expected = $"[Year: {{{(int)LogPattern.Parameter.Timestamp}:yyyy}}] ";
            expected       += $"[Month: {{{(int)LogPattern.Parameter.Timestamp}:MM}}] ";
            expected       += $"[Day: {{{(int)LogPattern.Parameter.Timestamp}:dd}}] ";
            expected       += $"***{{{(int)LogPattern.Parameter.Level}:F}}*** {{{(int)LogPattern.Parameter.Message}}} ***{{{(int)LogPattern.Parameter.Level}:F}}***";

            LogPattern actual = new LogPattern("[Year: {ts:yyyy}] [Month: {ts:MM}] [Day: {ts:dd}] ***{l:F}*** {msg} ***{l:F}***");
            Assert.AreEqual(expected, actual.CompositeFormatString);
        }

        [TestMethod]
        public void Format()
        {
            IFormatProvider provider = CultureInfo.InvariantCulture;
            LogLevel[]      levels   = (LogLevel[])Enum.GetValues(typeof(LogLevel));
            DateTime        dateTime = DateTime.UtcNow;
            ThreadPool.SetMinThreads(levels.Length, levels.Length);

            Process currentProcess = Process.GetCurrentProcess();
            int     pid = currentProcess.Id;
            string  pn  = currentProcess.ProcessName;

            LogPattern pattern = new LogPattern("[{ts:yyyy-MM-ddTHH:mm:ss}] [{l,-5:F}] [Process ({pid}): {pn}] [Thread ({tid}): {tn}] {msg}");
            Task[] tasks = new Task[levels.Length];

            for (int i = 0; i < levels.Length; i++)
            {
                LogLevel level = levels[i];
                tasks[i] = Task.Run(() =>
                {
                    Thread currentThread = Thread.CurrentThread;
                    int    tid           = currentThread.ManagedThreadId;

                    string tn;
                    lock (currentThread)
                    {
                        if (currentThread.Name == null)
                            currentThread.Name = $"{level:F} Thread";

                        tn = currentThread.Name;
                    }

                    string expected = $"[{dateTime:yyyy-MM-ddTHH:mm:ss}] [{level,-5:F}] [Process ({pid}): {pn}] [Thread ({tid}): {tn}] success";
                    string actual   = pattern.Format(provider, dateTime, level, "success");
                    Assert.AreEqual(expected, actual);
                });
            }

            Task.WaitAll(tasks);
        }

        [TestMethod]
        public void FormatSubset()
        {
            IFormatProvider provider = CultureInfo.InvariantCulture;

            string expected = $"#Debug# @ {Process.GetCurrentProcess().ProcessName} >> Foo Bar {{Baz}}!";
            string actual   = new LogPattern("#{level:F}# @ {pn} >> {msg}").Format(provider, LogLevel.Debug, "Foo Bar {Baz}!");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FormatCulture()
        {
            int             tid          = Thread.CurrentThread.ManagedThreadId;
            DateTime        dateTime     = DateTime.UtcNow;
            IFormatProvider frenchFrench = CultureInfo.GetCultureInfo("fr-FR");

            // Pretend the thread id is a currency for some interesting formatting changes
            // The "d" format string for DateTime is also impacted by the culture
            string expected = $"{string.Format(frenchFrench, "{0:C}", tid)} {dateTime:dd/MM/yyy} Bonjour de France";
            string actual   = new LogPattern("{tid:C} {ts:d} {msg}").Format(frenchFrench, dateTime, LogLevel.Error, "Bonjour de France");

            Assert.AreEqual(expected, actual);
        }
    }
}
