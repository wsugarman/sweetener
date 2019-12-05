// Generated from AsyncFunc.tt
using System.Threading.Tasks;

namespace Sweetener
{
    /// <summary>
    /// Encapsulates an asynchronous method that has zero parameters and returns a value
    /// of the type specified by the <typeparamref name="TResult"/> parameter.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task{TResult}" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncFunc{TResult}" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <returns>
    /// A task that represents the asynchronous operation. The value of the task parameter
    /// contains the return value of the method that this delegate encapsulates.
    /// </returns>
    public delegate Task<TResult> AsyncFunc<TResult>();

    /// <summary>
    /// Encapsulates an asynchronous method that has one parameter and returns a value
    /// of the type specified by the <typeparamref name="TResult"/> parameter.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task{TResult}" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncFunc{T, TResult}" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
    /// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="arg">The parameter of the method that this delegate encapsulates.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The value of the task parameter
    /// contains the return value of the method that this delegate encapsulates.
    /// </returns>
    public delegate Task<TResult> AsyncFunc<in T, TResult>(T arg);

    /// <summary>
    /// Encapsulates an asynchronous method that has two parameters and returns a value
    /// of the type specified by the <typeparamref name="TResult"/> parameter.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task{TResult}" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncFunc{T1, T2, TResult}" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The value of the task parameter
    /// contains the return value of the method that this delegate encapsulates.
    /// </returns>
    public delegate Task<TResult> AsyncFunc<in T1, in T2, TResult>(T1 arg1, T2 arg2);

    /// <summary>
    /// Encapsulates an asynchronous method that has three parameters and returns a value
    /// of the type specified by the <typeparamref name="TResult"/> parameter.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task{TResult}" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncFunc{T1, T2, T3, TResult}" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The value of the task parameter
    /// contains the return value of the method that this delegate encapsulates.
    /// </returns>
    public delegate Task<TResult> AsyncFunc<in T1, in T2, in T3, TResult>(T1 arg1, T2 arg2, T3 arg3);

    /// <summary>
    /// Encapsulates an asynchronous method that has four parameters and returns a value
    /// of the type specified by the <typeparamref name="TResult"/> parameter.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task{TResult}" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncFunc{T1, T2, T3, T4, TResult}" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The value of the task parameter
    /// contains the return value of the method that this delegate encapsulates.
    /// </returns>
    public delegate Task<TResult> AsyncFunc<in T1, in T2, in T3, in T4, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

    /// <summary>
    /// Encapsulates an asynchronous method that has five parameters and returns a value
    /// of the type specified by the <typeparamref name="TResult"/> parameter.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task{TResult}" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncFunc{T1, T2, T3, T4, T5, TResult}" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The value of the task parameter
    /// contains the return value of the method that this delegate encapsulates.
    /// </returns>
    public delegate Task<TResult> AsyncFunc<in T1, in T2, in T3, in T4, in T5, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

