# Release Notes

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