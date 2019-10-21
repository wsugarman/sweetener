// Generated from ExceptionPolicies.Retry.tt
using System;

namespace Sweetener.Reliability
{
    static partial class ExceptionPolicies
    {
        /// <summary>
        /// Creates an <see cref="ExceptionPolicy" /> that attempts to retry an operation
        /// if any exception is thrown whose type exactly matches the given type.
        /// All other exceptions are assumed to be fatal.
        /// </summary>
        /// <typeparam name="T">The type of the transient exception.</typeparam>
        public static ExceptionPolicy Retry<T>()
            where T : Exception
            => exception =>
            {
                if (exception == null)
                    throw new ArgumentNullException(nameof(exception));

                Type t = exception.GetType();
                return t == typeof(T);
            };

        /// <summary>
        /// Creates an <see cref="ExceptionPolicy" /> that attempts to retry an operation
        /// if any exception is thrown whose type exactly matches one of the given types.
        /// All other exceptions are assumed to be fatal.
        /// </summary>
        /// <typeparam name="T1">The type of the first transient exception.</typeparam>
        /// <typeparam name="T2">The type of the second transient exception.</typeparam>
        public static ExceptionPolicy Retry<T1, T2>()
            where T1 : Exception
            where T2 : Exception
            => exception =>
            {
                if (exception == null)
                    throw new ArgumentNullException(nameof(exception));

                Type t = exception.GetType();
                return t == typeof(T1)
                    || t == typeof(T2);
            };

        /// <summary>
        /// Creates an <see cref="ExceptionPolicy" /> that attempts to retry an operation
        /// if any exception is thrown whose type exactly matches one of the given types.
        /// All other exceptions are assumed to be fatal.
        /// </summary>
        /// <typeparam name="T1">The type of the first transient exception.</typeparam>
        /// <typeparam name="T2">The type of the second transient exception.</typeparam>
        /// <typeparam name="T3">The type of the third transient exception.</typeparam>
        public static ExceptionPolicy Retry<T1, T2, T3>()
            where T1 : Exception
            where T2 : Exception
            where T3 : Exception
            => exception =>
            {
                if (exception == null)
                    throw new ArgumentNullException(nameof(exception));

                Type t = exception.GetType();
                return t == typeof(T1)
                    || t == typeof(T2)
                    || t == typeof(T3);
            };

        /// <summary>
        /// Creates an <see cref="ExceptionPolicy" /> that attempts to retry an operation
        /// if any exception is thrown whose type exactly matches one of the given types.
        /// All other exceptions are assumed to be fatal.
        /// </summary>
        /// <typeparam name="T1">The type of the first transient exception.</typeparam>
        /// <typeparam name="T2">The type of the second transient exception.</typeparam>
        /// <typeparam name="T3">The type of the third transient exception.</typeparam>
        /// <typeparam name="T4">The type of the fourth transient exception.</typeparam>
        public static ExceptionPolicy Retry<T1, T2, T3, T4>()
            where T1 : Exception
            where T2 : Exception
            where T3 : Exception
            where T4 : Exception
            => exception =>
            {
                if (exception == null)
                    throw new ArgumentNullException(nameof(exception));

                Type t = exception.GetType();
                return t == typeof(T1)
                    || t == typeof(T2)
                    || t == typeof(T3)
                    || t == typeof(T4);
            };

        /// <summary>
        /// Creates an <see cref="ExceptionPolicy" /> that attempts to retry an operation
        /// if any exception is thrown whose type exactly matches one of the given types.
        /// All other exceptions are assumed to be fatal.
        /// </summary>
        /// <typeparam name="T1">The type of the first transient exception.</typeparam>
        /// <typeparam name="T2">The type of the second transient exception.</typeparam>
        /// <typeparam name="T3">The type of the third transient exception.</typeparam>
        /// <typeparam name="T4">The type of the fourth transient exception.</typeparam>
        /// <typeparam name="T5">The type of the fifth transient exception.</typeparam>
        public static ExceptionPolicy Retry<T1, T2, T3, T4, T5>()
            where T1 : Exception
            where T2 : Exception
            where T3 : Exception
            where T4 : Exception
            where T5 : Exception
            => exception =>
            {
                if (exception == null)
                    throw new ArgumentNullException(nameof(exception));

                Type t = exception.GetType();
                return t == typeof(T1)
                    || t == typeof(T2)
                    || t == typeof(T3)
                    || t == typeof(T4)
                    || t == typeof(T5);
            };

