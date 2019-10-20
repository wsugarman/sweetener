using System;

namespace Sweetener.Reliability.Test
{
    public class ReliableDelegateTest
    {
        internal static readonly TimeSpan ConstantDelay = TimeSpan.FromMilliseconds(30);
        internal static readonly TimeSpan MinDelay      = TimeSpan.FromMilliseconds(ConstantDelay.TotalMilliseconds * 0.8);

        protected static readonly Func<ReliableDelegate, ExceptionPolicy   > s_getExceptionPolicy = DynamicGetter.ForField<ReliableDelegate, ExceptionPolicy   >("_canRetry");
        protected static readonly Func<ReliableDelegate, ComplexDelayPolicy> s_getDelayPolicy     = DynamicGetter.ForField<ReliableDelegate, ComplexDelayPolicy>("_getDelay");
    }
}
