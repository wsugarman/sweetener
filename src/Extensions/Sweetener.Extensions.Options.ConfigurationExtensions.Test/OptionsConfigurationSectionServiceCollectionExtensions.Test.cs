// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweetener.Extensions.Configuration;
using Sweetener.Extensions.Configuration.Test;

namespace Sweetener.Extensions.DependencyInjection.Test
{
    [TestClass]
    public class OptionsConfigurationSectionServiceCollectionExtensionsTest
    {
        [TestMethod]
        public void ConfigureSection_Config()
        {
            IConfiguration config = CreateConfiguration();
            ServiceCollection services = new ServiceCollection();

            // Missing Attribute
            Assert.ThrowsException<ArgumentException>(() => services.ConfigureSection<InvalidSettings>(config));

            // Null Arguments
            Assert.ThrowsException<ArgumentNullException>(() => OptionsConfigurationSectionServiceCollectionExtensions.ConfigureSection<Features>(services: null!, config));
            Assert.ThrowsException<ArgumentNullException>(() => OptionsConfigurationSectionServiceCollectionExtensions.ConfigureSection<Features>(services       , config: null!));

            // Success
            IOptionsMonitor<Features> featuresMonitor = services
                .ConfigureSection<Features>(config)
                .BuildServiceProvider()
                .GetRequiredService<IOptionsMonitor<Features>>();

            AssertExpectedFeatures(featuresMonitor.CurrentValue);

            config["Features:Enabled:C"] = "true";
            Assert.IsTrue(featuresMonitor.CurrentValue.Enabled!["C"]);
        }

        [TestMethod]
        public void ConfigureSection_Config_ConfigureOptions()
        {
            IConfiguration config = CreateConfiguration();
            ServiceCollection services = new ServiceCollection();

            // Missing Attribute
            Assert.ThrowsException<ArgumentException>(() => services.ConfigureSection<InvalidSettings>(config, configureBinder: null));

            // Null Arguments
            Assert.ThrowsException<ArgumentNullException>(() => OptionsConfigurationSectionServiceCollectionExtensions.ConfigureSection<Features>(services: null!, config: config, configureBinder: null));
            Assert.ThrowsException<ArgumentNullException>(() => OptionsConfigurationSectionServiceCollectionExtensions.ConfigureSection<Features>(services       , config: null! , configureBinder: null));

            // Success
            int configureCalls = 0;
            Action<BinderOptions> configureAction = o => { Assert.IsNotNull(o); configureCalls++; };
            IOptionsMonitor<Features> featuresMonitor = services
                .ConfigureSection<Features>(config, configureAction)
                .BuildServiceProvider()
                .GetRequiredService<IOptionsMonitor<Features>>();

            Assert.AreEqual(0, configureCalls);
            AssertExpectedFeatures(featuresMonitor.CurrentValue);
            Assert.AreEqual(1, configureCalls);

            config["Features:Enabled:C"] = "true";
            Assert.AreEqual(2, configureCalls);
            Assert.IsTrue(featuresMonitor.CurrentValue.Enabled!["C"]);
        }

        [TestMethod]
        public void ConfigureSections_Config()
        {
            IConfiguration config = CreateConfiguration(includeWeb: true);
            ServiceCollection services = new ServiceCollection();

            // Null Arguments
            Assert.ThrowsException<ArgumentNullException>(() => OptionsConfigurationSectionServiceCollectionExtensions.ConfigureSections(services: null!, config));
            Assert.ThrowsException<ArgumentNullException>(() => OptionsConfigurationSectionServiceCollectionExtensions.ConfigureSections(services       , config: null!));

            // Success
            ServiceProvider provider = services
                .ConfigureSections(config)
                .BuildServiceProvider();

            IOptionsMonitor<Features>  featuresMonitor  = provider.GetRequiredService<IOptionsMonitor<Features>>();
            IOptionsMonitor<WebServer> webServerMonitor = provider.GetRequiredService<IOptionsMonitor<WebServer>>();

            AssertExpectedFeatures (featuresMonitor .CurrentValue);
            AssertExpectedWebServer(webServerMonitor.CurrentValue);

            config["Features:Enabled:C"] = "true";
            Assert.IsTrue(featuresMonitor.CurrentValue.Enabled!["C"]);

            config["WebServer:Port"] = "8081";
            Assert.AreEqual(8081, webServerMonitor.CurrentValue.Port);
        }

