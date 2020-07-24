using System;
using Sweetener.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Test
{
    [TestClass]
    public class TimeSpanExtensionsTest
    {
        [TestMethod]
        public void AddDays()
        {
            Assert.ThrowsException<ArgumentException>(() => TimeSpan.Zero    .AddDays(double.NaN             ));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.Zero    .AddDays(double.MinValue        ));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.Zero    .AddDays(double.MaxValue        ));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.Zero    .AddDays(double.PositiveInfinity));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.Zero    .AddDays(double.NegativeInfinity));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.MaxValue.AddDays( 1                      ));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.MinValue.AddDays(-1                      ));

            Assert.AreEqual(TimeSpan.MaxValue, TimeSpan.MaxValue.AddDays(0));
            Assert.AreEqual(new TimeSpan( 2,  14,  3,  4,  5), new TimeSpan( 1,  2,  3,  4,  5).AddDays( 1.5 ));
            Assert.AreEqual(new TimeSpan(-1,   2,  3,  4,  5), new TimeSpan( 1,  2,  3,  4,  5).AddDays(-2.0 ));
            Assert.AreEqual(new TimeSpan( 9,   4, -3, -4, -5), new TimeSpan(-1, -2, -3, -4, -5).AddDays(10.25));
            Assert.AreEqual(new TimeSpan(-6, -14, -3, -4, -5), new TimeSpan(-1, -2, -3, -4, -5).AddDays(-5.5 ));
        }

        [TestMethod]
        public void AddHours()
        {
            Assert.ThrowsException<ArgumentException>(() => TimeSpan.Zero    .AddHours(double.NaN             ));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.Zero    .AddHours(double.MinValue        ));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.Zero    .AddHours(double.MaxValue        ));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.Zero    .AddHours(double.PositiveInfinity));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.Zero    .AddHours(double.NegativeInfinity));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.MaxValue.AddHours( 1                      ));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.MinValue.AddHours(-1                      ));

            Assert.AreEqual(TimeSpan.MaxValue, TimeSpan.MaxValue.AddHours(0));
            Assert.AreEqual(new TimeSpan( 1,  3,  33,  4,  5), new TimeSpan( 1,  2,  3,  4,  5).AddHours( 1.5 ));
            Assert.AreEqual(new TimeSpan( 1,  0,   3,  4,  5), new TimeSpan( 1,  2,  3,  4,  5).AddHours(-2.0 ));
            Assert.AreEqual(new TimeSpan(-1,  8,  12, -4, -5), new TimeSpan(-1, -2, -3, -4, -5).AddHours(10.25));
            Assert.AreEqual(new TimeSpan(-1, -7, -33, -4, -5), new TimeSpan(-1, -2, -3, -4, -5).AddHours(-5.5 ));
        }

        [TestMethod]
        public void AddMinutes()
        {
            Assert.ThrowsException<ArgumentException>(() => TimeSpan.Zero    .AddMinutes(double.NaN             ));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.Zero    .AddMinutes(double.MinValue        ));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.Zero    .AddMinutes(double.MaxValue        ));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.Zero    .AddMinutes(double.PositiveInfinity));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.Zero    .AddMinutes(double.NegativeInfinity));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.MaxValue.AddMinutes( 1                      ));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.MinValue.AddMinutes(-1                      ));

            Assert.AreEqual(TimeSpan.MaxValue, TimeSpan.MaxValue.AddMinutes(0));
            Assert.AreEqual(new TimeSpan( 1,  2,  4,  34,  5), new TimeSpan( 1,  2,  3,  4,  5).AddMinutes( 1.5 ));
            Assert.AreEqual(new TimeSpan( 1,  2,  1,   4,  5), new TimeSpan( 1,  2,  3,  4,  5).AddMinutes(-2.0 ));
            Assert.AreEqual(new TimeSpan(-1, -2,  7,  11, -5), new TimeSpan(-1, -2, -3, -4, -5).AddMinutes(10.25));
            Assert.AreEqual(new TimeSpan(-1, -2, -8, -34, -5), new TimeSpan(-1, -2, -3, -4, -5).AddMinutes(-5.5 ));
        }

        [TestMethod]
        public void AddSeconds()
        {
            Assert.ThrowsException<ArgumentException>(() => TimeSpan.Zero    .AddSeconds(double.NaN             ));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.Zero    .AddSeconds(double.MinValue        ));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.Zero    .AddSeconds(double.MaxValue        ));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.Zero    .AddSeconds(double.PositiveInfinity));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.Zero    .AddSeconds(double.NegativeInfinity));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.MaxValue.AddSeconds( 1                      ));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.MinValue.AddSeconds(-1                      ));

            Assert.AreEqual(TimeSpan.MaxValue, TimeSpan.MaxValue.AddSeconds(0));
            Assert.AreEqual(new TimeSpan( 1,  2,  3,  5,  505), new TimeSpan( 1,  2,  3,  4,  5).AddSeconds( 1.5 ));
            Assert.AreEqual(new TimeSpan( 1,  2,  3,  2,    5), new TimeSpan( 1,  2,  3,  4,  5).AddSeconds(-2.0 ));
            Assert.AreEqual(new TimeSpan(-1, -2, -3,  6,  245), new TimeSpan(-1, -2, -3, -4, -5).AddSeconds(10.25));
            Assert.AreEqual(new TimeSpan(-1, -2, -3, -9, -505), new TimeSpan(-1, -2, -3, -4, -5).AddSeconds(-5.5 ));
        }

        [TestMethod]
        public void AddMilliseconds()
        {
            Assert.ThrowsException<ArgumentException>(() => TimeSpan.Zero    .AddMilliseconds(double.NaN             ));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.Zero    .AddMilliseconds(double.MinValue        ));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.Zero    .AddMilliseconds(double.MaxValue        ));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.Zero    .AddMilliseconds(double.PositiveInfinity));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.Zero    .AddMilliseconds(double.NegativeInfinity));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.MaxValue.AddMilliseconds( 1                      ));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.MinValue.AddMilliseconds(-1                      ));

            Assert.AreEqual(TimeSpan.MaxValue, TimeSpan.MaxValue.AddMilliseconds(0));
            Assert.AreEqual(new TimeSpan( 1,  2,  3,  4,   6).AddTicks( TimeSpan.TicksPerMillisecond / 2), new TimeSpan( 1,  2,  3,  4,  5).AddMilliseconds( 1.5 ));
            Assert.AreEqual(new TimeSpan( 1,  2,  3,  4,   3)                                            , new TimeSpan( 1,  2,  3,  4,  5).AddMilliseconds(-2.0 ));
            Assert.AreEqual(new TimeSpan(-1, -2, -3, -4,   5).AddTicks( TimeSpan.TicksPerMillisecond / 4), new TimeSpan(-1, -2, -3, -4, -5).AddMilliseconds(10.25));
            Assert.AreEqual(new TimeSpan(-1, -2, -3, -4, -10).AddTicks(-TimeSpan.TicksPerMillisecond / 2), new TimeSpan(-1, -2, -3, -4, -5).AddMilliseconds(-5.5 ));
        }

        [TestMethod]
        public void AddTicks()
        {
            Assert.ThrowsException<OverflowException>(() => TimeSpan.MaxValue.AddTicks( 1));
            Assert.ThrowsException<OverflowException>(() => TimeSpan.MinValue.AddTicks(-1));

            Assert.AreEqual(TimeSpan.MaxValue, TimeSpan.MaxValue.AddTicks(0));
            Assert.AreEqual(new TimeSpan( 12360), new TimeSpan( 12345).AddTicks(  15));
            Assert.AreEqual(new TimeSpan( 12325), new TimeSpan( 12345).AddTicks( -20));
            Assert.AreEqual(new TimeSpan(-11320), new TimeSpan(-12345).AddTicks(1025));
            Assert.AreEqual(new TimeSpan(-12400), new TimeSpan(-12345).AddTicks( -55));
        }

        [TestMethod]
        public void Truncate()
        {
            // Invalid Negative Input
            Assert.ThrowsException<ArgumentNegativeException>(() => TimeSpan.FromHours(7).Truncate(TimeSpan.FromMinutes(-3)));

            // TimeSpan.Zero graularity, a TimeSpan.Zero value, or trying to truncate a value that's already at
            // the desired granularity result in the identity function
            Assert.AreEqual(TimeSpan.MaxValue    , TimeSpan.MaxValue    .Truncate(TimeSpan.Zero        ));
            Assert.AreEqual(TimeSpan.Zero        , TimeSpan.Zero        .Truncate(TimeSpan.FromHours(2)));
            Assert.AreEqual(TimeSpan.FromDays(10), TimeSpan.FromDays(10).Truncate(TimeSpan.FromDays (5)));

            // Set to TimeSpan.Zero when truncation would result in a smaller value
            Assert.AreEqual(TimeSpan.Zero, TimeSpan.Zero.Add(TimeSpan.FromMinutes(15)).Truncate(TimeSpan.FromHours(1)));

            // Positive scenario
            TimeSpan positive = new TimeSpan(7, 6, 5, 4, 3);
            Assert.AreEqual(new TimeSpan(7, 6, 5, 0, 0), positive.Truncate(new TimeSpan(0, 2, 30)));

            // Negative scenario (value moves towards TimeSpan.Zero)
            TimeSpan negative = new TimeSpan(1, 2, 3, 4, 5).Negate();
            Assert.AreEqual(new TimeSpan(1, 2, 0, 0, 0).Negate(), negative.Truncate(TimeSpan.FromHours(1)));
        }
    }
}
