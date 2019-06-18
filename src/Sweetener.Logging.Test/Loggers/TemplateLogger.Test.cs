using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Logging.Test
{
    [TestClass]
    public class TemplateLoggerTest
    {
        [TestMethod]
        public void Constructor()
        {
            // Argument Validation
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MemoryTemplateLogger((LogLevel)27                    ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MemoryTemplateLogger((LogLevel)27  , null            ));
            Assert.ThrowsException<ArgumentNullException      >(() => new MemoryTemplateLogger(LogLevel.Trace, null, null      ));
            Assert.ThrowsException<FormatException            >(() => new MemoryTemplateLogger(LogLevel.Trace, null, "{foobar}"));

            // Constructor Overloads
            using (MemoryTemplateLogger logger = new MemoryTemplateLogger())
            {
                Assert.AreEqual(LogLevel.Trace                , logger.MinLevel            );
                Assert.AreEqual(CultureInfo.CurrentCulture    , logger.FormatProvider      );
                Assert.AreEqual(TemplateLogger.DefaultTemplate, logger._template.ToString());
            }

            using (MemoryTemplateLogger logger = new MemoryTemplateLogger(LogLevel.Warn))
            {
                Assert.AreEqual(LogLevel.Warn                 , logger.MinLevel            );
                Assert.AreEqual(CultureInfo.CurrentCulture    , logger.FormatProvider      );
                Assert.AreEqual(TemplateLogger.DefaultTemplate, logger._template.ToString());
            }

            using (MemoryTemplateLogger logger = new MemoryTemplateLogger(LogLevel.Info, "<{pn}|{pid}> - {msg}"))
            {
                Assert.AreEqual(LogLevel.Info             , logger.MinLevel            );
                Assert.AreEqual(CultureInfo.CurrentCulture, logger.FormatProvider      );
                Assert.AreEqual("<{pn}|{pid}> - {msg}"    , logger._template.ToString());
            }

            CultureInfo esES = CultureInfo.GetCultureInfo("es-ES");
            using (MemoryTemplateLogger logger = new MemoryTemplateLogger(LogLevel.Error, esES, "[{tid}] {msg}"))
            {
                Assert.AreEqual(LogLevel.Error , logger.MinLevel            );
                Assert.AreEqual(esES           , logger.FormatProvider      );
                Assert.AreEqual("[{tid}] {msg}", logger._template.ToString());
            }
        }

        [TestMethod]
        public void Log()
        {
            // Validate Log calls WriteLine appropriately based on the template
            // Logger.Test.cs already validates that Log is called appropriately
            using (MemoryTemplateLogger logger = new MemoryTemplateLogger(default, CultureInfo.InvariantCulture, "{l:F} - {msg}"))
            {
                // Trace
                logger.Trace("0");
                logger.Trace("0 {0}"                , 1            );
                logger.Trace("0 {0} {1}"            , 1, 2         );
                logger.Trace("0 {0} {1} {2}"        , 1, 2, 3      );
                logger.Trace("0 {0} {1} {2} {3}"    , 1, 2, 3, 4   );
                logger.Trace("0 {0} {1} {2} {3} {4}", 1, 2, 3, 4, 5);

                // Debug
                logger.Debug("0");
                logger.Debug("0 {0}"                , 1            );
                logger.Debug("0 {0} {1}"            , 1, 2         );
                logger.Debug("0 {0} {1} {2}"        , 1, 2, 3      );
                logger.Debug("0 {0} {1} {2} {3}"    , 1, 2, 3, 4   );
                logger.Debug("0 {0} {1} {2} {3} {4}", 1, 2, 3, 4, 5);

                // Info
                logger.Info("0"                                   );
                logger.Info("0 {0}"                , 1            );
                logger.Info("0 {0} {1}"            , 1, 2         );
                logger.Info("0 {0} {1} {2}"        , 1, 2, 3      );
                logger.Info("0 {0} {1} {2} {3}"    , 1, 2, 3, 4   );
                logger.Info("0 {0} {1} {2} {3} {4}", 1, 2, 3, 4, 5);

                // Warn
                logger.Warn("0"                                   );
                logger.Warn("0 {0}"                , 1            );
                logger.Warn("0 {0} {1}"            , 1, 2         );
                logger.Warn("0 {0} {1} {2}"        , 1, 2, 3      );
                logger.Warn("0 {0} {1} {2} {3}"    , 1, 2, 3, 4   );
                logger.Warn("0 {0} {1} {2} {3} {4}", 1, 2, 3, 4, 5);

                // Error
                logger.Error("0"                                   );
                logger.Error("0 {0}"                , 1            );
                logger.Error("0 {0} {1}"            , 1, 2         );
                logger.Error("0 {0} {1} {2}"        , 1, 2, 3      );
                logger.Error("0 {0} {1} {2} {3}"    , 1, 2, 3, 4   );
                logger.Error("0 {0} {1} {2} {3} {4}", 1, 2, 3, 4, 5);

                // Fatal
                logger.Fatal("0"                                   );
                logger.Fatal("0 {0}"                , 1            );
                logger.Fatal("0 {0} {1}"            , 1, 2         );
                logger.Fatal("0 {0} {1} {2}"        , 1, 2, 3      );
                logger.Fatal("0 {0} {1} {2} {3}"    , 1, 2, 3, 4   );
                logger.Fatal("0 {0} {1} {2} {3} {4}", 1, 2, 3, 4, 5);

                Assert.AreEqual(36, logger.Entries.Count);

                LogLevel level = LogLevel.Trace;
                while (logger.Entries.Count > 0)
                {
                    Assert.IsTrue(logger.Entries.Count >= 6);
                    Assert.AreEqual($"{level:F} - 0"          , logger.Entries.Dequeue());
                    Assert.AreEqual($"{level:F} - 0 1"        , logger.Entries.Dequeue());
                    Assert.AreEqual($"{level:F} - 0 1 2"      , logger.Entries.Dequeue());
                    Assert.AreEqual($"{level:F} - 0 1 2 3"    , logger.Entries.Dequeue());
                    Assert.AreEqual($"{level:F} - 0 1 2 3 4"  , logger.Entries.Dequeue());
                    Assert.AreEqual($"{level:F} - 0 1 2 3 4 5", logger.Entries.Dequeue());

                    level++;
                }
            }
        }
    }
}
