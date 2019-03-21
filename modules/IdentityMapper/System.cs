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
    public class System : ConceptAs<string>
    {
        /// <summary>
        /// Implicitly convert from <see cref="string"/> to <see cref="System"/>
        /// </summary>
        /// <param name="value">System as string</param>
        public static implicit operator System(string value)
        {
            return new System {Value = value};
        }
    }
}
