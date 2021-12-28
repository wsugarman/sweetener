using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Extensions.Configuration.Test;

[TestClass]
public class ConfigurationSectionAttributeTest
{
    [TestMethod]
    public void Ctor()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new ConfigurationSectionAttribute(null!));

        // Root
        Assert.AreEqual(ConfigurationSectionKey.None, new ConfigurationSectionAttribute(ConfigurationSectionKey.None).Key);

        // Non-root
        Assert.AreEqual("Settings", new ConfigurationSectionAttribute("Settings").Key);
    }

    [TestMethod]
    public void Annotation()
    {
        // Found
        ConfigurationSectionAttribute? attribute = Attribute.GetCustomAttribute(
            typeof(TestSettings),
            typeof(ConfigurationSectionAttribute)) as ConfigurationSectionAttribute;

        Assert.IsNotNull(attribute);
        Assert.AreEqual("Testing", attribute.Key);

        // Not Found (Cannot Inherit)
        Assert.IsNull(Attribute.GetCustomAttribute(typeof(NewSettings), typeof(ConfigurationSectionAttribute)));
    }

    [ConfigurationSection("Testing")]
    private class TestSettings
    { }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Type used in tests.")]
    private sealed class NewSettings : TestSettings
    { }
}
