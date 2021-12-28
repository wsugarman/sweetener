// Copyright © William Sugarman.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration.Memory;

namespace Sweetener.Extensions.Options.Test.Configuration;

[SuppressMessage("Naming", "CA1710: Identifiers should have correct suffix", Justification = "This type is not a collection.")]
internal class ReloadingMemoryConfigurationProvider : MemoryConfigurationProvider
{
    public ReloadingMemoryConfigurationProvider(ReloadingMemoryConfigurationSource source)
        : base(new MemoryConfigurationSource { InitialData = source.InitialData })
    { }

    public override void Set(string key, string value)
    {
        base.Set(key, value);
        OnReload();
    }
}
