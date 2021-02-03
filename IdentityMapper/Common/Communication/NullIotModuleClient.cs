using Microsoft.Azure.Devices.Client;
using Serilog;
using System.Text;
using System.Threading.Tasks;

namespace RaaLabs.IdentityMapper.Common.Communication
{
    class NullIotModuleClient : IIotModuleClient
    {
        private readonly ILogger _logger;

        public NullIotModuleClient(ILogger logger)
        {
            _logger = logger;
        }

        public Task SendEventAsync(string outputName, Message message)
        {
            var payload = Encoding.UTF8.GetString(message.GetBytes());
            _logger.Information("Payload to send: {Payload}", payload);
            return Task.CompletedTask;
        }

        public Task SetInputMessageHandlerAsync(string inputName, MessageHandler messageHandler, object userContext)
        {
            return Task.CompletedTask;
        }
    }
}
