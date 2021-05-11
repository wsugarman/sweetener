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
        /// Registers a configuration instance that <typeparamref name="TSection"/> will bind against,
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
        /// <para><paramref name="services"/> or <paramref name="config"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>TBD.</para>
        /// </exception>
        public static IServiceCollection ConfigureSection<TSection>(this IServiceCollection services, IConfiguration config)
            where TSection : class
            => services.ConfigureSection<TSection>(config, null);

        public static IServiceCollection ConfigureSection<TOptions>(this IServiceCollection services, IConfiguration config, Action<BinderOptions>? configureBinder) where TOptions : class
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            if (config is null)
                throw new ArgumentNullException(nameof(config));

            if (!TryFindConfigurationSectionAttribute(typeof(TOptions), out ConfigurationSectionAttribute? attribute))
                throw new ArgumentException(SR.Format(SR.InvalidConfigurationSectionFormat, typeof(TOptions)));

            return services.Configure<TOptions>(config.GetSection(attribute.Path), configureBinder);
        }

        public static IServiceCollection ConfigureSections(this IServiceCollection services, IConfiguration config)
            => services.ConfigureSections(config, null);

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
                // Update section
                arguments[1] = config.GetSection(attribute.Path);

                // Dynamically invoke the configuration method
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
