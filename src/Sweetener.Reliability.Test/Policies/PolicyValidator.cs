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

        public static ObservableFunc<int, TResult, Exception, TimeSpan> Create<TResult, TException>(ComplexDelayPolicy<TResult> policy, TResult transientResult)
            where TResult : IEquatable<TResult>
            where TException : Exception
        {
            ObservableFunc<int, TResult, Exception, TimeSpan> observableFunc = new ObservableFunc<int, TResult, Exception, TimeSpan>(policy.Invoke);
            observableFunc.Invoking +=
                (i, r, e) =>
                {
                    Assert.AreEqual(observableFunc.Calls, i);

                    if (e == null)
                    {
                        Assert.AreEqual(transientResult, r);
                    }
                    else
                    {
                        Assert.AreEqual(default, r);
                        Assert.AreEqual(typeof(TException), e.GetType());
                    }
                };

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
            where TFatal : Exception
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

        public static ObservableFunc<T, ResultKind> Create<T>(ResultPolicy<T> policy, T result)
            where T : IEquatable<T>
        {
            ObservableFunc<T, ResultKind> observableFunc = new ObservableFunc<T, ResultKind>(policy.Invoke);
            observableFunc.Invoking += r => Assert.AreEqual(result, r);

            return observableFunc;
        }

        public static ObservableFunc<int, TimeSpan> IgnoreDelayPolicy()
        {
            ObservableFunc<int, TimeSpan> observableFunc = new ObservableFunc<int, TimeSpan>(i => TimeSpan.Zero);
            observableFunc.Invoking += i => Assert.Fail();

            return observableFunc;
        }

        public static ObservableFunc<int, Exception, TimeSpan> IgnoreComplexDelayPolicy()
        {
            ObservableFunc<int, Exception, TimeSpan> observableFunc = new ObservableFunc<int, Exception, TimeSpan>((i, e) => TimeSpan.Zero);
            observableFunc.Invoking += (i, e) => Assert.Fail();

            return observableFunc;
        }

        public static ObservableFunc<int, T, Exception, TimeSpan> IgnoreComplexDelayPolicy<T>()
        {
            ObservableFunc<int, T, Exception, TimeSpan> observableFunc = new ObservableFunc<int, T, Exception, TimeSpan>((i, r, e) => TimeSpan.Zero);
            observableFunc.Invoking += (i, r, e) => Assert.Fail();

            return observableFunc;
        }

        public static ObservableFunc<Exception, bool> IgnoreExceptionPolicy()
        {
            ObservableFunc<Exception, bool> observableFunc = new ObservableFunc<Exception, bool>(r => false);
            observableFunc.Invoking += e => Assert.Fail();

            return observableFunc;
        }

        public static ObservableFunc<T, ResultKind> IgnoreResultPolicy<T>()
        {
            ObservableFunc<T, ResultKind> observableFunc = new ObservableFunc<T, ResultKind>(r => ResultKind.Fatal);
            observableFunc.Invoking += r => Assert.Fail();

            return observableFunc;
        }
    }
}
