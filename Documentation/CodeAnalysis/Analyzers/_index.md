---
title: Analyzers
description: This contains the overview of all the analyzers
---
## Analyzers

Below are the Dolittle specific rules that we are enforcing.
Rules can be disabled either through custom [rulesets](https://docs.microsoft.com/en-us/visualstudio/code-quality/how-to-create-a-custom-rule-set) or using the [-nowarn](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/nowarn-compiler-option) compiler option or using the
[<NoWarn/>](https://docs.microsoft.com/en-us/visualstudio/msbuild/common-msbuild-project-properties?view=vs-2019)
`property` in your `.csproj` file.

| Id | Title |
| -- | ----- |
| [DL0001](./DL0001.md) | SerializableNotAllowed |
| [DL0002](./DL0002.md) | PrivateNotAllowed |
| [DL0003](./DL0003.md) | SealedNotAllowed |
| DL0004 | ExceptionShouldNotBeSuffixed |
| DL0005 | ExceptionShouldOnlyHaveOneConstructor |
| DL0006 | ExceptionConstructorParametersShouldNotContainMessage |
| DL0007 | ExceptionDescriptionShouldFollowStandard |
| DL0008 | ExceptionShouldBeSpecific |

Due to some rules in the standard analyzers from Microsoft being marked as
*hidden*, we've created wrappers for the following as we see them as
important enough to break the build.

| Id | Title |
| -- | ----- |
| CS8019 | RemoveUnnecessaryImports |
