// Generated from AsyncFunc.Extensions.tt
using System.Threading.Tasks;

namespace Sweetener
{
    /// <summary>
    /// Provides a set of methods for invoking asynchronous functions.
    /// </summary>
    public static class AsyncFuncExtensions
    {
        #region InvokeAsync

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncFunc{TResult}" />.
        /// </summary>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="func">The asynchronous function to invoke.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<TResult>(this AsyncFunc<TResult> func)
            => await func().ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncFunc{T, TResult}" />.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="func">The asynchronous function to invoke.</param>
        /// <param name="arg">The parameter of the method that this delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<T, TResult>(this AsyncFunc<T, TResult> func, T arg)
            => await func(arg).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncFunc{T1, T2, TResult}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="func">The asynchronous function to invoke.</param>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<T1, T2, TResult>(this AsyncFunc<T1, T2, TResult> func, T1 arg1, T2 arg2)
            => await func(arg1, arg2).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncFunc{T1, T2, T3, TResult}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="func">The asynchronous function to invoke.</param>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<T1, T2, T3, TResult>(this AsyncFunc<T1, T2, T3, TResult> func, T1 arg1, T2 arg2, T3 arg3)
            => await func(arg1, arg2, arg3).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncFunc{T1, T2, T3, T4, TResult}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="func">The asynchronous function to invoke.</param>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<T1, T2, T3, T4, TResult>(this AsyncFunc<T1, T2, T3, T4, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            => await func(arg1, arg2, arg3, arg4).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncFunc{T1, T2, T3, T4, T5, TResult}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="func">The asynchronous function to invoke.</param>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<T1, T2, T3, T4, T5, TResult>(this AsyncFunc<T1, T2, T3, T4, T5, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            => await func(arg1, arg2, arg3, arg4, arg5).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncFunc{T1, T2, T3, T4, T5, T6, TResult}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="func">The asynchronous function to invoke.</param>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, TResult>(this AsyncFunc<T1, T2, T3, T4, T5, T6, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
            => await func(arg1, arg2, arg3, arg4, arg5, arg6).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncFunc{T1, T2, T3, T4, T5, T6, T7, TResult}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="func">The asynchronous function to invoke.</param>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg7">The seventh parameter of the method that this delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(this AsyncFunc<T1, T2, T3, T4, T5, T6, T7, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
            => await func(arg1, arg2, arg3, arg4, arg5, arg6, arg7).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncFunc{T1, T2, T3, T4, T5, T6, T7, T8, TResult}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="func">The asynchronous function to invoke.</param>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg7">The seventh parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg8">The eighth parameter of the method that this delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this AsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
            => await func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="func">The asynchronous function to invoke.</param>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg7">The seventh parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg8">The eighth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg9">The ninth parameter of the method that this delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this AsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
            => await func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="func">The asynchronous function to invoke.</param>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg7">The seventh parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg8">The eighth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg9">The ninth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg10">The tenth parameter of the method that this delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this AsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
            => await func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="func">The asynchronous function to invoke.</param>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg7">The seventh parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg8">The eighth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg9">The ninth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg10">The tenth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg11">The eleventh parameter of the method that this delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(this AsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
            => await func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="func">The asynchronous function to invoke.</param>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg7">The seventh parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg8">The eighth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg9">The ninth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg10">The tenth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg11">The eleventh parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg12">The twelfth parameter of the method that this delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(this AsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
            => await func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="func">The asynchronous function to invoke.</param>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg7">The seventh parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg8">The eighth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg9">The ninth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg10">The tenth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg11">The eleventh parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg12">The twelfth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg13">The thirteenth parameter of the method that this delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(this AsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
            => await func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="func">The asynchronous function to invoke.</param>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg7">The seventh parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg8">The eighth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg9">The ninth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg10">The tenth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg11">The eleventh parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg12">The twelfth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg13">The thirteenth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg14">The fourteenth parameter of the method that this delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(this AsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
            => await func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="func">The asynchronous function to invoke.</param>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg7">The seventh parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg8">The eighth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg9">The ninth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg10">The tenth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg11">The eleventh parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg12">The twelfth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg13">The thirteenth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg14">The fourteenth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg15">The fifteenth parameter of the method that this delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(this AsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
            => await func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T10">The type of the tenth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T11">The type of the eleventh parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T12">The type of the twelfth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T16">The type of the sixteenth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
        /// <param name="func">The asynchronous function to invoke.</param>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg7">The seventh parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg8">The eighth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg9">The ninth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg10">The tenth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg11">The eleventh parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg12">The twelfth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg13">The thirteenth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg14">The fourteenth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg15">The fifteenth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg16">The sixteenth parameter of the method that this delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(this AsyncFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
            => await func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16).ConfigureAwait(false);

        #endregion
    }
}
