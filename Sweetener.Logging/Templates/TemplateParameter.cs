using System;
using System.IO;

namespace Sweetener.Logging
{
    internal enum TemplateParameter
    {
        Message     = 0,
        Timestamp   = 1,
        Level       = 2,
        ProcessId   = 3,
        ProcessName = 4,
        ThreadId    = 5,
        ThreadName  = 6,
    }

    internal static class TemplateParameterName
    {
        public static TemplateParameter Parse(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            // TODO: Case insensitive?
            switch (name)
            {
                case "msg":
                case "message":
                    return TemplateParameter.Message;
                case "ts":
                case "timestamp":
                    return TemplateParameter.Timestamp;
                case "l":
                case "level":
                    return TemplateParameter.Level;
                case "pid":
                case "processId":
                    return TemplateParameter.ProcessId;
                case "pn":
                case "processName":
                    return TemplateParameter.ProcessName;
                case "tid":
                case "threadId":
                    return TemplateParameter.ThreadId;
                case "tn":
                case "threadName":
                    return TemplateParameter.ThreadName;
                default:
                    throw new FormatException($"Unrecognized template parameter '{name}'.");
            }
        }
    }
}
