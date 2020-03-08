using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweetener.Reflection;

namespace Sweetener.Reliability.Test
{
    public class ReliableDelegateTest<T>
    {
        protected static readonly Func<ReliableDelegate<T>, ResultHandler<T>      > s_getResultHandler    = DynamicGetter.ForField<ReliableDelegate<T>, ResultHandler<T>      >("_validate");
        protected static readonly Func<ReliableDelegate<T>, ExceptionHandler      > s_getExceptionHandler = DynamicGetter.ForField<ReliableDelegate<T>, ExceptionHandler      >("_canRetry");
        protected static readonly Func<ReliableDelegate<T>, ComplexDelayHandler<T>> s_getDelayHandler     = DynamicGetter.ForField<ReliableDelegate<T>, ComplexDelayHandler<T>>("_getDelay");

        internal void Ctor(ReliableDelegate<T> reliableAction, int expectedMaxRetries, ExceptionHandler expectedExceptionPolicy, FuncProxy<int, TimeSpan> expectedDelayPolicy)
            => Ctor(reliableAction, expectedMaxRetries, ResultPolicy.Default<T>(), expectedExceptionPolicy, expectedDelayPolicy);

        internal void Ctor(ReliableDelegate<T> reliableAction, int expectedMaxRetries, ExceptionHandler expectedExceptionPolicy, ComplexDelayHandler<T> expectedDelayPolicy)
            => Ctor(reliableAction, expectedMaxRetries, ResultPolicy.Default<T>(), expectedExceptionPolicy, expectedDelayPolicy);

        internal void Ctor(ReliableDelegate<T> reliableAction, int expectedMaxRetries, ResultHandler<T> expectedResultPolicy, ExceptionHandler expectedExceptionPolicy, FuncProxy<int, TimeSpan> expectedDelayPolicy)
            => Ctor(reliableAction, expectedMaxRetries, expectedResultPolicy, expectedExceptionPolicy, actual =>
            {
                TimeSpan expectedDelay = expectedDelayPolicy.Invoke(42);
                int expectedCalls = expectedDelayPolicy.Calls + 1;

                expectedDelayPolicy.Invoking += (i, c) => Assert.AreEqual(i, 42);
                Assert.AreEqual(expectedDelay, actual(42, default, default));
                Assert.AreEqual(expectedCalls, expectedDelayPolicy.Calls);
            });

        internal void Ctor(ReliableDelegate<T> reliableAction, int expectedMaxRetries, ResultHandler<T> expectedResultPolicy, ExceptionHandler expectedExceptionPolicy, ComplexDelayHandler<T> expectedDelayPolicy)
            => Ctor(reliableAction, expectedMaxRetries, expectedResultPolicy, expectedExceptionPolicy, actual => Assert.AreSame(expectedDelayPolicy, actual));

        private void Ctor(ReliableDelegate<T> reliableAction, int expectedMaxRetries, ResultHandler<T> expectedResultPolicy, ExceptionHandler expectedExceptionPolicy, Action<ComplexDelayHandler<T>> validateDelayPolicy)
        {
            Assert.AreEqual(expectedMaxRetries, reliableAction.MaxRetries);
            Assert.AreSame(expectedResultPolicy   , s_getResultHandler   (reliableAction));
            Assert.AreSame(expectedExceptionPolicy, s_getExceptionHandler(reliableAction));

            validateDelayPolicy(s_getDelayHandler(reliableAction));
        }
    }
}
