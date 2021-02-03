using Microsoft.Azure.Devices.Client;
using System.Threading.Tasks;

namespace RaaLabs.IdentityMapper.Common.Communication
{
    class IotHubHandler : IProduceEvent<IotEdgeMessage>
    {
        private readonly IIotModuleClient _client;

        public event EventEmitter<IotEdgeMessage> NewEdgeMessage;

        public IotHubHandler(IIotModuleClient iotModuleClient)
        {
            _client = iotModuleClient;
        }

        public async Task SendEvent<T>(T data)
        {
            await Task.CompletedTask;
        }

    }
}
