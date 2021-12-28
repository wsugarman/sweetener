// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;

namespace Sweetener.Extensions.Configuration;

/// <summary>
/// Specifies that the attributed class represents a section in a configuration. This class cannot be inherited.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class ConfigurationSectionAttribute : Attribute
{
    /// <summary>
    /// Gets the key for this section.
    /// </summary>
    /// <remarks>
    /// The key may or may not be found at the root of a configuration.
    /// </remarks>
    /// <value>The key for the attributed section's settings.</value>
    public string Key { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigurationSectionAttribute"/>
    /// class with the specified <paramref name="key"/>.
    /// </summary>
    /// <param name="key">The key for this section</param>
    public ConfigurationSectionAttribute(string key)
        => Key = key ?? throw new ArgumentNullException(nameof(key));
}
