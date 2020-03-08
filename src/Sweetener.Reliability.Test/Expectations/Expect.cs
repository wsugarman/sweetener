using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{

#nullable disable

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

        public static Action<CancellationToken, CallContext> AfterDelayWithToken(TimeSpan delay)
            => (token, context) =>
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
                    Assert.AreEqual(transientException, e.GetType());
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
                        Assert.AreEqual(transientException, e.GetType());
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
                        Assert.AreEqual(transientException, e.GetType());
                    }
                }
                else
                {
                    Assert.AreEqual(default, r);
                    Assert.AreEqual(fatalException, e.GetType());
                }
            };

        #endregion

        #region Exception(s)

        public static Action<Exception, CallContext> Exception(Type t)
            => (e, c) => Assert.AreEqual(t, e.GetType());

        public static Action<int, Exception, CallContext> ExceptionAsc(Type t)
            => (i, e, c) =>
            {
                Assert.AreEqual(c.Calls, i);
                Assert.AreEqual(t, e.GetType());
            };

        public static Action<Exception, CallContext> Exceptions(Type transientType, Type fatalType, int transientCount)
            => (e, c) =>
            {
                Type expectedType = c.Calls <= transientCount ? transientType : fatalType;
                Assert.AreEqual(expectedType, e.GetType());
            };

        public static Action<T, Exception, CallContext> OnlyException<T>(Type t)
            => (r, e, c) =>
            {
                Assert.AreEqual(default, r);
                Assert.AreEqual(t, e.GetType());
            };

        public static Action<int, T, Exception, CallContext> OnlyExceptionAsc<T>(Type t)
            => (i, r, e, c) =>
            {
                Assert.AreEqual(c.Calls, i);
                Assert.AreEqual(default, r);
                Assert.AreEqual(t, e.GetType());
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

#nullable enable

}
