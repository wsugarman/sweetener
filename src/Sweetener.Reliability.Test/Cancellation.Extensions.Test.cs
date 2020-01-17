using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    [TestClass]
    public class CancellationExtensionTest
    {
        [TestMethod]
        public void IsCancellation()
        {
            using CancellationTokenSource source1        = new CancellationTokenSource();
            using CancellationTokenSource source2        = new CancellationTokenSource();
            using CancellationTokenSource canceledSource = new CancellationTokenSource();

            canceledSource.Cancel();

            // Invalid Exception
            Assert.IsFalse(new FormatException().IsCancellation(CancellationToken.None));
            Assert.IsFalse(new FormatException().IsCancellation(source2       .Token  ));
            Assert.IsFalse(new FormatException().IsCancellation(canceledSource.Token  ));

            // Mismatched Tokens
            Assert.IsFalse(new OperationCanceledException(source1.Token).IsCancellation(CancellationToken.None));
            Assert.IsFalse(new OperationCanceledException(source1.Token).IsCancellation(source2       .Token  ));
            Assert.IsFalse(new OperationCanceledException(source1.Token).IsCancellation(canceledSource.Token  ));

            // Active Token
            Assert.IsFalse(new OperationCanceledException(CancellationToken.None).IsCancellation(CancellationToken.None));
            Assert.IsFalse(new OperationCanceledException(source1.Token         ).IsCancellation(source1.Token         ));

            // Exception from Canceled Task
            Assert.IsTrue (new OperationCanceledException(canceledSource.Token).IsCancellation(canceledSource.Token));
        }
    }
}