    /// <summary>
    /// Encapsulates an asynchronous method that has six parameters and returns a value
    /// of the type specified by the <typeparamref name="TResult"/> parameter.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task{TResult}" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncFunc{T1, T2, T3, T4, T5, T6, TResult}" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The value of the task parameter
    /// contains the return value of the method that this delegate encapsulates.
    /// </returns>
    public delegate Task<TResult> AsyncFunc<in T1, in T2, in T3, in T4, in T5, in T6, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);

    /// <summary>
    /// Encapsulates an asynchronous method that has seven parameters and returns a value
    /// of the type specified by the <typeparamref name="TResult"/> parameter.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task{TResult}" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncFunc{T1, T2, T3, T4, T5, T6, T7, TResult}" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg7">The seventh parameter of the method that this delegate encapsulates.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The value of the task parameter
    /// contains the return value of the method that this delegate encapsulates.
    /// </returns>
    public delegate Task<TResult> AsyncFunc<in T1, in T2, in T3, in T4, in T5, in T6, in T7, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);

    /// <summary>
    /// Encapsulates an asynchronous method that has eight parameters and returns a value
    /// of the type specified by the <typeparamref name="TResult"/> parameter.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task{TResult}" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncFunc{T1, T2, T3, T4, T5, T6, T7, T8, TResult}" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg7">The seventh parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg8">The eighth parameter of the method that this delegate encapsulates.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The value of the task parameter
    /// contains the return value of the method that this delegate encapsulates.
    /// </returns>
    public delegate Task<TResult> AsyncFunc<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);

    /// <summary>
    /// Encapsulates an asynchronous method that has nine parameters and returns a value
    /// of the type specified by the <typeparamref name="TResult"/> parameter.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task{TResult}" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult}" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
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
    /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg7">The seventh parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg8">The eighth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg9">The ninth parameter of the method that this delegate encapsulates.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The value of the task parameter
    /// contains the return value of the method that this delegate encapsulates.
    /// </returns>
    public delegate Task<TResult> AsyncFunc<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9);

    /// <summary>
    /// Encapsulates an asynchronous method that has ten parameters and returns a value
    /// of the type specified by the <typeparamref name="TResult"/> parameter.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task{TResult}" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult}" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
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
    /// <returns>
    /// A task that represents the asynchronous operation. The value of the task parameter
    /// contains the return value of the method that this delegate encapsulates.
    /// </returns>
    public delegate Task<TResult> AsyncFunc<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10);

    /// <summary>
    /// Encapsulates an asynchronous method that has eleven parameters and returns a value
    /// of the type specified by the <typeparamref name="TResult"/> parameter.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task{TResult}" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult}" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
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
    /// <returns>
    /// A task that represents the asynchronous operation. The value of the task parameter
    /// contains the return value of the method that this delegate encapsulates.
    /// </returns>
    public delegate Task<TResult> AsyncFunc<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11);

    /// <summary>
    /// Encapsulates an asynchronous method that has twelve parameters and returns a value
    /// of the type specified by the <typeparamref name="TResult"/> parameter.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task{TResult}" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult}" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
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
    /// <returns>
    /// A task that represents the asynchronous operation. The value of the task parameter
    /// contains the return value of the method that this delegate encapsulates.
    /// </returns>
    public delegate Task<TResult> AsyncFunc<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12);

    /// <summary>
    /// Encapsulates an asynchronous method that has thirteen parameters and returns a value
    /// of the type specified by the <typeparamref name="TResult"/> parameter.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task{TResult}" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult}" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
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
    /// <returns>
    /// A task that represents the asynchronous operation. The value of the task parameter
    /// contains the return value of the method that this delegate encapsulates.
    /// </returns>
    public delegate Task<TResult> AsyncFunc<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13);

    /// <summary>
    /// Encapsulates an asynchronous method that has fourteen parameters and returns a value
    /// of the type specified by the <typeparamref name="TResult"/> parameter.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task{TResult}" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult}" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
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
    /// <returns>
    /// A task that represents the asynchronous operation. The value of the task parameter
    /// contains the return value of the method that this delegate encapsulates.
    /// </returns>
    public delegate Task<TResult> AsyncFunc<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14);

    /// <summary>
    /// Encapsulates an asynchronous method that has fifteen parameters and returns a value
    /// of the type specified by the <typeparamref name="TResult"/> parameter.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task{TResult}" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult}" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
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
    /// <returns>
    /// A task that represents the asynchronous operation. The value of the task parameter
    /// contains the return value of the method that this delegate encapsulates.
    /// </returns>
    public delegate Task<TResult> AsyncFunc<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15);

    /// <summary>
    /// Encapsulates an asynchronous method that has sixteen parameters and returns a value
    /// of the type specified by the <typeparamref name="TResult"/> parameter.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task{TResult}" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncFunc{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult}" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
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
    /// <returns>
    /// A task that represents the asynchronous operation. The value of the task parameter
    /// contains the return value of the method that this delegate encapsulates.
    /// </returns>
    public delegate Task<TResult> AsyncFunc<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15, in T16, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16);

}
