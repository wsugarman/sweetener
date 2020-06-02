// Generated from Reliably.Async.Test.tt
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    partial class ReliablyTest
    {
        #region InvokeAsync

        [TestMethod]
        public async Task InvokeAsync_AsyncFunc_DelayHandler()
        {
#nullable disable

            Func<Task<string>> nullFunc = null;
            Func<Task<string>> testFunc = () => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, 10, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, -3, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, 10, null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, 10, ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

            Func<Task<string>> nullTaskFunc = () => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.InvokeAsync(nullTaskFunc, 10, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d) =>
            {
                Func<Task<string>> inputFunc = () =>
                {
                    return Task.FromResult(f());
                };

                return Reliably.InvokeAsync(inputFunc, n, e, d.Invoke);
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
            Invoke_Func_Canceled_NoTask           (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public async Task InvokeAsync_AsyncFunc_ComplexDelayHandler()
        {
#nullable disable

            Func<Task<string>> nullFunc = null;
            Func<Task<string>> testFunc = () => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync<string>(nullFunc, 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync<string>(testFunc, -3, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync<string>(testFunc, 10, null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync        (testFunc, 10, ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

            Func<Task<string>> nullTaskFunc = () => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.InvokeAsync<string>(nullTaskFunc, 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d) =>
            {
                Func<Task<string>> inputFunc = () =>
                {
                    return Task.FromResult(f());
                };

                return Reliably.InvokeAsync(inputFunc, n, e, d.Invoke);
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
            Invoke_Func_Canceled_NoTask           (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        [TestMethod]
        public async Task InvokeAsync_AsyncFunc_ResultPolicy_DelayHandler()
        {
#nullable disable

            Func<Task<string>> nullFunc = null;
            Func<Task<string>> testFunc = () => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, 10, (ResultHandler<string>)null   , ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, 10, ResultPolicy.Default<string>(), null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

            Func<Task<string>> nullTaskFunc = () => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.InvokeAsync(nullTaskFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d) =>
            {
                Func<Task<string>> inputFunc = () =>
                {
                    return Task.FromResult(f());
                };

                return Reliably.InvokeAsync(inputFunc, n, r, e, d.Invoke);
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
            Invoke_Func_Canceled_NoTask           (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public async Task InvokeAsync_AsyncFunc_ResultPolicy_ComplexDelayHandler()
        {
#nullable disable

            Func<Task<string>> nullFunc = null;
            Func<Task<string>> testFunc = () => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, 10, (ResultHandler<string>)null   , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, 10, ResultPolicy.Default<string>(), null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

            Func<Task<string>> nullTaskFunc = () => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.InvokeAsync(nullTaskFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d) =>
            {
                Func<Task<string>> inputFunc = () =>
                {
                    return Task.FromResult(f());
                };

                return Reliably.InvokeAsync(inputFunc, n, r, e, d.Invoke);
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
            Invoke_Func_Canceled_NoTask           (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        [TestMethod]
        public async Task InvokeAsync_AsyncFunc_Interruptable_DelayHandler()
        {
#nullable disable

            Func<Task<string>> nullFunc = null;
            Func<Task<string>> testFunc = () => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, -3, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, 10, null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

            Func<Task<string>> nullTaskFunc = () => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.InvokeAsync(nullTaskFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d) =>
            {
                Func<Task<string>> inputFunc = () =>
                {
                    return Task.FromResult(f());
                };

                return Reliably.InvokeAsync(inputFunc, t, n, e, d.Invoke);
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
            Invoke_Func_Canceled_NoTask           (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public async Task InvokeAsync_AsyncFunc_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Func<Task<string>> nullFunc = null;
            Func<Task<string>> testFunc = () => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync<string>(nullFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync<string>(testFunc, CancellationToken.None, -3, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync<string>(testFunc, CancellationToken.None, 10, null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync        (testFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

            Func<Task<string>> nullTaskFunc = () => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.InvokeAsync<string>(nullTaskFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d) =>
            {
                Func<Task<string>> inputFunc = () =>
                {
                    return Task.FromResult(f());
                };

                return Reliably.InvokeAsync(inputFunc, t, n, e, d.Invoke);
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
            Invoke_Func_Canceled_NoTask           (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        [TestMethod]
        public async Task InvokeAsync_AsyncFunc_Interruptable_ResultPolicy_DelayHandler()
        {
#nullable disable

            Func<Task<string>> nullFunc = null;
            Func<Task<string>> testFunc = () => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, 10, (ResultHandler<string>)null   , ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

            Func<Task<string>> nullTaskFunc = () => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.InvokeAsync(nullTaskFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d) =>
            {
                Func<Task<string>> inputFunc = () =>
                {
                    return Task.FromResult(f());
                };

                return Reliably.InvokeAsync(inputFunc, t, n, r, e, d.Invoke);
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
            Invoke_Func_Canceled_NoTask           (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public async Task InvokeAsync_AsyncFunc_Interruptable_ResultPolicy_ComplexDelayHandler()
        {
#nullable disable

            Func<Task<string>> nullFunc = null;
            Func<Task<string>> testFunc = () => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, 10, (ResultHandler<string>)null   , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

            Func<Task<string>> nullTaskFunc = () => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.InvokeAsync(nullTaskFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d) =>
            {
                Func<Task<string>> inputFunc = () =>
                {
                    return Task.FromResult(f());
                };

                return Reliably.InvokeAsync(inputFunc, t, n, r, e, d.Invoke);
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
            Invoke_Func_Canceled_NoTask           (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        [TestMethod]
        public async Task InvokeAsync_AsyncFunc_Stateful_DelayHandler()
        {
#nullable disable

            Func<object, Task<string>> nullFunc = null;
            Func<object, Task<string>> testFunc = s => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, new object(), 10, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, new object(), -3, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), 10, null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), 10, ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

            Func<object, Task<string>> nullTaskFunc = s => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.InvokeAsync(nullTaskFunc, new object(), 10, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, Task<string>> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return Task.FromResult(f());
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
            Invoke_Func_Canceled_NoTask           (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public async Task InvokeAsync_AsyncFunc_Stateful_ComplexDelayHandler()
        {
#nullable disable

            Func<object, Task<string>> nullFunc = null;
            Func<object, Task<string>> testFunc = s => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync<object, string>(nullFunc, new object(), 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync<object, string>(testFunc, new object(), -3, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync<object, string>(testFunc, new object(), 10, null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync                (testFunc, new object(), 10, ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

            Func<object, Task<string>> nullTaskFunc = s => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.InvokeAsync<object, string>(nullTaskFunc, new object(), 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, Task<string>> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return Task.FromResult(f());
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
            Invoke_Func_Canceled_NoTask           (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        [TestMethod]
        public async Task InvokeAsync_AsyncFunc_Stateful_ResultPolicy_DelayHandler()
        {
#nullable disable

            Func<object, Task<string>> nullFunc = null;
            Func<object, Task<string>> testFunc = s => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, new object(), -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), 10, (ResultHandler<string>)null   , ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), 10, ResultPolicy.Default<string>(), null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

            Func<object, Task<string>> nullTaskFunc = s => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.InvokeAsync(nullTaskFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, Task<string>> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return Task.FromResult(f());
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
            Invoke_Func_Canceled_NoTask           (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public async Task InvokeAsync_AsyncFunc_Stateful_ResultPolicy_ComplexDelayHandler()
        {
#nullable disable

            Func<object, Task<string>> nullFunc = null;
            Func<object, Task<string>> testFunc = s => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, new object(), -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), 10, (ResultHandler<string>)null   , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), 10, ResultPolicy.Default<string>(), null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

            Func<object, Task<string>> nullTaskFunc = s => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.InvokeAsync(nullTaskFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, Task<string>> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return Task.FromResult(f());
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
            Invoke_Func_Canceled_NoTask           (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        [TestMethod]
        public async Task InvokeAsync_AsyncFunc_Stateful_Interruptable_DelayHandler()
        {
#nullable disable

            Func<object, Task<string>> nullFunc = null;
            Func<object, Task<string>> testFunc = s => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, -3, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, 10, null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

            Func<object, Task<string>> nullTaskFunc = s => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.InvokeAsync(nullTaskFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, Task<string>> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return Task.FromResult(f());
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
            Invoke_Func_Canceled_NoTask           (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public async Task InvokeAsync_AsyncFunc_Stateful_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Func<object, Task<string>> nullFunc = null;
            Func<object, Task<string>> testFunc = s => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync<object, string>(nullFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync<object, string>(testFunc, new object(), CancellationToken.None, -3, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync<object, string>(testFunc, new object(), CancellationToken.None, 10, null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync                (testFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

            Func<object, Task<string>> nullTaskFunc = s => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.InvokeAsync<object, string>(nullTaskFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, Task<string>> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return Task.FromResult(f());
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
            Invoke_Func_Canceled_NoTask           (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        [TestMethod]
        public async Task InvokeAsync_AsyncFunc_Stateful_Interruptable_ResultPolicy_DelayHandler()
        {
#nullable disable

            Func<object, Task<string>> nullFunc = null;
            Func<object, Task<string>> testFunc = s => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, 10, (ResultHandler<string>)null   , ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

            Func<object, Task<string>> nullTaskFunc = s => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.InvokeAsync(nullTaskFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, Task<string>> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return Task.FromResult(f());
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
            Invoke_Func_Canceled_NoTask           (assertError , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public async Task InvokeAsync_AsyncFunc_Stateful_Interruptable_ResultPolicy_ComplexDelayHandler()
        {
#nullable disable

            Func<object, Task<string>> nullFunc = null;
            Func<object, Task<string>> testFunc = s => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(nullFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, 10, (ResultHandler<string>)null   , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.InvokeAsync(testFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

            Func<object, Task<string>> nullTaskFunc = s => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.InvokeAsync(nullTaskFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<string>> invoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, Task<string>> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return Task.FromResult(f());
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
            Invoke_Func_Canceled_NoTask           (assertError , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        #endregion

        #region TryInvokeAsync

        [TestMethod]
        public async Task TryInvokeAsync_AsyncFunc_DelayHandler()
        {
#nullable disable

            Func<Task<string>> nullFunc = null;
            Func<Task<string>> testFunc = () => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, 10, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, -3, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, 10, null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, 10, ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

            Func<Task<string>> nullTaskFunc = () => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.TryInvokeAsync(nullTaskFunc, 10, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d) =>
            {
                Func<Task<string>> inputFunc = () =>
                {
                    return Task.FromResult(f());
                };

                return Reliably.TryInvokeAsync(inputFunc, n, e, d.Invoke);
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
            Invoke_Func_Canceled_NoTask           (assertError           , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public async Task TryInvokeAsync_AsyncFunc_ComplexDelayHandler()
        {
#nullable disable

            Func<Task<string>> nullFunc = null;
            Func<Task<string>> testFunc = () => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync<string>(nullFunc, 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync<string>(testFunc, -3, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync<string>(testFunc, 10, null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync        (testFunc, 10, ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

            Func<Task<string>> nullTaskFunc = () => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.TryInvokeAsync<string>(nullTaskFunc, 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d) =>
            {
                Func<Task<string>> inputFunc = () =>
                {
                    return Task.FromResult(f());
                };

                return Reliably.TryInvokeAsync(inputFunc, n, e, d.Invoke);
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
            Invoke_Func_Canceled_NoTask           (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        [TestMethod]
        public async Task TryInvokeAsync_AsyncFunc_ResultPolicy_DelayHandler()
        {
#nullable disable

            Func<Task<string>> nullFunc = null;
            Func<Task<string>> testFunc = () => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, 10, (ResultHandler<string>)null   , ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, 10, ResultPolicy.Default<string>(), null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

            Func<Task<string>> nullTaskFunc = () => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.TryInvokeAsync(nullTaskFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d) =>
            {
                Func<Task<string>> inputFunc = () =>
                {
                    return Task.FromResult(f());
                };

                return Reliably.TryInvokeAsync(inputFunc, n, r, e, d.Invoke);
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
            Invoke_Func_Canceled_NoTask           (assertError           , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public async Task TryInvokeAsync_AsyncFunc_ResultPolicy_ComplexDelayHandler()
        {
#nullable disable

            Func<Task<string>> nullFunc = null;
            Func<Task<string>> testFunc = () => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, 10, (ResultHandler<string>)null   , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, 10, ResultPolicy.Default<string>(), null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

            Func<Task<string>> nullTaskFunc = () => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.TryInvokeAsync(nullTaskFunc, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d) =>
            {
                Func<Task<string>> inputFunc = () =>
                {
                    return Task.FromResult(f());
                };

                return Reliably.TryInvokeAsync(inputFunc, n, r, e, d.Invoke);
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
            Invoke_Func_Canceled_NoTask           (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        [TestMethod]
        public async Task TryInvokeAsync_AsyncFunc_Interruptable_DelayHandler()
        {
#nullable disable

            Func<Task<string>> nullFunc = null;
            Func<Task<string>> testFunc = () => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, -3, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, 10, null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

            Func<Task<string>> nullTaskFunc = () => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.TryInvokeAsync(nullTaskFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d) =>
            {
                Func<Task<string>> inputFunc = () =>
                {
                    return Task.FromResult(f());
                };

                return Reliably.TryInvokeAsync(inputFunc, t, n, e, d.Invoke);
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
            Invoke_Func_Canceled_NoTask           (assertError           , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public async Task TryInvokeAsync_AsyncFunc_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Func<Task<string>> nullFunc = null;
            Func<Task<string>> testFunc = () => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync<string>(nullFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync<string>(testFunc, CancellationToken.None, -3, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync<string>(testFunc, CancellationToken.None, 10, null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync        (testFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

            Func<Task<string>> nullTaskFunc = () => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.TryInvokeAsync<string>(nullTaskFunc, CancellationToken.None, 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d) =>
            {
                Func<Task<string>> inputFunc = () =>
                {
                    return Task.FromResult(f());
                };

                return Reliably.TryInvokeAsync(inputFunc, t, n, e, d.Invoke);
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
            Invoke_Func_Canceled_NoTask           (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        [TestMethod]
        public async Task TryInvokeAsync_AsyncFunc_Interruptable_ResultPolicy_DelayHandler()
        {
#nullable disable

            Func<Task<string>> nullFunc = null;
            Func<Task<string>> testFunc = () => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, 10, (ResultHandler<string>)null   , ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

            Func<Task<string>> nullTaskFunc = () => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.TryInvokeAsync(nullTaskFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d) =>
            {
                Func<Task<string>> inputFunc = () =>
                {
                    return Task.FromResult(f());
                };

                return Reliably.TryInvokeAsync(inputFunc, t, n, r, e, d.Invoke);
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
            Invoke_Func_Canceled_NoTask           (assertError           , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public async Task TryInvokeAsync_AsyncFunc_Interruptable_ResultPolicy_ComplexDelayHandler()
        {
#nullable disable

            Func<Task<string>> nullFunc = null;
            Func<Task<string>> testFunc = () => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, 10, (ResultHandler<string>)null   , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

            Func<Task<string>> nullTaskFunc = () => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.TryInvokeAsync(nullTaskFunc, CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d) =>
            {
                Func<Task<string>> inputFunc = () =>
                {
                    return Task.FromResult(f());
                };

                return Reliably.TryInvokeAsync(inputFunc, t, n, r, e, d.Invoke);
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
            Invoke_Func_Canceled_NoTask           (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        [TestMethod]
        public async Task TryInvokeAsync_AsyncFunc_Stateful_DelayHandler()
        {
#nullable disable

            Func<object, Task<string>> nullFunc = null;
            Func<object, Task<string>> testFunc = s => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, new object(), 10, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, new object(), -3, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), 10, null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), 10, ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

            Func<object, Task<string>> nullTaskFunc = s => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.TryInvokeAsync(nullTaskFunc, new object(), 10, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, Task<string>> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return Task.FromResult(f());
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
            Invoke_Func_Canceled_NoTask           (assertError           , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public async Task TryInvokeAsync_AsyncFunc_Stateful_ComplexDelayHandler()
        {
#nullable disable

            Func<object, Task<string>> nullFunc = null;
            Func<object, Task<string>> testFunc = s => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync<object, string>(nullFunc, new object(), 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync<object, string>(testFunc, new object(), -3, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync<object, string>(testFunc, new object(), 10, null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync                (testFunc, new object(), 10, ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

            Func<object, Task<string>> nullTaskFunc = s => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.TryInvokeAsync<object, string>(nullTaskFunc, new object(), 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, Task<string>> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return Task.FromResult(f());
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
            Invoke_Func_Canceled_NoTask           (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        [TestMethod]
        public async Task TryInvokeAsync_AsyncFunc_Stateful_ResultPolicy_DelayHandler()
        {
#nullable disable

            Func<object, Task<string>> nullFunc = null;
            Func<object, Task<string>> testFunc = s => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, new object(), -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), 10, (ResultHandler<string>)null   , ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), 10, ResultPolicy.Default<string>(), null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

            Func<object, Task<string>> nullTaskFunc = s => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.TryInvokeAsync(nullTaskFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, Task<string>> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return Task.FromResult(f());
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
            Invoke_Func_Canceled_NoTask           (assertError           , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public async Task TryInvokeAsync_AsyncFunc_Stateful_ResultPolicy_ComplexDelayHandler()
        {
#nullable disable

            Func<object, Task<string>> nullFunc = null;
            Func<object, Task<string>> testFunc = s => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, new object(), -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), 10, (ResultHandler<string>)null   , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), 10, ResultPolicy.Default<string>(), null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

            Func<object, Task<string>> nullTaskFunc = s => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.TryInvokeAsync(nullTaskFunc, new object(), 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, Task<string>> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return Task.FromResult(f());
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
            Invoke_Func_Canceled_NoTask           (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        [TestMethod]
        public async Task TryInvokeAsync_AsyncFunc_Stateful_Interruptable_DelayHandler()
        {
#nullable disable

            Func<object, Task<string>> nullFunc = null;
            Func<object, Task<string>> testFunc = s => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, -3, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, 10, null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

            Func<object, Task<string>> nullTaskFunc = s => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.TryInvokeAsync(nullTaskFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, Task<string>> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return Task.FromResult(f());
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
            Invoke_Func_Canceled_NoTask           (assertError           , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public async Task TryInvokeAsync_AsyncFunc_Stateful_Interruptable_ComplexDelayHandler()
        {
#nullable disable

            Func<object, Task<string>> nullFunc = null;
            Func<object, Task<string>> testFunc = s => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync<object, string>(nullFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync<object, string>(testFunc, new object(), CancellationToken.None, -3, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync<object, string>(testFunc, new object(), CancellationToken.None, 10, null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync                (testFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

            Func<object, Task<string>> nullTaskFunc = s => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.TryInvokeAsync<object, string>(nullTaskFunc, new object(), CancellationToken.None, 10, ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, Task<string>> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return Task.FromResult(f());
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
            Invoke_Func_Canceled_NoTask           (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        [TestMethod]
        public async Task TryInvokeAsync_AsyncFunc_Stateful_Interruptable_ResultPolicy_DelayHandler()
        {
#nullable disable

            Func<object, Task<string>> nullFunc = null;
            Func<object, Task<string>> testFunc = s => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, 10, (ResultHandler<string>)null   , ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), null                     , DelayPolicy.None)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (DelayHandler)null)).ConfigureAwait(false);

            Func<object, Task<string>> nullTaskFunc = s => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.TryInvokeAsync(nullTaskFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, DelayPolicy.None)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, Task<string>> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return Task.FromResult(f());
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
            Invoke_Func_Canceled_NoTask           (assertError           , t => new FuncProxy<int, TimeSpan>(i => t),  d        => d.Invoking += Expect.Nothing<int>());
        }

        [TestMethod]
        public async Task TryInvokeAsync_AsyncFunc_Stateful_Interruptable_ResultPolicy_ComplexDelayHandler()
        {
#nullable disable

            Func<object, Task<string>> nullFunc = null;
            Func<object, Task<string>> testFunc = s => Task.FromResult("Hello World");
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(nullFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, -3, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, 10, (ResultHandler<string>)null   , ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), null                     , (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);
            await Assert.ThrowsExceptionAsync<ArgumentNullException      >(() => Reliably.TryInvokeAsync(testFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (ComplexDelayHandler<string>)null)).ConfigureAwait(false);

            Func<object, Task<string>> nullTaskFunc = s => null;
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Reliably.TryInvokeAsync(nullTaskFunc, new object(), CancellationToken.None, 10, ResultPolicy.Default<string>(), ExceptionPolicy.Transient, (i, r, e) => TimeSpan.Zero)).ConfigureAwait(false);

#nullable enable

            Func<Func<string>, CancellationToken, int, ResultHandler<string>, ExceptionHandler, Func<int, string, Exception?, TimeSpan>, Task<Optional<string>>> tryInvoke = (f, t, n, r, e, d) =>
            {
                object state = new object();
                Func<object, Task<string>> inputFunc = (obj) =>
                {
                    Assert.AreSame(obj, state);
                    return Task.FromResult(f());
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
            Invoke_Func_Canceled_NoTask           (assertError           , t => new FuncProxy<int, string, Exception?, TimeSpan>((i, r, e) => t),  d        => d.Invoking += Expect.Nothing<int, string, Exception?>());
        }

        #endregion
    }
}

