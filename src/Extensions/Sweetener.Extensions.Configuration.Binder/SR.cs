// Copyright © William Sugarman.
// Licensed under the MIT License.

using System.Globalization;
using System.Resources;

namespace Sweetener.Extensions.Configuration
{
    internal static class SR
    {
        public static string InvalidConfigurationSectionFormat => ExceptionResourceManager.GetString(nameof(InvalidConfigurationSectionFormat), CultureInfo.CurrentUICulture);

        private static readonly ResourceManager ExceptionResourceManager = new ResourceManager("Sweetener.Extensions.Configuration.Resources.Exceptions", typeof(SR).Assembly);

        public static string Format(string format, object arg0)
            => string.Format(CultureInfo.CurrentCulture, format, arg0);
    }
}
