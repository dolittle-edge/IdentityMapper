using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using Autofac.Core.Resolving.Pipeline;
using RaaLabs.IdentityMapper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RaaLabs.IdentityMapper.Common.Modules.EventHandling
{
    class EventProducers : Autofac.Module
    {

        protected override void AttachToComponentRegistration(IComponentRegistryBuilder componentRegistry, IComponentRegistration registration)
        {
            if (HasInterface<IProduceEvent>(registration))
            {
                registration.PipelineBuilding += (sender, pipeline) =>
                {
                    pipeline.Use(PipelinePhase.Activation, MiddlewareInsertionMode.EndOfPhase, (c, next) =>
                    {
                        next(c);
                        SetupEventProductionForProducer(c);
                    });
                };
            }
        }

        private void SetupEventProductionForProducer(ResolveRequestContext context)
        {
            var producer = (IProduceEvent)context.Instance;
            List<Type> allMessageTypesToProduce = producer.GetType().GetInterfaces()
                .Where(i => i.IsGenericType)
                .Where(i => i.GetGenericTypeDefinition() == typeof(IProduceEvent<>))
                .Select(i => i.GetGenericArguments().First())
                .ToList();

            var emitters = producer.GetType().GetEvents()
                .Where(i => i.EventHandlerType.IsGenericType)
                .Where(i => i.EventHandlerType.GetGenericTypeDefinition() == typeof(EventEmitter<>))
                .ToList();

            var messageTypeToEmitter = emitters.ToDictionary(em => em.EventHandlerType.GetGenericArguments().First(), em => em);
            if (!allMessageTypesToProduce.All(m => messageTypeToEmitter.ContainsKey(m)))
            {
                throw new Exception($"{producer.GetType().Name} is missing events for some messages it claims to produce.");
            }

            SetupEventEmitting(context, producer, messageTypeToEmitter);
        }

        private void SetupEventEmitting(IComponentContext context, IProduceEvent producer, Dictionary<Type, EventInfo> messageTypeToEmitter)
        {
            foreach (var (messageType, emitter) in messageTypeToEmitter)
            {
                var eventSetupFunction = typeof(EventProducers).GetMethod("SetupEventEmitter").MakeGenericMethod(messageType);
                eventSetupFunction.Invoke(this, new object[] { context, producer, emitter });
            }
        }

        public static void SetupEventEmitter<T>(IComponentContext context, IProduceEvent producer, EventInfo emitter)
            where T : IEvent
        {
            var eventHandler = (Common.EventHandling.EventHandler<T>)context.Resolve(typeof(Common.EventHandling.EventHandler<T>));
            var eventDelegate = Delegate.CreateDelegate(typeof(EventEmitter<T>), eventHandler, "Produce", false);
            emitter.AddEventHandler(producer, eventDelegate);
        }

        private bool HasInterface<T>(IComponentRegistration registration)
        {
            return registration.Services.Any(s => s is IServiceWithType && typeof(T).IsAssignableFrom(((IServiceWithType)s).ServiceType));
        }
    }
}
