using System;
using System.Collections.Generic;
using System.Text;

namespace Sweetener.Reliability
{
    public interface IRetryPolicy
    {
        TimeSpan IsTransient(int attempt, Exception e);
    }

    public interface IRetryPolicy<in T> : IDelayPolicy
    {
        //TimeSpan ShouldRetryResult(int attempt, T result);
    }
}
