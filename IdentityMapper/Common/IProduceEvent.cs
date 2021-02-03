using System.Threading.Tasks;

namespace RaaLabs.IdentityMapper.Common
{
    public interface IProduceEvent { }

    public interface IProduceEvent<T> : IProduceEvent
    {
    }

    public delegate void EventEmitter<in T>(T @event) where T: IEvent;
}
