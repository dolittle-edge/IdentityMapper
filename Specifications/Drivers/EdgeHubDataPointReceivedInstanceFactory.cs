
using System.Globalization;
using RaaLabs.Edge.IdentityMapper.Events;
using TechTalk.SpecFlow;

namespace RaaLabs.Edge.IdentityMapper.Specs.Drivers
{
    class EdgeHubDataPointReceivedInstanceFactory : IEventInstanceFactory<EdgeHubDataPointReceived>
    {
        public EdgeHubDataPointReceived FromTableRow(TableRow row)
        {

            var edgeHubDataPointReceived = new Events.EdgeHubDataPointReceived
            {
                Source = row["Source"],
                Tag = row["Tag"],
                Timestamp = long.Parse(row["Timestamp"], CultureInfo.InvariantCulture.NumberFormat),
                Value = float.Parse(row["Value"], CultureInfo.InvariantCulture.NumberFormat)
            };
            return edgeHubDataPointReceived;
        }
    }
}
