// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Sweetener.Extensions.Options.Test.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

internal static class ReloadingMemoryConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddInMemoryCollection(this IConfigurationBuilder configurationBuilder, IEnumerable<KeyValuePair<string, string>> initialData, bool reloadOnChange)
    {
        if (configurationBuilder is null)
            throw new ArgumentNullException(nameof(configurationBuilder));

        return reloadOnChange
            ? configurationBuilder.Add(new ReloadingMemoryConfigurationSource { InitialData = initialData })
            : configurationBuilder.Add(new MemoryConfigurationSource { InitialData = initialData });
    }
}
