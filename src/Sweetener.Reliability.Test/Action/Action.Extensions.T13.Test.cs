// Generated from Action.Extensions.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    // Define type aliases for the various generic types used below as they can become pretty cumbersome
    using TestAction                        = Action     <int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>;
    using InterruptableTestAction           = Action     <int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken>;
    using AsyncTestAction                   = Func       <int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, Task>;
    using InterruptableAsyncTestAction      = Func       <int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken, Task>;
    using TestActionProxy                   = ActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>;
    using InterruptableTestActionProxy      = ActionProxy<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken>;
    using AsyncTestActionProxy              = FuncProxy  <int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, Task>;
    using InterruptableAsyncTestActionProxy = FuncProxy  <int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken, Task>;
    using DelayPolicyProxy                  = FuncProxy<int, TimeSpan>;
    using ComplexDelayPolicyProxy           = FuncProxy<int, Exception, TimeSpan>;

    partial class ActionExtensionsTest
    {
        [TestMethod]
        public void WithRetryT14_DelayPolicy()
        {
            TestAction nullAction = null;
            TestAction action     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, TestActionProxy> actionFactory = a => new TestActionProxy((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => a(CancellationToken.None));
            Func<TimeSpan, DelayPolicyProxy> delayPolicyFactory = t => new DelayPolicyProxy((i) => t);
            Func<TestAction, int, ExceptionPolicy, Func<int, TimeSpan>, TestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<TestAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken> invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);

            Action<TestActionProxy>           observeAction      = a          => a.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(Arguments.Validate);
            Action<TestActionProxy, TimeSpan> observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT14_Success         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT14_Failure         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT14_EventualSuccess (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT14_EventualFailure (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT14_RetriesExhausted(actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT14_Canceled_Delay  (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void WithRetryT14_ComplexDelayPolicy()
        {
            TestAction nullAction = null;
            TestAction action     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, TestActionProxy> actionFactory = a => new TestActionProxy((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => a(CancellationToken.None));
            Func<TimeSpan, ComplexDelayPolicyProxy> delayPolicyFactory = t => new ComplexDelayPolicyProxy((i, e) => t);
            Func<TestAction, int, ExceptionPolicy, Func<int, Exception, TimeSpan>, TestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<TestAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken> invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);

            Action<TestActionProxy>           observeAction      = a          => a.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(Arguments.Validate);
            Action<TestActionProxy, TimeSpan> observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT14_Success         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT14_Failure         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT14_EventualSuccess (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT14_EventualFailure (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT14_RetriesExhausted(actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT14_Canceled_Delay  (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
        }

        [TestMethod]
        public void WithRetryT14_WithToken_DelayPolicy()
        {
            InterruptableTestAction nullAction = null;
            InterruptableTestAction action     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, InterruptableTestActionProxy> actionFactory = a => new InterruptableTestActionProxy((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => a(token));
            Func<TimeSpan, DelayPolicyProxy> delayPolicyFactory = t => new DelayPolicyProxy((i) => t);
            Func<InterruptableTestAction, int, ExceptionPolicy, Func<int, TimeSpan>, InterruptableTestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<InterruptableTestAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken> invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token);

            Action<InterruptableTestActionProxy>           observeAction      = a          => a.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestActionProxy, TimeSpan> observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT14_Success         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT14_Failure         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT14_EventualSuccess (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT14_EventualFailure (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT14_RetriesExhausted(actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT14_Canceled_Action (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT14_Canceled_Delay  (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void WithRetryT14_WithToken_ComplexDelayPolicy()
        {
            InterruptableTestAction nullAction = null;
            InterruptableTestAction action     = (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => Operation.Null();
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, InterruptableTestActionProxy> actionFactory = a => new InterruptableTestActionProxy((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => a(token));
            Func<TimeSpan, ComplexDelayPolicyProxy> delayPolicyFactory = t => new ComplexDelayPolicyProxy((i, e) => t);
            Func<InterruptableTestAction, int, ExceptionPolicy, Func<int, Exception, TimeSpan>, InterruptableTestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<InterruptableTestAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken> invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token);

            Action<InterruptableTestActionProxy>           observeAction      = a          => a.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken>(Arguments.Validate);
            Action<InterruptableTestActionProxy, TimeSpan> observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT14_Success         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT14_Failure         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT14_EventualSuccess (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT14_EventualFailure (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT14_RetriesExhausted(actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT14_Canceled_Action (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT14_Canceled_Delay  (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
        }

        [TestMethod]
        public void WithRetryT14_Async_DelayPolicy()
        {
            AsyncTestAction nullAction = null;
            AsyncTestAction action     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => await Operation.NullAsync().ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, AsyncTestActionProxy> actionFactory = a => new AsyncTestActionProxy(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => { await Task.CompletedTask.ConfigureAwait(false); a(CancellationToken.None); });
            Func<TimeSpan, DelayPolicyProxy> delayPolicyFactory = t => new DelayPolicyProxy((i) => t);
            Func<AsyncTestAction, int, ExceptionPolicy, Func<int, TimeSpan>, AsyncTestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<AsyncTestAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken> invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13).Wait();

            Action<AsyncTestActionProxy>           observeAction      = a          => a.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(Arguments.Validate);
            Action<AsyncTestActionProxy, TimeSpan> observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT14_Success         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT14_Failure         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT14_EventualSuccess (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT14_EventualFailure (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT14_RetriesExhausted(actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT14_Canceled_Delay  (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void WithRetryT14_Async_ComplexDelayPolicy()
        {
            AsyncTestAction nullAction = null;
            AsyncTestAction action     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => await Operation.NullAsync().ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, AsyncTestActionProxy> actionFactory = a => new AsyncTestActionProxy(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => { await Task.CompletedTask.ConfigureAwait(false); a(CancellationToken.None); });
            Func<TimeSpan, ComplexDelayPolicyProxy> delayPolicyFactory = t => new ComplexDelayPolicyProxy((i, e) => t);
            Func<AsyncTestAction, int, ExceptionPolicy, Func<int, Exception, TimeSpan>, AsyncTestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<AsyncTestAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken> invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13).Wait();

            Action<AsyncTestActionProxy>           observeAction      = a          => a.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(Arguments.Validate);
            Action<AsyncTestActionProxy, TimeSpan> observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT14_Success         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT14_Failure         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT14_EventualSuccess (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT14_EventualFailure (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT14_RetriesExhausted(actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT14_Canceled_Delay  (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
        }

        [TestMethod]
        public void WithRetryT14_Async_WithToken_DelayPolicy()
        {
            InterruptableAsyncTestAction nullAction = null;
            InterruptableAsyncTestAction action     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => await Operation.NullAsync().ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , DelayPolicies.None));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (DelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, InterruptableAsyncTestActionProxy> actionFactory = a => new InterruptableAsyncTestActionProxy(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => { await Task.CompletedTask.ConfigureAwait(false); a(token); });
            Func<TimeSpan, DelayPolicyProxy> delayPolicyFactory = t => new DelayPolicyProxy((i) => t);
            Func<InterruptableAsyncTestAction, int, ExceptionPolicy, Func<int, TimeSpan>, InterruptableAsyncTestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<InterruptableAsyncTestAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken> invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token).Wait();

            Action<InterruptableAsyncTestActionProxy>           observeAction      = a          => a.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken>(Arguments.Validate);
            Action<InterruptableAsyncTestActionProxy, TimeSpan> observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT14_Success         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT14_Failure         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int>());
            WithRetryT14_EventualSuccess (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT14_EventualFailure (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT14_RetriesExhausted(actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT14_Canceled_Action (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
            WithRetryT14_Canceled_Delay  (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.Asc());
        }

        [TestMethod]
        public void WithRetryT14_Async_WithToken_ComplexDelayPolicy()
        {
            InterruptableAsyncTestAction nullAction = null;
            InterruptableAsyncTestAction action     = async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => await Operation.NullAsync().ConfigureAwait(false);
            Assert.ThrowsException<ArgumentNullException      >(() => nullAction.WithRetry( 4, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => action    .WithRetry(-2, ExceptionPolicies.Transient, (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, null                       , (i, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => action    .WithRetry( 4, ExceptionPolicies.Transient, (ComplexDelayPolicy)null));

            // Create the delegates necessary to test the WithRetry overload
            Func<Action<CancellationToken>, InterruptableAsyncTestActionProxy> actionFactory = a => new InterruptableAsyncTestActionProxy(async (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => { await Task.CompletedTask.ConfigureAwait(false); a(token); });
            Func<TimeSpan, ComplexDelayPolicyProxy> delayPolicyFactory = t => new ComplexDelayPolicyProxy((i, e) => t);
            Func<InterruptableAsyncTestAction, int, ExceptionPolicy, Func<int, Exception, TimeSpan>, InterruptableAsyncTestAction> withRetry = (a, r, e, d) => a.WithRetry(r, e, d.Invoke);
            Action<InterruptableAsyncTestAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken> invoke = (action, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, token).Wait();

            Action<InterruptableAsyncTestActionProxy>           observeAction      = a          => a.Invoking += Expect.Arguments<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken>(Arguments.Validate);
            Action<InterruptableAsyncTestActionProxy, TimeSpan> observeActionDelay = (a, delay) => a.Invoking += Expect.ArgumentsAfterDelay<int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken>(Arguments.Validate, delay);

            // Test each scenario
            WithRetryT14_Success         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT14_Failure         (actionFactory, delayPolicyFactory, withRetry, invoke, observeAction     ,  d     => d.Invoking += Expect.Nothing<int, Exception>());
            WithRetryT14_EventualSuccess (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT14_EventualFailure (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT14_RetriesExhausted(actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT14_Canceled_Action (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
            WithRetryT14_Canceled_Delay  (actionFactory, delayPolicyFactory, withRetry, invoke, observeActionDelay, (d, e) => d.Invoking += Expect.ExceptionAsc(e));
        }

        #region WithRetryT14_Success

        private void WithRetryT14_Success<TAction, TDelayPolicy, TActionProxy, TDelayPolicyProxy>(
            Func<Action<CancellationToken>, TActionProxy> actionFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TAction, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken> invoke,
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
                invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(1, action         .Calls);
            Assert.AreEqual(0, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT14_Failure

        private void WithRetryT14_Failure<TAction, TDelayPolicy, TActionProxy, TDelayPolicyProxy>(
            Func<Action<CancellationToken>, TActionProxy> actionFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TAction, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken> invoke,
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
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(1, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(0, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT14_EventualSuccess

        private void WithRetryT14_EventualSuccess<TAction, TDelayPolicy, TActionProxy, TDelayPolicyProxy>(
            Func<Action<CancellationToken>, TActionProxy> actionFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TAction, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken> invoke,
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
                invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(2, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(1, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT14_EventualFailure

        private void WithRetryT14_EventualFailure<TAction, TDelayPolicy, TActionProxy, TDelayPolicyProxy>(
            Func<Action<CancellationToken>, TActionProxy> actionFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TAction, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken> invoke,
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
                Assert.That.ThrowsException<InvalidOperationException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion

        #region WithRetryT14_RetriesExhausted

        private void WithRetryT14_RetriesExhausted<TAction, TDelayPolicy, TActionProxy, TDelayPolicyProxy>(
            Func<Action<CancellationToken>, TActionProxy> actionFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TAction, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken> invoke,
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
                Assert.That.ThrowsException<IOException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionPolicy .Calls);
            Assert.AreEqual(2, delayPolicy     .Calls);
        }

        #endregion

        #region WithRetryT14_Canceled_Action

        private void WithRetryT14_Canceled_Action<TAction, TDelayPolicy, TActionProxy, TDelayPolicyProxy>(
            Func<Action<CancellationToken>, TActionProxy> actionFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TAction, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken> invoke,
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
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            Assert.AreEqual(2, action         .Calls);
            Assert.AreEqual(1, exceptionPolicy.Calls);
            Assert.AreEqual(1, delayPolicy    .Calls);
        }

        #endregion

        #region WithRetryT14_Canceled_Delay

        private void WithRetryT14_Canceled_Delay<TAction, TDelayPolicy, TActionProxy, TDelayPolicyProxy>(
            Func<Action<CancellationToken>, TActionProxy> actionFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayPolicyFactory,
            Func<TAction, int, ExceptionPolicy, TDelayPolicy, TAction> withRetry,
            Action<TAction, int, string, double, long, ushort, byte, TimeSpan, uint, Tuple<bool, ulong>, DateTime, ulong, sbyte, decimal, CancellationToken> invoke,
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
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableAction, 42, "foo", 3.14D, 1000L, (ushort)1, (byte)255, TimeSpan.FromDays(30), 112U, Tuple.Create(true, 64UL), new DateTime(2019, 10, 06), 321UL, (sbyte)-7, -24.68M, tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            Assert.AreEqual(2, action         .Calls);
            Assert.AreEqual(2, exceptionPolicy.Calls);
            Assert.AreEqual(2, delayPolicy    .Calls);
        }

        #endregion
    }
}
