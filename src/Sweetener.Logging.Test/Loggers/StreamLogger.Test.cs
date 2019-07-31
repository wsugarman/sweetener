using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Logging.Test
{
    [TestClass]
    public class StreamLoggerTest
    {
        [TestMethod]
        public void AutoFlush()
        {
            // AutoFlush = false
            using (StreamLogger logger = new StreamLogger(new MemoryStream()))
            {
                Assert.IsFalse(logger.AutoFlush);

                logger.Log(LogLevel.Warn, "Warning!");
                Assert.AreEqual(0, logger.BaseStream.Position);
            }

            // AutoFlush = true
            using (StreamLogger logger = new StreamLogger(new MemoryStream()) { AutoFlush = true })
            {
                Assert.IsTrue(logger.AutoFlush);

                logger.Log(LogLevel.Warn, "Warning!");
                Assert.AreNotEqual(0, logger.BaseStream.Position);
            }
        }

        [TestMethod]
        public void BaseStream()
        {
            using (MemoryStream stream = new MemoryStream())
            using (StreamLogger logger = new StreamLogger(stream))
                Assert.AreEqual(stream, logger.BaseStream);
        }

        [TestMethod]
        // Change name to not conflict with class
        public void EncodingProperty()
        {
            // Default
            using (StreamLogger logger = new StreamLogger(Stream.Null, LogLevel.Fatal, null, "{msg}"))
                Assert.AreEqual(EncodingCache.UTF8NoBOM, logger.Encoding);

            // Non-null
            using (StreamLogger logger = new StreamLogger(Stream.Null, LogLevel.Fatal, null, "{msg}", Encoding.UTF32))
                Assert.AreEqual(Encoding.UTF32, logger.Encoding);
        }

        [TestMethod]
        public void NewLine()
        {
            using (StreamLogger logger = new StreamLogger(Stream.Null))
            {
                // Default value
                Assert.AreEqual(Environment.NewLine, logger.NewLine);

                // Assign to a different value
                logger.NewLine = "EOL";
                Assert.AreEqual("EOL", logger.NewLine);

                // Assign default using null
                logger.NewLine = null;
                Assert.AreEqual(Environment.NewLine, logger.NewLine);
            }
        }

        [TestMethod]
        public void ConstructorExceptions()
        {
            CultureInfo jaJP = CultureInfo.GetCultureInfo("ja-JP");
            CultureInfo ptBR = CultureInfo.GetCultureInfo("pt-BR");
            Stream readOnlyStream = new MemoryStream(Array.Empty<byte>(), writable: false);

            // StreamLogger(Stream)
            Assert.ThrowsException<ArgumentException    >(() => new StreamLogger(readOnlyStream));
            Assert.ThrowsException<ArgumentNullException>(() => new StreamLogger(null          ));

            // StreamLogger(Stream, LogLevel)
            Assert.ThrowsException<ArgumentException          >(() => new StreamLogger(readOnlyStream, LogLevel.Info));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger(null          , LogLevel.Warn));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new StreamLogger(Stream.Null   , (LogLevel)42 ));

            // StreamLogger(Stream, LogLevel, string)
            Assert.ThrowsException<ArgumentException          >(() => new StreamLogger(readOnlyStream, LogLevel.Info , "{msg}"      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger(null          , LogLevel.Warn , "{msg}"      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger(Stream.Null   , LogLevel.Debug, null         ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new StreamLogger(Stream.Null   , (LogLevel)42  , "{msg}"      ));
            Assert.ThrowsException<FormatException            >(() => new StreamLogger(Stream.Null   , LogLevel.Fatal, "Missing msg"));

            // StreamLogger(Stream, LogLevel, FormatProvider, string)
            Assert.ThrowsException<ArgumentException          >(() => new StreamLogger(readOnlyStream, LogLevel.Info , jaJP, "{msg}"      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger(null          , LogLevel.Warn , ptBR, "{msg}"      ));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger(Stream.Null   , LogLevel.Debug, ptBR, null         ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new StreamLogger(Stream.Null   , (LogLevel)42  , jaJP, "{msg}"      ));
            Assert.ThrowsException<FormatException            >(() => new StreamLogger(Stream.Null   , LogLevel.Fatal, null, "Missing msg"));

            // StreamLogger(Stream, LogLevel, FormatProvider, string, Encoding)
            Assert.ThrowsException<ArgumentException          >(() => new StreamLogger(readOnlyStream, LogLevel.Info , jaJP, "{msg}"      , Encoding.ASCII  ));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger(null          , LogLevel.Warn , ptBR, "{msg}"      , Encoding.UTF7   ));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger(Stream.Null   , LogLevel.Debug, ptBR, null         , Encoding.UTF8   ));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger(Stream.Null   , LogLevel.Warn , ptBR, "{msg}"      , null            ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new StreamLogger(Stream.Null   , (LogLevel)42  , jaJP, "{msg}"      , Encoding.UTF32  ));
            Assert.ThrowsException<FormatException            >(() => new StreamLogger(Stream.Null   , LogLevel.Fatal, null, "Missing msg", Encoding.Unicode));

            // StreamLogger(Stream, LogLevel, FormatProvider, string, Encoding, int)
            Assert.ThrowsException<ArgumentException          >(() => new StreamLogger(readOnlyStream, LogLevel.Info , jaJP, "{msg}"      , Encoding.ASCII  ,  1234));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger(null          , LogLevel.Warn , ptBR, "{msg}"      , Encoding.UTF7   ,  5678));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger(Stream.Null   , LogLevel.Debug, ptBR, null         , Encoding.UTF8   ,  0910));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger(Stream.Null   , LogLevel.Warn , ptBR, "{msg}"      , null            ,  1112));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new StreamLogger(Stream.Null   , (LogLevel)42  , jaJP, "{msg}"      , Encoding.UTF32  ,  1314));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new StreamLogger(Stream.Null   , LogLevel.Trace, jaJP, "{msg}"      , Encoding.Default, -1516));
            Assert.ThrowsException<FormatException            >(() => new StreamLogger(Stream.Null   , LogLevel.Fatal, null, "Missing msg", Encoding.Unicode,  1718));

            // StreamLogger(Stream, LogLevel, FormatProvider, string, Encoding, int, bool)
            Assert.ThrowsException<ArgumentException          >(() => new StreamLogger(readOnlyStream, LogLevel.Info , jaJP, "{msg}"      , Encoding.ASCII  ,  1234, false));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger(null          , LogLevel.Warn , ptBR, "{msg}"      , Encoding.UTF7   ,  5678, true ));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger(Stream.Null   , LogLevel.Debug, ptBR, null         , Encoding.UTF8   ,  0910, false));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger(Stream.Null   , LogLevel.Warn , ptBR, "{msg}"      , null            ,  1112, true ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new StreamLogger(Stream.Null   , (LogLevel)42  , jaJP, "{msg}"      , Encoding.UTF32  ,  1314, false));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new StreamLogger(Stream.Null   , LogLevel.Trace, jaJP, "{msg}"      , Encoding.Default, -1516, true ));
            Assert.ThrowsException<FormatException            >(() => new StreamLogger(Stream.Null   , LogLevel.Fatal, null, "Missing msg", Encoding.Unicode,  1718, false));
        }

        [TestMethod]
        public void Constructor()
        {
            CultureInfo frFr = CultureInfo.GetCultureInfo("fr-FR");

            // StreamLogger(Stream)
            AssertStreamLogger(
                LogLevel.Trace,
                CultureInfo.CurrentCulture,
                StreamLogger.DefaultTemplate,
                EncodingCache.UTF8NoBOM,
                1024,
                false,
                (stream) => new StreamLogger(stream));

            // StreamLogger(Stream, LogLevel)
            AssertStreamLogger(
                LogLevel.Warn,
                CultureInfo.CurrentCulture,
                StreamLogger.DefaultTemplate,
                EncodingCache.UTF8NoBOM,
                1024,
                false,
                (stream) => new StreamLogger(stream, LogLevel.Warn));

            // StreamLogger(Stream, LogLevel, string)
            AssertStreamLogger(
                LogLevel.Error,
                CultureInfo.CurrentCulture,
                "{l} {msg}",
                EncodingCache.UTF8NoBOM,
                1024,
                false,
                (stream) => new StreamLogger(stream, LogLevel.Error, "{l} {msg}"));

            // StreamLogger(Stream, LogLevel, FormatProvider, string)
            AssertStreamLogger(
                LogLevel.Debug,
                frFr,
                ">> {msg}",
                EncodingCache.UTF8NoBOM,
                1024,
                false,
                (stream) => new StreamLogger(stream, LogLevel.Debug, frFr, ">> {msg}"));

            // StreamLogger(Stream, LogLevel, FormatProvider, string, Encoding)
            AssertStreamLogger(
                LogLevel.Info,
                frFr,
                ">> {msg}",
                Encoding.Unicode,
                1024,
                false,
                (stream) => new StreamLogger(stream, LogLevel.Info, frFr, ">> {msg}", Encoding.Unicode));

            // StreamLogger(Stream, LogLevel, FormatProvider, string, Encoding, int)
            AssertStreamLogger(
                LogLevel.Fatal,
                frFr,
                ">> {msg}",
                Encoding.ASCII,
                500,
                false,
                (stream) => new StreamLogger(stream, LogLevel.Fatal, frFr, ">> {msg}", Encoding.ASCII, 500));

            // StreamLogger(Stream, LogLevel, FormatProvider, string, Encoding, int, bool)
            AssertStreamLogger(
                LogLevel.Debug,
                frFr,
                ">> {msg}",
                Encoding.UTF32,
                2000,
                true,
                (stream) => new StreamLogger(stream, LogLevel.Debug, frFr, ">> {msg}", Encoding.UTF32, 2000, true));
        }

        [TestMethod]
        public void ThrowObjectDisposedException()
        {
            StreamLogger logger = new StreamLogger(Stream.Null);
            logger.Dispose();

            Assert.ThrowsException<ObjectDisposedException>(() => logger.Log(LogLevel.Trace, "0"                                   ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Log(LogLevel.Debug, "0 {0}"                , 1            ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Log(LogLevel.Info , "0 {0} {1}"            , 1, 2         ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Log(LogLevel.Warn , "0 {0} {1} {2}"        , 1, 2, 3      ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Log(LogLevel.Error, "0 {0} {1} {2} {3}"    , 1, 2, 3, 4   ));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Log(LogLevel.Fatal, "0 {0} {1} {2} {3} {4}", 1, 2, 3, 4, 5));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Flush());
        }

        [TestMethod]
        public void Flush()
        {
            using (StreamLogger logger = new StreamLogger(new MemoryStream()))
            {
                Assert.IsFalse(logger.AutoFlush);

                logger.Log(LogLevel.Warn, "Warning!");
                Assert.AreEqual(0, logger.BaseStream.Position);

                logger.Flush();
                Assert.AreNotEqual(0, logger.BaseStream.Position);
            }
        }

        [TestMethod]
        public void Log()
        {
            using (StreamLogger logger = new StreamLogger(new MemoryStream(), default, CultureInfo.InvariantCulture, "[{l,-5:F}] - {msg}"))
            {
                DateTime timestamp = new DateTime(2999, 12, 31, 23, 59, 57);

                logger.Log(LogLevel.Trace, "Fib(0) = { 0 }"                                  );
                logger.Log(LogLevel.Debug, "Fib(1) = {{ 0, {0} }}"               , 1         );
                logger.Log(LogLevel.Info , "Fib(2) = {{ 0, {0}, {1} }}"          , 1, 1      );
                logger.Log(LogLevel.Warn , "Fib(3) = {{ 0, {0}, {1}, {2} }}"     , 1, 1, 2   );
                logger.Log(LogLevel.Error, "Fib(4) = {{ 0, {0}, {1}, {2}, {3} }}", 1, 1, 2, 3);

                logger.Flush();

                // Read back the log entries
                logger.BaseStream.Seek(0, SeekOrigin.Begin);
                using (StreamReader reader = new StreamReader(logger.BaseStream))
                {
                    Assert.AreEqual("[Trace] - Fib(0) = { 0 }"               , reader.ReadLine());
                    Assert.AreEqual("[Debug] - Fib(1) = { 0, 1 }"            , reader.ReadLine());
                    Assert.AreEqual("[Info ] - Fib(2) = { 0, 1, 1 }"         , reader.ReadLine());
                    Assert.AreEqual("[Warn ] - Fib(3) = { 0, 1, 1, 2 }"      , reader.ReadLine());
                    Assert.AreEqual("[Error] - Fib(4) = { 0, 1, 1, 2, 3 }"   , reader.ReadLine());
                }
            }
        }

        private static void AssertStreamLogger(
            LogLevel                   expectedMinLevel,
            IFormatProvider            expectedFormatProvider,
            string                     expectedTemplate,
            Encoding                   expectedEncoding,
            int                        expectedBufferSize,
            bool                       leaveOpen,
            Func<Stream, StreamLogger> loggerFactory)
        {
            // Assert the various properties, except the buffer size
            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamLogger actual = loggerFactory(stream))
                {
                    Assert.AreEqual(expectedMinLevel      , actual.MinLevel           );
                    Assert.AreEqual(expectedFormatProvider, actual.FormatProvider     );
                    Assert.AreEqual(expectedTemplate      , actual.Template.ToString());
                    Assert.AreEqual(expectedEncoding      , actual.Encoding           );
                }

                if (leaveOpen)
                    Assert.AreEqual(expectedEncoding.Preamble.Length, stream.Position); // Preamble flushed
                else
                    Assert.ThrowsException<ObjectDisposedException>(() => stream.Position);
            }

            // Assert that the buffer size is expected
            AssertBufferSize(expectedBufferSize, loggerFactory);
        }

        private static void AssertBufferSize(int expectedBufferSize, Func<Stream, StreamLogger> loggerFactory)
        {
            string msg;
            LogLevel msgLevel = LogLevel.Fatal;

            // Write a message that is the same length as the internal buffer
            using (StreamLogger logger = loggerFactory(new MemoryStream()))
            {
                LogEntry entry = new LogEntry(msgLevel, string.Empty);
                int lineLength = logger.Template.Format(logger.FormatProvider, entry).Length + logger.NewLine.Length;

                // Note: The Preamble is not included in the buffer, and therefore its size
                //       doesn't impact how many characters fit in the buffer for the first time
                msg = new string('a', expectedBufferSize - lineLength);

                // Write the message and assert that the buffer was not filled (ie. flushed)
                logger.Log(msgLevel, msg);
                Assert.AreEqual(0, logger.BaseStream.Position);
            }

            // Write a message that is 1 more character than the length of the internal buffer
            using (StreamLogger logger = loggerFactory(new MemoryStream()))
            {
                msg += 'a';
                LogEntry overflowMsg = new LogEntry(msgLevel, msg);
                string line = logger.Template.Format(logger.FormatProvider, overflowMsg) + logger.NewLine;

                // Note: Without an explicit flush, only a buffer's length worth of characters
                //       will be written at a time to the underlying stream
                int expectedBytes = logger.Encoding.GetByteCount(line.Substring(0, expectedBufferSize));

                // Write the message and assert that the buffer flushed when logging
                logger.Log(msgLevel, msg);
                Assert.AreEqual(logger.Encoding.Preamble.Length + expectedBytes, logger.BaseStream.Position);
            }
        }
    }
}
