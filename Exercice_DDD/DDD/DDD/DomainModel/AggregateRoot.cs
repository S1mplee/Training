using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.DomainModel
{
    public class AggregateRoot
    {
        public Guid Id;
        public readonly List<Event> events = new List<Event>();
    }
}
