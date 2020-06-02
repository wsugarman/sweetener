// Generated from Reliably.Test.tt
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    partial class ReliablyTest
    {
        #region Invoke

        [TestMethod]
        public void Invoke_Func_DelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullFunc, 10, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testFunc, -3, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, 10, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, 10, ExceptionPolicy.Transient, (DelayHandler)null));

#nullable enable

            Func  <Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> invoke       = (f, t, n, r, e, d)    => Reliably.Invoke(f, n, e, d.Invoke);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d));
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => invoke(f, t, n, r, e, d), x);

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), false);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
        }

        [TestMethod]
        public void Invoke_Func_ComplexDelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullFunc, 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testFunc, -3, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, 10, null                     , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, 10, ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null));

#nullable enable

            Func  <Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> invoke       = (f, t, n, r, e, d)    => Reliably.Invoke(f, n, e, d.Invoke);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d));
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => invoke(f, t, n, r, e, d), x);

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), false);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
        }

        [TestMethod]
        public void Invoke_Func_ResultPolicy_DelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testFunc, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, 10, null                          , ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, 10, ResultPolicy.Default<string>(), null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (DelayHandler)null));

#nullable enable

            Func  <Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> invoke       = (f, t, n, r, e, d)    => Reliably.Invoke(f, n, r, e, d.Invoke);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d));
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => invoke(f, t, n, r, e, d), x);

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), true);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertResult, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Result    (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());
            Invoke_Func_RetriesExhausted_Result   (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
        }

        [TestMethod]
        public void Invoke_Func_ResultPolicy_ComplexDelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testFunc, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, 10, null                          , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, 10, ResultPolicy.Default<string>(), null                     , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null));

#nullable enable

            Func  <Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> invoke       = (f, t, n, r, e, d)    => Reliably.Invoke(f, n, r, e, d.Invoke);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d));
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => invoke(f, t, n, r, e, d), x);

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), true);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Result    (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));
            Invoke_Func_RetriesExhausted_Result   (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
        }

        [TestMethod]
        public void Invoke_Func_Interruptable_DelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testFunc, CancellationToken.None, -3, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, CancellationToken.None, 10, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, (DelayHandler)null));

#nullable enable

            Func  <Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> invoke       = (f, t, n, r, e, d)    => Reliably.Invoke(f, t, n, e, d.Invoke);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d));
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => invoke(f, t, n, r, e, d), x);

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), false);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_Canceled_Delay            (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_Canceled                  (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public void Invoke_Func_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testFunc, CancellationToken.None, -3, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, CancellationToken.None, 10, null                     , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null));

#nullable enable

            Func  <Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> invoke       = (f, t, n, r, e, d)    => Reliably.Invoke(f, t, n, e, d.Invoke);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d));
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => invoke(f, t, n, r, e, d), x);

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), false);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_Canceled_Delay            (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_Canceled                  (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        [TestMethod]
        public void Invoke_Func_Interruptable_ResultPolicy_DelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testFunc, CancellationToken.None, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, CancellationToken.None, 10, null                          , ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (DelayHandler)null));

#nullable enable

            Func  <Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> invoke       = (f, t, n, r, e, d)    => Reliably.Invoke(f, t, n, r, e, d.Invoke);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d));
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => invoke(f, t, n, r, e, d), x);

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), true);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertResult, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Result    (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());
            Invoke_Func_RetriesExhausted_Result   (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_Canceled_Delay            (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_Canceled                  (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public void Invoke_Func_Interruptable_ResultPolicy_ComplexDelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testFunc, CancellationToken.None, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, CancellationToken.None, 10, null                          , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), null                     , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null));

