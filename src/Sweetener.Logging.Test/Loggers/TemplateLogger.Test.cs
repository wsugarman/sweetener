using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Logging.Test
{
    [TestClass]
    public class TemplateLoggerTest
    {
        [TestMethod]
        public void ConstructorExceptions()
        {
            // TemplateLogger(LogLevel)
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MemoryTemplateLogger((LogLevel)27));

            // TemplateLogger(LogLevel, string)
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MemoryTemplateLogger((LogLevel)27, null));

            // TemplateLogger(LogLevel, IFormatProvider, string)
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MemoryTemplateLogger((LogLevel)27  , null, "{msg}"   )); 
            Assert.ThrowsException<ArgumentNullException      >(() => new MemoryTemplateLogger(LogLevel.Trace, null, null      ));
            Assert.ThrowsException<FormatException            >(() => new MemoryTemplateLogger(LogLevel.Trace, null, "{foobar}"));
        }

        [TestMethod]
        public void Constructor()
        {
            using (TemplateLogger logger = new MemoryTemplateLogger())
            {
                Assert.AreEqual(LogLevel.Trace                , logger.MinLevel           );
                Assert.AreEqual(CultureInfo.CurrentCulture    , logger.FormatProvider     );
                Assert.AreEqual(TemplateLogger.DefaultTemplate, logger.Template.ToString());
            }

            using (TemplateLogger logger = new MemoryTemplateLogger(LogLevel.Warn))
            {
                Assert.AreEqual(LogLevel.Warn                 , logger.MinLevel           );
                Assert.AreEqual(CultureInfo.CurrentCulture    , logger.FormatProvider     );
                Assert.AreEqual(TemplateLogger.DefaultTemplate, logger.Template.ToString());
            }

            using (TemplateLogger logger = new MemoryTemplateLogger(LogLevel.Info, "<{pn}|{pid}> - {msg}"))
            {
                Assert.AreEqual(LogLevel.Info             , logger.MinLevel           );
                Assert.AreEqual(CultureInfo.CurrentCulture, logger.FormatProvider     );
                Assert.AreEqual("<{pn}|{pid}> - {msg}"    , logger.Template.ToString());
            }

            CultureInfo esES = CultureInfo.GetCultureInfo("es-ES");
            using (TemplateLogger logger = new MemoryTemplateLogger(LogLevel.Error, esES, "[{tid}] {msg}"))
            {
                Assert.AreEqual(LogLevel.Error , logger.MinLevel           );
                Assert.AreEqual(esES           , logger.FormatProvider     );
                Assert.AreEqual("[{tid}] {msg}", logger.Template.ToString());
            }
        }

        [TestMethod]
        public void Log()
        {
            // Validate Log calls WriteLine appropriately based on the template
            // Logger.Test.cs already validates that Log is called appropriately
            using (MemoryTemplateLogger logger = new MemoryTemplateLogger(default, CultureInfo.InvariantCulture, "[{l,-5:F}] - {msg}"))
            {
                logger.Log(LogLevel.Debug, "Hello"                                                       );
                logger.Log(LogLevel.Info , "Hello {0}"            , "World"                              );
                logger.Log(LogLevel.Fatal, "Hello {0} {1}"        , "World", "from"                      );
                logger.Log(LogLevel.Trace, "Hello {0} {1} {2}"    , "World", "from", "Template"          );
                logger.Log(LogLevel.Warn , "Hello {0} {1} {2} {3}", "World", "from", "Template", "Logger");

                Assert.AreEqual(5, logger.Entries.Count);
                Assert.AreEqual("[Debug] - Hello"                             , logger.Entries.Dequeue());
                Assert.AreEqual("[Info ] - Hello World"                       , logger.Entries.Dequeue());
                Assert.AreEqual("[Fatal] - Hello World from"                  , logger.Entries.Dequeue());
                Assert.AreEqual("[Trace] - Hello World from Template"         , logger.Entries.Dequeue());
                Assert.AreEqual("[Warn ] - Hello World from Template Logger"  , logger.Entries.Dequeue());
            }
        }
    }
}