        [TestMethod]
        public void ConfigureSections_Config_ConfigureOptions()
        {
            IConfiguration config = CreateConfiguration(includeWeb: true);
            ServiceCollection services = new ServiceCollection();

            // Null Arguments
            Assert.ThrowsException<ArgumentNullException>(() => OptionsConfigurationSectionServiceCollectionExtensions.ConfigureSections(services: null!, config: config, configureBinder: null));
            Assert.ThrowsException<ArgumentNullException>(() => OptionsConfigurationSectionServiceCollectionExtensions.ConfigureSections(services       , config: null! , configureBinder: null));

            // Success
            int configureCalls = 0;
            Action<BinderOptions> configureAction = o => { Assert.IsNotNull(o); configureCalls++; };
            ServiceProvider provider = services
                .ConfigureSections(config, configureAction)
                .BuildServiceProvider();

            IOptionsMonitor<Features>  featuresMonitor  = provider.GetRequiredService<IOptionsMonitor<Features>>();
            IOptionsMonitor<WebServer> webServerMonitor = provider.GetRequiredService<IOptionsMonitor<WebServer>>();

            // Check Features
            Assert.AreEqual(0, configureCalls);

            AssertExpectedFeatures(featuresMonitor.CurrentValue);
            Assert.AreEqual(1, configureCalls);

            config["Features:Enabled:C"] = "true";
            Assert.AreEqual(3, configureCalls); // reloads
            Assert.IsTrue(featuresMonitor.CurrentValue.Enabled!["C"]);

            // Check WebServer
            AssertExpectedWebServer(webServerMonitor.CurrentValue);
            Assert.AreEqual(3, configureCalls);

            config["WebServer:Port"] = "8081";
            Assert.AreEqual(5, configureCalls);
            Assert.AreEqual(8081, webServerMonitor.CurrentValue.Port);

            // Check Missing Section "Unused"
            // (Never registered, so this change should be ignored)
            IOptions<Unused> unused = provider.GetRequiredService<IOptions<Unused>>();
            config["Unused:Number"] = "42";
            Assert.AreEqual(7, configureCalls);
            Assert.AreEqual(0, unused.Value.Number);
        }

        private static IConfiguration CreateConfiguration(bool includeWeb = false)
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "Features:Enabled:A"       , "true"  },
                { "Features:Enabled:B"       , "false" },
                { "Features:Enabled:C"       , "false" },
                { "Features:Enabled:D"       , "false" },
                { "Features:FlightPercentage", "0.15"  },
                { "Features:Segment:Market"  , "en-US" },
            };

            if (includeWeb)
            {
                values["WebServer:Scheme"] = "http";
                values["WebServer:Host"  ] = "localhost";
                values["WebServer:Port"  ] = "8080";
            }

            return  new ConfigurationBuilder()
                .AddInMemoryCollection(values, reloadOnChange: true)
                .Build();
        }

        private static void AssertExpectedFeatures(Features actual)
        {
            Assert.IsNotNull(actual);

            Assert.IsNotNull(actual.Enabled);
            Assert.AreEqual(4    , actual.Enabled.Count);
            Assert.AreEqual(true , actual.Enabled["A"]);
            Assert.AreEqual(false, actual.Enabled["B"]);
            Assert.AreEqual(false, actual.Enabled["C"]);
            Assert.AreEqual(false, actual.Enabled["D"]);

            Assert.AreEqual(0.15d, actual.FlightPercentage);

            Assert.IsNotNull(actual.Segment);
            Assert.AreEqual("en-US", actual.Segment.Market);
        }

        private static void AssertExpectedWebServer(WebServer actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual("http"     , actual.Scheme);
            Assert.AreEqual("localhost", actual.Host  );
            Assert.AreEqual(8080       , actual.Port  );
        }

        #region Configuration Section Data Models

        [ConfigurationSection(nameof(Features))]
        [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Type used in tests.")]
        private sealed class Features
        {
            public IReadOnlyDictionary<string, bool>? Enabled { get; set; }

            public double FlightPercentage { get; set; }

            public UserSegment? Segment { get; set; }
        }

        [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Type is expected to be read by ConfigureSections.")]
        private sealed class UserSegment
        {
            public string? Market { get; set; }
        }

        [ConfigurationSection(nameof(WebServer))]
        [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Type used in tests.")]
        private sealed class WebServer
        {
            public string? Scheme { get; set; }

            public string? Host { get; set; }

            public int? Port { get; set; }
        }

        [ConfigurationSection(nameof(Unused))]
        [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Type used in tests.")]
        private sealed class Unused
        {
            public int Number { get; set; }
        }

        [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Type used in tests.")]
        private sealed class InvalidSettings
        { }

        #endregion
    }
}
