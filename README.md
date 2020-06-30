# Common properties and build rules

![.NET Library CI/CD](https://github.com/dolittle/DotNET.Common/workflows/.NET%20Library%20CI/CD/badge.svg)
[![codecov](https://codecov.io/gh/dolittle/DotNET.Common/branch/master/graph/badge.svg)](https://codecov.io/gh/dolittle/DotNET.Common)
This project contains common properties and build rules and more that
helps keep consistency across all the different Dolittle projects and
packages when building and deploying to NuGet.

Typically the tags, links, logo and things like this then become
standard and maintained in one location.

All properties are possible to override in your specific project.

## Cloning

This repository has sub modules, clone it with:

```text
$ git clone --recursive <repository url>
```

If you've already cloned it, you can get the submodules by doing the following:

```text
$ git submodule update --init --recursive
```

## Usage

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

## CodeAnalysis Rules

There are custom rules that has been built which enforces Dolittle specific rules.
Read more about these [here](./Documentation/CodeAnalysis/Analyzers/_index.md).

# Issues and Contributing
To learn how to contribute please read our [contributing](https://dolittle.io/contributing/) guide.

File issues to our [Home](https://github.com/dolittle/Home/issues) repository.
