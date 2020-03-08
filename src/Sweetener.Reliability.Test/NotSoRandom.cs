using System;

namespace Sweetener.Reliability.Test
{
    internal sealed class NotSoRandom : Random
    {
        public event Action<int, int>? OnNext;

        public int NextValue { get; set; }

        public override int Next()
            => throw new NotImplementedException();

        public override int Next(int maxValue)
            => throw new NotImplementedException();

        public override int Next(int minValue, int maxValue)
        {
            if (NextValue < minValue)
                throw new ArgumentOutOfRangeException($"{nameof(NextValue)} '{NextValue}' is too small for {nameof(minValue)} '{minValue}'");

            if (NextValue >= maxValue)
                throw new ArgumentOutOfRangeException($"{nameof(NextValue)} '{NextValue}' is too large for {nameof(maxValue)} '{maxValue}'");

            OnNext?.Invoke(minValue, maxValue);
            return NextValue;
        }

        public override void NextBytes(byte[] buffer)
            => throw new NotImplementedException();

        public override void NextBytes(Span<byte> buffer)
            => throw new NotImplementedException();

        public override double NextDouble()
            => throw new NotImplementedException();
    }
}
