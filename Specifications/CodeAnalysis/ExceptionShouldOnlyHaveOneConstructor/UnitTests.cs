// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dolittle.CodeAnalysis.ExceptionShouldOnlyHaveOneConstructor
{
    [TestClass]
    public class UnitTests : CodeFixVerifier
    {
        [TestMethod]
        public void WithoutConstructor()
        {
            const string content = @"
                using System;

                namespace MyNamespace
                {
                    public class SomethingWentWrong : Exception
                    {
                    }
                }       
            ";

            VerifyCSharpDiagnostic(content);
        }

        [TestMethod]
        public void WithSingleConstructor()
        {
            const string content = @"
                using System;

                namespace MyNamespace
                {
                    public class SomethingWentWrong : Exception
                    {
                        public SomethingWentWrong()
                        {
                        }
                    }
                }       
            ";

            VerifyCSharpDiagnostic(content);
        }

        [TestMethod]
        public void WithMultipleConstructors()
        {
            const string content = @"
                using System;

                namespace MyNamespace
                {
                    public class SomethingWentWrong : Exception
                    {
                        public SomethingWentWrong()
                        {
                        }

                        public SomethingWentWrong(string something)
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
                    new DiagnosticResultLocation("Test0.cs", 8, 25)
                }
            };

            var secondFailure = new DiagnosticResult
            {
                Id = Analyzer.Rule.Id,
                Message = (string)Analyzer.Rule.MessageFormat,
                Severity = Analyzer.Rule.DefaultSeverity,
                Locations = new[]
                {
                    new DiagnosticResultLocation("Test0.cs", 12, 25)
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