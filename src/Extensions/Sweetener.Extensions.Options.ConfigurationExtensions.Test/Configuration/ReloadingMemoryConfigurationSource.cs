// Copyright © William Sugarman.
// Licensed under the MIT License.

using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Sweetener.Extensions.Options.Test.Configuration;

internal class ReloadingMemoryConfigurationSource : IConfigurationSource
{
    public IEnumerable<KeyValuePair<string, string>>? InitialData { get; set; }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
        => new ReloadingMemoryConfigurationProvider(this);
}
