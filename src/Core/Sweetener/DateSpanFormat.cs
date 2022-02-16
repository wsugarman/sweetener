// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Sweetener;

internal static class DateSpanFormat
{
    /*
     * Pattern (where capital letters indicate extended)
     *  - Ii (S/E)
     *  - Ss (S/E)
     *  - Ee (D/E)
     * Minimize
     * 
     * Proposal 1: <format>[-]
     * Example: P-
     */
    internal const string BasicDateTimeFormat    = "yyyyMMdd'T'HHmmss.FFFFFFFK";
    internal const string ExtendedDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

    internal const string BasicStartEndFormat         = $"{{0:{BasicDateTimeFormat}}}/{{1:{BasicDateTimeFormat}}}";
    internal const string ExtendedStartEndFormat      = $"{{0:{ExtendedDateTimeFormat}}}/{{1:{ExtendedDateTimeFormat}}}";
    internal const string BasicStartDurationFormat    = $"{{0:{BasicDateTimeFormat}}}/{{1:C}}";
    internal const string ExtendedStartDurationFormat = $"{{0:{ExtendedDateTimeFormat}}}/{{1:C}}";
    internal const string BasicDurationEndFormat      = $"{{0:C}}/{{1:{BasicDateTimeFormat}}}";
    internal const string ExtendedDurationEndFormat   = $"{{0:C}}/{{1:{ExtendedDateTimeFormat}}}";

    public static string Format(DateSpan dateSpan, string? format)
    {
        FormatOptions options = format is null || format.Length == 0
            ? new FormatOptions { Extended = true, Representation = IsoRepresentation.StartEnd, Minimize = false }
            : ParseStandardFormat(format);

        return options.Representation switch
        {
            IsoRepresentation.StartEnd      => FormatStartEnd     (dateSpan.Start, dateSpan.End, options),
            IsoRepresentation.StartDuration => FormatStartDuration(dateSpan.Start, dateSpan.End, options),
            _                               => FormatDurationEnd  (dateSpan.Start, dateSpan.End, options),
        };
    }

    private static FormatOptions ParseStandardFormat(string format)
    {
        if (format.Length == 1 || format.Length == 2)
        {
            bool minimize = format.Length == 2 && format[1] == '-';
            return format[0] switch
            {
                'i' => new FormatOptions { Extended = false, Minimize = minimize, Representation = IsoRepresentation.StartEnd      },
                'I' => new FormatOptions { Extended = true , Minimize = minimize, Representation = IsoRepresentation.StartEnd      },
                's' => new FormatOptions { Extended = false, Minimize = minimize, Representation = IsoRepresentation.StartDuration },
                'S' => new FormatOptions { Extended = true , Minimize = minimize, Representation = IsoRepresentation.StartDuration },
                'e' => new FormatOptions { Extended = false, Minimize = minimize, Representation = IsoRepresentation.DurationEnd   },
                'E' => new FormatOptions { Extended = true , Minimize = minimize, Representation = IsoRepresentation.DurationEnd   },
                _ => throw new FormatException(SR.InvalidInputStringFormatMessage),
            };
        }

        throw new FormatException(SR.InvalidInputStringFormatMessage);
    }

    private static string FormatStartEnd(DateTime start, DateTime end, FormatOptions options)
    {
        if (!options.Minimize)
            return options.Extended
                ? string.Format(CultureInfo.InvariantCulture, ExtendedStartEndFormat, start, DateTime.SpecifyKind(end, DateTimeKind.Unspecified))
                : string.Format(CultureInfo.InvariantCulture, BasicStartEndFormat   , start, DateTime.SpecifyKind(end, DateTimeKind.Unspecified));


        return "";
    }

    private static string FormatStartDuration(DateTime start, DateTime end, FormatOptions options)
    {
        IntervalDuration duration = IntervalDuration.FromInterval(start, end);

        if (!options.Minimize)
            return options.Extended
                ? string.Format(CultureInfo.InvariantCulture, ExtendedStartDurationFormat, start, duration)
                : string.Format(CultureInfo.InvariantCulture, BasicStartDurationFormat   , start, duration);

        return "";
    }

    private static string FormatDurationEnd(DateTime start, DateTime end, FormatOptions options)
    {
        IntervalDuration duration = IntervalDuration.FromInterval(start, end);

        if (!options.Minimize)
            return options.Extended
                ? string.Format(CultureInfo.InvariantCulture, ExtendedDurationEndFormat, duration, end)
                : string.Format(CultureInfo.InvariantCulture, BasicDurationEndFormat   , duration, end);

        return "";
    }

    [SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types", Justification = "Private type")]
    private readonly struct FormatOptions
    {
        public IsoRepresentation Representation { get; init; }

        public bool Minimize { get; init; }

        public bool Extended { get; init; }

        public int FractionalPrecision { get; init; }
    }

    private enum IsoRepresentation
    {
        StartEnd,
        StartDuration,
        DurationEnd,
    }
}
