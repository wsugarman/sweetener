// Generated from Cancellation.Extensions.tt
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sweetener.Reliability
{
    internal static class InterruptableExtensions
    {
        public static bool IsCanceled(this Task task)
            => task != null && task.Status == TaskStatus.Canceled;

        public static bool IsCancellation(this Exception exception, CancellationToken cancellationToken)
        {
            OperationCanceledException oce = exception as OperationCanceledException;
            return oce != null
                && oce.CancellationToken == cancellationToken
                && cancellationToken.IsCancellationRequested;
        }

        #region Action

        public static Action<CancellationToken> IgnoreInterruption(this Action action)
            => action == null ? (Action<CancellationToken>)null : (token) => action();

        public static Action<T, CancellationToken> IgnoreInterruption<T>(this Action<T> action)
            => action == null ? (Action<T, CancellationToken>)null : (arg, token) => action(arg);

        public static Action<T1, T2, CancellationToken> IgnoreInterruption<T1, T2>(this Action<T1, T2> action)
            => action == null ? (Action<T1, T2, CancellationToken>)null : (arg1, arg2, token) => action(arg1, arg2);

        public static Action<T1, T2, T3, CancellationToken> IgnoreInterruption<T1, T2, T3>(this Action<T1, T2, T3> action)
            => action == null ? (Action<T1, T2, T3, CancellationToken>)null : (arg1, arg2, arg3, token) => action(arg1, arg2, arg3);

        public static Action<T1, T2, T3, T4, CancellationToken> IgnoreInterruption<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action)
            => action == null ? (Action<T1, T2, T3, T4, CancellationToken>)null : (arg1, arg2, arg3, arg4, token) => action(arg1, arg2, arg3, arg4);

        public static Action<T1, T2, T3, T4, T5, CancellationToken> IgnoreInterruption<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> action)
            => action == null ? (Action<T1, T2, T3, T4, T5, CancellationToken>)null : (arg1, arg2, arg3, arg4, arg5, token) => action(arg1, arg2, arg3, arg4, arg5);

        public static Action<T1, T2, T3, T4, T5, T6, CancellationToken> IgnoreInterruption<T1, T2, T3, T4, T5, T6>(this Action<T1, T2, T3, T4, T5, T6> action)
            => action == null ? (Action<T1, T2, T3, T4, T5, T6, CancellationToken>)null : (arg1, arg2, arg3, arg4, arg5, arg6, token) => action(arg1, arg2, arg3, arg4, arg5, arg6);

        public static Action<T1, T2, T3, T4, T5, T6, T7, CancellationToken> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7>(this Action<T1, T2, T3, T4, T5, T6, T7> action)
            => action == null ? (Action<T1, T2, T3, T4, T5, T6, T7, CancellationToken>)null : (arg1, arg2, arg3, arg4, arg5, arg6, arg7, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7);

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, CancellationToken> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8>(this Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
            => action == null ? (Action<T1, T2, T3, T4, T5, T6, T7, T8, CancellationToken>)null : (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, CancellationToken> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
            => action == null ? (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, CancellationToken>)null : (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, CancellationToken> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
            => action == null ? (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, CancellationToken>)null : (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, CancellationToken> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
            => action == null ? (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, CancellationToken>)null : (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, CancellationToken> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
            => action == null ? (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, CancellationToken>)null : (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, CancellationToken> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
            => action == null ? (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, CancellationToken>)null : (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, CancellationToken> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
            => action == null ? (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, CancellationToken>)null : (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, CancellationToken> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
            => action == null ? (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, CancellationToken>)null : (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, CancellationToken> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action)
            => action == null ? (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, CancellationToken>)null : (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);

        #endregion

        #region Func

        public static Func<CancellationToken, TResult> IgnoreInterruption<TResult>(this Func<TResult> func)
            => func == null ? (Func<CancellationToken, TResult>)null : (token) => func();

        public static Func<T, CancellationToken, TResult> IgnoreInterruption<T, TResult>(this Func<T, TResult> func)
            => func == null ? (Func<T, CancellationToken, TResult>)null : (arg, token) => func(arg);

        public static Func<T1, T2, CancellationToken, TResult> IgnoreInterruption<T1, T2, TResult>(this Func<T1, T2, TResult> func)
            => func == null ? (Func<T1, T2, CancellationToken, TResult>)null : (arg1, arg2, token) => func(arg1, arg2);

        public static Func<T1, T2, T3, CancellationToken, TResult> IgnoreInterruption<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func)
            => func == null ? (Func<T1, T2, T3, CancellationToken, TResult>)null : (arg1, arg2, arg3, token) => func(arg1, arg2, arg3);

        public static Func<T1, T2, T3, T4, CancellationToken, TResult> IgnoreInterruption<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func)
            => func == null ? (Func<T1, T2, T3, T4, CancellationToken, TResult>)null : (arg1, arg2, arg3, arg4, token) => func(arg1, arg2, arg3, arg4);

        public static Func<T1, T2, T3, T4, T5, CancellationToken, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> func)
            => func == null ? (Func<T1, T2, T3, T4, T5, CancellationToken, TResult>)null : (arg1, arg2, arg3, arg4, arg5, token) => func(arg1, arg2, arg3, arg4, arg5);

        public static Func<T1, T2, T3, T4, T5, T6, CancellationToken, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> func)
            => func == null ? (Func<T1, T2, T3, T4, T5, T6, CancellationToken, TResult>)null : (arg1, arg2, arg3, arg4, arg5, arg6, token) => func(arg1, arg2, arg3, arg4, arg5, arg6);

        public static Func<T1, T2, T3, T4, T5, T6, T7, CancellationToken, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func)
            => func == null ? (Func<T1, T2, T3, T4, T5, T6, T7, CancellationToken, TResult>)null : (arg1, arg2, arg3, arg4, arg5, arg6, arg7, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7);

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, CancellationToken, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func)
            => func == null ? (Func<T1, T2, T3, T4, T5, T6, T7, T8, CancellationToken, TResult>)null : (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, CancellationToken, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func)
            => func == null ? (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, CancellationToken, TResult>)null : (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, CancellationToken, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func)
            => func == null ? (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, CancellationToken, TResult>)null : (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, CancellationToken, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func)
            => func == null ? (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, CancellationToken, TResult>)null : (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, CancellationToken, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func)
            => func == null ? (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, CancellationToken, TResult>)null : (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, CancellationToken, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func)
            => func == null ? (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, CancellationToken, TResult>)null : (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, CancellationToken, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func)
            => func == null ? (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, CancellationToken, TResult>)null : (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, CancellationToken, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func)
            => func == null ? (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, CancellationToken, TResult>)null : (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, CancellationToken, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func)
            => func == null ? (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, CancellationToken, TResult>)null : (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);

        #endregion
    }
}
