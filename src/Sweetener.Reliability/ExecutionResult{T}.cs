using System;

namespace Sweetener.Reliability
{
    public readonly struct ExecutionResult<T>
    {
        public bool Success => FailureReason == FailureReason.None;

        public readonly T             Result;
        public readonly bool          HasResult;
        public readonly FailureReason FailureReason;
        public readonly int           Attempts;
        public readonly Exception     Exception;

        private ExecutionResult(T result, FailureReason failureReason, int attempts, Exception exception)
        {
            if (attempts <= 0)
                throw new ArgumentOutOfRangeException(nameof(attempts), "Attempts cannot be 0 or negative");

            HasResult     = true;
            Result        = result;
            FailureReason = failureReason;
            Attempts      = attempts;
            Exception     = exception;
        }

        private ExecutionResult(FailureReason failureReason, int attempts, Exception exception)
        {
            if (attempts <= 0)
                throw new ArgumentOutOfRangeException(nameof(attempts), "Attempts cannot be 0 or negative");

            HasResult     = false;
            Result        = default(T);
            FailureReason = failureReason;
            Attempts      = attempts;
            Exception     = exception;
        }

        public static ExecutionResult<T> Succeeded(T result, int attempts)
            => new ExecutionResult<T>(result, FailureReason.None, attempts, exception: null);

        public static ExecutionResult<T> Aborted(T result, int attempts)
            => new ExecutionResult<T>(result, FailureReason.Aborted, attempts, exception: null);

        public static ExecutionResult<T> Aborted(Exception exception, int attempts)
            => new ExecutionResult<T>(FailureReason.Aborted, attempts, exception);

        public static ExecutionResult<T> ExceededRetries(T result, int attempts)
            => new ExecutionResult<T>(result, FailureReason.ExceedRetries, attempts, exception: null);

        public static ExecutionResult<T> ExceededRetries(Exception exception, int attempts)
            => new ExecutionResult<T>(FailureReason.ExceedRetries, attempts, exception);
    }
}
