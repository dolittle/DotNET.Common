// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Dolittle.Roslyn.Analyzers.PackageDependenciesChecker
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class Analyzer : DiagnosticAnalyzer
    {
        const string RuleId = "DO0001";
        const string Title = "NoAmbiguousPackageDependencies";
        const string MessageFormat = "{0}";
        const string Description = "There are ambiguous package references";
        
        internal static DiagnosticDescriptor Rule =
            new DiagnosticDescriptor(
                RuleId,
                Title,
                MessageFormat,
                "Pre-Compile",
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            // while (!System.Diagnostics.Debugger.IsAttached) System.Threading.Thread.Sleep(100);
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterCompilationAction(AnalyzePackages);
        }

        void AnalyzePackages(CompilationAnalysisContext context)
        {
            var assemblies = context.Compilation.References.Select(_ => _.ToAssembly());
            var packages = new AmbiguousPackagesAnalyzer(assemblies);
            var ambiguousReferences = packages.GetAmbiguousPackages();
            foreach (var reference in ambiguousReferences) 
            {
                var diagnosticMessageStringBuild = new StringBuilder();
                diagnosticMessageStringBuild.AppendLine($"{reference.Key} exists in ${reference.Value.Count()} different versions:");
                foreach (var kvp in reference.Value) diagnosticMessageStringBuild.AppendLine($"\t{kvp.Value.ToString()} from assembly ${kvp.Key}");
                context.ReportDiagnostic(Diagnostic.Create(
                    Rule,
                    Location.None,
                    diagnosticMessageStringBuild.ToString()
                ));
            }
        }
    }
}
