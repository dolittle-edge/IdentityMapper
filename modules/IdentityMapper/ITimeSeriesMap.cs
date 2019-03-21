/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Dolittle.Edge.IdentityMapper
{
    /// <summary>
    /// Defines the map between <see cref="System"/> with <see cref="Tag">tags</see> and <see cref="TimeSeries"/>
    /// </summary>
    public interface ITimeSeriesMap
    {
        /// <summary>
        /// Get the map
        /// </summary>
        IDictionary<System, IDictionary<Tag,TimeSeries>>   Get();
    }
}