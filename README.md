# Sweetener
The Sweetener project is a collection of open source C# libraries that are meant to ease
the development of .NET applications. As project contributors encounter problems in
their own day-to-day programming, they add their solutions to the scope of Sweetener
to help other developers. All Sweetener APIs are developed with .NET paradigms in mind
and attempt to conform to the standards set forth by the Base Class Libraries (BCLs) as
much as possible.

## Table of Contents
  1. [Libraries](#Libraries)
     1. [Reliability](#Reliability)
  2. [Installation](#Installation)
  3. [License](#License)

## Libraries
### Reliability
![Build Status](https://img.shields.io/azure-devops/build/wsugarman/Sweetener/2/master.svg)
![Test Summary](https://img.shields.io/azure-devops/tests/wsugarman/Sweetener/2/master.svg)
![Code Coverage](https://img.shields.io/azure-devops/coverage/wsugarman/Sweetener/2/master.svg)

Sweetener.Reliability provides utilities for reliably running delegates that may
ocassionally result in transient failures. The lifecycle of these "unreliable" delegates
is communicated through policies which control aspects including:
  1. [`ResultPolicy<T>`](src/Sweetener.Reliability/Policies/Result/ResultPolicy.T.cs) -
     dictates how to process the result of functions
  2. [`ExceptionPolicy`](src/Sweetener.Reliability/Policies/Exception/ExceptionPolicy.cs) -
     dictates which exceptions are transient and which are fatal
  3. [`DelayPolicy`](src/Sweetener.Reliability/Policies/Delay/DelayPolicy.cs) -
     dictates how long to wait between retries
     1. For more advanced scenarios, users can leverage `ComplexDelayPolicy` and
        `ComplexDelayPolicy<T>` where the delay depends on the transient result or
        exception

#### Reliable Delegates
##### Creation
Reliable delegates can be created in a number of ways. First, users can create instances
[`ReliableAction`](src/Sweetener.Reliability/Action/ReliableAction.cs) and
[`ReliableFunc<T>`](src/Sweetener.Reliability/Func/ReliableFunc.T1.cs) using the
appropriate constructor or static helper method. Note that for every action and function
type in Sweetener.Reliability, there are type overloads that support up to 16 type
arguments.

```csharp
// Action
Action<int, bool> flakyAction = /* ... */;

ReliableAction<int, bool> reliableAction = new ReliableAction<int, bool>(
    flakyAction,
    Retries.Infinite,
    ExceptionPolicies.Retry<IOException>(),
    DelayPolicies.Constant(TimeSpan.FromMilliseconds(100)));

ReliableAction<int, bool> altReliableAction = ReliableAction.Create
    flakyAction,
    Retries.Infinite,
    ExceptionPolicies.Retry<IOException>(),
    DelayPolicies.Constant(TimeSpan.FromMilliseconds(100)));

// Func
Func<string, double, string> flakyFunc = /* ... */;

ReliableFunc<string, double, string> reliableFunc = new ReliableFunc<string, double, string>(
    flakyFunc,
    3,
    r => r == "Success" ? ResultKind.Successful : ResultKind.Transient,
    ExceptionPolicies.Fail<InvalidOperationException>(),
    DelayPolicies.Linear(50));

ReliableFunc<string, double, string> altReliableFunc = ReliableFunc.Create
    flakyFunc,
    3,
    r => r == "Success" ? ResultKind.Successful : ResultKind.Transient,
    ExceptionPolicies.Fail<InvalidOperationException>(),
    DelayPolicies.Linear(50));
);
```

##### Operations
Both `ReliableAction` and `ReliableFunc<T>` have two sets of methods:
  1. `Invoke` - Continues to invoke the underlying delegate based on the provided
     policies until the operation completes successfully, the operation fails, or
     the number of retries has been exhausted. It should be noted that the contract with
     callers of `Invoke` should look identical to that of the underlying delegate.
  2. `TryInvoke` - Similar to `Invoke`, however `TryInvoke` will only return `true`
     if the operation completes successfully; otherwise it returns `false`. Furthermore
     the `out` parameter `result` will only ever contain the value when `true` is
     returned. Transient and fatal results are never returned.

Both methods provide overrides with a [`CancellationToken`](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken?view=netstandard-2.0)
to interrupt potential retries.

Example usage of `ReliableAction` below:
```csharp
// Invoke
try
{
    // Careful!
    // Invoke will rethrow exceptions if the method is ultimately unsuccessful
    reliableAction.Invoke(42, false);
}
catch (IOException)
{
    /* ... */
}

// TryInvoke
// On the other hand, TryInvoke provides safer semantics
if (reliableAction.TryInvoke(42, false))
{
    // Success
}
else
{
    // Failure
}
```

Example usage of `ReliableFunc<T>` below:
```csharp
string result;

// Invoke
try
{
    // Careful!
    // Invoke will rethrow exceptions if the method is ultimately unsuccessful
    result = reliableFunc.Invoke("Foo", 3.14);
}
catch (IOException)
{
    /* ... */
}

// TryInvoke
// On the other hand, TryInvoke provides safer semantics
if (reliableFunc.TryInvoke("Foo", 3.14, out result))
{
    // Success
}
else
{
    // Failure
}
```

##### Events
`ReliableAction` and `ReliableFunc<T>` also provide a few events to observe the
reliable delegates at various points in its lifecycle:
  - `Retrying` - Triggers right after the reliable delegate has waited the amount specified
    by the policy, but before the underlying delegate is invoked again
  - `RetriesExhausted` - Triggers after the underlying delegate has resulted in a
    transient result or exception but is unable to retry the operation
  - `Failed` - Triggers after the underlying delegate has resulted in a fatal
    result or exception

```csharp
// Action
reliableAction.Retrying += (int attempt, Exception exception) =>
    Console.WriteLine($"Attempt #{attempt} failed with transient exception {exception.GetType().Name}");

reliableAction.RetriesExhausted += (Exception exception) =>
    Console.WriteLine($"Retries exhausted after exception {exception.GetType().Name} thrown!");

reliableAction.Failed += (Exception exception) =>
    Console.WriteLine($"Fatal exception {exception.GetType().Name} thrown!");

// Func
// Notice that the events for ReliableFunc<T> have both a result and an exception
// argument. However, only one of these values will be populated, depending on how
// the underlying delegate returned. The exception argument can be used to disambiguate.
reliableFunc.Retrying += (int attempt, string result, Exception exception) =>
{
    string msg = exception == null
        ? $"Attempt #{attempt} failed with transient result '{result}' returned!"
        : $"Attempt #{attempt} failed with transient exception {exception.GetType().Name} thrown!";

    Console.WriteLine(msg);
};

reliableFunc.RetriesExhausted += (string result, Exception exception) =>
{
    string msg = exception == null
        ? $"Retries exhausted after result '{result}' returned!"
        : $"Retries exhausted after exception {exception.GetType().Name} thrown!";

    Console.WriteLine(msg);
};

reliableFunc.Failed += (string result, Exception exception) =>
{
    string msg = exception == null
        ? $"Fatal result '{result}' returned!"
        : $"Fatal exception {exception.GetType().Name} thrown!";

    Console.WriteLine(msg);
};
```

#### Extensions
For simpler use-cases, where only the `Invoke` method is called on instances of
`ReliableAction` and `ReliableFunc<T>` without any event handlers, extension
methods can be used to wrap delegates instead. The `Sweetener.Reliability` namespace
defines extension methods on [`Action`](https://docs.microsoft.com/en-us/dotnet/api/system.action?view=netstandard-2.0)
and [`Func<T>`](https://docs.microsoft.com/en-us/dotnet/api/system.func-1?view=netstandard-2.0)
called [`WithRetry`](src/Sweetener.Reliability/Action/Action.Extensions.cs). These
methods return [`InterruptableAction`](src/Sweetener.Reliability/Action/InterruptableAction.cs)
and [`InterruptableFunc<T>`](src/Sweetener.Reliability/Func/InterruptableFunc.cs)
respectively which allow callers to pass an optional `CancellationToken`.

```csharp
// Action
InterruptableAction<int, bool> reliableInterruptableAction = flakyAction.WithRetry(
    Retries.Infinite,
    ExceptionPolicies.Retry<IOException>(),
    DelayPolicies.Constant(TimeSpan.FromMilliseconds(100)));

// Func
InterruptableFunc<string, double, string> reliableInterruptableFunc = flakyFunc.WithRetry(
    3,
    r => r == "Success" ? ResultKind.Successful : ResultKind.Transient,
    ExceptionPolicies.Fail<InvalidOperationException>(),
    DelayPolicies.Linear(50));
```

## Installation
The Sweetener libraries are planned to be consumable from NuGet in the near future.

## License
The Sweetener libraries are all licensed under the [MIT](LICENSE) license.
