using System;

namespace Sweetener.Reliability.Test
{
    internal sealed class NotSoRandom : Random
    {
        public event Action OnNext;

        public event Action<int> OnNextMax;

        public event Action<int, int> OnNextRange;

        public event Action<byte[]> OnNextByteArray;

        public event Action OnNextDouble;

        public int NextIntValue { get; set; }

        public byte[] NextByteArrayValue { get; set; }

        public double NextDoubleValue { get; set; }

        public override int Next()
        {
            OnNext?.Invoke();
            return NextIntValue;
        }

        public override int Next(int maxValue)
        {
            if (NextIntValue >= maxValue)
                throw new InvalidOperationException($"{nameof(NextIntValue)} '{NextIntValue}' is too large for {nameof(maxValue)} '{maxValue}'");

            OnNextMax?.Invoke(maxValue);
            return NextIntValue;
        }

        public override int Next(int minValue, int maxValue)
        {
            if (NextIntValue < minValue)
                throw new InvalidOperationException($"{nameof(NextIntValue)} '{NextIntValue}' is too small for {nameof(minValue)} '{minValue}'");

            if (NextIntValue >= maxValue)
                throw new InvalidOperationException($"{nameof(NextIntValue)} '{NextIntValue}' is too large for {nameof(maxValue)} '{maxValue}'");

            OnNextRange?.Invoke(minValue, maxValue);
            return NextIntValue;
        }

        public override void NextBytes(byte[] buffer)
        {
            OnNextByteArray?.Invoke(buffer);
            NextByteArrayValue.CopyTo(buffer, 0);
        }
   

        public override void NextBytes(Span<byte> buffer)
        {
            OnNextByteArray?.Invoke(buffer.ToArray());
            NextByteArrayValue.CopyTo(buffer);
        }

        public override double NextDouble()
        {
            OnNextDouble?.Invoke();
            return NextDoubleValue;
        }
    }
}
