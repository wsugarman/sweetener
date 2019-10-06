using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    [TestClass]
    public partial class DelayTest
    {
        private static readonly TimeSpan s_value          = TimeSpan.FromMilliseconds(100).Add(TimeSpan.FromTicks(9999));
        private static readonly TimeSpan s_truncatedValue = TimeSpan.FromMilliseconds((int)s_value.TotalMilliseconds);
    }
}
