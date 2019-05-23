/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Dolittle.TimeSeries.Modules;

namespace Dolittle.TimeSeries.IdentityMapper
{
    /// <summary>
    /// Represents the mapping between a <see cref="Tag"/> and a <see cref="Dolittle.TimeSeries.Modules.TimeSeries"/>
    /// </summary>
    public class TimeSeriesByTag : ReadOnlyDictionary<Tag, Dolittle.TimeSeries.Modules.TimeSeries>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TimeSeriesByTag"/>
        /// </summary>
        /// <param name="configuration">Configuration</param>
        public TimeSeriesByTag(IDictionary<Tag, Dolittle.TimeSeries.Modules.TimeSeries> configuration) : base(configuration) {}
    }
}