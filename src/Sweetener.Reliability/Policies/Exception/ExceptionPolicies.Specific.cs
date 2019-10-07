// Generated from ExceptionPolicies.Specific.tt
using System;

namespace Sweetener.Reliability
{
    static partial class ExceptionPolicies
    {
        #region Fail
        /// <summary>
        /// Creates an <see cref="ExceptionPolicy" /> that immediately fails an operation
        /// if any exception is thrown whose type exactly matches the given type.
        /// All other exceptions are assumed to be transient.
        /// </summary>
        /// <typeparam name="T">The type of the fatal exception.</typeparam>
        public static ExceptionPolicy Fail<T>()
            where T : Exception
            => exception =>
            {
                if (exception == null)
                    throw new ArgumentNullException(nameof(exception));

                Type t = exception.GetType();
                return t != typeof(T);
            };

        /// <summary>
        /// Creates an <see cref="ExceptionPolicy" /> that immediately fails an operation
        /// if any exception is thrown whose type exactly matches one of the given types.
        /// All other exceptions are assumed to be transient.
        /// </summary>
        /// <typeparam name="T1">The type of the first fatal exception.</typeparam>
        /// <typeparam name="T2">The type of the second fatal exception.</typeparam>
        public static ExceptionPolicy Fail<T1, T2>()
            where T1 : Exception
            where T2 : Exception
            => exception =>
            {
                if (exception == null)
                    throw new ArgumentNullException(nameof(exception));

                Type t = exception.GetType();
                return t != typeof(T1)
                    && t != typeof(T2);
            };

        /// <summary>
        /// Creates an <see cref="ExceptionPolicy" /> that immediately fails an operation
        /// if any exception is thrown whose type exactly matches one of the given types.
        /// All other exceptions are assumed to be transient.
        /// </summary>
        /// <typeparam name="T1">The type of the first fatal exception.</typeparam>
        /// <typeparam name="T2">The type of the second fatal exception.</typeparam>
        /// <typeparam name="T3">The type of the third fatal exception.</typeparam>
        public static ExceptionPolicy Fail<T1, T2, T3>()
            where T1 : Exception
            where T2 : Exception
            where T3 : Exception
            => exception =>
            {
                if (exception == null)
                    throw new ArgumentNullException(nameof(exception));

                Type t = exception.GetType();
                return t != typeof(T1)
                    && t != typeof(T2)
                    && t != typeof(T3);
            };

        /// <summary>
        /// Creates an <see cref="ExceptionPolicy" /> that immediately fails an operation
        /// if any exception is thrown whose type exactly matches one of the given types.
        /// All other exceptions are assumed to be transient.
        /// </summary>
        /// <typeparam name="T1">The type of the first fatal exception.</typeparam>
        /// <typeparam name="T2">The type of the second fatal exception.</typeparam>
        /// <typeparam name="T3">The type of the third fatal exception.</typeparam>
        /// <typeparam name="T4">The type of the fourth fatal exception.</typeparam>
        public static ExceptionPolicy Fail<T1, T2, T3, T4>()
            where T1 : Exception
            where T2 : Exception
            where T3 : Exception
            where T4 : Exception
            => exception =>
            {
                if (exception == null)
                    throw new ArgumentNullException(nameof(exception));

                Type t = exception.GetType();
                return t != typeof(T1)
                    && t != typeof(T2)
                    && t != typeof(T3)
                    && t != typeof(T4);
            };

        /// <summary>
        /// Creates an <see cref="ExceptionPolicy" /> that immediately fails an operation
        /// if any exception is thrown whose type exactly matches one of the given types.
        /// All other exceptions are assumed to be transient.
        /// </summary>
        /// <typeparam name="T1">The type of the first fatal exception.</typeparam>
        /// <typeparam name="T2">The type of the second fatal exception.</typeparam>
        /// <typeparam name="T3">The type of the third fatal exception.</typeparam>
        /// <typeparam name="T4">The type of the fourth fatal exception.</typeparam>
        /// <typeparam name="T5">The type of the fifth fatal exception.</typeparam>
        public static ExceptionPolicy Fail<T1, T2, T3, T4, T5>()
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
                return t != typeof(T1)
                    && t != typeof(T2)
                    && t != typeof(T3)
                    && t != typeof(T4)
                    && t != typeof(T5);
            };

