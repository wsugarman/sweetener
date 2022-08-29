// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweetener.Extensions.Configuration.Memory;

namespace Sweetener.Extensions.Configuration.Test.Memory;

[TestClass]
public sealed class MemoryConfigurationTest : IDisposable
{
    private readonly MemoryConfiguration _config = new MemoryConfiguration
    {
        { "Key1"                          , "Value1" },
        { "Key2"                          , "Value2" },
        { "Key3"                          , null     },
        { "Section1:Key1"                 , "Value3" },
        { "Section1:Key2"                 , "Value4" },
        { "Section1:Key3"                 , null     },
        { "Section1:Section2:Key5"        , "Value5" },
        { "Section:Key1"                  , "Value1" },
        { "Section:Key2"                  , "Value2" },
        { "Section:Key3"                  , null     },
        { "Section:Section1:Key1"         , "Value3" },
        { "Section:Section1:Key2"         , "Value4" },
        { "Section:Section1:Key3"         , null     },
        { "Section:Section1:Section2:Key5", "Value5" },
    };

    private readonly ConfigurationRoot _expected = new ConfigurationRoot(
        new IConfigurationProvider[]
        {
            new MemoryConfigurationProvider(
                new MemoryConfigurationSource
                {
                    InitialData = new KeyValuePair<string, string?>[]
                    {
                        new KeyValuePair<string, string?>("Key1"                          , "Value1"),
                        new KeyValuePair<string, string?>("Key2"                          , "Value2"),
                        new KeyValuePair<string, string?>("Key3"                          , null    ),
                        new KeyValuePair<string, string?>("Section1:Key1"                 , "Value3"),
                        new KeyValuePair<string, string?>("Section1:Key2"                 , "Value4"),
                        new KeyValuePair<string, string?>("Section1:Key3"                 , null    ),
                        new KeyValuePair<string, string?>("Section1:Section2:Key5"        , "Value5"),
                        new KeyValuePair<string, string?>("Section:Key1"                  , "Value1"),
                        new KeyValuePair<string, string?>("Section:Key2"                  , "Value2"),
                        new KeyValuePair<string, string?>("Section:Key3"                  , null    ),
                        new KeyValuePair<string, string?>("Section:Section1:Key1"         , "Value3"),
                        new KeyValuePair<string, string?>("Section:Section1:Key2"         , "Value4"),
                        new KeyValuePair<string, string?>("Section:Section1:Key3"         , null    ),
                        new KeyValuePair<string, string?>("Section:Section1:Section2:Key5", "Value5"),
                    },
                })
        });

