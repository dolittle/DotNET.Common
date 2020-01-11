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
| --- | ----- |
| [DL0001]({{< relref DL0001 >}}) | SerializableNotAllowed |
| [DL0002]({{< relref DL0002 >}}) | PrivateNotAllowed |
| [DL0003]({{< relref DL0003 >}}) | SealedNotAllowed |
| [DL0004]({{< relref DL0004 >}}) | ExceptionShouldNotBeSuffixed |
| [DL0005]({{< relref DL0005 >}}) | ExceptionShouldOnlyHaveOneConstructor |
| [DL0006]({{< relref DL0006 >}}) | ExceptionConstructorParametersShouldNotContainMessage |
| [DL0007]({{< relref DL0007 >}}) | ExceptionDescriptionShouldFollowStandard |
| [DL0008]({{< relref DL0008 >}}) | ExceptionShouldBeSpecific |

Due to some rules in the standard analyzers from Microsoft being marked as
*hidden* and as a consequence is not possible to enable during build, we've
created wrappers for the following.

| Id | Title |
| --- | ----- |
| CS8019 | RemoveUnnecessaryImports |
