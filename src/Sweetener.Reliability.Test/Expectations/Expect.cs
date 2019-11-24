using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    internal static partial class Expect
    {
        #region Asc

        public static Action<int, CallContext> Asc()
            => (i, c) => Assert.AreEqual(c.Calls, i);

        #endregion

        #region AfterDelay

        public static Action<CallContext> AfterDelay(TimeSpan delay)
            => context =>
            {
                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        #endregion

        #region Alternating

        public static Action<int, T, Exception, CallContext> AlternatingAsc<T>(T transientResult, Type transientException)
            => (i, r, e, c) =>
            {
                Assert.AreEqual(c.Calls, i);
                if (c.Calls % 2 == 0)
                {
                    Assert.AreEqual(transientResult, r);
                    Assert.IsNull(e);
                }
                else
                {
                    Assert.AreEqual(default, r);
                    Assert.That.ExceptionType(transientException, e);
                }
            };

        public static Action<int, T, Exception, CallContext> AlternatingAsc<T>(T transientResult, Type transientException, T finalResult, int transientCount)
            => (i, r, e, c) =>
            {
                Assert.AreEqual(c.Calls, i);
                if (c.Calls <= transientCount)
                {
                    if (c.Calls % 2 == 0)
                    {
                        Assert.AreEqual(transientResult, r);
                        Assert.IsNull(e);
                    }
                    else
                    {
                        Assert.AreEqual(default, r);
                        Assert.That.ExceptionType(transientException, e);
                    }
                }
                else
                {
                    Assert.AreEqual(finalResult, r);
                    Assert.IsNull(e);
                }
            };

        public static Action<int, T, Exception, CallContext> AlternatingAsc<T>(T transientResult, Type transientException, Type fatalException, int transientCount)
            => (i, r, e, c) =>
            {
                Assert.AreEqual(c.Calls, i);
                if (c.Calls <= transientCount)
                {
                    if (c.Calls % 2 == 0)
                    {
                        Assert.AreEqual(transientResult, r);
                        Assert.IsNull(e);
                    }
                    else
                    {
                        Assert.AreEqual(default, r);
                        Assert.That.ExceptionType(transientException, e);
                    }
                }
                else
                {
                    Assert.AreEqual(default, r);
                    Assert.That.ExceptionType(fatalException, e);
                }
            };

        #endregion

        #region Exception(s)

        public static Action<Exception, CallContext> Exception(Type t)
            => (e, c) => Assert.That.ExceptionType(t, e);

        public static Action<int, Exception, CallContext> ExceptionAsc(Type t)
            => (i, e, c) =>
            {
                Assert.AreEqual(c.Calls, i);
                Assert.That.ExceptionType(t, e);
            };

        public static Action<Exception, CallContext> Exceptions(Type transientType, Type fatalType, int transientCount)
            => (e, c) =>
            {
                Type expectedType = c.Calls <= transientCount ? transientType : fatalType;
                Assert.That.ExceptionType(expectedType, e);
            };

        public static Action<T, Exception, CallContext> OnlyException<T>(Type t)
            => (r, e, c) =>
            {
                Assert.AreEqual(default, r);
                Assert.That.ExceptionType(t, e);
            };

        public static Action<int, T, Exception, CallContext> OnlyExceptionAsc<T>(Type t)
            => (i, r, e, c) =>
            {
                Assert.AreEqual(c.Calls, i);
                Assert.AreEqual(default, r);
                Assert.That.ExceptionType(t, e);
            };

        #endregion

        #region Nothing

        public static Action<T1, CallContext> Nothing<T1>()
            => (arg, context) => Assert.Fail();

        public static Action<T1, T2, CallContext> Nothing<T1, T2>()
            => (arg1, arg2, context) => Assert.Fail();

        public static Action<T1, T2, T3, CallContext> Nothing<T1, T2, T3>()
            => (arg1, arg2, arg3, context) => Assert.Fail();

        #endregion

        #region Result

        public static Action<T, CallContext> Result<T>(T expectedResult)
            => (r, c) => Assert.AreEqual(expectedResult, r);

        public static Action<T, CallContext> Results<T>(T transientResult, T finalResult, int transientCount)
            => (r, c) =>
            {
                T expectedResult = c.Calls <= transientCount ? transientResult : finalResult;
                Assert.AreEqual(expectedResult, r);
            };

        public static Action<T, Exception, CallContext> OnlyResult<T>(T expectedResult)
            => (r, e, c) =>
            {
                Assert.AreEqual(expectedResult, r);
                Assert.IsNull(e);
            };

        #endregion
    }
}
