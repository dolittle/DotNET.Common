// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Net5
{
    public record AdvancedRecord(string FirstName, string LastName = default)
    {
        /// <summary>
        /// Gets a new <see cref="AdvancedRecord" /> with a basic name.
        /// </summary>
        public static AdvancedRecord BasicName => new("Basic Name");

        /// <summary>
        /// Gets a value indicating whether the first name is set.
        /// </summary>
        public bool IsSet => bool.Parse(FirstName);
    }
}
