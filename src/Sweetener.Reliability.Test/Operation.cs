using System;
using System.Threading.Tasks;

namespace Sweetener.Reliability.Test
{
    internal static class Operation
    {
        public static readonly Action Null = NoOp;

        public static readonly AsyncAction NullAsync = NoOpAsync;

        private static void NoOp()
        { }

        private static async Task NoOpAsync()
            => await Task.CompletedTask;
    }
}
