// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;

namespace Sweetener
{
    internal readonly struct DateSpan : IComparable, IComparable<DateSpan>, IEquatable<DateSpan>
    {
        private readonly long _ticks;

        public static readonly DateSpan MinValue = new DateSpan(DateTime.MinValue, 0L);
        public static readonly DateSpan MaxValue = new DateSpan(DateTime.MinValue, DateTime.MaxValue);

        public bool IsEmpty => _ticks == 0;

        public DateTimeKind Kind => Start.Kind;

        public DateTime Start { get; }

        public DateTime End { get; }

        public DateSpan(DateTime start, DateTime end)
            : this(start, (end - start).Ticks)
        { }

        public DateSpan(DateTime start, TimeSpan range)
            : this(start, range.Ticks)
        { }

        public DateSpan(DateTime start, long ticks)
        {
            if (ticks == 0)
            {
                // Normalize empty sets
                Start = DateTime.MinValue;
                _ticks = ticks;
            }
            else if (ticks < 0)
            {
                if (start.Ticks < -ticks)
                    throw new ArgumentOutOfRangeException("Effective start less than minimum DateTime", innerException: null);

                Start = start.AddTicks(ticks);
                _ticks = ticks;
            }
            else
            {
                if ((DateTime.MaxValue.Ticks - start.Ticks + 1) < ticks)
                    throw new ArgumentOutOfRangeException("Effective end greater than maximum DateTime", innerException: null);

                Start = start;
                _ticks = ticks;
            }
        }

        public DateSpan IntersectWith(DateSpan other)
        {
            if (IsEmpty || other.IsEmpty)
                return default;

            if (_start < other._start)
            {
                if (_start.Ticks + _ticks > other._start.Ticks)
                {

                }
                else
                {
                    return default;
                }
            }
        }

         public bool IsProperSubsetOf(DateSpan other)
            => other._ticks == 0 || (other.Start > Start && other._ticks < _ticks);

        public bool IsSubsetOf(DateSpan other)
            => other._ticks == 0 || (other.Start >= Start && other._ticks <= _ticks);

        public bool Contains(DateTime value)
            => _ticks != 0 && value >= Start && (value - Start).Ticks < (_ticks + 1);

        public int CompareTo(object obj)
        {
            if (obj is null)
                return 1;

            if (obj is not DateSpan other)
                throw new ArgumentException("Argument must be a DateSpan");

            return CompareTo(other);
        }

        public int CompareTo(DateSpan other)
        {
            int cmp = Start.CompareTo(other.Start);
            return cmp == 0 ? _ticks.CompareTo(other._ticks) : cmp;
        }

        public override bool Equals(object obj)
            => obj is DateSpan other && Equals(other);

        public bool Equals(DateSpan other)
            => Start.Equals(other.Start) && _ticks.Equals(other._ticks);

        public override int GetHashCode()
            => Start.GetHashCode() ^ _ticks.GetHashCode();

        public static bool operator ==(DateSpan left, DateSpan right)
            => left.Equals(right);

        public static bool operator !=(DateSpan left, DateSpan right)
            => !left.Equals(right);

        public static bool operator <(DateSpan left, DateSpan right)
            => left.CompareTo(right) < 0;

        public static bool operator <=(DateSpan left, DateSpan right)
            => left.CompareTo(right) <= 0;

        public static bool operator >(DateSpan left, DateSpan right)
            => left.CompareTo(right) > 0;

        public static bool operator >=(DateSpan left, DateSpan right)
            => left.CompareTo(right) >= 0;
    }
}
