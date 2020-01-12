using System;

namespace Sweetener.Reliability.Test
{
    internal readonly struct CallContext
    {
        public readonly int Calls;

        public readonly TimeSpan TimeSinceLastCall;

        public CallContext(int calls, TimeSpan timeSinceLastCall)
        {
            Calls             = calls;
            TimeSinceLastCall = timeSinceLastCall;
        }
    }
}
