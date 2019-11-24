// Generated from FuncProxy.tt
using System;

namespace Sweetener.Reliability.Test
{
    #region FuncProxy<TResult>

    internal sealed class FuncProxy<TResult> : DelegateProxy
    {
        public event Action<CallContext> Invoking;

        private readonly Func<TResult> _func;

        public FuncProxy()
            : this(() => default)
        { }

        public FuncProxy(Func<TResult> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public TResult Invoke()
        {
            UpdateContext();

            Invoking?.Invoke(_context);
            return _func();
        }
    }

    #endregion

    #region FuncProxy<T, TResult>

    internal sealed class FuncProxy<T, TResult> : DelegateProxy
    {
        public event Action<T, CallContext> Invoking;

        private readonly Func<T, TResult> _func;

        public FuncProxy()
            : this((arg) => default)
        { }

        public FuncProxy(Func<T, TResult> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public TResult Invoke(T arg)
        {
            UpdateContext();

            Invoking?.Invoke(arg, _context);
            return _func(arg);
        }
    }

    #endregion

    #region FuncProxy<T1, T2, TResult>

    internal sealed class FuncProxy<T1, T2, TResult> : DelegateProxy
    {
        public event Action<T1, T2, CallContext> Invoking;

        private readonly Func<T1, T2, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2) => default)
        { }

        public FuncProxy(Func<T1, T2, TResult> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public TResult Invoke(T1 arg1, T2 arg2)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, _context);
            return _func(arg1, arg2);
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, TResult>

    internal sealed class FuncProxy<T1, T2, T3, TResult> : DelegateProxy
    {
        public event Action<T1, T2, T3, CallContext> Invoking;

        private readonly Func<T1, T2, T3, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, TResult> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, _context);
            return _func(arg1, arg2, arg3);
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, TResult> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, CallContext> Invoking;

        private readonly Func<T1, T2, T3, T4, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, TResult> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, _context);
            return _func(arg1, arg2, arg3, arg4);
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, T5, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, T5, TResult> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, CallContext> Invoking;

        private readonly Func<T1, T2, T3, T4, T5, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4, arg5) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, T5, TResult> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, _context);
            return _func(arg1, arg2, arg3, arg4, arg5);
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, T5, T6, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, T5, T6, TResult> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, T6, CallContext> Invoking;

        private readonly Func<T1, T2, T3, T4, T5, T6, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, T5, T6, TResult> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, _context);
            return _func(arg1, arg2, arg3, arg4, arg5, arg6);
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, T5, T6, T7, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, T5, T6, T7, TResult> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, T6, T7, CallContext> Invoking;

        private readonly Func<T1, T2, T3, T4, T5, T6, T7, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, T5, T6, T7, TResult> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, _context);
            return _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, TResult> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, T6, T7, T8, CallContext> Invoking;

        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, _context);
            return _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, CallContext> Invoking;

        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, _context);
            return _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, CallContext> Invoking;

        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, _context);
            return _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, CallContext> Invoking;

        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, _context);
            return _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, CallContext> Invoking;

        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, _context);
            return _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, CallContext> Invoking;

        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, _context);
            return _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, CallContext> Invoking;

        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, _context);
            return _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, CallContext> Invoking;

        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, _context);
            return _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, CallContext> Invoking;

        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, _context);
            return _func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
        }
    }

    #endregion

}