#nullable enable

            Func  <Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> invoke       = (f, t, n, r, e, d)    => Reliably.Invoke(f, t, n, r, e, d.Invoke);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d));
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => invoke(f, t, n, r, e, d), x);

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), true);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Result    (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));
            Invoke_Func_RetriesExhausted_Result   (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_Canceled_Delay            (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_Canceled                  (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        [TestMethod]
        public void Invoke_Func_Stateful_DelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullFunc, new object(), 10, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testFunc, new object(), -3, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, new object(), 10, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, new object(), 10, ExceptionPolicy.Transient, (DelayHandler)null));

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> invoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                return Reliably.Invoke(obj => { Assert.AreSame(obj, state); return f(); }, state, n, e, d.Invoke);
            };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d));
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => invoke(f, t, n, r, e, d), x);

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), false);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
        }

        [TestMethod]
        public void Invoke_Func_Stateful_ComplexDelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullFunc, new object(), 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testFunc, new object(), -3, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, new object(), 10, null                     , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, new object(), 10, ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null));

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> invoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                return Reliably.Invoke(obj => { Assert.AreSame(obj, state); return f(); }, state, n, e, d.Invoke);
            };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d));
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => invoke(f, t, n, r, e, d), x);

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), false);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
        }

        [TestMethod]
        public void Invoke_Func_Stateful_ResultPolicy_DelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testFunc, new object(), -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, new object(), 10, null                          , ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, new object(), 10, ResultPolicy.Default<string>(), null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (DelayHandler)null));

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> invoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                return Reliably.Invoke(obj => { Assert.AreSame(obj, state); return f(); }, state, n, r, e, d.Invoke);
            };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d));
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => invoke(f, t, n, r, e, d), x);

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), true);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertResult, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Result    (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());
            Invoke_Func_RetriesExhausted_Result   (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
        }

        [TestMethod]
        public void Invoke_Func_Stateful_ResultPolicy_ComplexDelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testFunc, new object(), -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, new object(), 10, null                          , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, new object(), 10, ResultPolicy.Default<string>(), null                     , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null));

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> invoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                return Reliably.Invoke(obj => { Assert.AreSame(obj, state); return f(); }, state, n, r, e, d.Invoke);
            };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d));
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => invoke(f, t, n, r, e, d), x);

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), true);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Result    (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));
            Invoke_Func_RetriesExhausted_Result   (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
        }

        [TestMethod]
        public void Invoke_Func_Stateful_Interruptable_DelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testFunc, new object(), CancellationToken.None, -3, ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, new object(), CancellationToken.None, 10, null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, (DelayHandler)null));

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> invoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                return Reliably.Invoke(obj => { Assert.AreSame(obj, state); return f(); }, state, t, n, e, d.Invoke);
            };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d));
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => invoke(f, t, n, r, e, d), x);

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), false);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_Canceled_Delay            (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_Canceled                  (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public void Invoke_Func_Stateful_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testFunc, new object(), CancellationToken.None, -3, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, new object(), CancellationToken.None, 10, null                     , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null));

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> invoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                return Reliably.Invoke(obj => { Assert.AreSame(obj, state); return f(); }, state, t, n, e, d.Invoke);
            };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d));
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => invoke(f, t, n, r, e, d), x);

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), false);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_Canceled_Delay            (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_Canceled                  (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        [TestMethod]
        public void Invoke_Func_Stateful_Interruptable_ResultPolicy_DelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testFunc, new object(), CancellationToken.None, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, new object(), CancellationToken.None, 10, null                          , ExceptionPolicy.Transient, DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), null                     , DelayPolicy.None));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (DelayHandler)null));

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> invoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                return Reliably.Invoke(obj => { Assert.AreSame(obj, state); return f(); }, state, t, n, r, e, d.Invoke);
            };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d));
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => invoke(f, t, n, r, e, d), x);

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), true);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertResult, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Result    (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());
            Invoke_Func_RetriesExhausted_Result   (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_Canceled_Delay            (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_Canceled                  (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public void Invoke_Func_Stateful_Interruptable_ResultPolicy_ComplexDelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(nullFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.Invoke(testFunc, new object(), CancellationToken.None, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, new object(), CancellationToken.None, 10, null                          , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), null                     , (i, r, e) => TimeSpan.Zero));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.Invoke(testFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null));

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> invoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                return Reliably.Invoke(obj => { Assert.AreSame(obj, state); return f(); }, state, t, n, r, e, d.Invoke);
            };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d));
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => invoke(f, t, n, r, e, d), x);

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), true);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Result    (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));
            Invoke_Func_RetriesExhausted_Result   (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_Canceled_Delay            (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_Canceled                  (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        #endregion

        #region InvokeAsync

        [TestMethod]
        public async Task InvokeAsync_Func_DelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, 10, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, -3, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, 10, null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, 10, ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func  <Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d)    => Reliably.InvokeAsync(f, n, e, d.Invoke);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d).Result);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), false);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
        }

        [TestMethod]
        public async Task InvokeAsync_Func_ComplexDelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, -3, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, 10, null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, 10, ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

#nullable enable

            Func  <Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d)    => Reliably.InvokeAsync(f, n, e, d.Invoke);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d).Result);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), false);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
        }

        [TestMethod]
        public async Task InvokeAsync_Func_ResultPolicy_DelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, 10, null                          , ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, 10, ResultPolicy.Default<string>(), null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func  <Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d)    => Reliably.InvokeAsync(f, n, r, e, d.Invoke);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d).Result);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), true);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertResult, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Result    (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());
            Invoke_Func_RetriesExhausted_Result   (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
        }

        [TestMethod]
        public async Task InvokeAsync_Func_ResultPolicy_ComplexDelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, 10, null                          , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, 10, ResultPolicy.Default<string>(), null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

