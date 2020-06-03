# Release Notes

## 1.0.0-beta.1
- Fix build to be deterministic and correctly leverage SourceLink
- Remove reliable delegate type overloads beyond 8 inputs
- Add the class `Reliably` that provides static methods for invoking delegates
- Standardize `CancellationToken` usage across the library
    - Methods that pass the `CancellationToken` to the delegate will check the resulting
      `OperationCanceledException` to see if its token matches
    - The `Reliably` class always re-throws `OperationCanceledException` regardless of its token

## 1.0.0-alpha.2
- **Bug Fix**: Add missing timestamp to assembly signature

## 1.0.0-alpha.1
- Initial pre-release
- Define the following APIs:
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