    [TestMethod]
    public void Ctor()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new MemoryConfiguration(null!));

        MemoryConfiguration alternative = new MemoryConfiguration(
            new Dictionary<string, string?>
            {
                { "Key1"                          , "Value1" },
                { "Key2"                          , "Value2" },
                { "Key3"                          , null     },
                { "Section1:Key1"                 , "Value3" },
                { "Section1:Key2"                 , "Value4" },
                { "Section1:Key3"                 , null     },
                { "Section1:Section2:Key5"        , "Value5" },
                { "Section:Key1"                  , "Value1" },
                { "Section:Key2"                  , "Value2" },
                { "Section:Key3"                  , null     },
                { "Section:Section1:Key1"         , "Value3" },
                { "Section:Section1:Key2"         , "Value4" },
                { "Section:Section1:Key3"         , null     },
                { "Section:Section1:Section2:Key5", "Value5" },
            });

        List<KeyValuePair<string, string>> expected = _config.ToList();
        List<KeyValuePair<string, string>> actual = alternative.ToList();

        Assert.AreEqual(expected.Count, actual.Count);
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.AreEqual(expected[i].Key, actual[i].Key);
            Assert.AreEqual(expected[i].Value, actual[i].Value);
        }
    }

    [TestMethod]
    public void Get()
        => Get(c => c);

    [TestMethod]
    public void Set()
        => Set((c, k, v) => Assert.AreEqual(v, c[k] = v), c => c);

    [TestMethod]
    public void Add()
        => Set((c, k, v) => c.Add(k, v), c => c);

    [TestMethod]
    public void GetChildren()
        => GetChildren(c => c);

    [TestMethod]
    public void GetReloadToken()
        => GetReloadToken(c => c.GetReloadToken());

    [TestMethod]
    public void GetSection()
        => GetSection(c => c);

    [TestMethod]
    public void GetEnumerator()
        => GetEnumerator(c => c);

    [TestMethod]
    public void GetEnumerator_Object()
        => GetEnumerator(c => Enumerate<KeyValuePair<string, string>>(c));

    [TestMethod]
    public void Section_Get()
        => Get(c => c.GetSection("Section"));

    [TestMethod]
    public void Section_Set()
        => Set((c, k, v) => Assert.AreEqual(v, c[k] = v), c => c.GetSection("Section"));

    [TestMethod]
    public void Section_SetValue()
        => Set((c, k, v) => Assert.AreEqual(v, c.GetSection(k).Value = v), c => c.GetSection("Section"));

    [TestMethod]
    public void Section_GetChildren()
        => GetChildren(c => c.GetSection("Section"));

    [TestMethod]
    public void Section_GetReloadToken()
        => GetReloadToken(c => c.GetSection("Section").GetReloadToken());

    [TestMethod]
    public void Section_GetSection()
        => GetSection(c => c.GetSection("Section"), "Section");

    public void Dispose()
        => _expected.Dispose();

    private void Get(Func<IConfiguration, IConfiguration> getConfig)
    {
        IConfiguration config = getConfig(_config);

        Assert.AreEqual("Value1", config["Key1"]);
        Assert.AreEqual("Value2", config["Key2"]);
        Assert.AreEqual(null, config["Key3"]);
        Assert.AreEqual(null, config["Key4"]);
        Assert.AreEqual(null, config["Section1"]);

        Assert.AreEqual("Value3", config["Section1:Key1"]);
        Assert.AreEqual("Value4", config["Section1:Key2"]);
        Assert.AreEqual(null, config["Section1:Key3"]);
        Assert.AreEqual(null, config["Section1:Key4"]);
        Assert.AreEqual(null, config["Section1:Section2"]);
    }

    private void GetChildren(Func<IConfiguration, IConfiguration> getConfig)
    {
        List<IConfigurationSection> expectedChildren = getConfig(_expected).GetChildren().ToList();
        List<IConfigurationSection> actualChildren = getConfig(_config).GetChildren().ToList();

        Assert.AreEqual(expectedChildren.Count, actualChildren.Count);
        for (int i = 0; i < expectedChildren.Count; i++)
            AssertSection(expectedChildren[i], actualChildren[i]);
    }

    private void GetSection(Func<IConfiguration, IConfiguration> getConfig, string? path = null)
    {
        IConfiguration config = getConfig(_config);

        Func<string, string> getPath = k => path is null ? k : path + ":" + k;

        AssertSection("Key1", "Value1", getPath("Key1"), 0, config.GetSection("Key1"));
        AssertSection("Key2", "Value2", getPath("Key2"), 0, config.GetSection("Key2"));
        AssertSection("Key3", null, getPath("Key3"), 0, config.GetSection("Key3"));
        AssertSection("Key4", null, getPath("Key4"), 0, config.GetSection("Key4"));
        AssertSection("Section1", null, getPath("Section1"), 4, config.GetSection("Section1"));

        AssertSection("Key1", "Value3", getPath("Section1:Key1"), 0, config.GetSection("Section1:Key1"));
        AssertSection("Key2", "Value4", getPath("Section1:Key2"), 0, config.GetSection("Section1:Key2"));
        AssertSection("Key3", null, getPath("Section1:Key3"), 0, config.GetSection("Section1:Key3"));
        AssertSection("Key4", null, getPath("Section1:Key4"), 0, config.GetSection("Section1:Key4"));
        AssertSection("Section2", null, getPath("Section1:Section2"), 1, config.GetSection("Section1:Section2"));
    }

    private void GetEnumerator(Func<MemoryConfiguration, IEnumerable<KeyValuePair<string, string>>> getEnumerator)
    {
        List<KeyValuePair<string, string>> expected = _expected.AsEnumerable().ToList();
        List<KeyValuePair<string, string>> actual = getEnumerator(_config).ToList();

        Assert.AreEqual(expected.Count, actual.Count);
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.AreEqual(expected[i].Key, actual[i].Key);
            Assert.AreEqual(expected[i].Value, actual[i].Value);
        }
    }

    private static void Set<T>(Action<T, string, string> set, Func<MemoryConfiguration, T> getConfig)
        where T : IConfiguration
    {
        T config = getConfig(new MemoryConfiguration());

        // New key
        Assert.IsNull(config["Key"]);
        set(config, "Key", "Value1");
        Assert.AreEqual("Value1", config["Key"]);

        // Overwrite key
        set(config, "Key", "Value2");
        Assert.AreEqual("Value2", config["Key"]);

        // New key in section
        Assert.IsNull(config["Section:Key"]);
        set(config, "Section:Key", "Value1");
        Assert.AreEqual("Value1", config["Section:Key"]);

        // Overwrite key in section
        set(config, "Section:Key", "Value2");
        Assert.AreEqual("Value2", config["Section:Key"]);
    }

    private static void GetReloadToken(Func<MemoryConfiguration, IChangeToken> getToken)
    {
        MemoryConfiguration config = new MemoryConfiguration();
        IChangeToken token = getToken(config);
        token.RegisterChangeCallback(_ => Assert.Fail(), null);

        Assert.IsFalse(token.HasChanged);
        Assert.IsTrue(token.ActiveChangeCallbacks);
        Assert.AreNotSame(token, getToken(new MemoryConfiguration()));

        config["Key"] = "Value";

        Assert.IsFalse(token.HasChanged);
    }

    private static void AssertSection(IConfigurationSection expected, IConfigurationSection actual)
        => AssertSection(expected.Key, expected.Value, expected.Path, expected.GetChildren().Count(), actual);

    private static void AssertSection(string key, string? value, string path, int children, IConfigurationSection actual)
    {
        Assert.AreEqual(key, actual.Key);
        Assert.AreEqual(value, actual.Value);
        Assert.AreEqual(path, actual.Path);
        Assert.AreEqual(children, actual.GetChildren().Count());
    }

    private static IEnumerable<T> Enumerate<T>(IEnumerable source)
    {
        foreach (object? obj in source)
            yield return (T)obj!;
    }
}
