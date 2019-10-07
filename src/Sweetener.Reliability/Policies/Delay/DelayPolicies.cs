using System;

namespace Sweetener.Reliability
{
    /// <summary>
    /// Contains common strategies for deriving how long to wait in-between operation retries.
    /// </summary>
    public static class DelayPolicies
    {
        /// <summary>
        /// Returns a <see cref="DelayPolicy"/> whose delay is always <see cref="TimeSpan.Zero"/>.
        /// </summary>
        /// <value>
        /// A <see cref="DelayPolicy"/> that does not wait.
        /// </value>
        public static readonly DelayPolicy None = Constant(TimeSpan.Zero);

        /// <summary>
        /// Creates a <see cref="DelayPolicy"/> whose delay is the same regardless of the attempt.
        /// </summary>
        /// <param name="delay">The <see cref="TimeSpan"/> that represents the delay in milliseconds.</param>
        /// <returns>A <see cref="DelayPolicy"/> that waits a specific amount of time.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="delay"/> is negative.</para>
        /// <para>-or-</para>
        /// <para><paramref name="delay"/> is greater than <see cref="int.MaxValue"/> milliseconds.</para>
        /// </exception>
        public static DelayPolicy Constant(TimeSpan delay)
        {
            long delayMilliseconds = (long)delay.TotalMilliseconds;
            if (delayMilliseconds > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(delay));

            return Constant((int)delayMilliseconds);
        }

        /// <summary>
        /// Creates a <see cref="DelayPolicy"/> whose delay is the same regardless of the attempt.
        /// </summary>
        /// <param name="delayMilliseconds">The delay in milliseconds.</param>
        /// <returns>A <see cref="DelayPolicy"/> that waits a specific amount of time.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="delayMilliseconds"/> is negative.</para>
        /// <para>-or-</para>
        /// <para><paramref name="delayMilliseconds"/> is greater than <see cref="int.MaxValue"/> milliseconds.</para>
        /// </exception>
        public static DelayPolicy Constant(int delayMilliseconds)
        {
            if (delayMilliseconds < 0)
                throw new ArgumentOutOfRangeException(nameof(delayMilliseconds));

            return (int attempt) =>
            {
                if (attempt < 1)
                    throw new ArgumentOutOfRangeException(nameof(attempt), $"{nameof(attempt)} must be greater than 0.");

                return TimeSpan.FromMilliseconds(delayMilliseconds);
            };
        }

        /// <summary>
        /// Creates a <see cref="DelayPolicy"/> whose delay is on average exponentially
        /// longer with each attempt.
        /// </summary>
        /// <remarks>
        /// The i-th delay will wait between 0 to (2^i- 1) units of time to avoid
        /// multiple operations backing off in lockstep.
        /// </remarks>
        /// <param name="unit">The <see cref="TimeSpan"/> that represents the coefficient in milliseconds.</param>
        /// <returns>A <see cref="DelayPolicy"/> that employs an exponential backoff.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="unit"/> is less than or equal to <c>1</c> millisecond.</para>
        /// <para>-or-</para>
        /// <para><paramref name="unit"/> is greater than <see cref="int.MaxValue"/> milliseconds.</para>
        /// </exception>
        public static DelayPolicy Exponential(TimeSpan unit)
            => Exponential(unit, new Random());

        /// <summary>
        /// Creates a <see cref="DelayPolicy"/> whose delay is on average exponentially
        /// longer with each attempt.
        /// </summary>
        /// <remarks>
        /// The i-th delay will wait between 0 to (2^i- 1) units of time to avoid
        /// multiple operations backing off in lockstep.
        /// </remarks>
        /// <param name="unit">The <see cref="TimeSpan"/> that represents the coefficient in milliseconds.</param>
        /// <param name="random">A pseudo-random number generator used to provide jitter in the backoff algorithm.</param>
        /// <returns>A <see cref="DelayPolicy"/> that employs an exponential backoff.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="random"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="unit"/> is less than or equal to <c>1</c> millisecond.</para>
        /// <para>-or-</para>
        /// <para><paramref name="unit"/> is greater than <see cref="int.MaxValue"/> milliseconds.</para>
        /// </exception>
        public static DelayPolicy Exponential(TimeSpan unit, Random random)
        {
            long unitMilliseconds = (long)unit.TotalMilliseconds;
            if (unitMilliseconds > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(unit));

            return Exponential((int)unitMilliseconds, random);
        }

