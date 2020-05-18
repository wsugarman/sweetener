using System;
using System.IO;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    [TestClass]
    public partial class ReliablyTest
    {
        #region Invoke_Action_Success

        private void Invoke_Action_Success<TDelayHandler, TDelayHandlerProxy>(
            Action<Action, CancellationToken, int, ExceptionHandler, TDelayHandler> assertInvoke,
            Func<TimeSpan, TDelayHandlerProxy> delayHandlerFactory,
            Action<TDelayHandlerProxy> addDelayObservation)
            where TDelayHandler      : Delegate
            where TDelayHandlerProxy : DelegateProxy<TDelayHandler>
        {
            // Create a "successful" user-defined action
            ActionProxy action = new ActionProxy(Operation.Null);

            // Declare the various proxies for the input delegates
            FuncProxy<Exception, bool> exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Fatal.Invoke);
            TDelayHandlerProxy delayHandler = delayHandlerFactory(TimeSpan.Zero);

            // Define expectations
            exceptionHandler.Invoking += Expect.Nothing<Exception>();
            addDelayObservation(delayHandler);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(action.Invoke, tokenSource.Token, 42, exceptionHandler.Invoke, delayHandler.Proxy);

            // Validate the number of calls
            Assert.AreEqual(1, action          .Calls);
            Assert.AreEqual(0, exceptionHandler.Calls);
            Assert.AreEqual(0, delayHandler    .Calls);
        }

        #endregion

        #region Invoke_Action_Failure

        private void Invoke_Action_Failure<TDelayHandler, TDelayHandlerProxy>(
            Action<Action, CancellationToken, int, ExceptionHandler, TDelayHandler, Type> assertInvoke,
            Func<TimeSpan, TDelayHandlerProxy> delayHandlerFactory,
            Action<TDelayHandlerProxy> addDelayObservation)
            where TDelayHandler      : Delegate
            where TDelayHandlerProxy : DelegateProxy<TDelayHandler>
        {
            // Create an "unsuccessful" user-defined action
            ActionProxy action = new ActionProxy(() => throw new OutOfMemoryException());

            // Declare the various proxies for the input delegates
            FuncProxy<Exception, bool> exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Fail<OutOfMemoryException>().Invoke);
            TDelayHandlerProxy delayHandler = delayHandlerFactory(TimeSpan.Zero);

            // Define expectations
            exceptionHandler.Invoking += Expect.Exception(typeof(OutOfMemoryException));
            addDelayObservation(delayHandler);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(action.Invoke, tokenSource.Token, 42, exceptionHandler.Invoke, delayHandler.Proxy, typeof(OutOfMemoryException));

            // Validate the number of calls
            Assert.AreEqual(1, action          .Calls);
            Assert.AreEqual(1, exceptionHandler.Calls);
            Assert.AreEqual(0, delayHandler    .Calls);
        }

        #endregion

        #region Invoke_Action_EventualSuccess

        private void Invoke_Action_EventualSuccess<TDelayHandler, TDelayHandlerProxy>(
            Action<Action, CancellationToken, int, ExceptionHandler, TDelayHandler> assertInvoke,
            Func<TimeSpan, TDelayHandlerProxy> delayHandlerFactory,
            Action<TDelayHandlerProxy, Type> addDelayObservation)
            where TDelayHandler      : Delegate
            where TDelayHandlerProxy : DelegateProxy<TDelayHandler>
        {
            // Create a "successful" user-defined action that completes after 1 IOException
            ActionProxy action = new ActionProxy(FlakyAction.Create<IOException>(1));

            // Declare the various proxies for the input delegates
            FuncProxy<Exception, bool> exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            TDelayHandlerProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Define expectations
            action          .Invoking += Expect.AfterDelay(Constants.MinDelay);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            addDelayObservation(delayHandler, typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(action.Invoke, tokenSource.Token, 42, exceptionHandler.Invoke, delayHandler.Proxy);

            // Validate the number of calls
            Assert.AreEqual(2, action          .Calls);
            Assert.AreEqual(1, exceptionHandler.Calls);
            Assert.AreEqual(1, delayHandler    .Calls);
        }

        #endregion

        #region Invoke_Action_EventualFailure

        private void Invoke_Action_EventualFailure<TDelayHandler, TDelayHandlerProxy>(
            Action<Action, CancellationToken, int, ExceptionHandler, TDelayHandler, Type> assertInvoke,
            Func<TimeSpan, TDelayHandlerProxy> delayHandlerFactory,
            Action<TDelayHandlerProxy, Type> addDelayObservation)
            where TDelayHandler      : Delegate
            where TDelayHandlerProxy : DelegateProxy<TDelayHandler>
        {
            // Create an "unsuccessful" user-defined action that fails after 2 transient exceptions
            ActionProxy action = new ActionProxy(FlakyAction.Create<IOException, OutOfMemoryException>(2));

            // Declare the various proxies for the input delegates
            FuncProxy<Exception, bool> exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            TDelayHandlerProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Define expectations
            action          .Invoking += Expect.AfterDelay(Constants.MinDelay);
            exceptionHandler.Invoking += Expect.Exceptions(typeof(IOException), typeof(OutOfMemoryException), 2);
            addDelayObservation(delayHandler, typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(action.Invoke, tokenSource.Token, 42, exceptionHandler.Invoke, delayHandler.Proxy, typeof(OutOfMemoryException));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionHandler.Calls);
            Assert.AreEqual(2, delayHandler    .Calls);
        }

        #endregion

        #region Invoke_Action_RetriesExhausted

        private void Invoke_Action_RetriesExhausted<TDelayHandler, TDelayHandlerProxy>(
            Action<Action, CancellationToken, int, ExceptionHandler, TDelayHandler, Type> assertInvoke,
            Func<TimeSpan, TDelayHandlerProxy> delayHandlerFactory,
            Action<TDelayHandlerProxy, Type> addDelayObservation)
            where TDelayHandler      : Delegate
            where TDelayHandlerProxy : DelegateProxy<TDelayHandler>
        {
            // Create an "unsuccessful" user-defined action that exhausts the configured number of retries
            ActionProxy action = new ActionProxy(() => throw new IOException());

            // Declare the various proxies for the input delegates
            FuncProxy<Exception, bool> exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            TDelayHandlerProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Define expectations
            action          .Invoking += Expect.AfterDelay(Constants.MinDelay);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            addDelayObservation(delayHandler, typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(action.Invoke, tokenSource.Token, 2, exceptionHandler.Invoke, delayHandler.Proxy, typeof(IOException));

            // Validate the number of calls
            Assert.AreEqual(3, action          .Calls);
            Assert.AreEqual(3, exceptionHandler.Calls);
            Assert.AreEqual(2, delayHandler    .Calls);
        }

        #endregion

        #region Invoke_Action_Canceled_Delegate

        private void Invoke_Action_Canceled_Delegate<TDelayHandler, TDelayHandlerProxy>(
            Action<Action, CancellationToken, int, ExceptionHandler, TDelayHandler> invoke,
            Func<TimeSpan, TDelayHandlerProxy> delayHandlerFactory,
            Action<TDelayHandlerProxy, Type> addDelayObservation)
            where TDelayHandler      : Delegate
            where TDelayHandlerProxy : DelegateProxy<TDelayHandler>
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create a user-defined action that will throw an exception depending on whether its canceled
            ActionProxy action = new ActionProxy(
                () =>
                {
                    tokenSource.Token.ThrowIfCancellationRequested();
                    throw new IOException();
                });

            // Declare the various proxies for the input delegates
            FuncProxy<Exception, bool>          exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Transient.Invoke);
            TDelayHandlerProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Define expectations
            action          .Invoking += Expect.AfterDelay(Constants.MinDelay);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            addDelayObservation(delayHandler, typeof(IOException));

            // Cancel the action on its 2nd attempt
            action.Invoking += (c) =>
            {
                if (c.Calls == 2)
                    tokenSource.Cancel();
            };

            // Invoke, retry, and cancel
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(action.Invoke, tokenSource.Token, Retries.Infinite, exceptionHandler.Invoke, delayHandler.Proxy), allowedDerivedTypes: true);

            // Validate the number of calls
            Assert.AreEqual(2, action          .Calls);
            Assert.AreEqual(1, exceptionHandler.Calls);
            Assert.AreEqual(1, delayHandler    .Calls);
        }

        #endregion

        #region Invoke_Action_Canceled_Delay

        private void Invoke_Action_Canceled_Delay<TDelayHandler, TDelayHandlerProxy>(
            Action<Action, CancellationToken, int, ExceptionHandler, TDelayHandler> invoke,
            Func<TimeSpan, TDelayHandlerProxy> delayHandlerFactory,
            Action<TDelayHandlerProxy, Type> addDelayObservation)
            where TDelayHandler      : Delegate
            where TDelayHandlerProxy : DelegateProxy<TDelayHandler>
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined action that continues to fail with transient exceptions until it's canceled
            ActionProxy action = new ActionProxy(() => throw new IOException());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool>          exceptionHandler  = new FuncProxy<Exception, bool>(ExceptionPolicy.Transient.Invoke);
            TDelayHandlerProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Define expectations
            action          .Invoking += Expect.AfterDelay(Constants.MinDelay);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            addDelayObservation(delayHandler, typeof(IOException));

            // Cancel the delay on its 2nd invocation
            delayHandler.Invoking += (c) =>
            {
                if (c.Calls == 2)
                    tokenSource.Cancel();
            };

            // Invoke, retry, and cancel
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(action.Invoke, tokenSource.Token, Retries.Infinite, exceptionHandler.Invoke, delayHandler.Proxy), allowedDerivedTypes: true);

            // Validate the number of calls
            Assert.AreEqual(2, action          .Calls);
            Assert.AreEqual(2, exceptionHandler.Calls);
            Assert.AreEqual(2, delayHandler    .Calls);
        }

        #endregion
    }
}
