/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace RaaLabs.IdentityMapper
{
   public class MissingSource : Exception
    {
        /// <summary>
        /// Initalizes a new instance of <see cref="MissingSource"/>
        /// </summary>
        /// <param name="source"><see cref="Source"/> that does not exist</param>
        public MissingSource(string source) : base($"Source '{source}' does not exist") { }
    }
}