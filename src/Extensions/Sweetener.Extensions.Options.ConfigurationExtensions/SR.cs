// Copyright © William Sugarman.
// Licensed under the MIT License.

using System.Globalization;
using System.Resources;

namespace Sweetener.Extensions.Options;

internal static class SR
{
    public static string InvalidConfigurationSectionFormat => ExceptionResourceManager.GetString(nameof(InvalidConfigurationSectionFormat), CultureInfo.CurrentUICulture);

    private static readonly ResourceManager ExceptionResourceManager = new ResourceManager("Sweetener.Extensions.Options.Resources.Exceptions", typeof(SR).Assembly);

    public static string Format(string format, object arg0)
        => string.Format(CultureInfo.CurrentCulture, format, arg0);
}
