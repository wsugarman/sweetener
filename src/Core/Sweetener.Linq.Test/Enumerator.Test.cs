// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Linq.Test;

[TestClass]
public class EnumeratorTest
{
    [TestMethod]
    public void Empty()
    {
        IEnumerator<int> enumerator = Enumerator.Empty<int>();

        // Assert the behavior for this enumerator
        Assert.ThrowsException<NotSupportedException>(() => enumerator.Current);
        Assert.ThrowsException<NotSupportedException>(() => ((IEnumerator)enumerator).Current);

        enumerator.Dispose(); // No errors below

        Assert.IsFalse(enumerator.MoveNext());
        Assert.ThrowsException<NotSupportedException>(() => enumerator.Current); // No exception change

        enumerator.Reset();

        Assert.ThrowsException<NotSupportedException>(() => enumerator.Current);
    }
}
