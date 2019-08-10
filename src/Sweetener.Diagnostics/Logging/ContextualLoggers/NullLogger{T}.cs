using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Sweetener.Diagnostics.Logging
{
    internal sealed class NullLogger<T> : Logger<T>
    {
        public override IFormatProvider FormatProvider => CultureInfo.InvariantCulture;

        public override bool IsThreadSafe => true;

        public override LogLevel MinLevel => LogLevel.Trace;

        [ExcludeFromCodeCoverage]
        protected override void Add(LogEntry<T> logEntry)
        { }

        public override void Log(LogLevel level, T context, string message)
        { }

        public override void Log(LogLevel level, T context, string format, object arg0)
        { }

        public override void Log(LogLevel level, T context, string format, object arg0, object arg1)
        { }

        public override void Log(LogLevel level, T context, string format, object arg0, object arg1, object arg2)
        { }

        public override void Log(LogLevel level, T context, string format, params object[] args)
        { }
    }
}
