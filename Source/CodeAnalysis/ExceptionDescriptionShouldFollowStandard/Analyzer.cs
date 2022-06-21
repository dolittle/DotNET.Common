// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Dolittle.CodeAnalysis.ExceptionDescriptionShouldFollowStandard
{
    /// <summary>
    /// Represents a <see cref="DiagnosticAnalyzer"/> that does not allow the use of the 'sealed' keyword.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class Analyzer : DiagnosticAnalyzer
    {
        /// <summary>
        /// The expected phrase.
        /// </summary>
        public const string Phrase = "Exception that gets thrown when";

        /// <summary>
        /// Represents the <see cref="DiagnosticDescriptor">rule</see> for the analyzer.
        /// </summary>
        public static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
             id: "DL0007",
             title: "ExceptionDescriptionShouldFollowStandard",
             messageFormat: $"Exception description for API documentation should start with '{Phrase}'",
             category: "Naming",
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
            if (classDeclaration?.BaseList?.Types == null)
            {
                return;
            }

            if (!classDeclaration.InheritsASystemException(context.SemanticModel))
            {
                return;
            }

            foreach (var trivia in classDeclaration.GetLeadingTrivia())
            {
                if (!trivia.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) && !trivia.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia))
                {
                    continue;
                }

                var descendants = trivia.GetStructure()?.DescendantTokens().ToArray();
                var summaryTokenAndIndex = descendants
                    .Select((token, index) => new { Token = token, Index = index })
                    .FirstOrDefault(_ => _.Token.IsKind(SyntaxKind.IdentifierToken) && _.Token.Text.Equals("summary", StringComparison.InvariantCulture));

                if (summaryTokenAndIndex == default)
                {
                    return;
                }

                var xmlTextLiteralToken = descendants
                    .Skip(summaryTokenAndIndex.Index)
                    .FirstOrDefault(_ => _.IsKind(SyntaxKind.XmlTextLiteralToken));

                if (xmlTextLiteralToken == default)
                {
                    return;
                }

                if (xmlTextLiteralToken.Text.Trim().StartsWith(Phrase, StringComparison.InvariantCulture))
                {
                    continue;
                }

                var diagnostic = Diagnostic.Create(Rule, xmlTextLiteralToken.GetLocation());
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
