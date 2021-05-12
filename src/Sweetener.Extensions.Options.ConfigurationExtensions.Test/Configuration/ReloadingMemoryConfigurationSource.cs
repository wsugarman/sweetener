// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Sweetener.Extensions.Configuration.Test
{
    internal class ReloadingMemoryConfigurationSource : IConfigurationSource
    {
        public IEnumerable<KeyValuePair<string, string>>? InitialData { get; set; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
            => new ReloadingMemoryConfigurationProvider(this);
    }
}
