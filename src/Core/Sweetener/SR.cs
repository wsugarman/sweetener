// Copyright © William Sugarman.
// Licensed under the MIT License.

using System.Globalization;
using System.Resources;

namespace Sweetener;

internal static class SR
{
    public static string ArgumentNegativeMessage => ExceptionResourceManager.GetString(nameof(ArgumentNegativeMessage), CultureInfo.CurrentUICulture)!;

    public static string ArgumentEmptyMessage => ExceptionResourceManager.GetString(nameof(ArgumentEmptyMessage), CultureInfo.CurrentUICulture)!;

    public static string ArgumentWhiteSpaceMessage => ExceptionResourceManager.GetString(nameof(ArgumentWhiteSpaceMessage), CultureInfo.CurrentUICulture)!;

    public static string ClosedStreamMessage => ExceptionResourceManager.GetString(nameof(ClosedStreamMessage), CultureInfo.CurrentUICulture)!;

    public static string EndBeforeStartMessage => ExceptionResourceManager.GetString(nameof(EndBeforeStartMessage), CultureInfo.CurrentUICulture)!;

    public static string InvalidDateSpanRangeMessage => ExceptionResourceManager.GetString(nameof(InvalidDateSpanRangeMessage), CultureInfo.CurrentUICulture)!;

    public static string InvalidTypeFormat => ExceptionResourceManager.GetString(nameof(InvalidTypeFormat), CultureInfo.CurrentUICulture)!;

    public static string KindMismatchMessage => ExceptionResourceManager.GetString(nameof(KindMismatchMessage), CultureInfo.CurrentUICulture)!;

    public static string MissingOptionalValueMessage => ExceptionResourceManager.GetString(nameof(MissingOptionalValueMessage), CultureInfo.CurrentUICulture)!;

    public static string StreamSeekNotSupportedMessage => ExceptionResourceManager.GetString(nameof(StreamSeekNotSupportedMessage), CultureInfo.CurrentUICulture)!;

    public static string StreamWriteNotSupportedMessage => ExceptionResourceManager.GetString(nameof(StreamWriteNotSupportedMessage), CultureInfo.CurrentUICulture)!;

    public static string ValueNotCreatedMessage => ExceptionResourceManager.GetString(nameof(ValueNotCreatedMessage), CultureInfo.CurrentUICulture)!;

    private static readonly ResourceManager ExceptionResourceManager = new ResourceManager("Sweetener.Resources.Exceptions", typeof(SR).Assembly);

    public static string Format(string format, object? arg0)
        => string.Format(CultureInfo.CurrentCulture, format, arg0);
}
