using System;

namespace Sweetener.Reliability.Test
{
    internal static class FlakyAction
    {
        public static Action Create<T>(int retries)
            where T : Exception, new()
        {
            if (retries < Retries.Infinite)
                throw new ArgumentOutOfRangeException(nameof(retries));

            int calls = 0;
            return () =>
            {
                calls++;

                if (retries != Retries.Infinite && calls > retries)
                    return;

                throw new T();
            };
        }

        public static Action Create<TTransient, TFatal>(int retries)
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
