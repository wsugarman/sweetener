using System;

namespace Sweetener.Reliability.Test
{
    internal class DelegateProxy
    {
        public int Calls => _context.Calls;

        protected CallContext _context = default;

        private DateTime _lastCallUtc = default;

        protected void UpdateContext()
        {
            DateTime utcNow = DateTime.UtcNow;
            TimeSpan delay = _lastCallUtc == default
                ? TimeSpan.FromMilliseconds(-1)
                : utcNow - _lastCallUtc;

            _context     = new CallContext(_context.Calls + 1, delay);
            _lastCallUtc = utcNow;
        }
    }
}
