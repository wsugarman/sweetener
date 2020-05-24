// Generated from Cancellation.Extensions.tt
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Sweetener.Reliability
{
    internal static class CancellationExtensions
    {
        public static bool IsCancellation(this Exception exception, CancellationToken cancellationToken)
            => exception is OperationCanceledException oce
                && oce.CancellationToken == cancellationToken
                && cancellationToken.IsCancellationRequested;

        #region Action

        [return: NotNullIfNotNull("action")]
        public static Action<CancellationToken>? IgnoreInterruption(this Action? action)
            => action == null ? (Action<CancellationToken>?)null : (token) => action!();

        [return: NotNullIfNotNull("action")]
        public static Action<T, CancellationToken>? IgnoreInterruption<T>(this Action<T>? action)
            => action == null ? (Action<T, CancellationToken>?)null : (arg, token) => action!(arg);

        [return: NotNullIfNotNull("action")]
        public static Action<T1, T2, CancellationToken>? IgnoreInterruption<T1, T2>(this Action<T1, T2>? action)
            => action == null ? (Action<T1, T2, CancellationToken>?)null : (arg1, arg2, token) => action!(arg1, arg2);

        [return: NotNullIfNotNull("action")]
        public static Action<T1, T2, T3, CancellationToken>? IgnoreInterruption<T1, T2, T3>(this Action<T1, T2, T3>? action)
            => action == null ? (Action<T1, T2, T3, CancellationToken>?)null : (arg1, arg2, arg3, token) => action!(arg1, arg2, arg3);

        [return: NotNullIfNotNull("action")]
        public static Action<T1, T2, T3, T4, CancellationToken>? IgnoreInterruption<T1, T2, T3, T4>(this Action<T1, T2, T3, T4>? action)
            => action == null ? (Action<T1, T2, T3, T4, CancellationToken>?)null : (arg1, arg2, arg3, arg4, token) => action!(arg1, arg2, arg3, arg4);

        [return: NotNullIfNotNull("action")]
        public static Action<T1, T2, T3, T4, T5, CancellationToken>? IgnoreInterruption<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5>? action)
            => action == null ? (Action<T1, T2, T3, T4, T5, CancellationToken>?)null : (arg1, arg2, arg3, arg4, arg5, token) => action!(arg1, arg2, arg3, arg4, arg5);

        [return: NotNullIfNotNull("action")]
        public static Action<T1, T2, T3, T4, T5, T6, CancellationToken>? IgnoreInterruption<T1, T2, T3, T4, T5, T6>(this Action<T1, T2, T3, T4, T5, T6>? action)
            => action == null ? (Action<T1, T2, T3, T4, T5, T6, CancellationToken>?)null : (arg1, arg2, arg3, arg4, arg5, arg6, token) => action!(arg1, arg2, arg3, arg4, arg5, arg6);

        [return: NotNullIfNotNull("action")]
        public static Action<T1, T2, T3, T4, T5, T6, T7, CancellationToken>? IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7>(this Action<T1, T2, T3, T4, T5, T6, T7>? action)
            => action == null ? (Action<T1, T2, T3, T4, T5, T6, T7, CancellationToken>?)null : (arg1, arg2, arg3, arg4, arg5, arg6, arg7, token) => action!(arg1, arg2, arg3, arg4, arg5, arg6, arg7);

        [return: NotNullIfNotNull("action")]
        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, CancellationToken>? IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8>(this Action<T1, T2, T3, T4, T5, T6, T7, T8>? action)
            => action == null ? (Action<T1, T2, T3, T4, T5, T6, T7, T8, CancellationToken>?)null : (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => action!(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);

        #endregion

        #region Func

        [return: NotNullIfNotNull("func")]
        public static Func<CancellationToken, TResult>? IgnoreInterruption<TResult>(this Func<TResult>? func)
            => func == null ? (Func<CancellationToken, TResult>?)null : (token) => func!();

        [return: NotNullIfNotNull("func")]
        public static Func<T, CancellationToken, TResult>? IgnoreInterruption<T, TResult>(this Func<T, TResult>? func)
            => func == null ? (Func<T, CancellationToken, TResult>?)null : (arg, token) => func!(arg);

        [return: NotNullIfNotNull("func")]
        public static Func<T1, T2, CancellationToken, TResult>? IgnoreInterruption<T1, T2, TResult>(this Func<T1, T2, TResult>? func)
            => func == null ? (Func<T1, T2, CancellationToken, TResult>?)null : (arg1, arg2, token) => func!(arg1, arg2);

        [return: NotNullIfNotNull("func")]
        public static Func<T1, T2, T3, CancellationToken, TResult>? IgnoreInterruption<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult>? func)
            => func == null ? (Func<T1, T2, T3, CancellationToken, TResult>?)null : (arg1, arg2, arg3, token) => func!(arg1, arg2, arg3);

        [return: NotNullIfNotNull("func")]
        public static Func<T1, T2, T3, T4, CancellationToken, TResult>? IgnoreInterruption<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult>? func)
            => func == null ? (Func<T1, T2, T3, T4, CancellationToken, TResult>?)null : (arg1, arg2, arg3, arg4, token) => func!(arg1, arg2, arg3, arg4);

        [return: NotNullIfNotNull("func")]
        public static Func<T1, T2, T3, T4, T5, CancellationToken, TResult>? IgnoreInterruption<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult>? func)
            => func == null ? (Func<T1, T2, T3, T4, T5, CancellationToken, TResult>?)null : (arg1, arg2, arg3, arg4, arg5, token) => func!(arg1, arg2, arg3, arg4, arg5);

        [return: NotNullIfNotNull("func")]
        public static Func<T1, T2, T3, T4, T5, T6, CancellationToken, TResult>? IgnoreInterruption<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult>? func)
            => func == null ? (Func<T1, T2, T3, T4, T5, T6, CancellationToken, TResult>?)null : (arg1, arg2, arg3, arg4, arg5, arg6, token) => func!(arg1, arg2, arg3, arg4, arg5, arg6);

        [return: NotNullIfNotNull("func")]
        public static Func<T1, T2, T3, T4, T5, T6, T7, CancellationToken, TResult>? IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult>? func)
            => func == null ? (Func<T1, T2, T3, T4, T5, T6, T7, CancellationToken, TResult>?)null : (arg1, arg2, arg3, arg4, arg5, arg6, arg7, token) => func!(arg1, arg2, arg3, arg4, arg5, arg6, arg7);

        [return: NotNullIfNotNull("func")]
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, CancellationToken, TResult>? IgnoreInterruption<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>? func)
            => func == null ? (Func<T1, T2, T3, T4, T5, T6, T7, T8, CancellationToken, TResult>?)null : (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, token) => func!(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);

        #endregion
    }
}