        /// <summary>
        /// Creates an <see cref="ExceptionPolicy" /> that immediately fails an operation
        /// if any exception is thrown whose type exactly matches one of the given types.
        /// All other exceptions are assumed to be transient.
        /// </summary>
        /// <typeparam name="T1">The type of the first fatal exception.</typeparam>
        /// <typeparam name="T2">The type of the second fatal exception.</typeparam>
        /// <typeparam name="T3">The type of the third fatal exception.</typeparam>
        /// <typeparam name="T4">The type of the fourth fatal exception.</typeparam>
        /// <typeparam name="T5">The type of the fifth fatal exception.</typeparam>
        /// <typeparam name="T6">The type of the sixth fatal exception.</typeparam>
        public static ExceptionPolicy Fail<T1, T2, T3, T4, T5, T6>()
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
                return t != typeof(T1)
                    && t != typeof(T2)
                    && t != typeof(T3)
                    && t != typeof(T4)
                    && t != typeof(T5)
                    && t != typeof(T6);
            };

        /// <summary>
        /// Creates an <see cref="ExceptionPolicy" /> that immediately fails an operation
        /// if any exception is thrown whose type exactly matches one of the given types.
        /// All other exceptions are assumed to be transient.
        /// </summary>
        /// <typeparam name="T1">The type of the first fatal exception.</typeparam>
        /// <typeparam name="T2">The type of the second fatal exception.</typeparam>
        /// <typeparam name="T3">The type of the third fatal exception.</typeparam>
        /// <typeparam name="T4">The type of the fourth fatal exception.</typeparam>
        /// <typeparam name="T5">The type of the fifth fatal exception.</typeparam>
        /// <typeparam name="T6">The type of the sixth fatal exception.</typeparam>
        /// <typeparam name="T7">The type of the seventh fatal exception.</typeparam>
        public static ExceptionPolicy Fail<T1, T2, T3, T4, T5, T6, T7>()
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
                return t != typeof(T1)
                    && t != typeof(T2)
                    && t != typeof(T3)
                    && t != typeof(T4)
                    && t != typeof(T5)
                    && t != typeof(T6)
                    && t != typeof(T7);
            };

        /// <summary>
        /// Creates an <see cref="ExceptionPolicy" /> that immediately fails an operation
        /// if any exception is thrown whose type exactly matches one of the given types.
        /// All other exceptions are assumed to be transient.
        /// </summary>
        /// <typeparam name="T1">The type of the first fatal exception.</typeparam>
        /// <typeparam name="T2">The type of the second fatal exception.</typeparam>
        /// <typeparam name="T3">The type of the third fatal exception.</typeparam>
        /// <typeparam name="T4">The type of the fourth fatal exception.</typeparam>
        /// <typeparam name="T5">The type of the fifth fatal exception.</typeparam>
        /// <typeparam name="T6">The type of the sixth fatal exception.</typeparam>
        /// <typeparam name="T7">The type of the seventh fatal exception.</typeparam>
        /// <typeparam name="T8">The type of the eighth fatal exception.</typeparam>
        public static ExceptionPolicy Fail<T1, T2, T3, T4, T5, T6, T7, T8>()
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
                return t != typeof(T1)
                    && t != typeof(T2)
                    && t != typeof(T3)
                    && t != typeof(T4)
                    && t != typeof(T5)
                    && t != typeof(T6)
                    && t != typeof(T7)
                    && t != typeof(T8);
            };

        #endregion

        #region Retry
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
        /// <typeparam name="T9">The type of the ninth transient exception.</typeparam>
        public static ExceptionPolicy Retry<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
            where T1 : Exception
            where T2 : Exception
            where T3 : Exception
            where T4 : Exception
            where T5 : Exception
            where T6 : Exception
            where T7 : Exception
            where T8 : Exception
            where T9 : Exception
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
                    || t == typeof(T8)
                    || t == typeof(T9);
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
        /// <typeparam name="T9">The type of the ninth transient exception.</typeparam>
        /// <typeparam name="T10">The type of the tenth transient exception.</typeparam>
        public static ExceptionPolicy Retry<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
            where T1 : Exception
            where T2 : Exception
            where T3 : Exception
            where T4 : Exception
            where T5 : Exception
            where T6 : Exception
            where T7 : Exception
            where T8 : Exception
            where T9 : Exception
            where T10 : Exception
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
                    || t == typeof(T8)
                    || t == typeof(T9)
                    || t == typeof(T10);
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
        /// <typeparam name="T9">The type of the ninth transient exception.</typeparam>
        /// <typeparam name="T10">The type of the tenth transient exception.</typeparam>
        /// <typeparam name="T11">The type of the eleventh transient exception.</typeparam>
        public static ExceptionPolicy Retry<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
            where T1 : Exception
            where T2 : Exception
            where T3 : Exception
            where T4 : Exception
            where T5 : Exception
            where T6 : Exception
            where T7 : Exception
            where T8 : Exception
            where T9 : Exception
            where T10 : Exception
            where T11 : Exception
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
                    || t == typeof(T8)
                    || t == typeof(T9)
                    || t == typeof(T10)
                    || t == typeof(T11);
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
        /// <typeparam name="T9">The type of the ninth transient exception.</typeparam>
        /// <typeparam name="T10">The type of the tenth transient exception.</typeparam>
        /// <typeparam name="T11">The type of the eleventh transient exception.</typeparam>
        /// <typeparam name="T12">The type of the twelfth transient exception.</typeparam>
        public static ExceptionPolicy Retry<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
            where T1 : Exception
            where T2 : Exception
            where T3 : Exception
            where T4 : Exception
            where T5 : Exception
            where T6 : Exception
            where T7 : Exception
            where T8 : Exception
            where T9 : Exception
            where T10 : Exception
            where T11 : Exception
            where T12 : Exception
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
                    || t == typeof(T8)
                    || t == typeof(T9)
                    || t == typeof(T10)
                    || t == typeof(T11)
                    || t == typeof(T12);
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
        /// <typeparam name="T9">The type of the ninth transient exception.</typeparam>
        /// <typeparam name="T10">The type of the tenth transient exception.</typeparam>
        /// <typeparam name="T11">The type of the eleventh transient exception.</typeparam>
        /// <typeparam name="T12">The type of the twelfth transient exception.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth transient exception.</typeparam>
        public static ExceptionPolicy Retry<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
            where T1 : Exception
            where T2 : Exception
            where T3 : Exception
            where T4 : Exception
            where T5 : Exception
            where T6 : Exception
            where T7 : Exception
            where T8 : Exception
            where T9 : Exception
            where T10 : Exception
            where T11 : Exception
            where T12 : Exception
            where T13 : Exception
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
                    || t == typeof(T8)
                    || t == typeof(T9)
                    || t == typeof(T10)
                    || t == typeof(T11)
                    || t == typeof(T12)
                    || t == typeof(T13);
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
        /// <typeparam name="T9">The type of the ninth transient exception.</typeparam>
        /// <typeparam name="T10">The type of the tenth transient exception.</typeparam>
        /// <typeparam name="T11">The type of the eleventh transient exception.</typeparam>
        /// <typeparam name="T12">The type of the twelfth transient exception.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth transient exception.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth transient exception.</typeparam>
        public static ExceptionPolicy Retry<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
            where T1 : Exception
            where T2 : Exception
            where T3 : Exception
            where T4 : Exception
            where T5 : Exception
            where T6 : Exception
            where T7 : Exception
            where T8 : Exception
            where T9 : Exception
            where T10 : Exception
            where T11 : Exception
            where T12 : Exception
            where T13 : Exception
            where T14 : Exception
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
                    || t == typeof(T8)
                    || t == typeof(T9)
                    || t == typeof(T10)
                    || t == typeof(T11)
                    || t == typeof(T12)
                    || t == typeof(T13)
                    || t == typeof(T14);
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
        /// <typeparam name="T9">The type of the ninth transient exception.</typeparam>
        /// <typeparam name="T10">The type of the tenth transient exception.</typeparam>
        /// <typeparam name="T11">The type of the eleventh transient exception.</typeparam>
        /// <typeparam name="T12">The type of the twelfth transient exception.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth transient exception.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth transient exception.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth transient exception.</typeparam>
        public static ExceptionPolicy Retry<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
            where T1 : Exception
            where T2 : Exception
            where T3 : Exception
            where T4 : Exception
            where T5 : Exception
            where T6 : Exception
            where T7 : Exception
            where T8 : Exception
            where T9 : Exception
            where T10 : Exception
            where T11 : Exception
            where T12 : Exception
            where T13 : Exception
            where T14 : Exception
            where T15 : Exception
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
                    || t == typeof(T8)
                    || t == typeof(T9)
                    || t == typeof(T10)
                    || t == typeof(T11)
                    || t == typeof(T12)
                    || t == typeof(T13)
                    || t == typeof(T14)
                    || t == typeof(T15);
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
        /// <typeparam name="T9">The type of the ninth transient exception.</typeparam>
        /// <typeparam name="T10">The type of the tenth transient exception.</typeparam>
        /// <typeparam name="T11">The type of the eleventh transient exception.</typeparam>
        /// <typeparam name="T12">The type of the twelfth transient exception.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth transient exception.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth transient exception.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth transient exception.</typeparam>
        /// <typeparam name="T16">The type of the sixteenth transient exception.</typeparam>
        public static ExceptionPolicy Retry<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
            where T1 : Exception
            where T2 : Exception
            where T3 : Exception
            where T4 : Exception
            where T5 : Exception
            where T6 : Exception
            where T7 : Exception
            where T8 : Exception
            where T9 : Exception
            where T10 : Exception
            where T11 : Exception
            where T12 : Exception
            where T13 : Exception
            where T14 : Exception
            where T15 : Exception
            where T16 : Exception
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
                    || t == typeof(T8)
                    || t == typeof(T9)
                    || t == typeof(T10)
                    || t == typeof(T11)
                    || t == typeof(T12)
                    || t == typeof(T13)
                    || t == typeof(T14)
                    || t == typeof(T15)
                    || t == typeof(T16);
            };

        #endregion
    }
}
