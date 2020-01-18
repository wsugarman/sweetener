// Generated from ReliableAsyncFunc.Test.tt
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    [TestClass]
    public sealed class ReliableAsyncFuncTest : ReliableDelegateTest<string>
    {
        private static readonly Func<ReliableAsyncFunc<string>, Func<CancellationToken, Task<string>>> s_getFunc = DynamicGetter.ForField<ReliableAsyncFunc<string>, Func<CancellationToken, Task<string>>>("_func");

        [TestMethod]
        public void Ctor_DelayHandler()
            => Ctor_DelayHandler((f, m, e, d) => new ReliableAsyncFunc<string>(f, m, e, d));

        [TestMethod]
        public void Ctor_ComplexDelayHandler()
            => Ctor_ComplexDelayHandler((f, m, e, d) => new ReliableAsyncFunc<string>(f, m, e, d));

        [TestMethod]
        public void Ctor_ResultHandler_DelayHandler()
            => Ctor_ResultHandler_DelayHandler((f, m, r, e, d) => new ReliableAsyncFunc<string>(f, m, r, e, d));

        [TestMethod]
        public void Ctor_ResultHandler_ComplexDelayHandler()
            => Ctor_ResultHandler_ComplexDelayHandler((f, m, r, e, d) => new ReliableAsyncFunc<string>(f, m, r, e, d));

        [TestMethod]
        public void Ctor_Interruptable_DelayHandler()
            => Ctor_Interruptable_DelayHandler((f, m, e, d) => new ReliableAsyncFunc<string>(f, m, e, d));

        [TestMethod]
        public void Ctor_Interruptable_ComplexDelayHandler()
            => Ctor_Interruptable_ComplexDelayHandler((f, m, e, d) => new ReliableAsyncFunc<string>(f, m, e, d));

        [TestMethod]
        public void Ctor_Interruptable_ResultHandler_DelayHandler()
            => Ctor_Interruptable_ResultHandler_DelayHandler((f, m, r, e, d) => new ReliableAsyncFunc<string>(f, m, r, e, d));

        [TestMethod]
        public void Ctor_Interruptable_ResultHandler_ComplexDelayHandler()
            => Ctor_Interruptable_ResultHandler_ComplexDelayHandler((f, m, r, e, d) => new ReliableAsyncFunc<string>(f, m, r, e, d));

        [TestMethod]
        public void Create_DelayHandler()
            => Ctor_DelayHandler((f, m, e, d) => ReliableAsyncFunc.Create(f, m, e, d));

        [TestMethod]
        public void Create_ComplexDelayHandler()
            => Ctor_ComplexDelayHandler((f, m, e, d) => ReliableAsyncFunc.Create(f, m, e, d));

        [TestMethod]
        public void Create_ResultHandler_DelayHandler()
            => Ctor_ResultHandler_DelayHandler((f, m, r, e, d) => ReliableAsyncFunc.Create(f, m, r, e, d));

        [TestMethod]
        public void Create_ResultHandler_ComplexDelayHandler()
            => Ctor_ResultHandler_ComplexDelayHandler((f, m, r, e, d) => ReliableAsyncFunc.Create(f, m, r, e, d));

        [TestMethod]
        public void Create_Interruptable_DelayHandler()
            => Ctor_Interruptable_DelayHandler((f, m, e, d) => ReliableAsyncFunc.Create(f, m, e, d));

        [TestMethod]
        public void Create_Interruptable_ComplexDelayHandler()
            => Ctor_Interruptable_ComplexDelayHandler((f, m, e, d) => ReliableAsyncFunc.Create(f, m, e, d));

        [TestMethod]
        public void Create_Interruptable_ResultHandler_DelayHandler()
            => Ctor_Interruptable_ResultHandler_DelayHandler((f, m, r, e, d) => ReliableAsyncFunc.Create(f, m, r, e, d));

        [TestMethod]
        public void Create_Interruptable_ResultHandler_ComplexDelayHandler()
            => Ctor_Interruptable_ResultHandler_ComplexDelayHandler((f, m, r, e, d) => ReliableAsyncFunc.Create(f, m, r, e, d));

        [TestMethod]
        public void InvokeAsync()
            => InvokeAsync(passToken: false);

        [TestMethod]
        public void InvokeAsync_CancellationToken()
            => InvokeAsync(passToken: true);

        #region Ctor

        private void Ctor_DelayHandler(Func<Func<Task<string>>, int, ExceptionHandler, DelayHandler, ReliableAsyncFunc<string>> factory)
        {
            FuncProxy<Task<string>> func = new FuncProxy<Task<string>>();
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            FuncProxy<int, TimeSpan> delayHandler = new FuncProxy<int, TimeSpan>(i => Constants.Delay);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(func.Invoke, -2              , exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func.Invoke, Retries.Infinite, null            , delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func.Invoke, Retries.Infinite, exceptionHandler, null));

            // Create a ReliableAsyncFunc and validate
            ReliableAsyncFunc<string> actual = factory(func.Invoke, 37, exceptionHandler, delayHandler.Invoke);

            Ctor(actual, 37, exceptionHandler, delayHandler);
            CtorFunc(actual, func);
        }

        private void Ctor_ComplexDelayHandler(Func<Func<Task<string>>, int, ExceptionHandler, ComplexDelayHandler<string>, ReliableAsyncFunc<string>> factory)
        {
            FuncProxy<Task<string>> func = new FuncProxy<Task<string>>();
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            ComplexDelayHandler<string> delayHandler = (i, r, e) => TimeSpan.FromHours(1);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(func.Invoke, -2              , exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func.Invoke, Retries.Infinite, null            , delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func.Invoke, Retries.Infinite, exceptionHandler, null));

            // Create a ReliableAsyncFunc and validate
            ReliableAsyncFunc<string> actual = factory(func.Invoke, 37, exceptionHandler, delayHandler);

            Ctor(actual, 37, exceptionHandler, delayHandler);
            CtorFunc(actual, func);
        }

        private void Ctor_ResultHandler_DelayHandler(Func<Func<Task<string>>, int, ResultHandler<string>, ExceptionHandler, DelayHandler, ReliableAsyncFunc<string>> factory)
        {
            FuncProxy<Task<string>> func = new FuncProxy<Task<string>>();
            ResultHandler<string> resultHandler = r => r == "Successful Value" ? ResultKind.Successful : ResultKind.Fatal;
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            FuncProxy<int, TimeSpan> delayHandler = new FuncProxy<int, TimeSpan>(i => Constants.Delay);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, resultHandler, exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(func.Invoke, -2              , resultHandler, exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func.Invoke, Retries.Infinite, null         , exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func.Invoke, Retries.Infinite, resultHandler, null            , delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func.Invoke, Retries.Infinite, resultHandler, exceptionHandler, null));

            // Create a ReliableAsyncFunc and validate
            ReliableAsyncFunc<string> actual = factory(func.Invoke, 37, resultHandler, exceptionHandler, delayHandler.Invoke);

            Ctor(actual, 37, resultHandler, exceptionHandler, delayHandler);
            CtorFunc(actual, func);
        }

        private void Ctor_ResultHandler_ComplexDelayHandler(Func<Func<Task<string>>, int, ResultHandler<string>, ExceptionHandler, ComplexDelayHandler<string>, ReliableAsyncFunc<string>> factory)
        {
            FuncProxy<Task<string>> func = new FuncProxy<Task<string>>();
            ResultHandler<string> resultHandler = r => r == "Successful Value" ? ResultKind.Successful : ResultKind.Fatal;
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            ComplexDelayHandler<string> delayHandler = (i, r, e) => TimeSpan.FromHours(1);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, resultHandler, exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(func.Invoke, -2              , resultHandler, exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func.Invoke, Retries.Infinite, null         , exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func.Invoke, Retries.Infinite, resultHandler, null            , delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func.Invoke, Retries.Infinite, resultHandler, exceptionHandler, null));

            // Create a ReliableAsyncFunc and validate
            ReliableAsyncFunc<string> actual = factory(func.Invoke, 37, resultHandler, exceptionHandler, delayHandler);

            Ctor(actual, 37, resultHandler, exceptionHandler, delayHandler);
            CtorFunc(actual, func);
        }

        private void Ctor_Interruptable_DelayHandler(Func<Func<CancellationToken, Task<string>>, int, ExceptionHandler, DelayHandler, ReliableAsyncFunc<string>> factory)
        {
            Func<CancellationToken, Task<string>> func = async (token) => await Task.FromResult("Hello World");
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            FuncProxy<int, TimeSpan> delayHandler = new FuncProxy<int, TimeSpan>(i => Constants.Delay);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(func, -2              , exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, null            , delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, exceptionHandler, null));

            // Create a ReliableAsyncFunc and validate
            ReliableAsyncFunc<string> actual = factory(func, 37, exceptionHandler, delayHandler.Invoke);

            Ctor(actual, 37, exceptionHandler, delayHandler);
            CtorFunc(actual, func);
        }

        private void Ctor_Interruptable_ComplexDelayHandler(Func<Func<CancellationToken, Task<string>>, int, ExceptionHandler, ComplexDelayHandler<string>, ReliableAsyncFunc<string>> factory)
        {
            Func<CancellationToken, Task<string>> func = async (token) => await Task.FromResult("Hello World");
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            ComplexDelayHandler<string> delayHandler = (i, r, e) => TimeSpan.FromHours(1);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(func, -2              , exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, null            , delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, exceptionHandler, null));

            // Create a ReliableAsyncFunc and validate
            ReliableAsyncFunc<string> actual = factory(func, 37, exceptionHandler, delayHandler);

            Ctor(actual, 37, exceptionHandler, delayHandler);
            CtorFunc(actual, func);
        }

        private void Ctor_Interruptable_ResultHandler_DelayHandler(Func<Func<CancellationToken, Task<string>>, int, ResultHandler<string>, ExceptionHandler, DelayHandler, ReliableAsyncFunc<string>> factory)
        {
            Func<CancellationToken, Task<string>> func = async (token) => await Task.FromResult("Hello World");
            ResultHandler<string> resultHandler = r => r == "Successful Value" ? ResultKind.Successful : ResultKind.Fatal;
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            FuncProxy<int, TimeSpan> delayHandler = new FuncProxy<int, TimeSpan>(i => Constants.Delay);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, resultHandler, exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(func, -2              , resultHandler, exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, null         , exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, resultHandler, null            , delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, resultHandler, exceptionHandler, null));

            // Create a ReliableAsyncFunc and validate
            ReliableAsyncFunc<string> actual = factory(func, 37, resultHandler, exceptionHandler, delayHandler.Invoke);

            Ctor(actual, 37, resultHandler, exceptionHandler, delayHandler);
            CtorFunc(actual, func);
        }

        private void Ctor_Interruptable_ResultHandler_ComplexDelayHandler(Func<Func<CancellationToken, Task<string>>, int, ResultHandler<string>, ExceptionHandler, ComplexDelayHandler<string>, ReliableAsyncFunc<string>> factory)
        {
            Func<CancellationToken, Task<string>> func = async (token) => await Task.FromResult("Hello World");
            ResultHandler<string> resultHandler = r => r == "Successful Value" ? ResultKind.Successful : ResultKind.Fatal;
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            ComplexDelayHandler<string> delayHandler = (i, r, e) => TimeSpan.FromHours(1);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, resultHandler, exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(func, -2              , resultHandler, exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, null         , exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, resultHandler, null            , delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, resultHandler, exceptionHandler, null));

            // Create a ReliableAsyncFunc and validate
            ReliableAsyncFunc<string> actual = factory(func, 37, resultHandler, exceptionHandler, delayHandler);

            Ctor(actual, 37, resultHandler, exceptionHandler, delayHandler);
            CtorFunc(actual, func);
        }

        private void CtorFunc(ReliableAsyncFunc<string> reliableFunc, FuncProxy<Task<string>> expected)
            => CtorFunc(reliableFunc, actual =>
            {
                Assert.AreEqual(0, expected.Calls);
                actual(default);
                Assert.AreEqual(1, expected.Calls);
            });

        private void CtorFunc(ReliableAsyncFunc<string> reliableFunc, Func<CancellationToken, Task<string>> expected)
            => CtorFunc(reliableFunc, actual => Assert.AreSame(expected, actual));

        private void CtorFunc(ReliableAsyncFunc<string> reliableFunc, Action<Func<CancellationToken, Task<string>>> validateFunc)
            => validateFunc(s_getFunc(reliableFunc));

        #endregion

        #region InvokeAsync

        private void InvokeAsync(bool passToken)
        {
            Func<ReliableAsyncFunc<string>, CancellationToken, string> invoke;
            if (passToken)
                invoke = (r, t) => r.InvokeAsync(t).Result;
            else
                invoke = (r, t) => r.InvokeAsync().Result;

            // Callers may optionally include event handlers
            foreach (bool addEventHandlers in new bool[] { false, true })
            {
                // Success
                Invoke_Success                ((f, t, r) => Assert.AreEqual(r, invoke(f, t)), addEventHandlers);
                Invoke_EventualSuccess        ((f, t, r) => Assert.AreEqual(r, invoke(f, t)), addEventHandlers);

                // Failure (Result)
                Invoke_Failure_Result         ((f, t, r) => Assert.AreEqual(r, invoke(f, t)), addEventHandlers);
                Invoke_EventualFailure_Result ((f, t, r) => Assert.AreEqual(r, invoke(f, t)), addEventHandlers);
                Invoke_RetriesExhausted_Result((f, t, r) => Assert.AreEqual(r, invoke(f, t)), addEventHandlers);

                // Failure (Exception)
                Invoke_Failure_Exception         ((f, t, e) => Assert.That.ThrowsException(() => invoke(f, t), e), addEventHandlers);
                Invoke_EventualFailure_Exception ((f, t, e) => Assert.That.ThrowsException(() => invoke(f, t), e), addEventHandlers);
                Invoke_RetriesExhausted_Exception((f, t, e) => Assert.That.ThrowsException(() => invoke(f, t), e), addEventHandlers);

                if (passToken)
                {
                    Invoke_Canceled_Func ((f, t) => f.InvokeAsync(t).Wait(), addEventHandlers, useSynchronousFunc: false);
                    Invoke_Canceled_Func ((f, t) => f.InvokeAsync(t).Wait(), addEventHandlers, useSynchronousFunc: true );
                    Invoke_Canceled_Delay((f, t) => f.InvokeAsync(t).Wait(), addEventHandlers);
                }
            }
        }

        #endregion

        #region Invoke_Success

        private void Invoke_Success(Action<ReliableAsyncFunc<string>, CancellationToken, string> assertInvoke, bool addEventHandlers)
        {
            // Create a "successful" user-defined function
            FuncProxy<Task<string>> func = new FuncProxy<Task<string>>(async () => await Task.FromResult("Success"));

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<string, ResultKind>               resultHandler    = new FuncProxy<string, ResultKind>(r => r == "Success" ? ResultKind.Successful : ResultKind.Fatal);
            FuncProxy<Exception, bool>                  exceptionHandler = new FuncProxy<Exception, bool>();
            FuncProxy<int, string, Exception, TimeSpan> delayHandler     = new FuncProxy<int, string, Exception, TimeSpan>();

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableAsyncFunc
            ReliableAsyncFunc<string> reliableFunc = new ReliableAsyncFunc<string>(
                func.Invoke,
                Retries.Infinite,
                resultHandler   .Invoke,
                exceptionHandler.Invoke,
                delayHandler    .Invoke);

            if (addEventHandlers)
            {
                reliableFunc.Retrying         += retryHandler    .Invoke;
                reliableFunc.Failed           += failedHandler   .Invoke;
                reliableFunc.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            resultHandler   .Invoking += Expect.Result("Success");
            exceptionHandler.Invoking += Expect.Nothing<Exception>();
            delayHandler    .Invoking += Expect.Nothing<int, string, Exception>();
            retryHandler    .Invoking += Expect.Nothing<int, string, Exception>();
            failedHandler   .Invoking += Expect.Nothing<string, Exception>();
            exhaustedHandler.Invoking += Expect.Nothing<string, Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableFunc, tokenSource.Token, "Success");

            // Validate the number of calls
            Assert.AreEqual(1, func            .Calls);
            Assert.AreEqual(1, resultHandler   .Calls);
            Assert.AreEqual(0, exceptionHandler.Calls);
            Assert.AreEqual(0, delayHandler    .Calls);
            Assert.AreEqual(0, retryHandler    .Calls);
            Assert.AreEqual(0, failedHandler   .Calls);
            Assert.AreEqual(0, exhaustedHandler.Calls);
        }

        #endregion

        #region Invoke_Failure_Result

        private void Invoke_Failure_Result(Action<ReliableAsyncFunc<string>, CancellationToken, string> assertInvoke, bool addEventHandlers)
        {
            // Create an "unsuccessful" user-defined function that returns a fatal result
            FuncProxy<Task<string>> func = new FuncProxy<Task<string>>(async () => await Task.FromResult("Failure"));

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<string, ResultKind>               resultHandler    = new FuncProxy<string, ResultKind>(r => r == "Failure" ? ResultKind.Fatal : ResultKind.Successful);
            FuncProxy<Exception, bool>                  exceptionHandler = new FuncProxy<Exception, bool>();
            FuncProxy<int, string, Exception, TimeSpan> delayHandler     = new FuncProxy<int, string, Exception, TimeSpan>();

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableAsyncFunc
            ReliableAsyncFunc<string> reliableFunc = new ReliableAsyncFunc<string>(
                func.Invoke,
                Retries.Infinite,
                resultHandler   .Invoke,
                exceptionHandler.Invoke,
                delayHandler    .Invoke);

            if (addEventHandlers)
            {
                reliableFunc.Retrying         += retryHandler    .Invoke;
                reliableFunc.Failed           += failedHandler   .Invoke;
                reliableFunc.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            resultHandler   .Invoking += Expect.Result("Failure");
            exceptionHandler.Invoking += Expect.Nothing<Exception>();
            delayHandler    .Invoking += Expect.Nothing<int, string, Exception>();
            retryHandler    .Invoking += Expect.Nothing<int, string, Exception>();
            failedHandler   .Invoking += Expect.OnlyResult("Failure");
            exhaustedHandler.Invoking += Expect.Nothing<string, Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableFunc, tokenSource.Token, "Failure");

            // Validate the number of calls
            Assert.AreEqual(1, func            .Calls);
            Assert.AreEqual(1, resultHandler   .Calls);
            Assert.AreEqual(0, exceptionHandler.Calls);
            Assert.AreEqual(0, delayHandler    .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(0, retryHandler    .Calls);
                Assert.AreEqual(1, failedHandler   .Calls);
                Assert.AreEqual(0, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_Failure_Exception

        private void Invoke_Failure_Exception(Action<ReliableAsyncFunc<string>, CancellationToken, Type> assertInvoke, bool addEventHandlers)
        {
            // Create an "unsuccessful" user-defined function that throws a fatal exception
            FuncProxy<Task<string>> func = new FuncProxy<Task<string>>(async () => await Task.FromException<string>(new InvalidOperationException()));

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<string, ResultKind>               resultHandler    = new FuncProxy<string, ResultKind>();
            FuncProxy<Exception, bool>                  exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Fail<InvalidOperationException>().Invoke);
            FuncProxy<int, string, Exception, TimeSpan> delayHandler     = new FuncProxy<int, string, Exception, TimeSpan>();

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableAsyncFunc
            ReliableAsyncFunc<string> reliableFunc = new ReliableAsyncFunc<string>(
                func.Invoke,
                Retries.Infinite,
                resultHandler   .Invoke,
                exceptionHandler.Invoke,
                delayHandler    .Invoke);

            if (addEventHandlers)
            {
                reliableFunc.Retrying         += retryHandler    .Invoke;
                reliableFunc.Failed           += failedHandler   .Invoke;
                reliableFunc.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            resultHandler   .Invoking += Expect.Nothing<string>();
            exceptionHandler.Invoking += Expect.Exception(typeof(InvalidOperationException));
            delayHandler    .Invoking += Expect.Nothing<int, string, Exception>();
            retryHandler    .Invoking += Expect.Nothing<int, string, Exception>();
            failedHandler   .Invoking += Expect.OnlyException<string>(typeof(InvalidOperationException));
            exhaustedHandler.Invoking += Expect.Nothing<string, Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableFunc, tokenSource.Token, typeof(InvalidOperationException));

            // Validate the number of calls
            Assert.AreEqual(1, func             .Calls);
            Assert.AreEqual(0, resultHandler    .Calls);
            Assert.AreEqual(1, exceptionHandler .Calls);
            Assert.AreEqual(0, delayHandler     .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(0, retryHandler    .Calls);
                Assert.AreEqual(1, failedHandler   .Calls);
                Assert.AreEqual(0, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_EventualSuccess

        private void Invoke_EventualSuccess(Action<ReliableAsyncFunc<string>, CancellationToken, string> assertInvoke, bool addEventHandlers)
        {
            // Create a user-defined function that eventually succeeds after a transient result and exception
            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>("Retry", "Success", 2);
            FuncProxy<Task<string>> func = new FuncProxy<Task<string>>(async () => await Task.FromResult(flakyFunc()));

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<string, ResultKind>               resultHandler    = new FuncProxy<string, ResultKind>(r =>
                r switch
                {
                    "Retry"   => ResultKind.Transient,
                    "Success" => ResultKind.Successful,
                    _         => ResultKind.Fatal,
                });
            FuncProxy<Exception, bool>                  exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            FuncProxy<int, string, Exception, TimeSpan> delayHandler     = new FuncProxy<int, string, Exception, TimeSpan>((i, r, e) => Constants.Delay);

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableAsyncFunc
            ReliableAsyncFunc<string> reliableFunc = new ReliableAsyncFunc<string>(
                func.Invoke,
                Retries.Infinite,
                resultHandler   .Invoke,
                exceptionHandler.Invoke,
                delayHandler    .Invoke);

            if (addEventHandlers)
            {
                reliableFunc.Retrying         += retryHandler    .Invoke;
                reliableFunc.Failed           += failedHandler   .Invoke;
                reliableFunc.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            func            .Invoking += Expect.AfterDelay(Constants.MinDelay);
            resultHandler   .Invoking += Expect.Results("Retry", "Success", 1);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            delayHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            retryHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<string, Exception>();
            exhaustedHandler.Invoking += Expect.Nothing<string, Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableFunc, tokenSource.Token, "Success");

            // Validate the number of calls
            Assert.AreEqual(3, func             .Calls);
            Assert.AreEqual(2, resultHandler    .Calls);
            Assert.AreEqual(1, exceptionHandler .Calls);
            Assert.AreEqual(2, delayHandler     .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(2, retryHandler    .Calls);
                Assert.AreEqual(0, failedHandler   .Calls);
                Assert.AreEqual(0, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_EventualFailure_Result

        private void Invoke_EventualFailure_Result(Action<ReliableAsyncFunc<string>, CancellationToken, string> assertInvoke, bool addEventHandlers)
        {
            // Create a user-defined function that eventually fails after a transient result and exception
            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>("Retry", "Failure", 2);
            FuncProxy<Task<string>> func = new FuncProxy<Task<string>>(async () => await Task.FromResult(flakyFunc()));

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<string, ResultKind>               resultHandler    = new FuncProxy<string, ResultKind>(r =>
                r switch
                {
                    "Retry"   => ResultKind.Transient,
                    "Failure" => ResultKind.Fatal,
                    _         => ResultKind.Successful,
                });
            FuncProxy<Exception, bool>                  exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            FuncProxy<int, string, Exception, TimeSpan> delayHandler     = new FuncProxy<int, string, Exception, TimeSpan>((i, r, e) => Constants.Delay);

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableAsyncFunc
            ReliableAsyncFunc<string> reliableFunc = new ReliableAsyncFunc<string>(
                func.Invoke,
                Retries.Infinite,
                resultHandler   .Invoke,
                exceptionHandler.Invoke,
                delayHandler    .Invoke);

            if (addEventHandlers)
            {
                reliableFunc.Retrying         += retryHandler    .Invoke;
                reliableFunc.Failed           += failedHandler   .Invoke;
                reliableFunc.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            func            .Invoking += Expect.AfterDelay(Constants.MinDelay);
            resultHandler   .Invoking += Expect.Results("Retry", "Failure", 1);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            delayHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            retryHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            failedHandler   .Invoking += Expect.OnlyResult("Failure");
            exhaustedHandler.Invoking += Expect.Nothing<string, Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableFunc, tokenSource.Token, "Failure");

            // Validate the number of calls
            Assert.AreEqual(3, func             .Calls);
            Assert.AreEqual(2, resultHandler    .Calls);
            Assert.AreEqual(1, exceptionHandler .Calls);
            Assert.AreEqual(2, delayHandler     .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(2, retryHandler    .Calls);
                Assert.AreEqual(1, failedHandler   .Calls);
                Assert.AreEqual(0, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_EventualFailure_Exception

        private void Invoke_EventualFailure_Exception(Action<ReliableAsyncFunc<string>, CancellationToken, Type> assertInvoke, bool addEventHandlers)
        {
            // Create a user-defined function that eventually fails after a transient result and exception
            Func<string> flakyFunc = FlakyFunc.Create<string, IOException, InvalidOperationException>("Retry", 2);
            FuncProxy<Task<string>> func = new FuncProxy<Task<string>>(async () => await Task.FromResult(flakyFunc()));

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<string, ResultKind>               resultHandler    = new FuncProxy<string, ResultKind>(r => r == "Retry" ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool>                  exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            FuncProxy<int, string, Exception, TimeSpan> delayHandler     = new FuncProxy<int, string, Exception, TimeSpan>((i, r, e) => Constants.Delay);

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableAsyncFunc
            ReliableAsyncFunc<string> reliableFunc = new ReliableAsyncFunc<string>(
                func.Invoke,
                Retries.Infinite,
                resultHandler   .Invoke,
                exceptionHandler.Invoke,
                delayHandler    .Invoke);

            if (addEventHandlers)
            {
                reliableFunc.Retrying         += retryHandler    .Invoke;
                reliableFunc.Failed           += failedHandler   .Invoke;
                reliableFunc.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            func            .Invoking += Expect.AfterDelay(Constants.MinDelay);
            resultHandler   .Invoking += Expect.Result("Retry");
            exceptionHandler.Invoking += Expect.Exceptions(typeof(IOException), typeof(InvalidOperationException), 1);
            delayHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            retryHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            failedHandler   .Invoking += Expect.OnlyException<string>(typeof(InvalidOperationException));
            exhaustedHandler.Invoking += Expect.Nothing<string, Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableFunc, tokenSource.Token, typeof(InvalidOperationException));

            // Validate the number of calls
            Assert.AreEqual(3, func             .Calls);
            Assert.AreEqual(1, resultHandler    .Calls);
            Assert.AreEqual(2, exceptionHandler .Calls);
            Assert.AreEqual(2, delayHandler     .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(2, retryHandler    .Calls);
                Assert.AreEqual(1, failedHandler   .Calls);
                Assert.AreEqual(0, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_RetriesExhausted_Result

        private void Invoke_RetriesExhausted_Result(Action<ReliableAsyncFunc<string>, CancellationToken, string> assertInvoke, bool addEventHandlers)
        {
            // Create a user-defined function that eventually exhausts the maximum number of retries after transient results and exceptions
            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>("Retry");
            FuncProxy<Task<string>> func = new FuncProxy<Task<string>>(async () => await Task.FromResult(flakyFunc()));

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<string, ResultKind>               resultHandler    = new FuncProxy<string, ResultKind>(r => r == "Retry" ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool>                  exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            FuncProxy<int, string, Exception, TimeSpan> delayHandler     = new FuncProxy<int, string, Exception, TimeSpan>((i, r, e) => Constants.Delay);

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableAsyncFunc
            ReliableAsyncFunc<string> reliableFunc = new ReliableAsyncFunc<string>(
                func.Invoke,
                3, // Exception, Result, Exception, Result, ...
                resultHandler   .Invoke,
                exceptionHandler.Invoke,
                delayHandler    .Invoke);

            if (addEventHandlers)
            {
                reliableFunc.Retrying         += retryHandler    .Invoke;
                reliableFunc.Failed           += failedHandler   .Invoke;
                reliableFunc.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            func            .Invoking += Expect.AfterDelay(Constants.MinDelay);
            resultHandler   .Invoking += Expect.Result("Retry");
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            delayHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            retryHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<string, Exception>();
            exhaustedHandler.Invoking += Expect.OnlyResult("Retry");

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableFunc, tokenSource.Token, "Retry");

            // Validate the number of calls
            Assert.AreEqual(4, func             .Calls);
            Assert.AreEqual(2, resultHandler    .Calls);
            Assert.AreEqual(2, exceptionHandler .Calls);
            Assert.AreEqual(3, delayHandler     .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(3, retryHandler    .Calls);
                Assert.AreEqual(0, failedHandler   .Calls);
                Assert.AreEqual(1, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_RetriesExhausted_Exception

        private void Invoke_RetriesExhausted_Exception(Action<ReliableAsyncFunc<string>, CancellationToken, Type> assertInvoke, bool addEventHandlers)
        {
            // Create a user-defined function that eventually exhausts the maximum number of retries after transient results and exceptions
            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>("Retry");
            FuncProxy<Task<string>> func = new FuncProxy<Task<string>>(async () => await Task.FromResult(flakyFunc()));

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<string, ResultKind>               resultHandler    = new FuncProxy<string, ResultKind>(r => r == "Retry" ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool>                  exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            FuncProxy<int, string, Exception, TimeSpan> delayHandler     = new FuncProxy<int, string, Exception, TimeSpan>((i, r, e) => Constants.Delay);

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableAsyncFunc
            ReliableAsyncFunc<string> reliableFunc = new ReliableAsyncFunc<string>(
                func.Invoke,
                2, // Exception, Result, Exception, ...
                resultHandler   .Invoke,
                exceptionHandler.Invoke,
                delayHandler    .Invoke);

            if (addEventHandlers)
            {
                reliableFunc.Retrying         += retryHandler    .Invoke;
                reliableFunc.Failed           += failedHandler   .Invoke;
                reliableFunc.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            func            .Invoking += Expect.AfterDelay(Constants.MinDelay);
            resultHandler   .Invoking += Expect.Result("Retry");
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            delayHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            retryHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<string, Exception>();
            exhaustedHandler.Invoking += Expect.OnlyException<string>(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableFunc, tokenSource.Token, typeof(IOException));

            // Validate the number of calls
            Assert.AreEqual(3, func             .Calls);
            Assert.AreEqual(1, resultHandler    .Calls);
            Assert.AreEqual(2, exceptionHandler .Calls);
            Assert.AreEqual(2, delayHandler     .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(2, retryHandler    .Calls);
                Assert.AreEqual(0, failedHandler   .Calls);
                Assert.AreEqual(1, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_Canceled_Func

        private void Invoke_Canceled_Func(Action<ReliableAsyncFunc<string>, CancellationToken> invoke, bool addEventHandlers, bool useSynchronousFunc)
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create a user-defined action that will throw an exception depending on whether its canceled
            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>("Retry");
            // Note: We need to separately check the use of asynchronous and synchronous methods when checking cancellation
            FuncProxy<CancellationToken, Task<string>> func = useSynchronousFunc
                ? new FuncProxy<CancellationToken, Task<string>>(
                    (token) =>
                    {
                        token.ThrowIfCancellationRequested();
                        return Task.FromResult(flakyFunc());
                    })
                : new FuncProxy<CancellationToken, Task<string>>(
                    async (token) =>
                    {
                        await Task.CompletedTask;
                        token.ThrowIfCancellationRequested();
                        return flakyFunc();
                    });

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<string, ResultKind>               resultHandler    = new FuncProxy<string, ResultKind>(r => ResultKind.Transient);
            FuncProxy<Exception, bool>                  exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Transient.Invoke);
            FuncProxy<int, string, Exception, TimeSpan> delayHandler     = new FuncProxy<int, string, Exception, TimeSpan>((i, r, e) => Constants.Delay);

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableAsyncFunc
            ReliableAsyncFunc<string> reliableFunc = new ReliableAsyncFunc<string>(
                func.Invoke,
                Retries.Infinite, // Exception, Result, Exception, ...
                resultHandler   .Invoke,
                exceptionHandler.Invoke,
                delayHandler    .Invoke);

            if (addEventHandlers)
            {
                reliableFunc.Retrying         += retryHandler    .Invoke;
                reliableFunc.Failed           += failedHandler   .Invoke;
                reliableFunc.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            func            .Invoking += Expect.ArgumentsAfterDelay<CancellationToken>(Arguments.Validate, Constants.MinDelay);
            resultHandler   .Invoking += Expect.Result("Retry");
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            delayHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            retryHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<string, Exception>();
            exhaustedHandler.Invoking += Expect.OnlyException<string>(typeof(IOException));

            // Cancel the retry on its 3rd attempt
            func            .Invoking += (t, c) =>
            {
                if (c.Calls == 3)
                    tokenSource.Cancel();
            };

            // Invoke, retry, and cancel
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableFunc, tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            Assert.AreEqual(3, func             .Calls);
            Assert.AreEqual(1, resultHandler    .Calls);
            Assert.AreEqual(1, exceptionHandler .Calls);
            Assert.AreEqual(2, delayHandler     .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(2, retryHandler    .Calls);
                Assert.AreEqual(0, failedHandler   .Calls);
                Assert.AreEqual(0, exhaustedHandler.Calls);
            }
        }

        #endregion

        #region Invoke_Canceled_Delay

        private void Invoke_Canceled_Delay(Action<ReliableAsyncFunc<string>, CancellationToken> invoke, bool addEventHandlers)
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create a user-defined action that will throw an exception depending on whether its canceled
            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>("Retry");
            FuncProxy<Task<string>> func = new FuncProxy<Task<string>>(async () => await Task.FromResult(flakyFunc()));

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<string, ResultKind>               resultHandler    = new FuncProxy<string, ResultKind>(r => ResultKind.Transient);
            FuncProxy<Exception, bool>                  exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Transient.Invoke);
            FuncProxy<int, string, Exception, TimeSpan> delayHandler     = new FuncProxy<int, string, Exception, TimeSpan>((i, r, e) => Constants.Delay);

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableAsyncFunc
            ReliableAsyncFunc<string> reliableFunc = new ReliableAsyncFunc<string>(
                func.Invoke,
                Retries.Infinite, // Exception, Result, Exception, ...
                resultHandler   .Invoke,
                exceptionHandler.Invoke,
                delayHandler    .Invoke);

            if (addEventHandlers)
            {
                reliableFunc.Retrying         += retryHandler    .Invoke;
                reliableFunc.Failed           += failedHandler   .Invoke;
                reliableFunc.RetriesExhausted += exhaustedHandler.Invoke;
            }

            // Define expectations
            func            .Invoking += Expect.AfterDelay(Constants.MinDelay);
            resultHandler   .Invoking += Expect.Result("Retry");
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            delayHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            retryHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<string, Exception>();
            exhaustedHandler.Invoking += Expect.OnlyException<string>(typeof(IOException));

            // Cancel the retry on its 3rd attempt before the delay
            delayHandler     .Invoking += (i, r, e, c) =>
            {
                if (c.Calls == 3)
                    tokenSource.Cancel();
            };

            // Invoke, retry, and cancel
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableFunc, tokenSource.Token), allowedDerivedTypes: true);

            // Validate the number of calls
            Assert.AreEqual(3, func             .Calls);
            Assert.AreEqual(1, resultHandler    .Calls);
            Assert.AreEqual(2, exceptionHandler .Calls);
            Assert.AreEqual(3, delayHandler     .Calls);

            if (addEventHandlers)
            {
                Assert.AreEqual(2, retryHandler    .Calls);
                Assert.AreEqual(0, failedHandler   .Calls);
                Assert.AreEqual(0, exhaustedHandler.Calls);
            }
        }

        #endregion
    }
}
