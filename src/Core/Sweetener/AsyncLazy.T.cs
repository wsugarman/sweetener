// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sweetener;

/// <summary>
/// Provides support for asychronous lazy initialization.
/// </summary>
public sealed class AsyncLazy<T> : IDisposable
{
    private volatile Task<T>? _valueTask;
    private readonly AsyncFunc<CancellationToken, T> _valueFactory;
    private readonly LazyThreadSafetyMode _mode;
    private readonly SemaphoreSlim? _semaphore;

    /// <summary>
    /// Gets a value that indicates whether a value has been created for this <see cref="AsyncLazy{T}"/> instance.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if a value has been created for this <see cref="AsyncLazy{T}"/> instance;
    /// otherwise, <see langword="false"/>.
    /// </value>
    public bool IsValueCreated
    {
        get
        {
            Task<T>? task = _valueTask;
            return task != null && task.IsCompleted;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncLazy{T}"/> class.
    /// When lazy initialization occurs, the specified initialization function is used.
    /// </summary>
    /// <param name="valueFactory">The delegate that is invoked to produce the lazily initialized value when it is needed.</param>
    /// <exception cref="ArgumentNullException"><paramref name="valueFactory"/> is <see langword="null"/>.</exception>
    public AsyncLazy(AsyncFunc<CancellationToken, T> valueFactory)
        : this(valueFactory, LazyThreadSafetyMode.ExecutionAndPublication)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncLazy{T}"/> class.
    /// When lazy initialization occurs, the specified initialization function and initialization mode are used.
    /// </summary>
    /// <param name="valueFactory">The delegate that is invoked to produce the lazily initialized value when it is needed.</param>
    /// <param name="isThreadSafe">
    /// <see langword="true"/> to make this instance usable concurrently by multiple threads;
    /// <see langword="false"/> to make this instance usable by only one thread at a time.
    /// </param>
    /// <exception cref="ArgumentNullException"><paramref name="valueFactory"/> is <see langword="null"/>.</exception>
    public AsyncLazy(AsyncFunc<CancellationToken, T> valueFactory, bool isThreadSafe)
        : this(valueFactory, isThreadSafe ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncLazy{T}"/> class
    /// that uses the specified initialization function and thread-safety mode.
    /// </summary>
    /// <param name="valueFactory">The delegate that is invoked to produce the lazily initialized value when it is needed.</param>
    /// <param name="mode">One of the enumeration values that specifies the thread safety mode.</param>
    /// <exception cref="ArgumentNullException"><paramref name="valueFactory"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="mode"/> contains an invalid value.</exception>
    public AsyncLazy(AsyncFunc<CancellationToken, T> valueFactory, LazyThreadSafetyMode mode)
    {
        if (valueFactory is null)
            throw new ArgumentNullException(nameof(valueFactory));

        if (!Enum.IsDefined(typeof(LazyThreadSafetyMode), mode))
            throw new ArgumentOutOfRangeException(nameof(mode));

        _valueFactory = valueFactory;
        _mode = mode;
        _semaphore = mode != LazyThreadSafetyMode.None ? new SemaphoreSlim(1, 1) : null;
    }

    /// <summary>
    /// Asynchronously gets the lazily initialized value of the current <see cref="AsyncLazy{T}"/> instance.
    /// </summary>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous get operation.
    /// The value of the <see cref="Task{TResult}.Result"/> property contains the lazily initialized
    /// value of the current <see cref="AsyncLazy{T}"/> instance.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// The initialization function tries to invoke <see cref="GetValueAsync(CancellationToken)"/> on this instance.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// The <paramref name="cancellationToken"/> requested cancellation.
    /// </exception>
    public Task<T> GetValueAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (_valueTask is not null)
            return _valueTask;

        return _mode switch
        {
            LazyThreadSafetyMode.None => ExecuteAndPublishAsync(cancellationToken),
            LazyThreadSafetyMode.PublicationOnly => ExecuteAndPublishThreadSafeAsync(cancellationToken),
            _ => ExecuteThreadSafeAndPublishAsync(cancellationToken),
        };
    }

    /// <summary>
    /// Releases all resources used by the current instance of the <see cref="AsyncLazy{T}"/> class.
    /// </summary>
    public void Dispose()
    {
        _semaphore?.Dispose();
        GC.SuppressFinalize(this);
    }

    private Task<T> ExecuteAndPublishAsync(CancellationToken cancellationToken = default)
    {
        Task<T> valueTask = _valueFactory(cancellationToken);
        _valueTask = valueTask;
        return valueTask;
    }

    private async Task<T> ExecuteThreadSafeAndPublishAsync(CancellationToken cancellationToken = default)
    {
        await _semaphore!.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            _valueTask ??= _valueFactory(cancellationToken);
        }
        finally
        {
            _semaphore.Release();
        }

        return await _valueTask.ConfigureAwait(false);
    }

    private async Task<T> ExecuteAndPublishThreadSafeAsync(CancellationToken cancellationToken = default)
    {
        Task<T> valueTask = _valueFactory(cancellationToken);
        T result = await valueTask.ConfigureAwait(false);

        await _semaphore!.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            _valueTask ??= valueTask;
            return result;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
