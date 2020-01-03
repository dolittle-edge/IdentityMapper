/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using RaaLabs.TimeSeries.Modules;
using Dolittle.IO;
using Dolittle.Serialization.Json;

namespace RaaLabs.TimeSeries.IdentityMapper
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
        public RaaLabs.TimeSeries.TimeSeries GetTimeSeriesFor(Source source, Tag tag)
        {
            ThrowIfMissingSystem(source);
            ThrowIfTagIsMissingInSystem(source, tag);
            return _timeSeriesMap[source][tag];
        }

        /// <inheritdoc/>
        public bool HasTimeSeriesFor(Source source, Tag tag)
        {
            if( !_timeSeriesMap.ContainsKey(source)) return false;
            if( !_timeSeriesMap[source].ContainsKey(tag)) return false;
            return true;
        }

        void ThrowIfMissingSystem(Source source)
        {
            if( !_timeSeriesMap.ContainsKey(source)) throw new MissingSource(source);
        }

        void ThrowIfTagIsMissingInSystem(Source source, Tag tag)
        {
            if( !_timeSeriesMap[source].ContainsKey(tag)) throw new MissingTagInSource(source, tag);
        }
    }
}