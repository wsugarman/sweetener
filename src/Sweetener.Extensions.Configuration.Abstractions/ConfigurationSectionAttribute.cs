// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;

namespace Sweetener.Extensions.Configuration
{
    /// <summary>
    /// Specifies that the attributed class represents a section in a configuration. This class cannot be inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class ConfigurationSectionAttribute : Attribute
    {
        /// <summary>
        /// Gets the path to this section.
        /// </summary>
        /// <remarks>
        /// By default, the path should assume to be from the root of a configuration
        /// unless configured otherwise when defining the service container.
        /// </remarks>
        /// <value>The path to the attributed section's settings.</value>
        public string Path { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationSectionAttribute"/>
        /// class with the specified <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The path to this section</param>
        public ConfigurationSectionAttribute(string path)
            => Path = path ?? throw new ArgumentNullException(nameof(path));
    }
}
