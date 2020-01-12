// Generated from ExceptionPolicy.Fail.Test.tt
using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    partial class ExceptionPolicyTest
    {
        [TestMethod]
        public void Fail_1()
        {
            ExceptionHandler isTransient = ExceptionPolicy.Fail<AggregateException>();

            Assert.ThrowsException<ArgumentNullException>(() => isTransient(null));

            Assert.IsFalse(isTransient(new AggregateException        ()));
            Assert.IsTrue (isTransient(new ArgumentException         ()));
            Assert.IsTrue (isTransient(new InvalidOperationException ()));
            Assert.IsTrue (isTransient(new IOException               ()));
            Assert.IsTrue (isTransient(new FileNotFoundException     ()));
            Assert.IsTrue (isTransient(new FormatException           ()));
            Assert.IsTrue (isTransient(new OperationCanceledException()));
            Assert.IsTrue (isTransient(new OverflowException         ()));
            Assert.IsTrue (isTransient(new ArgumentNullException     ()));
        }

        [TestMethod]
        public void Fail_2()
        {
            ExceptionHandler isTransient = ExceptionPolicy.Fail<AggregateException, ArgumentException>();

            Assert.ThrowsException<ArgumentNullException>(() => isTransient(null));

            Assert.IsFalse(isTransient(new AggregateException        ()));
            Assert.IsFalse(isTransient(new ArgumentException         ()));
            Assert.IsTrue (isTransient(new InvalidOperationException ()));
            Assert.IsTrue (isTransient(new IOException               ()));
            Assert.IsTrue (isTransient(new FileNotFoundException     ()));
            Assert.IsTrue (isTransient(new FormatException           ()));
            Assert.IsTrue (isTransient(new OperationCanceledException()));
            Assert.IsTrue (isTransient(new OverflowException         ()));
            Assert.IsTrue (isTransient(new ArgumentNullException     ()));
        }

        [TestMethod]
        public void Fail_3()
        {
            ExceptionHandler isTransient = ExceptionPolicy.Fail<AggregateException, ArgumentException, InvalidOperationException>();

            Assert.ThrowsException<ArgumentNullException>(() => isTransient(null));

            Assert.IsFalse(isTransient(new AggregateException        ()));
            Assert.IsFalse(isTransient(new ArgumentException         ()));
            Assert.IsFalse(isTransient(new InvalidOperationException ()));
            Assert.IsTrue (isTransient(new IOException               ()));
            Assert.IsTrue (isTransient(new FileNotFoundException     ()));
            Assert.IsTrue (isTransient(new FormatException           ()));
            Assert.IsTrue (isTransient(new OperationCanceledException()));
            Assert.IsTrue (isTransient(new OverflowException         ()));
            Assert.IsTrue (isTransient(new ArgumentNullException     ()));
        }

        [TestMethod]
        public void Fail_4()
        {
            ExceptionHandler isTransient = ExceptionPolicy.Fail<AggregateException, ArgumentException, InvalidOperationException, IOException>();

            Assert.ThrowsException<ArgumentNullException>(() => isTransient(null));

            Assert.IsFalse(isTransient(new AggregateException        ()));
            Assert.IsFalse(isTransient(new ArgumentException         ()));
            Assert.IsFalse(isTransient(new InvalidOperationException ()));
            Assert.IsFalse(isTransient(new IOException               ()));
            Assert.IsTrue (isTransient(new FileNotFoundException     ()));
            Assert.IsTrue (isTransient(new FormatException           ()));
            Assert.IsTrue (isTransient(new OperationCanceledException()));
            Assert.IsTrue (isTransient(new OverflowException         ()));
            Assert.IsTrue (isTransient(new ArgumentNullException     ()));
        }

        [TestMethod]
        public void Fail_5()
        {
            ExceptionHandler isTransient = ExceptionPolicy.Fail<AggregateException, ArgumentException, InvalidOperationException, IOException, FileNotFoundException>();

            Assert.ThrowsException<ArgumentNullException>(() => isTransient(null));

            Assert.IsFalse(isTransient(new AggregateException        ()));
            Assert.IsFalse(isTransient(new ArgumentException         ()));
            Assert.IsFalse(isTransient(new InvalidOperationException ()));
            Assert.IsFalse(isTransient(new IOException               ()));
            Assert.IsFalse(isTransient(new FileNotFoundException     ()));
            Assert.IsTrue (isTransient(new FormatException           ()));
            Assert.IsTrue (isTransient(new OperationCanceledException()));
            Assert.IsTrue (isTransient(new OverflowException         ()));
            Assert.IsTrue (isTransient(new ArgumentNullException     ()));
        }

        [TestMethod]
        public void Fail_6()
        {
            ExceptionHandler isTransient = ExceptionPolicy.Fail<AggregateException, ArgumentException, InvalidOperationException, IOException, FileNotFoundException, FormatException>();

            Assert.ThrowsException<ArgumentNullException>(() => isTransient(null));

            Assert.IsFalse(isTransient(new AggregateException        ()));
            Assert.IsFalse(isTransient(new ArgumentException         ()));
            Assert.IsFalse(isTransient(new InvalidOperationException ()));
            Assert.IsFalse(isTransient(new IOException               ()));
            Assert.IsFalse(isTransient(new FileNotFoundException     ()));
            Assert.IsFalse(isTransient(new FormatException           ()));
            Assert.IsTrue (isTransient(new OperationCanceledException()));
            Assert.IsTrue (isTransient(new OverflowException         ()));
            Assert.IsTrue (isTransient(new ArgumentNullException     ()));
        }

        [TestMethod]
        public void Fail_7()
        {
            ExceptionHandler isTransient = ExceptionPolicy.Fail<AggregateException, ArgumentException, InvalidOperationException, IOException, FileNotFoundException, FormatException, OperationCanceledException>();

            Assert.ThrowsException<ArgumentNullException>(() => isTransient(null));

            Assert.IsFalse(isTransient(new AggregateException        ()));
            Assert.IsFalse(isTransient(new ArgumentException         ()));
            Assert.IsFalse(isTransient(new InvalidOperationException ()));
            Assert.IsFalse(isTransient(new IOException               ()));
            Assert.IsFalse(isTransient(new FileNotFoundException     ()));
            Assert.IsFalse(isTransient(new FormatException           ()));
            Assert.IsFalse(isTransient(new OperationCanceledException()));
            Assert.IsTrue (isTransient(new OverflowException         ()));
            Assert.IsTrue (isTransient(new ArgumentNullException     ()));
        }

        [TestMethod]
        public void Fail_8()
        {
            ExceptionHandler isTransient = ExceptionPolicy.Fail<AggregateException, ArgumentException, InvalidOperationException, IOException, FileNotFoundException, FormatException, OperationCanceledException, OverflowException>();

            Assert.ThrowsException<ArgumentNullException>(() => isTransient(null));

            Assert.IsFalse(isTransient(new AggregateException        ()));
            Assert.IsFalse(isTransient(new ArgumentException         ()));
            Assert.IsFalse(isTransient(new InvalidOperationException ()));
            Assert.IsFalse(isTransient(new IOException               ()));
            Assert.IsFalse(isTransient(new FileNotFoundException     ()));
            Assert.IsFalse(isTransient(new FormatException           ()));
            Assert.IsFalse(isTransient(new OperationCanceledException()));
            Assert.IsFalse(isTransient(new OverflowException         ()));
            Assert.IsTrue (isTransient(new ArgumentNullException     ()));
        }

    }
}
