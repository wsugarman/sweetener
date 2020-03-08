using System;

namespace Sweetener.Reliability.Test
{
    internal static class FlakyFunc
    {
        public static Func<TResult> Create<TResult, TException>(TResult transientResult)
            where TException : Exception, new()
            => Create<TResult, TException>(transientResult, default!, Retries.Infinite);

        public static Func<TResult> Create<TResult, TException>(TResult transientResult, TResult finalResult, int retries)
            where TException : Exception, new()
        {
            if (retries < Retries.Infinite)
                throw new ArgumentOutOfRangeException(nameof(retries));

            int calls = 0;
            return () =>
            {
                calls++;

                if (retries != Retries.Infinite && calls > retries)
                    return finalResult;
                else if (calls % 2 == 0)
                    return transientResult;
                else
                    throw new TException();
            };
        }

        public static Func<TResult> Create<TResult, TTransient, TFatal>(TResult result, int retries)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            if (retries < Retries.Infinite)
                throw new ArgumentOutOfRangeException(nameof(retries));

            int calls = 0;
            return () =>
            {
                calls++;

                if (retries != Retries.Infinite && calls > retries)
                    throw new TFatal();
                else if (calls % 2 == 0)
                    return result;
                else
                    throw new TTransient();
            };
        }

        public static Func<TResult> Create<TResult, TException>(TResult result, int retries)
            where TException : Exception, new()
        {
            if (retries < Retries.Infinite)
                throw new ArgumentOutOfRangeException(nameof(retries));

            int calls = 0;
            return () =>
            {
                calls++;

                if (retries != Retries.Infinite && calls > retries)
                    return result;

                throw new TException();
            };
        }

        public static Func<TResult> Create<TResult, TTransient, TFatal>(int retries)
            where TTransient : Exception, new()
            where TFatal     : Exception, new()
        {
            if (retries < Retries.Infinite)
                throw new ArgumentOutOfRangeException(nameof(retries));

            int calls = 0;
            return () =>
            {
                calls++;

                if (retries != Retries.Infinite && calls > retries)
                    throw new TFatal();

                throw new TTransient();
            };
        }
    }
}
