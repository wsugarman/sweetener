using System;
using System.Collections.Generic;

namespace Sweetener.Logging.Test
{
    internal class MemoryLogger<T> : Logger<T>
    {
        public Queue<LogEntry<T>> Entries { get; } = new Queue<LogEntry<T>>();

        public MemoryLogger()
            : base()
        { }

        public MemoryLogger(LogLevel minLevel)
            : base(minLevel)
        { }

        protected internal override void Log(LogEntry<T> logEntry)
            => Entries.Enqueue(logEntry);
    }
}
