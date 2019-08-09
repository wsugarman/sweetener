using System;
using System.Collections.Generic;

namespace Sweetener.Diagnostics.Logging.Test
{
    internal class MemoryLogger<T> : Logger<T>
    {
        public Queue<LogEntry<T>> Entries { get; } = new Queue<LogEntry<T>>();

        public override IFormatProvider FormatProvider { get; }

        public override LogLevel MinLevel { get; }

        public MemoryLogger(LogLevel minLevel, IFormatProvider formatProvider)
        {
            FormatProvider = formatProvider;
            MinLevel       = minLevel;
        }

        protected override void Add(LogEntry<T> logEntry)
            => Entries.Enqueue(logEntry);
    }
}
