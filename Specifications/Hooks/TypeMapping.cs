
using RaaLabs.Edge.Testing;
using TechTalk.SpecFlow;

namespace RaaLabs.Edge.IdentityMapper.Specs.Drivers
{

    [Binding]
    public sealed class TypeMapperProvider
    {
        private readonly TypeMapping _typeMapping;

        public TypeMapperProvider(TypeMapping typeMapping)
        {
            _typeMapping = typeMapping;
        }

        [BeforeScenario]
        public void SetupTypes()
        {
            _typeMapping.Add("IdentityMapperHandler", typeof(IdentityMapperHandler));
            _typeMapping.Add("EdgeHubDataPointReceived", typeof(Events.EdgeHubDataPointReceived));
            _typeMapping.Add("EdgeHubDataPointRemapped", typeof(Events.EdgeHubDataPointRemapped));
        }
    }
}


