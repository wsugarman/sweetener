// Generated from InterruptableAction.tt
using System;
using System.Threading;

namespace Sweetener.Reliability
{
    /// <summary>
    /// Encapsulates a method with zero parameters and no return value that may be
    /// cancelled prematurely using the optional <paramref name="cancellationToken" />.
    /// </summary>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the operation to complete.
    /// </param>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate void InterruptableAction(CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates a method with a single parameter and no return value that may be
    /// cancelled prematurely using the optional <paramref name="cancellationToken" />.
    /// </summary>
    /// <typeparam name="T">The type of the parameter of the underlying delegate.</typeparam>
    /// <param name="arg">The argument for the underlying delegate.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the operation to complete.
    /// </param>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate void InterruptableAction<in T>(T arg, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates a method with two parameters and no return value that may be
    /// cancelled prematurely using the optional <paramref name="cancellationToken" />.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <param name="arg1">The first argument for the underlying delegate.</param>
    /// <param name="arg2">The second argument for the underlying delegate.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the operation to complete.
    /// </param>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate void InterruptableAction<in T1, in T2>(T1 arg1, T2 arg2, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates a method with three parameters and no return value that may be
    /// cancelled prematurely using the optional <paramref name="cancellationToken" />.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <param name="arg1">The first argument for the underlying delegate.</param>
    /// <param name="arg2">The second argument for the underlying delegate.</param>
    /// <param name="arg3">The third argument for the underlying delegate.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the operation to complete.
    /// </param>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate void InterruptableAction<in T1, in T2, in T3>(T1 arg1, T2 arg2, T3 arg3, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates a method with four parameters and no return value that may be
    /// cancelled prematurely using the optional <paramref name="cancellationToken" />.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <param name="arg1">The first argument for the underlying delegate.</param>
    /// <param name="arg2">The second argument for the underlying delegate.</param>
    /// <param name="arg3">The third argument for the underlying delegate.</param>
    /// <param name="arg4">The fourth argument for the underlying delegate.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the operation to complete.
    /// </param>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate void InterruptableAction<in T1, in T2, in T3, in T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates a method with five parameters and no return value that may be
    /// cancelled prematurely using the optional <paramref name="cancellationToken" />.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <param name="arg1">The first argument for the underlying delegate.</param>
    /// <param name="arg2">The second argument for the underlying delegate.</param>
    /// <param name="arg3">The third argument for the underlying delegate.</param>
    /// <param name="arg4">The fourth argument for the underlying delegate.</param>
    /// <param name="arg5">The fifth argument for the underlying delegate.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the operation to complete.
    /// </param>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate void InterruptableAction<in T1, in T2, in T3, in T4, in T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates a method with six parameters and no return value that may be
    /// cancelled prematurely using the optional <paramref name="cancellationToken" />.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    /// <param name="arg1">The first argument for the underlying delegate.</param>
    /// <param name="arg2">The second argument for the underlying delegate.</param>
    /// <param name="arg3">The third argument for the underlying delegate.</param>
    /// <param name="arg4">The fourth argument for the underlying delegate.</param>
    /// <param name="arg5">The fifth argument for the underlying delegate.</param>
    /// <param name="arg6">The sixth argument for the underlying delegate.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the operation to complete.
    /// </param>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate void InterruptableAction<in T1, in T2, in T3, in T4, in T5, in T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates a method with seven parameters and no return value that may be
    /// cancelled prematurely using the optional <paramref name="cancellationToken" />.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
    /// <param name="arg1">The first argument for the underlying delegate.</param>
    /// <param name="arg2">The second argument for the underlying delegate.</param>
    /// <param name="arg3">The third argument for the underlying delegate.</param>
    /// <param name="arg4">The fourth argument for the underlying delegate.</param>
    /// <param name="arg5">The fifth argument for the underlying delegate.</param>
    /// <param name="arg6">The sixth argument for the underlying delegate.</param>
    /// <param name="arg7">The seventh argument for the underlying delegate.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the operation to complete.
    /// </param>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate void InterruptableAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates a method with eight parameters and no return value that may be
    /// cancelled prematurely using the optional <paramref name="cancellationToken" />.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
    /// <param name="arg1">The first argument for the underlying delegate.</param>
    /// <param name="arg2">The second argument for the underlying delegate.</param>
    /// <param name="arg3">The third argument for the underlying delegate.</param>
    /// <param name="arg4">The fourth argument for the underlying delegate.</param>
    /// <param name="arg5">The fifth argument for the underlying delegate.</param>
    /// <param name="arg6">The sixth argument for the underlying delegate.</param>
    /// <param name="arg7">The seventh argument for the underlying delegate.</param>
    /// <param name="arg8">The eighth argument for the underlying delegate.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the operation to complete.
    /// </param>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate void InterruptableAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates a method with nine parameters and no return value that may be
    /// cancelled prematurely using the optional <paramref name="cancellationToken" />.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
    /// <param name="arg1">The first argument for the underlying delegate.</param>
    /// <param name="arg2">The second argument for the underlying delegate.</param>
    /// <param name="arg3">The third argument for the underlying delegate.</param>
    /// <param name="arg4">The fourth argument for the underlying delegate.</param>
    /// <param name="arg5">The fifth argument for the underlying delegate.</param>
    /// <param name="arg6">The sixth argument for the underlying delegate.</param>
    /// <param name="arg7">The seventh argument for the underlying delegate.</param>
    /// <param name="arg8">The eighth argument for the underlying delegate.</param>
    /// <param name="arg9">The ninth argument for the underlying delegate.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the operation to complete.
    /// </param>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate void InterruptableAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates a method with ten parameters and no return value that may be
    /// cancelled prematurely using the optional <paramref name="cancellationToken" />.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
    /// <param name="arg1">The first argument for the underlying delegate.</param>
    /// <param name="arg2">The second argument for the underlying delegate.</param>
    /// <param name="arg3">The third argument for the underlying delegate.</param>
    /// <param name="arg4">The fourth argument for the underlying delegate.</param>
    /// <param name="arg5">The fifth argument for the underlying delegate.</param>
    /// <param name="arg6">The sixth argument for the underlying delegate.</param>
    /// <param name="arg7">The seventh argument for the underlying delegate.</param>
    /// <param name="arg8">The eighth argument for the underlying delegate.</param>
    /// <param name="arg9">The ninth argument for the underlying delegate.</param>
    /// <param name="arg10">The tenth argument for the underlying delegate.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the operation to complete.
    /// </param>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate void InterruptableAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates a method with eleven parameters and no return value that may be
    /// cancelled prematurely using the optional <paramref name="cancellationToken" />.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
    /// <param name="arg1">The first argument for the underlying delegate.</param>
    /// <param name="arg2">The second argument for the underlying delegate.</param>
    /// <param name="arg3">The third argument for the underlying delegate.</param>
    /// <param name="arg4">The fourth argument for the underlying delegate.</param>
    /// <param name="arg5">The fifth argument for the underlying delegate.</param>
    /// <param name="arg6">The sixth argument for the underlying delegate.</param>
    /// <param name="arg7">The seventh argument for the underlying delegate.</param>
    /// <param name="arg8">The eighth argument for the underlying delegate.</param>
    /// <param name="arg9">The ninth argument for the underlying delegate.</param>
    /// <param name="arg10">The tenth argument for the underlying delegate.</param>
    /// <param name="arg11">The eleventh argument for the underlying delegate.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the operation to complete.
    /// </param>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate void InterruptableAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates a method with twelve parameters and no return value that may be
    /// cancelled prematurely using the optional <paramref name="cancellationToken" />.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
    /// <param name="arg1">The first argument for the underlying delegate.</param>
    /// <param name="arg2">The second argument for the underlying delegate.</param>
    /// <param name="arg3">The third argument for the underlying delegate.</param>
    /// <param name="arg4">The fourth argument for the underlying delegate.</param>
    /// <param name="arg5">The fifth argument for the underlying delegate.</param>
    /// <param name="arg6">The sixth argument for the underlying delegate.</param>
    /// <param name="arg7">The seventh argument for the underlying delegate.</param>
    /// <param name="arg8">The eighth argument for the underlying delegate.</param>
    /// <param name="arg9">The ninth argument for the underlying delegate.</param>
    /// <param name="arg10">The tenth argument for the underlying delegate.</param>
    /// <param name="arg11">The eleventh argument for the underlying delegate.</param>
    /// <param name="arg12">The twelfth argument for the underlying delegate.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the operation to complete.
    /// </param>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate void InterruptableAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates a method with thirteen parameters and no return value that may be
    /// cancelled prematurely using the optional <paramref name="cancellationToken" />.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T13">The type of the thirteenth parameter of the underlying delegate.</typeparam>
    /// <param name="arg1">The first argument for the underlying delegate.</param>
    /// <param name="arg2">The second argument for the underlying delegate.</param>
    /// <param name="arg3">The third argument for the underlying delegate.</param>
    /// <param name="arg4">The fourth argument for the underlying delegate.</param>
    /// <param name="arg5">The fifth argument for the underlying delegate.</param>
    /// <param name="arg6">The sixth argument for the underlying delegate.</param>
    /// <param name="arg7">The seventh argument for the underlying delegate.</param>
    /// <param name="arg8">The eighth argument for the underlying delegate.</param>
    /// <param name="arg9">The ninth argument for the underlying delegate.</param>
    /// <param name="arg10">The tenth argument for the underlying delegate.</param>
    /// <param name="arg11">The eleventh argument for the underlying delegate.</param>
    /// <param name="arg12">The twelfth argument for the underlying delegate.</param>
    /// <param name="arg13">The thirteenth argument for the underlying delegate.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the operation to complete.
    /// </param>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate void InterruptableAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates a method with fourteen parameters and no return value that may be
    /// cancelled prematurely using the optional <paramref name="cancellationToken" />.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T13">The type of the thirteenth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T14">The type of the fourteenth parameter of the underlying delegate.</typeparam>
    /// <param name="arg1">The first argument for the underlying delegate.</param>
    /// <param name="arg2">The second argument for the underlying delegate.</param>
    /// <param name="arg3">The third argument for the underlying delegate.</param>
    /// <param name="arg4">The fourth argument for the underlying delegate.</param>
    /// <param name="arg5">The fifth argument for the underlying delegate.</param>
    /// <param name="arg6">The sixth argument for the underlying delegate.</param>
    /// <param name="arg7">The seventh argument for the underlying delegate.</param>
    /// <param name="arg8">The eighth argument for the underlying delegate.</param>
    /// <param name="arg9">The ninth argument for the underlying delegate.</param>
    /// <param name="arg10">The tenth argument for the underlying delegate.</param>
    /// <param name="arg11">The eleventh argument for the underlying delegate.</param>
    /// <param name="arg12">The twelfth argument for the underlying delegate.</param>
    /// <param name="arg13">The thirteenth argument for the underlying delegate.</param>
    /// <param name="arg14">The fourteenth argument for the underlying delegate.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the operation to complete.
    /// </param>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate void InterruptableAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates a method with fifteen parameters and no return value that may be
    /// cancelled prematurely using the optional <paramref name="cancellationToken" />.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T13">The type of the thirteenth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T14">The type of the fourteenth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T15">The type of the fifteenth parameter of the underlying delegate.</typeparam>
    /// <param name="arg1">The first argument for the underlying delegate.</param>
    /// <param name="arg2">The second argument for the underlying delegate.</param>
    /// <param name="arg3">The third argument for the underlying delegate.</param>
    /// <param name="arg4">The fourth argument for the underlying delegate.</param>
    /// <param name="arg5">The fifth argument for the underlying delegate.</param>
    /// <param name="arg6">The sixth argument for the underlying delegate.</param>
    /// <param name="arg7">The seventh argument for the underlying delegate.</param>
    /// <param name="arg8">The eighth argument for the underlying delegate.</param>
    /// <param name="arg9">The ninth argument for the underlying delegate.</param>
    /// <param name="arg10">The tenth argument for the underlying delegate.</param>
    /// <param name="arg11">The eleventh argument for the underlying delegate.</param>
    /// <param name="arg12">The twelfth argument for the underlying delegate.</param>
    /// <param name="arg13">The thirteenth argument for the underlying delegate.</param>
    /// <param name="arg14">The fourteenth argument for the underlying delegate.</param>
    /// <param name="arg15">The fifteenth argument for the underlying delegate.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the operation to complete.
    /// </param>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate void InterruptableAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, CancellationToken cancellationToken = default);