#nullable enable

            Func  <Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d)    => Reliably.InvokeAsync(f, n, r, e, d.Invoke);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d).Result);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), true);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Result    (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));
            Invoke_Func_RetriesExhausted_Result   (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
        }

        [TestMethod]
        public async Task InvokeAsync_Func_Interruptable_DelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, -3, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, 10, null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func  <Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d)    => Reliably.InvokeAsync(f, t, n, e, d.Invoke);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d).Result);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), false);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_Canceled_Delay            (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_Canceled                  (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public async Task InvokeAsync_Func_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, -3, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, 10, null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

#nullable enable

            Func  <Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d)    => Reliably.InvokeAsync(f, t, n, e, d.Invoke);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d).Result);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), false);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_Canceled_Delay            (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_Canceled                  (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        [TestMethod]
        public async Task InvokeAsync_Func_Interruptable_ResultPolicy_DelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, 10, null                          , ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func  <Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d)    => Reliably.InvokeAsync(f, t, n, r, e, d.Invoke);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d).Result);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), true);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertResult, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Result    (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());
            Invoke_Func_RetriesExhausted_Result   (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_Canceled_Delay            (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_Canceled                  (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public async Task InvokeAsync_Func_Interruptable_ResultPolicy_ComplexDelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, 10, null                          , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

#nullable enable

            Func  <Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d)    => Reliably.InvokeAsync(f, t, n, r, e, d.Invoke);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d).Result);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), true);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Result    (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));
            Invoke_Func_RetriesExhausted_Result   (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_Canceled_Delay            (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_Canceled                  (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        [TestMethod]
        public async Task InvokeAsync_Func_Stateful_DelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, new object(), 10, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, new object(), -3, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), 10, null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), 10, ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, string> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return f();
                };

                return Reliably.InvokeAsync(inputFunc, state, n, e, d.Invoke);
            };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d).Result);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), false);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
        }

        [TestMethod]
        public async Task InvokeAsync_Func_Stateful_ComplexDelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, new object(), 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, new object(), -3, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), 10, null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), 10, ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, string> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return f();
                };

                return Reliably.InvokeAsync(inputFunc, state, n, e, d.Invoke);
            };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d).Result);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), false);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
        }

        [TestMethod]
        public async Task InvokeAsync_Func_Stateful_ResultPolicy_DelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, new object(), -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), 10, null                          , ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), 10, ResultPolicy.Default<string>(), null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, string> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return f();
                };

                return Reliably.InvokeAsync(inputFunc, state, n, r, e, d.Invoke);
            };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d).Result);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), true);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertResult, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Result    (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());
            Invoke_Func_RetriesExhausted_Result   (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
        }

        [TestMethod]
        public async Task InvokeAsync_Func_Stateful_ResultPolicy_ComplexDelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, new object(), -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), 10, null                          , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), 10, ResultPolicy.Default<string>(), null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, string> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return f();
                };

                return Reliably.InvokeAsync(inputFunc, state, n, r, e, d.Invoke);
            };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d).Result);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), true);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Result    (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));
            Invoke_Func_RetriesExhausted_Result   (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
        }

        [TestMethod]
        public async Task InvokeAsync_Func_Stateful_Interruptable_DelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, -3, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, 10, null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, string> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return f();
                };

                return Reliably.InvokeAsync(inputFunc, state, t, n, e, d.Invoke);
            };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d).Result);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), false);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_Canceled_Delay            (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_Canceled                  (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public async Task InvokeAsync_Func_Stateful_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, -3, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, 10, null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, string> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return f();
                };

                return Reliably.InvokeAsync(inputFunc, state, t, n, e, d.Invoke);
            };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d).Result);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), false);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_Canceled_Delay            (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_Canceled                  (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        [TestMethod]
        public async Task InvokeAsync_Func_Stateful_Interruptable_ResultPolicy_DelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, 10, null                          , ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, string> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return f();
                };

                return Reliably.InvokeAsync(inputFunc, state, t, n, r, e, d.Invoke);
            };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d).Result);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), true);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertResult, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Result    (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());
            Invoke_Func_RetriesExhausted_Result   (assertResult, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_Canceled_Delay            (assertError , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_Canceled                  (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public async Task InvokeAsync_Func_Stateful_Interruptable_ResultPolicy_ComplexDelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, 10, null                          , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, string> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return f();
                };

                return Reliably.InvokeAsync(inputFunc, state, t, n, r, e, d.Invoke);
            };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertResult = (f, t, n, r, e, d, x) => Assert.AreEqual(x, invoke(f, t, n, r, e, d).Result);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError  = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => invoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), true);
            Invoke_Func_EventualSuccess           (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Result    (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));
            Invoke_Func_RetriesExhausted_Result   (assertResult, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_RetriesExhausted_Exception(assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_Canceled_Delay            (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_Canceled                  (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        #endregion

        #region TryInvoke

        [TestMethod]
        public void TryInvoke_Func_DelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(nullFunc, 10, ExceptionPolicy.Transient, DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.TryInvoke(testFunc, -3, ExceptionPolicy.Transient, DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, 10, null                     , DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, 10, ExceptionPolicy.Transient, (DelayHandler)null, out string _));

#nullable enable

            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue (TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(x      , a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => TryInvoke(f, t, n, r, e, d, out string _), x);

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), false);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            static bool TryInvoke(Func<string> f, CancellationToken t, int n, ResultHandler<string> r, ExceptionHandler e, Func<int, TimeSpan> d, out string x)
            {
                return Reliably.TryInvoke(f, n, e, d.Invoke, out x);
            }
        }

        [TestMethod]
        public void TryInvoke_Func_ComplexDelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(nullFunc, 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.TryInvoke(testFunc, -3, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, 10, null                     , (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, 10, ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null, out string _));

#nullable enable

            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue (TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(x      , a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => TryInvoke(f, t, n, r, e, d, out string _), x);

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), false);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            static bool TryInvoke(Func<string> f, CancellationToken t, int n, ResultHandler<string> r, ExceptionHandler e, Func<int, string, Exception?, TimeSpan> d, out string x)
            {
                return Reliably.TryInvoke(f, n, e, d.Invoke, out x);
            }
        }

        [TestMethod]
        public void TryInvoke_Func_ResultPolicy_DelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(nullFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.TryInvoke(testFunc, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, 10, null                          , ExceptionPolicy.Transient, DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, 10, ResultPolicy.Default<string>(), null                     , DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (DelayHandler)null, out string _));

#nullable enable

            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue (TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(x      , a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => TryInvoke(f, t, n, r, e, d, out string _), x);

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), true);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertFailureResult   , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Result    (assertFailureResult   , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());
            Invoke_Func_RetriesExhausted_Result   (assertFailureResult   , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            static bool TryInvoke(Func<string> f, CancellationToken t, int n, ResultHandler<string> r, ExceptionHandler e, Func<int, TimeSpan> d, out string x)
            {
                return Reliably.TryInvoke(f, n, r, e, d.Invoke, out x);
            }
        }

        [TestMethod]
        public void TryInvoke_Func_ResultPolicy_ComplexDelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(nullFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.TryInvoke(testFunc, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, 10, null                          , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, 10, ResultPolicy.Default<string>(), null                     , (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null, out string _));

#nullable enable

            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue (TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(x      , a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => TryInvoke(f, t, n, r, e, d, out string _), x);

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), true);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertFailureResult   , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Result    (assertFailureResult   , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));
            Invoke_Func_RetriesExhausted_Result   (assertFailureResult   , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            static bool TryInvoke(Func<string> f, CancellationToken t, int n, ResultHandler<string> r, ExceptionHandler e, Func<int, string, Exception?, TimeSpan> d, out string x)
            {
                return Reliably.TryInvoke(f, n, r, e, d.Invoke, out x);
            }
        }

        [TestMethod]
        public void TryInvoke_Func_Interruptable_DelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(nullFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.TryInvoke(testFunc, CancellationToken.None, -3, ExceptionPolicy.Transient, DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, CancellationToken.None, 10, null                     , DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, (DelayHandler)null, out string _));

