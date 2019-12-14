using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Dolittle.Roslyn.Analyzers.PackageDependenciesChecker
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class Analyzer : DiagnosticAnalyzer
    {
        const string RuleId = "DO1000";
        const string Title = "NoAmbiguousPackageDependencies";
        const string MessageFormat = "Something {0} rule broken";
        const string Description = "There are ambiguous package references";
        
        internal static DiagnosticDescriptor Rule =
            new DiagnosticDescriptor(
                RuleId,
                Title,
                MessageFormat,
                "Pre-Compile",
                DiagnosticSeverity.Error,
                isEnabledByDefault: true,
                Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            while(!System.Diagnostics.Debugger.IsAttached) System.Threading.Thread.Sleep(100);
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterCompilationStartAction(AnalyzePackages);
        }

        void AnalyzePackages(CompilationStartAnalysisContext context)
        {   
            while(!System.Diagnostics.Debugger.IsAttached) System.Threading.Thread.Sleep(100);
            Console.WriteLine("Hello");
            context.RegisterCompilationEndAction(compilationContext => {
                compilationContext.ReportDiagnostic(Diagnostic.Create(Rule, Location.None, "Message"));
            });
            
        }
    }
}
