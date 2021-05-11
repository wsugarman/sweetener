// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace Sweetener.Extensions.Configuration
{
    /// <summary>
    /// <see langword="static"/> helper class that allows the binding of strongly typed objects annotated with
    /// <see cref="ConfigurationSectionAttribute"/> to configuration values.
    /// </summary>
    public static class ConfigurationSectionBinder
    {
        /// <summary>
        /// Attempts to bind the given object instance, whose type is annotated with
        /// <see cref="ConfigurationSectionAttribute"/>, to configuration values by matching property names
        /// against configuration keys recursively.
        /// </summary>
        /// <param name="configuration">The configuration instance to bind.</param>
        /// <param name="instance">The object to bind.</param>
        /// <exception cref="ArgumentException">
        /// The type of <paramref name="instance"/> is not annotated with <see cref="ConfigurationSectionAttribute"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="configuration"/> or <paramref name="instance"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// One or more properties defined for <paramref name="instance"/> cannot be bound against the configuration.
        /// </exception>
        public static void BindSection(this IConfiguration configuration, object instance)
            => configuration.BindSection(instance, configureOptions: null);

        /// <summary>
        /// Attempts to bind the given object instance, whose type is annotated with
        /// <see cref="ConfigurationSectionAttribute"/>, to configuration values by matching property names
        /// against configuration keys recursively.
        /// </summary>
        /// <param name="configuration">The configuration instance to bind.</param>
        /// <param name="instance">The object to bind.</param>
        /// <param name="configureOptions">Configures the binder options.</param>
        /// <exception cref="ArgumentException">
        /// The type of <paramref name="instance"/> is not annotated with <see cref="ConfigurationSectionAttribute"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="configuration"/> or <paramref name="instance"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// One or more properties defined for <paramref name="instance"/> cannot be bound against the configuration.
        /// </exception>
        public static void BindSection(this IConfiguration configuration, object instance, Action<BinderOptions>? configureOptions)
        {
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));

            if (instance is null)
                throw new ArgumentNullException(nameof(instance));

            if (!TryFindConfigurationSectionAttribute(instance.GetType(), out ConfigurationSectionAttribute? attribute))
                throw new ArgumentException(SR.Format(SR.InvalidConfigurationSectionFormat, instance.GetType()), nameof(instance));

            configuration.GetSection(attribute.Path).Bind(instance, configureOptions);
        }

        /// <summary>
        /// Attempts to bind the configuration instance to a new instance of the given type <paramref name="type"/>
        /// which is annotated with <see cref="ConfigurationSectionAttribute"/>. If this configuration section
        /// has a value, that will be used. Otherwise binding by matching property names against
        /// configuration keys recursively.
        /// </summary>
        /// <param name="configuration">The configuration instance to bind.</param>
        /// <param name="type">The type of the new instance to bind.</param>
        /// <returns>The new instance if successful, <see langword="null"/> otherwise.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="type"/> is not annotated with <see cref="ConfigurationSectionAttribute"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="configuration"/> or <paramref name="type"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// One or more properties for type <paramref name="type"/> cannot be bound against the configuration.
        /// </exception>
        public static object GetSection(this IConfiguration configuration, Type type)
            => configuration.GetSection(type, configureOptions: null);

        /// <summary>
        /// Attempts to bind the configuration instance to a new instance of the given type <paramref name="type"/>
        /// which is annotated with <see cref="ConfigurationSectionAttribute"/>. If this configuration section
        /// has a value, that will be used. Otherwise binding by matching property names against
        /// configuration keys recursively.
        /// </summary>
        /// <param name="configuration">The configuration instance to bind.</param>
        /// <param name="type">The type of the new instance to bind.</param>
        /// <param name="configureOptions">Configures the binder options.</param>
        /// <returns>The new instance if successful, <see langword="null"/> otherwise.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="type"/> is not annotated with <see cref="ConfigurationSectionAttribute"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="configuration"/> or <paramref name="type"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// One or more properties for type <paramref name="type"/> cannot be bound against the configuration.
        /// </exception>
        public static object GetSection(this IConfiguration configuration, Type type, Action<BinderOptions>? configureOptions)
        {
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));

            if (type is null)
                throw new ArgumentNullException(nameof(type));

            if (!TryFindConfigurationSectionAttribute(type, out ConfigurationSectionAttribute? attribute))
                throw new ArgumentException(SR.Format(SR.InvalidConfigurationSectionFormat, type), nameof(type));

            return configuration.GetSection(attribute.Path).Get(type, configureOptions);
        }

        /// <summary>
        /// Attempts to bind the configuration instance to a new instance of the given type <typeparamref name="T"/>
        /// which is annotated with <see cref="ConfigurationSectionAttribute"/>. If this configuration section
        /// has a value, that will be used. Otherwise binding by matching property names against
        /// configuration keys recursively.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the new instance to bind which is annotated with <see cref="ConfigurationSectionAttribute"/>.
        /// </typeparam>
        /// <param name="configuration">The configuration instance to bind.</param>
        /// <returns>The new instance of type <typeparamref name="T"/> if successful, otherwise the default value.</returns>
        /// <exception cref="ArgumentException">
        /// Type <typeparamref name="T"/> is not annotated with <see cref="ConfigurationSectionAttribute"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="configuration"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// One or more properties for type <typeparamref name="T"/> cannot be bound against the configuration.
        /// </exception>
        public static T GetSection<T>(this IConfiguration configuration)
            => configuration.GetSection<T>(configureOptions: null);

        /// <summary>
        /// Attempts to bind the configuration instance to a new instance of the given type <typeparamref name="T"/>
        /// which is annotated with <see cref="ConfigurationSectionAttribute"/>. If this configuration section
        /// has a value, that will be used. Otherwise binding by matching property names against
        /// configuration keys recursively.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the new instance to bind which is annotated with <see cref="ConfigurationSectionAttribute"/>.
        /// </typeparam>
        /// <param name="configuration">The configuration instance to bind.</param>
        /// <param name="configureOptions">Configures the binder options.</param>
        /// <returns>The new instance of type <typeparamref name="T"/> if successful, otherwise the default value.</returns>
        /// <exception cref="ArgumentException">
        /// Type <typeparamref name="T"/> is not annotated with <see cref="ConfigurationSectionAttribute"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="configuration"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// One or more properties for type <typeparamref name="T"/> cannot be bound against the configuration.
        /// </exception>
        public static T GetSection<T>(this IConfiguration configuration, Action<BinderOptions>? configureOptions)
        {
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));

            if (!TryFindConfigurationSectionAttribute(typeof(T), out ConfigurationSectionAttribute? attribute))
                throw new ArgumentException(SR.Format(SR.InvalidConfigurationSectionFormat, typeof(T)));

            return configuration.GetSection(attribute.Path).Get<T>(configureOptions);
        }

        private static bool TryFindConfigurationSectionAttribute(Type type, [NotNullWhen(true)] out ConfigurationSectionAttribute? attribute)
        {
            attribute = Attribute.GetCustomAttribute(type, typeof(ConfigurationSectionAttribute)) as ConfigurationSectionAttribute;
            return attribute != null;
        }
    }
}
