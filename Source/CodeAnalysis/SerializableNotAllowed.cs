// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace MyFirstAnalyzer
{
    /// <summary>
    /// Represents a <see cref="DiagnosticAnalyzer"/> that does not allow the use of <see cref="SerializableAttribute"/>.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class SerializableNotAllowed : DiagnosticAnalyzer
    {
        static readonly DiagnosticDescriptor _rule = new DiagnosticDescriptor(
             id: "DL0001",
             title: nameof(SerializableNotAllowed),
             messageFormat: "There is an ambiguity in versioning that could lead to runtime problems.",
             category: "PackageVersioning",
             defaultSeverity: DiagnosticSeverity.Error,
             isEnabledByDefault: true,
             description: null,
             helpLinkUri: $"",
             customTags: Array.Empty<string>());

        /// <inheritdoc/>
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(_rule);

        /// <inheritdoc/>
        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);

            context.RegisterCompilationAction(AnalyzeVersion);
        }

        static void AnalyzeVersion(CompilationAnalysisContext context)
        {
            var diagnostic = Diagnostic.Create(_rule, null, context.Compilation.AssemblyName);

            context.ReportDiagnostic(diagnostic);
        }
    }
}
