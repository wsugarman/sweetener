// Generated from ActionProxy.tt
using System;

namespace Sweetener.Reliability.Test
{
    #region ActionProxy

    internal sealed class ActionProxy : DelegateProxy
    {
        public event Action<CallContext> Invoking;

        private readonly Action _action;

        public ActionProxy()
            : this(() => Operation.Null())
        { }

        public ActionProxy(Action action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Invoke()
        {
            UpdateContext();

            Invoking?.Invoke(_context);
            _action();
        }
    }

    #endregion

    #region ActionProxy<T>

    internal sealed class ActionProxy<T> : DelegateProxy
    {
        public event Action<T, CallContext> Invoking;

        private readonly Action<T> _action;

        public ActionProxy()
            : this((arg) => Operation.Null())
        { }

        public ActionProxy(Action<T> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Invoke(T arg)
        {
            UpdateContext();

            Invoking?.Invoke(arg, _context);
            _action(arg);
        }
    }

    #endregion

    #region ActionProxy<T1, T2>

    internal sealed class ActionProxy<T1, T2> : DelegateProxy
    {
        public event Action<T1, T2, CallContext> Invoking;

        private readonly Action<T1, T2> _action;

        public ActionProxy()
            : this((arg1, arg2) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Invoke(T1 arg1, T2 arg2)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, _context);
            _action(arg1, arg2);
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3>

    internal sealed class ActionProxy<T1, T2, T3> : DelegateProxy
    {
        public event Action<T1, T2, T3, CallContext> Invoking;

        private readonly Action<T1, T2, T3> _action;

        public ActionProxy()
            : this((arg1, arg2, arg3) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, _context);
            _action(arg1, arg2, arg3);
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4>

    internal sealed class ActionProxy<T1, T2, T3, T4> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, CallContext> Invoking;

        private readonly Action<T1, T2, T3, T4> _action;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, _context);
            _action(arg1, arg2, arg3, arg4);
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, CallContext> Invoking;

        private readonly Action<T1, T2, T3, T4, T5> _action;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, _context);
            _action(arg1, arg2, arg3, arg4, arg5);
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5, T6>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5, T6> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, T6, CallContext> Invoking;

        private readonly Action<T1, T2, T3, T4, T5, T6> _action;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5, T6> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, _context);
            _action(arg1, arg2, arg3, arg4, arg5, arg6);
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5, T6, T7>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5, T6, T7> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, T6, T7, CallContext> Invoking;

        private readonly Action<T1, T2, T3, T4, T5, T6, T7> _action;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5, T6, T7> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, _context);
            _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, T6, T7, T8, CallContext> Invoking;

        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8> _action;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, _context);
            _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, CallContext> Invoking;

        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> _action;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, _context);
            _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, CallContext> Invoking;

        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> _action;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, _context);
            _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, CallContext> Invoking;

        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> _action;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, _context);
            _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, CallContext> Invoking;

        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> _action;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, _context);
            _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, CallContext> Invoking;

        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> _action;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, _context);
            _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, CallContext> Invoking;

        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> _action;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, _context);
            _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, CallContext> Invoking;

        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> _action;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, _context);
            _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, CallContext> Invoking;

        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> _action;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, _context);
            _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
        }
    }

    #endregion

    #region ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>

    internal sealed class ActionProxy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> : DelegateProxy
    {
        public event Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, CallContext> Invoking;

        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> _action;

        public ActionProxy()
            : this((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17) => Operation.Null())
        { }

        public ActionProxy(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17)
        {
            UpdateContext();

            Invoking?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, _context);
            _action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17);
        }
    }

    #endregion

}

