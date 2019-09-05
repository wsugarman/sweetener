using System;

namespace Sweetener.Reliability
{
    public readonly struct ExecutionResult
    {
        public bool Success => FailureReason == FailureReason.None;

        public readonly FailureReason FailureReason;
        public readonly int           Attempts;
        public readonly Exception     Exception;

        private ExecutionResult(FailureReason failureReason, int attempts, Exception exception)
        {
            if (attempts <= 0)
                throw new ArgumentOutOfRangeException(nameof(attempts), "Attempts cannot be 0 or negative");

            FailureReason = failureReason;
            Attempts      = attempts;
            Exception     = exception;
        }

        public static ExecutionResult Succeeded(int attempts)
            => new ExecutionResult(FailureReason.None, attempts, exception: null);

        public static ExecutionResult Aborted(Exception exception, int attempts)
            => new ExecutionResult(FailureReason.Aborted, attempts, exception);

        public static ExecutionResult ExceededRetries(int attempts)
            => new ExecutionResult(FailureReason.ExceedRetries, attempts, exception: null);

        public static ExecutionResult ExceededRetries(Exception exception, int attempts)
            => new ExecutionResult(FailureReason.ExceedRetries, attempts, exception);
    }
}
