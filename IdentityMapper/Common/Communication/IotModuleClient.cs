using Microsoft.Azure.Devices.Client;
using Serilog;
using System.Text;
using System.Threading.Tasks;

namespace RaaLabs.IdentityMapper.Common.Communication
{
    class IotModuleClient : IIotModuleClient
    {
        private ModuleClient _client;
        private readonly ILogger _logger;

        public IotModuleClient(ILogger logger)
        {
            _logger = logger;

            ModuleClient.CreateFromEnvironmentAsync(TransportType.Mqtt)
                .ContinueWith(_ => _client = _.Result)
                .Wait();
        }

        public Task SendEventAsync(string outputName, Message message)
        {
            var payload = Encoding.UTF8.GetString(message.GetBytes());
            _logger.Information("Payload to send: {Payload}", payload);

            return _client.SendEventAsync(outputName, message);
        }

        public Task SetInputMessageHandlerAsync(string inputName, MessageHandler messageHandler, object userContext)
        {
            return _client.SetInputMessageHandlerAsync(inputName, messageHandler, userContext);
        }


    }
}
