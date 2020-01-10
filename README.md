# Sweetener
The Sweetener project is yet another collection of open source C# libraries that are
meant to ease the development of .NET applications. These libraries contain solutions
to common problems that are not immediately addressed by the foundational class libraries.
Each Sweetener library follows the same set of principles:

1. Integrate with existing .NET types and patterns
2. Provide modern and well-performing solutions
3. Maintain trust with users through transparency and code quality
4. Ensure compatability across .NET implementations

## Table of Contents
  1. [Libraries](#Libraries)
     1. [Reliability](#Reliability)
  2. [License](#License)

## Libraries
### Reliability
[![Build Status](https://wsugarman.visualstudio.com/Sweetener/_apis/build/status/Sweetener.Reliability?branchName=master)](https://wsugarman.visualstudio.com/Sweetener/_build/latest?definitionId=2&branchName=master)

The `Sweetener.Reliability` library provides an API for reliably running delegates that may
ocassionally result in transient failures. The execution of these "reliable" actions
and functions are controlled through user-defined policies that define behavior such as
"Which exceptions are transient?" and "How long should the delegate wait between retries?"

- NuGet: TBD

## License
The Sweetener libraries are all licensed under the [MIT](LICENSE) license.
