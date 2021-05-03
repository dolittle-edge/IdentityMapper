// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace RaaLabs.Edge.IdentityMapper
{
    public class MissingSourceException : Exception
    {
        /// <summary>
        /// Initalizes a new instance of <see cref="MissingSourceException"/>
        /// </summary>
        /// <param name="source"><see cref="Source"/> that does not exist</param>
        public MissingSourceException(string source) : base($"Source '{source}' does not exist") { }
    }
}