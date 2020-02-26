using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Sweetener.Reliability
{
    internal static class SR
    {
        public static string InvalidTaskResult => _exceptionResourceManager.GetString(nameof(InvalidTaskResult), CultureInfo.CurrentCulture);

        public static string MaximumDelayOverflow => _exceptionResourceManager.GetString(nameof(MaximumDelayOverflow), CultureInfo.CurrentCulture);

        private static readonly ResourceManager _exceptionResourceManager = new ResourceManager("Sweetener.Reliability.Resources.Exceptions", Assembly.GetExecutingAssembly());
    }
}
