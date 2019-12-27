using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    internal static class AssertExtensions
    {
        internal static void ThrowsException<T>(this Assert assert, Action action)
            where T : Exception
            => ThrowsException(assert, action, typeof(T));

        internal static void ThrowsException(this Assert assert, Action action, Type exceptionType)
        {
            if (!typeof(Exception).IsAssignableFrom(exceptionType))
                throw new ArgumentException($"Type '{exceptionType.Name}' is not an {nameof(Exception)}", nameof(exceptionType));

            try
            {
                action();
                Assert.Fail($"Action did not throw an expected exception of type '{exceptionType.Name}'");
            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception e)
            {
                Assert.AreEqual(exceptionType, e.GetType());
            }
        }

        internal static void ThrowsException<T>(this Assert assert, AsyncAction action)
            where T : Exception
            => ThrowsException(assert, action, typeof(T));

        internal static void ThrowsException(this Assert assert, AsyncAction action, Type exceptionType)
        {
            if (!typeof(Exception).IsAssignableFrom(exceptionType))
                throw new ArgumentException($"Type '{exceptionType.Name}' is not an {nameof(Exception)}", nameof(exceptionType));

            try
            {
                action().Wait();
                Assert.Fail($"Action did not throw an expected exception of type '{exceptionType.Name}'");
            }
            catch (Exception e)
            {
                if (e is AggregateException a)
                    e = a.InnerException;

                if (e is AssertFailedException)
                    throw;

                Assert.AreEqual(exceptionType, e.GetType());
            }
        }
    }
}
