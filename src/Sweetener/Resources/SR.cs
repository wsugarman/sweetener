using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Sweetener
{
    internal static class SR
    {
        public static string ArgumentNegativeMessage => s_exceptionResourceManager.GetString(nameof(ArgumentNegativeMessage), CultureInfo.CurrentCulture);

        public static string UndefinedOptionalValueMessage => s_exceptionResourceManager.GetString(nameof(UndefinedOptionalValueMessage), CultureInfo.CurrentCulture);

        private static readonly ResourceManager s_exceptionResourceManager = new ResourceManager("Sweetener.Resources.Exceptions", Assembly.GetExecutingAssembly());
    }
}
