using System;
using System.Collections.Generic;

namespace Sweetener.Diagnostics.Logging.Test
{
    internal class LogEntryEqualityComparer<T> : IEqualityComparer<LogEntry<T>>
    {
        public static readonly IEqualityComparer<LogEntry<T>> WithTimestamp = new LogEntryEqualityComparer<T>(includeTimestamp: true);

        public static readonly IEqualityComparer<LogEntry<T>> NoTimestamp = new LogEntryEqualityComparer<T>(includeTimestamp: false);

        private readonly bool _includeTimestamp;

        private LogEntryEqualityComparer(bool includeTimestamp)
        {
            _includeTimestamp = includeTimestamp;
        }

        public bool Equals(LogEntry<T> x, LogEntry<T> y)
            => (!_includeTimestamp || EqualityComparer<DateTime>.Default.Equals(x.Timestamp, y.Timestamp))
            && EqualityComparer<LogLevel>.Default.Equals(x.Level     , y.Level     )
            && EqualityComparer<T       >.Default.Equals(x.Context   , y.Context   )
            && EqualityComparer<string  >.Default.Equals(x.Message   , y.Message   )
            && EqualityComparer<int     >.Default.Equals(x.ThreadId  , y.ThreadId  )
            && EqualityComparer<string  >.Default.Equals(x.ThreadName, y.ThreadName);

        public int GetHashCode(LogEntry<T> obj)
            => throw new NotImplementedException();
    }
}
