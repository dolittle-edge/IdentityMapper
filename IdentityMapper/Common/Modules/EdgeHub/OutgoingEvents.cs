using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using Autofac.Core.Resolving.Pipeline;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using RaaLabs.IdentityMapper.Common;
using RaaLabs.IdentityMapper.Common.Communication;
using RaaLabs.IdentityMapper.Common.EventHandling;
using Serilog;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaaLabs.IdentityMapper.Common.Modules.EdgeHub
{
    class OutgoingEvents : Module
    {
        protected override void AttachToComponentRegistration(IComponentRegistryBuilder componentRegistry, IComponentRegistration registration)
        {
            Type eventType;

            if (IsEventHandlerForEdgeHubOutgoingEvent(registration, out eventType))
            {
                registration.PipelineBuilding += (sender, pipeline) =>
                {
                    pipeline.Use(PipelinePhase.Activation, MiddlewareInsertionMode.EndOfPhase, (c, next) =>
                    {
                        next(c);
                        var eventSetupFunction = typeof(OutgoingEvents).GetMethod("SetupEdgeHubOutgoingEvents").MakeGenericMethod(eventType);
                        eventSetupFunction.Invoke(this, new object[] { c });
                    });
                };
            }
        }

        public static void SetupEdgeHubOutgoingEvents<T>(ResolveRequestContext context)
            where T : IEvent
        {
            var client = context.Resolve<IIotModuleClient>();
            var logger = context.Resolve<ILogger>();
            RaaLabs.IdentityMapper.Common.EventHandling.EventHandler<T> eventHandler = (RaaLabs.IdentityMapper.Common.EventHandling.EventHandler<T>)context.Instance;
            var outputName = ((OutputNameAttribute)typeof(T).GetCustomAttributes(typeof(OutputNameAttribute), true).First()).OutputName;
            eventHandler.Subscribe(async message =>
            {
                var outputString = JsonConvert.SerializeObject(message);
                var outputBytes = Encoding.UTF8.GetBytes(outputString);
                var outputMessage = new Message(outputBytes);

                await client.SendEventAsync(outputName, outputMessage);
            });
        }

        private static bool IsEventHandlerForEdgeHubOutgoingEvent(IComponentRegistration registration, out Type eventType)
        {
            eventType = null;
            var eventHandlers = registration.Services
                .Where(s => s is IServiceWithType && typeof(IEventHandler).IsAssignableFrom(((IServiceWithType)s).ServiceType))
                .Select(s => ((IServiceWithType)s).ServiceType)
                .Where(eh => eh.GetGenericArguments().First().GetInterfaces().Any(i => typeof(IEdgeHubOutgoingEvent).IsAssignableFrom(i)))
                .ToList();

            if (eventHandlers.Count == 0)
            {
                return false;
            }

            eventType = eventHandlers.Select(eh => eh.GetGenericArguments().First()).First();

            return true;
        }
    }
}
