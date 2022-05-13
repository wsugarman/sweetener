// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Sweetener.Extensions.Configuration.Memory;

// Note that the Microsoft.Extensions.Configuration* libraries do not include
// nullable reference types for their .NET Standard 2.0 targets.
#nullable disable

/// <summary>
/// Represents an implementation of <see cref="IConfiguration"/> that stores all of its data in-memory.
/// </summary>
/// <remarks>
/// Unlike other configurations which may have multiple providers, the <see cref="MemoryConfiguration"/> stores
/// all of its data within a single in-memory provider.
/// </remarks>
[SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Name derived from MemoryConfigurationProvider and MemoryConfigurationSource.")]
public sealed class MemoryConfiguration : IConfiguration, IEnumerable<KeyValuePair<string, string>>
{
    // TODO: Should this class implement IConfigurationRoot?

    /// <inheritdoc cref="IConfiguration.this[string]"/>
    public string this[string key]
    {
        get => _provider.TryGet(key, out string value) ? value : null;
        set => _provider.Set(key, value);
    }

    private readonly ConfigurationProvider _provider;

    /// <summary>
    /// Initializes a new instance of the <see cref="MemoryConfiguration"/> class that is empty.
    /// </summary>
    public MemoryConfiguration()
        : this(new ConfigurationProvider())
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="MemoryConfiguration"/> class that
    /// contains elements copied from the specified argument.
    /// </summary>
    /// <param name="initialData">Elements to be initially present within the configuration.</param>
    /// <exception cref="ArgumentNullException"><paramref name="initialData"/> is <see langword="null"/>.</exception>
    public MemoryConfiguration(IEnumerable<KeyValuePair<string, string>> initialData)
        : this(new ConfigurationProvider(initialData))
    { }

    private MemoryConfiguration(ConfigurationProvider provider)
        => _provider = provider;

    // Note: Add(string, string) and IEnumerable<KeyValuePair<string, string>> are only implemented
    //       to allow users to conveniently leverage collection initializers.

    /// <summary>
    /// Adds the specified <paramref name="key"/> and <paramref name="value"/> to the configuration.
    /// </summary>
    /// <remarks>
    /// If the <paramref name="key"/> already exists in the configuration,
    /// its value is overwritten with the given <paramref name="value"/>.
    /// </remarks>
    /// <param name="key">The key of the element to add or update.</param>
    /// <param name="value">The value of the element to add. May be <see langword="null"/>.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Add(string key, string value)
        => _provider.Set(key, value);

    // TOOD: Should Remove(string) be implemented?

    /// <inheritdoc cref="IConfiguration.GetChildren"/>
    public IEnumerable<IConfigurationSection> GetChildren()
        => GetChildren(path: null);

    /// <summary>
    /// Returns an <see cref="IChangeToken"/> that can be used to observe when this configuration is reloaded.
    /// </summary>
    /// <remarks>
    /// Instances of the <see cref="MemoryConfiguration"/> cannot be reloaded and therefore the tokens will never
    /// invoke any registered callback delegates.
    /// </remarks>
    /// <returns>An unused <see cref="IChangeToken"/>.</returns>
    public IChangeToken GetReloadToken()
        => _provider.GetReloadToken();

    /// <inheritdoc cref="IConfiguration.GetSection(string)"/>
    public IConfigurationSection GetSection(string key)
        => new ConfigurationSection(this, key);

    // TODO: Should this expose IConfigurationSection instead?
    IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
        => ConfigurationExtensions.AsEnumerable(this).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => ConfigurationExtensions.AsEnumerable(this).GetEnumerator();

    private IEnumerable<IConfigurationSection> GetChildren(string path)
    {
        IEnumerable<string> childKeys = _provider
            .GetChildKeys(Enumerable.Empty<string>(), path)
            .Distinct(StringComparer.OrdinalIgnoreCase);

        return path is null
            ? childKeys.Select(key => GetSection(key))
            : childKeys.Select(key => GetSection(ConfigurationPath.Combine(path, key)));
    }

    private sealed class ConfigurationSection : IConfigurationSection
    {
        public string this[string key]
        {
            get => _root[ConfigurationPath.Combine(Path, key)];
            set => _root[ConfigurationPath.Combine(Path, key)] = value;
        }

        // Note: This is computed lazily like in Microsoft.Extensions.Configuration.ConfigurationSection
        public string Key => _key is null ? _key = ConfigurationPath.GetSectionKey(Path) : _key;

        public string Path { get; }

        public string Value
        {
            get => _root[Path];
            set => _root[Path] = value;
        }

        private readonly MemoryConfiguration _root;
        private string _key;

        public ConfigurationSection(MemoryConfiguration root, string path)
        {
            _root = root ?? throw new ArgumentNullException(nameof(root));
            Path  = path ?? throw new ArgumentNullException(nameof(path));
        }

        public IEnumerable<IConfigurationSection> GetChildren()
            => _root.GetChildren(Path);

        public IChangeToken GetReloadToken()
            => _root.GetReloadToken();

        public IConfigurationSection GetSection(string key)
            => _root.GetSection(ConfigurationPath.Combine(Path, key));
    }

    private sealed class ConfigurationProvider : Microsoft.Extensions.Configuration.ConfigurationProvider
    {
        public ConfigurationProvider()
        { }

        public ConfigurationProvider(IEnumerable<KeyValuePair<string, string>> initialData)
        {
            foreach (KeyValuePair<string, string> pair in initialData ?? throw new ArgumentNullException(nameof(initialData)))
                Data.Add(pair.Key, pair.Value);
        }
    }
}
