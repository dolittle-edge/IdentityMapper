/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Edge.Modules;

namespace Dolittle.Edge.IdentityMapper
{
    /// <summary>
    /// Defines a system that can translate a tag for a specific system into a timeseries identifier
    /// </summary>
    public interface ITimeSeriesMapper
    {
        /// <summary>
        /// Check if there is a <see cref="TimeSeries"/> for a <see cref="Tag"/> in a <see cref="System"/>
        /// </summary>
        /// <param name="controlSystem"><see cref="ControlSystem"/> the <see cref="Tag"/> belongs to</param>
        /// <param name="tag">The actual <see cref="Tag"/></param>
        /// <returns>True if it exists, false if not</returns>
        bool HasTimeSeriesFor(ControlSystem controlSystem, Tag tag);        

        /// <summary>
        /// Get the <see cref="TimeSeries"/> for a <see cref="Tag"/> in a <see cref="System"/>
        /// </summary>
        /// <param name="controlSystem"><see cref="ControlSystem"/> the <see cref="Tag"/> belongs to</param>
        /// <param name="tag">The actual <see cref="Tag"/></param>
        /// <returns><see cref="TimeSeries"/></returns>
        TimeSeries GetTimeSeriesFor(ControlSystem controlSystem, Tag tag);        
    }
}