        /// <summary>
        /// Creates an <see cref="ExceptionPolicy" /> that attempts to retry an operation
        /// if any exception is thrown whose type exactly matches one of the given types.
        /// All other exceptions are assumed to be fatal.
        /// </summary>
        /// <typeparam name="T1">The type of the first transient exception.</typeparam>
        /// <typeparam name="T2">The type of the second transient exception.</typeparam>
        /// <typeparam name="T3">The type of the third transient exception.</typeparam>
        /// <typeparam name="T4">The type of the fourth transient exception.</typeparam>
        /// <typeparam name="T5">The type of the fifth transient exception.</typeparam>
        /// <typeparam name="T6">The type of the sixth transient exception.</typeparam>
        public static ExceptionPolicy Retry<T1, T2, T3, T4, T5, T6>()
            where T1 : Exception
            where T2 : Exception
            where T3 : Exception
            where T4 : Exception
            where T5 : Exception
            where T6 : Exception
            => exception =>
            {
                if (exception == null)
                    throw new ArgumentNullException(nameof(exception));

                Type t = exception.GetType();
                return t == typeof(T1)
                    || t == typeof(T2)
                    || t == typeof(T3)
                    || t == typeof(T4)
                    || t == typeof(T5)
                    || t == typeof(T6);
            };

        /// <summary>
        /// Creates an <see cref="ExceptionPolicy" /> that attempts to retry an operation
        /// if any exception is thrown whose type exactly matches one of the given types.
        /// All other exceptions are assumed to be fatal.
        /// </summary>
        /// <typeparam name="T1">The type of the first transient exception.</typeparam>
        /// <typeparam name="T2">The type of the second transient exception.</typeparam>
        /// <typeparam name="T3">The type of the third transient exception.</typeparam>
        /// <typeparam name="T4">The type of the fourth transient exception.</typeparam>
        /// <typeparam name="T5">The type of the fifth transient exception.</typeparam>
        /// <typeparam name="T6">The type of the sixth transient exception.</typeparam>
        /// <typeparam name="T7">The type of the seventh transient exception.</typeparam>
        public static ExceptionPolicy Retry<T1, T2, T3, T4, T5, T6, T7>()
            where T1 : Exception
            where T2 : Exception
            where T3 : Exception
            where T4 : Exception
            where T5 : Exception
            where T6 : Exception
            where T7 : Exception
            => exception =>
            {
                if (exception == null)
                    throw new ArgumentNullException(nameof(exception));

                Type t = exception.GetType();
                return t == typeof(T1)
                    || t == typeof(T2)
                    || t == typeof(T3)
                    || t == typeof(T4)
                    || t == typeof(T5)
                    || t == typeof(T6)
                    || t == typeof(T7);
            };

        /// <summary>
        /// Creates an <see cref="ExceptionPolicy" /> that attempts to retry an operation
        /// if any exception is thrown whose type exactly matches one of the given types.
        /// All other exceptions are assumed to be fatal.
        /// </summary>
        /// <typeparam name="T1">The type of the first transient exception.</typeparam>
        /// <typeparam name="T2">The type of the second transient exception.</typeparam>
        /// <typeparam name="T3">The type of the third transient exception.</typeparam>
        /// <typeparam name="T4">The type of the fourth transient exception.</typeparam>
        /// <typeparam name="T5">The type of the fifth transient exception.</typeparam>
        /// <typeparam name="T6">The type of the sixth transient exception.</typeparam>
        /// <typeparam name="T7">The type of the seventh transient exception.</typeparam>
        /// <typeparam name="T8">The type of the eighth transient exception.</typeparam>
        public static ExceptionPolicy Retry<T1, T2, T3, T4, T5, T6, T7, T8>()
            where T1 : Exception
            where T2 : Exception
            where T3 : Exception
            where T4 : Exception
            where T5 : Exception
            where T6 : Exception
            where T7 : Exception
            where T8 : Exception
            => exception =>
            {
                if (exception == null)
                    throw new ArgumentNullException(nameof(exception));

                Type t = exception.GetType();
                return t == typeof(T1)
                    || t == typeof(T2)
                    || t == typeof(T3)
                    || t == typeof(T4)
                    || t == typeof(T5)
                    || t == typeof(T6)
                    || t == typeof(T7)
                    || t == typeof(T8);
            };

    }
}
