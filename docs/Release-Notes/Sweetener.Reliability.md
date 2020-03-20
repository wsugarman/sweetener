# Release Notes

## 1.0.0-alpha.2
- **Bug Fix**: Add missing timestamp to assembly signature

## 1.0.0-alpha.1
- Initial pre-release
- Defines the following APIs:
    - Reliable delegate type overloads:
        - `ReliableAction`
        - `ReliableAsyncAction`
        - `ReliableFunc<TResult>`
        - `ReliableAsyncFunc<TResult>`
    - A variety of methods for reliably invoking the underlying delegates:
        - `Invoke`
        - `InvokeAsync`
        - `TryInvoke`
        - `TryInvokeAsync`
    - Reliable delegate factory method overloads:
        - `ReliableAction.Create`
        - `ReliableAsyncAction.Create`
        - `ReliableFunc.Create`
        - `ReliableAsyncFunc.Create`
    - Extension methods for the most common use-cases:
        - `WithRetry`
        - `WithAsyncRetry`
    - Policies, and their corresponding handlers, that determine execution behavior:
        - `DelayPolicy`, `DelayHandler`, and `ComplexDelayHandler`
        - `ExceptionPolicy` and `ExceptionHandler`
        - `ResultPolicy` and `ResultHandler<T>`