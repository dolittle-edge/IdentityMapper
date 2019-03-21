/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Edge.IdentityMapper
{
    /// <summary>
    /// Exception that gets thrown when a <see cref="Tag"/> is missing in a <see cref="System"/>
    /// </summary>
    public class MissingTagInSystem : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MissingTagInSystem"/>
        /// </summary>
        /// <param name="system"><see cref="System"/> the <see cref="Tag"/> is missing</param>
        /// <param name="tag"><see cref="Tag"/> that is missing</param>
        public MissingTagInSystem(System system, Tag tag) : base($"Tag '{tag}' is missing in system '{system}'") {}
    }
}