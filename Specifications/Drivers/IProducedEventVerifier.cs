
using TechTalk.SpecFlow;

namespace RaaLabs.Edge.IdentityMapper.Specs.Drivers
{
    interface IProducedEventVerifier<T>
    {
        public void VerifyFromTableRow(T @event, TableRow row);
    }
}
