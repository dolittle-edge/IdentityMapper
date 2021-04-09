using BoDi;
using RaaLabs.Edge.Modules.EventHandling;
using RaaLabs.Edge.IdentityMapper.Specs.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TechTalk.SpecFlow;
using FluentAssertions;

namespace RaaLabs.Edge.IdentityMapper.Specs.Steps
{
    [Binding]
    public sealed class HandlerSteps
    {
        private IObjectContainer _container;
        private TypeMapping _typeMapping;
        private IConsumeEvent _handler;
        private Dictionary<Type, List<IEvent>> _emittedEvents;

        public HandlerSteps(IObjectContainer container, TypeMapping typeMapping)
        {
            _container = container;
            _typeMapping = typeMapping;
        }

        [Given(@"a handler of type (.*)")]
        void GivenAHandlerOfType(string typename)
        {
            var type = _typeMapping[typename];
            _handler = (IConsumeEvent) _container.Resolve(type);
            _emittedEvents = new Dictionary<Type, List<IEvent>>();
            var eventsEmittedByHandler = type.GetInterfaces()
                .Where(i => i.IsAssignableTo(typeof(IProduceEvent)))    // All interfaces that are descendants of IProduceEvent
                .Select(i => i.GetGenericArguments().First());          // Get the generic type argument of the interface

            var emitters = type.GetEvents()
                .Where(i => i.EventHandlerType.IsGenericType)
                .Where(i => i.EventHandlerType.GetGenericTypeDefinition() == typeof(EventEmitter<>))
                .ToList();

            emitters.ForEach(emitter => SetupEventListeningForEmitter(emitter, _handler));
        }

        [When(@"the following events of type (.*) is produced")]
        void WhenTheFollowingEventsOfTypeIsProduced(string typename, Table table)
        {
            var eventType = _typeMapping[typename];
            GetType().GetMethod("HandleEventsFromTable").MakeGenericMethod(eventType).Invoke(this, new object[] { table });
        }

        [Then(@"the following events of type (.*) is produced")]
        void ThenTheFollowingEventsOfTypeIsProduced(string typename, Table table)
        {
            var eventType = _typeMapping[typename];
            GetType().GetMethod("VerifyEventsProducedFromTable").MakeGenericMethod(eventType).Invoke(this, new object[] { table });

        }

        private void SetupEventListeningForEmitter(EventInfo emitter, IConsumeEvent handler)
        {
            var eventType = emitter.EventHandlerType.GetGenericArguments().First();
            _emittedEvents[eventType] = new List<IEvent>();
            Delegate del = Delegate.CreateDelegate(emitter.EventHandlerType, this, GetType().GetMethod("HandleEvent").MakeGenericMethod(eventType));
            emitter.AddEventHandler(handler, del);
        }

        public void HandleEvent<T>(T @event) where T: IEvent
        {
            _emittedEvents[typeof(T)].Add(@event);
        }

        public void HandleEventsFromTable<T>(Table table)
            where T: IEvent
        {
            var eventTypeFactory = _container.Resolve<IEventInstanceFactory<T>>();
            var handler = (IConsumeEvent<T>) _handler;
            foreach (var row in table.Rows)
            {
                var @event = eventTypeFactory.FromTableRow(row);
                handler.Handle(@event);
            }
        }

        public void VerifyEventsProducedFromTable<T>(Table table)
            where T: IEvent
        {
            var verifier = _container.Resolve<IProducedEventVerifier<T>>();
            var emittedEvents = _emittedEvents[typeof(T)].Select(_ => (T) _).ToList();
           
            emittedEvents.Count.Should().Be(table.Rows.Count);
            foreach (var (@event, expected) in emittedEvents.Zip(table.Rows))
            {
                verifier.VerifyFromTableRow(@event, expected);
            }
        }
    }
}
