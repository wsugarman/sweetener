using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    public class ReliableDelegateTest<T>
    {
        protected static readonly Func<ReliableDelegate<T>, ResultPolicy<T>      > s_getResultPolicy    = DynamicGetter.ForField<ReliableDelegate<T>, ResultPolicy<T>      >("_validate");
        protected static readonly Func<ReliableDelegate<T>, ExceptionPolicy      > s_getExceptionPolicy = DynamicGetter.ForField<ReliableDelegate<T>, ExceptionPolicy      >("_canRetry");
        protected static readonly Func<ReliableDelegate<T>, ComplexDelayPolicy<T>> s_getDelayPolicy     = DynamicGetter.ForField<ReliableDelegate<T>, ComplexDelayPolicy<T>>("_getDelay");

        internal void Ctor(ReliableDelegate<T> reliableAction, int expectedMaxRetries, ExceptionPolicy expectedExceptionPolicy, FuncProxy<int, TimeSpan> expectedDelayPolicy)
            => Ctor(reliableAction, expectedMaxRetries, ReliableDelegate<T>.DefaultResultPolicy, expectedExceptionPolicy, expectedDelayPolicy);

        internal void Ctor(ReliableDelegate<T> reliableAction, int expectedMaxRetries, ExceptionPolicy expectedExceptionPolicy, ComplexDelayPolicy<T> expectedDelayPolicy)
            => Ctor(reliableAction, expectedMaxRetries, ReliableDelegate<T>.DefaultResultPolicy, expectedExceptionPolicy, expectedDelayPolicy);

        internal void Ctor(ReliableDelegate<T> reliableAction, int expectedMaxRetries, ResultPolicy<T> expectedResultPolicy, ExceptionPolicy expectedExceptionPolicy, FuncProxy<int, TimeSpan> expectedDelayPolicy)
            => Ctor(reliableAction, expectedMaxRetries, expectedResultPolicy, expectedExceptionPolicy, actual =>
            {
                TimeSpan expectedDelay = expectedDelayPolicy.Invoke(42);
                int expectedCalls = expectedDelayPolicy.Calls + 1;

                expectedDelayPolicy.Invoking += (i, c) => Assert.AreEqual(i, 42);
                Assert.AreEqual(expectedDelay, actual(42, default, default));
                Assert.AreEqual(expectedCalls, expectedDelayPolicy.Calls);
            });

        internal void Ctor(ReliableDelegate<T> reliableAction, int expectedMaxRetries, ResultPolicy<T> expectedResultPolicy, ExceptionPolicy expectedExceptionPolicy, ComplexDelayPolicy<T> expectedDelayPolicy)
            => Ctor(reliableAction, expectedMaxRetries, expectedResultPolicy, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        private void Ctor(ReliableDelegate<T> reliableAction, int expectedMaxRetries, ResultPolicy<T> expectedResultPolicy, ExceptionPolicy expectedExceptionPolicy, Action<ComplexDelayPolicy<T>> validateDelayPolicy)
        {
            Assert.AreEqual(expectedMaxRetries, reliableAction.MaxRetries);
            Assert.AreSame(expectedResultPolicy   , s_getResultPolicy   (reliableAction));
            Assert.AreSame(expectedExceptionPolicy, s_getExceptionPolicy(reliableAction));

            validateDelayPolicy(s_getDelayPolicy(reliableAction));
        }
    }
}
