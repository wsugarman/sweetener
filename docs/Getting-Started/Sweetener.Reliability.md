# Getting Started

The release notes for this package can be found [here](../Release-Notes/Sweetener.Reliability.md)

## Table of Contents
1. [The Basics](#The-Basics)
2. [Factory Methods](#Factory-Methods)
3. [Extension Methods](#Extension-Methods)
4. [Policies](#Policies)
    1. [Result](#Result)
    2. [Exception](#Exception)
    3. [Delay](#Delay)
5. [Advanced Scenarios](#Advanced-Scenarios)
    1. [Asynchronous Operations](#Asynchronous-Operations)
    2. [Cancellation](#Cancellation)
    3. [Execution Events](#Execution-Events)
        1. [Retrying](#Retrying)
        2. [RetriesExhausted](#RetriesExhausted)
        3. [Failed](#Failed)

## The Basics

The example below is a common scenario. There is a method called `SendSometimes` that
occassionally throws instances of `TransientExceptions`. So to compensate, there is another
method called `Send` which will continue to retry `SendSometimes` if it encounters
`TransientExceptions` at most 5 times. To prevent making too many requests too soon,
`Send` will wait 50 milliseconds between attempts. In this way, we can write the retry logic
for `SendSometimes` one time:

```csharp
namespace Sweetener.Example
{
    public static void Main(params string[] args)
    {
        Console.WriteLine(Send("Hello World"));
    }

    public string Send(string message)
    {
        for (int attempts = 1; attempts <= 5; attempts++)
        {
            try
            {
                return SendSometimes(message);
            }
            catch (TransientException e)
            {
                if (attempt <= 5)
                    Task.Delay(TimeSpan.FromMilliseconds(50)).Wait();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    
        throw new TransientException("Out of retries");
    }

    private string SendSometimes(string message)
    {
        /* ... */
    }
}
```

However, the `Sweetener.Reliability` package allows this boilerplate to be significantly reduced:

```csharp
using Sweetener.Reliability;

namespace Sweetener.Example
{
    public static void Main(params string[] args)
    {
        ReliableFunc<string, string> send = new ReliableFunc<string, string>(
            SendSometimes,
            5,
            ExceptionPolicy.Retry<TransientException>(),
            DelayPolicy.Constant(TimeSpan.FromMilliseconds(50)));

        Console.WriteLine(send.Invoke("Hello World"));
    }

    private void SendSometimes(string message)
    {
        /* ... */
    }
```

Here, we've defined a reliable wrapper around `SendSometimes` called `send` whose
retry logic is defined by the provided arguments and handlers:

1. `SendSometimes` is the delegate we want to run more reliabily
2. `5` is the maximum number of attempts we'll allow before giving up
3. `ExceptionPolicy.Retry<TransientException>()` defines an `ExceptionHandler`
   that will only retry `SendSometimes` if `TransientException` is thrown
4. `DelayPolicy.Constant(TimeSpan.FromMilliseconds(50))` defines a `DelayPolicy`
   that will consistently wait 50 milliseconds between each attempt

## Factory Methods

Similar to how the [`Tuple`](https://docs.microsoft.com/en-us/dotnet/api/system.tuple?view=netstandard-2.0)
class provides a [`Create`](https://docs.microsoft.com/en-us/dotnet/api/system.tuple.create?view=netstandard-2.0)
method for creating tuples where the types can be inferred, so too do the various reliable delegate classes
provide their own respective `Create` methods, as seen in the following example. Note that for
every constructor overload, there is a corresponding `Create` overload.

```csharp
using Sweetener.Reliability;

namespace Sweetener.Example
{
    public static void Main(params string[] args)
    {
        ReliableFunc<string, string> send = ReliableFunc.Create(
            SendSometimes,
            5,
            ExceptionPolicy.Retry<TransientException>(),
            DelayPolicy.Constant(TimeSpan.FromMilliseconds(50)));

        Console.WriteLine(send.Invoke("Hello World"));
    }

    private void SendSometimes(string message)
    {
        /* ... */
    }
```

## Extension Methods

Preferrably, methods would handle the aspects of their reliability themselves; users could
call the method and it would retry as needed without them knowing. Therefore, the typical
caller is probably using the `Invoke` method to preserve the same method signature as their
underlying delegate. To enable this scenario more expressively, and without the overhead
of other features like the various [execution events](#Events), the Sweetener.Reliability
package provides the extension methods `WithRetry` and `WithAsyncRetry` like in the following
example:

```csharp
using Sweetener.Reliability;

namespace Sweetener.Example
{
    public static void Main(params string[] args)
    {
        Func<string, string> sendSometimes = /* ... */;
        Func<string, string> send = sendSometimes.WithRetry(
            5,
            ExceptionPolicy.Retry<TransientException>(),
            DelayPolicy.Constant(TimeSpan.FromMilliseconds(50)));

        Console.WriteLine(send("Hello World"));
    }
```

## Policies
### Result
### Exception
### Delay

## Advanced Scenarios

### Asynchronous Operations

Sweetener.Reliability also supports asynchronous operations. All reliable delegates,
synchronous or otherwise, support asynchronous invocation with methods like `InvokeAsync`
that will asynchronously delay. However, if the underlying delegate is also asynchronous,
`InvokeAsync` will also `await` the delegate itself.

In the following example, the asynchronous method `SendSometimes` is wrapped using an
asynchronous reliable function:

```csharp
using Sweetener.Reliability;

namespace Sweetener.Example
{
    public static void Main(params string[] args)
    {
        // Create the reliable function
        ReliableAsyncFunc<string, string> sendAsync = new ReliableAsyncFunc<string, string>(
            SendSometimesAsync,
            5,
            ExceptionPolicy.Retry<TransientException>(),
            DelayPolicy.Constant(TimeSpan.FromMilliseconds(50)));

        Console.WriteLine(sendAsync.InvokeAsync("Hello World").Result);
    }

    private Task<string> SendSometimesAsync(string message)
    {
        /* ... */
    }
}
```

### Cancellation

All of the invocation methods support cancellation via a
[`CancellationToken`](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken?view=netstandard-2.0),
which will at the very least interrupt any delay specified by the `DelayHandler`. However,
some underlying delegates may natively support cancellation and allow their execution to be
interrupted. To leverage this native cancellation support, all reliable types, factory methods,
and extension methods support the use of a `CancellationToken` via constructor and
method overloads.

In the following example, the interruptable method `SendSometimes` is wrapped using
a reliable function:

```csharp
using Sweetener.Reliability;

namespace Sweetener.Example
{
    public static void Main(params string[] args)
    {
        // Create the reliable function
        ReliableFunc<string, string> send = new ReliableFunc<string, string>(
            SendSometimes,
            5,
            ExceptionPolicy.Retry<TransientException>(),
            DelayPolicy.Constant(TimeSpan.FromMilliseconds(50)));

        // Create a CancellationTokenSource that cancels its token after 1 second
        using CancellationTokenSource source = new CancellationTokenSource();
        source.CancelAfter(TimeSpan.FromSeconds(1));

        // Run the function
        send.Invoke("Hello World", source.Token);
    }

    private string SendSometimes(string message, CancellationToken token)
    {
        /* ... */
    }
}
```

### Events

...
