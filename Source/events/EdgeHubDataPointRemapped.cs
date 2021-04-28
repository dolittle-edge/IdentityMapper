using System.Diagnostics.CodeAnalysis;
using RaaLabs.Edge.Modules.EdgeHub;

namespace RaaLabs.Edge.IdentityMapper.Events
{
    [ExcludeFromCodeCoverage]
    [OutputName("Translated")]
    public class EdgeHubDataPointRemapped : IEdgeHubOutgoingEvent
    {
        public string Timeseries { get; set; }

        public dynamic Value { get; set; }

        public long Timestamp { get; set; }
    }
}
