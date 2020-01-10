---
title: .NET Common
description: .NET Common
repository: https://github.com/dolittle-tools/DotNET.Common
---
The .NET Common packages represent common utilities and configuration for as building packages, releasing
packages in a consistent way. It also includes a pre-configured system for static code analysis,
with all the rules that we at Dolittle consider important. These rules can be overridden.

In addition to the automatic configuration of static code analysis, it also holds custom built
code analysis rules.

## Getting Started

All you have to do is add a package reference to the [Dolittle.Common package](https://www.nuget.org/packages/Dolittle.Common/).
By using a wildcard for minor in the version of the packages, you're guaranteed to have the latest of the package.
