using System;

namespace Sweetener.Diagnostics.Logging
{
    internal interface ILogEntryTemplate<T>
    {
        string Format(IFormatProvider provider, LogEntry<T> logEntry);
    }
}
