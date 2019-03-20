/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using Dolittle.IO;
using Dolittle.Serialization.Json;

namespace IdentityMapper
{

    /// <summary>
    /// Represents an implementation of <see cref="ITimeSeriesMapper"/>
    /// </summary>
    public class TimeSeriesMapper : ITimeSeriesMapper
    {
        readonly IDictionary<System, IDictionary<Tag, TimeSeries>> _map;

        /// <summary>
        /// Initializes a new instance of <see cref="TimeSeriesMapper"/>
        /// </summary>
        /// <param name="timeSeriesMap"></param>
        public TimeSeriesMapper(ITimeSeriesMap timeSeriesMap)
        {
            _map = timeSeriesMap.Get();

        }

        /// <inheritdoc/>
        public TimeSeries GetTimeSeriesFor(System system, Tag tag)
        {
            throw new global::System.NotImplementedException();
        }

        /// <inheritdoc/>
        public bool HasTimeSeriesFor(System system, Tag tag)
        {
            throw new global::System.NotImplementedException();
        }
    }
}