/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using TechTalk.SpecFlow;
using System.Linq;
using FluentAssertions;
using RaaLabs.Edge.IdentityMapper.Specs.Drivers;
using Newtonsoft.Json;
using RaaLabs.Edge.IdentityMapper.Events;
using RaaLabs.Edge.Modules.EdgeHub;
using Autofac;
using System.Globalization;
using System;

namespace RaaLabs.Edge.IdentityMapper.Specs.StepDefinitions
{
    [Binding]
    public class SystemIntegrationSteps
    {
        private readonly ApplicationContext _appContext;

        public SystemIntegrationSteps(ApplicationContext appContext)
        {
            _appContext = appContext;
        }

        [Given(@"a config file with the following mapping")]
        public void GivenAConfigFileWithThefollowingMapping(Table table)
        {
            var mappings = table.Rows.Select(row => (Source: row["Source"], From: row["From"], To: Guid.Parse(row["To"])));
            var identitiesBySource = mappings.GroupBy(row => row.Source);
            var identitiesSource = identitiesBySource.ToDictionary(entry => entry.Key,
                                                                entry => entry.ToDictionary(entryForSource => entryForSource.From,
                                                                entryForSource => entryForSource.To)).ToDictionary(entry => entry.Key,
                                                                entry => new TimeSeriesByTag(entry.Value));

            var identities = new Identities(identitiesSource);
            var file = JsonConvert.SerializeObject(identities);
            _appContext.AddConfigurationFile("data/identities.json", file);
        }

        [Given(@"the application is running")]
        public void TheApplicationIsRunning()
        {
            _appContext.Run();
        }

        [When(@"the following events are received")]
        public void WhenTheFollonwingEventsAreReceived(Table table)
        {
            NullIotModuleClient client = (NullIotModuleClient)_appContext.ScopeHolder.Scope.Resolve<IIotModuleClient>();
            foreach (var row in table.Rows)
            {
                var incomingEvent = new EdgeHubDataPointReceived
                {
                    Source = row["Source"],
                    Tag = row["Tag"],
                    Value = float.Parse(row["Value"], CultureInfo.InvariantCulture.NumberFormat),
                    Timestamp = long.Parse(row["Timestamp"], CultureInfo.InvariantCulture.NumberFormat)
                };
                client.SimulateIncomingEvent("events", JsonConvert.SerializeObject(incomingEvent));
            }

        }

        [Then(@"the following events are produced")]
        public void ThenTheFollowingEventsAreProduced(Table table)
        {
            NullIotModuleClient client = (NullIotModuleClient)_appContext.ScopeHolder.Scope.Resolve<IIotModuleClient>();            
            foreach (var (sentMessage, expectedMessage) in client.MessagesSent.Zip(table.Rows))
            {
                var sent = JsonConvert.DeserializeObject<EdgeHubDataPointRemapped>(sentMessage.Item2);
                ((double)sent.Value).Should().BeApproximately(float.Parse(expectedMessage["Value"], CultureInfo.InvariantCulture.NumberFormat), 0.01);

            }
        }
    }
}

