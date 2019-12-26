// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dolittle.CodeAnalysis.SerializableNotAllowed
{
    [TestClass]
    public class UnitTests : CodeFixVerifier
    {
        [TestMethod]
        public void WithoutSerializable()
        {
            const string content = @"
                using System;

                namespace MyNamespace
                {
                    public class MyClass
                    {

                    }
                }       
            ";

            VerifyCSharpDiagnostic(content);
        }

        [TestMethod]
        public void WithSerializable()
        {
            const string content = @"
                using System;

                namespace MyNamespace
                {
                    [Serializable]
                    public class MyClass
                    {

                    }
                }       
            ";

            var expected = new DiagnosticResult
            {
                Id = Analyzer.Rule.Id,
                Message = (string)Analyzer.Rule.MessageFormat,
                Severity = Analyzer.Rule.DefaultSeverity,
                Locations = new[]
                {
                    new DiagnosticResultLocation("Test0.cs", 6, 22)
                }
            };

            VerifyCSharpDiagnostic(content, expected);
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new Analyzer();
        }
    }
}