// Copyright © William Sugarman.
// Licensed under the MIT License.

using System.Globalization;
using System.Resources;

namespace Sweetener
{
    internal static class SR
    {
        public static string ArgumentNegativeMessage => ExceptionResourceManager.GetString(nameof(ArgumentNegativeMessage), CultureInfo.CurrentUICulture);

        public static string ArgumentNullOrEmptyMessage => ExceptionResourceManager.GetString(nameof(ArgumentNullOrEmptyMessage), CultureInfo.CurrentUICulture);

        public static string ArgumentNullOrWhiteSpaceMessage => ExceptionResourceManager.GetString(nameof(ArgumentNullOrWhiteSpaceMessage), CultureInfo.CurrentUICulture);

        public static string EmptyDateSpanMessage => ExceptionResourceManager.GetString(nameof(EmptyDateSpanMessage), CultureInfo.CurrentUICulture);

        public static string EmptyEnumeratorMessage => ExceptionResourceManager.GetString(nameof(EmptyEnumeratorMessage), CultureInfo.CurrentUICulture);

        public static string EndBeforeStartMessage => ExceptionResourceManager.GetString(nameof(EndBeforeStartMessage), CultureInfo.CurrentUICulture);

        public static string InvalidDateSpanEndMessage => ExceptionResourceManager.GetString(nameof(InvalidDateSpanEndMessage), CultureInfo.CurrentUICulture);

        public static string InvalidDateSpanRangeMessage => ExceptionResourceManager.GetString(nameof(InvalidDateSpanRangeMessage), CultureInfo.CurrentUICulture);

        public static string KindMismatchMessage => ExceptionResourceManager.GetString(nameof(KindMismatchMessage), CultureInfo.CurrentUICulture);

        public static string MissingOptionalValueMessage => ExceptionResourceManager.GetString(nameof(MissingOptionalValueMessage), CultureInfo.CurrentUICulture);

        private static readonly ResourceManager ExceptionResourceManager = new ResourceManager("Sweetener.Resources.Exceptions", typeof(SR).Assembly);
    }
}
