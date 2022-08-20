// Copyright © William Sugarman.
// Licensed under the MIT License.

namespace Sweetener.Threading;

/// <summary>
/// Specifies how an <see cref="AsyncLazy{T}"/> instance synchronizes access among multiple threads.
/// </summary>
public enum AsyncLazyThreadSafetyMode
{
    /// <summary>
    /// The <see cref="AsyncLazy{T}"/> instance is not thread safe; if the instance is accessed from multiple threads,
    /// its behavior is undefined. Use this mode only when high performance is crucial and the
    /// <see cref="AsyncLazy{T}"/> instance is guaranteed never to be initialized from more than one thread.
    /// If the initialization method throws an exception (or fails to handle an exception) the first time you call
    /// the <see cref="AsyncLazy{T}.GetValueAsync(System.Threading.CancellationToken)"/> method, then the exception
    /// is cached and thrown again on subsequent calls to the
    /// the <see cref="AsyncLazy{T}.GetValueAsync(System.Threading.CancellationToken)"/> method.
    /// </summary>
    None,

    /// <summary>
    /// When multiple threads try to initialize an <see cref="AsyncLazy{T}"/> instance simultaneously, all threads are
    /// allowed to run the initialization method. The first thread to complete initialization sets the value of the
    /// <see cref="AsyncLazy{T}"/> instance. That value is returned to any other threads that were simultaneously
    /// running the initialization method, unless the initialization method throws exceptions on those threads.
    /// Any instances of <c>T</c> that were created by the competing threads are discarded. Effectively,
    /// the publication of the initialized value is thread-safe in the sense that only one of the initialized values
    /// may be published and used by all threads. If the initialization method throws an exception on any thread,
    /// the exception is propagated out of the <see cref="AsyncLazy{T}.GetValueAsync(System.Threading.CancellationToken)"/>
    /// method on that thread. The exception is not cached. The value of the <see cref="AsyncLazy{T}.IsValueCreated"/>
    /// property remains <see langword="false"/>, and subsequent calls to the
    /// <see cref="AsyncLazy{T}.GetValueAsync(System.Threading.CancellationToken)"/> method, either by the thread
    /// where the exception was thrown or by other threads, cause the initialization method to run again.
    /// </summary>
    PublicationOnly,

    /// <summary>
    /// Semaphores are used to ensure that only a single thread can initialize an <see cref="AsyncLazy{T}"/> instance
    /// in a thread-safe manner. Effectively, the initialization method is executed in a thread-safe manner.
    /// Publication of the initialized value is also thread-safe in the sense that only one value may be published and
    /// used by all threads. If the initialization method uses locks internally, deadlocks can occur. If the
    /// initialization method throws an exception (or fails to handle an exception) the first time you call the
    /// <see cref="AsyncLazy{T}.GetValueAsync(System.Threading.CancellationToken)"/> method, then the exception is
    /// cached and thrown again on subsequent calls to the
    /// <see cref="AsyncLazy{T}.GetValueAsync(System.Threading.CancellationToken)"/> method.
    /// </summary>
    ExecutionAndPublication,
}
