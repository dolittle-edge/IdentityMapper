/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Dolittle.Edge.IdentityMapper
{
    /// <summary>
    /// Represents the concept of an Output
    /// </summary>
    public class Output : ConceptAs<string>
    {
        /// <summary>
        /// Implicitly convert from <see cref="string"/> to <see cref="Output"/>
        /// </summary>
        /// <param name="value">Output as string</param>
        public static implicit operator Output(string value)
        {
            return new Output {Value = value};
        }
    }
}
