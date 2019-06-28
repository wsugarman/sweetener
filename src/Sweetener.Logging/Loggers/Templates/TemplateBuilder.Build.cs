using System;
using System.Diagnostics;

namespace Sweetener.Logging
{
    partial class TemplateBuilder
    {
        public virtual ILogEntryTemplate Build()
        {
            if (_indices.ContainsKey(TemplateParameter.Context))
                throw new InvalidOperationException("Template cannot contain 'cxt' or 'context' parameter");

            if (!_indices.ContainsKey(TemplateParameter.Message))
                throw new InvalidOperationException("Template is missing required 'msg' or 'message' parameter");

            // The use of an interface will force the template (a struct) to box, but
            // it will provide a hook for polymorphism and further cement the relationship
            // between the builder and the template itself
            return new LogEntryTemplate(_template, _format);
        }

        private readonly struct LogEntryTemplate : ILogEntryTemplate
        {
            private readonly string _format;
            private readonly string _template;

            private static readonly Process s_currentProcess = Process.GetCurrentProcess();

            public LogEntryTemplate(string template, string format)
            {
                // TODO: While the originalTemplate is really bloat in production, it is
                //       very helpful to have when debugging and for unit testing.
                //       That said, is there a better mechanism that doesn't involve
                //       different code between debug/release configuration?
                _format   = format;
                _template = template;
            }

            public string Format(IFormatProvider provider, LogEntry logEntry)
            {
                // TODO: It seems silly to use the object[] override here when we avoid the
                //       object[] allocation in the ILogger interface. We should figure out
                //       how to avoid this allocation too if possible.
                // TODO: Can we replace the process parameters with real values when building?
                return string.Format(provider, _format,
                    logEntry.Message,              // {0} - Message
                    null,                          // {1} - Context (Unused)
                    logEntry.Timestamp,            // {2} - Timestamp
                    logEntry.Level,                // {3} - LogLevel
                    s_currentProcess.Id,           // {4} - Process Id
                    s_currentProcess.ProcessName,  // {5} - Process Name
                    logEntry.ThreadId,             // {6} - Thread Id
                    logEntry.ThreadName);          // {7} - Thread Name
            }

            public override string ToString()
                => _template;
        }
    }
}
