using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Logging.Test
{
    [TestClass]
    public class ContextualTemplateLoggerTest
    {
        [TestMethod]
        public void ConstructorExceptions()
        {
            // TemplateLogger(LogLevel)
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MemoryTemplateLogger<ushort>((LogLevel)27));

            // TemplateLogger(LogLevel, string)
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MemoryTemplateLogger<ushort>((LogLevel)27, null));

            // TemplateLogger(LogLevel, IFormatProvider, string)
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MemoryTemplateLogger<ushort>((LogLevel)27  , null, "{cxt} {msg}")); 
            Assert.ThrowsException<ArgumentNullException      >(() => new MemoryTemplateLogger<ushort>(LogLevel.Trace, null, null         ));
            Assert.ThrowsException<FormatException            >(() => new MemoryTemplateLogger<ushort>(LogLevel.Trace, null, "{foobar}"   ));
        }

        [TestMethod]
        public void Constructor()
        {
            using (TemplateLogger<byte> logger = new MemoryTemplateLogger<byte>())
            {
                Assert.AreEqual(LogLevel.Trace                      , logger.MinLevel           );
                Assert.AreEqual(CultureInfo.CurrentCulture          , logger.FormatProvider     );
                Assert.AreEqual(TemplateLogger<byte>.DefaultTemplate, logger.Template.ToString());
            }

            using (TemplateLogger<string> logger = new MemoryTemplateLogger<string>(LogLevel.Warn))
            {
                Assert.AreEqual(LogLevel.Warn                         , logger.MinLevel           );
                Assert.AreEqual(CultureInfo.CurrentCulture            , logger.FormatProvider     );
                Assert.AreEqual(TemplateLogger<string>.DefaultTemplate, logger.Template.ToString());
            }

            using (TemplateLogger<Guid> logger = new MemoryTemplateLogger<Guid>(LogLevel.Info, "<{pn}|{pid}> - {msg} {cxt}"))
            {
                Assert.AreEqual(LogLevel.Info               , logger.MinLevel           );
                Assert.AreEqual(CultureInfo.CurrentCulture  , logger.FormatProvider     );
                Assert.AreEqual("<{pn}|{pid}> - {msg} {cxt}", logger.Template.ToString());
            }

            CultureInfo esES = CultureInfo.GetCultureInfo("es-ES");
            using (TemplateLogger<TimeSpan> logger = new MemoryTemplateLogger<TimeSpan>(LogLevel.Error, esES, "[{tid}] {cxt} {msg}"))
            {
                Assert.AreEqual(LogLevel.Error       , logger.MinLevel           );
                Assert.AreEqual(esES                 , logger.FormatProvider     );
                Assert.AreEqual("[{tid}] {cxt} {msg}", logger.Template.ToString());
            }
        }

        [TestMethod]
        public void Log()
        {
            // Validate Log calls WriteLine appropriately based on the template
            // Logger{T}.Test.cs already validates that Log is called appropriately
            using (MemoryTemplateLogger<string> logger = new MemoryTemplateLogger<string>(default, CultureInfo.InvariantCulture, "[{l,-5:F}] - {cxt} {msg}"))
            {
                logger.Log(LogLevel.Debug, "Why", "Hello"                                                       );
                logger.Log(LogLevel.Info , "Why", "Hello {0}"            , "World"                              );
                logger.Log(LogLevel.Fatal, "Why", "Hello {0} {1}"        , "World", "from"                      );
                logger.Log(LogLevel.Trace, "Why", "Hello {0} {1} {2}"    , "World", "from", "Template"          );
                logger.Log(LogLevel.Warn , "Why", "Hello {0} {1} {2} {3}", "World", "from", "Template", "Logger");

                Assert.AreEqual(5, logger.Entries.Count);
                Assert.AreEqual("[Debug] - Why Hello"                             , logger.Entries.Dequeue());
                Assert.AreEqual("[Info ] - Why Hello World"                       , logger.Entries.Dequeue());
                Assert.AreEqual("[Fatal] - Why Hello World from"                  , logger.Entries.Dequeue());
                Assert.AreEqual("[Trace] - Why Hello World from Template"         , logger.Entries.Dequeue());
                Assert.AreEqual("[Warn ] - Why Hello World from Template Logger"  , logger.Entries.Dequeue());
            }
        }
    }
}
