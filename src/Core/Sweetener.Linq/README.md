# Sweetener.Linq
[![GitHub License](https://img.shields.io/github/license/wsugarman/sweetener?label=License)](https://github.com/wsugarman/sweetener/blob/main/LICENSE)
[![Sweetener.Linq Build](https://github.com/wsugarman/sweetener/actions/workflows/sweetener.linq-ci.yml/badge.svg)](https://github.com/wsugarman/sweetener/actions/workflows/sweetener.linq-ci.yml)
[![Sweetener.Linq Code Coverage](https://codecov.io/gh/wsugarman/sweetener/branch/main/graph/badge.svg?flag=Sweetener.Linq)](https://codecov.io/gh/wsugarman/sweetener)

The Sweetener.Linq library contains a set of APIs that extends the `System.Linq` namespace in the .NET Base Class
Libraries (BCLs).

## Collection
The `Collection` class provides extension methods for enumerating instances of `IReadOnlyCollection<T>`. Unlike
those found in the `System.Linq` namespace, these extension methods preserve the `IReadOnlyCollection<T>`
type such that callers can retrieve the value of the `Count` property without enumerating through every element.

```csharp
using System.Collections.Generic;
using Sweetener.Linq;

IReadOnlyCollection<int> numbers = new List<int> { 3, 2, 5, 1, 4 };

// Select
IReadOnlyCollection<int> result = numbers.Select(x => x * 2);

// Reverse
IReadOnlyCollection<int> backwards = numbers.Reverse();

// Take
IReadOnlyCollection<int> first = numbers.Take(3);

// Skip
IReadOnlyCollection<int> last = numbers.Skip(3);

// OrderBy
IReadOnlyCollection<int> ordered = numbers.OrderBy(x => x);
```
