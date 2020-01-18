// Generated from AsyncAction.Extensions.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    // Define type aliases for the various generic types used below as they can become pretty cumbersome
    using TestAction                   = Func     <int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, Task>;
    using InterruptableTestAction      = Func     <int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken, Task>;
    using TestActionProxy              = FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, Task>;
    using InterruptableTestActionProxy = FuncProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken, Task>;
    using DelayHandlerProxy            = FuncProxy<int, TimeSpan>;
    using ComplexDelayHandlerProxy     = FuncProxy<int, Exception, TimeSpan>;

    partial class AsyncActionExtensionsTest
    {
        [TestMethod]
        public void WithAsyncRetryT13_Async_DelayHandler()
        {
            TestAction nullAction = null;
            TestAction action     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => await Task.CompletedTask;
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithAsyncRetry( 4, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithAsyncRetry(-2, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, ExceptionPolicy.Transient, (DelayHandler)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, TestActionProxy> actionFactory = a => new TestActionProxy(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => { await Task.CompletedTask; a(CancellationToken.None); });
            Func<TimeSpan, DelayHandlerProxy> delayHandlerFactory = t => new DelayHandlerProxy((i) => t);
            Func<TestAction, int, ExceptionHandler, Func<int, TimeSpan>, TestAction> withAsyncRetry = (a, r, e, d) => a.WithAsyncRetry(r, e, d.Invoke);
            Action<TestAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12).Wait();

            Action<TestActionProxy>           observeAction      = a          => a.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(Arguments.Validate);
            Action<TestActionProxy, TimeSpan> observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT13_Success         (actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT13_Failure         (actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT13_EventualSuccess (actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT13_EventualFailure (actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT13_RetriesExhausted(actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void WithAsyncRetryT13_Async_ComplexDelayHandler()
        {
            TestAction nullAction = null;
            TestAction action     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => await Task.CompletedTask;
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithAsyncRetry( 4, ExceptionPolicy.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithAsyncRetry(-2, ExceptionPolicy.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, null                     , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, ExceptionPolicy.Transient, (ComplexDelayHandler)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, TestActionProxy> actionFactory = a => new TestActionProxy(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => { await Task.CompletedTask; a(CancellationToken.None); });
            Func<TimeSpan, ComplexDelayHandlerProxy> delayHandlerFactory = t => new ComplexDelayHandlerProxy((i, e) => t);
            Func<TestAction, int, ExceptionHandler, Func<int, Exception, TimeSpan>, TestAction> withAsyncRetry = (a, r, e, d) => a.WithAsyncRetry(r, e, d.Invoke);
            Action<TestAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12).Wait();

            Action<TestActionProxy>           observeAction      = a          => a.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(Arguments.Validate);
            Action<TestActionProxy, TimeSpan> observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT13_Success         (actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT13_Failure         (actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT13_EventualSuccess (actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT13_EventualFailure (actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT13_RetriesExhausted(actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
        }

        [TestMethod]
        public void WithAsyncRetryT13_Async_WithToken_DelayHandler()
        {
            InterruptableTestAction nullAction = null;
            InterruptableTestAction action     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => await Task.CompletedTask;
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithAsyncRetry( 4, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithAsyncRetry(-2, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, ExceptionPolicy.Transient, (DelayHandler)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, InterruptableTestActionProxy> actionFactory = a => new InterruptableTestActionProxy(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => { await Task.CompletedTask; a(token); });
            Func<TimeSpan, DelayHandlerProxy> delayHandlerFactory = t => new DelayHandlerProxy((i) => t);
            Func<InterruptableTestAction, int, ExceptionHandler, Func<int, TimeSpan>, InterruptableTestAction> withAsyncRetry = (a, r, e, d) => a.WithAsyncRetry(r, e, d.Invoke);
            Action<InterruptableTestAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token).Wait();

            Action<InterruptableTestActionProxy>           observeAction      = a          => a.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestActionProxy, TimeSpan> observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT13_Success         (actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT13_Failure         (actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT13_EventualSuccess (actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT13_EventualFailure (actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT13_RetriesExhausted(actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT13_Canceled_Action (actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT13_Canceled_Delay  (actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());

            // We also want to test the scenario where a user passes a synchronous method that returns a Task
            actionFactory = a => new InterruptableTestActionProxy((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => { a(token); return Task.CompletedTask; });
            WithRetryT13_Canceled_Action (actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
        }
        [TestMethod]
        public void WithAsyncRetryT13_Async_WithToken_ComplexDelayHandler()
        {
            InterruptableTestAction nullAction = null;
            InterruptableTestAction action     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => await Task.CompletedTask;
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithAsyncRetry( 4, ExceptionPolicy.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithAsyncRetry(-2, ExceptionPolicy.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, null                     , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithAsyncRetry( 4, ExceptionPolicy.Transient, (ComplexDelayHandler)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, InterruptableTestActionProxy> actionFactory = a => new InterruptableTestActionProxy(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => { await Task.CompletedTask; a(token); });
            Func<TimeSpan, ComplexDelayHandlerProxy> delayHandlerFactory = t => new ComplexDelayHandlerProxy((i, e) => t);
            Func<InterruptableTestAction, int, ExceptionHandler, Func<int, Exception, TimeSpan>, InterruptableTestAction> withAsyncRetry = (a, r, e, d) => a.WithAsyncRetry(r, e, d.Invoke);
            Action<InterruptableTestAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken> invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token).Wait();

            Action<InterruptableTestActionProxy>           observeAction      = a          => a.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestActionProxy, TimeSpan> observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, CancellationToken>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT13_Success         (actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT13_Failure         (actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT13_EventualSuccess (actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT13_EventualFailure (actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT13_RetriesExhausted(actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT13_Canceled_Action (actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT13_Canceled_Delay  (actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));

            // We also want to test the scenario where a user passes a synchronous method that returns a Task
            actionFactory = a => new InterruptableTestActionProxy((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, token) => { a(token); return Task.CompletedTask; });
            WithRetryT13_Canceled_Action (actionFactory, delayHandlerFactory, withAsyncRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
        }
    }
}
