// Copyright © William Sugarman.
// Licensed under the MIT License.

using System.Globalization;
using System.Resources;

namespace Sweetener
{
    internal static class SR
    {
        public static string ArgumentNegativeMessage => ExceptionResourceManager.GetString(nameof(ArgumentNegativeMessage), CultureInfo.CurrentUICulture);

        public static string CollectionFixedSizeMessage => ExceptionResourceManager.GetString(nameof(CollectionFixedSizeMessage), CultureInfo.CurrentUICulture);

        public static string CollectionReadOnlyMessage => ExceptionResourceManager.GetString(nameof(CollectionReadOnlyMessage), CultureInfo.CurrentUICulture);

        public static string EmptyEnumeratorMessage => ExceptionResourceManager.GetString(nameof(EmptyEnumeratorMessage), CultureInfo.CurrentUICulture);

        public static string MutateKeyCollectionMessage => ExceptionResourceManager.GetString(nameof(MutateKeyCollectionMessage), CultureInfo.CurrentUICulture);

        public static string MutateValueCollectionMessage => ExceptionResourceManager.GetString(nameof(MutateValueCollectionMessage), CultureInfo.CurrentUICulture);

        public static string UndefinedOptionalValueMessage => ExceptionResourceManager.GetString(nameof(UndefinedOptionalValueMessage), CultureInfo.CurrentUICulture);

        private static readonly ResourceManager ExceptionResourceManager = new ResourceManager("Sweetener.Resources.Exceptions", typeof(SR).Assembly);
    }
}