    /// <summary>
    /// Encapsulates a method with sixteen parameters and no return value that may be
    /// cancelled prematurely using the optional <paramref name="cancellationToken" />.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T11">The type of the eleventh parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T12">The type of the twelfth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T13">The type of the thirteenth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T14">The type of the fourteenth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T15">The type of the fifteenth parameter of the underlying delegate.</typeparam>
    /// <typeparam name="T16">The type of the sixteenth parameter of the underlying delegate.</typeparam>
    /// <param name="arg1">The first argument for the underlying delegate.</param>
    /// <param name="arg2">The second argument for the underlying delegate.</param>
    /// <param name="arg3">The third argument for the underlying delegate.</param>
    /// <param name="arg4">The fourth argument for the underlying delegate.</param>
    /// <param name="arg5">The fifth argument for the underlying delegate.</param>
    /// <param name="arg6">The sixth argument for the underlying delegate.</param>
    /// <param name="arg7">The seventh argument for the underlying delegate.</param>
    /// <param name="arg8">The eighth argument for the underlying delegate.</param>
    /// <param name="arg9">The ninth argument for the underlying delegate.</param>
    /// <param name="arg10">The tenth argument for the underlying delegate.</param>
    /// <param name="arg11">The eleventh argument for the underlying delegate.</param>
    /// <param name="arg12">The twelfth argument for the underlying delegate.</param>
    /// <param name="arg13">The thirteenth argument for the underlying delegate.</param>
    /// <param name="arg14">The fourteenth argument for the underlying delegate.</param>
    /// <param name="arg15">The fifteenth argument for the underlying delegate.</param>
    /// <param name="arg16">The sixteenth argument for the underlying delegate.</param>
    /// <param name="cancellationToken">
    /// An optional cancellation token to observe while waiting for the operation to complete.
    /// </param>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
    public delegate void InterruptableAction<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15, in T16>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, CancellationToken cancellationToken = default);

}
