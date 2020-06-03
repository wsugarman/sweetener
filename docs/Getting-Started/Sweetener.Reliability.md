# Getting Started

The release notes for this package can be found [here](../Release-Notes/Sweetener.Reliability.md)

## Table of Contents
1. [The Basics](#the-basics)
2. [Reliable Delegates](#reliable-delegates)
3. [Factory Methods](#factory-methods)
4. [Extension Methods](#extension-methods)
5. [Policies](#policies)
    1. [Result](#result)
    2. [Exception](#exception)
    3. [Delay](#delay)
6. [Advanced Scenarios](#advanced-scenarios)
    1. [Asynchronous Operations](#asynchronous-operations)
    2. [Cancellation](#cancellation)
    3. [Execution Events](#execution-events)
        1. [Retrying](#retrying)
        2. [RetriesExhausted](#retriesexhausted)
        3. [Failed](#failed)

## The Basics

The `Reliability` library centralizes the typical boilerplate necessary to retry a method.
For example, consider a method called `SendSometimes` that occassionally throws instances of
`TransientExceptions`. To compensate, we might write another method called `Send` which
continues to invoke `SendSometimes` until it either completes successfully, throws a
`TransientException` at most 5 times, or fails with an unexpected error. To prevent making too
many requests too soon, `Send` waits 50 milliseconds between attempts. The below snippet shows
what `SendSometimes` may look like when written without `Sweetener.Reliability`:

```csharp
namespace Sweetener.Example
{
    public static void Main(params string[] args)
    {
        Console.WriteLine(Send("Hello World"));
    }

    public string Send(string msg)
    {
        int i = 0;
        while (true)
        {
            i++;

            try
            {
                return SendSometimes(msg);
            }
            catch (TransientException e)
            {
                if (i > 5)
                    throw;

                Task.Delay(TimeSpan.FromMilliseconds(50)).Wait();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }

    private string SendSometimes(string msg)
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
        var msg = Reliably.Invoke(
            () => SendSometimes("Hello World")
            4,
            ExceptionPolicy.Retry<TransientException>(),
            DelayPolicy.Constant(TimeSpan.FromMilliseconds(50)));

        Console.WriteLine(msg);
    }

    private string SendSometimes(string msg)
    {
        /* ... */
    }
```

Here, we've passed a delegate that runs `SendSometimes` to `Reliably.Invoke` which will
unsurprisingly invoke the delegate based on the provided arguments and handlers:

- `4` is the maximum number of retries we'll allow before giving up
- `ExceptionPolicy.Retry<TransientException>()` defines an `ExceptionHandler`
   that will only retry `SendSometimes` if `TransientException` is thrown
- `DelayPolicy.Constant(TimeSpan.FromMilliseconds(50))` defines a `DelayHandler`
   that will consistently wait 50 milliseconds between each attempt

Typically there are four different methods for reliably invoking delegates:
- `Invoke` - synchronously invokes delegate based on the provided handlers. If delegate
              runs out of additional retries, it will return the last result or throw the
              last exception
- `InvokeAsync` - the same as `Invoke` except that the optional delay is run asychronously
- `TryInvoke` - synchronously invokes the delegate based on the provided handlers and returns
                 `true` if the delegate succeeded; otherwise it returns `false`
- `TryInvokeAsync` - the same as `TryInvoke` except that the optional delay is run asychronously

## Reliable Delegates

In some instances, it may be preferrable to wrap delegates in a statically-typed and reusable
wrapper whose invocation method is flexible. In those cases, developers can use the various type
overloads for `ReliableAction`, `ReliableFunc<TResult>`, `ReliableAsyncAction`, and
`ReliableAsyncFunc<TResult>`.

```csharp
using Sweetener.Reliability;

namespace Sweetener.Example
{
    public static void Main(params string[] args)
    {
        ReliableFunc<string, string> send = new ReliableFunc<string, string>(
            SendSometimes,
            4,
            ExceptionPolicy.Retry<TransientException>(),
            DelayPolicy.Constant(TimeSpan.FromMilliseconds(50)));

        Console.WriteLine(send.Invoke("Hello World"));
    }

    private string SendSometimes(string msg)
    {
        /* ... */
    }
}
```

## Factory Methods

Similar to how the [`Tuple`](https://docs.microsoft.com/en-us/dotnet/api/system.tuple?view=netstandard-2.0)
class provides a [`Create`](https://docs.microsoft.com/en-us/dotnet/api/system.tuple.create?view=netstandard-2.0)
method for creating tuples where the types can be inferred, so too do the various reliable delegate
classes provide their own respective `Create` methods, as seen in the following example. Note
that for every constructor overload, there is a corresponding `Create` overload.

```csharp
using Sweetener.Reliability;

namespace Sweetener.Example
{
    public static void Main(params string[] args)
    {
        ReliableFunc<string, string> send = ReliableFunc.Create(
            SendSometimes,
            4,
            ExceptionPolicy.Retry<TransientException>(),
            DelayPolicy.Constant(TimeSpan.FromMilliseconds(50)));

        Console.WriteLine(send.Invoke("Hello World"));
    }

    private string SendSometimes(string msg)
    {
        /* ... */
    }
}
```

## Extension Methods

Preferrably, methods would handle the aspects of their reliability themselves; users could
call the method and it would retry as needed without them knowing. In fact, we can preserve
the method signature such that downstream callers would never know the difference using the
extension methods `WithRetry` and `WithAsyncRetry` like in the following example:

```csharp
using Sweetener.Reliability;

namespace Sweetener.Example
{
    public static void Main(params string[] args)
    {
        Func<string, string> sendSometimes = /* ... */;
        Func<string, string> send = sendSometimes.WithRetry(
            4,
            ExceptionPolicy.Retry<TransientException>(),
            DelayPolicy.Constant(TimeSpan.FromMilliseconds(50)));

        Console.WriteLine(send("Hello World"));
    }
}
```

## Policies

The execution of delegates is configured using various *handlers* that enact *policies*
in three different categories.

### Result

Result handlers indicate how the reliable function should continue based on the output of a
function. By default, functions are assumed to have executed successfully if they complete
without throwing an exception, but sometimes the caller may want to inspect the output. For
example, a method may return status codes that indicate the success of the operation.

```csharp
ResultHandler<string> getResultKind = (string msg) =>
    msg switch
    {
        "What did you say?" => ResultKind.Transient,
        "I can't hear you." => ResultKind.Fatal,
        _                   => ResultKind.Successful,
    };
```

A default `ResultHandler<T>`, where every result is considered a success, is assumed if left
unspecified, but it can also be explicitly specified using `ResultPolicy.Default<T>()`.

### Exception

Exception handlers indicate whether an
[`Exception`](https://docs.microsoft.com/en-us/dotnet/api/system.exception?view=netstandard-2.0)
is transient such that the underlying delegate should be invoked again. Unlike a `ResultHandler<T>`,
an `ExceptionHandler` is always specified.

```csharp
ExceptionPolicy shouldRetry = (Exception e) => e is TransientException;
```

For the most common use cases, `Sweetener.Reliability` provides the following implementations:
- `ExceptionPolicy.Transient` - All exceptions are retried
- `ExceptionPolicy.Fatal` - All exceptions immediately fail the delegate
- `ExceptionPolicy.Retry<TException>()` - The one or more exception type parameters should
  be retried; all other exceptions are assumed to be fatal
- `ExceptionPolicy.Fail<TException>()` - The one or more exception type parameters should
  immediately fail the delegate; all other exceptions are assumed to be transient

### Delay

Delay handlers indicate the amount of time to wait after a given attempt to invoke the
underlying delegate. Like an `ExceptionHandler`, a `DelayHandler` is always specified.
However, there are two kinds of delay handlers

1. **Simple** (ie. the default) - delay handlers that optionally based the delay on the attempt number:

```csharp
DelayHandler constant = _ => TimeSpan.FromMilliseconds(100);

// or

DelayHandler simple = (int i) => TimeSpan.FromMilliseconds(100 * i);
```

2. **Complex** - delay handlers that leverage the exception (or the result in the case of reliable
  functions) that caused the retry:

```csharp
// For reliable actions
ComplexDelayHandler getDelay1 = (int i, Exception e) =>
{
    if (e is TransientException t)
        return t.Backoff;
    else
        return TimeSpan.FromMilliseconds(100 * i);
};

// or

// For reliable functions
ComplexDelayHandler<Response> getDelay2 = (int i, Response r, Exception e) =>
{
    if (e == null)
        return r.Backoff;
    else if (e is TransientException t)
        return t.Backoff;
    else
        return TimeSpan.FromMilliseconds(100 * i);
};
```

Commonly used `DelayHandlers` may be found in the `DelayPolicy` class, including:
- `DelayPolicy.None` - The underlying delegate is retried immediately
- `DelayPolicy.Constant()` - The delay between attempts is a constant period of time
- `DelayPolicy.Linear(int)` - The delay between attempts increases linearly
- `DelayPolicy.Exponential(int, Random)` - The delay between attempts increases exponentially

## Advanced Scenarios

### Asynchronous Operations

`Sweetener.Reliability` also supports asynchronous operations. Both `Reliably` and all
reliable delegates support asynchronous invocation with methods the `InvokeAsync` and
`TryInvokeAsync` which will asynchronously wait between invocation attempts. However, if
the underlying delegate is also asynchronous, they will be run asychronously too.

In the following example, the asynchronous method `SendSometimes` is wrapped using an
asynchronous reliable function:

```csharp
using System.Threading.Tasks;
using Sweetener.Reliability;

namespace Sweetener.Example
{
    public static async Task Main(params string[] args)
    {
        // Create the reliable function
        var msg = await Reliably.InvokeAsync
            () => SendSometimesAsync("Hello World"),
            4,
            ExceptionPolicy.Retry<TransientException>(),
            DelayPolicy.Constant(TimeSpan.FromMilliseconds(50))).ConfigureAwait(false);

        Console.WriteLine(msg);
    }

    private Task<string> SendSometimesAsync(string msg)
    {
        /* ... */
    }
}
```

### Cancellation

All of the invocation methods support cancellation via a
[`CancellationToken`](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken?view=netstandard-2.0),
and will at the very least interrupt any delay specified by the `DelayHandler`. However,
some underlying delegates may natively support cancellation and allow their execution to be
interrupted. To leverage this native cancellation support the reliably class, reliable delegate
types, and extension methods support the use of a `CancellationToken` via constructor and
method overloads. Please note that the `Reliably` class will always rethrows a thrown
`OperationCanceledException` while the other APIs will check that the thrown exception's
token matches the provided one.

```csharp
using System.Threading.Tasks;
using Sweetener.Reliability;

namespace Sweetener.Example
{
    public static async Task Main(params string[] args)
    {
        using var source = new CancellationTokenSource();

        // Cancel the token after 1 second
        source.CancelAfter(TimeSpan.FromSeconds(1));

        // Create the reliable function
        var msg = await Reliably.InvokeAsync(
            t => SendSometimesAsync("Hello World", t),
            source.Token,
            4,
            ExceptionPolicy.Retry<TransientException>(),
            DelayPolicy.Constant(TimeSpan.FromMilliseconds(50))).ConfigureAwait(false);

        Console.WriteLine(msg);
    }

    private Task<string> SendSometimesAsync(string msg, CancellationToken token)
    {
        /* ... */
    }
}
```

### Execution Events

For users that are interested in the various stages of invoking a reliable delegate, there are
a number of events that are raised during its lifetime.

#### Retrying

The `Retrying` event is raised before the underlying delegate is invoked after a transient
result or exception *and* after the specified delay. The attempt, as well as the transient result
or exception, is passed as an argument to the event handler.

```csharp
using Sweetener.Reliability;

namespace Sweetener.Example
{
    public static void Main(params string[] args)
    {
        ReliableFunc<string, string> send = /* ... */;
        send.Retrying += (int i, string r, Exception e) =>
        {
            string reason = e == null
                ? $"response '{r}'"
                : $"exception with message '{e.Message}'";

            Console.WriteLine(
                "Starting attempt #{0} after transient {1}",
                i,
                reason);
        };

        Console.WriteLine(send.Invoke("Hello World"));
    }

    private string SendSometimes(string msg)
    {
        /* ... */
    }
}
```

#### RetriesExhausted

The `RetriesExhausted` event is raised after a transient result or exception when the
number of retries has exceeded the configured maximum. The last transient result or exception
is passed as an argument to the event handler.

```csharp
using Sweetener.Reliability;

namespace Sweetener.Example
{
    public static void Main(params string[] args)
    {
        ReliableFunc<string, string> send = /* ... */;
        send.RetriesExhausted += (string r, Exception e) =>
        {
            string reason = e == null
                ? $"response '{r}'"
                : $"exception with message '{e.Message}'";

            Console.WriteLine(
                "Retries exhausted after transient {0}",
                reason);
        };

        Console.WriteLine(send.Invoke("Hello World"));
    }

    private string SendSometimes(string msg)
    {
        /* ... */
    }
}
```

#### Failed

The `Failed` event is raised after encountering a fatal result or exception. The fatal result
or exception is then passed as an argument to the event handler.

```csharp
using Sweetener.Reliability;

namespace Sweetener.Example
{
    public static void Main(params string[] args)
    {
        ReliableFunc<string, string> send = /* ... */;
        send.Failed += (string response, Exception exception) =>
        {
            string reason = e == null
                ? $"response '{r}'"
                : $"exception with message '{e.Message}'";

            Console.WriteLine(
                "Send failed after fatal {0}",
                reason);
        };

        Console.WriteLine(send.Invoke("Hello World"));
    }

    private string SendSometimes(string msg)
    {
        /* ... */
    }
}
```
