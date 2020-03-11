# Sweetener

## Overview

The Sweetener libraries provide additional APIs that extend the
[base class libraries (BCL)](https://docs.microsoft.com/en-us/dotnet/standard/framework-libraries).
Each library specializes in a domain hat may correlate to an existing namespace in the BCL,
like "Reflection," or an entirely new namespace, like "Reliability." It is our hope that these
APIs seamlessly augment the BCL as if they were always included. To that end, these libraries
try to leverage existing .NET types and patterns as much as possible and only create new types
where appropriate. To reduce the friction in leveraging these APIs, all Sweetener
packages try to target a wide variety of .NET platforms through
[CLS compliance](https://docs.microsoft.com/en-us/dotnet/standard/language-independence)
and targeting the [.NET Standard](https://docs.microsoft.com/en-us/dotnet/standard/net-standard).

## Libraries

- [Sweetener](Getting-Started/Sweetener.md)
- [Sweetener.Reliability](Getting-Started/Sweetener.Reliability.md)

## Questions

Feel free to leave questions and comments on our
[issues page](https://github.com/wsugarman/Sweetener/issues), or take the time to
contribute and start a [pull request](https://github.com/wsugarman/Sweetener/pulls).