using System;

namespace Sweetener.Logging.Test
{
    public static class DateTimeExtensions
    {
        public static DateTime Truncate(this DateTime dateTime, TimeSpan resolution)
            => new DateTime(dateTime.Ticks - (dateTime.Ticks % resolution.Ticks));
    }
}
