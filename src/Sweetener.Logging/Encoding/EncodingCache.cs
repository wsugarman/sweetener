// A familiar class from CoreFX that defines commonly used encodings

using System.Text;

namespace Sweetener.Logging
{
    internal static class EncodingCache
    {
        public static readonly Encoding UTF8NoBOM = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);
    }
}
