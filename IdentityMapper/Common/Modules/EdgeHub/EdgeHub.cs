using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using Autofac.Core.Resolving.Pipeline;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using RaaLabs.IdentityMapper.Common;
using RaaLabs.IdentityMapper.Common.Communication;
using RaaLabs.IdentityMapper.Common.EventHandling;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaaLabs.IdentityMapper.Common.Modules.EdgeHub
{
    class EdgeHub : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            if (IotEdgeHelpers.IsRunningInIotEdge())
            {
                builder.RegisterType<IotModuleClient>().As<IIotModuleClient>();
            }
            else
            {
                builder.RegisterType<NullIotModuleClient>().As<IIotModuleClient>();
            }

            builder.RegisterModule<IncomingEvents>();
            builder.RegisterModule<OutgoingEvents>();
        }
    }
}
