using RaaLabs.Edge.Modules.EdgeHub;

namespace RaaLabs.IdentityMapper.events
{
    [OutputName("Translated")]
    class EdgeHubDataPointRemapped : IEdgeHubOutgoingEvent
    {
        public string Timeseries { get; set; }

        public dynamic Value { get; set; }

        public long Timestamp { get; set; }
    }
}
