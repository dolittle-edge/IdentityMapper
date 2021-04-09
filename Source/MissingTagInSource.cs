/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace RaaLabs.Edge.IdentityMapper
{
    /// <summary>
    /// Exception that gets thrown when a <see cref="Tag"/> is missing in a <see cref="Source"/>
    /// </summary>
    public class MissingTagInSourceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MissingTagInSourceException"/>
        /// </summary>
        /// <param name="source"><see cref="Source"/> the <see cref="Tag"/> is missing</param>
        /// <param name="tag"><see cref="Tag"/> that is missing</param>
        public MissingTagInSourceException(string source, string tag) : base($"Tag '{tag}' is missing in source '{source}'") {}
    }
}