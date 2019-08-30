using DDD.DomainModel;
using DDD.DomainService;
using System;
using System.Collections.Generic;

namespace Tests
{
    public class EventStoreMock : IRepository<AccountBalance>
    {
        public Dictionary<Guid,List<Event>> dict = new Dictionary<Guid, List<Event>>();
        public AggregateRoot GetbyID(Guid id)
        {
            if (dict.Count == 0) return null;
            var acc = new AccountBalance();
            var list = dict[id];
            foreach(var elem in list)
            {
                dynamic c = elem;
                acc.SetState(c);
            }

            return acc;
        }

        public void SaveEvents(AggregateRoot agg)
        {
            if (dict.Count == 0) dict.Add(agg.Id, agg.events);
            else
            {
                foreach(var elem in agg.events)
                {
                    dict[agg.Id].Add(elem);
                }
            }
        }
    }
}
