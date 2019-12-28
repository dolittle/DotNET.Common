---
title: Analyzers
description: This contains the overview of all the analyzers
repository: https://github.com/dolittle-tools/DotNET.Common
---

Below are the Dolittle specific rules that we are enforcing.

| Id | Title |
| -- | ----- |
| [DL0001](./DL0001.md) | SerializableNotAllowed |
| DL0002 | PrivateNotAllowed |
| DL0003 | SealedNotAllowed |
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
