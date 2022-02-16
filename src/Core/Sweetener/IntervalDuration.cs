// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace Sweetener;

[SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types", Justification = "Equality is never checked.")]
internal readonly struct IntervalDuration : IFormattable
{
    private const char Prefix = 'P';
    private const char TimeSeparator = 'T';
    private const char YearSuffix = 'Y';
    private const char MonthSuffix = 'M';
    private const char DaySuffix = 'D';
    private const char HourSuffix = 'H';
    private const char MinuteSuffix = 'M';
    private const char SecondSuffix = 'S';

    private const string CompleteFormatSpecifier = "C";
    private const string MinimizedCompleteFormatSpecifier = "C-";
    private const string CompleteDurationFormat = "P{0}Y{1}M{2:%d}DT{2:%h}H{2:%m}M{2:%s\\.FFFFFFF}S";

    public int Years { get; }

    public int Months { get; }

    public int Days => _remaining.Days;

    public int Hours => _remaining.Hours;

    public int Minutes => _remaining.Minutes;

    public int Seconds => _remaining.Seconds;

    private readonly TimeSpan _remaining;

    private IntervalDuration(int years, int months, TimeSpan remaining)
    {
        Years = years;
        Months = months;
        _remaining = remaining;
    }

    public override string ToString()
        => ToString();

    // Note: formatProvider is unused
    public string ToString(string format, IFormatProvider formatProvider)
    {
        // ISO 8601 defines a couple representations for durations,
        // but for now we'll only support the first complete representation 5.5.2.2.a
        // which we'll denote with the format 'C'
        //
        // Like DateSpan[Offset], we'll also support the '-' character to indicate minimizing the value

        if (format is null || format.Length == 0 || format == CompleteFormatSpecifier)
            return string.Format(CultureInfo.InvariantCulture, CompleteDurationFormat, Years, Months, _remaining);

        if (format != MinimizedCompleteFormatSpecifier)
            throw new FormatException(format);

        TimeSpan remaining = _remaining;
        StringBuilder buffer = new StringBuilder();
        buffer.Append(Prefix);

        if (Years > 0)
        {
            buffer.Append(Years.ToString(CultureInfo.InvariantCulture));
            buffer.Append(YearSuffix);
        }

        if (Months > 0)
        {
            buffer.Append(Months.ToString(CultureInfo.InvariantCulture));
            buffer.Append(MonthSuffix);
        }

        if (Days > 0)
        {
            buffer.Append(Days.ToString(CultureInfo.InvariantCulture));
            buffer.Append(DaySuffix);

            remaining -= TimeSpan.FromDays(Days);
        }

        if (buffer.Length > 1 && _remaining.Ticks % TimeSpan.TicksPerDay != 0)
            buffer.Append(TimeSeparator);

        if (Hours > 0)
        {
            buffer.Append(Hours.ToString(CultureInfo.InvariantCulture));
            buffer.Append(HourSuffix);

            remaining -= TimeSpan.FromHours(Hours);
        }

        if (Minutes > 0)
        {
            buffer.Append(Minutes.ToString(CultureInfo.InvariantCulture));
            buffer.Append(MinuteSuffix);

            remaining -= TimeSpan.FromMinutes(Minutes);
        }

        if (buffer.Length == 1 || remaining.Ticks > 0)
        {
            buffer.Append(Seconds.ToString(CultureInfo.InvariantCulture));
            remaining -= TimeSpan.FromSeconds(Seconds);

            if (remaining.Ticks > 0)
                buffer.Append(remaining.ToString("\\.FFFFFFF", CultureInfo.InvariantCulture));

            buffer.Append(SecondSuffix);
        }

        return buffer.ToString();
    }

    public static IntervalDuration FromInterval(DateTime start, DateTime end)
    {
        if (start == end)
            return default;

        // TODO: There is probably a better way to derive this

        // Derive difference in years
        // (Remember that each year may have a different number of days per month)
        int years = end.Year - start.Year;
        end = end.AddYears(-years);
        if (end < start)
        {
            years--;
            end = end.AddYears(1);
        }

        // Derive difference in months
        int months = end.Month - start.Month;
        end = end.AddMonths(-months);
        if (end < start)
        {
            months--;
            end = end.AddMonths(1);
        }

        return new IntervalDuration(years, months, end - start);
    }
}
