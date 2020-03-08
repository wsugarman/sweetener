// Generated from Action.Extensions.Test.tt
using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    // Define type aliases for the various generic types used below as they can become pretty cumbersome
    using TestAction                   = Action     ;
    using InterruptableTestAction      = Action     <CancellationToken>;
    using TestActionProxy              = ActionProxy;
    using InterruptableTestActionProxy = ActionProxy<CancellationToken>;
    using DelayHandlerProxy            = FuncProxy<int, TimeSpan>;
    using ComplexDelayHandlerProxy     = FuncProxy<int, Exception, TimeSpan>;

    [TestClass]
    public partial class ActionExtensionsTest : BaseActionExtensionsTest
    {
        [TestMethod]
        public void WithRetryT1_DelayHandler()
        {
            TestAction? nullAction = null;
            TestAction  action     = () => Operation.Null();

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicy.Transient, (DelayHandler)null));

#nullable enable

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, TestActionProxy> actionFactory = a => new TestActionProxy(() => a(CancellationToken.None));
            Func<TimeSpan, DelayHandlerProxy> delayHandlerFactory = t => new DelayHandlerProxy((i) => t);
            Func<TestAction, int, ExceptionHandler, Func<int, TimeSpan>, TestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<TestAction, CancellationToken> invoke = (action, token) => action();

            Action<TestActionProxy>?           observeAction      = null;
            Action<TestActionProxy, TimeSpan>? observeActionDelay = (a, delay) => a.Invoking += Expect.AfterDelay(delay);

            // Test each scenario
            WithRetryT1_Success         (actionFactory, delayHandlerFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT1_Failure         (actionFactory, delayHandlerFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT1_EventualSuccess (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT1_EventualFailure (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT1_RetriesExhausted(actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void WithRetryT1_ComplexDelayHandler()
        {
            TestAction? nullAction = null;
            TestAction  action     = () => Operation.Null();

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicy.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicy.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                     , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicy.Transient, (ComplexDelayHandler)null));

#nullable enable

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, TestActionProxy> actionFactory = a => new TestActionProxy(() => a(CancellationToken.None));
            Func<TimeSpan, ComplexDelayHandlerProxy> delayHandlerFactory = t => new ComplexDelayHandlerProxy((i, e) => t);
            Func<TestAction, int, ExceptionHandler, Func<int, Exception, TimeSpan>, TestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<TestAction, CancellationToken> invoke = (action, token) => action();

            Action<TestActionProxy>?           observeAction      = null;
            Action<TestActionProxy, TimeSpan>? observeActionDelay = (a, delay) => a.Invoking += Expect.AfterDelay(delay);

            // Test each scenario
            WithRetryT1_Success         (actionFactory, delayHandlerFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT1_Failure         (actionFactory, delayHandlerFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT1_EventualSuccess (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT1_EventualFailure (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT1_RetriesExhausted(actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
        }

        [TestMethod]
        public void WithRetryT1_WithToken_DelayHandler()
        {
            InterruptableTestAction? nullAction = null;
            InterruptableTestAction  action     = (token) => Operation.Null();

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicy.Transient, (DelayHandler)null));

#nullable enable

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, InterruptableTestActionProxy> actionFactory = a => new InterruptableTestActionProxy((token) => a(token));
            Func<TimeSpan, DelayHandlerProxy> delayHandlerFactory = t => new DelayHandlerProxy((i) => t);
            Func<InterruptableTestAction, int, ExceptionHandler, Func<int, TimeSpan>, InterruptableTestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<InterruptableTestAction, CancellationToken> invoke = (action, token) => action(token);

            Action<InterruptableTestActionProxy>?           observeAction      = a          => a.Invoking += Expect.Arguments<CancellationToken>(Arguments.Validate);
            Action<InterruptableTestActionProxy, TimeSpan>? observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<CancellationToken>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT1_Success         (actionFactory, delayHandlerFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT1_Failure         (actionFactory, delayHandlerFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT1_EventualSuccess (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT1_EventualFailure (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT1_RetriesExhausted(actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT1_Canceled_Action (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT1_Canceled_Delay  (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void WithRetryT1_WithToken_ComplexDelayHandler()
        {
            InterruptableTestAction? nullAction = null;
            InterruptableTestAction  action     = (token) => Operation.Null();

#nullable disable

            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicy.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicy.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                     , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicy.Transient, (ComplexDelayHandler)null));

#nullable enable

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, InterruptableTestActionProxy> actionFactory = a => new InterruptableTestActionProxy((token) => a(token));
            Func<TimeSpan, ComplexDelayHandlerProxy> delayHandlerFactory = t => new ComplexDelayHandlerProxy((i, e) => t);
            Func<InterruptableTestAction, int, ExceptionHandler, Func<int, Exception, TimeSpan>, InterruptableTestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<InterruptableTestAction, CancellationToken> invoke = (action, token) => action(token);

            Action<InterruptableTestActionProxy>?           observeAction      = a          => a.Invoking += Expect.Arguments<CancellationToken>(Arguments.Validate);
            Action<InterruptableTestActionProxy, TimeSpan>? observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<CancellationToken>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT1_Success         (actionFactory, delayHandlerFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT1_Failure         (actionFactory, delayHandlerFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT1_EventualSuccess (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT1_EventualFailure (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT1_RetriesExhausted(actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT1_Canceled_Action (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT1_Canceled_Delay  (actionFactory, delayHandlerFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
        }
    }
}
