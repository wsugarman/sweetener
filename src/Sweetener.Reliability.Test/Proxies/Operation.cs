using System;

namespace Sweetener.Reliability.Test
{
    internal static class Operation
    {
        public static readonly Action Null = NoOp;

        private static void NoOp()
        { }
    }
}
