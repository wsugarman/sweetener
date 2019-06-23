using System;
using System.Collections.Generic;

namespace Sweetener.Logging.Test
{
    internal class MemoryTemplateLogger : TemplateLogger
    {
        public Queue<string> Entries { get; } = new Queue<string>();

        public MemoryTemplateLogger()
            : base()
        { }

        public MemoryTemplateLogger(LogLevel minLevel)
            : base(minLevel)
        { }

        public MemoryTemplateLogger(LogLevel minLevel, string template)
            : base(minLevel, template)
        { }

        public MemoryTemplateLogger(LogLevel minLevel, IFormatProvider formatProvider, string template)
            : base(minLevel, formatProvider, template)
        { }

        protected override void WriteLine(string message)
            => Entries.Enqueue(message);
    }
}
