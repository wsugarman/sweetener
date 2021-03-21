// Copyright © William Sugarman.
// Licensed under the MIT License.

// This file defines the attributes used to describe an API within a nullable context. While these types are defined
// for more recent target frameworks, they are not available for .NET Standard 2.0 libraries by default. The attributes
// defined in this file are both described in Microsoft's documentation and can be found in the runtime/dotnet
// repository on GitHub.
//
// Documentation: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/attributes/nullable-analysis
// Source: https://github.com/dotnet/runtime/blob/v5.0.0/src/libraries/System.Private.CoreLib/src/System/Diagnostics/CodeAnalysis/NullableAttributes.cs

#if NETSTANDARD2_0

namespace System.Diagnostics.CodeAnalysis
{
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false)]
    internal sealed class AllowNullAttribute : Attribute
    { }

    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false)]
    internal sealed class DisallowNullAttribute : Attribute
    { }

    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, Inherited = false)]
    internal sealed class MaybeNullAttribute : Attribute
    { }

    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, Inherited = false)]
    internal sealed class NotNullAttribute : Attribute
    { }

    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    internal sealed class MaybeNullWhenAttribute : Attribute
    {
        public bool ReturnValue { get; }

        public MaybeNullWhenAttribute(bool returnValue)
            => ReturnValue = returnValue;
    }

    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    internal sealed class NotNullWhenAttribute : Attribute
    {
        public bool ReturnValue { get; }

        public NotNullWhenAttribute(bool returnValue)
            => ReturnValue = returnValue;
    }

    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
    internal sealed class NotNullIfNotNullAttribute : Attribute
    {
        public string ParameterName { get; }

        public NotNullIfNotNullAttribute(string parameterName)
            => ParameterName = parameterName;
    }

    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    internal sealed class DoesNotReturnAttribute : Attribute
    { }

    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    internal sealed class DoesNotReturnIfAttribute : Attribute
    {
        public bool ParameterValue { get; }

        public DoesNotReturnIfAttribute(bool parameterValue)
            => ParameterValue = parameterValue;
    }

    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    internal sealed class MemberNotNullAttribute : Attribute
    {
        public string[] Members { get; }

        [SuppressMessage("Design", "CA1019:Define accessors for attribute arguments", Justification = "This API is pre-defined.")]
        public MemberNotNullAttribute(string member)
            : this(new string[] { member })
        { }

        public MemberNotNullAttribute(params string[] members)
            => Members = members;
    }

    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    internal sealed class MemberNotNullWhenAttribute : Attribute
    {
        public bool ReturnValue { get; }

        public string[] Members { get; }

        [SuppressMessage("Design", "CA1019:Define accessors for attribute arguments", Justification = "This API is pre-defined.")]
        public MemberNotNullWhenAttribute(bool returnValue, string member)
            : this(returnValue, new string[] { member })
        { }

        public MemberNotNullWhenAttribute(bool returnValue, params string[] members)
        {
            ReturnValue = returnValue;
            Members = members;
        }
    }
}

#endif
