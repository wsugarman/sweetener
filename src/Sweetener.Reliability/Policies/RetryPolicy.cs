using System;
using System.Collections.Generic;
using System.Text;

namespace Sweetener.Reliability
{
    internal abstract partial class RetryPolicy
    {
        public abstract bool IsTransient(Exception exception);
    }
}
