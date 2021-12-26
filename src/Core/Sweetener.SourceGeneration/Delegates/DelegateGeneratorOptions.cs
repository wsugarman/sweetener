// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;

namespace Sweetener.SourceGeneration.Delegates
{
    internal readonly struct DelegateGeneratorOptions : IEquatable<DelegateGeneratorOptions>
    {
        public string Namespace { get; }

        public int TypeOverloads { get; }

        public DelegateGeneratorOptions(string @namespace, int typeOverloads)
        {
            if (string.IsNullOrWhiteSpace(@namespace))
                throw new ArgumentNullException(nameof(@namespace));

            if (typeOverloads < 0)
                throw new ArgumentOutOfRangeException(nameof(typeOverloads));

            Namespace = @namespace;
            TypeOverloads = typeOverloads;
        }

        public override bool Equals(object obj)
            => obj is DelegateGeneratorOptions other && Equals(other);

        public bool Equals(DelegateGeneratorOptions other)
            => Namespace == other.Namespace && TypeOverloads == other.TypeOverloads;

        public override int GetHashCode()
            => Namespace.GetHashCode() ^ TypeOverloads.GetHashCode();

        public static bool operator ==(DelegateGeneratorOptions left, DelegateGeneratorOptions right)
            => left.Equals(right);

        public static bool operator !=(DelegateGeneratorOptions left, DelegateGeneratorOptions right)
            => !left.Equals(right);
    }
}
