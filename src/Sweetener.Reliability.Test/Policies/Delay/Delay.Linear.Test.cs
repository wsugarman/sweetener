using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    partial class DelayTest
    {
        [TestMethod]
        public void Linear_CtorExceptions()
        {
            // (1) Slope Too Small
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Linear(-100));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Linear(  -1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Linear(   0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Linear(   1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Linear(TimeSpan.FromDays        (-2)));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Linear(TimeSpan.FromMilliseconds(-1)));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Linear(TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Linear(TimeSpan.FromMilliseconds( 1)));

            // (2) Slope Too Large
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Linear(TimeSpan.FromMilliseconds((double)int.MaxValue + 1234)));
        }

        [TestMethod]
        public void Linear()
        {
            IDelayPolicy msPolicy = DelayPolicy.Linear((int)s_value.TotalMilliseconds);
            IDelayPolicy tsPolicy = DelayPolicy.Linear(s_value);

            #region GetDelay Exceptions
            // (1) Attempt Too Small
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicy.GetDelay(-4, new Exception()              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicy.GetDelay(-4, new Exception()              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicy.GetDelay( 0, new NotImplementedException()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicy.GetDelay( 0, new NotImplementedException()));

            // (2) Attempt Too Large
            Assert.ThrowsException<OverflowException>(() => msPolicy.GetDelay((int.MaxValue / (int)s_truncatedValue.TotalMilliseconds) + 1, new InvalidCastException()));
            Assert.ThrowsException<OverflowException>(() => tsPolicy.GetDelay((int.MaxValue / (int)s_truncatedValue.TotalMilliseconds) + 1, new InvalidCastException()));

            // (3) Null Exception Argument
            Assert.ThrowsException<ArgumentNullException>(() => msPolicy.GetDelay( 1, null));
            Assert.ThrowsException<ArgumentNullException>(() => tsPolicy.GetDelay( 1, null));
            #endregion

            #region GetDelay
            Assert.AreEqual(s_truncatedValue.Ticks *  1, msPolicy.GetDelay( 1, new ArgumentException()         ).Ticks);
            Assert.AreEqual(s_truncatedValue.Ticks *  1, tsPolicy.GetDelay( 1, new ArgumentException()         ).Ticks);
            Assert.AreEqual(s_truncatedValue.Ticks *  5, msPolicy.GetDelay( 5, new InvalidOperationException() ).Ticks);
            Assert.AreEqual(s_truncatedValue.Ticks *  5, tsPolicy.GetDelay( 5, new InvalidOperationException() ).Ticks);
            Assert.AreEqual(s_truncatedValue.Ticks * 23, msPolicy.GetDelay(23, new OperationCanceledException()).Ticks);
            Assert.AreEqual(s_truncatedValue.Ticks * 23, tsPolicy.GetDelay(23, new OperationCanceledException()).Ticks);
            #endregion
        }

        [TestMethod]
        public void Linear_T_CtorExceptions()
        {
            // (1) Slope Too Small
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Linear<bool>(-100));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Linear<bool>(  -1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Linear<bool>(   0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Linear<bool>(   1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Linear<bool>(TimeSpan.FromDays        (-2)));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Linear<bool>(TimeSpan.FromMilliseconds(-1)));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Linear<bool>(TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Linear<bool>(TimeSpan.FromMilliseconds( 1)));

            // (2) Slope Too Large
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Linear<bool>(TimeSpan.FromMilliseconds((double)int.MaxValue + 1234)));
        }

        [TestMethod]
        public void Linear_T()
        {
            IDelayPolicy<string> msPolicy = DelayPolicy.Linear<string>((int)s_value.TotalMilliseconds);
            IDelayPolicy<string> tsPolicy = DelayPolicy.Linear<string>(s_value);

            #region GetDelay Exceptions
            // (1) Attempt Too Small
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicy.GetDelay(-4, new Exception()              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicy.GetDelay(-4, new Exception()              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicy.GetDelay( 0, new NotImplementedException()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicy.GetDelay( 0, new NotImplementedException()));

            // (2) Attempt Too Large
            Assert.ThrowsException<OverflowException>(() => msPolicy.GetDelay((int.MaxValue / (int)s_truncatedValue.TotalMilliseconds) + 1, new InvalidCastException()));
            Assert.ThrowsException<OverflowException>(() => tsPolicy.GetDelay((int.MaxValue / (int)s_truncatedValue.TotalMilliseconds) + 1, new InvalidCastException()));

            // (3) Null Exception Argument
            Assert.ThrowsException<ArgumentNullException>(() => msPolicy.GetDelay( 1, (Exception)null));
            Assert.ThrowsException<ArgumentNullException>(() => tsPolicy.GetDelay( 1, (Exception)null));
            #endregion

            #region GetDelay
            Assert.AreEqual(s_truncatedValue.Ticks *  1, msPolicy.GetDelay( 1, new ArgumentException()         ).Ticks);
            Assert.AreEqual(s_truncatedValue.Ticks *  1, tsPolicy.GetDelay( 1, new ArgumentException()         ).Ticks);
            Assert.AreEqual(s_truncatedValue.Ticks *  5, msPolicy.GetDelay( 5, new InvalidOperationException() ).Ticks);
            Assert.AreEqual(s_truncatedValue.Ticks *  5, tsPolicy.GetDelay( 5, new InvalidOperationException() ).Ticks);
            Assert.AreEqual(s_truncatedValue.Ticks * 23, msPolicy.GetDelay(23, new OperationCanceledException()).Ticks);
            Assert.AreEqual(s_truncatedValue.Ticks * 23, tsPolicy.GetDelay(23, new OperationCanceledException()).Ticks);
            #endregion

            #region GetDelay<T> Exceptions
            // (1) Attempt Too Small
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicy.GetDelay(-4, "12345"            ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicy.GetDelay(-4, "12345"            ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicy.GetDelay( 0, Environment.NewLine));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicy.GetDelay( 0, Environment.NewLine));

            // (2) Attempt Too Large
            Assert.ThrowsException<OverflowException>(() => msPolicy.GetDelay((int.MaxValue / (int)s_truncatedValue.TotalMilliseconds) + 1, string.Empty));
            Assert.ThrowsException<OverflowException>(() => tsPolicy.GetDelay((int.MaxValue / (int)s_truncatedValue.TotalMilliseconds) + 1, string.Empty));
            #endregion

            #region GetDelay<T>
            Assert.AreEqual(s_truncatedValue *  1, msPolicy.GetDelay( 1, "ABC"       ));
            Assert.AreEqual(s_truncatedValue *  1, tsPolicy.GetDelay( 1, "ABC"       ));
            Assert.AreEqual(s_truncatedValue *  5, msPolicy.GetDelay( 5, string.Empty));
            Assert.AreEqual(s_truncatedValue *  5, tsPolicy.GetDelay( 5, string.Empty));
            Assert.AreEqual(s_truncatedValue * 23, msPolicy.GetDelay(23, null        ));
            Assert.AreEqual(s_truncatedValue * 23, tsPolicy.GetDelay(23, null        ));
            #endregion
        }
    }
}
