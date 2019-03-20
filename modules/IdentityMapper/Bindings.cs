/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.DependencyInversion;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Client.Transport.Mqtt;

namespace IdentityMapper
{
    /// <summary>
    /// Provides bindings
    /// </summary>
    public class Bindings : ICanProvideBindings
    {
        /// <inheritdoc/>
        public void Provide(IBindingProviderBuilder builder)
        {
            var mqttSetting = new MqttTransportSettings(TransportType.Mqtt_Tcp_Only);
            ITransportSettings[] settings = { mqttSetting };

            ModuleClient client = null;

            ModuleClient.CreateFromEnvironmentAsync(settings)
                .ContinueWith(_ => client = _.Result)
                .Wait();

            client.OpenAsync().Wait();

            builder.Bind<ModuleClient>().To(client);
        }
    }
}