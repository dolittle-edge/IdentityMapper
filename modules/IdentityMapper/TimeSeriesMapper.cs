/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.TimeSeries.Modules;
using Dolittle.IO;
using Dolittle.Serialization.Json;

namespace Dolittle.TimeSeries.IdentityMapper
{
    /// <summary>
    /// Represents an implementation of <see cref="ITimeSeriesMapper"/>
    /// </summary>
    public class TimeSeriesMapper : ITimeSeriesMapper
    {
        readonly TimeSeriesMap _timeSeriesMap;

        /// <summary>
        /// Initializes a new instance of <see cref="TimeSeriesMapper"/>
        /// </summary>
        /// <param name="timeSeriesMap"><see cref="TimeSeriesMap"/></param>
        public TimeSeriesMapper(TimeSeriesMap timeSeriesMap)
        {
            _timeSeriesMap = timeSeriesMap;
        }

        /// <inheritdoc/>
        public Dolittle.TimeSeries.Modules.TimeSeries GetTimeSeriesFor(ControlSystem controlSystem, Tag tag)
        {
            ThrowIfMissingSystem(controlSystem);
            ThrowIfTagIsMissingInSystem(controlSystem, tag);
            return _timeSeriesMap[controlSystem][tag];
        }

        /// <inheritdoc/>
        public bool HasTimeSeriesFor(ControlSystem controlSystem, Tag tag)
        {
            if( !_timeSeriesMap.ContainsKey(controlSystem)) return false;
            if( !_timeSeriesMap[controlSystem].ContainsKey(tag)) return false;
            return true;
        }

        void ThrowIfMissingSystem(ControlSystem controlSystem)
        {
            if( !_timeSeriesMap.ContainsKey(controlSystem)) throw new MissingSystem(controlSystem);
        }

        void ThrowIfTagIsMissingInSystem(ControlSystem system, Tag tag)
        {
            if( !_timeSeriesMap[system].ContainsKey(tag)) throw new MissingTagInSystem(system, tag);
        }
    }
}