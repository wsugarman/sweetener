# Sweetener
[![GitHub License](https://img.shields.io/github/license/wsugarman/sweetener?label=License)](https://github.com/wsugarman/sweetener/blob/main/LICENSE)
[![Sweetener Build](https://github.com/wsugarman/sweetener/actions/workflows/sweetener-ci.yml/badge.svg)](https://github.com/wsugarman/sweetener/actions/workflows/sweetener-ci.yml)
[![Sweetener Code Coverage](https://codecov.io/gh/wsugarman/sweetener/branch/main/graph/badge.svg?flag=Sweetener)](https://codecov.io/gh/wsugarman/sweetener)

The Sweetener library contains a set of APIs that extends the .NET Base Class Libraries (BCLs). New functionality is
built on top of both the BCLs' data types and the new types introduced by Sweetener. This README provides examples
for a subset of these new APIs.

## New Types
Sweetener introduces new types for common scenarios.

### Delegates
In addition to `Action` and `Func<TOut>`, Sweetener adds asynchronous and "try" delegates.

```csharp
using System.IO;
using Sweetener;

// TryFunc<TOut>
TryFunc<string, int> tryParse = int.TryParse;
bool result = tryParse("12345", out int number);

// AsyncAction
using StreamWriter writer = /* ... */;
AsyncAction<string?> writeLineAsync = writer.WriteLineAsync;
await writeLineAsync("Hello World!").ConfigureAwait(false);

// AsyncFunc<TOut>
using StreamReader reader = /* ... */;
AsyncFunc<string?> readLineAsync = reader.ReadLineAsync;
string? line = await readLineAsync().ConfigureAwait(false);
```

### Optional\<T>
While `Nullable<T>` creates a type union of `null` and value types, the `Optional<T>` class represents
*some* value of type `T` or no value at all. In this way, methods can choose to optionally return a value of type
`T`. This may be especially helpful for asynchronous methods that attempt to return a value but are unable to use
`out` parameters.

```csharp
using System;
using Sweetener;

Optional<string?> response = await TryReadMessageAsync().ConfigureAwait(false);
if (response.TryGetValue(out string? message))
    Console.WriteLine("Received: " + message);
else
    Console.WriteLine("Did not receive response");
```

## New Functionality
Sweetener adds additional functionality for many of the BCL APIs.

### BinaryComparer
For data types that represent binary data, like `byte[]`, `Stream`, and `ReadOnlySpan<byte>`, comparisons can be
made using the `BinaryComparer` class.

```csharp
using System.Collections.Generic;
using Sweetener.Collections.Generic;

byte[] a = /* ... */;
byte[] b = /* ... */;

// IComparer<T>
IComparer<byte[]> comparer = BinaryComparer.Instance;
int cmp = comparer.Compare(a, b);

// IEqualityComparer<T>
IEqualityComparer<byte[]> equalityComparer = BinaryComparer.Instance;
bool areEqual = equalityComparer.Equals(a, b);
int hashCode = equalityComparer.GetHashCode(a);
```

### MultiTask
Unlike `Task.WhenAll(Task[])`, the `MultiTask` class allows users to wait on the completion of multiple
heterogeneous tasks whose type arguments differ from one another.

```csharp
using System;
using System.Threading.Tasks;
using Sweetener.Threading.Tasks;

Task<int> task1 = /* ... */;
Task<string> task2 = /* ... */;
Task<DateTime> task3 = /* ... */;

(int n, string s, DateTime d) = await MultiTask.WhenAll(task1, task2, task3).ConfigureAwait(false);
```
