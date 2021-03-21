// Copyright © 2021 William Sugarman. All Rights Reserved.
// Licensed under the MIT License.

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
}

#endif
