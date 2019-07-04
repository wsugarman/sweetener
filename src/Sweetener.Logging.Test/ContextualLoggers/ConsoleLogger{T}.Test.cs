using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Logging.Test
{
    [TestClass]
    public class ContextualConsoleLoggerTest
    {
        [TestMethod]
        public void Constructor()
        {
            // Argument Validation
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ConsoleLogger<ushort>((LogLevel)27                    ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ConsoleLogger<ushort>((LogLevel)27  , null            ));
            Assert.ThrowsException<ArgumentNullException      >(() => new ConsoleLogger<ushort>(LogLevel.Trace, null, null      ));
            Assert.ThrowsException<FormatException            >(() => new ConsoleLogger<ushort>(LogLevel.Trace, null, "{foobar}"));

            // Constructor Overloads
            using (ConsoleLogger<byte> logger = new ConsoleLogger<byte>())
            {
                Assert.AreEqual(LogLevel.Trace                      , logger.MinLevel           );
                Assert.AreEqual(CultureInfo.CurrentCulture          , logger.FormatProvider     );
                Assert.AreEqual(TemplateLogger<byte>.DefaultTemplate, logger.Template.ToString());
            }

            using (ConsoleLogger<string> logger = new ConsoleLogger<string>(LogLevel.Warn))
            {
                Assert.AreEqual(LogLevel.Warn                         , logger.MinLevel           );
                Assert.AreEqual(CultureInfo.CurrentCulture            , logger.FormatProvider     );
                Assert.AreEqual(TemplateLogger<string>.DefaultTemplate, logger.Template.ToString());
            }

            using (ConsoleLogger<Guid> logger = new ConsoleLogger<Guid>(LogLevel.Info, "<{pn}|{pid}> - {msg} {cxt}"))
            {
                Assert.AreEqual(LogLevel.Info               , logger.MinLevel           );
                Assert.AreEqual(CultureInfo.CurrentCulture  , logger.FormatProvider     );
                Assert.AreEqual("<{pn}|{pid}> - {msg} {cxt}", logger.Template.ToString());
            }

            CultureInfo esES = CultureInfo.GetCultureInfo("es-ES");
            using (ConsoleLogger<TimeSpan> logger = new ConsoleLogger<TimeSpan>(LogLevel.Error, esES, "[{tid}] {cxt} {msg}"))
            {
                Assert.AreEqual(LogLevel.Error       , logger.MinLevel           );
                Assert.AreEqual(esES                 , logger.FormatProvider     );
                Assert.AreEqual("[{tid}] {cxt} {msg}", logger.Template.ToString());
            }
        }

        [TestMethod]
        public void IsSynchronized()
        {
            using (Logger<int> logger = new ConsoleLogger<int>())
                Assert.IsFalse(logger.IsSynchronized);

            using (Logger<int> logger = new ConsoleLogger<int>(LogLevel.Info))
                Assert.IsFalse(logger.IsSynchronized);

            using (Logger<int> logger = new ConsoleLogger<int>(LogLevel.Warn, "{cxt} {msg}"))
                Assert.IsFalse(logger.IsSynchronized);

            using (Logger<int> logger = new ConsoleLogger<int>(LogLevel.Debug, CultureInfo.GetCultureInfo("es-ES"), "{cxt} {msg}"))
                Assert.IsFalse(logger.IsSynchronized);
        }

        [TestMethod]
        public void SyncRoot()
        {
            using (Logger<int> logger = new ConsoleLogger<int>())
                Assert.AreEqual(logger, logger.SyncRoot);

            using (Logger<int> logger = new ConsoleLogger<int>(LogLevel.Info))
                Assert.AreEqual(logger, logger.SyncRoot);

            using (Logger<int> logger = new ConsoleLogger<int>(LogLevel.Warn, "{cxt} {msg}"))
                Assert.AreEqual(logger, logger.SyncRoot);

            using (Logger<int> logger = new ConsoleLogger<int>(LogLevel.Debug, CultureInfo.GetCultureInfo("es-ES"), "{cxt} {msg}"))
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
                    ConsoleLogger<int> logger = new ConsoleLogger<int>(default, CultureInfo.InvariantCulture, "{cxt} {msg}");
                    logger.Log(new LogEntry<int>(LogLevel.Info, 1, "Message"));

                    // Writer buffer is too large for the message to have been written
                    Assert.AreEqual(0, buffer.Position);

                    // Check position after flush from Dipose()
                    logger.Dispose();
                    Assert.AreEqual(Encoding.ASCII.GetByteCount("1 Message" + writer.NewLine), buffer.Position);

                    // After writing some more text, ensure that subsequent calls to Dipose() aren't flushing
                    Console.WriteLine("Another message");
                    logger.Dispose();
                    Assert.AreEqual(Encoding.ASCII.GetByteCount("1 Message" + writer.NewLine), buffer.Position);
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
                // TemplateLogger{T}.Test.cs already validates that the various logging methods
                // call Log(LogEntry<T>) correctly, so we'll use Log(LogEntry<T>) to validate
                // calls to WriteLine(string)
                using (MemoryStream buffer = new MemoryStream())
                using (StreamWriter writer = new StreamWriter(buffer))
                {
                    // Redirect the output
                    Console.SetOut(writer);

                    // Write a few lines
                    using (ConsoleLogger<long> logger = new ConsoleLogger<long>(default, CultureInfo.InvariantCulture, "At {ts:MM/dd/yyyy HH:mm:ss} [{l,-5:F}] [User: {cxt}] - {msg}"))
                    {
                        DateTime timestamp = new DateTime(1987, 9, 28, 12, 34, 56);
                        logger.Log(new LogEntry<long>(timestamp              , LogLevel.Info , 123L, "Hello"  ));
                        logger.Log(new LogEntry<long>(timestamp.AddSeconds(1), LogLevel.Fatal, 123L, "Console"));
                        logger.Log(new LogEntry<long>(timestamp.AddSeconds(1), LogLevel.Info , 456L, "Hello"  ));
                        logger.Log(new LogEntry<long>(timestamp.AddSeconds(2), LogLevel.Warn , 123L, "World"  ));
                    }

                    // Validate
                    buffer.Seek(0, SeekOrigin.Begin);
                    using (StreamReader reader = new StreamReader(buffer))
                    {
                        Assert.AreEqual("At 09/28/1987 12:34:56 [Info ] [User: 123] - Hello"  , reader.ReadLine());
                        Assert.AreEqual("At 09/28/1987 12:34:57 [Fatal] [User: 123] - Console", reader.ReadLine());
                        Assert.AreEqual("At 09/28/1987 12:34:57 [Info ] [User: 456] - Hello"  , reader.ReadLine());
                        Assert.AreEqual("At 09/28/1987 12:34:58 [Warn ] [User: 123] - World"  , reader.ReadLine());
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
