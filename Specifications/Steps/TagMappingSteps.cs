/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using TechTalk.SpecFlow;
using System.Linq;
using BoDi;
using Serilog;

namespace RaaLabs.Edge.IdentityMapper.Specs.StepDefinitions
{
    [Binding]
    public class TagMappingSteps
    {
        private readonly IObjectContainer _container;

        public TagMappingSteps(IObjectContainer container)
        {
            _container = container;
            var logger = new LoggerConfiguration().CreateLogger();
            _container.RegisterInstanceAs<ILogger>(logger); 
        }

        [Given(@"mappings between the following tags")]
        public void GivenMappingsBetweenTheFollowingTags(Table table)
        {
            var mappings = table.Rows.Select(row => (Source: row["Source"], From: row["From"], To: row["To"]));
            var identitiesBySource = mappings.GroupBy(row => row.Source);
            var identitiesSource = identitiesBySource.ToDictionary(entry => entry.Key,
                                                                entry => entry.ToDictionary(entryForSource => entryForSource.From,
                                                                entryForSource => entryForSource.To)).ToDictionary(entry => entry.Key, 
                                                                entry => new TimeSeriesByTag(entry.Value));
            
            var identities = new Identities(identitiesSource);
            _container.RegisterInstanceAs<Identities>(identities); 
        }

        // TODO: write test for Handler

        [Given(@"timeseries mapper with the following mapping")]
        public void GivenTimeseriesMapperWithTheFollowingMapping(Table table)
        {
            var mappings = table.Rows.Select(row => (Source: row["Source"], From: row["From"], To: row["To"]));
            var identitiesBySource = mappings.GroupBy(row => row.Source);
            var identitiesSource = identitiesBySource.ToDictionary(entry => entry.Key,
                                                                entry => entry.ToDictionary(entryForSource => entryForSource.From,
                                                                entryForSource => entryForSource.To)).ToDictionary(entry => entry.Key, 
                                                                entry => new TimeSeriesByTag(entry.Value));
            
            var identities = new Identities(identitiesSource);
            var mapper = new TimeSeriesMapper(identities);
            _container.RegisterInstanceAs<TimeSeriesMapper>(mapper); 
        }
    }
}