        /// <summary>
        /// Creates a <see cref="DelayPolicy"/> whose delay is on average exponentially
        /// longer with each attempt.
        /// </summary>
        /// <remarks>
        /// The i-th delay will wait between 0 to (2^i- 1) units of time to avoid
        /// multiple operations backing off in lockstep.
        /// </remarks>
        /// <param name="unitMilliseconds">The coefficient in milliseconds.</param>
        /// <returns>A <see cref="DelayPolicy"/> that employs an exponential backoff.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="unitMilliseconds"/> is less than or equal to <c>1</c>.</para>
        /// <para>-or-</para>
        /// <para><paramref name="unitMilliseconds"/> is greater than <see cref="int.MaxValue"/>.</para>
        /// </exception>
        public static DelayPolicy Exponential(int unitMilliseconds)
            => Exponential(unitMilliseconds, new Random());

        /// <summary>
        /// Creates a <see cref="DelayPolicy"/> whose delay is on average exponentially
        /// longer with each attempt.
        /// </summary>
        /// <remarks>
        /// The i-th delay will wait between 0 to (2^i- 1) units of time to avoid
        /// multiple operations backing off in lockstep.
        /// </remarks>
        /// <param name="unitMilliseconds">The coefficient in milliseconds.</param>
        /// <param name="random">A pseudo-random number generator used to provide jitter in the backoff algorithm.</param>
        /// <returns>A <see cref="DelayPolicy"/> that employs an exponential backoff.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="random"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="unitMilliseconds"/> is less than or equal to <c>1</c>.</para>
        /// <para>-or-</para>
        /// <para><paramref name="unitMilliseconds"/> is greater than <see cref="int.MaxValue"/>.</para>
        /// </exception>
        public static DelayPolicy Exponential(int unitMilliseconds, Random random)
        {
            if (unitMilliseconds <= 1 || unitMilliseconds > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(unitMilliseconds));

            if (random == null)
                throw new ArgumentNullException(nameof(random));

            return (int attempt) =>
            {
                if (attempt < 1)
                    throw new ArgumentOutOfRangeException(nameof(attempt), $"{nameof(attempt)} must be greater than 0.");

                if (attempt > 31)
                    throw new OverflowException($"Maximum delay exceeded '{int.MaxValue}' milliseconds.");

                // Since we cannot pass 2^31 as the exclusive upper bound of Random.Next(int, int),
                // we'll just offset the operation by 1 and add the 1 back afterwards
                int units = random.Next(-1, (int)((1U << attempt) - 1)) + 1;
                return TimeSpan.FromMilliseconds(checked(unitMilliseconds * units));
            };
        }

        /// <summary>
        /// Creates a <see cref="DelayPolicy"/> whose delay is directly proportional
        /// to the number of attemps.
        /// </summary>
        /// <param name="slope">
        /// The <see cref="TimeSpan"/> that represents the rate at which the delay
        /// increases in milliseconds.
        /// </param>
        /// <returns>A <see cref="DelayPolicy"/> that employs a linear backoff.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="slope"/> is a less than or equal to <c>1</c> millisecond.</para>
        /// <para>-or-</para>
        /// <para><paramref name="slope"/> is greater than <see cref="int.MaxValue"/> milliseconds.</para>
        /// </exception>
        public static DelayPolicy Linear(TimeSpan slope)
        {
            long slopeMilliseconds = (long)slope.TotalMilliseconds;
            if (slopeMilliseconds > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(slopeMilliseconds));

            return Linear((int)slopeMilliseconds);
        }

        /// <summary>
        /// Creates a <see cref="DelayPolicy"/> whose delay is directly proportional
        /// to the number of attemps.
        /// </summary>
        /// <param name="slopeMilliseconds">
        /// The rate at which the delay increases in milliseconds.
        /// </param>
        /// <returns>A <see cref="DelayPolicy"/> that employs a linear backoff.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="slopeMilliseconds"/> is a less than or equal to <c>1</c> millisecond.</para>
        /// <para>-or-</para>
        /// <para><paramref name="slopeMilliseconds"/> is greater than <see cref="int.MaxValue"/> milliseconds.</para>
        /// </exception>
        public static DelayPolicy Linear(int slopeMilliseconds)
        {
            if (slopeMilliseconds <= 1)
                throw new ArgumentOutOfRangeException(nameof(slopeMilliseconds));

            return (int attempt) =>
            {
                if (attempt < 1)
                    throw new ArgumentOutOfRangeException(nameof(attempt), $"{nameof(attempt)} must be greater than 0.");

                return TimeSpan.FromMilliseconds(checked(slopeMilliseconds * attempt));
            };
        }
    }
}
