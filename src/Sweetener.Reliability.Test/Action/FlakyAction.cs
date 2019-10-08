using System;

namespace Sweetener.Reliability.Test
{
    internal static class FlakyAction
    {
        public static Action Create<T>(int retries)
            where T : Exception, new()
            => () =>
            {
                if (retries > 0)
                {
                    retries--;
                    throw new T();
                }
                else if (retries == Retries.Infinite)
                {
                    throw new T();
                }
            };

        public static Action Create<TTransient, TFatal>(int retries)
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
