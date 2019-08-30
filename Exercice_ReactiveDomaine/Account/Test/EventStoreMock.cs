using ReactiveDomain;
using ReactiveDomain.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAccountBalance;

namespace Test
{
    public class EventStoreMock : IRepository
    {
        public Dictionary<Guid, List<object> > dict = new Dictionary<Guid, List<object>>();
        public IEventSource GetbyID(Guid id)
        {
            if (dict.Count == 0) return null;
            var acc = new AccountBalance();
            acc.RestoreFromEvents(dict[id]);
            return acc;
        }

        public void Save(IEventSource aggregate)
        {
            if (dict.Count == 0) dict.Add(aggregate.Id, aggregate.TakeEvents().OfType<object>().ToList());
            else
            {
                foreach (var elem in aggregate.TakeEvents())
                {
                    dict[aggregate.Id].Add(elem);
                }
            }
        }


        public void UpdateToCurrent(IEventSource aggregate)
        {
            throw new NotImplementedException();
        }

        TAggregate IRepository.GetById<TAggregate>(Guid id)
        {
            throw new NotImplementedException();
        }

        TAggregate IRepository.GetById<TAggregate>(Guid id, int version)
        {
            throw new NotImplementedException();
        }

        bool IRepository.TryGetById<TAggregate>(Guid id, out TAggregate aggregate)
        {
            if (dict.Count == 0)
            {
                aggregate = null;
                return false;
            }
            var acc = new AccountBalance();
            acc.RestoreFromEvents(dict[id]);
            dynamic d = acc;
            aggregate = d;
            return true;
        }

        bool IRepository.TryGetById<TAggregate>(Guid id, int version, out TAggregate aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
