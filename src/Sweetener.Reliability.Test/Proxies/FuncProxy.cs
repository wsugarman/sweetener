// Generated from FuncProxy.tt
using System;

namespace Sweetener.Reliability.Test
{
    #region FuncProxy<TResult>

    internal sealed class FuncProxy<TResult> : DelegateProxy<Func<TResult>>
    {
        public new event Action<CallContext> Invoking;

        public override Func<TResult> Proxy => Invoke;

        private readonly Func<TResult> _func;

        public FuncProxy()
            : this(() => default)
        { }

        public FuncProxy(Func<TResult> func)
            : base(func)
        { }

        public TResult Invoke()
        {
            UpdateContext();

            OnInvoking();
            return _delegate();
        }

        private new void OnInvoking()
        {
            Invoking?.Invoke(_context);
            base.OnInvoking();
        }
    }

    #endregion

    #region FuncProxy<T, TResult>

    internal sealed class FuncProxy<T, TResult> : DelegateProxy<Func<T, TResult>>
    {
        public new event Action<T, CallContext> Invoking;

        public override Func<T, TResult> Proxy => Invoke;

        private readonly Func<T, TResult> _func;

        public FuncProxy()
            : this((arg) => default)
        { }

        public FuncProxy(Func<T, TResult> func)
            : base(func)
        { }

        public TResult Invoke(T arg)
        {
            UpdateContext();

            OnInvoking(arg);
            return _delegate(arg);
        }

        private void OnInvoking(T arg)
        {
            Invoking?.Invoke(arg, _context);
            OnInvoking();
        }
    }

    #endregion

    #region FuncProxy<T1, T2, TResult>

    internal sealed class FuncProxy<T1, T2, TResult> : DelegateProxy<Func<T1, T2, TResult>>
    {
        public new event Action<T1, T2, CallContext> Invoking;

        public override Func<T1, T2, TResult> Proxy => Invoke;

        private readonly Func<T1, T2, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2) => default)
        { }

        public FuncProxy(Func<T1, T2, TResult> func)
            : base(func)
        { }

        public TResult Invoke(T1 arg1, T2 arg2)
        {
            UpdateContext();

            OnInvoking(arg1, arg2);
            return _delegate(arg1, arg2);
        }

        private void OnInvoking(T1 arg1, T2 arg2)
        {
            Invoking?.Invoke(arg1, arg2, _context);
            OnInvoking();
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, TResult>

    internal sealed class FuncProxy<T1, T2, T3, TResult> : DelegateProxy<Func<T1, T2, T3, TResult>>
    {
        public new event Action<T1, T2, T3, CallContext> Invoking;

        public override Func<T1, T2, T3, TResult> Proxy => Invoke;

        private readonly Func<T1, T2, T3, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, TResult> func)
            : base(func)
        { }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3);
            return _delegate(arg1, arg2, arg3);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3)
        {
            Invoking?.Invoke(arg1, arg2, arg3, _context);
            OnInvoking();
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, TResult> : DelegateProxy<Func<T1, T2, T3, T4, TResult>>
    {
        public new event Action<T1, T2, T3, T4, CallContext> Invoking;

        public override Func<T1, T2, T3, T4, TResult> Proxy => Invoke;

        private readonly Func<T1, T2, T3, T4, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, TResult> func)
            : base(func)
        { }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4);
            return _delegate(arg1, arg2, arg3, arg4);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, _context);
            OnInvoking();
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, T5, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, T5, TResult> : DelegateProxy<Func<T1, T2, T3, T4, T5, TResult>>
    {
        public new event Action<T1, T2, T3, T4, T5, CallContext> Invoking;

        public override Func<T1, T2, T3, T4, T5, TResult> Proxy => Invoke;

        private readonly Func<T1, T2, T3, T4, T5, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4, arg5) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, T5, TResult> func)
            : base(func)
        { }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5);
            return _delegate(arg1, arg2, arg3, arg4, arg5);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, _context);
            OnInvoking();
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, T5, T6, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, T5, T6, TResult> : DelegateProxy<Func<T1, T2, T3, T4, T5, T6, TResult>>
    {
        public new event Action<T1, T2, T3, T4, T5, T6, CallContext> Invoking;

        public override Func<T1, T2, T3, T4, T5, T6, TResult> Proxy => Invoke;

        private readonly Func<T1, T2, T3, T4, T5, T6, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, T5, T6, TResult> func)
            : base(func)
        { }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5, arg6);
            return _delegate(arg1, arg2, arg3, arg4, arg5, arg6);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, _context);
            OnInvoking();
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, T5, T6, T7, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, T5, T6, T7, TResult> : DelegateProxy<Func<T1, T2, T3, T4, T5, T6, T7, TResult>>
    {
        public new event Action<T1, T2, T3, T4, T5, T6, T7, CallContext> Invoking;

        public override Func<T1, T2, T3, T4, T5, T6, T7, TResult> Proxy => Invoke;

        private readonly Func<T1, T2, T3, T4, T5, T6, T7, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, T5, T6, T7, TResult> func)
            : base(func)
        { }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            return _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, _context);
            OnInvoking();
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, TResult> : DelegateProxy<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>>
    {
        public new event Action<T1, T2, T3, T4, T5, T6, T7, T8, CallContext> Invoking;

        public override Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Proxy => Invoke;

        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func)
            : base(func)
        { }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            return _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, _context);
            OnInvoking();
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> : DelegateProxy<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>>
    {
        public new event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, CallContext> Invoking;

        public override Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> Proxy => Invoke;

        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func)
            : base(func)
        { }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            return _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, _context);
            OnInvoking();
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> : DelegateProxy<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>>
    {
        public new event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, CallContext> Invoking;

        public override Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> Proxy => Invoke;

        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func)
            : base(func)
        { }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            return _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, _context);
            OnInvoking();
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> : DelegateProxy<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>>
    {
        public new event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, CallContext> Invoking;

        public override Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> Proxy => Invoke;

        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func)
            : base(func)
        { }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
            return _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, _context);
            OnInvoking();
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> : DelegateProxy<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>>
    {
        public new event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, CallContext> Invoking;

        public override Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> Proxy => Invoke;

        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func)
            : base(func)
        { }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
            return _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, _context);
            OnInvoking();
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> : DelegateProxy<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>>
    {
        public new event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, CallContext> Invoking;

        public override Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> Proxy => Invoke;

        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func)
            : base(func)
        { }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
            return _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, _context);
            OnInvoking();
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> : DelegateProxy<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>>
    {
        public new event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, CallContext> Invoking;

        public override Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> Proxy => Invoke;

        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func)
            : base(func)
        { }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
            return _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, _context);
            OnInvoking();
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> : DelegateProxy<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>>
    {
        public new event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, CallContext> Invoking;

        public override Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> Proxy => Invoke;

        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func)
            : base(func)
        { }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
            return _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, _context);
            OnInvoking();
        }
    }

    #endregion

    #region FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>

    internal sealed class FuncProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> : DelegateProxy<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>>
    {
        public new event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, CallContext> Invoking;

        public override Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> Proxy => Invoke;

        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> _func;

        public FuncProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => default)
        { }

        public FuncProxy(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func)
            : base(func)
        { }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
            return _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, _context);
            OnInvoking();
        }
    }

    #endregion

}
