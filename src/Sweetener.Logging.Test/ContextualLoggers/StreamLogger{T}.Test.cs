using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Logging.Test
{
    [TestClass]
    public class ContextualStreamLoggerTest
    {
        [TestMethod]
        public void ConstructorExceptions()
        {
            CultureInfo jaJP = CultureInfo.GetCultureInfo("ja-JP");
            CultureInfo ptBR = CultureInfo.GetCultureInfo("pt-BR");
            Stream readOnlyStream = new MemoryStream(Array.Empty<byte>(), writable: false);

            // StreamLogger(Stream)
            Assert.ThrowsException<ArgumentException    >(() => new StreamLogger<sbyte>(readOnlyStream));
            Assert.ThrowsException<ArgumentNullException>(() => new StreamLogger<short>(null          ));

            // StreamLogger(Stream, LogLevel)
            Assert.ThrowsException<ArgumentException          >(() => new StreamLogger<sbyte>(readOnlyStream, LogLevel.Info));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger<short>(null          , LogLevel.Warn));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new StreamLogger<int  >(Stream.Null   , (LogLevel)42 ));

            // StreamLogger(Stream, LogLevel, string)
            Assert.ThrowsException<ArgumentException          >(() => new StreamLogger<sbyte>(readOnlyStream, LogLevel.Info , "{cxt} {msg}"));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger<short>(null          , LogLevel.Warn , "{cxt} {msg}"));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger<short>(Stream.Null   , LogLevel.Debug, null         ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new StreamLogger<int  >(Stream.Null   , (LogLevel)42  , "{cxt} {msg}"));
            Assert.ThrowsException<FormatException            >(() => new StreamLogger<long >(Stream.Null   , LogLevel.Fatal, "Missing msg"));

            // StreamLogger(Stream, LogLevel, FormatProvider, string)
            Assert.ThrowsException<ArgumentException          >(() => new StreamLogger<sbyte>(readOnlyStream, LogLevel.Info , jaJP, "{cxt} {msg}"));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger<short>(null          , LogLevel.Warn , ptBR, "{cxt} {msg}"));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger<short>(Stream.Null   , LogLevel.Debug, ptBR, null         ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new StreamLogger<int  >(Stream.Null   , (LogLevel)42  , jaJP, "{cxt} {msg}"));
            Assert.ThrowsException<FormatException            >(() => new StreamLogger<long >(Stream.Null   , LogLevel.Fatal, null, "Missing msg"));

            // StreamLogger(Stream, LogLevel, FormatProvider, string, Encoding)
            Assert.ThrowsException<ArgumentException          >(() => new StreamLogger<sbyte>(readOnlyStream, LogLevel.Info , jaJP, "{cxt} {msg}", Encoding.ASCII  ));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger<short>(null          , LogLevel.Warn , ptBR, "{cxt} {msg}", Encoding.UTF7   ));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger<short>(Stream.Null   , LogLevel.Debug, ptBR, null         , Encoding.UTF8   ));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger<short>(Stream.Null   , LogLevel.Warn , ptBR, "{cxt} {msg}", null            ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new StreamLogger<int  >(Stream.Null   , (LogLevel)42  , jaJP, "{cxt} {msg}", Encoding.UTF32  ));
            Assert.ThrowsException<FormatException            >(() => new StreamLogger<long >(Stream.Null   , LogLevel.Fatal, null, "Missing msg", Encoding.Unicode));

            // StreamLogger(Stream, LogLevel, FormatProvider, string, Encoding, int)
            Assert.ThrowsException<ArgumentException          >(() => new StreamLogger<sbyte>(readOnlyStream, LogLevel.Info , jaJP, "{cxt} {msg}", Encoding.ASCII  ,  1234));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger<short>(null          , LogLevel.Warn , ptBR, "{cxt} {msg}", Encoding.UTF7   ,  5678));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger<short>(Stream.Null   , LogLevel.Debug, ptBR, null         , Encoding.UTF8   ,  0910));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger<short>(Stream.Null   , LogLevel.Warn , ptBR, "{cxt} {msg}", null            ,  1112));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new StreamLogger<int  >(Stream.Null   , (LogLevel)42  , jaJP, "{cxt} {msg}", Encoding.UTF32  ,  1314));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new StreamLogger<int  >(Stream.Null   , LogLevel.Trace, jaJP, "{cxt} {msg}", Encoding.Default, -1516));
            Assert.ThrowsException<FormatException            >(() => new StreamLogger<long >(Stream.Null   , LogLevel.Fatal, null, "Missing msg", Encoding.Unicode,  1718));

            // StreamLogger(Stream, LogLevel, FormatProvider, string, Encoding, int, bool)
            Assert.ThrowsException<ArgumentException          >(() => new StreamLogger<sbyte>(readOnlyStream, LogLevel.Info , jaJP, "{cxt} {msg}", Encoding.ASCII  ,  1234, false));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger<short>(null          , LogLevel.Warn , ptBR, "{cxt} {msg}", Encoding.UTF7   ,  5678, true ));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger<short>(Stream.Null   , LogLevel.Debug, ptBR, null         , Encoding.UTF8   ,  0910, false));
            Assert.ThrowsException<ArgumentNullException      >(() => new StreamLogger<short>(Stream.Null   , LogLevel.Warn , ptBR, "{cxt} {msg}", null            ,  1112, true ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new StreamLogger<int  >(Stream.Null   , (LogLevel)42  , jaJP, "{cxt} {msg}", Encoding.UTF32  ,  1314, false));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new StreamLogger<int  >(Stream.Null   , LogLevel.Trace, jaJP, "{cxt} {msg}", Encoding.Default, -1516, true ));
            Assert.ThrowsException<FormatException            >(() => new StreamLogger<long >(Stream.Null   , LogLevel.Fatal, null, "Missing msg", Encoding.Unicode,  1718, false));
        }

        [TestMethod]
        public void Constructor()
        {
            CultureInfo frFr = CultureInfo.GetCultureInfo("fr-FR");

            // StreamLogger(Stream)
            AssertStreamLogger(
                LogLevel.Trace,
                CultureInfo.CurrentCulture,
                StreamLogger<int>.DefaultTemplate,
                EncodingCache.UTF8NoBOM,
                1024,
                false,
                (stream) => new StreamLogger<int>(stream));

            // StreamLogger(Stream, LogLevel)
            AssertStreamLogger(
                LogLevel.Warn,
                CultureInfo.CurrentCulture,
                StreamLogger<int>.DefaultTemplate,
                EncodingCache.UTF8NoBOM,
                1024,
                false,
                (stream) => new StreamLogger<int>(stream, LogLevel.Warn));

            // StreamLogger(Stream, LogLevel, string)
            AssertStreamLogger(
                LogLevel.Error,
                CultureInfo.CurrentCulture,
                "{l} {cxt} {msg}",
                EncodingCache.UTF8NoBOM,
                1024,
                false,
                (stream) => new StreamLogger<int>(stream, LogLevel.Error, "{l} {cxt} {msg}"));

            // StreamLogger(Stream, LogLevel, FormatProvider, string)
            AssertStreamLogger(
                LogLevel.Debug,
                frFr,
                ">> {cxt} {msg}",
                EncodingCache.UTF8NoBOM,
                1024,
                false,
                (stream) => new StreamLogger<int>(stream, LogLevel.Debug, frFr, ">> {cxt} {msg}"));

            // StreamLogger(Stream, LogLevel, FormatProvider, string, Encoding)
            AssertStreamLogger(
                LogLevel.Info,
                frFr,
                ">> {cxt} {msg}",
                Encoding.Unicode,
                1024,
                false,
                (stream) => new StreamLogger<int>(stream, LogLevel.Info, frFr, ">> {cxt} {msg}", Encoding.Unicode));

            // StreamLogger(Stream, LogLevel, FormatProvider, string, Encoding, int)
            AssertStreamLogger(
                LogLevel.Fatal,
                frFr,
                ">> {cxt} {msg}",
                Encoding.ASCII,
                500,
                false,
                (stream) => new StreamLogger<int>(stream, LogLevel.Fatal, frFr, ">> {cxt} {msg}", Encoding.ASCII, 500));

            // StreamLogger(Stream, LogLevel, FormatProvider, string, Encoding, int, bool)
            AssertStreamLogger(
                LogLevel.Debug,
                frFr,
                ">> {cxt} {msg}",
                Encoding.UTF32,
                2000,
                true,
                (stream) => new StreamLogger<int>(stream, LogLevel.Debug, frFr, ">> {cxt} {msg}", Encoding.UTF32, 2000, true));
        }

        [TestMethod]
        public void ThrowObjectDisposedException()
        {
            StreamLogger<Guid> logger = new StreamLogger<Guid>(Stream.Null);
            logger.Dispose();

            // Methods
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Log(default));
            Assert.ThrowsException<ObjectDisposedException>(() => logger.Flush());
        }

        [TestMethod]
        public void AutoFlush()
        {
            // AutoFlush = false
            using (StreamLogger<char> logger = new StreamLogger<char>(new MemoryStream()))
            {
                Assert.IsFalse(logger.AutoFlush);

                logger.Warn('A', "Warning!");
                Assert.AreEqual(0, logger.BaseStream.Position);
            }

            // AutoFlush = true
            using (StreamLogger<char> logger = new StreamLogger<char>(new MemoryStream()) { AutoFlush = true })
            {
                Assert.IsTrue(logger.AutoFlush);

                logger.Warn('A', "Warning!");
                Assert.AreNotEqual(0, logger.BaseStream.Position);
            }
        }

        [TestMethod]
        public void BaseStream()
        {
            using (MemoryStream stream = new MemoryStream())
            using (StreamLogger<TimeSpan> logger = new StreamLogger<TimeSpan>(stream))
                Assert.AreEqual(stream, logger.BaseStream);
        }

        [TestMethod]
        // Change name to not conflict with class
        public void EncodingProperty()
        {
            // Default
            using (StreamLogger<ulong> logger = new StreamLogger<ulong>(Stream.Null, LogLevel.Fatal, null, "{cxt} {msg}"))
                Assert.AreEqual(EncodingCache.UTF8NoBOM, logger.Encoding);

            // Non-null
            using (StreamLogger<ulong> logger = new StreamLogger<ulong>(Stream.Null, LogLevel.Fatal, null, "{cxt} {msg}", Encoding.UTF32))
                Assert.AreEqual(Encoding.UTF32, logger.Encoding);
        }

        [TestMethod]
        public void IsSynchronized()
        {
            using (Logger<string> logger = new StreamLogger<string>(Stream.Null))
                Assert.IsFalse(logger.IsSynchronized);
        }

        [TestMethod]
        public void NewLine()
        {
            using (StreamLogger<Guid> logger = new StreamLogger<Guid>(Stream.Null))
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
        public void SyncRoot()
        {
            using (Logger<double> logger = new StreamLogger<double>(Stream.Null))
                Assert.AreEqual(logger, logger.SyncRoot);
        }

        [TestMethod]
        public void Flush()
        {
            using (StreamLogger<int> logger = new StreamLogger<int>(new MemoryStream()))
            {
                Assert.IsFalse(logger.AutoFlush);

                logger.Warn(2, "Warnings!");
                Assert.AreEqual(0, logger.BaseStream.Position);

                logger.Flush();
                Assert.AreNotEqual(0, logger.BaseStream.Position);
            }
        }

        [TestMethod]
        public void Log()
        {
            using (StreamLogger<ulong> logger = new StreamLogger<ulong>(new MemoryStream(), LogLevel.Trace, CultureInfo.InvariantCulture, "[{ts:HH:mm:ss}] [{l,-5:F}] [User: {cxt,4}] - {msg}"))
            {
                DateTime timestamp = new DateTime(2999, 12, 31, 23, 59, 57);

                logger.Log(new LogEntry<ulong>(timestamp              , LogLevel.Warn ,  246, "Hello" ));
                logger.Log(new LogEntry<ulong>(timestamp.AddSeconds(1), LogLevel.Info ,  246, "Stream"));
                logger.Log(new LogEntry<ulong>(timestamp.AddSeconds(2), LogLevel.Warn , 1357, "Hello" ));
                logger.Log(new LogEntry<ulong>(timestamp.AddSeconds(2), LogLevel.Debug,  246, "World" ));

                logger.Flush();

                // Read back the log entries
                logger.BaseStream.Seek(0, SeekOrigin.Begin);
                using (StreamReader reader = new StreamReader(logger.BaseStream))
                {
                    Assert.AreEqual("[23:59:57] [Warn ] [User:  246] - Hello" , reader.ReadLine());
                    Assert.AreEqual("[23:59:58] [Info ] [User:  246] - Stream", reader.ReadLine());
                    Assert.AreEqual("[23:59:59] [Warn ] [User: 1357] - Hello" , reader.ReadLine());
                    Assert.AreEqual("[23:59:59] [Debug] [User:  246] - World" , reader.ReadLine());
                }
            }
        }

        private static void AssertStreamLogger(
            LogLevel                        expectedMinLevel,
            IFormatProvider                 expectedFormatProvider,
            string                          expectedTemplate,
            Encoding                        expectedEncoding,
            int                             expectedBufferSize,
            bool                            leaveOpen,
            Func<Stream, StreamLogger<int>> loggerFactory)
        {
            // Assert the various properties, except the buffer size
            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamLogger<int> actual = loggerFactory(stream))
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

        private static void AssertBufferSize(int expectedBufferSize, Func<Stream, StreamLogger<int>> loggerFactory)
        {
            string msg;
            DateTime timestamp = new DateTime(2019, 7, 2, 21, 37, 04);

            // Write a message that is the same length as the internal buffer
            using (StreamLogger<int> logger = loggerFactory(new MemoryStream()))
            {
                LogEntry<int> entry = new LogEntry<int>(timestamp, LogLevel.Warn, 7, string.Empty);
                int lineLength = logger.Template.Format(logger.FormatProvider, entry).Length + logger.NewLine.Length;

                // Note: The Preamble is not included in the buffer, and therefore its size
                //       doesn't impact how many characters fit in the buffer for the first time 
                msg = new string('a', expectedBufferSize - lineLength);

                // Write the message and assert that the buffer was not filled (ie. flushed)
                logger.Log(new LogEntry<int>(timestamp, LogLevel.Warn, 7, msg));
                Assert.AreEqual(0, logger.BaseStream.Position);
            }

            // Write a message just 1 more character than the length of the buffer
            using (StreamLogger<int> logger = loggerFactory(new MemoryStream()))
            {
                LogEntry<int> overflowMsg = new LogEntry<int>(timestamp, LogLevel.Warn, 7, msg + 'a');
                string line = logger.Template.Format(logger.FormatProvider, overflowMsg) + logger.NewLine;

                // Note: Without an explicit flush, only a buffer's length worth of characters
                //       will be written at a time to the underlying stream
                int expectedBytes = logger.Encoding.GetByteCount(line.Substring(0, expectedBufferSize));

                // Write the message and assert that the buffer flushed when logging
                logger.Log(overflowMsg);
                Assert.AreEqual(logger.Encoding.Preamble.Length + expectedBytes, logger.BaseStream.Position);
            }
        }
    }
}
