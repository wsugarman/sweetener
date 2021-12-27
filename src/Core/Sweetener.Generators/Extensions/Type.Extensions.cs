// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Reflection;

namespace Sweetener.Generators.Extensions
{
    internal static class TypeExtensions
    {
        public static bool HasDefaultCtor(this Type type)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            return type.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null) is not null;
        }

        public static bool InheritsFrom(this Type type, Type baseType)
        {
            

            return type.BaseType is not null && (type.BaseType == baseType || type.BaseType.InheritsFrom(baseType));
        }
    }
}
