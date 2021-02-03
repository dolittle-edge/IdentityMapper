using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaaLabs.IdentityMapper.Common.EventHandling
{
    public class EventHandler<T> : IEventHandler
        where T: IEvent
    {
        private List<IConsumeEvent<T>> _observers;
        private List<Action<T>> _observerFunctions;
        public EventHandler()
        {
            _observers = new List<IConsumeEvent<T>>();
            _observerFunctions = new List<Action<T>>();
        }
        public IDisposable Subscribe(IConsumeEvent<T> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            return new Unsubscriber<T>(_observers, observer);
        }

        public IDisposable Subscribe(Action<T> observerFunction)
        {
            if (!_observerFunctions.Contains(observerFunction))
            {
                _observerFunctions.Add(observerFunction);
            }

            return new UnsubscriberFunction<T>(_observerFunctions, observerFunction);
        }

        public void Produce(T @event)
        {
            _observers.ForEach(_ => _.Handle(@event));
            _observerFunctions.ForEach(handlerFunction => handlerFunction(@event));
        }
    }

    public class Unsubscriber<T> : IDisposable
    {
        private List<IConsumeEvent<T>> _observers;
        private IConsumeEvent<T> _observer;
        internal Unsubscriber(List<IConsumeEvent<T>> observers, IConsumeEvent<T> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
            {
                _observers.Remove(_observer);
            }
        }
    }

    public class UnsubscriberFunction<T> : IDisposable
    {
        private List<Action<T>> _observerFunctions;
        private Action<T> _observerFunction;
        internal UnsubscriberFunction(List<Action<T>> observerFunctions, Action<T> observerFunction)
        {
            _observerFunctions = observerFunctions;
            _observerFunction = observerFunction;
        }

        public void Dispose()
        {
            if (_observerFunctions.Contains(_observerFunction))
            {
                _observerFunctions.Remove(_observerFunction);
            }
        }
    }

    interface IEventHandler { }
}
