// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sweetener.Extensions.Configuration;
using Sweetener.Extensions.Options;

namespace Sweetener.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for adding configuration-related options services to the DI container using types
    /// that have been annotated with <see cref="ConfigurationSectionAttribute"/>.
    /// </summary>
    public static class OptionsConfigurationSectionServiceCollectionExtensions
    {
        /// <summary>
        /// Registers a configuration instance that <typeparamref name="TSection"/>, a type that is annotated
        /// with <see cref="ConfigurationSectionAttribute"/>, will bind against automatically using the appropriate key,
        /// and updates the section when the configuration changes.
        /// </summary>
        /// <typeparam name="TSection">
        /// The type of section being configured. The type must be annotated with
        /// <see cref="ConfigurationSectionAttribute"/>.
        /// </typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="config">The configuration being bound that contains the section.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        /// <exception cref="ArgumentException">
        /// <typeparamref name="TSection"/> is not annotated with <see cref="ConfigurationSectionAttribute"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="services"/> or <paramref name="config"/> is <see langword="null"/>.
        /// </exception>
        public static IServiceCollection ConfigureSection<TSection>(this IServiceCollection services, IConfiguration config)
            where TSection : class
            => services.ConfigureSection<TSection>(config, null);

        /// <summary>
        /// Registers a configuration instance that <typeparamref name="TSection"/>, a type that is annotated
        /// with <see cref="ConfigurationSectionAttribute"/>, will bind against automatically using the appropriate key,
        /// and updates the section when the configuration changes.
        /// </summary>
        /// <typeparam name="TSection">
        /// The type of section being configured. The type must be annotated with
        /// <see cref="ConfigurationSectionAttribute"/>.
        /// </typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="config">The configuration being bound that contains the section.</param>
        /// <param name="configureBinder">Used to configure the <see cref="BinderOptions"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        /// <exception cref="ArgumentException">
        /// <typeparamref name="TSection"/> is not annotated with <see cref="ConfigurationSectionAttribute"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="services"/> or <paramref name="config"/> is <see langword="null"/>.
        /// </exception>
        public static IServiceCollection ConfigureSection<TSection>(this IServiceCollection services, IConfiguration config, Action<BinderOptions>? configureBinder)
            where TSection : class
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            if (config is null)
                throw new ArgumentNullException(nameof(config));

            if (!TryFindConfigurationSectionAttribute(typeof(TSection), out ConfigurationSectionAttribute? attribute))
                throw new ArgumentException(SR.Format(SR.InvalidConfigurationSectionFormat, typeof(TSection)));

            return services.Configure<TSection>(config.GetSection(attribute.Key), configureBinder);
        }

        /// <summary>
        /// Registers a configuration instance for each of the types that are annotated with
        /// <see cref="ConfigurationSectionAttribute"/> found in <see cref="AppDomain.CurrentDomain"/>
        /// and will bind against these types automatically using the appropriate key. When the
        /// configuration changes, so too will the associated configuration sections.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="config">The configuration being bound that contains the section.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="services"/> or <paramref name="config"/> is <see langword="null"/>.
        /// </exception>
        public static IServiceCollection ConfigureSections(this IServiceCollection services, IConfiguration config)
            => services.ConfigureSections(config, null);

        /// <summary>
        /// Registers a configuration instance for each of the types that are annotated with
        /// <see cref="ConfigurationSectionAttribute"/> found in <see cref="AppDomain.CurrentDomain"/>
        /// and will bind against these types automatically using the appropriate key. When the
        /// configuration changes, so too will the associated configuration sections.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="config">The configuration being bound that contains the section.</param>
        /// <param name="configureBinder">Used to configure the <see cref="BinderOptions"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="services"/> or <paramref name="config"/> is <see langword="null"/>.
        /// </exception>
        public static IServiceCollection ConfigureSections(this IServiceCollection services, IConfiguration config, Action<BinderOptions>? configureBinder)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            if (config is null)
                throw new ArgumentNullException(nameof(config));

            MethodInfo configureMethod = typeof(OptionsConfigurationServiceCollectionExtensions)
                .GetMethod(
                    nameof(OptionsConfigurationServiceCollectionExtensions.Configure),
                    new Type[3]
                    {
                        typeof(IServiceCollection),
                        typeof(IConfiguration),
                        typeof(Action<BinderOptions>),
                    });

            object?[] arguments = new object?[3] { services, config, configureBinder };
            foreach ((Type type, ConfigurationSectionAttribute attribute) in EnumerateConfigurationSections())
            {
                // Update section and check its existence is necessary
                // TODO: Allow callers to include missing sections
                IConfigurationSection section = config.GetSection(attribute.Key);
                if (!section.Exists())
                    continue;

                // Dynamically invoke the configuration method
                arguments[1] = section;
                configureMethod
                    .MakeGenericMethod(type)
                    .Invoke(
                        null,
                        BindingFlags.Public | BindingFlags.Static,
                        null,
                        arguments,
                        CultureInfo.InvariantCulture); // Unused type coercion
            }

            return services;
        }

        private static IEnumerable<(Type, ConfigurationSectionAttribute)> EnumerateConfigurationSections()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type t in assembly.GetTypes())
                {
                    if (TryFindConfigurationSectionAttribute(t, out ConfigurationSectionAttribute? attribute))
                        yield return (t, attribute);
                }
            }
        }

        private static bool TryFindConfigurationSectionAttribute(Type type, [NotNullWhen(true)] out ConfigurationSectionAttribute? attribute)
        {
            attribute = Attribute.GetCustomAttribute(type, typeof(ConfigurationSectionAttribute), inherit: false) as ConfigurationSectionAttribute;
            return attribute != null;
        }
    }
}
