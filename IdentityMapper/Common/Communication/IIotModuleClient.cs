using Microsoft.Azure.Devices.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaaLabs.IdentityMapper.Common.Communication
{
    interface IIotModuleClient
    {
        public Task SetInputMessageHandlerAsync(string inputName, MessageHandler messageHandler, object userContext);
        public Task SendEventAsync(string outputName, Message message);

    }
}
