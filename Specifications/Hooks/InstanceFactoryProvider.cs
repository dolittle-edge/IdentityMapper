using BoDi;
using RaaLabs.Edge.IdentityMapper.events;
using RaaLabs.Edge.IdentityMapper.Specs.Drivers;
using TechTalk.SpecFlow;

namespace RaaLabs.Edge.IdentityMapper.Specs.Hooks
{
    [Binding]
    class InstanceFactoryProvider
    {
        private readonly IObjectContainer _container;

        public InstanceFactoryProvider(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public void SetupLogger()
        {
            _container.RegisterTypeAs<EdgeHubDataPointReceivedInstanceFactory, IEventInstanceFactory<EdgeHubDataPointReceived>>();
            _container.RegisterTypeAs<EdgeHubDataPointRemappedVerifier, IProducedEventVerifier<EdgeHubDataPointRemapped>>();
        }

    }
}
