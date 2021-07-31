// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Extensions.Configuration.Test
{
    [TestClass]
    public class ConfigurationSectionBinderTest
    {
        private static readonly IConfiguration TestConfiguration = new ConfigurationBuilder()
            .AddInMemoryCollection(
                new Dictionary<string, string>
                {
                    { "Testing:Categories:0"                  , "Unit"        },
                    { "Testing:Categories:1"                  , "Integration" },
                    { "Testing:Categories:2"                  , "Smoke"       },
                    { "Testing:CodeCoverage:MinBranchCoverage", "0.73"        },
                    { "Testing:CodeCoverage:MinLineCoverage"  , "0.80"        },
                })
            .Build();

        private static readonly IConfiguration InvalidTestConfiguration = new ConfigurationBuilder()
            .AddInMemoryCollection(
                new Dictionary<string, string>
                {
                    { "Testing:CodeCoverage:MinBranchCoverage", "foo" },
                    { "Testing:CodeCoverage:MinLineCoverage"  , "bar" },
                })
            .Build();

        [TestMethod]
        public void BindSection_Object()
        {
            // Invalid Type for Instance
            Assert.ThrowsException<ArgumentException>(() => TestConfiguration.BindSection(new CodeCoverageSettings()));

            // Null Arguments
            Assert.ThrowsException<ArgumentNullException>(() => ConfigurationSectionBinder.BindSection(configuration: null!, instance: new TestSettings()));
            Assert.ThrowsException<ArgumentNullException>(() => ConfigurationSectionBinder.BindSection(TestConfiguration   , instance: null!));

            // Unable to Bind
            Assert.ThrowsException<InvalidOperationException>(() => InvalidTestConfiguration.BindSection(new TestSettings()));

            // Success
            TestSettings settings = new TestSettings();
            TestConfiguration.BindSection(settings);

            AssertExpectedConfiguration(settings);
        }

        [TestMethod]
        public void BindSection_Object_ConfigureOptions()
        {
            // Invalid Type for Instance
            Assert.ThrowsException<ArgumentException>(() => TestConfiguration.BindSection(new CodeCoverageSettings(), configureOptions: null));

            // Null Arguments
            Assert.ThrowsException<ArgumentNullException>(() => ConfigurationSectionBinder.BindSection(configuration: null!, instance: new TestSettings(), configureOptions: null));
            Assert.ThrowsException<ArgumentNullException>(() => ConfigurationSectionBinder.BindSection(TestConfiguration   , instance: null!             , configureOptions: null));

            // Unable to Bind
            Assert.ThrowsException<InvalidOperationException>(() => InvalidTestConfiguration.BindSection(new TestSettings(), configureOptions: null));

            // Success
            int configureCalls = 0;
            TestSettings settings = new TestSettings();
            Action<BinderOptions> configureAction = o => { Assert.IsNotNull(o); configureCalls++; };
            TestConfiguration.BindSection(settings, configureAction);

            AssertExpectedConfiguration(settings);
            Assert.AreEqual(1, configureCalls);
        }

        [TestMethod]
        public void GetSection_Type()
        {
            // Invalid Type for Instance
            Assert.ThrowsException<ArgumentException>(() => TestConfiguration.GetSection(typeof(CodeCoverageSettings)));

            // Null Arguments
            Assert.ThrowsException<ArgumentNullException>(() => ConfigurationSectionBinder.GetSection(configuration: null!, type: typeof(TestSettings)));
            Assert.ThrowsException<ArgumentNullException>(() => ConfigurationSectionBinder.GetSection(TestConfiguration   , type: null!));

            // Unable to Bind
            Assert.ThrowsException<InvalidOperationException>(() => InvalidTestConfiguration.GetSection(typeof(TestSettings)));

            // Success
            AssertExpectedConfiguration(TestConfiguration.GetSection(typeof(TestSettings)) as TestSettings);
        }

        [TestMethod]
        public void GetSection_Type_ConfigureOptions()
        {
            // Invalid Type for Instance
            Assert.ThrowsException<ArgumentException>(() => TestConfiguration.GetSection(typeof(CodeCoverageSettings), configureOptions: null));

            // Null Arguments
            Assert.ThrowsException<ArgumentNullException>(() => ConfigurationSectionBinder.GetSection(configuration: null!, type: typeof(TestSettings), configureOptions: null));
            Assert.ThrowsException<ArgumentNullException>(() => ConfigurationSectionBinder.GetSection(TestConfiguration   , type: null!               , configureOptions: null));

            // Unable to Bind
            Assert.ThrowsException<InvalidOperationException>(() => InvalidTestConfiguration.GetSection(typeof(TestSettings), configureOptions: null));

            // Success
            int configureCalls = 0;
            Action<BinderOptions> configureAction = o => { Assert.IsNotNull(o); configureCalls++; };
            AssertExpectedConfiguration(TestConfiguration.GetSection(typeof(TestSettings), configureAction) as TestSettings);
            Assert.AreEqual(1, configureCalls);
        }

        [TestMethod]
        public void GetSection_T()
        {
            // Invalid Type for Instance
            Assert.ThrowsException<ArgumentException>(() => TestConfiguration.GetSection<CodeCoverageSettings>());

            // Null Argument
            Assert.ThrowsException<ArgumentNullException>(() => ConfigurationSectionBinder.GetSection<TestSettings>(configuration: null!));

            // Unable to Bind
            Assert.ThrowsException<InvalidOperationException>(() => InvalidTestConfiguration.GetSection<TestSettings>());

            // Success
            AssertExpectedConfiguration(TestConfiguration.GetSection<TestSettings>());
        }

        [TestMethod]
        public void GetSection_T_ConfigureOptions()
        {
            // Invalid Type for Instance
            Assert.ThrowsException<ArgumentException>(() => TestConfiguration.GetSection<CodeCoverageSettings>(configureOptions: null));

            // Null Argument
            Assert.ThrowsException<ArgumentNullException>(() => ConfigurationSectionBinder.GetSection<TestSettings>(configuration: null!, configureOptions: null));

            // Unable to Bind
            Assert.ThrowsException<InvalidOperationException>(() => InvalidTestConfiguration.GetSection<TestSettings>(configureOptions: null));

            // Success
            int configureCalls = 0;
            Action<BinderOptions> configureAction = o => { Assert.IsNotNull(o); configureCalls++; };
            AssertExpectedConfiguration(TestConfiguration.GetSection<TestSettings>(configureAction));
            Assert.AreEqual(1, configureCalls);
        }

        private static void AssertExpectedConfiguration(TestSettings? actual)
        {
            Assert.IsNotNull(actual);

            Assert.IsNotNull(actual.Categories);
            Assert.AreEqual(3            , actual.Categories.Count);
            Assert.AreEqual("Unit"       , actual.Categories[0]);
            Assert.AreEqual("Integration", actual.Categories[1]);
            Assert.AreEqual("Smoke"      , actual.Categories[2]);

            Assert.IsNotNull(actual.CodeCoverage);
            Assert.AreEqual(0.73d, actual.CodeCoverage.MinBranchCoverage);
            Assert.AreEqual(0.80d, actual.CodeCoverage.MinLineCoverage);
        }

        [ConfigurationSection("Testing")]
        private sealed class TestSettings
        {
            public IReadOnlyList<string>? Categories { get; set; }

            public CodeCoverageSettings? CodeCoverage { get; set; }
        }

        private sealed class CodeCoverageSettings
        {
            public double MinBranchCoverage { get; set; }

            public double MinLineCoverage { get; set; }
        }
    }
}
