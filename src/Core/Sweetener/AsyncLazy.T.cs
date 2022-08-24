// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Sweetener.Threading;

namespace Sweetener;

/// <summary>
/// Provides support for asychronous lazy initialization.
/// </summary>
[DebuggerTypeProxy(typeof(AsyncLazyDebugView<>))]
[DebuggerDisplay("ThreadSafetyMode={Mode}, IsValueCreated={IsValueCreated}, IsValueFaulted={IsValueFaulted}, Value={ValueForDebugDisplay}")]
public sealed class AsyncLazy<T> : IDisposable
{
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
            Task<T>? valueTask = _valueTask;
            return valueTask != null
#if NETCOREAPP2_0_OR_GREATER
                && valueTask.IsCompletedSuccessfully;
#else
                && valueTask.Status == TaskStatus.RanToCompletion;
#endif
        }
    }

    internal bool IsValueFaulted
    {
        get
        {
            Task<T>? valueTask = _valueTask;
            return valueTask != null && valueTask.IsFaulted;
        }
    }

    internal AsyncLazyThreadSafetyMode Mode { get; }

    internal T? ValueForDebugDisplay
    {
        get
        {
            Task<T>? valueTask = _valueTask;
            return valueTask != null
#if NETCOREAPP2_0_OR_GREATER
                && valueTask.IsCompletedSuccessfully
#else
                && valueTask.Status == TaskStatus.RanToCompletion
#endif
                ? valueTask.Result : default;
        }
    }

    private AsyncFunc<CancellationToken, T> ValueFactory
    {
        get
        {
            AsyncFunc<CancellationToken, T>? valueFactory = _valueFactory;
            if (valueFactory is null)
                throw new ObjectDisposedException(typeof(AsyncLazy<T>).FullName);

            return valueFactory;
        }
    }

    private volatile Task<T>? _valueTask;
    private AsyncFunc<CancellationToken, T>? _valueFactory;
    private readonly SemaphoreSlim? _semaphore;

    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncLazy{T}"/> class.
    /// When lazy initialization occurs, the specified initialization function is used.
    /// </summary>
    /// <param name="valueFactory">The delegate that is invoked to produce the lazily initialized value when it is needed.</param>
    /// <exception cref="ArgumentNullException"><paramref name="valueFactory"/> is <see langword="null"/>.</exception>
    public AsyncLazy(AsyncFunc<CancellationToken, T> valueFactory)
        : this(valueFactory, AsyncLazyThreadSafetyMode.ExecutionAndPublication)
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
        : this(valueFactory, isThreadSafe ? AsyncLazyThreadSafetyMode.ExecutionAndPublication : AsyncLazyThreadSafetyMode.None)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncLazy{T}"/> class
    /// that uses the specified initialization function and thread-safety mode.
    /// </summary>
    /// <param name="valueFactory">The delegate that is invoked to produce the lazily initialized value when it is needed.</param>
    /// <param name="mode">One of the enumeration values that specifies the thread safety mode.</param>
    /// <exception cref="ArgumentNullException"><paramref name="valueFactory"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="mode"/> contains an invalid value.</exception>
    public AsyncLazy(AsyncFunc<CancellationToken, T> valueFactory, AsyncLazyThreadSafetyMode mode)
    {
        if (valueFactory is null)
            throw new ArgumentNullException(nameof(valueFactory));

        if (!Enum.IsDefined(typeof(AsyncLazyThreadSafetyMode), mode))
            throw new ArgumentOutOfRangeException(nameof(mode));

        Mode = mode;
        _valueFactory = valueFactory;
        _semaphore = mode == AsyncLazyThreadSafetyMode.ExecutionAndPublication ? new SemaphoreSlim(1, 1) : null;
    }

    /// <summary>
    /// Releases all resources used by the current instance of the <see cref="AsyncLazy{T}"/> class.
    /// </summary>
    public void Dispose()
    {
        _semaphore?.Dispose();
        _valueFactory = null;
        GC.SuppressFinalize(this);
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

        return Mode switch
        {
            AsyncLazyThreadSafetyMode.None => ExecuteAndPublishAsync(cancellationToken),
            AsyncLazyThreadSafetyMode.PublicationOnly => ExecuteAndOnlyPublishThreadSafeAsync(cancellationToken),
            _ => ExecuteAndPublishThreadSafeAsync(cancellationToken),
        };
    }

    /// <summary>
    /// Creates and returns a string representation of this instance.
    /// </summary>
    /// <returns>The result of calling <see cref="object.ToString"/> on the lazily evaluated value.</returns>
    /// <exception cref="NullReferenceException">The lazily evaluated value is <see langword="null"/>.</exception>
    public override string? ToString()
    {
        Task<T>? valueTask = _valueTask;
        return valueTask != null
#if NETCOREAPP2_0_OR_GREATER
            && valueTask.IsCompletedSuccessfully
#else
            && valueTask.Status == TaskStatus.RanToCompletion
#endif
            ? valueTask.Result!.ToString() : SR.ValueNotCreatedMessage;
    }

    // Note: It was a deliberate decision to not include the recursion detection exposed by the type
    // System.Lazy<T> for LazyThreadSafetyMode.None and LazyThreadSafetyMode.ExecutionAndPublication.
    // Like the locks employed by Lazy<T>, AsyncLazy<T> requires the use of some synchronization
    // primitive. Types like SemaphoreSlim, which are typically used in a similar capacity within asynchronous
    // code, do not record thread metadata, and so the same thread cannot "wait" upon the same semaphore instance
    // without incrementing the counter twice. Furthermore, the coordination of tasks and threads should not be
    // guaranteed in the library. As such, this behavior does not appear to naturally translate to an
    // asynchronous context.

    private async Task<T> ExecuteAndPublishAsync(CancellationToken cancellationToken)
    {
        Task<T>? valueTask = null;
        AsyncFunc<CancellationToken, T> valueFactory = ValueFactory;

        try
        {
            valueTask = valueFactory.Invoke(cancellationToken);
            T result = await valueTask.ConfigureAwait(false);

            _valueTask = valueTask;
            return result;
        }
        catch (Exception e) when (e is not OperationCanceledException)
        {
            _valueTask = valueTask ?? Task.FromException<T>(e);
            throw;
        }
    }

    [SuppressMessage("Performance", "CA1849:Call async methods when in an async method", Justification = "The _valueTask field is guaranteed to be complete.")]
    private async Task<T> ExecuteAndPublishThreadSafeAsync(CancellationToken cancellationToken)
    {
        Task<T>? valueTask = null;
        AsyncFunc<CancellationToken, T> valueFactory = ValueFactory;

        await _semaphore!.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            if (_valueTask is null)
            {
                valueTask = valueFactory.Invoke(cancellationToken);
                T result = await valueTask.ConfigureAwait(false);

                _valueTask = valueTask;
                return result;
            }

            return _valueTask.Result;
        }
        catch (Exception e) when (e is not OperationCanceledException)
        {
            _valueTask = valueTask ?? Task.FromException<T>(e);
            throw;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    [SuppressMessage("Performance", "CA1849:Call async methods when in an async method", Justification = "The _valueTask field is guaranteed to be complete.")]
    private async Task<T> ExecuteAndOnlyPublishThreadSafeAsync(CancellationToken cancellationToken)
    {
        Task<T> valueTask = ValueFactory.Invoke(cancellationToken);
        T result = await valueTask.ConfigureAwait(false);

        return Interlocked.CompareExchange(ref _valueTask, valueTask, null) is null ? result : _valueTask.Result;
    }
}

internal sealed class AsyncLazyDebugView<T>
{
    public bool IsValueCreated => _asyncLazy.IsValueCreated;

    public T? Value => _asyncLazy.ValueForDebugDisplay;

    public AsyncLazyThreadSafetyMode Mode => _asyncLazy.Mode;

    public bool IsValueFaulted => _asyncLazy.IsValueFaulted;

    private readonly AsyncLazy<T> _asyncLazy;

    public AsyncLazyDebugView(AsyncLazy<T> lazy)
        => _asyncLazy = lazy;
}
