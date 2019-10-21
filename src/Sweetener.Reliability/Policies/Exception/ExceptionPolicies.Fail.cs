// Generated from ExceptionPolicies.Fail.tt
using System;

namespace Sweetener.Reliability
{
    static partial class ExceptionPolicies
    {
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

    }
}