#nullable enable

            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue (TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(x      , a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => TryInvoke(f, t, n, r, e, d, out string _), x);

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), false);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_Canceled_Delay            (assertError           , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_Canceled                  (assertError           , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());

            static bool TryInvoke(Func<string> f, CancellationToken t, int n, ResultHandler<string> r, ExceptionHandler e, Func<int, TimeSpan> d, out string x)
            {
                return Reliably.TryInvoke(f, t, n, e, d.Invoke, out x);
            }
        }

        [TestMethod]
        public void TryInvoke_Func_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(nullFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.TryInvoke(testFunc, CancellationToken.None, -3, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, CancellationToken.None, 10, null                     , (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null, out string _));

#nullable enable

            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue (TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(x      , a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => TryInvoke(f, t, n, r, e, d, out string _), x);

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), false);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_Canceled_Delay            (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_Canceled                  (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());

            static bool TryInvoke(Func<string> f, CancellationToken t, int n, ResultHandler<string> r, ExceptionHandler e, Func<int, string, Exception?, TimeSpan> d, out string x)
            {
                return Reliably.TryInvoke(f, t, n, e, d.Invoke, out x);
            }
        }

        [TestMethod]
        public void TryInvoke_Func_Interruptable_ResultPolicy_DelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(nullFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.TryInvoke(testFunc, CancellationToken.None, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, CancellationToken.None, 10, null                          , ExceptionPolicy.Transient, DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), null                     , DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (DelayHandler)null, out string _));

