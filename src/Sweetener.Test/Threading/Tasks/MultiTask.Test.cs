// Copyright © William Sugarman.
// Licensed under the MIT License.

// Do not modify this file. It was automatically generated from MultiTask.Test.tt

using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Threading.Tasks.Test
{
    [TestClass]
    public class MultiTaskTest
    {
        [TestMethod]
        public async Task WhenAllT2()
        {
            // Bad Input
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    (Task<int>)null!,
                    Task.FromResult("Hello World"))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    (Task<string>)null!)).ConfigureAwait(false);

            // Success
            (int value1, string value2) = await MultiTask.WhenAll(
                Task.FromResult(42),
                Task.FromResult("Hello World")).ConfigureAwait(false);

            Assert.AreEqual(42           , value1);
            Assert.AreEqual("Hello World", value2);
        }

        [TestMethod]
        public async Task WhenAllT3()
        {
            // Bad Input
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    (Task<int>)null!,
                    Task.FromResult("Hello World"),
                    Task.FromResult(TimeSpan.FromHours(3)))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    (Task<string>)null!,
                    Task.FromResult(TimeSpan.FromHours(3)))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    Task.FromResult("Hello World"),
                    (Task<TimeSpan>)null!)).ConfigureAwait(false);

            // Success
            (int value1, string value2, TimeSpan value3) = await MultiTask.WhenAll(
                Task.FromResult(42),
                Task.FromResult("Hello World"),
                Task.FromResult(TimeSpan.FromHours(3))).ConfigureAwait(false);

            Assert.AreEqual(42                   , value1);
            Assert.AreEqual("Hello World"        , value2);
            Assert.AreEqual(TimeSpan.FromHours(3), value3);
        }

        [TestMethod]
        public async Task WhenAllT4()
        {
            // Bad Input
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    (Task<int>)null!,
                    Task.FromResult("Hello World"),
                    Task.FromResult(TimeSpan.FromHours(3)),
                    Task.FromResult(3.14d))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    (Task<string>)null!,
                    Task.FromResult(TimeSpan.FromHours(3)),
                    Task.FromResult(3.14d))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    Task.FromResult("Hello World"),
                    (Task<TimeSpan>)null!,
                    Task.FromResult(3.14d))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    Task.FromResult("Hello World"),
                    Task.FromResult(TimeSpan.FromHours(3)),
                    (Task<double>)null!)).ConfigureAwait(false);

            // Success
            (int value1, string value2, TimeSpan value3, double value4) = await MultiTask.WhenAll(
                Task.FromResult(42),
                Task.FromResult("Hello World"),
                Task.FromResult(TimeSpan.FromHours(3)),
                Task.FromResult(3.14d)).ConfigureAwait(false);

            Assert.AreEqual(42                   , value1);
            Assert.AreEqual("Hello World"        , value2);
            Assert.AreEqual(TimeSpan.FromHours(3), value3);
            Assert.AreEqual(3.14d                , value4);
        }

        [TestMethod]
        public async Task WhenAllT5()
        {
            // Bad Input
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    (Task<int>)null!,
                    Task.FromResult("Hello World"),
                    Task.FromResult(TimeSpan.FromHours(3)),
                    Task.FromResult(3.14d),
                    Task.FromResult(100L))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    (Task<string>)null!,
                    Task.FromResult(TimeSpan.FromHours(3)),
                    Task.FromResult(3.14d),
                    Task.FromResult(100L))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    Task.FromResult("Hello World"),
                    (Task<TimeSpan>)null!,
                    Task.FromResult(3.14d),
                    Task.FromResult(100L))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    Task.FromResult("Hello World"),
                    Task.FromResult(TimeSpan.FromHours(3)),
                    (Task<double>)null!,
                    Task.FromResult(100L))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    Task.FromResult("Hello World"),
                    Task.FromResult(TimeSpan.FromHours(3)),
                    Task.FromResult(3.14d),
                    (Task<long>)null!)).ConfigureAwait(false);

            // Success
            (int value1, string value2, TimeSpan value3, double value4, long value5) = await MultiTask.WhenAll(
                Task.FromResult(42),
                Task.FromResult("Hello World"),
                Task.FromResult(TimeSpan.FromHours(3)),
                Task.FromResult(3.14d),
                Task.FromResult(100L)).ConfigureAwait(false);

            Assert.AreEqual(42                   , value1);
            Assert.AreEqual("Hello World"        , value2);
            Assert.AreEqual(TimeSpan.FromHours(3), value3);
            Assert.AreEqual(3.14d                , value4);
            Assert.AreEqual(100L                 , value5);
        }

        [TestMethod]
        public async Task WhenAllT6()
        {
            // Bad Input
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    (Task<int>)null!,
                    Task.FromResult("Hello World"),
                    Task.FromResult(TimeSpan.FromHours(3)),
                    Task.FromResult(3.14d),
                    Task.FromResult(100L),
                    Task.FromResult('a'))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    (Task<string>)null!,
                    Task.FromResult(TimeSpan.FromHours(3)),
                    Task.FromResult(3.14d),
                    Task.FromResult(100L),
                    Task.FromResult('a'))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    Task.FromResult("Hello World"),
                    (Task<TimeSpan>)null!,
                    Task.FromResult(3.14d),
                    Task.FromResult(100L),
                    Task.FromResult('a'))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    Task.FromResult("Hello World"),
                    Task.FromResult(TimeSpan.FromHours(3)),
                    (Task<double>)null!,
                    Task.FromResult(100L),
                    Task.FromResult('a'))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    Task.FromResult("Hello World"),
                    Task.FromResult(TimeSpan.FromHours(3)),
                    Task.FromResult(3.14d),
                    (Task<long>)null!,
                    Task.FromResult('a'))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    Task.FromResult("Hello World"),
                    Task.FromResult(TimeSpan.FromHours(3)),
                    Task.FromResult(3.14d),
                    Task.FromResult(100L),
                    (Task<char>)null!)).ConfigureAwait(false);

            // Success
            (int value1, string value2, TimeSpan value3, double value4, long value5, char value6) = await MultiTask.WhenAll(
                Task.FromResult(42),
                Task.FromResult("Hello World"),
                Task.FromResult(TimeSpan.FromHours(3)),
                Task.FromResult(3.14d),
                Task.FromResult(100L),
                Task.FromResult('a')).ConfigureAwait(false);

            Assert.AreEqual(42                   , value1);
            Assert.AreEqual("Hello World"        , value2);
            Assert.AreEqual(TimeSpan.FromHours(3), value3);
            Assert.AreEqual(3.14d                , value4);
            Assert.AreEqual(100L                 , value5);
            Assert.AreEqual('a'                  , value6);
        }

        [TestMethod]
        public async Task WhenAllT7()
        {
            // Bad Input
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    (Task<int>)null!,
                    Task.FromResult("Hello World"),
                    Task.FromResult(TimeSpan.FromHours(3)),
                    Task.FromResult(3.14d),
                    Task.FromResult(100L),
                    Task.FromResult('a'),
                    Task.FromResult(Guid.Parse("56128c75-379f-4c24-ac02-7ceb335807af")))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    (Task<string>)null!,
                    Task.FromResult(TimeSpan.FromHours(3)),
                    Task.FromResult(3.14d),
                    Task.FromResult(100L),
                    Task.FromResult('a'),
                    Task.FromResult(Guid.Parse("56128c75-379f-4c24-ac02-7ceb335807af")))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    Task.FromResult("Hello World"),
                    (Task<TimeSpan>)null!,
                    Task.FromResult(3.14d),
                    Task.FromResult(100L),
                    Task.FromResult('a'),
                    Task.FromResult(Guid.Parse("56128c75-379f-4c24-ac02-7ceb335807af")))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    Task.FromResult("Hello World"),
                    Task.FromResult(TimeSpan.FromHours(3)),
                    (Task<double>)null!,
                    Task.FromResult(100L),
                    Task.FromResult('a'),
                    Task.FromResult(Guid.Parse("56128c75-379f-4c24-ac02-7ceb335807af")))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    Task.FromResult("Hello World"),
                    Task.FromResult(TimeSpan.FromHours(3)),
                    Task.FromResult(3.14d),
                    (Task<long>)null!,
                    Task.FromResult('a'),
                    Task.FromResult(Guid.Parse("56128c75-379f-4c24-ac02-7ceb335807af")))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    Task.FromResult("Hello World"),
                    Task.FromResult(TimeSpan.FromHours(3)),
                    Task.FromResult(3.14d),
                    Task.FromResult(100L),
                    (Task<char>)null!,
                    Task.FromResult(Guid.Parse("56128c75-379f-4c24-ac02-7ceb335807af")))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    Task.FromResult("Hello World"),
                    Task.FromResult(TimeSpan.FromHours(3)),
                    Task.FromResult(3.14d),
                    Task.FromResult(100L),
                    Task.FromResult('a'),
                    (Task<Guid>)null!)).ConfigureAwait(false);

            // Success
            (int value1, string value2, TimeSpan value3, double value4, long value5, char value6, Guid value7) = await MultiTask.WhenAll(
                Task.FromResult(42),
                Task.FromResult("Hello World"),
                Task.FromResult(TimeSpan.FromHours(3)),
                Task.FromResult(3.14d),
                Task.FromResult(100L),
                Task.FromResult('a'),
                Task.FromResult(Guid.Parse("56128c75-379f-4c24-ac02-7ceb335807af"))).ConfigureAwait(false);

            Assert.AreEqual(42                                                , value1);
            Assert.AreEqual("Hello World"                                     , value2);
            Assert.AreEqual(TimeSpan.FromHours(3)                             , value3);
            Assert.AreEqual(3.14d                                             , value4);
            Assert.AreEqual(100L                                              , value5);
            Assert.AreEqual('a'                                               , value6);
            Assert.AreEqual(Guid.Parse("56128c75-379f-4c24-ac02-7ceb335807af"), value7);
        }

        [TestMethod]
        public async Task WhenAllT8()
        {
            // Bad Input
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    (Task<int>)null!,
                    Task.FromResult("Hello World"),
                    Task.FromResult(TimeSpan.FromHours(3)),
                    Task.FromResult(3.14d),
                    Task.FromResult(100L),
                    Task.FromResult('a'),
                    Task.FromResult(Guid.Parse("56128c75-379f-4c24-ac02-7ceb335807af")),
                    Task.FromResult((sbyte)-3))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    (Task<string>)null!,
                    Task.FromResult(TimeSpan.FromHours(3)),
                    Task.FromResult(3.14d),
                    Task.FromResult(100L),
                    Task.FromResult('a'),
                    Task.FromResult(Guid.Parse("56128c75-379f-4c24-ac02-7ceb335807af")),
                    Task.FromResult((sbyte)-3))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    Task.FromResult("Hello World"),
                    (Task<TimeSpan>)null!,
                    Task.FromResult(3.14d),
                    Task.FromResult(100L),
                    Task.FromResult('a'),
                    Task.FromResult(Guid.Parse("56128c75-379f-4c24-ac02-7ceb335807af")),
                    Task.FromResult((sbyte)-3))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    Task.FromResult("Hello World"),
                    Task.FromResult(TimeSpan.FromHours(3)),
                    (Task<double>)null!,
                    Task.FromResult(100L),
                    Task.FromResult('a'),
                    Task.FromResult(Guid.Parse("56128c75-379f-4c24-ac02-7ceb335807af")),
                    Task.FromResult((sbyte)-3))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    Task.FromResult("Hello World"),
                    Task.FromResult(TimeSpan.FromHours(3)),
                    Task.FromResult(3.14d),
                    (Task<long>)null!,
                    Task.FromResult('a'),
                    Task.FromResult(Guid.Parse("56128c75-379f-4c24-ac02-7ceb335807af")),
                    Task.FromResult((sbyte)-3))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    Task.FromResult("Hello World"),
                    Task.FromResult(TimeSpan.FromHours(3)),
                    Task.FromResult(3.14d),
                    Task.FromResult(100L),
                    (Task<char>)null!,
                    Task.FromResult(Guid.Parse("56128c75-379f-4c24-ac02-7ceb335807af")),
                    Task.FromResult((sbyte)-3))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    Task.FromResult("Hello World"),
                    Task.FromResult(TimeSpan.FromHours(3)),
                    Task.FromResult(3.14d),
                    Task.FromResult(100L),
                    Task.FromResult('a'),
                    (Task<Guid>)null!,
                    Task.FromResult((sbyte)-3))).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => MultiTask.WhenAll(
                    Task.FromResult(42),
                    Task.FromResult("Hello World"),
                    Task.FromResult(TimeSpan.FromHours(3)),
                    Task.FromResult(3.14d),
                    Task.FromResult(100L),
                    Task.FromResult('a'),
                    Task.FromResult(Guid.Parse("56128c75-379f-4c24-ac02-7ceb335807af")),
                    (Task<sbyte>)null!)).ConfigureAwait(false);

            // Success
            (int value1, string value2, TimeSpan value3, double value4, long value5, char value6, Guid value7, sbyte value8) = await MultiTask.WhenAll(
                Task.FromResult(42),
                Task.FromResult("Hello World"),
                Task.FromResult(TimeSpan.FromHours(3)),
                Task.FromResult(3.14d),
                Task.FromResult(100L),
                Task.FromResult('a'),
                Task.FromResult(Guid.Parse("56128c75-379f-4c24-ac02-7ceb335807af")),
                Task.FromResult((sbyte)-3)).ConfigureAwait(false);

            Assert.AreEqual(42                                                , value1);
            Assert.AreEqual("Hello World"                                     , value2);
            Assert.AreEqual(TimeSpan.FromHours(3)                             , value3);
            Assert.AreEqual(3.14d                                             , value4);
            Assert.AreEqual(100L                                              , value5);
            Assert.AreEqual('a'                                               , value6);
            Assert.AreEqual(Guid.Parse("56128c75-379f-4c24-ac02-7ceb335807af"), value7);
            Assert.AreEqual((sbyte)-3                                         , value8);
        }
    }
}
