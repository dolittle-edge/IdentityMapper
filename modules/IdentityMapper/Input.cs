/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Dolittle.Edge.IdentityMapper
{
    /// <summary>
    /// Represents the concept of an Input
    /// </summary>
    public class Input : ConceptAs<string>
    {
        /// <summary>
        /// Implicitly convert from <see cref="string"/> to <see cref="Input"/>
        /// </summary>
        /// <param name="value">Input as string</param>
        public static implicit operator Input(string value)
        {
            return new Input {Value = value};
        }
    }
}
