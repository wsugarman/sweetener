// Copyright © William Sugarman.
// Licensed under the MIT License.

using System.Globalization;
using System.Resources;

namespace Sweetener
{
    internal static class SR
    {
        public static string ArgumentMustBeTypeFormatString => ExceptionResourceManager.GetString(nameof(ArgumentMustBeTypeFormatString), CultureInfo.CurrentUICulture);

        public static string ArgumentNegativeMessage => ExceptionResourceManager.GetString(nameof(ArgumentNegativeMessage), CultureInfo.CurrentUICulture);

        public static string InvalidCastFormatString => ExceptionResourceManager.GetString(nameof(InvalidCastFormatString), CultureInfo.CurrentUICulture);

        public static string OverflowCharMessage => ExceptionResourceManager.GetString(nameof(OverflowCharMessage), CultureInfo.CurrentUICulture);

        public static string OverflowSByteMessage => ExceptionResourceManager.GetString(nameof(OverflowSByteMessage), CultureInfo.CurrentUICulture);

        public static string OverflowByteMessage => ExceptionResourceManager.GetString(nameof(OverflowByteMessage), CultureInfo.CurrentUICulture);

        public static string OverflowInt16Message => ExceptionResourceManager.GetString(nameof(OverflowInt16Message), CultureInfo.CurrentUICulture);

        public static string OverflowUInt16Message => ExceptionResourceManager.GetString(nameof(OverflowUInt16Message), CultureInfo.CurrentUICulture);

        public static string OverflowInt32Message => ExceptionResourceManager.GetString(nameof(OverflowInt32Message), CultureInfo.CurrentUICulture);

        public static string OverflowUInt32Message => ExceptionResourceManager.GetString(nameof(OverflowUInt32Message), CultureInfo.CurrentUICulture);

        public static string OverflowInt64Message => ExceptionResourceManager.GetString(nameof(OverflowInt64Message), CultureInfo.CurrentUICulture);

        public static string OverflowUInt64Message => ExceptionResourceManager.GetString(nameof(OverflowUInt64Message), CultureInfo.CurrentUICulture);

        public static string UndefinedOptionalValueMessage => ExceptionResourceManager.GetString(nameof(UndefinedOptionalValueMessage), CultureInfo.CurrentUICulture);

        private static readonly ResourceManager ExceptionResourceManager = new ResourceManager("Sweetener.Resources.Exceptions", typeof(SR).Assembly);
    }
}
