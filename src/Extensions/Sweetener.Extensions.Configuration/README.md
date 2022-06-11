# Sweetener.Extensions.Configuration
[![GitHub License](https://img.shields.io/github/license/wsugarman/sweetener?label=License)](https://github.com/wsugarman/sweetener/blob/main/LICENSE)
[![Sweetener.Extensions.Configuration Build](https://github.com/wsugarman/sweetener/actions/workflows/sweetener.extensions.configuration-ci.yml/badge.svg)](https://github.com/wsugarman/sweetener/actions/workflows/sweetener.extensions.configuration-ci.yml)
[![Sweetener.Extensions.Configuration Code Coverage](https://codecov.io/gh/wsugarman/sweetener/branch/main/graph/badge.svg?flag=Configuration)](https://codecov.io/gh/wsugarman/sweetener)

The Sweetener.Extensions.Configuration library contains a set of APIs that extends the `Microsoft.Extensions.Configuration`
package.

## MemoryConfiguration
The `MemoryConfiguration` class provides an easy lighter weight `IConfiguration` implementation over those built
using the `MemoryConfigurationBuilderExtensions.AddInMemoryCollection(IConfigurationBuilder)` extension method.

```csharp
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Sweetener.Extensions.Configuration;

IConfiguration config = new MemoryConfiguration(
    new Dictionary<string, string?>
    {
        { "Key"         , "Value1" },
        { "Section1:Key", "Value2" },
        { "Section2:Key", "Value3" },
    });

string? value2 = config["Section1:Key"];
string? value3 = config.GetSection("Section2")["Key"];
```
