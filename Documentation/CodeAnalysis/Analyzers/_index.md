---
title: Analyzers
description: This contains the overview of all the analyzers
---
Below are the Dolittle specific rules that we are enforcing.
Rules can be disabled either through custom [rulesets](https://docs.microsoft.com/en-us/visualstudio/code-quality/how-to-create-a-custom-rule-set) or using the [-nowarn](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/nowarn-compiler-option) compiler option or using the
[NoWarn property](https://docs.microsoft.com/en-us/visualstudio/msbuild/common-msbuild-project-properties?view=vs-2019)
in your `.csproj` file.

Example of NoWarn property:

```xml
<NoWarn>$(NoWarn),DL0001,DL0004</NoWarn>
```

## Rules

| Id | Title |
| -- | ----- |
| [DL0001](./DL0001.md) | SerializableNotAllowed |
| [DL0002](./DL0002.md) | PrivateNotAllowed |
| [DL0003](./DL0003.md) | SealedNotAllowed |
| [DL0004](./DL0004.md) | ExceptionShouldNotBeSuffixed |
| [DL0005](./DL0005.md) | ExceptionShouldOnlyHaveOneConstructor |
| [DL0006](./DL0006.md) | ExceptionConstructorParametersShouldNotContainMessage |
| [DL0007](./DL0007.md) | ExceptionDescriptionShouldFollowStandard |
| [DL0008](./DL0008.md) | ExceptionShouldBeSpecific |

Due to some rules in the standard analyzers from Microsoft being marked as
*hidden* and as a consequence is not possible to enable during build, we've
created wrappers for the following.

| Id | Title |
| -- | ----- |
| CS8019 | RemoveUnnecessaryImports |
