// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using System.Reflection;
using Microsoft.CodeAnalysis;

namespace Dolittle.Roslyn.Analyzers.PackageDependenciesChecker
{
    public static class MetadataReferenceExtensions
    {
        public static Assembly ToAssembly(this MetadataReference reference)
        {
            Assembly assembly = null;
            var assemblyPath = reference.Display;
            if (File.Exists(assemblyPath)) assembly = Assembly.LoadFrom(assemblyPath);
            
            return assembly;
        }
    }
}