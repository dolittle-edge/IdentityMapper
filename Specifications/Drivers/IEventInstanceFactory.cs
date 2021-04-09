
using TechTalk.SpecFlow;

namespace RaaLabs.Edge.IdentityMapper.Specs.Drivers
{
    interface IEventInstanceFactory<T>
    {
        public T FromTableRow(TableRow row);
    }
}