#nullable enable

            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue (TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(x      , a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => TryInvoke(f, t, n, r, e, d, out string _), x);

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), true);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertFailureResult   , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Result    (assertFailureResult   , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());
            Invoke_Func_RetriesExhausted_Result   (assertFailureResult   , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_Canceled_Delay            (assertError           , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_Canceled                  (assertError           , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());

            static bool TryInvoke(Func<string> f, CancellationToken t, int n, ResultHandler<string> r, ExceptionHandler e, Func<int, TimeSpan> d, out string x)
            {
                return Reliably.TryInvoke(f, t, n, r, e, d.Invoke, out x);
            }
        }

        [TestMethod]
        public void TryInvoke_Func_Interruptable_ResultPolicy_ComplexDelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(nullFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.TryInvoke(testFunc, CancellationToken.None, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, CancellationToken.None, 10, null                          , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), null                     , (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null, out string _));

#nullable enable

            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue (TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(x      , a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => TryInvoke(f, t, n, r, e, d, out string _), x);

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), true);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertFailureResult   , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Result    (assertFailureResult   , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));
            Invoke_Func_RetriesExhausted_Result   (assertFailureResult   , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_Canceled_Delay            (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_Canceled                  (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());

            static bool TryInvoke(Func<string> f, CancellationToken t, int n, ResultHandler<string> r, ExceptionHandler e, Func<int, string, Exception?, TimeSpan> d, out string x)
            {
                return Reliably.TryInvoke(f, t, n, r, e, d.Invoke, out x);
            }
        }

        [TestMethod]
        public void TryInvoke_Func_Stateful_DelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(nullFunc, new object(), 10, ExceptionPolicy.Transient, DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.TryInvoke(testFunc, new object(), -3, ExceptionPolicy.Transient, DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, new object(), 10, null                     , DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, new object(), 10, ExceptionPolicy.Transient, (DelayHandler)null, out string _));

#nullable enable

            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue (TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(x      , a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => TryInvoke(f, t, n, r, e, d, out string _), x);

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), false);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            static bool TryInvoke(Func<string> f, CancellationToken t, int n, ResultHandler<string> r, ExceptionHandler e, Func<int, TimeSpan> d, out string x)
            {
                object state = new object();
                return Reliably.TryInvoke(obj => { Assert.AreSame(obj, state); return f(); }, state, n, e, d.Invoke, out x);
            }
        }

        [TestMethod]
        public void TryInvoke_Func_Stateful_ComplexDelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(nullFunc, new object(), 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.TryInvoke(testFunc, new object(), -3, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, new object(), 10, null                     , (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, new object(), 10, ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null, out string _));

#nullable enable

            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue (TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(x      , a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => TryInvoke(f, t, n, r, e, d, out string _), x);

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), false);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            static bool TryInvoke(Func<string> f, CancellationToken t, int n, ResultHandler<string> r, ExceptionHandler e, Func<int, string, Exception?, TimeSpan> d, out string x)
            {
                object state = new object();
                return Reliably.TryInvoke(obj => { Assert.AreSame(obj, state); return f(); }, state, n, e, d.Invoke, out x);
            }
        }

        [TestMethod]
        public void TryInvoke_Func_Stateful_ResultPolicy_DelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(nullFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.TryInvoke(testFunc, new object(), -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, new object(), 10, null                          , ExceptionPolicy.Transient, DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, new object(), 10, ResultPolicy.Default<string>(), null                     , DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (DelayHandler)null, out string _));

#nullable enable

            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue (TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(x      , a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => TryInvoke(f, t, n, r, e, d, out string _), x);

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), true);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertFailureResult   , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Result    (assertFailureResult   , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());
            Invoke_Func_RetriesExhausted_Result   (assertFailureResult   , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            static bool TryInvoke(Func<string> f, CancellationToken t, int n, ResultHandler<string> r, ExceptionHandler e, Func<int, TimeSpan> d, out string x)
            {
                object state = new object();
                return Reliably.TryInvoke(obj => { Assert.AreSame(obj, state); return f(); }, state, n, r, e, d.Invoke, out x);
            }
        }

        [TestMethod]
        public void TryInvoke_Func_Stateful_ResultPolicy_ComplexDelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(nullFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.TryInvoke(testFunc, new object(), -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, new object(), 10, null                          , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, new object(), 10, ResultPolicy.Default<string>(), null                     , (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null, out string _));

#nullable enable

            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue (TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(x      , a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => TryInvoke(f, t, n, r, e, d, out string _), x);

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), true);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertFailureResult   , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Result    (assertFailureResult   , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));
            Invoke_Func_RetriesExhausted_Result   (assertFailureResult   , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            static bool TryInvoke(Func<string> f, CancellationToken t, int n, ResultHandler<string> r, ExceptionHandler e, Func<int, string, Exception?, TimeSpan> d, out string x)
            {
                object state = new object();
                return Reliably.TryInvoke(obj => { Assert.AreSame(obj, state); return f(); }, state, n, r, e, d.Invoke, out x);
            }
        }

        [TestMethod]
        public void TryInvoke_Func_Stateful_Interruptable_DelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(nullFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.TryInvoke(testFunc, new object(), CancellationToken.None, -3, ExceptionPolicy.Transient, DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, new object(), CancellationToken.None, 10, null                     , DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, (DelayHandler)null, out string _));

