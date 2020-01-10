// Generated from ActionProxy.tt
using System;

namespace Sweetener.Reliability.Test
{
    #region ActionProxy

    internal sealed class ActionProxy : DelegateProxy<Action>
    {
        public new event Action<CallContext> Invoking;

        public override Action Proxy => Invoke;

        public ActionProxy()
            : this(() => Operation.Null())
        { }

        public ActionProxy(Action action)
            : base(action)
        { }

        public void Invoke()
        {
            UpdateContext();

            OnInvoking();
            _delegate();
        }

        private new void OnInvoking()
        {
            Invoking?.Invoke(_context);
            base.OnInvoking();
        }
    }

    #endregion

    #region ActionProxy<T>

    internal sealed class ActionProxy<T> : DelegateProxy<Action<T>>
    {
        public new event Action<T, CallContext> Invoking;

        public override Action<T> Proxy => Invoke;

        public ActionProxy()
            : this((arg) => Operation.Null())
        { }

        public ActionProxy(Action<T> action)
            : base(action)
        { }

        public void Invoke(T arg)
        {
            UpdateContext();

            OnInvoking(arg);
            _delegate(arg);
        }

        private void OnInvoking(T arg)
        {
            Invoking?.Invoke(arg, _context);
            OnInvoking();
        }
    }

    #endregion

    #region ActionProxy<T1, T2>

    internal sealed class ActionProxy<T1, T2> : DelegateProxy<Action<T1, T2>>
    {
        public new event Action<T1, T2, CallContext> Invoking;

        public override Action<T1, T2> Proxy => Invoke;

        public ActionProxy()
            : this((arg1, arg2) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2> action)
            : base(action)
        { }

        public void Invoke(T1 arg1, T2 arg2)
        {
            UpdateContext();

            OnInvoking(arg1, arg2);
            _delegate(arg1, arg2);
        }

        private void OnInvoking(T1 arg1, T2 arg2)
        {
            Invoking?.Invoke(arg1, arg2, _context);
            OnInvoking();
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3>

    internal sealed class ActionProxy<T1, T2, T3> : DelegateProxy<Action<T1, T2, T3>>
    {
        public new event Action<T1, T2, T3, CallContext> Invoking;

        public override Action<T1, T2, T3> Proxy => Invoke;

        public ActionProxy()
            : this((arg1, arg2, arg3) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3> action)
            : base(action)
        { }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3);
            _delegate(arg1, arg2, arg3);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3)
        {
            Invoking?.Invoke(arg1, arg2, arg3, _context);
            OnInvoking();
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4>

    internal sealed class ActionProxy<T1, T2, T3, T4> : DelegateProxy<Action<T1, T2, T3, T4>>
    {
        public new event Action<T1, T2, T3, T4, CallContext> Invoking;

        public override Action<T1, T2, T3, T4> Proxy => Invoke;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4> action)
            : base(action)
        { }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4);
            _delegate(arg1, arg2, arg3, arg4);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, _context);
            OnInvoking();
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5> : DelegateProxy<Action<T1, T2, T3, T4, T5>>
    {
        public new event Action<T1, T2, T3, T4, T5, CallContext> Invoking;

        public override Action<T1, T2, T3, T4, T5> Proxy => Invoke;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5> action)
            : base(action)
        { }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5);
            _delegate(arg1, arg2, arg3, arg4, arg5);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, _context);
            OnInvoking();
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5, T6>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5, T6> : DelegateProxy<Action<T1, T2, T3, T4, T5, T6>>
    {
        public new event Action<T1, T2, T3, T4, T5, T6, CallContext> Invoking;

        public override Action<T1, T2, T3, T4, T5, T6> Proxy => Invoke;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5, T6> action)
            : base(action)
        { }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5, arg6);
            _delegate(arg1, arg2, arg3, arg4, arg5, arg6);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, _context);
            OnInvoking();
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5, T6, T7>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5, T6, T7> : DelegateProxy<Action<T1, T2, T3, T4, T5, T6, T7>>
    {
        public new event Action<T1, T2, T3, T4, T5, T6, T7, CallContext> Invoking;

        public override Action<T1, T2, T3, T4, T5, T6, T7> Proxy => Invoke;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5, T6, T7> action)
            : base(action)
        { }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, _context);
            OnInvoking();
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8> : DelegateProxy<Action<T1, T2, T3, T4, T5, T6, T7, T8>>
    {
        public new event Action<T1, T2, T3, T4, T5, T6, T7, T8, CallContext> Invoking;

        public override Action<T1, T2, T3, T4, T5, T6, T7, T8> Proxy => Invoke;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
            : base(action)
        { }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, _context);
            OnInvoking();
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9> : DelegateProxy<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>>
    {
        public new event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, CallContext> Invoking;

        public override Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> Proxy => Invoke;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
            : base(action)
        { }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, _context);
            OnInvoking();
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : DelegateProxy<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>
    {
        public new event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, CallContext> Invoking;

        public override Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Proxy => Invoke;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
            : base(action)
        { }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, _context);
            OnInvoking();
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : DelegateProxy<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>
    {
        public new event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, CallContext> Invoking;

        public override Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Proxy => Invoke;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
            : base(action)
        { }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
            _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, _context);
            OnInvoking();
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : DelegateProxy<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>
    {
        public new event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, CallContext> Invoking;

        public override Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Proxy => Invoke;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
            : base(action)
        { }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
            _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, _context);
            OnInvoking();
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : DelegateProxy<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>
    {
        public new event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, CallContext> Invoking;

        public override Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Proxy => Invoke;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
            : base(action)
        { }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
            _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, _context);
            OnInvoking();
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : DelegateProxy<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>
    {
        public new event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, CallContext> Invoking;

        public override Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Proxy => Invoke;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
            : base(action)
        { }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
            _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, _context);
            OnInvoking();
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : DelegateProxy<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>
    {
        public new event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, CallContext> Invoking;

        public override Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Proxy => Invoke;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
            : base(action)
        { }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
            _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, _context);
            OnInvoking();
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : DelegateProxy<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>
    {
        public new event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, CallContext> Invoking;

        public override Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Proxy => Invoke;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action)
            : base(action)
        { }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
            _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, _context);
            OnInvoking();
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> : DelegateProxy<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>>
    {
        public new event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, CallContext> Invoking;

        public override Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> Proxy => Invoke;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> action)
            : base(action)
        { }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17)
        {
            UpdateContext();

            OnInvoking(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17);
            _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17);
        }

        private void OnInvoking(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17)
        {
            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, _context);
            OnInvoking();
        }
    }

    #endregion

}
