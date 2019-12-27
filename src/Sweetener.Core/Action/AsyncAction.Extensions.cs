// Generated from AsyncAction.Extensions.tt
using System.Threading.Tasks;

namespace Sweetener
{
    /// <summary>
    /// Provides a set of methods for invoking asynchronous actions.
    /// </summary>
    public static class AsyncActionExtensions
    {
        #region InvokeAsync

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncAction" />.
        /// </summary>
        /// <param name="action">The asynchronous action to invoke.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync(this AsyncAction action)
            => await action().ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncAction{T}" />.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates.</typeparam>
        /// <param name="action">The asynchronous action to invoke.</param>
        /// <param name="arg">The parameter of the method that this delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<T>(this AsyncAction<T> action, T arg)
            => await action(arg).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncAction{T1, T2}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <param name="action">The asynchronous action to invoke.</param>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<T1, T2>(this AsyncAction<T1, T2> action, T1 arg1, T2 arg2)
            => await action(arg1, arg2).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncAction{T1, T2, T3}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <param name="action">The asynchronous action to invoke.</param>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<T1, T2, T3>(this AsyncAction<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
            => await action(arg1, arg2, arg3).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncAction{T1, T2, T3, T4}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <param name="action">The asynchronous action to invoke.</param>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<T1, T2, T3, T4>(this AsyncAction<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            => await action(arg1, arg2, arg3, arg4).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncAction{T1, T2, T3, T4, T5}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
        /// <param name="action">The asynchronous action to invoke.</param>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<T1, T2, T3, T4, T5>(this AsyncAction<T1, T2, T3, T4, T5> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            => await action(arg1, arg2, arg3, arg4, arg5).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncAction{T1, T2, T3, T4, T5, T6}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates.</typeparam>
        /// <param name="action">The asynchronous action to invoke.</param>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6>(this AsyncAction<T1, T2, T3, T4, T5, T6> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
            => await action(arg1, arg2, arg3, arg4, arg5, arg6).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncAction{T1, T2, T3, T4, T5, T6, T7}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the method that this delegate encapsulates.</typeparam>
        /// <param name="action">The asynchronous action to invoke.</param>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg7">The seventh parameter of the method that this delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7>(this AsyncAction<T1, T2, T3, T4, T5, T6, T7> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
            => await action(arg1, arg2, arg3, arg4, arg5, arg6, arg7).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncAction{T1, T2, T3, T4, T5, T6, T7, T8}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the method that this delegate encapsulates.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the method that this delegate encapsulates.</typeparam>
        /// <param name="action">The asynchronous action to invoke.</param>
        /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg7">The seventh parameter of the method that this delegate encapsulates.</param>
        /// <param name="arg8">The eighth parameter of the method that this delegate encapsulates.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this AsyncAction<T1, T2, T3, T4, T5, T6, T7, T8> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
            => await action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncAction{T1, T2, T3, T4, T5, T6, T7, T8, T9}" />.
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
        /// <param name="action">The asynchronous action to invoke.</param>
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
        public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this AsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
            => await action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10}" />.
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
        /// <param name="action">The asynchronous action to invoke.</param>
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
        public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this AsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
            => await action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11}" />.
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
        /// <param name="action">The asynchronous action to invoke.</param>
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
        public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this AsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
            => await action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12}" />.
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
        /// <param name="action">The asynchronous action to invoke.</param>
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
        public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this AsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
            => await action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13}" />.
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
        /// <param name="action">The asynchronous action to invoke.</param>
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
        public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this AsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
            => await action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14}" />.
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
        /// <param name="action">The asynchronous action to invoke.</param>
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
        public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this AsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
            => await action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15}" />.
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
        /// <param name="action">The asynchronous action to invoke.</param>
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
        public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this AsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
            => await action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously invokes an <see cref="AsyncAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16}" />.
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
        /// <param name="action">The asynchronous action to invoke.</param>
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
        public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this AsyncAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
            => await action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16).ConfigureAwait(false);

        #endregion
    }
}
