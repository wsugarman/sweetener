using System;
using System.Collections.Generic;
using System.Text;

namespace Sweetener.Reliability
{
    internal abstract partial class DelayPolicy
    {
        public abstract TimeSpan GetDelay(int attempt, Exception e);
    }
}
