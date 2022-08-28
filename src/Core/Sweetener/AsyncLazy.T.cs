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
    // Throughout the class, there will be the repeated pattern of fetching the _valueFactory before the _valueTask.
    // This pattern is used because the _valueFactory is volatile, and the volatile read will update the values
    // of other variables for the current thread. As long as _valueFactory is updated after the non-volatile
    // members, we can make assumptions about their values. For example, if the _valueFactory is null, the _valueTask
    // itself must be non-null.

    /// <summary>
    /// Gets a value that indicates whether a value has been created for this <see cref="AsyncLazy{T}"/> instance.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if a value has been created for this <see cref="AsyncLazy{T}"/> instance;
    /// otherwise, <see langword="false"/>.
    /// </value>
    public bool IsValueCreated => _valueFactory is null
        && !ReferenceEquals(_valueTask, DisposedTask)
#if NETCOREAPP2_0_OR_GREATER
        && _valueTask!.IsCompletedSuccessfully;
#else
        && _valueTask!.Status == TaskStatus.RanToCompletion;
#endif

    internal bool IsValueFaulted => _valueFactory is null
        && !ReferenceEquals(_valueTask, DisposedTask)
        && _valueTask!.IsFaulted;

    internal AsyncLazyThreadSafetyMode Mode { get; }

    internal T? ValueForDebugDisplay => IsValueCreated ? _valueTask!.Result : default;

    private Task<T>? _valueTask;
    private SemaphoreSlim? _semaphore;
    private volatile AsyncFunc<CancellationToken, T>? _valueFactory;

    private static readonly Task<T> DisposedTask = Task.FromException<T>(new ObjectDisposedException(typeof(AsyncLazy<T>).FullName));

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

        Mode          = mode;
        _valueFactory = valueFactory;
        _semaphore    = mode == AsyncLazyThreadSafetyMode.ExecutionAndPublication ? new SemaphoreSlim(1, 1) : null;
    }

    /// <summary>
    /// Releases all resources used by the current instance of the <see cref="AsyncLazy{T}"/> class.
    /// </summary>
    /// <remarks>
    /// Even if the instance is thread-safe or was created with the mode
    /// <see cref="AsyncLazyThreadSafetyMode.ExecutionAndPublication"/>, <see cref="Dispose"/> is not thread-safe
    /// and may not be used concurrently with other members of this instance.
    /// </remarks>
    public void Dispose()
    {
        if (_valueFactory is not null || !ReferenceEquals(_valueTask, DisposedTask))
        {
            _semaphore?.Dispose();
            _valueTask?.Dispose();
            _semaphore    = null;
            _valueTask    = DisposedTask;
            _valueFactory = null; // Volatile field must be last

            GC.SuppressFinalize(this);
        }
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
    /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
    /// <exception cref="OperationCanceledException">
    /// The <paramref name="cancellationToken"/> requested cancellation.
    /// </exception>
    public Task<T> GetValueAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        AsyncFunc<CancellationToken, T>? valueFactory = _valueFactory;
        if (valueFactory is null)
            return _valueTask!;

        return Mode switch
        {
            AsyncLazyThreadSafetyMode.None            => ExecuteAndPublishAsync              (valueFactory, cancellationToken),
            AsyncLazyThreadSafetyMode.PublicationOnly => ExecuteAndOnlyPublishThreadSafeAsync(valueFactory, cancellationToken),
            _                                         => ExecuteAndPublishThreadSafeAsync    (              cancellationToken),
        };
    }

    /// <summary>
    /// Creates and returns a string representation of this instance.
    /// </summary>
    /// <returns>The result of calling <see cref="object.ToString"/> on the lazily evaluated value.</returns>
    /// <exception cref="NullReferenceException">The lazily evaluated value is <see langword="null"/>.</exception>
    public override string? ToString()
        => IsValueCreated ? _valueTask!.Result!.ToString() : SR.ValueNotCreatedMessage;

    // Note: It was a deliberate decision to not include the recursion detection exposed by the type
    // System.Lazy<T> for LazyThreadSafetyMode.None and LazyThreadSafetyMode.ExecutionAndPublication.
    // Like the locks employed by Lazy<T>, AsyncLazy<T> requires the use of some synchronization
    // primitive. Types like SemaphoreSlim, which are typically used in a similar capacity within asynchronous
    // code, do not record thread metadata, and so the same thread cannot "wait" upon the same semaphore instance
    // without incrementing the counter twice. It may be reevaluated whether this behavior is necessary.

    private async Task<T> ExecuteAndPublishAsync(AsyncFunc<CancellationToken, T> valueFactory, CancellationToken cancellationToken)
    {
        Task<T>? valueTask = null;

        try
        {
            // Note: The behavior is undefined if more than 1 thread is used with LazyThreadSafetyMode.None.
            // For example, it may lead to a disposed instance no longer appearing disposed!

            valueTask = valueFactory.Invoke(cancellationToken);
            T result = await valueTask.ConfigureAwait(false);

            _valueTask    = valueTask;
            _valueFactory = null; // Volatile field must be last
            return result;
        }
        catch (Exception e) when (e is not OperationCanceledException)
        {
            _valueTask    = valueTask ?? Task.FromException<T>(e);
            _valueFactory = null; // Volatile field must be last
            throw;
        }
    }

    [SuppressMessage("Performance", "CA1849:Call async methods when in an async method", Justification = "The _valueTask field is guaranteed to be complete.")]
    private async Task<T> ExecuteAndPublishThreadSafeAsync(CancellationToken cancellationToken)
    {
        Task<T>? valueTask = null;

        // Note: Callers are warned not to call Dispose() concurrently,
        // so it is safe to fetch _semaphore without checking for a null value
        await _semaphore!.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            AsyncFunc<CancellationToken, T>? valueFactory = _valueFactory;
            if (valueFactory is not null)
            {
                valueTask = valueFactory.Invoke(cancellationToken);
                T result = await valueTask.ConfigureAwait(false);

                _valueTask    = valueTask;
                _valueFactory = null; // Volatile field must be last
                return result;
            }

            return _valueTask!.Result;
        }
        catch (Exception e) when (e is not OperationCanceledException)
        {
            _valueTask    = valueTask ?? Task.FromException<T>(e);
            _valueFactory = null; // Volatile field must be last
            throw;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    [SuppressMessage("Performance", "CA1849:Call async methods when in an async method", Justification = "The _valueTask field is guaranteed to be complete.")]
    private async Task<T> ExecuteAndOnlyPublishThreadSafeAsync(AsyncFunc<CancellationToken, T> valueFactory, CancellationToken cancellationToken)
    {
        Task<T> valueTask = valueFactory.Invoke(cancellationToken);
        T result = await valueTask.ConfigureAwait(false);

        Task<T>? previous = Interlocked.CompareExchange(ref _valueTask, valueTask, null);
        if (previous is null)
        {
            _valueFactory = null; // Volatile field must be last
            return result;
        }

        return previous.Result;
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
