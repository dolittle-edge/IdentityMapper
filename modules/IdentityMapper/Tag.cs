/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Dolittle.Edge.IdentityMapper
{
    /// <summary>
    /// Represents the concept of an System
    /// </summary>
    public class Tag : ConceptAs<string>
    {
        /// <summary>
        /// Implicitly convert from <see cref="string"/> to <see cref="Tag"/>
        /// </summary>
        /// <param name="value">Tag as string</param>
        public static implicit operator Tag(string value)
        {
            return new Tag {Value = value};
        }
    }
}
