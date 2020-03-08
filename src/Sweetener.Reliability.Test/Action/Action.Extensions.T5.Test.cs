// Generated from Action.Extensions.Test.tt
using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    // Define type aliases for the various generic types used below as they can become pretty cumbersome
    using TestAction                   = Action     <int, string, double, long, ushort>;
    using InterruptableTestAction      = Action     <int, string, double, long, ushort, CancellationToken>;
    using TestActionProxy              = ActionProxy<int, string, double, long, ushort>;
    using InterruptableTestActionProxy = ActionProxy<int, string, double, long, ushort, CancellationToken>;
    using DelayHandlerProxy            = FuncProxy<int, TimeSpan>;
    using ComplexDelayHandlerProxy     = FuncProxy<int, Exception, TimeSpan>;

    partial class ActionExtensionsTest
    {
        [TestMethod]
        public void WithRetryT6_DelayHandler()
        {
            TestAction? nullAction = null;
            TestAction  action     = (arg1, arg2, arg3, arg4, arg5) => Operation.Null();

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicy.Transient, (DelayHandler)null));

#nullable enable

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, TestActionProxy> actionFactory = a => new TestActionProxy((arg1, arg2, arg3, arg4, arg5) => a(CancellationToken.None));
            Func<TimeSpan, DelayHandlerProxy> delayHandlerFactory = t => new DelayHandlerProxy((i) => t);
            Func<TestAction, int, ExceptionHandler, Func<int, TimeSpan>, TestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<TestAction, int, string, double, long, ushort, CancellationToken> invoke = (action, arg1, arg2, arg3, arg4, arg5, token) => action(arg1, arg2, arg3, arg4, arg5);

            Action<TestActionProxy>?           observeAction      = a          => a.Invoking += Expect.Arguments<int, string, double, long, ushort>(Arguments.Validate);
            Action<TestActionProxy, TimeSpan>? observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT6_Success         (actionFactory, delayHandlerFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT6_Failure         (actionFactory, delayHandlerFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT6_EventualSuccess (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT6_EventualFailure (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT6_RetriesExhausted(actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void WithRetryT6_ComplexDelayHandler()
        {
            TestAction? nullAction = null;
            TestAction  action     = (arg1, arg2, arg3, arg4, arg5) => Operation.Null();

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicy.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicy.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                     , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicy.Transient, (ComplexDelayHandler)null));

#nullable enable

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, TestActionProxy> actionFactory = a => new TestActionProxy((arg1, arg2, arg3, arg4, arg5) => a(CancellationToken.None));
            Func<TimeSpan, ComplexDelayHandlerProxy> delayHandlerFactory = t => new ComplexDelayHandlerProxy((i, e) => t);
            Func<TestAction, int, ExceptionHandler, Func<int, Exception, TimeSpan>, TestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<TestAction, int, string, double, long, ushort, CancellationToken> invoke = (action, arg1, arg2, arg3, arg4, arg5, token) => action(arg1, arg2, arg3, arg4, arg5);

            Action<TestActionProxy>?           observeAction      = a          => a.Invoking += Expect.Arguments<int, string, double, long, ushort>(Arguments.Validate);
            Action<TestActionProxy, TimeSpan>? observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT6_Success         (actionFactory, delayHandlerFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT6_Failure         (actionFactory, delayHandlerFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT6_EventualSuccess (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT6_EventualFailure (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT6_RetriesExhausted(actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
        }

        [TestMethod]
        public void WithRetryT6_WithToken_DelayHandler()
        {
            InterruptableTestAction? nullAction = null;
            InterruptableTestAction  action     = (arg1, arg2, arg3, arg4, arg5, token) => Operation.Null();

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicy.Transient, (DelayHandler)null));

#nullable enable

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, InterruptableTestActionProxy> actionFactory = a => new InterruptableTestActionProxy((arg1, arg2, arg3, arg4, arg5, token) => a(token));
            Func<TimeSpan, DelayHandlerProxy> delayHandlerFactory = t => new DelayHandlerProxy((i) => t);
            Func<InterruptableTestAction, int, ExceptionHandler, Func<int, TimeSpan>, InterruptableTestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<InterruptableTestAction, int, string, double, long, ushort, CancellationToken> invoke = (action, arg1, arg2, arg3, arg4, arg5, token) => action(arg1, arg2, arg3, arg4, arg5, token);

            Action<InterruptableTestActionProxy>?           observeAction      = a          => a.Invoking += Expect.Arguments<int, string, double, long, ushort, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestActionProxy, TimeSpan>? observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, CancellationToken>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT6_Success         (actionFactory, delayHandlerFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT6_Failure         (actionFactory, delayHandlerFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT6_EventualSuccess (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT6_EventualFailure (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT6_RetriesExhausted(actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT6_Canceled_Action (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT6_Canceled_Delay  (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void WithRetryT6_WithToken_ComplexDelayHandler()
        {
            InterruptableTestAction? nullAction = null;
            InterruptableTestAction  action     = (arg1, arg2, arg3, arg4, arg5, token) => Operation.Null();

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicy.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicy.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                     , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicy.Transient, (ComplexDelayHandler)null));

#nullable enable

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, InterruptableTestActionProxy> actionFactory = a => new InterruptableTestActionProxy((arg1, arg2, arg3, arg4, arg5, token) => a(token));
            Func<TimeSpan, ComplexDelayHandlerProxy> delayHandlerFactory = t => new ComplexDelayHandlerProxy((i, e) => t);
            Func<InterruptableTestAction, int, ExceptionHandler, Func<int, Exception, TimeSpan>, InterruptableTestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<InterruptableTestAction, int, string, double, long, ushort, CancellationToken> invoke = (action, arg1, arg2, arg3, arg4, arg5, token) => action(arg1, arg2, arg3, arg4, arg5, token);

            Action<InterruptableTestActionProxy>?           observeAction      = a          => a.Invoking += Expect.Arguments<int, string, double, long, ushort, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestActionProxy, TimeSpan>? observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, CancellationToken>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT6_Success         (actionFactory, delayHandlerFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT6_Failure         (actionFactory, delayHandlerFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT6_EventualSuccess (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT6_EventualFailure (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT6_RetriesExhausted(actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT6_Canceled_Action (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT6_Canceled_Delay  (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
        }
    }
}
