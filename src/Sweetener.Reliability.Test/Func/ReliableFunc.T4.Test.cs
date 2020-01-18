// Generated from ReliableFunc.Test.tt
using System;
using System.IO;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    [TestClass]
    public sealed class ReliableFunc3Test : ReliableDelegateTest<string>
    {
        private static readonly Func<ReliableFunc<int, string, double, string>, Func<int, string, double, CancellationToken, string>> s_getFunc = DynamicGetter.ForField<ReliableFunc<int, string, double, string>, Func<int, string, double, CancellationToken, string>>("_func");

        [TestMethod]
        public void Ctor_DelayHandler()
            => Ctor_DelayHandler((f, m, e, d) => new ReliableFunc<int, string, double, string>(f, m, e, d));

        [TestMethod]
        public void Ctor_ComplexDelayHandler()
            => Ctor_ComplexDelayHandler((f, m, e, d) => new ReliableFunc<int, string, double, string>(f, m, e, d));

        [TestMethod]
        public void Ctor_ResultHandler_DelayHandler()
            => Ctor_ResultHandler_DelayHandler((f, m, r, e, d) => new ReliableFunc<int, string, double, string>(f, m, r, e, d));

        [TestMethod]
        public void Ctor_ResultHandler_ComplexDelayHandler()
            => Ctor_ResultHandler_ComplexDelayHandler((f, m, r, e, d) => new ReliableFunc<int, string, double, string>(f, m, r, e, d));

        [TestMethod]
        public void Ctor_Interruptable_DelayHandler()
            => Ctor_Interruptable_DelayHandler((f, m, e, d) => new ReliableFunc<int, string, double, string>(f, m, e, d));

        [TestMethod]
        public void Ctor_Interruptable_ComplexDelayHandler()
            => Ctor_Interruptable_ComplexDelayHandler((f, m, e, d) => new ReliableFunc<int, string, double, string>(f, m, e, d));

        [TestMethod]
        public void Ctor_Interruptable_ResultHandler_DelayHandler()
            => Ctor_Interruptable_ResultHandler_DelayHandler((f, m, r, e, d) => new ReliableFunc<int, string, double, string>(f, m, r, e, d));

        [TestMethod]
        public void Ctor_Interruptable_ResultHandler_ComplexDelayHandler()
            => Ctor_Interruptable_ResultHandler_ComplexDelayHandler((f, m, r, e, d) => new ReliableFunc<int, string, double, string>(f, m, r, e, d));

        [TestMethod]
        public void Create_DelayHandler()
            => Ctor_DelayHandler((f, m, e, d) => ReliableFunc.Create(f, m, e, d));

        [TestMethod]
        public void Create_ComplexDelayHandler()
            => Ctor_ComplexDelayHandler((f, m, e, d) => ReliableFunc.Create(f, m, e, d));

        [TestMethod]
        public void Create_ResultHandler_DelayHandler()
            => Ctor_ResultHandler_DelayHandler((f, m, r, e, d) => ReliableFunc.Create(f, m, r, e, d));

        [TestMethod]
        public void Create_ResultHandler_ComplexDelayHandler()
            => Ctor_ResultHandler_ComplexDelayHandler((f, m, r, e, d) => ReliableFunc.Create(f, m, r, e, d));

        [TestMethod]
        public void Create_Interruptable_DelayHandler()
            => Ctor_Interruptable_DelayHandler((f, m, e, d) => ReliableFunc.Create(f, m, e, d));

        [TestMethod]
        public void Create_Interruptable_ComplexDelayHandler()
            => Ctor_Interruptable_ComplexDelayHandler((f, m, e, d) => ReliableFunc.Create(f, m, e, d));

        [TestMethod]
        public void Create_Interruptable_ResultHandler_DelayHandler()
            => Ctor_Interruptable_ResultHandler_DelayHandler((f, m, r, e, d) => ReliableFunc.Create(f, m, r, e, d));

        [TestMethod]
        public void Create_Interruptable_ResultHandler_ComplexDelayHandler()
            => Ctor_Interruptable_ResultHandler_ComplexDelayHandler((f, m, r, e, d) => ReliableFunc.Create(f, m, r, e, d));

        [TestMethod]
        public void Invoke()
            => Invoke(passToken: false);

        [TestMethod]
        public void Invoke_CancellationToken()
            => Invoke(passToken: true);

        [TestMethod]
        public void InvokeAsync()
            => InvokeAsync(passToken: false);

        [TestMethod]
        public void InvokeAsync_CancellationToken()
            => InvokeAsync(passToken: true);

        [TestMethod]
        public void TryInvoke()
            => TryInvoke(passToken: false);

        [TestMethod]
        public void TryInvoke_CancellationToken()
            => TryInvoke(passToken: true);

        #region Ctor

        private void Ctor_DelayHandler(Func<Func<int, string, double, string>, int, ExceptionHandler, DelayHandler, ReliableFunc<int, string, double, string>> factory)
        {
            FuncProxy<int, string, double, string> func = new FuncProxy<int, string, double, string>();
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            FuncProxy<int, TimeSpan> delayHandler = new FuncProxy<int, TimeSpan>(i => Constants.Delay);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(func.Invoke, -2              , exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func.Invoke, Retries.Infinite, null            , delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func.Invoke, Retries.Infinite, exceptionHandler, null));

            // Create a ReliableFunc and validate
            ReliableFunc<int, string, double, string> actual = factory(func.Invoke, 37, exceptionHandler, delayHandler.Invoke);

            Ctor(actual, 37, exceptionHandler, delayHandler);
            CtorFunc(actual, func);
        }

        private void Ctor_ComplexDelayHandler(Func<Func<int, string, double, string>, int, ExceptionHandler, ComplexDelayHandler<string>, ReliableFunc<int, string, double, string>> factory)
        {
            FuncProxy<int, string, double, string> func = new FuncProxy<int, string, double, string>();
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            ComplexDelayHandler<string> delayHandler = (i, r, e) => TimeSpan.FromHours(1);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(func.Invoke, -2              , exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func.Invoke, Retries.Infinite, null            , delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func.Invoke, Retries.Infinite, exceptionHandler, null));

            // Create a ReliableFunc and validate
            ReliableFunc<int, string, double, string> actual = factory(func.Invoke, 37, exceptionHandler, delayHandler);

            Ctor(actual, 37, exceptionHandler, delayHandler);
            CtorFunc(actual, func);
        }

        private void Ctor_ResultHandler_DelayHandler(Func<Func<int, string, double, string>, int, ResultHandler<string>, ExceptionHandler, DelayHandler, ReliableFunc<int, string, double, string>> factory)
        {
            FuncProxy<int, string, double, string> func = new FuncProxy<int, string, double, string>();
            ResultHandler<string> resultHandler = r => r == "Successful Value" ? ResultKind.Successful : ResultKind.Fatal;
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            FuncProxy<int, TimeSpan> delayHandler = new FuncProxy<int, TimeSpan>(i => Constants.Delay);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, resultHandler, exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(func.Invoke, -2              , resultHandler, exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func.Invoke, Retries.Infinite, null         , exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func.Invoke, Retries.Infinite, resultHandler, null            , delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func.Invoke, Retries.Infinite, resultHandler, exceptionHandler, null));

            // Create a ReliableFunc and validate
            ReliableFunc<int, string, double, string> actual = factory(func.Invoke, 37, resultHandler, exceptionHandler, delayHandler.Invoke);

            Ctor(actual, 37, resultHandler, exceptionHandler, delayHandler);
            CtorFunc(actual, func);
        }

        private void Ctor_ResultHandler_ComplexDelayHandler(Func<Func<int, string, double, string>, int, ResultHandler<string>, ExceptionHandler, ComplexDelayHandler<string>, ReliableFunc<int, string, double, string>> factory)
        {
            FuncProxy<int, string, double, string> func = new FuncProxy<int, string, double, string>();
            ResultHandler<string> resultHandler = r => r == "Successful Value" ? ResultKind.Successful : ResultKind.Fatal;
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            ComplexDelayHandler<string> delayHandler = (i, r, e) => TimeSpan.FromHours(1);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, resultHandler, exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(func.Invoke, -2              , resultHandler, exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func.Invoke, Retries.Infinite, null         , exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func.Invoke, Retries.Infinite, resultHandler, null            , delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func.Invoke, Retries.Infinite, resultHandler, exceptionHandler, null));

            // Create a ReliableFunc and validate
            ReliableFunc<int, string, double, string> actual = factory(func.Invoke, 37, resultHandler, exceptionHandler, delayHandler);

            Ctor(actual, 37, resultHandler, exceptionHandler, delayHandler);
            CtorFunc(actual, func);
        }

        private void Ctor_Interruptable_DelayHandler(Func<Func<int, string, double, CancellationToken, string>, int, ExceptionHandler, DelayHandler, ReliableFunc<int, string, double, string>> factory)
        {
            Func<int, string, double, CancellationToken, string> func = (arg1, arg2, arg3, token) => "Hello World";
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            FuncProxy<int, TimeSpan> delayHandler = new FuncProxy<int, TimeSpan>(i => Constants.Delay);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(func, -2              , exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, null            , delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, exceptionHandler, null));

            // Create a ReliableFunc and validate
            ReliableFunc<int, string, double, string> actual = factory(func, 37, exceptionHandler, delayHandler.Invoke);

            Ctor(actual, 37, exceptionHandler, delayHandler);
            CtorFunc(actual, func);
        }

        private void Ctor_Interruptable_ComplexDelayHandler(Func<Func<int, string, double, CancellationToken, string>, int, ExceptionHandler, ComplexDelayHandler<string>, ReliableFunc<int, string, double, string>> factory)
        {
            Func<int, string, double, CancellationToken, string> func = (arg1, arg2, arg3, token) => "Hello World";
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            ComplexDelayHandler<string> delayHandler = (i, r, e) => TimeSpan.FromHours(1);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(func, -2              , exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, null            , delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, exceptionHandler, null));

            // Create a ReliableFunc and validate
            ReliableFunc<int, string, double, string> actual = factory(func, 37, exceptionHandler, delayHandler);

            Ctor(actual, 37, exceptionHandler, delayHandler);
            CtorFunc(actual, func);
        }

        private void Ctor_Interruptable_ResultHandler_DelayHandler(Func<Func<int, string, double, CancellationToken, string>, int, ResultHandler<string>, ExceptionHandler, DelayHandler, ReliableFunc<int, string, double, string>> factory)
        {
            Func<int, string, double, CancellationToken, string> func = (arg1, arg2, arg3, token) => "Hello World";
            ResultHandler<string> resultHandler = r => r == "Successful Value" ? ResultKind.Successful : ResultKind.Fatal;
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            FuncProxy<int, TimeSpan> delayHandler = new FuncProxy<int, TimeSpan>(i => Constants.Delay);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, resultHandler, exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(func, -2              , resultHandler, exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, null         , exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, resultHandler, null            , delayHandler.Invoke));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, resultHandler, exceptionHandler, null));

            // Create a ReliableFunc and validate
            ReliableFunc<int, string, double, string> actual = factory(func, 37, resultHandler, exceptionHandler, delayHandler.Invoke);

            Ctor(actual, 37, resultHandler, exceptionHandler, delayHandler);
            CtorFunc(actual, func);
        }

        private void Ctor_Interruptable_ResultHandler_ComplexDelayHandler(Func<Func<int, string, double, CancellationToken, string>, int, ResultHandler<string>, ExceptionHandler, ComplexDelayHandler<string>, ReliableFunc<int, string, double, string>> factory)
        {
            Func<int, string, double, CancellationToken, string> func = (arg1, arg2, arg3, token) => "Hello World";
            ResultHandler<string> resultHandler = r => r == "Successful Value" ? ResultKind.Successful : ResultKind.Fatal;
            ExceptionHandler exceptionHandler = ExceptionPolicy.Fatal;
            ComplexDelayHandler<string> delayHandler = (i, r, e) => TimeSpan.FromHours(1);

            Assert.ThrowsException<ArgumentNullException      >(() => factory(null, Retries.Infinite, resultHandler, exceptionHandler, delayHandler.Invoke));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => factory(func, -2              , resultHandler, exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, null         , exceptionHandler, delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, resultHandler, null            , delayHandler));
            Assert.ThrowsException<ArgumentNullException      >(() => factory(func, Retries.Infinite, resultHandler, exceptionHandler, null));

            // Create a ReliableFunc and validate
            ReliableFunc<int, string, double, string> actual = factory(func, 37, resultHandler, exceptionHandler, delayHandler);

            Ctor(actual, 37, resultHandler, exceptionHandler, delayHandler);
            CtorFunc(actual, func);
        }

        private void CtorFunc(ReliableFunc<int, string, double, string> reliableFunc, FuncProxy<int, string, double, string> expected)
            => CtorFunc(reliableFunc, actual =>
            {
                expected.Invoking += Expect.Arguments<int, string, double>(Arguments.Validate);

                Assert.AreEqual(0, expected.Calls);
                actual(42, "foo", 3.14D, default);
                Assert.AreEqual(1, expected.Calls);
            });

        private void CtorFunc(ReliableFunc<int, string, double, string> reliableFunc, Func<int, string, double, CancellationToken, string> expected)
            => CtorFunc(reliableFunc, actual => Assert.AreSame(expected, actual));

        private void CtorFunc(ReliableFunc<int, string, double, string> reliableFunc, Action<Func<int, string, double, CancellationToken, string>> validateFunc)
            => validateFunc(s_getFunc(reliableFunc));

        #endregion

        #region Invoke

        private void Invoke(bool passToken)
        {
            Func<ReliableFunc<int, string, double, string>, int, string, double, CancellationToken, string> invoke;
            if (passToken)
                invoke = (r, arg1, arg2, arg3, t) => r.Invoke(arg1, arg2, arg3, t);
            else
                invoke = (r, arg1, arg2, arg3, t) => r.Invoke(arg1, arg2, arg3);

            // Callers may optionally include event handlers
            foreach (bool addEventHandlers in new bool[] { false, true })
            {
                // Success
                Invoke_Success                ((f, arg1, arg2, arg3, t, r) => Assert.AreEqual(r, invoke(f, arg1, arg2, arg3, t)), addEventHandlers);
                Invoke_EventualSuccess        ((f, arg1, arg2, arg3, t, r) => Assert.AreEqual(r, invoke(f, arg1, arg2, arg3, t)), addEventHandlers);

                // Failure (Result)
                Invoke_Failure_Result         ((f, arg1, arg2, arg3, t, r) => Assert.AreEqual(r, invoke(f, arg1, arg2, arg3, t)), addEventHandlers);
                Invoke_EventualFailure_Result ((f, arg1, arg2, arg3, t, r) => Assert.AreEqual(r, invoke(f, arg1, arg2, arg3, t)), addEventHandlers);
                Invoke_RetriesExhausted_Result((f, arg1, arg2, arg3, t, r) => Assert.AreEqual(r, invoke(f, arg1, arg2, arg3, t)), addEventHandlers);

                // Failure (Exception)
                Invoke_Failure_Exception         ((f, arg1, arg2, arg3, t, e) => Assert.That.ThrowsException(() => invoke(f, arg1, arg2, arg3, t), e), addEventHandlers);
                Invoke_EventualFailure_Exception ((f, arg1, arg2, arg3, t, e) => Assert.That.ThrowsException(() => invoke(f, arg1, arg2, arg3, t), e), addEventHandlers);
                Invoke_RetriesExhausted_Exception((f, arg1, arg2, arg3, t, e) => Assert.That.ThrowsException(() => invoke(f, arg1, arg2, arg3, t), e), addEventHandlers);

                if (passToken)
                {
                    Invoke_Canceled_Func ((f, arg1, arg2, arg3, t) => f.Invoke(arg1, arg2, arg3, t), addEventHandlers);
                    Invoke_Canceled_Delay((f, arg1, arg2, arg3, t) => f.Invoke(arg1, arg2, arg3, t), addEventHandlers);
                }
            }
        }

        #endregion

        #region InvokeAsync

        private void InvokeAsync(bool passToken)
        {
            Func<ReliableFunc<int, string, double, string>, int, string, double, CancellationToken, string> invoke;
            if (passToken)
                invoke = (r, arg1, arg2, arg3, t) => r.InvokeAsync(arg1, arg2, arg3, t).Result;
            else
                invoke = (r, arg1, arg2, arg3, t) => r.InvokeAsync(arg1, arg2, arg3).Result;

            // Callers may optionally include event handlers
            foreach (bool addEventHandlers in new bool[] { false, true })
            {
                // Success
                Invoke_Success                ((f, arg1, arg2, arg3, t, r) => Assert.AreEqual(r, invoke(f, arg1, arg2, arg3, t)), addEventHandlers);
                Invoke_EventualSuccess        ((f, arg1, arg2, arg3, t, r) => Assert.AreEqual(r, invoke(f, arg1, arg2, arg3, t)), addEventHandlers);

                // Failure (Result)
                Invoke_Failure_Result         ((f, arg1, arg2, arg3, t, r) => Assert.AreEqual(r, invoke(f, arg1, arg2, arg3, t)), addEventHandlers);
                Invoke_EventualFailure_Result ((f, arg1, arg2, arg3, t, r) => Assert.AreEqual(r, invoke(f, arg1, arg2, arg3, t)), addEventHandlers);
                Invoke_RetriesExhausted_Result((f, arg1, arg2, arg3, t, r) => Assert.AreEqual(r, invoke(f, arg1, arg2, arg3, t)), addEventHandlers);

                // Failure (Exception)
                Invoke_Failure_Exception         ((f, arg1, arg2, arg3, t, e) => Assert.That.ThrowsException(() => invoke(f, arg1, arg2, arg3, t), e), addEventHandlers);
                Invoke_EventualFailure_Exception ((f, arg1, arg2, arg3, t, e) => Assert.That.ThrowsException(() => invoke(f, arg1, arg2, arg3, t), e), addEventHandlers);
                Invoke_RetriesExhausted_Exception((f, arg1, arg2, arg3, t, e) => Assert.That.ThrowsException(() => invoke(f, arg1, arg2, arg3, t), e), addEventHandlers);

                if (passToken)
                {
                    Invoke_Canceled_Func ((f, arg1, arg2, arg3, t) => f.InvokeAsync(arg1, arg2, arg3, t).Wait(), addEventHandlers);
                    Invoke_Canceled_Delay((f, arg1, arg2, arg3, t) => f.InvokeAsync(arg1, arg2, arg3, t).Wait(), addEventHandlers);
                }
            }
        }

        #endregion

        #region TryInvoke

        private void TryInvoke(bool passToken)
        {
            TryFunc<ReliableFunc<int, string, double, string>, int, string, double, CancellationToken, string> tryInvoke;
            if (passToken)
                tryInvoke = TryInvokeFuncWithToken;
            else
                tryInvoke = TryInvokeFunc;

            Action<ReliableFunc<int, string, double, string>, int, string, double, CancellationToken, string> assertSuccess =
                (f, arg1, arg2, arg3, t, r) =>
                {
                    Assert.IsTrue(tryInvoke(f, arg1, arg2, arg3, t, out string actual));
                    Assert.AreEqual(r, actual);
                };

            Action<ReliableFunc<int, string, double, string>, int, string, double, CancellationToken, string> assertResultFailure =
                (f, arg1, arg2, arg3, t, r) =>
                {
                    // TryInvoke returns the default value instead of the failed value 'r'
                    Assert.IsFalse(tryInvoke(f, arg1, arg2, arg3, t, out string actual));
                    Assert.AreEqual(default, actual);
                };

            Action<ReliableFunc<int, string, double, string>, int, string, double, CancellationToken, Type> assertExceptionFailure =
                (f, arg1, arg2, arg3, t, e) =>
                {
                    // TryInvoke returns false instead of throwing the provided exception 'e'
                    Assert.IsFalse(tryInvoke(f, arg1, arg2, arg3, t, out string actual));
                    Assert.AreEqual(default, actual);
                };

            foreach (bool addEventHandlers in new bool[] { false, true })
            {
                // Success
                Invoke_Success                (assertSuccess, addEventHandlers);
                Invoke_EventualSuccess        (assertSuccess, addEventHandlers);

                // Failure (Result)
                Invoke_Failure_Result         (assertResultFailure, addEventHandlers);
                Invoke_EventualFailure_Result (assertResultFailure, addEventHandlers);
                Invoke_RetriesExhausted_Result(assertResultFailure, addEventHandlers);

                // Failure (Exception)
                Invoke_Failure_Exception         (assertExceptionFailure, addEventHandlers);
                Invoke_EventualFailure_Exception (assertExceptionFailure, addEventHandlers);
                Invoke_RetriesExhausted_Exception(assertExceptionFailure, addEventHandlers);

                if (passToken)
                {
                    Invoke_Canceled_Func ((f, arg1, arg2, arg3, t) => f.TryInvoke(arg1, arg2, arg3, t, out string _), addEventHandlers);
                    Invoke_Canceled_Delay((f, arg1, arg2, arg3, t) => f.TryInvoke(arg1, arg2, arg3, t, out string _), addEventHandlers);
                }
            }

            bool TryInvokeFunc(ReliableFunc<int, string, double, string> reliableFunc, int arg1, string arg2, double arg3, CancellationToken token, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, out result);

            bool TryInvokeFuncWithToken(ReliableFunc<int, string, double, string> reliableFunc, int arg1, string arg2, double arg3, CancellationToken token, out string result)
                => reliableFunc.TryInvoke(arg1, arg2, arg3, token, out result);
        }

        #endregion

        #region Invoke_Success

        private void Invoke_Success(Action<ReliableFunc<int, string, double, string>, int, string, double, CancellationToken, string> assertInvoke, bool addEventHandlers)
        {
            // Create a "successful" user-defined function
            FuncProxy<int, string, double, string> func = new FuncProxy<int, string, double, string>((arg1, arg2, arg3) => "Success");

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<string, ResultKind>               resultHandler    = new FuncProxy<string, ResultKind>(r => r == "Success" ? ResultKind.Successful : ResultKind.Fatal);
            FuncProxy<Exception, bool>                  exceptionHandler = new FuncProxy<Exception, bool>();
            FuncProxy<int, string, Exception, TimeSpan> delayHandler     = new FuncProxy<int, string, Exception, TimeSpan>();

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableFunc
            ReliableFunc<int, string, double, string> reliableFunc = new ReliableFunc<int, string, double, string>(
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
            func            .Invoking += Expect.Arguments<int, string, double>(Arguments.Validate);
            resultHandler   .Invoking += Expect.Result("Success");
            exceptionHandler.Invoking += Expect.Nothing<Exception>();
            delayHandler    .Invoking += Expect.Nothing<int, string, Exception>();
            retryHandler    .Invoking += Expect.Nothing<int, string, Exception>();
            failedHandler   .Invoking += Expect.Nothing<string, Exception>();
            exhaustedHandler.Invoking += Expect.Nothing<string, Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableFunc, 42, "foo", 3.14D, tokenSource.Token, "Success");

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

        private void Invoke_Failure_Result(Action<ReliableFunc<int, string, double, string>, int, string, double, CancellationToken, string> assertInvoke, bool addEventHandlers)
        {
            // Create an "unsuccessful" user-defined function that returns a fatal result
            FuncProxy<int, string, double, string> func = new FuncProxy<int, string, double, string>((arg1, arg2, arg3) => "Failure");

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<string, ResultKind>               resultHandler    = new FuncProxy<string, ResultKind>(r => r == "Failure" ? ResultKind.Fatal : ResultKind.Successful);
            FuncProxy<Exception, bool>                  exceptionHandler = new FuncProxy<Exception, bool>();
            FuncProxy<int, string, Exception, TimeSpan> delayHandler     = new FuncProxy<int, string, Exception, TimeSpan>();

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableFunc
            ReliableFunc<int, string, double, string> reliableFunc = new ReliableFunc<int, string, double, string>(
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
            func            .Invoking += Expect.Arguments<int, string, double>(Arguments.Validate);
            resultHandler   .Invoking += Expect.Result("Failure");
            exceptionHandler.Invoking += Expect.Nothing<Exception>();
            delayHandler    .Invoking += Expect.Nothing<int, string, Exception>();
            retryHandler    .Invoking += Expect.Nothing<int, string, Exception>();
            failedHandler   .Invoking += Expect.OnlyResult("Failure");
            exhaustedHandler.Invoking += Expect.Nothing<string, Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableFunc, 42, "foo", 3.14D, tokenSource.Token, "Failure");

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

        private void Invoke_Failure_Exception(Action<ReliableFunc<int, string, double, string>, int, string, double, CancellationToken, Type> assertInvoke, bool addEventHandlers)
        {
            // Create an "unsuccessful" user-defined function that throws a fatal exception
            FuncProxy<int, string, double, string> func = new FuncProxy<int, string, double, string>((arg1, arg2, arg3) => throw new InvalidOperationException());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<string, ResultKind>               resultHandler    = new FuncProxy<string, ResultKind>();
            FuncProxy<Exception, bool>                  exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Fail<InvalidOperationException>().Invoke);
            FuncProxy<int, string, Exception, TimeSpan> delayHandler     = new FuncProxy<int, string, Exception, TimeSpan>();

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableFunc
            ReliableFunc<int, string, double, string> reliableFunc = new ReliableFunc<int, string, double, string>(
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
            func            .Invoking += Expect.Arguments<int, string, double>(Arguments.Validate);
            resultHandler   .Invoking += Expect.Nothing<string>();
            exceptionHandler.Invoking += Expect.Exception(typeof(InvalidOperationException));
            delayHandler    .Invoking += Expect.Nothing<int, string, Exception>();
            retryHandler    .Invoking += Expect.Nothing<int, string, Exception>();
            failedHandler   .Invoking += Expect.OnlyException<string>(typeof(InvalidOperationException));
            exhaustedHandler.Invoking += Expect.Nothing<string, Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableFunc, 42, "foo", 3.14D, tokenSource.Token, typeof(InvalidOperationException));

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

        private void Invoke_EventualSuccess(Action<ReliableFunc<int, string, double, string>, int, string, double, CancellationToken, string> assertInvoke, bool addEventHandlers)
        {
            // Create a user-defined function that eventually succeeds after a transient result and exception
            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>("Retry", "Success", 2);
            FuncProxy<int, string, double, string> func = new FuncProxy<int, string, double, string>((arg1, arg2, arg3) => flakyFunc());

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

            // Create ReliableFunc
            ReliableFunc<int, string, double, string> reliableFunc = new ReliableFunc<int, string, double, string>(
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
            func            .Invoking += Expect.Arguments<int, string, double>(Arguments.Validate);
            resultHandler   .Invoking += Expect.Results("Retry", "Success", 1);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            delayHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            retryHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<string, Exception>();
            exhaustedHandler.Invoking += Expect.Nothing<string, Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableFunc, 42, "foo", 3.14D, tokenSource.Token, "Success");

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

        private void Invoke_EventualFailure_Result(Action<ReliableFunc<int, string, double, string>, int, string, double, CancellationToken, string> assertInvoke, bool addEventHandlers)
        {
            // Create a user-defined function that eventually fails after a transient result and exception
            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>("Retry", "Failure", 2);
            FuncProxy<int, string, double, string> func = new FuncProxy<int, string, double, string>((arg1, arg2, arg3) => flakyFunc());

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

            // Create ReliableFunc
            ReliableFunc<int, string, double, string> reliableFunc = new ReliableFunc<int, string, double, string>(
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
            func            .Invoking += Expect.ArgumentsAfterDelay<int, string, double>(Arguments.Validate, Constants.MinDelay);
            resultHandler   .Invoking += Expect.Results("Retry", "Failure", 1);
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            delayHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            retryHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            failedHandler   .Invoking += Expect.OnlyResult("Failure");
            exhaustedHandler.Invoking += Expect.Nothing<string, Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableFunc, 42, "foo", 3.14D, tokenSource.Token, "Failure");

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

        private void Invoke_EventualFailure_Exception(Action<ReliableFunc<int, string, double, string>, int, string, double, CancellationToken, Type> assertInvoke, bool addEventHandlers)
        {
            // Create a user-defined function that eventually fails after a transient result and exception
            Func<string> flakyFunc = FlakyFunc.Create<string, IOException, InvalidOperationException>("Retry", 2);
            FuncProxy<int, string, double, string> func = new FuncProxy<int, string, double, string>((arg1, arg2, arg3) => flakyFunc());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<string, ResultKind>               resultHandler    = new FuncProxy<string, ResultKind>(r => r == "Retry" ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool>                  exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            FuncProxy<int, string, Exception, TimeSpan> delayHandler     = new FuncProxy<int, string, Exception, TimeSpan>((i, r, e) => Constants.Delay);

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableFunc
            ReliableFunc<int, string, double, string> reliableFunc = new ReliableFunc<int, string, double, string>(
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
            func            .Invoking += Expect.ArgumentsAfterDelay<int, string, double>(Arguments.Validate, Constants.MinDelay);
            resultHandler   .Invoking += Expect.Result("Retry");
            exceptionHandler.Invoking += Expect.Exceptions(typeof(IOException), typeof(InvalidOperationException), 1);
            delayHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            retryHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            failedHandler   .Invoking += Expect.OnlyException<string>(typeof(InvalidOperationException));
            exhaustedHandler.Invoking += Expect.Nothing<string, Exception>();

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableFunc, 42, "foo", 3.14D, tokenSource.Token, typeof(InvalidOperationException));

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

        private void Invoke_RetriesExhausted_Result(Action<ReliableFunc<int, string, double, string>, int, string, double, CancellationToken, string> assertInvoke, bool addEventHandlers)
        {
            // Create a user-defined function that eventually exhausts the maximum number of retries after transient results and exceptions
            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>("Retry");
            FuncProxy<int, string, double, string> func = new FuncProxy<int, string, double, string>((arg1, arg2, arg3) => flakyFunc());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<string, ResultKind>               resultHandler    = new FuncProxy<string, ResultKind>(r => r == "Retry" ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool>                  exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            FuncProxy<int, string, Exception, TimeSpan> delayHandler     = new FuncProxy<int, string, Exception, TimeSpan>((i, r, e) => Constants.Delay);

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableFunc
            ReliableFunc<int, string, double, string> reliableFunc = new ReliableFunc<int, string, double, string>(
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
            func            .Invoking += Expect.ArgumentsAfterDelay<int, string, double>(Arguments.Validate, Constants.MinDelay);
            resultHandler   .Invoking += Expect.Result("Retry");
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            delayHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            retryHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<string, Exception>();
            exhaustedHandler.Invoking += Expect.OnlyResult("Retry");

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableFunc, 42, "foo", 3.14D, tokenSource.Token, "Retry");

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

        private void Invoke_RetriesExhausted_Exception(Action<ReliableFunc<int, string, double, string>, int, string, double, CancellationToken, Type> assertInvoke, bool addEventHandlers)
        {
            // Create a user-defined function that eventually exhausts the maximum number of retries after transient results and exceptions
            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>("Retry");
            FuncProxy<int, string, double, string> func = new FuncProxy<int, string, double, string>((arg1, arg2, arg3) => flakyFunc());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<string, ResultKind>               resultHandler    = new FuncProxy<string, ResultKind>(r => r == "Retry" ? ResultKind.Transient : ResultKind.Successful);
            FuncProxy<Exception, bool>                  exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Retry<IOException>().Invoke);
            FuncProxy<int, string, Exception, TimeSpan> delayHandler     = new FuncProxy<int, string, Exception, TimeSpan>((i, r, e) => Constants.Delay);

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableFunc
            ReliableFunc<int, string, double, string> reliableFunc = new ReliableFunc<int, string, double, string>(
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
            func            .Invoking += Expect.ArgumentsAfterDelay<int, string, double>(Arguments.Validate, Constants.MinDelay);
            resultHandler   .Invoking += Expect.Result("Retry");
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            delayHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            retryHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<string, Exception>();
            exhaustedHandler.Invoking += Expect.OnlyException<string>(typeof(IOException));

            // Invoke
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
                assertInvoke(reliableFunc, 42, "foo", 3.14D, tokenSource.Token, typeof(IOException));

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

        private void Invoke_Canceled_Func(Action<ReliableFunc<int, string, double, string>, int, string, double, CancellationToken> invoke, bool addEventHandlers)
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create a user-defined action that will throw an exception depending on whether its canceled
            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>("Retry");
            FuncProxy<int, string, double, CancellationToken, string> func = new FuncProxy<int, string, double, CancellationToken, string>(
                (arg1, arg2, arg3, token) =>
                {
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

            // Create ReliableFunc
            ReliableFunc<int, string, double, string> reliableFunc = new ReliableFunc<int, string, double, string>(
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
            func            .Invoking += Expect.ArgumentsAfterDelay<int, string, double, CancellationToken>(Arguments.Validate, Constants.MinDelay);
            resultHandler   .Invoking += Expect.Result("Retry");
            exceptionHandler.Invoking += Expect.Exception(typeof(IOException));
            delayHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            retryHandler    .Invoking += Expect.AlternatingAsc("Retry", typeof(IOException));
            failedHandler   .Invoking += Expect.Nothing<string, Exception>();
            exhaustedHandler.Invoking += Expect.OnlyException<string>(typeof(IOException));

            // Cancel the retry on its 3rd attempt
            func            .Invoking += (arg1, arg2, arg3, t, c) =>
            {
                if (c.Calls == 3)
                    tokenSource.Cancel();
            };

            // Invoke, retry, and cancel
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableFunc, 42, "foo", 3.14D, tokenSource.Token), allowedDerivedTypes: true);

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

        private void Invoke_Canceled_Delay(Action<ReliableFunc<int, string, double, string>, int, string, double, CancellationToken> invoke, bool addEventHandlers)
        {
            using CancellationTokenSource tokenSource = new CancellationTokenSource();

            // Create a user-defined action that will throw an exception depending on whether its canceled
            Func<string> flakyFunc = FlakyFunc.Create<string, IOException>("Retry");
            FuncProxy<int, string, double, string> func = new FuncProxy<int, string, double, string>((arg1, arg2, arg3) => flakyFunc());

            // Declare the various proxies for the input delegates and event handlers
            FuncProxy<string, ResultKind>               resultHandler    = new FuncProxy<string, ResultKind>(r => ResultKind.Transient);
            FuncProxy<Exception, bool>                  exceptionHandler = new FuncProxy<Exception, bool>(ExceptionPolicy.Transient.Invoke);
            FuncProxy<int, string, Exception, TimeSpan> delayHandler     = new FuncProxy<int, string, Exception, TimeSpan>((i, r, e) => Constants.Delay);

            ActionProxy<int, string, Exception> retryHandler     = new ActionProxy<int, string, Exception>();
            ActionProxy<string, Exception>      failedHandler    = new ActionProxy<string, Exception>();
            ActionProxy<string, Exception>      exhaustedHandler = new ActionProxy<string, Exception>();

            // Create ReliableFunc
            ReliableFunc<int, string, double, string> reliableFunc = new ReliableFunc<int, string, double, string>(
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
            func            .Invoking += Expect.ArgumentsAfterDelay<int, string, double>(Arguments.Validate, Constants.MinDelay);
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
            Assert.That.ThrowsException<OperationCanceledException>(() => invoke(reliableFunc, 42, "foo", 3.14D, tokenSource.Token), allowedDerivedTypes: true);

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
