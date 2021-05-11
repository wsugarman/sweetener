// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Threading.Tasks.Test
{
    [TestClass]
    public class TaskExtensionsTest
    {
        [TestMethod]
        public async Task WithResultOnSuccess()
        {
            // Bad Input
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => TaskExtensions.WithResultOnSuccess(null!, s => 42, "State")).ConfigureAwait(false);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => Task.CompletedTask.WithResultOnSuccess<string, int>(null!, "State")).ConfigureAwait(false);

            // Success
            string actual = await Task.CompletedTask.WithResultOnSuccess(s => s, "Success!").ConfigureAwait(false);
            Assert.AreEqual("Success!", actual);

            // Canceled
            using CancellationTokenSource source = new CancellationTokenSource();
            source.Cancel();

            await Assert.ThrowsExceptionAsync<TaskCanceledException>(async () =>
            {
                try
                {
                    await Task.FromCanceled(source.Token)
                        .WithResultOnSuccess(s => s, "Hello World")
                        .ConfigureAwait(false);
                }
                catch (TaskCanceledException oce)
                {
                    Assert.AreEqual(source.Token, oce.CancellationToken);
                    throw;
                }
            }).ConfigureAwait(false);

            // Failure
            await Assert.ThrowsExceptionAsync<IOException>(async () =>
            {
                Task t = Task
                    .WhenAll(
                        Task.FromException(new IOException()),
                        Task.FromResult(5),
                        Task.FromException(new NotSupportedException()))
                    .WithResultOnSuccess(s => s, "Hello World");

                try
                {
                    await t.ConfigureAwait(false);
                }
                catch (IOException)
                {
                    ReadOnlyCollection<Exception>? exceptions = t.Exception?.InnerExceptions;
                    Assert.IsNotNull(exceptions);

                    Assert.AreEqual(2, exceptions.Count);
                    Assert.IsInstanceOfType(exceptions[0], typeof(IOException));
                    Assert.IsInstanceOfType(exceptions[1], typeof(NotSupportedException));
                    throw;
                }
            }).ConfigureAwait(false);
        }
    }
}
