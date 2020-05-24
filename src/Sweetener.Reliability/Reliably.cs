namespace Sweetener.Reliability
{
    /// <summary>
    /// A collection of methods to reliablty invoke user-defined delegates.
    /// </summary>
    public static partial class Reliably
    {
        // Implemenations of Invoke, InvokeAsync, TryInvoke, and TryInvokeAsync are spread across
        // multiple partial classes for both synchronous and asynchronous delegates
        // See the following source files for details:
        // - Action/Reliably.cs
        // - Action/Reliably.Async.cs
        // - Func/Reliably.cs
        // - Func/Reliably.Async.cs
    }
}
