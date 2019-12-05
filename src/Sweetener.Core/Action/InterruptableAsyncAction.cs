// Generated from InterruptableAsyncAction.tt
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sweetener
{
    /// <summary>
    /// Encapsulates an asynchronous method that has zero parameters, does not return a value, and may be canceled prematurely.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncAction" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the method to complete.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate Task InterruptableAsyncAction(CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates an asynchronous method that has one parameter, does not return a value, and may be canceled prematurely.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncAction{T}" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
    /// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates.</typeparam>
    /// <param name="arg">The parameter of the method that this delegate encapsulates.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the method to complete.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate Task InterruptableAsyncAction<in T>(T arg, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates an asynchronous method that has two parameters, does not return a value, and may be canceled prematurely.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncAction{T1, T2}" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the method to complete.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate Task InterruptableAsyncAction<in T1, in T2>(T1 arg1, T2 arg2, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates an asynchronous method that has three parameters, does not return a value, and may be canceled prematurely.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncAction{T1, T2, T3}" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the method to complete.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate Task InterruptableAsyncAction<in T1, in T2, in T3>(T1 arg1, T2 arg2, T3 arg3, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates an asynchronous method that has four parameters, does not return a value, and may be canceled prematurely.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncAction{T1, T2, T3, T4}" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
    /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the method to complete.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate Task InterruptableAsyncAction<in T1, in T2, in T3, in T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates an asynchronous method that has five parameters, does not return a value, and may be canceled prematurely.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncAction{T1, T2, T3, T4, T5}" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
    /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the method to complete.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate Task InterruptableAsyncAction<in T1, in T2, in T3, in T4, in T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates an asynchronous method that has six parameters, does not return a value, and may be canceled prematurely.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncAction{T1, T2, T3, T4, T5, T6}" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates.</typeparam>
    /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the method to complete.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate Task InterruptableAsyncAction<in T1, in T2, in T3, in T4, in T5, in T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates an asynchronous method that has seven parameters, does not return a value, and may be canceled prematurely.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncAction{T1, T2, T3, T4, T5, T6, T7}" />.
    /// Otherwise, the delegate may be invoked incorrectly.
    /// </remarks>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the method that this delegate encapsulates.</typeparam>
    /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg7">The seventh parameter of the method that this delegate encapsulates.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the method to complete.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate Task InterruptableAsyncAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates an asynchronous method that has eight parameters, does not return a value, and may be canceled prematurely.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncAction{T1, T2, T3, T4, T5, T6, T7, T8}" />.
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
    /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg7">The seventh parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg8">The eighth parameter of the method that this delegate encapsulates.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the method to complete.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate Task InterruptableAsyncAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates an asynchronous method that has nine parameters, does not return a value, and may be canceled prematurely.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncAction{T1, T2, T3, T4, T5, T6, T7, T8, T9}" />.
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
    /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg7">The seventh parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg8">The eighth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg9">The ninth parameter of the method that this delegate encapsulates.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the method to complete.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate Task InterruptableAsyncAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates an asynchronous method that has ten parameters, does not return a value, and may be canceled prematurely.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10}" />.
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
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the method to complete.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate Task InterruptableAsyncAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates an asynchronous method that has eleven parameters, does not return a value, and may be canceled prematurely.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11}" />.
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
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the method to complete.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate Task InterruptableAsyncAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates an asynchronous method that has twelve parameters, does not return a value, and may be canceled prematurely.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12}" />.
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
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the method to complete.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate Task InterruptableAsyncAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates an asynchronous method that has thirteen parameters, does not return a value, and may be canceled prematurely.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13}" />.
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
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the method to complete.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate Task InterruptableAsyncAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates an asynchronous method that has fourteen parameters, does not return a value, and may be canceled prematurely.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14}" />.
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
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the method to complete.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate Task InterruptableAsyncAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates an asynchronous method that has fifteen parameters, does not return a value, and may be canceled prematurely.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15}" />.
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
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the method to complete.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate Task InterruptableAsyncAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates an asynchronous method that has sixteen parameters, does not return a value, and may be canceled prematurely.
    /// </summary>
    /// <remarks>
    /// A delegate that returns a <see cref="Task" /> is not necessarily asynchronous, and only
    /// asynchronous delegates should be encapsulated by an <see cref="AsyncAction{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16}" />.
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
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the method to complete.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate Task InterruptableAsyncAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15, in T16>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, CancellationToken cancellationToken = default);

}
