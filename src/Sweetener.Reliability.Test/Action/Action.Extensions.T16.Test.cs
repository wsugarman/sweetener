// Generated from Action.Extensions.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    // Define type aliases for the various generic types used below as they can become pretty cumbersome
    using TestAction                   = Action     <int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>;
    using InterruptableTestAction      = Action     <int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, CancellationToken>;
    using TestActionProxy              = ActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>;
    using InterruptableTestActionProxy = ActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, CancellationToken>;
    using DelayHandlerProxy            = FuncProxy<int, TimeSpan>;
    using ComplexDelayHandlerProxy     = FuncProxy<int, Exception, TimeSpan>;

    partial class ActionExtensionsTest
    {
        [TestMethod]
        public void WithRetryT17_DelayHandler()
        {
            TestAction nullAction = null;
            TestAction action     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicy.Transient, (DelayHandler)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, TestActionProxy> actionFactory = a => new TestActionProxy((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => a(CancellationToken.None));
            Func<TimeSpan, DelayHandlerProxy> delayHandlerFactory = t => new DelayHandlerProxy((i) => t);
            Func<TestAction, int, ExceptionHandler, Func<int, TimeSpan>, TestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<TestAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, CancellationToken> invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);

            Action<TestActionProxy>           observeAction      = a          => a.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(Arguments.Validate);
            Action<TestActionProxy, TimeSpan> observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT17_Success         (actionFactory, delayHandlerFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT17_Failure         (actionFactory, delayHandlerFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT17_EventualSuccess (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT17_EventualFailure (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT17_RetriesExhausted(actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void WithRetryT17_ComplexDelayHandler()
        {
            TestAction nullAction = null;
            TestAction action     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicy.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicy.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                     , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicy.Transient, (ComplexDelayHandler)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, TestActionProxy> actionFactory = a => new TestActionProxy((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => a(CancellationToken.None));
            Func<TimeSpan, ComplexDelayHandlerProxy> delayHandlerFactory = t => new ComplexDelayHandlerProxy((i, e) => t);
            Func<TestAction, int, ExceptionHandler, Func<int, Exception, TimeSpan>, TestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<TestAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, CancellationToken> invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);

            Action<TestActionProxy>           observeAction      = a          => a.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(Arguments.Validate);
            Action<TestActionProxy, TimeSpan> observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT17_Success         (actionFactory, delayHandlerFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT17_Failure         (actionFactory, delayHandlerFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT17_EventualSuccess (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT17_EventualFailure (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT17_RetriesExhausted(actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
        }

        [TestMethod]
        public void WithRetryT17_WithToken_DelayHandler()
        {
            InterruptableTestAction nullAction = null;
            InterruptableTestAction action     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, token) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicy.Transient, (DelayHandler)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, InterruptableTestActionProxy> actionFactory = a => new InterruptableTestActionProxy((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, token) => a(token));
            Func<TimeSpan, DelayHandlerProxy> delayHandlerFactory = t => new DelayHandlerProxy((i) => t);
            Func<InterruptableTestAction, int, ExceptionHandler, Func<int, TimeSpan>, InterruptableTestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<InterruptableTestAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, CancellationToken> invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, token);

            Action<InterruptableTestActionProxy>           observeAction      = a          => a.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestActionProxy, TimeSpan> observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, CancellationToken>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT17_Success         (actionFactory, delayHandlerFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT17_Failure         (actionFactory, delayHandlerFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT17_EventualSuccess (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT17_EventualFailure (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT17_RetriesExhausted(actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT17_Canceled_Action (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT17_Canceled_Delay  (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void WithRetryT17_WithToken_ComplexDelayHandler()
        {
            InterruptableTestAction nullAction = null;
            InterruptableTestAction action     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, token) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicy.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicy.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                     , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicy.Transient, (ComplexDelayHandler)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, InterruptableTestActionProxy> actionFactory = a => new InterruptableTestActionProxy((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, token) => a(token));
            Func<TimeSpan, ComplexDelayHandlerProxy> delayHandlerFactory = t => new ComplexDelayHandlerProxy((i, e) => t);
            Func<InterruptableTestAction, int, ExceptionHandler, Func<int, Exception, TimeSpan>, InterruptableTestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<InterruptableTestAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, CancellationToken> invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, token);

            Action<InterruptableTestActionProxy>           observeAction      = a          => a.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestActionProxy, TimeSpan> observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, char, float, Guid, CancellationToken>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT17_Success         (actionFactory, delayHandlerFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT17_Failure         (actionFactory, delayHandlerFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT17_EventualSuccess (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT17_EventualFailure (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT17_RetriesExhausted(actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT17_Canceled_Action (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT17_Canceled_Delay  (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
        }
    }
}
