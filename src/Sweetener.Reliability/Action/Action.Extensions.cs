using System;

namespace Sweetener.Reliability.Extensions
{
    public static class ActionExtensions
    {
        public static bool TryInvokeReliably<T>(this Action<T> action, T arg, int maxRetries, Delay delayPolicy, Retry retryPolicy)
            => TryInvokeReliably(action, arg, maxRetries, delayPolicy, retryPolicy, null);

        public static bool TryInvokeReliably<T>(this Action<T> action, T arg, int maxRetries, Delay delayPolicy, Retry retryPolicy, Action<int> retryHandler)
        {
            ReliableAction<T> reliableAction = new ReliableAction<T>(action, maxRetries, delayPolicy, retryPolicy);

            if (retryHandler != null)
                reliableAction.Retrying += retryHandler;

            return reliableAction.TryInvoke(arg);
        }

        public static void InvokeReliably<T>(this Action<T> action, T arg, int maxRetries, Delay delayPolicy, Retry retryPolicy)
            => InvokeReliably(action, arg, maxRetries, delayPolicy, retryPolicy, null);

        public static void InvokeReliably<T>(this Action<T> action, T arg, int maxRetries, Delay delayPolicy, Retry retryPolicy, Action<int> retryHandler)
        {
            ReliableAction<T> reliableAction = new ReliableAction<T>(action, maxRetries, delayPolicy, retryPolicy);

            if (retryHandler != null)
                reliableAction.Retrying += retryHandler;

            reliableAction.Invoke(arg);
        }

        public static void Foo()
        {
            Action<object> action = x => Console.WriteLine(x);

            action.InvokeReliably("Hello world", 5, Delay.ExponentialBackoff(TimeSpan.Zero), null);
        }
    }
}
