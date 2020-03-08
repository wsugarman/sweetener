// Generated from Action.Extensions.Test.Base.tt
using System;
using System.IO;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    public partial class BaseActionExtensionsTest
    {
        #region WithRetryT1_Success

        internal void WithRetryT1_Success<TAction, TDelayPolicy, TActionProxy, TDelayPolicyProxy>(
            Func<Action<CancellationToken>, TActionProxy> actionFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayHandlerFactory,
            Func<TAction, int, ExceptionHandler, TDelayPolicy, TAction> withRetry,
            Action<TAction, CancellationToken> invoke,
            Action<TActionProxy>? observeAction = null,
            Action<TDelayPolicyProxy>? observeDelayPolicy = null)
            where TAction           : Delegate
            where TDelayPolicy      : Delegate
            where TActionProxy      : DelegateProxy<TAction>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create a "successful" user-defined action
            TActionProxy action = actionFactory(t => Operation.Null());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool> exceptionHandler = FuncProxy<Exception, bool>.Unused;
            TDelayPolicyProxy delayHandler = delayHandlerFactory(TimeSpan.Zero);

            // Create the reliable action
            TAction reliableAction = withRetry(
                action.Proxy,
                Retries.Infinite,
                exceptionHandler.Invoke,
                delayHandler.Proxy);

            // Define expectations
            exceptionHandler.Invoking += Expect.Nothing<Exception>();

            observeAction     ?.Invoke(action);
            observeDelayPolicy?.Invoke(delayHandler);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                invoke(reliableAction, tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(1, action          .Calls);
            Assert.AreEqual(0, exceptionHandler.Calls);
            Assert.AreEqual(0, delayHandler    .Calls);
        }

        #endregion

        #region WithRetryT1_Failure

        internal void WithRetryT1_Failure<TAction, TDelayPolicy, TActionProxy, TDelayPolicyProxy>(
            Func<Action<CancellationToken>, TActionProxy> actionFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayHandlerFactory,
            Func<TAction, int, ExceptionHandler, TDelayPolicy, TAction> withRetry,
            Action<TAction, CancellationToken> invoke,
            Action<TActionProxy>? observeAction = null,
            Action<TDelayPolicyProxy>? observeDelayPolicy = null)
            where TAction           : Delegate
            where TDelayPolicy      : Delegate
            where TActionProxy      : DelegateProxy<TAction>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create an "unsuccessful" user-defined action
            TActionProxy action = actionFactory(t => throw new OutOfMemoryException());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Fail<OutOfMemoryException>().Invoke);
            TDelayPolicyProxy delayHandler = delayHandlerFactory(TimeSpan.Zero);

            // Create the reliable action
            TAction reliableAction = withRetry(
                action.Proxy,
                Retries.Infinite,
                exceptionHandler.Invoke,
                delayHandler.Proxy);

            // Define expectations
            exceptionHandler.Invoking += Expect.Exception(typeof(OutOfMemoryException));

            observeAction     ?.Invoke(action);
            observeDelayPolicy?.Invoke(delayHandler);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<OutOfMemoryException>(() => invoke(reliableAction, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(1, action          .Calls);
            Assert.AreEqual(1, exceptionHandler.Calls);
            Assert.AreEqual(0, delayHandler    .Calls);
        }

        #endregion

        #region WithRetryT1_EventualSuccess

        internal void WithRetryT1_EventualSuccess<TAction, TDelayPolicy, TActionProxy, TDelayPolicyProxy>(
            Func<Action<CancellationToken>, TActionProxy> actionFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayHandlerFactory,
            Func<TAction, int, ExceptionHandler, TDelayPolicy, TAction> withRetry,
            Action<TAction, CancellationToken> invoke,
            Action<TActionProxy, TimeSpan>? observeAction = null,
            Action<TDelayPolicyProxy, Type>? observeDelayPolicy = null)
            where TAction           : Delegate
            where TDelayPolicy      : Delegate
            where TActionProxy      : DelegateProxy<TAction>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create a "successful" user-defined action that completes after 1 IOException
            Action flakyAction = FlakyAction.Create<IOException>(1);
            TActionProxy action = actionFactory(t => flakyAction());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            TDelayPolicyProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Create the reliable action
            TAction reliableAction = withRetry(
                action.Proxy,
                Retries.Infinite,
                exceptionHandler.Invoke,
                delayHandler.Proxy);

            // Define expectations
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));

            observeAction     ?.Invoke(action, Constants.MinDelay);
            observeDelayPolicy?.Invoke(delayHandler, typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                invoke(reliableAction, tokenSource.Token);

            // Validate the number of calls
            Assert.AreEqual(2, action          .Calls);
            Assert.AreEqual(1, exceptionHandler.Calls);
            Assert.AreEqual(1, delayHandler    .Calls);
        }

        #endregion

        #region WithRetryT1_EventualFailure

        internal void WithRetryT1_EventualFailure<TAction, TDelayPolicy, TActionProxy, TDelayPolicyProxy>(
            Func<Action<CancellationToken>, TActionProxy> actionFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayHandlerFactory,
            Func<TAction, int, ExceptionHandler, TDelayPolicy, TAction> withRetry,
            Action<TAction, CancellationToken> invoke,
            Action<TActionProxy, TimeSpan>? observeAction = null,
            Action<TDelayPolicyProxy, Type>? observeDelayPolicy = null)
            where TAction           : Delegate
            where TDelayPolicy      : Delegate
            where TActionProxy      : DelegateProxy<TAction>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create an "unsuccessful" user-defined action that fails after 2 transient exceptions
            Action flakyAction = FlakyAction.Create<IOException, OutOfMemoryException>(2);
            TActionProxy action = actionFactory(t => flakyAction());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            TDelayPolicyProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Create the reliable action
            TAction reliableAction = withRetry(
                action.Proxy,
                Retries.Infinite,
                exceptionHandler.Invoke,
                delayHandler.Proxy);

            // Define expectations
            exceptionHandler.Invoking += Expect.Exceptions(typeof(IOException), typeof(OutOfMemoryException), 2);

            observeAction     ?.Invoke(action, Constants.MinDelay);
            observeDelayPolicy?.Invoke(delayHandler, typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<OutOfMemoryException>(() => invoke(reliableAction, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionHandler.Calls);
            Assert.AreEqual(2, delayHandler    .Calls);
        }

        #endregion

        #region WithRetryT1_RetriesExhausted

        internal void WithRetryT1_RetriesExhausted<TAction, TDelayPolicy, TActionProxy, TDelayPolicyProxy>(
            Func<Action<CancellationToken>, TActionProxy> actionFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayHandlerFactory,
            Func<TAction, int, ExceptionHandler, TDelayPolicy, TAction> withRetry,
            Action<TAction, CancellationToken> invoke,
            Action<TActionProxy, TimeSpan>? observeAction = null,
            Action<TDelayPolicyProxy, Type>? observeDelayPolicy = null)
            where TAction           : Delegate
            where TDelayPolicy      : Delegate
            where TActionProxy      : DelegateProxy<TAction>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            // Create an "unsuccessful" user-defined action that exhausts the configured number of retries
            TActionProxy action = actionFactory(t => throw new IOException());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            TDelayPolicyProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Create the reliable action
            TAction reliableAction = withRetry(
                action.Proxy,
                2,
                exceptionHandler.Invoke,
                delayHandler.Proxy);

            // Define expectations
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));

            observeAction     ?.Invoke(action, Constants.MinDelay);
            observeDelayPolicy?.Invoke(delayHandler, typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                Assert.That.ThrowsException<IOException>(() => invoke(reliableAction, tokenSource.Token));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionHandler.Calls);
            Assert.AreEqual(2, delayHandler    .Calls);
        }

        #endregion

        #region WithRetryT1_Canceled_Action

        internal void WithRetryT1_Canceled_Action<TAction, TDelayPolicy, TActionProxy, TDelayPolicyProxy>(
            Func<Action<CancellationToken>, TActionProxy> actionFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayHandlerFactory,
            Func<TAction, int, ExceptionHandler, TDelayPolicy, TAction> withRetry,
            Action<TAction, CancellationToken> invoke,
            Action<TActionProxy, TimeSpan>? observeAction = null,
            Action<TDelayPolicyProxy, Type>? observeDelayPolicy = null)
            where TAction           : Delegate
            where TDelayPolicy      : Delegate
            where TActionProxy      : DelegateProxy<TAction>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create a user-defined action that will throw an exception depending on whether it's canceled
            TActionProxy action = actionFactory(t =>
            {
                t.ThrowIfCancellationRequested();
                throw new IOException();
            });

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Transient.Invoke);
            TDelayPolicyProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Create the reliable action
            TAction reliableAction = withRetry(
                action.Proxy,
                Retries.Infinite,
                exceptionHandler.Invoke,
                delayHandler.Proxy);

            // Define expectations
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));

            observeAction     ?.Invoke(action, Constants.MinDelay);
            observeDelayPolicy?.Invoke(delayHandler, typeof(IOException));

            // Cancel the action on its 2nd attempt
            action.Invoking += c =>
            {
                if (c.Calls == 2)
                    tokenSource.Cancel();
            };

            // Invoke
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableAction, tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            Assert.AreEqual(2, action          .Calls);
            Assert.AreEqual(1, exceptionHandler.Calls);
            Assert.AreEqual(1, delayHandler    .Calls);
        }

        #endregion

        #region WithRetryT1_Canceled_Delay

        internal void WithRetryT1_Canceled_Delay<TAction, TDelayPolicy, TActionProxy, TDelayPolicyProxy>(
            Func<Action<CancellationToken>, TActionProxy> actionFactory,
            Func<TimeSpan, TDelayPolicyProxy> delayHandlerFactory,
            Func<TAction, int, ExceptionHandler, TDelayPolicy, TAction> withRetry,
            Action<TAction, CancellationToken> invoke,
            Action<TActionProxy, TimeSpan>? observeAction = null,
            Action<TDelayPolicyProxy, Type>? observeDelayPolicy = null)
            where TAction           : Delegate
            where TDelayPolicy      : Delegate
            where TActionProxy      : DelegateProxy<TAction>
            where TDelayPolicyProxy : DelegateProxy<TDelayPolicy>
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined action that continues to fail with transient exceptions until it's canceled
            TActionProxy action = actionFactory(t => throw new IOException());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Transient.Invoke);
            TDelayPolicyProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Create the reliable InterruptableAction
            TAction reliableAction = withRetry(
                action.Proxy,
                Retries.Infinite,
                exceptionHandler.Invoke,
                delayHandler.Proxy);

            // Define expectations
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));

            observeAction     ?.Invoke(action, Constants.MinDelay);
            observeDelayPolicy?.Invoke(delayHandler, typeof(IOException));

            // Cancel the delay on its 2nd invocation
            delayHandler.Invoking += c =>
            {
                if (c.Calls == 2)
                    tokenSource.Cancel();
            };

            // Begin the invocation
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableAction, tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            Assert.AreEqual(2, action          .Calls);
            Assert.AreEqual(2, exceptionHandler.Calls);
            Assert.AreEqual(2, delayHandler    .Calls);
        }

        #endregion
    }
}
