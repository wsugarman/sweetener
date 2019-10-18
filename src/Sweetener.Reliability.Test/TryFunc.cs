namespace Sweetener.Reliability.Test
{
    public delegate bool TryFunc<in T, TResult>(T arg, out TResult result);
}
