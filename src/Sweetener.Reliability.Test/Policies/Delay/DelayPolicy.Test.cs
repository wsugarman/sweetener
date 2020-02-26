using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    [TestClass]
    public sealed class DelayPolicyTest
    {
        [TestMethod]
        public void Constant_TimeSpan()
        {
            Assert.ThrowsException<ArgumentNegativeException  >(() => DelayPolicy.Constant(TimeSpan.FromMilliseconds(-1)));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Constant(TimeSpan.FromMilliseconds((double)int.MaxValue + 1)));

            Constant(DelayPolicy.Constant(TimeSpan.FromMilliseconds(100)), 100);
        }

        [TestMethod]
        public void Constant_int()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Constant(-1));

            Constant(DelayPolicy.Constant(100), 100);
        }

        private void Constant(DelayHandler getDelay, int expectedMilliseconds)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => getDelay(0));

            Assert.AreEqual(TimeSpan.FromMilliseconds(expectedMilliseconds), getDelay( 1));
            Assert.AreEqual(TimeSpan.FromMilliseconds(expectedMilliseconds), getDelay( 2));
            Assert.AreEqual(TimeSpan.FromMilliseconds(expectedMilliseconds), getDelay(10));
        }

        [TestMethod]
        public void Exponential_TimeSpan()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(TimeSpan.FromMilliseconds(-1)));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(TimeSpan.FromMilliseconds( 1)));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(TimeSpan.FromMilliseconds((double)int.MaxValue + 1)));

            Exponential(DelayPolicy.Exponential(TimeSpan.FromMilliseconds(100)), 100);
        }

        [TestMethod]
        public void Exponential_TimeSpan_Random()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(TimeSpan.FromMilliseconds(-1), new Random()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(TimeSpan.FromMilliseconds( 1), new Random()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(TimeSpan.FromMilliseconds((double)int.MaxValue + 1), new Random()));
            Assert.ThrowsException<ArgumentNullException      >(() => DelayPolicy.Exponential(TimeSpan.FromMilliseconds( 2), null));

            NotSoRandom rand = new NotSoRandom();
            Exponential(DelayPolicy.Exponential(TimeSpan.FromMilliseconds(100), rand), 100, rand);
        }

        [TestMethod]
        public void Exponential_int()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(-1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential( 1));

            Exponential(DelayPolicy.Exponential(100), 100);
        }

        [TestMethod]
        public void Exponential_int_Random()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential(-1, new Random()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Exponential( 1, new Random()));
            Assert.ThrowsException<ArgumentNullException      >(() => DelayPolicy.Exponential( 2, null        ));

            NotSoRandom rand = new NotSoRandom();
            Exponential(DelayPolicy.Exponential(100, rand), 100, rand);
        }

        private void Exponential(DelayHandler getDelay, int unitMilliseconds, NotSoRandom rand = null)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => getDelay( 0));
            Assert.ThrowsException<OverflowException          >(() => getDelay(32)); // 2^32 > int.MaxValue (32 bits)

            // We are going to use some apriori knowledge about the implementation...!
            // The random value is offset by 1, so we'll do the same here
            int[] attempts = new int[] { 1, 2, 10 };
            if (rand != null)
            {
                int attempt = 0;
                DelayHandler getDelayWrapper = x =>
                {
                    // First assign the attempt so we can use it to later validate arguments in OnNextRange
                    attempt = x;
                    return getDelay(x);
                };

                rand.OnNext += (min, max) =>
                {
                    Assert.AreEqual(-1, min);
                    Assert.AreEqual((int)((1U << attempt) - 1), max);
                };
          
                for (int i = 0; i < attempts.Length; i++)
                {
                    rand.NextValue = i * 2;
                    Assert.AreEqual(TimeSpan.FromMilliseconds(unitMilliseconds * (rand.NextValue + 1)), getDelayWrapper(attempts[i]));
                }

                // Overflow!
                rand.NextValue = int.MaxValue - 1;
                Assert.ThrowsException<OverflowException>(() => getDelayWrapper(31));
            }
            else
            {
                Action<DelayHandler, int, int> validateRange = (policy, unit, attempt) =>
                {
                    TimeSpan actual = policy(attempt);

                    TimeSpan max = TimeSpan.FromMilliseconds(unit * (int)((1U << attempt) - 1));
                    Assert.IsTrue(actual >= TimeSpan.Zero, "Actual delay was less than zero");
                    Assert.IsTrue(actual <= max, $"Actual delay was larger than max value '{max}'");
                };

                foreach (int attempt in attempts)
                    validateRange(getDelay, unitMilliseconds, attempt);

                // Attempt to overflow
                Assert.ThrowsException<OverflowException>(() => { while (true) { getDelay(31); } });
            }
        }

        [TestMethod]
        public void Linear_TimeSpan()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Linear(TimeSpan.FromMilliseconds(-1)));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Linear(TimeSpan.FromMilliseconds( 1)));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Linear(TimeSpan.FromMilliseconds((double)int.MaxValue + 1)));

            Linear(DelayPolicy.Linear(TimeSpan.FromMilliseconds(100)), 100);
        }

        [TestMethod]
        public void Linear_int()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Linear(-1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => DelayPolicy.Linear( 1));

            Linear(DelayPolicy.Linear(100), 100);
        }

        private void Linear(DelayHandler getDelay, int slopeMilliseconds)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => getDelay(0));

            Assert.AreEqual(TimeSpan.FromMilliseconds(slopeMilliseconds *  1), getDelay( 1));
            Assert.AreEqual(TimeSpan.FromMilliseconds(slopeMilliseconds *  2), getDelay( 2));
            Assert.AreEqual(TimeSpan.FromMilliseconds(slopeMilliseconds * 10), getDelay(10));

            Assert.ThrowsException<OverflowException>(() => getDelay(int.MaxValue));
        }
    }
}
