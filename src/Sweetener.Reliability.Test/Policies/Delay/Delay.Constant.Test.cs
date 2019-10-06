using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    partial class DelayTest
    {
        [TestMethod]
        public void Constant_CtorExceptions()
        {
            // (1) Delay Too Small
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Constant(-100));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Constant(  -1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Constant(TimeSpan.FromDays        (-2)));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Constant(TimeSpan.FromMilliseconds(-1)));

            // (2) Delay Too Large
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Constant(TimeSpan.FromMilliseconds((double)int.MaxValue + 1234)));
        }

        [TestMethod]
        public void Constant()
        {
            IDelayPolicy msPolicy = DelayPolicy.Constant((int)s_value.TotalMilliseconds);
            IDelayPolicy tsPolicy = DelayPolicy.Constant(s_value);

            #region GetDelay Exceptions
            // (1) Attempt Too Small
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicy.GetDelay(-4, new Exception()              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicy.GetDelay(-4, new Exception()              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicy.GetDelay( 0, new NotImplementedException()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicy.GetDelay( 0, new NotImplementedException()));

            // (2) Null Exception Argument
            Assert.ThrowsException<ArgumentNullException>(() => msPolicy.GetDelay(1, null));
            Assert.ThrowsException<ArgumentNullException>(() => tsPolicy.GetDelay(1, null));
            #endregion

            #region GetDelay
            Assert.AreEqual(s_truncatedValue, msPolicy.GetDelay( 1, new ArgumentException()         ));
            Assert.AreEqual(s_truncatedValue, tsPolicy.GetDelay( 1, new ArgumentException()         ));
            Assert.AreEqual(s_truncatedValue, msPolicy.GetDelay( 5, new InvalidOperationException() ));
            Assert.AreEqual(s_truncatedValue, tsPolicy.GetDelay( 5, new InvalidOperationException() ));
            Assert.AreEqual(s_truncatedValue, msPolicy.GetDelay(23, new OperationCanceledException()));
            Assert.AreEqual(s_truncatedValue, tsPolicy.GetDelay(23, new OperationCanceledException()));
            #endregion
        }

        [TestMethod]
        public void Constant_T_CtorExceptions()
        {
            // (1) Delay Too Small
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Constant<bool>(-100));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Constant<bool>(  -1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Constant<bool>(TimeSpan.FromDays        (-2)));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Constant<bool>(TimeSpan.FromMilliseconds(-1)));

            // (2) Delay Too Large
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Constant<bool>(TimeSpan.FromMilliseconds((double)int.MaxValue + 1234)));
        }

        [TestMethod]
        public void Constant_T()
        {
            IDelayPolicy<string> msPolicy = DelayPolicy.Constant<string>((int)s_value.TotalMilliseconds);
            IDelayPolicy<string> tsPolicy = DelayPolicy.Constant<string>(s_value);

            #region GetDelay Exceptions
            // (1) Attempt Too Small
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicy.GetDelay(-4, new Exception()              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicy.GetDelay(-4, new Exception()              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicy.GetDelay( 0, new NotImplementedException()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicy.GetDelay( 0, new NotImplementedException()));

            // (2) Null Exception Argument
            Assert.ThrowsException<ArgumentNullException>(() => msPolicy.GetDelay(1, (Exception)null));
            Assert.ThrowsException<ArgumentNullException>(() => tsPolicy.GetDelay(1, (Exception)null));
            #endregion

            #region GetDelay
            Assert.AreEqual(s_truncatedValue, msPolicy.GetDelay( 1, new ArgumentException()         ));
            Assert.AreEqual(s_truncatedValue, tsPolicy.GetDelay( 1, new ArgumentException()         ));
            Assert.AreEqual(s_truncatedValue, msPolicy.GetDelay( 5, new InvalidOperationException() ));
            Assert.AreEqual(s_truncatedValue, tsPolicy.GetDelay( 5, new InvalidOperationException() ));
            Assert.AreEqual(s_truncatedValue, msPolicy.GetDelay(23, new OperationCanceledException()));
            Assert.AreEqual(s_truncatedValue, tsPolicy.GetDelay(23, new OperationCanceledException()));
            #endregion

            #region GetDelay<T> Exceptions
            // (1) Attempt Too Small
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicy.GetDelay(-4, "12345"            ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicy.GetDelay(-4, "12345"            ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicy.GetDelay( 0, Environment.NewLine));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicy.GetDelay( 0, Environment.NewLine));
            #endregion

            #region GetDelay<T>
            Assert.AreEqual(s_truncatedValue, msPolicy.GetDelay( 1, "ABC"       ));
            Assert.AreEqual(s_truncatedValue, tsPolicy.GetDelay( 1, "ABC"       ));
            Assert.AreEqual(s_truncatedValue, msPolicy.GetDelay( 5, string.Empty));
            Assert.AreEqual(s_truncatedValue, tsPolicy.GetDelay( 5, string.Empty));
            Assert.AreEqual(s_truncatedValue, msPolicy.GetDelay(23, null        ));
            Assert.AreEqual(s_truncatedValue, tsPolicy.GetDelay(23, null        ));
            #endregion
        }

        [TestMethod]
        public void None()
        {
            IDelayPolicy policy = DelayPolicy.None();

            #region GetDelay Exceptions
            // (1) Attempt Too Small
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => policy.GetDelay(-4, new Exception()              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => policy.GetDelay(-4, new Exception()              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => policy.GetDelay( 0, new NotImplementedException()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => policy.GetDelay( 0, new NotImplementedException()));

            // (2) Null Exception Argument
            Assert.ThrowsException<ArgumentNullException>(() => policy.GetDelay(1, null));
            Assert.ThrowsException<ArgumentNullException>(() => policy.GetDelay(1, null));
            #endregion

            #region GetDelay
            Assert.AreEqual(TimeSpan.Zero, policy.GetDelay( 1, new ArgumentException()         ));
            Assert.AreEqual(TimeSpan.Zero, policy.GetDelay( 1, new ArgumentException()         ));
            Assert.AreEqual(TimeSpan.Zero, policy.GetDelay( 5, new InvalidOperationException() ));
            Assert.AreEqual(TimeSpan.Zero, policy.GetDelay( 5, new InvalidOperationException() ));
            Assert.AreEqual(TimeSpan.Zero, policy.GetDelay(23, new OperationCanceledException()));
            Assert.AreEqual(TimeSpan.Zero, policy.GetDelay(23, new OperationCanceledException()));
            #endregion
        }

        [TestMethod]
        public void None_T()
        {
            IDelayPolicy<string> policy = DelayPolicy.None<string>();

            #region GetDelay Exceptions
            // (1) Attempt Too Small
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => policy.GetDelay(-4, new Exception()              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => policy.GetDelay(-4, new Exception()              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => policy.GetDelay( 0, new NotImplementedException()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => policy.GetDelay( 0, new NotImplementedException()));

            // (2) Null Exception Argument
            Assert.ThrowsException<ArgumentNullException>(() => policy.GetDelay(1, (Exception)null));
            Assert.ThrowsException<ArgumentNullException>(() => policy.GetDelay(1, (Exception)null));
            #endregion

            #region GetDelay
            Assert.AreEqual(TimeSpan.Zero, policy.GetDelay( 1, new ArgumentException()         ));
            Assert.AreEqual(TimeSpan.Zero, policy.GetDelay( 1, new ArgumentException()         ));
            Assert.AreEqual(TimeSpan.Zero, policy.GetDelay( 5, new InvalidOperationException() ));
            Assert.AreEqual(TimeSpan.Zero, policy.GetDelay( 5, new InvalidOperationException() ));
            Assert.AreEqual(TimeSpan.Zero, policy.GetDelay(23, new OperationCanceledException()));
            Assert.AreEqual(TimeSpan.Zero, policy.GetDelay(23, new OperationCanceledException()));
            #endregion

            #region GetDelay<T> Exceptions
            // (1) Attempt Too Small
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => policy.GetDelay(-4, "12345"            ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => policy.GetDelay(-4, "12345"            ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => policy.GetDelay( 0, Environment.NewLine));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => policy.GetDelay( 0, Environment.NewLine));
            #endregion

            #region GetDelay<T>
            Assert.AreEqual(TimeSpan.Zero, policy.GetDelay( 1, "ABC"       ));
            Assert.AreEqual(TimeSpan.Zero, policy.GetDelay( 1, "ABC"       ));
            Assert.AreEqual(TimeSpan.Zero, policy.GetDelay( 5, string.Empty));
            Assert.AreEqual(TimeSpan.Zero, policy.GetDelay( 5, string.Empty));
            Assert.AreEqual(TimeSpan.Zero, policy.GetDelay(23, null        ));
            Assert.AreEqual(TimeSpan.Zero, policy.GetDelay(23, null        ));
            #endregion
        }
    }
}
