using System;
using System.Diagnostics.CodeAnalysis;

namespace Sweetener.Logging
{
    internal enum TemplateParameter
    {
        Message     = 0,
        Context     = 1,
        Timestamp   = 2,
        Level       = 3,
        ProcessId   = 4,
        ProcessName = 5,
        ThreadId    = 6,
        ThreadName  = 7,
    }

    internal static class TemplateParameterName
    {
        // TODO: Re-enable code coverage
        // The IL generated for switch statements may lead to false negatives
        // when checking code coverage due to unreachable code blocks
        [ExcludeFromCodeCoverage]
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
                case "cxt":
                case "context":
                    return TemplateParameter.Context;
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
