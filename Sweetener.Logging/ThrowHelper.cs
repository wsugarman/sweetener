// The purpose of ThrowHelper.cs is the same as those seen in the CoreFX repository
// like https://github.com/dotnet/corefx/blob/master/src/Common/src/CoreLib/System/ThrowHelper.cs.
//
// As logging may be called through an application, it is imperative that we optimize and
// do not adversely impact consumers. To that end, it would be best if we can inline
// logging as much as possible. However, throwing exceptions can often be verbose in IL
// and may prevent the inlining of our methods. So instead we wrap our throw expressions
// in non-inlined methods to prevent them from bloating our log methods.

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Sweetener.Logging
{
    internal static class ThrowHelper
    {
        // TODO: Also perform resource localization

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowArgumentNullException(ExceptionArgument argument)
            => throw new ArgumentNullException(argument.ToString());
    }

    internal enum ExceptionArgument
    {
        args,
        format,
        message,
    }
}
