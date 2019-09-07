using System;

namespace Sweetener.Reliability
{
    public interface IDelayPolicy
    {
        TimeSpan GetExceptionDelay(int attempt, Exception e);
    }

    public interface IDelayPolicy<in T> : IDelayPolicy
    {
        TimeSpan GetResultDelay(int attempt, T result);
    }
}
