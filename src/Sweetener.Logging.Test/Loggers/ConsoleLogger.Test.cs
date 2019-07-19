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
        public void IsSynchronized()
        {
            using (Logger logger = new ConsoleLogger())
                Assert.IsFalse(logger.IsSynchronized);
        }

        [TestMethod]
        public void SyncRoot()
        {
            using (Logger logger = new ConsoleLogger())
                Assert.AreEqual(logger, logger.SyncRoot);
        }

        [TestMethod]
        public void ConstructorExceptions()
        {
            // ConsoleLogger(LogLevel)
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ConsoleLogger((LogLevel)27));

            // ConsoleLogger(LogLevel, string)
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ConsoleLogger((LogLevel)27, null));

            // ConsoleLogger(LogLevel, IFormatProvider, string)
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ConsoleLogger((LogLevel)27  , null, "{msg}"   )); 
            Assert.ThrowsException<ArgumentNullException      >(() => new ConsoleLogger(LogLevel.Trace, null, null      ));
            Assert.ThrowsException<FormatException            >(() => new ConsoleLogger(LogLevel.Trace, null, "{foobar}"));
        }

        [TestMethod]
        public void Constructor()
        {
            using (ConsoleLogger logger = new ConsoleLogger())
            {
                Assert.AreEqual(LogLevel.Trace                , logger.MinLevel           );
                Assert.AreEqual(CultureInfo.CurrentCulture    , logger.FormatProvider     );
                Assert.AreEqual(TemplateLogger.DefaultTemplate, logger.Template.ToString());
            }

            using (ConsoleLogger logger = new ConsoleLogger(LogLevel.Warn))
            {
                Assert.AreEqual(LogLevel.Warn                 , logger.MinLevel           );
                Assert.AreEqual(CultureInfo.CurrentCulture    , logger.FormatProvider     );
                Assert.AreEqual(TemplateLogger.DefaultTemplate, logger.Template.ToString());
            }

            using (ConsoleLogger logger = new ConsoleLogger(LogLevel.Info, "<{pn}|{pid}> - {msg}"))
            {
                Assert.AreEqual(LogLevel.Info             , logger.MinLevel           );
                Assert.AreEqual(CultureInfo.CurrentCulture, logger.FormatProvider     );
                Assert.AreEqual("<{pn}|{pid}> - {msg}"    , logger.Template.ToString());
            }

            CultureInfo esES = CultureInfo.GetCultureInfo("es-ES");
            using (ConsoleLogger logger = new ConsoleLogger(LogLevel.Error, esES, "[{tid}] {msg}"))
            {
                Assert.AreEqual(LogLevel.Error , logger.MinLevel           );
                Assert.AreEqual(esES           , logger.FormatProvider     );
                Assert.AreEqual("[{tid}] {msg}", logger.Template.ToString());
            }
        }

        [TestMethod]
        public void Dispose()
        {
            TextWriter stdOut = Console.Out;
            try
            {
                using (StreamWriter writer = new StreamWriter(new MemoryStream(), Encoding.ASCII, 1024 * 1024)) // 1 MB buffer!
                {
                    // Redirect the output
                    Console.SetOut(writer);

                    // Ensure that calls to Dipose flush the logger
                    int position = 0;
                    ConsoleLogger logger = new ConsoleLogger(default, CultureInfo.InvariantCulture, "{msg}");
                    logger.Log(LogLevel.Info, "Message");

                    // Writer buffer is too large for the message to have been written
                    Assert.AreEqual(position, writer.BaseStream.Position);

                    // Check position after flush from Dipose()
                    logger.Dispose();
                    position += Encoding.ASCII.GetByteCount("Message" + writer.NewLine);
                    Assert.AreEqual(position, writer.BaseStream.Position);

                    // Ensure we can continue to call Dispose()
                    logger.Log(LogLevel.Warn, "Another message");
                    Assert.AreEqual(position, writer.BaseStream.Position);

                    logger.Dispose();
                    position += Encoding.ASCII.GetByteCount("Another message" + writer.NewLine);
                    Assert.AreEqual(position, writer.BaseStream.Position);

                    // Read back the messages
                    writer.BaseStream.Seek(0, SeekOrigin.Begin);
                    using (StreamReader reader = new StreamReader(writer.BaseStream))
                    {
                        Assert.AreEqual("Message"        , reader.ReadLine());
                        Assert.AreEqual("Another message", reader.ReadLine());
                    }
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
                    using (ConsoleLogger logger = new ConsoleLogger(default, CultureInfo.InvariantCulture, "[{l,-5:F}] - {msg}"))
                    {
                        logger.Log(LogLevel.Trace, "Hello"                                                      );
                        logger.Log(LogLevel.Debug, "Hello {0}"            , "World"                             );
                        logger.Log(LogLevel.Info , "Hello {0} {1}"        , "World", "from"                     );
                        logger.Log(LogLevel.Warn , "Hello {0} {1} {2}"    , "World", "from", "Console"          );
                        logger.Log(LogLevel.Error, "Hello {0} {1} {2} {3}", "World", "from", "Console", "Logger");
                    }

                    // Validate
                    buffer.Seek(0, SeekOrigin.Begin);
                    using (StreamReader reader = new StreamReader(buffer))
                    {
                        Assert.AreEqual("[Trace] - Hello"                          , reader.ReadLine());
                        Assert.AreEqual("[Debug] - Hello World"                    , reader.ReadLine());
                        Assert.AreEqual("[Info ] - Hello World from"               , reader.ReadLine());
                        Assert.AreEqual("[Warn ] - Hello World from Console"       , reader.ReadLine());
                        Assert.AreEqual("[Error] - Hello World from Console Logger", reader.ReadLine());
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
