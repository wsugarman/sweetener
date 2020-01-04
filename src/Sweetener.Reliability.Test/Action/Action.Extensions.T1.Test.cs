// Generated from Action.Extensions.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    // Define type aliases for the various generic types used below as they can become pretty cumbersome
    using TestAction                        = Action     <int>;
    using InterruptableTestAction           = Action     <int, CancellationToken>;
    using AsyncTestAction                   = Func       <int, Task>;
    using InterruptableAsyncTestAction      = Func       <int, CancellationToken, Task>;
    using TestActionProxy                   = ActionProxy<int>;
    using InterruptableTestActionProxy      = ActionProxy<int, CancellationToken>;
    using AsyncTestActionProxy              = FuncProxy  <int, Task>;
    using InterruptableAsyncTestActionProxy = FuncProxy  <int, CancellationToken, Task>;
    using DelayPolicyProxy                  = FuncProxy<int, TimeSpan>;
    using ComplexDelayPolicyProxy           = FuncProxy<int, Exception, TimeSpan>;

    partial class ActionExtensionsTest
    {
        [TestMethod]
        public void WithRetryT2_DelayPolicy()
        {
            TestAction nullAction = null;
            TestAction action     = (arg) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, TestActionProxy> actionFactory = a => new TestActionProxy((arg) => a(CancellationToken.None));
            Func<TimeSpan, DelayPolicyProxy> delayPolicyFactory = t => new DelayPolicyProxy((i) => t);
            Func<TestAction, int, ExceptionPolicy, Func<int, TimeSpan>, TestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<TestAction, int, CancellationToken> invoke = (action, arg, token) => action(arg);

            Action<TestActionProxy>           observeAction      = a          => a.Invoking += Expect.Arguments<int>(Arguments.Validate);
            Action<TestActionProxy, TimeSpan> observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT2_Success         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT2_Failure         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT2_EventualSuccess (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT2_EventualFailure (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT2_RetriesExhausted(actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT2_Canceled_Delay  (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void WithRetryT2_ComplexDelayPolicy()
        {
            TestAction nullAction = null;
            TestAction action     = (arg) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, TestActionProxy> actionFactory = a => new TestActionProxy((arg) => a(CancellationToken.None));
            Func<TimeSpan, ComplexDelayPolicyProxy> delayPolicyFactory = t => new ComplexDelayPolicyProxy((i, e) => t);
            Func<TestAction, int, ExceptionPolicy, Func<int, Exception, TimeSpan>, TestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<TestAction, int, CancellationToken> invoke = (action, arg, token) => action(arg);

            Action<TestActionProxy>           observeAction      = a          => a.Invoking += Expect.Arguments<int>(Arguments.Validate);
            Action<TestActionProxy, TimeSpan> observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT2_Success         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT2_Failure         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT2_EventualSuccess (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT2_EventualFailure (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT2_RetriesExhausted(actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT2_Canceled_Delay  (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
        }

        [TestMethod]
        public void WithRetryT2_WithToken_DelayPolicy()
        {
            InterruptableTestAction nullAction = null;
            InterruptableTestAction action     = (arg, token) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, InterruptableTestActionProxy> actionFactory = a => new InterruptableTestActionProxy((arg, token) => a(token));
            Func<TimeSpan, DelayPolicyProxy> delayPolicyFactory = t => new DelayPolicyProxy((i) => t);
            Func<InterruptableTestAction, int, ExceptionPolicy, Func<int, TimeSpan>, InterruptableTestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<InterruptableTestAction, int, CancellationToken> invoke = (action, arg, token) => action(arg, token);

            Action<InterruptableTestActionProxy>           observeAction      = a          => a.Invoking += Expect.Arguments<int, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestActionProxy, TimeSpan> observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int, CancellationToken>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT2_Success         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT2_Failure         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT2_EventualSuccess (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT2_EventualFailure (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT2_RetriesExhausted(actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT2_Canceled_Action (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT2_Canceled_Delay  (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void WithRetryT2_WithToken_ComplexDelayPolicy()
        {
            InterruptableTestAction nullAction = null;
            InterruptableTestAction action     = (arg, token) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, InterruptableTestActionProxy> actionFactory = a => new InterruptableTestActionProxy((arg, token) => a(token));
            Func<TimeSpan, ComplexDelayPolicyProxy> delayPolicyFactory = t => new ComplexDelayPolicyProxy((i, e) => t);
            Func<InterruptableTestAction, int, ExceptionPolicy, Func<int, Exception, TimeSpan>, InterruptableTestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<InterruptableTestAction, int, CancellationToken> invoke = (action, arg, token) => action(arg, token);

            Action<InterruptableTestActionProxy>           observeAction      = a          => a.Invoking += Expect.Arguments<int, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestActionProxy, TimeSpan> observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int, CancellationToken>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT2_Success         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT2_Failure         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT2_EventualSuccess (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT2_EventualFailure (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT2_RetriesExhausted(actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT2_Canceled_Action (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT2_Canceled_Delay  (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
        }

        [TestMethod]
        public void WithRetryT2_Async_DelayPolicy()
        {
            AsyncTestAction nullAction = null;
            AsyncTestAction action     = async (arg) => await Operation.NullAsync().ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, AsyncTestActionProxy> actionFactory = a => new AsyncTestActionProxy(async (arg) => { await Task.CompletedTask.ConfigureAwait(false); a(CancellationToken.None); });
            Func<TimeSpan, DelayPolicyProxy> delayPolicyFactory = t => new DelayPolicyProxy((i) => t);
            Func<AsyncTestAction, int, ExceptionPolicy, Func<int, TimeSpan>, AsyncTestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<AsyncTestAction, int, CancellationToken> invoke = (action, arg, token) => action(arg).Wait();

            Action<AsyncTestActionProxy>           observeAction      = a          => a.Invoking += Expect.Arguments<int>(Arguments.Validate);
            Action<AsyncTestActionProxy, TimeSpan> observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT2_Success         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT2_Failure         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT2_EventualSuccess (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT2_EventualFailure (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT2_RetriesExhausted(actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT2_Canceled_Delay  (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void WithRetryT2_Async_ComplexDelayPolicy()
        {
            AsyncTestAction nullAction = null;
            AsyncTestAction action     = async (arg) => await Operation.NullAsync().ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, AsyncTestActionProxy> actionFactory = a => new AsyncTestActionProxy(async (arg) => { await Task.CompletedTask.ConfigureAwait(false); a(CancellationToken.None); });
            Func<TimeSpan, ComplexDelayPolicyProxy> delayPolicyFactory = t => new ComplexDelayPolicyProxy((i, e) => t);
            Func<AsyncTestAction, int, ExceptionPolicy, Func<int, Exception, TimeSpan>, AsyncTestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<AsyncTestAction, int, CancellationToken> invoke = (action, arg, token) => action(arg).Wait();

            Action<AsyncTestActionProxy>           observeAction      = a          => a.Invoking += Expect.Arguments<int>(Arguments.Validate);
            Action<AsyncTestActionProxy, TimeSpan> observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT2_Success         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT2_Failure         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT2_EventualSuccess (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT2_EventualFailure (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT2_RetriesExhausted(actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT2_Canceled_Delay  (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
        }

        [TestMethod]
        public void WithRetryT2_Async_WithToken_DelayPolicy()
        {
            InterruptableAsyncTestAction nullAction = null;
            InterruptableAsyncTestAction action     = async (arg, token) => await Operation.NullAsync().ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, InterruptableAsyncTestActionProxy> actionFactory = a => new InterruptableAsyncTestActionProxy(async (arg, token) => { await Task.CompletedTask.ConfigureAwait(false); a(token); });
            Func<TimeSpan, DelayPolicyProxy> delayPolicyFactory = t => new DelayPolicyProxy((i) => t);
            Func<InterruptableAsyncTestAction, int, ExceptionPolicy, Func<int, TimeSpan>, InterruptableAsyncTestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<InterruptableAsyncTestAction, int, CancellationToken> invoke = (action, arg, token) => action(arg, token).Wait();

            Action<InterruptableAsyncTestActionProxy>           observeAction      = a          => a.Invoking += Expect.Arguments<int, CancellationToken>(Arguments.Validate);
            Action<InterruptableAsyncTestActionProxy, TimeSpan> observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int, CancellationToken>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT2_Success         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT2_Failure         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT2_EventualSuccess (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT2_EventualFailure (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT2_RetriesExhausted(actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT2_Canceled_Action (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT2_Canceled_Delay  (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void WithRetryT2_Async_WithToken_ComplexDelayPolicy()
        {
            InterruptableAsyncTestAction nullAction = null;
            InterruptableAsyncTestAction action     = async (arg, token) => await Operation.NullAsync().ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, InterruptableAsyncTestActionProxy> actionFactory = a => new InterruptableAsyncTestActionProxy(async (arg, token) => { await Task.CompletedTask.ConfigureAwait(false); a(token); });
            Func<TimeSpan, ComplexDelayPolicyProxy> delayPolicyFactory = t => new ComplexDelayPolicyProxy((i, e) => t);
            Func<InterruptableAsyncTestAction, int, ExceptionPolicy, Func<int, Exception, TimeSpan>, InterruptableAsyncTestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<InterruptableAsyncTestAction, int, CancellationToken> invoke = (action, arg, token) => action(arg, token).Wait();

            Action<InterruptableAsyncTestActionProxy>           observeAction      = a          => a.Invoking += Expect.Arguments<int, CancellationToken>(Arguments.Validate);
            Action<InterruptableAsyncTestActionProxy, TimeSpan> observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int, CancellationToken>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT2_Success         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT2_Failure         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT2_EventualSuccess (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT2_EventualFailure (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT2_RetriesExhausted(actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT2_Canceled_Action (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT2_Canceled_Delay  (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
        }

        #region WithRetryT2_Success

        private void WithRetryT2_Success<TAction, TDelayPolicy, TActionProxy, TDelayPolicyProxy>(
            Func<Action<CancellationToken>, TActionProxy> actionFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TAction, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, CancellationToken> invoke,
            Action<TActionProxy> observeAction,
            Action<TDelayPolicyProxy> observeDelayPolicy)
            where TAction           : Delegate
            where TDelayPolicy      : Delegate
            where TActionProxy      : DelegateProxy<TAction>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create a "successful" user-defined action
            TActionProxy action = actionFactory(t => Operation.Null());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>();
            TDelayPolicyProxy delayPolicy = delayPolicyFactory(TimeSpan.Zero);

            // Create the reliable action
            TAction reliableAction = withRetry(
                action.Proxy,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy.Proxy);

            // Define expectations
            observeAction?.Invoke(action);
            exceptionPolicy.Invoking += Expect.Nothing<Exception>();
            observeDelayPolicy?.Invoke(delayPolicy);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                invoke(reliableAction, 42, tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(1, action         .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT2_Failure

        private void WithRetryT2_Failure<TAction, TDelayPolicy, TActionProxy, TDelayPolicyProxy>(
            Func<Action<CancellationToken>, TActionProxy> actionFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TAction, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, CancellationToken> invoke,
            Action<TActionProxy> observeAction,
            Action<TDelayPolicyProxy> observeDelayPolicy)
            where TAction           : Delegate
            where TDelayPolicy      : Delegate
            where TActionProxy      : DelegateProxy<TAction>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create an "unsuccessful" user-defined action
            TActionProxy action = actionFactory(t => throw new InvalidOperationException());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Fail<InvalidOperationException>().Invoke);
            TDelayPolicyProxy delayPolicy = delayPolicyFactory(TimeSpan.Zero);

            // Create the reliable action
            TAction reliableAction = withRetry(
                action.Proxy,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy.Proxy);

            // Define expectations
            observeAction?.Invoke(action);
            exceptionPolicy.Invoking += Expect.Exception(typeof(InvalidOperationException));
            observeDelayPolicy?.Invoke(delayPolicy);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableAction, 42, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(1, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT2_EventualSuccess

        private void WithRetryT2_EventualSuccess<TAction, TDelayPolicy, TActionProxy, TDelayPolicyProxy>(
            Func<Action<CancellationToken>, TActionProxy> actionFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TAction, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, CancellationToken> invoke,
            Action<TActionProxy, TimeSpan> observeAction,
            Action<TDelayPolicyProxy, Type> observeDelayPolicy)
            where TAction           : Delegate
            where TDelayPolicy      : Delegate
            where TActionProxy      : DelegateProxy<TAction>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create a "successful" user-defined action that completes after 1 IOException
            Action flakyAction = FlakyAction.Create<IOException>(1);
            TActionProxy action = actionFactory(t => flakyAction());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            TDelayPolicyProxy delayPolicy = delayPolicyFactory(Constants.Delay);

            // Create the reliable action
            TAction reliableAction = withRetry(
                action.Proxy,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy.Proxy);

            // Define expectations
            observeAction?.Invoke(action, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));
            observeDelayPolicy?.Invoke(delayPolicy, typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                invoke(reliableAction, 42, tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(2, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(1, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT2_EventualFailure

        private void WithRetryT2_EventualFailure<TAction, TDelayPolicy, TActionProxy, TDelayPolicyProxy>(
            Func<Action<CancellationToken>, TActionProxy> actionFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TAction, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, CancellationToken> invoke,
            Action<TActionProxy, TimeSpan> observeAction,
            Action<TDelayPolicyProxy, Type> observeDelayPolicy)
            where TAction           : Delegate
            where TDelayPolicy      : Delegate
            where TActionProxy      : DelegateProxy<TAction>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create an "unsuccessful" user-defined action that fails after 2 transient exceptions
            Action flakyAction = FlakyAction.Create<IOException, InvalidOperationException>(2);
            TActionProxy action = actionFactory(t => flakyAction());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            TDelayPolicyProxy delayPolicy = delayPolicyFactory(Constants.Delay);

            // Create the reliable action
            TAction reliableAction = withRetry(
                action.Proxy,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy.Proxy);

            // Define expectations
            observeAction?.Invoke(action, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exceptions(typeof(IOException), typeof(InvalidOperationException), 2);
            observeDelayPolicy?.Invoke(delayPolicy, typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableAction, 42, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion

        #region WithRetryT2_RetriesExhausted

        private void WithRetryT2_RetriesExhausted<TAction, TDelayPolicy, TActionProxy, TDelayPolicyProxy>(
            Func<Action<CancellationToken>, TActionProxy> actionFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TAction, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, CancellationToken> invoke,
            Action<TActionProxy, TimeSpan> observeAction,
            Action<TDelayPolicyProxy, Type> observeDelayPolicy)
            where TAction           : Delegate
            where TDelayPolicy      : Delegate
            where TActionProxy      : DelegateProxy<TAction>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create an "unsuccessful" user-defined action that exhausts the configured number of retries
            TActionProxy action = actionFactory(t => throw new IOException());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            TDelayPolicyProxy delayPolicy = delayPolicyFactory(Constants.Delay);

            // Create the reliable action
            TAction reliableAction = withRetry(
                action.Proxy,
                2,
                exceptionPolicy.Invoke,
                delayPolicy.Proxy);

            // Define expectations
            observeAction?.Invoke(action, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));
            observeDelayPolicy?.Invoke(delayPolicy, typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<IOException>(() => invoke(reliableAction, 42, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion

        #region WithRetryT2_Canceled_Action

        private void WithRetryT2_Canceled_Action<TAction, TDelayPolicy, TActionProxy, TDelayPolicyProxy>(
            Func<Action<CancellationToken>, TActionProxy> actionFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TAction, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, CancellationToken> invoke,
            Action<TActionProxy, TimeSpan> observeAction,
            Action<TDelayPolicyProxy, Type> observeDelayPolicy)
            where TAction           : Delegate
            where TDelayPolicy      : Delegate
            where TActionProxy      : DelegateProxy<TAction>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create a user-defined action that will throw an exception depending on whether its canceled
            TActionProxy action = actionFactory(t =>
            {
                t.ThrowIfCancellationRequested();
                throw new IOException();
            });

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            TDelayPolicyProxy delayPolicy = delayPolicyFactory(Constants.Delay);

            // Create the reliable action
            TAction reliableAction = withRetry(
                action.Proxy,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy.Proxy);

            // Define expectations
            observeAction?.Invoke(action, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));
            observeDelayPolicy?.Invoke(delayPolicy, typeof(IOException));

            // Cancel the action on its 2nd attempt
            action.Invoking += c =>
            {
                if (c.Calls == 2)
                    tokenSource.Cancel();
            };

            // Invoke
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableAction, 42, tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            Assert.AreEqual(2, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(1, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT2_Canceled_Delay

        private void WithRetryT2_Canceled_Delay<TAction, TDelayPolicy, TActionProxy, TDelayPolicyProxy>(
            Func<Action<CancellationToken>, TActionProxy> actionFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TAction, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, CancellationToken> invoke,
            Action<TActionProxy, TimeSpan> observeAction,
            Action<TDelayPolicyProxy, Type> observeDelayPolicy)
            where TAction           : Delegate
            where TDelayPolicy      : Delegate
            where TActionProxy      : DelegateProxy<TAction>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined action that continues to fail with transient exceptions until it's canceled
            TActionProxy action = actionFactory(t => throw new IOException());

            // Declare the various policy and event handler proxies
            FuncProxy<Exception, bool> exceptionPolicy = new FuncProxy<Exception, bool>(ExceptionPolicies.Retry<IOException>().Invoke);
            TDelayPolicyProxy delayPolicy = delayPolicyFactory(Constants.Delay);

            // Create the reliable InterruptableAction
            TAction reliableAction = withRetry(
                action.Proxy,
                Retries.Infinite,
                exceptionPolicy.Invoke,
                delayPolicy.Proxy);

            // Define expectations
            observeAction?.Invoke(action, Constants.MinDelay);
            exceptionPolicy.Invoking += Expect.Exception(typeof(IOException));
            observeDelayPolicy?.Invoke(delayPolicy, typeof(IOException));

            // Cancel the delay on its 2nd invocation
            delayPolicy.Invoking += c =>
            {
                if (c.Calls == 2)
                    tokenSource.Cancel();
            };

            // Begin the invocation
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableAction, 42, tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            Assert.AreEqual(2, action         .Calls);
            Assert.AreEqual(2, exceptionPolicy.Calls);
            Assert.AreEqual(2, delayPolicy    .Calls);
        }

        #endregion
    }
}
