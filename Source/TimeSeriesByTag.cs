/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RaaLabs.TimeSeries.Modules;

namespace RaaLabs.TimeSeries.IdentityMapper
{
    /// <summary>
    /// Represents the mapping between a <see cref="Tag"/> and a <see cref="RaaLabs.TimeSeries.TimeSeries"/>
    /// </summary>
    public class TimeSeriesByTag : ReadOnlyDictionary<Tag, RaaLabs.TimeSeries.TimeSeries>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TimeSeriesByTag"/>
        /// </summary>
        /// <param name="configuration">Configuration</param>
        public TimeSeriesByTag(IDictionary<Tag, RaaLabs.TimeSeries.TimeSeries> configuration) : base(configuration) {}
    }
}