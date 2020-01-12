using System;

namespace Sweetener.Reliability.Test
{
    internal abstract class DelegateProxy<T>
        where T : Delegate
    {
        public event Action<CallContext> Invoking;

        public int Calls => _context.Calls;

        public abstract T Proxy { get; }

        protected T _delegate { get; }

        protected CallContext _context = default;

        private DateTime _lastCallUtc = default;

        public DelegateProxy(T underlyingDelegate)
        {
            _delegate = underlyingDelegate ?? throw new ArgumentNullException(nameof(underlyingDelegate));
        }

        protected void UpdateContext()
        {
            DateTime utcNow = DateTime.UtcNow;
            TimeSpan delay  = _lastCallUtc == default ? TimeSpan.FromMilliseconds(-1) : utcNow - _lastCallUtc;

            _context     = new CallContext(_context.Calls + 1, delay);
            _lastCallUtc = utcNow;
        }

        protected void OnInvoking()
            => Invoking?.Invoke(_context);
    }
}
