using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Sweetener.Reliability
{
    internal static class CancellationExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsCanceled(this Task task)
            => task != null && task.Status == TaskStatus.Canceled;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsCancellation(this Exception exception, CancellationToken cancellationToken)
        {
            OperationCanceledException oce = exception as OperationCanceledException;
            return oce != null
                && oce.CancellationToken == cancellationToken
                && cancellationToken.IsCancellationRequested;
        }
    }
}
