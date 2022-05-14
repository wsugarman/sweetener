// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Sweetener.Threading.Tasks;

internal static class TaskExtensions
{
    // TODO: Add Task<T> if necessary
    public static Task<TResult> WithResultOnSuccess<TState, TResult>(this Task task, Func<TState, TResult> resultSelector, TState state)
    {
        if (task is null)
            throw new ArgumentNullException(nameof(task));

        if (resultSelector is null)
            throw new ArgumentNullException(nameof(resultSelector));

        return task
            .ContinueWith((t, obj) =>
                {
                    switch (t.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            return Task.FromResult(resultSelector((TState)obj!));
                        case TaskStatus.Canceled:
                            return ThrowIfCancellationRequested<TResult>(t);
                        default:
                            // Preserve all of the AggregateException's errors!
                            TaskCompletionSource<TResult> tcs = new TaskCompletionSource<TResult>();
                            tcs.SetException(t.Exception!.InnerExceptions);
                            return tcs.Task;
                    }
                },
                state,
                CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.DenyChildAttach,
                TaskScheduler.Default)
            .Unwrap();
    }

    [ExcludeFromCodeCoverage]
    [SuppressMessage("Performance", "CA1849: Call async methods when in an async method", Justification = "Task is already complete.")]
    private static Task<TResult> ThrowIfCancellationRequested<TResult>(Task t)
    {
        // Getting the result will cause an exception to be thrown such that
        // the subsequent line is unreachable
        t.GetAwaiter().GetResult();
        return Task.FromResult<TResult>(default!);
    }
}
