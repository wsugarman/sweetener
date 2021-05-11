# Release Notes

## 1.0.0-beta.2
- Define the following APIs:
    - `ArgumentNullOrEmptyException` and `ArgumentNullOrWhiteSpaceException` which can be
      thrown if an argument returns `true` for `string.IsNullOrEmpty` or `string.IsNullOrWhiteSpace`
      respectively
    - `AsyncAction` and `AsyncFunc<T>` which are asynchronous parallels for `Action` and `Func<T>`
    - `Enumerator.Empty<T>`, similar to `Enumerable.Empty<T>`, which may be used when implementing
      `IEnumerable<T>` when no items should be enumerated
    - Extensions for `DateTime` and `TimeSpan` for truncation and addition
    - `MultiTask` which provides heterogeneous `Task<TResult>` utilities like `WhenAll`

## 1.0.0-beta.1
- Fix build to be deterministic and correctly leverage SourceLink
- Remove `Action` and `Func<TResult>` overloads

## 1.0.0-alpha.2
- **Bug Fix**: Add missing timestamp to assembly signature

## 1.0.0-alpha.1
- Initial pre-release
- Define the following APIs:
    - `ArgumentNegativeException` which serves as a common use-case for `ArgumentOutOfRangeException`
    - `Optional<T>` values where the value may or may not be defined
    - `TryFunc` delegates
    - Additional `Action` and `Func<TResult>` overloads
