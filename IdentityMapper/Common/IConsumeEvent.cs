using System;
using System.Threading.Tasks;

namespace RaaLabs.IdentityMapper.Common
{
    public interface IConsumeEvent { }

    public interface IConsumeEvent<T> : IConsumeEvent
    {
        public void Handle(T @event);

    }
}
