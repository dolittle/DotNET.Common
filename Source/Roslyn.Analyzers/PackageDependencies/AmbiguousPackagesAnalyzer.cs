// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dolittle.Roslyn.Analyzers.PackageDependenciesChecker
{
    public class AmbiguousPackagesAnalyzer
    {
        readonly IDictionary<string, IEnumerable<KeyValuePair<string, Version>>> _ambiguousPackages = new Dictionary<string, IEnumerable<KeyValuePair<string, Version>>>();
        
        public AmbiguousPackagesAnalyzer(IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                foreach (var referencedAssembly in assembly.GetReferencedAssemblies())
                {
                    if (referencedAssembly.Name.StartsWith("System")) continue; 
                    if (_ambiguousPackages.ContainsKey(referencedAssembly.Name))
                    {
                        var parentAssembliesAndVersions = _ambiguousPackages[referencedAssembly.Name].ToList();
                        var version = referencedAssembly.Version;

                        if (parentAssembliesAndVersions.Where(_ => _.Value.Equals(version)).Count() == 0)
                        {
                            parentAssembliesAndVersions.Add(new KeyValuePair<string, Version>(assembly.GetName().Name, version));
                            _ambiguousPackages[referencedAssembly.Name] = parentAssembliesAndVersions;
                        }
                    }
                    else
                    {
                        var version = referencedAssembly.Version;
                        _ambiguousPackages.Add(referencedAssembly.Name, new KeyValuePair<string, Version>[] 
                        {
                            new KeyValuePair<string, Version>(assembly.GetName().Name, version)
                        });
                    }
                }
            }
        }

        public IDictionary<string, IEnumerable<KeyValuePair<string, Version>>> GetAmbiguousPackages()
        {
            var dict = new Dictionary<string, IEnumerable<KeyValuePair<string, Version>>>();
            foreach (var kvp in _ambiguousPackages)
            {
                if (kvp.Value.Count() > 1) dict.Add(kvp.Key, kvp.Value);
            }
            return dict;
        }
    }
}
