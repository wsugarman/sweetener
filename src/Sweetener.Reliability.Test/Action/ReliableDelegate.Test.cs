using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    public class ReliableDelegateTest
    {
        private static readonly Func<ReliableDelegate, ExceptionHandler   > s_getExceptionHandler = DynamicGetter.ForField<ReliableDelegate, ExceptionHandler   >("_canRetry");
        private static readonly Func<ReliableDelegate, ComplexDelayHandler> s_getDelayHandler     = DynamicGetter.ForField<ReliableDelegate, ComplexDelayHandler>("_getDelay");

        internal void Ctor(ReliableDelegate reliableAction, int expectedMaxRetries, ExceptionHandler expectedExceptionPolicy, FuncProxy<int, TimeSpan> expectedDelayPolicy)
            => Ctor(reliableAction, expectedMaxRetries, expectedExceptionPolicy, actual =>
            {
                TimeSpan expectedDelay = expectedDelayPolicy.Invoke(42);
                int expectedCalls = expectedDelayPolicy.Calls + 1;

                expectedDelayPolicy.Invoking += (i, c) => Assert.AreEqual(i, 42);
                Assert.AreEqual(expectedDelay, actual(42, new ArgumentOutOfRangeException()));
                Assert.AreEqual(expectedCalls, expectedDelayPolicy.Calls);
            });

        internal void Ctor(ReliableDelegate reliableAction, int expectedMaxRetries, ExceptionHandler expectedExceptionPolicy, ComplexDelayHandler expectedDelayPolicy)
            => Ctor(reliableAction, expectedMaxRetries, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        private void Ctor(ReliableDelegate reliableAction, int expectedMaxRetries, ExceptionHandler expectedExceptionPolicy, Action<ComplexDelayHandler> validateDelayPolicy)
        {
            Assert.AreEqual(expectedMaxRetries, reliableAction.MaxRetries);
            Assert.AreSame(expectedExceptionPolicy, s_getExceptionHandler(reliableAction));

            validateDelayPolicy(s_getDelayHandler(reliableAction));
        }
    }
}
