using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    partial class DelayTest
    {
        private const int s_seed = 2109091513;

        [TestMethod]
        public void Exponential_CtorExceptions()
        {
            // (1) Time Unit Too Small
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(-100));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(  -1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(   0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(   1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(-100, new Random()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(  -1, new Random()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(   0, new Random()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(   1, new Random()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(TimeSpan.FromDays        (-2)));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(TimeSpan.FromMilliseconds(-1)));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(TimeSpan.FromMilliseconds( 1)));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(TimeSpan.FromDays        (-2), new Random()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(TimeSpan.FromMilliseconds(-1), new Random()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(TimeSpan.Zero                , new Random()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(TimeSpan.FromMilliseconds( 1), new Random()));

            // (2) Time Unit Too Large
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(TimeSpan.FromMilliseconds((double)int.MaxValue + 1234)));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(TimeSpan.FromMilliseconds((double)int.MaxValue + 1234), new Random()));

            // (3) Null Random Argument
            Assert.ThrowsException<ArgumentNullException>(() => DelayPolicy.Exponential(2, null));
            Assert.ThrowsException<ArgumentNullException>(() => DelayPolicy.Exponential(TimeSpan.FromMilliseconds(2), null));
        }

        [TestMethod]
        public void Exponential()
        {
            IDelayPolicy msPolicy     = DelayPolicy.Exponential((int)s_value.TotalMilliseconds);
            IDelayPolicy msPolicySeed = DelayPolicy.Exponential((int)s_value.TotalMilliseconds, new Random(s_seed));
            IDelayPolicy tsPolicy     = DelayPolicy.Exponential(s_value);
            IDelayPolicy tsPolicySeed = DelayPolicy.Exponential(s_value, new Random(s_seed));

            #region GetDelay Exceptions
            // (1) Attempt Too Small
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicy    .GetDelay(-4, new Exception()              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicySeed.GetDelay(-4, new Exception()              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicy    .GetDelay(-4, new Exception()              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicySeed.GetDelay(-4, new Exception()              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicy    .GetDelay( 0, new NotImplementedException()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicySeed.GetDelay( 0, new NotImplementedException()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicy    .GetDelay( 0, new NotImplementedException()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicySeed.GetDelay( 0, new NotImplementedException()));

            // (2) Attempt Too Large
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicy    .GetDelay(33, new InvalidCastException()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicySeed.GetDelay(33, new InvalidCastException()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicy    .GetDelay(33, new InvalidCastException()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicySeed.GetDelay(33, new InvalidCastException()));

            // (3) Delay Calculation Overflow
            Assert.ThrowsException<OverflowException>(() => { while (true) { DelayPolicy.Exponential(int.MaxValue)                                               .GetDelay(31, new FormatException()); } });
            Assert.ThrowsException<OverflowException>(() => { while (true) { DelayPolicy.Exponential(int.MaxValue                           , new Random(s_seed)).GetDelay(31, new FormatException()); } });
            Assert.ThrowsException<OverflowException>(() => { while (true) { DelayPolicy.Exponential(TimeSpan.FromMilliseconds(int.MaxValue))                    .GetDelay(31, new FormatException()); } });
            Assert.ThrowsException<OverflowException>(() => { while (true) { DelayPolicy.Exponential(TimeSpan.FromMilliseconds(int.MaxValue), new Random(s_seed)).GetDelay(31, new FormatException()); } });

            // (4) Null Exception Argument
            Assert.ThrowsException<ArgumentNullException>(() => msPolicy    .GetDelay( 1, null));
            Assert.ThrowsException<ArgumentNullException>(() => msPolicySeed.GetDelay( 1, null));
            Assert.ThrowsException<ArgumentNullException>(() => tsPolicy    .GetDelay( 1, null));
            Assert.ThrowsException<ArgumentNullException>(() => tsPolicySeed.GetDelay( 1, null));
            #endregion

            #region GetDelay
            Random msPolicyRandom = new Random(s_seed);
            Random tsPolicyRandom = new Random(s_seed);
            Func<int, Random, TimeSpan> getExpectedDelay = (i, r) => TimeSpan.FromMilliseconds((int)s_truncatedValue.TotalMilliseconds * r.Next(0, 1 << i));

            // (1) With Seed
            // Reset the underlying Random state
            msPolicySeed = DelayPolicy.Exponential((int)s_value.TotalMilliseconds, new Random(s_seed));
            tsPolicySeed = DelayPolicy.Exponential(s_value, new Random(s_seed));

            Assert.AreEqual(getExpectedDelay( 1, msPolicyRandom), msPolicySeed.GetDelay( 1, new ArgumentException()         ));
            Assert.AreEqual(getExpectedDelay( 1, tsPolicyRandom), tsPolicySeed.GetDelay( 1, new ArgumentException()         ));
            Assert.AreEqual(getExpectedDelay( 5, msPolicyRandom), msPolicySeed.GetDelay( 5, new InvalidOperationException() ));
            Assert.AreEqual(getExpectedDelay( 5, tsPolicyRandom), tsPolicySeed.GetDelay( 5, new InvalidOperationException() ));
            Assert.AreEqual(getExpectedDelay(23, msPolicyRandom), msPolicySeed.GetDelay(23, new OperationCanceledException()));
            Assert.AreEqual(getExpectedDelay(23, tsPolicyRandom), tsPolicySeed.GetDelay(23, new OperationCanceledException()));

            // (2) Without Seed
            AssertValidRange( 1, msPolicy.GetDelay( 1, new ArgumentException()         ));
            AssertValidRange( 1, tsPolicy.GetDelay( 1, new ArgumentException()         ));
            AssertValidRange( 5, msPolicy.GetDelay( 5, new InvalidOperationException() ));
            AssertValidRange( 5, tsPolicy.GetDelay( 5, new InvalidOperationException() ));
            AssertValidRange(23, msPolicy.GetDelay(23, new OperationCanceledException()));
            AssertValidRange(23, tsPolicy.GetDelay(23, new OperationCanceledException()));
            #endregion
        }

        [TestMethod]
        public void Exponential_T()
        {
            #region Ctor Input Validation
            // (1) Time Unit Too Small
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential<bool>(-100));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential<bool>(  -1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential<bool>(   0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential<bool>(   1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential<bool>(-100, new Random()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential<bool>(  -1, new Random()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential<bool>(   0, new Random()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential<bool>(   1, new Random()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential<bool>(TimeSpan.FromDays        (-2)));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential<bool>(TimeSpan.FromMilliseconds(-1)));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential<bool>(TimeSpan.Zero));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential<bool>(TimeSpan.FromMilliseconds( 1)));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential<bool>(TimeSpan.FromDays        (-2), new Random()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential<bool>(TimeSpan.FromMilliseconds(-1), new Random()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential<bool>(TimeSpan.Zero                , new Random()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential<bool>(TimeSpan.FromMilliseconds( 1), new Random()));

            // (2) Time Unit Too Large
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential<bool>(TimeSpan.FromMilliseconds((double)int.MaxValue + 1234)));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential<bool>(TimeSpan.FromMilliseconds((double)int.MaxValue + 1234), new Random()));

            // (3) Null Random Argument
            Assert.ThrowsException<ArgumentNullException>(() => DelayPolicy.Exponential<bool>(2, null));
            Assert.ThrowsException<ArgumentNullException>(() => DelayPolicy.Exponential<bool>(TimeSpan.FromMilliseconds(2), null));
            #endregion

            IDelayPolicy<string> msPolicy     = DelayPolicy.Exponential<string>((int)s_value.TotalMilliseconds);
            IDelayPolicy<string> msPolicySeed = DelayPolicy.Exponential<string>((int)s_value.TotalMilliseconds, new Random(s_seed));
            IDelayPolicy<string> tsPolicy     = DelayPolicy.Exponential<string>(s_value);
            IDelayPolicy<string> tsPolicySeed = DelayPolicy.Exponential<string>(s_value, new Random(s_seed));

            #region GetDelay Exceptions
            // (1) Attempt Too Small
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicy    .GetDelay(-4, new Exception()              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicySeed.GetDelay(-4, new Exception()              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicy    .GetDelay(-4, new Exception()              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicySeed.GetDelay(-4, new Exception()              ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicy    .GetDelay( 0, new NotImplementedException()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicySeed.GetDelay( 0, new NotImplementedException()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicy    .GetDelay( 0, new NotImplementedException()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicySeed.GetDelay( 0, new NotImplementedException()));

            // (2) Attempt Too Large
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicy    .GetDelay(33, new InvalidCastException()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicySeed.GetDelay(33, new InvalidCastException()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicy    .GetDelay(33, new InvalidCastException()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicySeed.GetDelay(33, new InvalidCastException()));

            // (3) Delay Calculation Overflow
            Assert.ThrowsException<OverflowException>(() => { while (true) { DelayPolicy.Exponential(int.MaxValue)                                               .GetDelay(31, new InvalidCastException()); } });
            Assert.ThrowsException<OverflowException>(() => { while (true) { DelayPolicy.Exponential(int.MaxValue                           , new Random(s_seed)).GetDelay(31, new InvalidCastException()); } });
            Assert.ThrowsException<OverflowException>(() => { while (true) { DelayPolicy.Exponential(TimeSpan.FromMilliseconds(int.MaxValue))                    .GetDelay(31, new InvalidCastException()); } });
            Assert.ThrowsException<OverflowException>(() => { while (true) { DelayPolicy.Exponential(TimeSpan.FromMilliseconds(int.MaxValue), new Random(s_seed)).GetDelay(31, new InvalidCastException()); } });

            // (4) Null Exception Argument
            Assert.ThrowsException<ArgumentNullException>(() => msPolicy    .GetDelay( 1, (Exception)null));
            Assert.ThrowsException<ArgumentNullException>(() => msPolicySeed.GetDelay( 1, (Exception)null));
            Assert.ThrowsException<ArgumentNullException>(() => tsPolicy    .GetDelay( 1, (Exception)null));
            Assert.ThrowsException<ArgumentNullException>(() => tsPolicySeed.GetDelay( 1, (Exception)null));
            #endregion

            #region GetDelay
            Random msPolicyRandom = new Random(s_seed);
            Random tsPolicyRandom = new Random(s_seed);
            Func<int, Random, TimeSpan> getExpectedDelay = (i, r) => TimeSpan.FromMilliseconds((int)s_truncatedValue.TotalMilliseconds * r.Next(0, 1 << i));

            // (1) With Seed
            // Reset the underlying Random state
            msPolicySeed = DelayPolicy.Exponential<string>((int)s_value.TotalMilliseconds, new Random(s_seed));
            tsPolicySeed = DelayPolicy.Exponential<string>(s_value, new Random(s_seed));

            Assert.AreEqual(getExpectedDelay( 1, msPolicyRandom), msPolicySeed.GetDelay( 1, new ArgumentException()         ));
            Assert.AreEqual(getExpectedDelay( 1, tsPolicyRandom), tsPolicySeed.GetDelay( 1, new ArgumentException()         ));
            Assert.AreEqual(getExpectedDelay( 5, msPolicyRandom), msPolicySeed.GetDelay( 5, new InvalidOperationException() ));
            Assert.AreEqual(getExpectedDelay( 5, tsPolicyRandom), tsPolicySeed.GetDelay( 5, new InvalidOperationException() ));
            Assert.AreEqual(getExpectedDelay(23, msPolicyRandom), msPolicySeed.GetDelay(23, new OperationCanceledException()));
            Assert.AreEqual(getExpectedDelay(23, tsPolicyRandom), tsPolicySeed.GetDelay(23, new OperationCanceledException()));

            // (2) Without Seed
            AssertValidRange( 1, msPolicy.GetDelay( 1, new ArgumentException()         ));
            AssertValidRange( 1, tsPolicy.GetDelay( 1, new ArgumentException()         ));
            AssertValidRange( 5, msPolicy.GetDelay( 5, new InvalidOperationException() ));
            AssertValidRange( 5, tsPolicy.GetDelay( 5, new InvalidOperationException() ));
            AssertValidRange(23, msPolicy.GetDelay(23, new OperationCanceledException()));
            AssertValidRange(23, tsPolicy.GetDelay(23, new OperationCanceledException()));
            #endregion

            #region GetDelay<T> Exceptions
            // (1) Attempt Too Small
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicy    .GetDelay(-4, "12345"            ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicySeed.GetDelay(-4, "12345"            ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicy    .GetDelay(-4, "12345"            ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicySeed.GetDelay(-4, "12345"            ));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicy    .GetDelay( 0, Environment.NewLine));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicySeed.GetDelay( 0, Environment.NewLine));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicy    .GetDelay( 0, Environment.NewLine));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicySeed.GetDelay( 0, Environment.NewLine));

            // (2) Attempt Too Large
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicy    .GetDelay(33, string.Empty));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => msPolicySeed.GetDelay(33, string.Empty));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicy    .GetDelay(33, string.Empty));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tsPolicySeed.GetDelay(33, string.Empty));

            // (3) Delay Calculation Overflow
            Assert.ThrowsException<OverflowException>(() => { while (true) { DelayPolicy.Exponential<string>(int.MaxValue                                               ).GetDelay(31, new InvalidCastException()); } });
            Assert.ThrowsException<OverflowException>(() => { while (true) { DelayPolicy.Exponential<string>(int.MaxValue                           , new Random(s_seed)).GetDelay(31, new InvalidCastException()); } });
            Assert.ThrowsException<OverflowException>(() => { while (true) { DelayPolicy.Exponential<string>(TimeSpan.FromMilliseconds(int.MaxValue)                    ).GetDelay(31, new InvalidCastException()); } });
            Assert.ThrowsException<OverflowException>(() => { while (true) { DelayPolicy.Exponential<string>(TimeSpan.FromMilliseconds(int.MaxValue), new Random(s_seed)).GetDelay(31, new InvalidCastException()); } });

            // (4) Null Exception Argument
            Assert.ThrowsException<ArgumentNullException>(() => msPolicy    .GetDelay( 1, (Exception)null));
            Assert.ThrowsException<ArgumentNullException>(() => msPolicySeed.GetDelay( 1, (Exception)null));
            Assert.ThrowsException<ArgumentNullException>(() => tsPolicy    .GetDelay( 1, (Exception)null));
            Assert.ThrowsException<ArgumentNullException>(() => tsPolicySeed.GetDelay( 1, (Exception)null));
            #endregion

            #region GetDelay
            msPolicyRandom = new Random(s_seed);
            tsPolicyRandom = new Random(s_seed);

            // (1) With Seed
            // Reset the underlying Random state
            msPolicySeed = DelayPolicy.Exponential<string>((int)s_value.TotalMilliseconds, new Random(s_seed));
            tsPolicySeed = DelayPolicy.Exponential<string>(s_value, new Random(s_seed));

            Assert.AreEqual(getExpectedDelay( 1, msPolicyRandom), msPolicySeed.GetDelay( 1, "ABC"       ));
            Assert.AreEqual(getExpectedDelay( 1, tsPolicyRandom), tsPolicySeed.GetDelay( 1, "ABC"       ));
            Assert.AreEqual(getExpectedDelay( 5, msPolicyRandom), msPolicySeed.GetDelay( 5, string.Empty));
            Assert.AreEqual(getExpectedDelay( 5, tsPolicyRandom), tsPolicySeed.GetDelay( 5, string.Empty));
            Assert.AreEqual(getExpectedDelay(23, msPolicyRandom), msPolicySeed.GetDelay(23, null        ));
            Assert.AreEqual(getExpectedDelay(23, tsPolicyRandom), tsPolicySeed.GetDelay(23, null        ));

            // (2) Without Seed
            AssertValidRange( 1, msPolicy.GetDelay( 1, "ABC"       ));
            AssertValidRange( 1, tsPolicy.GetDelay( 1, "ABC"       ));
            AssertValidRange( 5, msPolicy.GetDelay( 5, string.Empty));
            AssertValidRange( 5, tsPolicy.GetDelay( 5, string.Empty));
            AssertValidRange(23, msPolicy.GetDelay(23, null        ));
            AssertValidRange(23, tsPolicy.GetDelay(23, null        ));
            #endregion
        }

        private static void AssertValidRange(int i, TimeSpan actual)
        {
            uint maxUnits = (1U << i) - 1;
            TimeSpan expectedMaxMagnitude = TimeSpan.FromMilliseconds((int)s_truncatedValue.TotalMilliseconds * maxUnits);
            Assert.IsTrue((long)expectedMaxMagnitude.TotalMilliseconds <= int.MaxValue, "Cannot have maximum possible delay greater than int.MaxValue");
            Assert.IsTrue(actual >= TimeSpan.Zero       , "Delay {0} must be greater than or equal to zero.", actual);
            Assert.IsTrue(actual <= expectedMaxMagnitude, "Delay {0} must be less than or equal to {1}"     , actual, expectedMaxMagnitude);
        }
    }
}
