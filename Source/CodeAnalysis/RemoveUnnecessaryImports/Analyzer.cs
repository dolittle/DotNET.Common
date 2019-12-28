// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

#pragma warning disable RS1025, RS1026

namespace Dolittle.CodeAnalysis.RemoveUnnecessaryImports
{
    /// <summary>
    /// Represents a <see cref="DiagnosticAnalyzer"/> that does not allow the unnecessary import statements.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class Analyzer : DiagnosticAnalyzer
    {
        /// <summary>
        /// Represents the <see cref="DiagnosticDescriptor">rule</see> for the analyzer.
        /// </summary>
        public static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
             id: "CS8019",
             title: "RemoveUnnecessaryImports",
             messageFormat: $"Unnecessary using directive.",
             category: "Style",
             defaultSeverity: DiagnosticSeverity.Error,
             isEnabledByDefault: true,
             description: null,
             helpLinkUri: $"",
             customTags: Array.Empty<string>());

        /// <inheritdoc/>
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        /// <inheritdoc/>
        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);

            context.RegisterSemanticModelAction(AnalyzeSemanticModel);
        }

        void AnalyzeSemanticModel(SemanticModelAnalysisContext context)
        {
            var cancellationToken = context.CancellationToken;
            var model = context.SemanticModel;
            var root = model.SyntaxTree.GetRoot(cancellationToken);

            var diagnostics = model.GetDiagnostics(cancellationToken: cancellationToken);
            if (!diagnostics.Any()) return;

            foreach (var diagnostic in diagnostics)
            {
                if (diagnostic.Id == "CS8019")
                {
                    if (root.FindNode(diagnostic.Location.SourceSpan) is UsingDirectiveSyntax node)
                    {
                        var diagnosticWrapper = Diagnostic.Create(Rule, node.GetLocation());
                        context.ReportDiagnostic(diagnosticWrapper);
                    }
                }
            }
        }
    }
}