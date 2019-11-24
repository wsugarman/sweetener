using System;
using System.Runtime.ExceptionServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    internal static class AssertExtensions
    {
        internal static void ThrowsException<T>(this Assert assert, Action action, bool rethrowAssertions = true)
            where T : Exception
            => ThrowsException(assert, action, typeof(T), rethrowAssertions);

        internal static void ThrowsException(this Assert assert, Action action, Type exceptionType, bool rethrowAssertions = true)
        {
            if (!typeof(Exception).IsAssignableFrom(exceptionType))
                throw new ArgumentException($"Type '{exceptionType.Name}' is not an {nameof(Exception)}", nameof(exceptionType));

            try
            {
                action();
                Assert.Fail($"Action did not throw an expected exception of type '{exceptionType.Name}'");
            }
            catch (Exception e)
            {
                assert.ExceptionType(exceptionType, e, rethrowAssertions);
            }
        }

        internal static void ExceptionType(this Assert assert, Type expectedType, Exception actual, bool rethrowAssertions = true)
        {
            if (!typeof(Exception).IsAssignableFrom(expectedType))
                throw new ArgumentException($"Type '{expectedType.Name}' is not an {nameof(Exception)}", nameof(expectedType));

            Type actualType = actual.GetType();
            if (expectedType != actualType)
            {
                if (rethrowAssertions && actualType == typeof(AssertFailedException))
                    ExceptionDispatchInfo.Capture(actual).Throw();

                Assert.AreEqual(expectedType, actualType);
            }
        }
    }
}
