using System;

namespace Sweetener.Logging
{
    internal interface ILogEntryTemplate
    {
        string Format(IFormatProvider provider, LogEntry logEntry);
    }
}
