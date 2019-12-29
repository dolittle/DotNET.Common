// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Dolittle.CodeAnalysis.ExceptionConstructorParametersShouldNotContainMessage
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
        public static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
             id: "DL0006",
             title: "ExceptionConstructorParametersShouldNotContainMessage",
             messageFormat: "An argument of an exception with the name 'message' in it indicates its a generic exception and output string ownership is wrong.",
             category: "Exceptions",
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
            context.RegisterSyntaxNodeAction(
                HandleClassDeclaration,
                ImmutableArray.Create(
                    SyntaxKind.ClassDeclaration));
        }

        void HandleClassDeclaration(SyntaxNodeAnalysisContext context)
        {
            var classDeclaration = context.Node as ClassDeclarationSyntax;
            if (classDeclaration?.BaseList == null || classDeclaration?.BaseList?.Types == null) return;

            if (classDeclaration.BaseList.Types
                            .Where(_ => _.Type is IdentifierNameSyntax)
                            .Select(_ => _.Type as IdentifierNameSyntax)
                            .Any(_ => _.Identifier.Text.EndsWith("Exception", StringComparison.InvariantCulture)))
            {
                var constructors = classDeclaration.Members
                                                        .Where(_ => _.IsKind(SyntaxKind.ConstructorDeclaration))
                                                        .Select(_ => _ as ConstructorDeclarationSyntax);

                foreach (var constructor in constructors)
                {
                    var wronglyNamedParameters = constructor.ParameterList.Parameters
                                                    .Where(_ =>
                                                        _.Type.ToString().EndsWith("string", StringComparison.InvariantCultureIgnoreCase) &&
                                                        _.Identifier.Text.IndexOf("message", StringComparison.InvariantCultureIgnoreCase) >= 0);
                    foreach (var parameter in wronglyNamedParameters)
                    {
                        var diagnostic = Diagnostic.Create(Rule, parameter.Identifier.GetLocation());
                        context.ReportDiagnostic(diagnostic);
                    }
                }
            }
        }
    }
}
