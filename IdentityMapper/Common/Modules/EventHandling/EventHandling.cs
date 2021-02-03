using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaaLabs.IdentityMapper.Common.Modules.EventHandling
{
    class EventHandling : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<EventHandlers>();
            builder.RegisterModule<EventProducers>();
            builder.RegisterModule<EventConsumers>();
        }
    }
}