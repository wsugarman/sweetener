using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    internal static class PolicyValidator
    {
        public static ObservableFunc<int, TimeSpan> Create(DelayPolicy policy)
        {
            ObservableFunc<int, TimeSpan> observableFunc = new ObservableFunc<int, TimeSpan>(policy.Invoke);
            observableFunc.Invoking += i => Assert.AreEqual(observableFunc.Calls, i); // Attempt == # of calls

            return observableFunc;
        }

        public static ObservableFunc<int, Exception, TimeSpan> Create<T>(ComplexDelayPolicy policy)
            where T : Exception
        {
            ObservableFunc<int, Exception, TimeSpan> observableFunc = new ObservableFunc<int, Exception, TimeSpan>(policy.Invoke);
            observableFunc.Invoking +=
                (i, e) =>
                {
                    Assert.AreEqual(observableFunc.Calls, i);
                    Assert.AreEqual(typeof(T), e.GetType());
                };

            return observableFunc;
        }

        public static ObservableFunc<Exception, bool> Create(ExceptionPolicy policy)
        {
            ObservableFunc<Exception, bool> observableFunc = new ObservableFunc<Exception, bool>(policy.Invoke);
            observableFunc.Invoking += e => Assert.Fail();

            return observableFunc;
        }

        public static ObservableFunc<Exception, bool> Create<T>(ExceptionPolicy policy)
            where T : Exception
        {
            ObservableFunc<Exception, bool> observableFunc = new ObservableFunc<Exception, bool>(policy.Invoke);
            observableFunc.Invoking += e => Assert.AreEqual(typeof(T), e.GetType());

            return observableFunc;
        }

        public static ObservableFunc<Exception, bool> Create<TTransient, TFatal>(ExceptionPolicy policy)
            where TTransient : Exception
            where TFatal     : Exception
        {
            ObservableFunc<Exception, bool> observableFunc = new ObservableFunc<Exception, bool>(policy.Invoke);
            observableFunc.Invoking += e => Assert.IsNotNull(e);
            observableFunc.Invoked +=
                (e, isTransient) =>
                {
                    if (isTransient)
                        Assert.AreEqual(typeof(TTransient), e.GetType());
                    else
                        Assert.AreEqual(typeof(TFatal), e.GetType());
                };

            return observableFunc;
        }
    }
}
