using System;

namespace Sweetener.Diagnostics.Logging
{
    internal interface ILogEntryTemplate
    {
        string Format(IFormatProvider provider, LogEntry logEntry);
    }
}
