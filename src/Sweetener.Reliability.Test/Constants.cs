using System;

namespace Sweetener.Reliability.Test
{
    internal static class Constants
    {
        internal const double MinFactor = 1/2;

        // Note that the resolution for the Windows system clock is ~15 milliseconds, so
        // any time used for our delay should be greater than 15 milliseconds
        internal static readonly TimeSpan Delay    = TimeSpan.FromMilliseconds(30);
        internal static readonly TimeSpan MinDelay = TimeSpan.FromMilliseconds(Delay.TotalMilliseconds * MinFactor);
    }
}
