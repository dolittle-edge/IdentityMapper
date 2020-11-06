/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Dolittle.Configuration;
using RaaLabs.TimeSeries.Modules;

namespace RaaLabs.TimeSeries.IdentityMapper
{
    /// <summary>
    /// Represents the configuration for timeseries and their relationship to systems and tags
    /// </summary>
    [Name("identities")]
    public class TimeSeriesMap : 
        ReadOnlyDictionary<Source, TimeSeriesByTag>,
        IConfigurationObject,
        ITriggerAppRestartOnChange
    {
        /// <summary>
        /// Initializes a new instace of <see cref="TimeSeriesMap"/>
        /// </summary>
        /// <param name="timeSeriesByTag">Dictionary to initialize configuration with</param>
        public TimeSeriesMap(IDictionary<Source, TimeSeriesByTag> timeSeriesByTag) : base(timeSeriesByTag){}
    }
}