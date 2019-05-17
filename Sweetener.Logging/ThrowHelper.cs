using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Sweetener.Logging
{
    /// <summary>
    /// A set of factory methods for optimally throwing common exceptions.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The use of <see cref="ThrowHelper"/> is not mandatory in all circumstances.
    /// Instead, <see cref="ThrowHelper"/> focuses on exceptions that may be thrown in
    /// the beginning of methods that may be called in tight loops, such as in the case
    /// of argument validation and disposal checks. The use of <see cref="ThrowHelper"/>
    /// reduces the size of the function's IL, which can be verbose when throwing exceptions.
    /// For non-standard exceptions, Sweetener classes will likely wrap the throw expression
    /// inside of the method defined in-line.
    /// </para>
    /// <para>
    /// Readers may recognize the class to similar ones declared throughout CoreFx. Like
    /// this <see cref="ThrowHelper"/>, their "ThrowHelpers" are also for internal use.
    /// Eg. https://github.com/dotnet/corefx/blob/master/src/Common/src/CoreLib/System/ThrowHelper.cs
    /// </para>
    /// </remarks>
    internal static class ThrowHelper
    {
        // TODO: Also perform resource localization

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> concerning the given argument.
        /// </summary>
        /// <param name="argument">The name of the argument.</param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowArgumentNullException(ExceptionArgument argument)
            => throw new ArgumentNullException(argument.ToString()); // TODO: Are there .NET environments where this doesn't work, like AOT?
    }

    /// <summary>
    /// The names of the exceptional arguments.
    /// </summary>
    internal enum ExceptionArgument
    {
        /// <summary>
        /// A collection of arguments.
        /// </summary>
        args,

        /// <summary>
        /// A format string.
        /// </summary>
        format,
    }
}
