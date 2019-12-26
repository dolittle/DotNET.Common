// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dolittle.CodeAnalysis.ExceptionConstructorParametersShouldNotContainMessage
{
    [TestClass]
    public class UnitTests : CodeFixVerifier
    {
        [TestMethod]
        public void WithoutMessageInParameters()
        {
            const string content = @"
                using System;

                namespace MyNamespace
                {
                    public class SomethingWentWrong : Exception
                    {
                        public SomethingWentWrong(string first, string second)
                        {
                        }
                    }
                }       
            ";

            VerifyCSharpDiagnostic(content);
        }

        [TestMethod]
        public void WithMessageInConstructorParameters()
        {
            const string content = @"
                using System;

                namespace MyNamespace
                {
                    public class SomethingWentWrong : Exception
                    {
                        public SomethingWentWrong(string firstMessage, string messageForSecond)
                        {
                        }
                    }
                }       
            ";

            var firstFailure = new DiagnosticResult
            {
                Id = Analyzer.Rule.Id,
                Message = (string)Analyzer.Rule.MessageFormat,
                Severity = Analyzer.Rule.DefaultSeverity,
                Locations = new[]
                {
                    new DiagnosticResultLocation("Test0.cs", 8, 58)
                }
            };

            var secondFailure = new DiagnosticResult
            {
                Id = Analyzer.Rule.Id,
                Message = (string)Analyzer.Rule.MessageFormat,
                Severity = Analyzer.Rule.DefaultSeverity,
                Locations = new[]
                {
                    new DiagnosticResultLocation("Test0.cs", 8, 79)
                }
            };

            VerifyCSharpDiagnostic(content, firstFailure, secondFailure);
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new Analyzer();
        }
    }
}