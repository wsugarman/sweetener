# Release Notes

## 1.0.0-alpha.1
- Initial pre-release
- Define the following APIs:
    - `Collection` which provides LINQ-like extension methods for `IReadOnlyCollection<T>` that return
      `IReadOnlyCollection<T>` instead of `IEnumerable<T>` when possible
    - `Enumerator.Empty<T>()` which can be used to efficiently enumerator over zero elements
    - `IOrderedReadOnlyCollection<TElement>` which serves a parallel to `IOrderedEnumerable<TElement>`
