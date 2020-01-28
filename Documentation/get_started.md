---
title: Get started
url: /tooling/dotnet-common/
description: .NET Common
weight: 1
aliases:
    - /tooling/dotnet-common/get-started
---
The .NET Common packages represent common utilities and configuration for as building packages, releasing
packages in a consistent way. It also includes a pre-configured system for static code analysis,
with all the rules that we at Dolittle consider important. These rules can be overridden.

In addition to the automatic configuration of static code analysis, it also holds custom built
code analysis rules.

## Get started

In your project all you need is to add a **PackageReference** to the [package](https://www.nuget.org/packages/Dolittle.Common/).
The `dotnet` tool-chain will during build include any `.props` or `.targets` files found in the package by convention.
From the `.props` file you'll get a lot of default configuration set up, and since this is used by Dolittle itself,
it will put in package information saying it is a Dolittle package and all the details Dolittle wants to have there.
This can be overridden if you're only interested in parts of the configuration.

You add the reference by doing the following from your terminal:

```shell
$ dotnet add package Dolittle.Common
```

Or manually add the following to your `.csproj` - obviously for good measure,
you should just add the `<PackageReference>` inside an existing `<ItemGroup>`
with package references.

```xml
<ItemGroup>
    <PackageReference Include="Dolittle.Common" Version="2.*"/>
</ItemGroup>
```

By using a wildcard for minor in the version of the packages, you're guaranteed to have the latest of the package.

