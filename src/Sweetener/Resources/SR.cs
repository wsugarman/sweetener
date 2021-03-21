// Copyright © William Sugarman.
// Licensed under the MIT License.

using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Sweetener
{
    internal static class SR
    {
        public static string ArgumentNegativeMessage => ExceptionResourceManager.GetString(nameof(ArgumentNegativeMessage), CultureInfo.CurrentCulture);

        public static string UndefinedOptionalValueMessage => ExceptionResourceManager.GetString(nameof(UndefinedOptionalValueMessage), CultureInfo.CurrentCulture);

        private static readonly ResourceManager ExceptionResourceManager = new ResourceManager("Sweetener.Resources.Exceptions", Assembly.GetExecutingAssembly());
    }
}
