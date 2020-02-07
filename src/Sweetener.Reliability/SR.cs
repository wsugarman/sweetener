using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Sweetener.Reliability
{
    internal static class SR
    {
        public static string InvalidTaskResult => exceptionResourceManager.GetString("InvalidTaskResult", CultureInfo.CurrentCulture);

        private static readonly ResourceManager exceptionResourceManager = new ResourceManager("Sweetener.Reliability.Resources.Exceptions", Assembly.GetExecutingAssembly());
    }
}
