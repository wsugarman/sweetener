using System;
using System.Collections.Generic;
using System.Text;

namespace Sweetener.Reliability.Policies
{
    public static class Retry
    {
        public const int Infinite = -1;

        public static IRetryPolicy AllExceptions()
        {

        }

        public static IRetryPolicy<T> AllExceptions<T>()
        {

        }

        // TODO: On specific exceptions, etc
    }
}
