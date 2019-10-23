using System;

namespace Sweetener.Reliability.Test
{
    public class ReliableDelegateTest
    {
        protected static readonly Func<ReliableDelegate, ExceptionPolicy   > s_getExceptionPolicy = DynamicGetter.ForField<ReliableDelegate, ExceptionPolicy   >("_canRetry");
        protected static readonly Func<ReliableDelegate, ComplexDelayPolicy> s_getDelayPolicy     = DynamicGetter.ForField<ReliableDelegate, ComplexDelayPolicy>("_getDelay");
    }
}
