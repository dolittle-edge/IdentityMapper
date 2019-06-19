/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.TimeSeries.Modules;

namespace Dolittle.TimeSeries.IdentityMapper
{
    /// <summary>
    /// Exception that gets thrown when a <see cref="Source"/> does not exist
    /// </summary>
    public class MissingSource : Exception
    {
        /// <summary>
        /// Initalizes a new instance of <see cref="MissingSource"/>
        /// </summary>
        /// <param name="source"><see cref="Source"/> that does not exist</param>
        public MissingSource(Source source) : base($"System '{source}' does not exist") { }
    }
}