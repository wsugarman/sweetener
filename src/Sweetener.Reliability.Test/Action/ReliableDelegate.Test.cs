using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    public class ReliableDelegateTest
    {
        private static readonly Func<ReliableDelegate, ExceptionPolicy   > s_getExceptionPolicy = DynamicGetter.ForField<ReliableDelegate, ExceptionPolicy   >("_canRetry");
        private static readonly Func<ReliableDelegate, ComplexDelayPolicy> s_getDelayPolicy     = DynamicGetter.ForField<ReliableDelegate, ComplexDelayPolicy>("_getDelay");

        internal void Ctor(ReliableDelegate reliableAction, int expectedMaxRetries, ExceptionPolicy expectedExceptionPolicy, ComplexDelayPolicy expectedDelayPolicy)
            => Ctor(reliableAction, expectedMaxRetries, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        internal void Ctor(ReliableDelegate reliableAction, int expectedMaxRetries, ExceptionPolicy expectedExceptionPolicy, FuncProxy<int, TimeSpan> expectedDelayPolicy)
            => Ctor(reliableAction, expectedMaxRetries, expectedExceptionPolicy, actual =>
            {
                TimeSpan expectedDelay = expectedDelayPolicy.Invoke(42);
                int expectedCalls = expectedDelayPolicy.Calls + 1;

                expectedDelayPolicy.Invoking += (i, c) => Assert.AreEqual(i, 42);
                Assert.AreEqual(expectedDelay, actual(42, new ArgumentOutOfRangeException()));
                Assert.AreEqual(expectedCalls, expectedDelayPolicy.Calls);
            });

        private void Ctor(ReliableDelegate reliableAction, int expectedMaxRetries, ExceptionPolicy expectedExceptionPolicy, Action<ComplexDelayPolicy> validateDelayPolicy)
        {
            Assert.AreEqual(expectedMaxRetries, reliableAction.MaxRetries);
            Assert.AreSame(expectedExceptionPolicy, s_getExceptionPolicy(reliableAction));

            validateDelayPolicy(s_getDelayPolicy(reliableAction));
        }
    }
}
