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
    class IncomingEvents : Module
    {
        protected override void AttachToComponentRegistration(IComponentRegistryBuilder componentRegistry, IComponentRegistration registration)
        {
            Type eventType;

            if (IsEventHandlerForEdgeHubIncomingEvent(registration, out eventType))
            {
                registration.PipelineBuilding += (sender, pipeline) =>
                {
                    pipeline.Use(PipelinePhase.Activation, MiddlewareInsertionMode.EndOfPhase, (c, next) =>
                    {
                        next(c);
                        var eventSetupFunction = typeof(IncomingEvents).GetMethod("SetupEdgeHubIncomingEvents").MakeGenericMethod(eventType);
                        eventSetupFunction.Invoke(this, new object[] { c });
                    });
                };
            }
        }

        public static void SetupEdgeHubIncomingEvents<T>(ResolveRequestContext context)
            where T : IEvent
        {
            var client = context.Resolve<IIotModuleClient>();
            RaaLabs.IdentityMapper.Common.EventHandling.EventHandler<T> eventHandler = (RaaLabs.IdentityMapper.Common.EventHandling.EventHandler<T>)context.Instance;
            var inputName = ((InputNameAttribute)typeof(T).GetCustomAttributes(typeof(InputNameAttribute), true).First()).InputName;

            client.SetInputMessageHandlerAsync(inputName, async (message, context) =>
            {
                return await HandleSubscriber(eventHandler, message);
            }, null);
        }

        async static Task<MessageResponse> HandleSubscriber<T>(RaaLabs.IdentityMapper.Common.EventHandling.EventHandler<T> eventHandler, Message message)
            where T : IEvent
        {
            try
            {
                var messageBytes = message.GetBytes();
                var messageString = Encoding.UTF8.GetString(messageBytes);
                var deserialized = JsonConvert.DeserializeObject<T>(messageString);
                eventHandler.Produce(deserialized);

                await Task.CompletedTask;
                return MessageResponse.Completed;
            }
            catch (Exception ex)
            {
                return MessageResponse.Abandoned;
            }
        }

        private static bool IsEventHandlerForEdgeHubIncomingEvent(IComponentRegistration registration, out Type eventType)
        {
            eventType = null;
            var eventHandlers = registration.Services
                .Where(s => s is IServiceWithType && typeof(IEventHandler).IsAssignableFrom(((IServiceWithType)s).ServiceType))
                .Select(s => ((IServiceWithType)s).ServiceType)
                .Where(eh => eh.GetGenericArguments().First().GetInterfaces().Any(i => typeof(IEdgeHubIncomingEvent).IsAssignableFrom(i)))
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
