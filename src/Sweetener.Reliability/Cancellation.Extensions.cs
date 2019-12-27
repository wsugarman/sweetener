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
            => exception is OperationCanceledException oce
                && oce != null
                && oce.CancellationToken == cancellationToken
                && cancellationToken.IsCancellationRequested;

        #region Action

        public static InterruptableAction IgnoreInterruption(this Action action)
            => (token) => action();

        public static InterruptableAction<T> IgnoreInterruption<T>(this Action<T> action)
            => (arg, token) => action(arg);

        public static InterruptableAction<T1, T2> IgnoreInterruption<T1, T2>(this Action<T1, T2> action)
            => (arg1, arg2, token) => action(arg1, arg2);

        public static InterruptableAction<T1, T2, T3> IgnoreInterruption<T1, T2, T3>(this Action<T1, T2, T3> action)
            => (arg1, arg2, arg3, token) => action(arg1, arg2, arg3);

        public static InterruptableAction<T1, T2, T3, T4> IgnoreInterruption<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action)
            => (arg1, arg2, arg3, arg4, token) => action(arg1, arg2, arg3, arg4);

        public static InterruptableAction<T1, T2, T3, T4, T5> IgnoreInterruption<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> action)
            => (arg1, arg2, arg3, arg4, arg5, token) => action(arg1, arg2, arg3, arg4, arg5);

        public static InterruptableAction<T1, T2, T3, T4, T5, T6> IgnoreInterruption<T1, T2, T3, T4, T5, T6>(this Action<T1, T2, T3, T4, T5, T6> action)
            => (arg1, arg2, arg3, arg4, arg5, arg6, token) => action(arg1, arg2, arg3, arg4, arg5, arg6);

        public static InterruptableAction<T1, T2, T3, T4, T5, T6, T7> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7>(this Action<T1, T2, T3, T4, T5, T6, T7> action)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7);

        public static InterruptableAction<T1, T2, T3, T4, T5, T6, T7, T8> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8>(this Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);

        public static InterruptableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);

        public static InterruptableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

        public static InterruptableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);

        public static InterruptableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);

        public static InterruptableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);

        public static InterruptableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);

        public static InterruptableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);

        public static InterruptableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);

        #endregion

        #region AsyncAction

        public static InterruptableAsyncAction IgnoreInterruption(this AsyncAction action)
            => async (token) => await action().ConfigureAwait(false);

        public static InterruptableAsyncAction<T> IgnoreInterruption<T>(this AsyncAction<T> action)
            => async (arg, token) => await action(arg).ConfigureAwait(false);

        public static InterruptableAsyncAction<T1, T2> IgnoreInterruption<T1, T2>(this AsyncAction<T1, T2> action)
            => async (arg1, arg2, token) => await action(arg1, arg2).ConfigureAwait(false);

        public static InterruptableAsyncAction<T1, T2, T3> IgnoreInterruption<T1, T2, T3>(this AsyncAction<T1, T2, T3> action)
            => async (arg1, arg2, arg3, token) => await action(arg1, arg2, arg3).ConfigureAwait(false);

        public static InterruptableAsyncAction<T1, T2, T3, T4> IgnoreInterruption<T1, T2, T3, T4>(this AsyncAction<T1, T2, T3, T4> action)
            => async (arg1, arg2, arg3, arg4, token) => await action(arg1, arg2, arg3, arg4).ConfigureAwait(false);

        public static InterruptableAsyncAction<T1, T2, T3, T4, T5> IgnoreInterruption<T1, T2, T3, T4, T5>(this AsyncAction<T1, T2, T3, T4, T5> action)
            => async (arg1, arg2, arg3, arg4, arg5, token) => await action(arg1, arg2, arg3, arg4, arg5).ConfigureAwait(false);

        public static InterruptableAsyncAction<T1, T2, T3, T4, T5, T6> IgnoreInterruption<T1, T2, T3, T4, T5, T6>(this AsyncAction<T1, T2, T3, T4, T5, T6> action)
            => async (arg1, arg2, arg3, arg4, arg5, arg6, token) => await action(arg1, arg2, arg3, arg4, arg5, arg6).ConfigureAwait(false);

        public static InterruptableAsyncAction<T1, T2, T3, T4, T5, T6, T7> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7>(this AsyncAction<T1, T2, T3, T4, T5, T6, T7> action)
            => async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, token) => await action(arg1, arg2, arg3, arg4, arg5, arg6, arg7).ConfigureAwait(false);

        public static InterruptableAsyncAction<T1, T2, T3, T4, T5, T6, T7, T8> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8>(this AsyncAction<T1, T2, T3, T4, T5, T6, T7, T8> action)
            => async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => await action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8).ConfigureAwait(false);

        public static InterruptableAsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this AsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
            => async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token) => await action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9).ConfigureAwait(false);

        public static InterruptableAsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this AsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
            => async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, token) => await action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10).ConfigureAwait(false);

        public static InterruptableAsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this AsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
            => async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, token) => await action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11).ConfigureAwait(false);

        public static InterruptableAsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this AsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
            => async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => await action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12).ConfigureAwait(false);

        public static InterruptableAsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this AsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
            => async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => await action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13).ConfigureAwait(false);

        public static InterruptableAsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this AsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
            => async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token) => await action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14).ConfigureAwait(false);

        public static InterruptableAsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this AsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
            => async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => await action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15).ConfigureAwait(false);

        public static InterruptableAsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this AsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action)
            => async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, token) => await action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16).ConfigureAwait(false);

        #endregion

        #region Func

        public static InterruptableFunc<TResult> IgnoreInterruption<TResult>(this Func<TResult> func)
            => (token) => func();

        public static InterruptableFunc<T, TResult> IgnoreInterruption<T, TResult>(this Func<T, TResult> func)
            => (arg, token) => func(arg);

        public static InterruptableFunc<T1, T2, TResult> IgnoreInterruption<T1, T2, TResult>(this Func<T1, T2, TResult> func)
            => (arg1, arg2, token) => func(arg1, arg2);

        public static InterruptableFunc<T1, T2, T3, TResult> IgnoreInterruption<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func)
            => (arg1, arg2, arg3, token) => func(arg1, arg2, arg3);

        public static InterruptableFunc<T1, T2, T3, T4, TResult> IgnoreInterruption<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func)
            => (arg1, arg2, arg3, arg4, token) => func(arg1, arg2, arg3, arg4);

        public static InterruptableFunc<T1, T2, T3, T4, T5, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> func)
            => (arg1, arg2, arg3, arg4, arg5, token) => func(arg1, arg2, arg3, arg4, arg5);

        public static InterruptableFunc<T1, T2, T3, T4, T5, T6, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> func)
            => (arg1, arg2, arg3, arg4, arg5, arg6, token) => func(arg1, arg2, arg3, arg4, arg5, arg6);

        public static InterruptableFunc<T1, T2, T3, T4, T5, T6, T7, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7);

        public static InterruptableFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);

        public static InterruptableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);

        public static InterruptableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

        public static InterruptableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);

        public static InterruptableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);

        public static InterruptableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);

        public static InterruptableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);

        public static InterruptableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);

        public static InterruptableFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, token) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);

        #endregion

        #region AsyncFunc

        public static InterruptableAsyncFunc<TResult> IgnoreInterruption<TResult>(this AsyncFunc<TResult> func)
            => async (token) => await func().ConfigureAwait(false);

        public static InterruptableAsyncFunc<T, TResult> IgnoreInterruption<T, TResult>(this AsyncFunc<T, TResult> func)
            => async (arg, token) => await func(arg).ConfigureAwait(false);

        public static InterruptableAsyncFunc<T1, T2, TResult> IgnoreInterruption<T1, T2, TResult>(this AsyncFunc<T1, T2, TResult> func)
            => async (arg1, arg2, token) => await func(arg1, arg2).ConfigureAwait(false);

        public static InterruptableAsyncFunc<T1, T2, T3, TResult> IgnoreInterruption<T1, T2, T3, TResult>(this AsyncFunc<T1, T2, T3, TResult> func)
            => async (arg1, arg2, arg3, token) => await func(arg1, arg2, arg3).ConfigureAwait(false);

        public static InterruptableAsyncFunc<T1, T2, T3, T4, TResult> IgnoreInterruption<T1, T2, T3, T4, TResult>(this AsyncFunc<T1, T2, T3, T4, TResult> func)
            => async (arg1, arg2, arg3, arg4, token) => await func(arg1, arg2, arg3, arg4).ConfigureAwait(false);

        public static InterruptableAsyncFunc<T1, T2, T3, T4, T5, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, TResult>(this AsyncFunc<T1, T2, T3, T4, T5, TResult> func)
            => async (arg1, arg2, arg3, arg4, arg5, token) => await func(arg1, arg2, arg3, arg4, arg5).ConfigureAwait(false);

        public static InterruptableAsyncFunc<T1, T2, T3, T4, T5, T6, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, TResult>(this AsyncFunc<T1, T2, T3, T4, T5, T6, TResult> func)
            => async (arg1, arg2, arg3, arg4, arg5, arg6, token) => await func(arg1, arg2, arg3, arg4, arg5, arg6).ConfigureAwait(false);

        public static InterruptableAsyncFunc<T1, T2, T3, T4, T5, T6, T7, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, TResult>(this AsyncFunc<T1, T2, T3, T4, T5, T6, T7, TResult> func)
            => async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, token) => await func(arg1, arg2, arg3, arg4, arg5, arg6, arg7).ConfigureAwait(false);

        public static InterruptableAsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this AsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func)
            => async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => await func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8).ConfigureAwait(false);

        public static InterruptableAsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this AsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func)
            => async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, token) => await func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9).ConfigureAwait(false);

        public static InterruptableAsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this AsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func)
            => async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, token) => await func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10).ConfigureAwait(false);

        public static InterruptableAsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(this AsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func)
            => async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, token) => await func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11).ConfigureAwait(false);

        public static InterruptableAsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(this AsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func)
            => async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => await func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12).ConfigureAwait(false);

        public static InterruptableAsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(this AsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func)
            => async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => await func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13).ConfigureAwait(false);

        public static InterruptableAsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(this AsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func)
            => async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, token) => await func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14).ConfigureAwait(false);

        public static InterruptableAsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(this AsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func)
            => async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, token) => await func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15).ConfigureAwait(false);

        public static InterruptableAsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(this AsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func)
            => async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, token) => await func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16).ConfigureAwait(false);

        #endregion
    }
}
