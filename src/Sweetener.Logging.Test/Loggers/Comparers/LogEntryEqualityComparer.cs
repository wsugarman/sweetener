using System;
using System.Collections.Generic;

namespace Sweetener.Logging.Test
{
    internal class LogEntryEqualityComparer : IEqualityComparer<LogEntry>
    {
        public static readonly IEqualityComparer<LogEntry> WithTimestamp = new LogEntryEqualityComparer(includeTimestamp: true);

        public static readonly IEqualityComparer<LogEntry> NoTimestamp = new LogEntryEqualityComparer(includeTimestamp: false);

        private readonly bool _includeTimestamp;

        private LogEntryEqualityComparer(bool includeTimestamp)
        {
            _includeTimestamp = includeTimestamp;
        }

        public bool Equals(LogEntry x, LogEntry y)
            => (!_includeTimestamp || EqualityComparer<DateTime>.Default.Equals(x.Timestamp, y.Timestamp))
            && EqualityComparer<LogLevel>.Default.Equals(x.Level     , y.Level     )
            && EqualityComparer<string  >.Default.Equals(x.Message   , y.Message   )
            && EqualityComparer<int     >.Default.Equals(x.ThreadId  , y.ThreadId  )
            && EqualityComparer<string  >.Default.Equals(x.ThreadName, y.ThreadName);

        public int GetHashCode(LogEntry obj)
        {
            int hash = HashHelpers.RandomSeed;

            if (_includeTimestamp)
                hash = HashHelpers.Combine(hash, obj.Timestamp.GetHashCode());

            hash = HashHelpers.Combine(hash, obj.Level      .GetHashCode()     );
            hash = HashHelpers.Combine(hash, obj.Message?   .GetHashCode() ?? 0);
            hash = HashHelpers.Combine(hash, obj.ThreadId   .GetHashCode()     );
            hash = HashHelpers.Combine(hash, obj.ThreadName?.GetHashCode() ?? 0);

            return hash;
        }
    }
}
