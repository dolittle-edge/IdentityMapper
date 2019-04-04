/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Dolittle.Configuration;
using Dolittle.Edge.Modules;

namespace Dolittle.Edge.IdentityMapper
{
    /// <summary>
    /// Represents the configuration for timeseries and their relationship to systems and tags
    /// </summary>
    public class TimeSeriesMap : 
        ReadOnlyDictionary<ControlSystem, TimeSeriesByTag>,
        IConfigurationObject
    {
        /// <summary>
        /// Initializes a new instace of <see cref="TimeSeriesMap"/>
        /// </summary>
        /// <param name="timeSeriesByTag">Dictionary to initialize configuration with</param>
        public TimeSeriesMap(IDictionary<ControlSystem, TimeSeriesByTag> timeSeriesByTag) : base(timeSeriesByTag){}
    }
}