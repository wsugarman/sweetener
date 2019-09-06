using System;
using System.Collections.Generic;
using System.Text;

namespace Sweetener.Reliability
{
    public abstract partial class DelayPolicy
    {
        public abstract TimeSpan GetDelay(int attempt, Exception e);
    }
}
