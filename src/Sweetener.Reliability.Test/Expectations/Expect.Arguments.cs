// Generated from Expect.Arguments.tt
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    static partial class Expect
    {
        public static Action<T, CallContext> Arguments<T>(Action<T> assertArguments)
            => (arg, context) => assertArguments(arg);

        public static Action<T, CallContext> ArgumentsAfterDelay<T>(Action<T> assertArguments, TimeSpan delay)
            => (arg, context) =>
            {
                assertArguments(arg);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, CallContext> Arguments<T1, T2>(Action<T1, T2> assertArguments)
            => (arg1, arg2, context) => assertArguments(arg1, arg2);

        public static Action<T1, T2, CallContext> ArgumentsAfterDelay<T1, T2>(Action<T1, T2> assertArguments, TimeSpan delay)
            => (arg1, arg2, context) =>
            {
                assertArguments(arg1, arg2);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, CallContext> Arguments<T1, T2, T3>(Action<T1, T2, T3> assertArguments)
            => (arg1, arg2, arg3, context) => assertArguments(arg1, arg2, arg3);

        public static Action<T1, T2, T3, CallContext> ArgumentsAfterDelay<T1, T2, T3>(Action<T1, T2, T3> assertArguments, TimeSpan delay)
            => (arg1, arg2, arg3, context) =>
            {
                assertArguments(arg1, arg2, arg3);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, CallContext> Arguments<T1, T2, T3, T4>(Action<T1, T2, T3, T4> assertArguments)
            => (arg1, arg2, arg3, arg4, context) => assertArguments(arg1, arg2, arg3, arg4);

        public static Action<T1, T2, T3, T4, CallContext> ArgumentsAfterDelay<T1, T2, T3, T4>(Action<T1, T2, T3, T4> assertArguments, TimeSpan delay)
            => (arg1, arg2, arg3, arg4, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, CallContext> Arguments<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> assertArguments)
            => (arg1, arg2, arg3, arg4, arg5, context) => assertArguments(arg1, arg2, arg3, arg4, arg5);

        public static Action<T1, T2, T3, T4, T5, CallContext> ArgumentsAfterDelay<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> assertArguments, TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, CallContext> Arguments<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> assertArguments)
            => (arg1, arg2, arg3, arg4, arg5, arg6, context) => assertArguments(arg1, arg2, arg3, arg4, arg5, arg6);

        public static Action<T1, T2, T3, T4, T5, T6, CallContext> ArgumentsAfterDelay<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> assertArguments, TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, CallContext> Arguments<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> assertArguments)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, context) => assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7);

        public static Action<T1, T2, T3, T4, T5, T6, T7, CallContext> ArgumentsAfterDelay<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> assertArguments, TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, CallContext> Arguments<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> assertArguments)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, context) => assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, CallContext> ArgumentsAfterDelay<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> assertArguments, TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, CallContext> Arguments<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> assertArguments)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, context) => assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, CallContext> ArgumentsAfterDelay<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> assertArguments, TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

    }
}
