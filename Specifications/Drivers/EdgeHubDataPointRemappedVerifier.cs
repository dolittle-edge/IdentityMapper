using RaaLabs.Edge.IdentityMapper.Events;
using TechTalk.SpecFlow;
using FluentAssertions;
using System.Globalization;

namespace RaaLabs.Edge.IdentityMapper.Specs.Drivers
{
    class EdgeHubDataPointRemappedVerifier : IProducedEventVerifier<EdgeHubDataPointRemapped>
    {
        public void VerifyFromTableRow(EdgeHubDataPointRemapped @event, TableRow row)
        {
            float actualValue = @event.Value;
            var expectedValue = float.Parse(row["Value"], CultureInfo.InvariantCulture.NumberFormat);
            @event.TimeSeries.Should().Be(row["TimeSeries"]);
            actualValue.Should().BeApproximately(expectedValue, 0.0001f);
        }
    }
}

