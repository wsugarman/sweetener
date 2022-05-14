// Copyright © William Sugarman.
// Licensed under the MIT License.

using System.Globalization;
using System.Resources;

namespace Sweetener.Linq;

internal static class SR
{
    public static string EmptyEnumeratorMessage => ExceptionResourceManager.GetString(nameof(EmptyEnumeratorMessage), CultureInfo.CurrentUICulture)!;

    private static readonly ResourceManager ExceptionResourceManager = new ResourceManager("Sweetener.Linq.Resources.Exceptions", typeof(SR).Assembly);
}
