using System;
using System.Collections.Generic;

namespace Sweetener.Logging.Test
{
    internal class MemoryLogger : Logger
    {
        public Queue<LogEntry> Entries { get; } = new Queue<LogEntry>();

        public override IFormatProvider FormatProvider { get; }

        public override LogLevel MinLevel { get; }

        public MemoryLogger(LogLevel minLevel, IFormatProvider formatProvider)
        {
            FormatProvider = formatProvider;
            MinLevel       = minLevel;
        }

        protected override void Add(LogEntry logEntry)
            => Entries.Enqueue(logEntry);
    }
}