#nullable enable

            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue (TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(x      , a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => TryInvoke(f, t, n, r, e, d, out string _), x);

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), false);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_Canceled_Delay            (assertError           , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_Canceled                  (assertError           , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());

            static bool TryInvoke(Func<string> f, CancellationToken t, int n, ResultHandler<string> r, ExceptionHandler e, Func<int, TimeSpan> d, out string x)
            {
                object state = new object();
                return Reliably.TryInvoke(obj => { Assert.AreSame(obj, state); return f(); }, state, t, n, e, d.Invoke, out x);
            }
        }

        [TestMethod]
        public void TryInvoke_Func_Stateful_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(nullFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.TryInvoke(testFunc, new object(), CancellationToken.None, -3, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, new object(), CancellationToken.None, 10, null                     , (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null, out string _));

#nullable enable

            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue (TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(x      , a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => TryInvoke(f, t, n, r, e, d, out string _), x);

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), false);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_Canceled_Delay            (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_Canceled                  (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());

            static bool TryInvoke(Func<string> f, CancellationToken t, int n, ResultHandler<string> r, ExceptionHandler e, Func<int, string, Exception?, TimeSpan> d, out string x)
            {
                object state = new object();
                return Reliably.TryInvoke(obj => { Assert.AreSame(obj, state); return f(); }, state, t, n, e, d.Invoke, out x);
            }
        }

        [TestMethod]
        public void TryInvoke_Func_Stateful_Interruptable_ResultPolicy_DelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(nullFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.TryInvoke(testFunc, new object(), CancellationToken.None, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, new object(), CancellationToken.None, 10, null                          , ExceptionPolicy.Transient, DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), null                     , DelayPolicy.None, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (DelayHandler)null, out string _));

#nullable enable

            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue (TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(x      , a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => TryInvoke(f, t, n, r, e, d, out string _), x);

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), true);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertFailureResult   , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Result    (assertFailureResult   , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());
            Invoke_Func_RetriesExhausted_Result   (assertFailureResult   , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_Canceled_Delay            (assertError           , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_Canceled                  (assertError           , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());

            static bool TryInvoke(Func<string> f, CancellationToken t, int n, ResultHandler<string> r, ExceptionHandler e, Func<int, TimeSpan> d, out string x)
            {
                object state = new object();
                return Reliably.TryInvoke(obj => { Assert.AreSame(obj, state); return f(); }, state, t, n, r, e, d.Invoke, out x);
            }
        }

        [TestMethod]
        public void TryInvoke_Func_Stateful_Interruptable_ResultPolicy_ComplexDelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(nullFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Reliably.TryInvoke(testFunc, new object(), CancellationToken.None, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, new object(), CancellationToken.None, 10, null                          , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), null                     , (i, r, e) => TimeSpan.Zero, out string _));
            Assert.ThrowsException<ArgumentNullException      >(() => Reliably.TryInvoke(testFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null, out string _));

#nullable enable

            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue (TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(x      , a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => { Assert.IsFalse(TryInvoke(f, t, n, r, e, d, out string a)); Assert.AreEqual(default, a); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsException(() => TryInvoke(f, t, n, r, e, d, out string _), x);

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), true);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertFailureResult   , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Result    (assertFailureResult   , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));
            Invoke_Func_RetriesExhausted_Result   (assertFailureResult   , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_Canceled_Delay            (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_Canceled                  (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());

            static bool TryInvoke(Func<string> f, CancellationToken t, int n, ResultHandler<string> r, ExceptionHandler e, Func<int, string, Exception?, TimeSpan> d, out string x)
            {
                object state = new object();
                return Reliably.TryInvoke(obj => { Assert.AreSame(obj, state); return f(); }, state, t, n, r, e, d.Invoke, out x);
            }
        }

        #endregion

        #region TryInvokeAsync

        [TestMethod]
        public async Task TryInvokeAsync_Func_DelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, 10, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, -3, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, 10, null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, 10, ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func  <Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d)    => Reliably.TryInvokeAsync(f, n, e, d.Invoke);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue(tryInvoke(f, t, n, r, e, d).Result.TryGetValue(out string? actual)); Assert.AreEqual(x, actual); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => tryInvoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), false);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
        }

        [TestMethod]
        public async Task TryInvokeAsync_Func_ComplexDelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, -3, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, 10, null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, 10, ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

