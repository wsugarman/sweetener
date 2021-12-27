// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;

namespace Sweetener.Generators;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
internal sealed class ProjectAttribute : Attribute
{
    public string Name { get; }

    public ProjectAttribute(string name)
        => Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException("Name must be specified.", nameof(name));
}
