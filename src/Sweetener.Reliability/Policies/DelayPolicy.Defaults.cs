using System;
using System.Collections.Generic;
using System.Text;

namespace Sweetener.Reliability
{
    partial class DelayPolicy
    {
        public static DelayPolicy ExponentialBackoff(TimeSpan initial)
            => new ExpoentialBackoffDelayPolicy(initial);

        private class ExpoentialBackoffDelayPolicy : DelayPolicy
        {
            private readonly TimeSpan _initial;

            public ExpoentialBackoffDelayPolicy(TimeSpan initial)
            {
                if (initial <= TimeSpan.Zero)
                    throw new ArgumentOutOfRangeException(nameof(initial));

                _initial = initial;
            }

            public override TimeSpan GetDelay(int attempt, Exception e)
                => TimeSpan.FromMilliseconds(_initial.TotalMilliseconds * Math.Exp(attempt));
        }
    }
}
