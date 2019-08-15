using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.DomainModel;

namespace DDD.DomainService
{
    interface IRepository<T> where T: AggregateRoot
    {
        void SaveEvents(AggregateRoot agg);
        T GetbyID(Guid id);
    }
}
