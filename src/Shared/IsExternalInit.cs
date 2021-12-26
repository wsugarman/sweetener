// Copyright © William Sugarman.
// Licensed under the MIT License.

// This file defines the IsExternalInit static class used to implement init-only properties.
//
// Documentation: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/init
// Source: https://github.com/dotnet/runtime/blob/v6.0.0/src/libraries/Common/src/System/Runtime/CompilerServices/IsExternalInit.cs

#if NETSTANDARD2_0

using System.ComponentModel;

namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static class IsExternalInit
    { }
}

#endif
