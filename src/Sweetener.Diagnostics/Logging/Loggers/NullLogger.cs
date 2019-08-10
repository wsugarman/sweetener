using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Sweetener.Diagnostics.Logging
{
    internal sealed class NullLogger : Logger
    {
        public override IFormatProvider FormatProvider => CultureInfo.InvariantCulture;

        public override bool IsThreadSafe => true;

        public override LogLevel MinLevel => LogLevel.Trace;

        [ExcludeFromCodeCoverage]
        protected override void Add(LogEntry logEntry)
        { }

        public override void Log(LogLevel level, string message)
        { }

        public override void Log(LogLevel level, string format, object arg0)
        { }

        public override void Log(LogLevel level, string format, object arg0, object arg1)
        { }

        public override void Log(LogLevel level, string format, object arg0, object arg1, object arg2)
        { }

        public override void Log(LogLevel level, string format, params object[] args)
        { }
    }
}
