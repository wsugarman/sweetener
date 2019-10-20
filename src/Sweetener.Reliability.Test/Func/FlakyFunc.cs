using System;

namespace Sweetener.Reliability.Test
{
    internal static class FlakyFunc
    {
        public static Func<TResult> Create<TResult, TException>(int retries, TResult result, bool transientError = true)
            where TException : Exception, new()
            => () =>
            {
                if (retries > 0)
                {
                    retries--;

                    if (transientError)
                        throw new TException();

                    return result;
                }
                else if (retries == Retries.Infinite)
                {
                    if (transientError)
                        throw new TException();

                    return result;
                }
                else if (transientError)
                {
                    return result;
                }
                else
                {
                    throw new TException();
                }
            };

        public static Func<T> Create<T>(int retries, T transientResult, T result)
            => () =>
            {
                if (retries > 0)
                {
                    retries--;
                    return transientResult;
                }
                
                return retries == Retries.Infinite ? transientResult : result;
            };

        public static Func<TResult> Create<TResult, TTransient, TFatal>(int retries)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
            => () =>
            {
                if (retries > 0)
                {
                    retries--;
                    throw new TTransient();
                }
                else if (retries == Retries.Infinite)
                {
                    throw new TTransient();
                }
                else
                {
                    throw new TFatal();
                }
            };
    }
}
