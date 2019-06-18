using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Logging.Test
{
    [TestClass]
    public class TemplateBuilderTest
    {
        [TestMethod]
        public void ParseExceptions()
        {
            #region Invalid Templates
            // Null Template
            Assert.ThrowsException<ArgumentNullException>(() => new TemplateBuilder(null));

            // Unknown Parameter
            Assert.ThrowsException<FormatException>(() => new TemplateBuilder("{foo}"));

            // Gap In Name
            Assert.ThrowsException<FormatException>(() => new TemplateBuilder("{ mess age }"));

            // Invalid White Space
            Assert.ThrowsException<FormatException>(() => new TemplateBuilder("{ msg\t}"));

            // String Ends Prematurely
            Assert.ThrowsException<FormatException>(() => new TemplateBuilder("{msg"    ));
            Assert.ThrowsException<FormatException>(() => new TemplateBuilder("{tid,3"  ));
            Assert.ThrowsException<FormatException>(() => new TemplateBuilder("{tid,3:X"));

            // Unexpected Closed Curly Brace
            Assert.ThrowsException<FormatException>(() => new TemplateBuilder("text }"));
            #endregion

            #region Invalid Formats in an Item
            // Message
            // Note: strings accept any format value
            Assert.ThrowsException<FormatException>(() => new TemplateBuilder("{msg,k}"));
            //Assert.ThrowsException<FormatException>(() => new TemplateBuilder("{msg:abc}"));

            // Timestamp
            Assert.ThrowsException<FormatException>(() => new TemplateBuilder("{ts,k}"  ));
            //Assert.ThrowsException<FormatException>(() => new TemplateBuilder("{ts:abc}")); // Unknown format characters are used as literals

            // Level
            Assert.ThrowsException<FormatException>(() => new TemplateBuilder("{l,k}"));
            Assert.ThrowsException<FormatException>(() => new TemplateBuilder("{l:abc}"));

            // ProcessId
            Assert.ThrowsException<FormatException>(() => new TemplateBuilder("{pid,k}"));
            Assert.ThrowsException<FormatException>(() => new TemplateBuilder("{pid:q}"));

            // ProcessName
            Assert.ThrowsException<FormatException>(() => new TemplateBuilder("{pn,k}"  ));
            //Assert.ThrowsException<FormatException>(() => new TemplateBuilder("{pn:x}"));

            // ThreadId
            Assert.ThrowsException<FormatException>(() => new TemplateBuilder("{tid,k}"));
            Assert.ThrowsException<FormatException>(() => new TemplateBuilder("{tid:q}"));

            // ThreadName
            Assert.ThrowsException<FormatException>(() => new TemplateBuilder("{tn,k}"  ));
            //Assert.ThrowsException<FormatException>(() => new TemplateBuilder("{tn:abc}"));
            #endregion
        }

        [TestMethod]
        public void BuildExceptions()
        {
            Assert.ThrowsException<InvalidOperationException>(() => new TemplateBuilder("{ts}").Build<string>());
        }

        [TestMethod]
        public void ParseEscapedCharacters()
        {
            string expected = $"}}}}{{{{escaped}}}} }}}}text{{{{";

            Assert.AreEqual(expected, new TemplateBuilder("}}{{escaped}} }}text{{")._format);
        }

        [TestMethod]
        public void GetMessageIndex()
        {
            string expected = $"{{{(int)TemplateParameter.Message}}}";

            Assert.AreEqual(expected, new TemplateBuilder("{msg}"    )._format);
            Assert.AreEqual(expected, new TemplateBuilder("{message}")._format);
        }

        [TestMethod]
        public void GetTimestampIndex()
        {
            string expected = $"{{{(int)TemplateParameter.Timestamp}}}";

            Assert.AreEqual(expected, new TemplateBuilder("{ts}"       )._format);
            Assert.AreEqual(expected, new TemplateBuilder("{timestamp}")._format);
        }

        [TestMethod]
        public void GetLevelIndex()
        {
            string expected = $"{{{(int)TemplateParameter.Level}}}";

            Assert.AreEqual(expected, new TemplateBuilder("{l}"    )._format);
            Assert.AreEqual(expected, new TemplateBuilder("{level}")._format);
        }

        [TestMethod]
        public void GetProcessIdIndex()
        {
            string expected = $"{{{(int)TemplateParameter.ProcessId}}}";

            Assert.AreEqual(expected, new TemplateBuilder("{pid}"      )._format);
            Assert.AreEqual(expected, new TemplateBuilder("{processId}")._format);
        }

        [TestMethod]
        public void GetProcessNameIndex()
        {
            string expected = $"{{{(int)TemplateParameter.ProcessName}}}";

            Assert.AreEqual(expected, new TemplateBuilder("{pn}"         )._format);
            Assert.AreEqual(expected, new TemplateBuilder("{processName}")._format);
        }

        [TestMethod]
        public void GetThreadIdIndex()
        {
            string expected = $"{{{(int)TemplateParameter.ThreadId}}}";

            Assert.AreEqual(expected, new TemplateBuilder("{tid}"     )._format);
            Assert.AreEqual(expected, new TemplateBuilder("{threadId}")._format);
        }

        [TestMethod]
        public void GetThreadNameIndex()
        {
            string expected = $"{{{(int)TemplateParameter.ThreadName}}}";

            Assert.AreEqual(expected, new TemplateBuilder("{tn}"        )._format);
            Assert.AreEqual(expected, new TemplateBuilder("{threadName}")._format);
        }

        [TestMethod]
        public void Parse()
        {
            string expected = $"[{{{(int)TemplateParameter.Timestamp}:yyyy-MM-ddTHH:mm:ss}}] [{{{(int)TemplateParameter.Level},-5:F}}] ";
            expected       += $"[Process ({{{(int)TemplateParameter.ProcessId}}}): {{{(int)TemplateParameter.ProcessName}}}] ";
            expected       += $"[Thread ({{{(int)TemplateParameter.ThreadId}}}): {{{(int)TemplateParameter.ThreadName}}}] ";
            expected       += $"{{{(int)TemplateParameter.Message}}}";

            TemplateBuilder actual = new TemplateBuilder("[{ts:yyyy-MM-ddTHH:mm:ss}] [{l,-5:F}] [Process ({pid}): {pn}] [Thread ({tid}): {tn}] {msg}");
            Assert.AreEqual(expected, actual._format);
        }

        [TestMethod]
        public void ParseRepeatParameters()
        {
            string expected = $"[Year: {{{(int)TemplateParameter.Timestamp}:yyyy}}] ";
            expected       += $"[Month: {{{(int)TemplateParameter.Timestamp}:MM}}] ";
            expected       += $"[Day: {{{(int)TemplateParameter.Timestamp}:dd}}] ";
            expected       += $"***{{{(int)TemplateParameter.Level}:F}}*** {{{(int)TemplateParameter.Message}}} ***{{{(int)TemplateParameter.Level}:F}}***";

            TemplateBuilder actual = new TemplateBuilder("[Year: {ts:yyyy}] [Month: {ts:MM}] [Day: {ts:dd}] ***{l:F}*** {msg} ***{l:F}***");
            Assert.AreEqual(expected, actual._format);
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

            ILogEntryTemplate<string> template = new TemplateBuilder("[{ts:yyyy-MM-ddTHH:mm:ss}] [{l,-5:F}] [Process ({pid}): {pn}] [Thread ({tid}): {tn}] {msg}").Build<string>();
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
                    string actual   = template.Format(provider, LogEntry.Create(dateTime, level, "success"));
                    Assert.AreEqual(expected, actual);
                });
            }

            Task.WaitAll(tasks);
        }

        [TestMethod]
        public void FormatEscapedFormatItem()
        {
            IFormatProvider provider = CultureInfo.InvariantCulture;

            string expected = $"#Debug# @ {Process.GetCurrentProcess().ProcessName} >> Foo Bar {{Baz}}!";
            string actual   = new TemplateBuilder("#{level:F}# @ {pn} >> {msg}")
                .Build<string>()
                .Format(provider, LogEntry.Create(LogLevel.Debug, "Foo Bar {Baz}!"));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FormatCulture()
        {
            int             tid      = Thread.CurrentThread.ManagedThreadId;
            DateTime        dateTime = DateTime.UtcNow;
            IFormatProvider frFR     = CultureInfo.GetCultureInfo("fr-FR");

            // Pretend the thread id is a currency for some interesting formatting changes
            // The "d" format string for DateTime is also impacted by the culture
            string expected = string.Format(frFR, "{0:C} {1:d} Bonjour de France", tid, dateTime);
            string actual   = new TemplateBuilder("{tid:C} {ts:d} {msg}")
                .Build<string>()
                .Format(frFR, LogEntry.Create(dateTime, LogLevel.Error, "Bonjour de France"));

            Assert.AreEqual(expected, actual);
        }
    }
}
