using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Logging.Test
{
    [TestClass]
    public class TemplateParameterTest
    {
        [TestMethod]
        public void ParseExceptions()
        {
            // Null
            Assert.ThrowsException<ArgumentNullException>(() => TemplateParameterName.Parse(null));

            // Empty
            Assert.ThrowsException<FormatException>(() => TemplateParameterName.Parse(string.Empty));

            // White Space
            Assert.ThrowsException<FormatException>(() => TemplateParameterName.Parse("  \t "));

            // Case-sensitive
            Assert.ThrowsException<FormatException>(() => TemplateParameterName.Parse("MSG"));

            // Unknown template parameter
            Assert.ThrowsException<FormatException>(() => TemplateParameterName.Parse("foo"));
        }

        [TestMethod]
        public void ParseMessage()
        {
            Assert.AreEqual(TemplateParameter.Message, TemplateParameterName.Parse("msg"    ));
            Assert.AreEqual(TemplateParameter.Message, TemplateParameterName.Parse("message"));
        }

        [TestMethod]
        public void ParseTimestamp()
        {
            Assert.AreEqual(TemplateParameter.Timestamp, TemplateParameterName.Parse("ts"       ));
            Assert.AreEqual(TemplateParameter.Timestamp, TemplateParameterName.Parse("timestamp"));
        }

        [TestMethod]
        public void ParseLevel()
        {
            Assert.AreEqual(TemplateParameter.Level, TemplateParameterName.Parse("l"    ));
            Assert.AreEqual(TemplateParameter.Level, TemplateParameterName.Parse("level"));
        }

        [TestMethod]
        public void ParseProcessId()
        {
            Assert.AreEqual(TemplateParameter.ProcessId, TemplateParameterName.Parse("pid"      ));
            Assert.AreEqual(TemplateParameter.ProcessId, TemplateParameterName.Parse("processId"));
        }

        [TestMethod]
        public void ParseProcessName()
        {
            Assert.AreEqual(TemplateParameter.ProcessName, TemplateParameterName.Parse("pn"         ));
            Assert.AreEqual(TemplateParameter.ProcessName, TemplateParameterName.Parse("processName"));
        }

        [TestMethod]
        public void ParseThreadId()
        {
            Assert.AreEqual(TemplateParameter.ThreadId, TemplateParameterName.Parse("tid"     ));
            Assert.AreEqual(TemplateParameter.ThreadId, TemplateParameterName.Parse("threadId"));
        }

        [TestMethod]
        public void ParseThreadName()
        {
            Assert.AreEqual(TemplateParameter.ThreadName, TemplateParameterName.Parse("tn"        ));
            Assert.AreEqual(TemplateParameter.ThreadName, TemplateParameterName.Parse("threadName"));
        }
    }
}
