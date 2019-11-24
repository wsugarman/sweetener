// Generated from Expect.Arguments.tt
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    static partial class Expect
    {
        public static Action<T, CallContext> AfteDelay<T>(TimeSpan delay)
            => (arg, context) =>
            {
                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T, CallContext> Arguments<T>(Action<T> assertArguments)
            => (arg, context) =>
            {
                assertArguments(arg);
            };

        public static Action<T, CallContext> ArgumentsAfterDelay<T>(Action<T> assertArguments, TimeSpan delay)
            => (arg, context) =>
            {
                assertArguments(arg);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, CallContext> AfteDelay<T1, T2>(TimeSpan delay)
            => (arg1, arg2, context) =>
            {
                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, CallContext> Arguments<T1, T2>(Action<T1, T2> assertArguments)
            => (arg1, arg2, context) =>
            {
                assertArguments(arg1, arg2);
            };

        public static Action<T1, T2, CallContext> ArgumentsAfterDelay<T1, T2>(Action<T1, T2> assertArguments, TimeSpan delay)
            => (arg1, arg2, context) =>
            {
                assertArguments(arg1, arg2);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, CallContext> AfteDelay<T1, T2, T3>(TimeSpan delay)
            => (arg1, arg2, arg3, context) =>
            {
                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, CallContext> Arguments<T1, T2, T3>(Action<T1, T2, T3> assertArguments)
            => (arg1, arg2, arg3, context) =>
            {
                assertArguments(arg1, arg2, arg3);
            };

        public static Action<T1, T2, T3, CallContext> ArgumentsAfterDelay<T1, T2, T3>(Action<T1, T2, T3> assertArguments, TimeSpan delay)
            => (arg1, arg2, arg3, context) =>
            {
                assertArguments(arg1, arg2, arg3);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, CallContext> AfteDelay<T1, T2, T3, T4>(TimeSpan delay)
            => (arg1, arg2, arg3, arg4, context) =>
            {
                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, CallContext> Arguments<T1, T2, T3, T4>(Action<T1, T2, T3, T4> assertArguments)
            => (arg1, arg2, arg3, arg4, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4);
            };

        public static Action<T1, T2, T3, T4, CallContext> ArgumentsAfterDelay<T1, T2, T3, T4>(Action<T1, T2, T3, T4> assertArguments, TimeSpan delay)
            => (arg1, arg2, arg3, arg4, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, CallContext> AfteDelay<T1, T2, T3, T4, T5>(TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, context) =>
            {
                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, CallContext> Arguments<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> assertArguments)
            => (arg1, arg2, arg3, arg4, arg5, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5);
            };

        public static Action<T1, T2, T3, T4, T5, CallContext> ArgumentsAfterDelay<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> assertArguments, TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, CallContext> AfteDelay<T1, T2, T3, T4, T5, T6>(TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, context) =>
            {
                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, CallContext> Arguments<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> assertArguments)
            => (arg1, arg2, arg3, arg4, arg5, arg6, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6);
            };

        public static Action<T1, T2, T3, T4, T5, T6, CallContext> ArgumentsAfterDelay<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> assertArguments, TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, CallContext> AfteDelay<T1, T2, T3, T4, T5, T6, T7>(TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, context) =>
            {
                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, CallContext> Arguments<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> assertArguments)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, CallContext> ArgumentsAfterDelay<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> assertArguments, TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, CallContext> AfteDelay<T1, T2, T3, T4, T5, T6, T7, T8>(TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, context) =>
            {
                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, CallContext> Arguments<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> assertArguments)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, CallContext> ArgumentsAfterDelay<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> assertArguments, TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, CallContext> AfteDelay<T1, T2, T3, T4, T5, T6, T7, T8, T9>(TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, context) =>
            {
                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, CallContext> Arguments<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> assertArguments)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, CallContext> ArgumentsAfterDelay<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> assertArguments, TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, CallContext> AfteDelay<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, context) =>
            {
                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, CallContext> Arguments<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> assertArguments)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, CallContext> ArgumentsAfterDelay<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> assertArguments, TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, CallContext> AfteDelay<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, context) =>
            {
                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, CallContext> Arguments<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> assertArguments)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, CallContext> ArgumentsAfterDelay<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> assertArguments, TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, CallContext> AfteDelay<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, context) =>
            {
                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, CallContext> Arguments<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> assertArguments)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, CallContext> ArgumentsAfterDelay<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> assertArguments, TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, CallContext> AfteDelay<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, context) =>
            {
                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, CallContext> Arguments<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> assertArguments)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, CallContext> ArgumentsAfterDelay<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> assertArguments, TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, CallContext> AfteDelay<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, context) =>
            {
                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, CallContext> Arguments<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> assertArguments)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, CallContext> ArgumentsAfterDelay<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> assertArguments, TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, CallContext> AfteDelay<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, context) =>
            {
                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, CallContext> Arguments<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> assertArguments)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, CallContext> ArgumentsAfterDelay<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> assertArguments, TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, CallContext> AfteDelay<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, context) =>
            {
                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, CallContext> Arguments<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> assertArguments)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
            };

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, CallContext> ArgumentsAfterDelay<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> assertArguments, TimeSpan delay)
            => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, context) =>
            {
                assertArguments(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);

                if (context.Calls > 1)
                    Assert.IsTrue(context.TimeSinceLastCall >= delay, $"Time since last call '{context.TimeSinceLastCall}' less than expected delay '{delay}'.");
            };

    }
}
