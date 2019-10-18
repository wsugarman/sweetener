using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    public class ReliableDelegateTest
    {
        internal static readonly TimeSpan ConstantDelay = TimeSpan.FromMilliseconds(30);

        internal const double MinDelay = 30;
        internal const double MaxDelay = 50;

        private static readonly Func<ReliableDelegate, ExceptionPolicy   > s_getExceptionPolicy = DynamicGetter.ForField<ReliableDelegate, ExceptionPolicy   >("_canRetry");
        private static readonly Func<ReliableDelegate, ComplexDelayPolicy> s_getDelayPolicy     = DynamicGetter.ForField<ReliableDelegate, ComplexDelayPolicy>("_getDelay");

        // We cannot assert that the reference to the DelayPolicy is the same in the ReliableDelegate as we wrap it,
        // so we can only validate the functionality using some user-defined action
        protected void Ctor(ReliableDelegate reliableAction, int expectedMaxRetries, ExceptionPolicy expectedExceptionPolicy, ComplexDelayPolicy expectedDelayPolicy)
            => Ctor(reliableAction, expectedMaxRetries, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        protected void Ctor(ReliableDelegate reliableAction, int expectedMaxRetries, ExceptionPolicy expectedExceptionPolicy, Action<ComplexDelayPolicy> validateDelayPolicy)
        {
            Assert.AreEqual(expectedMaxRetries     , reliableAction.MaxRetries);
            Assert.AreSame (expectedExceptionPolicy, s_getExceptionPolicy(reliableAction));
            validateDelayPolicy(s_getDelayPolicy(reliableAction));
        }
    }
}
