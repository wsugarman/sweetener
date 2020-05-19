using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    internal static class AssertExtensions
    {
        public static void ThrowsException<T>(this Assert assert, Action action, bool allowedDerivedTypes = false)
            where T : Exception
            => ThrowsException(assert, action, typeof(T), allowedDerivedTypes);

        public static void ThrowsException(this Assert assert, Action action, Type exceptionType, bool allowedDerivedTypes = false)
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
                if (exceptionType != typeof(AggregateException) && e is AggregateException a && a.InnerException != null)
                {
                    e = a.InnerException;
                    if (e is AssertFailedException)
                        ExceptionDispatchInfo.Capture(e).Throw();
                }

                if (allowedDerivedTypes)
                    Assert.IsInstanceOfType(e, exceptionType);
                else
                    Assert.AreEqual(exceptionType, e!.GetType());
            }
        }

        public static Task ThrowsExceptionAsync<T>(this Assert assert, Func<Task> action, bool allowedDerivedTypes = false)
            where T : Exception
            => ThrowsExceptionAsync(assert, action, typeof(T), allowedDerivedTypes);

        public static async Task ThrowsExceptionAsync(this Assert assert, Func<Task> action, Type exceptionType, bool allowedDerivedTypes = false)
        {
            if (!typeof(Exception).IsAssignableFrom(exceptionType))
                throw new ArgumentException($"Type '{exceptionType.Name}' is not an {nameof(Exception)}", nameof(exceptionType));

            try
            {
                await action().ConfigureAwait(false);
                Assert.Fail($"Action did not throw an expected exception of type '{exceptionType.Name}'");
            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception e)
            {
                if (exceptionType != typeof(AggregateException) && e is AggregateException a && a.InnerException != null)
                {
                    e = a.InnerException;
                    if (e is AssertFailedException)
                        ExceptionDispatchInfo.Capture(e).Throw();
                }

                if (allowedDerivedTypes)
                    Assert.IsInstanceOfType(e, exceptionType);
                else
                    Assert.AreEqual(exceptionType, e!.GetType());
            }
        }
    }
}
