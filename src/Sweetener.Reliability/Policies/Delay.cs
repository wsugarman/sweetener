using System;

namespace Sweetener.Reliability.Policies
{
    public static class Delay
    {
        public static IDelayPolicy Exponentially(TimeSpan initial)
            => new ExpoentialBackoffDelayPolicy(initial);

        private sealed class AttemptDelayPolicy : IDelayPolicy
        {
            private readonly Func<int, TimeSpan> _getDelay;

            public AttemptDelayPolicy(Func<int, TimeSpan> getDelay)
                => _getDelay = getDelay;

            public TimeSpan GetExceptionDelay(int attempt, Exception e)
                => _getDelay(attempt);
        }

        private static TimeSpan GetExponentialDelay(int attempt)
            => 
    }
}
