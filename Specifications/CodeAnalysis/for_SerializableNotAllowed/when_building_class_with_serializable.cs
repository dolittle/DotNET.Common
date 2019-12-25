// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Machine.Specifications;

namespace Dolittle.CodeAnalysis.for_SerializableNotAllowed
{
    public class when_building_class_with_serializable
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

        It should_fail = () => new CodeFixVerifier().VerifyCSharpDiagnostic(content);
    }
}