#nullable enable

            Func  <Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d)    => Reliably.TryInvokeAsync(f, n, e, d.Invoke);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue(tryInvoke(f, t, n, r, e, d).Result.TryGetValue(out string? actual)); Assert.AreEqual(x, actual); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => tryInvoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), false);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
        }

        [TestMethod]
        public async Task TryInvokeAsync_Func_ResultPolicy_DelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, 10, null                          , ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, 10, ResultPolicy.Default<string>(), null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func  <Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d)    => Reliably.TryInvokeAsync(f, n, r, e, d.Invoke);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue(tryInvoke(f, t, n, r, e, d).Result.TryGetValue(out string? actual)); Assert.AreEqual(x, actual); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => tryInvoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), true);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertFailureResult   , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Result    (assertFailureResult   , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());
            Invoke_Func_RetriesExhausted_Result   (assertFailureResult   , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
        }

        [TestMethod]
        public async Task TryInvokeAsync_Func_ResultPolicy_ComplexDelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, 10, null                          , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, 10, ResultPolicy.Default<string>(), null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

#nullable enable

            Func  <Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d)    => Reliably.TryInvokeAsync(f, n, r, e, d.Invoke);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue(tryInvoke(f, t, n, r, e, d).Result.TryGetValue(out string? actual)); Assert.AreEqual(x, actual); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => tryInvoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), true);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertFailureResult   , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Result    (assertFailureResult   , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));
            Invoke_Func_RetriesExhausted_Result   (assertFailureResult   , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
        }

        [TestMethod]
        public async Task TryInvokeAsync_Func_Interruptable_DelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, -3, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, 10, null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func  <Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d)    => Reliably.TryInvokeAsync(f, t, n, e, d.Invoke);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue(tryInvoke(f, t, n, r, e, d).Result.TryGetValue(out string? actual)); Assert.AreEqual(x, actual); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => tryInvoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), false);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_Canceled_Delay            (assertError           , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_Canceled                  (assertError           , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public async Task TryInvokeAsync_Func_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, -3, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, 10, null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

#nullable enable

            Func  <Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d)    => Reliably.TryInvokeAsync(f, t, n, e, d.Invoke);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue(tryInvoke(f, t, n, r, e, d).Result.TryGetValue(out string? actual)); Assert.AreEqual(x, actual); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => tryInvoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), false);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_Canceled_Delay            (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_Canceled                  (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        [TestMethod]
        public async Task TryInvokeAsync_Func_Interruptable_ResultPolicy_DelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, 10, null                          , ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func  <Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d)    => Reliably.TryInvokeAsync(f, t, n, r, e, d.Invoke);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue(tryInvoke(f, t, n, r, e, d).Result.TryGetValue(out string? actual)); Assert.AreEqual(x, actual); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => tryInvoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), true);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertFailureResult   , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Result    (assertFailureResult   , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());
            Invoke_Func_RetriesExhausted_Result   (assertFailureResult   , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_Canceled_Delay            (assertError           , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_Canceled                  (assertError           , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public async Task TryInvokeAsync_Func_Interruptable_ResultPolicy_ComplexDelayHandler()
        {
#nullable disable

            Func<string> nullFunc = null;
            Func<string> testFunc = () => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, 10, null                          , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

#nullable enable

            Func  <Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d)    => Reliably.TryInvokeAsync(f, t, n, r, e, d.Invoke);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue(tryInvoke(f, t, n, r, e, d).Result.TryGetValue(out string? actual)); Assert.AreEqual(x, actual); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => tryInvoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), true);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertFailureResult   , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Result    (assertFailureResult   , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));
            Invoke_Func_RetriesExhausted_Result   (assertFailureResult   , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_Canceled_Delay            (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_Canceled                  (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        [TestMethod]
        public async Task TryInvokeAsync_Func_Stateful_DelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, new object(), 10, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, new object(), -3, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), 10, null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), 10, ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, string> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return f();
                };

                return Reliably.TryInvokeAsync(inputFunc, state, n, e, d.Invoke);
            };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue(tryInvoke(f, t, n, r, e, d).Result.TryGetValue(out string? actual)); Assert.AreEqual(x, actual); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => tryInvoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), false);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
        }

        [TestMethod]
        public async Task TryInvokeAsync_Func_Stateful_ComplexDelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, new object(), 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, new object(), -3, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), 10, null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), 10, ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, string> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return f();
                };

                return Reliably.TryInvokeAsync(inputFunc, state, n, e, d.Invoke);
            };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue(tryInvoke(f, t, n, r, e, d).Result.TryGetValue(out string? actual)); Assert.AreEqual(x, actual); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => tryInvoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), false);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
        }

        [TestMethod]
        public async Task TryInvokeAsync_Func_Stateful_ResultPolicy_DelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, new object(), -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), 10, null                          , ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), 10, ResultPolicy.Default<string>(), null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, string> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return f();
                };

                return Reliably.TryInvokeAsync(inputFunc, state, n, r, e, d.Invoke);
            };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue(tryInvoke(f, t, n, r, e, d).Result.TryGetValue(out string? actual)); Assert.AreEqual(x, actual); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => tryInvoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), true);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertFailureResult   , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Result    (assertFailureResult   , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());
            Invoke_Func_RetriesExhausted_Result   (assertFailureResult   , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
        }

        [TestMethod]
        public async Task TryInvokeAsync_Func_Stateful_ResultPolicy_ComplexDelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, new object(), -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), 10, null                          , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), 10, ResultPolicy.Default<string>(), null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, string> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return f();
                };

                return Reliably.TryInvokeAsync(inputFunc, state, n, r, e, d.Invoke);
            };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue(tryInvoke(f, t, n, r, e, d).Result.TryGetValue(out string? actual)); Assert.AreEqual(x, actual); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => tryInvoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), true);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertFailureResult   , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Result    (assertFailureResult   , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));
            Invoke_Func_RetriesExhausted_Result   (assertFailureResult   , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
        }

        [TestMethod]
        public async Task TryInvokeAsync_Func_Stateful_Interruptable_DelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, -3, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, 10, null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, string> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return f();
                };

                return Reliably.TryInvokeAsync(inputFunc, state, t, n, e, d.Invoke);
            };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue(tryInvoke(f, t, n, r, e, d).Result.TryGetValue(out string? actual)); Assert.AreEqual(x, actual); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => tryInvoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), false);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_Canceled_Delay            (assertError           , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), false);
            Invoke_Func_Canceled                  (assertError           , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public async Task TryInvokeAsync_Func_Stateful_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, -3, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, 10, null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, string> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return f();
                };

                return Reliably.TryInvokeAsync(inputFunc, state, t, n, e, d.Invoke);
            };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue(tryInvoke(f, t, n, r, e, d).Result.TryGetValue(out string? actual)); Assert.AreEqual(x, actual); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => tryInvoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), false);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_Canceled_Delay            (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.OnlyExceptionAsc<string>(t), false);
            Invoke_Func_Canceled                  (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        [TestMethod]
        public async Task TryInvokeAsync_Func_Stateful_Interruptable_ResultPolicy_DelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, 10, null                          , ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, string> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return f();
                };

                return Reliably.TryInvokeAsync(inputFunc, state, t, n, r, e, d.Invoke);
            };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue(tryInvoke(f, t, n, r, e, d).Result.TryGetValue(out string? actual)); Assert.AreEqual(x, actual); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => tryInvoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>(), true);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertFailureResult   , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Result    (assertFailureResult   , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());
            Invoke_Func_RetriesExhausted_Result   (assertFailureResult   , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc());

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_Canceled_Delay            (assertError           , t => new FuncProxy<int, TimeSpan>(i => t), (d, r, t) => d.Invoking += Expect.Asc(), true);
            Invoke_Func_Canceled                  (assertError           , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public async Task TryInvokeAsync_Func_Stateful_Interruptable_ResultPolicy_ComplexDelayHandler()
        {
#nullable disable

            Func<object, string> nullFunc = null;
            Func<object, string> testFunc = s => "Hello World";
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, 10, null                          , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, string> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return f();
                };

                return Reliably.TryInvokeAsync(inputFunc, state, t, n, r, e, d.Invoke);
            };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertSuccess          = (f, t, n, r, e, d, x) => { Assert.IsTrue(tryInvoke(f, t, n, r, e, d).Result.TryGetValue(out string? actual)); Assert.AreEqual(x, actual); };
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, string> assertFailureResult    = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertFailureException = (f, t, n, r, e, d, x) => Assert.IsFalse(tryInvoke(f, t, n, r, e, d).Result.HasValue);
            Action<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Type>   assertError            = (f, t, n, r, e, d, x) => Assert.That.ThrowsExceptionAsync(() => tryInvoke(f, t, n, r, e, d), x).Wait();

            // Success
            Invoke_Func_Success                   (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>(), true);
            Invoke_Func_EventualSuccess           (assertSuccess         , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Failure (Result)
            Invoke_Func_Failure_Result            (assertFailureResult   , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Result    (assertFailureResult   , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));
            Invoke_Func_RetriesExhausted_Result   (assertFailureResult   , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t));

            // Failure (Exception)
            Invoke_Func_Failure_Exception         (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
            Invoke_Func_EventualFailure_Exception (assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_RetriesExhausted_Exception(assertFailureException, t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);

            // Cancellation
            Invoke_Func_Canceled_Delegate         (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_Canceled_Delay            (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t), (d, r, t) => d.Invoking += Expect.AlternatingAsc(r, t), true);
            Invoke_Func_Canceled                  (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        #endregion
    }
}

