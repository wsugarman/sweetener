using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Diagnostics.Logging.Test
{
    [TestClass]
    public class ContextualConsoleLoggerTest
    {
        [TestMethod]
        public void ConstructorExceptions()
        {
            // ConsoleLogger(LogLevel)
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ConsoleLogger<ushort>((LogLevel)27));

            // ConsoleLogger(LogLevel, string)
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ConsoleLogger<ushort>((LogLevel)27, null));

            // ConsoleLogger(LogLevel, IFormatProvider, string)
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ConsoleLogger<ushort>((LogLevel)27  , null, "{msg}"   )); 
            Assert.ThrowsException<ArgumentNullException      >(() => new ConsoleLogger<ushort>(LogLevel.Trace, null, null      ));
            Assert.ThrowsException<FormatException            >(() => new ConsoleLogger<ushort>(LogLevel.Trace, null, "{foobar}"));
        }

        [TestMethod]
        public void Constructor()
        {
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
                    ConsoleLogger<int> logger = new ConsoleLogger<int>(default, CultureInfo.InvariantCulture, "{cxt} {msg}");
                    logger.Log(LogLevel.Info, 1, "message");

                    // Writer buffer is too large for the message to have been written
                    Assert.AreEqual(position, writer.BaseStream.Position);

                    // Check position after flush from Dipose()
                    logger.Dispose();
                    position += Encoding.ASCII.GetByteCount("1 message" + writer.NewLine);
                    Assert.AreEqual(position, writer.BaseStream.Position);

                    // Ensure we can continue to call Dispose()
                    logger.Log(LogLevel.Warn, 2, "more messages");
                    Assert.AreEqual(position, writer.BaseStream.Position);

                    logger.Dispose();
                    position += Encoding.ASCII.GetByteCount("2 more messages" + writer.NewLine);
                    Assert.AreEqual(position, writer.BaseStream.Position);

                    // Read back the messages
                    writer.BaseStream.Seek(0, SeekOrigin.Begin);
                    using (StreamReader reader = new StreamReader(writer.BaseStream))
                    {
                        Assert.AreEqual("1 message"      , reader.ReadLine());
                        Assert.AreEqual("2 more messages", reader.ReadLine());
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
                // TemplateLogger{T}.Test.cs already validates that the various logging methods' formatting
                using (MemoryStream buffer = new MemoryStream())
                using (StreamWriter writer = new StreamWriter(buffer))
                {
                    // Redirect the output
                    Console.SetOut(writer);

                    // Write a few lines
                    using (ConsoleLogger<string> logger = new ConsoleLogger<string>(default, CultureInfo.InvariantCulture, "[{l,-5:F}] - {cxt} {msg}"))
                    {
                        logger.Log(LogLevel.Trace, "Why", "Hello"                                                      );
                        logger.Log(LogLevel.Debug, "Why", "Hello {0}"            , "World"                             );
                        logger.Log(LogLevel.Info , "Why", "Hello {0} {1}"        , "World", "from"                     );
                        logger.Log(LogLevel.Warn , "Why", "Hello {0} {1} {2}"    , "World", "from", "Console"          );
                        logger.Log(LogLevel.Error, "Why", "Hello {0} {1} {2} {3}", "World", "from", "Console", "Logger");
                    }

                    // Validate
                    buffer.Seek(0, SeekOrigin.Begin);
                    using (StreamReader reader = new StreamReader(buffer))
                    {
                        Assert.AreEqual("[Trace] - Why Hello"                          , reader.ReadLine());
                        Assert.AreEqual("[Debug] - Why Hello World"                    , reader.ReadLine());
                        Assert.AreEqual("[Info ] - Why Hello World from"               , reader.ReadLine());
                        Assert.AreEqual("[Warn ] - Why Hello World from Console"       , reader.ReadLine());
                        Assert.AreEqual("[Error] - Why Hello World from Console Logger", reader.ReadLine());
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
