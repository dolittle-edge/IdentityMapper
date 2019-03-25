/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Edge.Modules;
using Dolittle.IO;
using Dolittle.Serialization.Json;

namespace Dolittle.Edge.IdentityMapper
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
        /// <param name="timeSeriesMap"></param>
        public TimeSeriesMapper(TimeSeriesMap timeSeriesMap)
        {
            _timeSeriesMap = timeSeriesMap;
        }

        /// <inheritdoc/>
        public TimeSeries GetTimeSeriesFor(System system, Tag tag)
        {
            ThrowIfMissingSystem(system);
            ThrowIfTagIsMissingInSystem(system, tag);
            return _timeSeriesMap[system][tag];
        }

        /// <inheritdoc/>
        public bool HasTimeSeriesFor(System system, Tag tag)
        {
            if( !_timeSeriesMap.ContainsKey(system)) return false;
            if( !_timeSeriesMap[system].ContainsKey(tag)) return false;
            return true;
        }

        void ThrowIfMissingSystem(System system)
        {
            if( !_timeSeriesMap.ContainsKey(system)) throw new MissingSystem(system);
        }

        void ThrowIfTagIsMissingInSystem(System system, Tag tag)
        {
            if( !_timeSeriesMap[system].ContainsKey(tag)) throw new MissingTagInSystem(system, tag);
        }
    }
}