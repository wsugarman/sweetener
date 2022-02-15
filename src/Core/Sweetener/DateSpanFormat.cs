// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

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
    internal const string BasicDateTimeFormat = "yyyyMMdd'T'HHmmss.FFFFFFFK";
    internal const string ExtendedDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
    internal const string DurationFormat = "[“P”][i][“Y”][i][“M”][i][“D”][“T”][i][“H”][i][“M”][i][“S”]";

    public static string Format(DateSpan dateSpan, string? format)
    {
        if (format is not null)
        {
        }
    }

    private static string Format(DateSpan dateSpan, IsoIntervalFormat format)
    {
        switch (format.Interval)
        {
            case IsoInterval.StartEnd:
                return "";
            case IsoInterval.StartDuration:
                return "";
            default: // IsoInterval.DurationEnd
                return "";
        }
    }

    private static bool TryParseStandardFormat(string format, out IsoIntervalFormat intervalFormat)
    {
        if (format.Length == 1 || format.Length == 2)
        {
            bool extended = format.Length == 2 && format[1] == '+';
            switch (format[0])
            {
                case 'p':
                    intervalFormat = new IsoIntervalFormat { Extended = extended, Interval = IsoInterval.StartEnd, Minimize = true };
                    return true;
                case 'P':
                    intervalFormat = new IsoIntervalFormat { Extended = extended, Interval = IsoInterval.StartEnd, Minimize = false };
                    return true;
                case 's':
                    intervalFormat = new IsoIntervalFormat { Extended = extended, Interval = IsoInterval.StartDuration, Minimize = true };
                    return true;
                case 'S':
                    intervalFormat = new IsoIntervalFormat { Extended = extended, Interval = IsoInterval.StartDuration, Minimize = false };
                    return true;
                case 'e':
                    intervalFormat = new IsoIntervalFormat { Extended = extended, Interval = IsoInterval.DurationEnd, Minimize = true };
                    return true;
                case 'E':
                    intervalFormat = new IsoIntervalFormat { Extended = extended, Interval = IsoInterval.DurationEnd, Minimize = false };
                    return true;
            }
        }

        intervalFormat = default;
        return false;
    }

    [SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types", Justification = "Private type")]
    private readonly struct IsoIntervalFormat
    {
        public IsoInterval Interval { get; init; }

        public bool Minimize { get; init; }

        public bool Extended { get; init; }

        public int FractionalPrecision { get; init; }
    }

    private enum IsoInterval
    {
        Unknown,
        StartEnd,
        StartDuration,
        DurationEnd,
    }
}
