/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
namespace Dolittle.Edge.IdentityMapper
{
    /// <summary>
    /// Exception that gets thrown when a <see cref="System"/> does not exist
    /// </summary>
    public class MissingSystem : Exception
    {
        /// <summary>
        /// Initalizes a new instance of <see cref="MissingSystem"/>
        /// </summary>
        /// <param name="system"><see cref="System"/> that does not exist</param>
        public MissingSystem(System system) : base($"System '{system}' does not exist") { }
    }
}