// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Dolittle.CodeAnalysis.ExceptionShouldOnlyHaveOneConstructor
{
    /// <summary>
    /// Represents a <see cref="DiagnosticAnalyzer"/> that does not allow the use of the 'sealed' keyword.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class Analyzer : DiagnosticAnalyzer
    {
        /// <summary>
        /// Represents the <see cref="DiagnosticDescriptor">rule</see> for the analyzer.
        /// </summary>
        public static readonly DiagnosticDescriptor Rule = new(
             id: "DL0005",
             title: "ExceptionShouldOnlyHaveOneConstructor",
             messageFormat: "An exception should not have more than one constructor and typically not a generic one taking a message",
             category: "Exceptions",
             defaultSeverity: DiagnosticSeverity.Error,
             isEnabledByDefault: true,
             description: null,
             helpLinkUri: string.Empty,
             customTags: Array.Empty<string>());

        /// <inheritdoc/>
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        /// <inheritdoc/>
        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(
                HandleClassDeclaration,
                ImmutableArray.Create(
                    SyntaxKind.ClassDeclaration));
        }

        static void HandleClassDeclaration(SyntaxNodeAnalysisContext context)
        {
            var classDeclaration = context.Node as ClassDeclarationSyntax;
            if (classDeclaration?.BaseList?.Types is null)
            {
                return;
            }

            if (!classDeclaration.InheritsASystemException(context.SemanticModel))
            {
                return;
            }

            var constructors = classDeclaration.Members.Where(_ => _.IsKind(SyntaxKind.ConstructorDeclaration)).ToArray();
            if (constructors.Length <= 1)
            {
                return;
            }

            foreach (var constructor in constructors)
            {
                var diagnostic = Diagnostic.Create(Rule, constructor.GetLocation());
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
