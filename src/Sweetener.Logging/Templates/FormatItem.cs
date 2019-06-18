using System;

namespace Sweetener.Logging
{
    internal readonly struct FormatItem
    {
        public readonly int Index;

        public readonly int? Alignment;

        public readonly string Format;

        public FormatItem(int index, int? alignment, string format)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            Index     = index;
            Alignment = alignment;
            Format    = format;
        }

        public override string ToString()
            => ToString(Index);

        public string ToString(int index)
        {
            string formatString = "{" + index;
            if (Alignment != null)
                formatString += "," + Alignment;

            if (Format != null)
                formatString += ":" + Format;

            return formatString + "}";
        }
    }
}
