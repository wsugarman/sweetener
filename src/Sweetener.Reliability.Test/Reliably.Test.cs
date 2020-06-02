using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
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
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Fatal.Invoke);
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
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Fail<OutOfMemoryException>().Invoke);
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
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
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
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
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
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
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

        #region Invoke_Action_Canceled

        private void Invoke_Action_Canceled<TDelayHandler, TDelayHandlerProxy>(
            Action<Action, CancellationToken, int, ExceptionHandler, TDelayHandler, Type> assertInvoke,
            Func<TimeSpan, TDelayHandlerProxy> delayHandlerFactory,
            Action<TDelayHandlerProxy> addDelayObservation)
            where TDelayHandler      : Delegate
            where TDelayHandlerProxy : DelegateProxy<TDelayHandler>
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            tokenSource.Cancel();

            // Create a user-defined action that will throw an OperationCanceledException
            ActionProxy action = new ActionProxy(() => tokenSource.Token.ThrowIfCancellationRequested());

            // Declare the various proxies for the input delegates
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Transient.Invoke);
            TDelayHandlerProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Define expectations
            action          .Invoking += Expect.Nothing();
            exceptionHandler.Invoking += Expect.Nothing<Exception>();
            addDelayObservation(delayHandler);

            // Invoke, retry, and cancel
            assertInvoke(action.Invoke, tokenSource.Token, Retries.Infinite, exceptionHandler.Invoke, delayHandler.Proxy, typeof(OperationCanceledException));

            // Validate the number of calls
            Assert.AreEqual(0, action          .Calls);
            Assert.AreEqual(0, exceptionHandler.Calls);
            Assert.AreEqual(0, delayHandler    .Calls);
        }

        #endregion

        #region Invoke_Action_Canceled_NoTask

        private void Invoke_Action_Canceled_NoTask<TDelayHandler, TDelayHandlerProxy>(
            Action<Action, CancellationToken, int, ExceptionHandler, TDelayHandler, Type> assertInvoke,
            Func<TimeSpan, TDelayHandlerProxy> delayHandlerFactory,
            Action<TDelayHandlerProxy> addDelayObservation)
            where TDelayHandler      : Delegate
            where TDelayHandlerProxy : DelegateProxy<TDelayHandler>
        {
            // Create a user-defined action that will immediately throw an OperationCanceledException
            ActionProxy action = new ActionProxy(() => throw new OperationCanceledException());

            // Declare the various proxies for the input delegates
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Transient.Invoke);
            TDelayHandlerProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Define expectations
            exceptionHandler.Invoking += Expect.Nothing<Exception>();
            addDelayObservation(delayHandler);

            // Invoke, retry, and cancel
            assertInvoke(action.Invoke, CancellationToken.None, Retries.Infinite, exceptionHandler.Invoke, delayHandler.Proxy, typeof(OperationCanceledException));

            // Validate the number of calls
            Assert.AreEqual(1, action          .Calls);
            Assert.AreEqual(0, exceptionHandler.Calls);
            Assert.AreEqual(0, delayHandler    .Calls);
        }

        #endregion

        #region Invoke_Action_Canceled_Delegate

        private void Invoke_Action_Canceled_Delegate<TDelayHandler, TDelayHandlerProxy>(
            Action<Action, CancellationToken, int, ExceptionHandler, TDelayHandler, Type> assertInvoke,
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
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Transient.Invoke);
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
            assertInvoke(action.Invoke, tokenSource.Token, Retries.Infinite, exceptionHandler.Invoke, delayHandler.Proxy, typeof(OperationCanceledException));

            // Validate the number of calls
            Assert.AreEqual(2, action          .Calls);
            Assert.AreEqual(1, exceptionHandler.Calls);
            Assert.AreEqual(1, delayHandler    .Calls);
        }

        #endregion

        #region Invoke_Action_Canceled_Delay

        private void Invoke_Action_Canceled_Delay<TDelayHandler, TDelayHandlerProxy>(
            Action<Action, CancellationToken, int, ExceptionHandler, TDelayHandler, Type> assertInvoke,
            Func<TimeSpan, TDelayHandlerProxy> delayHandlerFactory,
            Action<TDelayHandlerProxy, Type> addDelayObservation)
            where TDelayHandler      : Delegate
            where TDelayHandlerProxy : DelegateProxy<TDelayHandler>
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined action that continues to fail with transient exceptions until it's canceled
            ActionProxy action = new ActionProxy(() => throw new IOException());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Transient.Invoke);
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
            assertInvoke(action.Invoke, tokenSource.Token, Retries.Infinite, exceptionHandler.Invoke, delayHandler.Proxy, typeof(TaskCanceledException));

            // Validate the number of calls
            Assert.AreEqual(2, action          .Calls);
            Assert.AreEqual(2, exceptionHandler.Calls);
            Assert.AreEqual(2, delayHandler    .Calls);
        }

        #endregion

        #region Invoke_Func_Success

        private void Invoke_Func_Success<TDelayHandler, TDelayHandlerProxy>(
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, TDelayHandler, string> assertInvoke,
            Func<TimeSpan, TDelayHandlerProxy> delayHandlerFactory,
            Action<TDelayHandlerProxy> addDelayObservation,
            bool passResultHandler)
            where TDelayHandler      : Delegate
            where TDelayHandlerProxy : DelegateProxy<TDelayHandler>
        {
            // Create a "successful" user-defined function
            FuncProxy<string> func = new FuncProxy<string>(() => "Success");

            // Declare the various proxies for the input delegates
            FuncProxy<string, ResultKind> resultHandler    = new FuncProxy<string, ResultKind>(r => r == "Success" ? ResultKind.Successful : ResultKind.Fatal);
            FuncProxy<Exception, bool>    exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Fatal.Invoke);
            TDelayHandlerProxy delayHandler = delayHandlerFactory(TimeSpan.Zero);

            // Define expectations
            resultHandler   .Invoking += Expect.Result("Success");
            exceptionHandler.Invoking += Expect.Nothing<Exception>();
            addDelayObservation(delayHandler);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(func.Invoke, tokenSource.Token, 42, resultHandler.Invoke, exceptionHandler.Invoke, delayHandler.Proxy, "Success");

            // Validate the number of calls
            int x = passResultHandler ? 1 : 0;
            Assert.AreEqual(1, func            .Calls);
            Assert.AreEqual(x, resultHandler   .Calls);
            Assert.AreEqual(0, exceptionHandler.Calls);
            Assert.AreEqual(0, delayHandler    .Calls);
        }

        #endregion

        #region Invoke_Func_Failure_Result

        private void Invoke_Func_Failure_Result<TDelayHandler, TDelayHandlerProxy>(
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, TDelayHandler, string> assertInvoke,
            Func<TimeSpan, TDelayHandlerProxy> delayHandlerFactory,
            Action<TDelayHandlerProxy> addDelayObservation)
            where TDelayHandler      : Delegate
            where TDelayHandlerProxy : DelegateProxy<TDelayHandler>
        {
            // Create an "unsuccessful" user-defined function that returns a fatal result
            FuncProxy<string> func = new FuncProxy<string>(() => "Failure");

            // Declare the various proxies for the input delegates
            FuncProxy<string, ResultKind> resultHandler    = new FuncProxy<string, ResultKind>(r => r == "Failure" ? ResultKind.Fatal : ResultKind.Successful);
            FuncProxy<Exception, bool>    exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Fatal.Invoke);
            TDelayHandlerProxy delayHandler = delayHandlerFactory(TimeSpan.Zero);

            // Define expectations
            resultHandler   .Invoking += Expect.Result("Failure");
            exceptionHandler.Invoking += Expect.Nothing<Exception>();
            addDelayObservation(delayHandler);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(func.Invoke, tokenSource.Token, 42, resultHandler.Invoke, exceptionHandler.Invoke, delayHandler.Proxy, "Failure");

            // Validate the number of calls
            Assert.AreEqual(1, func            .Calls);
            Assert.AreEqual(1, resultHandler   .Calls);
            Assert.AreEqual(0, exceptionHandler.Calls);
            Assert.AreEqual(0, delayHandler    .Calls);
        }

        #endregion

        #region Invoke_Func_Failure_Exception

        private void Invoke_Func_Failure_Exception<TDelayHandler, TDelayHandlerProxy>(
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, TDelayHandler, Type> assertInvoke,
            Func<TimeSpan, TDelayHandlerProxy> delayHandlerFactory,
            Action<TDelayHandlerProxy> addDelayObservation)
            where TDelayHandler      : Delegate
            where TDelayHandlerProxy : DelegateProxy<TDelayHandler>
        {
            // Create an "unsuccessful" user-defined function that throws a fatal exception
            FuncProxy<string> func = new FuncProxy<string>(() => throw new OutOfMemoryException());

            // Declare the various proxies for the input delegates
            FuncProxy<string, ResultKind> resultHandler    = new FuncProxy<string, ResultKind>(r => ResultKind.Successful);
            FuncProxy<Exception, bool>    exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Fail<OutOfMemoryException>().Invoke);
            TDelayHandlerProxy delayHandler = delayHandlerFactory(TimeSpan.Zero);

            // Define expectations
            resultHandler   .Invoking += Expect.Nothing<string>();
            exceptionHandler.Invoking += Expect.Exception(typeof(OutOfMemoryException));
            addDelayObservation(delayHandler);

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(func.Invoke, tokenSource.Token, 42, resultHandler.Invoke, exceptionHandler.Invoke, delayHandler.Proxy, typeof(OutOfMemoryException));

            // Validate the number of calls
            Assert.AreEqual(1, func            .Calls);
            Assert.AreEqual(0, resultHandler   .Calls);
            Assert.AreEqual(1, exceptionHandler.Calls);
            Assert.AreEqual(0, delayHandler    .Calls);
        }

        #endregion

        #region Invoke_Func_EventualSuccess

        private void Invoke_Func_EventualSuccess<TDelayHandler, TDelayHandlerProxy>(
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, TDelayHandler, string> assertInvoke,
            Func<TimeSpan, TDelayHandlerProxy> delayHandlerFactory,
            Action<TDelayHandlerProxy, string, Type> addDelayObservation,
            bool passResultHandler)
            where TDelayHandler      : Delegate
            where TDelayHandlerProxy : DelegateProxy<TDelayHandler>
        {
            // Create a "successful" user-defined function that completes after either
            // (1) 2 IOExceptions OR
            // (2) an IOException and a transient result
            Func<string> flakyFunc = passResultHandler
                ? FlakyFunc.Create<string, IOException>("Try Again", "Success", 2)
                : FlakyFunc.Create<string, IOException>(             "Success", 2);
            FuncProxy<string> func = new FuncProxy<string>(flakyFunc);

            // Declare the various proxies for the input delegates
            FuncProxy<string, ResultKind> resultHandler = new FuncProxy<string, ResultKind>(r =>
                r switch
                {
                    "Try Again" => ResultKind.Transient,
                    "Success"   => ResultKind.Successful,
                    _           => ResultKind.Fatal,
                });
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            TDelayHandlerProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Define expectations
            func            .Invoking += Expect.AfterDelay(Constants.MinDelay);
            resultHandler   .Invoking += Expect.Results("Try Again", "Success", 1);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            addDelayObservation(delayHandler, "Try Again", typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(func.Invoke, tokenSource.Token, 42, resultHandler.Invoke, exceptionHandler.Invoke, delayHandler.Proxy, "Success");

            // Validate the number of calls
            int x = passResultHandler ? 2 : 0;
            int y = passResultHandler ? 1 : 2;
            Assert.AreEqual(3, func            .Calls);
            Assert.AreEqual(x, resultHandler   .Calls);
            Assert.AreEqual(y, exceptionHandler.Calls);
            Assert.AreEqual(2, delayHandler    .Calls);
        }

        #endregion

        #region Invoke_Func_EventualFailure_Result

        private void Invoke_Func_EventualFailure_Result<TDelayHandler, TDelayHandlerProxy>(
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, TDelayHandler, string> assertInvoke,
            Func<TimeSpan, TDelayHandlerProxy> delayHandlerFactory,
            Action<TDelayHandlerProxy, string, Type> addDelayObservation)
            where TDelayHandler      : Delegate
            where TDelayHandlerProxy : DelegateProxy<TDelayHandler>
        {
            // Create an "unsuccessful" user-defined function that completes after a transient result and exception
            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>("Try Again", "Failure", 2);
            FuncProxy<string> func = new FuncProxy<string>(flakyFunc);

            // Declare the various proxies for the input delegates
            FuncProxy<string, ResultKind> resultHandler = new FuncProxy<string, ResultKind>(r =>
                r switch
                {
                    "Try Again" => ResultKind.Transient,
                    "Failure"   => ResultKind.Fatal,
                    _           => ResultKind.Successful,
                });
            FuncProxy<Exception, bool> exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            TDelayHandlerProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Define expectations
            func            .Invoking += Expect.AfterDelay(Constants.MinDelay);
            resultHandler   .Invoking += Expect.Results("Try Again", "Failure", 1);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            addDelayObservation(delayHandler, "Try Again", typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(func.Invoke, tokenSource.Token, 42, resultHandler.Invoke, exceptionHandler.Invoke, delayHandler.Proxy, "Failure");

            // Validate the number of calls
            Assert.AreEqual(3, func            .Calls);
            Assert.AreEqual(2, resultHandler   .Calls);
            Assert.AreEqual(1, exceptionHandler.Calls);
            Assert.AreEqual(2, delayHandler    .Calls);
        }

        #endregion

        #region Invoke_Func_EventualFailure_Exception

        internal void Invoke_Func_EventualFailure_Exception<TDelayHandler, TDelayHandlerProxy>(
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, TDelayHandler, Type> assertInvoke,
            Func<TimeSpan, TDelayHandlerProxy> delayHandlerFactory,
            Action<TDelayHandlerProxy, string, Type> addDelayObservation,
            bool passResultHandler)
            where TDelayHandler      : Delegate
            where TDelayHandlerProxy : DelegateProxy<TDelayHandler>
        {
            // Create an "unsuccessful" user-defined function that completes after either
            // (1) 2 IOExceptions OR
            // (2) an IOException and a transient result
            Func<string> flakyFunc = passResultHandler
                ? FlakyFunc.Create<string, IOException, OutOfMemoryException>("Try Again", 2)
                : FlakyFunc.Create<string, IOException, OutOfMemoryException>(             2);
            FuncProxy<string> func = new FuncProxy<string>(flakyFunc);

            // Declare the various proxies for the input delegates
            FuncProxy<string, ResultKind> resultHandler    = new FuncProxy<string, ResultKind>(r => r == "Try Again" ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool>    exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            TDelayHandlerProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Define expectations
            func            .Invoking += Expect.AfterDelay(Constants.MinDelay);
            resultHandler   .Invoking += Expect.Result("Try Again");
            exceptionHandler.Invoking += Expect.Exceptions(typeof(IOException), typeof(OutOfMemoryException), passResultHandler ? 1 : 2);
            addDelayObservation(delayHandler, "Try Again", typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(func.Invoke, tokenSource.Token, 42, resultHandler.Invoke, exceptionHandler.Invoke, delayHandler.Proxy, typeof(OutOfMemoryException));

            // Validate the number of calls
            int x = passResultHandler ? 1 : 0;
            int y = passResultHandler ? 2 : 3;
            Assert.AreEqual(3, func            .Calls);
            Assert.AreEqual(x, resultHandler   .Calls);
            Assert.AreEqual(y, exceptionHandler.Calls);
            Assert.AreEqual(2, delayHandler    .Calls);
        }

        #endregion

        #region Invoke_Func_RetriesExhausted_Result

        internal void Invoke_Func_RetriesExhausted_Result<TDelayHandler, TDelayHandlerProxy>(
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, TDelayHandler, string> assertInvoke,
            Func<TimeSpan, TDelayHandlerProxy> delayHandlerFactory,
            Action<TDelayHandlerProxy, string, Type> addDelayObservation)
            where TDelayHandler      : Delegate
            where TDelayHandlerProxy : DelegateProxy<TDelayHandler>
        {
            // Create an "unsuccessful" user-defined function that eventually exhausts all of its retries
            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>("Try Again");
            FuncProxy<string> func = new FuncProxy<string>(flakyFunc);

            // Declare the various proxies for the input delegates
            FuncProxy<string, ResultKind> resultHandler    = new FuncProxy<string, ResultKind>(r => r == "Try Again" ? ResultKind.Transient : ResultKind.Fatal);
            FuncProxy<Exception, bool>    exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            TDelayHandlerProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Define expectations
            func            .Invoking += Expect.AfterDelay(Constants.MinDelay);
            resultHandler   .Invoking += Expect.Result("Try Again");
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            addDelayObservation(delayHandler, "Try Again", typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(func.Invoke, tokenSource.Token, 3, resultHandler.Invoke, exceptionHandler.Invoke, delayHandler.Proxy, "Try Again");

            // Validate the number of calls
            Assert.AreEqual(4, func            .Calls);
            Assert.AreEqual(2, resultHandler   .Calls);
            Assert.AreEqual(2, exceptionHandler.Calls);
            Assert.AreEqual(3, delayHandler    .Calls);
        }

        #endregion

        #region Invoke_Func_RetriesExhausted_Exception

        internal void Invoke_Func_RetriesExhausted_Exception<TDelayHandler, TDelayHandlerProxy>(
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, TDelayHandler, Type> assertInvoke,
            Func<TimeSpan, TDelayHandlerProxy> delayHandlerFactory,
            Action<TDelayHandlerProxy, string, Type> addDelayObservation,
            bool passResultHandler)
            where TDelayHandler      : Delegate
            where TDelayHandlerProxy : DelegateProxy<TDelayHandler>
        {
            // Create an "unsuccessful" user-defined function that eventually exhausts all of its retries
            Func<string> flakyFunc = passResultHandler
                ? FlakyFunc.Create<string, IOException>("Try Again")
                : () => throw new IOException();
            FuncProxy<string> func = new FuncProxy<string>(flakyFunc);

            // Declare the various proxies for the input delegates
            FuncProxy<string, ResultKind> resultHandler    = new FuncProxy<string, ResultKind>(r => r == "Try Again" ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool>    exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            TDelayHandlerProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Define expectations
            func            .Invoking += Expect.AfterDelay(Constants.MinDelay);
            resultHandler   .Invoking += Expect.Result("Try Again");
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            addDelayObservation(delayHandler, "Try Again", typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(func.Invoke, tokenSource.Token, 2, resultHandler.Invoke, exceptionHandler.Invoke, delayHandler.Proxy, typeof(IOException));

            // Validate the number of calls
            int x = passResultHandler ? 1 : 0;
            int y = passResultHandler ? 2 : 3;
            Assert.AreEqual(3, func            .Calls);
            Assert.AreEqual(x, resultHandler   .Calls);
            Assert.AreEqual(y, exceptionHandler.Calls);
            Assert.AreEqual(2, delayHandler    .Calls);
        }

        #endregion

        #region Invoke_Func_Canceled

        private void Invoke_Func_Canceled<TDelayHandler, TDelayHandlerProxy>(
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, TDelayHandler, Type> assertInvoke,
            Func<TimeSpan, TDelayHandlerProxy> delayHandlerFactory,
            Action<TDelayHandlerProxy> addDelayObservation)
            where TDelayHandler      : Delegate
            where TDelayHandlerProxy : DelegateProxy<TDelayHandler>
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();
            tokenSource.Cancel();

            // Create a user-defined function that will throw an OperationCanceledException
            FuncProxy<string> func = new FuncProxy<string>(() => { tokenSource.Token.ThrowIfCancellationRequested(); return "Failure"; });

            // Declare the various proxies for the input delegates
            FuncProxy<string, ResultKind> resultHandler    = new FuncProxy<string, ResultKind>(r => ResultKind.Transient);
            FuncProxy<Exception, bool>    exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Transient.Invoke);
            TDelayHandlerProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Define expectations
            func            .Invoking += Expect.Nothing();
            resultHandler   .Invoking += Expect.Nothing<string>();
            exceptionHandler.Invoking += Expect.Nothing<Exception>();
            addDelayObservation(delayHandler);

            // Invoke, retry, and cancel
            assertInvoke(func.Invoke, tokenSource.Token, Retries.Infinite, resultHandler.Invoke, exceptionHandler.Invoke, delayHandler.Proxy, typeof(OperationCanceledException));

            // Validate the number of calls
            Assert.AreEqual(0, func            .Calls);
            Assert.AreEqual(0, resultHandler   .Calls);
            Assert.AreEqual(0, exceptionHandler.Calls);
            Assert.AreEqual(0, delayHandler    .Calls);
        }

        #endregion

        #region Invoke_Func_Canceled_NoTask

        private void Invoke_Func_Canceled_NoTask<TDelayHandler, TDelayHandlerProxy>(
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, TDelayHandler, Type> assertInvoke,
            Func<TimeSpan, TDelayHandlerProxy> delayHandlerFactory,
            Action<TDelayHandlerProxy> addDelayObservation)
            where TDelayHandler      : Delegate
            where TDelayHandlerProxy : DelegateProxy<TDelayHandler>
        {
            // Create a user-defined function that will immediately throw an OperationCanceledException
            FuncProxy<string> func = new FuncProxy<string>(() => throw new OperationCanceledException());

            // Declare the various proxies for the input delegates
            FuncProxy<string, ResultKind> resultHandler    = new FuncProxy<string, ResultKind>(r => ResultKind.Transient);
            FuncProxy<Exception, bool>    exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Transient.Invoke);
            TDelayHandlerProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Define expectations
            resultHandler   .Invoking += Expect.Nothing<string>();
            exceptionHandler.Invoking += Expect.Nothing<Exception>();
            addDelayObservation(delayHandler);

            // Invoke, retry, and cancel
            assertInvoke(func.Invoke, CancellationToken.None, Retries.Infinite, resultHandler.Invoke, exceptionHandler.Invoke, delayHandler.Proxy, typeof(OperationCanceledException));

            // Validate the number of calls
            Assert.AreEqual(1, func            .Calls);
            Assert.AreEqual(0, resultHandler   .Calls);
            Assert.AreEqual(0, exceptionHandler.Calls);
            Assert.AreEqual(0, delayHandler    .Calls);
        }

        #endregion

        #region Invoke_Func_Canceled_Delegate

        internal void Invoke_Func_Canceled_Delegate<TDelayHandler, TDelayHandlerProxy>(
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, TDelayHandler, Type> assertInvoke,
            Func<TimeSpan, TDelayHandlerProxy> delayHandlerFactory,
            Action<TDelayHandlerProxy, string, Type> addDelayObservation,
            bool passResultHandler)
            where TDelayHandler      : Delegate
            where TDelayHandlerProxy : DelegateProxy<TDelayHandler>
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create a user-defined function that will throw an exception depending on whether it's canceled
            Func<string> flakyFunc = passResultHandler ? FlakyFunc.Create<string, IOException>("Try Again") : () => throw new IOException();
            FuncProxy<string> func = new FuncProxy<string>(() =>
            {
                tokenSource.Token.ThrowIfCancellationRequested();
                return flakyFunc();
            });

            // Declare the various proxies for the input delegates
            FuncProxy<string, ResultKind> resultHandler    = new FuncProxy<string, ResultKind>(r => r == "Try Again" ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool>    exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Transient.Invoke);
            TDelayHandlerProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Define expectations
            func            .Invoking += Expect.AfterDelay(Constants.MinDelay);
            resultHandler   .Invoking += Expect.Result("Try Again");
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            addDelayObservation(delayHandler, "Try Again", typeof(IOException));

            // Cancel the function after 2 or 3 invocations
            int i = passResultHandler ? 3 : 2;

            func.Invoking += c =>
            {
                if (c.Calls == i)
                    tokenSource.Cancel();
            };

            // Invoke, retry, and cancel
            assertInvoke(func.Invoke, tokenSource.Token, Retries.Infinite, resultHandler.Invoke, exceptionHandler.Invoke, delayHandler.Proxy, typeof(OperationCanceledException));

            // Validate the number of calls
            int r = i - 1;
            int x = passResultHandler ? r / 2 : 0;
            int y = passResultHandler ? r / 2 : r;
            Assert.AreEqual(i, func            .Calls);
            Assert.AreEqual(x, resultHandler   .Calls);
            Assert.AreEqual(y, exceptionHandler.Calls);
            Assert.AreEqual(r, delayHandler    .Calls);
        }

        #endregion

        #region Invoke_Func_Canceled_Delay

        internal void Invoke_Func_Canceled_Delay<TDelayHandler, TDelayHandlerProxy>(
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, TDelayHandler, Type> assertInvoke,
            Func<TimeSpan, TDelayHandlerProxy> delayHandlerFactory,
            Action<TDelayHandlerProxy, string, Type> addDelayObservation,
            bool passResultHandler)
            where TDelayHandler      : Delegate
            where TDelayHandlerProxy : DelegateProxy<TDelayHandler>
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create an "unsuccessful" user-defined function that continues to fail with transient exceptions until it's canceled
            Func<string> flakyFunc = passResultHandler ? FlakyFunc.Create<string, IOException>("Try Again") : () => throw new IOException();
            FuncProxy<string> func = new FuncProxy<string>(flakyFunc);

            // Declare the various proxies for the input delegates
            FuncProxy<string, ResultKind> resultHandler    = new FuncProxy<string, ResultKind>(r => r == "Try Again" ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool>    exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Transient.Invoke);
            TDelayHandlerProxy delayHandler = delayHandlerFactory(Constants.Delay);

            // Define expectations
            func            .Invoking += Expect.AfterDelay(Constants.MinDelay);
            resultHandler   .Invoking += Expect.Result("Try Again");
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            addDelayObservation(delayHandler, "Try Again", typeof(IOException));

            // Cancel the delay after 1 or 2 invocations
            int r = passResultHandler ? 2 : 1;

            delayHandler.Invoking += c =>
            {
                if (c.Calls == r)
                    tokenSource.Cancel();
            };

            // Invoke, retry, and cancel
            assertInvoke(func.Invoke, tokenSource.Token, Retries.Infinite, resultHandler.Invoke, exceptionHandler.Invoke, delayHandler.Proxy, typeof(TaskCanceledException));

            // Validate the number of calls
            int x = passResultHandler ? r / 2 : 0;
            int y = passResultHandler ? r / 2 : r;
            Assert.AreEqual(r, func            .Calls);
            Assert.AreEqual(x, resultHandler   .Calls);
            Assert.AreEqual(y, exceptionHandler.Calls);
            Assert.AreEqual(r, delayHandler    .Calls);
        }

        #endregion
    }
}
