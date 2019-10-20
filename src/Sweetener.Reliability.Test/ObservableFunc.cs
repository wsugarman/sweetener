using System;

namespace Sweetener.Reliability.Test
{
    internal sealed class ObservableFunc<T, TResult>
    {
        public event Action<T> Invoking;

        public event Action<T, TResult> Invoked;

        public int Calls { get; private set; }

        private readonly Func<T, TResult> _func;

        public ObservableFunc(Func<T, TResult> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public TResult Invoke(T arg)
        {
            Calls++;

            Invoking?.Invoke(arg);
            TResult result = _func(arg);
            Invoked?.Invoke(arg, result);

            return result;
        }

        public static implicit operator Func<T, TResult>(ObservableFunc<T, TResult> observableFunc)
            => observableFunc.Invoke;
    }

    internal sealed class ObservableFunc<T1, T2, TResult>
    {
        public event Action<T1, T2> Invoking;

        public event Action<T1, T2, TResult> Invoked;

        public int Calls { get; private set; }

        private readonly Func<T1, T2, TResult> _func;

        public ObservableFunc(Func<T1, T2, TResult> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public TResult Invoke(T1 arg1, T2 arg2)
        {
            Calls++;

            Invoking?.Invoke(arg1, arg2);
            TResult result = _func(arg1, arg2);
            Invoked?.Invoke(arg1, arg2, result);

            return result;
        }

        public static implicit operator Func<T1, T2, TResult>(ObservableFunc<T1, T2, TResult> observableFunc)
            => observableFunc.Invoke;
    }

    internal sealed class ObservableFunc<T1, T2, T3, TResult>
    {
        public event Action<T1, T2, T3> Invoking;

        public event Action<T1, T2, T3, TResult> Invoked;

        public int Calls { get; private set; }

        private readonly Func<T1, T2, T3, TResult> _func;

        public ObservableFunc(Func<T1, T2, T3, TResult> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            Calls++;

            Invoking?.Invoke(arg1, arg2, arg3);
            TResult result = _func(arg1, arg2, arg3);
            Invoked?.Invoke(arg1, arg2, arg3, result);

            return result;
        }

        public static implicit operator Func<T1, T2, T3, TResult>(ObservableFunc<T1, T2, T3, TResult> observableFunc)
            => observableFunc.Invoke;
    }
}
