using System;
using System.Diagnostics;

namespace Sweetener.Logging
{
    partial class TemplateBuilder
    {
        public virtual ILogEntryTemplate<T> Build<T>()
        {
            if (!_indices.ContainsKey(TemplateParameter.Context))
                throw new InvalidOperationException("Template is missing required 'cxt' or 'context' parameter");

            if (!_indices.ContainsKey(TemplateParameter.Message))
                throw new InvalidOperationException("Template is missing required 'msg' or 'message' parameter");

            return new LogEntryTemplate<T>(_template, _format);
        }

        private readonly struct LogEntryTemplate<T> : ILogEntryTemplate<T>
        {
            private readonly string _format;
            private readonly string _template;

            private static readonly Process s_currentProcess = Process.GetCurrentProcess();

            public LogEntryTemplate(string template, string format)
            {
                _format   = format;
                _template = template;
            }

            public string Format(IFormatProvider provider, LogEntry<T> logEntry)
            {
                return string.Format(provider, _format,
                    logEntry.Message,             // {0} - Message
                    logEntry.Context,             // {1} - Context
                    logEntry.Timestamp,           // {2} - Timestamp
                    logEntry.Level,               // {3} - LogLevel
                    s_currentProcess.Id,          // {4} - Process Id
                    s_currentProcess.ProcessName, // {5} - Process Name
                    logEntry.ThreadId,            // {6} - Thread Id
                    logEntry.ThreadName);         // {7} - Thread Name
            }

            public override string ToString()
                => _template;
        }
    }
}
