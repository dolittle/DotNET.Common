# Common properties and build rules

This project contains common properties and build rules and more that
helps keep consistency across all the different Dolittle projects and
packages when building and deploying to NuGet.

Typically the tags, links, logo and things like this then become
standard and maintained in one location.

All properties are possible to override in your specific project.

## Usage

In your `.csproj` file all you'll have to do is add a **PackageReference**
to the package. The `dotnet` tool-chain will during build include any `.props`
or `.targets` files by convention from this project.

You add the reference by doing the following from your terminal:

```shell
$ dotnet add package Dolittle.Common
```

Or manually add the following to your `.csproj` - obviously for good measure,
you should just add the `<PackageReference>` inside an existing `<ItemGroup>`
with package references.

```xml
<ItemGroup>
    <PackageReference Include="Dolittle.Common" Version="1.*"/>
</ItemGroup>
```