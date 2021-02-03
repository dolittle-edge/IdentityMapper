using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using Autofac.Core.Resolving.Pipeline;
using RaaLabs.IdentityMapper.Common;
using RaaLabs.IdentityMapper.Common.EventHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RaaLabs.IdentityMapper.Common.Modules.EventHandling
{
    class EventConsumers : Autofac.Module
    {
        protected override void AttachToComponentRegistration(IComponentRegistryBuilder componentRegistry, IComponentRegistration registration)
        {
            if (HasInterface<IConsumeEvent>(registration))
            {
                registration.PipelineBuilding += (sender, pipeline) =>
                {
                    pipeline.Use(PipelinePhase.Activation, MiddlewareInsertionMode.EndOfPhase, (c, next) =>
                    {
                        next(c);
                        SetupEventConsumptionForConsumer(c);
                    });
                };
            }
        }

        private void SetupEventConsumptionForConsumer(ResolveRequestContext context)
        {
            var consumer = (IConsumeEvent) context.Instance;
            List<Type> allMessageTypesToConsume = consumer.GetType().GetInterfaces()
                .Where(i => i.IsGenericType)
                .Where(i => i.GetGenericTypeDefinition() == typeof(IConsumeEvent<>))
                .Select(i => i.GetGenericArguments().First())
                .ToList();

            SetupSubscriptions(context, consumer, allMessageTypesToConsume);
        }

        private void SetupSubscriptions(IComponentContext context, IConsumeEvent consumer, List<Type> messageTypesToConsume)
        {
            foreach (var messageType in messageTypesToConsume)
            {
                var subscribeFunction = typeof(EventConsumers).GetMethod("SubscribeToEvent").MakeGenericMethod(messageType);
                var unsubscriber = subscribeFunction.Invoke(this, new object[] { context, consumer });
            }
        }

        public static Unsubscriber<T> SubscribeToEvent<T>(IComponentContext context, IConsumeEvent<T> consumer)
            where T : IEvent
        {
            var eventHandler = (Common.EventHandling.EventHandler<T>)context.Resolve(typeof(Common.EventHandling.EventHandler<T>));
            return (Unsubscriber<T>)eventHandler.Subscribe(consumer);
        }

        private bool HasInterface<T>(IComponentRegistration registration)
        {
            return registration.Services.Any(s => s is IServiceWithType && typeof(T).IsAssignableFrom(((IServiceWithType)s).ServiceType));
        }
    }
}
