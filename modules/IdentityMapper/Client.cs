/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Client.Transport.Mqtt;
using Dolittle.Lifecycle;
using Dolittle.Logging;

namespace Dolittle.Edge.IdentityMapper
{
    /// <summary>
    /// Represents an implementation of <see cref="IClient"/>
    /// </summary>
    [Singleton]
    public class Client : IClient
    {
        ModuleClient _client;

        /// <summary>
        /// Initializes a new instance of <see cref="Client"/>
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public Client(ILogger logger)
        {
            logger.Information("Setting up ModuleClient");

            var mqttSetting = new MqttTransportSettings(TransportType.Mqtt_Tcp_Only);
            ITransportSettings[] settings = { mqttSetting };

            _client = null;

            ModuleClient.CreateFromEnvironmentAsync(settings)
                .ContinueWith(_ => _client = _.Result)
                .Wait();

            logger.Information("Open and wait");
            _client.OpenAsync().Wait();
            logger.Information("Client is ready");
        }


        /// <inheritdoc/>
        public Task SendEvent(Output output, Message message)
        {
            return _client.SendEventAsync(output, message);
        }

        /// <inheritdoc/>
        public Task SetInputMessageHandler(Input input, MessageHandler messageHandler, object userContext)
        {
            return SetInputMessageHandler(input, messageHandler, userContext);
        }
    }
}