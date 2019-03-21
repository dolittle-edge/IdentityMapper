/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.IO;
using Dolittle.Serialization.Json;

namespace Dolittle.Edge.IdentityMapper
{
    /// <summary>
    /// Represents an implementation of <see cref="ITimeSeriesMap"/>
    /// </summary>
    public class TimeSeriesMap : ITimeSeriesMap
    {
        readonly IDictionary<System, IDictionary<Tag,TimeSeries>> _map;

        /// <summary>
        /// Initializes a new instance of <see cref="TimeSeriesMapper"/>
        /// </summary>
        /// <param name="fileSystem"><see cref="IFileSystem"/> to use</param>
        /// <param name="serializer"><see cref="ISerializer">JSON serializer</see> to use</param>
        public TimeSeriesMap(IFileSystem fileSystem, ISerializer serializer)
        {
            var json = fileSystem.ReadAllText("./data/identities.json");
            _map = serializer.FromJson<IDictionary<System, IDictionary<Tag,TimeSeries>>>(json);
        }

        /// <inheritdoc/>
        public IDictionary<System, IDictionary<Tag,TimeSeries>> Get()
        {
            return _map;
        }
    }
}