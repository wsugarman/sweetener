// Copyright © William Sugarman.
// Licensed under the MIT License.

using System.Globalization;
using System.Resources;

namespace Sweetener
{
    internal static class SR
    {
        public static string ArgumentNegativeMessage => ExceptionResourceManager.GetString(nameof(ArgumentNegativeMessage), CultureInfo.CurrentUICulture);

        public static string EmptyEnumeratorMessage => ExceptionResourceManager.GetString(nameof(EmptyEnumeratorMessage), CultureInfo.CurrentUICulture);

        public static string MissingOptionalValueMessage => ExceptionResourceManager.GetString(nameof(MissingOptionalValueMessage), CultureInfo.CurrentUICulture);

        private static readonly ResourceManager ExceptionResourceManager = new ResourceManager("Sweetener.Resources.Exceptions", typeof(SR).Assembly);
    }
}
