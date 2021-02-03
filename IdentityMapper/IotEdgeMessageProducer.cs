using IdentityMapper.events;
using RaaLabs.IdentityMapper.Common;

namespace RaaLabs.IdentityMapper
{
    class IotEdgeMessageProducer: IProduceEvent<EdgeHubDataPointReceived>
    {

        public event EventEmitter<EdgeHubDataPointReceived> IotEdgeMessageProduced;
        public IotEdgeMessageProducer()
        {
        }

        public void ProduceMessage()
        {
            IotEdgeMessageProduced(new EdgeHubDataPointReceived
            {
                Source = "KChief",
                Tag = "ME RPM",
                Timestamp = 0,
                Value = 1.3
            });
        }
    }
}
