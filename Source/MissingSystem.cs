/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.TimeSeries.Modules;

namespace Dolittle.TimeSeries.IdentityMapper
{
    /// <summary>
    /// Exception that gets thrown when a <see cref="ControlSystem"/> does not exist
    /// </summary>
    public class MissingSystem : Exception
    {
        /// <summary>
        /// Initalizes a new instance of <see cref="MissingSystem"/>
        /// </summary>
        /// <param name="controlSystem"><see cref="ControlSystem"/> that does not exist</param>
        public MissingSystem(ControlSystem controlSystem) : base($"System '{controlSystem}' does not exist") { }
    }
}