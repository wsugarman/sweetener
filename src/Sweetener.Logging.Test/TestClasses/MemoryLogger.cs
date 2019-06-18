using System;
using System.Collections.Generic;

namespace Sweetener.Logging.Test
{
    internal class MemoryLogger : Logger
    {
        public Queue<LogEntry<string>> Entries { get; } = new Queue<LogEntry<string>>();

        public MemoryLogger()
            : base()
        { }

        public MemoryLogger(LogLevel minLevel)
            : base(minLevel)
        { }

        public MemoryLogger(LogLevel minLevel, IFormatProvider formatProvider)
            : base(minLevel, formatProvider)
        { }

        protected internal override void Log(LogEntry<string> logEntry)
            => Entries.Enqueue(logEntry);
    }
}
