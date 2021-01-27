// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Net5
{
    public class Program
    {
        public static void Main()
        {
            var hi = "hi world";
            Console.WriteLine(hi);
            int numbr = 5;
            Console.WriteLine(numbr);
            var record = new MyRecord(hi);
            Console.WriteLine(record);
        }
        public static string MakeString() => "string value";
    }

    public record MyRecord(string Value)
    {
        public static implicit operator MyRecord(int number) => new MyRecord(number);
    }
}
