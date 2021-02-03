using Autofac;
using RaaLabs.IdentityMapper.Common;
using RaaLabs.IdentityMapper.Common.EventHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaaLabs.IdentityMapper.Common.Modules.EventHandling
{
    class EventHandlers : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var allEventTypes = TypeFinder.ImplementationsOf<IEvent>();
            var allEventHandlers = allEventTypes.Select(et => typeof(Common.EventHandling.EventHandler<>).MakeGenericType(et));
            allEventHandlers.ToList().ForEach(eh => builder.RegisterType(eh).AsSelf().InstancePerLifetimeScope());
        }
    }
}
