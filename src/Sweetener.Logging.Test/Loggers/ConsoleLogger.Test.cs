using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Logging.Test
{
    [TestClass]
    public class ConsoleLoggerTest
    {
        [TestMethod]
        public void Constructor()
        {
            // Argument Validation
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ConsoleLogger((LogLevel)27                    ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ConsoleLogger((LogLevel)27  , null            ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ConsoleLogger(LogLevel.Trace, null, null      ));
            Assert.ThrowsException<FormatException            >(() => new ConsoleLogger(LogLevel.Trace, null, "{foobar}"));

            // Constructor Overloads
            using (ConsoleLogger logger = new ConsoleLogger())
            {
                Assert.AreEqual(LogLevel.Trace                , logger.MinLevel            );
                Assert.AreEqual(CultureInfo.CurrentCulture    , logger.FormatProvider      );
                Assert.AreEqual(TemplateLogger.DefaultTemplate, logger._template.ToString());
            }

            using (ConsoleLogger logger = new ConsoleLogger(LogLevel.Warn))
            {
                Assert.AreEqual(LogLevel.Warn                 , logger.MinLevel            );
                Assert.AreEqual(CultureInfo.CurrentCulture    , logger.FormatProvider      );
                Assert.AreEqual(TemplateLogger.DefaultTemplate, logger._template.ToString());
            }

            using (ConsoleLogger logger = new ConsoleLogger(LogLevel.Info, "<{pn}|{pid}> - {msg}"))
            {
                Assert.AreEqual(LogLevel.Info             , logger.MinLevel            );
                Assert.AreEqual(CultureInfo.CurrentCulture, logger.FormatProvider      );
                Assert.AreEqual("<{pn}|{pid}> - {msg}"    , logger._template.ToString());
            }

            CultureInfo esES = CultureInfo.GetCultureInfo("es-ES");
            using (ConsoleLogger logger = new ConsoleLogger(LogLevel.Error, esES, "[{tid}] {msg}"))
            {
                Assert.AreEqual(LogLevel.Error , logger.MinLevel            );
                Assert.AreEqual(esES           , logger.FormatProvider      );
                Assert.AreEqual("[{tid}] {msg}", logger._template.ToString());
            }
        }

        [TestMethod]
        public void IsSynchronized()
        {
            using (Logger logger = new ConsoleLogger())
                Assert.IsFalse(logger.IsSynchronized);

            using (Logger logger = new ConsoleLogger(LogLevel.Info))
                Assert.IsFalse(logger.IsSynchronized);

            using (Logger logger = new ConsoleLogger(LogLevel.Warn, "{msg}"))
                Assert.IsFalse(logger.IsSynchronized);

            using (Logger logger = new ConsoleLogger(LogLevel.Debug, CultureInfo.GetCultureInfo("es-ES"), "{msg}"))
                Assert.IsFalse(logger.IsSynchronized);
        }

        [TestMethod]
        public void SyncRoot()
        {
            using (Logger logger = new ConsoleLogger())
                Assert.AreEqual(logger, logger.SyncRoot);

            using (Logger logger = new ConsoleLogger(LogLevel.Info))
                Assert.AreEqual(logger, logger.SyncRoot);

            using (Logger logger = new ConsoleLogger(LogLevel.Warn, "{msg}"))
                Assert.AreEqual(logger, logger.SyncRoot);

            using (Logger logger = new ConsoleLogger(LogLevel.Debug, CultureInfo.GetCultureInfo("es-ES"), "{msg}"))
                Assert.AreEqual(logger, logger.SyncRoot);
        }

        [TestMethod]
        public void Dispose()
        {
            TextWriter stdOut = Console.Out;
            try
            {
                using (MemoryStream buffer = new MemoryStream())
                using (StreamWriter writer = new StreamWriter(buffer, Encoding.ASCII, 1024 * 1024)) // 1 MB buffer!
                {
                    // Redirect the output
                    Console.SetOut(writer);

                    // Ensure that calls to Dipose flush the logger
                    ConsoleLogger logger = new ConsoleLogger(default, CultureInfo.InvariantCulture, "{msg}");
                    logger.Log(new LogEntry(LogLevel.Info, "Message"));

                    // Writer buffer is too large for the message to have been written
                    Assert.AreEqual(0, buffer.Position);

                    // Check position after flush from Dipose()
                    logger.Dispose();
                    Assert.AreEqual(Encoding.ASCII.GetByteCount("Message" + writer.NewLine), buffer.Position);

                    // After writing some more text, ensure that subsequent calls to Dipose() aren't flushing
                    Console.WriteLine("Another message");
                    logger.Dispose();
                    Assert.AreEqual(Encoding.ASCII.GetByteCount("Message" + writer.NewLine), buffer.Position);
                }
            }
            finally
            {
                Console.SetOut(stdOut);
            }
        }

        [TestMethod]
        public void Log()
        {
            TextWriter stdOut = Console.Out;
            try
            {
                // TemplateLogger.Test.cs already validates that the various logging methods
                // call Log(LogEntry) correctly, so we'll use Log(LogEntry) to validate
                // calls to WriteLine(string)
                using (MemoryStream buffer = new MemoryStream())
                using (StreamWriter writer = new StreamWriter(buffer))
                {
                    // Redirect the output
                    Console.SetOut(writer);

                    // Write a few lines
                    using (ConsoleLogger logger = new ConsoleLogger(default, CultureInfo.InvariantCulture, "At {ts:MM/dd/yyyy HH:mm:ss} [{l,-5:F}] - {msg}"))
                    {
                        DateTime timestamp = new DateTime(1987, 9, 28, 12, 34, 56);
                        logger.Log(new LogEntry(timestamp              , LogLevel.Info , "Hello"  ));
                        logger.Log(new LogEntry(timestamp.AddSeconds(1), LogLevel.Fatal, "Console"));
                        logger.Log(new LogEntry(timestamp.AddSeconds(2), LogLevel.Warn , "World"  ));
                    }

                    // Validate
                    buffer.Seek(0, SeekOrigin.Begin);
                    using (StreamReader reader = new StreamReader(buffer))
                    {
                        Assert.AreEqual("At 09/28/1987 12:34:56 [Info ] - Hello"  , reader.ReadLine());
                        Assert.AreEqual("At 09/28/1987 12:34:57 [Fatal] - Console", reader.ReadLine());
                        Assert.AreEqual("At 09/28/1987 12:34:58 [Warn ] - World"  , reader.ReadLine());
                    }
                }
            }
            finally
            {
                Console.SetOut(stdOut);
            }
        }
    }
}
